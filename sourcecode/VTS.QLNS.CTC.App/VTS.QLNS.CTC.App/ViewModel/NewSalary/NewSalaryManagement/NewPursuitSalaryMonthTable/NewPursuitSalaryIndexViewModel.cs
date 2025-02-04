using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewImportSalary;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using Newtonsoft.Json.Linq;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewPursuitSalaryMonthTable
{
    public class NewPursuitSalaryIndexViewModel : GridViewModelBase<TlDSCapNhapBangLuongNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private ITlDmPhuCapNq104Service _iTlDmPhuCapService;
        private readonly IExportService _exportService;
        private readonly ITlDmCanBoNq104Service _tlDmCanboService;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private SessionInfo _sessionInfo;
        private ICollectionView _dtDanhSachBangLuongView;
        private readonly ITlBaoCaoNq104Service _tlBaoCaoService;
        private readonly ISysAuditLogService _sysAuditLogService;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_PURSUIT_SALARY_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Bảng lương tháng truy lĩnh";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagement.NewPursuitSalaryMonthTable.NewPursuitSalaryMonthTableIndex);
        public override PackIconKind IconKind => PackIconKind.ClipboardListOutline;
        public override string Title => "Bảng lương tháng truy lĩnh";
        public override string Description => "Bảng lương tháng truy lĩnh của các đơn vị";

        public NewPursuitSalaryDialogViewModel PursuitSalaryAddDialogViewModel { get; }
        public NewPursuitSalaryDetailViewModel PursuitSalaryDetailViewModel { get; }
        public NewImportSalaryViewModel ImportSalaryViewModel { get; }
        public NewReportDialogViewModel ListReportDialogViewModel { get; }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set
            {
                SetProperty(ref _monthSelected, value);
                LoadData();

                //if ( && _dtDanhSachBangLuongView != null)
                //{
                //}
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set
            {
                if (SetProperty(ref _yearSelected, value) && _dtDanhSachBangLuongView != null && _yearSelected != null)
                {
                    LoadData();
                }
            }
        }

        private string _searchBangLuong;
        public string SearchBangLuong
        {
            get => _searchBangLuong;
            set => SetProperty(ref _searchBangLuong, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (SelectedDonViItems.MaDonVi == null)
                    {
                        SelectAll(value.Value, Items.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(YearSelected.ValueItem)));
                    }
                    else
                    {
                        SelectAll(value.Value, Items.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.MaDonVi == SelectedDonViItems.MaDonVi && x.Nam == int.Parse(YearSelected.ValueItem)));
                    }
                    OnPropertyChanged();
                }
            }
        }

        private void SelectAll(bool select, IEnumerable<TlDSCapNhapBangLuongNq104Model> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        private ObservableCollection<TlDmDonViNq104Model> _donViItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViNq104Model _selectedDonViItems;
        public TlDmDonViNq104Model SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && _dtDanhSachBangLuongView != null)
                {
                    _dtDanhSachBangLuongView.Refresh();
                }
            }
        }

        public bool IsEnableExportData => Items != null && Items.Where(x => x.Selected).Count() > 0;

        public RelayCommand ExportBangLuongCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand CapNhatCommand { get; }

        public NewPursuitSalaryIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            NewPursuitSalaryDialogViewModel pursuitSalaryAddDialogViewModel,
            NewPursuitSalaryDetailViewModel pursuitSalaryDetailViewModel,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlDmDonViNq104Service tlDmDonViService,
            INsDonViService nsDonViService,
            ITlBaoCaoNq104Service tlBaoCaoService,
            ITlDmPhuCapNq104Service iTlDmPhuCapService,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service,
            IExportService exportService,
            ITlDmCanBoNq104Service tlDmCanboService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ISysAuditLogService sysAuditLogService,
            NewImportSalaryViewModel importSalaryViewModel,
            NewReportDialogViewModel listReportDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmDonViService = tlDmDonViService;
            _nsDonViService = nsDonViService;
            _iTlDmPhuCapService = iTlDmPhuCapService;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
            _exportService = exportService;
            _tlDmCanboService = tlDmCanboService;
            _tlBaoCaoService = tlBaoCaoService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _sysAuditLogService = sysAuditLogService;

            ImportSalaryViewModel = importSalaryViewModel;
            PursuitSalaryAddDialogViewModel = pursuitSalaryAddDialogViewModel;
            PursuitSalaryDetailViewModel = pursuitSalaryDetailViewModel;
            ListReportDialogViewModel = listReportDialogViewModel;

            SearchCommand = new RelayCommand(o => _dtDanhSachBangLuongView.Refresh());
            ExportBangLuongCommand = new RelayCommand(obj => OnExportBangLuong());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintCommand = new RelayCommand(o => OnPrintBangLuong());
            CapNhatCommand = new RelayCommand(obj => OnCapNhat());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadYear();
            LoadMonths();
            LoadData();
            LoadDonViData();
        }

        private void LoadData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    var predicate = PredicateBuilder.True<TlDsCapNhapBangLuongNq104>();
                    predicate = predicate.And(item => CachTinhLuong.CACH5.Equals(item.MaCachTl));
                    // predicate = predicate.And(item => item.Thang == int.Parse(MonthSelected.ValueItem));
                    predicate = predicate.And(item => item.Nam == int.Parse(YearSelected.ValueItem));

                    var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                    if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                    {
                        //IEnumerable<TlDsCapNhapBangLuongNq104> data = _tlDsCapNhapBangLuongService.FindByCondition(predicate).OrderBy(x => x.MaCbo);
                        IEnumerable<TlDsCapNhapBangLuongNq104> data = _tlDsCapNhapBangLuongService.FindHaveDataByCondition(CachTinhLuong.CACH5, string.Empty, int.Parse(MonthSelected.ValueItem ?? "0"), int.Parse(YearSelected.ValueItem)).OrderBy(x => x.MaCbo);
                        e.Result = data;
                    }
                    else
                    {
                        //IEnumerable<TlDsCapNhapBangLuongNq104> data = _tlDsCapNhapBangLuongService.FindByCondition(predicate).Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.MaCbo)).OrderBy(x => x.MaCbo);
                        IEnumerable<TlDsCapNhapBangLuongNq104> data = _tlDsCapNhapBangLuongService.FindHaveDataByCondition(CachTinhLuong.CACH5, _sessionService.Current.IdsPhanHoQuanLy, int.Parse(MonthSelected.ValueItem ?? "0"), int.Parse(YearSelected.ValueItem)).OrderBy(x => x.MaCbo);
                        e.Result = data;
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<TlDSCapNhapBangLuongNq104Model>>(e.Result);

                        var listDonVi = _tlDmDonViService.FindAllDonViNq104().ToDictionary(n => n.MaDonVi, n => n.TenDonVi);
                        foreach (var item in Items)
                        {
                            // var donVi = _tlDmDonViService.FindByCondition(x => x.MaDonVi.Equals(item.MaCbo)).FirstOrDefault();
                            if (!string.IsNullOrEmpty(listDonVi[item.MaCbo]))
                            {
                                //item.TenDonVi = donVi.TenDonVi;
                                item.TenDonVi = listDonVi[item.MaCbo];
                            }
                            item.PropertyChanged += (sender, args) =>
                            {
                                if (args.PropertyName == nameof(TlDSCapNhapBangLuongNq104Model.Selected))
                                {
                                    OnPropertyChanged(nameof(IsAllItemsSelected));
                                    OnPropertyChanged(nameof(IsEnableExportData));
                                }
                            };
                        }
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        _dtDanhSachBangLuongView = CollectionViewSource.GetDefaultView(Items);
                        _dtDanhSachBangLuongView.Filter = BangLuongFilter;
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);

            var lstDonVi = new List<TlDmDonViNq104Model>();

            TlDmDonViNq104Model tlDmDonViModel = new TlDmDonViNq104Model();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            DonViItems = new ObservableCollection<TlDmDonViNq104Model>(lstDonVi);
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            ComboboxItem tatCa = new ComboboxItem("-- Tất cả --", null);
            _months.Add(tatCa);
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        protected override void OnAdd()
        {
            TlDSCapNhapBangLuongNq104Model danhSachBangLuongModel = new TlDSCapNhapBangLuongNq104Model();

            danhSachBangLuongModel.Nam = int.Parse(YearSelected.ValueItem);
            if (MonthSelected.ValueItem != null)
            {
                danhSachBangLuongModel.Thang = int.Parse(MonthSelected.ValueItem);
                var firstDay = new DateTime((int)danhSachBangLuongModel.Nam, int.Parse(MonthSelected.ValueItem), 1);
                danhSachBangLuongModel.TuNgay = firstDay;
                danhSachBangLuongModel.DenNgay = firstDay.AddMonths(1).AddDays(-1);
            }
            else
            {
                danhSachBangLuongModel.Thang = _sessionService.Current.Month;
                var firstDay = new DateTime((int)danhSachBangLuongModel.Nam, _sessionService.Current.Month, 1);
                danhSachBangLuongModel.TuNgay = firstDay;
                danhSachBangLuongModel.DenNgay = firstDay.AddMonths(1).AddDays(-1);
            }
            PursuitSalaryAddDialogViewModel.Model = danhSachBangLuongModel;
            PursuitSalaryAddDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new View.NewSalary.NewSalaryManagement.NewPursuitSalaryMonthTable.NewPursuitSalaryDiallog
            {
                DataContext = PursuitSalaryAddDialogViewModel
            };
            PursuitSalaryAddDialogViewModel.Init();
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnDelete()
        {
            try
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgConfirmDeleteBangLuong), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                List<TlDSCapNhapBangLuongNq104Model> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    if (SelectedItem != null && tlDsCapNhapBangLuongModels.Count() == 0)
                    {
                        if (dialogResult == DialogResult.Yes)
                        {
                            _tlDsCapNhapBangLuongService.DeleteBangLuong((int)SelectedItem.Thang, (int)SelectedItem.Nam, SelectedItem.MaCbo, "CACH5");
                        }
                    }
                    else if (tlDsCapNhapBangLuongModels.Count() != 0)
                    {
                        if (dialogResult == DialogResult.Yes)
                        {
                            var lstId = tlDsCapNhapBangLuongModels.Select(x => x.Id.ToString()).ToList();
                            string idXoa = string.Join(",", lstId);
                            _tlDsCapNhapBangLuongService.DeleteBangLuong((int)SelectedItem.Thang, (int)SelectedItem.Nam, string.Join(",", tlDsCapNhapBangLuongModels.Select(x => x.MaCbo)), "CACH5");
                        }
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        _sysAuditLogService.WriteLog(Resources.ApplicationName, "Xóa bảng lương tháng truy lĩnh", (int)TypeExecute.Delete, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
                        MessageBoxHelper.Info("Xóa dữ liệu thành công");
                        OnRefresh();
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

        private void OnPrintBangLuong()
        {
            TlBaoCaoModel tlBaoCaoModel = new TlBaoCaoModel();
            var lstBaoCao = _tlBaoCaoService.FindAll();
            var baoCao = lstBaoCao.FirstOrDefault(x => x.MaBaoCao.Equals("1.13"));
            if (baoCao != null)
            {
                tlBaoCaoModel = _mapper.Map<TlBaoCaoModel>(baoCao);
            }
            tlBaoCaoModel.SelectedMonth = int.Parse(MonthSelected.ValueItem);
            tlBaoCaoModel.SelectedYear = int.Parse(YearSelected.ValueItem);

            ListReportDialogViewModel.LstMaDonViIndexViewSelected = new List<string>();
            if (Items.Any(n => n.Selected))
            {
                ListReportDialogViewModel.LstMaDonViIndexViewSelected
                    = Items.Where(n => n.Selected).Select(n => n.MaCbo).Distinct().ToList();
                tlBaoCaoModel.SelectedMonth = (int)(Items.FirstOrDefault(n => n.Selected).Thang ?? 0);
                tlBaoCaoModel.SelectedYear = (int)(Items.FirstOrDefault(n => n.Selected).Nam ?? 0);
            }
            ListReportDialogViewModel.Model = tlBaoCaoModel;
            ListReportDialogViewModel.LoaiBaoCao = BaoCaoLuong.BTLTL;
            ListReportDialogViewModel.Init();
            ListReportDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenBangLuongDetail((TlDSCapNhapBangLuongNq104Model)obj);
        }

        private void OnOpenBangLuongDetail(TlDSCapNhapBangLuongNq104Model tlDSCapNhapBangLuongModel)
        {
            try
            {
                if (tlDSCapNhapBangLuongModel == null)
                    return;
                PursuitSalaryDetailViewModel.Model = tlDSCapNhapBangLuongModel;
                PursuitSalaryDetailViewModel.ThoiGian = string.Format("Tháng {0} Năm {1}", tlDSCapNhapBangLuongModel.Thang, tlDSCapNhapBangLuongModel.Nam);
                PursuitSalaryDetailViewModel.Init();
                var view = new View.NewSalary.NewSalaryManagement.NewPursuitSalaryMonthTable.NewPursuitSalaryDetail() { DataContext = PursuitSalaryDetailViewModel };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool BangLuongFilter(object obj)
        {
            var result = true;
            var item = (TlDSCapNhapBangLuongNq104Model)obj;

            if (MonthSelected != null && MonthSelected.ValueItem != null)
            {
                result &= item.Thang == int.Parse(MonthSelected.ValueItem);
            }
            if (_searchBangLuong != null)
            {
                result &= item.TenDsCnbluong.ToLower().Contains(_searchBangLuong.ToLower());
            }
            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result &= item.MaCbo == SelectedDonViItems.MaDonVi;
            }
            return result;
        }

        private void OnExportBangLuong()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_IMPORT_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<TlDSCapNhapBangLuongNq104Model> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();
                    var lstPhuCaps = _iTlDmPhuCapService.FindAll();
                    var lstPhuCapsModel = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(lstPhuCaps).ToList();
                    ExportChiTietBangLuongNq104Model lstHeader = new ExportChiTietBangLuongNq104Model();
                    lstHeader.LstPhuCap = lstPhuCapsModel;
                    List<ExportChiTietBangLuongNq104Model> lstHeaderItems = new List<ExportChiTietBangLuongNq104Model>();
                    lstHeaderItems.Add(lstHeader);
                    foreach (var bangLuongIndex in tlDsCapNhapBangLuongModels)
                    {
                        var predicateLT = PredicateBuilder.True<TlBangLuongThangNq104>();
                        predicateLT = predicateLT.And(x => x.Parent.Equals(bangLuongIndex.Id));
                        var bangLuongThang = _tlBangLuongThangService.FindByCondition(predicateLT).OrderBy(x => x.MaCbo).ToList();
                        var bangLuongThangModel = _mapper.Map<List<TlBangLuongThangNq104Model>>(bangLuongThang);
                        ExportChiTietBangLuongNq104Model bangLuongDoc = new ExportChiTietBangLuongNq104Model();
                        bangLuongDoc.ListGiaTriDoc = bangLuongThangModel;
                        List<ExportChiTietBangLuongNq104Model> bangLuongDocItems = new List<ExportChiTietBangLuongNq104Model>();
                        bangLuongDocItems.Add(bangLuongDoc);
                        List<TlDSCapNhapBangLuongNq104Model> bangLuongIndexItems = new List<TlDSCapNhapBangLuongNq104Model>();
                        bangLuongIndexItems.Add(bangLuongIndex);
                        var lstCanBo = bangLuongThangModel.Select(item => item.MaCbo).Distinct().ToList();
                        List<ExportChiTietBangLuongNq104Model> bangLuongViewItems = new List<ExportChiTietBangLuongNq104Model>();
                        int i = 1;
                        var donVi = _tlDmDonViService.FindByMaDonVi(bangLuongIndex.MaCbo);
                        foreach (var maCanBo in lstCanBo)
                        {
                            ExportChiTietBangLuongNq104Model itemRpt = new ExportChiTietBangLuongNq104Model();
                            var canBo = _tlDmCanboService.FindByMaCanBo(maCanBo);
                            itemRpt.iStt = i++;
                            itemRpt.iThang = bangLuongIndex.Thang.ToString();
                            itemRpt.iNam = bangLuongIndex.Nam.ToString();
                            itemRpt.sMaCanbo = canBo != null ? canBo.MaCanBo : "";
                            itemRpt.sTenCbo = canBo != null ? canBo.TenCanBo : "";
                            itemRpt.sTenDonVi = donVi != null ? donVi.TenDonVi : "";
                            itemRpt.ListGiaTri = new List<TlBangLuongThangNq104Model>();
                            foreach (var pc in lstPhuCapsModel)
                            {
                                TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                giaTri.MaPhuCap = pc.MaPhuCap;
                                var phuCap = bangLuongThangModel.FirstOrDefault(item => item.MaCbo.Equals(maCanBo) && item.MaPhuCap.Equals(pc.MaPhuCap));
                                giaTri.GiaTri = phuCap != null ? phuCap.GiaTri ?? 0 : 0;
                                itemRpt.ListGiaTri.Add(giaTri);
                            }
                            bangLuongViewItems.Add(itemRpt);
                        }

                        // lấy dm đơn vị
                        var predicateDonVi = PredicateBuilder.True<TlDmDonViNq104>();
                        predicateDonVi = predicateDonVi.And(x => x.MaDonVi.Equals(bangLuongIndex.MaCbo));
                        var donViItems = _tlDmDonViService.FindByCondition(predicateDonVi).ToList();

                        // lay dm cán bộ
                        var predicateCanBo = PredicateBuilder.True<TlDmCanBoNq104>();
                        predicateCanBo = predicateCanBo.And(x => lstCanBo.Contains(x.MaCanBo));
                        var lstCanBoItems = _tlDmCanboService.FindByCondition(predicateCanBo).ToList();
                        var canBoItems = _mapper.Map<List<CadresNq104Model>>(lstCanBoItems);

                        // lay phu cap can bo
                        var predicateCanBoPhuCap = PredicateBuilder.True<TlCanBoPhuCapNq104>();
                        predicateCanBoPhuCap = predicateCanBoPhuCap.And(x => lstCanBo.Contains(x.MaCbo));
                        var canBoPhuCapItems = _tlCanBoPhuCapService.FindAll(predicateCanBoPhuCap).ToList();

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("BangLuongIndexItems", bangLuongIndexItems);
                        data.Add("BangLuongDocItems", bangLuongDocItems);
                        data.Add("LstHeaderItems", lstHeaderItems);
                        data.Add("BangLuongViewItems", bangLuongViewItems);
                        data.Add("DonViItems", donViItems);
                        data.Add("CanBoItems", canBoItems);
                        data.Add("CanBoPhuCapItems", canBoPhuCapItems);

                        fileNamePrefix = string.Format("Bang_Luong_Chi_Tiet_Import_{0}_{1}_{2}", donVi != null ? donVi.TenDonVi : "", MonthSelected.ValueItem, DateTime.Now.Year);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ExportChiTietBangLuongNq104Model, TlDSCapNhapBangLuongNq104Model, TlDmDonViNq104, CadresNq104Model, TlCanBoPhuCapNq104>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
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

        private void OnImportData()
        {
            ImportSalaryViewModel.Init();
            ImportSalaryViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnOpenBangLuongDetail((TlDSCapNhapBangLuongNq104Model)obj);
                IsAllItemsSelected = false;
            };
            ImportSalaryViewModel.ShowDialog();
        }

        private void OnCapNhat()
        {
            try
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Đ/c muốn cập nhật lại bảng lương tháng truy lĩnh không?", Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                List<TlDSCapNhapBangLuongNq104Model> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    if (!tlDsCapNhapBangLuongModels.IsEmpty())
                    {
                        if (dialogResult == DialogResult.Yes)
                        {
                            var lstId = tlDsCapNhapBangLuongModels.Select(x => x.Id.ToString()).ToList();
                            string idXoa = string.Join(",", lstId);
                            string maDonVi = string.Join(",", tlDsCapNhapBangLuongModels.Select(x => x.MaCbo));
                            int thang = int.Parse(MonthSelected.ValueItem);
                            int nam = int.Parse(YearSelected.ValueItem);

                            tlDsCapNhapBangLuongModels.Select(x =>
                            {
                                if (!x.TenDsCnbluong.Contains("Cập nhật"))
                                {
                                    x.TenDsCnbluong = x.TenDsCnbluong + string.Format(" - Cập nhật");
                                }
                                x.NgayTaoBL = DateTime.Now;
                                return x;
                            }).ToList();

                            var lstAdd = _mapper.Map<List<TlDsCapNhapBangLuongNq104>>(tlDsCapNhapBangLuongModels);

                            var lstChiTietAdd = new List<TlBangLuongThangNq104>();

                            var dicBangLuong = tlDsCapNhapBangLuongModels.GroupBy(x => x.Thang).ToDictionary(x => x.Key, x => x.ToList());
                            foreach (var item in dicBangLuong)
                            {
                                lstChiTietAdd.AddRange(CapNhat(item.Value));
                            }

                            _tlDsCapNhapBangLuongService.DeleteBangLuong(thang, nam, maDonVi, CachTinhLuong.CACH5);
                            _tlDsCapNhapBangLuongService.CapNhatBangLuong(idXoa, lstAdd, lstChiTietAdd);
                        }
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        if (dialogResult == DialogResult.Yes)
                        {
                            _sysAuditLogService.WriteLog(Resources.ApplicationName, "Cập nhật bảng lương tháng truy lĩnh", (int)TypeExecute.Delete, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
                            MessageBoxHelper.Info("Cập nhật dữ liệu thành công");
                            OnRefresh();
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

        public List<TlBangLuongThangNq104> CapNhat(List<TlDSCapNhapBangLuongNq104Model> listDsCapNhapBangLuong)
        {
            try
            {
                List<TlDsCapNhapBangLuongNq104> entities = new List<TlDsCapNhapBangLuongNq104>();
                List<TlBangLuongThangNq104> detailEntities = new List<TlBangLuongThangNq104>();
                int thang = (int)listDsCapNhapBangLuong.FirstOrDefault().Thang;
                int nam = (int)listDsCapNhapBangLuong.FirstOrDefault().Nam;
                var dayInMonth = DateTime.DaysInMonth(nam, thang);

                // Tạo dữ liệu bảng lương
                string maDonVi = string.Join(",", listDsCapNhapBangLuong.Select(x => x.MaCbo).ToArray());

                _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(thang, nam);
                var data = _tlBangLuongThangService.GetDataInsert(thang, nam, maDonVi, "CACH5", dayInMonth);
                var lstCongChuan = _tlCanBoPhuCapBridgeNq104Service.FindAll(x => x.MaPhuCap.Equals(PhuCap.CONGCHUAN_SN));
                var res = data.AsParallel().GroupBy(x => x.MaCbo).Select(x => x.ToList());
                Parallel.ForEach(res, lstPhuCap =>
                {
                    var parent = listDsCapNhapBangLuong.FirstOrDefault(x => x.MaCbo.Equals(lstPhuCap.FirstOrDefault().MaDonVi));
                    if (parent != null)
                    {
                        _ = lstPhuCap.Select(x =>
                        {
                            x.Parent = parent.Id;
                            return x;
                        }).ToList();
                        var pcCongChuan = lstCongChuan.FirstOrDefault();

                        //Tính lương
                        foreach (var phuCap in lstPhuCap.Where(x => !x.CongThuc.IsEmpty()))
                        {
                            TinhLuong(lstPhuCap, phuCap, pcCongChuan);
                        }
                    }
                });

                var dataSave = data.GroupBy(x => x.MaCbo).Select(y =>
                {
                    var phuCapJson = new JObject();
                    foreach (var item in y)
                    {
                        phuCapJson[item.MaPhuCap] = item.GiaTri;
                    }
                    return new
                    {
                        First = y.FirstOrDefault(),
                        Data = CompressExtension.CompressToBase64(phuCapJson.ToString()),
                    };
                });

                dataSave.ForAll(x => x.First.Data = x.Data);

                _mapper.Map(dataSave.Select(x => x.First), detailEntities);
                //_mapper.Map(data, detailEntities);

                return detailEntities;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new List<TlBangLuongThangNq104>();
            }
        }

        private void TinhLuong(List<TlBangLuongThangNq104Query> items, TlBangLuongThangNq104Query currentItem, TlCanBoPhuCapBridgeNq104 pcCongChuan)
        {
            try
            {
                if (currentItem.IsCalculated) return;

                decimal giaTri = 0;
                var data = new Dictionary<string, object>();
                List<string> lstPhuCap = currentItem.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var maPhuCap in lstPhuCap)
                {
                    decimal giaTriPhuCap = 0;
                    var phuCap = items.FirstOrDefault(x => x.MaPhuCap.Equals(maPhuCap));
                    if (phuCap != null)
                    {
                        if (!phuCap.CongThuc.IsEmpty() && !phuCap.IsCalculated)
                        {
                            // Nếu phụ cấp có công thức và chưa được tính toán => Đệ qui
                            TinhLuong(items, phuCap, pcCongChuan);
                        }

                        if (phuCap.CongThuc.IsEmpty() && phuCap.HuongPcSn != null && pcCongChuan != null && pcCongChuan.GiaTri > 0 && phuCap.IsTinhTheoSoCongChuan == true)
                        {
                            // Không có công thức thì tính giá trị dựa theo số ngày hưởng và công chuẩn
                            giaTriPhuCap = (decimal)(phuCap.GiaTri * phuCap.HuongPcSn / pcCongChuan.GiaTri);
                        }
                        else if (phuCap.CongThuc.IsEmpty() && phuCap.HuongPcSn != null && pcCongChuan != null && pcCongChuan.GiaTri > 0 && phuCap.IsTinhTheoSoCongChuan == false)
                        {
                            // Không có công thức thì tính giá trị dựa theo số ngày hưởng và số giờ
                            giaTriPhuCap = (decimal)(phuCap.GiaTri * phuCap.HuongPcSn);
                        }
                        else
                        {
                            // Trường hợp còn lại lấy đúng giá trị của phụ cấp
                            giaTriPhuCap = phuCap.GiaTri;
                        }
                    }
                    data.Add(maPhuCap, giaTriPhuCap);
                }

                if (!data.IsEmpty())
                {
                    try
                    {
                        var val = EvalExtensions.Execute(currentItem.CongThuc, data);
                        giaTri = decimal.Parse(val.ToString());
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);
                        _logger.InfoFormat("Công thức: {0}. Data: {1}", ObjectCopier.ToJsonString(currentItem), ObjectCopier.ToJsonString(data));
                    }
                }
                currentItem.GiaTri = Math.Round(giaTri);
                currentItem.IsCalculated = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
