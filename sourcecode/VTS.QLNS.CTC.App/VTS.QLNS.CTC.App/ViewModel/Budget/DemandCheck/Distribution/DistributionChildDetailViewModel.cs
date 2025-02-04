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
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution
{
    public class DistributionChildDetailViewModel : DetailViewModelBase<NsSktChungTuModel, NsSktChungTuChiTietModel>
    {
        private const string DetailHeaderStringFormat = "[{0}] Phân bổ số kiểm tra - Chứng từ chi tiết | Số: {1} - QĐ: {2} - Ngày: {3} | {4} | Chi tiết chứng từ";
        private readonly ProgressDialog _progressDialog = new ProgressDialog();
        private readonly ISessionService _sessionService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly IDanhMucService _iDanhMucService;
        private readonly INsSktNganhThamDinhChiTietSktService _iSktNganhThamDinhChiTietSktService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;


        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        private ICollectionView _sktChungTuChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }

        private NsSktChungTuChiTietModel _nsSktChungTuChiTietSearchModel;
        public NsSktChungTuChiTietModel NsSktChungTuChiTietSearchModel
        {
            get => _nsSktChungTuChiTietSearchModel;
            set => SetProperty(ref _nsSktChungTuChiTietSearchModel, value);
        }

        private ObservableCollection<SktMucLucModel> _sktMucLucModelItems;
        public ObservableCollection<SktMucLucModel> SktMucLucModelItems
        {
            get => _sktMucLucModelItems;
            set => SetProperty(ref _sktMucLucModelItems, value);
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        public bool IsBVTC;
        public bool IsInit { get; set; } = false;
        public Visibility ShowColNSBD { get; set; }
        public Visibility ShowColNSSD { get; set; }

        private string _popupSearchText;
        public string PopupSearchText
        {
            set
            {
                SetProperty(ref _popupSearchText, value);
                _searchPopupView?.Refresh();
            }
            get => _popupSearchText;
        }

        private SktMucLucModel _selectedPopupItem;
        public SktMucLucModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                NsSktChungTuChiTietSearchModel.SKyHieu = _selectedPopupItem?.SKyHieu;
                OnPropertyChanged(nameof(NsSktChungTuChiTietSearchModel));
                IsPopupOpen = false;
            }
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);

        private ComboboxItem _selectedDataState;
        public ComboboxItem SelectedDataState
        {
            get => _selectedDataState;
            set
            {
                SetProperty(ref _selectedDataState, value);
                if (_selectedDataState != null)
                {
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
                    if (SelectedNNganh != null)
                    {
                        ListMucLucTheoNganh = _sktChungTuChiTietService.FindMucLucSKTTheoNganh(SelectedNNganh.SGiaTri, 2, _sessionInfo.YearOfWork).ToList();
                    }
                    else
                    {
                        ListMucLucTheoNganh = null;
                    }

                    OnSearch();
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
                    if (_selectedCNganh != null)
                    {
                        ListMucLucTheoChuyenNganh = _sktChungTuChiTietService.FindMucLucSKTTheoNganh(SelectedCNganh.IIDMaDanhMuc, 2, _sessionInfo.YearOfWork).ToList();
                    }
                    else
                    {
                        ListMucLucTheoChuyenNganh = null;
                    }

                    OnSearch();
                }
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

        public List<MucLucSoKiemTraTheoNganhQuery> ListMucLucTheoNganh { get; set; }
        public List<MucLucSoKiemTraTheoNganhQuery> ListMucLucTheoChuyenNganh { get; set; }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                SetProperty(ref _isPopupOpen, value);
            }
        }

        private double _fTongHuyDongTonKho;
        public double FTongHuyDongTonKho
        {
            get => _fTongHuyDongTonKho;
            set => SetProperty(ref _fTongHuyDongTonKho, value);
        }

        public string QuyetToan => "Quyết toán " + (_sessionInfo.YearOfWork - 2);
        public string DuToan => "Dự toán đầu năm " + (_sessionInfo.YearOfWork - 1);
        public string SoNhuCau => "Số nhu cầu năm " + (_sessionInfo.YearOfWork);
        public string SoKiemTra => "Số kiểm tra năm " + (_sessionInfo.YearOfWork - 1);

        public int NamLamViec { get; set; }

        public string DetailHeader
        {
            set { }
            get
            {
                var temp = new StringBuilder();
                var user = _sessionInfo.Principal;
                var soChungTu = Model.SSoChungTu ?? string.Empty;
                var soQuyetDinh = Model.SSoQuyetDinh ?? string.Empty;
                var ngayQuyetDinh = Model.DNgayQuyetDinh == null
                    ? string.Empty
                    : Model.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy");
                var tenDonVi = Model.TenDonViIdDonVi ?? string.Empty;
                temp.AppendFormat(DetailHeaderStringFormat, user, soChungTu, soQuyetDinh, ngayQuyetDinh, tenDonVi)
                    .ToString();
                return temp.ToString();
            }
        }
        public override Type ContentType => typeof(CheckDetail);
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand CopySoNhuCau { get; }


        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get; }
        public CheckExpertiseDetailViewModel CheckExpertiseDetailViewModel { get; }
        public DistributionChildDetailViewModel(
            ISktChungTuChiTietService sktChungTuChiTietService,
            ISktChungTuService sktChungTuService,
            ISktMucLucService sktMucLucService,
            IDanhMucService iDanhMucService,
            ISessionService sessionService,
            INsSktNganhThamDinhChiTietSktService iSktNganhThamDinhChiTietSktService,
            INsMucLucNganSachService iMucLucNganSachService,
            ILog logger,
            CheckExpertiseDetailViewModel checkExpertiseDetailViewModel,
            IMapper mapper,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel)
        {
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktChungTuService = sktChungTuService;
            _sktMucLucService = sktMucLucService;
            _iDanhMucService = iDanhMucService;
            _sessionService = sessionService;
            _iSktNganhThamDinhChiTietSktService = iSktNganhThamDinhChiTietSktService;
            _iNsMucLucNganSachService = iMucLucNganSachService;
            _logger = logger;
            _mapper = mapper;
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;
            CheckExpertiseDetailViewModel = checkExpertiseDetailViewModel;
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();

            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            PrintCommand = new RelayCommand(OnPrint);
            CopySoNhuCau = new RelayCommand(obj => CopySoNhuCauSangTuChi());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null) IsLock = Model.BKhoa;
            LoadDataState();
            LoadNhomNganh();
            LoadChuyenNganh();
            LoadData();
            LoadPopupData();
            LoadDataChiTietThongBaoDv();
        }

        private void LoadDataState()
        {
            DataStateItems = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem()
                {
                    ValueItem = DataStateValue.HIEN_THI_TAT_CA.ToString(),
                    DisplayItem = DataStateName.HIEN_THI_TAT_CA,
                },
                new ComboboxItem()
                {
                    ValueItem = DataStateValue.DA_NHAP_SKT.ToString(), DisplayItem = DataStateName.DA_NHAP_SKT,
                }
            };
            _selectedDataState = DataStateItems[1];
        }

        public override void LoadData(params object[] args)
        {
            var iLoai = DemandCheckType.DISTRIBUTION;
            if (IsBVTC)
            {
                iLoai = DemandCheckType.CORPORATIZED_HOSPITAL;
            }
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = Model.Id;
            searchCondition.ILoai = iLoai;
            searchCondition.ILoaiNguonNganSach = Model.ILoaiNguonNganSach.GetValueOrDefault(1);
            searchCondition.IdDonVi = Model.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.UserName = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = Model.ILoaiChungTu.GetValueOrDefault(-1);
            searchCondition.HienThi = int.Parse(SelectedDataState.ValueItem);
            var temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            Items = _mapper.Map<ObservableCollection<NsSktChungTuChiTietModel>>(temp);
            _sktChungTuChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            //them can cu du toan
            AddCanCuDuToanNamTruoc();
            foreach (var sktChungTuChiTietModel in Items)
            {
                sktChungTuChiTietModel.ILoaiChungTu = Model.ILoaiChungTu;
                sktChungTuChiTietModel.ILoai = DemandCheckType.DISTRIBUTION;
                sktChungTuChiTietModel.IsFilter = true;
                if (!sktChungTuChiTietModel.IsHangCha)
                {
                    sktChungTuChiTietModel.PropertyChanged += ((sender, args) =>
                    {
                        if (args.PropertyName == nameof(SelectedItem.FHuyDongTonKho) ||
                            args.PropertyName == nameof(SelectedItem.FTuChi) ||
                            args.PropertyName == nameof(SelectedItem.FMuaHangCapHienVat) ||
                            args.PropertyName == nameof(SelectedItem.FPhanCap) ||
                            args.PropertyName == nameof(SelectedItem.SoNhuCau))
                        {

                            NsSktChungTuChiTietModel item = (NsSktChungTuChiTietModel)sender;
                            item.IsModified = true;
                            if (!IsInit)
                            {
                                CalculateData();
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    });
                }
            }
            _sktChungTuChiTietModelsView.Filter = SktChungTuChiTietFilter;
            CalculateData();
        }

        private void AddCanCuDuToanNamTruoc()
        {
            var loaiChungTu = Model.ILoaiChungTu;
            List<CanCuDuToanNamTruocSoKiemTraQuery> lstCanCu = _sktChungTuChiTietService
                .FindCanCuPhanSoKiemTra(loaiChungTu.GetValueOrDefault(), Model.IIdMaDonVi, Model.INamLamViec - 1, _sessionInfo.YearOfBudget, _sessionInfo.Budget, false).ToList();
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
                var mucLuc = Items.FirstOrDefault(item => item.SKyHieuCu == cc.KyHieu);
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
                        mucLuc.DuToanMHCHV += cc.HangNhap + cc.HangMua;
                        mucLuc.DuToanDT += cc.PhanCap;
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
            foreach (var item in temp)
            {
                CalculateParentCanCuDuToanNamTruoc(item.IdMlnsCha, items, item);
            }

            UpdateEstimateTotal();
        }

        private void CalculateParentCanCuDuToanNamTruoc(Guid? idParent, List<CanCuDuToanNamTruocSoKiemTraQuery> listData, CanCuDuToanNamTruocSoKiemTraQuery item)
        {
            var model = listData.FirstOrDefault(x => x.IdMlns == idParent.GetValueOrDefault());
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
            foreach (var item in listChildren)
            {
                Model.TongHuyDong += item.FHuyDongTonKho;
                Model.FTongTuChi += item.FTuChi;
                Model.FTongMuaHangCapHienVat += item.FMuaHangCapHienVat;
                Model.FTongPhanCap += item.FPhanCap;
                Model.TongSoNhuCau += item.SoNhuCau;
                Model.TongSoNhuCauMHHV += item.SoNhuCauMHHV;
                Model.TongSoNhuCauDT += item.SoNhuCauDT;
                Model.TongSoKiemTra += item.SoKiemTra;
                Model.TongSoKiemTraMHHV += item.SoKiemTraMHHV;
                Model.TongSoKiemTraDT += item.SoKiemTraDT;
                Model.TongCanCuDuToan += item.DuToan;
                Model.TongCanCuDuToanMHCHV += item.DuToanMHCHV;
                Model.TongCanCuDuToanDT += item.DuToanDT;
                Model.FTongThongBaoDonVi += item.FThongBaoDonVi;
                FTongHuyDongTonKho += item.FHuyDongTonKho;
            }
            OnPropertyChanged(nameof(FTongHuyDongTonKho));
        }

        private void LoadPopupData()
        {
            var predicate = PredicateBuilder.True<NsSktMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => true.Equals(x.BHangCha));
            var temp = _sktMucLucService.FindByCondition(predicate).OrderBy(x => x.SKyHieu).ToList();
            SktMucLucModelItems = _mapper.Map<ObservableCollection<SktMucLucModel>>(temp);
            _searchPopupView = CollectionViewSource.GetDefaultView(SktMucLucModelItems);
            _searchPopupView.Filter = PopupFilter;
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
            var list = _iDanhMucService.FindByCondition(predicate).ToList();
            CNganhItems = _mapper.Map<ObservableCollection<DanhMucNganhModel>>(list);
            _selectedCNganh = null;
        }

        private void OnClearSearch(object obj)
        {
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();
            _sktChungTuChiTietModelsView.Refresh();
        }

        private void OnSearch()
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

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;
            SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            CalculateData();
            OnPropertyChanged(nameof(IsSaveData));
        }

        public override void OnSave()
        {
            Func<NsSktChungTuChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<NsSktChungTuChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<NsSktChungTuChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<NsSktChungTuChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _sktChungTuChiTietService.AddRange(addItems);
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
                    _sktChungTuChiTietService.Update(sktChungTuChiTiet);
                    updateItem.IsModified = false;
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
            var message = Resources.MsgSaveDone;
            var messageBox = new NSMessageBoxViewModel(message);
            DialogHost.Show(messageBox.Content, DemandCheckScreen.DETAIL_DIALOG);
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
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        public void CopySoNhuCauSangTuChi()
        {
            IsInit = true;
            Items.ForAll(x => x.FTuChi = x.SoNhuCau);
            CalculateData();
            IsInit = false;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FThongBaoDonVi = 0;
                    x.FPhanCap = 0;
                    x.SoNhuCau = 0;
                    x.SoNhuCauMHHV = 0;
                    x.SoNhuCauDT = 0;
                    x.SoKiemTra = 0;
                    x.DuToan = 0;
                    return x;
                }).ToList();
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item);
            }

            UpdateTotal();
        }

        private void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item)
        {
            var model = Items.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FThongBaoDonVi += item.FThongBaoDonVi;
            model.FPhanCap += item.FPhanCap;
            model.SoNhuCau += item.SoNhuCau;
            model.SoNhuCauMHHV += item.SoNhuCauMHHV;
            model.SoNhuCauDT += item.SoNhuCauDT;
            model.SoKiemTra += item.SoKiemTra;
            model.DuToan += item.DuToan;
            CalculateParent(model.IdParent, item);
        }

        private bool PopupFilter(object obj)
        {
            var temp = (SktMucLucModel)obj;
            var condition = true;
            if (!string.IsNullOrEmpty(PopupSearchText))
                condition = condition && (temp.SSTT.ToLower().Contains(PopupSearchText.Trim().ToLower())
                                          || temp.SMoTa.ToLower().Contains(PopupSearchText.Trim().ToLower())
                            || temp.SKyHieu.ToLower().Contains(PopupSearchText.Trim().ToLower()));
            return condition;
        }

        private bool SktChungTuChiTietFilter(object obj)
        {
            var temp = (NsSktChungTuChiTietModel)obj;
            var condition = true;
            if (!string.IsNullOrEmpty(NsSktChungTuChiTietSearchModel.SKyHieu))
                condition = condition && temp.SKyHieu.ToLower()
                    .Contains(NsSktChungTuChiTietSearchModel.SKyHieu.Trim().ToLower());

            if (SelectedNNganh != null)
            {
                if (!string.IsNullOrEmpty(SelectedNNganh.SGiaTri))
                {
                    condition = condition && ListMucLucTheoNganh != null && ListMucLucTheoNganh.Exists(x => x.IIdMlskt.Equals(temp.IIdMlskt));
                }
            }

            if (SelectedCNganh != null)
            {
                if (!string.IsNullOrEmpty(SelectedCNganh.IIDMaDanhMuc))
                {
                    condition = condition && ListMucLucTheoChuyenNganh != null && ListMucLucTheoChuyenNganh.Exists(x => x.IIdMlskt.Equals(temp.IIdMlskt));
                }
            }

            temp.IsFilter = condition;
            return condition;
        }

        private void IsChild(NsSktChungTuChiTietModel parent, string[] values, List<NsSktChungTuChiTietModel> listParent)
        {
            foreach (var item in Items)
            {
                if (!listParent.Contains(item) && item.IdParent.Equals(parent.IIdMlskt))
                {
                    listParent.Add(item);
                    IsChild(item, values, listParent);
                }
            }
        }

        private void UpdateTotal()
        {
            Model.TongDuToan = 0;
            Model.TongQuyetToan = 0;
            Model.TongHuyDong = 0;
            Model.FTongTuChi = 0;
            Model.FTongMuaHangCapHienVat = 0;
            Model.FTongThongBaoDonVi = 0;
            Model.FTongPhanCap = 0;
            Model.TongSoNhuCau = 0;
            Model.TongSoNhuCauMHHV = 0;
            Model.TongSoNhuCauDT = 0;
            Model.TongSoKiemTra = 0;
            Model.TongSoKiemTraMHHV = 0;
            Model.TongSoKiemTraDT = 0;
            Model.TongCanCuDuToan = 0;
            FTongHuyDongTonKho = 0;
            var listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                Model.TongHuyDong += item.FHuyDongTonKho;
                Model.FTongTuChi += item.FTuChi;
                Model.FTongMuaHangCapHienVat += item.FMuaHangCapHienVat;
                Model.FTongThongBaoDonVi += item.FThongBaoDonVi;
                Model.FTongPhanCap += item.FPhanCap;
                Model.TongSoNhuCau += item.SoNhuCau;
                Model.TongSoNhuCauMHHV += item.SoNhuCauMHHV;
                Model.TongSoNhuCauDT += item.SoNhuCauDT;
                Model.TongSoKiemTra += item.SoKiemTra;
                Model.TongSoKiemTraMHHV += item.SoKiemTraMHHV;
                Model.TongSoKiemTraDT += item.SoKiemTraDT;
                Model.TongCanCuDuToan += item.DuToan;
                FTongHuyDongTonKho += item.FHuyDongTonKho;
            }
            OnPropertyChanged(nameof(FTongHuyDongTonKho));
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
                CheckExpertiseDetailViewModel.IsReadOnly = true;

                var view = new CheckExpertiseDetail
                {
                    DataContext = CheckExpertiseDetailViewModel
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
    }
}