using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagement.NewCalculateSalary.NewImportCongThuc;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewCalculateSalary.NewImportCongThuc;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewCalculateSalary
{
    public class NewCalculateSalaryIndexViewModel : GridViewModelBase<TlCachTinhLuongNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        private readonly ITlDmCachTinhLuongChuanNq104Service _cachTinhLuongChuanService;
        private readonly ITlDmCachTinhLuongTruyLinhNq104Service _cachTinhLuongTruyLinhService;
        private readonly ITlDmThemCachTinhLuongService _themCachTinhLuongService;
        private readonly ITlDmCachTinhLuongBaoHiemNq104Service _cachTinhLuongBaoHiemService;
        private readonly IExportService _exportService;
        private readonly TlDmPhuCapNq104Service _tlDmPhuCapService;
        private ICollectionView _chiTieuView;
        private NewCongThucImport _importView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_CALCULATE_SALARY_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Danh mục cách tính lương";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagement.NewCalculateSalary.NewCalculateSalaryIndex);
        //public override PackIconKind IconKind => PackIconKind.Calculator;
        public override string Title => "Danh mục cách tính lương";
        public override string Description => "Danh mục các chỉ tiêu tính lương";

        public bool IsEnabled => SelectedItem != null;

        public NewCalculateSalaryDialogViewModel CalculateSalaryDialogViewModel { get; }

        private ObservableCollection<TlDmThemCachTinhLuongModel> _cachTinhLuongItems;
        public ObservableCollection<TlDmThemCachTinhLuongModel> CachTinhLuongItems
        {
            get => _cachTinhLuongItems;
            set => SetProperty(ref _cachTinhLuongItems, value);
        }

        private TlDmThemCachTinhLuongModel _selectedCachTinhLuongItem;
        public TlDmThemCachTinhLuongModel SelectedCachTinhLuongItem
        {
            get => _selectedCachTinhLuongItem;
            set
            {
                SetProperty(ref _selectedCachTinhLuongItem, value);
                LoadData();
            }
        }

        private string _searchChiTieu;
        public string SearchChiTieu
        {
            get => _searchChiTieu;
            set
            {
                SetProperty(ref _searchChiTieu, value);
            }
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set
            {
                if (SetProperty(ref _months, value) && _chiTieuView != null)
                {
                    _chiTieuView.Refresh();
                }
            }
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set
            {
                if (SetProperty(ref _years, value) && _chiTieuView != null)
                {
                    _chiTieuView.Refresh();
                }
            }
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set => SetProperty(ref _yearSelected, value);
        }

        public string ComboboxDisplayMemberPath => nameof(SelectedCachTinhLuongItem.TenThemCachTl);

        public NewCongThucImportViewModel CongThucImportViewModel { get; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportCongThucCommand { get; }
        public RelayCommand ImportCongThucCommand { get; }

        public NewCalculateSalaryIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmCachTinhLuongChuanNq104Service cachTinhLuongChuanService,
            ITlDmCachTinhLuongTruyLinhNq104Service cachTinhLuongTruyLinhService,
            ITlDmThemCachTinhLuongService themCachTinhLuongService,
            NewCalculateSalaryDialogViewModel calculateSalaryDialogViewModel,
            NewCongThucImportViewModel congThucImportViewModel,
            ITlDmCachTinhLuongBaoHiemNq104Service cachTinhLuongBaoHiemService,
            IExportService exportService,
            TlDmPhuCapNq104Service tlDmPhuCapService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _cachTinhLuongChuanService = cachTinhLuongChuanService;
            _cachTinhLuongTruyLinhService = cachTinhLuongTruyLinhService;
            _themCachTinhLuongService = themCachTinhLuongService;
            _cachTinhLuongBaoHiemService = cachTinhLuongBaoHiemService;
            _tlDmPhuCapService = tlDmPhuCapService;

            CalculateSalaryDialogViewModel = calculateSalaryDialogViewModel;
            CongThucImportViewModel = congThucImportViewModel;

            SearchCommand = new RelayCommand(o => _chiTieuView.Refresh());
            ExportCongThucCommand = new RelayCommand(o => OnExportData());
            _exportService = exportService;
            ImportCongThucCommand = new RelayCommand(o => OnImportData());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _searchChiTieu = string.Empty;
            LoadMonths();
            LoadYears();
            LoadData();
            LoadThemCachTinhLuong();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                var month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem("Năm " + i, i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadData()
        {
            try
            {
                if (SelectedCachTinhLuongItem == null)
                {
                    Items = null;
                }
                if (SelectedCachTinhLuongItem != null && CachTinhLuong.CACH0.Equals(SelectedCachTinhLuongItem.MaThemCachTl))
                {
                    IEnumerable<TlDmCachTinhLuongChuanNq104> data = _cachTinhLuongChuanService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork);
                    Items = _mapper.Map<ObservableCollection<TlCachTinhLuongNq104Model>>(data);
                }
                if (SelectedCachTinhLuongItem != null && CachTinhLuong.CACH5.Equals(SelectedCachTinhLuongItem.MaThemCachTl))
                {
                    IEnumerable<TlDmCachTinhLuongTruyLinhNq104> data = _cachTinhLuongTruyLinhService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork);
                    Items = _mapper.Map<ObservableCollection<TlCachTinhLuongNq104Model>>(data);
                }
                if (SelectedCachTinhLuongItem != null && CachTinhLuong.CACH2.Equals(SelectedCachTinhLuongItem.MaThemCachTl))
                {
                    IEnumerable<TlDmCachTinhLuongBaoHiemNq104> data = _cachTinhLuongBaoHiemService.FindAll();
                    Items = _mapper.Map<ObservableCollection<TlCachTinhLuongNq104Model>>(data);
                }

                _chiTieuView = CollectionViewSource.GetDefaultView(Items);
                if (_chiTieuView != null)
                {
                    _chiTieuView.Filter = ChiTieuFilter;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ChiTieuFilter(object obj)
        {
            if (string.IsNullOrEmpty(_searchChiTieu))
            {
                return true;
            }
            var item = (TlCachTinhLuongNq104Model)obj;
            var condition = item.MaCot.ToLower().Contains(_searchChiTieu.ToLower())
                || item.TenCot.ToLower().Contains(_searchChiTieu.ToLower())
                || item.CongThuc.ToLower().Contains(_searchChiTieu.ToLower());
            return condition;
        }

        private void LoadThemCachTinhLuong()
        {
            IEnumerable<TlDmThemCachTinhLuong> dataCachTinhLuong = _themCachTinhLuongService.FindAll();
            CachTinhLuongItems = _mapper.Map<ObservableCollection<TlDmThemCachTinhLuongModel>>(dataCachTinhLuong);
            SelectedCachTinhLuongItem = CachTinhLuongItems.FirstOrDefault(x => x.MaThemCachTl == CachTinhLuong.CACH0);
        }

        protected override void OnAdd()
        {
            CalculateSalaryDialogViewModel.ViewState = Utility.Enum.FormViewState.ADD;
            CalculateSalaryDialogViewModel.Model = new TlCachTinhLuongNq104Model();
            CalculateSalaryDialogViewModel.Model.Thang = int.Parse(MonthSelected.ValueItem);
            CalculateSalaryDialogViewModel.Model.Nam = int.Parse(YearSelected.ValueItem);
            CalculateSalaryDialogViewModel.SavedAction = obj => this.OnRefresh();
            CalculateSalaryDialogViewModel.Init();
            CalculateSalaryDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem != null)
                {
                    CalculateSalaryDialogViewModel.Model = SelectedItem;
                    CalculateSalaryDialogViewModel.ViewState = Utility.Enum.FormViewState.UPDATE;
                    CalculateSalaryDialogViewModel.BeforeChanged = SelectedItem.MaCot;
                    CalculateSalaryDialogViewModel.Init();
                    CalculateSalaryDialogViewModel.SavedAction = obj => this.OnRefresh();
                    CalculateSalaryDialogViewModel.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Đồng chí chắc chắn muốn xóa chỉ tiêu này?", Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (CachTinhLuong.CACH0.Equals(SelectedCachTinhLuongItem.MaThemCachTl))
                    {
                        _cachTinhLuongChuanService.Delete(SelectedItem.Id);
                        OnRefresh();
                    }
                    if (CachTinhLuong.CACH5.Equals(SelectedCachTinhLuongItem.MaThemCachTl))
                    {
                        _cachTinhLuongTruyLinhService.Delete(SelectedItem.Id);
                        OnRefresh();
                    }
                    if (CachTinhLuong.CACH2.Equals(SelectedCachTinhLuongItem.MaThemCachTl))
                    {
                        _cachTinhLuongBaoHiemService.Delete(SelectedItem.Id);
                        OnRefresh();
                    }
                    SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabled));
        }

        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_CONGTHUC_IMPORT_EXPORT_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<TlCachTinhLuongNq104Model> items = new List<TlCachTinhLuongNq104Model>();
                    IEnumerable<TlDmCachTinhLuongChuanNq104> dataLuongChuan = _cachTinhLuongChuanService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork);
                    items.AddRange(_mapper.Map<ObservableCollection<TlCachTinhLuongNq104Model>>(dataLuongChuan));

                    IEnumerable<TlDmCachTinhLuongTruyLinhNq104> dataLuongTruyLinh = _cachTinhLuongTruyLinhService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork);
                    items.AddRange(_mapper.Map<ObservableCollection<TlCachTinhLuongNq104Model>>(dataLuongTruyLinh));

                    IEnumerable<TlDmCachTinhLuongBaoHiemNq104> dataLuongBaoHiem = _cachTinhLuongBaoHiemService.FindAll();
                    items.AddRange(_mapper.Map<ObservableCollection<TlCachTinhLuongNq104Model>>(dataLuongBaoHiem));

                    int stt = 1;
                    foreach (var it in items)
                    {
                        it.Stt = stt++;
                    }

                    //danh muc phu cap
                    List<TlDmPhuCapNq104Model> itemsPhuCap = new List<TlDmPhuCapNq104Model>();
                    itemsPhuCap = _mapper.Map<List<TlDmPhuCapNq104Model>>(_tlDmPhuCapService.FindAll());
                    itemsPhuCap = itemsPhuCap.OrderBy(x => x.XauNoiMa).ToList();
                    stt = 1;
                    foreach (var it in itemsPhuCap)
                    {
                        it.SttExport = stt++;
                    }


                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Items", items);
                    data.Add("ItemsPhuCap", itemsPhuCap);
                    fileNamePrefix = string.Format("CongThuc_Luong_Import_{0}_{1}",
                        MonthSelected != null ? int.Parse(MonthSelected.ValueItem) : DateTime.Now.Month, YearSelected != null ? int.Parse(YearSelected.ValueItem) : DateTime.Now.Year);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlCachTinhLuongNq104Model, TlDmPhuCapNq104Model>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

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
            try
            {
                CongThucImportViewModel.Init();
                _importView = new NewCongThucImport { DataContext = CongThucImportViewModel };
                CongThucImportViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    //OnShowDetail((ExpertiseModel)obj);
                };
                _importView.Show();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
