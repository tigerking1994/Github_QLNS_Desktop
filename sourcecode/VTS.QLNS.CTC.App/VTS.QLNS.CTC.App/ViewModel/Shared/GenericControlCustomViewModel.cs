using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.ConvertGenericData;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.GenericControlService;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.View.Shared.Import;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDanhMucNhaThau.ImportNhaThau;
using VTS.QLNS.CTC.App.ViewModel.ImportViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using Application = System.Windows.Application;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class GenericControlCustomViewModel<TModel, TEntity, TService> : GridViewModelBase<TModel>
        where TModel : ModelBase, new()
        where TEntity : EntityBase, new()
        where TService : IService<TEntity>
    {
        public ILog _logger;
        public TService _service;
        public IMapper _mapper;
        public IServiceProvider _serviceProvider;
        public ISessionService _sessionService;
        public ICollectionView _dataCollectionView;
        public IExportService _exportService;
        public ISysAuditLogService _auditLog;
        private MapperDataGenericControl _mapperDataGenericControl;
        public AuthenticationInfo _authenticationInfo;
        public Dictionary<string, bool> _checkboxDictionary;
        public Dictionary<string, string> _formatDictionary;
        public Dictionary<string, int> _currentCodeValDictionary;
        public string _currentCol;
        public bool _isFirstLoad;
        public bool _isFirstUpdateSelectedChucNang = true;
        private readonly SessionInfo _sessionInfo;

        public const string POPUP_SUFFIX = "popupbox";
        private const string TEXTBOX_SUFFIX = "txtbox";

        public override Type ContentType => typeof(GenericControlCustom);
        public Type ModelType => typeof(TModel);
        public bool IsEdit => SelectedItem != null;
        public List<string> NotIns { get; set; }
        public bool IsDialog { get; set; }
        public bool IsPopup { get; set; }
        public bool IsDanhMucChuky { get; set; }

        private bool _isProcessReport = false;
        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }
        //private bool _isShowTyLeTangLuong;
        //public bool IsShowTyLeTangLuong
        //{
        //    get => _isShowTyLeTangLuong;
        //    set
        //    {
        //        SetProperty(ref _isShowTyLeTangLuong, value);
        //        OnPropertyChanged(nameof(IsShowTyLeTangLuong));
        //    }
        //}
        private int _iTyLeTang;
        public int ITyLeTang
        {
            get => _iTyLeTang;
            set => SetProperty(ref _iTyLeTang, value);
        }
        public bool IsShowTyLeTangLuong { get; set; }
        public bool IsMultipleSelect { get; set; }
        public bool IsEnabledSaveBtn => SelectedItem != null;
        public Visibility VisibilityDialogBtn => IsDialog ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibilityFunctionBtn => IsDanhMucChuky ? Visibility.Collapsed : Visibility.Visible;
        public string Hint => IsDialog ? "Ấn phím space để chọn checkbox nếu có" : "";
        public string Total => " (Tổng số bản ghi: " + Items.Count() + ")";
        public string ModelName => typeof(TModel).Name;
        public string DataTemplateFileName { get; set; }
        public Visibility ExportBtnVisibility => string.IsNullOrEmpty(TemplateFileName) ? Visibility.Collapsed : Visibility.Visible;
        public bool IsReadOnlyGrid => typeof(TModel).Equals(typeof(DmChuKyModel));
        public bool IsShowFilter => typeof(TModel).Name.Contains("Nq104Model");
        public bool IsVisibleAddBtn => !typeof(TModel).Equals(typeof(CauHinhMLNSModel)) && !typeof(TModel).Equals(typeof(BhDmMucLucBHXHMapModel)) && !typeof(TModel).Equals(typeof(HTChucNangModel)) && !typeof(TModel).Equals(typeof(CauHinhUserMLNSModel));
        public bool IsVisibleDeleteBtn => !typeof(TModel).Equals(typeof(CauHinhMLNSModel)) && !typeof(TModel).Equals(typeof(BhDmMucLucBHXHMapModel)) && !typeof(TModel).Equals(typeof(HTChucNangModel)) && !typeof(TModel).Equals(typeof(CauHinhUserMLNSModel)) && !typeof(TModel).Equals(typeof(DmCapBacNhomChuKyModel));
        public bool IsVisibleSaveBtn => !typeof(TModel).Equals(typeof(CauHinhUserMLNSModel)) && !typeof(TModel).Equals(typeof(HTChucNangModel));
        public bool IsVisibleFormatBtn => !typeof(TModel).Equals(typeof(CauHinhUserMLNSModel)) && !typeof(TModel).Equals(typeof(HTChucNangModel)) && !typeof(TModel).Equals(typeof(BhDmMucLucBHXHMapModel)) && !typeof(TModel).Equals(typeof(BhDanhMucLoaiChiModel)) && !typeof(TModel).Equals(typeof(BhDmCoSoYTeModel)) && !typeof(TModel).Equals(typeof(BhDmThamDinhQuyetToanModel)) && !typeof(TModel).Equals(typeof(BhDmCauHinhThamSoModel)) && !typeof(TModel).Equals(typeof(DmCapBacNhomChuKyModel));
        public bool IsVisibleFilterBtn => !typeof(TModel).Equals(typeof(BhDmMucLucBHXHMapModel)) && !typeof(TModel).Equals(typeof(BhDanhMucLoaiChiModel)) && !typeof(TModel).Equals(typeof(BhDmCoSoYTeModel)) && !typeof(TModel).Equals(typeof(BhDmThamDinhQuyetToanModel)) && !typeof(TModel).Equals(typeof(BhDmCauHinhThamSoModel)) && !typeof(TModel).Equals(typeof(DmCapBacNhomChuKyModel));
        public bool IsVisibleRefreshBtn => !typeof(TModel).Equals(typeof(HTChucNangModel));
        public bool IsVisibleToggleBtn => !IsDialog;
        public bool IsVisibleViewDetailBtn => !IsDialog;

        public bool IsMenuBar => !IsPopup;
        public bool IsConfigMLNS { get; set; }
        public Visibility IsVisibleEditBtn => typeof(TModel).Equals(typeof(DmChuKyModel)) ? Visibility.Visible : Visibility.Collapsed;
        public bool IsVisibleUpdateCadres => typeof(TModel).Equals(typeof(TlDanhMucChucVuNq104Model)) || typeof(TModel).Equals(typeof(TlDanhMucChucDanhNq104Model));
        public bool IsEnableAddChild =>
            typeof(TModel).Equals(typeof(RevenueExpenditureCategoryModel)) ||
            typeof(TModel).Equals(typeof(NsMuclucNgansachModel)) ||
            typeof(TModel).Equals(typeof(VdtDmLoaiCongTrinhModel)) ||
            typeof(TModel).Equals(typeof(SktMucLucModel)) ||
            typeof(TModel).Equals(typeof(VdtDmDonViThucHienDuAnModel)) ||
            typeof(TModel).Equals(typeof(BhDmCheDoBhxhModel)) ||
            typeof(TModel).Equals(typeof(VdtDmDuToanChiModel)) ||
            typeof(TModel).Equals(typeof(QsMucLucModel)) ||
            typeof(TModel).Equals(typeof(DanhMucNguonNganSachModel)) ||
            //typeof(TModel).Equals(typeof(TlDmDonViModel)) ||
            typeof(TModel).Equals(typeof(TLDmKinhPhiModel)) ||
            typeof(TModel).Equals(typeof(CauHinhMLNSChiTieuLuongModel)) ||
            typeof(TModel).Equals(typeof(DmMucLucQuyetToanModel)) ||
            typeof(TModel).Equals(typeof(NhDmLoaiCongTrinhModel)) ||
            typeof(TModel).Equals(typeof(DmCongKhaiTaiChinhModel));
        public bool IsEnableExportExcel
            => ListEnableImportExport.Contains(typeof(TModel));
        public bool IsEnableImportExcel
            => ListEnableImportExport.Contains(typeof(TModel));
        public bool IsVisibleYearType => typeof(TModel).Equals(typeof(DanhMucCauHinhHeThongModel));
        public bool IsVisibleFilterByMlnsMappingType { get; set; }
        public ObservableCollection<ComboboxItem> Years { get; set; }

        private int? _yearSelected;
        public int? YearSelected
        {
            get => _yearSelected;
            set
            {
                SetProperty(ref _yearSelected, value);
                if (!IsDialog) LoadData();
            }
        }

        private string _mlnsMapping;
        public string MlnsMapping
        {
            get => _mlnsMapping;
            set
            {
                SetProperty(ref _mlnsMapping, value);
                if (_dataCollectionView != null && IsVisibleFilterByMlnsMappingType)
                {
                    genericControlBaseService.BeForeRefresh();
                    _dataCollectionView.Refresh();
                }
            }
        }

        private string _iIdMaDanhMucExisted;
        public string IIdMaDanhMucExisted
        {
            get => _iIdMaDanhMucExisted;
            set => SetProperty(ref _iIdMaDanhMucExisted, value);
        }

        public Type ImportModelType { get; set; }
        public GenericControlCustom GenericControlCustomDialog { get; set; }
        public GenericControlCustomWindow GenericControlCustomWindow { get; set; }
        public bool AfterSelectAll { get; set; }

        private string _filtertext;
        public string FilterText
        {
            get => _filtertext;
            set => SetProperty(ref _filtertext, value);
        }

        private TModel _filterModel;
        public TModel FilterModel
        {
            get => _filterModel;
            set => _filterModel = value;
        }

        public string _templateFileName;
        public string TemplateFileName
        {
            get => _templateFileName;
            set
            {
                SetProperty(ref _templateFileName, value);
                OnPropertyChanged("ExportBtnVisibility");
            }
        }

        public string _jsonFileName;
        public string ExportJsonFileName
        {
            get => _jsonFileName;
            set
            {
                SetProperty(ref _jsonFileName, value);
                OnPropertyChanged("ExportJsonBtnVisibility");
            }
        }

        public Visibility ExportJsonBtnVisibility
        {
            get => string.IsNullOrEmpty(ExportJsonFileName) ? Visibility.Collapsed : Visibility.Visible;
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    List<TModel> unSelected = Items.Where(item => item.IsFilter && !item.IsSelected).ToList();
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

        private string _yearType;
        public string YearType
        {
            get => _yearType;
            set
            {
                SetProperty(ref _yearType, value);
                if (_dataCollectionView != null && IsVisibleYearType)
                {
                    _dataCollectionView.Refresh();
                }
            }
        }

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        private List<Type> ListEnableImportExport
        {
            get
            {
                return new List<Type>()
                {
                    typeof(DonViModel),
                    typeof(NSPhongBanModel),
                    typeof(DanhMucCauHinhHeThongModel),
                    typeof(DmChuKyModel),
                    typeof(DmCapBacModel),
                    typeof(CauHinhCanCuModel),
                    typeof(DmDeTaiModel),
                    typeof(NhDmNhaThauModel),
                    typeof(TlDmCheDoBHXHModel),
                    typeof(BhDmCauHinhThamSoModel),
                    //typeof(NhDaHopDongModel)
                    typeof(DmCapBacNhomChuKyModel),
                };
            }
        }

        public MucLucNganSachBHXHView MucLucNganSachBHXHView { get; set; }
        public MucLucDanhMucLoaiChiView MucLucDanhMucLoaiChiView { get; set; }
        public MucLucPopupBoxChucVuLuongMoiView MucLucPopupBoxChucVuLuongMoiView { get; set; }
        public virtual Guid? iID_DonViID { get; set; }
        public virtual bool? IsLoaiChucVu { get; set; }
        public virtual Guid? iIdKhttNhiemVuChiId { get; set; }
        public ObservableCollection<ComboboxItem> FilterByYear { get; set; }
        public ObservableCollection<ComboboxItem> FilterByMappingMlnsStatus { get; set; }
        public GenericControlBaseService<TModel, TEntity, TService> genericControlBaseService { get; set; }
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
        public RelayCommand OpenPopupExportCommand { get; }
        public RelayCommand DeleteReferCommand { get; }
        public RelayCommand UpdateCadresCommand { get; }

        public GenericControlCustomViewModel(TService service, IMapper mapper, ISessionService sessionService, IServiceProvider provider)
        {
            _serviceProvider = provider;
            _auditLog = (ISysAuditLogService)_serviceProvider.GetService(typeof(ISysAuditLogService));
            _logger = _serviceProvider.GetRequiredService<ILog>();
            _exportService = (IExportService)_serviceProvider.GetService(typeof(IExportService));
            _service = service;
            _mapper = mapper;
            _sessionService = sessionService;
            _sessionInfo = _sessionService.Current;

            AutoGeneratingColumnsCommand = new RelayCommand(obj => OnAutoGeneratingColumnsCommand(obj));
            SaveReferenceCommand = new RelayCommand(obj => OnSaveReference());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            SelectedCellChangeCommand = new RelayCommand(obj => _currentCol = obj.ToString());
            OpenFilterPopupCommand = new RelayCommand(obj => ToggleFilterPopup(true));
            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
            OnLoadedFormatPopupCommand = new RelayCommand(obj => OnLoadedFormatPopup(obj));
            ExportCommand = new RelayCommand(obj => OnExportExcel());
            ExportJsonCommand = new RelayCommand(obj => OnExportJsonFile());
            ImportExcelCommand = new RelayCommand(obj => OnImportExcel());
            UpdateDmChuKyCommand = new RelayCommand(obj => OnUpdateDanhMucChuKy());
            AddChildCommand = new RelayCommand(obj => OnAddChild(obj));
            ViewDetailCommand = new RelayCommand(obj => OnViewDetail(), obj => !IsDialog);
            OpenPopupExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            DeleteReferCommand = new RelayCommand(obj => OnDeleteRefer(obj));
            UpdateCadresCommand = new RelayCommand(obj => OnUpdateCadres());

            _authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);

            genericControlBaseService = GenericControlBaseService<TModel, TEntity, TService>.BuildControlService(typeof(TModel));
            if (genericControlBaseService != null)
                genericControlBaseService.sourceVM = this;

            FilterByYear = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Không có năm làm việc", ValueItem = CoNamLamViec.HAS_NO_YEAR.ToString()},
                new ComboboxItem { DisplayItem = "Có năm làm việc", ValueItem = CoNamLamViec.HAS_YEAR.ToString()}
            };
            FilterByMappingMlnsStatus = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Tất cả", ValueItem = ""},
                new ComboboxItem { DisplayItem = "Đã mapping", ValueItem = "true"},
                new ComboboxItem { DisplayItem = "Chưa mapping", ValueItem = "false"}
            };
        }

        public override void Init()
        {
            try
            {
                base.Init();

                _checkboxDictionary = new Dictionary<string, bool>();
                _formatDictionary = new Dictionary<string, string>();
                _currentCodeValDictionary = new Dictionary<string, int>();
                _mapperDataGenericControl = (MapperDataGenericControl)_serviceProvider.GetService(typeof(MapperDataGenericControl));
                _filterModel = new TModel();
                if (_sessionService.Current.AutoGenerateDataSetting.ContainsKey(typeof(TModel).Name))
                {
                    _formatDictionary = _sessionService.Current.AutoGenerateDataSetting[typeof(TModel).Name];
                }
                else
                {
                    _sessionService.Current.AutoGenerateDataSetting[typeof(TModel).Name] = _formatDictionary;
                }

                Years = new ObservableCollection<ComboboxItem>();
                IDanhMucService dmService = (IDanhMucService)_serviceProvider.GetService(typeof(IDanhMucService));
                IEnumerable<DanhMuc> danhMucNamLV = dmService.FindByType("NS_NamLamViec").OrderBy(c => c.INamLamViec).ToList();
                foreach (DanhMuc namLV in danhMucNamLV)
                {
                    Years.Add(new ComboboxItem { DisplayItem = namLV.SGiaTri, ValueItem = namLV.SGiaTri });
                }

                YearSelected = int.Parse(Years.FirstOrDefault(x => int.Parse(x.ValueItem) == _sessionService.Current.YearOfWork)?.ValueItem
                    ?? Years.FirstOrDefault().ValueItem);

                LoadData();
                YearType = CoNamLamViec.HAS_YEAR.ToString();
                MlnsMapping = "";



            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            if (iIdKhttNhiemVuChiId.HasValue && iIdKhttNhiemVuChiId != Guid.Empty)
            {
                genericControlBaseService.LoadData(args, iID_DonViID, iIdKhttNhiemVuChiId);
            }
            else if (iID_DonViID.HasValue && iID_DonViID != Guid.Empty)
            {
                genericControlBaseService.LoadData(args, iID_DonViID);
            }
            else if (IsLoaiChucVu.HasValue)
            {
                genericControlBaseService.LoadData(args, Guid.Empty, Guid.Empty, IsLoaiChucVu.Value, YearSelected);
            }
            else if (YearSelected.HasValue)
            {
                genericControlBaseService.LoadData(args, Guid.Empty, Guid.Empty, false, YearSelected);
            }
            else
            {
                genericControlBaseService.LoadData(args);
            }
        }

        protected override void OnItemsChanged()
        {
            OnPropertyChanged("Total");
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged("IsEdit");
            OnPropertyChanged("SelectedItem");
            OnPropertyChanged("IsEnabledSaveBtn");
        }

        private void OnAutoGeneratingColumnsCommand(object obj)
        {
            if (obj is DataGridAutoGeneratingColumnEventArgs e)
            {
                if (e.PropertyDescriptor is PropertyDescriptor descriptor)
                {
                    PropertyInfo property = ModelType.GetProperty(e.PropertyName);
                    if (property != null && Attribute.IsDefined(property, typeof(DisplayNameAttribute)))
                    {
                        // e.Column.IsReadOnly = IsDialog;
                        if (e.PropertyName.Equals("IsSelected") && !IsMultipleSelect)
                        {
                            e.Cancel = true;
                        }
                        else if (e.PropertyName.Equals("IsSelected") && IsMultipleSelect)
                        {
                            GenerateCheckboxColumn(property, e);
                            e.Column.IsReadOnly = false;
                            e.Column.DisplayIndex = 0;
                        }
                        else if (Attribute.IsDefined(property, typeof(ColumnTypeAttribute)))
                        {
                            ColumnTypeAttribute attribute = (ColumnTypeAttribute)Attribute.GetCustomAttribute(property, typeof(ColumnTypeAttribute));
                            switch (attribute.ColumnType)
                            {
                                case ColumnType.ReferencePopup:
                                    GenerateReferenceColumn(property, e);
                                    break;
                                case ColumnType.DateTime:
                                    GenerateDateTimeColumn(property, e);
                                    break;
                                case ColumnType.Checkbox:
                                    GenerateCheckboxColumn(property, e);
                                    break;
                                case ColumnType.Combobox:
                                    GenerateComboboxColumn(property, e);
                                    break;
                                default:
                                    break;
                            }
                            e.Column.IsReadOnly = IsReadOnlyGrid;
                        }
                        else
                        {
                            if (typeof(TModel).Equals(typeof(NsMuclucNgansachModel)))
                            {
                                List<string> mlnsType = new List<string>
                                {
                                    "LNS", "L", "K", "M", "TM", "TTM", "NG", "TNG", "TNG1", "TNG2", "TNG3"
                                };
                                IMucLucNganSachService mucLucNganSachService = _service as IMucLucNganSachService;
                                DanhMuc mlnsChiTietToi = mucLucNganSachService.FindMLNSChiTietToi(_sessionService.Current.YearOfWork);
                                if (mlnsChiTietToi != null && mlnsType.Contains(e.PropertyName))
                                {
                                    int indexMLnsChiTietToi = mlnsType.IndexOf(mlnsChiTietToi.SGiaTri);
                                    int indexOfMlnsColumn = mlnsType.IndexOf(e.PropertyName);
                                    if (indexOfMlnsColumn > indexMLnsChiTietToi)
                                    {
                                        e.Cancel = true;
                                    }
                                }
                            }
                            GenerateDatagridTextColumn(property, e);
                            if (typeof(TModel).Equals(typeof(BhDmMucLucBHXHMapModel)))
                            {
                                List<string> dmType = new List<string>
                                {
                                    "FTyLeBHXHNSD","FTyLeBHXHNLD","FTyLeBHYTNSD","FTyLeBHYTNLD","FTyLeBHTNNSD","FTyLeBHTNNLD","FHeSoLayQuyLuong"
                                };
                                if (!dmType.Contains(e.PropertyName))
                                {
                                    e.Column.IsReadOnly = true;
                                }
                            }
                            else
                            {
                                e.Column.IsReadOnly = IsReadOnlyGrid || genericControlBaseService.IsDisableColumn(property);
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void ClickFilter(object sender, RoutedEventArgs e)
        {
            _dataCollectionView.Refresh();
            _dataCollectionView.MoveCurrentToFirst();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.ToString());
        }

        protected override void OnAdd(object obj)
        {
            try
            {
                genericControlBaseService.OnAdd(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAddChild(object obj)
        {
            if (!IsEnableAddChild || SelectedItem == null)
            {
                return;
            }
            genericControlBaseService.OnAddChild(obj);
        }

        protected override void OnDelete(object obj)
        {
            if (!IsVisibleDeleteBtn)
            {
                return;
            }
            try
            {
                genericControlBaseService.OnDelete(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave(object obj)
        {
            if (!IsVisibleSaveBtn)
            {
                return;
            }
            try
            {
                List<string> modelNameList = new List<string>()
                {
                    "DmLoaiTienTeModel",
                    "NhDmPhanCapPheDuyetModel",
                    "NhDmLoaiHopDongModel",
                    "NhDmLoaiCongTrinhModel",
                    "NhDmNoiDungChiModel",
                    "NhDmLoaiTaiSanModel",
                    "NhDmChiPhiModel",
                    "NhDmNhaThauModel",
                    "NhDmNhiemVuChiModel",
                    "NhDmHinhThucChonNhaThauModel",
                    "NhDmPhuongThucChonNhaThauModel",
                    "NhDmTaiKhoanModel",
                    "NhDmButToanInputModel",
                    "NhDmCongThucOutputModel",
                    "BhDmCauHinhThamSoModel",
                    "BhDmCheDoBhxhModel",
                    "BhDmCoSoYTeModel",
                    "BhDanhMucLoaiChiModel",
                    "BhDmKinhPhiModel",
                    "BhDmMucDongBHXHModel",
                    "TlDmCheDoBHXHModel",
                    "BhDmThamDinhQuyetToanModel"
                };
                if (modelNameList.Contains(ModelName.ToString()))
                {
                    if (!genericControlBaseService.ValidateField(Items.Where(i => i.IsModified && !i.IsDeleted)))
                    {
                        return;
                    }
                }
                if (!genericControlBaseService.validate())
                {
                    return;
                }

                if (!ValidateCheckNotChoseParent())
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgWarningNotChoseDonViCha));
                    return;
                }

                if (MaDonViOver50())
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgErrorOver50));
                    return;
                }

                if (DanhMucChuKyHasDuplicate())
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgWarningChuKyDuplicate));
                    return;
                }

                if (!ValidateCheckNotDuplicate())
                {
                    if (Items is ObservableCollection<BhDmCheDoBhxhModel> || Items is ObservableCollection<BhDmThamDinhQuyetToanModel>)
                    {
                        MessageBoxHelper.Info("Bản ghi với mã " + IIdMaDanhMucExisted + " đã tồn tại");
                    }
                    else if (Items is ObservableCollection<TlDmCheDoBHXHModel>)
                    {
                        MessageBoxHelper.Info(string.Format(Resources.MsgWarningLoaiTruyLinhDuplicate, IIdMaDanhMucExisted));
                    }
                    else
                    {
                        MessageBoxHelper.Warning(string.Format("Mã cấu hình đã tồn tại!"));
                    }
                    return;
                }

                if (ModelName.ToString() == "DanhMucNguonNganSachModel" && !ValidateCheckNotEdit())
                {
                    MessageBoxHelper.Warning(string.Format("Mã nguồn ngân sách 1,2,4,8,9 chỉ được cấu hình lại thứ tự sắp xếp!"));
                    return;
                }

                DateTime time = DateTime.Now;
                string msgConfirm = "Bạn chắc chắn muốn lưu thay đổi ?";
                MessageBoxResult dialogResult = MessageBox.Show(msgConfirm, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        IEnumerable<TEntity> dataToSave = _mapper.Map<IEnumerable<TEntity>>(Items.Where(i => i.IsModified || i.IsDeleted).Select(x =>
                        {
                            if (x is TlDmPhuCapHeThongModel)
                            {
                                TlDmPhuCapHeThongModel phucapHeThong = x as TlDmPhuCapHeThongModel;
                                if (!phucapHeThong.MaPhuCap.Equals(PhuCap.TENNGANHANG))
                                {
                                    phucapHeThong.GiaTri = decimal.Parse(phucapHeThong.GiaTriMoi);
                                }
                                else
                                {
                                    phucapHeThong.TenNganHang = phucapHeThong.GiaTriMoi;
                                }
                            }
                            else if (x is TlDmPhuCapHeThongNq104Model)
                            {
                                TlDmPhuCapHeThongNq104Model phucapHeThong = x as TlDmPhuCapHeThongNq104Model;
                                if (!phucapHeThong.MaPhuCap.Equals(PhuCap.TENNGANHANG))
                                {
                                    phucapHeThong.GiaTri = decimal.Parse(phucapHeThong.GiaTriMoi);
                                }
                                else
                                {
                                    phucapHeThong.TenNganHang = phucapHeThong.GiaTriMoi;
                                }
                            }
                            else if (x is TlDanhMucChucVuNq104Model tlDanhMucChucVuNq104Model)
                            {
                                tlDanhMucChucVuNq104Model.Loai = IsLoaiChucVu;
                                tlDanhMucChucVuNq104Model.Nam = YearSelected;
                                if (IsShowTyLeTangLuong)
                                {
                                    tlDanhMucChucVuNq104Model.TienLuong = tlDanhMucChucVuNq104Model.TienLuong;
                                    tlDanhMucChucVuNq104Model.TienNangLuong = tlDanhMucChucVuNq104Model.TienNangLuong;
                                }

                            }
                            else if (x is TlDanhMucChucDanhNq104Model tlDanhMucChucDanhNq104Model)
                            {
                                tlDanhMucChucDanhNq104Model.Loai = IsLoaiChucVu;
                                tlDanhMucChucDanhNq104Model.Nam = YearSelected;
                                if (IsShowTyLeTangLuong)
                                {
                                    tlDanhMucChucDanhNq104Model.TienLuong = tlDanhMucChucDanhNq104Model.TienLuong;
                                    tlDanhMucChucDanhNq104Model.TienNangLuong = tlDanhMucChucDanhNq104Model.TienNangLuong;
                                }
                            }

                            OnTrimProperty(x);
                            return x;
                        }));
                        genericControlBaseService.BeforeSave();
                        if (ModelName.ToString().Equals("NhDaDuAnModel") || ModelName.ToString().Equals("NhDaHopDongModel"))
                        {
                            dataToSave = _mapper.Map<IEnumerable<TEntity>>(Items);
                        }

                        List<string> messages = new List<string>();

                        if (dataToSave is IEnumerable<NsMucLucNganSach>)
                        {
                            IEnumerable<NsMucLucNganSach> mlnsList = ((IEnumerable<NsMucLucNganSach>)dataToSave);
                            List<string> mlns = mlnsList.Select(n => n.Lns).ToList();

                            List<TEntity> listChildMlns = _service.FindAllMlnsByLnsAndNamLmaViec(mlns, _authenticationInfo).ToList();
                            mlnsList.ToList().ForEach(mlns =>
                            {
                                if (mlns.Id.IsNullOrEmpty())
                                    mlns.MlnsId = Guid.NewGuid();
                            });

                            foreach (TEntity item in listChildMlns)
                            {
                                NsMucLucNganSach mlnsChild = item as NsMucLucNganSach;
                                NsMucLucNganSach iLoaiMLNS = mlnsList.FirstOrDefault(l => l.Lns == mlnsChild.Lns);
                                mlnsChild.ILoaiNganSach = iLoaiMLNS.ILoaiNganSach;
                                mlnsChild.IsModified = true;
                            }

                            foreach (NsMucLucNganSach item in mlnsList)
                            {
                                UpdateXauNoiMa(item);
                            }

                            _service.AddOrUpdateRange(listChildMlns, _authenticationInfo);
                        }
                        else
                        {
                            foreach (TEntity i in dataToSave)
                            {
                                if (i.IsDeleted)
                                    i.IsModified = false;
                                if (i is NhDmPhanCapPheDuyet)
                                {
                                    NhDmPhanCapPheDuyet dmPhanCap = i as NhDmPhanCapPheDuyet;
                                    dmPhanCap.BActive = true;
                                    dmPhanCap.IThuTu = 1;
                                }
                                if (i is NhDmNhaThau)
                                {
                                    NhDmNhaThau dmNhaThau = i as NhDmNhaThau;
                                    dmNhaThau.DNgayTao = DateTime.Now;
                                    dmNhaThau.SNguoiTao = _sessionInfo.Principal;
                                }

                                if (i is BhDmCauHinhThamSo)
                                {
                                    BhDmCauHinhThamSo dmCauHinhThamSo = i as BhDmCauHinhThamSo;
                                    dmCauHinhThamSo.INamLamViec = _sessionInfo.YearOfWork;
                                }
                                //if (i is NsMucLucNganSach)
                                //{
                                //    var mlns = i as NsMucLucNganSach;
                                //    var listChildMlns = _service.FindAllMlnsByLnsAndNamLmaViec(mlns.Lns, _authenticationInfo).ToList();
                                //    if (mlns.Id.IsNullOrEmpty()) mlns.MlnsId = Guid.NewGuid();
                                //    foreach (var item in listChildMlns)
                                //    {
                                //        var mlnsChild = item as NsMucLucNganSach;
                                //        mlnsChild.ILoaiNganSach = mlns.ILoaiNganSach;
                                //        mlnsChild.IsModified = true;
                                //    }
                                //    UpdateXauNoiMa(mlns);
                                //    _service.AddOrUpdateRange(listChildMlns, _authenticationInfo);
                                //}
                            }
                        }

                        if (dataToSave is IEnumerable<TlDmPhuCapNq104>)
                        {
                            BackgroundWorkerHelper.Run((s, e) =>
                            {
                                IsProcessReport = true;
                                _service.AddOrUpdateRange(dataToSave, _authenticationInfo, value =>
                                {
                                    ProgressValue = value;
                                });

                            }, (s, e) =>
                            {
                                if (e.Error is null)
                                {
                                    DataGrid dataGrid = obj as DataGrid;
                                    genericControlBaseService.OnRefreshWithOutReload(dataGrid);
                                    MessageBoxHelper.Info("Lưu dữ liệu thành công");
                                }
                                IsProcessReport = false;
                            });
                        }
                        else
                        {
                            if (ModelName.ToString().Equals("DmCapBacNhomChuKyModel"))
                                _service.AddOrUpdateRangeSignatureGroup(dataToSave, _authenticationInfo);
                            else
                                _service.AddOrUpdateRange(dataToSave, _authenticationInfo);
                            DataGrid dataGrid = obj as DataGrid;
                            if (ModelName.ToString().Equals("NhDaDuAnModel")
                                || ModelName.ToString().Equals("NhDaHopDongModel")
                                || ModelName.ToString().Equals("TlDanhMucChucVuNq104Model")
                                || ModelName.ToString().Equals("TlDanhMucChucDanhNq104Model"))
                            {
                                LoadData();
                            }
                            else
                            {
                                genericControlBaseService.OnRefreshWithOutReload(dataGrid);
                            }
                            MessageBoxHelper.Info("Lưu dữ liệu thành công");
                        }
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

        private bool MaDonViOver50()
        {
            if (Items is ObservableCollection<DonViModel> items)
            {
                if (items.Any(x => !string.IsNullOrEmpty(x.MaSoKBNN) && x.MaSoKBNN.Length > 50))
                {
                    return true;
                }
                if (items.Any(x => !string.IsNullOrEmpty(x.MaSoDVQHNS) && x.MaSoDVQHNS.Length > 50))
                {
                    return true;
                }
            }
            return false;
        }

        private bool DanhMucChuKyHasDuplicate()
        {
            if (Items is ObservableCollection<DmCapBacModel> items)
            {
                if (items.Any(x => string.IsNullOrEmpty(x.IIDMaDanhMuc))) { return true; }
                if (items.GroupBy(x => x.SType).Any(y => y.ToList().GroupBy(z => z.IIDMaDanhMuc).Any(t => t.ToList().Count() > 1))) { return true; };
            }
            return false;
        }

        private bool ValidateCheckNotChoseParent()
        {
            if (Items is ObservableCollection<DmChuDauTuModel> items)
            {
                if (items.Any(x => x.Id.Equals(x.IIDDonViCha)))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateCheckNotDuplicate()
        {
            ObservableCollection<TEntity> itemsModified = _mapper.Map<ObservableCollection<TEntity>>(Items.Where(i => i.IsModified && !i.IsDeleted));
            ObservableCollection<TEntity> itemsContain = _mapper.Map<ObservableCollection<TEntity>>(Items.Where(i => !i.IsModified && !i.IsDeleted));
            ObservableCollection<TEntity> itemsEditDanhMucChucVuNq104 = _mapper.Map<ObservableCollection<TEntity>>(Items.Where(i => i.IsModified && !i.IsDeleted && !i.IsAdded));
            ObservableCollection<TEntity> itemsEditDanhMucChucDanhNq104Nq104 = _mapper.Map<ObservableCollection<TEntity>>(Items.Where(i => i.IsModified && !i.IsDeleted && !i.IsAdded));

            if (Items is ObservableCollection<BhDmCauHinhThamSoModel>)
            {
                foreach (BhDmCauHinhThamSo item in itemsModified as ObservableCollection<BhDmCauHinhThamSo>)
                {
                    for (int i = 0; i < itemsContain.Count(); i++)
                    {
                        if (item.SMa.ToLower().Trim().Equals((itemsContain as ObservableCollection<BhDmCauHinhThamSo>)[i].SMa.ToLower()))
                        {
                            return false;
                        }
                    }
                }

                IGrouping<string, BhDmCauHinhThamSo> lstItemMDFGroupBy = (itemsModified as ObservableCollection<BhDmCauHinhThamSo>).GroupBy(x => x.SMa.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                    return false;

            }
            else if (Items is ObservableCollection<BhDmCheDoBhxhModel>)
            {
                List<string> lstItemsContain = (itemsContain as ObservableCollection<BhDmCheDoBhxh>).Select(x => x.IIdMaCheDo.ToLower()).ToList();
                foreach (BhDmCheDoBhxh item in itemsModified as ObservableCollection<BhDmCheDoBhxh>)
                {
                    if (lstItemsContain.Contains(item.IIdMaCheDo.ToLower().Trim()))
                    {
                        IIdMaDanhMucExisted = item.IIdMaCheDo.Trim();
                        return false;
                    }
                }

                IGrouping<string, BhDmCheDoBhxh> lstItemMDFGroupBy = (itemsModified as ObservableCollection<BhDmCheDoBhxh>).GroupBy(x => x.IIdMaCheDo.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.Trim();
                    return false;
                }
            }
            else if (Items is ObservableCollection<BhDmKinhPhiModel>)
            {
                List<string> lstItemsContain = (itemsContain as ObservableCollection<BhDmKinhPhi>).Select(x => x.MaKinhPhi.ToLower()).ToList();
                foreach (BhDmKinhPhi item in itemsModified as ObservableCollection<BhDmKinhPhi>)
                {
                    if (lstItemsContain.Contains(item.MaKinhPhi.ToLower().Trim()))
                    {
                        return false;
                    }
                }

                IGrouping<string, BhDmKinhPhi> lstItemMDFGroupBy = (itemsModified as ObservableCollection<BhDmKinhPhi>).GroupBy(x => x.MaKinhPhi.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.Trim();
                    return false;
                }
            }
            else if (Items is ObservableCollection<BhDanhMucLoaiChiModel>)
            {
                List<string> lstItemsContain = (itemsContain as ObservableCollection<BhDanhMucLoaiChi>).Select(x => x.SMaLoaiChi.ToLower()).ToList();
                foreach (BhDanhMucLoaiChi item in itemsModified as ObservableCollection<BhDanhMucLoaiChi>)
                {
                    if (lstItemsContain.Contains(item.SMaLoaiChi.ToLower().Trim()))
                    {
                        return false;
                    }
                }

                IGrouping<string, BhDanhMucLoaiChi> lstItemMDFGroupBy = (itemsModified as ObservableCollection<BhDanhMucLoaiChi>).GroupBy(x => x.SMaLoaiChi.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.Trim();
                    return false;
                }
            }
            else if (Items is ObservableCollection<TlDanhMucChucVuNq104Model>)
            {
                List<string> lstItemsContain = (itemsContain as ObservableCollection<TlDmChucVuNq104>).Select(x => x.Ma.ToLower()).ToList();

                foreach (TlDmChucVuNq104 item in itemsEditDanhMucChucVuNq104 as ObservableCollection<TlDmChucVuNq104>)
                {
                    if (lstItemsContain.Contains(item.Ma.ToLower().Trim()))
                    {
                        return false;
                    }
                }

                IGrouping<string, TlDmChucVuNq104> lstItemMDFGroupBy = (itemsEditDanhMucChucVuNq104 as ObservableCollection<TlDmChucVuNq104>).GroupBy(x => x.Ma.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.Trim();
                    return false;
                }
            }
            else if (Items is ObservableCollection<TlDanhMucChucDanhNq104Model>)
            {
                List<string> lstItemsContain = (itemsContain as ObservableCollection<TlDmChucVuNq104>).Select(x => x.Ma.ToLower()).ToList();

                foreach (TlDmChucVuNq104 item in itemsEditDanhMucChucDanhNq104Nq104 as ObservableCollection<TlDmChucVuNq104>)
                {
                    if (lstItemsContain.Contains(item.Ma.ToLower().Trim()))
                    {
                        return false;
                    }
                }

                IGrouping<string, TlDmChucVuNq104> lstItemMDFGroupBy = (itemsEditDanhMucChucDanhNq104Nq104 as ObservableCollection<TlDmChucVuNq104>).GroupBy(x => x.Ma.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.Trim();
                    return false;
                }
            }
            else if (Items is ObservableCollection<TlDmPhuCapHeThongNq104Model>)
            {
                List<string> lstItemsContain = (itemsContain as ObservableCollection<TlDmPhuCapNq104>).Select(x => x.MaPhuCap.ToLower()).ToList();
                foreach (TlDmPhuCapNq104 item in itemsModified as ObservableCollection<TlDmPhuCapNq104>)
                {
                    if (lstItemsContain.Contains(item.MaPhuCap.ToLower().Trim()))
                    {
                        IIdMaDanhMucExisted = item.MaPhuCap.Trim();
                        return false;
                    }
                }

                IGrouping<string, TlDmPhuCapNq104> lstItemMDFGroupBy = (itemsModified as ObservableCollection<TlDmPhuCapNq104>).GroupBy(x => x.MaPhuCap.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.Trim();
                    return false;
                }
            }
            else if (Items is ObservableCollection<BhDmMucDongBHXHModel>)
            {
                List<string> lstItemsContain = (itemsContain as ObservableCollection<BhDmMucDongBHXH>).Select(x => x.SMaMucDong.ToLower()).ToList();
                foreach (BhDmMucDongBHXH item in itemsModified as ObservableCollection<BhDmMucDongBHXH>)
                {
                    if (lstItemsContain.Contains(item.SMaMucDong.ToLower().Trim()))
                    {
                        return false;
                    }
                }

                IGrouping<string, BhDmMucDongBHXH> lstItemMDFGroupBy = (itemsModified as ObservableCollection<BhDmMucDongBHXH>).GroupBy(x => x.SMaMucDong.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.Trim();
                    return false;
                }
            }
            else if (Items is ObservableCollection<BhDmCoSoYTeModel>)
            {
                List<string> lstItemsContain = (itemsContain as ObservableCollection<BhDmCoSoYTe>).Select(x => x.IIDMaCoSoYTe.ToLower()).ToList();
                foreach (BhDmCoSoYTe item in itemsModified as ObservableCollection<BhDmCoSoYTe>)
                {
                    if (lstItemsContain.Contains(item.IIDMaCoSoYTe.ToLower().Trim()))
                    {
                        return false;
                    }
                }

                IGrouping<string, BhDmCoSoYTe> lstItemMDFGroupBy = (itemsModified as ObservableCollection<BhDmCoSoYTe>).GroupBy(x => x.IIDMaCoSoYTe.ToLower()).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.Trim();
                    return false;
                }
            }
            else if (Items is ObservableCollection<BhDmThamDinhQuyetToanModel>)
            {
                List<int> lstItemsContain = (itemsContain as ObservableCollection<BhDmThamDinhQuyetToan>).Select(x => x.IMa).ToList();
                foreach (BhDmThamDinhQuyetToan item in itemsModified as ObservableCollection<BhDmThamDinhQuyetToan>)
                {
                    if (lstItemsContain.Contains(item.IMa))
                    {
                        IIdMaDanhMucExisted = item.IMa.ToString();
                        return false;
                    }
                }

                IGrouping<int, BhDmThamDinhQuyetToan> lstItemMDFGroupBy = (itemsModified as ObservableCollection<BhDmThamDinhQuyetToan>).GroupBy(x => x.IMa).FirstOrDefault(i => i.Count() > 1);
                if (lstItemMDFGroupBy != null)
                {
                    IIdMaDanhMucExisted = lstItemMDFGroupBy.Key.ToString();
                    return false;
                }
            }
            else if (Items is ObservableCollection<TlDmCheDoBHXHModel>)
            {
                Dictionary<string, int> dataDic = (Items as ObservableCollection<TlDmCheDoBHXHModel>).Where(x => !x.IsDeleted && !string.IsNullOrEmpty(x.SLoaiTruyLinh)).GroupBy(x => x.SLoaiTruyLinh).ToDictionary(x => x.Key, y => y.Count());
                List<TlDmCheDoBHXH> lstItemModified = (itemsModified as ObservableCollection<TlDmCheDoBHXH>).ToList();
                if (lstItemModified.Any(x => !string.IsNullOrEmpty(x.SLoaiTruyLinh) && dataDic.ContainsKey(x.SLoaiTruyLinh) && dataDic[x.SLoaiTruyLinh] > 1))
                {
                    TlDmCheDoBHXH item = lstItemModified.FirstOrDefault(x => !string.IsNullOrEmpty(x.SLoaiTruyLinh) && dataDic.ContainsKey(x.SLoaiTruyLinh) && dataDic[x.SLoaiTruyLinh] > 1);
                    IIdMaDanhMucExisted = item.SLoaiTruyLinh;
                    return false;
                }

            }
            return true;
        }
        private bool ValidateCheckNotEdit()
        {
            ObservableCollection<DanhMucNguonNganSachModel> itemsModified = _mapper.Map<ObservableCollection<DanhMucNguonNganSachModel>>(Items.Where(i => i.IsModified && !i.IsDeleted));
            ObservableCollection<DanhMucNguonNganSachModel> itemsDeleted = _mapper.Map<ObservableCollection<DanhMucNguonNganSachModel>>(Items.Where(i => i.IsDeleted));

            foreach (DanhMucNguonNganSachModel item in itemsModified)
            {
                string[] listFixMaNguon = "1,2,5,8,9".Split(',');
                if (listFixMaNguon.Contains(item.IIDMaDanhMuc))
                {
                    DanhMucNguonNganSachModel itemFix = _mapper.Map<ObservableCollection<DanhMucNguonNganSachModel>>(_service.FindFixNguonNganSach(item.Id)).FirstOrDefault();
                    if (item.IIDMaDanhMuc != itemFix.IIDMaDanhMuc || item.STen != itemFix.STen)
                    {
                        return false;
                    }

                }
            }
            foreach (DanhMucNguonNganSachModel item in itemsDeleted)
            {
                string[] listFixMaNguon = "1,2,5,8,9".Split(',');
                if (listFixMaNguon.Contains(item.IIDMaDanhMuc))
                {
                    return false;
                }
            }

            return true;
        }
        private void UpdateXauNoiMa(NsMucLucNganSach item)
        {
            item.XauNoiMa = "";
            if (!string.IsNullOrEmpty(item.Lns))
            {
                item.XauNoiMa = string.Format("{0}", item.Lns);
                if (!string.IsNullOrEmpty(item.L))
                {
                    item.XauNoiMa = string.Format("{0}-{1}", item.Lns, item.L);
                    if (!string.IsNullOrEmpty(item.K))
                    {
                        item.XauNoiMa = string.Format("{0}-{1}-{2}", item.Lns, item.L, item.K);
                        if (!string.IsNullOrEmpty(item.M))
                        {
                            item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}", item.Lns, item.L, item.K, item.M);
                            if (!string.IsNullOrEmpty(item.Tm))
                            {
                                item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}", item.Lns, item.L, item.K, item.M, item.Tm);
                                if (!string.IsNullOrEmpty(item.Ttm))
                                {
                                    item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", item.Lns, item.L, item.K, item.M, item.Tm, item.Ttm);
                                    if (!string.IsNullOrEmpty(item.Ng))
                                    {
                                        item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", item.Lns, item.L, item.K, item.M, item.Tm, item.Ttm, item.Ng);
                                        if (!string.IsNullOrEmpty(item.Tng))
                                        {
                                            item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", item.Lns, item.L, item.K, item.M, item.Tm, item.Ttm, item.Ng, item.Tng);
                                            if (!string.IsNullOrEmpty(item.Tng1))
                                            {
                                                item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}", item.Lns, item.L, item.K, item.M,
                                                    item.Tm, item.Ttm, item.Ng, item.Tng, item.Tng1);
                                                if (!string.IsNullOrEmpty(item.Tng2))
                                                {
                                                    item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}", item.Lns, item.L, item.K, item.M,
                                                        item.Tm, item.Ttm, item.Ng, item.Tng, item.Tng1, item.Tng2);
                                                    if (!string.IsNullOrEmpty(item.Tng3))
                                                    {
                                                        item.XauNoiMa = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}", item.Lns, item.L, item.K, item.M,
                                                            item.Tm, item.Ttm, item.Ng, item.Tng, item.Tng1, item.Tng2, item.Tng3);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void OnRefresh(object obj)
        {
            try
            {
                if (ModelName.ToString().Equals("NhDaDuAnModel")
                            || ModelName.ToString().Equals("NhDaHopDongModel")
                            || ModelName.ToString().Equals("TlDanhMucChucVuNq104Model")
                            || ModelName.ToString().Equals("TlDanhMucChucDanhNq104Model"))
                {
                    LoadData();
                }
                else
                {
                    genericControlBaseService.OnRefresh(obj);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void InitDialog(PropertyInfo property)
        {
            genericControlBaseService.InitDialog(property);
        }

        public void DeleteColumnDataRefer(PropertyInfo property)
        {
            genericControlBaseService.DeleteColumnDataRefer(property);
        }

        private void SetCommonStyleForHeader(DataGridColumn col, PropertyInfo property)
        {
            if (Attribute.IsDefined(property, typeof(ColumnTypeAttribute)))
            {
                ColumnTypeAttribute columnTypeAttribute = (ColumnTypeAttribute)Attribute.GetCustomAttribute(property, typeof(ColumnTypeAttribute));
                if (columnTypeAttribute.ColumnType.Equals(ColumnType.ReferencePopup))
                {
                    col.HeaderStyle = (Style)Application.Current.TryFindResource("MaterialDesignDataGridColumnHeaderRef");
                    col.SetValue(DataGridColumn.WidthProperty, new DataGridLength(300, DataGridLengthUnitType.Pixel));
                }
            }
        }

        public bool CheckModelAndPropertyHidden(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (((Items is ObservableCollection<BhDmCheDoBhxhModel> ||
                Items is ObservableCollection<BhDanhMucLoaiChiModel> ||
                Items is ObservableCollection<BhDmMucDongBHXHModel> ||
                Items is ObservableCollection<BhDmCoSoYTeModel>)
                && e.PropertyName.Equals("ITrangThai")))
                return true;

            if (Items is ObservableCollection<BhDmCauHinhThamSoModel> && e.PropertyName.Equals("BTrangThai"))
                return true;

            if (Items is ObservableCollection<BhDmKinhPhiModel> && e.PropertyName.Equals("SapXep"))
                return true;

            return false;
        }

        private void BuildHeaderContent(DataGridAutoGeneratingColumnEventArgs e, DataGridColumn col)
        {
            StackPanel headerContent = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            // column name
            string colName = (e.PropertyDescriptor as PropertyDescriptor).DisplayName;
            if (e.PropertyName.Equals(nameof(ModelBase.IsSelected)))
            {
                colName = "Chọn";
                CheckBox checkBox = new CheckBox();
                checkBox.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(IsAllItemsSelected)) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Source = this });
                headerContent.Children.Add(checkBox);
                col.Header = headerContent;
                return;
            }
            TextBlock textBlock = new TextBlock
            {
                Text = colName,
                VerticalAlignment = VerticalAlignment.Center,
                Name = "col"
            };
            headerContent.Children.Add(textBlock);

            // Hide filter column "Trang thai"
            if (!CheckModelAndPropertyHidden(e))
            {
                // create filter stack panel wrap colorzone
                StackPanel filterStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                // create colorzone wrap borderOutside
                ColorZone colorZone = new ColorZone
                {
                    Mode = ColorZoneMode.PrimaryLight
                };

                // borderOutside wrap searchbar
                Border borderOutside = new Border
                {
                    Background = Brushes.White,
                    Margin = new Thickness(5, 5, 5, 5),
                    MaxHeight = 30.0,
                    CornerRadius = new CornerRadius(3),
                    BorderThickness = new Thickness(1, 1, 1, 1)
                };
                // search bar wrap textbox and filter btn
                StackPanel searchBar = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                // filter button
                Button filterBtn = BuildFrameworkElementFactoryIconButton(PackIconKind.Magnify);
                filterBtn.Click += new RoutedEventHandler((object sender, RoutedEventArgs evt) =>
                {
                    OnFilterDataByColumn(e.PropertyName);
                });
                searchBar.Children.Add(filterBtn);
                // filter text box
                TextBox filterTextBox = new TextBox
                {
                    Width = 150.0,
                    Margin = new Thickness(5, 0, 0, 0),
                    BorderThickness = new Thickness(0, 0, 0, 0),
                    Style = (Style)Application.Current.TryFindResource("MaterialDesignTextBox"),
                    VerticalAlignment = VerticalAlignment.Center,
                    Name = e.PropertyName + TEXTBOX_SUFFIX,
                    Visibility = Visibility.Visible
                };
                filterTextBox.SetValue(TextFieldAssist.DecorationVisibilityProperty, Visibility.Hidden);
                KeyEventHandler previewKeyDownEventHandler = (object sender, System.Windows.Input.KeyEventArgs args) =>
                {
                    if (args.Key == Key.Return)
                    {
                        OnFilterDataByColumn(e.PropertyName);
                        args.Handled = true;
                    }
                    else if (args.Key == Key.Escape)
                    {
                        ToggleFilterPopup(false, e.PropertyName + POPUP_SUFFIX);
                        args.Handled = true;
                    }
                };
                filterTextBox.PreviewKeyDown += previewKeyDownEventHandler;
                // binding for filter text
                Binding newBinding = new Binding(e.PropertyName) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Source = FilterModel };
                filterTextBox.SetBinding(TextBox.TextProperty, newBinding);
                RoutedEventHandler eventHandler = (sender, evt) =>
                {
                    TextBox x = sender as System.Windows.Controls.TextBox;
                    Keyboard.Focus(x);
                };
                filterTextBox.Loaded += eventHandler;
                searchBar.Children.Add(filterTextBox);
                borderOutside.Child = searchBar;
                colorZone.Content = borderOutside;
                filterStackPanel.Children.Add(colorZone);

                PopupBox popupBox = new PopupBox
                {
                    PopupContent = filterStackPanel,
                    Padding = new Thickness(0, 0, 0, 0),
                    StaysOpen = true,
                    Name = e.PropertyName + POPUP_SUFFIX
                };
                RoutedEventHandler routedEventHandler = (sender, obj) =>
                {
                    PopupBox box = sender as PopupBox;
                    PackIcon packIcon = new PackIcon();
                    packIcon.Kind = PackIconKind.FilterOutline;
                    packIcon.Opacity = 0.5;
                    packIcon.Style = (Style)Application.Current.TryFindResource("DataGridFilterIcon");
                    box.ToggleContent = packIcon;
                };
                popupBox.Loaded += routedEventHandler;
                //if (!(col is DataGridCheckBoxColumn) && !(col is MaterialDesignThemes.Wpf.DataGridComboBoxColumn))
                headerContent.Children.Add(popupBox);
            }

            col.Header = headerContent;
        }

        private void BuildDatePickerCellEditingTemplate(string propertyName, DataGridTemplateColumn col, string format)
        {
            // cell template
            FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));
            Binding dateBinding = new Binding(propertyName) { Converter = new DateConverter { format = format } };
            textBlock.SetValue(TextBlock.TextProperty, dateBinding);
            Style style = new Style();
            DataTrigger trigger = new DataTrigger
            {
                Value = true,
                Binding = new Binding("IsDeleted"),
                Setters = { new Setter(TextBlock.TextDecorationsProperty, TextDecorations.Strikethrough),
                            new Setter(TextBlock.ForegroundProperty, (SolidColorBrush)(new BrushConverter().ConvertFrom("#d50000"))) }
            };
            style.Triggers.Add(trigger);
            textBlock.SetValue(TextBlock.StyleProperty, style);
            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = textBlock;
            col.CellTemplate = cellTemplate;
            // cell editing template
            FrameworkElementFactory datePicker = new FrameworkElementFactory(typeof(DatePicker));
            datePicker.SetValue(DatePicker.SelectedDateProperty, new Binding(propertyName));
            Style datepickerStyle = new Style();
            datepickerStyle.TargetType = typeof(DatePickerTextBox);
            ControlTemplate controlTemplate = new ControlTemplate();
            FrameworkElementFactory textbox = new FrameworkElementFactory(typeof(TextBox));
            textbox.SetValue(TextBox.NameProperty, "PART_Textbox");
            Binding textboxBinding = new Binding(propertyName) { StringFormat = format, RelativeSource = new RelativeSource { AncestorType = typeof(DatePicker) } };
            textbox.SetValue(TextBox.TextProperty, textboxBinding);
            controlTemplate.VisualTree = textbox;
            datepickerStyle.Setters.Add(new Setter(Control.TemplateProperty, controlTemplate));
            datePicker.SetResourceReference(DatePicker.StyleProperty, datepickerStyle);
            DataTemplate cellEditingTemplate = new DataTemplate();
            cellEditingTemplate.VisualTree = datePicker;
            col.CellEditingTemplate = cellEditingTemplate;
        }

        private void OnFilterDataByColumn(string colName)
        {
            try
            {
                DataGrid d = FnCommonUtils.FindChild<DataGrid>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), ModelName);
                if (d == null)
                {
                    d = FnCommonUtils.FindChild<DataGrid>(GenericControlCustomWindow, ModelName);
                }
                if (d == null)
                {
                    return;
                }
                CancelEditData(d);
                genericControlBaseService.BeForeRefresh();
                _dataCollectionView.Refresh();
                if (d.Items.Count > 0)
                {
                    foreach (object itemSource in d.ItemsSource)
                    {
                        ((TModel)itemSource).IsFilter = false;
                    }
                    foreach (object item in d.Items)
                    {
                        ((TModel)item).IsFilter = true;
                    }
                    DataGridCellInfo cell = new DataGridCellInfo(d.Items[0], d.Columns[0]);
                    SelectedItem = (TModel)d.Items[0];
                    d.CurrentCell = cell;
                }
                ToggleFilterPopup(false, colName + POPUP_SUFFIX);
                OnPropertyChanged(nameof(IsAllItemsSelected));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSaveReference()
        {
            try
            {
                if (!IsMultipleSelect)
                {
                    GenericControlCustomWindow?.SavedAction?.Invoke(SelectedItem);
                }

                else
                {
                    //if (IsShowTyLeTangLuong && (Items is ObservableCollection<TlDanhMucChucDanhNq104Model> || Items is ObservableCollection<TlDanhMucChucVuNq104Model>))
                    //{
                    //    GenericControlCustomWindow?.SavedAction?.Invoke(Items.Where(i => i.IsSelected));
                    //    var itemsEdit = _mapper.Map<ObservableCollection<TEntity>>(Items.Where(i => i.IsModified && !i.IsDeleted && !i.IsAdded));
                    //    foreach (var item in itemsEdit as ObservableCollection<TlDanhMucChucDanhNq104Model>)
                    //    {
                    //        item.TienLuong = item.TienLuong + ((item.TienLuong * ITyLeTang) / 100);
                    //        item.TienNangLuong = item.TienNangLuong + ((item.TienNangLuong * ITyLeTang) / 100);
                    //    }
                    //    foreach (var item in itemsEdit as ObservableCollection<TlDanhMucChucVuNq104Model>)
                    //    {
                    //        item.TienLuong = item.TienLuong + ((item.TienLuong * ITyLeTang) / 100);
                    //        item.TienNangLuong = item.TienNangLuong + ((item.TienNangLuong * ITyLeTang) / 100);
                    //    }
                    //}
                    //else
                    GenericControlCustomWindow?.SavedAction?.Invoke(Items.Where(i => i.IsSelected));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnCloseWindow()
        {
            GenericControlCustomWindow?.Close();
        }

        private void ToggleFilterPopup(bool IsOpen)
        {
            ToggleFilterPopup(IsOpen, _currentCol + POPUP_SUFFIX);
        }

        private void ToggleFilterPopup(bool IsOpen, string elementName)
        {
            PopupBox popupBox = FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
            if (popupBox == null)
            {
                popupBox = FnCommonUtils.FindChild<PopupBox>(GenericControlCustomWindow, elementName);
            }
            if (popupBox != null)
            {
                popupBox.IsPopupOpen = IsOpen;
                if (!IsOpen)
                {
                    DataGrid d = FnCommonUtils.FindChild<DataGrid>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), ModelName);
                    if (d == null)
                    {
                        d = FnCommonUtils.FindChild<DataGrid>(GenericControlCustomWindow, ModelName);
                    }
                    if (d == null)
                    {
                        return;
                    }
                    if (d.Items.Count > 0)
                    {
                        DataGridCellInfo cell = new DataGridCellInfo(d.Items[0], d.Columns[0]);
                        SelectedItem = (TModel)d.Items[0];
                        d.CurrentCell = cell;
                    }
                }
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (!IsMultipleSelect)
            {
                DialogHost.CloseDialogCommand.Execute(SelectedItem, null);
            }
            else
            {
                DialogHost.CloseDialogCommand.Execute(Items.Where(i => i.IsSelected), null);
            }
        }

        private void OnOpenReferencePopup(object obj)
        {
            try
            {
                DataGrid d = obj as DataGrid;
                if (d == null || string.IsNullOrEmpty(_currentCol))
                {
                    return;
                }
                if ((_currentCol == nameof(TlDanhMucChucVuNq104Model.TienLuong) || _currentCol == nameof(TlDanhMucChucVuNq104Model.TienNangLuong)) && d.Name == "TlDanhMucChucVuNq104Model")
                {
                    PropertyInfo property = ModelType.GetProperty(_currentCol);
                    InitDialog(property);
                }
                else if ((_currentCol == nameof(TlDanhMucChucDanhNq104Model.TienLuong) || _currentCol == nameof(TlDanhMucChucDanhNq104Model.TienNangLuong)) && d.Name == "TlDanhMucChucDanhNq104Model")
                {
                    PropertyInfo property = ModelType.GetProperty(_currentCol);
                    InitDialog(property);
                }
                else
                {
                    PropertyInfo property = ModelType.GetProperty(_currentCol);
                    InitDialog(property);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnDeleteRefer(object obj)
        {
            PropertyInfo property = ModelType.GetProperty(_currentCol);
            DeleteColumnDataRefer(property);
        }

        private void OnLoadedFormatPopup(object obj)
        {
            PopupBox p = (PopupBox)obj;
            StackPanel stackpanel = new StackPanel();
            PropertyInfo[] properties = typeof(TModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (Attribute.IsDefined(property, typeof(DisplayNameAttribute)) && property.PropertyType == typeof(string))
                {
                    DisplayNameAttribute name = (DisplayNameAttribute)Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute));
                    StackPanel elementFactory = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal
                    };
                    TextBlock textblock = new TextBlock()
                    {
                        Text = name.DisplayName,
                        Width = 100,
                        Margin = new Thickness(10),
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    elementFactory.Children.Add(textblock);

                    TextBox textbox = new TextBox()
                    {
                        Margin = new Thickness(10),
                        VerticalAlignment = VerticalAlignment.Center,
                        MinWidth = 150
                    };
                    textbox.TextChanged += (sender, e) =>
                    {
                        TextBox textBox = sender as TextBox;
                        if (_formatDictionary.ContainsKey(property.Name))
                        {
                            _formatDictionary[property.Name] = textBox.Text;
                        }
                        else
                        {
                            _formatDictionary.Add(property.Name, string.Empty);
                        }
                    };
                    textbox.Loaded += (sender, e) =>
                    {
                        TextBox textBox = sender as TextBox;
                        if (!_formatDictionary.ContainsKey(property.Name))
                        {
                            _formatDictionary.Add(property.Name, string.Empty);
                        }
                        textBox.Text = _formatDictionary[property.Name];
                    };
                    elementFactory.Children.Add(textbox);
                    stackpanel.Children.Add(elementFactory);
                }
            }
            p.PopupContent = stackpanel;
        }

        private void GenerateReferenceColumn(PropertyInfo property, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTemplateColumn col = new DataGridTemplateColumn();
            // style for header
            SetCommonStyleForHeader(col, property);
            // build header
            BuildHeaderContent(e, col);
            SubStringConverter subStringConverter = new SubStringConverter();
            FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));
            textBlock.SetValue(TextBlock.TextProperty, new Binding(e.PropertyName) { Converter = subStringConverter });
            if (e.PropertyDescriptor is PropertyDescriptor reflect
                && !new List<string> { "DmCongKhaiTaiChinhModel", "DmMucLucQuyetToanModel" }.Contains(reflect.ComponentType.Name))
            {
                textBlock.SetValue(TextBlock.TextWrappingProperty, TextWrapping.WrapWithOverflow);
                Style style = new Style();
                DataTrigger trigger = new DataTrigger
                {
                    Value = true,
                    Binding = new Binding("IsDeleted"),
                    Setters = { new Setter(TextBlock.TextDecorationsProperty, TextDecorations.Strikethrough),
                            new Setter(TextBlock.ForegroundProperty, (SolidColorBrush)(new BrushConverter().ConvertFrom("#d50000")))}
                };
                style.Triggers.Add(trigger);
                textBlock.SetValue(TextBlock.StyleProperty, style);
            }
            else
            {
                Style styleResource = Application.Current.Resources["DataGridTextColumnStyle"] as Style;
                textBlock.SetValue(TextBlock.StyleProperty, styleResource);
            }

            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = textBlock;
            col.CellTemplate = cellTemplate;
            col.MaxWidth = 1200;
            e.Column = col;
            e.Column.SortMemberPath = e.PropertyName;
            e.Column.CanUserSort = true;
        }

        private void GenerateDateTimeColumn(PropertyInfo property, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTemplateColumn col = new DataGridTemplateColumn();
            SetCommonStyleForHeader(col, property);
            // build header
            BuildHeaderContent(e, col);
            string dateFormat = "dd/MM/yyyy";
            if (Attribute.IsDefined(property, typeof(FormatAttribute)))
            {
                FormatAttribute formatAttribute = (FormatAttribute)Attribute.GetCustomAttribute(property, typeof(FormatAttribute));
                dateFormat = formatAttribute.Format;
            }
            BuildDatePickerCellEditingTemplate(e.PropertyName, col, dateFormat);
            e.Column = col;
            e.Column.SortMemberPath = e.PropertyName;
            e.Column.CanUserSort = true;
        }

        private void GenerateCheckboxColumn(PropertyInfo property, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridCheckBoxColumn col = new DataGridCheckBoxColumn();
            Style elementStyle = new Style();
            elementStyle.TargetType = typeof(CheckBox);
            elementStyle.BasedOn = (Style)Application.Current.TryFindResource("MaterialDesignDataGridCheckBoxColumnStyle");
            Style editingStyle = new Style();
            editingStyle.TargetType = typeof(CheckBox);
            editingStyle.BasedOn = (Style)Application.Current.TryFindResource("MaterialDesignDataGridCheckBoxColumnEditingStyle");
            if (typeof(HTChucNangModel).Equals(typeof(TModel)))
            {
                elementStyle.Setters.Add(new Setter(CheckBox.IsEnabledProperty, new Binding("IsEnableCheckbox")));
                editingStyle.Setters.Add(new Setter(CheckBox.IsEnabledProperty, new Binding("IsEnableCheckbox")));
            }
            col.SetValue(DataGridCheckBoxColumn.ElementStyleProperty, elementStyle);
            col.SetValue(DataGridCheckBoxColumn.EditingElementStyleProperty, editingStyle);
            BuildHeaderContent(e, col);
            SetCommonStyleForHeader(col, property);
            col.Binding = new Binding(e.PropertyName);
            e.Column = col;
            e.Column.CanUserSort = false;
        }

        private void GenerateDatagridTextColumn(PropertyInfo property, DataGridAutoGeneratingColumnEventArgs e)
        {
            MaterialDesignThemes.Wpf.DataGridTextColumn col = new MaterialDesignThemes.Wpf.DataGridTextColumn();
            SetCommonStyleForHeader(col, property);
            // set style for text
            Style eleStyle = new Style();
            eleStyle.TargetType = typeof(TextBlock);
            if (property.Name.StartsWith("Tien"))
            {
                eleStyle.BasedOn = (Style)Application.Current.TryFindResource("DataGridTextColumnDetailRightStyle");
            }
            else
            {
                eleStyle.BasedOn = (Style)Application.Current.TryFindResource("DataGridTextColumnDetailStyle");
            }

            if (Attribute.IsDefined(property, typeof(HorizontalAttribute)))
            {
                HorizontalAttribute extensionAttribute = (HorizontalAttribute)Attribute.GetCustomAttribute(property, typeof(HorizontalAttribute));
                eleStyle.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, extensionAttribute.horizontalAlignment));
            }
            col.ElementStyle = eleStyle;
            //col.Cell Edit Style 
            col.EditingElementStyle = (Style)Application.Current.TryFindResource("DataGridTextColumnEditingStyle");
            // custom style for column, base on type of tmodel
            genericControlBaseService.CustomGenerateDatagridTextColumn(col, property);
            // for header
            BuildHeaderContent(e, col);
            Binding binding = new Binding(e.PropertyName) { ValidatesOnDataErrors = true, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            if (property.Name.StartsWith("IMaCha"))
            {
                var convertNumber = (IValueConverter)Application.Current.TryFindResource("NumberIntToStringEmptyConverter");
                binding.Converter = convertNumber;
            }
            if (Attribute.IsDefined(property, typeof(FormatAttribute)))
            {
                FormatAttribute formatAttribute = (FormatAttribute)Attribute.GetCustomAttribute(property, typeof(FormatAttribute));
                binding.StringFormat = formatAttribute.Format;
                //binding.ConverterCulture = new CultureInfo("en-US");
            }
            col.Binding = binding;
            // col.IsReadOnly = IsDialog;
            e.Column = col;
        }

        private Button BuildFrameworkElementFactoryIconButton(PackIconKind? packIcon)
        {
            Button btn = new Button
            {
                Style = (Style)Application.Current.TryFindResource("MaterialDesignToolButton")
            };
            PackIcon searchIcon = new PackIcon
            {
                Kind = packIcon.Value
            };
            btn.Content = searchIcon;
            return btn;
        }

        private void GenerateComboboxColumn(PropertyInfo property, DataGridAutoGeneratingColumnEventArgs e)
        {
            // load combobox data
            ObservableCollection<ComboboxItem> DataSource = genericControlBaseService.LoadComboboxData(property);
            MaterialDesignThemes.Wpf.DataGridComboBoxColumn col = new MaterialDesignThemes.Wpf.DataGridComboBoxColumn();
            SetCommonStyleForHeader(col, property);
            BuildHeaderContent(e, col);
            col.ItemsSource = DataSource;
            col.DisplayMemberPath = "DisplayItem";
            col.SelectedValuePath = "ValueItem";
            col.SelectedValueBinding = new Binding(property.Name);
            genericControlBaseService.CustomGenerateComboboxColumn(col, property);
            e.Column = col;
            e.Column.CanUserSort = false;
        }

        private void OnExportExcel()
        {
            if (typeof(TModel).Equals(typeof(NsMuclucNgansachModel)))
            {
                MlnsExportViewModel mlnsExportViewModel = new MlnsExportViewModel((MucLucNganSachService)_serviceProvider.GetService(typeof(IMucLucNganSachService)))
                {
                    sessionService = _sessionService,
                    Mapper = _mapper,
                    ServiceProvider = _serviceProvider
                };
                mlnsExportViewModel.Init();
                MlnsExportView Dialog = new MlnsExportView()
                {
                    DataContext = mlnsExportViewModel
                };
                System.Threading.Tasks.Task<object> dialog = DialogHost.Show(Dialog, "RootDialog", ClosingEventHandler);
            }
            else if (typeof(TModel).Equals(typeof(NhDmNhaThauModel)))
            {
                OnExportDmNhaThauTemplate();
            }
            //else if (typeof(TModel).Equals(typeof(NhDaHopDongModel)))
            //{
            //    OnExportHopDongTemplate();
            //}
            else
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Items", Items);
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<TModel>(TemplateFileName, data);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(TemplateFileName) + "_" + DateTime.Now.ToString("ddMMyyhhmmss");
                    results.Add(new ExportResult(Description, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
        }

        private void OnViewDetail()
        {
            if (SelectedItem == null)
            {
                return;
            }
            genericControlBaseService.OnViewDetail();
        }

        private void OnExportJsonFile()
        {
            _exportService.ExportJsonFile(ExportJsonFileName, Items);
        }

        private void OnImportExcel()
        {
            if (typeof(TModel).Equals(typeof(NhDmNhaThauModel)))
            {
                OnImportDmNhaThauData();
            }
            else
            {
                Type genericType = typeof(ImportExcelViewModel<,,,>);
                Type constructed = genericType.MakeGenericType(typeof(TModel), typeof(TEntity), typeof(TService), ImportModelType);
                Object model = Activator.CreateInstance(constructed, _serviceProvider, _service, _authenticationInfo);
                PropertyInfo dataTemplateFileName = model.GetType().GetProperty("DataTemplateFileName");
                dataTemplateFileName.SetValue(model, DataTemplateFileName);
                ImportExcelView importExcelView = new ImportExcelView()
                {
                    DataContext = model
                };
                MethodInfo InitMethod = model.GetType().GetMethod("Init");
                InitMethod.Invoke(model, null);
                importExcelView.Show();
            }
        }
        //public void OnExportHopDongTemplate()
        //{
        //    try
        //    {
        //        BackgroundWorkerHelper.Run((s, e) =>
        //        {
        //            IsLoading = true;

        //            List<ExportResult> results = new List<ExportResult>();
        //            string templateFileName;
        //            string fileNamePrefix;
        //            string fileNameWithoutExtension;

        //            templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, ExportFileName.RPT_NH_DANHMUC_HOPDONG);
        //            fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
        //            var xlsFile = _exportService.Export<NhDaHopDongModel>(templateFileName, new Dictionary<string, object>());
        //            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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

        public void OnExportDmNhaThauTemplate()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;

                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DM, ExportFileName.EPT_NH_DANHMUC_NHATHAU);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<NhDmNhaThauModel>(templateFileName, new Dictionary<string, object>());
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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
        public void OnImportDmNhaThauData()
        {
            ImportNhaThauViewModel importNhaThauViewModel = new ImportNhaThauViewModel(
                _sessionService,
                _logger,
                _mapper,
                (IImportExcelService)_serviceProvider.GetService(typeof(IImportExcelService)),
                (INhDmNhaThauService)_serviceProvider.GetService(typeof(INhDmNhaThauService)));
            importNhaThauViewModel.Init();
            importNhaThauViewModel.SavedAction = obj =>
            {
                LoadData();
            };
            importNhaThauViewModel.ShowDialog();
        }

        private void OnUpdateDanhMucChuKy()
        {
            if (!typeof(TModel).Equals(typeof(DmChuKyModel)) || SelectedItem == null)
            {
                return;
            }
            DmChuKyDialogViewModel dmChuKyDialogViewModel = new DmChuKyDialogViewModel(_mapper, _serviceProvider, _sessionService)
            {
                DmChuKyModel = SelectedItem as DmChuKyModel
            };
            dmChuKyDialogViewModel.ParentPage = this;
            dmChuKyDialogViewModel.Init();
            dmChuKyDialogViewModel.ShowDialog();
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

        private void SelectAll(bool select, IEnumerable<TModel> models)
        {
            AfterSelectAll = true;
            foreach (TModel model in models.Where(n => n.IsFilter))
            {
                model.IsSelected = select;
            }
            AfterSelectAll = false;
        }

        public void InvokePropertyChange(string name)
        {
            OnPropertyChanged(name);
        }

        private void OnUpdateCadres()
        {
            try
            {
                MessageBoxResult dialogResult = MessageBox.Show(Resources.ConfirmUpdateCadres, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes && (typeof(TModel).Equals(typeof(TlDanhMucChucVuNq104Model)) || typeof(TModel).Equals(typeof(TlDanhMucChucDanhNq104Model))))
                {
                    _service.UpdateCadres();
                }
                MessageBoxHelper.Info(Resources.MsgSaveDone);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public int GetValueTyLeTang()
        {
            return ITyLeTang;
        }
    }
}
