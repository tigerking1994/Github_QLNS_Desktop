using AutoMapper;
using ControlzEx.Standard;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport
{
    public class PrintReportDieuChinhDuToanTheoLanViewModel : ViewModelBase
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IBhDtcDcdToanChiService _bhDtcDcdToanChiService;
        private readonly IBhDtcDcdToanChiChiTietService _bhDtcDcdToanChiChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhMucLoaiChiService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IDanhMucService _danhMucService;
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        private string _diaDiem;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        string templateFileName;
        string fileNamePrefix;
        private string _cap1;
        private string TitleFirst;
        List<ExportResult> results = new List<ExportResult>();


        private List<BhDtcDcdToanChiChiTietModel> _lstChungTuOrigin;
        public List<BhDtcDcdToanChiChiTietModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }

        private List<BhDtcDcdToanChiModel> _lstDtcDcdToanChi;
        public List<BhDtcDcdToanChiModel> LstDtcDcdToanChi
        {
            get => _lstDtcDcdToanChi;
            set => SetProperty(ref _lstDtcDcdToanChi, value);
        }
        public bool IsExportEnable => ListDonVi != null && ListDonVi.Any(x => x.IsChecked);
        public override Type ContentType => typeof(PrintReportDieuChinhDuToanChiTiet);
        public static DtDcDtCheckPrintType dtDcDtCheckPrintType { get; set; }

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

        public override string Name => "Báo cáo điều chỉnh dự toán - Đơn vị";
        public override string Title => "Báo cáo điều chỉnh dự toán - Đơn vị";
        public override string Description => "Báo cáo điều chỉnh dự toán - Đơn vị";

        public bool IsShowInTheoTongHop { get; set; }

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

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (_selectedDanhMucLoaiChi != null)
                {
                    LoadDonVi();
                    LoadTypeChuKy(_selectedDanhMucLoaiChi.HiddenValue);
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);

        }


        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Count : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
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

        public string LabelSelectedCountDonVi
        {
            get => $"ĐƠN VỊ ({ListDonVi.Count(item => item.IsChecked)}/{ListDonVi.Count})";
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

        private ObservableCollection<CheckBoxItem> _listDonVi = new ObservableCollection<CheckBoxItem>();

        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region Constructor
        public PrintReportDieuChinhDuToanTheoLanViewModel(
            ISessionService sessionService,
            IMapper mapper,
            INsDonViService nsDonViService,
            IBhDtcDcdToanChiService bhDtcDcdToanChiService,
            IBhDtcDcdToanChiChiTietService bhDtcDcdToanChiChiTietService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            IDmChuKyService dmChuKyService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            ILog log,
            IDanhMucService danhMucService,
            IExportService exportService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _bhDtcDcdToanChiService = bhDtcDcdToanChiService;
            _bhDtcDcdToanChiChiTietService = bhDtcDcdToanChiChiTietService;
            _bhMucLoaiChiService = bhDanhMucLoaiChiService;
            _dmChuKyService = dmChuKyService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _logger = log;
            _danhMucService = danhMucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _exportService = exportService;

            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }
        #endregion

        #region Add chữ ký
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
        #endregion

        #region Init
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;

            InitReportDefaultDate();
            LoadDanhMucLoaiChi();
            Clear();
            LoadTitleFirst();
            LoadCatUnitTypes();
            LoadDiaDiem();
            LoadKieuGiayIn();
            LoadDonVi();
            //LoadDataDot();
        }
        #endregion

        #region Load data
        private void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }


        private void LoadDiaDiem()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM);
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
        }

        private void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }

        private void LoadCatUnitTypes()
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

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy == null) return string.Empty;
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private void Clear()
        {
            IsInTheoTongHop = false;
            _donViCollectionView = null;
        }

        public override void OnClose(object obj)
        {
            try
            {
                base.OnClose(obj);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void LoadDonVi()
        {
            ListDonVi = LoadDonViBaoCaoPhuLucDonVi();
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
                    OnPropertyChanged(nameof(IsExportEnable));
                };
            }

            OnPropertyChanged(nameof(IsExportEnable));
        }

        private ObservableCollection<CheckBoxItem> LoadDonViBaoCaoPhuLucDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicateDv = PredicateBuilder.True<DonVi>();

            var lstChungTuDuocXem = GetListChungTuThuongTheoDonViXem();
            predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
            predicateDv = predicateDv.And(x => lstChungTuDuocXem.Contains(x.IIDMaDonVi));

            var lstDonVi = _nsDonViService.FindByCondition(predicateDv).ToList().Distinct();
            if (IsInTheoTongHop)
                lstDonVi = lstDonVi.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
            else lstDonVi = lstDonVi.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
            if (lstDonVi.Any())
            {
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join("-", item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }

        private List<string> GetListChungTuThuongTheoDonViXem()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicateDv = PredicateBuilder.True<BhDtcDcdToanChi>();
            predicateDv = predicateDv.And(x => x.INamLamViec == yearOfWork);

            if (SelectedDanhMucLoaiChi != null)
            {
                predicateDv = predicateDv.And(x => x.IID_LoaiCap == SelectedDanhMucLoaiChi.Id);
            }

            IEnumerable<BhDtcDcdToanChi> listChungTuBHXH;
            listChungTuBHXH = _bhDtcDcdToanChiService.FindByCondition(predicateDv).ToList();
            var lstIdDonVi = listChungTuBHXH.Select(x => x.IID_MaDonVi).Distinct().ToList();
            return lstIdDonVi;
        }

        private void LoadDanhMucLoaiChi()
        {
            try
            {
                ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
                IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                if (listDanhMucLoaiChi != null)
                {
                    ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                    {
                        DisplayItem = n.STenDanhMucLoaiChi,
                        ValueItem = n.Id.ToString(),
                        HiddenValue = n.SLNS,
                        Id = n.Id
                    }));
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
                }

                OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }

        }

        private void LoadTypeChuKy(string sLNSForDanhMuc)
        {
            switch (sLNSForDanhMuc.Trim())
            {
                case DanhMucLoaiChiSLNS.CHI_BHXH_SLNS:
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIBHXH_CHITIET;
                    TitleFirst = EstimateTitlePrint.Title1BaoCao;
                    break;
                case DanhMucLoaiChiSLNS.CHI_KINH_PHI_QUAN_LY_SLNS:
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPQL_CHITIET;
                    TitleFirst = EstimateTitlePrint.Title1BaoCao;
                    break;

                case DanhMucLoaiChiSLNS.CHI_KINH_PHI_QUAN_Y_SLNS:
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_CHITIET;
                    TitleFirst = EstimateTitlePrint.Title1BaoCao;
                    break;

                case DanhMucLoaiChiSLNS.CHI_KINH_PHI_TRUONG_SA_SLNS:
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPKCB_TS_CHITIET;
                    TitleFirst = EstimateTitlePrint.Title1BaoCao;
                    break;

                case DanhMucLoaiChiSLNS.CHI_KINH_PHI_CHAM_SOC_HSSV_NLD_SLNS:
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_CHITIET;
                    TitleFirst = EstimateTitlePrint.Title1BaoCao;
                    break;
                case DanhMucLoaiChiSLNS.CHI_TU_NGUON_KET_DU_QUY_KCB_BHYT_QUAN_NHAN_SLNS:
                    _typeChuky = TypeChuKy.RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_CHITIET;
                    TitleFirst = EstimateTitlePrint.Title1BaoCao;
                    break;
                default:
                    _typeChuky = string.Empty;
                    break;

            }

            LoadTitleFirst();
        }

        #endregion

        #region Export
        private void OnExport(ExportType exportType)
        {
            var currentDonVi = GetNsDonViOfCurrentUser();
            var yearOfWork = _sessionService.Current.YearOfWork;
            if (!ListDonVi.Any(item => item.IsChecked))
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            //try
            //{
            //    BackgroundWorkerHelper.Run((s, e) =>
            //    {
            //        IsLoading = true;
            //        string sCap1 = GetLevelTitle(_dmChuKy, 1);
            //        string sCap2 = GetLevelTitle(_dmChuKy, 2);
            //        int dvt = Convert.ToInt32(CatUnitTypeSelected.ValueItem);
            //        List<BhDtcDcdToanChiChiTietModel> lstForDanhMucLoaiChi = new List<BhDtcDcdToanChiChiTietModel>();
            //        List<ExportResult> results = new List<ExportResult>();
            //        foreach (CheckBoxItem itemDonVi in ListDonVi.Where(n => n.IsChecked))
            //        {
            //            var predicate = PredicateBuilder.True<BhDtcDcdToanChi>();
            //            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            //            predicate = predicate.And(x => x.IID_MaDonVi == itemDonVi.ValueItem);
            //            predicate = predicate.And(x => x.IID_LoaiCap == SelectedDanhMucLoaiChi.Id);

            //            BhDtcDcdToanChi chungTu = _bhDtcDcdToanChiService.FindByCondition(predicate).FirstOrDefault();
            //            if (chungTu == null) return;

            //            var searchCondition = new BhDtcDcdToanChiChiTietCriteria
            //            {
            //                DtcDcdToanChiId = chungTu.Id,
            //                LNS = chungTu.SLNS,
            //                NamLamViec = _sessionInfo.YearOfWork,
            //                IdDonVi = chungTu.IID_MaDonVi,
            //                ILoaiDanhMucChi = chungTu.IID_LoaiCap,
            //                NgayChungTu = chungTu.DNgayChungTu,
            //                LoaiChungTu = chungTu.ILoaiTongHop,
            //                UserName = _sessionInfo.Principal
            //            };

            //            var lstbhDtcDcdToanChiChiTiets = _bhDtcDcdToanChiChiTietService.FindByConditionForChildUnit(searchCondition).ToList();

            //            _lstChungTuOrigin = _mapper.Map<List<BhDtcDcdToanChiChiTietModel>>(lstbhDtcDcdToanChiChiTiets);
            //            CalculateData(_lstChungTuOrigin);

            //            _lstChungTuOrigin.ForAll(x =>
            //            {
            //                x.FTienDuToanDuocGiao = x.FTienDuToanDuocGiao / dvt;
            //                x.FTienThucHien06ThangDauNam = x.FTienThucHien06ThangDauNam / dvt;
            //                x.FTienUocThucHien06ThangCuoiNam = x.FTienUocThucHien06ThangCuoiNam / dvt;
            //                x.FTienUocThucHienCaNam = x.FTienUocThucHienCaNam / dvt;
            //                x.FTienSoSanhTang = x.FTienSoSanhTang / dvt;
            //                x.FTienSoSanhGiam = x.FTienSoSanhGiam / dvt;
            //            });

            //            switch (SelectedDanhMucLoaiChi.HiddenValue.Trim())
            //            {
            //                case DanhMucLoaiChiSLNS.CHI_BHXH_SLNS:
            //                    lstForDanhMucLoaiChi = _lstChungTuOrigin.Where(x => string.IsNullOrEmpty(x.STM)).ToList();
            //                    break;
            //                case DanhMucLoaiChiSLNS.CHI_KINH_PHI_QUAN_LY_SLNS:
            //                    ListDieuChinhDuToanTheoKPQL(lstForDanhMucLoaiChi);
            //                    break;
            //                case DanhMucLoaiChiSLNS.CHI_KINH_PHI_QUAN_Y_SLNS:
            //                case DanhMucLoaiChiSLNS.CHI_KINH_PHI_TRUONG_SA_SLNS:
            //                case DanhMucLoaiChiSLNS.CHI_KINH_PHI_CHAM_SOC_HSSV_NLD_SLNS:
            //                case DanhMucLoaiChiSLNS.CHI_TU_NGUON_KET_DU_QUY_KCB_BHYT_QUAN_NHAN_SLNS:
            //                    lstForDanhMucLoaiChi = _lstChungTuOrigin.ToList();
            //                    break;
            //            }

            //            var dataSLNS = SelectedDanhMucLoaiChi.HiddenValue.Split(',');
            //            var lstSum = lstForDanhMucLoaiChi.Where(x => dataSLNS.Contains(x.SXauNoiMa));
            //            var SumFTienDuToanDuocGiao = lstSum.Sum(x => x.FTienDuToanDuocGiao);
            //            var SumFTienThucHien06ThangDauNam = lstSum.Sum(x => x.FTienThucHien06ThangDauNam);
            //            var SumFTienUocThucHien06ThangCuoiNam = lstSum.Sum(x => x.FTienUocThucHien06ThangCuoiNam);
            //            var SumFTienUocThucHienCaNam = lstSum.Sum(x => x.FTienUocThucHienCaNam);
            //            var SumFTienSoSanhTang = lstSum.Sum(x => x.FTienSoSanhTang);
            //            var SumFTienSoSanhGiam = lstSum.Sum(x => x.FTienSoSanhGiam);
            //            var TongSoTien = SumFTienSoSanhTang;
            //            Dictionary<string, object> data = new Dictionary<string, object>();

            //            FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
            //            data.Add("TieuDe1", TxtTitleFirst);
            //            data.Add("FormatNumber", formatNumber);
            //            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            //            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
            //            data.Add("DonVi", itemDonVi.NameItem);
            //            data.Add("SumFTienDuToanDuocGiao", SumFTienDuToanDuocGiao);
            //            data.Add("SumFTienThucHien06ThangDauNam", SumFTienThucHien06ThangDauNam);
            //            data.Add("SumFTienUocThucHien06ThangCuoiNam", SumFTienUocThucHien06ThangCuoiNam);
            //            data.Add("SumFTienUocThucHienCaNam", SumFTienUocThucHienCaNam);
            //            data.Add("SumFTienSoSanhTang", SumFTienSoSanhTang);
            //            data.Add("SumFTienSoSanhGiam", SumFTienSoSanhGiam);
            //            data.Add("ListData", lstForDanhMucLoaiChi);
            //            data.Add("SKTML", lstForDanhMucLoaiChi);
            //            data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText((double)TongSoTien* dvt, true) : string.Empty);
            //            data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));

            //            AddChuKy(data, _typeChuky);
            //            data.Add("ThoiGian", _diaDiem + "," + DateUtils.FormatDateReport(ReportDate));

            //            templateFileName = string.Empty;
            //            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.PRT_BH_DT_DCDT_CHUNGTU_CHITIET_PHULUC_DOC));
            //            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
            //            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + chungTu.SSoChungTu);
            //            var xlsFile = _exportService.Export<BhDtcDcdToanChiChiTietModel, BhDmMucLucNganSach, BhDtcDcdToanChiChiTiet>(templateFileName, data);
            //            results.Add(new ExportResult("DỰ TOÁN ĐIỀU CHỈNH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
            //            e.Result = results;
            //        }
            //    }, (s, e) =>
            //    {
            //        if (e.Error == null)
            //        {
            //            var result = (List<ExportResult>)e.Result;
            //            if (result != null)
            //            {
            //                _exportService.Open(result, exportType);
            //            }
            //        }
            //        else
            //        {
            //            _logger.Error(e.Error.Message);
            //        }
            //        IsLoading = false;
            //    });
            //}
            //catch (Exception ex)
            //{

            //    _logger.Error(ex.Message, ex);
            //}
        }

        private static void ListDieuChinhDuToanTheoKPQL(List<BhDtcDcdToanChiChiTietModel> lstForDanhMucLoaiChi)
        {
            foreach (var chungTuModel in lstForDanhMucLoaiChi)
            {
                if (!chungTuModel.IsHangCha)
                {
                    chungTuModel.SM = string.Empty;
                }

                if (!string.IsNullOrEmpty(chungTuModel.STTM))
                {
                    if (Convert.ToInt32(chungTuModel.STTM) > 0)
                    {
                        chungTuModel.STM = string.Empty;
                    }
                }
            }
        }

        private string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_DT_DCDT, input + FileExtensionFormats.Xlsx);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == LoaiDonVi.ROOT);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser; ;
        }

        private void AddChuKy(Dictionary<string, object> data, string rPT_BHXH_DUTOAN_DIEUCHINH)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(rPT_BHXH_DUTOAN_DIEUCHINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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

        private void CalculateData(List<BhDtcDcdToanChiChiTietModel> lstChungTuOrigin)
        {
            lstChungTuOrigin.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FTienDuToanDuocGiao = 0;
                   x.FTienThucHien06ThangDauNam = 0;
                   x.FTienUocThucHien06ThangCuoiNam = 0;
                   x.FTienUocThucHienCaNam = 0;
                   x.FTienSoSanhTang = 0;
                   x.FTienSoSanhGiam = 0;
               });

            LstChungTuOrigin.Where(x => !x.IsHangCha)
              .ForAll(x =>
              {
                  x.FTienSoSanhTang = (x.FCong > 0 && x.FCong > x.FTienDuToanDuocGiao) ? x.FCong - x.FTienDuToanDuocGiao : 0;
                  x.FTienSoSanhGiam = (x.FCong > 0 && x.FCong < x.FTienDuToanDuocGiao) ? x.FTienDuToanDuocGiao - x.FCong : 0;
              });

            var temp = lstChungTuOrigin.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = lstChungTuOrigin.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }
        }

        private void CalculateParent(Guid idParent, BhDtcDcdToanChiChiTietModel item, Dictionary<Guid, BhDtcDcdToanChiChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao;
            model.FTienThucHien06ThangDauNam += item.FTienThucHien06ThangDauNam;
            model.FTienUocThucHien06ThangCuoiNam += item.FTienUocThucHien06ThangCuoiNam;
            model.FTienUocThucHienCaNam += item.FTienUocThucHienCaNam;
            model.FTienSoSanhTang += item.FTienSoSanhTang;
            model.FTienSoSanhGiam += item.FTienSoSanhGiam;

            CalculateParent(model.IdParent, item, dictByMlns);
        }
        #endregion
    }
}
