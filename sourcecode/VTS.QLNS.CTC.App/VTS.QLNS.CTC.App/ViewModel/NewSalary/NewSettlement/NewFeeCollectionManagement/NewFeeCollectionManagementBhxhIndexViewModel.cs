using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewImportSalary;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewSalaryTableMonth;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Data;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.IO;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewFeeCollectionManagement
{
    public class NewFeeCollectionManagementBhxhIndexViewModel : GridViewModelBase<TlQuanLyThuNopBhxhModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dtQuanlyThuNopBhxhView;
        private ICollectionView _dtBangLuongTongHopView;
        private ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private ITlQuanLyThuNopBhxhChiTietService _tlQuanLyThuNopBhxhChiTietService;
        private ITlQuanLyThuNopBhxhService _tlQuanLyThuNopBhxhService;
        private ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private ITlDmPhuCapNq104Service _iTlDmPhuCapService;
        private readonly IExportService _exportService;
        private readonly ITlDmCanBoNq104Service _tlDmCanboService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlBaoCaoNq104Service _tlBaoCaoService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly ITlDmCapBacService _dmCapBacService;
        private readonly ISysAuditLogService _sysAuditLogService;


        //public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_SALARY_TABLE_MONTH_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Quản lý thu nộp BHXH";
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewFeeCollectionManagement.NewFeeCollectionManagementIndex);
        public override PackIconKind IconKind => PackIconKind.ClipboardListOutline;
        public override string Title => "Quản lý thu nộp BHXH";
        public override string Description => "Danh sách chứng từ quản lý thu nộp BHXH (Tổng số bản ghi: " + Items.Count() + ")";
        public static List<string> lstPhuCapNuocNgoai = new List<string>() { PhuCap.LHT_TT, PhuCap.PCCV_TT, PhuCap.PCTN_TT, PhuCap.PCTNVK_TT, PhuCap.PCCOV_TT };
        public static List<string> lstPhuCapLdTamTuyen = new List<string>() { PhuCap.BHXHDV_TT, PhuCap.BHYTCN_TT, PhuCap.BHTNCN_TT, PhuCap.BHYTDV_TT, PhuCap.BHTNDV_TT, PhuCap.BHXHCN_TT };

        public NewFeeCollectionManagementBhxhDetailViewModel FeeCollectionManagementBhxhDetailViewModel { get; }
        public NewReportDialogViewModel ListReportDialogViewModel { get; }
        public NewFeeCollectionManagementBhxhDialogViewModel FeeCollectionManagementBhxhDialogViewModel { get; }
        public NewFeeCollectionManagementBhxhAggregateDialogViewModel FeeCollectionManagementBhxhAggregateDialogViewModel { get; }
        public NewImportFeeCollectionBhxhViewModel ImportFeeCollectionBhxhViewModel { get; }

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
                if (SetProperty(ref _monthSelected, value) && _dtQuanlyThuNopBhxhView != null)
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
                if (SetProperty(ref _yearSelected, value) && _dtQuanlyThuNopBhxhView != null && _yearSelected != null)
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
                        SelectAll(value.Value, Items.Where(x => x.IThang == int.Parse(MonthSelected.ValueItem) && x.INam == int.Parse(YearSelected.ValueItem)));
                    }
                    else
                    {
                        SelectAll(value.Value, Items.Where(x => x.IThang == int.Parse(MonthSelected.ValueItem) && x.IIdMaDonVi == SelectedDonViItems.MaDonVi && x.INam == int.Parse(YearSelected.ValueItem)));
                    }
                    OnPropertyChanged();
                }
            }
        }

        private void SelectAll(bool select, IEnumerable<TlQuanLyThuNopBhxhModel> models)
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
                if (SetProperty(ref _selectedDonViItems, value) && _dtQuanlyThuNopBhxhView != null)
                {
                    _dtQuanlyThuNopBhxhView.Refresh();

                }
                if (_dtBangLuongTongHopView != null)
                {
                    _dtBangLuongTongHopView.Refresh();
                }
            }
        }

        private List<DonVi> _ItemDonVi;
        private ObservableCollection<TlQuanLyThuNopBhxhModel> _itemBangLuongTongHop;
        public ObservableCollection<TlQuanLyThuNopBhxhModel> ItemsBangLuongTongHop
        {
            get => _itemBangLuongTongHop;
            set => SetProperty(ref _itemBangLuongTongHop, value);
        }

        private TlQuanLyThuNopBhxhModel _selectedItemTongHop;

        public TlQuanLyThuNopBhxhModel SelectedItemTongHop
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
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                //OnPropertyChanged(nameof(IsEnableButtonDataShow));

            }
        }


        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        public bool IsButtonEnableLock
        {
            get
            {
                var lstCtSelected = Items.Where(x => x.Selected);
                if (lstCtSelected.Count() > 0)
                {
                    var checkTt = lstCtSelected.Select(x => x.BIsKhoa.GetValueOrDefault()).Distinct();
                    if (checkTt.Count() == 1)
                    {
                        IsLock = checkTt.FirstOrDefault();
                        return true;
                    }

                    return false;
                }
                return SelectedItem != null;
            }
        }

        public bool IsCensorship
        {
            get
            {
                return Items.Where(item => item.Selected).ToList().Count > 0 && Items
                    .Where(item => item.Selected).All(x => x.Selected && x.STongHop.IsEmpty() && x.BIsKhoa.GetValueOrDefault());
            }
        }
        public bool IsBangLuongTongHopCheck => !IsBangLuongCheck;

        public bool IsEnableDelete
        {
            get
            {
                return Items.Any(item => item.Selected && item.BIsKhoa == false) && !Items.Any(x => x.Selected && x.BIsKhoa == true);
            }
        }

        public RelayCommand OpenClockCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand CapNhatCommand { get; }

        public NewFeeCollectionManagementBhxhIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            NewSalaryTableMonthDialogViewModel salaryTableMonthDialogViewModel,
            NewSalaryTableMonthDetailViewModel salaryTableMonthDetailViewModel,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlDmPhuCapNq104Service iTlDmPhuCapService,
            IExportService exportService,
            ITlDmCanBoNq104Service tlDmCanboService,
            ITlDmDonViNq104Service tlDmDonViService,
            INsDonViService nsDonViService,
            ITlBaoCaoNq104Service tlBaoCaoService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            ITlDmCapBacService dmCapBacService,
            ITlQuanLyThuNopBhxhChiTietService tlQuanLyThuNopBhxhChiTietService,
            ITlQuanLyThuNopBhxhService tlQuanLyThuNopBhxhService,
            ISysAuditLogService sysAuditLogService,
            NewReportDialogViewModel listReportDialogViewModel,
            NewFeeCollectionManagementBhxhDialogViewModel feeCollectionManagementBhxhDialogViewModel,
            NewFeeCollectionManagementBhxhDetailViewModel feeCollectionManagementBhxhDetailViewModel,
            NewFeeCollectionManagementBhxhAggregateDialogViewModel feeCollectionManagementBhxhAggregateDialogViewModel,
            NewImportFeeCollectionBhxhViewModel importFeeCollectionBhxhViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            ListReportDialogViewModel = listReportDialogViewModel;
            FeeCollectionManagementBhxhDialogViewModel = feeCollectionManagementBhxhDialogViewModel;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _dmCapBacService = dmCapBacService;
            _tlQuanLyThuNopBhxhService = tlQuanLyThuNopBhxhService;
            _tlQuanLyThuNopBhxhChiTietService = tlQuanLyThuNopBhxhChiTietService;
            FeeCollectionManagementBhxhDetailViewModel = feeCollectionManagementBhxhDetailViewModel;
            FeeCollectionManagementBhxhAggregateDialogViewModel = feeCollectionManagementBhxhAggregateDialogViewModel;
            ImportFeeCollectionBhxhViewModel = importFeeCollectionBhxhViewModel;
            _sysAuditLogService = sysAuditLogService;

            _iTlDmPhuCapService = iTlDmPhuCapService;
            _exportService = exportService;
            _tlDmCanboService = tlDmCanboService;
            _tlDmDonViService = tlDmDonViService;
            _nsDonViService = nsDonViService;
            _tlBaoCaoService = tlBaoCaoService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            ExportBangLuongCommand = new RelayCommand(obj => OnExportData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            SearchCommand = new RelayCommand(o => _dtQuanlyThuNopBhxhView.Refresh());
            PrintCommand = new RelayCommand(o => OnPrintBangLuong());
            AggregateCommand = new RelayCommand(o => OnAggregate());
            CapNhatCommand = new RelayCommand(obj => OnCapNhat());
            OpenClockCommand = new RelayCommand(obj => OnLockUnLock());
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

            var lstDonVi = new List<TlDmDonViNq104Model>();

            TlDmDonViNq104Model tlDmDonViModel = new TlDmDonViNq104Model();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            DonViItems = new ObservableCollection<TlDmDonViNq104Model>(lstDonVi);
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

                    _ItemDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                    if (_ItemDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                    {
                        if (TabIndex == ImportTabIndex.Data)
                        {
                            e.Result = _tlQuanLyThuNopBhxhService.FindByThangByNam(int.Parse(nam)).Where(x => string.IsNullOrEmpty(x.STongHop) && !x.IsTongHop);

                        }
                        else
                        {
                            var ItemsTongHop = _tlQuanLyThuNopBhxhService.FindByThangByNam(int.Parse(nam)).Where(x => !string.IsNullOrEmpty(x.STongHop));
                            if (!ItemsTongHop.IsEmpty())
                                ItemsTongHop.Select(x =>
                                {
                                    x.STenDonVi = x.IIdMaDonVi.IsEmpty() ? $"{_sessionInfo.IdDonVi}-{_sessionInfo.TenDonVi}" : $"{x.IIdMaDonVi}-{_sessionInfo.TenDonVi}";
                                    return x;
                                }).ToList();
                            e.Result = ItemsTongHop;
                        }

                    }
                    else
                    {
                        if (TabIndex == ImportTabIndex.Data)
                        {
                            e.Result = _tlQuanLyThuNopBhxhService.FindByThangByNam(int.Parse(nam)).Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.IIdMaDonVi) && string.IsNullOrEmpty(n.STongHop) && !n.IsTongHop);
                        }
                        else
                        {
                            var ItemsTongHop = _tlQuanLyThuNopBhxhService.FindByThangByNam(int.Parse(nam)).Where(x => !string.IsNullOrEmpty(x.STongHop));
                            if (!ItemsTongHop.IsEmpty())
                                ItemsTongHop.Select(x =>
                                {
                                    x.STenDonVi = x.IIdMaDonVi.IsEmpty() ? $"{_sessionInfo.IdDonVi}-{_sessionInfo.TenDonVi}" : $"{x.IIdMaDonVi}-{_sessionInfo.TenDonVi}";
                                    return x;
                                }).ToList();
                            e.Result = ItemsTongHop;
                        }

                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<TlQuanLyThuNopBhxhModel>>(e.Result);
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        DonVi donvi = new DonVi();
                        foreach (var item in Items)
                        {
                            //donvi = _ItemDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(item.IIdMaDonVi));
                            //if (donvi != null)
                            //    item.STenDonVi = $"{donvi.IIDMaDonVi}-{donvi.TenDonVi}";
                            item.PropertyChanged += (sender, args) =>
                            {
                                if (args.PropertyName == nameof(TlQuanLyThuNopBhxhModel.Selected))
                                {
                                    OnPropertyChanged(nameof(IsCensorship));
                                    OnPropertyChanged(nameof(IsButtonEnableLock));
                                    OnPropertyChanged(nameof(IsAllItemsSelected));
                                    OnPropertyChanged(nameof(IsEnableExportData));
                                    OnPropertyChanged(nameof(IsEnableDelete));
                                }
                            };
                        }
                        _dtQuanlyThuNopBhxhView = CollectionViewSource.GetDefaultView(Items);
                        _dtQuanlyThuNopBhxhView.Filter = SalaryTableFilter;
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
            var item = (TlQuanLyThuNopBhxhModel)obj;
            if (_monthSelected != null && _monthSelected.ValueItem != null)
            {
                result &= item.IThang == int.Parse(_monthSelected.ValueItem);
            }
            if (_searchBangLuong != null)
            {
                result &= item.STen.ToLower().Contains(_searchBangLuong.ToLower());
            }
            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result &= item.IIdMaDonVi == SelectedDonViItems.MaDonVi;
            }
            return result;
        }

        protected override void OnAdd()
        {
            TlQuanLyThuNopBhxhModel qlThuNopBhxh = new TlQuanLyThuNopBhxhModel();
            ObservableCollection<TlQuanLyThuNopBhxhModel> qlThuNopBhxhModels = new ObservableCollection<TlQuanLyThuNopBhxhModel>();
            qlThuNopBhxh.INam = Int32.Parse(YearSelected.ValueItem);
            if (MonthSelected.ValueItem == null)
            {
                int thang = _sessionService.Current.Month;
                qlThuNopBhxh.IThang = thang;
                DateTime firstDayOfMonth = new DateTime((int)qlThuNopBhxh.INam, thang, 1);
                DateTime lastDayOfMonth = new DateTime((int)qlThuNopBhxh.INam, thang, 1).AddMonths(1).AddDays(-1);
                qlThuNopBhxh.DTuNgay = firstDayOfMonth;
                qlThuNopBhxh.DDenNgay = lastDayOfMonth;
            }
            else
            {
                qlThuNopBhxh.IThang = Int32.Parse(MonthSelected.ValueItem);
                DateTime firstDayOfMonth = new DateTime((int)qlThuNopBhxh.INam, Int32.Parse(MonthSelected.ValueItem), 1);
                DateTime lastDayOfMonth = new DateTime((int)qlThuNopBhxh.INam, Int32.Parse(MonthSelected.ValueItem), 1).AddMonths(1).AddDays(-1);
                qlThuNopBhxh.DTuNgay = firstDayOfMonth;
                qlThuNopBhxh.DDenNgay = lastDayOfMonth;
            }
            FeeCollectionManagementBhxhDialogViewModel.Model = qlThuNopBhxh;
            FeeCollectionManagementBhxhDialogViewModel.ViewState = FormViewState.ADD;
            FeeCollectionManagementBhxhDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            FeeCollectionManagementBhxhDialogViewModel.Init();
            FeeCollectionManagementBhxhDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenTlDsCapNhapBangLuongDetail((TlQuanLyThuNopBhxhModel)obj);
        }

        private void OnOpenTlDsCapNhapBangLuongDetail(TlQuanLyThuNopBhxhModel qlThuNopBhxh)
        {
            FeeCollectionManagementBhxhDetailViewModel.Model = qlThuNopBhxh;
            FeeCollectionManagementBhxhDetailViewModel.Init();
            FeeCollectionManagementBhxhDetailViewModel.ShowDialog();
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnDelete()
        {
            try
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgConfirmDeleteQSChungTu), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                List<TlQuanLyThuNopBhxhModel> tlQuanLyThuNopBhxhModel = Items.Where(x => x.Selected).ToList();
                if (TabIndex == ImportTabIndex.Data)
                {

                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        if (SelectedItem != null && tlQuanLyThuNopBhxhModel.Count() == 0)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                _tlQuanLyThuNopBhxhService.DeleteModelAndDetail((int)SelectedItem.IThang, (int)SelectedItem.INam, SelectedItem.IIdMaDonVi, CachTinhLuong.CACH0);
                                MessageBoxHelper.Info("Xóa dữ liệu thành công");
                            }
                        }
                        else if (tlQuanLyThuNopBhxhModel.Count() != 0)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                var lstId = tlQuanLyThuNopBhxhModel.Select(x => x.Id.ToString()).ToList();
                                string idXoa = string.Join(",", lstId);
                                _tlQuanLyThuNopBhxhService.DeleteModelAndDetail((int)SelectedItem.IThang, (int)SelectedItem.INam, string.Join(",", tlQuanLyThuNopBhxhModel.Select(x => x.IIdMaDonVi)), CachTinhLuong.CACH0);
                                MessageBoxHelper.Info("Xóa dữ liệu thành công");
                            }
                        }
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            _sysAuditLogService.WriteLog(Resources.ApplicationName, "Xóa chứng từ quản lý thu nộp BHXH", (int)TypeExecute.Delete, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
                            OnRefresh();
                        }
                        else
                        {
                            _logger.Error(e.Error.Message);
                        }
                        IsLoading = false;
                    });
                }
                else
                {
                    if (SelectedItem != null && tlQuanLyThuNopBhxhModel.Count() == 0)
                    {
                        if (dialogResult == DialogResult.Yes)
                        {
                            _tlQuanLyThuNopBhxhService.DeleteModelAndDetail((int)SelectedItem.IThang, (int)SelectedItem.INam, SelectedItem.IIdMaDonVi, "CACH0", SelectedItem.Id, true);
                            MessageBoxHelper.Info("Xóa dữ liệu thành công");
                        }
                    }
                    else if (tlQuanLyThuNopBhxhModel.Count() != 0)
                    {
                        if (dialogResult == DialogResult.Yes)
                        {
                            var lstId = tlQuanLyThuNopBhxhModel.Select(x => x.Id.ToString()).ToList();
                            string idXoa = string.Join(",", lstId);
                            _tlQuanLyThuNopBhxhService.DeleteModelAndDetail((int)SelectedItem.IThang, (int)SelectedItem.INam, string.Join(",", tlQuanLyThuNopBhxhModel.Select(x => x.IIdMaDonVi)), "CACH0", SelectedItem.Id, true);
                            MessageBoxHelper.Info("Xóa dữ liệu thành công");
                        }
                    }
                    OnRefresh();
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
            tlBaoCaoModel.TenBaoCao = "Báo cáo thu nộp BHXH";
            ListReportDialogViewModel.LstMaDonViIndexViewSelected = new List<string>();
            if (Items.Any(n => n.Selected))
            {
                ListReportDialogViewModel.LstMaDonViIndexViewSelected
                    = Items.Where(n => n.Selected).Select(n => n.IIdMaDonVi).Distinct().ToList();
                tlBaoCaoModel.SelectedMonth = (int)(Items.FirstOrDefault(n => n.Selected).IThang ?? 0);
                tlBaoCaoModel.SelectedYear = (int)(Items.FirstOrDefault(n => n.Selected).INam ?? 0);
            }
            ListReportDialogViewModel.Model = tlBaoCaoModel;
            ListReportDialogViewModel.IsThuNopBhxh = true;
            ListReportDialogViewModel.LoaiBaoCao = BaoCaoLuong.DSCPL_A4;
            ListReportDialogViewModel.Init();
            ListReportDialogViewModel.ShowDialogHost();
        }

        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QLTHU_NOP_BHXH_IMPORT_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<TlQuanLyThuNopBhxhModel> tlThuNopBhxhs = Items.Where(x => x.Selected).ToList();
                    var listDonVi = _tlDmDonViService.FindAllDonViNq104();

                    foreach (var item in tlThuNopBhxhs)
                    {
                        var lstPhuCaps = _iTlDmPhuCapService.FindByIdThuNopBhxh(item.Id);
                        var lstPhuCapsModel = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(lstPhuCaps).ToList();
                        //ExportChiTietThuNopBhxhModel lstHeader = new ExportChiTietThuNopBhxhModel
                        //{
                        //    LstPhuCap = lstPhuCapsModel
                        //};

                        var predicate = PredicateBuilder.True<TlQuanLyThuNopBhxhChiTiet>();
                        predicate = predicate.And(x => x.IIdParentId.Equals(item.Id));
                        var tlThuNops = _tlQuanLyThuNopBhxhChiTietService.FindByCondition(predicate).OrderBy(x => x.SMaCbo).ToList();
                        var tlThuNopModels = _mapper.Map<List<TlQuanLyThuNopBhxhNq104ChiTietModel>>(tlThuNops);
                        List<ExportChiTietThuNopBhxhNq104Model> bangLuongViewItems = new List<ExportChiTietThuNopBhxhNq104Model>();
                        var dataGroups = tlThuNops.GroupBy(g => g.SMaCbo).Select(x => new ExportChiTietThuNopBhxhNq104Model
                        {
                            IThang = x.FirstOrDefault().IThang,
                            INam = x.FirstOrDefault().INam,
                            SMaCanbo = x.FirstOrDefault().SMaCbo,
                            STenCbo = x.FirstOrDefault().STenCbo,
                            STenDonVi = item.STenDonVi,
                            ListGiaTriDoc = _mapper.Map<List<TlQuanLyThuNopBhxhNq104ChiTietModel>>(x.ToList()),
                            ListGiaTri = _mapper.Map<List<TlQuanLyThuNopBhxhNq104ChiTietModel>>(x.ToList()),
                            LstPhuCap = lstPhuCapsModel


                        }).ToList();

                        foreach (var itemData in dataGroups.Select((value, index) => new { value, index }))
                        {
                            itemData.value.IStt = itemData.index + 1;
                        }
                        Dictionary<string, object> data = new Dictionary<string, object>
                        {
                            { "BangLuongViewItems", dataGroups },
                            { "LstHeader", lstPhuCapsModel },
                            { "ListGiaTriDoc", tlThuNops }

                        };

                        fileNamePrefix = string.Format("Bang_Luong_Chi_Tiet_Import_{0}_{1}_{2}", item.STenDonVi, item.IThang, item.INam);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ExportChiTietThuNopBhxhNq104Model, TlQuanLyThuNopBhxhNq104ChiTietModel, TlQuanLyThuNopBhxhChiTiet, TlDmPhuCapNq104Model>(templateFileName, data);
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

                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Đ/c muốn cập nhật lại chứng từ quản lý thu nộp BHXH không?", Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    List<TlQuanLyThuNopBhxhModel> tlqlThuNopBhxhs = Items.Where(x => x.Selected).ToList();
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        if (!tlqlThuNopBhxhs.IsEmpty())
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                var lstId = tlqlThuNopBhxhs.Select(x => x.Id.ToString()).ToList();
                                string idXoa = string.Join(",", lstId);
                                string maDonVi = string.Join(",", tlqlThuNopBhxhs.Select(x => x.IIdMaDonVi));
                                int thang = int.Parse(MonthSelected.ValueItem);
                                int nam = int.Parse(YearSelected.ValueItem);

                                tlqlThuNopBhxhs.Select(x =>
                                {
                                    if (!x.STen.Contains("Cập nhật"))
                                    {
                                        x.STen += string.Format(" - Cập nhật");
                                    }
                                    x.DNgayTao = DateTime.Now;
                                    return x;
                                }).ToList();

                                var lstAdd = _mapper.Map<List<TlQuanLyThuNopBhxh>>(tlqlThuNopBhxhs);

                                var lstChiTietAdd = new List<TlQuanLyThuNopBhxhChiTiet>();

                                var dicBangLuong = tlqlThuNopBhxhs.GroupBy(x => x.IThang).ToDictionary(x => x.Key, x => x.ToList());
                                foreach (var item in dicBangLuong)
                                {
                                    lstChiTietAdd.AddRange(CapNhat(item.Value));
                                }

                                _tlQuanLyThuNopBhxhService.DeleteModelAndDetail(thang, nam, maDonVi, CachTinhLuong.CACH0);
                                _tlQuanLyThuNopBhxhService.UpdateEntitiesAndDetails(idXoa, lstAdd, lstChiTietAdd);

                                tlqlThuNopBhxhs.GroupBy(n => new { n.IThang, n.INam })
                                .ForAll(n => _tlQuanLyThuNopBhxhService.UpdateDetailBhxhTheoCapBac((int)(n.Key.IThang ?? 0), (int)(n.Key.INam ?? 0), n.Select(k => k.IIdMaDonVi).ToList()));
                            }
                        }
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                _sysAuditLogService.WriteLog(Resources.ApplicationName, "Cập nhật chứng từ quản lý thu nộp BHXH", (int)TypeExecute.Update, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
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


        private void OnImportData()
        {
            TlQuanLyThuNopBhxhModel model = new TlQuanLyThuNopBhxhModel()
            {
                Id = Guid.NewGuid(),
                INam = DateTime.Now.Year,
                IThang = DateTime.Now.Month,
            };
            ImportFeeCollectionBhxhViewModel.Model = model;
            ImportFeeCollectionBhxhViewModel.Init();
            ImportFeeCollectionBhxhViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnOpenTlDsCapNhapBangLuongDetail((TlQuanLyThuNopBhxhModel)obj);
                IsAllItemsSelected = false;
            };
            ImportFeeCollectionBhxhViewModel.ShowDialog();
        }


        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var messageBox = new NSMessageBoxViewModel(message, "Xác nhận", NSMessageBoxButtons.YesNo, OnLockHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnLockHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            //if (SelectedItem == null) return;
            var lstCtSelected = Items.Where(x => x.Selected);
            if (lstCtSelected.Count() > 0)
            {
                IsLock = !IsLock;
                foreach (var it in lstCtSelected)
                {
                    _tlQuanLyThuNopBhxhService.LockOrUnlock(it.Id, IsLock);
                    var qsChungTu = Items.First(x => x.Id == it.Id);
                    qsChungTu.BIsKhoa = IsLock;
                }
                lstCtSelected.ForAll(x => x.Selected = IsLock);
                OnPropertyChanged(nameof(IsCensorship));
                OnPropertyChanged(nameof(IsEnableDelete));
            }
            else
            {
                IsLock = !IsLock;
                _tlQuanLyThuNopBhxhService.LockOrUnlock(SelectedItem.Id, IsLock);
                var qsChungTu = Items.First(x => x.Id == SelectedItem.Id);
                qsChungTu.BIsKhoa = !SelectedItem.BIsKhoa.GetValueOrDefault();
            }
            //LoadData();

        }

        private void OnAggregate()
        {
            if (Items.Count(x => x.Selected && x.BIsKhoa == true) <= 0)
            {
                System.Windows.MessageBox.Show(Resources.MsgQLThuNopRequireCheck, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            TlQuanLyThuNopBhxhModel tlQlThuNopBhxhModel = new TlQuanLyThuNopBhxhModel
            {
                Id = Guid.NewGuid(),
                IsTongHop = true,
                STongHop = string.Join(",", Items.Where(x => x.Selected && x.BIsKhoa == true).Select(s => s.Id)),
                SMaCachTl = CachTinhLuong.CACH0,
                INam = _sessionService.Current.YearOfWork,
                IThang = _sessionService.Current.Month,
                SNguoiTao = _sessionService.Current.Principal,
                IIdMaDonVi = _sessionService.Current.IdDonVi,
                STenDonVi = _sessionService.Current.TenDonVi
            };

            FeeCollectionManagementBhxhAggregateDialogViewModel.Model = tlQlThuNopBhxhModel;
            FeeCollectionManagementBhxhAggregateDialogViewModel.DataAllSummary = new ObservableCollection<TlQuanLyThuNopBhxhModel>(Items.Where(x => x.Selected && x.BIsKhoa == true));
            FeeCollectionManagementBhxhAggregateDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            FeeCollectionManagementBhxhAggregateDialogViewModel.Init();
            FeeCollectionManagementBhxhAggregateDialogViewModel.ShowDialogHost();
        }


        private List<TlQuanLyThuNopBhxhChiTiet> CapNhat(List<TlQuanLyThuNopBhxhModel> listQlThuNopBhxhs)
        {
            try
            {
                var lstPhuCapSum = _iTlDmPhuCapService.FindAll(n => n.Parent == "SUM" && (n.IsFormula ?? false)).Select(n => n.MaPhuCap);
                List<TlQuanLyThuNopBhxh> entities = new List<TlQuanLyThuNopBhxh>();
                List<TlQuanLyThuNopBhxhChiTiet> detailEntities = new List<TlQuanLyThuNopBhxhChiTiet>();
                int thang = (int)listQlThuNopBhxhs.FirstOrDefault().IThang;
                int nam = (int)listQlThuNopBhxhs.FirstOrDefault().INam;
                var dayInMonth = DateTime.DaysInMonth(nam, thang);
                List<string> lstPhuCapLDTamTuyen = GetListPhuCapTamTuyen(lstPhuCapLdTamTuyen);

                // Tạo dữ liệu bảng chi tiết
                string maDonVi = string.Join(",", listQlThuNopBhxhs.Select(x => x.IIdMaDonVi).ToArray());

                var data = _tlBangLuongThangService.GetDataInsertBhxh(thang, nam, maDonVi, "CACH0", dayInMonth);

                var res = data.AsParallel().GroupBy(x => x.MaCbo).Select(x => x.ToList());

                var objTiLeHuongNN = _iTlDmPhuCapService.FindByMaPhuCap(PhuCap.TILE_HUONGNN);
                decimal fTiLeHuongNn = 1;
                if (objTiLeHuongNN != null)
                {
                    fTiLeHuongNn = objTiLeHuongNN.GiaTri ?? 1;
                }

                Parallel.ForEach(res, lstPhuCap =>
                {
                    var objFirst = lstPhuCap.FirstOrDefault();

                    var parent = listQlThuNopBhxhs.FirstOrDefault(x => x.IIdMaDonVi.Equals(objFirst.MaDonVi));
                    if (parent != null)
                    {
                        _ = lstPhuCap.Select(x =>
                        {
                            x.Parent = parent.Id;
                            return x;
                        }).ToList();
                        var pcCongChuan = lstPhuCap.FirstOrDefault(x => PhuCap.CONGCHUAN_SN.Equals(x.MaPhuCap));

                        //Tính lương
                        foreach (var phuCap in lstPhuCap.Where(x => !x.CongThuc.IsEmpty()))
                        {
                            TinhLuong(lstPhuCap, phuCap, pcCongChuan);
                        }
                    }

                    if (objFirst.MaCb == "43")
                    {
                        List<TlBangLuongThangNq104Query> tblClone = new List<TlBangLuongThangNq104Query>();
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

                        var pcCongChuan = lstPhuCap.FirstOrDefault(x => PhuCap.CONGCHUAN_SN.Equals(x.MaPhuCap));
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
                _mapper.Map(data, detailEntities);
                return detailEntities;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new List<TlQuanLyThuNopBhxhChiTiet>();
            }
        }

        private void TinhLuong(List<TlBangLuongThangNq104Query> items, TlBangLuongThangNq104Query currentItem, TlBangLuongThangNq104Query pcCongChuan)
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
                        t -= ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu);
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

        private void TinhLuongTheoTiLeHuong(List<TlBangLuongThangNq104Query> items, TlBangLuongThangNq104Query current, ref List<TlBangLuongThangNq104Query> tblClone)
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
