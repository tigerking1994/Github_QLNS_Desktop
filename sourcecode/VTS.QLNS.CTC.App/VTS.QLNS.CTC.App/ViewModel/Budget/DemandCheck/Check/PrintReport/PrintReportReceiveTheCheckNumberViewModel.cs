using AutoMapper;
using log4net;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.PrintReport
{
    public class PrintReportReceiveTheCheckNumberViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private ICollectionView _listDonViView;
        private readonly INsDonViService _nSDonViService;
        private readonly ISktChungTuChiTietService _iSktChungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly ISktChungTuService _iSktChungTuService;
        private readonly IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private string _diaDiem;

        public override Type ContentType =>
            typeof(View.Budget.DemandCheck.Check.PrintReport.PrintReportReceiveTheCheckNumber);

        public RelayCommand ExportExcelCommand { get; set; }
        public RelayCommand ExportPdfCommand { get; set; }
        public RelayCommand ExportPrintCommand { get; set; }
        public RelayCommand ConfigSignCommand { get; }
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public DemandCheckPrintType _demandCheckPrintType { get; set; }

        private LoaiNSBD _loaiNSBD;
        public LoaiNSBD LoaiNSBD
        {
            get => _loaiNSBD;
            set
            {
                SetProperty(ref _loaiNSBD, value);
            }
        }

        public bool IsVisibilityRadioButtonNSBD => DemandCheckPrintType.REPORT_SO_SANH_NHAN_SKT_NAM_TRUOC_NAM_NAY.Equals(_demandCheckPrintType)
            && VoucherType.NSBD_Key.Equals(VoucherTypeSelected?.ValueItem);

        public bool IsVisibilityLoaiBaoCao => !DemandCheckPrintType.REPORT_SO_SANH_NHAN_SKT_NAM_TRUOC_NAM_NAY.Equals(_demandCheckPrintType);

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get
            {
                if (StringUtils.IsNullOrEmpty(_txtTitleFirst))
                {
                    return $"Sổ kiểm tra dự toán ngân sách năm {_sessionInfo.YearOfWork}";
                }
                return _txtTitleFirst;
            }
            set
            {
                SetProperty(ref _txtTitleFirst, value);
            }
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get
            {
                //if (StringUtils.IsNullOrEmpty(_txtTitleSecond))
                //{
                //    return "(Kèm theo Quyết định số ........., ngày 11/05/2021)";
                //}
                return _txtTitleSecond;
            }

            set
            {
                SetProperty(ref _txtTitleSecond, value);
            }
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
            }
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
        public string PopupTitle => _demandCheckPrintType switch
        {
            DemandCheckPrintType.THE_REPORT_RECEIVES_THE_CHECK_NUMBER => "Báo cáo nhận số kiểm tra",
            DemandCheckPrintType.REPORT_SO_SANH_NHAN_SKT_NAM_TRUOC_NAM_NAY => "Báo cáo so sánh SKT năm trước - năm nay",
            _ => "Báo cáo chi tiết số kiểm tra đơn vị"
        };


        public Visibility RadioVisibility
        {
            get
            {
                if (DemandCheckPrintType.THE_REPORT_RECEIVES_THE_CHECK_NUMBER.Equals(_demandCheckPrintType))
                {
                    return Visibility.Collapsed;
                }

                return Visibility.Visible;
            }
        }

        public bool IsAllowPrint
        {
            get
            {
                if (TypeReportValue.Equals(TypeReport.SUMMARY) && ListDonVi.Count(item => item.IsChecked) > 0 ||
                    TypeReportValue.Equals(TypeReport.NUMBER_CHECK))
                {
                    return true;
                }

                return false;
            }
        }

        private TypeReport _typeReportValue;

        public TypeReport TypeReportValue
        {
            get => _typeReportValue;
            set
            {
                SetProperty(ref _typeReportValue, value);
                OnPropertyChanged(nameof(IsAllowPrint));
            }
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set => SetProperty(ref _paperPrintTypeSelected, value);
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
                    SelectAll(value.Value, ListDonVi);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
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
                OnPropertyChanged(nameof(IsVisibilityRadioButtonNSBD));
                OnPropertyChanged(nameof(IsVisibilityLoaiBaoCao));
            }
        }

        public PrintReportReceiveTheCheckNumberViewModel(
            IMapper mapper,
            INsDonViService nSDonViService,
            IExportService exportService,
            ISessionService sessionService,
            ISktChungTuChiTietService iSktChungTuChiTietService,
            ISktChungTuService iSktChungTuService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _mapper = mapper;
            _nSDonViService = nSDonViService;
            _exportService = exportService;
            _iSktChungTuChiTietService = iSktChungTuChiTietService;
            _sessionService = sessionService;
            _iSktChungTuService = iSktChungTuService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportPrintCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            LoadTitleFirst();
            LoadDonVi();
            LoadCatUnitTypes();
            LoadPaperPrintTypes();
            LoadBudgetSourceTypes();
            TypeReportValue = TypeReport.NUMBER_CHECK;
            LoadVoucherTypes();
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        public void LoadTitleFirst()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
            TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
            TxtTitleThird = _dmChuKy.TieuDe3MoTa;
        }

        public void LoadDonVi()
        {
            if (DemandCheckPrintType.THE_REPORT_RECEIVES_THE_CHECK_NUMBER.Equals(_demandCheckPrintType))
            {
                ListDonVi = new ObservableCollection<CheckBoxItem>();
            }
            else
            {
                ListDonVi = new ObservableCollection<CheckBoxItem>();
                IEnumerable<DonVi> listDonVi = _nSDonViService.FindAll();
                ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
                // Filter
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
                            OnPropertyChanged(nameof(IsAllowPrint));
                        }
                    };
                }
            }
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }

            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchDonVi!.ToLower());
        }

        private void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(n => n.SGiaTri).OrderBy(x => x.SGiaTri).ToList();
            _catUnitTypes = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _catUnitTypes.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            _catUnitTypeSelected = _catUnitTypes.FirstOrDefault();
        }

        private void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Biểu trình ký", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Phụ lục", ValueItem = "2"},
            };

            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            _paperPrintTypeSelected = paperPrintTypes.ElementAt(0);
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

        public void OnExport(ExportType exportType)
        {
            if (DemandCheckPrintType.REPORT_SO_SANH_NHAN_SKT_NAM_TRUOC_NAM_NAY.Equals(_demandCheckPrintType))
            {
                OnExportCompare(exportType);
            }
            else
            {
                OnExportDefault(exportType);
            }
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

        public void OnExportCompare(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var suffix = _catUnitTypeSelected.ValueItem == "1" ? true : false;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var yearOfBudget = _sessionService.Current.YearOfBudget;
                    var loaiNguonNganSach = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNguonNganSach != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                    var budgetSource = _sessionService.Current.Budget;
                    var currentIdDonVi = _sessionService.Current.IdDonVi;
                    var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
                    List<Guid> lstSKTCt = new List<Guid>();

                    SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
                    searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                    searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
                    searchCondition.NguonNganSach = _sessionInfo.Budget;
                    searchCondition.ITrangThai = StatusType.ACTIVE;
                    searchCondition.ILoai = DemandCheckType.CHECK;
                    searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
                    searchCondition.IdDonVi = _sessionInfo.IdDonVi;
                    searchCondition.LoaiChungTu = loaiChungTu;
                    searchCondition.UserName = _sessionInfo.Principal;
                    searchCondition.DonViTinh = donViTinh;
                    if (VoucherType.NSSD_Key.Equals(VoucherTypeSelected.ValueItem))
                    {
                        searchCondition.LoaiBaoCao = 1;
                    }
                    else if (LoaiNSBD.Equals(LoaiNSBD.MHHV))
                    {
                        searchCondition.LoaiBaoCao = 2;
                    }
                    else
                    {
                        searchCondition.LoaiBaoCao = 3;
                    }
                    searchCondition.ILoaiNguonNganSach = int.Parse(BudgetSourceTypeSelected.ValueItem);
                    searchCondition.lstSktChungTuId = lstSKTCt;
                    var listData = _iSktChungTuChiTietService.FindReportSoSanhSKT(searchCondition).ToList();
                    CalculateData(listData);

                    listData = listData.Where(item => Math.Abs(item.SoLieuCot1) >= Double.Epsilon
                                                      || Math.Abs(item.SoLieuCot2) >= Double.Epsilon
                                                      || Math.Abs(item.ChenhLech) >= Double.Epsilon).ToList();


                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    FormatDisplay(listData);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : (itemDanhMuc != null ? itemDanhMuc.SGiaTri : ""));
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : "");
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : "");
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : "");
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : "");
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : "");
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : "");
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : "");
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : "");
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : "");
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("HasChiTiet", false);
                    data.Add("Donvi", string.Empty);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("Count", 10000);
                    var tongSoLieuCot1 = listData.Sum(x => x.SoLieuCot1);
                    var tongSoLieuCot2 = listData.Sum(x => x.SoLieuCot2);
                    var tongSoLieuCot1BangChu = listData.Where(x => !x.BHangCha).Sum(x => x.SoLieuCot1);
                    var tongSoLieuCot2BangChu = listData.Where(x => !x.BHangCha).Sum(x => x.SoLieuCot2);
                    data.Add("TongSoLieuCot1", tongSoLieuCot1BangChu);
                    data.Add("TongSoLieuCot2", tongSoLieuCot2BangChu);
                    data.Add("TongChenhLech", tongSoLieuCot2BangChu - tongSoLieuCot1BangChu);
                    data.Add("Tien", StringUtils.NumberToText((double)tongSoLieuCot2BangChu * donViTinh, true));

                    if (VoucherType.NSSD_Key.Equals(VoucherTypeSelected.ValueItem))
                    {
                        data.Add("TieuDeCot1", "Số kiểm tra năm trước");
                        data.Add("TieuDeCot2", "Số kiểm tra năm nay");
                    }
                    else if (LoaiNSBD.Equals(LoaiNSBD.MHHV))
                    {
                        data.Add("TieuDeCot1", "Mua hàng hiện vật năm trước");
                        data.Add("TieuDeCot2", "Mua hàng hiện vật năm nay");
                    }
                    else
                    {
                        data.Add("TieuDeCot1", "Đặc thù năm trước");
                        data.Add("TieuDeCot2", "Đặc thù năm nay");
                    }

                    string templateFileName;
                    if (exportType == ExportType.EXCEL)
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY_EXCEL);
                    else
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY);

                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<SktChungTuChiTietQuery>(templateFileName, data);
                    e.Result = new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile);
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

        private void FormatDisplay(List<SktChungTuChiTietQuery> lstData)
        {

            foreach (var item in lstData.Where(x => !string.IsNullOrEmpty(x.SK)))
            {
                var parent = lstData.FirstOrDefault(x => x.IIdMlskt == item.IIdParentMlskt);
                if (parent != null && parent.SL != string.Empty)
                {
                    //item.sM = string.Empty;
                    item.SL = string.Empty;
                    //item.sK = string.Empty;
                    //item.sLNS = string.Empty;

                }
            }
        }

        public void OnExportDefault(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var suffix = _catUnitTypeSelected.ValueItem == "1" ? true : false;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var yearOfBudget = _sessionService.Current.YearOfBudget;
                    var loaiNguonNganSach = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNguonNganSach != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                    var budgetSource = _sessionService.Current.Budget;
                    var currentIdDonVi = _sessionService.Current.IdDonVi;
                    var iLoai = DemandCheckType.CHECK;
                    var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
                    IEnumerable<NsSktChungTu> listChungTu;
                    listChungTu = _iSktChungTuService
                        .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, null, _sessionService.Current.Principal, "sp_skt_nhan_so_kiem_tra").ToList();
                    listChungTu = listChungTu.Where(x => loaiNguonNganSach == 0 || x.ILoaiNguonNganSach == loaiNguonNganSach).ToList();
                    List<NsSktChungTuChiTietModel> listData = new List<NsSktChungTuChiTietModel>();
                    List<Guid> lstSKTCt = new List<Guid>();
                    if (listChungTu != null)
                    {
                        foreach (var item in listChungTu)
                        {
                            lstSKTCt.Add(item.Id);
                        }
                    }

                    if (listChungTu != null && listChungTu.Count() > 0)
                    {
                        var sktChungTu = listChungTu.First();
                        SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
                        searchCondition.NguonNganSach = _sessionInfo.Budget;
                        searchCondition.ITrangThai = StatusType.ACTIVE;
                        searchCondition.SktChungTuId = sktChungTu.Id;
                        searchCondition.ILoai = sktChungTu.ILoai;
                        searchCondition.IdDonVi = sktChungTu.IIdMaDonVi;
                        searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
                        searchCondition.LoaiChungTu = loaiChungTu;
                        searchCondition.UserName = _sessionInfo.Principal;
                        searchCondition.ILoaiNguonNganSach = int.Parse(BudgetSourceTypeSelected.ValueItem);
                        searchCondition.lstSktChungTuId = lstSKTCt;
                        var temp = _iSktChungTuChiTietService.FindByConditionForChildUnit_1(searchCondition);
                        listData = _mapper.Map<ObservableCollection<NsSktChungTuChiTietModel>>(temp).ToList();
                        foreach (var dt in listData)
                        {
                            dt.FTuChi = dt.FTuChi / donViTinh;
                            dt.FMuaHangCapHienVat = dt.FMuaHangCapHienVat / donViTinh;
                            dt.FPhanCap = dt.FPhanCap / donViTinh;
                        }
                        var parentList = listData.Where(entity => entity.IsHangCha && Guid.Empty.Equals(entity.IdParent)).ToList();
                        parentList.ForEach(parent => parent.Level = 1);
                        listData.Where(entity => entity.IsHangCha && parentList.Any(parent => parent.IIdMlskt.Equals(entity.IdParent)))
                            .ToList().ForEach(subParent => subParent.Level = 2);
                        CalculateData(listData);
                    }
                    listData = listData.Where(item => Math.Abs(item.FTuChi) >= Double.Epsilon
                                                      || Math.Abs(item.FMuaHangCapHienVat) >= Double.Epsilon
                                                      || Math.Abs(item.FPhanCap) >= Double.Epsilon).ToList();
                    double SumTotal = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
                    double SumTotalMuaHangHienVat = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
                    double SumTotalDacThu = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
                    double SumTotalNSBD = listData.Where(item => item.IdParent == Guid.Empty)
                        .Sum(x => x.FMuaHangCapHienVat + x.FPhanCap);
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : "");
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : "");
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : "");
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : "");
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : "");
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : "");
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : "");
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : "");
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : "");
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("SumTotal", SumTotal);
                    data.Add("SumTotalMuaHangHienVat", SumTotalMuaHangHienVat);
                    data.Add("SumTotalDacThu", SumTotalDacThu);
                    data.Add("SumTotalText", StringUtils.NumberToText(SumTotal * donViTinh));
                    data.Add("SumTotalTextNSBD", StringUtils.NumberToText(SumTotalNSBD * donViTinh));

                    string templateFileName;
                    if (loaiChungTu == 1)
                    {
                        if (_paperPrintTypeSelected.ValueItem == "1")
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_NHANSOKIEMTRA_TRINHKY_NSSD);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_NHANSOKIEMTRA_PHULUC_NSSD);
                        }
                    }
                    else
                    {
                        if (_paperPrintTypeSelected.ValueItem == "1")
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_NHANSOKIEMTRA_TRINHKY_NSBD);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_NHANSOKIEMTRA_PHULUC_NSBD);
                        }
                    }

                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NsSktChungTuChiTietModel>(templateFileName, data);
                    e.Result = new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile);
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

        private void CalculateData<T>(List<T> itemsProp)
        {
            if (itemsProp is List<NsSktChungTuChiTietModel> items)
            {
                items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FPhanCap = 0;
                    x.SoNhuCau = 0;
                    x.SoNhuCauMHHV = 0;
                    x.SoNhuCauDT = 0;
                    return x;
                }).ToList();
                var temp = items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter);
                foreach (var item in temp)
                {
                    CalculateParent(item.IdParent, item, items);
                }
            }
            else if (itemsProp is List<SktChungTuChiTietQuery> itemQueries)
            {
                itemQueries.Where(x => x.BHangCha)
                        .Select(x =>
                        {
                            x.SoLieuCot1 = 0;
                            x.SoLieuCot2 = 0;
                            return x;
                        }).ToList();
                var temp = itemQueries.Where(x => !x.BHangCha);
                foreach (var item in temp)
                {
                    CalculateParent(item.IIdParentMlskt, item, itemQueries);
                }
            }
        }

        private void CalculateParent<T>(Guid idParent, T item, List<T> items)
        {
            if (item is NsSktChungTuChiTietModel item1 && items is List<NsSktChungTuChiTietModel> items1)
            {
                var model = items1.FirstOrDefault(x => x.IIdMlskt == idParent);
                if (model == null) return;
                model.FTuChi += item1.FTuChi;
                model.FHuyDongTonKho += item1.FHuyDongTonKho;
                model.FMuaHangCapHienVat += item1.FMuaHangCapHienVat;
                model.FPhanCap += item1.FPhanCap;
                model.SoNhuCau += item1.SoNhuCau;
                model.SoNhuCauMHHV += item1.SoNhuCauMHHV;
                model.SoNhuCauDT += item1.SoNhuCauDT;
                CalculateParent(model.IdParent, item1, items1);
            }
            else if (item is SktChungTuChiTietQuery item2 && items is List<SktChungTuChiTietQuery> items2)
            {
                var model = items2.FirstOrDefault(x => x.IIdMlskt == idParent);
                if (model == null) return;
                model.SoLieuCot1 += item2.SoLieuCot1;
                model.SoLieuCot2 += item2.SoLieuCot2;
                CalculateParent(model.IIdParentMlskt, item2, items2);
            }

        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA;
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
    }
}