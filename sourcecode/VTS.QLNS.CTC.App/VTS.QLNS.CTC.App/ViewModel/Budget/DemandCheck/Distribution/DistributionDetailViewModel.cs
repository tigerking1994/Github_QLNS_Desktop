using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution.ImportExpertise;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution.ImportExpertise;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution
{
    public class DistributionDetailViewModel : DetailViewModelBase<NsSktChungTuModel, NsSktChungTuChiTietModel>
    {
        private const string DetailHeaderStringFormat = "Năm làm việc: {0} | Số: {1} - QĐ: {2} - Ngày: {3} | {4}";
        private const string _idDonViDuPhong = "999";
        private readonly ISessionService _sessionService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly IDanhMucService _iDanhMucService;
        private readonly INsDonViService _iNsDonViService;
        private readonly ISktNganhThamDinhService _sktThamDinhService;
        private readonly ISktNganhThamDinhChiTietService _sktThamDinhChiTietService;
        private readonly INsSktNganhThamDinhChiTietSktService _iSktNganhThamDinhChiTietSktService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private ICollectionView _sktChungTuChiTietModelsView;
        private ICollectionView _searchPopupView;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        private readonly string _idDvDuPhong = "999";
        //private ICollectionView _nNganhModelsView;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        public override string Title => "PHÂN BỔ SỐ KIỂM TRA - CHỨNG TỪ CHI TIẾT";
        public override Type ContentType => typeof(DistributionDetail);
        public override string Description
        {
            get
            {
                var temp = new StringBuilder();
                var namLamViec = _sessionInfo.YearOfWork;
                var soChungTu = Model.SSoChungTu ?? string.Empty;
                var soQuyetDinh = Model.SSoQuyetDinh ?? string.Empty;
                var ngayQuyetDinh = Model.DNgayQuyetDinh == null
                    ? string.Empty
                    : Model.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy");
                var tenDonVi = Model.TenDonViIdDonVi ?? string.Empty;
                temp.AppendFormat(DetailHeaderStringFormat, namLamViec, soChungTu, soQuyetDinh, ngayQuyetDinh, tenDonVi);
                return temp.ToString();
            }
        }

        private NsSktChungTuChiTietModel _nsSktChungTuChiTietSearchModel;
        public NsSktChungTuChiTietModel NsSktChungTuChiTietSearchModel
        {
            get => _nsSktChungTuChiTietSearchModel;
            set => SetProperty(ref _nsSktChungTuChiTietSearchModel, value);
        }

        private SktMucLucModel _selectedSktMucLuc;
        public SktMucLucModel SelectedSktMucLuc
        {
            get => _selectedSktMucLuc;
            set
            {
                SetProperty(ref _selectedSktMucLuc, value);
                NsSktChungTuChiTietSearchModel.SKyHieu = _selectedSktMucLuc?.SKyHieu;
                OnPropertyChanged(nameof(NsSktChungTuChiTietSearchModel));
                IsPopupOpen = false;
            }
        }

        public bool IsInit { get; set; } = false;

        private double _fTongHuyDongTonKho;
        public double FTongHuyDongTonKho
        {
            get => _fTongHuyDongTonKho;
            set => SetProperty(ref _fTongHuyDongTonKho, value);
        }

        public NsSktChungTuChiTietModel ModelFromCheck { get; set; }

        private ObservableCollection<SktMucLucModel> _sktMucLucItems;
        public ObservableCollection<SktMucLucModel> SktMucLucItems
        {
            get => _sktMucLucItems;
            set => SetProperty(ref _sktMucLucItems, value);
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private string _sktMucLucSearch;
        public string SktMucLucSearch
        {
            set
            {
                SetProperty(ref _sktMucLucSearch, value);
                _searchPopupView?.Refresh();
            }
            get => _sktMucLucSearch;
        }

        private ComboboxItem _selectedDataState;
        public ComboboxItem SelectedDataState
        {
            get => _selectedDataState;
            set
            {
                SetProperty(ref _selectedDataState, value);
                if (_selectedDataState != null)
                {
                    if (_selectedDataState.ValueItem.Equals(DataStateValue.SO_CON_LAI_CHUA_PHAN_BO.ToString()))
                    {
                        GetListMucLucFilterConLai();
                    }
                    LoadData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataStateItems;
        public ObservableCollection<ComboboxItem> DataStateItems
        {
            get => _dataStateItems;
            set => SetProperty(ref _dataStateItems, value);
        }

        //public string ComboboxDisplayMemberPathDanhMucNganh => nameof(DanhMucNganhModel.Ten);


        private ObservableCollection<ComboboxItem> _khoiDonViItems;
        public ObservableCollection<ComboboxItem> KhoiDonViItems
        {
            get => _khoiDonViItems;
            set => SetProperty(ref _khoiDonViItems, value);
        }

        private ComboboxItem _selectedKhoiDonVi;
        public ComboboxItem SelectedKhoiDonVi
        {
            get => _selectedKhoiDonVi;
            set
            {
                if (SetProperty(ref _selectedKhoiDonVi, value))
                {
                    LoadDataDonVi();
                    OnSearch();
                }
            }
        }

        private ObservableCollection<DanhMucNganhModel> _NNganhItems;
        public ObservableCollection<DanhMucNganhModel> NNganhItems
        {
            get => _NNganhItems;
            set => SetProperty(ref _NNganhItems, value);
        }

        private DanhMucNganhModel _selectedNNganh;
        public DanhMucNganhModel SelectedNNganh
        {
            get => _selectedNNganh;
            set
            {
                if (SetProperty(ref _selectedNNganh, value))
                {
                    LoadChuyenNganh();
                    LoadData();
                }
            }
        }

        private ObservableCollection<DanhMucNganhModel> _cNganhItems;
        public ObservableCollection<DanhMucNganhModel> CNganhItems
        {
            get => _cNganhItems;
            set => SetProperty(ref _cNganhItems, value);
        }

        private DanhMucNganhModel _selectedCNganh;
        public DanhMucNganhModel SelectedCNganh
        {
            get => _selectedCNganh;
            set
            {
                if (SetProperty(ref _selectedCNganh, value))
                {
                    LoadData();
                }
            }
        }

        private ObservableCollection<DonViModel> _donViItems;
        public ObservableCollection<DonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                if (_selectedDonVi != null)
                {
                    GetListMucLucFilterDonVi();
                    OnSearch();
                }
                else
                {
                    ListIdMucLucDonViFilter = null;
                    OnSearch();
                }
                OnPropertyChanged(nameof(IsEnablePhanBoAll));
            }
        }

        private ObservableCollection<NsSktNganhThamDinhChiTietSktModel> _itemsThamDinhSkt;
        public ObservableCollection<NsSktNganhThamDinhChiTietSktModel> ItemsThamDinhSkt
        {
            get => _itemsThamDinhSkt;
            set
            {
                if (SetProperty(ref _itemsThamDinhSkt, value))
                {
                }
            }
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                SetProperty(ref _isPopupOpen, value);
            }
        }

        public List<MucLucSoKiemTraTheoNganhQuery> ListMucLucTheoNganh { get; set; }
        public List<MucLucSoKiemTraTheoNganhQuery> ListMucLucTheoChuyenNganh { get; set; }
        private Dictionary<Guid, CanCuDuToanNamTruocSoKiemTraQuery> DicDataCalculate { get; set; }
        private Dictionary<Guid, NsSktChungTuChiTietModel> DicItems { get; set; }
        public List<Guid> ListIdMucLucFilter { get; set; }
        public List<Guid> ListIdMucLucDonViFilter { get; set; }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        //public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified && item.HasData);
        public bool FirstTimePhanBo { get; set; }
        public bool IsGetDuLieuThamDinh { get; set; }
        public bool IsEnablePhanBoAll => SelectedDonVi != null;
        public bool ShowFilterKhoiDonVi => Model.ILoaiNguonNganSach.Value == 1;
        public bool ShowCopyEtmData => Model.ILoaiChungTu.ToString() == VoucherType.NSSD_Key;
        public Visibility ShowColNSBD { get; set; }
        public Visibility ShowColNSSD { get; set; }
        public string DuToan => "Dự toán đầu năm " + (_sessionInfo.YearOfWork - 1);
        public string SoNhuCau => "Số nhu cầu năm " + (_sessionInfo.YearOfWork);
        public string SoKiemTra => "Số kiểm tra năm " + (_sessionInfo.YearOfWork - 1);
        public DateTime DtNow => DateTime.Now;
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand GetDataNganhThamDinh { get; }
        public RelayCommand GetDataNganhThamDinhExcel { get; }
        public RelayCommand PhanBoConLaiCommand { get; }
        public RelayCommand CopySoNhuCau { get; }
        public RelayCommand CopySoDuToan { get; }

        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get; }
        public CheckExpertiseDetailViewModel CheckExpertiseDetailViewModel { get; }
        public ImportExpertiseDistributionViewModel ImportExpertiseDistributionViewModel { get; }

        public DistributionDetailViewModel(
            ISktChungTuChiTietService sktChungTuChiTietService,
            ISktChungTuService sktChungTuService,
            ISktMucLucService sktMucLucService,
            IDanhMucService iDanhMucService,
            ISessionService sessionService,
            INsDonViService iNsDonViService,
            ISktNganhThamDinhService sktThamDinhService,
            ISktNganhThamDinhChiTietService sktThamDinhChiTietService,
            INsSktNganhThamDinhChiTietSktService iSktNganhThamDinhChiTietSktService,
            INsMucLucNganSachService iMucLucNganSachService,
            ILog logger,
            ISysAuditLogService log,
            IMapper mapper,
            CheckExpertiseDetailViewModel checkExpertiseDetailViewModel,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel,
            ImportExpertiseDistributionViewModel importExpertiseDistributionViewModel)
        {
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktChungTuService = sktChungTuService;
            _sktMucLucService = sktMucLucService;
            _iDanhMucService = iDanhMucService;
            _sessionService = sessionService;
            _iNsDonViService = iNsDonViService;
            _sktThamDinhService = sktThamDinhService;
            _sktThamDinhChiTietService = sktThamDinhChiTietService;
            _iSktNganhThamDinhChiTietSktService = iSktNganhThamDinhChiTietSktService;
            _iNsMucLucNganSachService = iMucLucNganSachService;
            _log = log;
            _logger = logger;
            _mapper = mapper;
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;
            CheckExpertiseDetailViewModel = checkExpertiseDetailViewModel;
            ImportExpertiseDistributionViewModel = importExpertiseDistributionViewModel;
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();

            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            PrintCommand = new RelayCommand(OnPrint);
            GetDataNganhThamDinh = new RelayCommand(obj => LayDuLieuNganhThamDinh());
            GetDataNganhThamDinhExcel = new RelayCommand(obj => LayDuLieuNganhThamDinhExcel());
            PhanBoConLaiCommand = new RelayCommand(obj => OnPhanBoConLai());
            CopySoNhuCau = new RelayCommand(obj => CopySoNhuCauSangTuChi());
            CopySoDuToan = new RelayCommand(obj => OnCopyEstimateData());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _selectedDonVi = null;
            if (Model != null) IsLock = Model.BKhoa;
            LoadDataState();
            LoadSktMucLuc();
            LoadKhoiDonVi();
            LoadNhomNganh();
            LoadChuyenNganh();
            LoadDataDonVi();
            LoadData();
            LoadDataChiTietThongBaoDv();
        }

        private void LoadDataState()
        {
            DataStateItems = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem()
                {
                    ValueItem = DataStateValue.DA_NHAP_SKT.ToString(), DisplayItem = DataStateName.DA_NHAP_SKT,
                },
                new ComboboxItem()
                {
                ValueItem = DataStateValue.HIEN_THI_TAT_CA.ToString(),
                DisplayItem = DataStateName.HIEN_THI_TAT_CA,
                }
            };
            _selectedDataState = DataStateItems[0];
            if (Model != null && Model.IIdMaDonVi.Equals(_sessionInfo.IdDonVi))
            {
                ComboboxItem daNhanSkt = new ComboboxItem()
                {
                    ValueItem = DataStateValue.DA_NHAN_SKT.ToString(),
                    DisplayItem = DataStateName.DA_NHAN_SKT,
                };
                DataStateItems.Add(daNhanSkt);
                ComboboxItem soKtConLai = new ComboboxItem()
                {
                    ValueItem = DataStateValue.SO_CON_LAI_CHUA_PHAN_BO.ToString(),
                    DisplayItem = DataStateName.SO_CON_LAI_CHUA_PHAN_BO,
                };
                DataStateItems.Add(soKtConLai);
                _selectedDataState = DataStateItems[2];
            }
        }

        private void LoadSktMucLuc()
        {
            var predicate = PredicateBuilder.True<NsSktMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => true.Equals(x.BHangCha));
            var temp = _sktMucLucService.FindByCondition(predicate).OrderBy(x => x.SKyHieu).ToList();
            SktMucLucItems = _mapper.Map<ObservableCollection<SktMucLucModel>>(temp);
            _searchPopupView = CollectionViewSource.GetDefaultView(SktMucLucItems);
            _searchPopupView.Filter = SktMucLucFilter;
        }

        private bool SktMucLucFilter(object obj)
        {
            var temp = (SktMucLucModel)obj;
            var condition = true;
            if (!string.IsNullOrEmpty(SktMucLucSearch))
                condition = condition && (temp.SSTT.ToLower().Contains(SktMucLucSearch.Trim().ToLower())
                                          || temp.SMoTa.ToLower().Contains(SktMucLucSearch.Trim().ToLower())
                                          || temp.SKyHieu.ToLower().Contains(SktMucLucSearch.Trim().ToLower()));
            return condition;
        }

        private void LoadNhomNganh()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.SType.Equals(VoucherType.VOCHER_TYPE));
            var list = _iDanhMucService.FindByCondition(predicate).ToList();
            NNganhItems = _mapper.Map<ObservableCollection<DanhMucNganhModel>>(list);
            _selectedNNganh = null;
        }

        private void LoadChuyenNganh()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.SType.Equals(VoucherType.DM_Nganh));
            if (SelectedNNganh != null)
            {
                var lstMaCn = SelectedNNganh.SGiaTri.Split(',').ToList();
                predicate = predicate.And(x => lstMaCn.Contains(x.IIDMaDanhMuc));
            }
            var list = _iDanhMucService.FindByCondition(predicate).ToList();
            CNganhItems = _mapper.Map<ObservableCollection<DanhMucNganhModel>>(list);
            _selectedCNganh = null;
        }

        private void LoadKhoiDonVi()
        {
            KhoiDonViItems = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem("Tất cả", TypeKhoi.TAT_CA.ToString()),
                new ComboboxItem("Dự toán", TypeKhoi.DU_TOAN.ToString()),
                new ComboboxItem("Doanh nghiệp", TypeKhoi.DOANH_NGHIEP.ToString()),
                new ComboboxItem("Bệnh viện tự chủ", TypeKhoi.BENH_VIEN.ToString()),
            };

            if (Model.ILoaiNguonNganSach == TypeLoaiNNS.DU_TOAN)
            {
                SelectedKhoiDonVi = KhoiDonViItems.FirstOrDefault(x => x.ValueItem == TypeKhoi.TAT_CA.ToString());
            }
            else
            if (Model.ILoaiNguonNganSach == TypeLoaiNNS.DOANH_NGHIEP)
            {
                SelectedKhoiDonVi = KhoiDonViItems.FirstOrDefault(x => x.ValueItem == TypeKhoi.DOANH_NGHIEP.ToString());
            }
            else
            {
                SelectedKhoiDonVi = KhoiDonViItems.FirstOrDefault(x => x.ValueItem == TypeKhoi.BENH_VIEN.ToString());
            }
        }

        private void LoadDataDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var loaiChungTu = Model.ILoaiChungTu.GetValueOrDefault(-1);
            if (SelectedKhoiDonVi != null)
            {
                var temp = GetListDonViByLoaiChungTu(loaiChungTu, yearOfWork)
                    .Where(x => SelectedKhoiDonVi.ValueItem == "0"
                        || SelectedKhoiDonVi.ValueItem == x.Khoi).ToList();
                DonViItems = new ObservableCollection<DonViModel>(temp);
            }
            else
            {
                DonViItems = GetListDonViByLoaiChungTu(loaiChungTu, yearOfWork);
            }
        }

        public override void LoadData(params object[] args)
        {
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = Model.Id;
            searchCondition.ILoai = Model.ILoai;
            searchCondition.IdDonVi = Model.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.ILoaiNguonNganSach = Model.ILoaiNguonNganSach.GetValueOrDefault(1);
            searchCondition.LoaiChungTu = Model.ILoaiChungTu.GetValueOrDefault(-1);
            searchCondition.HienThi = int.Parse(SelectedDataState.ValueItem);
            searchCondition.IdDonViSearch = SelectedDonVi != null ? SelectedDonVi.IIDMaDonVi : null;
            searchCondition.UserName = _sessionInfo.Principal;
            if (_selectedNNganh != null)
            {
                searchCondition.ChuyenNganh = _selectedNNganh.SGiaTri != null ? _selectedNNganh.SGiaTri : "";
            }
            if (_selectedCNganh != null)
            {
                searchCondition.ChuyenNganh = _selectedCNganh.IIDMaDanhMuc != null ? _selectedCNganh.IIDMaDanhMuc : "";
            }
            IEnumerable<NsSktChungTuChiTiet> list;
            if (Model.IIdMaDonVi == _sessionInfo.IdDonVi)
            {
                if (ModelFromCheck == null)
                {
                    list = _sktChungTuChiTietService.FindByConditionForParentUnit(searchCondition);
                }
                else
                {
                    searchCondition.IdMucLucSkt = ModelFromCheck.IIdMlskt;
                    list = _sktChungTuChiTietService.FindByConditionForParentUnitByIdMucLuc(searchCondition);
                }
            }
            else
            {
                list = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            }
            Items = _mapper.Map<ObservableCollection<NsSktChungTuChiTietModel>>(list);
            _sktChungTuChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _sktChungTuChiTietModelsView.Filter = SktChungTuChiTietFilter;
            //them can cu du toan
            AddCanCuDuToanNamTruoc();
            CalculateDataSoNhuCau();
            if (Model != null && int.Parse(VoucherType.NSBD_Key).Equals(Model.ILoaiChungTu.GetValueOrDefault()))
            {
                AutoPhanBoFirstTime();
            }
            CalculateData();
            foreach (var item in Items.Where(x => !x.IsHangCha))
            {
                if (item.SKhoiDonVi == KhoiDonVi.BENH_VIEN_TU_CHU)
                {
                    item.ILoai = DemandCheckType.CORPORATIZED_HOSPITAL;
                }
                else
                {
                    item.ILoai = DemandCheckType.DISTRIBUTION;
                }

                item.ILoaiChungTu = Model.ILoaiChungTu;
                item.PropertyChanged += Item_PropertyChanged;
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(SelectedItem.FHuyDongTonKho) ||
                args.PropertyName == nameof(SelectedItem.FTuChi) ||
                args.PropertyName == nameof(SelectedItem.FMuaHangCapHienVat) ||
                args.PropertyName == nameof(SelectedItem.FPhanCap) ||
                args.PropertyName == nameof(SelectedItem.SoNhuCau) ||
                args.PropertyName == nameof(SelectedItem.SGhiChu))
            {

                NsSktChungTuChiTietModel item = (NsSktChungTuChiTietModel)sender;
                item.IsModified = true;
                if (item.IIdMaDonVi != _idDonViDuPhong && !FirstTimePhanBo && !IsInit)
                {
                    CalculateData();
                }
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void AddCanCuDuToanNamTruoc()
        {
            var loaiChungTu = Model.ILoaiChungTu;
            List<CanCuDuToanNamTruocSoKiemTraQuery> lstCanCu = _sktChungTuChiTietService
                .FindCanCuPhanSoKiemTra(loaiChungTu.GetValueOrDefault(), Model.IIdMaDonVi, Model.INamLamViec - 1, _sessionInfo.YearOfBudget, _sessionInfo.Budget, true).ToList();
            List<Guid> lstIdMucLuc = lstCanCu.Select(x => x.IdMlns).Distinct().ToList();
            List<NsMucLucNganSach> sktMucLucs = FindListParentMucLucByChildNamTruoc(lstIdMucLuc);
            foreach (var mlc in sktMucLucs)
            {
                if (!lstIdMucLuc.Contains(mlc.MlnsId))
                {
                    CanCuDuToanNamTruocSoKiemTraQuery mlCha = new CanCuDuToanNamTruocSoKiemTraQuery();
                    mlCha.IdMlns = mlc.MlnsId;
                    mlCha.IdMlnsCha = mlc.MlnsIdParent;
                    mlCha.SXauNoiMa = mlc.XauNoiMa;
                    mlCha.BHangCha = mlc.BHangCha;
                    lstCanCu.Add(mlCha);
                }
            }
            CalculateDataCanCuDuToanNamTruoc(lstCanCu);
            List<NsMlsktMlns> lstMap = new List<NsMlsktMlns>();
            lstMap = _sktMucLucService.FindAllMapMlsktMlns(_sessionInfo.YearOfWork - 1).ToList();
            foreach (var it in lstCanCu)
            {
                var itSkt = lstMap.FirstOrDefault(x => x.SNsXauNoiMa.Equals(it.SXauNoiMa));
                if (itSkt != null)
                {
                    it.KyHieu = itSkt.SSktKyHieu;
                }
            }
            foreach (var cc in lstCanCu)
            {
                var mucLuc = Items.FirstOrDefault(item => item.SKyHieuCu == cc.KyHieu && item.IIdMaDonVi == cc.IIdMaDonVi);
                if (mucLuc != null)
                {
                    if (loaiChungTu.GetValueOrDefault(0).Equals(int.Parse(VoucherType.NSSD_Key)))
                    {
                        mucLuc.DuToan += cc.TuChi;
                        mucLuc.DuToanMHCHV += 0;
                        mucLuc.DuToanDT += 0;
                    }
                    else
                    {
                        mucLuc.DuToan += 0;
                        mucLuc.DuToanMHCHV += cc.HangNhap + cc.HangMua + cc.TuChi;
                        mucLuc.DuToanDT += cc.PhanCap + cc.DuPhong;
                    }
                }
            }
        }

        public List<NsMucLucNganSach> FindListParentMucLucByChildNamTruoc(List<Guid> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork - 1;
            var listMucLuc = _iNsMucLucNganSachService
                .FindByCondition(x => listIdMucLuc.Contains(x.MlnsId) && x.NamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsMucLucNganSach> sktMucLucs = new List<NsMucLucNganSach>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.MlnsId).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.MlnsIdParent.GetValueOrDefault())).Select(x => x.MlnsIdParent).ToList();
                    var listParent1 = _iNsMucLucNganSachService.FindByCondition(x => listIdParent.Contains(x.MlnsId) && x.NamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.MlnsId).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.MlnsId).Select(x => x.First()).OrderBy(x => x.XauNoiMa).ToList();
            return sktMucLucs;
        }

        private void CalculateDataCanCuDuToanNamTruoc(List<CanCuDuToanNamTruocSoKiemTraQuery> items)
        {
            items.Where(x => x.BHangCha.GetValueOrDefault()).ToList();
            var temp = items.Where(x => !x.BHangCha.GetValueOrDefault());
            if (!items.IsEmpty()) DicDataCalculate = items.GroupBy(x => x.IdMlns).ToList().ToDictionary(x => x.Key, value => value.FirstOrDefault());
            foreach (var item in temp)
            {
                CalculateParentCanCuDuToanNamTruoc(item.IdMlnsCha, items, item);
            }

            UpdateEstimateTotal();
        }

        private void CalculateParentCanCuDuToanNamTruoc(Guid? idParent, List<CanCuDuToanNamTruocSoKiemTraQuery> listData, CanCuDuToanNamTruocSoKiemTraQuery item)
        {
            CanCuDuToanNamTruocSoKiemTraQuery model;
            if (DicDataCalculate.IsEmpty())
            {
                model = listData.FirstOrDefault(x => x.IdMlns == idParent.GetValueOrDefault());

            }
            else
            {
                model = DicDataCalculate.ContainsKey(idParent.GetValueOrDefault()) ? DicDataCalculate[idParent.GetValueOrDefault()] : null;
            }
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.HangNhap += item.HangNhap;
            model.HangMua += item.HangMua;
            model.PhanCap += item.PhanCap;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            CalculateParentCanCuDuToanNamTruoc(model.IdMlnsCha, listData, item);
        }

        private void UpdateEstimateTotal()
        {
            Model.TongDuToan = 0;
            Model.TongQuyetToan = 0;
            Model.TongHuyDong = 0;
            Model.FTongTuChi = 0;
            Model.FTongMuaHangCapHienVat = 0;
            Model.FTongPhanCap = 0;
            Model.TongSoNhuCau = 0;
            Model.TongSoNhuCauMHHV = 0;
            Model.TongSoNhuCauDT = 0;
            Model.TongSoKiemTra = 0;
            Model.TongSoKiemTraMHHV = 0;
            Model.TongSoKiemTraDT = 0;
            Model.TongSoNhuCauHangNhap = 0;
            Model.TongSoNhuCauHangMua = 0;
            Model.TongSoNhuCauPhanCap = 0;
            Model.TongCanCuDuToan = 0;
            Model.TongCanCuDuToanMHCHV = 0;
            Model.TongCanCuDuToanDT = 0;
            Model.FTongThongBaoDonVi = 0;
            FTongHuyDongTonKho = 0;
            var listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();

            Model.TongHuyDong = listChildren.Sum(x => x.FHuyDongTonKho);// item.FHuyDongTonKho;
            Model.FTongTuChi = listChildren.Sum(x => x.FTuChi);// item.FTuChi;
            Model.FTongMuaHangCapHienVat = listChildren.Sum(x => x.FMuaHangCapHienVat);// item.FMuaHangCapHienVat;
            Model.FTongPhanCap = listChildren.Sum(x => x.FPhanCap);// item.FPhanCap;
            Model.TongSoNhuCau = listChildren.Sum(x => x.SoNhuCau);// item.SoNhuCau;
            Model.TongSoNhuCauMHHV = listChildren.Sum(x => x.SoNhuCauDT);// item.SoNhuCauDT;
            Model.TongSoNhuCauDT = listChildren.Sum(x => x.SoNhuCauDT);// item.SoNhuCauDT;
            Model.TongSoKiemTra = listChildren.Sum(x => x.SoKiemTra);// item.SoKiemTra;
            Model.TongSoKiemTraMHHV = listChildren.Sum(x => x.SoKiemTraMHHV);// item.SoKiemTraMHHV;
            Model.TongSoKiemTraDT = listChildren.Sum(x => x.SoKiemTraDT);// item.SoKiemTraDT;
            Model.TongCanCuDuToan = listChildren.Sum(x => x.DuToan);// item.DuToan;
            Model.TongCanCuDuToanMHCHV = listChildren.Sum(x => x.DuToanMHCHV);// item.DuToanMHCHV;
            Model.TongCanCuDuToanDT = listChildren.Sum(x => x.DuToanDT);// item.DuToanDT;
            Model.FTongThongBaoDonVi = listChildren.Sum(x => x.FThongBaoDonVi);// item.FThongBaoDonVi;
            FTongHuyDongTonKho = listChildren.Sum(x => x.FHuyDongTonKho);// item.FHuyDongTonKho;

            //foreach (var item in listChildren)
            //{
            //    Model.TongHuyDong += item.FHuyDongTonKho;
            //    Model.FTongTuChi += item.FTuChi;
            //    Model.FTongMuaHangCapHienVat += item.FMuaHangCapHienVat;
            //    Model.FTongPhanCap += item.FPhanCap;
            //    Model.TongSoNhuCau += item.SoNhuCau;
            //    Model.TongSoNhuCauMHHV += item.SoNhuCauMHHV;
            //    Model.TongSoNhuCauDT += item.SoNhuCauDT;
            //    Model.TongSoKiemTra += item.SoKiemTra;
            //    Model.TongSoKiemTraMHHV += item.SoKiemTraMHHV;
            //    Model.TongSoKiemTraDT += item.SoKiemTraDT;
            //    Model.TongCanCuDuToan += item.DuToan;
            //    Model.TongCanCuDuToanMHCHV += item.DuToanMHCHV;
            //    Model.TongCanCuDuToanDT += item.DuToanDT;
            //    Model.FTongThongBaoDonVi += item.FThongBaoDonVi;
            //    FTongHuyDongTonKho += item.FHuyDongTonKho;
            //}
            OnPropertyChanged(nameof(FTongHuyDongTonKho));
        }

        private void OnClearSearch(object obj)
        {
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();
            _sktChungTuChiTietModelsView.Refresh();
        }

        public void OnSearch()
        {
            _sktChungTuChiTietModelsView?.Refresh();
            if (Items != null)
            {
                CalculateData();
            }
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        /// <summary>
        ///     Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OnPrint(object param)
        {
            var demandCheckPrintType = (DemandCheckPrintType)(int)param;
            object content;
            switch (demandCheckPrintType)
            {
                case DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DemandCheckScreen.DETAIL_DIALOG, null, null);
            }
        }

        protected override void OnLockUnLock()
        {
            var message = string.Empty;
            message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var messageBox =
                new NSMessageBoxViewModel(message, "Xác nhận", NSMessageBoxButtons.YesNo, LockConfirmEventHandler);
            //show the dialog
            DialogHost.Show(messageBox.Content, DemandCheckScreen.DETAIL_DIALOG);
        }

        private void LockConfirmEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            LockOrUnLockDetail();
        }

        private void LockOrUnLockDetail()
        {
            IsLock = !IsLock;
            _sktChungTuService.LockOrUnlock(Model.Id, IsLock);
            OnPropertyChanged(nameof(IsDeleteAll));
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;
            SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            CalculateData();
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            if (result == MessageBoxResult.Yes)
            {
                var lstItemFilter = Items.Where(x => x.IsFilter
                                                     && !x.IsHangCha
                                                     && (x.FTuChi != 0 || x.FTuChiDeNghi != 0 || x.FHuyDongTonKho != 0 || x.FMuaHangCapHienVat != 0
                                                         || x.FPhanCap != 0 || x.FTonKhoDenNgay != 0)).ToList();
                if (lstItemFilter.Count > 0)
                {
                    //xóa chứng từ chi tiết
                    DeleteChiTiet(lstItemFilter);
                }

                LoadData();
                OnSaveDuPhong();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        public override void OnSave()
        {
            CalculateDuPhong();
            Func<NsSktChungTuChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<NsSktChungTuChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<NsSktChungTuChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();
            var listMl = Items.Select(x => x.IIdMlskt).ToList();
            var lstChiTietDuPhong = _sktChungTuChiTietService.FindByCondition(x =>
                x.IIdCtsoKiemTra == Model.Id && _idDonViDuPhong.Equals(x.IIdMaDonVi) && !listMl.Contains(x.IIdMlskt));
            if (lstChiTietDuPhong != null)
            {
                _sktChungTuChiTietService.RemoveRange(lstChiTietDuPhong);
            }

            //save số liệu chi tiết thông tri thẩm định
            if (IsGetDuLieuThamDinh)
            {
                _iSktNganhThamDinhChiTietSktService.DeleteByYearOfWork(Model.INamLamViec);
                IsGetDuLieuThamDinh = false;
            }
            SaveThongTriThamDinh();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<NsSktChungTuChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _sktChungTuChiTietService.AddRange(addItems);
                _log.WriteLog(Resources.ApplicationName, "Phân bổ số kiểm tra - chứng từ chi tiết", (int)TypeExecute.Insert, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                Items.Where(isAdd).Select(x =>
                {
                    x.IsModified = false;
                    x.IsAdd = false;
                    return x;
                }).ToList();
            }

            //cập nhật chứng từ chi tiết
            if (detailsUpdate.Count > 0)
            {
                foreach (var updateItem in detailsUpdate)
                {
                    var sktChungTuChiTiet = _sktChungTuChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, sktChungTuChiTiet);
                    if (IsUpdateToDelete(updateItem))
                    {
                        detailsDelete.Add(updateItem);
                    }
                    else
                    {
                        _sktChungTuChiTietService.Update(sktChungTuChiTiet);
                        _log.WriteLog(Resources.ApplicationName, "Phân bổ số kiểm tra - chứng từ chi tiết", (int)TypeExecute.Update, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                        updateItem.IsModified = false;
                    }
                }
            }

            //xóa chứng từ chi tiết
            if (detailsDelete.Count > 0)
            {
                foreach (var item in detailsDelete)
                {
                    var deleteItem = _sktChungTuChiTietService.FindById(item.Id);
                    if (deleteItem != null)
                    {
                        _mapper.Map(item, deleteItem);
                        _sktChungTuChiTietService.Delete(deleteItem);
                        _log.WriteLog(Resources.ApplicationName, "Phân bổ số kiểm tra - chứng từ chi tiết", (int)TypeExecute.Delete, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    }
                    item.FTuChi = 0;
                    item.FHuyDongTonKho = 0;
                    item.FMuaHangCapHienVat = 0;
                    item.FPhanCap = 0;
                    item.SMoTa = string.Empty;
                    item.IsModified = false;
                    item.IsDeleted = false;
                }
            }

            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            var message = Resources.MsgSaveDone;
            var messageBox = new NSMessageBoxViewModel(message);
            DialogHost.Show(messageBox.Content, DemandCheckScreen.DETAIL_DIALOG);
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha && !x.IsFirstParentRow)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FPhanCap = 0;
                    x.SoNhuCau = 0;
                    x.SoNhuCauMHHV = 0;
                    x.SoNhuCauDT = 0;
                    x.SoKiemTra = 0;
                    x.SoKiemTraMHHV = 0;
                    x.SoKiemTraDT = 0;
                    x.FThongBaoDonVi = 0;
                    x.DuToanMHCHV = 0;
                    x.DuToanDT = 0;
                    x.DuToan = 0;
                    return x;
                }).ToList();
            IEnumerable<NsSktChungTuChiTietModel> temps = Items.Where(x => x.IsRemainRow);
            DicItems = Items.IsEmpty() ? new Dictionary<Guid, NsSktChungTuChiTietModel>() : Items.GroupBy(x => x.IIdMlskt).ToDictionary(key => key.Key, value => value.FirstOrDefault());
            foreach (var item in temps)
            {
                NsSktChungTuChiTietModel firstParentRow = Items.FirstOrDefault(x => x.IIdMlskt == item.IdParent && x.IsFilter);

                if (firstParentRow?.IdParent != null)
                    CalculateParent(firstParentRow.IdParent, firstParentRow);
                if (firstParentRow == null) continue;
                {
                    var temp2 = Items.Where(x => x.IdParent == item.IIdMlskt && x.IdParent != Guid.Empty && x.IIdMaDonVi != _idDonViDuPhong && x.IsFilter).ToList();
                    var sumTC = temp2.Sum(x => x.FTuChi);
                    var sumMHHV = temp2.Sum(x => x.FMuaHangCapHienVat);
                    var sumDT = temp2.Sum(x => x.FPhanCap);
                    var sumHdtk = temp2.Sum(n => n.FHuyDongTonKho);

                    item.FTuChi = firstParentRow.FTuChi - sumTC;
                    item.FMuaHangCapHienVat = firstParentRow.FMuaHangCapHienVat - sumMHHV;
                    item.FPhanCap = firstParentRow.FPhanCap - sumDT;
                    item.FHuyDongTonKho = firstParentRow.FHuyDongTonKho - sumHdtk;
                }
            }
            UpdateTotal();
        }

        private void CountParentChild(List<NsSktChungTuChiTietModel> listChild, NsSktChungTuChiTietModel parent)
        {
            if (parent == null) return;
            parent.FTuChi = listChild.Sum(n => n.FTuChi);
            parent.FHienVat = listChild.Sum(n => n.FHienVat);
            parent.FHuyDongTonKho = listChild.Sum(n => n.FHuyDongTonKho);
            parent.FMuaHangCapHienVat = listChild.Sum(n => n.FMuaHangCapHienVat);
            parent.FPhanCap = listChild.Sum(n => n.FPhanCap);
            parent.SoNhuCau = listChild.Sum(n => n.SoNhuCau);
            parent.SoNhuCauMHHV = listChild.Sum(n => n.SoNhuCauMHHV);
            parent.SoNhuCauDT = listChild.Sum(n => n.SoNhuCauDT);
            parent.SoKiemTra = listChild.Sum(n => n.SoKiemTra);
            parent.SoKiemTraMHHV = listChild.Sum(n => n.SoKiemTraMHHV);
            parent.SoKiemTraDT = listChild.Sum(n => n.SoKiemTraDT);
            parent.FThongBaoDonVi = listChild.Sum(n => n.FThongBaoDonVi);
        }

        private void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item)
        {
            if (idParent == Guid.Empty) return;
            var model = DicItems.ContainsKey(idParent) ? DicItems[idParent] : null;
            if (model == null) return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            model.SoNhuCau += item.SoNhuCau;
            model.SoNhuCauMHHV += item.SoNhuCauMHHV;
            model.SoNhuCauDT += item.SoNhuCauDT;
            model.SoKiemTra += item.SoKiemTra;
            model.SoKiemTraMHHV += item.SoKiemTraMHHV;
            model.SoKiemTraDT += item.SoKiemTraDT;
            model.FThongBaoDonVi += item.FThongBaoDonVi;
            model.DuToan += item.DuToan;
            model.DuToanMHCHV += item.DuToanMHCHV;
            model.DuToanDT += item.DuToanDT;
            CalculateParent(model.IdParent, item);
        }

        private void CalculateDuPhong()
        {
            foreach (var conLai in Items)
            {
                if (conLai.IsRemainRow)
                {
                    var duPhong =
                        Items.FirstOrDefault(x => x.IIdMaDonVi == _idDonViDuPhong && x.IdParent == conLai.IIdMlskt);
                    if (duPhong != null && conLai != null)
                    {
                        duPhong.FTuChi = conLai.FTuChi;
                        duPhong.FMuaHangCapHienVat = conLai.FMuaHangCapHienVat;
                        duPhong.FPhanCap = conLai.FPhanCap;
                        if (duPhong.FTuChi != 0
                            || duPhong.FMuaHangCapHienVat != 0
                            || duPhong.FMuaHangCapHienVat != 0)
                        {
                            duPhong.IsModified = true;
                        }
                    }
                }
            }
        }

        private void CalculateDataSoNhuCau()
        {
            foreach (var it in Items)
            {
                if (it.IsFirstParentRow && it.IsHangCha)
                {
                    it.SoNhuCau = Items.Where(x => x.IIdMlskt == it.IIdMlskt && !x.IsHangCha && !x.IsRemainRow).Sum(x => x.SoNhuCau);
                    it.SoNhuCauMHHV = Items.Where(x => x.IIdMlskt == it.IIdMlskt && !x.IsHangCha && !x.IsRemainRow).Sum(x => x.SoNhuCauMHHV);
                    it.SoNhuCauDT = Items.Where(x => x.IIdMlskt == it.IIdMlskt && !x.IsHangCha && !x.IsRemainRow).Sum(x => x.SoNhuCauDT);
                }
            }
        }

        private void OnSaveDuPhong()
        {
            List<NsSktChungTuChiTietModel> lstUpdate = new List<NsSktChungTuChiTietModel>();
            foreach (var conLai in Items)
            {
                if (conLai.IsRemainRow)
                {
                    var duPhong =
                        Items.FirstOrDefault(x => x.IIdMaDonVi == _idDonViDuPhong && x.IdParent == conLai.IIdMlskt);
                    if (duPhong != null && conLai != null)
                    {
                        duPhong.FTuChi = conLai.FTuChi;
                        duPhong.FMuaHangCapHienVat = conLai.FMuaHangCapHienVat;
                        duPhong.FPhanCap = conLai.FPhanCap;
                        if (duPhong.FTuChi != 0
                            || duPhong.FMuaHangCapHienVat != 0
                            || duPhong.FMuaHangCapHienVat != 0)
                        {
                            lstUpdate.Add(duPhong);
                        }
                    }
                }
            }

            if (lstUpdate.Count > 0)
            {
                foreach (var updateItem in lstUpdate)
                {
                    var sktChungTuChiTiet = _sktChungTuChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, sktChungTuChiTiet);
                    _sktChungTuChiTietService.Update(sktChungTuChiTiet);
                }
            }
        }

        public void SaveThongTriThamDinh()
        {
            if (ItemsThamDinhSkt != null && ItemsThamDinhSkt.Count > 0)
            {
                List<NsSktNganhThamDinhChiTietSkt> lstEntities =
                    _mapper.Map<List<NsSktNganhThamDinhChiTietSkt>>(ItemsThamDinhSkt.Where(x => x.IsModified));
                foreach (var it in lstEntities)
                {
                    it.IIdCtsoKiemTra = Model.Id;
                    if (it.Id.Equals(Guid.Empty))
                    {
                        _iSktNganhThamDinhChiTietSktService.Add(it);
                    }
                    else
                    {
                        _iSktNganhThamDinhChiTietSktService.Update(it);
                    }

                    it.IsModified = false;
                }
            }
        }

        private void AutoPhanBoFirstTime()
        {
            var predicate = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.ILoai == DemandCheckType.DISTRIBUTION);
            predicate = predicate.And(x => x.ILoaiChungTu == Model.ILoaiChungTu.GetValueOrDefault(-1));
            predicate = predicate.And(x => x.IIdCtsoKiemTra.Equals(Model.Id));
            predicate = predicate.And(x => !x.IIdMaDonVi.Equals(_idDvDuPhong));
            predicate = predicate.And(x => x.FTuChi != 0 || x.FMuaHangCapHienVat != 0 || x.FPhanCap != 0 || x.FHuyDongTonKho != 0);
            var lstChiTiet = _sktChungTuChiTietService.FindByCondition(predicate);
            if (lstChiTiet.Count() <= 0)
            {
                FirstTimePhanBo = true;
                foreach (var conLai in Items)
                {
                    if (conLai.IsRemainRow)
                    {
                        var dv =
                            Items.FirstOrDefault(x => x.IdParent == conLai.IIdMlskt && !x.IIdMaDonVi.Equals(_idDonViDuPhong));
                        if (dv != null)
                        {
                            dv.FHuyDongTonKho = conLai.FHuyDongTonKho;
                            dv.FTuChi = conLai.FTuChi;
                            dv.FMuaHangCapHienVat = conLai.FMuaHangCapHienVat;
                            dv.FPhanCap = conLai.FPhanCap;
                            if (dv.FTuChi != 0
                                || dv.FMuaHangCapHienVat != 0
                                || dv.FPhanCap != 0
                                || dv.FHuyDongTonKho != 0)
                            {
                                dv.IsModified = true;
                            }
                        }
                    }
                }
                FirstTimePhanBo = false;
            }
        }

        public void CopySoNhuCauSangTuChi()
        {
            IsInit = true;
            if (Model.ILoaiChungTu.ToString() == VoucherType.NSSD_Key)
            {
                Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ForAll(x =>
                {
                    x.FTuChi = x.SoNhuCau;
                });
            }
            else
            {
                Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ForAll(x =>
                {
                    x.FPhanCap = x.SoNhuCauDT;
                    x.FMuaHangCapHienVat = x.SoNhuCauMHHV;
                });
            }

            CalculateData();
            IsInit = false;
        }

        private bool SktChungTuChiTietFilter(object obj)
        {
            var temp = (NsSktChungTuChiTietModel)obj;
            var condition = true && temp.IIdMaDonVi != _idDonViDuPhong;
            if (!string.IsNullOrEmpty(NsSktChungTuChiTietSearchModel.SKyHieu))
                condition = condition && temp.SKyHieu.ToLower()
                    .Contains(NsSktChungTuChiTietSearchModel.SKyHieu.Trim().ToLower());
            if (_selectedDataState != null && ListIdMucLucFilter != null && _selectedDataState.ValueItem.Equals(DataStateValue.SO_CON_LAI_CHUA_PHAN_BO.ToString()))
            {
                condition = condition && (ListIdMucLucFilter.Contains(temp.IIdMlskt) || temp.IsRemainRow && ListIdMucLucFilter.Contains(temp.IdParent));
            }
            if (_selectedDonVi != null && ListIdMucLucDonViFilter != null)
            {
                condition = condition && (!temp.IsHangCha && temp.IIdMaDonVi.Equals(_selectedDonVi.IIDMaDonVi) || temp.IsHangCha && (ListIdMucLucDonViFilter.Contains(temp.IdParent) || ListIdMucLucDonViFilter.Contains(temp.IIdMlskt)));
            }
            if (_selectedKhoiDonVi != null && _selectedKhoiDonVi.ValueItem != TypeKhoi.TAT_CA.ToString())
            {
                condition = condition && (!temp.IsHangCha && DonViItems.Select(x => x.IIDMaDonVi).Contains(temp.IIdMaDonVi) || temp.IsHangCha);
            }
            temp.IsFilter = condition;
            return condition;
        }

        private void UpdateTotal()
        {
            var listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var listRoot = Items.Where(x => x.IdParent.Equals(Guid.Empty) && !x.IsDeleted && x.IsFilter).ToList();
            Model.TongDuToan = 0;
            Model.TongQuyetToan = 0;
            FTongHuyDongTonKho = 0;
            Model.TongHuyDong = listChildren.Sum(x => x.FHuyDongTonKho); ;
            Model.FTongTuChi = listChildren.Sum(x => x.FTuChi);
            Model.FTongMuaHangCapHienVat = listChildren.Sum(x => x.FMuaHangCapHienVat);
            Model.FTongPhanCap = listChildren.Sum(x => x.FPhanCap);
            Model.TongSoNhuCau = listChildren.Sum(x => x.SoNhuCau);
            Model.TongSoNhuCauMHHV = listChildren.Sum(x => x.SoKiemTraMHHV);
            Model.TongSoNhuCauDT = listChildren.Sum(x => x.SoNhuCauDT);
            Model.TongSoKiemTra = listChildren.Sum(x => x.SoKiemTra);
            Model.TongSoKiemTraMHHV = listChildren.Sum(x => x.SoKiemTraMHHV);
            Model.TongSoKiemTraDT = listChildren.Sum(x => x.SoKiemTraDT);
            Model.TongTangPhanBo = listChildren.Sum(x => x.Tang);
            Model.TongGiamPhanBo = listChildren.Sum(x => x.Giam);
            Model.FTongThongBaoDonVi = listChildren.Sum(x => x.FThongBaoDonVi);
            Model.TongCanCuDuToan = listRoot.Sum(x => x.DuToan);
            Model.TongCanCuDuToanMHCHV = listRoot.Sum(x => x.DuToanMHCHV);
            Model.TongCanCuDuToanDT = listRoot.Sum(x => x.DuToanDT);
            Model.TongCanCuDuToan = listRoot.Sum(x => x.DuToan);
            FTongHuyDongTonKho = listChildren.Sum(x => x.FHuyDongTonKho);
            OnPropertyChanged(nameof(FTongHuyDongTonKho));
        }

        public ObservableCollection<DonViModel> GetListDonViByLoaiChungTu(int loaiChungTu, int namLamViec)
        {
            int iTrangThai = StatusType.ACTIVE;
            string loaiDV = "";
            List<DonVi> listNsDonViChild = new List<DonVi>();
            var isDvCap4 = _iNsDonViService
                .FindByCondition(x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.Loai == "1")
                .Count() <= 0;
            if (isDvCap4)
            {
                loaiDV = "0";
                listNsDonViChild = _iNsDonViService.FindByCondition(x =>
                    x.Loai == loaiDV && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).ToList();
            }
            else
            {
                loaiDV = "1";
                if (loaiChungTu == 1)
                {
                    listNsDonViChild = _iNsDonViService.FindByCondition(x =>
                        x.Loai == loaiDV && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).ToList();
                }
                else if (loaiChungTu == 2)
                {
                    var lstIdQuanLy = _iDanhMucService.FindByCondition(x => x.INamLamViec == namLamViec
                                                                            && x.ITrangThai == iTrangThai
                                                                            && VoucherType.DM_Nganh.Equals(x.SType))
                        .Select(x => x.SGiaTri).ToList();
                    listNsDonViChild = _iNsDonViService.FindByCondition(x => x.NamLamViec == namLamViec
                                                                             && x.ITrangThai == iTrangThai
                                                                             && x.Loai == loaiDV
                                                                             && (true.Equals(x.BCoNSNganh) ||
                                                                                 _idDonViDuPhong.Equals(x.IIDMaDonVi))
                                                                             && lstIdQuanLy.Contains(x.IIDMaDonVi))
                        .ToList();
                }
            }

            //return _mapper.Map<ObservableCollection<DonViModel>>(listNsDonViChild.Where(n => n.Khoi != "3"));
            return _mapper.Map<ObservableCollection<DonViModel>>(listNsDonViChild);
        }


        public void GetListMucLucFilterDonVi()
        {
            ListIdMucLucDonViFilter = new List<Guid>();
            if (Items != null)
            {
                var lstMucLucConLai = Items.Where(x => _selectedDonVi.IIDMaDonVi.Equals(x.IIdMaDonVi)).ToList();
                var lstIdMucLucConLai = lstMucLucConLai.Select(x => x.IIdMlskt).Distinct().ToList();
                //ListIdMucLucDonViFilter.AddRange(lstIdMucLucConLai);

                //tim cha
                while (true)
                {
                    var listIdMlCha = lstMucLucConLai.Select(x => x.IdParent).ToList();
                    var lstMlCha = Items.Where(x =>
                        !ListIdMucLucDonViFilter.Contains(x.IIdMlskt) && listIdMlCha.Contains(x.IIdMlskt)).ToList();
                    if (lstMlCha.Count > 0)
                    {
                        var lstId = lstMlCha.Select(x => x.IIdMlskt).Distinct().ToList();
                        ListIdMucLucDonViFilter.AddRange(lstId);
                        lstMucLucConLai = lstMlCha;
                    }
                    else
                    {
                        break;
                    }
                }

                //tim con
                lstMucLucConLai = Items.Where(x => _selectedDonVi.IIDMaDonVi.Equals(x.IIdMaDonVi)).ToList();
                while (true)
                {
                    var listIdMlCha = lstMucLucConLai.Select(x => x.IIdMlskt).ToList();
                    var lstMlCon = Items.Where(x =>
                        !ListIdMucLucDonViFilter.Contains(x.IIdMlskt) && listIdMlCha.Contains(x.IdParent)).ToList();
                    if (lstMlCon.Count > 0)
                    {
                        var lstId = lstMlCon.Select(x => x.IIdMlskt).Distinct().ToList();
                        ListIdMucLucDonViFilter.AddRange(lstId);
                        lstMucLucConLai = lstMlCon;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void GetListMucLucFilterConLai()
        {
            ListIdMucLucFilter = new List<Guid>();
            if (Items != null)
            {
                var lstMucLucConLai = Items.Where(x => x.IsRemainRow && (x.FTuChi > 0 || x.FMuaHangCapHienVat > 0 || x.FPhanCap > 0)).ToList();
                var lstIdMucLucConLai = lstMucLucConLai.Select(x => x.IIdMlskt).Distinct().ToList();
                ListIdMucLucFilter.AddRange(lstIdMucLucConLai);

                //tim cha
                while (true)
                {
                    var listIdMlCha = lstMucLucConLai.Select(x => x.IdParent).ToList();
                    var lstMlCha = Items.Where(x =>
                        !ListIdMucLucFilter.Contains(x.IIdMlskt) && listIdMlCha.Contains(x.IIdMlskt)).ToList();
                    if (lstMlCha.Count > 0)
                    {
                        var lstId = lstMlCha.Select(x => x.IIdMlskt).Distinct().ToList();
                        ListIdMucLucFilter.AddRange(lstId);
                        lstMucLucConLai = lstMlCha;
                    }
                    else
                    {
                        break;
                    }
                }

                //tim con
                lstMucLucConLai = Items.Where(x => x.IsRemainRow && (x.FTuChi > 0 || x.FMuaHangCapHienVat > 0 || x.FPhanCap > 0)).ToList();
                while (true)
                {
                    var listIdMlCha = lstMucLucConLai.Select(x => x.IIdMlskt).ToList();
                    var lstMlCon = Items.Where(x =>
                        !ListIdMucLucFilter.Contains(x.IIdMlskt) && listIdMlCha.Contains(x.IdParent)).ToList();
                    if (lstMlCon.Count > 0)
                    {
                        var lstId = lstMlCon.Select(x => x.IIdMlskt).Distinct().ToList();
                        ListIdMucLucFilter.AddRange(lstId);
                        lstMucLucConLai = lstMlCon;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void DeleteChiTiet(List<NsSktChungTuChiTietModel> lstItems)
        {
            if (lstItems.Count > 0)
            {
                foreach (var item in lstItems)
                {
                    var deleteItem = _sktChungTuChiTietService.FindById(item.Id);
                    if (deleteItem != null)
                    {
                        _mapper.Map(item, deleteItem);
                        _sktChungTuChiTietService.Delete(deleteItem);
                    }
                    item.FTuChi = 0;
                    item.FTuChiDeNghi = 0;
                    item.FHuyDongTonKho = 0;
                    item.FMuaHangCapHienVat = 0;
                    item.FPhanCap = 0;
                    item.FTonKhoDenNgay = 0;
                    item.SMoTa = string.Empty;
                    item.IsModified = false;
                    item.IsDeleted = false;
                }
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            //OpenDetailDialog();
            OpenExpertiseDetail();
        }

        public void OpenExpertiseDetail()
        {
            try
            {
                if (Model.ILoaiChungTu.GetValueOrDefault().Equals(int.Parse(VoucherType.NSSD_Key)) || SelectedItem == null || SelectedItem != null && SelectedItem.IsHangCha)
                {
                    return;
                }
                var lstItemsThamDinh = ItemsThamDinhSkt != null ? new ObservableCollection<NsSktNganhThamDinhChiTietSktModel>(ItemsThamDinhSkt.Where(x => x.IIdMucLuc.Equals(SelectedItem.IIdMlskt))) : null;
                CheckExpertiseDetailViewModel.Model = new ExpertiseModel();
                CheckExpertiseDetailViewModel.DataThamDinh = lstItemsThamDinh;
                CheckExpertiseDetailViewModel.IdMucLucSeleted = SelectedItem.IIdMlskt;
                CheckExpertiseDetailViewModel.PhanLoai = 2;

                var view = new CheckExpertiseDetail
                {
                    DataContext = CheckExpertiseDetailViewModel
                };
                view.SavedAction = obj =>
                {
                    var data = (List<NsSktNganhThamDinhChiTietSktModel>)obj;
                    var idMlskt = data.First().IIdMucLuc;
                    if (Items.Any())
                    {
                        var it = Items.FirstOrDefault(x => x.IIdMlskt.Equals(idMlskt) && !x.IsHangCha);
                        if (it != null)
                        {
                            it.FThongBaoDonVi = data.Sum(x => x.FTuChi.GetValueOrDefault());
                            it.IsModified = true;
                        }
                    }

                    AddItemToItemsThamDinh(data.ToList());
                    CalculateData();
                    view.Close();
                };
                CheckExpertiseDetailViewModel.CheckExpertiseDetail = view;
                CheckExpertiseDetailViewModel.Init();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LayDuLieuNganhThamDinh()
        {
            MessageBoxResult boxResult = MessageBoxHelper.Confirm(string.Format(Resources.MsgTransferThamDinh));
            if (boxResult != MessageBoxResult.Yes)
            {
                return;
            }

            ItemsThamDinhSkt = _mapper.Map<ObservableCollection<NsSktNganhThamDinhChiTietSktModel>>(GetChungTuThamDinhChiTiets());
            foreach (var td in ItemsThamDinhSkt)
            {
                td.Id = Guid.Empty;
                td.IsModified = true;
            }

            foreach (var it in Items)
            {
                double giaTri = ItemsThamDinhSkt.Where(x => x.IIdMucLuc.Equals(it.IIdMlskt)).Sum(x => x.FTuChi.GetValueOrDefault());
                if (!it.IsHangCha && !it.FThongBaoDonVi.Equals(giaTri))
                {
                    it.FThongBaoDonVi = giaTri;
                    it.IsModified = true;
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }

            IsGetDuLieuThamDinh = true;
            CalculateData();
        }

        public List<NsSktNganhThamDinhChiTiet> GetChungTuThamDinhChiTiets()
        {
            var lstCtThamDinh = GetChungTuThamDinh();
            var lstIdCtThamDinh = lstCtThamDinh.Select(x => x.Id).ToList();
            var predicate = PredicateBuilder.True<NsSktNganhThamDinhChiTiet>();
            predicate = predicate.And(x => lstIdCtThamDinh.Contains(x.IIdCtnganhThamDinh));
            var results = _sktThamDinhChiTietService.FindByCondition(predicate).ToList();
            return results;
        }

        public List<ExpertiseModel> GetChungTuThamDinh()
        {
            int loai = LoaiNganhThamDinh.CTNTD;
            int loaiNganSach = int.Parse(VoucherType.NSSD_Key);
            IEnumerable<ThDChungTuQuery> data = _sktThamDinhService.FindByNamLamViec(_sessionService.Current.YearOfWork,
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, _sessionService.Current.Principal, loai, loaiNganSach);
            List<ExpertiseModel> results;
            results = _mapper.Map<List<ExpertiseModel>>(data.Where(x => x.IsLocked));
            return results;
        }

        public void LoadDataChiTietThongBaoDv()
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinhChiTietSkt>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            var data = _iSktNganhThamDinhChiTietSktService.FindByCondition(predicate);
            ItemsThamDinhSkt = _mapper.Map<ObservableCollection<NsSktNganhThamDinhChiTietSktModel>>(data);
        }

        public void AddItemToItemsThamDinh(List<NsSktNganhThamDinhChiTietSktModel> dataAdd)
        {
            if (ItemsThamDinhSkt != null)
            {
                if (dataAdd.Any())
                {
                    foreach (var it in dataAdd)
                    {
                        var itTd = ItemsThamDinhSkt.FirstOrDefault(x =>
                            x.IIdMucLuc.Equals(it.IIdMucLuc) && x.IIdMaDonVi.Equals(it.IIdMaDonVi));
                        if (itTd != null)
                        {
                            itTd.FTuChi = it.FTuChi;
                            itTd.SGhiChu = it.SGhiChu;
                            itTd.IsModified = true;
                        }
                        else
                        {
                            it.Id = new Guid();
                            it.IsModified = true;
                            ItemsThamDinhSkt.Add(it);
                        }
                    }
                }
                else
                {
                    ItemsThamDinhSkt = new ObservableCollection<NsSktNganhThamDinhChiTietSktModel>();
                }

            }
            else
            {
                ItemsThamDinhSkt = new ObservableCollection<NsSktNganhThamDinhChiTietSktModel>(dataAdd);
                foreach (var td in ItemsThamDinhSkt)
                {
                    td.Id = Guid.Empty;
                    td.IsModified = true;
                }
            }
            OnPropertyChanged(nameof(IsSaveData));
        }

        public void LayDuLieuNganhThamDinhExcel()
        {
            try
            {
                var view = new ImportExpertiseDistribution
                {
                    DataContext = ImportExpertiseDistributionViewModel
                };
                view.SavedAction = obj =>
                {
                    var data = (List<NsSktNganhThamDinhChiTietSktModel>)obj;
                    var lstIdMucLuc = data.Select(x => x.IIdMucLuc).Distinct().ToList();
                    foreach (var idMl in lstIdMucLuc)
                    {
                        if (Items.Any())
                        {
                            var it = Items.FirstOrDefault(x => x.IIdMlskt.Equals(idMl) && !x.IsHangCha);
                            if (it != null)
                            {
                                it.FThongBaoDonVi = data.Where(x => x.IIdMucLuc.Equals(idMl)).Sum(x => x.FTuChi.GetValueOrDefault());
                                it.IsModified = true;
                            }
                        }
                    }

                    ItemsThamDinhSkt = null;
                    AddItemToItemsThamDinh(data.ToList());
                    IsGetDuLieuThamDinh = true;
                    CalculateData();
                    view.Close();
                };
                ImportExpertiseDistributionViewModel.ImportExpertiseDistribution = view;
                ImportExpertiseDistributionViewModel.Init();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPhanBoConLai()
        {
            FirstTimePhanBo = true;
            foreach (var conLai in Items)
            {
                if (conLai.IsRemainRow && conLai.FTuChi != 0)
                {
                    var dv =
                        Items.FirstOrDefault(x => x.IsFilter && x.IdParent == conLai.IIdMlskt && x.IIdMaDonVi.Equals(_selectedDonVi.IIDMaDonVi));
                    if (dv != null)
                    {
                        dv.FTuChi += conLai.FTuChi;
                        dv.IsModified = true;
                    }
                }
            }
            CalculateData();
            FirstTimePhanBo = false;
        }

        private bool IsUpdateToDelete(NsSktChungTuChiTietModel entity)
        {
            return entity.FTuChi == 0 && entity.FMuaHangCapHienVat == 0 && entity.FPhanCap == 0 && entity.FHuyDongTonKho == 0;
        }

        public void OnCopyEstimateData()
        {
            IsInit = true;
            if (Model.ILoaiChungTu.ToString() == VoucherType.NSSD_Key)
            {
                Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ForAll(x =>
                {
                    x.FTuChi = x.DuToan;
                });
            }
            else
            {
                Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ForAll(x =>
                {
                    x.FMuaHangCapHienVat = x.DuToanMHCHV;
                    x.FPhanCap = x.DuToanDT;
                });
            }

            CalculateData();
            IsInit = false;
        }
    }
}