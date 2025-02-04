using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
using VTS.QLNS.CTC.App.ViewModel.Salary.ImportSalary;
using VTS.QLNS.CTC.App.ViewModel.Salary.Report.ListReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.SalaryTableMonth
{
    public class SalaryTableMonthIndexViewModel : GridViewModelBase<TlDSCapNhapBangLuongModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dtCapNhapBangLuongView;
        private ICollectionView _dtBangLuongTongHopView;
        private ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private ITlBangLuongThangService _tlBangLuongThangService;
        private ITlDmPhuCapService _iTlDmPhuCapService;
        private readonly IExportService _exportService;
        private readonly ITlDmCanBoService _tlDmCanboService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlBaoCaoService _tlBaoCaoService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlDmCachTinhLuongChuanService _tlDmCachTinhLuongChuanService;
        private readonly ITlDmCapBacService _dmCapBacService;

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_SALARY_TABLE_MONTH_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Bảng lương tháng";
        public override Type ContentType => typeof(View.Salary.SalaryManagement.SalaryTableMonth.SalaryTableMonthIndex);
        public override PackIconKind IconKind => PackIconKind.ClipboardListOutline;
        public override string Title => "Bảng lương tháng";
        public override string Description => "Danh sách bảng lương tháng (Tổng số bản ghi: " + Items.Count() + ")";
        public static List<string> lstPhuCapNuocNgoai = new List<string>() { PhuCap.LHT_TT, PhuCap.PCCV_TT, PhuCap.PCTN_TT, PhuCap.PCTNVK_TT, PhuCap.PCCOV_TT };
        public static List<string> lstPhuCapLdTamTuyen = new List<string>() { PhuCap.BHXHDV_TT, PhuCap.BHYTCN_TT, PhuCap.BHTNCN_TT, PhuCap.BHYTDV_TT, PhuCap.BHTNDV_TT, PhuCap.BHXHCN_TT };
        public static List<string> lstPhuCapTamGiamTamGiu = new List<string>() { PhuCap.LHT_TT, PhuCap.PCCV_TT, PhuCap.PCTN_TT, PhuCap.PCCOV_TT, PhuCap.BHXHDV_TT, PhuCap.BHXHCN_TT };
        public static List<string> lstPhuCapTamGiamTamGiuDefault = new List<string>() { PhuCap.PCCOV_TT, PhuCap.BHXHDV_TT, PhuCap.BHXHCN_TT };

        public SalaryTableMonthDialogViewModel SalaryTableMonthDialogViewModel { get; }
        public SalaryTableMonthDetailViewModel SalaryTableMonthDetailViewModel { get; }
        public ImportSalaryViewModel ImportSalaryViewModel { get; }
        public ReportDialogViewModel ListReportDialogViewModel { get; }
        public SalaryTableMonthAggregateDialogViewModel SalaryTableMonthAggregateDialogViewModel { get; }

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
                if (SetProperty(ref _monthSelected, value) && _dtCapNhapBangLuongView != null)
                {
                    LoadData();
                }
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
                if (SetProperty(ref _yearSelected, value) && _dtCapNhapBangLuongView != null && _yearSelected != null)
                {
                    LoadData();
                }
            }
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

        private void SelectAll(bool select, IEnumerable<TlDSCapNhapBangLuongModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        public bool IsEnableExportData =>
            Items != null && Items.Where(x => x.Selected).Count() > 0 && Items.Where(x => x.Selected).All(x => true.Equals(x.Status));

        public RelayCommand ExportBangLuongCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        private List<ComboboxItem> _trangThaiBangLuong;
        public List<ComboboxItem> TrangThaiBangLuong
        {
            get => _trangThaiBangLuong;
            set => SetProperty(ref _trangThaiBangLuong, value);
        }

        private Visibility _unlockVisibility;
        public Visibility UnlockVisibility
        {
            get => _unlockVisibility;
            set => SetProperty(ref _unlockVisibility, value);
        }

        private Visibility _lockVisibility;
        public Visibility LockVisibility
        {
            get => _lockVisibility;
            set => SetProperty(ref _lockVisibility, value);
        }

        private string _searchBangLuong;
        public string SearchBangLuong
        {
            get => _searchBangLuong;
            set => SetProperty(ref _searchBangLuong, value);
        }

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && _dtCapNhapBangLuongView != null)
                {
                    _dtCapNhapBangLuongView.Refresh();

                }
                if (_dtBangLuongTongHopView != null)
                {
                    _dtBangLuongTongHopView.Refresh();
                }
            }
        }

        private ObservableCollection<TlDSCapNhapBangLuongModel> _itemBangLuongTongHop;
        public ObservableCollection<TlDSCapNhapBangLuongModel> ItemsBangLuongTongHop
        {
            get => _itemBangLuongTongHop;
            set => SetProperty(ref _itemBangLuongTongHop, value);
        }

        private TlDSCapNhapBangLuongModel _selectedItemTongHop;

        public TlDSCapNhapBangLuongModel SelectedItemTongHop
        {
            get => _selectedItemTongHop;
            set => SetProperty(ref _selectedItemTongHop, value);
        }

        private bool _isBangLuongCheck;
        public bool IsBangLuongCheck
        {
            get => _isBangLuongCheck;
            set
            {
                SetProperty(ref _isBangLuongCheck, value);
                OnPropertyChanged(nameof(IsBangLuongTongHopCheck));
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        public bool IsBangLuongTongHopCheck => !IsBangLuongCheck;

        public RelayCommand OpenClockSalaryCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand CapNhatCommand { get; }

        public SalaryTableMonthIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            SalaryTableMonthDialogViewModel salaryTableMonthDialogViewModel,
            SalaryTableMonthDetailViewModel salaryTableMonthDetailViewModel,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlDmPhuCapService iTlDmPhuCapService,
            IExportService exportService,
            ITlDmCanBoService tlDmCanboService,
            ITlDmDonViService tlDmDonViService,
            INsDonViService nsDonViService,
            ITlBaoCaoService tlBaoCaoService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlDmCachTinhLuongChuanService tlDmCachTinhLuongChuanService,
            ITlDmCapBacService dmCapBacService,
            ImportSalaryViewModel importSalaryViewModel,
            ReportDialogViewModel listReportDialogViewModel,
            SalaryTableMonthAggregateDialogViewModel salaryTableMonthAggregateDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            SalaryTableMonthDialogViewModel = salaryTableMonthDialogViewModel;
            SalaryTableMonthDetailViewModel = salaryTableMonthDetailViewModel;
            ImportSalaryViewModel = importSalaryViewModel;
            ListReportDialogViewModel = listReportDialogViewModel;
            SalaryTableMonthAggregateDialogViewModel = salaryTableMonthAggregateDialogViewModel;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _dmCapBacService = dmCapBacService;

            _iTlDmPhuCapService = iTlDmPhuCapService;
            _exportService = exportService;
            _tlDmCanboService = tlDmCanboService;
            _tlDmDonViService = tlDmDonViService;
            _nsDonViService = nsDonViService;
            _tlBaoCaoService = tlBaoCaoService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            ExportBangLuongCommand = new RelayCommand(obj => OnExportBangLuong());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            SearchCommand = new RelayCommand(o => _dtCapNhapBangLuongView.Refresh());
            PrintCommand = new RelayCommand(o => OnPrintBangLuong());
            AggregateCommand = new RelayCommand(o => OnAggregate());
            CapNhatCommand = new RelayCommand(obj => OnCapNhat());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            IsBangLuongCheck = true;
            LoadMonths();
            LoadYear();
            LoadDonViData();
            LoadData();
            LoadTrangThaiBangLuong();
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
            _monthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
            OnPropertyChanged(nameof(MonthSelected));
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
            _yearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
            OnPropertyChanged(nameof(YearSelected));
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);

            var lstDonVi = new List<TlDmDonViModel>();

            TlDmDonViModel tlDmDonViModel = new TlDmDonViModel();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViModel>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            DonViItems = new ObservableCollection<TlDmDonViModel>(lstDonVi);
        }

        public void LoadTrangThaiBangLuong()
        {
            TrangThaiBangLuong = new List<ComboboxItem>();
            _trangThaiBangLuong.Add(new ComboboxItem(TrangThaiBangLuongThang.SU_DUNG, TrangThaiBangLuongThang.SU_DUNG));
            _trangThaiBangLuong.Add(new ComboboxItem(TrangThaiBangLuongThang.KHONG_SU_DUNG, TrangThaiBangLuongThang.KHONG_SU_DUNG));
        }

        private void LoadData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var nam = _yearSelected == null ? _sessionService.Current.YearOfWork.ToString() : _yearSelected.ValueItem;

                    var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                    if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                    {
                        e.Result = _tlDsCapNhapBangLuongService.FindBangLuongThangByNam(int.Parse(nam));
                    }
                    else
                    {
                        e.Result = _tlDsCapNhapBangLuongService.FindBangLuongThangByNam(int.Parse(nam)).Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.TlDmDonVi.MaDonVi));
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<TlDSCapNhapBangLuongModel>>(e.Result);
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        foreach (var item in Items)
                        {
                            item.PropertyChanged += (sender, args) =>
                            {
                                if (args.PropertyName == nameof(TlDSCapNhapBangLuongModel.Selected))
                                {
                                    OnPropertyChanged(nameof(IsAllItemsSelected));
                                    OnPropertyChanged(nameof(IsEnableExportData));
                                }
                            };
                        }
                        _dtCapNhapBangLuongView = CollectionViewSource.GetDefaultView(Items);
                        _dtCapNhapBangLuongView.Filter = SalaryTableFilter;
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool SalaryTableFilter(object obj)
        {
            var result = true;
            var item = (TlDSCapNhapBangLuongModel)obj;
            if (_monthSelected != null && _monthSelected.ValueItem != null)
            {
                result &= item.Thang == int.Parse(_monthSelected.ValueItem);
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

        protected override void OnAdd()
        {
            TlDSCapNhapBangLuongModel capNhapBangLuong = new TlDSCapNhapBangLuongModel();
            ObservableCollection<TlDSCapNhapBangLuongModel> tlDsCapNhapBangLuongModels = new ObservableCollection<TlDSCapNhapBangLuongModel>();
            capNhapBangLuong.Nam = Int32.Parse(YearSelected.ValueItem);
            if (MonthSelected.ValueItem == null)
            {
                int thang = _sessionService.Current.Month;
                capNhapBangLuong.Thang = thang;
                DateTime firstDayOfMonth = new DateTime((int)capNhapBangLuong.Nam, thang, 1);
                DateTime lastDayOfMonth = new DateTime((int)capNhapBangLuong.Nam, thang, 1).AddMonths(1).AddDays(-1);
                capNhapBangLuong.TuNgay = firstDayOfMonth;
                capNhapBangLuong.DenNgay = lastDayOfMonth;
            }
            else
            {
                capNhapBangLuong.Thang = Int32.Parse(MonthSelected.ValueItem);
                DateTime firstDayOfMonth = new DateTime((int)capNhapBangLuong.Nam, Int32.Parse(MonthSelected.ValueItem), 1);
                DateTime lastDayOfMonth = new DateTime((int)capNhapBangLuong.Nam, Int32.Parse(MonthSelected.ValueItem), 1).AddMonths(1).AddDays(-1);
                capNhapBangLuong.TuNgay = firstDayOfMonth;
                capNhapBangLuong.DenNgay = lastDayOfMonth;
            }
            SalaryTableMonthDialogViewModel.Model = capNhapBangLuong;
            SalaryTableMonthDialogViewModel.ViewState = FormViewState.ADD;
            SalaryTableMonthDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            SalaryTableMonthDialogViewModel.Init();
            SalaryTableMonthDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenTlDsCapNhapBangLuongDetail((TlDSCapNhapBangLuongModel)obj);
        }

        private void OnOpenTlDsCapNhapBangLuongDetail(TlDSCapNhapBangLuongModel tlDsCapNhapBangLuong)
        {
            SalaryTableMonthDetailViewModel.Model = tlDsCapNhapBangLuong;
            SalaryTableMonthDetailViewModel.Init();
            SalaryTableMonthDetailViewModel.ShowDialog();
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnAggregate()
        {
            TlDSCapNhapBangLuongModel tlDSCapNhapBangLuongModel = new TlDSCapNhapBangLuongModel();
            tlDSCapNhapBangLuongModel.Id = Guid.NewGuid();
            tlDSCapNhapBangLuongModel.IsTongHop = true;
            tlDSCapNhapBangLuongModel.MaCachTl = CachTinhLuong.CACH0;
            tlDSCapNhapBangLuongModel.NguoiTao = _sessionService.Current.Principal;

            if (MonthSelected.ValueItem != null)
            {
                tlDSCapNhapBangLuongModel.Thang = int.Parse(MonthSelected.ValueItem);
            }
            else
            {
                tlDSCapNhapBangLuongModel.Thang = _sessionService.Current.Month;
            }
            tlDSCapNhapBangLuongModel.Nam = int.Parse(YearSelected.ValueItem);
            SalaryTableMonthAggregateDialogViewModel.Model = tlDSCapNhapBangLuongModel;
            SalaryTableMonthAggregateDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            SalaryTableMonthAggregateDialogViewModel.Init();
            SalaryTableMonthAggregateDialogViewModel.ShowDialogHost();
        }

        protected override void OnDelete()
        {
            try
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgConfirmDeleteBangLuong), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    List<TlDSCapNhapBangLuongModel> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        if (SelectedItem != null && tlDsCapNhapBangLuongModels.Count() == 0)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                _tlDsCapNhapBangLuongService.DeleteBangLuong((int)SelectedItem.Thang, (int)SelectedItem.Nam, SelectedItem.MaCbo, "CACH0");
                                MessageBoxHelper.Info("Xóa dữ liệu thành công");
                            }
                        }
                        else if (tlDsCapNhapBangLuongModels.Count() != 0)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                var lstId = tlDsCapNhapBangLuongModels.Select(x => x.Id.ToString()).ToList();
                                string idXoa = string.Join(",", lstId);
                                _tlDsCapNhapBangLuongService.DeleteBangLuong((int)SelectedItem.Thang, (int)SelectedItem.Nam, string.Join(",",tlDsCapNhapBangLuongModels.Select(x => x.MaCbo)), "CACH0");
                                MessageBoxHelper.Info("Xóa dữ liệu thành công");
                            }
                        }
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            OnRefresh();
                        }
                        else
                        {
                            _logger.Error(e.Error.Message);
                        }
                        IsLoading = false;
                    });
                }
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
            var baoCao = lstBaoCao.FirstOrDefault(x => x.MaBaoCao.Equals("1.2"));
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
                    = Items.Where(n => n.Selected).Select(n => n.MaDonVi).Distinct().ToList();
                tlBaoCaoModel.SelectedMonth = (int)(Items.FirstOrDefault(n => n.Selected).Thang ?? 0);
                tlBaoCaoModel.SelectedYear = (int)(Items.FirstOrDefault(n => n.Selected).Nam ?? 0);
            }
            ListReportDialogViewModel.Model = tlBaoCaoModel;
            ListReportDialogViewModel.LoaiBaoCao = BaoCaoLuong.DSCPL_A4;
            ListReportDialogViewModel.Init();
            ListReportDialogViewModel.ShowDialogHost();
        }

        private void OnExportBangLuong()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_LUONG_THANG_IMPORT);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<TlDSCapNhapBangLuongModel> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();
                    var lstPhuCaps = _iTlDmPhuCapService.FindAll();
                    var lstPhuCapsModel = _mapper.Map<ObservableCollection<TlDmPhuCapModel>>(lstPhuCaps).ToList();
                    ExportChiTietBangLuongModel lstHeader = new ExportChiTietBangLuongModel();
                    lstHeader.LstPhuCap = lstPhuCapsModel;
                    List<ExportChiTietBangLuongModel> lstHeaderItems = new List<ExportChiTietBangLuongModel>();
                    lstHeaderItems.Add(lstHeader);
                    foreach (var bangLuongIndex in tlDsCapNhapBangLuongModels)
                    {
                        var predicateLT = PredicateBuilder.True<TlBangLuongThang>();
                        predicateLT = predicateLT.And(x => x.Parent.Equals(bangLuongIndex.Id));
                        var bangLuongThang = _tlBangLuongThangService.FindByCondition(predicateLT).OrderBy(x => x.MaCbo).ToList();
                        var bangLuongThangModel = _mapper.Map<List<TlBangLuongThangModel>>(bangLuongThang);
                        ExportChiTietBangLuongModel bangLuongDoc = new ExportChiTietBangLuongModel();
                        bangLuongDoc.ListGiaTriDoc = bangLuongThangModel;
                        List<ExportChiTietBangLuongModel> bangLuongDocItems = new List<ExportChiTietBangLuongModel>();
                        bangLuongDocItems.Add(bangLuongDoc);
                        List<TlDSCapNhapBangLuongModel> bangLuongIndexItems = new List<TlDSCapNhapBangLuongModel>();
                        bangLuongIndexItems.Add(bangLuongIndex);
                        var lstCanBo = bangLuongThangModel.Select(item => item.MaCbo).Distinct().ToList();
                        List<ExportChiTietBangLuongModel> bangLuongViewItems = new List<ExportChiTietBangLuongModel>();
                        int i = 1;
                        var donVi = _tlDmDonViService.FindByMaDonVi(bangLuongIndex.MaCbo);
                        foreach (var maCanBo in lstCanBo)
                        {
                            ExportChiTietBangLuongModel itemRpt = new ExportChiTietBangLuongModel();
                            var canBo = _tlDmCanboService.FindByMaCanBo(maCanBo);
                            itemRpt.iStt = i++;
                            itemRpt.iThang = bangLuongIndex.Thang.ToString();
                            itemRpt.iNam = bangLuongIndex.Nam.ToString();
                            itemRpt.sMaCanbo = canBo != null ? canBo.MaCanBo : "";
                            itemRpt.sTenCbo = canBo != null ? canBo.TenCanBo : "";
                            itemRpt.sTenDonVi = donVi != null ? donVi.TenDonVi : "";
                            itemRpt.ListGiaTri = new List<TlBangLuongThangModel>();
                            foreach (var pc in lstPhuCapsModel)
                            {
                                TlBangLuongThangModel giaTri = new TlBangLuongThangModel();
                                giaTri.MaPhuCap = pc.MaPhuCap;
                                var phuCap = bangLuongThangModel.FirstOrDefault(item => item.MaCbo.Equals(maCanBo) && item.MaPhuCap.Equals(pc.MaPhuCap));
                                giaTri.GiaTri = phuCap != null ? phuCap.GiaTri ?? 0 : 0;
                                itemRpt.ListGiaTri.Add(giaTri);
                            }
                            bangLuongViewItems.Add(itemRpt);
                        }
                        // lấy dm đơn vị
                        var predicateDonVi = PredicateBuilder.True<TlDmDonVi>();
                        predicateDonVi = predicateDonVi.And(x => x.MaDonVi.Equals(bangLuongIndex.MaCbo));
                        var donViItems = _tlDmDonViService.FindByCondition(predicateDonVi).ToList();

                        // lay dm cán bộ
                        var predicateCanBo = PredicateBuilder.True<TlDmCanBo>();
                        predicateCanBo = predicateCanBo.And(x => lstCanBo.Contains(x.MaCanBo));
                        var lstCanBoItems = _tlDmCanboService.FindByCondition(predicateCanBo).ToList();
                        var canBoItems = _mapper.Map<List<CadresModel>>(lstCanBoItems);

                        // lay phu cap can bo
                        var predicateCanBoPhuCap = PredicateBuilder.True<TlCanBoPhuCap>();
                        predicateCanBoPhuCap = predicateCanBoPhuCap.And(x => lstCanBo.Contains(x.MaCbo));
                        var canBoPhuCapItems = _tlCanBoPhuCapService.FindAll(predicateCanBoPhuCap);
                        if (canBoPhuCapItems != null)
                        {
                            canBoPhuCapItems = canBoPhuCapItems.Select(n =>
                            {
                                n.HuongPcSn = ((n.HuongPcSn ?? 0) == 0 ? null : n.HuongPcSn);
                                n.HeSo = ((n.HeSo ?? 0) == 0 ? null : n.HeSo);
                                n.PhanTramCt = ((n.PhanTramCt ?? 0) == 0 ? null : n.PhanTramCt);
                                return n;
                            });
                        }

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
                        var xlsFile = _exportService.Export<ExportChiTietBangLuongModel, TlDSCapNhapBangLuongModel, TlDmDonVi, CadresModel, TlCanBoPhuCap>(templateFileName, data);
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

        //private void ExportLuongPhuCapAmCommand(bool isBienPhong = false)
        //{
        //    try
        //    {
        //        BackgroundWorkerHelper.Run((s, e) =>
        //        {
        //            IsLoading = true;

        //            List<ExportResult> results = new List<ExportResult>();
        //            string templateFileName;
        //            if (isBienPhong)
        //            {
        //                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_LUONG_THANG_BIEN_PHONG_1);
        //            }
        //            else
        //            {
        //                if (SelectedKhoIn.ValueItem == "A3")
        //                {
        //                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_LUONG_THANG_A3_SUMMARY);
        //                }
        //                else
        //                {
        //                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_LUONG_THANG_SUMMARY);
        //                }
        //            }
        //            var capNhatBangLuongItem = Items.FirstOrDefault(x => x.Selected);
        //            var thang = capNhatBangLuongItem.Thang;
        //            var nam = capNhatBangLuongItem.Nam;
        //            int donViTinh = 1;
        //            FormatNumber formatNumber = new FormatNumber(donViTinh, ExportType.EXCEL);
        //            var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
        //            var lstDonViSelect = _mapper.Map<ObservableCollection<TlDmDonVi>>(lstDonVi).ToList();

        //            DataTable items = _tlBangLuongThangService.ReportBangLuongThang(lstDonViSelect, thang, nam, donViTinh, IsOrderTheoChucVu);
        //            Dictionary<string, object> data = new Dictionary<string, object>();
        //            data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
        //            data.Add("Cap2", GetHeader2Report());
        //            data.Add("FormatNumber", formatNumber);
        //            data.Add("Unit", donViTinh);
        //            data.Add("DonViTinh", "Đơn vị tính: Đồng");
        //            data.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
        //            data.Add("TieuDe", string.Format("Báo cáo tổng hợp").ToUpper());
        //            data.Add("Items", items);
        //            data.Add("ReportName", ReportName);
        //            data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
        //            AddChuKy(data, _typeChuky);

        //            var xlsFile = _exportService.Export<TlDmCanBoKeHoachModel>(templateFileName, data);
        //            string fileNamePrefix = string.Format("rpt_Luong_BangLuong_{0}_Nam_{1}", thang, nam);
        //            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
        //            results.Add(new ExportResult(string.Format("BangLuongTongHop_thang{0}-{1}", thang, nam), fileNameWithoutExtension, null, xlsFile));

        //            e.Result = results;
        //        }, (s, e) =>
        //        {
        //            if (e.Error == null)
        //            {
        //                var result = (List<ExportResult>)e.Result;
        //                _exportService.Open(result, ExportType.EXCEL);
        //            }
        //            else
        //            {
        //                _logger.Error(e.Error.Message);
        //            }
        //            IsLoading = false;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        private void OnImportData()
        {
            ImportSalaryViewModel.Init();
            ImportSalaryViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnOpenTlDsCapNhapBangLuongDetail((TlDSCapNhapBangLuongModel)obj);
                IsAllItemsSelected = false;
            };
            ImportSalaryViewModel.ShowDialog();
        }

        public override void OnSave()
        {
            List<TlDSCapNhapBangLuongModel> listEdit = Items.Where(x => x.IsModified == true).ToList();
            if (listEdit != null && listEdit.Count > 0)
            {
                foreach (var item in listEdit)
                {
                    var message = GetMessageValidate(item);
                    if (message == null || message == "")
                    {
                        if (item.SuDung == TrangThaiBangLuongThang.SU_DUNG)
                        {
                            item.Status = true;
                        }
                        else
                        {
                            item.Status = false;
                        }
                        var listUpdate = _mapper.Map<TlDsCapNhapBangLuong>(item);
                        _tlDsCapNhapBangLuongService.Update(listUpdate);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            LoadData();
        }

        private string GetMessageValidate(TlDSCapNhapBangLuongModel item)
        {
            IList<string> messages = new List<string>();
            if (item.KhoaBangLuong == true)
            {
                messages.Add(string.Format(Resources.MsgKhoaBangLuong, item.Thang, item.Nam, item.TenDonVi));
            }
            return string.Join(Environment.NewLine, messages);
        }

        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(Description));
        }

        private void OnCapNhat()
        {
            try
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (Items.Count(x => x.Selected) <= 0)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgBangLuongRequireCheck, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Đ/c muốn cập nhật lại bảng lương tháng không?", Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    List<TlDSCapNhapBangLuongModel> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();
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
                                        x.TenDsCnbluong += string.Format(" - Cập nhật");
                                    }
                                    x.NgayTaoBL = DateTime.Now;
                                    return x;
                                }).ToList();

                                var lstAdd = _mapper.Map<List<TlDsCapNhapBangLuong>>(tlDsCapNhapBangLuongModels);

                                var lstChiTietAdd = new List<TlBangLuongThang>();

                                var dicBangLuong = tlDsCapNhapBangLuongModels.GroupBy(x => x.Thang).ToDictionary(x => x.Key, x => x.ToList());
                                foreach (var item in dicBangLuong)
                                {
                                    lstChiTietAdd.AddRange(CapNhat(item.Value));
                                }

                                _tlDsCapNhapBangLuongService.DeleteBangLuong(thang, nam, maDonVi, "CACH0");
                                _tlDsCapNhapBangLuongService.CapNhatBangLuong(idXoa, lstAdd, lstChiTietAdd);

                                tlDsCapNhapBangLuongModels.GroupBy(n => new { n.Thang, n.Nam })
                                .ForAll(n => _tlDsCapNhapBangLuongService.UpdateBangLuongBhxhTheoCapBac((int)(n.Key.Thang ?? 0), (int)(n.Key.Nam ?? 0), n.Select(k => k.MaDonVi).ToList()));
                            }
                        }
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
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
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<TlBangLuongThang> CapNhat(List<TlDSCapNhapBangLuongModel> listDsBangLuong)
        {
            try
            {
                var lstPhuCapSum = _iTlDmPhuCapService.FindAll(n => n.Parent == "SUM" && (n.IsFormula ?? false)).Select(n => n.MaPhuCap);
                List<TlDsCapNhapBangLuong> entities = new List<TlDsCapNhapBangLuong>();
                List<TlBangLuongThang> detailEntities = new List<TlBangLuongThang>();
                int thang = (int)listDsBangLuong.FirstOrDefault().Thang;
                int nam = (int)listDsBangLuong.FirstOrDefault().Nam;
                var dayInMonth = DateTime.DaysInMonth(nam, thang);
                List<string> lstPhuCapLDTamTuyen = GetListPhuCapTamTuyen(lstPhuCapLdTamTuyen);

                // Tạo dữ liệu bảng lương
                string maDonVi = string.Join(",", listDsBangLuong.Select(x => x.MaCbo).ToArray());

                var data = _tlBangLuongThangService.GetDataInsert(thang, nam, maDonVi, "CACH0", dayInMonth);

                var res = data.AsParallel().GroupBy(x => x.MaCbo).Select(x => x.ToList());

                var objTiLeHuongNN = _iTlDmPhuCapService.FindByMaPhuCap(PhuCap.TILE_HUONGNN);
                var objTiLeHuongTamGiamTamGiu = _iTlDmPhuCapService.FindByMaPhuCap(PhuCap.TILE_HUONGTGTG);
                decimal fTiLeHuongNn = 1;
                decimal fTiLeHuongTamGiamTamGiu = objTiLeHuongTamGiamTamGiu is null ? 1 : objTiLeHuongTamGiamTamGiu.GiaTri ?? 1;

                if (objTiLeHuongNN != null)
                {
                    fTiLeHuongNn = objTiLeHuongNN.GiaTri ?? 1;
                }

                Parallel.ForEach(res, lstPhuCap =>
                {
                    var objFirst = lstPhuCap.FirstOrDefault();

                    var parent = listDsBangLuong.FirstOrDefault(x => x.MaCbo.Equals(objFirst.MaDonVi));
                    var pcCongChuan = lstPhuCap.FirstOrDefault(x => PhuCap.CONGCHUAN_SN.Equals(x.MaPhuCap));
                    if (parent != null)
                    {
                        _ = lstPhuCap.Select(x =>
                        {
                            x.Parent = parent.Id;
                            return x;
                        }).ToList();

                        //Tính lương
                        foreach (var phuCap in lstPhuCap.Where(x => !x.CongThuc.IsEmpty()))
                        {
                            TinhLuong(lstPhuCap, phuCap, pcCongChuan);
                        }
                    }

                    if (objFirst.MaCb == "43")
                    {
                        List<TlBangLuongThangQuery> tblClone = new List<TlBangLuongThangQuery>();
                        foreach (var item in lstPhuCapLDTamTuyen)
                        {
                            var current = lstPhuCap.FirstOrDefault(n => n.MaPhuCap == item);
                            if (current == null) continue;
                            if (lstPhuCapLdTamTuyen.IndexOf(item) != -1)
                            {
                                TinhLuongTheoTiLeHuong(lstPhuCap, current, ref tblClone);
                            }
                            else
                            {
                                TinhLuongTheoTiLeHuong(lstPhuCap, current.Clone(), ref tblClone);
                            }
                        }
                    }
                    else if (objFirst.BNuocNgoai ?? false)
                    {
                        foreach (var item in lstPhuCap.Where(n => lstPhuCapNuocNgoai.Contains(n.MaPhuCap)))
                        {
                            item.GiaTri *= fTiLeHuongNn;
                        }

                        if (lstPhuCapSum != null)
                        {
                            foreach (var item in lstPhuCapSum)
                            {
                                var objItem = lstPhuCap.FirstOrDefault(x => x.MaPhuCap == item);
                                if (string.IsNullOrEmpty(objItem.CongThuc)) continue;
                                objItem.IsCalculated = false;
                                TinhLuong(lstPhuCap, objItem, pcCongChuan);
                            }
                        }
                    }
                    else if (objFirst.BTamGiamTamGiu ?? false)
                    {
                        foreach (var item in lstPhuCap.Where(n => lstPhuCapTamGiamTamGiu.Contains(n.MaPhuCap)))
                        {
                            if (lstPhuCapTamGiamTamGiuDefault.Contains(item.MaPhuCap))
                            {
                                item.GiaTri = 0;
                            }
                            else
                            {
                                item.GiaTri *= fTiLeHuongTamGiamTamGiu;
                            }
                        }

                        if (lstPhuCapSum != null)
                        {
                            foreach (var item in lstPhuCapSum)
                            {
                                var objItem = lstPhuCap.FirstOrDefault(x => x.MaPhuCap == item);
                                if (string.IsNullOrEmpty(objItem.CongThuc)) continue;
                                objItem.IsCalculated = false;
                                TinhLuong(lstPhuCap, objItem, pcCongChuan);
                            }
                        }
                    }
                });

                // Với dữ liệu cán bố có mã tăng giảm thuộc BHXH (bắt đầu bằng 0) thì trả về giá trị 0 - Fix tạm
                data.Where(x => x.MaTangGiam != null && x.MaTangGiam.StartsWith("0")).Select(x =>
                {
                    x.GiaTri = 0;
                    return x;
                }).ToList();

                _mapper.Map(data.Where(x => x.GiaTri != 0 || x.MaPhuCap == "TM"), detailEntities);
                return detailEntities;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new List<TlBangLuongThang>();
            }
        }

        private void TinhLuong(List<TlBangLuongThangQuery> items, TlBangLuongThangQuery currentItem, TlBangLuongThangQuery pcCongChuan)
        {
            try
            {
                if (currentItem.IsCalculated) return;

                decimal giaTri = 0;
                var data = new Dictionary<string, object>();
                if (currentItem.CongThuc.Equals(PhuCap.THUETNCN_TT_CONGTHUC))
                {
                    var luongTinhThue = items.FirstOrDefault(x => PhuCap.LUONGTHUE_TT.Equals(x.MaPhuCap));
                    if (!luongTinhThue.IsCalculated)
                    {
                        TinhLuong(items, luongTinhThue, pcCongChuan);
                    }

                    // Nếu lương chịu thuế < 0 thì cập nhật = 0
                    if (luongTinhThue.GiaTri < 0)
                    {
                        luongTinhThue.GiaTri = 0;
                    }
                    giaTri = TinhThueTN(luongTinhThue.GiaTri);
                }
                else
                {
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

                            if (phuCap.CongThuc.IsEmpty() && phuCap.HuongPcSn != null && pcCongChuan != null && pcCongChuan.GiaTri > 0)
                            {
                                // Không có công thức thì tính giá trị dựa theo số ngày hưởng và công chuẩn
                                giaTriPhuCap = (decimal)(phuCap.GiaTri * phuCap.HuongPcSn / pcCongChuan.GiaTri);
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
                }

                currentItem.GiaTri = Math.Round(giaTri);
                currentItem.IsCalculated = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private decimal TinhThueTN(decimal luongThuThue)
        {
            var data = _tlBangLuongThangService.FindThue().OrderBy(x => x.ThuNhapTu).ToList();
            var dsThuThue = _mapper.Map<List<TlDmThueThuNhapCaNhanModel>>(data);
            decimal tienThue = 0;
            decimal t = luongThuThue.Clone();
            if (luongThuThue <= 0)
            {
                return 0;
            }
            else
            {
                foreach (var item in dsThuThue)
                {
                    if (luongThuThue >= (decimal)item.ThuNhapDen && (int)item.ThuNhapDen != 0)
                    {
                        tienThue += ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                        t = t - ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu);
                    }
                    else if ((int)item.ThuNhapDen == 0)
                    {
                        tienThue += (luongThuThue - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                    }
                    else if (luongThuThue < (decimal)item.ThuNhapDen)
                    {
                        decimal tien = t * ((decimal)item.ThueXuat / 100);
                        tienThue += tien;
                        return tienThue;
                    }
                }
                return tienThue;
            }
        }

        public List<string> GetListPhuCapTamTuyen(List<string> lstPhuCap)
        {
            List<string> results = new List<string>();
            foreach (var item in lstPhuCap)
            {
                results.AddRange(RecusivePhuCap(item));
            }
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            foreach (var item in results)
            {
                if (!dicData.ContainsKey(item)) dicData.Add(item, item);
            }
            return dicData.Keys.ToList();
        }

        private List<string> RecusivePhuCap(string sPhuCap)
        {
            List<string> results = new List<string>();
            var objCongThuc = _tlDmCachTinhLuongChuanService.FindByMaCot(sPhuCap);
            if (objCongThuc != null && !string.IsNullOrEmpty(objCongThuc.CongThuc))
            {
                var phucapChilds = objCongThuc.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var child in phucapChilds)
                {
                    var current = RecusivePhuCap(child);
                    if (current.Count != 0) results.AddRange(current);
                }
                if (phucapChilds.IndexOf(PhuCap.TILE_HUONG) != -1) results.Add(sPhuCap);
            }
            if (results.Count != 0) results.Add(sPhuCap);
            return results;
        }

        private void TinhLuongTheoTiLeHuong(List<TlBangLuongThangQuery> items, TlBangLuongThangQuery current, ref List<TlBangLuongThangQuery> tblClone)
        {
            decimal giaTri = 0;
            List<string> lstPhuCap = current.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (lstPhuCap.IndexOf(PhuCap.TILE_HUONG) != -1)
            {
                data.Add(PhuCap.TILE_HUONG, 1);
            }
            foreach (var sMaPhuCap in lstPhuCap)
            {
                if (sMaPhuCap == PhuCap.TILE_HUONG) continue;
                decimal giaTriPhuCap = 0;
                var phuCap = tblClone.FirstOrDefault(x => x.MaPhuCap.Equals(sMaPhuCap));
                if (phuCap != null)
                {
                    giaTriPhuCap = phuCap.GiaTri;
                }
                else
                {
                    phuCap = items.FirstOrDefault(x => x.MaPhuCap.Equals(sMaPhuCap));
                    if (phuCap != null)
                    {
                        var objClone = phuCap.Clone();
                        giaTriPhuCap = objClone.GiaTri;
                        tblClone.Add(objClone);
                    }
                }
                data.Add(sMaPhuCap, giaTriPhuCap);
            }
            try
            {
                var val = EvalExtensions.Execute(current.CongThuc, data);
                giaTri = decimal.Parse(val.ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.InfoFormat("Công thức: {0}. Data: {1}", ObjectCopier.ToJsonString(current), ObjectCopier.ToJsonString(data));
            }
            current.GiaTri = Math.Round(giaTri);
            current.IsCalculated = true;
            tblClone.Add(current);
        }
    }
}