using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Shared;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Utility.Enum;
using Aspose.Cells.Charts;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewSalaryConfiguration
{
    public class NewSalaryConfigurationIndexViewModel : GridViewModelBase<TlPhuCapMlnsNq104Model>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlPhuCapMlnsNq104Service _tlPhuCapMlnsService;
        private readonly ITlDmPhuCapNq104Service _tlDmPhuCapService;
        private readonly ITlDmThemCachTinhLuongService _tlDmThemCachTinhLuongService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly ITlDmKinhPhiService _tlDmKinhPhiService;
        private readonly ITlDmCapBacNq104Service _tlDmCapBacService;
        private readonly IServiceProvider _provider;
        private readonly IExportService _exportService;
        private ICollectionView _pcMlnsView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_SETTLEMENT_SALARY_CONFIGURATION_CONFIG_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Cấu hình chỉ tiêu lương";
        public override string Description => "Cấu hình chỉ tiêu tính lương với Nguồn ngân sách chi trả";
        public override PackIconKind IconKind => PackIconKind.Cog;
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewSalaryConfiguration.NewSalaryConfiguration);

        private ObservableCollection<TlPhuCapMlnsNq104Model> _phuCapMlnsItems;
        public ObservableCollection<TlPhuCapMlnsNq104Model> PhuCapMlnsItems
        {
            get => _phuCapMlnsItems;
            set => SetProperty(ref _phuCapMlnsItems, value);
        }

        private TlPhuCapMlnsNq104Model _selectedPhuCapMlns;
        public TlPhuCapMlnsNq104Model SelectedPhuCapMlns
        {
            get => _selectedPhuCapMlns;
            set
            {
                SetProperty(ref _selectedPhuCapMlns, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private ObservableCollection<ComboboxItem> _PhuCapItems;
        public ObservableCollection<ComboboxItem> PhuCapItems
        {
            get => _PhuCapItems;
            set => SetProperty(ref _PhuCapItems, value);
        }

        private ObservableCollection<ComboboxItem> _cachTinhLuongItems;
        public ObservableCollection<ComboboxItem> CachTinhLuongItems
        {
            get => _cachTinhLuongItems;
            set => SetProperty(ref _cachTinhLuongItems, value);
        }

        private ObservableCollection<ComboboxItem> _mlnsItems;
        public ObservableCollection<ComboboxItem> MlnsItems
        {
            get => _mlnsItems;
            set => SetProperty(ref _mlnsItems, value);
        }

        private ObservableCollection<ComboboxItem> _mlnsAllItems;
        public ObservableCollection<ComboboxItem> MlnsAllItems
        {
            get => _mlnsAllItems;
            set => SetProperty(ref _mlnsAllItems, value);
        }

        private ObservableCollection<ComboboxItem> _capBacItems;
        public ObservableCollection<ComboboxItem> CapBacItems
        {
            get => _capBacItems;
            set => SetProperty(ref _capBacItems, value);
        }

        private string _searchPhuCap;
        public string SearchPhuCap
        {
            get => _searchPhuCap;
            set => SetProperty(ref _searchPhuCap, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (SetProperty(ref _selectedYear, value) && _pcMlnsView != null)
                {
                    _pcMlnsView.Refresh();
                }
            }
        }

        private int currentRow = -1;
        public bool IsEnabled => SelectedPhuCapMlns != null;
        public bool IsSaveData => PhuCapMlnsItems.Any(item => item.IsModified || item.IsDeleted);

        public RelayCommand OpenReferencePopupCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportPhuCapChuaCauHinhCommand { get; }

        public NewSalaryConfigurationIndexViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IServiceProvider serviceProvider,
            ITlPhuCapMlnsNq104Service tlPhuCapMlnsService,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            ITlDmThemCachTinhLuongService tlDmThemCachTinhLuongService,
            INsMucLucNganSachService nsMucLucNganSachService,
            ITlDmKinhPhiService tlDmKinhPhiService,
            ITlDmCapBacNq104Service tlDmCapBacService,
            IExportService exportService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _tlPhuCapMlnsService = tlPhuCapMlnsService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmThemCachTinhLuongService = tlDmThemCachTinhLuongService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _tlDmKinhPhiService = tlDmKinhPhiService;
            _tlDmCapBacService = tlDmCapBacService;
            _provider = serviceProvider;
            _exportService = exportService;

            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
            SearchCommand = new RelayCommand(obj => _pcMlnsView.Refresh());
            ExportPhuCapChuaCauHinhCommand = new RelayCommand(obj => OnExportPhuCapChuaCauHinh());
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            LoadYears();
            LoadData();
            LoadPhuCap();
            LoadCachTinhLuong();
            LoadMLNS();
            LoadCapBac();
        }

        private void LoadData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    e.Result = _tlPhuCapMlnsService.FindAll().OrderBy(x => x.MaPhuCap);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        PhuCapMlnsItems = _mapper.Map<ObservableCollection<TlPhuCapMlnsNq104Model>>(e.Result);
                        if (PhuCapMlnsItems.Count > 0)
                        {
                            foreach (var item in PhuCapMlnsItems)
                            {
                                item.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        _pcMlnsView = CollectionViewSource.GetDefaultView(PhuCapMlnsItems);
                        _pcMlnsView.Filter = PhuCapMlnsFilter;
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 30; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            SelectedYear = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());

        }

        private bool PhuCapMlnsFilter(object obj)
        {
            var result = true;
            var item = (TlPhuCapMlnsNq104Model)obj;
            if (SelectedYear != null)
            {
                result &= item.Nam == int.Parse(SelectedYear.ValueItem);
            }
            if (!string.IsNullOrEmpty(_searchPhuCap))
            {
                result &= item.MaPhuCap.ToLower().Contains(SearchPhuCap.ToLower())
                                || item.TenPhuCap.ToLower().Contains(SearchPhuCap.ToLower())
                                || item.MoTa.ToLower().Contains(SearchPhuCap.ToLower())
                                || item.NguonNganSach.ToLower().Contains(SearchPhuCap.ToLower());
            }

            return result;
        }

        private void LoadPhuCap()
        {
            try
            {
                var data = _tlDmPhuCapService.FindAll().OrderBy(x => x.MaPhuCap);
                PhuCapItems = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCachTinhLuong()
        {
            try
            {
                var data = _tlDmThemCachTinhLuongService.FindAll();
                CachTinhLuongItems = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadMLNS()
        {
            try
            {
                var data = _nsMucLucNganSachService.FindForPhuCap(_sessionService.Current.YearOfWork);
                var dataAllMlns = _nsMucLucNganSachService.FindByLnsAndNam("1010000", _sessionService.Current.YearOfWork).OrderBy(x => x.XauNoiMa);
                var lstMlnsModel = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(dataAllMlns);

                MlnsItems = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
                MlnsAllItems = _mapper.Map<ObservableCollection<ComboboxItem>>(lstMlnsModel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCapBac()
        {
            try
            {
                CapBacItems = new ObservableCollection<ComboboxItem>();
                var data = _tlDmCapBacService.FindParent();
                if (data == null || !data.Any()) return;
                data = data.Where(n => (string.IsNullOrEmpty(n.Parent) && n.MaCb != "3") || n.Parent == "3");
                var lstdata= _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
                CapBacItems = _mapper.Map<ObservableCollection<ComboboxItem>>(lstdata);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlPhuCapMlnsNq104Model tlPhuCapMlnsModel = (TlPhuCapMlnsNq104Model)sender;
            if (args.PropertyName == nameof(tlPhuCapMlnsModel.IdPhuCap))
            {
                tlPhuCapMlnsModel.MaPhuCap = PhuCapItems.FirstOrDefault(x => x.Id == tlPhuCapMlnsModel.IdPhuCap).DisplayItem;
                tlPhuCapMlnsModel.TenPhuCap = PhuCapItems.FirstOrDefault(x => x.Id == tlPhuCapMlnsModel.IdPhuCap).HiddenValue;
                SelectedPhuCapMlns = tlPhuCapMlnsModel;
            }
            if (args.PropertyName == nameof(tlPhuCapMlnsModel.IdMlns))
            {
                var mlns = MlnsAllItems.FirstOrDefault(X => X.Id == tlPhuCapMlnsModel.IdMlns);
                if (mlns != null)
                {
                    tlPhuCapMlnsModel.XauNoiMa = mlns.HiddenValue;
                    tlPhuCapMlnsModel.MoTa = mlns.DisplayItem;
                }
            }
            if (args.PropertyName == nameof(tlPhuCapMlnsModel.IdCachTinhLuong))
            {
                tlPhuCapMlnsModel.MaCachTl = CachTinhLuongItems.FirstOrDefault(x => x.Id == tlPhuCapMlnsModel.IdCachTinhLuong).HiddenValue;
            }
            tlPhuCapMlnsModel.IsModified = true;
            OnPropertyChanged(nameof(SelectedPhuCapMlns));
            OnPropertyChanged(nameof(PhuCapMlnsItems));
        }

        protected override void OnAdd()
        {
            if (PhuCapMlnsItems.Count == 0 || SelectedPhuCapMlns == null)
            {
                TlPhuCapMlnsNq104Model tlPhuCapMlnsModel = new TlPhuCapMlnsNq104Model();
                tlPhuCapMlnsModel.DateCreated = DateTime.Now;
                tlPhuCapMlnsModel.UserCreator = _sessionService.Current.Principal;
                tlPhuCapMlnsModel.Nam = _sessionService.Current.YearOfWork;
                tlPhuCapMlnsModel.PropertyChanged += DetailModel_PropertyChanged;
                PhuCapMlnsItems.Add(tlPhuCapMlnsModel);
            }
            else
            {
                TlPhuCapMlnsNq104Model sourceItem = SelectedPhuCapMlns;
                TlPhuCapMlnsNq104Model targetItem = ObjectCopier.Clone(sourceItem);

                currentRow = PhuCapMlnsItems.IndexOf(SelectedPhuCapMlns);

                targetItem.Id = Guid.Empty;
                targetItem.IdCachTinhLuong = sourceItem.IdCachTinhLuong;
                targetItem.MaCachTl = sourceItem.MaCachTl;
                targetItem.TenPhuCap = sourceItem.TenPhuCap;
                targetItem.IdNguonNganSach = sourceItem.IdNguonNganSach;
                targetItem.IdPhuCap = sourceItem.IdPhuCap;
                targetItem.IdMlns = sourceItem.IdMlns;
                targetItem.DateCreated = DateTime.Now;
                targetItem.UserCreator = _sessionService.Current.Principal;
                targetItem.IsModified = true;

                targetItem.PropertyChanged += DetailModel_PropertyChanged;
                PhuCapMlnsItems.Insert(currentRow + 1, targetItem);
            }
            OnPropertyChanged(nameof(PhuCapMlnsItems));
        }

        protected override void OnRefresh()
        {
            Init();
        }

        public override void OnSave()
        {
            List<TlPhuCapMlnsNq104Model> listAdd = PhuCapMlnsItems.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null)).ToList();
            List<TlPhuCapMlnsNq104Model> listEdit = PhuCapMlnsItems.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlPhuCapMlnsNq104Model> listDelete = PhuCapMlnsItems.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            if (listAdd != null && listAdd.Count > 0)
            {
                var lstPhuCapMlns = _mapper.Map<List<TlPhuCapMlnNq104>>(listAdd);
                _tlPhuCapMlnsService.AddRange(lstPhuCapMlns);
            }

            if (listEdit != null && listEdit.Count > 0)
            {
                foreach (var item in listEdit)
                {
                    TlPhuCapMlnNq104 tlPhuCapMln = _mapper.Map<TlPhuCapMlnNq104>(item);
                    tlPhuCapMln.DateModified = DateTime.Now;
                    tlPhuCapMln.UserModifier = _sessionService.Current.Principal;
                    _tlPhuCapMlnsService.Update(tlPhuCapMln);
                }
            }

            if (listDelete != null && listDelete.Count > 0)
            {
                foreach (var item in listDelete)
                {
                    _tlPhuCapMlnsService.Delete(item.Id);
                }
            }
            MessageBoxHelper.Info("Lưu thành công.");
            LoadData();
        }

        protected override void OnDelete()
        {
            if (PhuCapMlnsItems != null && PhuCapMlnsItems.Count > 0 && SelectedPhuCapMlns != null)
            {
                SelectedPhuCapMlns.IsDeleted = !SelectedPhuCapMlns.IsDeleted;
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void OnOpenReferencePopup(object obj)
        {
            try
            {
                DataGrid dataGrid = obj as DataGrid;
                if (dataGrid.CurrentCell.Column.SortMemberPath.Equals("NguonNganSach"))
                {
                    GenericControlCustomViewModel<TLDmKinhPhiModel, NsMucLucNganSach, TlDmKinhPhiService> viewModelBase = new GenericControlCustomViewModel<TLDmKinhPhiModel, NsMucLucNganSach, TlDmKinhPhiService>((TlDmKinhPhiService)_tlDmKinhPhiService, _mapper, _sessionService, _provider)
                    {
                        Name = "Danh mục kinh phí thường xuyên",
                        Title = "Danh mục kinh phí thường xuyên",
                        Description = "Danh mục kinh phí thường xuyên",
                        IsDialog = true
                    };

                    GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                    GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                    {
                        DataContext = genericControlCustomWindowViewModel
                    };

                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        try
                        {
                            TLDmKinhPhiModel nsMucLucNganSach = (TLDmKinhPhiModel)obj;
                            if (nsMucLucNganSach != null)
                            {
                                foreach (var item in PhuCapMlnsItems)
                                {
                                    if ((SelectedPhuCapMlns.XauNoiMa == null || item.XauNoiMa.Equals(SelectedPhuCapMlns.XauNoiMa)) && item.Nam == SelectedPhuCapMlns.Nam && item.DateCreated == SelectedPhuCapMlns.DateCreated)
                                    {
                                        item.NguonNganSach = nsMucLucNganSach.MoTa;
                                        //item.XauNoiMa = nsMucLucNganSach.XauNoiMa;
                                        item.IdMlns = nsMucLucNganSach.Id;
                                        item.Lns = nsMucLucNganSach.Lns;
                                        item.L = nsMucLucNganSach.L;
                                        item.K = nsMucLucNganSach.K;
                                        item.M = nsMucLucNganSach.M;
                                        item.Tm = nsMucLucNganSach.TM;
                                        item.Ttm = nsMucLucNganSach.TTM;
                                        item.Ng = nsMucLucNganSach.NG;
                                        item.STNG = nsMucLucNganSach.TNG;
                                        item.STNG1 = nsMucLucNganSach.TNG1;
                                        item.STNG2 = nsMucLucNganSach.TNG2;
                                        item.STNG3 = nsMucLucNganSach.TNG3;
                                        var XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", item.Lns, item.L, item.K, item.M, item.Tm, item.Ttm, item.Ng);
                                        if (string.IsNullOrEmpty(item.STNG))
                                        {
                                            //item.XauNoiMa = nsMucLucNganSach.XauNoiMa;
                                            item.XauNoiMa = XauNoiMa;
                                        }
                                        else if (string.IsNullOrEmpty(item.STNG1))
                                        {
                                            item.XauNoiMa = string.Format("{0}-{1}", XauNoiMa, item.STNG);
                                        }
                                        else if (string.IsNullOrEmpty(item.STNG2))
                                        {
                                            item.XauNoiMa = string.Format("{0}-{1}-{2}", XauNoiMa, item.STNG, item.STNG1);
                                        }
                                        else if (string.IsNullOrEmpty(item.STNG3))
                                        {
                                            item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}", XauNoiMa, item.STNG, item.STNG1, item.STNG2);
                                        }
                                        else
                                        {
                                            item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}", XauNoiMa, item.STNG, item.STNG1, item.STNG2, item.STNG3);

                                        }
                                    }
                                }
                            }
                            OnPropertyChanged(nameof(PhuCapMlnsItems));
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
                        GenericControlCustomWindow.Close();
                    };
                    viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                    GenericControlCustomWindow.Show();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExportPhuCapChuaCauHinh()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_PHU_CAP_CHUA_MAP_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    var lstPhuCapMap = _tlPhuCapMlnsService.FindAll();
                    var lstMaPhuCapMap = lstPhuCapMap.Select(x => x.MaPhuCap).Distinct().ToList();
                    var lsPhuCapChuaMap = _tlDmPhuCapService.FindAll(x => x.IDinhDang.HasValue && x.IDinhDang == 0 && !lstMaPhuCapMap.Contains(x.MaPhuCap)).OrderBy(x => x.Parent).ToList();
                    var items = _mapper.Map<List<TlDmPhuCapNq104Model>>(lsPhuCapChuaMap);
                    int stt = 1;
                    foreach (var it in items)
                    {
                        it.SttExport = stt++;
                        if (!string.IsNullOrEmpty(it.Parent))
                        {
                            var pcCha = _tlDmPhuCapService.FindAll().FirstOrDefault(x => x.MaPhuCap.Equals(it.Parent));
                            if (pcCha != null)
                            {
                                it.TenPhuCapCha = pcCha.TenPhuCap;
                            }
                        }
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Items", items);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlDmPhuCapNq104Model>(templateFileName, data);
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
    }
}
