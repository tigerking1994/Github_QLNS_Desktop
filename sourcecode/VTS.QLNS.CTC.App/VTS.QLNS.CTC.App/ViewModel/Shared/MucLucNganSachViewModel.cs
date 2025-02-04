using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.View.Shared.Import;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.ImportViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class MucLucNganSachViewModel : GridViewModelBase<NsMuclucNgansachModel>
    {
        public ILog _logger;
        public MucLucNganSachService _service;
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

        public override string FuncCode => NSFunctionCode.CATEGORY_BUDGET_MLNS;
        public override string Name => "Danh mục MLNS - Khoa";
        public override string Title => "Danh mục mục lục ngân sách - Khoa";
        public override string Description => "Chỉnh sửa thông tin mục lục ngân sách";
        public string ExportJsonFileName = "mlns.json";
        public string TemplateFileName = "rpt_dm_mlns.xlsx";
        public Type ImportModelType = typeof(MLNSImportModel);
        public string DataTemplateFileName = "rpt_dm_mlns_template_data.xlsx";

        public override Type ContentType => typeof(MucLucNganSachView);
        public Type ModelType => typeof(NsMuclucNgansachModel);
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

        //Thêmmmmm

        private NsMucLucNganSachTotalModel _nsMucLucNganSachTotal;
        public NsMucLucNganSachTotalModel NsMucLucNganSachTotal
        {
            get => _nsMucLucNganSachTotal;
            set => SetProperty(ref _nsMucLucNganSachTotal, value);
        }

        public bool IsInit { get; set; } = false;

        //Hết Thêmmmmm

        public bool IsDanhMucChuky { get; set; }
        public bool IsMultipleSelect { get; set; }
        public bool IsEnabledSaveBtn => SelectedItem != null;
        public Visibility VisibilityDialogBtn => IsDialog ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibilityFunctionBtn => IsDanhMucChuky ? Visibility.Collapsed : Visibility.Visible;
        public string Hint => IsDialog ? "Ấn phím space để chọn checkbox nếu có" : "";
        public string Total => " (Tổng số bản ghi: " + Items.Count() + ")";
        public string ModelName => "dgdDataMLNS";

        public bool AfterSelectAll;

        private string _filtertext;
        public string FilterText
        {
            get => _filtertext;
            set => SetProperty(ref _filtertext, value);
        }

        private NsMuclucNgansachModel _filterModel;
        public NsMuclucNgansachModel FilterModel
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

        public MucLucNganSachView MucLucNganSachView { get; set; }

        private List<string> mlnsType = new List<string>
        {
            "TNG3", "TNG2", "TNG1", "TNG", "NG"
        };
        private string xnmConcatenation = "";
        private ICollection<NsMuclucNgansachModel> _filterResult = new HashSet<NsMuclucNgansachModel>();
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
        public double FTienAn { get; set; }
        public bool SktKyHieu { get; set; } = true;
        public NsMuclucNgansachModel NsMuclucNgansachModel { get; set; }
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
        public RelayCommand EditDetailCommand { get; }
        public RelayCommand FilterCommand { get; }
        public RelayCommand EscapteFilterCommand { get; }
        public RelayCommand SelectMlnsParentCommand { get; }
        public RelayCommand CheckDataCommand { get; }

        public MucLucNganSachViewModel(IMucLucNganSachService service, IMapper mapper, ISessionService sessionService, IExportService exportService, IServiceProvider serviceProvider, MucLucNganSachCheckDataViewModel mucLucNganSachCheckDataViewModel)
        {
            _service = service as MucLucNganSachService;
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
            ViewDetailCommand = new RelayCommand(obj => OnViewDetail(), obj => !IsDialog);
            EditDetailCommand = new RelayCommand(obj => OnViewEditDetail(), obj => !IsDialog);
            FilterCommand = new RelayCommand(o => OnFilterDataByColumn(o.ToString()));
            EscapteFilterCommand = new RelayCommand(o => ToggleFilterPopup(false, o.ToString() + POPUP_SUFFIX));
            SelectMlnsParentCommand = new RelayCommand(o => OnSelectMlnsParent());
            CheckDataCommand = new RelayCommand(obj => CheckData());
            MucLucNganSachCheckDataViewModel = mucLucNganSachCheckDataViewModel;
        }

        public override void Init()
        {
            try
            {
                InitBaseData();
                IsInit = false;
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
            var data = _service.FindAll(_authenticationInfo);
            Items = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(data);
            foreach (var item in Items)
            {
                Debug.WriteLine($"Item type: {item.GetType()}");
                string typeMLNS = getTypeOfMlns(item);
                item.IsEnableDuToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedDuToanChiTietToi;
                item.IsEnableQuyetToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedQuyetToanChiTietToi;
                item.PropertyChanged += Item_PropertyChanged;
                item.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
                item.PropertyChanged += NsTuChiModel_PropertyChanged;
            }
            _isFirstLoad = true;
            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            _dataCollectionView.Filter = ItemsViewFilter;

            //Thêmmmmm

            CalculateData();

            //Hết Thêmmmmm

            OnPropertyChanged(nameof(Items));
            _isFirstLoad = false;
        }

        

        //Thêmmmmm
        private void NsTuChiModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(NsMuclucNgansachModel.TuChi))
            {
                NsMuclucNgansachModel item = (NsMuclucNgansachModel)sender;
                item.IsModified = true;
                if (args.PropertyName == nameof(NsMuclucNgansachModel.TuChi) && !IsInit)
                {
                    IsInit = true;
                    CalculateData(); // Cập nhật tổng toàn bộ nếu cần
                    IsInit = false;
                }
            }
        }
        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TuChi = 0;
                    return x;
                }).ToList();

            foreach (var item in Items.Where(x => x.IsEditable && x.IsFilter && (x.TuChi != 0)))
            {
                CalculateParent(item, item);
            }

            CalculateTotal();
        }

        private void CalculateParent(NsMuclucNgansachModel currentItem, NsMuclucNgansachModel seftItem)
        {
            var parrentItem = Items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.TuChi += seftItem.TuChi;
            CalculateParent(parrentItem, seftItem);
        }

        private void CalculateTotal()
        {
            NsMucLucNganSachTotal = new NsMucLucNganSachTotalModel();
            var listChildren = Items.Where(x => x.IsEditable && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                NsMucLucNganSachTotal.FTongTuChi += item.TuChi;
            }
        }
        //Hết Thêmmmmm

        private void NsMuclucNgansachModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<NsMuclucNgansachModel> models = Items;
            NsMuclucNgansachModel nsMuclucNgansachModel = sender as NsMuclucNgansachModel;

            // khi tích vào ô tất cả, tránh để hàm propertychanged lặp lại để đảm bảo performance (dùng biến afterSelectAll để kiểm tra điều kiện người dùng vừa tích vào ô tất cả)
            if (args.PropertyName == nameof(NsMuclucNgansachModel.IsDeleted))
            {
                NsMuclucNgansachModel_OnDeleteMLNS(nsMuclucNgansachModel, models);
            }
            else if (args.PropertyName == nameof(NsMuclucNgansachModel.ITrangThai))
            {
                IEnumerable<NsMuclucNgansachModel> children = models.Where(t => nsMuclucNgansachModel.MlnsId.Equals(t.MlnsIdParent));
                foreach (var c in children)
                {
                    c.ITrangThai = nsMuclucNgansachModel.ITrangThai;
                }
            }
            else if (mlnsType.Contains(args.PropertyName))
            {
                string typeMLNS = getTypeOfMlns(nsMuclucNgansachModel);
                if (!typeMLNS.Equals("NG"))
                {
                    nsMuclucNgansachModel.SDuToanChiTietToi = "";
                    nsMuclucNgansachModel.SQuyetToanChiTietToi = "";
                }
                nsMuclucNgansachModel.IsEnableDuToanNGCombobox = typeMLNS.Equals("NG") && !nsMuclucNgansachModel.IsUsedDuToanChiTietToi;
                nsMuclucNgansachModel.IsEnableQuyetToanNGCombobox = typeMLNS.Equals("NG") && !nsMuclucNgansachModel.IsUsedQuyetToanChiTietToi;
            }
            else if (nameof(NsMuclucNgansachModel.SDuToanChiTietToi).Equals(args.PropertyName))
            {
                List<NsMuclucNgansachModel> children = models.Where(t => t.XNM.Contains(nsMuclucNgansachModel.XNM + "-")).ToList();
                string typeMLNS = getTypeOfMlns(nsMuclucNgansachModel);
                if (typeMLNS.Equals("NG"))
                {
                    // todo update bHangChaDuToan of children
                    int selectedSDuToanIndex = mlnsType.IndexOf(nsMuclucNgansachModel.SDuToanChiTietToi);
                    string modelNsType = "NG";
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaDuToan(selectedSDuToanIndex, modelNsTypeIndex, nsMuclucNgansachModel);
                    foreach (NsMuclucNgansachModel ns in children)
                    {
                        string nsType = getTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        UpdateBHangChaDuToan(selectedSDuToanIndex, nsTypeIndex, ns);
                    }
                }
            }
            else if (nameof(NsMuclucNgansachModel.SQuyetToanChiTietToi).Equals(args.PropertyName))
            {
                List<NsMuclucNgansachModel> children = models.Where(t => t.XNM.Contains(nsMuclucNgansachModel.XNM + "-")).ToList();
                string typeMLNS = getTypeOfMlns(nsMuclucNgansachModel);
                if (typeMLNS.Equals("NG"))
                {
                    // todo update bHangChaDuToan of children
                    int selectedSQTIndex = mlnsType.IndexOf(nsMuclucNgansachModel.SQuyetToanChiTietToi);
                    string modelNsType = "NG";
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaQuyetToan(selectedSQTIndex, modelNsTypeIndex, nsMuclucNgansachModel);
                    foreach (NsMuclucNgansachModel ns in children)
                    {
                        string nsType = getTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        UpdateBHangChaQuyetToan(selectedSQTIndex, nsTypeIndex, ns);
                    }
                }
            }
            //nsMuclucNgansachModel.IsUnableEditBQuanlyChiTietToi = !this.IsEditableMlnsBQuanLyAndChiTietToi(nsMuclucNgansachModel);
        }

        public void CheckData()
        {

            MucLucNganSachCheckDataViewModel.Name = "Kiểm tra mục lục ngân sách";
            MucLucNganSachCheckDataViewModel.Description = $"Danh sách chứng từ sử dụng MLNS {SelectedItem.XauNoiMa}";
            var listXauNoiMaDuToan = Items.Where(n => n.BHangChaDuToan.HasValue && !n.BHangChaDuToan.Value && n.MlnsIdParent == SelectedItem.MlnsId).Select(n => n.XauNoiMa).ToList();
            listXauNoiMaDuToan.Add(SelectedItem.XauNoiMa);
            var listXauNoiMaQuyetToan = Items.Where(n => n.BHangChaQuyetToan.HasValue && !n.BHangChaQuyetToan.Value && n.MlnsIdParent == SelectedItem.MlnsId).Select(n => n.XauNoiMa).ToList();
            listXauNoiMaQuyetToan.Add(SelectedItem.XauNoiMa);
            var listXauNoiMaCapPhat = Items.Where(n => n.MlnsIdParent == SelectedItem.MlnsId).Select(n => n.XauNoiMa).ToList();
            listXauNoiMaCapPhat.Add(SelectedItem.XauNoiMa);
            MucLucNganSachCheckDataViewModel.CodeChain = SelectedItem.XauNoiMa;
            MucLucNganSachCheckDataViewModel.CodeChainDuToan = string.Join(StringUtils.COMMA, listXauNoiMaDuToan);
            MucLucNganSachCheckDataViewModel.CodeChainQuyetToan = string.Join(StringUtils.COMMA, listXauNoiMaQuyetToan);
            MucLucNganSachCheckDataViewModel.CodeChainCapPhat = string.Join(StringUtils.COMMA, listXauNoiMaCapPhat);
            MucLucNganSachCheckDataViewModel.Init();
            //MucLucNganSachCheckDataViewModel.SavedAction = obj =>
            //{
            //    var sktChungTu = (NsSktChungTuModel)obj;
            //    _voucherTypeSelected = sktChungTu.ILoaiChungTu.HasValue
            //        ? _voucherTypes.First(x => x.ValueItem.Equals(sktChungTu.ILoaiChungTu.Value.ToString()))
            //        : _voucherTypes.First();
            //    OnPropertyChanged(nameof(VoucherTypeSelected));
            //    this.LoadData();
            //    OpenDetailDialog(sktChungTu);
            //};
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

            IMucLucNganSachService mucLucNganSachService = _service;
            mlnsChiTietToi = mucLucNganSachService.FindMLNSChiTietToi(_sessionService.Current.YearOfWork);
            if (mlnsChiTietToi != null)
            {
                foreach (var col in mlnsType)
                {
                    int indexMLnsChiTietToi = mlnsType.IndexOf(mlnsChiTietToi.SGiaTri);
                    int indexOfMlnsColumn = mlnsType.IndexOf(col);
                    PropertyInfo column = typeof(MucLucNganSachViewModel).GetProperty(col);
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
            _filterModel = new NsMuclucNgansachModel();
            if (_sessionService.Current.AutoGenerateDataSetting.ContainsKey(typeof(NsMuclucNgansachModel).Name))
            {
                _formatDictionary = _sessionService.Current.AutoGenerateDataSetting[typeof(NsMuclucNgansachModel).Name];
            }
            else
            {
                _sessionService.Current.AutoGenerateDataSetting[typeof(NsMuclucNgansachModel).Name] = _formatDictionary;
            }
        }

        private string getTypeOfMlns(NsMuclucNgansachModel nsMuclucNgansachModel)
        {
            foreach (string type in mlnsType)
            {
                PropertyInfo propertyInfo = typeof(NsMuclucNgansachModel).GetProperty(type);
                object val = propertyInfo.GetValue(nsMuclucNgansachModel, null);
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
            NsMuclucNgansachModel nsMuclucNgansachModel = eventArgs.Parameter as NsMuclucNgansachModel;
            if (nsMuclucNgansachModel == null)
            {
                return;
            }
            if (SelectedItem.XNM.Equals(nsMuclucNgansachModel.XNM) || !SelectedItem.XNM.StartsWith(nsMuclucNgansachModel.XNM))
            {
                MessageBoxHelper.Warning("MLNS không hợp lệ");
                return;
            }
            nsMuclucNgansachModel.BHangCha = true;
            SelectedItem.MlnsParentName = nsMuclucNgansachModel.XNM;
            SelectedItem.MlnsIdParent = nsMuclucNgansachModel.MlnsId;
        }

        protected override void OnAdd(object obj)
        {
            try
            {
                DataGrid dgdData = FindDatagrid();
                this.CancelEditData(dgdData);

                int currentRow = Items.Count - 1;
                NsMuclucNgansachModel newRow;
                if (SelectedItem == null)
                {
                    newRow = new NsMuclucNgansachModel();
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
                newRow.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
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
            List<string> mlnsTypes = new List<string>
            {
                "LNS", "L", "K", "M", "TM", "TTM", "NG", "TNG", "TNG1", "TNG2", "TNG3"
            };
            if (mlnsChiTietToi != null)
            {
                string mlnsType = getTypeOfMlns(SelectedItem);
                int indexMLnsChiTietToi = mlnsTypes.IndexOf(mlnsChiTietToi.SGiaTri);
                int indexOfMlnsColumn = mlnsTypes.IndexOf(mlnsType);
                if (indexMLnsChiTietToi <= indexOfMlnsColumn)
                {
                    MessageBoxHelper.Info("Cấu hình chi tiết tới là " + mlnsChiTietToi.SGiaTri + ", không thể thêm mới dòng con cho mlns " + SelectedItem.XNM);
                    return;
                }
            }
            DataGrid dgdData = FindDatagrid();
            this.CancelEditData(dgdData);

            int currentRow = Items.IndexOf(SelectedItem);
            NsMuclucNgansachModel parent = SelectedItem;
            NsMuclucNgansachModel newRow = ObjectCopier.Clone(SelectedItem);
            newRow.Id = Guid.Empty;
            newRow.MlnsId = Guid.NewGuid();
            newRow.MlnsIdParent = parent.MlnsId;
            newRow.IsModified = true;
            newRow.BHangCha = false;
            parent.BHangCha = true;
            newRow.BHangChaDuToan = null;
            newRow.BHangChaQuyetToan = null;
            newRow.MlnsParentName = parent.XNM;
            newRow.IsEditableStatus = true;
            // nếu dòng mới là ng,tng,tng1,2,3 thì cần update bhangchadutoan và bhangcha quyết toán
            var parentRowType = getTypeOfMlns(parent);
            if (mlnsTypes.IndexOf(parentRowType) > -1)
            {
                // find ng parent
                var ngParent = Items.FirstOrDefault(i => "NG".Equals(getTypeOfMlns(i)) && newRow.XNM.Contains(i.XNM) &&
                    (!string.IsNullOrWhiteSpace(i.SDuToanChiTietToi) || !string.IsNullOrWhiteSpace(i.SQuyetToanChiTietToi)));
                if (ngParent != null)
                {
                    ngParent.IsModified = true;
                }
            }
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
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
                IMucLucNganSachService mucLucNganSachService = _service as IMucLucNganSachService;
                if (mucLucNganSachService.IsUsedMLNS(SelectedItem.MlnsId, _authenticationInfo.YearOfWork))
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
                        var dataToSave = _mapper.Map<IEnumerable<NsMucLucNganSach>>(Items.Where(i => i.IsModified || i.IsDeleted));
                        foreach (var i in dataToSave)
                        {
                            if (i.IsDeleted)
                                i.IsModified = false;
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
                FilterModel = new NsMuclucNgansachModel();
                _currentCodeValDictionary = new Dictionary<string, int>();
                Items.Clear();
                Items = new ObservableCollection<NsMuclucNgansachModel>();
                Items = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(rawData);
                foreach (var item in Items)
                {
                    string typeMLNS = getTypeOfMlns(item);
                    item.IsEnableDuToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedDuToanChiTietToi;
                    item.IsEnableQuyetToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedQuyetToanChiTietToi;
                    item.PropertyChanged += Item_PropertyChanged;
                    item.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
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
                if (dgdData?.Items.Count > 0)
                {
                    var cell = new DataGridCellInfo(dgdData.Items[0], dgdData.Columns[0]);
                    SelectedItem = (NsMuclucNgansachModel)dgdData.Items[0];
                    dgdData.CurrentCell = cell;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(NsMuclucNgansachModel.MlnsParentName)))
            {
                MucLucNganSachViewModel mucLucNganSachViewModel = _serviceProvider.GetService(typeof(MucLucNganSachViewModel)) as MucLucNganSachViewModel;
                mucLucNganSachViewModel.InitBaseData();
                mucLucNganSachViewModel.Items = new ObservableCollection<NsMuclucNgansachModel>(Items.Where(t => !t.IsDeleted));
                mucLucNganSachViewModel._isFirstLoad = true;
                mucLucNganSachViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucNganSachViewModel.Items);
                mucLucNganSachViewModel._dataCollectionView.Filter = mucLucNganSachViewModel.ItemsViewFilter;
                mucLucNganSachViewModel._isFirstLoad = false;
                mucLucNganSachViewModel.NsMuclucNgansachModel = SelectedItem;
                MucLucNganSachView = new MucLucNganSachView()
                {
                    DataContext = mucLucNganSachViewModel
                };
                mucLucNganSachViewModel.MucLucNganSachView = MucLucNganSachView;
                var dialog = DialogHost.Show(MucLucNganSachView, "RootDialog", ClosingEventHandler);
                mucLucNganSachViewModel.IsDialog = true;
                mucLucNganSachViewModel.Name = "Chọn MLNS cha cho mlns " + SelectedItem.XNM;
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
                    SelectedItem = (NsMuclucNgansachModel)d.Items[0];
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
                        SelectedItem = (NsMuclucNgansachModel)d.Items[0];
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
            var dataToSave = Items.Where(i => i.IsModified && !i.IsDeleted && !string.IsNullOrEmpty(i.NG));
            foreach (NsMuclucNgansachModel model in dataToSave)
            {
                int selectedSDuToanIndex = mlnsType.IndexOf(model.SDuToanChiTietToi);
                int selectedSQTIndex = mlnsType.IndexOf(model.SQuyetToanChiTietToi);
                if (selectedSQTIndex > selectedSDuToanIndex)
                {
                    MessageBoxHelper.Error(Resources.MLNS_DT_QT_Err);
                    return false;
                }
            }
            return true;
        }

        private void BeforeSave()
        {
            // get saved ng type
            List<NsMuclucNgansachModel> Parent = new List<NsMuclucNgansachModel>();
            var dataToSave = Items.Where(i => i.IsModified && !i.IsDeleted).ToList();
            foreach (NsMuclucNgansachModel model in dataToSave)
            {
                string typeMLNS = getTypeOfMlns(model);
                if ("NG".Equals(typeMLNS) &&
                (!string.IsNullOrWhiteSpace(model.SDuToanChiTietToi) || !string.IsNullOrWhiteSpace(model.SQuyetToanChiTietToi)))
                {
                    List<NsMuclucNgansachModel> children = Items.Where(t => t.XNM.Contains(model.XNM + "-")).ToList();
                    // update bhangcha du toan vaf bhangcha quyet toan
                    int selectedSDuToanIndex = mlnsType.IndexOf(model.SDuToanChiTietToi);
                    int selectedSQTIndex = mlnsType.IndexOf(model.SQuyetToanChiTietToi);
                    string modelNsType = getTypeOfMlns(model);
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaDuToan(selectedSDuToanIndex, modelNsTypeIndex, model);
                    UpdateBHangChaQuyetToan(selectedSQTIndex, modelNsTypeIndex, model);
                    foreach (NsMuclucNgansachModel ns in children)
                    {
                        ns.IsModified = true;
                        string nsType = getTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
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

        private void OnPropertyChanged(NsMuclucNgansachModel model)
        {
            model.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
        }

        private void CustomValueProps(NsMuclucNgansachModel newRow, NsMuclucNgansachModel currentRow)
        {
            newRow.Id = Guid.Empty;
            newRow.IsModified = true;
            newRow.MlnsId = Guid.NewGuid();
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

        private void NsMuclucNgansachModel_OnDeleteMLNS(NsMuclucNgansachModel model, IEnumerable<NsMuclucNgansachModel> models)
        {
            NsMuclucNgansachModel parent = models.FirstOrDefault(i => i.MlnsId == model.MlnsIdParent);
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.MlnsIdParent == parent.MlnsId && !i.IsDeleted);
            parent.BHangCha = hasChild;
            foreach (var item in Items.Where(t => t.MlnsIdParent.Equals(SelectedItem.MlnsId)))
            {
                item.MlnsParentName = string.Empty;
                item.MlnsIdParent = null;
            }
        }

        private void UpdateBHangChaDuToan(int selectedSDuToanIndex, int nsTypeIndex, NsMuclucNgansachModel ns)
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

        private void UpdateBHangChaQuyetToan(int selectedSQTIndex, int nsTypeIndex, NsMuclucNgansachModel ns)
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
            var item = (NsMuclucNgansachModel)obj;
            result = FilterFunction(FilterModel, item);
            if (!result && item.BHangCha)
            {
                result = xnmConcatenation.StartsWith(item.XNM) || xnmConcatenation.Contains(";" + item.XNM);
            }
            return result;
        }

        private bool FilterFunction(NsMuclucNgansachModel filterModel, NsMuclucNgansachModel item)
        {
            var result = true;
            if (!string.IsNullOrEmpty(filterModel.Lns))
                result = result && item.Lns.ToLower().StartsWith(filterModel.Lns.ToLower());
            if (!string.IsNullOrEmpty(filterModel.L))
                result = result && item.L.ToLower().StartsWith(filterModel.L.ToLower());
            if (!string.IsNullOrEmpty(filterModel.K))
                result = result && item.K.ToLower().StartsWith(filterModel.K.ToLower());
            if (!string.IsNullOrEmpty(filterModel.M))
                result = result && item.M.ToLower().StartsWith(filterModel.M.ToLower());
            if (!string.IsNullOrEmpty(filterModel.TM))
                result = result && item.TM.ToLower().StartsWith(filterModel.TM.ToLower());
            if (!string.IsNullOrEmpty(filterModel.TTM))
                result = result && item.TTM.ToLower().StartsWith(filterModel.TTM.ToLower());
            if (!string.IsNullOrEmpty(filterModel.NG))
                result = result && item.NG.ToLower().StartsWith(filterModel.NG.ToLower());
            if (!string.IsNullOrEmpty(filterModel.TNG))
                result = result && item.TNG.ToLower().StartsWith(filterModel.TNG.ToLower());
            if (!string.IsNullOrEmpty(filterModel.TNG1))
                result = result && item.TNG1.ToLower().StartsWith(filterModel.TNG1.ToLower());
            if (!string.IsNullOrEmpty(filterModel.TNG2))
                result = result && item.TNG2.ToLower().StartsWith(filterModel.TNG2.ToLower());
            if (!string.IsNullOrEmpty(filterModel.TNG3))
                result = result && item.TNG3.ToLower().StartsWith(filterModel.TNG3.ToLower());
            if (!string.IsNullOrEmpty(filterModel.MoTa))
                result = result && item.MoTa.ToLower().Contains(filterModel.MoTa.ToLower());
            if (!string.IsNullOrEmpty(filterModel.MlnsParentName))
                result = result && item.MlnsParentName != null && item.MlnsParentName.Contains(filterModel.MlnsParentName);
            return result;
        }

        private void SelectAll(bool select, IEnumerable<NsMuclucNgansachModel> models)
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
                if (SelectedItem != null)
                {
                    if (SelectedItem.XNM.Equals(NsMuclucNgansachModel.XNM) || !NsMuclucNgansachModel.XNM.StartsWith(SelectedItem.XNM))
                    {
                        MessageBoxHelper.Warning("MLNS không hợp lệ");
                        return;
                    }
                }
                DialogHost.CloseDialogCommand.Execute(SelectedItem, null);
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
            FilterModel = new NsMuclucNgansachModel();
            _currentCodeValDictionary = new Dictionary<string, int>();
            Items = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(Items.Where(t => !t.IsDeleted).OrderBy(s => s.XNM));
            foreach (var item in Items)
            {
                string typeMLNS = getTypeOfMlns(item);
                item.IsEnableDuToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedDuToanChiTietToi;
                item.IsEnableQuyetToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedQuyetToanChiTietToi;
                item.IsModified = false;
                item.IsDeleted = false;
                item.PropertyChanged += Item_PropertyChanged;
                item.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
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
                SelectedItem = (NsMuclucNgansachModel)dgdData.Items[0];
                dgdData.CurrentCell = cell;
            }
        }

        private void OnExportExcel()
        {
            MlnsExportViewModel mlnsExportViewModel = new MlnsExportViewModel(_service)
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
            var dialog = DialogHost.Show(Dialog, "RootDialog", ClosingEventHandler);
        }

        private void OnExportJsonFile()
        {
            _exportService.ExportJsonFile(ExportJsonFileName, Items);
        }

        private void OnImportExcel()
        {
            ImportExcelViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService, MLNSImportModel> importExcelViewModel =
                new ImportExcelViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService, MLNSImportModel>(_serviceProvider, _service, _authenticationInfo);
            importExcelViewModel.DataTemplateFileName = DataTemplateFileName;
            ImportExcelView importExcelView = new ImportExcelView()
            {
                DataContext = importExcelViewModel
            };
            importExcelViewModel.Init();
            importExcelView.Show();
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

        private void OnViewEditDetail()
        {
            if (SelectedItem == null || SelectedItem.BHangCha)
            {
                return;
            }
            var description = (SelectedItem?.DetailInfoModalTitle);
            GenericControlCustomEditDetailViewModel genericControlCustomEditDetailViewModel = new GenericControlCustomEditDetailViewModel(SelectedItem)
            {
                Title = Title,
                Description = description.Replace("Chi tiết ", "Điều chỉnh ")
            };
            genericControlCustomEditDetailViewModel.ColumnWidth = 260;
            genericControlCustomEditDetailViewModel._service = _service;
            genericControlCustomEditDetailViewModel._logger = _logger;
            GenericControlCustomViewEditDetail genericControlCustomViewEditDetail = new GenericControlCustomViewEditDetail()
            {
                DataContext = genericControlCustomEditDetailViewModel
            };
            var dialog = DialogHost.Show(genericControlCustomViewEditDetail, "RootDialog");
        }

        private void OnSelectMlnsParent()
        {
            if (Items.Any(t => t.IsSelected))
            {
                MucLucNganSachViewModel mucLucNganSachViewModel = _serviceProvider.GetService(typeof(MucLucNganSachViewModel)) as MucLucNganSachViewModel;
                mucLucNganSachViewModel.InitBaseData();
                mucLucNganSachViewModel.Items = new ObservableCollection<NsMuclucNgansachModel>(Items.Where(t => !t.IsDeleted));
                mucLucNganSachViewModel._isFirstLoad = true;
                mucLucNganSachViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucNganSachViewModel.Items);
                mucLucNganSachViewModel._dataCollectionView.Filter = mucLucNganSachViewModel.ItemsViewFilter;
                mucLucNganSachViewModel._isFirstLoad = false;
                MucLucNganSachView = new MucLucNganSachView()
                {
                    DataContext = mucLucNganSachViewModel
                };
                mucLucNganSachViewModel.MucLucNganSachView = MucLucNganSachView;
                DialogClosingEventHandler closeEvent = (sender, eventArgs) =>
                {
                    if (eventArgs.Parameter != null)
                    {
                        NsMuclucNgansachModel nsMuclucNgansachModel = eventArgs.Parameter as NsMuclucNgansachModel;
                        foreach (var item in Items.Where(t => t.IsSelected))
                        {
                            item.MlnsParentName = nsMuclucNgansachModel.XNM;
                            item.MlnsIdParent = nsMuclucNgansachModel.MlnsId;
                        }
                        nsMuclucNgansachModel.BHangCha = true;
                    }
                };
                var dialog = DialogHost.Show(MucLucNganSachView, "RootDialog", closeEvent);
                mucLucNganSachViewModel.IsDialog = true;
            }
        }

        private PopupBox FindPopupByName(string elementName)
        {
            PopupBox popupBox = FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
            if (IsDialog)
            {
                return FnCommonUtils.FindChild<PopupBox>(MucLucNganSachView, elementName);
            }
            else
                return FnCommonUtils.FindChild<PopupBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), elementName);
        }

        private DataGrid FindDatagrid()
        {
            if (IsDialog)
            {
                return FnCommonUtils.FindChild<DataGrid>(MucLucNganSachView, ModelName);
            }
            else
                return FnCommonUtils.FindChild<DataGrid>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), ModelName);
        }
    }
}
