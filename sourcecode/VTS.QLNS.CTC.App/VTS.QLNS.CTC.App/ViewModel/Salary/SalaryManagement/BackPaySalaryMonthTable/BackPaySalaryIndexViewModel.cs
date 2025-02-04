using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using System.IO;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.ViewModel.Salary.Report;
using VTS.QLNS.CTC.App.View.Salary.Report;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.InsuranceSalaryMonthTable.Export;
using VTS.QLNS.CTC.App.View.Salary.SalaryManagement.InsuranceSalaryMonthTable.Export;
using System.Windows;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.BackPaySalaryMonthTable;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.InsuranceSalaryMonthTable
{
    public class BackPaySalaryIndexViewModel : GridViewModelBase<TlDSCapNhapBangLuongModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly ITlBangLuongThangBHXHService _tlBangLuongThangBHXHService;
        private readonly ITlBangLuongThangTruyThuService _tlBangLuongThangTruyThuService;
        private readonly IExportService _exportService;
        private readonly ITlDmCanBoService _tlDmCanboService;
        private readonly ITlCanBoCheDoBHXHService _tTlCanBoCheDoBHXHService;
        private readonly ITlCanBoPhuCapService _iTlCanBoPhuCapService;
        private readonly ITlDmCapBacService _iTlDmCapBacService;
        private readonly ITlDmCachTinhLuongBaoHiemService _tlDmCachTinhLuongBaoHiemService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private ITlDmCheDoBHXHService _iTlDmCheDoBHXHService;
        private ICollectionView _dtDanhSachBangLuongView;
        private ITlDmDonViService _tlDmDonViService;
        private readonly INsDonViService _iNsDonViService;
        private readonly ITlDmCachTinhLuongTruyThuService _dmCachTinhLuongTruyThuService;

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_BACK_PAY_SALARY_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Bảng lương tháng truy thu";
        public override Type ContentType => typeof(View.Salary.SalaryManagement.BackPaySalaryMonthTable.BackPaySalaryTableMonthIndex);
        public override PackIconKind IconKind => PackIconKind.ClipboardListOutline;
        public override string Title => "Bảng lương tháng truy thu";
        public override string Description => "Bảng lương tháng truy thu của các đơn vị";
        public BackPaySalaryAddDialogViewModel BackPaySalaryAddDialogViewModel { get; }
        public InsuranceSalaryDetailViewModel InsuranceSalaryDetailViewModel { get; }
        public BackPaySalaryDetailViewModel BackPaySalaryDetailViewModel { get; }
        public InsuranceSalaryExportViewModel InsuranceSalaryExportViewModel { get; set; }
        public InsuranceSalaryAggregateViewModel InsuranceSalaryAggregateViewModel { get; }

        private List<TlCachTinhLuongModel> _cachTinhLuongData;
        public List<TlCachTinhLuongModel> CachTinhLuongData
        {
            get => _cachTinhLuongData;
            set => _cachTinhLuongData = value;
        }

        private TlDSCapNhapBangLuongModel _selectedBangLuong;
        public TlDSCapNhapBangLuongModel SelectedBangLuong
        {
            get => _selectedBangLuong;
            set
            {
                SetProperty(ref _selectedBangLuong, value);
                if (_selectedBangLuong != null)
                {
                    IsLock = _selectedBangLuong.KhoaBangLuong.GetValueOrDefault();
                    IsEdit = true;
                }
                else
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
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
                    var checkTt = lstCtSelected.Select(x => x.KhoaBangLuong.GetValueOrDefault()).Distinct();
                    if (checkTt.Count() == 1)
                    {
                        IsLock = (bool)checkTt.FirstOrDefault();
                        return true;
                    }

                    return false;
                }
                return SelectedBangLuong != null;
            }
        }

        public bool IsEnabled
        {
            get
            {
                var lstCtSelected = Items.Where(x => x.Selected);
                if (lstCtSelected.Count() > 0)
                {
                    if (lstCtSelected.Any(x => x.KhoaBangLuong.GetValueOrDefault()))
                    {
                        return false;
                    }
                    else { return true; }
                }
                else
                {
                    return false;
                }
            }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public bool IsButtonEnable
        {
            get
            {
                var result = false;
                var lstSelected = Items.Where(x => x.Selected).ToList();

                if (lstSelected.Count > 0)
                {
                    var lstSelectedKhoa = lstSelected.Where(x => x.KhoaBangLuong.GetValueOrDefault()).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.KhoaBangLuong.GetValueOrDefault()).ToList();
                    if (lstSelectedKhoa.Count() > 0 && lstSelectedMo.Count() > 0)
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Count() > 0)
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Count() > 0)
                    {
                        IsLock = false;
                        result = true;
                    }
                }

                return result;
            }
        }

        public bool IsCensorship
        {
            get
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    var itemSelected = Items.Where(x => x.Selected);
                    return itemSelected.Any() && itemSelected.All(x => x.KhoaBangLuong.GetValueOrDefault());
                }
                else
                {
                    return false;
                }
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
                OnPropertyChanged(nameof(IsButtonEnable));
            }
        }

        private List<ComboboxItem> _monthBHXHs;
        public List<ComboboxItem> MonthBHXHs
        {
            get => _monthBHXHs;
            set => SetProperty(ref _monthBHXHs, value);
        }

        private ComboboxItem _monthBHXHSelected;
        public ComboboxItem MonthBHXHSelected
        {
            get => _monthBHXHSelected;
            set
            {
                if (SetProperty(ref _monthBHXHSelected, value) && _dtDanhSachBangLuongView != null)
                {
                    OnRefresh();
                }
            }
        }

        private List<ComboboxItem> _yearBHXHs;
        public List<ComboboxItem> YearBHXHs
        {
            get => _yearBHXHs;
            set => SetProperty(ref _yearBHXHs, value);
        }

        private ComboboxItem _yearBHXHSelected;
        public ComboboxItem YearBHXHSelected
        {
            get => _yearBHXHSelected;
            set
            {
                if (SetProperty(ref _yearBHXHSelected, value) && _dtDanhSachBangLuongView != null && _yearBHXHSelected != null)
                {
                    OnRefresh();
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
                    SelectAll(value.Value, Items.Where(x => x.Thang == int.Parse(MonthBHXHSelected.ValueItem) && x.Nam == int.Parse(YearBHXHSelected.ValueItem)));
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEnableExportData =>
            Items != null && Items.Where(x => x.Selected).Count() > 0;
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportBangLuongCommand { get; }
        public RelayCommand PrintCommand { get; }
        public InsuranceSalaryReportViewModel InsuranceSalaryReportViewModel { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand UpdateCommand { get; }

        public BackPaySalaryIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IExportService exportService,
            ITlDmCanBoService iTlDmCanBoService,
            ITlCanBoCheDoBHXHService iTlCanBoCheDoBHXHService,
            ITlCanBoPhuCapService iTlCanBoPhuCapService,
            BackPaySalaryAddDialogViewModel backPaySalaryAddDialogViewModel,
            BackPaySalaryDetailViewModel backPaySalaryDetailViewModel,
            InsuranceSalaryDetailViewModel insuranceSalaryDetailViewModel,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            ITlBangLuongThangBHXHService tlBangLuongThangBHXHService,
            ITlDmDonViService tlDmDonViService,
            INsDonViService iNsDonViService,
            ITlDmCheDoBHXHService iTlDmCheDoBHXHService,
            InsuranceSalaryReportViewModel insuranceSalaryReportViewModel,
            InsuranceSalaryExportViewModel insuranceSalaryExportViewModel,
            InsuranceSalaryAggregateViewModel insuranceSalaryAggregateViewModel,
            ITlDmCapBacService iTlDmCapBacService,
            ITlDmCachTinhLuongBaoHiemService iTlDmCachTinhLuongBaoHiemService,
            ITlBangLuongThangService iTlBangLuongThangService,
            ITlBangLuongThangTruyThuService tlBangLuongThangTruyThuService,
            ITlDmCachTinhLuongTruyThuService tlDmCachTinhLuongTruyThuService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _exportService = exportService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlBangLuongThangBHXHService = tlBangLuongThangBHXHService;
            _tlDmDonViService = tlDmDonViService;
            _iNsDonViService = iNsDonViService;
            _iTlDmCheDoBHXHService = iTlDmCheDoBHXHService;
            _tlDmCanboService = iTlDmCanBoService;
            _tTlCanBoCheDoBHXHService = iTlCanBoCheDoBHXHService;
            InsuranceSalaryReportViewModel = insuranceSalaryReportViewModel;
            InsuranceSalaryExportViewModel = insuranceSalaryExportViewModel;
            InsuranceSalaryAggregateViewModel = insuranceSalaryAggregateViewModel;
            _iTlCanBoPhuCapService = iTlCanBoPhuCapService;
            _iTlDmCapBacService = iTlDmCapBacService;
            _tlDmCachTinhLuongBaoHiemService = iTlDmCachTinhLuongBaoHiemService;
            _tlBangLuongThangService = iTlBangLuongThangService;
            _tlBangLuongThangTruyThuService = tlBangLuongThangTruyThuService;
            _dmCachTinhLuongTruyThuService = tlDmCachTinhLuongTruyThuService;

            BackPaySalaryDetailViewModel = backPaySalaryDetailViewModel;
            BackPaySalaryAddDialogViewModel = backPaySalaryAddDialogViewModel;
            InsuranceSalaryDetailViewModel = insuranceSalaryDetailViewModel;
            SearchCommand = new RelayCommand(o => _dtDanhSachBangLuongView.Refresh());
            ExportBangLuongCommand = new RelayCommand(obj => OnExportBangLuong());
            PrintCommand = new RelayCommand(OnPrint);
            ExportDataCommand = new RelayCommand(obj => OnExportDataDialog());
            LockCommand = new RelayCommand(OnLock);
            AggregateCommand = new RelayCommand(ConfirmAggregate);
            UpdateCommand = new RelayCommand(obj => OnUpdateSalary());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            TabIndex = ImportTabIndex.Data;
            LoadMonths();
            LoadYear();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<TlDsCapNhapBangLuong> data = new List<TlDsCapNhapBangLuong>();
                var predicate = PredicateBuilder.True<TlDsCapNhapBangLuong>();
                predicate = predicate.And(x => x.MaCachTl.Equals(CachTinhLuong.CACH1));
                predicate = predicate.And(x => x.Thang.Equals(decimal.Parse(MonthBHXHSelected.ValueItem ?? _sessionInfo.Month.ToString())));
                predicate = predicate.And(x => x.Nam.Equals(decimal.Parse(YearBHXHSelected.ValueItem ?? _sessionInfo.YearOfWork.ToString())));
                if (TabIndex == ImportTabIndex.Data)
                {
                    data = _tlDsCapNhapBangLuongService.FindByCondition(predicate).Where(x => !x.IsTongHop.GetValueOrDefault() && string.IsNullOrEmpty(x.STongHop)).ToList();
                }
                else
                {
                    data = _tlDsCapNhapBangLuongService.FindByCondition(predicate).Where(x => !string.IsNullOrEmpty(x.STongHop)).ToList();
                }

                Items = _mapper.Map<ObservableCollection<TlDSCapNhapBangLuongModel>>(data);
                foreach (var item in Items)
                {
                    if (string.IsNullOrEmpty(item.STongHop))
                    {
                        var donVi = _tlDmDonViService.FindByCondition(x => x.MaDonVi.Equals(item.MaCbo)).FirstOrDefault();
                        item.TenDonVi = donVi?.TenDonVi ?? null;
                    }
                    else
                    {
                        var donVi = _iNsDonViService.FindByMaDonViAndNamLamViec(item.MaCbo, _sessionInfo.YearOfWork);
                        item.TenDonVi = donVi?.TenDonVi ?? null;
                    }
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(TlDSCapNhapBangLuongModel.Selected))
                        {
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsEnableExportData));
                            OnPropertyChanged(nameof(IsButtonEnableLock));
                            OnPropertyChanged(nameof(IsEnabled));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsCensorship));
                        }
                    };
                }
                if (Items != null && Items.Count > 0)
                {
                    SelectedBangLuong = Items.FirstOrDefault();
                }
                _dtDanhSachBangLuongView = CollectionViewSource.GetDefaultView(Items);
                _dtDanhSachBangLuongView.Filter = BangLuongFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadYear()
        {
            _yearBHXHs = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _yearBHXHs.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(YearBHXHs));
            _yearBHXHSelected = _yearBHXHs.FirstOrDefault(x => x.ValueItem == nam.ToString());
            OnPropertyChanged(nameof(YearBHXHSelected));
        }

        private void LoadMonths()
        {
            MonthBHXHs = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _monthBHXHs.Add(month);
            }
            var thang = _sessionService.Current.Month;
            MonthBHXHSelected = _monthBHXHs.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        protected override void OnAdd()
        {
            TlDSCapNhapBangLuongModel danhSachBangLuongModel = new TlDSCapNhapBangLuongModel();
            danhSachBangLuongModel.Thang = int.Parse(MonthBHXHSelected.ValueItem);
            danhSachBangLuongModel.Nam = _sessionService.Current.YearOfWork;
            var firstDay = new DateTime(DateTime.Now.Year, int.Parse(MonthBHXHSelected.ValueItem), 1);
            danhSachBangLuongModel.TuNgay = firstDay;
            danhSachBangLuongModel.DenNgay = firstDay.AddMonths(1).AddDays(-1);
            danhSachBangLuongModel.TenDsCnbluong = "Danh sách bảng lương truy thu tháng - " + MonthBHXHSelected.ValueItem + " năm - " + DateTime.Now.Year.ToString();

            BackPaySalaryAddDialogViewModel.Model = danhSachBangLuongModel;
            BackPaySalaryAddDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new View.Salary.SalaryManagement.BackPaySalaryMonthTable.BackPaySalaryDialog()
            {
                DataContext = BackPaySalaryAddDialogViewModel
            };
            BackPaySalaryAddDialogViewModel.Init();
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
                if (dialogResult == DialogResult.Yes)
                {
                    var selectedSalaries = Items.Where(n => n.Selected).ToList();
                    foreach (var item in selectedSalaries)
                    {
                        if (!string.IsNullOrEmpty(item.STongHop))
                        {
                            var lstChild = item.STongHop.Split(",");
                            foreach (var child in lstChild)
                            {
                                var ctChild = _tlDsCapNhapBangLuongService.FindByCondition(CachTinhLuong.CACH1, child, (int)item.Thang, (int)item.Nam);
                                if (ctChild != null)
                                {
                                    ctChild.IsTongHop = false;
                                    _tlDsCapNhapBangLuongService.Update(ctChild);
                                }
                            }
                        }
                        _tlBangLuongThangTruyThuService.DeleteByParentId(item.Id);
                        _tlDsCapNhapBangLuongService.Delete(item.Id);
                    }
                    OnRefresh();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenBangLuongDetail((TlDSCapNhapBangLuongModel)obj);
        }

        private void OnOpenBangLuongDetail(TlDSCapNhapBangLuongModel tlDSCapNhapBangLuongModel)
        {
            try
            {
                if (tlDSCapNhapBangLuongModel == null)
                    return;
                BackPaySalaryDetailViewModel.Model = tlDSCapNhapBangLuongModel;
                BackPaySalaryDetailViewModel.ThoiGian = string.Format("Tháng {0} Năm {1}", tlDSCapNhapBangLuongModel.Thang, tlDSCapNhapBangLuongModel.Nam);
                BackPaySalaryDetailViewModel.Init();
                var view = new View.Salary.SalaryManagement.BackPaySalaryMonthTable.BackPaySalaryDetail { DataContext = BackPaySalaryDetailViewModel };
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
            var item = (TlDSCapNhapBangLuongModel)obj;
            if (_monthBHXHSelected != null && _monthBHXHSelected.ValueItem != null && _yearBHXHSelected != null && _yearBHXHSelected.ValueItem != null)
            {
                result &= item.Thang == int.Parse(MonthBHXHSelected.ValueItem) && item.Nam == int.Parse(YearBHXHSelected.ValueItem);
            }
            if (_searchBangLuong != null)
            {
                result &= item.TenDsCnbluong.ToLower().Contains(_searchBangLuong.ToLower());
            }
            return result;
        }

        private void SelectAll(bool select, IEnumerable<TlDSCapNhapBangLuongModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }

        private void OnExportBangLuong()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_LUONG_THANG_BHXH_IMPORT);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<TlDSCapNhapBangLuongModel> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();
                    var lstCheDos = _iTlDmCheDoBHXHService.FindAll();
                    var lstCheDosModel = _mapper.Map<ObservableCollection<TlDmCheDoBHXHModel>>(lstCheDos).ToList();
                    ExportChiTietBangLuongBHXHModel lstHeader = new ExportChiTietBangLuongBHXHModel();
                    lstHeader.LstCheDo = lstCheDosModel;
                    List<ExportChiTietBangLuongBHXHModel> lstHeaderItems = new List<ExportChiTietBangLuongBHXHModel>();
                    lstHeaderItems.Add(lstHeader);
                    foreach (var bangLuongIndex in tlDsCapNhapBangLuongModels)
                    {
                        var predicateLT = PredicateBuilder.True<TlBangLuongThangBHXH>();
                        predicateLT = predicateLT.And(x => x.Parent.Equals(bangLuongIndex.Id));
                        var bangLuongThang = _tlBangLuongThangBHXHService.FindByCondition(predicateLT).OrderBy(x => x.MaCbo).ToList();
                        var bangLuongThangModel = _mapper.Map<List<TlBangLuongThangBHXHModel>>(bangLuongThang);
                        ExportChiTietBangLuongBHXHModel bangLuongDoc = new ExportChiTietBangLuongBHXHModel();
                        bangLuongDoc.ListGiaTriDoc = bangLuongThangModel;
                        List<ExportChiTietBangLuongBHXHModel> bangLuongDocItems = new List<ExportChiTietBangLuongBHXHModel>();
                        bangLuongDocItems.Add(bangLuongDoc);
                        List<TlDSCapNhapBangLuongModel> bangLuongIndexItems = new List<TlDSCapNhapBangLuongModel>();
                        bangLuongIndexItems.Add(bangLuongIndex);
                        var lstCanBo = bangLuongThangModel.Select(item => item.MaCbo).Distinct().ToList();
                        List<ExportChiTietBangLuongBHXHModel> bangLuongViewItems = new List<ExportChiTietBangLuongBHXHModel>();
                        int i = 1;
                        var donVi = _tlDmDonViService.FindByMaDonVi(bangLuongIndex.MaCbo);
                        foreach (var maCanBo in lstCanBo)
                        {
                            ExportChiTietBangLuongBHXHModel itemRpt = new ExportChiTietBangLuongBHXHModel();
                            var canBo = _tlDmCanboService.FindByMaCanBo(maCanBo);
                            itemRpt.iStt = i++;
                            itemRpt.Thang = bangLuongIndex.Thang.ToString();
                            itemRpt.Nam = bangLuongIndex.Nam.ToString();
                            itemRpt.MaCbo = canBo != null ? canBo.MaCanBo : "";
                            itemRpt.TenCbo = canBo != null ? canBo.TenCanBo : "";
                            itemRpt.TenDonVi = donVi != null ? donVi.TenDonVi : "";
                            itemRpt.ListGiaTriCheDo = new List<TlBangLuongThangBHXHModel>();
                            foreach (var pc in lstCheDosModel)
                            {
                                TlBangLuongThangBHXHModel giaTri = new TlBangLuongThangBHXHModel();
                                giaTri.MaCheDo = pc.SMaCheDo;
                                var phuCap = bangLuongThangModel.FirstOrDefault(item => item.MaCbo.Equals(maCanBo) && item.MaCheDo.Equals(pc.SMaCheDo));
                                giaTri.GiaTri = phuCap != null ? phuCap.GiaTri ?? 0 : 0;
                                itemRpt.ListGiaTriCheDo.Add(giaTri);
                            }
                            bangLuongViewItems.Add(itemRpt);
                        }
                        // lấy dm don vi
                        var predicateDonVi = PredicateBuilder.True<TlDmDonVi>();
                        predicateDonVi = predicateDonVi.And(x => x.MaDonVi.Equals(bangLuongIndex.MaCbo));
                        var donViItems = _tlDmDonViService.FindByCondition(predicateDonVi).ToList();

                        // lay dm can bo
                        var predicateCanBo = PredicateBuilder.True<TlDmCanBo>();
                        predicateCanBo = predicateCanBo.And(x => lstCanBo.Contains(x.MaCanBo));
                        var lstCanBoItems = _tlDmCanboService.FindByCondition(predicateCanBo).ToList();
                        var canBoItems = _mapper.Map<List<CadresModel>>(lstCanBoItems);

                        // lay phu cap can bo
                        var predicateCanBoPhuCap = PredicateBuilder.True<TlCanBoCheDoBHXH>();
                        predicateCanBoPhuCap = predicateCanBoPhuCap.And(x => lstCanBo.Contains(x.SMaCanBo));
                        var canBoCheDoItems = _tTlCanBoCheDoBHXHService.FindAll(predicateCanBoPhuCap);
                        if (canBoCheDoItems != null)
                        {
                            canBoCheDoItems = canBoCheDoItems.Select(n =>
                            {
                                n.SMaCanBo = ((n.SMaCanBo ?? "") == "" ? null : n.SMaCanBo);
                                n.SMaCheDo = ((n.SMaCheDo ?? "") == "" ? null : n.SMaCheDo);
                                n.STenCheDo = ((n.STenCheDo ?? "") == "" ? null : n.STenCheDo);
                                n.DTuNgay = ((n.DTuNgay ?? null) == null ? null : n.DTuNgay);
                                n.DDenNgay = ((n.DDenNgay ?? null) == null ? null : n.DDenNgay);
                                n.ISoNgayNghi = ((n.ISoNgayNghi ?? 0) == 0 ? null : n.ISoNgayNghi);
                                n.FSoNgayHuongBHXH = ((n.FSoNgayHuongBHXH ?? 0) == 0 ? null : n.FSoNgayHuongBHXH);
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
                        data.Add("CanBoCheDoItems", canBoCheDoItems);

                        fileNamePrefix = string.Format("Bang_Luong_BHXH_Chi_Tiet_Import_{0}_{1}_{2}", donVi != null ? donVi.TenDonVi : "", MonthBHXHSelected.ValueItem, DateTime.Now.Year);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ExportChiTietBangLuongBHXHModel, TlDSCapNhapBangLuongModel, TlDmDonVi, CadresModel, TlCanBoCheDoBHXH>(templateFileName, data);
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

        private void OnPrint(object param)
        {
            var printType = (InsuranceSalaryPrintType)((int)param);
            object content;
            InsuranceSalaryReportViewModel.InsuranceSalaryPrintType = printType;
            InsuranceSalaryReportViewModel.ReportNameTypeValue = (int)printType;
            InsuranceSalaryReportViewModel.IsShowSummary = true;
            InsuranceSalaryReportViewModel.IndexMonth = int.Parse(MonthBHXHSelected.ValueItem);
            InsuranceSalaryReportViewModel.IndexYear = int.Parse(YearBHXHSelected.ValueItem);
            InsuranceSalaryReportViewModel.Init();
            content = new InsuranceSalaryReport
            {
                DataContext = InsuranceSalaryReportViewModel
            };

            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.ROOT_DIALOG, null, null);
            }
        }

        private void OnExportDataDialog()
        {
            InsuranceSalaryExportViewModel.Init();
            InsuranceSalaryExportViewModel.YearIndex = YearBHXHSelected != null ? int.Parse(YearBHXHSelected.ValueItem) : _sessionService.Current.YearOfWork;
            var addView = new InsuranceSalaryExport { DataContext = InsuranceSalaryExportViewModel };
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().KhoaBangLuong.GetValueOrDefault();
            foreach (var ct in lstSelected)
            {
                _tlDsCapNhapBangLuongService.LockOrUnlock(ct.Id, isLock);
                var bangLuong = Items.First(x => x.Id == ct.Id);
                bangLuong.KhoaBangLuong = !ct.KhoaBangLuong.GetValueOrDefault();
            }
            LoadData();
        }

        private void OnLock(object obj)
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockMultiVoucher();
                MessageBoxHelper.Info(msgDone);
            }
        }

        private void ConfirmAggregate(object obj)
        {
            var selectedQtChungTus = Items.Where(x => x.Selected && x.KhoaBangLuong.GetValueOrDefault()).ToList();
            bool isSameMonth = selectedQtChungTus.Select(x => x.Thang).Distinct().Count() == 1;
            if (!isSameMonth)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgChungTuKhacThang), Resources.Alert, (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
                return;
            }
            var checkAllowAggregate = selectedQtChungTus.All(x => x.KhoaBangLuong.GetValueOrDefault());
            if (checkAllowAggregate)
            {
                OnAggregate();
            }
            else
            {
                string message = Resources.ConfirmAggregate;
                MessageBoxResult result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) OnAggregate();
            }
        }

        private void OnAggregate()
        {
            string message = string.Empty;
            var selectedQtChungTus = Items.Where(x => x.Selected && x.KhoaBangLuong.GetValueOrDefault() && !x.IsTongHop.GetValueOrDefault()).ToList();
            if (selectedQtChungTus.Count <= 0)
            {
                return;
            }

            TlDSCapNhapBangLuongModel tlQlThuNopBhxhModel = new TlDSCapNhapBangLuongModel
            {
                Id = Guid.NewGuid(),
                IsTongHop = true,
                STongHop = string.Join(",", Items.Where(x => x.Selected && x.KhoaBangLuong.GetValueOrDefault() == true).Select(s => s.MaCbo)),
                MaCachTl = CachTinhLuong.CACH1,
                Nam = Items.FirstOrDefault(x => x.Selected && x.KhoaBangLuong.GetValueOrDefault() == true).Nam ?? _sessionService.Current.YearOfWork,
                Thang = Items.FirstOrDefault(x => x.Selected && x.KhoaBangLuong.GetValueOrDefault() == true).Thang ?? _sessionService.Current.Month,
                NguoiTao = _sessionService.Current.Principal,
                NgayTaoBL = DateTime.Now,
                MaCbo = _sessionService.Current.IdDonVi,
                //STenDonVi = _sessionService.Current.TenDonVi
            };

            InsuranceSalaryAggregateViewModel.Model = tlQlThuNopBhxhModel;
            InsuranceSalaryAggregateViewModel.DataAllSummary = new ObservableCollection<TlDSCapNhapBangLuongModel>(Items.Where(x => x.Selected && x.KhoaBangLuong == true));
            InsuranceSalaryAggregateViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            InsuranceSalaryAggregateViewModel.Init();
            InsuranceSalaryAggregateViewModel.ShowDialogHost();
        }

        private void OnUpdateSalary()
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

                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Đ/c muốn cập nhật lại bảng lương truy thu không?", Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    List<TlDSCapNhapBangLuongModel> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();

                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        if (!tlDsCapNhapBangLuongModels.IsEmpty())
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                var lstId = tlDsCapNhapBangLuongModels.Select(x => x.Id).ToList();
                                string idXoa = string.Join(",", lstId);
                                string maDonVi = string.Join(",", tlDsCapNhapBangLuongModels.Select(x => x.MaCbo));
                                int thang = int.Parse(MonthBHXHSelected?.ValueItem ?? _sessionInfo.Month.ToString());
                                int nam = int.Parse(YearBHXHSelected?.ValueItem ?? _sessionInfo.YearOfWork.ToString());
                                var dataMapReCalculate = _dmCachTinhLuongTruyThuService.FindAll().Where(x => !string.IsNullOrEmpty(x.MaCachTl)).ToDictionary(key => key.MaCot, value => value.CongThuc);

                                tlDsCapNhapBangLuongModels.Select(x =>
                                {
                                    if (!x.TenDsCnbluong.Contains("cập nhật"))
                                    {
                                        x.TenDsCnbluong += string.Format(" - cập nhật");
                                    }
                                    x.NgayTaoBL = DateTime.Now;
                                    return x;
                                }).ToList();

                                var lstAdd = _mapper.Map<List<TlDsCapNhapBangLuong>>(tlDsCapNhapBangLuongModels);
                                var lstChiTiet = new List<TlBangLuongThangQuery>();
                                var dicBangLuong = tlDsCapNhapBangLuongModels.GroupBy(x => new { x.Thang, x.Nam }).ToDictionary(x => x.Key, x => x.ToList());
                                foreach (var item in dicBangLuong)
                                {
                                    lstChiTiet.AddRange(GetDataUpdateDetail(item.Value, dataMapReCalculate));
                                }
                                var lstChiTietAdd = _mapper.Map<List<TlBangLuongThangTruyThu>>(lstChiTiet);
                                _tlDsCapNhapBangLuongService.DeleteBangLuongTruyThu(thang, nam, maDonVi, "CACH1");
                                _tlBangLuongThangTruyThuService.CapNhatBangLuong(lstAdd, lstChiTietAdd);

                            }
                        }
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            if (dialogResult == DialogResult.Yes)
                            {
                                MessageBoxHelper.Info("Cập nhật dữ liệu thành công.");
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

        private List<TlBangLuongThangQuery> GetDataUpdateDetail(List<TlDSCapNhapBangLuongModel> dataEntities, Dictionary<string, string> dicRecalculate)
        {
            int thang = (int)dataEntities.FirstOrDefault().Thang;
            int nam = (int)dataEntities.FirstOrDefault().Nam;
            string maDonVi = string.Join(",", dataEntities.Select(x => x.MaCbo));
            IEnumerable<TlBangLuongThangQuery> dataLuongTruyThu = _tlBangLuongThangService.GetDataInsertTruyThu(Guid.Empty, thang, nam, maDonVi);
            if (dataLuongTruyThu.IsEmpty() || dicRecalculate.IsEmpty()) return new List<TlBangLuongThangQuery>();
            dataEntities.ForEach(x =>
            {
                dataLuongTruyThu.Where(y => x.MaCbo.Equals(y.MaDonVi)).Select(s => s.Parent = x.Id).ToList();
            });
            dataLuongTruyThu.Where(x => PhuCap.TRUYTHU_SN.Equals(x.MaPhuCap)).ForAll(s => s.GiaTri = s.SoNgayTruyThu);
            dataLuongTruyThu.Where(x => !dicRecalculate.ContainsKey(x.MaPhuCap)).Select(S => S.IsCalculated = true).ToList();
            var listMaCanBo = dataLuongTruyThu.GroupBy(g => new { g.MaHieuCanBo, g.MaDonVi }).ToDictionary(key => key.Key.MaHieuCanBo, value => value.Key.MaDonVi);
            Parallel.ForEach(listMaCanBo, item =>
            {
                var dataCanbo = dataLuongTruyThu.Where(x => !string.IsNullOrEmpty(x.MaDonVi) && !string.IsNullOrEmpty(x.MaHieuCanBo) && !string.IsNullOrEmpty(x.MaPhuCap) && item.Key.Equals(x.MaHieuCanBo) && item.Value.Equals(x.MaDonVi)).ToList();

                Dictionary<string, object> dataMap = dataCanbo.Where(w => w.IsCalculated).ToDictionary(key => key.MaPhuCap, value => (object)value.GiaTri);
                foreach (var itemDetail in dataCanbo)
                {
                    TinhLuongTruyThu(itemDetail.MaPhuCap, dataMap, dicRecalculate, dataCanbo, itemDetail);
                }
            });
            dataLuongTruyThu.Where(x => !dicRecalculate.ContainsKey(x.MaPhuCap)).Select(s => s.GiaTri = 0).ToList();
            return dataLuongTruyThu.ToList();
        }


        private decimal TinhLuongTruyThu(string maPhuCap, Dictionary<string, object> dataMap, Dictionary<string, string> dataReCalculate, List<TlBangLuongThangQuery> dataPhuCap, TlBangLuongThangQuery currentItem)
        {
            if (dataReCalculate.ContainsKey(maPhuCap)) currentItem.CongThuc = dataReCalculate[maPhuCap];
            string congThuc = currentItem.CongThuc;
            if (currentItem.IsCalculated)
            {
                AddDataMap(currentItem, currentItem.GiaTri, dataMap);
                return currentItem.GiaTri;
            }
            List<string> lstPhuCap = congThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (congThuc.Equals(PhuCap.THUETNCN_TT_CONGTHUC))
            {
                var luongTinhThue = dataPhuCap.FirstOrDefault(x => PhuCap.LUONGTHUE_TT.Equals(x.MaPhuCap));
                if (!luongTinhThue.IsCalculated)
                {
                    TinhLuongTruyThu(luongTinhThue.MaPhuCap, dataMap, dataReCalculate, dataPhuCap, luongTinhThue);
                }
                if (luongTinhThue.GiaTri < 0)
                {
                    luongTinhThue.GiaTri = 0;
                }
                //AddDataMap(luongTinhThue, luongTinhThue.GiaTri, dataMap);
                AddDataMap(currentItem, TinhThueTN(luongTinhThue.GiaTri), dataMap);
                return luongTinhThue.GiaTri;
            }
            else
            {
                foreach (var item in lstPhuCap.Where(x => !dataMap.ContainsKey(x)))
                {
                    var phuCap = dataPhuCap.FirstOrDefault(x => x.MaPhuCap.Equals(item));
                    if (phuCap != null)
                    {
                        var congthucPhuCap = dataReCalculate.ContainsKey(phuCap.MaPhuCap) ? dataReCalculate[item] : phuCap.CongThuc;
                        // Nếu phụ cấp có công thức và chưa được tính toán => Đệ qui
                        TinhLuongTruyThu(phuCap.MaPhuCap, dataMap, dataReCalculate, dataPhuCap, phuCap);
                    }
                }
            }

            currentItem.IsCalculated = true;
            var val = EvalExtensions.Execute(congThuc, dataMap);
            AddDataMap(currentItem, decimal.Parse(val.ToString()), dataMap);
            return currentItem.GiaTri;
        }

        private void AddDataMap(TlBangLuongThangQuery itemMap, object value, Dictionary<string, object> dataMap)
        {
            if (dataMap.ContainsKey(itemMap.MaPhuCap))
            {
                dataMap[itemMap.MaPhuCap] = value;
            }
            else
            {
                dataMap.Add(itemMap.MaPhuCap, value);
            }
            itemMap.IsCalculated = true;
            itemMap.GiaTri = (decimal)value;
        }

        private decimal TinhThueTN(decimal luongThuThue)
        {
            var data = _tlBangLuongThangService.FindThue(true).OrderBy(x => x.ThuNhapTu).ToList();
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
    }
}