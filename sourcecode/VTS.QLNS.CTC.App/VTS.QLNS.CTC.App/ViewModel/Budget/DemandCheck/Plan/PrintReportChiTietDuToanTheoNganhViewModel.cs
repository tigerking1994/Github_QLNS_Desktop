using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using DanhMuc = VTS.QLNS.CTC.Core.Domain.DanhMuc;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PrintReportChiTietDuToanTheoNganhViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _nNganhCollectionView;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChungTuService _sktSoLieuChungTuService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsPhongBanService _iNsPhongBanService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        private string _diaDiem;
        private EstimationVoucherDetailCriteria _searchCondition;

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public override string Name
        {
            get => $"Báo cáo chi tiết dự toán đầu năm theo ngành năm {_sessionInfo.YearOfWork}";
        }

        public override string Title
        {
            get => $"Báo cáo chi tiết dự toán đầu năm theo ngành năm {_sessionInfo.YearOfWork}";
        }

        public override string Description
        {
            get => $"Báo cáo chi tiết dự toán đầu năm theo ngành năm {_sessionInfo.YearOfWork}";
        }

        public List<Guid> ListIdChungTuBaoCaoSncNganh { get; set; }
        //public override Type ContentType => typeof(PrintReportDemandOrg);
        public static DemandCheckPrintType DemandCheckPrintType { get; set; }

        private string _txtTitleFirst;

        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
                //SetProperty(ref _txtTitleSecond, "(Kèm theo Quyết định số ........., ngày 11/05/2021)");
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


        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set
            {
                SetProperty(ref _paperPrintTypeSelected, value);
                LoadTitleFirst();
                LoadNNganh();
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

        private ObservableCollection<CheckBoxItem> _listNNganh = new ObservableCollection<CheckBoxItem>();

        public ObservableCollection<CheckBoxItem> ListNNganh
        {
            get => _listNNganh;
            set => SetProperty(ref _listNNganh, value);
        }

        private bool _selectAllNNganh;

        public bool SelectAllNNganh
        {
            get
            {
                if (ListNNganh != null)
                {
                    return ListNNganh.All(item => item.IsChecked);
                }
                return false;
            }

            set
            {
                SetProperty(ref _selectAllNNganh, value);
                foreach (var item in ListNNganh) item.IsChecked = _selectAllNNganh;
            }
        }

        private ObservableCollection<CheckBoxItem> _listChuyenNganh = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListChuyenNganh
        {
            get => _listChuyenNganh;
            set => SetProperty(ref _listChuyenNganh, value);
        }

        public string LabelSelectedCountNNganh
        {
            get
            {
                if (IsChuyenNganh)
                {
                    return ListNNganh != null
                        ? $"CHUYÊN NGÀNH ({ListNNganh.Count(item => item.IsChecked)}/{ListNNganh.Count})"
                        : "CHUYÊN NGÀNH (0/0)";
                }
                return ListNNganh != null
                    ? $"NGÀNH ({ListNNganh.Count(item => item.IsChecked)}/{ListNNganh.Count})"
                    : "NGÀNH (0/0)";
            }
        }

        private string _searchNNganh;

        public string SearchNNganh
        {
            get => _searchNNganh;
            set
            {
                if (SetProperty(ref _searchNNganh, value))
                {
                    _nNganhCollectionView.Refresh();
                }
            }
        }



        private bool _isChuyenNganh;
        public bool IsChuyenNganh
        {
            get => _isChuyenNganh;
            set
            {
                if (SetProperty(ref _isChuyenNganh, value))
                {
                    LoadNNganh();
                }
                OnPropertyChanged(nameof(LabelSelectedCountNNganh));
                OnPropertyChanged(nameof(SelectAllNNganh));
            }
        }

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadNNganh();
            }
        }

        public PrintReportChiTietDuToanTheoNganhViewModel(INsDonViService nsDonViService, IExportService exportService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsMucLucNganSachService nsMucLucNganSachService,
            ISktChungTuService sktChungTuService,
            ISktSoLieuService sktSoLieuService,
            ISktSoLieuChungTuService sktSoLieuChungTuService,
            IDanhMucService danhMucService,
            ISktMucLucService iSktMucLucService,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            INsPhongBanService iNsPhongBanService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IMapper mapper,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _sktSoLieuService = sktSoLieuService;
            _sktSoLieuChungTuService = sktSoLieuChungTuService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _iNsPhongBanService = iNsPhongBanService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            _isInTheoTongHop = false;

            InitReportDefaultDate();
            LoadCatUnitTypes();
            LoadNNganh();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadPaperPrintTypes();
            LoadKieuGiayIn();
            LoadBQuanLy();

            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadBQuanLy()
        {
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            List<DmBQuanLy> listPhongBan = _iNsPhongBanService.FindByCondition(predicate).OrderBy(x => x.IIDMaBQuanLy).ToList();
            _bQuanLy = _mapper.Map<List<ComboboxItem>>(listPhongBan);
            if (_bQuanLy.Count() > 0)
            {
                _bQuanLy.Insert(0, new ComboboxItem("Tất cả", string.Empty));
                SelectedBQuanLy = _bQuanLy.FirstOrDefault();
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
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

        private void LoadTypeChuKy()
        {
            _typeChuky = TypeChuKy.RPT_NS_DUTOANDAUNAM_CHUYENNGANH;
        }

        public void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }


        public void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>();
            paperPrintTypes = new List<ComboboxItem>
                {
                    //new ComboboxItem {DisplayItem = "Biểu trình ký", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị ngang - mục lục dọc", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị dọc, mục lục ngang", ValueItem = "2"}
                };
            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            _paperPrintTypeSelected = paperPrintTypes.ElementAt(0);
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
            _catUnitTypeSelected = _catUnitTypes.FirstOrDefault();
        }

        public void LoadNNganh()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var iTrangThai = StatusType.ACTIVE;

            var predicateChungTu = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
            predicateChungTu = predicateChungTu.And(x => x.INamLamViec == yearOfWork);
            predicateChungTu = predicateChungTu.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicateChungTu = predicateChungTu.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicateChungTu = predicateChungTu.And(x => Convert.ToInt16(x.ILoaiChungTu) == int.Parse(VoucherType.NSSD_Key));
            predicateChungTu = predicateChungTu.And(x => x.FTuChi != 0);
            var lstCtChiTietChungTu = _sktSoLieuService.FindByCondition(predicateChungTu).ToList();

            List<string> lstChuyenNganh = new List<string>();
            if (lstCtChiTietChungTu != null && lstCtChiTietChungTu.Count() > 0)
            {
                var lstNG = lstCtChiTietChungTu.Select(x => x.SNg).Distinct().ToList();

                var lstMucLuc = _nsMucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork
                                                                        && x.ITrangThai == StatusType.ACTIVE
                                                                        && lstNG.Contains(x.Ng)).ToList();

                lstChuyenNganh = lstMucLuc.Select(x => x.Ng).Distinct().ToList();
            }

            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            predicate = predicate.And(item => item.ITrangThai == iTrangThai);

            // lấy list chuyên ngành
            var predicateChuyenNganh = predicate.And(item => VoucherType.DM_Nganh.Equals(item.SType));
            predicateChuyenNganh = predicateChuyenNganh.And(item => lstChuyenNganh.Contains(item.IIDMaDanhMuc));
            var lstDmChuyenNganh = _danhMucService.FindByCondition(predicateChuyenNganh).ToList();
            var result = lstDmChuyenNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDanhMuc,
                DisplayItem = string.Join("-", item.IIDMaDanhMuc, item.STen),
                NameItem = item.STen
            }).OrderBy(item => item.ValueItem);
            ListChuyenNganh = new ObservableCollection<CheckBoxItem>(result);
            if (IsChuyenNganh)
            {
                ListNNganh = ListChuyenNganh;
            }
            else
            {
                var predicateNhomNganh = predicate.And(item => VoucherType.VOCHER_TYPE.Equals(item.SType));
                var lstDmNganh = _danhMucService.FindByCondition(predicateNhomNganh).ToList();
                lstDmNganh = lstDmNganh.Where(item => lstChuyenNganh.Any(x => item.SGiaTri.Contains(x))).ToList();
                var resultN = lstDmNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
                {
                    ValueItem = item.SGiaTri,
                    DisplayItem = string.Join("-", item.IIDMaDanhMuc, item.STen),
                    NameItem = item.STen
                }).OrderBy(item => item.ValueItem);
                ListNNganh = new ObservableCollection<CheckBoxItem>(resultN);
            }

            // Filter
            _nNganhCollectionView = CollectionViewSource.GetDefaultView(ListNNganh);
            _nNganhCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchNNganh)
                || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchNNganh, StringComparison.OrdinalIgnoreCase));

            foreach (var org in ListNNganh)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountNNganh));
                    OnPropertyChanged(nameof(SelectAllNNganh));
                };
            }
        }


        public void OnExport(ExportType exportType)
        {
            if (_paperPrintTypeSelected.ValueItem == "1")
            {
                OnPrintReportTheoNganhMucLucDoc(exportType);
            }
            else
            {
                OnPrintReportTheoNganhMucLucNgang(exportType);
            }
        }

        //public void LoadNNganhSnc()
        //{
        //    var yearOfWork = _sessionInfo.YearOfWork;
        //    var iTrangThai = StatusType.ACTIVE;
        //    ListIdChungTuBaoCaoSncNganh = new List<Guid>();

        //    var predicateTh = PredicateBuilder.True<NsDtdauNamChungTu>();
        //    predicateTh = predicateTh.And(x => x.INamLamViec == yearOfWork);
        //    predicateTh = predicateTh.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
        //    predicateTh = predicateTh.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
        //    predicateTh = predicateTh.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key));
        //    predicateTh = predicateTh.And(x => x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi));

        //    var ctTongHop = _sktSoLieuChungTuService.FindByCondition(predicateTh).FirstOrDefault();
        //    List<NsDtdauNamChungTuChiTiet> lstCtChiTiet = new List<NsDtdauNamChungTuChiTiet>();
        //    if (IsInTheoTongHop)
        //    {
        //        if (ctTongHop != null)
        //        {
        //            //var predicateChungTu = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
        //            //predicateChungTu = predicateChungTu.And(x => x.INamLamViec == yearOfWork);
        //            //predicateChungTu = predicateChungTu.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
        //            //predicateChungTu = predicateChungTu.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
        //            ////predicateChungTu = predicateChungTu.And(x => x.ILoai == DemandCheckType.DEMAND);

        //            //predicateChungTu = predicateChungTu.And(x => Convert.ToUInt32(x.ILoaiChungTu) == int.Parse(VoucherType.NSSD_Key));
        //            //if (!string.IsNullOrEmpty(ctTongHop.SDSSoChungTuTongHop))
        //            //{
        //            //    predicateChungTu = predicateChungTu.And(x => ctTongHop.SDSSoChungTuTongHop.Contains(x.s));
        //            //}
        //            //else
        //            //{
        //            //    predicateChungTu = predicateChungTu.And(x => x.IIdCtsoKiemTra == ctTongHop.Id);
        //            //}

        //            //lstCtChiTiet = _sktSoLieuService.FindByCondition(predicateChungTu).ToList();
        //            //ListIdChungTuBaoCaoSncNganh = lstCtChiTiet.Select(x => x.IIdCtdtdauNam).Distinct().ToList();
        //        }
        //    }
        //    else
        //    {
        //        IEnumerable<NsDtdauNamChungTu> listChungTu;

        //        var predicateChungTu = PredicateBuilder.True<NsDtdauNamChungTu>();
        //        predicateChungTu = predicateChungTu.And(x => x.INamLamViec == yearOfWork);
        //        predicateChungTu = predicateChungTu.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
        //        predicateChungTu = predicateChungTu.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key));
        //        predicateChungTu = predicateChungTu.And(x => x.IIdMaNguonNganSach == Convert.ToInt16(_sessionInfo.Principal));
        //        listChungTu = _sktSoLieuChungTuService.FindByCondition(predicateChungTu).ToList();

        //        var lstIdChungTuKhoa = listChungTu.Select(x => x.Id).ToList();
        //        ListIdChungTuBaoCaoSncNganh.AddRange(lstIdChungTuKhoa);

        //        var predicateChungTuChiTiet = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
        //        predicateChungTuChiTiet = predicateChungTuChiTiet.And(x => x.INamLamViec == yearOfWork);
        //        predicateChungTuChiTiet = predicateChungTuChiTiet.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
        //        predicateChungTuChiTiet = predicateChungTuChiTiet.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
        //        //predicateChungTu = predicateChungTu.And(x => x.ILoai == DemandCheckType.DEMAND);
        //        if(lstIdChungTuKhoa.Count() > 0)
        //        {
        //            predicateChungTuChiTiet = predicateChungTuChiTiet.And(x => lstIdChungTuKhoa.Contains(x.IIdCtdtdauNam.Value));
        //        }
        //        lstCtChiTiet = _sktSoLieuService.FindByCondition(predicateChungTuChiTiet).ToList();


        //    }


        //    List<string> lstChuyenNganh = new List<string>();
        //    if (lstCtChiTiet.Count() > 0)
        //    {
        //        var lstIdMl = lstCtChiTiet.Select(x => x.SXauNoiMa).Distinct().ToList();
        //        var lstMucLuc = _nsMucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork
        //                                                                && x.ITrangThai == StatusType.ACTIVE
        //                                                                && lstIdMl.Contains(x.XauNoiMa)).ToList();

        //        lstChuyenNganh = lstMucLuc.Select(x => x.Ng).Distinct().ToList();
        //    }

        //    var predicate = PredicateBuilder.True<DanhMuc>();
        //    predicate = predicate.And(item => item.INamLamViec == yearOfWork);
        //    predicate = predicate.And(item => item.ITrangThai == iTrangThai);
        //    // lấy list chuyên ngành
        //    var predicateChuyenNganh = predicate.And(item => VoucherType.DM_Nganh.Equals(item.SType));
        //    predicateChuyenNganh = predicateChuyenNganh.And(item => lstChuyenNganh.Contains(item.IIDMaDanhMuc));
        //    var lstDmChuyenNganh = _danhMucService.FindByCondition(predicateChuyenNganh).ToList();
        //    var result = lstDmChuyenNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
        //    {
        //        ValueItem = item.IIDMaDanhMuc,
        //        DisplayItem = string.Join("-", item.IIDMaDanhMuc, item.STen),
        //        NameItem = item.STen
        //    }).OrderBy(item => item.ValueItem);
        //    ListChuyenNganh = new ObservableCollection<CheckBoxItem>(result);
        //    if (IsChuyenNganh)
        //    {
        //        ListNNganh = ListChuyenNganh;
        //    }
        //    else
        //    {
        //        var predicateNhomNganh = predicate.And(item => VoucherType.VOCHER_TYPE.Equals(item.SType));
        //        var lstDmNganh = _danhMucService.FindByCondition(predicateNhomNganh).ToList();
        //        lstDmNganh = lstDmNganh.Where(item => lstChuyenNganh.Any(x => item.SGiaTri.Contains(x))).ToList();
        //        var resultN = lstDmNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
        //        {
        //            ValueItem = item.SGiaTri,
        //            DisplayItem = string.Join("-", item.IIDMaDanhMuc, item.STen),
        //            NameItem = item.STen
        //        }).OrderBy(item => item.ValueItem);
        //        ListNNganh = new ObservableCollection<CheckBoxItem>(resultN);
        //    }

        //    // Filter
        //    _nNganhCollectionView = CollectionViewSource.GetDefaultView(ListNNganh);
        //    _nNganhCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchNNganh)
        //        || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchNNganh, StringComparison.OrdinalIgnoreCase));

        //    foreach (var org in ListNNganh)
        //    {
        //        org.PropertyChanged += (sender, args) =>
        //        {
        //            OnPropertyChanged(nameof(LabelSelectedCountNNganh));
        //            OnPropertyChanged(nameof(SelectAllNNganh));
        //        };
        //    }
        //}

        public void OnPrintReportTheoNganhMucLucNgang(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DEMAND;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var tenNganh = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.NameItem).ToList());
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportDuToanDauNamTheoNganhPhuLucQuery> listDataDonVi = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();

                    if (ListNNganh != null)
                    {
                        var lstNNganhSelected = ListNNganh.Where(item => item.IsChecked).ToList();
                        foreach (var nnganh in lstNNganhSelected)
                        {
                            if (!StringUtils.IsNullOrEmpty(nnganh.ValueItem))
                            {
                                string lstChungTuSnc = null;
                                if (ListIdChungTuBaoCaoSncNganh != null)
                                {
                                    lstChungTuSnc = string.Join(",", ListIdChungTuBaoCaoSncNganh);
                                }

                                listDataDonVi = _sktSoLieuService.FindReportDuToanDauNamTheoNganhPhuLuc(nnganh.ValueItem, null, lstChungTuSnc, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh, IsInTheoTongHop)
                                    .ToList();
                                if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                                {
                                    var lns = _nsMucLucNganSachService.FindByListLnsDonVi(listDataDonVi.Select(x => x.sLNS).ToList(), _sessionService.Current.YearOfWork);
                                    var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                                    listDataDonVi = listDataDonVi.Where(x => listLNS.Contains(x.sLNS)).ToList();
                                }
                                listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();


                                tenNganh = nnganh.NameItem;
                                var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                                var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
                                var listIdMucLuc = listDataDonVi.Select(x => x.iID_MLNS.Value).Distinct().ToList();
                                var sktMucLucs = FindListParentMucLucByChild(listIdMucLuc);
                                foreach (var mlc in sktMucLucs)
                                {
                                    if (!listIdMucLuc.Contains(mlc.MlnsId))
                                    {
                                        foreach (var dv in listDonVi)
                                        {
                                            ReportDuToanDauNamTheoNganhPhuLucQuery mlCha = new ReportDuToanDauNamTheoNganhPhuLucQuery();
                                            mlCha.BHangCha = mlc.BHangCha;
                                            mlCha.iID_MLNS_Cha = mlc.MlnsIdParent;
                                            mlCha.iID_MLNS = mlc.MlnsId;
                                            mlCha.IdDonVi = dv.IIDMaDonVi;
                                            mlCha.sNG = mlc.Ng;
                                            mlCha.sTNG = mlc.Tng;
                                            mlCha.sMoTa = mlc.MoTa;
                                            mlCha.sXauNoiMa = mlc.XauNoiMa;
                                            listDataDonVi.Add(mlCha);
                                        }
                                    }
                                }

                                CalculateData(listDataDonVi);
                                listDataDonVi = listDataDonVi.Where(x => !string.IsNullOrEmpty(x.sNG) && string.IsNullOrEmpty(x.sTNG)).ToList();
                                listIdMucLuc = listDataDonVi.Select(x => x.iID_MLNS.Value).Distinct().ToList();
                                var nsMucLucs = _nsMucLucNganSachService.FindByCondition(x => listIdMucLuc.Contains(x.MlnsId) && x.NamLamViec == yearOfWork).OrderBy(x => x.Ng).ToList();
                                List<NsMucLucNganSach> lstMlResult = new List<NsMucLucNganSach>();
                                List<string> lstNg = new List<string>();
                                foreach (var mucluc in nsMucLucs)
                                {
                                    //if (IsChuyenNganh)
                                    //{
                                    if (lstNg.Count <= 0 || !lstNg.Contains(mucluc.Ng))
                                    {
                                        NsMucLucNganSach mlTong = new NsMucLucNganSach();
                                        mlTong.Ng = mucluc.Ng;
                                        mlTong.MoTa = "(+)";
                                        lstMlResult.Add(mlTong);
                                        lstNg.Add(mucluc.Ng);
                                    }
                                    lstMlResult.Add(mucluc);
                                }
                                int numberColPage = 6;
                                var listMucLucSplits = SplitList(lstMlResult, numberColPage).ToList();
                                if (exportType == ExportType.EXCEL)
                                {
                                    numberColPage = lstMlResult.Count();
                                    listMucLucSplits = SplitList(lstMlResult, numberColPage).ToList();
                                }

                                for (int i = 0; i < listMucLucSplits.Count; i++)
                                {
                                    List<HeaderReportDuToanDauNamTheoNganhDonViDocModel> headers = new List<HeaderReportDuToanDauNamTheoNganhDonViDocModel>();
                                    var lstNganhHeader = listMucLucSplits[i].Select(x => x.Ng).Distinct().ToList();
                                    int columnStart = 5;
                                    foreach (var nganhHeader in lstNganhHeader)
                                    {
                                        var nganh = ListChuyenNganh.FirstOrDefault(x => x.ValueItem.Contains(nganhHeader));
                                        var lstMlHeader = listMucLucSplits[i].Where(x => nganhHeader.Contains(x.Ng)).Select(x => x.MoTa).ToList();
                                        var mergeRange = "";
                                        var columnStartName = GetExcelColumnName(columnStart);
                                        var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                                        mergeRange = columnStartName + "8" + ":" + columnEndName + "8";
                                        //columnStart += 1;

                                        HeaderReportDuToanDauNamTheoNganhDonViDocModel hd = new HeaderReportDuToanDauNamTheoNganhDonViDocModel();
                                        hd.TenNganh = nganh != null ? nganh.NameItem : "";
                                        hd.LstMucLuc = new List<HeaderDetail>();
                                        hd.LstNganhHeader = new List<HeaderDetail>();
                                        hd.MergeRange = mergeRange;
                                        int j = 0;
                                        foreach (var mlHeader in lstMlHeader)
                                        {
                                            HeaderDetail mlhd = new HeaderDetail();
                                            if (j == 0)
                                            {
                                                mlhd = new HeaderDetail();
                                                mlhd.MoTa = nganh != null ? nganh.NameItem : "";
                                                mlhd.SSttBC = "1";
                                                hd.LstNganhHeader.Add(mlhd);
                                            }
                                            else
                                            {
                                                mlhd = new HeaderDetail();
                                                mlhd.MoTa = "";
                                                mlhd.SSttBC = "2";
                                                hd.LstNganhHeader.Add(mlhd);
                                            }
                                            mlhd = new HeaderDetail();
                                            mlhd.MoTa = mlHeader;
                                            hd.LstMucLuc.Add(mlhd);
                                            j++;
                                        }
                                        headers.Add(hd);
                                    }
                                    int stt = 1;
                                    List<ReportDuToanDauNamTheoNganhPhuLucDonViDocModel> results = new List<ReportDuToanDauNamTheoNganhPhuLucDonViDocModel>();
                                    listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();
                                    foreach (var dv in listDonVi)
                                    {
                                        ReportDuToanDauNamTheoNganhPhuLucDonViDocModel kq = new ReportDuToanDauNamTheoNganhPhuLucDonViDocModel();
                                        kq.Stt = stt++;
                                        kq.MaDonVi = dv.IIDMaDonVi;
                                        kq.TenDonVi = dv.TenDonVi;
                                        kq.TongCong = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                                        kq.LstGiaTri = new List<NsDtdauNamChungTuChiTiet>();
                                        foreach (var ml in listMucLucSplits[i])
                                        {
                                            NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                            if (ml.NamLamViec.Equals(0) || ml.NamLamViec == null)
                                            {
                                                giaTri = new NsDtdauNamChungTuChiTiet();
                                                var gtDonVi = listDataDonVi.Where(x => ml.Ng.Contains(x.sNG) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                                giaTri.FTuChi = gtDonVi;
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            else
                                            {
                                                giaTri = new NsDtdauNamChungTuChiTiet();
                                                var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.iID_MLNS == ml.MlnsId);
                                                giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                        }
                                        results.Add(kq);
                                    }

                                    List<ReportDuToanDauNamTheoNganhPhuLucDonViDocModel> resultsTotal = new List<ReportDuToanDauNamTheoNganhPhuLucDonViDocModel>();
                                    ReportDuToanDauNamTheoNganhPhuLucDonViDocModel total = new ReportDuToanDauNamTheoNganhPhuLucDonViDocModel();
                                    total.LstGiaTriTotal = new List<NsDtdauNamChungTuChiTiet>();
                                    foreach (var dv in listMucLucSplits[i])
                                    {
                                        NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                        if (dv.NamLamViec.Equals(0) || dv.NamLamViec == null)
                                        {
                                            giaTri = new NsDtdauNamChungTuChiTiet();
                                            var gtDonVi = listDataDonVi.Where(x => dv.Ng.Contains(x.sNG)).Sum(x => x.TuChi);
                                            giaTri.FTuChi = gtDonVi;
                                            total.LstGiaTriTotal.Add(giaTri);
                                        }
                                        else
                                        {
                                            giaTri = new NsDtdauNamChungTuChiTiet();
                                            var gtDonVi = listDataDonVi.Where(x => x.iID_MLNS.Equals(dv.MlnsId)).Sum(x => x.TuChi);
                                            giaTri.FTuChi = gtDonVi;
                                            total.LstGiaTriTotal.Add(giaTri);
                                        }
                                    }
                                    resultsTotal.Add(total);
                                    var prefixTenNganh = "Ngành: ";
                                    if (IsChuyenNganh)
                                    {
                                        prefixTenNganh = "Chuyên ngành: ";
                                    }
                                    var sumTotal = listDataDonVi.Sum(x => x.TuChi);
                                    var numColumnMerge = listMucLucSplits[i].Count() < numberColPage ? (numberColPage + 1) : listMucLucSplits[i].Count();
                                    var nameColunmMerge = GetExcelColumnName(numColumnMerge + numberColPage);
                                    Dictionary<string, object> data = new Dictionary<string, object>();
                                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                    data.Add("FormatNumber", formatNumber);
                                    data.Add("ListData", results);
                                    data.Add("ListDataTotal", resultsTotal);
                                    data.Add("SumTotal", sumTotal);
                                    data.Add("TieuDe1", TxtTitleFirst);
                                    data.Add("TieuDe2", TxtTitleSecond);
                                    data.Add("TieuDe3", TxtTitleThird);
                                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                    data.Add("DonVi", "");
                                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                    data.Add("Cap2", _sessionInfo.TenDonVi);
                                    data.Add("DiaDiem", _diaDiem);
                                    data.Add("h1", h1);
                                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                                    data.Add("h2", "");
                                    //data.Add("TenNganh", tenNganh);
                                    data.Add("TenNganh", prefixTenNganh + tenNganh);
                                    data.Add("Headers", headers);
                                    data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                    data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                    data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                    data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                    data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                                    AddChuKy(data, _typeChuky);

                                    if (i == 0)
                                    {
                                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_DONVI_DOC_TO1));
                                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                    }
                                    else
                                    {
                                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_DONVI_DOC_TO2));
                                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                                    }
                                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                    var xlsFile = _exportService.Export<ReportDuToanDauNamTheoNganhPhuLucDonViDocModel, HeaderReportDuToanDauNamTheoNganhDonViDocModel>(templateFileName, data);
                                    //exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                    if (IsChuyenNganh)
                                    {
                                        xlsFile.SetColHidden(4, true);
                                        exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                    }
                                    else
                                    {
                                        exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                    }
                                }

                                // Không có dữ liệu
                                if (listDonVi.Count <= 0)
                                {
                                    var nameColunmMerge = GetExcelColumnName(8);
                                    Dictionary<string, object> data = new Dictionary<string, object>();
                                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                    data.Add("FormatNumber", formatNumber);
                                    data.Add("ListData", new List<ReportDuToanDauNamTheoNganhPhuLucQuery>());
                                    data.Add("ListDataTotal", new List<ReportDuToanDauNamTheoNganhPhuLucQuery>());
                                    data.Add("TieuDe1", TxtTitleFirst);
                                    data.Add("TieuDe2", TxtTitleSecond);
                                    data.Add("TieuDe3", TxtTitleThird);
                                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                    data.Add("DonVi", "");
                                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                    data.Add("Cap2", _sessionInfo.TenDonVi);
                                    data.Add("DiaDiem", _diaDiem);
                                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                                    data.Add("h1", h1);
                                    data.Add("h2", "");
                                    data.Add("TenNganh", tenNganh);
                                    data.Add("Headers", new List<DonVi>());
                                    data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                    data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                    data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                    data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                    data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_EMPTY));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                    var xlsFile = _exportService.Export<ReportDuToanDauNamTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                                    exportResults.Add(new ExportResult("Tổng hợp dự toán - ngành", fileNameWithoutExtension, null, xlsFile));
                                }
                            }

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

        public void OnPrintReportTheoNganhMucLucDoc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DEMAND;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var tenNganh = "";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportDuToanDauNamTheoNganhPhuLucQuery> listDataDonVi = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                    var listMucLuc = _nsMucLucNganSachService.FindByCondition(x => x.NamLamViec == yearOfWork).ToList();
                    if (ListNNganh != null)
                    {
                        //lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                        //var lstChuyenNganh = ListChuyenNganh.Where(x => lstIdCodeSelected.Contains(x.ValueItem)).ToList();
                        //tenNganh = string.Join(", ", lstChuyenNganh.Select(x => x.NameItem));

                        var lstNNganhSelected = ListNNganh.Where(item => item.IsChecked).ToList();
                        foreach (var nnganh in lstNNganhSelected)
                        {
                            string lstChungTuSnc = null;
                            if (ListIdChungTuBaoCaoSncNganh != null)
                            {
                                lstChungTuSnc = string.Join(",", ListIdChungTuBaoCaoSncNganh);
                            }


                            if (!StringUtils.IsNullOrEmpty(nnganh.ValueItem))
                            {
                                listDataDonVi = _sktSoLieuService.FindReportDuToanDauNamTheoNganhPhuLuc(nnganh.ValueItem, null, lstChungTuSnc, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh, IsInTheoTongHop)
                                    .ToList();
                                if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                                {
                                    var lns = _nsMucLucNganSachService.FindByListLnsDonVi(listDataDonVi.Select(x => x.sLNS).ToList(), _sessionService.Current.YearOfWork);
                                    var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                                    listDataDonVi = listDataDonVi.Where(x => listLNS.Contains(x.sLNS)).ToList();
                                }
                                listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();
                            }
                            tenNganh = nnganh.NameItem;

                            var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                            var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
                            var listIdMucLuc = listDataDonVi.Select(x => x.iID_MLNS.Value).Distinct().ToList();
                            List<NsMucLucNganSach> sktMucLucs = FindListParentMucLucByChild(listIdMucLuc);
                            listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();
                            foreach (var mlc in sktMucLucs)
                            {
                                if (!listIdMucLuc.Contains(mlc.MlnsId))
                                {
                                    foreach (var dv in listDonVi)
                                    {
                                        ReportDuToanDauNamTheoNganhPhuLucQuery mlCha = new ReportDuToanDauNamTheoNganhPhuLucQuery();
                                        mlCha.BHangCha = mlc.BHangCha;
                                        mlCha.iID_MLNS_Cha = mlc.MlnsIdParent;
                                        mlCha.iID_MLNS = mlc.MlnsId;
                                        mlCha.IdDonVi = dv.IIDMaDonVi;
                                        mlCha.sNG = mlc.Ng;
                                        mlCha.sTNG = mlc.Tng;
                                        mlCha.sMoTa = mlc.MoTa;
                                        mlCha.sXauNoiMa = mlc.XauNoiMa;
                                        //mlCha.STT = mlc.;
                                        //mlCha.sKyHieu = mlc.SKyHieu;
                                        listDataDonVi.Add(mlCha);
                                    }
                                }
                            }

                            CalculateData(listDataDonVi);
                            listDataDonVi = listDataDonVi.Where(x => string.IsNullOrEmpty(x.sTNG)).ToList();
                            listIdMucLuc = listDataDonVi.OrderBy(x => x.sXauNoiMa).Select(x => x.iID_MLNS.Value).Distinct().ToList();
                            int numberColPage = 3;

                            var listDonViSplits = SplitList(listDonVi, numberColPage, numberColPage + 1).ToList();
                            if (exportType == ExportType.EXCEL)
                            {
                                int countDonVi = listDonVi.Count();
                                numberColPage = countDonVi;
                                listDonViSplits = SplitList(listDonVi, countDonVi, countDonVi + 1).ToList();
                            }
                            for (int i = 0; i < listDonViSplits.Count; i++)
                            {
                                if (listDonViSplits[i].Count < numberColPage + 1)
                                {
                                    var coutRowEmpty = (numberColPage + 1) - listDonViSplits[i].Count;
                                    for (int j = 0; j < coutRowEmpty; j++)
                                    {
                                        DonVi empty = new DonVi();
                                        listDonViSplits[i].Add(empty);
                                    }
                                }

                                List<ReportDuToanDauNamTheoNganhPhuLucQuery> results = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                                foreach (var ml in listIdMucLuc)
                                {
                                    var mucLuc = listMucLuc.FirstOrDefault(x => x.MlnsId == ml);
                                    var mucLucParent = listMucLuc.FirstOrDefault(x => x.MlnsId == mucLuc.MlnsIdParent);

                                    if (mucLuc != null)
                                    {
                                        ReportDuToanDauNamTheoNganhPhuLucQuery kq = new ReportDuToanDauNamTheoNganhPhuLucQuery();
                                        kq.iID_MLNS = ml;
                                        kq.iID_MLNS_Cha = mucLuc.MlnsIdParent;
                                        //kq.STT = mucLuc.SSTT;
                                        kq.sMoTa = mucLuc.MoTa;
                                        kq.sL = string.IsNullOrEmpty(mucLuc.L) ? mucLuc.Lns : string.Join(StringUtils.DIVISION, mucLuc.L, mucLuc.K);
                                        kq.sM = mucLuc.M;
                                        kq.sTM = mucLuc.Tm;
                                        kq.sTTM = mucLuc.Ttm;
                                        kq.sNG = mucLuc.Ng;
                                        if (mucLucParent != null && !string.IsNullOrEmpty(mucLuc.M) && !string.IsNullOrEmpty(mucLucParent.L) && !string.IsNullOrEmpty(mucLucParent.K))
                                        {
                                            kq.sL = null;
                                        }
                                        if (!string.IsNullOrEmpty(mucLuc.Tm))
                                        {
                                            kq.sM = null;
                                        }
                                        if (!string.IsNullOrEmpty(mucLuc.Ttm))
                                        {
                                            kq.sTM = null;
                                        }
                                        kq.BHangCha = mucLuc.BHangCha;
                                        kq.LstGiaTri = new List<NsDtdauNamChungTuChiTiet>();
                                        kq.TongCong = listDataDonVi.Where(x => x.iID_MLNS == ml).Sum(x => x.TuChi);
                                        foreach (var dv in listDonViSplits[i])
                                        {
                                            NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                            if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                            {
                                                var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.iID_MLNS == ml);
                                                giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            else
                                            {
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        results.Add(kq);
                                    }
                                }

                                List<ReportDuToanDauNamTheoNganhPhuLucQuery> resultsTotal = new List<ReportDuToanDauNamTheoNganhPhuLucQuery>();
                                ReportDuToanDauNamTheoNganhPhuLucQuery total = new ReportDuToanDauNamTheoNganhPhuLucQuery();
                                total.LstGiaTriTotal = new List<NsDtdauNamChungTuChiTiet>();
                                foreach (var dv in listDonViSplits[i])
                                {
                                    NsDtdauNamChungTuChiTiet giaTri = new NsDtdauNamChungTuChiTiet();
                                    if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                    {
                                        var gtDonVi = listDataDonVi.Where(x => x.IdDonVi == dv.IIDMaDonVi && !x.BHangCha.GetValueOrDefault(false)).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                    else
                                    {
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                }
                                resultsTotal.Add(total);
                                var prefixTenNganh = "Ngành: ";
                                if (IsChuyenNganh)
                                {
                                    prefixTenNganh = "Chuyên ngành: ";
                                }

                                //Order stt
                                int index0 = 0;
                                int index1 = 1;
                                foreach (var item in results)
                                {
                                    //parent cấp 0
                                    if (item.iID_MLNS_Cha == null)
                                    {
                                        item.STT = printToLetter(index0);
                                        index0 = index0 + 1;
                                        index1 = 1;
                                    }
                                    var parent_cap1 = results.Where(x => x.iID_MLNS == item.iID_MLNS_Cha && x.iID_MLNS_Cha == null).FirstOrDefault();
                                    if (parent_cap1 != null)
                                    {
                                        item.STT = int_to_Roman(index1);
                                        index1 = index1 + 1;
                                    }

                                    if (!string.IsNullOrEmpty(item.sNG))
                                    {
                                        item.BHangCha = false;
                                    }

                                }


                                var nameColunmMerge = GetExcelColumnName(listDonViSplits[i].Count() + numberColPage);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                data.Add("FormatNumber", formatNumber);
                                data.Add("ListData", results);
                                data.Add("ListDataTotal", resultsTotal);
                                data.Add("TongCong", results.Where(x => x.iID_MLNS_Cha == null).Sum(x => x.TongCong));
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("TieuDe3", TxtTitleThird);
                                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                data.Add("DonVi", "");
                                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                data.Add("Cap2", _sessionInfo.TenDonVi);
                                data.Add("DiaDiem", _diaDiem);
                                data.Add("h1", h1);
                                data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                                data.Add("h2", "");
                                //data.Add("TenNganh", tenNganh);
                                data.Add("TenNganh", prefixTenNganh + tenNganh);
                                data.Add("Headers", listDonViSplits[i]);
                                data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                                AddChuKy(data, _typeChuky);

                                if (i == 0)
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_TO1));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_TO2));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                                }
                                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportDuToanDauNamTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                                //exportResults.Add(new ExportResult("Tổng hợp số nhu cầu theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }

                            // Không có dữ liệu
                            if (listDonVi.Count <= 0)
                            {
                                var nameColunmMerge = GetExcelColumnName(8);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                data.Add("FormatNumber", formatNumber);
                                data.Add("ListData", new List<ReportDuToanDauNamTheoNganhPhuLucQuery>());
                                data.Add("ListDataTotal", new List<ReportDuToanDauNamTheoNganhPhuLucQuery>());
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("TieuDe3", TxtTitleThird);
                                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                data.Add("DonVi", "");
                                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                data.Add("Cap2", _sessionInfo.TenDonVi);
                                data.Add("DiaDiem", "Hà Nội");
                                data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                                data.Add("h1", h1);
                                data.Add("h2", _sessionInfo.TenDonVi);
                                data.Add("TenNganh", tenNganh);
                                data.Add("Headers", new List<DonVi>());
                                data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_EMPTY));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportDuToanDauNamTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                                exportResults.Add(new ExportResult("Tổng hợp dự toán - ngành", fileNameWithoutExtension, null, xlsFile));
                            }
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

        public List<NsMucLucNganSach> FindListParentMucLucByChild(List<Guid> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listMucLuc = _nsMucLucNganSachService
                .FindByCondition(x => listIdMucLuc.Contains(x.MlnsId) && x.NamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsMucLucNganSach> nsMucLucs = new List<NsMucLucNganSach>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.MlnsId).ToList();
                nsMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.MlnsIdParent.GetValueOrDefault())).Select(x => x.MlnsIdParent).ToList();
                    var listParent1 = _nsMucLucNganSachService.FindByCondition(x => listIdParent.Contains(x.MlnsId) && x.NamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.MlnsId).ToList();
                        listIdMlskt.AddRange(lstId);
                        nsMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            nsMucLucs = nsMucLucs.GroupBy(x => x.MlnsId).Select(x => x.First()).OrderBy(x => x.XauNoiMa).ToList();
            return nsMucLucs;
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

        public string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_TL_DTDN, input + FileExtensionFormats.Xlsx);
        }


        private void CalculateParent(Guid? idParent, string idDonVi, ReportDuToanDauNamTheoNganhPhuLucQuery item, List<ReportDuToanDauNamTheoNganhPhuLucQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.iID_MLNS == idParent && x.IdDonVi == idDonVi);
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            model.PhanCap += item.PhanCap;
            CalculateParent(model.iID_MLNS_Cha, model.IdDonVi, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportDuToanDauNamTheoNganhPhuLucQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.MuaHangHienVat = 0;
                    x.DacThu = 0;
                    x.PhanCap = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.iID_MLNS_Cha, item.IdDonVi, item, lstSktChungTuChiTiet);
            }
        }

        private string printToLetter(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }

        private string int_to_Roman(int n)
        {
            string[] roman_symbol = { "MMM", "MM", "M", "CM", "DCCC", "DCC", "DC", "D", "CD", "CCC", "CC", "C", "XC", "LXXX", "LXX", "LX", "L", "XL", "XXX", "XX", "X", "IX", "VIII", "VII", "VI", "V", "IV", "III", "II", "I" };
            int[] int_value = { 3000, 2000, 1000, 900, 800, 700, 600, 500, 400, 300, 200, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            var roman_numerals = new StringBuilder();
            var index_num = 0;
            while (n != 0)
            {
                if (n >= int_value[index_num])
                {
                    n -= int_value[index_num];
                    roman_numerals.Append(roman_symbol[index_num]);
                }
                else
                {
                    index_num++;
                }
            }

            return roman_numerals.ToString();
        }
    }
}
