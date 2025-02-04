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
using VTS.QLNS.CTC.App.View.Salary.SalaryManagement.CalculateSalary.ImportCongThuc;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CalculateSalary.ImportCongThuc;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CalculateSalary
{
    public class CalculateSalaryIndexViewModel : GridViewModelBase<TlCachTinhLuongModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        private readonly ITlDmCachTinhLuongChuanService _cachTinhLuongChuanService;
        private readonly ITlDmCachTinhLuongTruyLinhService _cachTinhLuongTruyLinhService;
        private readonly ITlDmThemCachTinhLuongService _themCachTinhLuongService;
        private readonly ITlDmCachTinhLuongBaoHiemService _cachTinhLuongBaoHiemService;
        private readonly ITlDmCachTinhLuongTruyThuService _cachTinhLuongTruyThuService;
        private readonly IExportService _exportService;
        private readonly TlDmPhuCapService _tlDmPhuCapService;
        private ICollectionView _chiTieuView;
        private CongThucImport _importView;

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_CALCULATE_SALARY_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Danh mục cách tính lương";
        public override Type ContentType => typeof(View.Salary.SalaryManagement.CalculateSalary.CalculateSalaryIndex);
        //public override PackIconKind IconKind => PackIconKind.Calculator;
        public override string Title => "Danh mục cách tính lương";
        public override string Description => "Danh mục các chỉ tiêu tính lương";

        public bool IsEnabled => SelectedItem != null;

        public CalculateSalaryDialogViewModel CalculateSalaryDialogViewModel { get; }

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

        public CongThucImportViewModel CongThucImportViewModel { get; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportCongThucCommand { get; }
        public RelayCommand ImportCongThucCommand { get; }

        public CalculateSalaryIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmCachTinhLuongChuanService cachTinhLuongChuanService,
            ITlDmCachTinhLuongTruyLinhService cachTinhLuongTruyLinhService,
            ITlDmThemCachTinhLuongService themCachTinhLuongService,
            CalculateSalaryDialogViewModel calculateSalaryDialogViewModel,
            CongThucImportViewModel congThucImportViewModel,
            ITlDmCachTinhLuongBaoHiemService cachTinhLuongBaoHiemService,
            IExportService exportService,
            TlDmPhuCapService tlDmPhuCapService,
            ITlDmCachTinhLuongTruyThuService tlDmCachTinhLuongTruyThuService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _cachTinhLuongChuanService = cachTinhLuongChuanService;
            _cachTinhLuongTruyLinhService = cachTinhLuongTruyLinhService;
            _themCachTinhLuongService = themCachTinhLuongService;
            _cachTinhLuongBaoHiemService = cachTinhLuongBaoHiemService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _cachTinhLuongTruyThuService = tlDmCachTinhLuongTruyThuService;

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
                    return;
                }
                switch (SelectedCachTinhLuongItem.MaThemCachTl)
                {
                    case CachTinhLuong.CACH0:
                        IEnumerable<TlDmCachTinhLuongChuan> data = _cachTinhLuongChuanService.FindAll();
                        Items = _mapper.Map<ObservableCollection<TlCachTinhLuongModel>>(data);
                        break;
                    case CachTinhLuong.CACH1:
                        IEnumerable<TlDmCachTinhLuongTruyThu> data1 = _cachTinhLuongTruyThuService.FindAll();
                        Items = _mapper.Map<ObservableCollection<TlCachTinhLuongModel>>(data1);
                        break;
                    case CachTinhLuong.CACH2:
                        IEnumerable<TlDmCachTinhLuongBaoHiem> data2 = _cachTinhLuongBaoHiemService.FindAll();
                        Items = _mapper.Map<ObservableCollection<TlCachTinhLuongModel>>(data2);
                        break;
                    case CachTinhLuong.CACH5:
                        IEnumerable<TlDmCachTinhLuongTruyLinh> data5 = _cachTinhLuongTruyLinhService.FindAll();
                        Items = _mapper.Map<ObservableCollection<TlCachTinhLuongModel>>(data5);
                        break;
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
            var item = (TlCachTinhLuongModel)obj;
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
            CalculateSalaryDialogViewModel.Model = new TlCachTinhLuongModel();
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
                    SelectedCachTinhLuongItem ??= new TlDmThemCachTinhLuongModel();
                    switch (SelectedCachTinhLuongItem.MaThemCachTl)
                    {
                        case CachTinhLuong.CACH0:
                            _cachTinhLuongChuanService.Delete(SelectedItem.Id);
                            break;
                        case CachTinhLuong.CACH1:
                            _cachTinhLuongTruyThuService.Delete(SelectedItem.Id);
                            break;
                        case CachTinhLuong.CACH2:
                            _cachTinhLuongBaoHiemService.Delete(SelectedItem.Id);
                            break;
                        case CachTinhLuong.CACH5:
                            _cachTinhLuongTruyLinhService.Delete(SelectedItem.Id);
                            break;
                    }
                    OnRefresh();
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
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_CONGTHUC_IMPORT_EXPORT);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<TlCachTinhLuongModel> items = new List<TlCachTinhLuongModel>();
                    IEnumerable<TlDmCachTinhLuongChuan> dataLuongChuan = _cachTinhLuongChuanService.FindAll();
                    items.AddRange(_mapper.Map<ObservableCollection<TlCachTinhLuongModel>>(dataLuongChuan));

                    IEnumerable<TlDmCachTinhLuongTruyLinh> dataLuongTruyLinh = _cachTinhLuongTruyLinhService.FindAll();
                    items.AddRange(_mapper.Map<ObservableCollection<TlCachTinhLuongModel>>(dataLuongTruyLinh));

                    IEnumerable<TlDmCachTinhLuongBaoHiem> dataLuongBaoHiem = _cachTinhLuongBaoHiemService.FindAll();
                    items.AddRange(_mapper.Map<ObservableCollection<TlCachTinhLuongModel>>(dataLuongBaoHiem));

                    int stt = 1;
                    foreach (var it in items)
                    {
                        it.Stt = stt++;
                    }

                    //danh muc phu cap
                    List<TlDmPhuCapModel> itemsPhuCap = new List<TlDmPhuCapModel>();
                    itemsPhuCap = _mapper.Map<List<TlDmPhuCapModel>>(_tlDmPhuCapService.FindAll());
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
                    var xlsFile = _exportService.Export<TlCachTinhLuongModel, TlDmPhuCapModel>(templateFileName, data);
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
                _importView = new CongThucImport { DataContext = CongThucImportViewModel };
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
