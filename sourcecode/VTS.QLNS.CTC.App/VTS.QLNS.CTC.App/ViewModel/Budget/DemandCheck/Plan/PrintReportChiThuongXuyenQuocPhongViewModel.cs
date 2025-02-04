using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
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
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PrintReportChiThuongXuyenQuocPhongViewModel : ViewModelBase
    {
        private ICpChungTuService _chungTuService;
        private IMapper _mapper;
        private ICollectionView _listDonViView;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private ISktSoLieuService _sktSoLieuService;
        private SoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private INsPhongBanService _phongBanService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsDonViService _nsDonViService;
        private ISktSoLieuChungTuService _sktChungTuService;
        private INsNguoiDungDonViService _nsNguoiDungDonViService;
        private INsMucLucNganSachService _nSMucLucNganSachService;
        private INsQtChungTuService _iQtChungTuService;
        private INsQtChungTuChiTietService _iQtChungTuChiTietService;
        private ICauHinhCanCuService _iCauHinhCanCuService;
        private ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private DanhMucCauHinhHeThongService _danhMucCauHinhHeThongService;
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private double _tongTongSoNamNay;
        private double _tongHienVatNamNay;
        private double _tongBangTienNamNay;
        private double _tongChuaPhanCapNamNay;
        private double _tongTongSoNam1;
        private double _tongHienVatNam1;
        private double _tongBangTienNam1;
        private double _tongTongSoNam2;
        private double _tongHienVatNam2;
        private double _tongBangTienNam2;

        private double _tongTongSo;
        private double _tongTongMuaTrongNuocNhapKhau;
        private double _tongMuaTrongNuoc;
        private double _tongNhapKhau;
        private double _tongTongPhanBo;
        private double _tongDaPhanBo;
        private double _tongChuaPhanBo;
        private string _chiTietToi;
        private string _typeChuKy;
        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.PrintReportChiTietDuToanDonVi);
        public ObservableCollection<CheckBoxItem> ListNganh { get; set; }

        public bool IsEnableListDonVi => (LoaiBaoCao != null && LoaiBaoCao.Where(n => n.IsChecked && n.DisplayItem == VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_DONVI0).Count() > 0)
            ? false : true;

        private ObservableCollection<CheckBoxItem> _loaiBaoCao;
        public ObservableCollection<CheckBoxItem> LoaiBaoCao
        {
            get => _loaiBaoCao;
            set => SetProperty(ref _loaiBaoCao, value);
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
            set => SetProperty(ref _selectedBQuanLy, value);
        }


        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
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
        public bool IsCheckInToiMuc => !(LoaiBaoCao != null && LoaiBaoCao.Any(e => e.IsChecked && (e.DisplayItem == VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_NGANG || e.DisplayItem == VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC)));

        public bool IsShowInMotTo => (LoaiBaoCao != null && LoaiBaoCao.Any(e => e.IsChecked && e.DisplayItem == VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC));

        private bool _isInMotTo;
        public bool IsInMotTo
        {
            get => _isInMotTo;
            set => SetProperty(ref _isInMotTo, value);
        }
        //private string _loaiChungTu;
        //public string LoaiChungTu
        //{
        //    get => _loaiChungTu;
        //    set => SetProperty(ref _loaiChungTu, value);
        //}

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

        public string SelectedCountNganh
        {
            get
            {
                int totalCount = ListNganh != null ? ListNganh.Count : 0;
                int totalSelected = ListNganh != null ? ListNganh.Count(item => item.IsChecked) : 0;
                return string.Format("NGÀNH ({0}/{1})", totalSelected, totalCount);
            }
        }

        public bool? SelectAllNganh
        {
            get
            {
                if (ListNganh != null)
                {
                    var selected = ListNganh.Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ListNganh);
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

        public bool IsEnableButtonPrint => ListNganh != null && ListNganh.Where(n => n.IsChecked).Any();

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

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadNganh();
            }
        }

        public bool IsShowInTheoTongHop => _sessionService.Current.IsQuanLyDonViCha;

        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }

        public NsDacThuNganhGhiChuDialogViewModel NsDacThuNganhGhiChuDialogViewModel { get; }



        public PrintReportChiThuongXuyenQuocPhongViewModel(
         ICpChungTuService chungTuService,
         INsDonViService donViService,
         IMapper mapper,
         INsDonViService nSDonViService,
         ISessionService sessionService,
         SoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
         INsDonViService nsDonViService,
         ILog logger,
         ISktSoLieuService sktSoLieuService,
         IDanhMucService danhMucService,
         IExportService exportService,
         INsNguoiDungDonViService nsNguoiDungDonViService,
         ICauHinhCanCuService iCauHinhCanCuService,
         INsPhongBanService phongBanService,
         IDmChuKyService dmChuKyService,
         INsMucLucNganSachService nSMucLucNganSachService,
         ISktSoLieuChungTuService sktChungTuService,
         INsMucLucNganSachService iNsMucLucNganSachService,
         INsQtChungTuService iQtChungTuService,
         INsQtChungTuChiTietService iQtChungTuChiTietService,
         ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuService,
         DanhMucCauHinhHeThongService danhMucCauHinhHeThongService,
         DmChuKyDialogViewModel dmChuKyDialogViewModel,
         NsDacThuNganhGhiChuDialogViewModel nsDacThuNganhGhiChuDialogViewModel,
         INsBaoCaoGhiChuService ghiChuService)
        {
            _chungTuService = chungTuService;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _sktSoLieuService = sktSoLieuService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _nsDonViService = nsDonViService;
            _phongBanService = phongBanService;
            _sktChungTuService = sktChungTuService;
            _iQtChungTuService = iQtChungTuService;
            _iQtChungTuChiTietService = iQtChungTuChiTietService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _nSMucLucNganSachService = nSMucLucNganSachService;
            _danhMucCauHinhHeThongService = danhMucCauHinhHeThongService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _sktSoLieuChiTietCanCuService = sktSoLieuChiTietCanCuService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            NsDacThuNganhGhiChuDialogViewModel = nsDacThuNganhGhiChuDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => PrintPDF());
            PrintExcelCommand = new RelayCommand(o => PrintExcel());
            PrintBrowserCommand = new RelayCommand(o => OnPrintBrowser());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
            _ghiChuService = ghiChuService;
        }

        public override void Init()
        {
            try
            {
                _isInTheoTongHop = false;
                InitReportDefaultDate();
                LoadCombobox();
                LoadNganh();
                LoadChiTietToi();
                LoadBQuanLy();
                LoadTypeChuKy();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnNoteCommand()
        {
            NsDacThuNganhGhiChuDialogViewModel.IsInTheoTongHop = IsInTheoTongHop;
            NsDacThuNganhGhiChuDialogViewModel.LoaiBaoCao = ObjectCopier.Clone(LoaiBaoCao);
            NsDacThuNganhGhiChuDialogViewModel.BQuanLyItems = BQuanLy;
            NsDacThuNganhGhiChuDialogViewModel.BQuanLySelected = SelectedBQuanLy;
            NsDacThuNganhGhiChuDialogViewModel.SMaBaoCao = _typeChuKy;
            NsDacThuNganhGhiChuDialogViewModel.Init();
            NsDacThuNganhGhiChuDialogViewModel.ShowDialogHost("PrintReportChiThuongXuyenQuocPhong");
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TieuDe3 = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadBQuanLy()
        {
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            List<DmBQuanLy> listPhongBan = _phongBanService.FindByCondition(predicate);
            _bQuanLy = _mapper.Map<List<ComboboxItem>>(listPhongBan);
            if (_bQuanLy.Count() > 0)
            {
                _bQuanLy.Insert(0, new ComboboxItem("Tất cả", string.Empty));
                SelectedBQuanLy = _bQuanLy.FirstOrDefault();
            }
        }

        public void LoadCombobox()
        {
            LoaiBaoCao = new ObservableCollection<CheckBoxItem>();
            LoaiBaoCao.Add(new CheckBoxItem { IsChecked = true, DisplayItem = VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DU_TOAN_NAM });
            LoaiBaoCao.Add(new CheckBoxItem { IsChecked = false, DisplayItem = VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_TIET_THEO_DON_VI_SU_DUNG });
            LoaiBaoCao.Add(new CheckBoxItem { IsChecked = false, DisplayItem = VTS.QLNS.CTC.Utility.LoaiBaoCao.TONG_HOP_DU_TOAN_NGAN_SACH_DAC_THU_NGANH });
            LoaiBaoCao.Add(new CheckBoxItem { IsChecked = false, DisplayItem = VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC });
            LoaiBaoCao.Add(new CheckBoxItem { IsChecked = false, DisplayItem = VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_NGANG });
            LoaiBaoCao.Add(new CheckBoxItem { IsChecked = false, DisplayItem = VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_CHIMUAHANGTAPTRUNG_CAPHIENVAT });
            foreach (var item in LoaiBaoCao)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        LoadTypeChuKy();
                        OnPropertyChanged(nameof(IsEnableListDonVi));
                        OnPropertyChanged(nameof(IsCheckInToiMuc));
                        OnPropertyChanged(nameof(IsShowInMotTo));
                    }
                };
            }

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

        private void LoadNganh()
        {
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).ToList();
            var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoaiChungTu == VoucherType.NSBD_Key);
            if (IsInTheoTongHop)
            {
                predicate = predicate.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            }
            else
            {
                string loaiChungTu = VoucherType.NSBD_Key;
                List<DonViPlanBeginYearQuery> data = _nsDonViService.FindPlanBeginYearByConditon
                    (_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, loaiChungTu, 0, _sessionService.Current.Principal).ToList();
                var lstIdChungTu = data.Select(x => x.Id).Distinct().ToList();
                predicate = predicate.And(x => !x.IIdMaDonVi.Equals(donvi0.IIDMaDonVi));
                predicate = predicate.And(x => lstIdChungTu.Contains(x.IIdCtdtdauNam));
            }
            List<NsDtdauNamChungTuChiTiet> listDataChiTiet = _sktSoLieuService.FindByCondition(predicate).OrderBy(x => x.SNg).ToList();
            if (listDataChiTiet.Any())
            {
                ListNganh = new ObservableCollection<CheckBoxItem>();
                foreach (string nganh in listDataChiTiet.Where(n => !string.IsNullOrEmpty(n.SNg)).Select(n => n.SNg).Distinct().ToList())
                {
                    string tenNganh = listDanhMuc.FirstOrDefault(n => n.IIDMaDanhMuc == nganh) != null ?
                        listDanhMuc.FirstOrDefault(n => n.IIDMaDanhMuc == nganh).STen : string.Empty;
                    ListNganh.Add(new CheckBoxItem { ValueItem = nganh, DisplayItem = string.Format("{0}-{1}", nganh, tenNganh) });
                }
            }
            else
            {
                ListNganh = new ObservableCollection<CheckBoxItem>();
            }

            _listDonViView = CollectionViewSource.GetDefaultView(ListNganh);
            _listDonViView.Filter = ListDonViFilter;
            foreach (var model in ListNganh)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountNganh));
                        OnPropertyChanged(nameof(SelectAllNganh));
                        OnPropertyChanged(nameof(IsEnableButtonPrint));
                    }
                };
            }
            OnPropertyChanged(nameof(IsEnableButtonPrint));
            OnPropertyChanged(nameof(ListNganh));
            OnPropertyChanged(nameof(SelectedCountNganh));
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

        private void PrintExcel()
        {
            try
            {
                string loaiBaoCao = LoaiBaoCao.Where(n => n.IsChecked).FirstOrDefault().DisplayItem;
                switch (loaiBaoCao)
                {
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DU_TOAN_NAM:
                        PrintReportDuToanDuToanNam(ExportType.EXCEL);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_TIET_THEO_DON_VI_SU_DUNG:
                        PrintReportDuToanChiTietDonViSuDung(ExportType.EXCEL);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC:
                        if (IsInMotTo)
                        {
                            OnPrintReportDuToanPhanBoNganSachDacThuChoDonViDocOnePage(ExportType.EXCEL);
                        }
                        else
                        {
                            OnPrintReportDuToanPhanBoNganSachDacThuChoDonViDoc(ExportType.EXCEL);
                        }
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_NGANG:
                        OnPrintReportDuToanPhanBoNganSachDacThuChoDonVi(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_CHIMUAHANGTAPTRUNG_CAPHIENVAT:
                        PrintReportDuToanChiMuaHangTapTrung(ExportType.EXCEL);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.TONG_HOP_DU_TOAN_NGAN_SACH_DAC_THU_NGANH:
                        PrintReportTongHopDuToanDacThu(ExportType.PDF);
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
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DU_TOAN_NAM:
                        PrintReportDuToanDuToanNam(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_TIET_THEO_DON_VI_SU_DUNG:
                        PrintReportDuToanChiTietDonViSuDung(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC:
                        OnPrintReportDuToanPhanBoNganSachDacThuChoDonViDoc(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_NGANG:
                        OnPrintReportDuToanPhanBoNganSachDacThuChoDonVi(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_CHIMUAHANGTAPTRUNG_CAPHIENVAT:
                        PrintReportDuToanChiMuaHangTapTrung(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.TONG_HOP_DU_TOAN_NGAN_SACH_DAC_THU_NGANH:
                        PrintReportTongHopDuToanDacThu(ExportType.PDF);
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
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DU_TOAN_NAM:
                        PrintReportDuToanDuToanNam(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_TIET_THEO_DON_VI_SU_DUNG:
                        PrintReportDuToanChiTietDonViSuDung(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.TONG_HOP_DU_TOAN_NGAN_SACH_DAC_THU_NGANH:
                        PrintReportTongHopDuToanDacThu(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC:
                        OnPrintReportDuToanPhanBoNganSachDacThuChoDonViDoc(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_NGANG:
                        OnPrintReportDuToanPhanBoNganSachDacThuChoDonVi(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_CHIMUAHANGTAPTRUNG_CAPHIENVAT:
                        PrintReportDuToanChiMuaHangTapTrung(ExportType.PDF);
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

        private List<NsDtdauNamChungTuChiTietReportModel> GetDataPrintDuToanNamDonVi(List<string> listNganh, double donViTinh)
        {
            List<DuToanDauNamChiThuongXuyenChiTietModel> result = new List<DuToanDauNamChiThuongXuyenChiTietModel>();
            if (ListNganh == null || ListNganh.Count() == 0)
            {
                return new List<NsDtdauNamChungTuChiTietReportModel>();
            }
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);

            List<ReportChungTuDacThuDauNamPhanCapQuery> listDataChiTiet = _sktSoLieuService.GetDataBaoCaoDuToanPhanBoNganSachDacThuPhanCap(listNganh, _sessionService.Current.YearOfWork,
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, int.Parse(VoucherType.NSBD_Key), donvi0.IIDMaDonVi, IsInTheoTongHop).ToList();


            listDataChiTiet.Select(n =>
            {
                n.TuChi = n.TuChi / donViTinh;
                return n;
            }).ToList();
            return _mapper.Map<List<NsDtdauNamChungTuChiTietReportModel>>(listDataChiTiet); ;
        }

        private List<NsMucLucNganSach> GetMLNS()
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            List<NsMucLucNganSach> data = _nSMucLucNganSachService.FindByCondition(predicate).OrderBy(n => n.XauNoiMa).ToList();
            return data;
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
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

        public static IEnumerable<List<T>> SplitList2<T>(List<T> bigList, int nSize = 4)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                if (i == 0)
                {
                    yield return bigList.GetRange(0, Math.Min(nSize - 1, bigList.Count - i));
                    i--;
                }
                else
                {
                    yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
                }
            }
        }

        public void OnPrintReportDuToanPhanBoNganSachDacThuChoDonViDocOnePage(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    if (ListNganh == null || ListNganh.Where(item => item.IsChecked).Count() == 0)
                    {
                        return;
                    }
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).ToList();
                    var lstNsMucLucNganSach = _nSMucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai == StatusType.ACTIVE);
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    int donViTinh = GetDonViTinh();
                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var yearOfBudget = _sessionService.Current.YearOfBudget;
                    var budgetSource = _sessionService.Current.Budget;
                    //var donviCap2 = _nsDonViService.FindByListDonViCap2ToanQuanKhacCha(_sessionService.Current.YearOfWork).Select(x => x.IIDMaDonVi).ToList();

                    var h1 = string.Empty;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportDuToanDauNamTheoNganhPhuLucQuery> listDataDonViPrint = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                    var lstIdCodeSelected = "";
                    if (ListNganh != null)
                    {
                        lstIdCodeSelected = string.Join(",", ListNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                    }
                    if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                    {
                        listDataDonViPrint = _sktSoLieuService.FindReportDuToanDauNamPhanCapTheoNganh(lstIdCodeSelected, null, null, yearOfWork, yearOfBudget, budgetSource, 0, donViTinh, IsInTheoTongHop)
                        .OrderBy(n => n.sNG).ToList();
                    }
                    if (listDataDonViPrint == null || listDataDonViPrint.Count() == 0)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("TieuDe1", TieuDe1.ToUpper());
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        //data.Add("Ngay", DateUtils.GetCurrentDateReport());
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", "");
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty) + " - Tờ: 1");
                        data.Add("h2", "");
                        data.Add("Nam", _sessionService.Current.YearOfWork);
                        data.Add("TongTienBangChu", StringUtils.NumberToText(0, true));
                        data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                        var ghiChu = GetGhiChu();
                        data.Add("HasGhiChu", ghiChu.Any());
                        data.Add("ListGhiChu", ghiChu);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_DONVI_DOC_TRANG1_EMPTY);
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<NsDtdauNamChungTuDacThuDonViReportModel, NsDtdauNamChungTuDacThuDonViHeaderReportModel, GhiChu>(templateFileName, data);
                        exportResults.Add(new ExportResult("Tổng hợp dự toán ngân sách đặc thù cho chác đơn vị", fileNameWithoutExtension, null, xlsFile));

                    }
                    else
                    {
                        List<DonVi> lstDonVi = new List<DonVi>();
                        var listIdDonVi = string.Join(",", listDataDonViPrint.Select(x => x.IdDonVi).Distinct().ToList());
                        var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();

                        //var lstDonViTrucThuoc = listDonVi.Where(x => !donviCap2.Contains(x.IIDMaDonVi)).ToList();
                        //var lstDonViBanThan = listDonVi.Where(x => donviCap2.Contains(x.IIDMaDonVi)).ToList();
                        //if (!lstDonViTrucThuoc.Any())
                        //{
                        //    lstDonVi = lstDonViBanThan;
                        //}
                        //else
                        //{
                        //    lstDonVi = lstDonViTrucThuoc;
                        //}

                        listDonVi.Insert(0, new DonVi()
                        {
                            IIDMaDonVi = "ALL",
                            TenDonVi = "Tổng số"
                        });

                        //lstDonVi.Insert(1, new DonVi()
                        //{
                        //    IIDMaDonVi = "SAME",
                        //    TenDonVi = "Bản thân"
                        //});

                        //lstDonVi = lstDonVi.Select((value, index) =>
                        //{
                        //    if (index == 0)
                        //    {
                        //        value.STT = "1=2+3+...";
                        //    }
                        //    else
                        //    {
                        //        value.STT = (index + 1).ToString();
                        //    }
                        //    return value;
                        //}).ToList();
                        //var listXauNoiMa = listDataDonViPrint.Select(x => x.sXauNoiMa).Distinct().ToList();
                        //var listDonViSplits = SplitList(lstDonVi, 6).ToList();
                        //if (exportType == ExportType.EXCEL)
                        //{
                        //    int countDonVi = lstDonVi.Count();
                        //    listDonViSplits = SplitList(lstDonVi, 6).ToList();
                        //}


                        listDonVi = listDonVi.Select((value, index) =>
                        {
                            if (index == 0)
                            {
                                value.STT = "1=2+3+...";
                            }
                            else
                            {
                                value.STT = (index + 1).ToString();
                            }
                            return value;
                        }).ToList();
                        var listXauNoiMa = listDataDonViPrint.Select(x => x.sXauNoiMa).Distinct().ToList();
                        var listDonViSplits = SplitList(listDonVi, listDonVi.Count).ToList();
                        //if (exportType == ExportType.EXCEL)
                        //{
                        //    int countDonVi = listDonVi.Count();
                        //    listDonViSplits = SplitList(listDonVi, 6).ToList();
                        //}

                        List<string> lstXauNoiMaToiTTM = new List<string>();
                        List<string> lstXauNoiMaToiTM = new List<string>();
                        List<string> lstXauNoiMaToiM = new List<string>();
                        foreach (var item in listXauNoiMa)
                        {
                            var sXauNoiMa = StringUtils.SplitXauNoiMaParent(item);
                            if (sXauNoiMa != null)
                            {
                                sXauNoiMa.RemoveAt(sXauNoiMa.Count - 1);
                                lstXauNoiMaToiTTM.Add(sXauNoiMa.LastOrDefault());
                                sXauNoiMa.RemoveAt(sXauNoiMa.Count - 1);
                                lstXauNoiMaToiTM.Add(sXauNoiMa.LastOrDefault());
                                sXauNoiMa.RemoveAt(sXauNoiMa.Count - 1);
                                lstXauNoiMaToiM.Add(sXauNoiMa.LastOrDefault());
                            }
                        }

                        var lstMucLucNganSachToiTTM = lstNsMucLucNganSach.Where(x => lstXauNoiMaToiTTM.Contains(x.SXauNoiMa) && x.BHangCha).ToList();
                        var lstMucLucNganSachToiTM = lstNsMucLucNganSach.Where(x => lstXauNoiMaToiTM.Contains(x.SXauNoiMa) && x.BHangCha).ToList();
                        var lstMucLucNganSachToiM = lstNsMucLucNganSach.Where(x => lstXauNoiMaToiM.Contains(x.SXauNoiMa) && x.BHangCha).ToList();

                        List<NsMucLucNganSach> lstMucLucSortTTM = lstMucLucNganSachToiTTM.OrderBy(items => lstXauNoiMaToiTTM.IndexOf(items.SXauNoiMa)).ToList();
                        List<NsMucLucNganSach> lstMucLucSortTM = lstMucLucNganSachToiTM.OrderBy(items => lstXauNoiMaToiTM.IndexOf(items.SXauNoiMa)).ToList();
                        List<NsMucLucNganSach> lstMucLucSortM = lstMucLucNganSachToiM.OrderBy(items => lstXauNoiMaToiM.IndexOf(items.SXauNoiMa)).ToList();

                        for (int i = 0; i < listDonViSplits.Count; i++)
                        {
                            if (listDonViSplits[i].Count < 6)
                            {
                                var coutRowEmpty = 6 - listDonViSplits[i].Count;
                                for (int j = 0; j < coutRowEmpty; j++)
                                {
                                    DonVi empty = new DonVi();
                                    listDonViSplits[i].Add(empty);
                                }
                            }



                            List<ReportDuToanDauNamTheoNganhPhuLucQuery> results = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                            foreach (var xauNoiMa in listXauNoiMa)
                            {
                                var first = listDataDonViPrint.FirstOrDefault(x => x.sXauNoiMa == xauNoiMa);
                                ReportDuToanDauNamTheoNganhPhuLucQuery kq = first;
                                kq.LstGiaTri = new List<NsDtdauNamChungTuChiTiet>();
                                kq.TongCong = listDataDonViPrint.Where(x => x.sXauNoiMa == xauNoiMa).Sum(x => x.TuChi);
                                foreach (var dv in listDonViSplits[i])
                                {
                                    NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                    if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                    {
                                        if (dv.IIDMaDonVi == "ALL")
                                        {
                                            giaTri.FTuChi = listDataDonViPrint.Where(x => x.sXauNoiMa == xauNoiMa).Sum(x => x.TuChi);
                                            kq.LstGiaTri.Add(giaTri);
                                        }
                                        //if (dv.IIDMaDonVi == "SAME")
                                        //{
                                        //    giaTri = new NsDtdauNamChungTuChiTiet();
                                        //    var gtDonVi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sXauNoiMa == xauNoiMa);
                                        //    giaTri.FTuChi = gtDonVi != null ? gtDonVi.Sum(x => x.TuChi) : 0;
                                        //    kq.LstGiaTri.Add(giaTri);
                                        //}
                                        else
                                        {
                                            giaTri = new NsDtdauNamChungTuChiTiet();
                                            var gtDonVi = listDataDonViPrint.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.sXauNoiMa == xauNoiMa);
                                            giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                            kq.LstGiaTri.Add(giaTri);
                                        }
                                    }
                                    else
                                    {
                                        kq.LstGiaTri.Add(giaTri);
                                    }

                                }
                                results.Add(kq);
                            }

                            results = results.OrderBy(x => x.sL).ThenBy(x => x.sK).ThenBy(x => x.sM).ToList();
                            // Add nganh
                            int number = 1;
                            foreach (var nganh in listDanhMuc)
                            {
                                var listData = results.Where(x => x.sNG == nganh.IIDMaDanhMuc);
                                if (listData is null || !listData.Any()) continue;
                                int index = results.IndexOf(listData.FirstOrDefault());
                                if (index >= 0)
                                {
                                    var kq = new ReportDuToanDauNamTheoNganhPhuLucQuery()
                                    {
                                        //sMoTa = $"{number++}. {nganh.STen}",
                                        ILoai = number++,
                                        sMoTa = $"{nganh.IIDMaDanhMuc}. {nganh.STen}",
                                        BHangCha = true,
                                    };
                                    foreach (var dv in listDonViSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                        {
                                            if (dv.IIDMaDonVi == "ALL")
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            //else if (dv.IIDMaDonVi == "SAME")
                                            //{
                                            //    giaTri.FTuChi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                            //    kq.LstGiaTri.Add(giaTri);
                                            //}
                                            else
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        else
                                        {
                                            kq.LstGiaTri.Add(giaTri);
                                        }

                                    }
                                    results.Insert(index, kq);
                                }
                            }

                            // Merge len dong TTM
                            foreach (var toiMuc in lstMucLucSortTTM)
                            {
                                var listData = results.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm && x.sTTM == toiMuc.Ttm);
                                if (listData is null || !listData.Any()) continue;
                                int index = results.IndexOf(listData.FirstOrDefault());
                                if (index >= 0)
                                {
                                    var kq = new ReportDuToanDauNamTheoNganhPhuLucQuery()
                                    {
                                        //sMoTa = $"{number++}. {nganh.STen}",
                                        sMoTa = toiMuc.MoTa,
                                        sLNS = toiMuc.Lns,
                                        sL = toiMuc.L,
                                        sK = toiMuc.K,
                                        sM = toiMuc.M,
                                        sTM = toiMuc.Tm,
                                        sTTM = toiMuc.Ttm,
                                        sXauNoiMa = toiMuc.SXauNoiMa,
                                        //iID_MLNS = toiMuc.MlnsId,
                                        //iID_MLNS_Cha = toiMuc.MlnsIdParent,
                                        BHangCha = false,
                                    };
                                    foreach (var dv in listDonViSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                        {
                                            if (dv.IIDMaDonVi == "ALL")
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm && x.sTTM == toiMuc.Ttm).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            //else if (dv.IIDMaDonVi == "SAME")
                                            //{
                                            //    giaTri.FTuChi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                            //    kq.LstGiaTri.Add(giaTri);
                                            //}
                                            else
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm && x.sTTM == toiMuc.Ttm).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        else
                                        {
                                            kq.LstGiaTri.Add(giaTri);
                                        }

                                    }
                                    results.Insert(index, kq);
                                }
                            }

                            // Merge len dong TM
                            foreach (var toiMuc in lstMucLucSortTM)
                            {
                                var listData = results.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm);
                                if (listData is null || !listData.Any()) continue;
                                int index = results.IndexOf(listData.FirstOrDefault());
                                if (index >= 0)
                                {
                                    var kq = new ReportDuToanDauNamTheoNganhPhuLucQuery()
                                    {
                                        //sMoTa = $"{number++}. {nganh.STen}",
                                        sMoTa = toiMuc.MoTa,
                                        sLNS = toiMuc.Lns,
                                        sL = toiMuc.L,
                                        sK = toiMuc.K,
                                        sM = toiMuc.M,
                                        sTM = toiMuc.Tm,
                                        sTTM = toiMuc.Ttm,
                                        sXauNoiMa = toiMuc.SXauNoiMa,
                                        //iID_MLNS = toiMuc.MlnsId,
                                        //iID_MLNS_Cha = toiMuc.MlnsIdParent,
                                        BHangCha = true,
                                    };
                                    foreach (var dv in listDonViSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                        {
                                            if (dv.IIDMaDonVi == "ALL")
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            //else if (dv.IIDMaDonVi == "SAME")
                                            //{
                                            //    giaTri.FTuChi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                            //    kq.LstGiaTri.Add(giaTri);
                                            //}
                                            else
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        else
                                        {
                                            kq.LstGiaTri.Add(giaTri);
                                        }

                                    }
                                    results.Insert(index, kq);
                                }
                            }

                            // Merge len dong M
                            foreach (var toiMuc in lstMucLucSortM)
                            {
                                var listData = results.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M);
                                if (listData is null || !listData.Any()) continue;
                                int index = results.IndexOf(listData.FirstOrDefault());
                                if (index >= 0)
                                {
                                    var kq = new ReportDuToanDauNamTheoNganhPhuLucQuery()
                                    {
                                        //sMoTa = $"{number++}. {nganh.STen}",
                                        sMoTa = toiMuc.MoTa,
                                        sLNS = toiMuc.Lns,
                                        sL = toiMuc.L,
                                        sK = toiMuc.K,
                                        sM = toiMuc.M,
                                        sTM = toiMuc.Tm,
                                        sTTM = toiMuc.Ttm,
                                        sXauNoiMa = toiMuc.SXauNoiMa,
                                        //iID_MLNS = toiMuc.MlnsId,
                                        //iID_MLNS_Cha = toiMuc.MlnsIdParent,
                                        BHangCha = true,
                                    };
                                    foreach (var dv in listDonViSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                        {
                                            if (dv.IIDMaDonVi == "ALL")
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            //else if (dv.IIDMaDonVi == "SAME")
                                            //{
                                            //    giaTri.FTuChi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                            //    kq.LstGiaTri.Add(giaTri);
                                            //}
                                            else
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        else
                                        {
                                            kq.LstGiaTri.Add(giaTri);
                                        }

                                    }
                                    results.Insert(index, kq);
                                }
                            }

                            // Dong tong cong ngang
                            List<ReportDuToanDauNamTheoNganhPhuLucQuery> resultsTotal = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                            ReportDuToanDauNamTheoNganhPhuLucQuery total = new ReportDuToanDauNamTheoNganhPhuLucQuery();
                            total.LstGiaTriTotal = new List<NsDtdauNamChungTuChiTiet>();
                            foreach (var dv in listDonViSplits[i])
                            {
                                NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                {
                                    if (dv.IIDMaDonVi == "ALL")
                                    {
                                        var gtDonVi = listDataDonViPrint.Where(x => !x.BHangCha.GetValueOrDefault(false)).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                    //else if (dv.IIDMaDonVi == "SAME")
                                    //{
                                    //    var gtDonVi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && !x.BHangCha.GetValueOrDefault(false)).Sum(x => x.TuChi);
                                    //    giaTri.FTuChi = gtDonVi;
                                    //    total.LstGiaTriTotal.Add(giaTri);
                                    //}
                                    else
                                    {
                                        var gtDonVi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && !x.BHangCha.GetValueOrDefault(false)).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }

                                }
                                else
                                {
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                            }
                            resultsTotal.Add(total);

                            // Xoa dong tong cong
                            listDonViSplits[0].RemoveAt(0);
                            resultsTotal[0].LstGiaTriTotal.RemoveAt(0);
                            // update iID_MLNS cho nganh
                            var lstResults = UpdateIdMucLucNganSach(results, exportType);
                            FormatDisplay(lstResults);
                            //var nameColunmMerge = GetExcelColumnName(listDonViSplits[i].Count() + 6);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", lstResults.OrderBy(x => x.ILoai).ToList());
                            data.Add("ListDataTotal", resultsTotal);
                            data.Add("TongCong", results.Where(x => x.iID_MLNS_Cha == null).Sum(x => x.TongCong));
                            //data.Add("SumTotal", sumTotal);
                            data.Add("TieuDe1", TieuDe1);
                            data.Add("TieuDe2", TieuDe2);
                            data.Add("TieuDe3", TieuDe3);
                            data.Add("Count", 10000);
                            //data.Add("Ngay", DateUtils.GetCurrentDateReport());
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", string.IsNullOrEmpty(GetLevelTitle(2)) ? "NGÀNH..." : GetLevelTitle(2));
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("h1", h1);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty) + " - Tờ: " + (i + 1));
                            data.Add("h2", "");
                            data.Add("Nam", _sessionService.Current.YearOfWork);
                            data.Add("Headers", listDonViSplits[i]);
                            //data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            //data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            //data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            //data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            //data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                            var ghiChu = GetGhiChu();
                            data.Add("HasGhiChu", ghiChu.Any());
                            data.Add("ListGhiChu", ghiChu);

                            data.Add("TongTienBangChu", StringUtils.NumberToText(listDataDonViPrint.Sum(x => x.TuChi) * donViTinh, true));
                            data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_PHANCAP_ONEPAGE);

                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportDuToanDauNamTheoNganhPhuLucQuery, DonVi, GhiChu>(templateFileName, data);
                            exportResults.Add(new ExportResult("Tổng hợp dự toán ngân sách đặc thù cho chác đơn vị - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
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

        private List<ReportDuToanDauNamTheoNganhPhuLucQuery> UpdateIdMucLucNganSach(List<ReportDuToanDauNamTheoNganhPhuLucQuery> results, ExportType exportType)
        {
            int number = 0;
            var lstResults = results.Where(x => string.IsNullOrEmpty(x.sNG)).ToList();
            foreach (var item in lstResults)
            {
                if (string.IsNullOrEmpty(item.sXauNoiMa))
                {
                    item.iID_MLNS = Guid.NewGuid();
                    number = item.ILoai;
                }
                else
                {
                    var parent = lstResults.Where(x => string.IsNullOrEmpty(x.sXauNoiMa) == string.IsNullOrEmpty(item.sTM)
                                && string.IsNullOrEmpty(x.sTM) == string.IsNullOrEmpty(item.sTTM)
                                && x.ILoai == number
                                && (string.IsNullOrEmpty(item.sTM) || x.SXauNoiMaToiM == item.SXauNoiMaToiM)
                                && (string.IsNullOrEmpty(item.sTTM) || x.SXauNoiMaToiTM == item.SXauNoiMaToiTM)).Select(x => x.iID_MLNS).FirstOrDefault();
                    item.iID_MLNS = Guid.NewGuid();
                    item.iID_MLNS_Cha = parent;
                    item.ILoai = number;
                }
            }

            if (IsInMotTo && exportType == ExportType.EXCEL)
            {
                // Xoa dong tong cong
                foreach (var item in lstResults)
                {
                    item.LstGiaTri.RemoveAt(0);
                }
            }

             // Chua toi ưu
            //for (int j = 0; j < lstResults.Count; j++)
            //{
            //    if (string.IsNullOrEmpty(lstResults[j].sXauNoiMa))
            //    {
            //        lstResults[j].iID_MLNS = Guid.NewGuid();
            //        number = lstResults[j].ILoai;
            //    }
            //    else
            //    {

            //        if (string.IsNullOrEmpty(lstResults[j].sTM) && string.IsNullOrEmpty(lstResults[j].sTTM))
            //        {
            //            lstResults[j].iID_MLNS = Guid.NewGuid();
            //            lstResults[j].iID_MLNS_Cha = lstResults.Where(x => string.IsNullOrEmpty(x.sXauNoiMa) && x.ILoai == number).Select(x => x.iID_MLNS).FirstOrDefault();
            //            lstResults[j].ILoai = number;

            //        }
            //        else if (!string.IsNullOrEmpty(lstResults[j].sTM) && string.IsNullOrEmpty(lstResults[j].sTTM))
            //        {
            //            lstResults[j].iID_MLNS = Guid.NewGuid();
            //            lstResults[j].iID_MLNS_Cha = lstResults.Where(x => !string.IsNullOrEmpty(x.sXauNoiMa) && string.IsNullOrEmpty(x.sTM) && string.IsNullOrEmpty(x.sTTM) && x.ILoai == number
            //                                         && x.SXauNoiMaToiM == lstResults[j].SXauNoiMaToiM).Select(x => x.iID_MLNS).FirstOrDefault();
            //            lstResults[j].ILoai = number;
            //        }
            //        else if (!string.IsNullOrEmpty(lstResults[j].sTM) && !string.IsNullOrEmpty(lstResults[j].sTTM))
            //        {
            //            lstResults[j].iID_MLNS = Guid.NewGuid();
            //            lstResults[j].iID_MLNS_Cha = lstResults.Where(x => !string.IsNullOrEmpty(x.sXauNoiMa) && !string.IsNullOrEmpty(x.sTM) && string.IsNullOrEmpty(x.sTTM) && x.ILoai == number
            //                                        && x.SXauNoiMaToiM == lstResults[j].SXauNoiMaToiM && x.SXauNoiMaToiTM == lstResults[j].SXauNoiMaToiTM).Select(x => x.iID_MLNS).FirstOrDefault();
            //            lstResults[j].ILoai = number;
            //        }

            //    }
            //}

            return lstResults;
        }

        public void OnPrintReportDuToanPhanBoNganSachDacThuChoDonViDoc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    if (ListNganh == null || ListNganh.Where(item => item.IsChecked).Count() == 0)
                    {
                        return;
                    }
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).ToList();
                    var lstNsMucLucNganSach = _nSMucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai == StatusType.ACTIVE);
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    int donViTinh = GetDonViTinh();
                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var yearOfBudget = _sessionService.Current.YearOfBudget;
                    var budgetSource = _sessionService.Current.Budget;
                    //var donviCap2 = _nsDonViService.FindByListDonViCap2ToanQuanKhacCha(_sessionService.Current.YearOfWork).Select(x => x.IIDMaDonVi).ToList();

                    var h1 = string.Empty;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportDuToanDauNamTheoNganhPhuLucQuery> listDataDonViPrint = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                    var lstIdCodeSelected = "";
                    if (ListNganh != null)
                    {
                        lstIdCodeSelected = string.Join(",", ListNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                    }
                    if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                    {
                        listDataDonViPrint = _sktSoLieuService.FindReportDuToanDauNamPhanCapTheoNganh(lstIdCodeSelected, null, null, yearOfWork, yearOfBudget, budgetSource, 0, donViTinh, IsInTheoTongHop)
                        .OrderBy(n => n.sNG).ToList();
                    }
                    if (listDataDonViPrint == null || listDataDonViPrint.Count() == 0)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("TieuDe1", TieuDe1.ToUpper());
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        //data.Add("Ngay", DateUtils.GetCurrentDateReport());
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", "");
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty) + " - Tờ: 1");
                        data.Add("h2", "");
                        data.Add("Nam", _sessionService.Current.YearOfWork);
                        data.Add("TongTienBangChu", StringUtils.NumberToText(0, true));
                        data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                        var ghiChu = GetGhiChu();
                        data.Add("HasGhiChu", ghiChu.Any());
                        data.Add("ListGhiChu", ghiChu);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_DONVI_DOC_TRANG1_EMPTY);
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<NsDtdauNamChungTuDacThuDonViReportModel, NsDtdauNamChungTuDacThuDonViHeaderReportModel, GhiChu>(templateFileName, data);
                        exportResults.Add(new ExportResult("Tổng hợp dự toán ngân sách đặc thù cho chác đơn vị", fileNameWithoutExtension, null, xlsFile));

                    }
                    else
                    {
                        List<DonVi> lstDonVi = new List<DonVi>();
                        var listIdDonVi = string.Join(",", listDataDonViPrint.Select(x => x.IdDonVi).Distinct().ToList());
                        var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();

                        //var lstDonViTrucThuoc = listDonVi.Where(x => !donviCap2.Contains(x.IIDMaDonVi)).ToList();
                        //var lstDonViBanThan = listDonVi.Where(x => donviCap2.Contains(x.IIDMaDonVi)).ToList();
                        //if (!lstDonViTrucThuoc.Any())
                        //{
                        //    lstDonVi = lstDonViBanThan;
                        //}
                        //else
                        //{
                        //    lstDonVi = lstDonViTrucThuoc;
                        //}

                        listDonVi.Insert(0, new DonVi()
                        {
                            IIDMaDonVi = "ALL",
                            TenDonVi = "Tổng số"
                        });

                        //lstDonVi.Insert(1, new DonVi()
                        //{
                        //    IIDMaDonVi = "SAME",
                        //    TenDonVi = "Bản thân"
                        //});

                        //lstDonVi = lstDonVi.Select((value, index) =>
                        //{
                        //    if (index == 0)
                        //    {
                        //        value.STT = "1=2+3+...";
                        //    }
                        //    else
                        //    {
                        //        value.STT = (index + 1).ToString();
                        //    }
                        //    return value;
                        //}).ToList();
                        //var listXauNoiMa = listDataDonViPrint.Select(x => x.sXauNoiMa).Distinct().ToList();
                        //var listDonViSplits = SplitList(lstDonVi, 6).ToList();
                        //if (exportType == ExportType.EXCEL)
                        //{
                        //    int countDonVi = lstDonVi.Count();
                        //    listDonViSplits = SplitList(lstDonVi, 6).ToList();
                        //}


                        listDonVi = listDonVi.Select((value, index) =>
                        {
                            if (index == 0)
                            {
                                value.STT = "1=2+3+...";
                            }
                            else
                            {
                                value.STT = (index + 1).ToString();
                            }
                            return value;
                        }).ToList();
                        var listXauNoiMa = listDataDonViPrint.Select(x => x.sXauNoiMa).Distinct().OrderBy(x => x).ToList();
                        var listDonViSplits = SplitList(listDonVi, 6).ToList();
                        if (exportType == ExportType.EXCEL)
                        {
                            int countDonVi = listDonVi.Count();
                            listDonViSplits = SplitList(listDonVi, 6).ToList();
                        }

                        List<string> lstXauNoiMaToiTTM = new List<string>();
                        List<string> lstXauNoiMaToiTM = new List<string>();
                        List<string> lstXauNoiMaToiM = new List<string>();
                        foreach (var item in listXauNoiMa)
                        {
                            var sXauNoiMa = StringUtils.SplitXauNoiMaParent(item);
                            if (sXauNoiMa != null)
                            {
                                sXauNoiMa.RemoveAt(sXauNoiMa.Count - 1);
                                lstXauNoiMaToiTTM.Add(sXauNoiMa.LastOrDefault());
                                sXauNoiMa.RemoveAt(sXauNoiMa.Count - 1);
                                lstXauNoiMaToiTM.Add(sXauNoiMa.LastOrDefault());
                                sXauNoiMa.RemoveAt(sXauNoiMa.Count - 1);
                                lstXauNoiMaToiM.Add(sXauNoiMa.LastOrDefault());
                            }
                        }

                        var lstMucLucNganSachToiTTM = lstNsMucLucNganSach.Where(x => lstXauNoiMaToiTTM.Contains(x.SXauNoiMa) && x.BHangCha).ToList();
                        var lstMucLucNganSachToiTM = lstNsMucLucNganSach.Where(x => lstXauNoiMaToiTM.Contains(x.SXauNoiMa) && x.BHangCha).ToList();
                        var lstMucLucNganSachToiM = lstNsMucLucNganSach.Where(x => lstXauNoiMaToiM.Contains(x.SXauNoiMa) && x.BHangCha).ToList();

                        List<NsMucLucNganSach> lstMucLucSortTTM = lstMucLucNganSachToiTTM.OrderBy(items => lstXauNoiMaToiTTM.IndexOf(items.SXauNoiMa)).ToList();
                        List<NsMucLucNganSach> lstMucLucSortTM = lstMucLucNganSachToiTM.OrderBy(items => lstXauNoiMaToiTM.IndexOf(items.SXauNoiMa)).ToList();
                        List<NsMucLucNganSach> lstMucLucSortM = lstMucLucNganSachToiM.OrderBy(items => lstXauNoiMaToiM.IndexOf(items.SXauNoiMa)).ToList();

                        for (int i = 0; i < listDonViSplits.Count; i++)
                        {
                            if (listDonViSplits[i].Count < 6)
                            {
                                var coutRowEmpty = 6 - listDonViSplits[i].Count;
                                for (int j = 0; j < coutRowEmpty; j++)
                                {
                                    DonVi empty = new DonVi();
                                    listDonViSplits[i].Add(empty);
                                }
                            }


                            List<ReportDuToanDauNamTheoNganhPhuLucQuery> results = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                            foreach (var xauNoiMa in listXauNoiMa)
                            {
                                var first = listDataDonViPrint.FirstOrDefault(x => x.sXauNoiMa == xauNoiMa);
                                ReportDuToanDauNamTheoNganhPhuLucQuery kq = first;
                                kq.LstGiaTri = new List<NsDtdauNamChungTuChiTiet>();
                                kq.TongCong = listDataDonViPrint.Where(x => x.sXauNoiMa == xauNoiMa).Sum(x => x.TuChi);
                                foreach (var dv in listDonViSplits[i])
                                {
                                    NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                    if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                    {
                                        if (dv.IIDMaDonVi == "ALL")
                                        {
                                            giaTri.FTuChi = listDataDonViPrint.Where(x => x.sXauNoiMa == xauNoiMa).Sum(x => x.TuChi);
                                            kq.LstGiaTri.Add(giaTri);
                                        }
                                        //if (dv.IIDMaDonVi == "SAME")
                                        //{
                                        //    giaTri = new NsDtdauNamChungTuChiTiet();
                                        //    var gtDonVi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sXauNoiMa == xauNoiMa);
                                        //    giaTri.FTuChi = gtDonVi != null ? gtDonVi.Sum(x => x.TuChi) : 0;
                                        //    kq.LstGiaTri.Add(giaTri);
                                        //}
                                        else
                                        {
                                            giaTri = new NsDtdauNamChungTuChiTiet();
                                            var gtDonVi = listDataDonViPrint.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.sXauNoiMa == xauNoiMa);
                                            giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                            kq.LstGiaTri.Add(giaTri);
                                        }
                                    }
                                    else
                                    {
                                        kq.LstGiaTri.Add(giaTri);
                                    }

                                }
                                results.Add(kq);
                            }

                            results = results.OrderBy(x => x.sL).ThenBy(x => x.sK).ThenBy(x => x.sM).ToList();
                            // Add nganh
                            int number = 1;
                            foreach (var nganh in listDanhMuc)
                            {
                                var listData = results.Where(x => x.sNG == nganh.IIDMaDanhMuc);
                                if (listData is null || !listData.Any()) continue;
                                int index = results.IndexOf(listData.FirstOrDefault());
                                if (index >= 0)
                                {
                                    var kq = new ReportDuToanDauNamTheoNganhPhuLucQuery()
                                    {
                                        //sMoTa = $"{number++}. {nganh.STen}",
                                        ILoai = number++,
                                        sMoTa = $"{nganh.IIDMaDanhMuc}. {nganh.STen}",
                                        BHangCha = true,
                                    };
                                    foreach (var dv in listDonViSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                        {
                                            if (dv.IIDMaDonVi == "ALL")
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            //else if (dv.IIDMaDonVi == "SAME")
                                            //{
                                            //    giaTri.FTuChi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                            //    kq.LstGiaTri.Add(giaTri);
                                            //}
                                            else
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        else
                                        {
                                            kq.LstGiaTri.Add(giaTri);
                                        }

                                    }
                                    results.Insert(index, kq);
                                }
                            }

                            // Merge len dong TTM
                            foreach (var toiMuc in lstMucLucSortTTM)
                            {
                                var listData = results.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm && x.sTTM == toiMuc.Ttm);
                                if (listData is null || !listData.Any()) continue;
                                int index = results.IndexOf(listData.FirstOrDefault());
                                if (index >= 0)
                                {
                                    var kq = new ReportDuToanDauNamTheoNganhPhuLucQuery()
                                    {
                                        //sMoTa = $"{number++}. {nganh.STen}",
                                        sMoTa = toiMuc.MoTa,
                                        sLNS = toiMuc.Lns,
                                        sL = toiMuc.L,
                                        sK = toiMuc.K,
                                        sM = toiMuc.M,
                                        sTM = toiMuc.Tm,
                                        sTTM = toiMuc.Ttm,
                                        sXauNoiMa = toiMuc.SXauNoiMa,
                                        iID_MLNS = toiMuc.MlnsId,
                                        iID_MLNS_Cha = toiMuc.MlnsIdParent,
                                        BHangCha = false,
                                    };
                                    foreach (var dv in listDonViSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                        {
                                            if (dv.IIDMaDonVi == "ALL")
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm && x.sTTM == toiMuc.Ttm).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            //else if (dv.IIDMaDonVi == "SAME")
                                            //{
                                            //    giaTri.FTuChi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                            //    kq.LstGiaTri.Add(giaTri);
                                            //}
                                            else
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm && x.sTTM == toiMuc.Ttm).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        else
                                        {
                                            kq.LstGiaTri.Add(giaTri);
                                        }

                                    }
                                    results.Insert(index, kq);
                                }
                            }

                            // Merge len dong TM
                            foreach (var toiMuc in lstMucLucSortTM)
                            {
                                var listData = results.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm);
                                if (listData is null || !listData.Any()) continue;
                                int index = results.IndexOf(listData.FirstOrDefault());
                                if (index >= 0)
                                {
                                    var kq = new ReportDuToanDauNamTheoNganhPhuLucQuery()
                                    {
                                        //sMoTa = $"{number++}. {nganh.STen}",
                                        sMoTa = toiMuc.MoTa,
                                        sLNS = toiMuc.Lns,
                                        sL = toiMuc.L,
                                        sK = toiMuc.K,
                                        sM = toiMuc.M,
                                        sTM = toiMuc.Tm,
                                        sTTM = toiMuc.Ttm,
                                        sXauNoiMa = toiMuc.SXauNoiMa,
                                        iID_MLNS = toiMuc.MlnsId,
                                        iID_MLNS_Cha = toiMuc.MlnsIdParent,
                                        BHangCha = true,
                                    };
                                    foreach (var dv in listDonViSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                        {
                                            if (dv.IIDMaDonVi == "ALL")
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            //else if (dv.IIDMaDonVi == "SAME")
                                            //{
                                            //    giaTri.FTuChi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                            //    kq.LstGiaTri.Add(giaTri);
                                            //}
                                            else
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M && x.sTM == toiMuc.Tm).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        else
                                        {
                                            kq.LstGiaTri.Add(giaTri);
                                        }

                                    }
                                    results.Insert(index, kq);
                                }
                            }

                            // Merge len dong M
                            foreach (var toiMuc in lstMucLucSortM)
                            {
                                var listData = results.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M);
                                if (listData is null || !listData.Any()) continue;
                                int index = results.IndexOf(listData.FirstOrDefault());
                                if (index >= 0)
                                {
                                    var kq = new ReportDuToanDauNamTheoNganhPhuLucQuery()
                                    {
                                        //sMoTa = $"{number++}. {nganh.STen}",
                                        sMoTa = toiMuc.MoTa,
                                        sLNS = toiMuc.Lns,
                                        sL = toiMuc.L,
                                        sK = toiMuc.K,
                                        sM = toiMuc.M,
                                        sTM = toiMuc.Tm,
                                        sTTM = toiMuc.Ttm,
                                        sXauNoiMa = toiMuc.SXauNoiMa,
                                        iID_MLNS = toiMuc.MlnsId,
                                        iID_MLNS_Cha = toiMuc.MlnsIdParent,
                                        BHangCha = true,
                                    };
                                    foreach (var dv in listDonViSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                        {
                                            if (dv.IIDMaDonVi == "ALL")
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            //else if (dv.IIDMaDonVi == "SAME")
                                            //{
                                            //    giaTri.FTuChi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && x.sNG == nganh.IIDMaDanhMuc).Sum(x => x.TuChi);
                                            //    kq.LstGiaTri.Add(giaTri);
                                            //}
                                            else
                                            {
                                                giaTri.FTuChi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.sLNS == toiMuc.Lns && x.sL == toiMuc.L && x.sK == toiMuc.K && x.sM == toiMuc.M).Sum(x => x.TuChi);
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        else
                                        {
                                            kq.LstGiaTri.Add(giaTri);
                                        }

                                    }
                                    results.Insert(index, kq);
                                }
                            }

                            // Dong tong cong ngang
                            List<ReportDuToanDauNamTheoNganhPhuLucQuery> resultsTotal = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                            ReportDuToanDauNamTheoNganhPhuLucQuery total = new ReportDuToanDauNamTheoNganhPhuLucQuery();
                            total.LstGiaTriTotal = new List<NsDtdauNamChungTuChiTiet>();
                            foreach (var dv in listDonViSplits[i])
                            {
                                NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                {
                                    if (dv.IIDMaDonVi == "ALL")
                                    {
                                        var gtDonVi = listDataDonViPrint.Where(x => !x.BHangCha.GetValueOrDefault(false)).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                    //else if (dv.IIDMaDonVi == "SAME")
                                    //{
                                    //    var gtDonVi = listDataDonViPrint.Where(x => donviCap2.Contains(x.IdDonVi) && !x.BHangCha.GetValueOrDefault(false)).Sum(x => x.TuChi);
                                    //    giaTri.FTuChi = gtDonVi;
                                    //    total.LstGiaTriTotal.Add(giaTri);
                                    //}
                                    else
                                    {
                                        var gtDonVi = listDataDonViPrint.Where(x => x.IdDonVi == dv.IIDMaDonVi && !x.BHangCha.GetValueOrDefault(false)).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }

                                }
                                else
                                {
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                            }
                            resultsTotal.Add(total);

                            // Lay toi tieu muc 
                            var lstResults = UpdateIdMucLucNganSach(results, exportType);
                            FormatDisplay(lstResults);
                            //var nameColunmMerge = GetExcelColumnName(listDonViSplits[i].Count() + 6);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", lstResults.OrderBy(x => x.ILoai).ToList());
                            data.Add("ListDataTotal", resultsTotal);
                            data.Add("TongCong", results.Where(x => x.iID_MLNS_Cha == null).Sum(x => x.TongCong));
                            //data.Add("SumTotal", sumTotal);
                            data.Add("TieuDe1", TieuDe1);
                            data.Add("TieuDe2", TieuDe2);
                            data.Add("TieuDe3", TieuDe3);
                            data.Add("Count", 10000);
                            //data.Add("Ngay", DateUtils.GetCurrentDateReport());
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", string.IsNullOrEmpty(GetLevelTitle(2)) ? "NGÀNH..." : GetLevelTitle(2));
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("h1", h1);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty) + " - Tờ: " + (i + 1));
                            data.Add("h2", "");
                            data.Add("Nam", _sessionService.Current.YearOfWork);
                            data.Add("Headers", listDonViSplits[i]);
                            //data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            //data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            //data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            //data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            //data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                            var ghiChu = GetGhiChu();
                            data.Add("HasGhiChu", ghiChu.Any());
                            data.Add("ListGhiChu", ghiChu);

                            data.Add("TongTienBangChu", StringUtils.NumberToText(listDataDonViPrint.Sum(x => x.TuChi) * donViTinh, true));
                            data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                            // ân côt thừa trong excel
                            List<int> hideCollum = new List<int>();
                            hideCollum.Add(16);
                            hideCollum.Add(17);
                            hideCollum.Add(18);
                            hideCollum.Add(19);
                            hideCollum.Add(20);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_PHANCAP);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportDuToanDauNamTheoNganhPhuLucQuery, DonVi, GhiChu>(templateFileName, data, hideCollum);
                            exportResults.Add(new ExportResult("Tổng hợp dự toán ngân sách đặc thù cho chác đơn vị - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
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

        private void FormatDisplay(List<ReportDuToanDauNamTheoNganhPhuLucQuery> lstData)
        {

            foreach (var item in lstData.Where(x => !string.IsNullOrEmpty(x.sTM)))
            {
                var parent = lstData.FirstOrDefault(x => x.iID_MLNS == item.iID_MLNS_Cha);
                if (parent != null && parent.sM != string.Empty)
                {
                    item.sM = string.Empty;
                    item.sL = string.Empty;
                    item.sK = string.Empty;
                    item.sLNS = string.Empty;

                }
            }
            foreach (var item in lstData.Where(x => !string.IsNullOrEmpty(x.sTTM)))
            {
                var parent = lstData.FirstOrDefault(x => x.iID_MLNS == item.iID_MLNS_Cha);
                if (parent != null && parent.sTM != string.Empty)
                {
                    item.sTM = string.Empty;
                    item.sM = string.Empty;
                    item.sL = string.Empty;
                    item.sK = string.Empty;
                    item.sLNS = string.Empty;
                }
            }

        }

        public void OnPrintReportDuToanPhanBoNganSachDacThuChoDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    if (ListNganh == null || ListNganh.Where(item => item.IsChecked).Count() == 0)
                    {
                        return;
                    }
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).ToList();
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    int donViTinh = GetDonViTinh();
                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var yearOfBudget = _sessionService.Current.YearOfBudget;
                    var budgetSource = _sessionService.Current.Budget;
                    //var loai = DemandCheckType.DISTRIBUTION;
                    var h1 = string.Empty;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<NsDtdauNamChungTuChiTietReportModel> listDataDonViPrint = new List<NsDtdauNamChungTuChiTietReportModel>();
                    var lstIdCodeSelected = "";
                    if (ListNganh != null)
                    {
                        lstIdCodeSelected = string.Join(",", ListNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                    }
                    if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                    {
                        listDataDonViPrint = GetDataPrintDuToanNamDonVi(ListNganh.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList(), (double)donViTinh)
                        .OrderBy(n => n.NG).ToList();
                    }
                    if (listDataDonViPrint == null || listDataDonViPrint.Count() == 0)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("TieuDe1", TieuDe1);
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        //data.Add("Ngay", DateUtils.GetCurrentDateReport());
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", "");
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty) + " - Tờ: 1");
                        data.Add("h2", "");
                        data.Add("Nam", _sessionService.Current.YearOfWork);
                        data.Add("TongTienBangChu", StringUtils.NumberToText(0, true));
                        data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                        var ghiChu = GetGhiChu();
                        data.Add("HasGhiChu", ghiChu.Any());
                        data.Add("ListGhiChu", ghiChu);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_DONVI_DOC_TRANG1_EMPTY);
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<NsDtdauNamChungTuDacThuDonViReportModel, NsDtdauNamChungTuDacThuDonViHeaderReportModel, GhiChu>(templateFileName, data);
                        exportResults.Add(new ExportResult("Tổng hợp dự toán ngân sách đặc thù cho chác đơn vị", fileNameWithoutExtension, null, xlsFile));

                    }
                    else
                    {
                        var listIdDonVi = string.Join(",", listDataDonViPrint.Select(x => x.MaDonVi).Distinct().ToList());
                        var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
                        var listXauNoiMaData = listDataDonViPrint.Select(x => x.XauNoiMa).Distinct().ToList();

                        List<NsMucLucNganSach> sktMucLucs = GetMLNS().Where(n => listXauNoiMaData.Contains(n.XauNoiMa)).OrderBy(n => n.Ng).ToList();

                        List<NsMucLucNganSach> lstMlResult = new List<NsMucLucNganSach>();
                        List<string> lstNg = new List<string>();
                        foreach (var mucluc in sktMucLucs)
                        {
                            if (lstNg.Count <= 0 || !lstNg.Contains(mucluc.Ng))
                            {
                                NsMucLucNganSach mlTong = new NsMucLucNganSach();
                                mlTong.Ng = mucluc.Ng;
                                mlTong.XauNoiMa = mucluc.XauNoiMa;
                                mlTong.MoTa = "(+)";
                                mlTong.NamLamViec = 0;
                                lstMlResult.Add(mlTong);
                                lstNg.Add(mucluc.Ng);
                            }
                            lstMlResult.Add(mucluc);
                        }

                        List<List<NsMucLucNganSach>> listMucLucSplits = SplitList(lstMlResult, 6).ToList();
                        //string path = "";
                        for (int i = 0; i < listMucLucSplits.Count; i++)
                        {
                            List<NsDtdauNamChungTuDacThuDonViHeaderReportModel> headers = new List<NsDtdauNamChungTuDacThuDonViHeaderReportModel>();
                            List<string> lstNganhHeader = listMucLucSplits[i].Select(x => x.Ng).Distinct().ToList();
                            int columnStart = 5;
                            foreach (var nganhHeader in lstNganhHeader)
                            {
                                var nganh = listDanhMuc.FirstOrDefault(x => x.IIDMaDanhMuc.Contains(nganhHeader));
                                var lstMlHeader = listMucLucSplits[i].Where(x => nganhHeader.Contains(x.Ng)).Select(x => x.MoTa).ToList();
                                var mergeRange = "";
                                var columnStartName = GetExcelColumnName(columnStart);
                                var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                                //mergeRange = columnStartName + "7" + ":" + columnEndName + "7";
                                mergeRange = columnStartName + "8" + ":" + columnEndName + "8";
                                //columnStart += 1;
                                NsDtdauNamChungTuDacThuDonViHeaderReportModel hd = new NsDtdauNamChungTuDacThuDonViHeaderReportModel();
                                hd.TenNganh = nganh != null ? nganh.STen : "";
                                hd.LstMucLuc = new List<TitleData>();
                                hd.LstNganhHeader = new List<TitleData>();
                                hd.MergeRange = mergeRange;
                                int j = 0;
                                foreach (var mlHeader in lstMlHeader)
                                {
                                    TitleData mlhd = new TitleData();
                                    if (j == 0)
                                    {
                                        mlhd = new TitleData();
                                        mlhd.SMoTa = nganh != null ? nganh.STen : "";
                                        mlhd.SSttBC = "1";
                                        hd.LstNganhHeader.Add(mlhd);
                                    }
                                    else
                                    {
                                        mlhd = new TitleData();
                                        mlhd.SMoTa = "";
                                        mlhd.SSttBC = "2";
                                        hd.LstNganhHeader.Add(mlhd);
                                    }
                                    mlhd = new TitleData();
                                    mlhd.SMoTa = mlHeader;
                                    hd.LstMucLuc.Add(mlhd);
                                    j++;
                                }
                                headers.Add(hd);
                            }
                            int stt = 1;
                            List<NsDtdauNamChungTuDacThuDonViReportModel> results = new List<NsDtdauNamChungTuDacThuDonViReportModel>();
                            foreach (var dv in listDonVi.OrderBy(x => x.IIDMaDonVi))
                            {
                                NsDtdauNamChungTuDacThuDonViReportModel kq = new NsDtdauNamChungTuDacThuDonViReportModel();
                                kq.Stt = stt++;
                                kq.MaDonVi = dv.IIDMaDonVi;
                                kq.TenDonVi = dv.TenDonVi;
                                kq.TongCong = listDataDonViPrint.Where(x => x.MaDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                                kq.LstGiaTri = new List<DetailMLNSData>();
                                foreach (var ml in listMucLucSplits[i])
                                {
                                    DetailMLNSData giaTri = new DetailMLNSData();
                                    if (ml.NamLamViec.Equals(0))
                                    {
                                        giaTri = new DetailMLNSData();
                                        var gtDonVi = listDataDonViPrint.Where(x => ml.Ng.Contains(x.NG) && x.MaDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                    else
                                    {
                                        giaTri = new DetailMLNSData();
                                        var gtDonVi = listDataDonViPrint.Where(x => x.MaDonVi == dv.IIDMaDonVi && x.XauNoiMa == ml.XauNoiMa).ToList();
                                        giaTri.FTuChi = gtDonVi != null ? gtDonVi.Sum(n => n.TuChi) : 0;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                }
                                results.Add(kq);
                            }

                            List<NsDtdauNamChungTuDacThuDonViReportModel> resultsTotal = new List<NsDtdauNamChungTuDacThuDonViReportModel>();
                            NsDtdauNamChungTuDacThuDonViReportModel total = new NsDtdauNamChungTuDacThuDonViReportModel();
                            total.LstGiaTriTotal = new List<DetailMLNSData>();
                            foreach (var dv in listMucLucSplits[i])
                            {
                                DetailMLNSData giaTri = new DetailMLNSData();
                                if (dv.NamLamViec.Equals(0))
                                {
                                    giaTri = new DetailMLNSData();
                                    var gtDonVi = listDataDonViPrint.Where(x => dv.Ng.Contains(x.NG)).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi;
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                                else
                                {
                                    giaTri = new DetailMLNSData();
                                    var gtDonVi = listDataDonViPrint.Where(x => x.XauNoiMa.Equals(dv.XauNoiMa)).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi;
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                            }
                            resultsTotal.Add(total);
                            var sumTotal = listDataDonViPrint.Sum(x => x.TuChi);
                            var numColumnMerge = listMucLucSplits[i].Count() < 3 ? 4 : listMucLucSplits[i].Count();
                            var nameColunmMerge = GetExcelColumnName(numColumnMerge + 4);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", results);
                            data.Add("ListDataTotal", resultsTotal);
                            data.Add("SumTotal", sumTotal);
                            data.Add("TieuDe1", TieuDe1);
                            data.Add("TieuDe2", TieuDe2);
                            data.Add("TieuDe3", TieuDe3);
                            //data.Add("Ngay", DateUtils.GetCurrentDateReport());
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionService.Current.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("h1", h1);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty) + " - Tờ: " + (i + 1));
                            data.Add("h2", "");
                            data.Add("Nam", _sessionService.Current.YearOfWork);
                            data.Add("Headers", headers);
                            data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                            var ghiChu = GetGhiChu();
                            data.Add("HasGhiChu", ghiChu.Any());
                            data.Add("ListGhiChu", ghiChu);
                            if (i == 0)
                            {
                                data.Add("TongTienBangChu", StringUtils.NumberToText(sumTotal * donViTinh, true));
                                data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                                data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                                data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                                data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                                data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                                data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                                data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                                data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                                data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                                data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                            }
                            if (i == 0)
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_DONVI_DOC_TRANG1);
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            }
                            else
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_DONVI_DOC_TRANG);
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                            }
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<NsDtdauNamChungTuDacThuDonViReportModel, NsDtdauNamChungTuDacThuDonViHeaderReportModel, GhiChu>(templateFileName, data);
                            exportResults.Add(new ExportResult("Tổng hợp dự toán ngân sách đặc thù cho chác đơn vị - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
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

        private Dictionary<string, Guid> GetDicMucLucNganSach()
        {
            var data = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
            Dictionary<string, Guid> _dicMucLucNganSach = new Dictionary<string, Guid>();
            _dicMucLucNganSach = new Dictionary<string, Guid>();
            if (data != null && data.Any())
            {
                foreach (var n in data)
                {
                    string sKey = n.Lns + "-" + n.L + "-" + n.K + "-" + n.M + "-" + n.Tm + "-" + n.Ttm;
                    if (!_dicMucLucNganSach.ContainsKey(sKey))
                        _dicMucLucNganSach.Add(sKey, n.Id);
                }
            }
            return _dicMucLucNganSach;
        }


        private List<DuToanDauNamChiThuongXuyenChiTietModel> GetDataPrintDuToanNamChiTiet(List<string> listNganh, double donViTinh)
        {
            List<DuToanDauNamChiThuongXuyenChiTietModel> result = new List<DuToanDauNamChiThuongXuyenChiTietModel>();
            IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

            if (ListNganh == null || ListNganh.Count() == 0)
            {
                return result;
            }
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoaiChungTu == VoucherType.NSBD_Key);
            if (IsInTheoTongHop)
                predicate = predicate.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else
                predicate = predicate.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicate = predicate.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTiet> listDataChiTiet = _sktSoLieuService.FindByCondition(predicate).ToList();

            List<string> xauNoiMa = new List<string>();
            if (listDataChiTiet != null && listDataChiTiet.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTiet.Select(n => n.SXauNoiMa).Distinct().ToList());
            }

            xauNoiMa = xauNoiMa.Distinct().ToList();
            foreach (string itemXauNoiMa in xauNoiMa)
            {
                DuToanDauNamChiThuongXuyenChiTietModel itemResult = new DuToanDauNamChiThuongXuyenChiTietModel();
                List<NsDtdauNamChungTuChiTiet> itemData = listDataChiTiet.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                if (itemData != null && itemData.Count() > 0)
                {
                    itemResult.MuaTrongNuoc = itemData.Sum(n => n.FHangMua);
                    itemResult.NhapKhau = itemData.Sum(n => n.FHangNhap);
                    itemResult.SoDaPhanBo = itemData.Sum(n => n.FPhanCap);
                    itemResult.NganhChuaPhanBo = itemData.Sum(n => n.FChuaPhanCap);
                    itemResult.LNS = itemData.FirstOrDefault().SLns;
                    itemResult.M = itemData.FirstOrDefault().SM;
                    itemResult.TM = itemData.FirstOrDefault().STm;
                    itemResult.TTM = itemData.FirstOrDefault().STtm;
                    itemResult.NG = itemData.FirstOrDefault().SNg;
                    itemResult.TNG = itemData.FirstOrDefault().STng;

                    var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == itemData.FirstOrDefault().SXauNoiMa).FirstOrDefault();
                    if (mlns != null)
                    {
                        itemResult.LNS = mlns.Lns == null ? String.Empty : mlns.Lns;
                        itemResult.TNG = mlns.Tng == null ? String.Empty : mlns.Tng;
                        itemResult.TNG1 = mlns.Tng1 == null ? String.Empty : mlns.Tng1;
                        itemResult.TNG2 = mlns.Tng2 == null ? String.Empty : mlns.Tng2;
                        itemResult.TNG3 = mlns.Tng3 == null ? String.Empty : mlns.Tng3;
                        itemResult.XauNoiMa = mlns.XauNoiMa;
                        itemResult.MlnsId = mlns.MlnsId;
                        itemResult.MlnsIdParent = mlns.MlnsIdParent;

                    }
                    else
                    {
                        itemResult.TNG = String.Empty;
                        itemResult.TNG1 = String.Empty;
                        itemResult.TNG2 = String.Empty;
                        itemResult.TNG3 = String.Empty;
                    }

                    itemResult.MoTa = itemData.FirstOrDefault().SMoTa;
                }
                result.Add(itemResult);
            }
            result = result.OrderBy(n => n.NG).ToList();

            _tongTongSo = result.Sum(n => n.TongSo) / donViTinh;
            _tongTongMuaTrongNuocNhapKhau = result.Sum(n => n.TongMuaNhap) / donViTinh;
            _tongMuaTrongNuoc = result.Sum(n => n.MuaTrongNuoc) / donViTinh;
            _tongNhapKhau = result.Sum(n => n.NhapKhau) / donViTinh;
            _tongTongPhanBo = result.Sum(n => n.TongPhanBo) / donViTinh;
            _tongDaPhanBo = result.Sum(n => n.SoDaPhanBo) / donViTinh;
            _tongChuaPhanBo = result.Sum(n => n.NganhChuaPhanBo) / donViTinh;

            result.Select(n =>
            {
                n.MuaTrongNuoc = n.MuaTrongNuoc / donViTinh;
                n.NhapKhau = n.NhapKhau / donViTinh;
                n.SoDaPhanBo = n.SoDaPhanBo / donViTinh;
                n.NganhChuaPhanBo = n.NganhChuaPhanBo / donViTinh;
                return n;
            }).ToList();
            return result;
        }

        private void PrintReportDuToanChiTietDonViSuDung(ExportType exportType)
        {
            try
            {
                if (ListNganh == null || ListNganh.Where(n => n.IsChecked).Count() == 0)
                {
                    return;
                }
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    int donViTinh = GetDonViTinh();
                    List<DuToanDauNamChiThuongXuyenChiTietModel> listData = GetDataPrintDuToanNamChiTiet(ListNganh.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList(), (double)donViTinh);

                    if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    {
                        var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(listData.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                        var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                        listData = listData.Where(x => listLNS.Contains(x.LNS)).ToList();
                    }

                    List<DuToanDauNamChiThuongXuyenChiTietModel> listHeader = new List<DuToanDauNamChiThuongXuyenChiTietModel>();
                    IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                    if (listData != null && listData.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                          string.Join(",", listData.Select(n => n.XauNoiMa).ToList())).ToList();

                        if (listParent != null && listParent.Count > 0)
                        {
                            //Lấy ra các header
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new DuToanDauNamChiThuongXuyenChiTietModel
                                {
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    MoTa = item.MoTa,
                                    MuaTrongNuoc = 0,
                                    NhapKhau = 0,
                                    SoDaPhanBo = 0,
                                    NganhChuaPhanBo = 0,
                                    XauNoiMa = item.XauNoiMa,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }

                            //Lấy cấp cha con
                            foreach (DuToanDauNamChiThuongXuyenChiTietModel item in listData)
                            {
                                var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }

                    }

                    //Chỉ lấy đến mức NG
                    listHeader = listHeader.Where(x => x.NG != "").ToList();
                    listHeader = listHeader.OrderBy(x => x.M).OrderBy(x => x.TM).OrderBy(x => x.TTM).OrderBy(x => x.NG).OrderBy(x => x.TNG1).OrderBy(x => x.TNG2).OrderBy(x => x.TNG2).ToList();
                    //Tính tổng các cấp cha
                    foreach (var item in listHeader)
                    {
                        var lstChild = listHeader.Where(x => x.MlnsIdParent == item.MlnsId).ToList();
                        if (lstChild.Count > 0)
                        {
                            item.MuaTrongNuoc = lstChild.Sum(x => x.MuaTrongNuoc);
                            item.NhapKhau = lstChild.Sum(x => x.NhapKhau);
                            item.SoDaPhanBo = lstChild.Sum(x => x.SoDaPhanBo);
                            item.NganhChuaPhanBo = lstChild.Sum(x => x.NganhChuaPhanBo);
                        }
                    }

                    //Lấy thông tin chuyên ngành
                    List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_NGANH", _sessionService.Current.YearOfWork).ToList();
                    List<string> listNG = listHeader.Select(n => n.NG).Distinct().ToList();

                    int number = 1;

                    foreach (string nganh in listNG)
                    {
                        int index = listHeader.IndexOf(listHeader.FirstOrDefault(n => n.NG == nganh && n.TNG == ""));
                        if (index >= 0)
                        {
                            DuToanDauNamChiThuongXuyenChiTietModel itemParent = new DuToanDauNamChiThuongXuyenChiTietModel();
                            DanhMuc itemDanhMuc = listDanhMuc.FirstOrDefault(x => x.IIDMaDanhMuc == nganh);
                            if (itemDanhMuc != null)
                            {
                                itemParent.MoTa = number + "." + itemDanhMuc.STen;
                                itemParent.IsHangCha = true;
                                itemParent.MuaTrongNuoc = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.MuaTrongNuoc);
                                itemParent.NhapKhau = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NhapKhau);
                                itemParent.SoDaPhanBo = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.SoDaPhanBo);
                                itemParent.NganhChuaPhanBo = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhChuaPhanBo);
                                itemParent.NG = String.Empty;
                                itemParent.TNG = String.Empty;
                                itemParent.TNG1 = String.Empty;
                                itemParent.TNG2 = String.Empty;
                                itemParent.TNG3 = String.Empty;
                                listHeader.Insert(index, itemParent);
                                number++;
                            }
                        }
                    }

                    //Ẩn hiển cột theo combobox
                    string sTTNoiDung = "E";
                    if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "NG")
                    {
                        listHeader = listHeader.Where(x => x.TNG == "" && x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG")
                    {
                        listHeader = listHeader.Where(x => x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "F";
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG1")
                    {
                        listHeader = listHeader.Where(x => x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "G";
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG2")
                    {
                        listHeader = listHeader.Where(x => x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "H";
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG3")
                    {
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG3)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "I";
                    }

                    foreach (var item in listHeader)
                    {
                        if (!string.IsNullOrEmpty(item.TNG))
                        {
                            item.M = String.Empty;
                            item.TM = String.Empty;
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

                    listData = new List<DuToanDauNamChiThuongXuyenChiTietModel>(listHeader);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    //data.Add("Cap1", isPrintPhuLuc ? string.Empty : _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                    //data.Add("Cap2", isPrintPhuLuc ? string.Empty : GetHeader2Report());
                    //data.Add("TieuDe1", TieuDe1.ToUpper());
                    //data.Add("TieuDe2", TieuDe2);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(_tongTongSoNamNay * donViTinh, true));
                    //data.Add("ThoiGian", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("ThoiGian", string.Format("{0}, {1} ", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("Items", listData);
                    data.Add("NamHienTai", _sessionService.Current.YearOfWork.ToString());
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);

                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("sTTNoiDung", sTTNoiDung);

                    data.Add("TongTongSo", _tongTongSo);
                    data.Add("TongTongMuaTrongNuocNhapKhau", _tongTongMuaTrongNuocNhapKhau);
                    data.Add("TongMuaTrongNuoc", _tongMuaTrongNuoc);
                    data.Add("TongNhapKhau", _tongNhapKhau);
                    data.Add("TongTongPhanBo", _tongTongPhanBo);
                    data.Add("TongDaPhanBo", _tongDaPhanBo);
                    data.Add("TongChuaPhanBo", _tongChuaPhanBo);
                    var ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);

                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_DUTOANNAMCHITIET_All);
                    fileNamePrefix = "rptDuToanDauNamDuToanNam";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi);
                    var xlsFile = _exportService.Export<DuToanDauNamChiThuongXuyenChiTietModel, GhiChu>(templateFileName, data, hideColumns);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
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


        private List<DuToanDauNamChiThuongXuyenModel> GetDataPrintDuToanNam(List<string> listNganh, double donViTinh, bool IsInTheoTongHop)
        {
            List<DuToanDauNamChiThuongXuyenModel> result = new List<DuToanDauNamChiThuongXuyenModel>();

            if (ListNganh == null || ListNganh.Count() == 0)
            {
                return result;
            }
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoaiChungTu == VoucherType.NSBD_Key);
            if (IsInTheoTongHop) predicate = predicate.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicate = predicate.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicate = predicate.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTiet> listDataChiTiet = _sktSoLieuService.FindByCondition(predicate).ToList();
            IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

            //var predicateNam_1 = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            //predicateNam_1 = predicateNam_1.And(x => x.INamLamViec == (_sessionService.Current.YearOfWork - 1));
            //predicateNam_1 = predicateNam_1.And(x => x.ILoaiChungTu == VoucherType.NSBD_Key);
            //if (IsInTheoTongHop) predicateNam_1 = predicateNam_1.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            //else predicateNam_1 = predicateNam_1.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            //predicateNam_1 = predicateNam_1.And(x => listNganh.Contains(x.SNg));
            //List<NsDtdauNamChungTuChiTiet> listDataChiTietNam_1 = _sktSoLieuService.FindByCondition(predicateNam_1).ToList();
            //IEnumerable<NsMucLucNganSach> lstmuclucngansach_1 = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork - 1).ToList();

            //var predicateNam_2 = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            //predicateNam_2 = predicateNam_2.And(x => x.INamLamViec == (_sessionService.Current.YearOfWork - 2));
            //predicateNam_2 = predicateNam_2.And(x => x.ILoaiChungTu == VoucherType.NSBD_Key);
            //if (IsInTheoTongHop) predicateNam_2 = predicateNam_2.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            //else predicateNam_2 = predicateNam_2.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            //predicateNam_2 = predicateNam_2.And(x => listNganh.Contains(x.SNg));
            //List<NsDtdauNamChungTuChiTiet> listDataChiTietNam_2 = _sktSoLieuService.FindByCondition(predicateNam_2).ToList();
            //IEnumerable<NsMucLucNganSach> lstmuclucngansach_2 = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork - 2).ToList();
            //Lấy cấu hình căn cứ 
            int yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_cancu = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate_cancu = predicate_cancu.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate_cancu = predicate_cancu.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate_cancu).OrderBy(n => n.INamCanCu).OrderBy(x => x.STenCot);
            var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);

            //Lấy dự toán năm n-1

            var predicateNam_1 = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
            predicateNam_1 = predicateNam_1.And(x => x.INamLamViec == (_sessionService.Current.YearOfWork));
            predicateNam_1 = predicateNam_1.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key));
            predicateNam_1 = predicateNam_1.And(x => x.IIdCanCu == cauHinhCanCu.Where(x => x.IIDMaChucNang == "BUDGET_ESTIMATE").Select(x => x.Id).FirstOrDefault());
            if (IsInTheoTongHop) predicateNam_1 = predicateNam_1.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicateNam_1 = predicateNam_1.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicateNam_1 = predicateNam_1.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTietCanCu> listDataChiTietNam_1 = _sktSoLieuChiTietCanCuService.FindByCondition(predicateNam_1).ToList();
            IEnumerable<NsMucLucNganSach> lstmuclucngansach_1 = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork - 1).ToList();


            //Lấy quyết toán năm n-2
            var predicateNam_2 = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
            predicateNam_2 = predicateNam_2.And(x => x.INamLamViec == (_sessionService.Current.YearOfWork));
            predicateNam_2 = predicateNam_2.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key));
            predicateNam_2 = predicateNam_2.And(x => x.IIdCanCu == cauHinhCanCu.Where(x => x.IIDMaChucNang == "BUDGET_SETTLEMENT").Select(x => x.Id).FirstOrDefault());
            if (IsInTheoTongHop) predicateNam_2 = predicateNam_2.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicateNam_2 = predicateNam_2.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicateNam_2 = predicateNam_2.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTietCanCu> listDataChiTietNam_2 = _sktSoLieuChiTietCanCuService.FindByCondition(predicateNam_2).ToList();
            IEnumerable<NsMucLucNganSach> lstmuclucngansach_2 = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork - 2).ToList();


            List<string> xauNoiMa = new List<string>();
            if (listDataChiTiet != null && listDataChiTiet.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTiet.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            if (listDataChiTietNam_1 != null && listDataChiTietNam_1.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTietNam_1.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            if (listDataChiTietNam_2 != null && listDataChiTietNam_2.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTietNam_2.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            xauNoiMa = xauNoiMa.Distinct().ToList();
            foreach (string itemXauNoiMa in xauNoiMa)
            {
                DuToanDauNamChiThuongXuyenModel itemResult = new DuToanDauNamChiThuongXuyenModel();
                List<NsDtdauNamChungTuChiTiet> itemData = listDataChiTiet.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                if (!itemData.IsEmpty())
                {
                    itemResult.NganhPhanCapHienVatNamNay = itemData.Sum(n => n.FHangNhap) + itemData.Sum(n => n.FHangMua);
                    itemResult.NganhPhanCapBangTienNamNay = itemData.Sum(n => n.FPhanCap);
                    itemResult.NganhChuaPhanCapNamNay = itemData.Sum(n => n.FChuaPhanCap);
                    itemResult.L = itemData.FirstOrDefault().SL;
                    itemResult.K = itemData.FirstOrDefault().SK;
                    itemResult.M = itemData.FirstOrDefault().SM;
                    itemResult.TM = itemData.FirstOrDefault().STm;
                    itemResult.TTM = itemData.FirstOrDefault().STtm;
                    itemResult.NG = itemData.FirstOrDefault().SNg;
                    itemResult.TNG = itemData.FirstOrDefault().STng;
                    itemResult.XauNoiMa = itemXauNoiMa;

                    var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == itemData.FirstOrDefault().SXauNoiMa).FirstOrDefault();
                    if (mlns != null)
                    {
                        itemResult.TNG = mlns.Tng ?? String.Empty;
                        itemResult.TNG1 = mlns.Tng1 ?? String.Empty;
                        itemResult.TNG2 = mlns.Tng2 ?? String.Empty;
                        itemResult.TNG3 = mlns.Tng3 ?? String.Empty;
                        itemResult.MlnsId = mlns.MlnsId;
                        itemResult.MlnsIdParent = mlns.MlnsIdParent.Value;

                    }
                    else
                    {
                        itemResult.TNG = String.Empty;
                        itemResult.TNG1 = String.Empty;
                        itemResult.TNG2 = String.Empty;
                        itemResult.TNG3 = String.Empty;
                    }

                    itemResult.MoTa = mlns == null ? String.Empty : mlns.MoTa;
                }
                List<NsDtdauNamChungTuChiTietCanCu> itemData_1 = listDataChiTietNam_1.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                if (!itemData_1.IsEmpty())
                {
                    itemResult.NganhPhanCapHienVatNam1 = itemData_1.Sum(n => n.FHangNhap) + itemData_1.Sum(n => n.FHangMua);
                    itemResult.NganhPhanCapBangTienNam1 = itemData_1.Sum(n => n.FPhanCap);
                    itemResult.L = itemData_1.FirstOrDefault().SL;
                    itemResult.K = itemData_1.FirstOrDefault().SK;
                    itemResult.M = itemData_1.FirstOrDefault().SM;
                    itemResult.TM = itemData_1.FirstOrDefault().STm;
                    itemResult.TTM = itemData_1.FirstOrDefault().STtm;
                    itemResult.NG = itemData_1.FirstOrDefault().SNg;
                    itemResult.TNG = itemData_1.FirstOrDefault().STng;
                    itemResult.XauNoiMa = itemXauNoiMa;

                    var tng = String.Empty;
                    var tng1 = String.Empty;
                    var tng2 = String.Empty;
                    var tng3 = String.Empty;

                    var mlns1 = lstmuclucngansach.Where(x => x.XauNoiMa == itemData_1.FirstOrDefault().SXauNoiMa).FirstOrDefault();
                    if (mlns1 != null)
                    {
                        tng = mlns1.Tng ?? String.Empty;
                        tng1 = mlns1.Tng1 ?? String.Empty;
                        tng2 = mlns1.Tng2 ?? String.Empty;
                        tng3 = mlns1.Tng3 ?? String.Empty;

                    }

                    itemResult.TNG = tng;
                    itemResult.TNG1 = tng1;
                    itemResult.TNG2 = tng2;
                    itemResult.TNG3 = tng3;
                    itemResult.MlnsId = mlns1.MlnsId;
                    itemResult.MlnsIdParent = mlns1.MlnsIdParent.Value;


                    itemResult.MoTa = mlns1 == null ? String.Empty : mlns1.MoTa;
                }
                List<NsDtdauNamChungTuChiTietCanCu> itemData_2 = listDataChiTietNam_2.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                if (!itemData_2.IsEmpty())
                {
                    itemResult.NganhPhanCapHienVatNam2 = itemData_2.Sum(n => n.FHangNhap) + itemData_2.Sum(n => n.FHangMua);
                    itemResult.NganhPhanCapBangTienNam2 = 0;
                    itemResult.L = itemData_2.FirstOrDefault().SL;
                    itemResult.K = itemData_2.FirstOrDefault().SK;
                    itemResult.M = itemData_2.FirstOrDefault().SM;
                    itemResult.TM = itemData_2.FirstOrDefault().STm;
                    itemResult.TTM = itemData_2.FirstOrDefault().STtm;
                    itemResult.NG = itemData_2.FirstOrDefault().SNg;
                    itemResult.TNG = itemData_2.FirstOrDefault().STng;
                    itemResult.XauNoiMa = itemXauNoiMa;

                    var tng = String.Empty;
                    var tng1 = String.Empty;
                    var tng2 = String.Empty;
                    var tng3 = String.Empty;

                    var mlns2 = lstmuclucngansach.Where(x => x.XauNoiMa == itemData_2.FirstOrDefault().SXauNoiMa).FirstOrDefault();
                    if (mlns2 != null)
                    {
                        tng = mlns2.Tng ?? String.Empty;
                        tng1 = mlns2.Tng1 ?? String.Empty;
                        tng2 = mlns2.Tng2 ?? String.Empty;
                        tng3 = mlns2.Tng3 ?? String.Empty;
                    }

                    itemResult.TNG = tng;
                    itemResult.TNG1 = tng1;
                    itemResult.TNG2 = tng2;
                    itemResult.TNG3 = tng3;
                    itemResult.MlnsId = mlns2.MlnsId;
                    itemResult.MlnsIdParent = mlns2.MlnsIdParent.Value;
                    itemResult.MoTa = mlns2 == null ? String.Empty : mlns2.MoTa;

                }
                result.Add(itemResult);
            }
            result = result.OrderBy(n => n.NG).ToList();
            _tongTongSoNamNay = result.Sum(n => n.TongSoNamNay) / donViTinh;
            _tongHienVatNamNay = result.Sum(n => n.NganhPhanCapHienVatNamNay) / donViTinh;
            _tongBangTienNamNay = result.Sum(n => n.NganhPhanCapBangTienNamNay) / donViTinh;
            _tongChuaPhanCapNamNay = result.Sum(n => n.NganhChuaPhanCapNamNay) / donViTinh;
            _tongTongSoNam1 = result.Sum(n => n.TongSoNam1) / donViTinh;
            _tongHienVatNam1 = result.Sum(n => n.NganhPhanCapHienVatNam1) / donViTinh;
            _tongBangTienNam1 = result.Sum(n => n.NganhPhanCapBangTienNam1) / donViTinh;
            _tongTongSoNam2 = result.Sum(n => n.TongSoNam2) / donViTinh;
            _tongHienVatNam2 = result.Sum(n => n.NganhPhanCapHienVatNam2) / donViTinh;
            _tongBangTienNam2 = result.Sum(n => n.NganhPhanCapBangTienNam2) / donViTinh;

            //List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_NGANH", _sessionService.Current.YearOfWork).ToList();
            //List<string> listNG = result.Select(n => n.NG).Distinct().ToList();
            //int number = 1;
            //foreach (string nganh in listNG)
            //{
            //    int index = result.IndexOf(result.FirstOrDefault(n => n.NG == nganh));
            //    if (index >= 0)
            //    {
            //        DuToanDauNamChiThuongXuyenModel itemParent = new DuToanDauNamChiThuongXuyenModel();
            //        DanhMuc itemDanhMuc = listDanhMuc.FirstOrDefault(x => x.IIDMaDanhMuc == nganh);
            //        if (itemDanhMuc != null)
            //        {
            //            itemParent.MoTa = number + "." + itemDanhMuc.STen;
            //            itemParent.IsHangCha = true;
            //            itemParent.NganhPhanCapHienVatNamNay = result.Where(n => n.NG == nganh).Sum(n => n.NganhPhanCapHienVatNamNay);
            //            itemParent.NganhPhanCapBangTienNamNay = result.Where(n => n.NG == nganh).Sum(n => n.NganhPhanCapBangTienNamNay);
            //            itemParent.NganhChuaPhanCapNamNay = result.Where(n => n.NG == nganh).Sum(n => n.NganhChuaPhanCapNamNay);
            //            itemParent.NganhPhanCapHienVatNam1 = result.Where(n => n.NG == nganh).Sum(n => n.NganhPhanCapHienVatNam1);
            //            itemParent.NganhPhanCapBangTienNam1 = result.Where(n => n.NG == nganh).Sum(n => n.NganhPhanCapBangTienNam1);
            //            itemParent.NganhPhanCapHienVatNam2 = result.Where(n => n.NG == nganh).Sum(n => n.NganhPhanCapHienVatNam2);
            //            itemParent.NganhPhanCapBangTienNam2 = result.Where(n => n.NG == nganh).Sum(n => n.NganhPhanCapBangTienNam2);
            //            result.Insert(index, itemParent);
            //            number++;
            //        }
            //    }
            //}
            result.Select(n =>
            {
                n.NganhPhanCapHienVatNamNay = n.NganhPhanCapHienVatNamNay / donViTinh;
                n.NganhPhanCapBangTienNamNay = n.NganhPhanCapBangTienNamNay / donViTinh;
                n.NganhChuaPhanCapNamNay = n.NganhChuaPhanCapNamNay / donViTinh;
                n.NganhPhanCapHienVatNam1 = n.NganhPhanCapHienVatNam1 / donViTinh;
                n.NganhPhanCapBangTienNam1 = n.NganhPhanCapBangTienNam1 / donViTinh;
                n.NganhPhanCapHienVatNam2 = n.NganhPhanCapHienVatNam2 / donViTinh;
                n.NganhPhanCapBangTienNam2 = n.NganhPhanCapBangTienNam2 / donViTinh;
                return n;
            }).ToList();
            return result;
        }

        private List<DuToanDauNamTongHopDacThuModel> GetDataPrintTongHopDuToanDacThu(List<string> listNganh, double donViTinh, bool IsInTheoTongHop)
        {
            List<DuToanDauNamTongHopDacThuModel> result = new List<DuToanDauNamTongHopDacThuModel>();

            if (ListNganh == null || ListNganh.Count() == 0)
            {
                return result;
            }
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoaiChungTu == VoucherType.NSBD_Key);
            if (IsInTheoTongHop) predicate = predicate.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicate = predicate.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicate = predicate.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTiet> listDataChiTiet = _sktSoLieuService.FindByCondition(predicate).ToList();
            var listDataPhanCap = _soLieuChiTietPhanCapService
                .FindAll()
                .Where(t => listDataChiTiet.Select(x => x.IIdCtdtdauNam).Contains(t.IIdCtdtDauNam));

            IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

            //Lấy cấu hình căn cứ 
            int yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_cancu = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate_cancu = predicate_cancu.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate_cancu = predicate_cancu.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate_cancu).OrderBy(n => n.INamCanCu).OrderBy(x => x.STenCot);
            var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);

            //Lấy dự toán năm n-1

            var predicateNam_1 = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
            predicateNam_1 = predicateNam_1.And(x => x.INamLamViec == (_sessionService.Current.YearOfWork));
            predicateNam_1 = predicateNam_1.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key));
            predicateNam_1 = predicateNam_1.And(x => x.IIdCanCu == cauHinhCanCu.Where(x => x.IIDMaChucNang == "BUDGET_ESTIMATE").Select(x => x.Id).FirstOrDefault());
            if (IsInTheoTongHop) predicateNam_1 = predicateNam_1.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicateNam_1 = predicateNam_1.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicateNam_1 = predicateNam_1.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTietCanCu> listDataChiTietNam_1 = _sktSoLieuChiTietCanCuService.FindByCondition(predicateNam_1).ToList();
            var listDataPhanCap1 = _soLieuChiTietPhanCapService
                .FindAll()
                .Where(t => listDataChiTietNam_1.Select(x => x.IID_CTDTDauNam).Contains(t.IIdCtdtDauNam));
            IEnumerable<NsMucLucNganSach> lstmuclucngansach_1 = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork - 1).ToList();


            //Lấy quyết toán năm n-2
            var predicateNam_2 = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
            predicateNam_2 = predicateNam_2.And(x => x.INamLamViec == (_sessionService.Current.YearOfWork));
            predicateNam_2 = predicateNam_2.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key));
            predicateNam_2 = predicateNam_2.And(x => x.IIdCanCu == cauHinhCanCu.Where(x => x.IIDMaChucNang == "BUDGET_SETTLEMENT").Select(x => x.Id).FirstOrDefault());
            if (IsInTheoTongHop) predicateNam_2 = predicateNam_2.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicateNam_2 = predicateNam_2.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicateNam_2 = predicateNam_2.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTietCanCu> listDataChiTietNam_2 = _sktSoLieuChiTietCanCuService.FindByCondition(predicateNam_2).ToList();
            IEnumerable<NsMucLucNganSach> lstmuclucngansach_2 = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork - 2).ToList();


            List<string> xauNoiMa = new List<string>();
            if (listDataChiTiet != null && listDataChiTiet.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTiet.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            if (listDataChiTietNam_1 != null && listDataChiTietNam_1.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTietNam_1.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            if (listDataChiTietNam_2 != null && listDataChiTietNam_2.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTietNam_2.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            xauNoiMa = xauNoiMa.Distinct().ToList();
            foreach (string itemXauNoiMa in xauNoiMa)
            {
                DuToanDauNamTongHopDacThuModel itemResult = new DuToanDauNamTongHopDacThuModel();
                List<NsDtdauNamChungTuChiTiet> itemData = listDataChiTiet.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                var itemPhanCap = listDataPhanCap.Where(x => x.sXauNoiMaGoc == itemXauNoiMa).ToList();
                if (!itemData.IsEmpty())
                {
                    itemResult.DaPhanCapNamNay = itemPhanCap.Sum(n => n.FTuChi ?? 0);
                    itemResult.ChuaPhanCapNamNay = itemData.Sum(n => n.FChuaPhanCap);
                    itemResult.LNS = itemData.FirstOrDefault().SLns;
                    itemResult.L = itemData.FirstOrDefault().SL;
                    itemResult.K = itemData.FirstOrDefault().SK;
                    itemResult.M = itemData.FirstOrDefault().SM;
                    itemResult.TM = itemData.FirstOrDefault().STm;
                    itemResult.TTM = itemData.FirstOrDefault().STtm;
                    itemResult.NG = itemData.FirstOrDefault().SNg;
                    itemResult.TNG = itemData.FirstOrDefault().STng;
                    itemResult.XauNoiMa = itemXauNoiMa;

                    var mlns = lstmuclucngansach.FirstOrDefault(x => x.XauNoiMa == itemData.FirstOrDefault().SXauNoiMa);
                    if (mlns != null)
                    {
                        itemResult.TNG = mlns.Tng ?? String.Empty;
                        itemResult.TNG1 = mlns.Tng1 ?? String.Empty;
                        itemResult.TNG2 = mlns.Tng2 ?? String.Empty;
                        itemResult.TNG3 = mlns.Tng3 ?? String.Empty;
                        itemResult.MlnsId = mlns.MlnsId;
                        itemResult.MlnsIdParent = mlns.MlnsIdParent.Value;

                    }
                    else
                    {
                        itemResult.TNG = String.Empty;
                        itemResult.TNG1 = String.Empty;
                        itemResult.TNG2 = String.Empty;
                        itemResult.TNG3 = String.Empty;
                    }

                    itemResult.MoTa = mlns == null ? String.Empty : mlns.MoTa;
                }
                List<NsDtdauNamChungTuChiTietCanCu> itemData_1 = listDataChiTietNam_1.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                var itemPhanCap1 = listDataPhanCap1.Where(x => x.sXauNoiMaGoc == itemXauNoiMa).ToList();
                if (!itemData_1.IsEmpty())
                {
                    itemResult.DaPhanCapNamTruoc = itemPhanCap1.Sum(n => n.FTuChi ?? 0);
                    itemResult.ChuaPhanCapNamTruoc = itemData_1.Sum(n => n.FChuaPhanCap);
                    itemResult.LNS = itemData_1.FirstOrDefault().SLns;
                    itemResult.L = itemData_1.FirstOrDefault().SL;
                    itemResult.K = itemData_1.FirstOrDefault().SK;
                    itemResult.M = itemData_1.FirstOrDefault().SM;
                    itemResult.TM = itemData_1.FirstOrDefault().STm;
                    itemResult.TTM = itemData_1.FirstOrDefault().STtm;
                    itemResult.NG = itemData_1.FirstOrDefault().SNg;
                    itemResult.TNG = itemData_1.FirstOrDefault().STng;
                    itemResult.XauNoiMa = itemXauNoiMa;

                    var tng = String.Empty;
                    var tng1 = String.Empty;
                    var tng2 = String.Empty;
                    var tng3 = String.Empty;

                    var mlns1 = lstmuclucngansach.Where(x => x.XauNoiMa == itemData_1.FirstOrDefault().SXauNoiMa).FirstOrDefault();
                    if (mlns1 != null)
                    {
                        tng = mlns1.Tng ?? String.Empty;
                        tng1 = mlns1.Tng1 ?? String.Empty;
                        tng2 = mlns1.Tng2 ?? String.Empty;
                        tng3 = mlns1.Tng3 ?? String.Empty;

                    }

                    itemResult.TNG = tng;
                    itemResult.TNG1 = tng1;
                    itemResult.TNG2 = tng2;
                    itemResult.TNG3 = tng3;
                    itemResult.MlnsId = mlns1.MlnsId;
                    itemResult.MlnsIdParent = mlns1.MlnsIdParent.Value;


                    itemResult.MoTa = mlns1 == null ? String.Empty : mlns1.MoTa;
                }

                result.Add(itemResult);
            }
            result = result.OrderBy(n => n.NG).ToList();
            _tongTongSoNamNay = result.Sum(n => n.TongSoNamNay) / donViTinh;
            _tongTongSoNam1 = result.Sum(n => n.TongSoNamTruoc) / donViTinh;



            result.Select(n =>
            {
                n.ChuaPhanCapNamNay = n.ChuaPhanCapNamNay / donViTinh;
                n.DaPhanCapNamNay = n.DaPhanCapNamNay / donViTinh;
                n.ChuaPhanCapNamTruoc = n.ChuaPhanCapNamTruoc / donViTinh;
                n.DaPhanCapNamTruoc = n.DaPhanCapNamTruoc / donViTinh;
                return n;
            }).ToList();
            return result;
        }


        private List<DuToanDauNamChiThuongXuyenModel> GetDataPrintDuToanNamChiTapTrungCapHienVat(List<string> listNganh, double donViTinh, bool IsInTheoTongHop)
        {
            List<DuToanDauNamChiThuongXuyenModel> result = new List<DuToanDauNamChiThuongXuyenModel>();

            if (ListNganh == null || ListNganh.Count() == 0)
            {
                return result;
            }
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoaiChungTu == VoucherType.NSBD_Key);
            if (IsInTheoTongHop) predicate = predicate.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicate = predicate.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicate = predicate.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTiet> listDataChiTiet = _sktSoLieuService.FindByCondition(predicate).ToList();
            IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

            //Lấy cấu hình căn cứ 
            int yearOfWork = _sessionService.Current.YearOfWork;
            var predicate_cancu = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate_cancu = predicate_cancu.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate_cancu = predicate_cancu.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate_cancu).OrderBy(n => n.INamCanCu).OrderBy(x => x.STenCot);
            var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);

            //Lấy dự toán năm n-1

            var predicateNam_1 = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
            predicateNam_1 = predicateNam_1.And(x => x.INamLamViec == (_sessionService.Current.YearOfWork));
            predicateNam_1 = predicateNam_1.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key));
            predicateNam_1 = predicateNam_1.And(x => x.IIdCanCu == cauHinhCanCu.Where(x => x.IIDMaChucNang == "BUDGET_ESTIMATE").Select(x => x.Id).FirstOrDefault());
            if (IsInTheoTongHop) predicateNam_1 = predicateNam_1.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicateNam_1 = predicateNam_1.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicateNam_1 = predicateNam_1.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTietCanCu> listDataChiTietNam_1 = _sktSoLieuChiTietCanCuService.FindByCondition(predicateNam_1).ToList();
            IEnumerable<NsMucLucNganSach> lstmuclucngansach_1 = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork - 1).ToList();


            //Lấy quyết toán năm n-2
            var predicateNam_2 = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
            predicateNam_2 = predicateNam_2.And(x => x.INamLamViec == (_sessionService.Current.YearOfWork));
            predicateNam_2 = predicateNam_2.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key));
            predicateNam_2 = predicateNam_2.And(x => x.IIdCanCu == cauHinhCanCu.Where(x => x.IIDMaChucNang == "BUDGET_SETTLEMENT").Select(x => x.Id).FirstOrDefault());
            if (IsInTheoTongHop) predicateNam_2 = predicateNam_2.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
            else predicateNam_2 = predicateNam_2.And(x => x.IIdMaDonVi != donvi0.IIDMaDonVi);
            predicateNam_2 = predicateNam_2.And(x => listNganh.Contains(x.SNg));
            List<NsDtdauNamChungTuChiTietCanCu> listDataChiTietNam_2 = _sktSoLieuChiTietCanCuService.FindByCondition(predicateNam_2).ToList();
            IEnumerable<NsMucLucNganSach> lstmuclucngansach_2 = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork - 2).ToList();

            List<string> xauNoiMa = new List<string>();
            if (listDataChiTiet != null && listDataChiTiet.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTiet.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            if (listDataChiTietNam_1 != null && listDataChiTietNam_1.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTietNam_1.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            if (listDataChiTietNam_2 != null && listDataChiTietNam_2.Count() > 0)
            {
                xauNoiMa.AddRange(listDataChiTietNam_2.Select(n => n.SXauNoiMa).Distinct().ToList());
            }
            xauNoiMa = xauNoiMa.Distinct().ToList();
            foreach (string itemXauNoiMa in xauNoiMa)
            {
                DuToanDauNamChiThuongXuyenModel itemResult = new DuToanDauNamChiThuongXuyenModel();
                List<NsDtdauNamChungTuChiTiet> itemData = listDataChiTiet.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                if (itemData != null && itemData.Count() > 0)
                {
                    itemResult.NganhPhanCapHienVatNamNay = itemData.Sum(n => n.FHangNhap);
                    itemResult.NganhPhanCapBangTienNamNay = itemData.Sum(n => n.FHangMua);
                    itemResult.NganhChuaPhanCapNamNay = 0;
                    itemResult.L = itemData.FirstOrDefault().SL;
                    itemResult.K = itemData.FirstOrDefault().SK;
                    itemResult.M = itemData.FirstOrDefault().SM;
                    itemResult.TM = itemData.FirstOrDefault().STm;
                    itemResult.TTM = itemData.FirstOrDefault().STtm;
                    itemResult.NG = itemData.FirstOrDefault().SNg;
                    itemResult.TNG = itemData.FirstOrDefault().STng;
                    itemResult.XauNoiMa = itemXauNoiMa;

                    var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == itemData.FirstOrDefault().SXauNoiMa).FirstOrDefault();
                    if (mlns != null)
                    {
                        itemResult.TNG = mlns.Tng == null ? String.Empty : mlns.Tng;
                        itemResult.TNG1 = mlns.Tng1 == null ? String.Empty : mlns.Tng1;
                        itemResult.TNG2 = mlns.Tng2 == null ? String.Empty : mlns.Tng2;
                        itemResult.TNG3 = mlns.Tng3 == null ? String.Empty : mlns.Tng3;
                        itemResult.MlnsId = mlns.MlnsId;
                        itemResult.MlnsIdParent = mlns.MlnsIdParent.Value;

                    }
                    else
                    {
                        itemResult.TNG = String.Empty;
                        itemResult.TNG1 = String.Empty;
                        itemResult.TNG2 = String.Empty;
                        itemResult.TNG3 = String.Empty;
                    }

                    itemResult.MoTa = mlns == null ? String.Empty : mlns.MoTa;
                }
                List<NsDtdauNamChungTuChiTietCanCu> itemData_1 = listDataChiTietNam_1.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                if (itemData_1 != null && itemData_1.Count() > 0)
                {
                    itemResult.NganhPhanCapHienVatNam1 = itemData_1.Sum(n => n.FHangMua);
                    itemResult.NganhPhanCapBangTienNam1 = itemData_1.Sum(n => n.FHangNhap);
                    itemResult.L = itemData_1.FirstOrDefault().SL;
                    itemResult.K = itemData_1.FirstOrDefault().SK;
                    itemResult.M = itemData_1.FirstOrDefault().SM;
                    itemResult.TM = itemData_1.FirstOrDefault().STm;
                    itemResult.TTM = itemData_1.FirstOrDefault().STtm;
                    itemResult.NG = itemData_1.FirstOrDefault().SNg;
                    itemResult.TNG = itemData_1.FirstOrDefault().STng;
                    itemResult.XauNoiMa = itemXauNoiMa;

                    var tng = String.Empty;
                    var tng1 = String.Empty;
                    var tng2 = String.Empty;
                    var tng3 = String.Empty;

                    var mlns1 = lstmuclucngansach.Where(x => x.XauNoiMa == itemData_1.FirstOrDefault().SXauNoiMa).FirstOrDefault();
                    if (mlns1 != null)
                    {
                        tng = mlns1.Tng == null ? String.Empty : mlns1.Tng;
                        tng1 = mlns1.Tng1 == null ? String.Empty : mlns1.Tng1;
                        tng2 = mlns1.Tng2 == null ? String.Empty : mlns1.Tng2;
                        tng3 = mlns1.Tng3 == null ? String.Empty : mlns1.Tng3;

                    }

                    itemResult.TNG = tng;
                    itemResult.TNG1 = tng1;
                    itemResult.TNG2 = tng2;
                    itemResult.TNG3 = tng3;
                    itemResult.MlnsId = mlns1 != null ? mlns1.MlnsId : Guid.Empty;
                    itemResult.MlnsIdParent = mlns1 != null ? mlns1.MlnsIdParent.Value : Guid.Empty;

                    itemResult.MoTa = mlns1 == null ? String.Empty : mlns1.MoTa;
                }

                //Lấy thông tin quyết toán n-2
                List<NsDtdauNamChungTuChiTietCanCu> itemData_2 = listDataChiTietNam_2.Where(n => n.SXauNoiMa == itemXauNoiMa).ToList();
                if (itemData_2 != null && itemData_2.Count() > 0)
                {
                    itemResult.NganhPhanCapHienVatNam2 = itemData_2.Sum(n => n.FHangMua);
                    itemResult.NganhPhanCapBangTienNam2 = itemData_2.Sum(n => n.FHangNhap);
                    if (itemData_2.Select(x => x.SLns).FirstOrDefault() == "1040100" || itemData_2.Select(x => x.SLns).FirstOrDefault() == "1040200")
                    {
                        itemResult.NganhPhanCapHienVatNam2 = 0;
                    }
                    else
                    {

                        itemResult.NganhPhanCapBangTienNam2 = 0;
                    }


                    itemResult.L = itemData_2.FirstOrDefault().SL;
                    itemResult.K = itemData_2.FirstOrDefault().SK;
                    itemResult.M = itemData_2.FirstOrDefault().SM;
                    itemResult.TM = itemData_2.FirstOrDefault().STm;
                    itemResult.TTM = itemData_2.FirstOrDefault().STtm;
                    itemResult.NG = itemData_2.FirstOrDefault().SNg;
                    itemResult.TNG = itemData_2.FirstOrDefault().STng;
                    itemResult.XauNoiMa = itemXauNoiMa;

                    var tng = String.Empty;
                    var tng1 = String.Empty;
                    var tng2 = String.Empty;
                    var tng3 = String.Empty;

                    var mlns2 = lstmuclucngansach.Where(x => x.XauNoiMa == itemData_2.FirstOrDefault().SXauNoiMa).FirstOrDefault();
                    if (mlns2 != null)
                    {
                        tng = mlns2.Tng == null ? String.Empty : mlns2.Tng;
                        tng1 = mlns2.Tng1 == null ? String.Empty : mlns2.Tng1;
                        tng2 = mlns2.Tng2 == null ? String.Empty : mlns2.Tng2;
                        tng3 = mlns2.Tng3 == null ? String.Empty : mlns2.Tng3;
                    }

                    itemResult.TNG = tng;
                    itemResult.TNG1 = tng1;
                    itemResult.TNG2 = tng2;
                    itemResult.TNG3 = tng3;
                    itemResult.MlnsId = mlns2 != null ? mlns2.MlnsId : Guid.Empty;
                    itemResult.MlnsIdParent = mlns2 != null ? mlns2.MlnsIdParent.Value : Guid.Empty;

                    itemResult.MoTa = mlns2 == null ? String.Empty : mlns2.MoTa;
                }
                result.Add(itemResult);
            }
            result = result.OrderBy(n => n.NG).ToList();
            _tongTongSoNamNay = result.Sum(n => n.TongSoNamNay) / donViTinh;
            _tongHienVatNamNay = result.Sum(n => n.NganhPhanCapHienVatNamNay) / donViTinh;
            _tongBangTienNamNay = result.Sum(n => n.NganhPhanCapBangTienNamNay) / donViTinh;
            _tongChuaPhanCapNamNay = result.Sum(n => n.NganhChuaPhanCapNamNay) / donViTinh;
            _tongTongSoNam1 = result.Sum(n => n.TongSoNam1) / donViTinh;
            _tongHienVatNam1 = result.Sum(n => n.NganhPhanCapHienVatNam1) / donViTinh;
            _tongBangTienNam1 = result.Sum(n => n.NganhPhanCapBangTienNam1) / donViTinh;
            _tongTongSoNam2 = result.Sum(n => n.TongSoNam2) / donViTinh;
            _tongHienVatNam2 = result.Sum(n => n.NganhPhanCapHienVatNam2) / donViTinh;
            _tongBangTienNam2 = result.Sum(n => n.NganhPhanCapBangTienNam2) / donViTinh;

            result.Select(n =>
            {
                n.NganhPhanCapHienVatNamNay = n.NganhPhanCapHienVatNamNay / donViTinh;
                n.NganhPhanCapBangTienNamNay = n.NganhPhanCapBangTienNamNay / donViTinh;
                n.NganhChuaPhanCapNamNay = n.NganhChuaPhanCapNamNay / donViTinh;
                n.NganhPhanCapHienVatNam1 = n.NganhPhanCapHienVatNam1 / donViTinh;
                n.NganhPhanCapBangTienNam1 = n.NganhPhanCapBangTienNam1 / donViTinh;
                n.NganhPhanCapHienVatNam2 = n.NganhPhanCapHienVatNam2 / donViTinh;
                n.NganhPhanCapBangTienNam2 = n.NganhPhanCapBangTienNam2 / donViTinh;
                return n;
            }).ToList();
            return result;
        }

        private void PrintReportTongHopDuToanDacThu(ExportType exportType)
        {
            try
            {
                if (ListNganh == null || ListNganh.Where(n => n.IsChecked).Count() == 0)
                {
                    return;
                }
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    int donViTinh = GetDonViTinh();
                    List<DuToanDauNamTongHopDacThuModel> listData = GetDataPrintTongHopDuToanDacThu(ListNganh.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList(), (double)donViTinh, IsInTheoTongHop);

                    if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    {
                        var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(listData.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                        var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                        listData = listData.Where(x => listLNS.Contains(x.LNS)).ToList();
                    }

                    List<DuToanDauNamTongHopDacThuModel> listHeader = new List<DuToanDauNamTongHopDacThuModel>();
                    IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                    if (listData != null && listData.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                           string.Join(",", listData.Select(n => n.XauNoiMa).ToList())).ToList();


                        if (listParent != null && listParent.Count > 0)
                        {
                            //Lấy ra các header
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new DuToanDauNamTongHopDacThuModel
                                {
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    MoTa = item.MoTa,
                                    ChuaPhanCapNamNay = 0,
                                    ChuaPhanCapNamTruoc = 0,
                                    DaPhanCapNamNay = 0,
                                    DaPhanCapNamTruoc = 0,
                                    XauNoiMa = item.XauNoiMa,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }

                            //Lấy cấp cha con
                            foreach (DuToanDauNamTongHopDacThuModel item in listData)
                            {
                                var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                    }


                    //Chỉ lấy đến mức NG
                    listHeader = listHeader.Where(x => x.NG != "").ToList();
                    listHeader = listHeader.OrderBy(x => x.M).OrderBy(x => x.TM).OrderBy(x => x.TTM).OrderBy(x => x.NG).OrderBy(x => x.TNG1).OrderBy(x => x.TNG2).OrderBy(x => x.TNG2).ToList();
                    //Tính tổng các cấp cha
                    foreach (var item in listHeader)
                    {
                        var lstChild = listHeader.Where(x => x.MlnsIdParent == item.MlnsId).ToList();
                        if (lstChild.Count > 0)
                        {
                            item.DaPhanCapNamNay = lstChild.Sum(x => x.DaPhanCapNamNay);
                            item.DaPhanCapNamTruoc = lstChild.Sum(x => x.DaPhanCapNamTruoc);
                            item.ChuaPhanCapNamNay = lstChild.Sum(x => x.ChuaPhanCapNamNay);
                            item.ChuaPhanCapNamTruoc = lstChild.Sum(x => x.ChuaPhanCapNamTruoc);
                        }
                    }


                    //Lấy thông tin chuyên ngành
                    List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_NGANH", _sessionService.Current.YearOfWork).ToList();
                    List<string> listNG = listHeader.Select(n => n.NG).Distinct().ToList();

                    int number = 1;

                    foreach (string nganh in listNG)
                    {
                        int index = listHeader.IndexOf(listHeader.FirstOrDefault(n => n.NG == nganh && n.TNG == ""));
                        if (index >= 0)
                        {
                            DuToanDauNamTongHopDacThuModel itemParent = new DuToanDauNamTongHopDacThuModel();
                            DanhMuc itemDanhMuc = listDanhMuc.FirstOrDefault(x => x.IIDMaDanhMuc == nganh);
                            if (itemDanhMuc != null)
                            {
                                itemParent.MoTa = number + "." + itemDanhMuc.STen;
                                itemParent.IsHangCha = true;
                                itemParent.ChuaPhanCapNamTruoc = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.ChuaPhanCapNamTruoc);
                                itemParent.ChuaPhanCapNamNay = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.ChuaPhanCapNamNay);
                                itemParent.DaPhanCapNamTruoc = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.DaPhanCapNamTruoc);
                                itemParent.DaPhanCapNamNay = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.DaPhanCapNamNay);
                                itemParent.NG = String.Empty;
                                itemParent.TNG = String.Empty;
                                itemParent.TNG1 = String.Empty;
                                itemParent.TNG2 = String.Empty;
                                itemParent.TNG3 = String.Empty;
                                listHeader.Insert(index, itemParent);
                                number++;
                            }
                        }
                    }

                    //Ẩn hiển cột theo combobox
                    List<int> hideColumns = new List<int>();
                    string sTTNoiDung = "G";
                    if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "NG")
                    {
                        listHeader = listHeader.Where(x => x.TNG == "" && x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        hideColumns.Add(8);
                        hideColumns.Add(9);
                        hideColumns.Add(10);
                        hideColumns.Add(11);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG")
                    {
                        listHeader = listHeader.Where(x => x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "H";
                        hideColumns.Add(9);
                        hideColumns.Add(10);
                        hideColumns.Add(11);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG1")
                    {
                        listHeader = listHeader.Where(x => x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "I";
                        hideColumns.Add(10);
                        hideColumns.Add(11);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG2")
                    {
                        listHeader = listHeader.Where(x => x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "J";
                        hideColumns.Add(11);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG3")
                    {
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG3)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "K";
                    }

                    foreach (var item in listHeader)
                    {
                        if (!string.IsNullOrEmpty(item.TNG))
                        {
                            item.M = String.Empty;
                            item.TM = String.Empty;
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

                    listData = new List<DuToanDauNamTongHopDacThuModel>(listHeader);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    //data.Add("Cap1", isPrintPhuLuc ? string.Empty : _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                    //data.Add("Cap2", isPrintPhuLuc ? string.Empty : GetHeader2Report());
                    data.Add("Cap1", GetLevelTitle(1));
                    data.Add("Cap2", GetLevelTitle(2));
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(_tongTongSoNamNay * donViTinh, true));
                    //data.Add("ThoiGian", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("ThoiGian", string.Format("{0}, {1} ", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("Items", listData);
                    data.Add("NamHienTai", _sessionService.Current.YearOfWork.ToString());
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("HeaderCol1", string.Format("Thực hiện năm {0}", (_sessionService.Current.YearOfWork - 2).ToString()));
                    data.Add("HeaderCol2", string.Format("Dự toán năm {0}", (_sessionService.Current.YearOfWork - 1).ToString()));
                    data.Add("HeaderCol3", string.Format("Dự toán năm {0}", _sessionService.Current.YearOfWork.ToString()));
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("sTTNoiDung", sTTNoiDung);



                    data.Add("TongSoNamTruoc", listData.Where(x => !x.IsHangCha).Sum(n => n.TongSoNamTruoc) / donViTinh);
                    data.Add("TongSoNamNay", listData.Where(x => !x.IsHangCha).Sum(n => n.TongSoNamNay) / donViTinh);
                    data.Add("TongDaPhanCapNamTruoc", listData.Where(x => !x.IsHangCha).Sum(n => n.DaPhanCapNamTruoc) / donViTinh);
                    data.Add("TongDaPhanCapNamNay", listData.Where(x => !x.IsHangCha).Sum(n => n.DaPhanCapNamNay) / donViTinh);
                    data.Add("TongChuaPhanCapNamTruoc", listData.Where(x => !x.IsHangCha).Sum(n => n.ChuaPhanCapNamTruoc) / donViTinh);
                    data.Add("TongChuaPhanCapNamNay", listData.Where(x => !x.IsHangCha).Sum(n => n.ChuaPhanCapNamNay) / donViTinh);

                    var ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);

                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_TONGHOP_DACTHU);
                    fileNamePrefix = "rptDuToanDauNamDuToanNam";

                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<DuToanDauNamTongHopDacThuModel, GhiChu>(templateFileName, data, hideColumns);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
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

        private void PrintReportDuToanDuToanNam(ExportType exportType)
        {
            try
            {
                if (ListNganh == null || ListNganh.Where(n => n.IsChecked).Count() == 0)
                {
                    return;
                }
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    int donViTinh = GetDonViTinh();
                    List<DuToanDauNamChiThuongXuyenModel> listData = GetDataPrintDuToanNam(ListNganh.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList(), (double)donViTinh, IsInTheoTongHop);

                    if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    {
                        var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(listData.Select(x => x.SLNS).ToList(), _sessionService.Current.YearOfWork);
                        var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                        listData = listData.Where(x => listLNS.Contains(x.SLNS)).ToList();
                    }

                    List<DuToanDauNamChiThuongXuyenModel> listHeader = new List<DuToanDauNamChiThuongXuyenModel>();
                    IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                    if (listData != null && listData.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                           string.Join(",", listData.Select(n => n.XauNoiMa).ToList())).ToList();


                        if (listParent != null && listParent.Count > 0)
                        {
                            //Lấy ra các header
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new DuToanDauNamChiThuongXuyenModel
                                {
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    MoTa = item.MoTa,
                                    NganhPhanCapBangTienNamNay = 0,
                                    NganhPhanCapHienVatNamNay = 0,
                                    NganhChuaPhanCapNamNay = 0,
                                    NganhPhanCapHienVatNam1 = 0,
                                    NganhPhanCapBangTienNam1 = 0,
                                    NganhPhanCapHienVatNam2 = 0,
                                    NganhPhanCapBangTienNam2 = 0,
                                    XauNoiMa = item.XauNoiMa,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }

                            //Lấy cấp cha con
                            foreach (DuToanDauNamChiThuongXuyenModel item in listData)
                            {
                                var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                    }


                    //Chỉ lấy đến mức NG
                    listHeader = listHeader.Where(x => x.NG != "").ToList();
                    listHeader = listHeader.OrderBy(x => x.M).OrderBy(x => x.TM).OrderBy(x => x.TTM).OrderBy(x => x.NG).OrderBy(x => x.TNG1).OrderBy(x => x.TNG2).OrderBy(x => x.TNG2).ToList();
                    //Tính tổng các cấp cha
                    foreach (var item in listHeader)
                    {
                        var lstChild = listHeader.Where(x => x.MlnsIdParent == item.MlnsId).ToList();
                        if (lstChild.Count > 0)
                        {
                            item.NganhPhanCapHienVatNamNay = lstChild.Sum(x => x.NganhPhanCapHienVatNamNay);
                            item.NganhPhanCapBangTienNamNay = lstChild.Sum(x => x.NganhPhanCapBangTienNamNay);
                            item.NganhChuaPhanCapNamNay = lstChild.Sum(x => x.NganhChuaPhanCapNamNay);
                            item.NganhPhanCapHienVatNam1 = lstChild.Sum(x => x.NganhPhanCapHienVatNam1);
                            item.NganhPhanCapBangTienNam1 = lstChild.Sum(x => x.NganhPhanCapBangTienNam1);
                            item.NganhPhanCapHienVatNam2 = lstChild.Sum(x => x.NganhPhanCapHienVatNam2);
                            item.NganhPhanCapBangTienNam2 = lstChild.Sum(x => x.NganhPhanCapBangTienNam2);
                        }
                    }


                    //Lấy thông tin chuyên ngành
                    List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_NGANH", _sessionService.Current.YearOfWork).ToList();
                    List<string> listNG = listHeader.Select(n => n.NG).Distinct().ToList();

                    int number = 1;

                    foreach (string nganh in listNG)
                    {
                        int index = listHeader.IndexOf(listHeader.FirstOrDefault(n => n.NG == nganh && n.TNG == ""));
                        if (index >= 0)
                        {
                            DuToanDauNamChiThuongXuyenModel itemParent = new DuToanDauNamChiThuongXuyenModel();
                            DanhMuc itemDanhMuc = listDanhMuc.FirstOrDefault(x => x.IIDMaDanhMuc == nganh);
                            if (itemDanhMuc != null)
                            {
                                itemParent.MoTa = number + "." + itemDanhMuc.STen;
                                itemParent.IsHangCha = true;
                                itemParent.NganhPhanCapHienVatNamNay = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapHienVatNamNay);
                                itemParent.NganhPhanCapBangTienNamNay = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapBangTienNamNay);
                                itemParent.NganhChuaPhanCapNamNay = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhChuaPhanCapNamNay);
                                itemParent.NganhPhanCapHienVatNam1 = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapHienVatNam1);
                                itemParent.NganhPhanCapBangTienNam1 = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapBangTienNam1);
                                itemParent.NganhPhanCapHienVatNam2 = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapHienVatNam2);
                                itemParent.NganhPhanCapBangTienNam2 = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapBangTienNam2);
                                itemParent.NG = String.Empty;
                                itemParent.TNG = String.Empty;
                                itemParent.TNG1 = String.Empty;
                                itemParent.TNG2 = String.Empty;
                                itemParent.TNG3 = String.Empty;
                                listHeader.Insert(index, itemParent);
                                number++;
                            }
                        }
                    }

                    //Ẩn hiển cột theo combobox
                    List<int> hideColumns = new List<int>();
                    string sTTNoiDung = "F";
                    if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "NG")
                    {
                        listHeader = listHeader.Where(x => x.TNG == "" && x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        hideColumns.Add(7);
                        hideColumns.Add(8);
                        hideColumns.Add(9);
                        hideColumns.Add(10);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG")
                    {
                        listHeader = listHeader.Where(x => x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "G";
                        hideColumns.Add(8);
                        hideColumns.Add(9);
                        hideColumns.Add(10);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG1")
                    {
                        listHeader = listHeader.Where(x => x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "H";
                        hideColumns.Add(9);
                        hideColumns.Add(10);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG2")
                    {
                        listHeader = listHeader.Where(x => x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "I";
                        hideColumns.Add(10);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG3")
                    {
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG3)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "J";
                    }

                    foreach (var item in listHeader)
                    {
                        if (!string.IsNullOrEmpty(item.TNG))
                        {
                            item.M = String.Empty;
                            item.TM = String.Empty;
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

                    listData = new List<DuToanDauNamChiThuongXuyenModel>(listHeader);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    //data.Add("Cap1", isPrintPhuLuc ? string.Empty : _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                    //data.Add("Cap2", isPrintPhuLuc ? string.Empty : GetHeader2Report());
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(_tongTongSoNamNay * donViTinh, true));
                    //data.Add("ThoiGian", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("ThoiGian", string.Format("{0}, {1} ", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("Items", listData);
                    data.Add("NamHienTai", _sessionService.Current.YearOfWork.ToString());
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("HeaderCol1", string.Format("Thực hiện năm {0}", (_sessionService.Current.YearOfWork - 2).ToString()));
                    data.Add("HeaderCol2", string.Format("Dự toán giao đầu năm {0}", (_sessionService.Current.YearOfWork - 1).ToString()));
                    data.Add("HeaderCol3", string.Format("Dự toán năm {0}", _sessionService.Current.YearOfWork.ToString()));
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("sTTNoiDung", sTTNoiDung);

                    data.Add("TongTongSoNam2", _tongTongSoNam2);
                    data.Add("TongHienVatNam2", _tongHienVatNam2);
                    data.Add("TongBangTienNam2", _tongBangTienNam2);
                    data.Add("TongTongSoNam1", _tongTongSoNam1);
                    data.Add("TongHienVatNam1", _tongHienVatNam1);
                    data.Add("TongBangTienNam1", _tongBangTienNam1);
                    data.Add("TongTongSoNamNay", _tongTongSoNamNay);
                    data.Add("TongHienVatNamNay", _tongHienVatNamNay);
                    data.Add("TongBangTienNamNay", _tongBangTienNamNay);
                    data.Add("TongChuaPhanCapNamNay", _tongChuaPhanCapNamNay);
                    var ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);

                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_DUTOANNAM_All);
                    fileNamePrefix = "rptDuToanDauNamDuToanNam";

                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<DuToanDauNamChiThuongXuyenModel, GhiChu>(templateFileName, data, hideColumns);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
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

        private class GhiChu
        {
            public string Content { get; set; }
            //public string SGhiChu => $"- {Content}";
            public string SGhiChu => Content;
        }
        private List<GhiChu> GetGhiChu()
        {
            var iNamLamViec = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhBaoCao>();
            predicate = predicate.And(x => x.INamLamViec.Equals(iNamLamViec));
            predicate = predicate.And(x => x.SMaBaoCao == _typeChuKy);
            predicate = predicate.And(x => x.SMaGhiChu == GetMaGhiChu());
            var data = _ghiChuService.FindByCondition(predicate).ToList();
            if (!string.IsNullOrEmpty(data.FirstOrDefault()?.SGhiChu))
            {
                return data.FirstOrDefault()?.SGhiChu.Split(Environment.NewLine).Select(x => new GhiChu()
                {
                    Content = x
                }).ToList();
            }
            else
            {
                return new List<GhiChu>();
            }
        }

        private string GetMaGhiChu()
        {
            if (LoaiBaoCao != null
                 && SelectedBQuanLy != null)
            {
                var data = JsonConvert.SerializeObject(new
                {
                    LoaiBaoCao = LoaiBaoCao.FirstOrDefault(x => x.IsChecked).DisplayItem,
                    InTheoChungTuTongHop = IsInTheoTongHop,
                    BQuanLy = SelectedBQuanLy.DisplayItem,
                });
                return CompressExtension.CompressToBase64(data);
            }
            else
                return string.Empty;
        }

        private void LoadTypeChuKy()
        {
            string loaiBaoCao = LoaiBaoCao.FirstOrDefault(n => n.IsChecked)?.DisplayItem;
            if (loaiBaoCao is object)
            {
                if (loaiBaoCao == Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_NGANG)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_DACTHU_NGANG;
                }
                else if (loaiBaoCao == Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_DACTHU_DOC;
                }
                else if (loaiBaoCao == Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_CHIMUAHANGTAPTRUNG_CAPHIENVAT)
                {
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_MHHV;
                }
                else
                {
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN;
                }
            }
            LoadTieuDe();
        }

        private void PrintReportDuToanChiMuaHangTapTrung(ExportType exportType)
        {
            try
            {
                if (ListNganh == null || ListNganh.Where(n => n.IsChecked).Count() == 0)
                {
                    return;
                }
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    int donViTinh = GetDonViTinh();
                    List<DuToanDauNamChiThuongXuyenModel> listData = GetDataPrintDuToanNamChiTapTrungCapHienVat(ListNganh.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList(), (double)donViTinh, IsInTheoTongHop);
                    if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    {
                        var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(listData.Select(x => x.SLNS).ToList(), _sessionService.Current.YearOfWork);
                        var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                        listData = listData.Where(x => listLNS.Contains(x.SLNS)).ToList();
                    }
                    List<DuToanDauNamChiThuongXuyenModel> listHeader = new List<DuToanDauNamChiThuongXuyenModel>();
                    IEnumerable<NsMucLucNganSach> lstmuclucngansach = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                    if (listData != null && listData.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionService.Current.YearOfWork,
                           string.Join(",", listData.Select(n => n.XauNoiMa).ToList())).ToList();


                        if (listParent != null && listParent.Count > 0)
                        {
                            //Lấy ra các header
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new DuToanDauNamChiThuongXuyenModel
                                {
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
                                    NganhPhanCapBangTienNamNay = 0,
                                    NganhPhanCapHienVatNamNay = 0,
                                    NganhChuaPhanCapNamNay = 0,
                                    NganhPhanCapHienVatNam1 = 0,
                                    NganhPhanCapBangTienNam1 = 0,
                                    NganhPhanCapHienVatNam2 = 0,
                                    NganhPhanCapBangTienNam2 = 0,
                                    XauNoiMa = item.XauNoiMa,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }

                            //Lấy cấp cha con
                            foreach (DuToanDauNamChiThuongXuyenModel item in listData)
                            {
                                var mlns = lstmuclucngansach.Where(x => x.XauNoiMa == item.XauNoiMa).FirstOrDefault();
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                    }


                    //Chỉ lấy đến mức NG
                    listHeader = listHeader.Where(x => x.NG != "").ToList();
                    listHeader = listHeader.OrderBy(x => x.SLNS).OrderBy(x => x.M).OrderBy(x => x.TM).OrderBy(x => x.TTM).OrderBy(x => x.NG).OrderBy(x => x.TNG1).OrderBy(x => x.TNG2).OrderBy(x => x.TNG2).ToList();
                    //Tính tổng các cấp cha
                    foreach (var item in listHeader)
                    {
                        var lstChild = listHeader.Where(x => x.MlnsIdParent == item.MlnsId).ToList();
                        if (lstChild.Count > 0)
                        {
                            item.NganhPhanCapHienVatNamNay = lstChild.Sum(x => x.NganhPhanCapHienVatNamNay);
                            item.NganhPhanCapBangTienNamNay = lstChild.Sum(x => x.NganhPhanCapBangTienNamNay);
                            item.NganhChuaPhanCapNamNay = lstChild.Sum(x => x.NganhChuaPhanCapNamNay);
                            item.NganhPhanCapHienVatNam1 = lstChild.Sum(x => x.NganhPhanCapHienVatNam1);
                            item.NganhPhanCapBangTienNam1 = lstChild.Sum(x => x.NganhPhanCapBangTienNam1);
                            item.NganhPhanCapHienVatNam2 = lstChild.Sum(x => x.NganhPhanCapHienVatNam2);
                            item.NganhPhanCapBangTienNam2 = lstChild.Sum(x => x.NganhPhanCapBangTienNam2);
                        }
                    }


                    //Lấy thông tin chuyên ngành
                    List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_NGANH", _sessionService.Current.YearOfWork).ToList();
                    List<string> listNG = listHeader.Select(n => n.NG).Distinct().ToList();

                    int number = 1;

                    foreach (string nganh in listNG)
                    {
                        int index = listHeader.IndexOf(listHeader.FirstOrDefault(n => n.NG == nganh && n.TNG == ""));
                        if (index >= 0)
                        {
                            DuToanDauNamChiThuongXuyenModel itemParent = new DuToanDauNamChiThuongXuyenModel();
                            DanhMuc itemDanhMuc = listDanhMuc.FirstOrDefault(x => x.IIDMaDanhMuc == nganh);
                            if (itemDanhMuc != null)
                            {
                                itemParent.MoTa = number + "." + itemDanhMuc.STen;
                                itemParent.IsHangCha = true;
                                itemParent.NganhPhanCapHienVatNamNay = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapHienVatNamNay);
                                itemParent.NganhPhanCapBangTienNamNay = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapBangTienNamNay);
                                itemParent.NganhChuaPhanCapNamNay = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhChuaPhanCapNamNay);
                                itemParent.NganhPhanCapHienVatNam1 = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapHienVatNam1);
                                itemParent.NganhPhanCapBangTienNam1 = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapBangTienNam1);
                                itemParent.NganhPhanCapHienVatNam2 = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapHienVatNam2);
                                itemParent.NganhPhanCapBangTienNam2 = listHeader.Where(n => n.NG == nganh && n.TNG == "").Sum(n => n.NganhPhanCapBangTienNam2);
                                itemParent.NG = String.Empty;
                                itemParent.TNG = String.Empty;
                                itemParent.TNG1 = String.Empty;
                                itemParent.TNG2 = String.Empty;
                                itemParent.TNG3 = String.Empty;
                                listHeader.Insert(index, itemParent);
                                number++;
                            }
                        }
                    }

                    //Ẩn hiển cột theo combobox
                    string sTTNoiDung = "M";
                    List<int> hideColumns = new List<int>();
                    if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "NG")
                    {
                        listHeader = listHeader.Where(x => x.TNG == "" && x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "H";
                        hideColumns.Add(9);
                        hideColumns.Add(10);
                        hideColumns.Add(11);
                        hideColumns.Add(12);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG")
                    {
                        listHeader = listHeader.Where(x => x.TNG1 == "" && x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "I";
                        hideColumns.Add(10);
                        hideColumns.Add(11);
                        hideColumns.Add(12);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG1")
                    {
                        listHeader = listHeader.Where(x => x.TNG2 == "" && x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "J";
                        hideColumns.Add(11);
                        hideColumns.Add(12);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG2")
                    {
                        listHeader = listHeader.Where(x => x.TNG3 == "").ToList();
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "K";
                        hideColumns.Add(12);
                    }
                    else if (SelectedInToiMuc != null && SelectedInToiMuc.ValueItem == "TNG3")
                    {
                        listHeader.Where(x => !string.IsNullOrEmpty(x.TNG3)).Select(x => x.IsHangCha = false).ToList();
                        sTTNoiDung = "M";
                    }

                    foreach (var item in listHeader)
                    {
                        if (!string.IsNullOrEmpty(item.TNG))
                        {
                            item.M = String.Empty;
                            item.TM = String.Empty;
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

                    listData = new List<DuToanDauNamChiThuongXuyenModel>(listHeader);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", GetLevelTitle(1));
                    data.Add("Cap2", GetLevelTitle(2));
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(_tongTongSoNamNay * donViTinh, true));
                    //data.Add("ThoiGian", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("ThoiGian", string.Format("{0}, {1} ", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("Items", listData);
                    data.Add("NamHienTai", _sessionService.Current.YearOfWork.ToString());
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("HeaderCol1", string.Format("Thực hiện năm {0}", (_sessionService.Current.YearOfWork - 2).ToString()));
                    data.Add("HeaderCol2", string.Format("Dự toán giao đầu năm {0}", (_sessionService.Current.YearOfWork - 1).ToString()));
                    data.Add("HeaderCol3", string.Format("Dự toán năm {0}", _sessionService.Current.YearOfWork.ToString()));
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("sTTNoiDung", sTTNoiDung);

                    data.Add("TongTongSoNam2", _tongTongSoNam2);
                    data.Add("TongHienVatNam2", _tongHienVatNam2);
                    data.Add("TongBangTienNam2", _tongBangTienNam2);
                    data.Add("TongTongSoNam1", _tongTongSoNam1);
                    data.Add("TongHienVatNam1", _tongHienVatNam1);
                    data.Add("TongBangTienNam1", _tongBangTienNam1);
                    data.Add("TongTongSoNamNay", _tongTongSoNamNay);
                    data.Add("TongHienVatNamNay", _tongHienVatNamNay);
                    data.Add("TongBangTienNamNay", _tongBangTienNamNay);
                    data.Add("TongChuaPhanCapNamNay", _tongChuaPhanCapNamNay);
                    var ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);

                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_CHIMUAHANGTAPTRUNG_CAPHIENVAT);
                    fileNamePrefix = "rptDuToanDauNamDuToanNam";
                    //var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";

                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<DuToanDauNamChiThuongXuyenModel, GhiChu>(templateFileName, data, hideColumns);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
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

        private string GetLevelTitle(int level)
        {
            var danhMucChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (danhMucChuKy is null) return string.Empty;
            var loaiDVBanHanh = danhMucChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(danhMucChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => "TÊN ĐƠN VỊ ĐƯỢC CHỌN",
                LoaiDonViBanHanh.TUY_CHINH => danhMucChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(danhMucChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
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

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuKy;
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
                _chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(_chiTietToi));
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
            }
        }
    }
}

