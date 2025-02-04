using AutoMapper;
using ControlzEx.Standard;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Management.Smo.Wmi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class MucLucCapBacLuongViewModel : ViewModelBase
    {
        public readonly ILog _logger;
        public readonly TlDmCapBacNq104Service _tlDmCapBacNq104Service;
        public readonly TlDmCapBacLuongNq104Service _tlDmCapBacLuongNq104Service;
        public readonly ITlDmCanBoNq104Repository _canBoRepository;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;
        private readonly ITlDmPhuCapNq104Service _phuCapService;
        public readonly IMapper _mapper;
        public readonly IServiceProvider _serviceProvider;
        public readonly ISessionService _sessionService;
        public ICollectionView _dataCollectionView;
        public ICollectionView _dataCapBacLuongCollectionView;
        public readonly IExportService _exportService;
        public readonly ISysAuditLogService _auditLog;
        public AuthenticationInfo _authenticationInfo;
        public Dictionary<string, bool> _checkboxDictionary;
        public Dictionary<string, string> _formatDictionary;
        public Dictionary<string, int> _currentCodeValDictionary;
        private string _currentCol;
        private bool _isFirstLoad;
        private bool _isFirstUpdateSelectedChucNang = true;

        public string PopupBoxSuffix = "PopupBox";
        public const string TextBoxSuffix = "TextBox";

        public override string FuncCode => NSFunctionCode.CATEGORY_BUDGET_MLNS;
        public override string Name => "Danh mục cấp bậc";
        public override string Title => "Danh mục cấp bậc - lương";
        public override string Description => "Chỉnh sửa thông tin danh mục";

        public string ExportJsonFileName = "mlns.json";
        public string TemplateFileName = "rpt_dm_mlns.xlsx";
        public Type ImportModelType = typeof(MLNSImportModel);
        public string DataTemplateFileName = "rpt_dm_mlns_template_data.xlsx";

        public override Type ContentType => typeof(MucLucCapBacLuongView);
        public Type ModelType => typeof(TlDmCapBacNq104Model);

        public Type ModelTypeCapBacLuong => typeof(TlDmCapBacLuongNq104Model);
        private ObservableCollection<TlDmCapBacNq104Model> _capBacItems;
        public ObservableCollection<TlDmCapBacNq104Model> CapBacItems
        {
            get => _capBacItems;
            set
            {
                if (SetProperty(ref _capBacItems, value))
                {
                    OnCapBacItemsChanged();
                }
            }
        }

        private TlDmCapBacNq104Model _selectedCapBacItem;
        public TlDmCapBacNq104Model SelectedCapBacItem
        {
            get => _selectedCapBacItem;
            set
            {
                if (SetProperty(ref _selectedCapBacItem, value))
                {
                    OnSelectedCapBacItemChanged();
                    SetSelectedCapBacLuongItem();
                }
            }
        }

        private void OnSelectedCapBacItemChanged()
        {
            SelectedDatagrid = 1;
            //throw new NotImplementedException();
        }

        private void OnCapBacItemsChanged()
        {
            //throw new NotImplementedException();
        }

        private ObservableCollection<TlDmCapBacLuongNq104Model> _capBacLuongItems;
        public ObservableCollection<TlDmCapBacLuongNq104Model> CapBacLuongItems
        {
            get => _capBacLuongItems;
            set
            {
                if (SetProperty(ref _capBacLuongItems, value))
                {
                    OnCapBacLuongItemsChanged();
                }
            }
        }

        private TlDmCapBacLuongNq104Model _selectedCapBacLuongItem;
        public TlDmCapBacLuongNq104Model SelectedCapBacLuongItem
        {
            get => _selectedCapBacLuongItem;
            set
            {
                if (SetProperty(ref _selectedCapBacLuongItem, value))
                {
                    OnSelectedCapBacLuongItemChanged();
                }
            }
        }

        private void OnSelectedCapBacLuongItemChanged()
        {
            SelectedDatagrid = 2;
            //throw new NotImplementedException();
        }

        private void OnCapBacLuongItemsChanged()
        {
            //throw new NotImplementedException();
        }

        public bool IsEdit => SelectedCapBacItem != null;
        public bool IsShowCapBacLuong { get; set; } = true;
        public bool IsShowCapBac { get; set; } = true;
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
        public int SelectedDatagrid { get; set; } = 1;
        public bool IsDanhMucChuky { get; set; }
        public bool IsMultipleSelect { get; set; }
        public bool IsEnabledSaveBtn => SelectedCapBacItem != null;
        public Visibility VisibilityDialogBtn => IsDialog ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibilityFunctionBtn => IsDanhMucChuky ? Visibility.Collapsed : Visibility.Visible;
        public string Hint => IsDialog ? "Ấn phím space để chọn checkbox nếu có" : "";
        public string Total => " (Tổng số bản ghi: " + (IsShowCapBac ? CapBacItems.Count() : CapBacLuongItems.Count()) + ")";

        public bool AfterSelectAll;

        private string _filtertext;
        public string FilterText
        {
            get => _filtertext;
            set => SetProperty(ref _filtertext, value);
        }

        private decimal? _tyLeTang;
        public decimal? TyLeTang
        {
            get => _tyLeTang;
            set => SetProperty(ref _tyLeTang, value);
        }

        private TlDmCapBacNq104Model _filterCapBacModel;
        public TlDmCapBacNq104Model FilterCapBacModel
        {
            get => _filterCapBacModel;
            set => SetProperty(ref _filterCapBacModel, value);
        }

        private TlDmCapBacLuongNq104Model _filterCapBacLuongModel;
        public TlDmCapBacLuongNq104Model FilterCapBacLuongModel
        {
            get => _filterCapBacLuongModel;
            set => SetProperty(ref _filterCapBacLuongModel, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                // Cap bac
                if (IsShowCapBac)
                {
                    if (CapBacItems != null)
                    {
                        var unSelected = CapBacItems.Where(item => !item.IsSelected).ToList();
                        return unSelected.Count == 0;
                    }
                    return false;
                }
                // Cap bac luong
                else
                {
                    if (CapBacLuongItems != null)
                    {
                        var unSelected = CapBacLuongItems.Where(item => !item.IsSelected).ToList();
                        return unSelected.Count == 0;
                    }
                    return false;
                }

            }
            set
            {
                if (value.HasValue)
                {
                    // Cap bac
                    if (IsShowCapBac)
                    {
                        SelectAll(value.Value, CapBacItems);
                    }
                    // Cap bac luong
                    else
                    {
                        SelectCapBacLuongAll(value.Value, CapBacLuongItems);
                    }
                }
            }
        }

        public MucLucCapBacLuongView MucLucCapBacLuongView { get; set; }
        public MucLucPopupBoxCapBacLuongView MucLucPopupBoxCapBacLuongView { get; set; }

        private List<string> mlnsType = new List<string>
        {
            "TNG3", "TNG2", "TNG1", "TNG", "NG"
        };
        private string xnmConcatenation = "";
        private ICollection<TlDmCapBacNq104Model> _filterResult = new HashSet<TlDmCapBacNq104Model>();
        private DanhMuc mlnsChiTietToi { get; set; }

        public ObservableCollection<ComboboxItem> LstTrangThai { get; set; }
        public ObservableCollection<ComboboxItem> BHangChaChiTietToi { get; set; }
        public ObservableCollection<ComboboxItem> CPChiTietToi { get; set; }
        public ObservableCollection<ComboboxItem> LstCapBac { get; set; }

        public bool LNS { get; set; }
        public bool L { get; set; }
        public bool K { get; set; }
        public bool M { get; set; }
        public bool TM { get; set; }
        public bool TTM { get; set; }
        public bool NG { get; set; }
        public bool TNG { get; set; }
        public bool TNG1 { get; set; }
        public bool TNG2 { get; set; }
        public bool TNG3 { get; set; }
        public bool SktKyHieu { get; set; } = true;
        public TlDmCapBacNq104Model TlDmCapBacNq104Model { get; set; }
        public TlDmCapBacLuongNq104Model TlDmCapBacLuongNq104Model { get; set; }
        public MucLucNganSachCheckDataViewModel MucLucNganSachCheckDataViewModel { get; set; }
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
        public RelayCommand AddCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ViewDetailCommand { get; }
        public RelayCommand FilterCommand { get; }
        public RelayCommand EscapteFilterCommand { get; }
        public RelayCommand SelectMlnsParentCommand { get; }
        public RelayCommand UpdateNangCapBacLuong { get; }
        public RelayCommand UpdateCadresCommand { get; }

        public MucLucCapBacLuongViewModel(
            TlDmCapBacNq104Service tlDmCapBacNq104Service,
            TlDmCapBacLuongNq104Service tlDmCapBacLuongNq104Service,
            IMapper mapper, ISessionService sessionService,
            IExportService exportService, IServiceProvider serviceProvider,
            MucLucNganSachCheckDataViewModel mucLucNganSachCheckDataViewModel,
            ITlDmCanBoNq104Repository TlDmCanBoNq104Repository,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service,
            ITlDmPhuCapNq104Service iTlDmPhuCapNq104Service)
        {
            _tlDmCapBacNq104Service = tlDmCapBacNq104Service;
            _tlDmCapBacLuongNq104Service = tlDmCapBacLuongNq104Service;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
            _phuCapService = iTlDmPhuCapNq104Service;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _serviceProvider = serviceProvider;
            SaveReferenceCommand = new RelayCommand(obj => OnSaveReference());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            SelectedCellChangeCommand = new RelayCommand(obj => _currentCol = obj.ToString());
            OpenFilterPopupCommand = new RelayCommand(obj => ToggleFilterPopup(true));
            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
            //OnLoadedFormatPopupCommand = new RelayCommand(obj => OnLoadedFormatPopup(obj));
            ExportCommand = new RelayCommand(obj => OnExportExcel());
            ExportJsonCommand = new RelayCommand(obj => OnExportJsonFile());
            ImportExcelCommand = new RelayCommand(obj => OnImportExcel());
            AddChildCommand = new RelayCommand(obj => OnAddChild(obj));
            AddCommand = new RelayCommand(obj => OnAdd(obj));
            RefreshCommand = new RelayCommand(obj => OnRefresh(obj));
            DeleteCommand = new RelayCommand(obj => OnDelete(obj));
            ViewDetailCommand = new RelayCommand(obj => OnViewDetail(), obj => !IsDialog);
            FilterCommand = new RelayCommand(o => OnFilterDataByColumn(o.ToString()));
            EscapteFilterCommand = new RelayCommand(o => ToggleFilterPopup(false, o.ToString() + PopupBoxSuffix));
            SelectMlnsParentCommand = new RelayCommand(o => OnSelectMlnsParent());
            UpdateNangCapBacLuong = new RelayCommand(o => OnUpdateNangCapBacLuong());
            UpdateCadresCommand = new RelayCommand(obj => OnUpdateCadres());
            MucLucNganSachCheckDataViewModel = mucLucNganSachCheckDataViewModel;
            _canBoRepository = TlDmCanBoNq104Repository;
        }

        private void OnUpdateNangCapBacLuong()
        {
            try
            {
                var lstNangCapBacLuong = CapBacLuongItems.Where(x => x.IsSelected && !x.IsDeleted && !x.IsHangCha).ToList();
                if (lstNangCapBacLuong.Count < 0)
                {
                    MessageBoxHelper.Warning("Đ/c chưa chọn mã bậc lương tăng lương");
                    return;
                }
                if (lstNangCapBacLuong.Count > 0)
                {
                    foreach (var item in lstNangCapBacLuong)
                    {
                        item.TienLuong = item.TienLuong.GetValueOrDefault() + ((item.TienLuong.GetValueOrDefault() * TyLeTang.GetValueOrDefault()) / 100);
                        item.TienNangLuong = item.TienNangLuong.GetValueOrDefault() + ((item.TienNangLuong.GetValueOrDefault() * TyLeTang.GetValueOrDefault()) / 100);
                    }
                }

                DialogHost.CloseDialogCommand.Execute(SelectedCapBacLuongItem, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void Init()
        {
            try
            {
                InitBaseData();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private void InitBaseData()
        {
            LstTrangThai = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
                };
            BHangChaChiTietToi = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = null },
                    new ComboboxItem { DisplayItem = "NG", ValueItem = "NG" },
                    new ComboboxItem { DisplayItem = "TNG", ValueItem = "TNG" },
                    new ComboboxItem { DisplayItem = "TNG1", ValueItem = "TNG1" },
                    new ComboboxItem { DisplayItem = "TNG2", ValueItem = "TNG2" },
                    new ComboboxItem { DisplayItem = "TNG3", ValueItem = "TNG3" },
                };
            CPChiTietToi = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = null },
                    new ComboboxItem { DisplayItem = "M", ValueItem = "M" },
                    new ComboboxItem { DisplayItem = "TM", ValueItem = "TM" },
                    new ComboboxItem { DisplayItem = "NG", ValueItem = "NG" },
                };
            List<string> mlnsType = new List<string>
                {
                    "LNS", "L", "K", "M", "TM", "TTM", "NG", "TNG", "TNG1", "TNG2", "TNG3"
                };

            LstCapBac = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = "" },
                    new ComboboxItem { DisplayItem = "Công chức quốc phòng", ValueItem = "3.2" },
                    new ComboboxItem { DisplayItem = "Quân nhân chuyên nghiệp", ValueItem = "2" },
                    new ComboboxItem { DisplayItem = "Công nhân quốc phòng", ValueItem = "3.1" },
                    new ComboboxItem { DisplayItem = "HSQ-CS", ValueItem = "4" },
                    new ComboboxItem { DisplayItem = "Viên chức quốc phòng", ValueItem = "3.3" },
                    new ComboboxItem { DisplayItem = "Sĩ quan", ValueItem = "1" },
                };


            base.Init();
            _authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
            _checkboxDictionary = new Dictionary<string, bool>();
            _formatDictionary = new Dictionary<string, string>();
            _currentCodeValDictionary = new Dictionary<string, int>();
            _filterCapBacModel = new TlDmCapBacNq104Model();
            _filterCapBacLuongModel = new TlDmCapBacLuongNq104Model();
            if (_sessionService.Current.AutoGenerateDataSetting.ContainsKey(typeof(TlDmCapBacNq104Model).Name))
            {
                _formatDictionary = _sessionService.Current.AutoGenerateDataSetting[typeof(TlDmCapBacNq104Model).Name];
            }
            else
            {
                _sessionService.Current.AutoGenerateDataSetting[typeof(TlDmCapBacNq104Model).Name] = _formatDictionary;
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var data = _tlDmCapBacNq104Service.FindAll().Where(x => x.Nam == _authenticationInfo.YearOfWork).OrderBy(x => x.XauNoiMa);
            var dataLuong = _tlDmCapBacLuongNq104Service.FindAll().Where(x => x.Nam == _authenticationInfo.YearOfWork).OrderBy(x => x.XauNoiMa);
            CapBacItems = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
            CapBacLuongItems = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(dataLuong);

            foreach (var item in CapBacItems)
            {
                item.PropertyChanged += Item_PropertyChanged;
                item.PropertyChanged += TlDmCapBacNq104Model_PropertyChanged;
            }


            foreach (var item in CapBacLuongItems)
            {
                item.PropertyChanged += Item_PropertyChanged;
                item.PropertyChanged += TlDmCapBacLuongNq104Model_PropertyChanged;
            }

            _isFirstLoad = true;
            _dataCollectionView = CollectionViewSource.GetDefaultView(CapBacItems);
            _dataCollectionView.Filter = ItemsViewFilter;
            _dataCapBacLuongCollectionView = CollectionViewSource.GetDefaultView(CapBacLuongItems);
            _dataCapBacLuongCollectionView.Filter = ItemsViewCapBacLuongFilter;
            OnPropertyChanged(nameof(CapBacItems));
            OnPropertyChanged(nameof(CapBacLuongItems));
            _isFirstLoad = false;
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter == null)
                return;
            TlDmCapBacNq104Model tlDmCapBacNq104Model = eventArgs.Parameter as TlDmCapBacNq104Model;
            if (tlDmCapBacNq104Model == null)
            {
                return;
            }
            if (SelectedCapBacItem.MaCb.Equals(tlDmCapBacNq104Model.MaCb))
            {
                MessageBoxHelper.Warning("Cấp bậc thêm không hợp lệ");
                return;
            }
            tlDmCapBacNq104Model.IsHangCha = true;
            SelectedCapBacItem.Parent = tlDmCapBacNq104Model.MaCb;
        }

        protected void OnAdd(object obj)
        {
            try
            {
                if (SelectedDatagrid == 1)
                {
                    DataGrid dgdData = FindDatagrid();
                    this.CancelEditData(dgdData);

                    int currentRow = CapBacItems.Count - 1;
                    TlDmCapBacNq104Model newRow;
                    if (SelectedCapBacItem == null)
                    {
                        newRow = new TlDmCapBacNq104Model();
                        CustomValueProps(newRow, SelectedCapBacItem);
                        CapBacItems.Add(newRow);
                    }
                    else
                    {
                        currentRow = CapBacItems.IndexOf(SelectedCapBacItem);
                        newRow = ObjectCopier.Clone(SelectedCapBacItem);
                        CustomValueProps(newRow, SelectedCapBacItem);
                        CapBacItems.Insert(currentRow + 1, newRow);
                    }
                    newRow.PropertyChanged += Item_PropertyChanged;
                    newRow.PropertyChanged += TlDmCapBacNq104Model_PropertyChanged;
                    OnPropertyChanged(newRow);
                    OnPropertyChanged(nameof(CapBacItems));

                    var cell = new DataGridCellInfo(CapBacItems[currentRow + 1], dgdData.Columns[0]);
                    dgdData.ScrollIntoView(CapBacItems[currentRow + 1], dgdData.Columns[0]);
                    dgdData.CurrentCell = cell;
                    dgdData.BeginEdit();
                }
                else if (SelectedDatagrid == 2)
                {
                    DataGrid dgdData = FindDatagrid();
                    this.CancelEditData(dgdData);

                    int currentRow = CapBacLuongItems.Count - 1;
                    TlDmCapBacLuongNq104Model newRow;
                    if (SelectedCapBacLuongItem == null)
                    {
                        newRow = new TlDmCapBacLuongNq104Model();
                        CustomValueProps(newRow, SelectedCapBacLuongItem);
                        CapBacLuongItems.Add(newRow);
                    }
                    else
                    {
                        currentRow = CapBacLuongItems.IndexOf(SelectedCapBacLuongItem);
                        newRow = ObjectCopier.Clone(SelectedCapBacLuongItem);
                        CustomValueProps(newRow, SelectedCapBacLuongItem);
                        CapBacLuongItems.Insert(currentRow + 1, newRow);
                    }
                    newRow.PropertyChanged += Item_PropertyChanged;
                    newRow.PropertyChanged += TlDmCapBacNq104Model_PropertyChanged;
                    OnPropertyChanged(newRow);
                    OnPropertyChanged(nameof(CapBacLuongItems));

                    var cell = new DataGridCellInfo(CapBacLuongItems[currentRow + 1], dgdData.Columns[0]);
                    dgdData.ScrollIntoView(CapBacLuongItems[currentRow + 1], dgdData.Columns[0]);
                    dgdData.CurrentCell = cell;
                    dgdData.BeginEdit();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnAddChild(object obj)
        {
            if (SelectedCapBacItem == null)
                return;

            DataGrid dgdData = FindDatagrid();
            this.CancelEditData(dgdData);

            int currentRow = CapBacItems.IndexOf(SelectedCapBacItem);
            TlDmCapBacNq104Model parent = SelectedCapBacItem;
            TlDmCapBacNq104Model newRow = ObjectCopier.Clone(SelectedCapBacItem);
            newRow.Id = Guid.Empty;
            newRow.Parent = parent.MaCb;
            newRow.IsModified = true;
            newRow.IsHangCha = false;
            parent.IsHangCha = true;
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += TlDmCapBacNq104Model_PropertyChanged;
            CapBacItems.Insert(currentRow + 1, newRow);
            OnPropertyChanged(nameof(CapBacItems));
            var cell = new DataGridCellInfo(CapBacItems[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(CapBacItems[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        protected void OnDelete(object obj)
        {
            try
            {
                if (SelectedDatagrid == 1)
                {
                    SelectedCapBacItem.IsDeleted = !SelectedCapBacItem.IsDeleted;
                    if (SelectedCapBacItem.IsDeleted)
                    {
                        DataGrid dgdData = FindDatagrid();
                        this.CancelEditData(dgdData);
                    }
                }
                else
                {
                    SelectedCapBacLuongItem.IsDeleted = !SelectedCapBacLuongItem.IsDeleted;
                    if (SelectedCapBacLuongItem.IsDeleted)
                    {
                        DataGrid dgdData = FindDatagrid();
                        this.CancelEditData(dgdData);
                    }
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
                if (!Validate())
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
                        var dataToSave1 = _mapper.Map<IEnumerable<TlDmCapBacNq104>>(CapBacItems.Where(i => i.IsModified || i.IsDeleted));
                        var dataToSave2 = _mapper.Map<IEnumerable<TlDmCapBacLuongNq104>>(CapBacLuongItems.Where(i => i.IsModified || i.IsDeleted));
                        foreach (var i in dataToSave1)
                        {
                            if (i.IsDeleted)
                                i.IsModified = false;
                        }
                        foreach (var i in dataToSave2)
                        {
                            if (i.IsDeleted)
                                i.IsModified = false;
                        }
                        _tlDmCapBacNq104Service.AddOrUpdateRange(dataToSave1);
                        _tlDmCapBacLuongNq104Service.AddOrUpdateRange(dataToSave2);
                        DataGrid dataGrid = FindDatagrid();
                        LoadData(dataGrid);
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

        protected void OnRefresh(object obj)
        {
            LoadData();
        }

        private void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(TlDmCapBacNq104Model.Parent)))
            {
                MucLucCapBacLuongViewModel vm = _serviceProvider.GetService(typeof(MucLucCapBacLuongViewModel)) as MucLucCapBacLuongViewModel;
                vm.CapBacItems = new ObservableCollection<TlDmCapBacNq104Model>(CapBacItems.Where(t => !t.IsDeleted));
                vm._isFirstLoad = true;
                vm._dataCollectionView = CollectionViewSource.GetDefaultView(vm.CapBacItems);
                vm._dataCollectionView.Filter = vm.ItemsViewFilter;
                vm._isFirstLoad = false;
                vm.IsShowCapBacLuong = false;
                vm.TlDmCapBacNq104Model = SelectedCapBacItem;
                vm.FilterCapBacModel = new TlDmCapBacNq104Model();
                MucLucCapBacLuongView = new MucLucCapBacLuongView()
                {
                    DataContext = vm
                };
                vm.MucLucCapBacLuongView = MucLucCapBacLuongView;
                var dialog = DialogHost.Show(MucLucCapBacLuongView, "RootDialog", ClosingEventHandler);
                vm.IsDialog = true;
                vm.Name = "Chọn MLNS cha cho mlns " + SelectedCapBacItem.MaCb;
            }

            if (property.Name.Equals(nameof(TlDmCapBacLuongNq104Model.TienLuong)) || property.Name.Equals(nameof(TlDmCapBacLuongNq104Model.TienNangLuong)))
            {
                MucLucCapBacLuongViewModel vm = _serviceProvider.GetService(typeof(MucLucCapBacLuongViewModel)) as MucLucCapBacLuongViewModel;
                vm.CapBacLuongItems = new ObservableCollection<TlDmCapBacLuongNq104Model>(CapBacLuongItems.Where(x => !x.IsDeleted));
                vm._isFirstLoad = true;
                vm._dataCapBacLuongCollectionView = CollectionViewSource.GetDefaultView(vm.CapBacLuongItems);
                vm._dataCapBacLuongCollectionView.Filter = vm.ItemsViewCapBacLuongFilter;
                vm._isFirstLoad = false;
                vm.IsShowCapBac = false;
                vm.TlDmCapBacLuongNq104Model = SelectedCapBacLuongItem;
                vm.FilterCapBacLuongModel = new TlDmCapBacLuongNq104Model();
                MucLucPopupBoxCapBacLuongView = new MucLucPopupBoxCapBacLuongView()
                {
                    DataContext = vm
                };
                vm.MucLucPopupBoxCapBacLuongView = MucLucPopupBoxCapBacLuongView;
                var dialog = DialogHost.Show(MucLucPopupBoxCapBacLuongView, "RootDialog", ClosingEventHandler);
                vm.IsDialog = true;
                //vm.Name = "Chọn MLNS cha cho mlns " + SelectedCapBacItem.MaCb;
            }
        }

        public void OnFilterDataByColumn(string colName)
        {
            try
            {
                var d = FindDatagrid();
                if (d == null)
                {
                    return;
                }
                this.CancelEditData(d);
                BeForeRefresh();
                _dataCollectionView.Refresh();
                _dataCapBacLuongCollectionView.Refresh();
                if (d.Items.Count > 0)
                {
                    var cell = new DataGridCellInfo(d.Items[0], d.Columns[0]);
                    SelectedCapBacItem = (TlDmCapBacNq104Model)d.Items[0];
                    d.CurrentCell = cell;
                }
                ToggleFilterPopup(false, colName + PopupBoxSuffix);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ToggleFilterPopup(bool IsOpen)
        {
            ToggleFilterPopup(IsOpen, _currentCol + PopupBoxSuffix);
        }

        public void ToggleFilterPopup(bool IsOpen, string elementName)
        {
            PopupBox popupBox = FindPopupByName(elementName);
            if (popupBox != null)
            {
                popupBox.IsPopupOpen = IsOpen;
                if (!IsOpen)
                {
                    DataGrid d = FindDatagrid();
                    if (d == null)
                    {
                        return;
                    }
                    if (d.Items.Count > 0)
                    {
                        var cell = new DataGridCellInfo(d.Items[0], d.Columns[0]);
                        SelectedCapBacItem = d.Items[0] as TlDmCapBacNq104Model;
                        d.CurrentCell = cell;
                    }
                }
            }
        }

        private void BeForeRefresh()
        {
            var filterCapBacModel = FilterCapBacModel;
            _filterResult = CapBacItems.Where(item => FilterFunction(FilterCapBacModel, item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.MaCb).ToHashSet());
        }

        private bool Validate()
        {
            var dataToSave = CapBacItems.Where(i => i.IsModified && !i.IsDeleted);
            foreach (TlDmCapBacNq104Model model in dataToSave)
            {
                //int selectedSDuToanIndex = mlnsType.IndexOf(model.SDuToanChiTietToi);
                //int selectedSQTIndex = mlnsType.IndexOf(model.SQuyetToanChiTietToi);
                //if (selectedSQTIndex > selectedSDuToanIndex)
                //{
                //    MessageBoxHelper.Error(Resources.MLNS_DT_QT_Err);
                //    return false;
                //}
            }
            return true;
        }

        private void BeforeSave()
        {
            // get saved ng type
            List<TlDmCapBacNq104Model> Parent = new List<TlDmCapBacNq104Model>();
            var dataToSave = CapBacItems.Where(i => i.IsModified && !i.IsDeleted).ToList();
            foreach (TlDmCapBacNq104Model model in dataToSave)
            {
                //if ("NG".Equals(typeMLNS) &&
                //(!string.IsNullOrWhiteSpace(model.SDuToanChiTietToi) || !string.IsNullOrWhiteSpace(model.SQuyetToanChiTietToi)))
                //{
                //    List<TlDmCapBacNq104Model> children = CapBacItems.Where(t => t.XNM.Contains(model.XNM + "-")).ToList();
                //    // update bhangcha du toan vaf bhangcha quyet toan
                //    int selectedSDuToanIndex = mlnsType.IndexOf(model.SDuToanChiTietToi);
                //    int selectedSQTIndex = mlnsType.IndexOf(model.SQuyetToanChiTietToi);
                //    string modelNsType = getTypeOfMlns(model);
                //    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                //    UpdateBHangChaDuToan(selectedSDuToanIndex, modelNsTypeIndex, model);
                //    UpdateBHangChaQuyetToan(selectedSQTIndex, modelNsTypeIndex, model);
                //    foreach (TlDmCapBacNq104Model ns in children)
                //    {
                //        ns.IsModified = true;
                //        string nsType = getTypeOfMlns(ns);
                //        int nsTypeIndex = mlnsType.IndexOf(nsType);
                //        UpdateBHangChaDuToan(selectedSDuToanIndex, nsTypeIndex, ns);
                //        UpdateBHangChaQuyetToan(selectedSQTIndex, nsTypeIndex, ns);
                //    }
                //}
                //if (string.IsNullOrEmpty(typeMLNS))
                //{
                //    model.BHangChaDuToan = true;
                //    model.BHangChaQuyetToan = true;
                //}
            }
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

        private void OnPropertyChanged(TlDmCapBacNq104Model model)
        {
            model.PropertyChanged += TlDmCapBacNq104Model_PropertyChanged;
        }

        private void OnPropertyChanged(TlDmCapBacLuongNq104Model model)
        {
            model.PropertyChanged += TlDmCapBacLuongNq104Model_PropertyChanged;
        }

        private void CustomValueProps(TlDmCapBacNq104Model newRow, TlDmCapBacNq104Model currentRow)
        {
            newRow.Id = Guid.Empty;
            newRow.IsModified = true;
            newRow.IsHangCha = false;
        }

        private void CustomValueProps(TlDmCapBacLuongNq104Model newRow, TlDmCapBacLuongNq104Model currentRow)
        {
            newRow.Id = Guid.Empty;
            newRow.IsModified = true;
            newRow.IsHangCha = false;
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals(nameof(ModelBase.IsDeleted))
                && !e.PropertyName.Equals(nameof(ModelBase.IsSelected))
                && !e.PropertyName.Equals(nameof(ModelBase.IsModified)))
            {
                ((ModelBase)sender).IsModified = true;
            }

            if (IsMultipleSelect && e.PropertyName.Equals(nameof(ModelBase.IsSelected)))
            {
                OnPropertyChanged(nameof(IsAllItemsSelected));
            }
        }

        private void TlDmCapBacNq104Model_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<TlDmCapBacNq104Model> models = CapBacItems;
            TlDmCapBacNq104Model TlDmCapBacNq104Model = sender as TlDmCapBacNq104Model;
            // khi tích vào ô tất cả, tránh để hàm propertychanged lặp lại để đảm bảo performance (dùng biến afterSelectAll để kiểm tra điều kiện người dùng vừa tích vào ô tất cả)
            if (args.PropertyName == nameof(TlDmCapBacNq104Model.IsDeleted))
            {
                TlDmCapBacNq104Model_OnDeleteMLNS(TlDmCapBacNq104Model, models);
            }
            else if (args.PropertyName == nameof(TlDmCapBacNq104Model.MaCb))
            {
                TlDmCapBacNq104Model.XauNoiMa = string.Concat(TlDmCapBacNq104Model.Parent, "-", TlDmCapBacNq104Model.MaCb);
            }
            else if (args.PropertyName == nameof(TlDmCapBacNq104Model.IsReadonly))
            {
                IEnumerable<TlDmCapBacNq104Model> children = models.Where(t => TlDmCapBacNq104Model.MaCb.Equals(t.Parent));
                foreach (var c in children)
                {
                    c.IsReadonly = TlDmCapBacNq104Model.IsReadonly;
                }
            }
        }

        private void TlDmCapBacLuongNq104Model_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<TlDmCapBacLuongNq104Model> models = CapBacLuongItems;
            TlDmCapBacLuongNq104Model TlDmCapBacLuongNq104Model = sender as TlDmCapBacLuongNq104Model;
            // khi tích vào ô tất cả, tránh để hàm propertychanged lặp lại để đảm bảo performance (dùng biến afterSelectAll để kiểm tra điều kiện người dùng vừa tích vào ô tất cả)
            if (args.PropertyName == nameof(TlDmCapBacLuongNq104Model.IsDeleted))
            {
                TlDmCapBacLuongNq104Model_OnDeleteMLNS(TlDmCapBacLuongNq104Model, models);
            }
            // check cha all con theo xau noi ma
            if (TlDmCapBacLuongNq104Model.IsHangCha && !AfterSelectAll)
            {
                if (args.PropertyName == nameof(TlDmCapBacLuongNq104Model.IsSelected) && args.PropertyName != nameof(TlDmCapBacLuongNq104Model.IsDeleted))
                {
                    var lstCapBacLuongChild = models.Where(x => !x.IsDeleted && x.XauNoiMa.StartsWith(TlDmCapBacLuongNq104Model.XauNoiMa)).ToList();
                    foreach (var item in lstCapBacLuongChild)
                    {
                        item.IsSelected = TlDmCapBacLuongNq104Model.IsSelected;
                    }
                }
            }
        }

        private void TlDmCapBacNq104Model_OnDeleteMLNS(TlDmCapBacNq104Model model, IEnumerable<TlDmCapBacNq104Model> models)
        {
            TlDmCapBacNq104Model parent = models.FirstOrDefault(i => i.MaCb == model.Parent);
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.Parent == parent.MaCb && !i.IsDeleted);
            parent.IsHangCha = hasChild;
            //foreach (var item in CapBacItems.Where(t => t.Parent.Equals(SelectedCapBacItem.MaCb)))
            //{
            //    item.MlnsParentName = string.Empty;
            //    item.MlnsIdParent = null;
            //}
        }

        private void TlDmCapBacLuongNq104Model_OnDeleteMLNS(TlDmCapBacLuongNq104Model model, IEnumerable<TlDmCapBacLuongNq104Model> models)
        {

        }

        public bool ItemsViewFilter(object obj)
        {
            if (_isFirstLoad)
            {
                return true;
            }
            bool result = true;
            var item = (TlDmCapBacNq104Model)obj;
            result = FilterFunction(FilterCapBacModel, item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.StartsWith(item.MaCb) || xnmConcatenation.Contains(";" + item.MaCb);
            }
            return result;
        }


        public bool ItemsViewCapBacLuongFilter(object obj)
        {
            if (_isFirstLoad)
            {
                return true;
            }
            bool result = true;
            var item = (TlDmCapBacLuongNq104Model)obj;
            result = FilterCapBacLuongFunction(FilterCapBacLuongModel, item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.StartsWith(item.XauNoiMa) || xnmConcatenation.Contains(";" + item.XauNoiMa);
            }
            return result;
        }


        private bool FilterCapBacLuongFunction(TlDmCapBacLuongNq104Model FilterCapBacLuongModel, TlDmCapBacLuongNq104Model item)
        {
            var result = true;
            if (!string.IsNullOrEmpty(FilterCapBacLuongModel.MaDm))
                result = result && item.MaDm.ToLower().StartsWith(FilterCapBacLuongModel.MaDm.ToLower());
            if (!string.IsNullOrEmpty(FilterCapBacLuongModel.TenDm))
                result = result && item.TenDm.ToLower().StartsWith(FilterCapBacLuongModel.TenDm.ToLower());
            if (!string.IsNullOrEmpty(FilterCapBacLuongModel.LoaiDoiTuong))
                result = result && item.LoaiDoiTuong.ToLower().StartsWith(FilterCapBacLuongModel.LoaiDoiTuong.ToLower());
            return result;
        }


        private bool FilterFunction(TlDmCapBacNq104Model FilterCapBacModel, TlDmCapBacNq104Model item)
        {
            var result = true;
            if (!string.IsNullOrEmpty(FilterCapBacModel.MaCb))
                result = result && item.MaCb.ToLower().StartsWith(FilterCapBacModel.MaCb.ToLower());
            if (!string.IsNullOrEmpty(FilterCapBacModel.TenCb))
                result = result && item.TenCb.ToLower().StartsWith(FilterCapBacModel.TenCb.ToLower());
            if (!string.IsNullOrEmpty(FilterCapBacModel.Note))
                result = result && item.Note.ToLower().StartsWith(FilterCapBacModel.Note.ToLower());
            return result;
        }

        private void SelectAll(bool select, IEnumerable<TlDmCapBacNq104Model> models)
        {
            AfterSelectAll = true;
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
            AfterSelectAll = false;
        }


        private void SelectCapBacLuongAll(bool select, IEnumerable<TlDmCapBacLuongNq104Model> models)
        {
            AfterSelectAll = true;
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
            AfterSelectAll = false;
        }

        private void OnSaveReference()
        {
            try
            {
                if (SelectedCapBacItem != null)
                {
                    if (SelectedCapBacItem.MaCb.Equals(TlDmCapBacNq104Model.MaCb))
                    {
                        MessageBoxHelper.Warning("Mã cấp bậc không hợp lệ");
                        return;
                    }
                }
                DialogHost.CloseDialogCommand.Execute(SelectedCapBacItem, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
                if (_currentCol == nameof(TlDmCapBacLuongNq104Model.TienLuong) || _currentCol == nameof(TlDmCapBacLuongNq104Model.TienNangLuong))
                {
                    var property = ModelTypeCapBacLuong.GetProperty(_currentCol);
                    InitDialog(property);
                }
                if (_currentCol == nameof(TlDmCapBacNq104Model.Parent))
                {
                    var property = ModelType.GetProperty(_currentCol);
                    InitDialog(property);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnCloseWindow()
        {

        }

        private void OnRefreshWithOutReload(object obj)
        {
            FilterCapBacModel = new TlDmCapBacNq104Model();
            _currentCodeValDictionary = new Dictionary<string, int>();
            CapBacItems = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(CapBacItems.Where(t => !t.IsDeleted).OrderBy(s => s.MaCb));
            foreach (var item in CapBacItems)
            {
                item.IsModified = false;
                item.IsDeleted = false;
                item.PropertyChanged += Item_PropertyChanged;
                item.PropertyChanged += TlDmCapBacNq104Model_PropertyChanged;
            }
            _isFirstLoad = true;
            _dataCollectionView = CollectionViewSource.GetDefaultView(CapBacItems);
            _dataCollectionView.SortDescriptions.Clear();
            _dataCollectionView.Filter = ItemsViewFilter;
            _isFirstLoad = false;
            OnPropertyChanged(nameof(CapBacItems));
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
                SelectedCapBacItem = (TlDmCapBacNq104Model)dgdData.Items[0];
                dgdData.CurrentCell = cell;
            }
        }

        private void OnExportExcel()
        {
            //MlnsExportViewModel mlnsExportViewModel = new MlnsExportViewModel(_service)
            //{
            //    sessionService = _sessionService,
            //    Mapper = _mapper,
            //    ServiceProvider = _serviceProvider
            //};
            //mlnsExportViewModel.Init();
            //MlnsExportView Dialog = new MlnsExportView()
            //{
            //    DataContext = mlnsExportViewModel
            //};
            //var dialog = DialogHost.Show(Dialog, "RootDialog", ClosingEventHandler);
        }

        private void OnExportJsonFile()
        {
            _exportService.ExportJsonFile(ExportJsonFileName, CapBacItems);
        }

        private void OnImportExcel()
        {
            //ImportExcelViewModel<TlDmCapBacNq104Model, NsMucLucNganSach, _tlDmCapBacNq104Service, MLNSImportModel> importExcelViewModel =
            //    new ImportExcelViewModel<TlDmCapBacNq104Model, NsMucLucNganSach, _tlDmCapBacNq104Service, MLNSImportModel>(_serviceProvider, _service, _authenticationInfo);
            //importExcelViewModel.DataTemplateFileName = DataTemplateFileName;
            //ImportExcelView importExcelView = new ImportExcelView()
            //{
            //    DataContext = importExcelViewModel
            //};
            //importExcelViewModel.Init();
            //importExcelView.Show();
        }

        private void OnViewDetail()
        {
            if (SelectedCapBacItem == null)
            {
                return;
            }
            GenericControlCustomDetailViewModel genericControlCustomDetailViewModel = new GenericControlCustomDetailViewModel(SelectedCapBacItem)
            {
                Title = Title,
                Description = SelectedCapBacItem?.DetailInfoModalTitle
            };
            genericControlCustomDetailViewModel.ColumnWidth = 260;
            GenericControlCustomViewDetail genericControlCustomViewDetail = new GenericControlCustomViewDetail()
            {
                DataContext = genericControlCustomDetailViewModel
            };
            var dialog = DialogHost.Show(genericControlCustomViewDetail, "RootDialog");
        }

        private void OnSelectMlnsParent()
        {
            //if (CapBacItems.Any(t => t.IsSelected))
            //{
            //    MucLucNganSachViewModel mucLucNganSachViewModel = _serviceProvider.GetService(typeof(MucLucNganSachViewModel)) as MucLucNganSachViewModel;
            //    //mucLucNganSachViewModel.InitBaseData();
            //    mucLucNganSachViewModel.CapBacItems = new ObservableCollection<TlDmCapBacNq104Model>(CapBacItems.Where(t => !t.IsDeleted));
            //    mucLucNganSachViewModel._isFirstLoad = true;
            //    mucLucNganSachViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucNganSachViewModel.CapBacItems);
            //    mucLucNganSachViewModel._dataCollectionView.Filter = mucLucNganSachViewModel.ItemsViewFilter;
            //    mucLucNganSachViewModel._isFirstLoad = false;
            //    MucLucNganSachView = new MucLucNganSachView()
            //    {
            //        DataContext = mucLucNganSachViewModel
            //    };
            //    mucLucNganSachViewModel.MucLucNganSachView = MucLucNganSachView;
            //    DialogClosingEventHandler closeEvent = (sender, eventArgs) =>
            //    {
            //        if (eventArgs.Parameter != null)
            //        {
            //            TlDmCapBacNq104Model TlDmCapBacNq104Model = eventArgs.Parameter as TlDmCapBacNq104Model;
            //            foreach (var item in CapBacItems.Where(t => t.IsSelected))
            //            {
            //                item.MlnsParentName = TlDmCapBacNq104Model.XNM;
            //                item.MlnsIdParent = TlDmCapBacNq104Model.MlnsId;
            //            }
            //            TlDmCapBacNq104Model.BHangCha = true;
            //        }
            //    };
            //    var dialog = DialogHost.Show(MucLucNganSachView, "RootDialog", closeEvent);
            //    mucLucNganSachViewModel.IsDialog = true;
            //}
        }

        private PopupBox FindPopupByName(string elementName)
        {
            PopupBox popupBox = FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
            if (IsDialog)
            {
                return FnCommonUtils.FindChild<PopupBox>(MucLucCapBacLuongView, elementName);
            }
            else
                return FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
        }

        private DataGrid FindDatagrid()
        {
            if (IsDialog)
            {
                return FnCommonUtils.FindChild<DataGrid>(MucLucCapBacLuongView, SelectedDatagrid == 1 ? "DataGridCapBac" : "DataGridCapBacLuong");
            }
            else
                return FnCommonUtils.FindChild<DataGrid>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), SelectedDatagrid == 1 ? "DataGridCapBac" : "DataGridCapBacLuong");
        }

        private void SetSelectedCapBacLuongItem()
        {
            if (SelectedCapBacItem != null && SelectedCapBacItem.Parent != null && SelectedCapBacItem.Parent == PaymentTypeEnum.LoaiCapBac.SyQuan_HamCoYeu)
            {
                GetCapBacLuongItem(SelectedCapBacItem, PaymentTypeEnum.CapBacLuong.SyQuan_HamCoYeu);
            }
            else if (SelectedCapBacItem != null && SelectedCapBacItem.Parent != null && SelectedCapBacItem.Parent == PaymentTypeEnum.LoaiCapBac.QNCN_ChuyenMonKyThuatCoYeu)
            {
                GetCapBacLuongItem(SelectedCapBacItem, PaymentTypeEnum.CapBacLuong.QNCN_ChuyenMonKyThuatCoYeu);
            }
            else if (SelectedCapBacItem != null && SelectedCapBacItem.Parent != null
                && (SelectedCapBacItem.Parent != PaymentTypeEnum.LoaiCapBac.QNCN_ChuyenMonKyThuatCoYeu || SelectedCapBacItem.Parent != PaymentTypeEnum.LoaiCapBac.SyQuan_HamCoYeu))
            {
                GetCapBacLuongItem(SelectedCapBacItem, SelectedCapBacItem.Parent);
            }
            else if (SelectedCapBacItem != null)
            {
                GetCapBacLuongItem(SelectedCapBacItem, SelectedCapBacItem.Parent);
            }
            SelectedDatagrid = 1;
        }

        private void GetCapBacLuongItem(TlDmCapBacNq104Model selectedCapBacItem, string maCapBac)
        {
            if (maCapBac == null)
            {
                var dataLuong = _tlDmCapBacLuongNq104Service.FindAll().Where(x => x.Nam == _authenticationInfo.YearOfWork).OrderBy(x => x.XauNoiMa);
                CapBacLuongItems = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(dataLuong);
            }
            else
            {
                var dataLuong = _tlDmCapBacLuongNq104Service.FindAll().Where(x => x.Nam == _authenticationInfo.YearOfWork && x.LoaiDoiTuong.StartsWith(maCapBac)).OrderBy(x => x.XauNoiMa);
                CapBacLuongItems = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(dataLuong);
                var capBacLuongItemsFilter = CapBacLuongItems.Where(x => x.LoaiDoiTuong.StartsWith(maCapBac));

                if (SelectedCapBacItem.Parent == PaymentTypeEnum.LoaiCapBac.SyQuan_HamCoYeu)
                {
                    var macbNew = selectedCapBacItem.MaCb.Remove(0, 1).Insert(0, PaymentTypeEnum.LoaiCapBac.SyQuan_HamCoYeu);
                    SelectedCapBacLuongItem = capBacLuongItemsFilter.FirstOrDefault(x => x.LoaiDoiTuong.StartsWith(maCapBac) && x.MaDm == selectedCapBacItem.MaCb.Remove(0, 1).Insert(0, LoaiDoiTuong.SQ));
                }
                else if (SelectedCapBacItem.Parent == PaymentTypeEnum.LoaiCapBac.QNCN_ChuyenMonKyThuatCoYeu)
                {
                    SelectedCapBacLuongItem = capBacLuongItemsFilter.FirstOrDefault(x => x.LoaiDoiTuong.StartsWith(maCapBac) && x.MaDm == selectedCapBacItem.MaCb.Remove(0, 1).Insert(0, LoaiDoiTuong.QNCN));
                }
                else
                {
                    SelectedCapBacLuongItem = capBacLuongItemsFilter.FirstOrDefault(x => x.LoaiDoiTuong.StartsWith(maCapBac) && x.MaDm == selectedCapBacItem.MaCb);
                }
            }
            foreach (var item in CapBacLuongItems)
            {
                item.PropertyChanged += Item_PropertyChanged;
                item.PropertyChanged += TlDmCapBacLuongNq104Model_PropertyChanged;
            }
        }

        private void OnUpdateCadres()
        {
            try
            {
                MessageBoxResult dialogResult = MessageBox.Show(Resources.ConfirmUpdateCadres, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                var month = int.Parse(DateTime.Now.ToString("MM"));
                var year = int.Parse(DateTime.Now.ToString("yyyy"));
                var lstCanBo = _canBoRepository.FindByMonthYear(month, year);
                List<TlDmCanBoNq104> lstCanBoUpdate = new List<TlDmCanBoNq104>();

                foreach (var canBo in lstCanBo)
                {
                    if (canBo.MaCb104 != null)
                    {
                        TlDmCapBacLuongNq104 capbacLuong = new TlDmCapBacLuongNq104();
                        if (canBo.LoaiDoiTuong == LoaiDoiTuong.SQ)
                        {
                            capbacLuong = _tlDmCapBacLuongNq104Service.FindByMaCapBac(canBo.MaCb104, year);
                        }
                        else if (canBo.LoaiDoiTuong == LoaiDoiTuong.HCY)
                        {
                            var maCbHCY = canBo.MaCb104.Remove(0, 1).Insert(0, LoaiDoiTuong.SQ);
                            capbacLuong = _tlDmCapBacLuongNq104Service.FindByMaCapBac(maCbHCY, year);
                        }
                        else if (canBo.LoaiDoiTuong == LoaiDoiTuong.QNCN)
                        {
                            var xauNoima = canBo.LoaiDoiTuong + "-" + canBo.Loai + "-" + canBo.NhomChuyenMon + "-" + canBo.MaBacLuong;
                            capbacLuong = _tlDmCapBacLuongNq104Service.FindByXauNoiMa(xauNoima, year);
                        }
                        else if (canBo.LoaiDoiTuong == LoaiDoiTuong.CNQP)
                        {
                            var xauNoima = "3" + "-" + canBo.Loai + "-" + canBo.NhomChuyenMon + "-" + canBo.MaBacLuong;
                            capbacLuong = _tlDmCapBacLuongNq104Service.FindByXauNoiMa(xauNoima, year);
                        }
                        else if (canBo.LoaiDoiTuong == LoaiDoiTuong.VCQP)
                        {
                            var xauNoima = LoaiDoiTuong.HCY + "-" + canBo.Loai + "-" + canBo.NhomChuyenMon + "-" + canBo.MaBacLuong;
                            capbacLuong = _tlDmCapBacLuongNq104Service.FindByXauNoiMa(xauNoima, year);
                        }
                        else if (canBo.LoaiDoiTuong == LoaiDoiTuong.CMKTCY)
                        {
                            var xauNoima = LoaiDoiTuong.QNCN + "-" + canBo.Loai + "-" + canBo.NhomChuyenMon + "-" + canBo.MaBacLuong;
                            capbacLuong = _tlDmCapBacLuongNq104Service.FindByXauNoiMa(xauNoima, year);
                        }
                        else
                        {
                            var xauNoima = canBo.LoaiDoiTuong + "-" + canBo.MaBacLuong;
                            capbacLuong = _tlDmCapBacLuongNq104Service.FindByXauNoiMa(xauNoima, year);
                        }

                        canBo.TienLuongCb = capbacLuong?.TienLuong ?? 0;
                        canBo.TienNangLuongCb = capbacLuong?.TienNangLuong ?? 0;
                        lstCanBoUpdate.Add(canBo);

                        updatePhuCapCanBo(canBo, year, capbacLuong);
                        _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(month, year);
                    }
                }
                if (lstCanBoUpdate.Any())
                    _canBoRepository.UpdateRange(lstCanBoUpdate);

                MessageBoxHelper.Info(Resources.MsgSaveDone);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void updatePhuCapCanBo(TlDmCanBoNq104 canBo, int year, TlDmCapBacLuongNq104 capbacLuong)
        {
            var canBoPhuCap = _tlCanBoPhuCapService.FindByMaCbo(canBo.MaCanBo).FirstOrDefault();
            var allowences = _phuCapService.FindAll(x => x.Nam == year);
            var allowencesSaved = new List<AllowencePhuCapNq104Criteria>();

            if (canBoPhuCap != null && capbacLuong != null)
            {
                var plainText = CompressExtension.DecompressFromBase64(canBoPhuCap.Data);
                allowencesSaved = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(plainText).X.ToList();

                allowencesSaved.FirstOrDefault(x => x.A.Equals("LCB_TT")).B = capbacLuong.TienLuong;
                allowencesSaved.FirstOrDefault(x => x.A.Equals("NLCB_TT")).B = capbacLuong.TienNangLuong;
                allowencesSaved.FirstOrDefault(x => x.A.Equals("LBLCB_TT")).B = (canBo.TienLuongCbCu - (capbacLuong.TienLuong + capbacLuong.TienNangLuong)) > 0 ? (canBo.TienLuongCbCu - (capbacLuong.TienLuong + capbacLuong.TienNangLuong)) : null;

                string strJson = JsonConvert.SerializeObject(new AllowenceCanBoNq104Criteria()
                {
                    X = allowencesSaved
                });
                canBoPhuCap.MaPhuCap = "";
                canBoPhuCap.Data = CompressExtension.CompressToBase64(strJson);
                _tlCanBoPhuCapService.Update(canBoPhuCap);
            }
        }
    }
}
