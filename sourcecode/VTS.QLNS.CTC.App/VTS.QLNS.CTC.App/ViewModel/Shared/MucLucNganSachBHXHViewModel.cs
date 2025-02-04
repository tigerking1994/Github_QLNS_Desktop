using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.View.Shared.Import;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.ImportViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class MucLucNganSachBHXHViewModel : GridViewModelBase<BhDmMucLucNganSachModel>
    {
        public ILog _logger;
        public BhDmMucLucNganSachService _service;
        public TlDmPhuCapService _tlDmPhuCapService;
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
        public ITlDmCheDoBHXHService _cheDoBHXHSerVice;
        public string POPUP_SUFFIX = "popupbox";
        private const string TEXTBOX_SUFFIX = "txtbox";

        public override string Name => "Danh mục MLNS";
        public override string Title => "Danh mục mục lục ngân sách - BHXH";
        public override string Description => "Chỉnh sửa thông tin mục lục ngân sách";
        public string ExportJsonFileName = "mlns.json";
        public string TemplateFileName = "rpt_dm_mlns.xlsx";
        public Type ImportModelType = typeof(MLNSImportModel);

        public override Type ContentType => typeof(MucLucNganSachBHXHView);
        public Type ModelType => typeof(BhDmMucLucNganSachModel);
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

        private bool _isShowCheckBox;
        public bool IsShowCheckBox
        {
            get => _isShowCheckBox;
            set
            {
                SetProperty(ref _isShowCheckBox, value);
                OnPropertyChanged(nameof(IsShowCheckBox));
            }
        }

        public bool CheckAllForDanhMuc;
        public bool IsDanhMucChuky { get; set; }
        public bool IsMultipleSelect { get; set; }
        public Visibility VisibilityDialogBtn => IsDialog ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibilityFunctionBtn => IsDanhMucChuky ? Visibility.Collapsed : Visibility.Visible;
        public string Hint => IsDialog ? "Ấn phím space để chọn checkbox nếu có" : "";
        public string Total => " (Tổng số bản ghi: " + Items.Count() + ")";
        public string ModelName => "dgdDataBhMLNS";

        public bool AfterSelectAll;

        private string _filtertext;
        public string FilterText
        {
            get => _filtertext;
            set => SetProperty(ref _filtertext, value);
        }
        public TlDmCheDoBHXHModel TlDmCheDoBHXHModel { get; set; }
        public bool IsMappingRegime { get; set; }
        private BhDmMucLucNganSachModel _filterModel;
        public BhDmMucLucNganSachModel FilterModel
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
                    SelectAll(value.Value, Items);
                }
            }
        }
        public MucLucNganSachBHXHView MucLucNganSachBHXHView { get; set; }
        public MucLucDanhMucLoaiChiView MucLucDanhMucLoaiChiView { get; set; }
        public PhuCapDialogViewModel PhuCapDialogViewModel { get; }
        //public MLNSDialogViewModel MLNSDialogViewModel { get; }

        private List<string> mlnsType = new List<string>
        {
          "SLNS", "SL", "SK", "SM", "STM", "STTM", "SNG", "STNG", "STNG1", "STNG2", "STNG3"
        };

        private List<string> mlnsDuToanDieuChinhType = new List<string>
        {
            "STNG", "SNG", "STNG1", "STNG2", "STNG3"
        };
        private string xnmConcatenation = "";
        private ICollection<BhDmMucLucNganSachModel> _filterResult = new HashSet<BhDmMucLucNganSachModel>();
        private DanhMuc mlnsChiTietToi { get; set; }

        public ObservableCollection<ComboboxItem> LstTrangThai { get; set; }
        public ObservableCollection<ComboboxItem> BHangChaChiTietToi { get; set; }
        public ObservableCollection<ComboboxItem> BHangChaDTDieuChinhChiTietToi { get; set; }
        public ObservableCollection<ComboboxItem> CPChiTietToi { get; set; }
        public ObservableCollection<ComboboxItem> LstCapBac { get; set; }
        public ObservableCollection<ComboboxItem> ItemsUnit { get; set; }

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
        public string TenPhuCap { get; set; }
        public BhDmMucLucNganSachModel BhDmMucLucNganSachModel { get; set; }
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
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ViewDetailCommand { get; }
        public RelayCommand FilterCommand { get; }
        public RelayCommand EscapteFilterCommand { get; }
        public RelayCommand SelectMlnsParentCommand { get; }
        public RelayCommand SelectPhuCapCommand { get; }
        public RelayCommand MapMLNSCommand { get; }
        public RelayCommand MapPhuCapCommand { get; }
        public RelayCommand CheckDataCommand { get; }

        public MucLucNganSachBHXHViewModel(IBhDmMucLucNganSachService service,
            TlDmPhuCapService tlDmPhuCapService,
            IMapper mapper,
            ISessionService sessionService,
            IExportService exportService,
            IServiceProvider serviceProvider,
            MucLucNganSachCheckDataViewModel mucLucNganSachCheckDataViewModel,
            PhuCapDialogViewModel phuCapDialogViewModel,
            //MLNSDialogViewModel mlnsDialogViewModel,
            ITlDmCheDoBHXHService iTlDmCheDoBHXHService)
        {
            _service = service as BhDmMucLucNganSachService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _serviceProvider = serviceProvider;
            MucLucNganSachCheckDataViewModel = mucLucNganSachCheckDataViewModel;
            PhuCapDialogViewModel = phuCapDialogViewModel;
            //MLNSDialogViewModel = mlnsDialogViewModel;
            _cheDoBHXHSerVice = iTlDmCheDoBHXHService;

            SaveReferenceCommand = new RelayCommand(obj => OnSaveReference());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            SelectedCellChangeCommand = new RelayCommand(obj => _currentCol = obj.ToString());
            OpenFilterPopupCommand = new RelayCommand(obj => ToggleFilterPopup(true));
            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
            ExportCommand = new RelayCommand(obj => OnExportExcel());
            ImportExcelCommand = new RelayCommand(obj => OnImportExcel());
            AddChildCommand = new RelayCommand(obj => OnAddChild(obj));
            ViewDetailCommand = new RelayCommand(obj => OnViewDetail(), obj => !IsDialog);
            FilterCommand = new RelayCommand(o => OnFilterDataByColumn(o.ToString()));
            EscapteFilterCommand = new RelayCommand(o => ToggleFilterPopup(false, o.ToString() + POPUP_SUFFIX));
            SelectMlnsParentCommand = new RelayCommand(o => OnSelectMlnsParent());
            SelectPhuCapCommand = new RelayCommand(o => OnSelectPhuCap());
            MapMLNSCommand = new RelayCommand(o => OnMapMLNS());
            MapPhuCapCommand = new RelayCommand(o => OnMapPhuCap());
            CheckDataCommand = new RelayCommand(obj => CheckData());
        }

        public override void Init()
        {
            try
            {
                InitBaseData();
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void CheckData()
        {

            MucLucNganSachCheckDataViewModel.Name = "Kiểm tra mục lục ngân sách";
            MucLucNganSachCheckDataViewModel.Description = $"Danh sách chứng từ sử dụng MLNS {SelectedItem.SXauNoiMa}";
            var listXauNoiMaDuToan = Items.Where(n => n.BHangChaDuToan.HasValue && !n.BHangChaDuToan.Value && n.IIDMLNSCha == SelectedItem.IIDMLNS).Select(n => n.SXauNoiMa).ToList();
            listXauNoiMaDuToan.Add(SelectedItem.SXauNoiMa);
            var listXauNoiMaQuyetToan = Items.Where(n => n.BHangChaQuyetToan.HasValue && !n.BHangChaQuyetToan.Value && n.IIDMLNSCha == SelectedItem.IIDMLNS).Select(n => n.SXauNoiMa).ToList();
            listXauNoiMaQuyetToan.Add(SelectedItem.SXauNoiMa);
            MucLucNganSachCheckDataViewModel.CodeChain = SelectedItem.SXauNoiMa;
            MucLucNganSachCheckDataViewModel.CodeChainDuToan = string.Join(StringUtils.COMMA, listXauNoiMaDuToan);
            MucLucNganSachCheckDataViewModel.CodeChainQuyetToan = string.Join(StringUtils.COMMA, listXauNoiMaQuyetToan);
            MucLucNganSachCheckDataViewModel.Init();

            MucLucNganSachCheckDataViewModel.DeletedActionHandler += (object sender, EventArgs e) =>
            {
                OnRefresh(sender);
            };
            var exportView = new MucLucNganSachCheckDataView() { DataContext = MucLucNganSachCheckDataViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        private void InitBaseData()
        {
            LstTrangThai = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
                };
            ItemsUnit = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Ngày", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "Tháng", ValueItem = "2" },
                    new ComboboxItem { DisplayItem = "Người", ValueItem = "3" }
                };
            BHangChaChiTietToi = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = null },
                    new ComboboxItem { DisplayItem = "LNS", ValueItem = "LNS" },
                    new ComboboxItem { DisplayItem = "L", ValueItem = "L" },
                    new ComboboxItem { DisplayItem = "K", ValueItem = "K" },
                    new ComboboxItem { DisplayItem = "M", ValueItem = "M" },
                    new ComboboxItem { DisplayItem = "TM", ValueItem = "TM" },
                    new ComboboxItem { DisplayItem = "TTM", ValueItem = "TTM" },
                    new ComboboxItem { DisplayItem = "NG", ValueItem = "NG" },
                    new ComboboxItem { DisplayItem = "TNG", ValueItem = "TNG" },
                };
            BHangChaDTDieuChinhChiTietToi = new ObservableCollection<ComboboxItem>
                {

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
            List<string> mlnsBHXHType = new List<string>
                {
                    "LNS", "L", "K", "M", "TM", "TTM", "NG", "TNG", "TNG1", "TNG2", "TNG3"
                };

            LstCapBac = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = "" },
                    new ComboboxItem { DisplayItem = "Sĩ quan", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "Quân nhân chuyên nghiệp", ValueItem = "2" },
                    new ComboboxItem { DisplayItem = "CC, CN, VCQP", ValueItem = "3" },
                    new ComboboxItem { DisplayItem = "HSQ-CS", ValueItem = "4" },
                    new ComboboxItem { DisplayItem = "LĐHĐ", ValueItem = "43" }
                };

            BhDmMucLucNganSachService mucLucNganSachService = _service;
            mlnsChiTietToi = mucLucNganSachService.FindMLNSChiTietToi(_sessionService.Current.YearOfWork);
            if (mlnsChiTietToi != null)
            {
                foreach (var col in mlnsBHXHType)
                {
                    int indexMLnsChiTietToi = mlnsBHXHType.IndexOf(mlnsChiTietToi.SGiaTri);
                    int indexOfMlnsColumn = mlnsBHXHType.IndexOf(col);
                    PropertyInfo column = typeof(MucLucNganSachBHXHViewModel).GetProperty(col);
                    if (indexOfMlnsColumn > indexMLnsChiTietToi)
                    {
                        // hide col
                        column.SetValue(this, false);
                    }
                    else
                    {
                        column.SetValue(this, true);
                    }
                }
            }
            base.Init();
            _authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
            _checkboxDictionary = new Dictionary<string, bool>();
            _formatDictionary = new Dictionary<string, string>();
            _currentCodeValDictionary = new Dictionary<string, int>();
            _filterModel = new BhDmMucLucNganSachModel();
            if (_sessionService.Current.AutoGenerateDataSetting.ContainsKey(typeof(BhDmMucLucNganSachModel).Name))
            {
                _formatDictionary = _sessionService.Current.AutoGenerateDataSetting[typeof(BhDmMucLucNganSachModel).Name];
            }
            else
            {
                _sessionService.Current.AutoGenerateDataSetting[typeof(BhDmMucLucNganSachModel).Name] = _formatDictionary;
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var data = _service.FindAll(_authenticationInfo);
            Items = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(data);
            foreach (var item in Items)
            {
                string typeDTDCMLNS = GetTypeOfMlnsDTDC(item);
                item.IsEnableDuToanNGCombobox = item.IsUsedDuToanChiTietToi;
                item.IsEnableQuyetToanNGCombobox = item.IsUsedQuyetToanChiTietToi;
                item.IsEnableDuToanDieuChinhNGCombobox = typeDTDCMLNS.Equals("SNG") && item.IsUsedDuToanDieuChinhChiTietToi;
                item.PropertyChanged += Item_PropertyChanged;
                item.PropertyChanged += BhDmMucLucNganSachModel_PropertyChanged;
                item.STenPhuCap = GetTenPhuCap(item.SMaPhuCap);

                if (CheckAllForDanhMuc)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                        {
                            foreach (var item1 in Items)
                            {
                                if (item1.IIDMLNSCha == item.IIDMLNS)
                                {
                                    item1.IsSelected = item.IsSelected;
                                }
                            }

                        }
                    };
                }
            }
            _isFirstLoad = true;
            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            _dataCollectionView.Filter = ItemsViewFilter;
            OnPropertyChanged(nameof(Items));
            _isFirstLoad = false;
        }

        private string GetTenPhuCap(string maPCs)
        {
            if (!string.IsNullOrEmpty(maPCs))
            {
                List<string> lstMaPC = maPCs.Split(",").ToList();
                List<string> lstTenPheCap = new List<string>();
                foreach (var item in lstMaPC)
                {
                    var phuCapData = _tlDmPhuCapService.FindByMaPhuCap(item);
                    lstTenPheCap.Add(phuCapData.TenPhuCap);
                }
                return string.Join(", ", lstTenPheCap);
            }
            return string.Empty;
        }

        private string GetTypeOfMlns(BhDmMucLucNganSachModel bhDmMucLucNganSachModel)
        {
            foreach (string type in mlnsType)
            {
                PropertyInfo propertyInfo = typeof(BhDmMucLucNganSachModel).GetProperty(type);
                object val = propertyInfo.GetValue(bhDmMucLucNganSachModel, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return type;
                }
            }
            return "";
        }

        private string GetTypeOfMlnsDTDC(BhDmMucLucNganSachModel bhDmMucLucNganSachModel)
        {
            foreach (string type in mlnsDuToanDieuChinhType)
            {
                PropertyInfo propertyInfo = typeof(BhDmMucLucNganSachModel).GetProperty(type);
                object val = propertyInfo.GetValue(bhDmMucLucNganSachModel, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return type;
                }
            }
            return "";
        }

        protected override void OnItemsChanged()
        {
            OnPropertyChanged("Total");
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter == null)
                return;
            BhDmMucLucNganSachModel BhDmMucLucNganSachModel = eventArgs.Parameter as BhDmMucLucNganSachModel;
            if (BhDmMucLucNganSachModel == null)
            {
                return;
            }
            if (SelectedItem.XNM.Equals(BhDmMucLucNganSachModel.XNM) || !SelectedItem.XNM.StartsWith(BhDmMucLucNganSachModel.XNM))
            {
                MessageBoxHelper.Warning("MLNS không hợp lệ");
                return;
            }
            BhDmMucLucNganSachModel.BHangCha = true;
            SelectedItem.MlnsParentName = BhDmMucLucNganSachModel.XNM;
            SelectedItem.IIDMLNSCha = BhDmMucLucNganSachModel.IIDMLNS;
        }

        protected override void OnAdd(object obj)
        {
            try
            {
                DataGrid dgdData = FindDatagrid();
                this.CancelEditData(dgdData);

                int currentRow = Items.Count - 1;
                BhDmMucLucNganSachModel newRow;
                if (SelectedItem == null)
                {
                    newRow = new BhDmMucLucNganSachModel();
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
                newRow.PropertyChanged += BhDmMucLucNganSachModel_PropertyChanged;
                OnPropertyChanged(newRow);
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

        public void OnAddChild(object obj)
        {
            if (SelectedItem == null)
                return;
            List<string> mlnsAddChildTypes = new List<string>
            {
                "LNS", "L", "K", "M", "TM", "TTM", "NG", "TNG", "TNG1", "TNG2", "TNG3"
            };
            if (mlnsChiTietToi != null)
            {
                string mlnsType = GetTypeOfMlns(SelectedItem);
                int indexMLnsChiTietToi = mlnsAddChildTypes.IndexOf(mlnsChiTietToi.SGiaTri);
                int indexOfMlnsColumn = mlnsAddChildTypes.IndexOf(mlnsType);
                if (indexMLnsChiTietToi <= indexOfMlnsColumn)
                {
                    MessageBoxHelper.Info("Cấu hình chi tiết tới là " + mlnsChiTietToi.SGiaTri + ", không thể thêm mới dòng con cho mlns " + SelectedItem.XNM);
                    return;
                }
            }
            DataGrid dgdData = FindDatagrid();
            this.CancelEditData(dgdData);

            int currentRow = Items.IndexOf(SelectedItem);
            BhDmMucLucNganSachModel parent = SelectedItem;
            BhDmMucLucNganSachModel newRow = ObjectCopier.Clone(SelectedItem);
            newRow.Id = Guid.Empty;
            newRow.IIDMLNS = Guid.NewGuid();
            newRow.IIDMLNSCha = parent.IIDMLNS;
            newRow.IsModified = true;
            newRow.BHangCha = false;
            parent.BHangCha = true;
            newRow.BHangChaDuToan = null;
            newRow.BHangChaQuyetToan = null;
            newRow.MlnsParentName = parent.XNM;
            newRow.IsEditableStatus = true;
            // nếu dòng mới là ng,tng,tng1,2,3 thì cần update bhangchadutoan và bhangcha quyết toán
            var parentRowType = GetTypeOfMlns(parent);
            if (mlnsAddChildTypes.IndexOf(parentRowType) > -1)
            {
                // find ng parent
                var ngParent = Items.FirstOrDefault(i => "NG".Equals(GetTypeOfMlns(i)) && newRow.XNM.Contains(i.XNM) &&
                    (!string.IsNullOrWhiteSpace(i.SDuToanChiTietToi) || !string.IsNullOrWhiteSpace(i.SQuyetToanChiTietToi)));
                if (ngParent != null)
                {
                    ngParent.IsModified = true;
                }
            }
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += BhDmMucLucNganSachModel_PropertyChanged;
            Items.Insert(currentRow + 1, newRow);
            OnPropertyChanged(nameof(Items));
            var cell = new DataGridCellInfo(Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        protected override void OnDelete(object obj)
        {
            try
            {
                IBhDmMucLucNganSachService mucLucNganSachService = _service as IBhDmMucLucNganSachService;
                if (mucLucNganSachService.IsUsedMLNS(SelectedItem.IIDMLNS, _authenticationInfo.YearOfWork))
                {
                    MessageBox.Show(Resources.UnableToDeleteUsedMLNS, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (!SelectedItem.IsDeleted && SelectedItem.BHangCha)
                {
                    var confirmRs = MessageBoxHelper.Confirm("MLNS đã có mlns phụ thuộc, bạn có chắc chắn muỗn xóa?");
                    if (confirmRs != MessageBoxResult.Yes)
                        return;
                }
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                if (SelectedItem.IsDeleted)
                {
                    DataGrid dgdData = FindDatagrid();
                    this.CancelEditData(dgdData);
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
                        var dataToSave = _mapper.Map<IEnumerable<BhDmMucLucNganSach>>(Items.Where(i => i.IsModified || i.IsDeleted));
                        foreach (var i in dataToSave)
                        {
                            if (i.IsDeleted)
                                i.IsModified = false;
                            OnTrimProperty(i);
                        }
                        _service.AddOrUpdateRange(dataToSave, _authenticationInfo);
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

        protected override void OnRefresh(object obj)
        {
            try
            {
                var rawData = _service.FindAll(_authenticationInfo);
                foreach (var i in rawData)
                {
                    i.IsModified = false;
                    i.IsDeleted = false;
                }
                FilterModel = new BhDmMucLucNganSachModel();
                _currentCodeValDictionary = new Dictionary<string, int>();
                Items.Clear();
                Items = new ObservableCollection<BhDmMucLucNganSachModel>();
                Items = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(rawData);
                foreach (var item in Items)
                {
                    string typeMLNS = GetTypeOfMlns(item);
                    string typeDTDCMLNS = GetTypeOfMlns(item);
                    item.IsEnableDuToanNGCombobox = item.IsUsedDuToanChiTietToi;
                    item.IsEnableQuyetToanNGCombobox = item.IsUsedQuyetToanChiTietToi;
                    item.IsEnableDuToanDieuChinhNGCombobox = typeDTDCMLNS.Equals("SNG") && !item.IsUsedDuToanDieuChinhChiTietToi;
                    item.PropertyChanged += Item_PropertyChanged;
                    item.PropertyChanged += BhDmMucLucNganSachModel_PropertyChanged;
                    item.STenPhuCap = GetTenPhuCap(item.SMaPhuCap);
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
                    SelectedItem = (BhDmMucLucNganSachModel)dgdData.Items[0];
                    dgdData.CurrentCell = cell;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDmMucLucNganSachModel.MlnsParentName)))
            {
                MucLucNganSachBHXHViewModel mucLucNganSachViewModel = _serviceProvider.GetService(typeof(MucLucNganSachBHXHViewModel)) as MucLucNganSachBHXHViewModel;
                mucLucNganSachViewModel.InitBaseData();
                mucLucNganSachViewModel.Items = new ObservableCollection<BhDmMucLucNganSachModel>(Items.Where(t => !t.IsDeleted));
                mucLucNganSachViewModel._isFirstLoad = true;
                mucLucNganSachViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucNganSachViewModel.Items);
                mucLucNganSachViewModel._dataCollectionView.Filter = mucLucNganSachViewModel.ItemsViewFilter;
                mucLucNganSachViewModel._isFirstLoad = false;
                mucLucNganSachViewModel.BhDmMucLucNganSachModel = SelectedItem;
                MucLucNganSachBHXHView = new MucLucNganSachBHXHView()
                {
                    DataContext = mucLucNganSachViewModel
                };
                mucLucNganSachViewModel.MucLucNganSachBHXHView = MucLucNganSachBHXHView;
                var dialog = DialogHost.Show(MucLucNganSachBHXHView, "RootDialog", ClosingEventHandler);
                mucLucNganSachViewModel.IsDialog = true;
                mucLucNganSachViewModel.Name = "Chọn MLNS cha cho mlns " + SelectedItem.XNM;
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
                if (d.Items.Count > 0)
                {
                    var cell = new DataGridCellInfo(d.Items[0], d.Columns[0]);
                    SelectedItem = (BhDmMucLucNganSachModel)d.Items[0];
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
                        SelectedItem = (BhDmMucLucNganSachModel)d.Items[0];
                        d.CurrentCell = cell;
                    }
                }
            }
        }

        private void BeForeRefresh()
        {
            var filterModel = FilterModel;
            _filterResult = Items.Where(item => FilterFunction(FilterModel, item)).Where(item => !item.BHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.XNM).ToHashSet());
        }

        private bool validate()
        {
            var dataToSave = Items.Where(i => i.IsModified && !i.IsDeleted);
            foreach (BhDmMucLucNganSachModel model in dataToSave)
            {
                int selectedSDuToanIndex = mlnsType.IndexOf(model.SDuToanChiTietToi);
                int selectedSQTIndex = mlnsType.IndexOf(model.SQuyetToanChiTietToi);
                if (selectedSQTIndex > selectedSDuToanIndex)
                {
                    MessageBoxHelper.Error(Resources.MLNS_DT_QT_Err);
                    return false;
                }

                if (!ValidateViewModelHelper.Validate(model))
                    return false;
            }
            return true;
        }

        private void BeforeSave()
        {
            // get saved ng type
            List<BhDmMucLucNganSachModel> Parent = new List<BhDmMucLucNganSachModel>();
            var dataToSave = Items.Where(i => i.IsModified && !i.IsDeleted).ToList();
            foreach (BhDmMucLucNganSachModel model in dataToSave)
            {
                string typeMLNS = GetTypeOfMlns(model);
                if ("NG".Equals(typeMLNS) &&
                (!string.IsNullOrWhiteSpace(model.SDuToanChiTietToi) || !string.IsNullOrWhiteSpace(model.SQuyetToanChiTietToi)) || !string.IsNullOrWhiteSpace(model.SDuToanDieuChinhChiTietToi))
                {
                    List<BhDmMucLucNganSachModel> children = Items.Where(t => t.XNM.Contains(model.XNM + "-")).ToList();
                    // update bhangcha du toan vaf bhangcha quyet toan
                    int selectedSDuToanIndex = mlnsType.IndexOf(model.SDuToanChiTietToi);
                    int selectedSQTIndex = mlnsType.IndexOf(model.SQuyetToanChiTietToi);
                    string valueCollumSDuToanDieuChinhChiTietToi = "S" + model.SDuToanDieuChinhChiTietToi;
                    int selectedSDuToanDieuChinhIndex = mlnsDuToanDieuChinhType.IndexOf(valueCollumSDuToanDieuChinhChiTietToi);
                    string modelNsType = GetTypeOfMlns(model);
                    string modelBHXHType = GetTypeOfMlnsDTDC(model);
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    int modelBhTypeIndex = mlnsDuToanDieuChinhType.IndexOf(modelBHXHType);
                    UpdateBHangChaDuToan(selectedSDuToanIndex, modelNsTypeIndex, model);
                    UpdateBHangChaDuToanDieuChinh(selectedSDuToanDieuChinhIndex, modelBhTypeIndex, model);
                    UpdateBHangChaQuyetToan(selectedSQTIndex, modelNsTypeIndex, model);
                    foreach (BhDmMucLucNganSachModel ns in children)
                    {
                        //valueCollumSDuToanDieuChinhChiTietToi = ns.SDuToanDieuChinhChiTietToi;
                        ns.IsModified = true;
                        string nsType = GetTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        string nsBHXHType = GetTypeOfMlnsDTDC(ns);
                        int bhTypeIndex = mlnsDuToanDieuChinhType.IndexOf(nsBHXHType);
                        UpdateBHangChaDuToanDieuChinh(selectedSDuToanDieuChinhIndex, bhTypeIndex, ns);
                        UpdateBHangChaDuToan(selectedSDuToanIndex, nsTypeIndex, ns);
                        UpdateBHangChaQuyetToan(selectedSQTIndex, nsTypeIndex, ns);
                    }
                }
                if (string.IsNullOrEmpty(typeMLNS))
                {
                    model.BHangChaDuToan = true;
                    model.BHangChaQuyetToan = true;
                }
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

        private void OnPropertyChanged(BhDmMucLucNganSachModel model)
        {
            model.PropertyChanged += BhDmMucLucNganSachModel_PropertyChanged;
        }

        private void CustomValueProps(BhDmMucLucNganSachModel newRow, BhDmMucLucNganSachModel currentRow)
        {
            newRow.Id = Guid.Empty;
            newRow.IsModified = true;
            newRow.IIDMLNS = Guid.NewGuid();
            newRow.BHangCha = false;
            newRow.IsEditableStatus = true;
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals(nameof(ModelBase.IsDeleted)) && !e.PropertyName.Equals(nameof(ModelBase.IsSelected)) && !e.PropertyName.Equals(nameof(ModelBase.IsModified)))
                ((ModelBase)sender).IsModified = true;

            if (IsMultipleSelect && e.PropertyName.Equals(nameof(ModelBase.IsSelected)))
            {
                OnPropertyChanged(nameof(IsAllItemsSelected));
            }
        }

        private void BhDmMucLucNganSachModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<BhDmMucLucNganSachModel> models = Items;
            BhDmMucLucNganSachModel BhDmMucLucNganSachModel = sender as BhDmMucLucNganSachModel;
            // khi tích vào ô tất cả, tránh để hàm propertychanged lặp lại để đảm bảo performance (dùng biến afterSelectAll để kiểm tra điều kiện người dùng vừa tích vào ô tất cả)
            if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsDeleted))
            {
                BhDmMucLucNganSachModel_OnDeleteMLNS(BhDmMucLucNganSachModel, models);
            }
            else if (args.PropertyName == nameof(BhDmMucLucNganSachModel.ITrangThai))
            {
                IEnumerable<BhDmMucLucNganSachModel> children = models.Where(t => BhDmMucLucNganSachModel.IIDMLNS.Equals(t.IIDMLNSCha));
                foreach (var c in children)
                {
                    c.ITrangThai = BhDmMucLucNganSachModel.ITrangThai;
                }
            }
            else if (mlnsType.Contains(args.PropertyName))
            {
                string typeMLNS = GetTypeOfMlns(BhDmMucLucNganSachModel);
                string typeDTDCMLNS = GetTypeOfMlnsDTDC(BhDmMucLucNganSachModel);
                if (!typeMLNS.Equals("SNG"))
                {
                    BhDmMucLucNganSachModel.SDuToanChiTietToi = "";
                    BhDmMucLucNganSachModel.SQuyetToanChiTietToi = "";
                }
                BhDmMucLucNganSachModel.IsEnableDuToanNGCombobox = BhDmMucLucNganSachModel.IsUsedDuToanChiTietToi;
                BhDmMucLucNganSachModel.IsEnableQuyetToanNGCombobox = BhDmMucLucNganSachModel.IsUsedQuyetToanChiTietToi;
                BhDmMucLucNganSachModel.IsEnableDuToanDieuChinhNGCombobox = typeDTDCMLNS.Equals("SNG") && !BhDmMucLucNganSachModel.IsUsedQuyetToanChiTietToi;
            }
            else if (nameof(BhDmMucLucNganSachModel.SDuToanChiTietToi).Equals(args.PropertyName))
            {
                List<BhDmMucLucNganSachModel> children = models.Where(t => t.XNM.Contains(BhDmMucLucNganSachModel.XNM + "-")).ToList();
                string typeMLNS = GetTypeOfMlns(BhDmMucLucNganSachModel);
                if (typeMLNS.Equals("SNG"))
                {
                    // todo update bHangChaDuToan of children
                    int selectedSDuToanIndex = mlnsType.IndexOf(BhDmMucLucNganSachModel.SDuToanChiTietToi);
                    string modelNsType = "SNG";
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaDuToan(selectedSDuToanIndex, modelNsTypeIndex, BhDmMucLucNganSachModel);
                    foreach (BhDmMucLucNganSachModel ns in children)
                    {
                        string nsType = GetTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        UpdateBHangChaDuToan(selectedSDuToanIndex, nsTypeIndex, ns);
                    }
                }
            }
            else if (nameof(BhDmMucLucNganSachModel.SQuyetToanChiTietToi).Equals(args.PropertyName))
            {
                List<BhDmMucLucNganSachModel> children = models.Where(t => t.XNM.Contains(BhDmMucLucNganSachModel.XNM + "-")).ToList();
                string typeMLNS = GetTypeOfMlns(BhDmMucLucNganSachModel);
                if (typeMLNS.Equals("SNG"))
                {
                    // todo update bHangChaDuToan of children
                    int selectedSQTIndex = mlnsType.IndexOf(BhDmMucLucNganSachModel.SQuyetToanChiTietToi);
                    string modelNsType = "SNG";
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaQuyetToan(selectedSQTIndex, modelNsTypeIndex, BhDmMucLucNganSachModel);
                    foreach (BhDmMucLucNganSachModel ns in children)
                    {
                        string nsType = GetTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        UpdateBHangChaQuyetToan(selectedSQTIndex, nsTypeIndex, ns);
                    }
                }
            }

            else if (nameof(BhDmMucLucNganSachModel.SDuToanDieuChinhChiTietToi).Equals(args.PropertyName))
            {
                List<BhDmMucLucNganSachModel> children = models.Where(t => t.XNM.Contains(BhDmMucLucNganSachModel.XNM + "-")).ToList();
                string typeMLNS = GetTypeOfMlnsDTDC(BhDmMucLucNganSachModel);
                if (typeMLNS.Equals("SNG"))
                {
                    string sDuToanDieuChinhChiTietToi = "S" + BhDmMucLucNganSachModel.SDuToanDieuChinhChiTietToi;
                    // todo update bHangChaDuToan of children
                    int selectedSQTIndex = mlnsDuToanDieuChinhType.IndexOf(sDuToanDieuChinhChiTietToi);
                    string modelNsType = "SNG";
                    int modelNsTypeIndex = mlnsDuToanDieuChinhType.IndexOf(modelNsType);
                    UpdateBHangChaDuToanDieuChinh(selectedSQTIndex, modelNsTypeIndex, BhDmMucLucNganSachModel);
                    foreach (BhDmMucLucNganSachModel ns in children)
                    {
                        string nsType = GetTypeOfMlnsDTDC(ns);
                        int nsTypeIndex = mlnsDuToanDieuChinhType.IndexOf(nsType);
                        UpdateBHangChaDuToanDieuChinh(selectedSQTIndex, nsTypeIndex, ns);
                    }
                }
            }

            else if (nameof(BhDmMucLucNganSachModel.SCPChiTietToi).Equals(args.PropertyName))
            {
                List<BhDmMucLucNganSachModel> children = models.Where(t => t.XNM.Contains(BhDmMucLucNganSachModel.XNM + "-")).ToList();
                string typeMLNS = GetTypeOfMlns(BhDmMucLucNganSachModel);
                if (typeMLNS.Equals("SNG"))
                {
                    // todo update bHangChaDuToan of children
                    int selectedSQTIndex = mlnsType.IndexOf(BhDmMucLucNganSachModel.SQuyetToanChiTietToi);
                    string modelNsType = "SNG";
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaQuyetToan(selectedSQTIndex, modelNsTypeIndex, BhDmMucLucNganSachModel);
                    foreach (BhDmMucLucNganSachModel ns in children)
                    {
                        string nsType = GetTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        UpdateBHangChaQuyetToan(selectedSQTIndex, nsTypeIndex, ns);
                    }
                }
            }
        }

        private void BhDmMucLucNganSachModel_OnDeleteMLNS(BhDmMucLucNganSachModel model, IEnumerable<BhDmMucLucNganSachModel> models)
        {
            BhDmMucLucNganSachModel parent = models.FirstOrDefault(i => i.IIDMLNS == model.IIDMLNSCha);
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.IIDMLNSCha == parent.IIDMLNS && !i.IsDeleted);
            parent.BHangCha = hasChild;
            foreach (var item in Items.Where(t => t.IIDMLNSCha.Equals(SelectedItem.IIDMLNS)))
            {
                item.MlnsParentName = string.Empty;
                item.IIDMLNSCha = null;
            }
        }

        private void UpdateBHangChaDuToan(int selectedSDuToanIndex, int nsTypeIndex, BhDmMucLucNganSachModel ns)
        {
            if (selectedSDuToanIndex > -1)
            {
                if (nsTypeIndex > selectedSDuToanIndex)
                {
                    ns.BHangChaDuToan = true;
                }
                else if (nsTypeIndex == selectedSDuToanIndex)
                {
                    ns.BHangChaDuToan = false;
                }
                else
                {
                    ns.BHangChaDuToan = null;
                }
            }
        }

        private void UpdateBHangChaDuToanDieuChinh(int selectedSDuToanIndex, int nsTypeIndex, BhDmMucLucNganSachModel ns)
        {
            if (selectedSDuToanIndex > -1)
            {
                if (nsTypeIndex > selectedSDuToanIndex)
                {
                    ns.BHangChaDuToanDieuChinh = true;
                }
                else if (nsTypeIndex == selectedSDuToanIndex)
                {
                    ns.BHangChaDuToanDieuChinh = false;
                }
                else
                {
                    ns.BHangChaDuToanDieuChinh = null;
                }
            }
        }

        private void UpdateBHangChaQuyetToan(int selectedSQTIndex, int nsTypeIndex, BhDmMucLucNganSachModel ns)
        {
            if (selectedSQTIndex > -1)
            {
                if (nsTypeIndex > selectedSQTIndex)
                {
                    ns.BHangChaQuyetToan = true;
                    ns.SMaCB = null;
                }
                else if (nsTypeIndex == selectedSQTIndex)
                {
                    ns.BHangChaQuyetToan = false;
                }
                else
                {
                    ns.BHangChaQuyetToan = null;
                    ns.SMaCB = null;
                }
            }
        }

        public bool ItemsViewFilter(object obj)
        {
            if (_isFirstLoad)
            {
                return true;
            }
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            result = FilterFunction(FilterModel, item);
            if (!result && item.BHangCha)
            {
                result = xnmConcatenation.StartsWith(item.XNM) || xnmConcatenation.Contains(";" + item.XNM);
            }
            return result;
        }

        private bool FilterFunction(BhDmMucLucNganSachModel filterModel, BhDmMucLucNganSachModel item)
        {
            var result = true;
            if (!string.IsNullOrEmpty(filterModel.SLNS))
            {
                FilterModel.SLNS = filterModel.SLNS.Trim();
                result = result && item.SLNS.ToLower().Contains(filterModel.SLNS.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.SL))
            {
                FilterModel.SL = filterModel.SL.Trim();
                result = result && item.SL.ToLower().Contains(filterModel.SL.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.SK))
            {
                FilterModel.SK = filterModel.SK.Trim();
                result = result && item.SK.ToLower().Contains(filterModel.SK.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.SM))
            {
                FilterModel.SM = filterModel.SM.Trim();
                result = result && item.SM.ToLower().Contains(filterModel.SM.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.STM))
            {
                FilterModel.STM = filterModel.STM.Trim();
                result = result && item.STM.ToLower().Contains(filterModel.STM.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.STTM))
            {
                FilterModel.STTM = filterModel.STTM.Trim();
                result = result && item.STTM.ToLower().Contains(filterModel.STTM.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.SNG))
            {
                FilterModel.SNG = filterModel.SNG.Trim();
                result = result && item.SNG.ToLower().Contains(filterModel.SNG.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.STNG))
            {        
                FilterModel.STNG = filterModel.STNG.Trim();
                result = result && item.STNG.ToLower().Contains(filterModel.STNG.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.STNG1))
            {
                FilterModel.STNG1 = filterModel.STNG1.Trim();
                result = result && item.STNG1.ToLower().Contains(filterModel.STNG1.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.STNG2))
            {
                FilterModel.STNG2 = filterModel.STNG2.Trim();
                result = result && item.STNG2.ToLower().Contains(filterModel.STNG2.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.STNG3))
            {
                FilterModel.STNG3 = filterModel.STNG3.Trim();
                result = result && item.STNG3.ToLower().Contains(filterModel.STNG3.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.SMoTa))
            {
                FilterModel.SMoTa = filterModel.SMoTa.Trim();
                result = result && item.SMoTa.ToLower().Contains(filterModel.SMoTa.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(filterModel.MlnsParentName))
            {
                FilterModel.MlnsParentName = filterModel.MlnsParentName.Trim();
                result = result && item.MlnsParentName != null && item.MlnsParentName.Contains(filterModel.MlnsParentName.Trim());
            }
            return result;
        }

        private void SelectAll(bool select, IEnumerable<BhDmMucLucNganSachModel> models)
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
                if (!CheckAllForDanhMuc)
                {
                    if (SelectedItem != null)
                    {
                        //Mapping MLNS - Che Do BHXH
                        if (IsMappingRegime)
                        {
                            SavedAction?.Invoke(Items.Where(i => i.IsSelected));
                        }
                        else
                        {
                            if (SelectedItem.XNM.Equals(BhDmMucLucNganSachModel.XNM) || !BhDmMucLucNganSachModel.XNM.StartsWith(SelectedItem.XNM))
                            {
                                MessageBoxHelper.Warning("MLNS không hợp lệ");
                                return;
                            }
                        }
                        DialogHost.CloseDialogCommand.Execute(SelectedItem, null);
                    }
                }
                else
                {
                    SavedAction?.Invoke(Items.Where(i => i.IsSelected));
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
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
                var property = ModelType.GetProperty(_currentCol);
                InitDialog(property);
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
            FilterModel = new BhDmMucLucNganSachModel();
            _currentCodeValDictionary = new Dictionary<string, int>();
            Items = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(Items.Where(t => !t.IsDeleted).OrderBy(s => s.XNM));
            foreach (var item in Items)
            {
                string typeMLNS = GetTypeOfMlns(item);
                item.IsModified = false;
                item.IsDeleted = false;
                item.PropertyChanged += Item_PropertyChanged;
                item.PropertyChanged += BhDmMucLucNganSachModel_PropertyChanged;
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
                SelectedItem = (BhDmMucLucNganSachModel)dgdData.Items[0];
                dgdData.CurrentCell = cell;
            }
        }

        private void OnViewDetail()
        {
            if (SelectedItem == null)
            {
                return;
            }
            GenericControlCustomDetailViewModel genericControlCustomDetailViewModel = new GenericControlCustomDetailViewModel(SelectedItem)
            {
                Title = Title,
                Description = SelectedItem?.DetailInfoModalTitle
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
            if (Items.Any(t => t.IsSelected))
            {
                MucLucNganSachBHXHViewModel mucLucNganSachViewModel = _serviceProvider.GetService(typeof(MucLucNganSachBHXHViewModel)) as MucLucNganSachBHXHViewModel;
                mucLucNganSachViewModel.InitBaseData();
                mucLucNganSachViewModel.Items = new ObservableCollection<BhDmMucLucNganSachModel>(Items.Where(t => !t.IsDeleted));
                mucLucNganSachViewModel._isFirstLoad = true;
                mucLucNganSachViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucNganSachViewModel.Items);
                mucLucNganSachViewModel._dataCollectionView.Filter = mucLucNganSachViewModel.ItemsViewFilter;
                mucLucNganSachViewModel._isFirstLoad = false;
                MucLucNganSachBHXHView = new MucLucNganSachBHXHView()
                {
                    DataContext = mucLucNganSachViewModel
                };
                mucLucNganSachViewModel.MucLucNganSachBHXHView = MucLucNganSachBHXHView;
                DialogClosingEventHandler closeEvent = (sender, eventArgs) =>
                {
                    if (eventArgs.Parameter != null)
                    {
                        BhDmMucLucNganSachModel BhDmMucLucNganSachModel = eventArgs.Parameter as BhDmMucLucNganSachModel;
                        foreach (var item in Items.Where(t => t.IsSelected))
                        {
                            item.MlnsParentName = BhDmMucLucNganSachModel.XNM;
                            item.IIDMLNSCha = BhDmMucLucNganSachModel.IIDMLNS;
                        }
                        BhDmMucLucNganSachModel.BHangCha = true;
                    }
                };
                var dialog = DialogHost.Show(MucLucNganSachBHXHView, "RootDialog", closeEvent);
                mucLucNganSachViewModel.IsDialog = true;
            }
        }

        private void OnSelectPhuCap()
        {
            if (SelectedItem != null)
            {
                PhuCapDialogViewModel.Model = SelectedItem;
                PhuCapDialogViewModel.Init();
                PhuCapDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                PhuCapDialogViewModel.ShowDialogHost();
            }
        }

        private void OnMapMLNS()
        {
            if (SelectedItem != null)
            {
                //MLNSDialogViewModel.Model = SelectedItem;
                //MLNSDialogViewModel.Init();
                //MLNSDialogViewModel.SavedAction = obj =>
                //{
                //    this.OnRefresh();
                //};
                //MLNSDialogViewModel.ShowDialogHost();
            }
        }

        private void OnMapPhuCap()
        {
            if (SelectedItem != null)
            {
                PhuCapDialogViewModel.Model = SelectedItem;
                PhuCapDialogViewModel.Init();
                PhuCapDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                PhuCapDialogViewModel.ShowDialogHost();
            }
        }

        private PopupBox FindPopupByName(string elementName)
        {
            PopupBox popupBox = FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
            if (IsDialog)
            {
                return FnCommonUtils.FindChild<PopupBox>(MucLucNganSachBHXHView, elementName);
            }
            else
                return FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
        }

        private DataGrid FindDatagrid()
        {
            if (IsDialog)
            {
                return FnCommonUtils.FindChild<DataGrid>(MucLucNganSachBHXHView, ModelName);
            }
            else
                return FnCommonUtils.FindChild<DataGrid>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), ModelName);
        }
        private void OnExportExcel()
        {
            MlnsBhxhExportViewModel mlnsExportViewModel = new MlnsBhxhExportViewModel(_service)
            {
                sessionService = _sessionService,
                Mapper = _mapper,
                ServiceProvider = _serviceProvider
            };
            mlnsExportViewModel.Init();
            MlnsBhxhExportView Dialog = new MlnsBhxhExportView()
            {
                DataContext = mlnsExportViewModel
            };
            var dialog = DialogHost.Show(Dialog, SystemConstants.ROOT_DIALOG, ClosingEventHandler);
        }

        private void OnImportExcel()
        {
            ImportExcelViewModel<BhDmMucLucNganSachModel, BhDmMucLucNganSach, BhDmMucLucNganSachService, MLNSBHXHImportModel> importExcelViewModel =
                new ImportExcelViewModel<BhDmMucLucNganSachModel, BhDmMucLucNganSach, BhDmMucLucNganSachService, MLNSBHXHImportModel>(_serviceProvider, _service, _authenticationInfo);
            importExcelViewModel.DataTemplateFileName = ExportFileName.RPT_DM_MLNS_BHXH_IMPORT;
            ImportExcelView importExcelView = new ImportExcelView()
            {
                DataContext = importExcelViewModel
            };
            importExcelViewModel.Init();
            importExcelView.Show();
        }
    }
}
