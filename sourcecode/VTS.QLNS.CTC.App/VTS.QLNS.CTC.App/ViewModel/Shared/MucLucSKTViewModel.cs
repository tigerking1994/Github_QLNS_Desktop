using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.View.Shared.Import;
using VTS.QLNS.CTC.App.ViewModel.ImportViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class MucLucSKTViewModel : GridViewModelBase<SktMucLucModel>
    {
        public ILog _logger;
        public SktMucLucService _service;
        public IMapper _mapper;
        public IServiceProvider _serviceProvider;
        public ISessionService _sessionService;
        public ICollectionView _dataCollectionView;
        public IExportService _exportService;
        public ISysAuditLogService _auditLog;
        public AuthenticationInfo _authenticationInfo;
        public Dictionary<string, bool> _checkboxDictionary;
        public Dictionary<string, string> _formatDictionary;
        public Dictionary<string, int> _currentCodeValDictionary;
        public string _currentCol;
        public bool _isFirstLoad;
        public bool _isFirstUpdateSelectedChucNang = true;

        public string POPUP_SUFFIX = "popupbox";
        private const string TEXTBOX_SUFFIX = "txtbox";
        private string maConcatenation = "";
        private ICollection<SktMucLucModel> _filterResult = new HashSet<SktMucLucModel>();

        public override string FuncCode => NSFunctionCode.CATEGORY_BUDGET_MLSKT;
        public override string Name => "Danh mục MLSKT";
        public override string Title => "Danh mục mục lục số kiểm tra";
        public override string Description => "Chỉnh sửa thông tin mục lục ngân sách";
        public string ExportJsonFileName = "mlns.json";
        public string TemplateFileName = "rpt_dm_mlskt.xlsx";
        public Type ImportModelType = typeof(DanhMucMucLucSKTImportModel);
        public string DataTemplateFileName = "rpt_dm_mlskt_template_data.xlsx";

        public override Type ContentType => typeof(MuclucSKTView);
        public Type ModelType => typeof(SktMucLucModel);
        public bool IsEdit => SelectedItem != null;

        private bool _isDialog;
        public bool IsDialog
        {
            get => _isDialog;
            set
            {
                SetProperty(ref _isDialog, value);
                OnPropertyChanged(nameof(IsDialog));
            }
        }

        public bool IsDanhMucChuky { get; set; }
        public bool IsMultipleSelect { get; set; }
        public bool IsEnabledSaveBtn => SelectedItem != null;
        public string Hint => IsDialog ? "Ấn phím space để chọn checkbox nếu có" : "";
        public string Total => " (Tổng số bản ghi: " + Items.Count() + ")";
        public string ModelName => "dgdDataMLSKT";

        public bool AfterSelectAll;

        private string _filtertext;
        public string FilterText
        {
            get => _filtertext;
            set => SetProperty(ref _filtertext, value);
        }

        private SktMucLucModel _filterModel;
        public SktMucLucModel FilterModel
        {
            get => _filterModel;
            set => SetProperty(ref _filterModel, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var unSelected = Items.Where(item => !item.IsSelected).ToList();
                    return unSelected.Count == 0;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    // SelectAll(value.Value, Items);
                }
            }
        }

        public MuclucSKTView MucLucSKTView { get; set; }

        public ObservableCollection<ComboboxItem> LstTrangThai { get; set; }
        public ObservableCollection<ComboboxItem> BHangChaChiTietToi { get; set; }
        public ObservableCollection<ComboboxItem> CPChiTietToi { get; set; }

        public SktMucLucModel SktMucLucModel { get; set; }

        public RelayCommand AutoGeneratingColumnsCommand { get; }
        public RelayCommand SaveReferenceCommand { get; }
        public RelayCommand SelectedCellChangeCommand { get; }
        public RelayCommand OpenFilterPopupCommand { get; }
        public RelayCommand OpenReferencePopupCommand { get; }
        public RelayCommand OnLoadedFormatPopupCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportJsonCommand { get; }
        public RelayCommand ImportExcelCommand { get; }
        public RelayCommand UpdateDmChuKyCommand { get; }
        public RelayCommand AddChildCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ViewDetailCommand { get; }
        public RelayCommand FilterCommand { get; }
        public RelayCommand EscapteFilterCommand { get; }
        public RelayCommand SelectMlnsParentCommand { get; }

        public MucLucSKTViewModel(ISktMucLucService service, IMapper mapper, ISessionService sessionService, IExportService exportService, IServiceProvider serviceProvider)
        {
            _service = service as SktMucLucService;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _serviceProvider = serviceProvider;
            SaveReferenceCommand = new RelayCommand(obj => OnSaveReference());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            SelectedCellChangeCommand = new RelayCommand(obj => _currentCol = obj.ToString());
            OpenFilterPopupCommand = new RelayCommand(obj => ToggleFilterPopup(true));
            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
            ExportCommand = new RelayCommand(obj => OnExportExcel());
            ImportExcelCommand = new RelayCommand(obj => OnImportExcel());
            AddChildCommand = new RelayCommand(obj => OnAddChild(obj));
            // ViewDetailCommand = new RelayCommand(obj => OnViewDetail(), obj => !IsDialog);
            FilterCommand = new RelayCommand(o => OnFilterDataByColumn(o.ToString()));
            EscapteFilterCommand = new RelayCommand(o => ToggleFilterPopup(false, o.ToString() + POPUP_SUFFIX));
            SelectMlnsParentCommand = new RelayCommand(o => OnSelectMlsktParent());
        }

        public override void Init()
        {
            try
            {
                base.Init();
                _authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
                _checkboxDictionary = new Dictionary<string, bool>();
                _formatDictionary = new Dictionary<string, string>();
                _currentCodeValDictionary = new Dictionary<string, int>();
                _filterModel = new SktMucLucModel();
                if (_sessionService.Current.AutoGenerateDataSetting.ContainsKey(typeof(SktMucLucModel).Name))
                {
                    _formatDictionary = _sessionService.Current.AutoGenerateDataSetting[typeof(SktMucLucModel).Name];
                }
                else
                {
                    _sessionService.Current.AutoGenerateDataSetting[typeof(SktMucLucModel).Name] = _formatDictionary;
                }
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var data = _service.FindAll(_authenticationInfo).OrderBy(t=>t.SSttBC).ToList();
            Items = _mapper.Map<ObservableCollection<SktMucLucModel>>(data);
            foreach (var item in Items)
            {
                item.MLNS = string.Join("; ", item.SktMucLucMaps.Select(x => x.SNsXauNoiMa));
                item.PropertyChanged += Item_PropertyChanged;
            }
            OnPropertyChanged();
            _isFirstLoad = true;
            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            _dataCollectionView.Filter = ItemsViewFilter;
            OnPropertyChanged(nameof(Items));
            _isFirstLoad = false;
        }

        protected override void OnItemsChanged()
        {
            OnPropertyChanged("Total");
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals(nameof(ModelBase.IsDeleted)) && !e.PropertyName.Equals(nameof(ModelBase.IsSelected)) && !e.PropertyName.Equals(nameof(ModelBase.IsModified)))
                ((ModelBase)sender).IsModified = true;

            else if (IsMultipleSelect && e.PropertyName.Equals(nameof(ModelBase.IsSelected)))
            {
                OnPropertyChanged(nameof(IsAllItemsSelected));
            }
            else if (e.PropertyName == nameof(SktMucLucModel.IsDeleted))
            {
                IEnumerable<SktMucLucModel> models = Items;
                SktMucLucModel sktMucLucModel = sender as SktMucLucModel;
                SktMucLucModel_OnDeleteMLNS(sktMucLucModel, models);
            }
        }

        private bool ItemsViewFilter(object obj)
        {
            if (_isFirstLoad)
            {
                return true;
            }
            bool result = true;
            var item = (SktMucLucModel)obj;
            result = FilterFunction(FilterModel, item);
            if (!result && item.BHangCha)
            {
                result = this.maConcatenation.Contains(item.SKyHieu + "-");
            }
            return result;
        }

        private bool FilterFunction(SktMucLucModel filterModel, SktMucLucModel item)
        {
            var result = true;
            if (!string.IsNullOrEmpty(filterModel.SM))
                result = result && item.SM.ToLower().StartsWith(filterModel.SM.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SNGCha))
                result = result && item.SNGCha.ToLower().StartsWith(filterModel.SNGCha.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SNg))
                result = result && item.SNg.ToLower().StartsWith(filterModel.SNg.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SSTT))
                result = result && item.SSTT.ToLower().StartsWith(filterModel.SSTT.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SSttBC))
                result = result && item.SSttBC.ToLower().StartsWith(filterModel.SSttBC.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SKyHieu))
                result = result && item.SKyHieu.ToLower().StartsWith(filterModel.SKyHieu.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SLoaiNhap))
                result = result && item.SLoaiNhap.ToLower().Contains(filterModel.SLoaiNhap.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SMoTa))
                result = result && item.SMoTa.ToLower().Contains(filterModel.SMoTa.ToLower());
            if (!string.IsNullOrEmpty(filterModel.KyHieuCha))
                result = result && item.KyHieuCha.ToLower().StartsWith(filterModel.KyHieuCha.ToLower());
            if (!string.IsNullOrEmpty(filterModel.MLNS))
                result = result && item.MLNS.ToLower().Contains(filterModel.MLNS.ToLower());
            return result;
        }

        protected override void OnAdd(object obj)
        {
            try
            {
                DataGrid dgdData = FindDatagrid();
                this.CancelEditData(dgdData);

                int currentRow = Items.Count - 1;
                SktMucLucModel newRow;
                if (SelectedItem == null)
                {
                    newRow = new SktMucLucModel();
                    CustomValueProps(newRow, SelectedItem);
                    Items.Add(newRow);
                }
                else
                {
                    currentRow = Items.IndexOf(SelectedItem);
                    newRow = ObjectCopier.Clone(SelectedItem);
                    CustomValueProps(newRow, SelectedItem);
                    Items.Insert(currentRow + 1, newRow);
                }
                newRow.PropertyChanged += Item_PropertyChanged;
                OnPropertyChanged(nameof(Items));

                var cell = new DataGridCellInfo(Items[currentRow + 1], dgdData.Columns[0]);
                dgdData.ScrollIntoView(Items[currentRow + 1], dgdData.Columns[0]);
                dgdData.CurrentCell = cell;
                dgdData.BeginEdit();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAddChild(object obj)
        {
            try
            {
                if (SelectedItem == null)
                    return;
                DataGrid dgdData = FindDatagrid();
                this.CancelEditData(dgdData);
                SktMucLucModel parent = SelectedItem;
                parent.BHangCha = true;
                SelectedItem.BHangCha = true;
                int currentRow = Items.IndexOf(SelectedItem);

                SktMucLucModel newRow = ObjectCopier.Clone(SelectedItem);
                newRow.Id = Guid.Empty;
                newRow.IIDMLSKT = Guid.NewGuid();
                newRow.SktMucLucMaps = new List<NsMlsktMlns>();
                newRow.MLNS = null;
                newRow.IIDMLSKTCha = parent.IIDMLSKT;
                newRow.KyHieuCha = parent.SKyHieu;
                newRow.IsModified = true;
                newRow.BHangCha = false;

                newRow.PropertyChanged += Item_PropertyChanged;
                Items.Insert(currentRow + 1, newRow);
                OnPropertyChanged("Items");
                var cell = new DataGridCellInfo(Items[currentRow + 1], dgdData.Columns[0]);
                dgdData.ScrollIntoView(Items[currentRow + 1], dgdData.Columns[0]);
                dgdData.CurrentCell = cell;
                dgdData.BeginEdit();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete(object obj)
        {
            try
            {
                SktMucLucModel item = SelectedItem;
                SktMucLucService sktMucLucService = _service;
                if (sktMucLucService.IsUsedMLSKT(item.IIDMLSKT, _authenticationInfo.YearOfWork))
                {
                    MessageBox.Show(Resources.UnableToDeleteUsedMLSKT, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                if (SelectedItem.IsDeleted)
                {
                    DataGrid dgdData = FindDatagrid();
                    CancelEditData(dgdData);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave(object obj)
        {
            try
            {
                if (!validate())
                {
                    return;
                }
                var time = DateTime.Now;
                string msgConfirm = "Bạn chắc chắn muốn lưu thay đổi ?";
                MessageBoxResult dialogResult = MessageBox.Show(msgConfirm, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        BeforeSave();
                        var itemModified = Items.Where(i => i.IsModified || i.IsDeleted);
                        var dataToSave = _mapper.Map<IEnumerable<NsSktMucLuc>>(itemModified);
                        foreach (var i in dataToSave)
                        {
                            if (i.IsDeleted)
                                i.IsModified = false;
                        }
                        _service.AddOrUpdateRange(dataToSave, _authenticationInfo);
                        DataGrid dataGrid = FindDatagrid();
                        OnRefreshWithOutReload(dataGrid);
                        MessageBoxHelper.Info("Lưu dữ liệu thành công");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnRefreshWithOutReload(object obj)
        {
            OnRefresh(obj);
        }

        private void BeforeSave()
        {

        }

        private bool validate()
        {
            return true;
        }
        private void CustomValueProps(SktMucLucModel newRow, SktMucLucModel currentRow)
        {
            newRow.Id = Guid.Empty;
            newRow.IsModified = true;
            newRow.SktMucLucMaps = new List<NsMlsktMlns>();
            newRow.IIDMLSKT = Guid.NewGuid();
            newRow.MLNS = null;
            newRow.BHangCha = false;
        }

        private void SktMucLucModel_OnDeleteMLNS(SktMucLucModel model, IEnumerable<SktMucLucModel> models)
        {
            SktMucLucModel parent = models.FirstOrDefault(i => i.IIDMLSKT == model.IIDMLSKTCha);
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.IIDMLSKTCha == parent.IIDMLSKT && !i.IsDeleted);
            parent.BHangCha = hasChild;
        }

        private void CancelEditData(DataGrid dgdData)
        {
            if (dgdData != null)
            {
                // One for the column and another for the row
                dgdData.CancelEdit();
                dgdData.CancelEdit();
            }
        }

        protected override void OnRefresh(object obj)
        {
            var rawData = _service.FindAll(_authenticationInfo);
            foreach (var i in rawData)
            {
                i.IsDeleted = false;
                i.IsModified = false;
            }
            FilterModel = new SktMucLucModel();
            _currentCodeValDictionary = new Dictionary<string, int>();
            Items.Clear();
            Items = new ObservableCollection<SktMucLucModel>();
            Items = _mapper.Map<ObservableCollection<SktMucLucModel>>(rawData);
            foreach (var item in Items)
            {
                item.MLNS = string.Join("; ", item.SktMucLucMaps.Select(x => x.SNsXauNoiMa));
                item.PropertyChanged += Item_PropertyChanged;
            }
            _isFirstLoad = true;
            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            _dataCollectionView.SortDescriptions.Clear();
            _dataCollectionView.Filter = ItemsViewFilter;
            _isFirstLoad = false;
            OnPropertyChanged(nameof(Items));
            DataGrid dgdData = FindDatagrid();
            if (dgdData != null && dgdData.Columns != null)
            {
                foreach (var column in dgdData.Columns)
                {
                    column.SortDirection = null;
                }
            }
            this.CancelEditData(dgdData);
            if (dgdData.Items.Count > 0)
            {
                var cell = new DataGridCellInfo(dgdData.Items[0], dgdData.Columns[0]);
                SelectedItem = (SktMucLucModel)dgdData.Items[0];
                dgdData.CurrentCell = cell;
            }
        }

        private object[] InitMLNSParams()
        {
            SktMucLucModel sktMucLuc = SelectedItem;
            return new object[] { DialogType.LoadMLNSOfSktMucLuc, sktMucLuc.SKyHieu };
        }

        public void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(SktMucLucModel.SNGCha)))
            {
                var DanhMucNhomNganhService = _serviceProvider.GetService(typeof(DanhMucNhomNganhService));
                GenericControlCustomViewModel<DanhMucNhomNganhModel, DanhMuc, DanhMucNhomNganhService> dialogVM = new GenericControlCustomViewModel<DanhMucNhomNganhModel, DanhMuc, DanhMucNhomNganhService>
                    ((DanhMucNhomNganhService)DanhMucNhomNganhService, _mapper, _sessionService, _serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục nhóm ngành";
                dialogVM.Title = "Danh mục nhóm ngành";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIDMaDanhMuc.Equals(SelectedItem.SNGCha)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    DanhMucNhomNganhModel data = obj as DanhMucNhomNganhModel;
                    SelectedItem.SNGCha = data.IIDMaDanhMuc;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
            /*else if (property.Name.Equals(nameof(SktMucLucModel.KyHieuCha)))
            {
                var SktMucLucService = _serviceProvider.GetService(typeof(ISktMucLucService));
                GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService> dialogVM = new GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService>
                    ((SktMucLucService)SktMucLucService, _mapper, _sessionService, _serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục số kiểm tra";
                dialogVM.Title = "Danh mục số kiểm tra";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIDMLSKT.Equals(SelectedItem.IIDMLSKTCha)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    SktMucLucModel data = obj as SktMucLucModel;
                    SelectedItem.IIDMLSKTCha = data.IIDMLSKT;
                    SelectedItem.KyHieuCha = data.SKyHieu;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }*/
            else if (property.Name.Equals(nameof(SktMucLucModel.MLNS)))
            {
                var MucLucNganSachService = _serviceProvider.GetService(typeof(IMucLucNganSachService));
                GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialogVM = new GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService>
                    ((MucLucNganSachService)MucLucNganSachService, _mapper, _sessionService, _serviceProvider);
                dialogVM._authenticationInfo.OptionalParam = InitMLNSParams();
                dialogVM.IsDialog = true;
                dialogVM.IsVisibleFilterByMlnsMappingType = true;
                dialogVM.Description = "Danh mục MLNS";
                dialogVM.Title = "Danh mục MLNS";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;
                SetSelectedMLNS(dialogVM);
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    IEnumerable<NsMuclucNgansachModel> data = obj as IEnumerable<NsMuclucNgansachModel>;
                    // chỉ lưu mlns có bhangchadutoan = 0 (false)
                    SelectedItem.MLNS = string.Join("; ", data.Where(t => t.BHangChaDuToan.HasValue && !t.BHangChaDuToan.Value).Select(t => t.XauNoiMa));
                    List<NsMlsktMlns> sktMucLucMaps = data.Where(t => t.BHangChaDuToan.HasValue && !t.BHangChaDuToan.Value).Select(model => new NsMlsktMlns { SNsXauNoiMa = model.XauNoiMa }).ToList();
                    SelectedItem.SktMucLucMaps = sktMucLucMaps;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
            else if (property.Name.Equals(nameof(SktMucLucModel.KyHieuCha)))
            {
                MucLucSKTViewModel mucLucSKTViewModel = _serviceProvider.GetService(typeof(MucLucSKTViewModel)) as MucLucSKTViewModel;
                mucLucSKTViewModel.Items = new ObservableCollection<SktMucLucModel>(Items.Where(t => !t.IsDeleted));
                mucLucSKTViewModel._isFirstLoad = true;
                mucLucSKTViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucSKTViewModel.Items);
                mucLucSKTViewModel._dataCollectionView.Filter = mucLucSKTViewModel.ItemsViewFilter;
                mucLucSKTViewModel._isFirstLoad = false;
                mucLucSKTViewModel.SktMucLucModel = SelectedItem;
                mucLucSKTViewModel._authenticationInfo = _authenticationInfo;
                mucLucSKTViewModel._filterModel = new SktMucLucModel();
                MucLucSKTView = new MuclucSKTView()
                {
                    DataContext = mucLucSKTViewModel
                };
                mucLucSKTViewModel.MucLucSKTView = MucLucSKTView;
                DialogClosingEventHandler closeEvent = (sender, eventArgs) =>
                {
                    if (eventArgs.Parameter != null)
                    {
                        SktMucLucModel nsMuclucSKTModel = eventArgs.Parameter as SktMucLucModel;
                        nsMuclucSKTModel.BHangCha = true;
                        SelectedItem.IIDMLSKTCha = nsMuclucSKTModel.IIDMLSKT;
                        SelectedItem.KyHieuCha = nsMuclucSKTModel.SKyHieu;
                    }
                };
                var dialog = DialogHost.Show(MucLucSKTView, "RootDialog", closeEvent);
                mucLucSKTViewModel.IsDialog = true;
                mucLucSKTViewModel.Name = "Chọn MLSKT cha cho mlns " + SelectedItem.SKyHieu;
            }
        }

        private void SetSelectedMLNS(GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialogVM)
        {
            dialogVM.AfterSelectAll = true;
            IEnumerable<string> nsXauNoima = SelectedItem.SktMucLucMaps.Select(t => t.SNsXauNoiMa);
            string allXNm = String.Join(";", nsXauNoima);
            foreach (var model in dialogVM.Items)
            {
                if (nsXauNoima.Contains(model.XNM) || allXNm.StartsWith(model.XNM + "-") || allXNm.Contains(";" + model.XNM + "-"))
                {
                    model.IsSelected = true;
                }
                // nếu loại là LNS (1, 101, 1010000) thì check startwith (ko cos daaus -)
                if (string.IsNullOrEmpty(model.L))
                {
                    model.IsSelected = allXNm.StartsWith(model.XNM);
                }
            }
            dialogVM.AfterSelectAll = false;
        }

        private void OnOpenReferencePopup(object obj)
        {
            try
            {
                DataGrid d = FindDatagrid();
                if (d == null || string.IsNullOrEmpty(_currentCol))
                {
                    return;
                }
                var property = ModelType.GetProperty(_currentCol);
                InitDialog(property);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnFilterDataByColumn(string colName)
        {
            try
            {
                /*DataGrid d = FnCommonUtils.FindChild<DataGrid>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), ModelName);
                if (d == null)
                {
                    d = FnCommonUtils.FindChild<DataGrid>(MucLucNganSachView, ModelName);
                }*/
                var d = FindDatagrid();
                if (d == null)
                {
                    return;
                }
                this.CancelEditData(d);
                BeForeRefresh();
                _dataCollectionView.Refresh();
                if (d.Items.Count > 0)
                {
                    var cell = new DataGridCellInfo(d.Items[0], d.Columns[0]);
                    SelectedItem = (SktMucLucModel)d.Items[0];
                    d.CurrentCell = cell;
                }
                ToggleFilterPopup(false, colName + POPUP_SUFFIX);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ToggleFilterPopup(bool IsOpen)
        {
            ToggleFilterPopup(IsOpen, _currentCol + POPUP_SUFFIX);
        }

        public void ToggleFilterPopup(bool IsOpen, string elementName)
        {
            /*PopupBox popupBox = FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
            if (popupBox == null)
            {
                popupBox = FnCommonUtils.FindChild<PopupBox>(MucLucNganSachView, elementName);
            }*/
            PopupBox popupBox = FindPopupByName(elementName);
            if (popupBox != null)
            {
                popupBox.IsPopupOpen = IsOpen;
                if (!IsOpen)
                {
                    /*DataGrid d = FnCommonUtils.FindChild<DataGrid>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), ModelName);
                    if (d == null)
                    {
                        d = FnCommonUtils.FindChild<DataGrid>(MucLucNganSachView, ModelName);
                    }*/
                    DataGrid d = FindDatagrid();
                    if (d == null)
                    {
                        return;
                    }
                    if (d.Items.Count > 0)
                    {
                        var cell = new DataGridCellInfo(d.Items[0], d.Columns[0]);
                        SelectedItem = (SktMucLucModel)d.Items[0];
                        d.CurrentCell = cell;
                    }
                }
            }
        }

        private void BeForeRefresh()
        {
            var filterModel = FilterModel;
            _filterResult = Items.Where(item => FilterFunction(FilterModel, item)).Where(item => !item.BHangCha).ToList();
            maConcatenation = string.Join(";", _filterResult.Select(i => i.SKyHieu).ToHashSet());
        }

        private PopupBox FindPopupByName(string elementName)
        {
            PopupBox popupBox = FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
            if (IsDialog)
            {
                return FnCommonUtils.FindChild<PopupBox>(MucLucSKTView, elementName);
            }
            else
                return FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
        }

        private DataGrid FindDatagrid()
        {
            if (IsDialog)
            {
                return FnCommonUtils.FindChild<DataGrid>(MucLucSKTView, ModelName);
            }
            else
                return FnCommonUtils.FindChild<DataGrid>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), ModelName);
        }

        private void OnSaveReference()
        {
            try
            {
                if (SelectedItem.SKyHieu.Equals(SktMucLucModel.SKyHieu) || !SktMucLucModel.SKyHieu.StartsWith(SelectedItem.SKyHieu))
                {
                    MessageBoxHelper.Warning("MLSKT không hợp lệ");
                    return;
                }
                DialogHost.CloseDialogCommand.Execute(SelectedItem, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnCloseWindow()
        {

        }

        private void OnExportExcel()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Items", Items);
                var xlsFile = _exportService.Export<SktMucLucModel>(TemplateFileName, data);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(TemplateFileName) + "_" + DateTime.Now.ToString("ddMMyyhhmmss");
                results.Add(new ExportResult(Description, fileNameWithoutExtension, null, xlsFile));
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

        private void OnImportExcel()
        {
            ImportExcelViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService, DanhMucMucLucSKTImportModel> importExcelViewModel =
                new ImportExcelViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService, DanhMucMucLucSKTImportModel>(_serviceProvider, _service, _authenticationInfo);
            importExcelViewModel.DataTemplateFileName = DataTemplateFileName;
            ImportExcelView importExcelView = new ImportExcelView()
            {
                DataContext = importExcelViewModel
            };
            importExcelViewModel.Init();
            importExcelView.Show();
        }

        private void OnSelectMlsktParent()
        {
            if (Items.Any(t => t.IsSelected))
            {
                MucLucSKTViewModel mucLucSKTViewModel = _serviceProvider.GetService(typeof(MucLucSKTViewModel)) as MucLucSKTViewModel;
                mucLucSKTViewModel.Items = new ObservableCollection<SktMucLucModel>(Items.Where(t => !t.IsDeleted));
                mucLucSKTViewModel._isFirstLoad = true;
                mucLucSKTViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucSKTViewModel.Items);
                mucLucSKTViewModel._dataCollectionView.Filter = mucLucSKTViewModel.ItemsViewFilter;
                mucLucSKTViewModel._isFirstLoad = false;
                mucLucSKTViewModel._authenticationInfo = _authenticationInfo;
                mucLucSKTViewModel._filterModel = new SktMucLucModel();
                MucLucSKTView = new MuclucSKTView()
                {
                    DataContext = mucLucSKTViewModel
                };
                mucLucSKTViewModel.MucLucSKTView = MucLucSKTView;
                DialogClosingEventHandler closeEvent = (sender, eventArgs) =>
                {
                    if (eventArgs.Parameter != null)
                    {
                        SktMucLucModel SktMucLucModel = eventArgs.Parameter as SktMucLucModel;
                        foreach (var item in Items.Where(t => t.IsSelected))
                        {
                            item.IIDMLSKTCha = SktMucLucModel.IIDMLSKT;
                            item.KyHieuCha = SktMucLucModel.SKyHieu;
                        }
                        SktMucLucModel.BHangCha = true;
                    }
                };
                var dialog = DialogHost.Show(MucLucSKTView, "RootDialog", closeEvent);
                mucLucSKTViewModel.IsDialog = true;
            }
        }
    }
}
