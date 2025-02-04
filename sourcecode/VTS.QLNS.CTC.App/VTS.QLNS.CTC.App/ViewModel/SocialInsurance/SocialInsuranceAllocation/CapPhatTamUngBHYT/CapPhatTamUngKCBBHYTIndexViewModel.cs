using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.ImportCptuBHYT;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.ExportReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.ImportCptuBHYT;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation
{
    public class CapPhatTamUngKCBBHYTIndexViewModel : GridViewModelBase<BhCptuBHYTModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private INsDonViService _nsDonViService;
        private INsNguoiDungDonViService _nsNguoiDungDonViService;
        private IDanhMucService _danhMucService;
        private IExportService _exportService;
        private ICptuBHYTService _cptuBHYTService;
        private ICptuBHYTChiTietService _cptuBHYTChiTietService;
        private IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private ICptuBHYTChiTietRepository _cptuChiTietRepository;

        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private bool _isCapPhatToanDonVi;
        private ICollectionView _bhChungTuModelsView;
        public CapPhatTamUngKCBBHYTDetail view;

        public override string FuncCode => NSFunctionCode.BUDGET_ALLOCATION;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Cấp tạm ứng KP KCB BHYT";
        public override Type ContentType => typeof(CapPhatTamUngKCBBHYTIndex);
        public override PackIconKind IconKind => PackIconKind.ViewList;
        public override string Title => "Danh sách cấp tạm ứng KP KCB BHYT";
        public override string Description => "Danh sách cấp tạm ứng KP KCB BHYT";

        private ObservableCollection<BhCptuBHYTModel> _dataBhcptuBHYT;
        public ObservableCollection<BhCptuBHYTModel> DataBhcptuBHYT
        {
            get => _dataBhcptuBHYT;
            set => SetProperty(ref _dataBhcptuBHYT, value);
        }

        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
                LoadData();
            }

        }
        public bool IsEnableButtonDataShow => TabIndex == ImportTabIndex.Data;

        public bool IsExportDataFilter => _selectedChungTu != null;

        private BhCptuBHYTModel _selectedChungTu;
        public BhCptuBHYTModel SelectedChungTu
        {
            get => _selectedChungTu;
            set
            {
                SetProperty(ref _selectedChungTu, value);
                if (_selectedChungTu != null)
                {
                    IsLock = _selectedChungTu.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedChungTu == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportDataFilter));
                OnPropertyChanged(nameof(IsEnableButtonExport));
            }
        }

        private ComboboxItem _selectedLoaiKinhPhi;
        public ComboboxItem SelectedLoaiKinhPhi
        {
            get => _selectedLoaiKinhPhi;
            set
            {
                SetProperty(ref _selectedLoaiKinhPhi, value);
                if (_selectedLoaiKinhPhi != null)
                {
                    this.LoadData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiKinhPhi;
        public ObservableCollection<ComboboxItem> ItemsLoaiKinhPhi
        {
            get => _itemsLoaiKinhPhi;
            set => SetProperty(ref _itemsLoaiKinhPhi, value);
        }

        private List<BhCptuBHYTModel> _lstChungTuOrigin;
        public List<BhCptuBHYTModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsQuarter = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsQuarter
        {
            get => _itemsQuarter;
            set => SetProperty(ref _itemsQuarter, value);
        }

        private ComboboxItem _quarterSelected;

        public ComboboxItem QuarterSelected
        {
            get => _quarterSelected;
            set
            {
                SetProperty(ref _quarterSelected, value);
                if (_quarterSelected != null)
                {
                    LoadData();
                }
            }
        }

        public bool IsCensorship
        {
            get
            {
                var itemSelected = Items.Where(x => x.Selected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BIsKhoa);
            }
        }

        private ObservableCollection<BhCptuBHYTModel> _dataBhcptuBHYTSummary;
        public ObservableCollection<BhCptuBHYTModel> DataBhcptuBHYTSummary
        {
            get => _dataBhcptuBHYTSummary;
            set => SetProperty(ref _dataBhcptuBHYTSummary, value);
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEdit));
                //OnPropertyChanged(nameof(IsEnableLock));
                OnPropertyChanged(nameof(IsEnableButtonSummary));
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
            }
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set
            {
                SetProperty(ref _isCollapse, value);
                ExpandChild();
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
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsEnableButtonExport));
                    OnPropertyChanged(nameof(IsEnableButtonSummary));
                }
            }
        }

        public bool IsEnableButtonExport => Items != null && Items.Any(n => n.Selected);

        private static void SelectAll(bool select, IEnumerable<BhCptuBHYTModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.Selected = select;
            }
        }

        private bool _isLock;

        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
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
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    var lstSelectedKhoa = lstSelected.Where(x => x.BIsKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BIsKhoa).ToList();
                    if (lstSelectedKhoa.Any() && lstSelectedMo.Any())
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Any())
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Any())
                    {
                        IsLock = false;
                        result = true;
                    }

                }
                return result;

            }
        }

        private ImportCapPhatTamUngBHYT _importCapPhatTamUngBHYT;
        public bool IsEnableButtonSummary => !_isCapPhatToanDonVi && DataBhcptuBHYT != null && DataBhcptuBHYT.Where(n => n.Selected && n.BIsKhoa).Count() > 0;
        public bool IsVisibleSummaryTab => !_isCapPhatToanDonVi && _sessionService.Current.IsQuanLyDonViCha;
        public bool IsVisibleColumnStatus => !_isCapPhatToanDonVi && !_sessionService.Current.IsQuanLyDonViCha;

        public bool IsCreate;

        public RelayCommand DeleteCommand { get; }
        public RelayCommand RefeshCommand { get; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand ShowPopupAddCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand SummaryProcessCommand { get; set; }
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand UploadFileCommand { get; set; }
        public RelayCommand DownloadTemplateImportCommand { get; set; }

        public CapPhatTamUngKCBBHYTDialogViewModel CapPhatTamUngKCBBHYTDialogViewModel { get; set; }
        public CapPhatTamUngKCBBHYTDetailViewModel CapPhatTamUngKCBBHYTDetailViewModel { get; set; }
        public CapPhatTamUngKCBBHYTSummaryViewModel CapPhatTamUngKCBBHYTSummaryViewModel { get; set; }
        public PrintCapPhatTamUngKCBBHYTViewModel PrintCapPhatTamUngKCBBHYTViewModel { get; set; }
        public ExportCapPhatTamUngKCBBHYTViewModel ExportCapPhatTamUngKCBBHYTViewModel { get; set; }
        public ImportCapPhatTamUngBHYTViewModel ImportCapPhatTamUngBHYTViewModel { get; set; }

        public CapPhatTamUngKCBBHYTIndexViewModel(ICpChungTuService cpChungTuService,
            IMapper mapper,
            ISessionService sessionService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            ICptuBHYTService cptuBHYTService,
            ICptuBHYTChiTietService cptuBHYTChiTietService,
            IExportService exportService,
            ILog logger,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            ICptuBHYTChiTietRepository cptuBHYTChiTietRepository,
            CapPhatTamUngKCBBHYTDialogViewModel capPhatTamUngKCBBHYTDialogViewModel,
            CapPhatTamUngKCBBHYTDetailViewModel capPhatTamUngKCBBHYTDetailViewModel,
            CapPhatTamUngKCBBHYTSummaryViewModel capPhatTamUngKCBBHYTSummaryViewModel,
            PrintCapPhatTamUngKCBBHYTViewModel printCapPhatTamUngKCBBHYTViewModel,
            ExportCapPhatTamUngKCBBHYTViewModel exportCapPhatTamUngKCBBHYTViewModel,
            ImportCapPhatTamUngBHYTViewModel importCapPhatTamUngBHYTViewModel
           )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _cptuBHYTService = cptuBHYTService;
            _cptuBHYTChiTietService = cptuBHYTChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _cptuChiTietRepository = cptuBHYTChiTietRepository;

            CapPhatTamUngKCBBHYTDialogViewModel = capPhatTamUngKCBBHYTDialogViewModel;
            CapPhatTamUngKCBBHYTDialogViewModel.ParentPage = this;

            CapPhatTamUngKCBBHYTDetailViewModel = capPhatTamUngKCBBHYTDetailViewModel;
            CapPhatTamUngKCBBHYTDetailViewModel.ParentPage = this;

            CapPhatTamUngKCBBHYTSummaryViewModel = capPhatTamUngKCBBHYTSummaryViewModel;
            CapPhatTamUngKCBBHYTSummaryViewModel.ParentPage = this;

            PrintCapPhatTamUngKCBBHYTViewModel = printCapPhatTamUngKCBBHYTViewModel;
            PrintCapPhatTamUngKCBBHYTViewModel.ParentPage = this;

            ExportCapPhatTamUngKCBBHYTViewModel = exportCapPhatTamUngKCBBHYTViewModel;
            ExportCapPhatTamUngKCBBHYTViewModel.ParentPage = this;

            ImportCapPhatTamUngBHYTViewModel = importCapPhatTamUngBHYTViewModel;
            ImportCapPhatTamUngBHYTViewModel.ParentPage = this;

            SelectionDoubleClickCommand = new RelayCommand(o => DoubleClickCommand((BhCptuBHYTModel)o));
            ShowPopupAddCommand = new RelayCommand(o => OnShowPopupAdd());
            LockUnLockCommand = new RelayCommand(o => OnLock());
            DeleteCommand = new RelayCommand(o => OnDelete());
            RefeshCommand = new RelayCommand(o => OnRefresh());
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            ExportDataCommand = new RelayCommand(obj => OnExportDataDialog());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            SummaryProcessCommand = new RelayCommand(obj => ConfirmAggregate());
            UploadFileCommand = new RelayCommand(obj => OnUpload());
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            DownloadTemplateImportCommand = new RelayCommand(obj => DownloadTemplateImport());
        }

        private void DownloadTemplateImport()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName = Path.Combine(ExportPrefix.PATH_BH_CPTU, ExportFileName.IMPORT_BH_CPTU_CHUNGTU_CHITIET);
                var namLamViec = _sessionService.Current.YearOfWork;

                List<BhDmCoSoYTe> listCsYTe = new List<BhDmCoSoYTe>();
                var predicate_csyte = PredicateBuilder.True<BhDmCoSoYTe>();
                predicate_csyte = predicate_csyte.And(x => x.INamLamViec == namLamViec && x.ITrangThai == StatusType.ACTIVE);
                listCsYTe = _bhDmCoSoYTeService.FindByCondition(predicate_csyte).ToList();
                List<BhCptuBHYTChiTietModel> lstData = new List<BhCptuBHYTChiTietModel>();
                var itemsExport = Items.Where(x => x.Selected);

                var data = new Dictionary<string, object>();
                data.Add("Items", lstData);
                data.Add("MLNS", listCsYTe);

                List<int> hideColumns = new List<int>();

                var xlsFile = _exportService.Export<BhCptuBHYTChiTietModel, BhDmCoSoYTe>(templateFileName, data, hideColumns);
                var nameRange = xlsFile.GetNamedRange(1);
                nameRange.Comment = "Workbook";
                xlsFile.SetNamedRange(nameRange);
                xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                xlsFile.SetCellValue(50, 50, "CheckSum");
                xlsFile.SetRowHidden(50, true);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName("KeHoachCapTamUng_Template_Import");
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

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                TabIndex = ImportTabIndex.Data;
                LoadLockStatus();
                LoadQuarter();
                OnPropertyChanged(nameof(TabIndex));
                LoadSettingCapPhat();
                LoadDanhMucLoaiChi();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadQuarter()
        {
            var lstQuarter = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "--Tất cả--", ValueItem = "-1"},
                new ComboboxItem {DisplayItem = "Quý I", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Quý II", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Quý III", ValueItem = "3"},
                new ComboboxItem {DisplayItem = "Quý IV", ValueItem = "4"}
            };

            ItemsQuarter = new ObservableCollection<ComboboxItem>(lstQuarter);
            QuarterSelected = ItemsQuarter.ElementAt(0);
        }
        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }

        #region Load danh muc loai chi
        private void LoadDanhMucLoaiChi()
        {
            ObservableCollection<ComboboxItem> lstKinhPhi = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN,
                    HiddenValue = LNSValue.LNS_9040001
                },
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD,
                    HiddenValue = LNSValue.LNS_9040002
                },

            };
            ItemsLoaiKinhPhi = lstKinhPhi;
            ItemsLoaiKinhPhi.Insert(0, new ComboboxItem
            {
                DisplayItem = "--Tất cả--",
                ValueItem = "-1",
                HiddenValue = "-1",
            });

            SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.First();
            OnPropertyChanged(nameof(ItemsLoaiKinhPhi));
        }
        #endregion


        private async void OnShowPopupSummary()
        {
            try
            {
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                    return;
                }

                //kiểm tra trạng thái các bản ghi
                if (DataBhcptuBHYTSummary.Any(x => x.Selected && !x.BIsKhoa))
                {
                    MessageBoxHelper.Info(Resources.AlertAggregateUnLocked);
                    return;
                }

                if (DataBhcptuBHYTSummary.Where(x => x.Selected).Select(x => x.IQuy).Distinct().Count() > 1)
                {
                    MessageBoxHelper.Info(Resources.AlertKhacLoaiCapPhat);
                    return;
                }

                CapPhatTamUngKCBBHYTSummaryViewModel.BhcptuBHYT = new BhCptuBHYTModel();
                CapPhatTamUngKCBBHYTSummaryViewModel.DataBhcptuBHYT = new ObservableCollection<BhCptuBHYTModel>(DataBhcptuBHYT.Where(n => n.Selected && n.BIsKhoa).ToList());
                CapPhatTamUngKCBBHYTSummaryViewModel.IsEditProcess = false;
                CapPhatTamUngKCBBHYTSummaryViewModel.Init();
                CapPhatTamUngKCBBHYTSummaryViewModel.SavedAction = obj =>
                {
                    this.TabIndex = ImportTabIndex.MLNS;
                    this.OnRefresh();
                };
                var view = new CapPhatTamUngKCBBHYTSummary
                {
                    DataContext = CapPhatTamUngKCBBHYTSummaryViewModel
                };
                var result = await DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupAdd()
        {
            try
            {
                if (_isCapPhatToanDonVi && !_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRoleAddAllocation);
                    return;
                }
                CapPhatTamUngKCBBHYTDialogViewModel.CpctBHYTModel = new BhCptuBHYTModel();
                CapPhatTamUngKCBBHYTDialogViewModel.IsEditProcess = false;
                CapPhatTamUngKCBBHYTDialogViewModel.Init();
                IsCreate = true;
                CapPhatTamUngKCBBHYTDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnShowDetailAllocation((BhCptuBHYTModel)obj);
                };
                var view = new CapPhatTamUngKCBBHYTDialog
                {
                    DataContext = CapPhatTamUngKCBBHYTDialogViewModel
                };

                DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLock()
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.Selected && (bool)n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                    return;
                }
                string lstSoChungTuDaTongHop = string.Join(", ", Items.Where(n => n.Selected && n.BIsTongHop && (bool)n.BIsKhoa && !n.IIDMaDonVi.Equals(_sessionInfo.IdDonVi)).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(lstSoChungTuDaTongHop))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertUnlockAggregatedVoucher, lstSoChungTuDaTongHop));
                    return;
                }
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !(bool)n.BIsKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(lstSoChungTuInvalid))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, lstSoChungTuInvalid));
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockMultiVoucher();
                MessageBoxHelper.Info(msgDone);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
            foreach (var ct in lstSelected)
            {
                _cptuBHYTService.LockOrUnlock(ct.Id, isLock);
                var chungTu = Items.First(x => x.Id == ct.Id);
                chungTu.BIsKhoa = !ct.BIsKhoa;
            }
            OnRefresh();
        }

        private void OnLockHandler(BhCptuBHYTModel obj, string msgDone)
        {
            _cptuBHYTService.LockOrUnLock(obj.Id, !obj.BIsKhoa);
        }

        protected override void OnDelete()
        {
            if (SelectedChungTu == null) return;
            if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedChungTu.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedChungTu.SSoChungTu, SelectedChungTu.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnDeleteHandler(NSDialogResult result)
        {
            BhCpTUChungTuChiTietCriteria searchCondition = new BhCpTUChungTuChiTietCriteria();
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedChungTu != null)
            {
                var chungTu = _cptuBHYTService.FindById(SelectedChungTu.Id);
                searchCondition.IIDCTCapPhatTU = chungTu.Id;
                if (chungTu != null)
                {
                    var lstChungTuChiTiet = _cptuBHYTChiTietService.FindChungTuChiTietByChungTuId(searchCondition).ToList();
                    //Xóa chứng từ
                    _cptuBHYTService.Delete(chungTu);

                    if (!string.IsNullOrEmpty(chungTu.SDSSoChungTuTongHop))
                    {
                        var lstSoCtChild = chungTu.SDSSoChungTuTongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _cptuBHYTService.FindChungTuDaTongHopBySCT(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.BIsTongHop = false;
                                _cptuBHYTService.Update(ctChild);
                            }
                        }
                    }
                    //Xóa chi tiết chứng từ
                    _cptuBHYTChiTietService.RemoveRange(lstChungTuChiTiet);
                    OnRefresh();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        public void DeleteChungTuChiTiet(Guid idChungTu)
        {
            var predicate = PredicateBuilder.True<BhCptuBHYTChiTiet>();
            predicate = predicate.And(x => x.IID_BH_CP_CapTamUng_KCB_BHYT == idChungTu);
            List<BhCptuBHYTChiTiet> list = _cptuBHYTChiTietService.FindByCondition(predicate).ToList();
            if (list != null && list.Count > 0)
            {
                _cptuBHYTChiTietService.RemoveRange(list);
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        public void DoubleClickCommand(BhCptuBHYTModel bhCptuBHYTDetail)
        {
            IsCreate = false;
            OnShowDetailAllocation(bhCptuBHYTDetail);
        }
        public void OnShowDetailAllocation(BhCptuBHYTModel bhCptuBHYTDetail)
        {
            try
            {
                if (bhCptuBHYTDetail == null)
                    return;
                CapPhatTamUngKCBBHYTDetailViewModel.Model = bhCptuBHYTDetail;
                CapPhatTamUngKCBBHYTDetailViewModel.Init();
                CapPhatTamUngKCBBHYTDetailViewModel.IsCreate = IsCreate;
                CapPhatTamUngKCBBHYTDetailViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                view = new CapPhatTamUngKCBBHYTDetail
                {
                    DataContext = CapPhatTamUngKCBBHYTDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            try
            {
                view.Close();
                OnRefresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadData()
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                int yearOfBudget = _sessionInfo.YearOfBudget;
                int budgetSource = _sessionInfo.Budget;
                var currentIdDonVi = _sessionInfo.IdDonVi;

                var listChungTu = _cptuBHYTService.FindByYear(_sessionInfo.YearOfWork);
                _lstChungTuOrigin = _mapper.Map<List<BhCptuBHYTModel>>(listChungTu);

                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == ImportTabIndex.Data)
                    {
                        Items = _mapper.Map<ObservableCollection<BhCptuBHYTModel>>(listChungTu.Where(x => x.SDSSoChungTuTongHop == null && !x.BIsTongHop).OrderBy(x => x.ILoaiKinhPhi).ThenBy(x => x.IQuy));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.SDSSoChungTuTongHop != null).ToList();
                        var listTongHop = new List<BhCptuBHYTModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhCptuBHYTModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);

                            if (!string.IsNullOrEmpty(ctTongHop.SDSSoChungTuTongHop))
                            {
                                var listChild = _mapper.Map<List<BhCptuBHYTModel>>(listChungTu.Where(x => ctTongHop.SDSSoChungTuTongHop != null && ctTongHop.SDSSoChungTuTongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SSoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }

                        Items = _mapper.Map<ObservableCollection<BhCptuBHYTModel>>(listTongHop.OrderBy(x => x.ILoaiKinhPhi).ThenBy(x => x.IQuy));
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhCptuBHYTModel>>(listChungTu.Where(x => x.SDSSoChungTuTongHop == null && !x.BIsTongHop).OrderBy(x => x.ILoaiKinhPhi).ThenBy(x => x.IQuy));
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhCptuBHYTModel.Selected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsEnableButtonExport));
                        }
                        if (args.PropertyName == nameof(BhCptuBHYTModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }
                _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
                _bhChungTuModelsView.Filter = ChungTuModelsFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ChungTuModelsFilter(object obj)
        {
            if (!(obj is BhCptuBHYTModel temp)) return true;
            var condition1 = true;

            if (SelectedLoaiKinhPhi != null && SelectedLoaiKinhPhi.ValueItem != "-1")
            {
                if (SelectedLoaiKinhPhi.ValueItem == "1")
                {
                    // Do khi lưu dữ liệu với trường hợp "Kinh phí KCB BHYT quân nhân" thì ILoaiKinhPhi có thể lưu giá trị 0 hoặc 1
                    condition1 = condition1 && (temp.ILoaiKinhPhi == 0 || temp.ILoaiKinhPhi == 1);
                }
                else
                {
                    condition1 = condition1 && temp.ILoaiKinhPhi == Convert.ToInt32(SelectedLoaiKinhPhi.ValueItem);
                }
            }

            if (QuarterSelected != null && QuarterSelected.ValueItem != "-1")
            {
                condition1 = condition1 && temp.IQuy == int.Parse(QuarterSelected.ValueItem);
            }

            temp.IsFilter = condition1;
            return condition1;
        }

        protected override void OnAdd()
        {
            CapPhatTamUngKCBBHYTDialogViewModel.Name = "Thêm mới chứng từ";
            CapPhatTamUngKCBBHYTDialogViewModel.Description = "Tạo mới chứng từ";
            CapPhatTamUngKCBBHYTDialogViewModel.CpctBHYTModel = new BhCptuBHYTModel();
            CapPhatTamUngKCBBHYTDialogViewModel.IsEditProcess = false;
            CapPhatTamUngKCBBHYTDialogViewModel.IsSummary = false;
            CapPhatTamUngKCBBHYTDialogViewModel.Init();
            CapPhatTamUngKCBBHYTDialogViewModel.SavedAction = obj =>
            {
                var chungTu = (BhCptuBHYTModel)obj;
                this.LoadData();
                OpenDetailDialog(chungTu);
            };
            var exportView = new CapPhatTamUngKCBBHYTDialog() { DataContext = CapPhatTamUngKCBBHYTDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            if (SelectedChungTu != null)
            {
                if (SelectedChungTu.SDSSoChungTuTongHop != null)
                {
                    OnAggregateEdit();
                }
                else
                {
                    if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedChungTu.SNguoiTao));
                        return;
                    }
                    CapPhatTamUngKCBBHYTDialogViewModel.CpctBHYTModel = SelectedChungTu;
                    CapPhatTamUngKCBBHYTDialogViewModel.IsEditProcess = true;
                    CapPhatTamUngKCBBHYTDialogViewModel.Name = "Sửa chứng từ";
                    CapPhatTamUngKCBBHYTDialogViewModel.Description = "Cập nhật thông tin chứng từ số nhu cầu";
                    CapPhatTamUngKCBBHYTDialogViewModel.IsSummary = false;
                    CapPhatTamUngKCBBHYTDialogViewModel.Init();
                    CapPhatTamUngKCBBHYTDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    CapPhatTamUngKCBBHYTDialogViewModel.ShowDialogHost();
                }
            }
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhCptuBHYTModel> selectedVoucher = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedChungTu.SDSSoChungTuTongHop) && SelectedChungTu.SDSSoChungTuTongHop.Contains(x.SSoChungTu)).ToList();
            CapPhatTamUngKCBBHYTDialogViewModel.IsEditProcess = true;
            CapPhatTamUngKCBBHYTDialogViewModel.Name = "Sửa chứng từ tổng hợp";
            CapPhatTamUngKCBBHYTDialogViewModel.Description = "Sửa chứng từ cấp phát tạm ứng tổng hợp";
            CapPhatTamUngKCBBHYTDialogViewModel.CpctBHYTModel = SelectedChungTu;
            CapPhatTamUngKCBBHYTDialogViewModel.ListIdsChungTuSummary = selectedVoucher;
            CapPhatTamUngKCBBHYTDialogViewModel.IsSummary = true;
            CapPhatTamUngKCBBHYTDialogViewModel.Init();
            CapPhatTamUngKCBBHYTDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
            };
            var addView = new CapPhatTamUngKCBBHYTDialog() { DataContext = CapPhatTamUngKCBBHYTDialogViewModel };
            DialogHost.Show(addView);
        }

        private void OnSelectedChange(object obj)
        {
            SelectedChungTu = (BhCptuBHYTModel)obj;
            if (SelectedChungTu is { BIsKhoa: true } || SelectedChungTu == null)
            {
                IsEdit = false;
            }
            else
            {
                IsEdit = true;
            }
        }

        private void ExpandChild()
        {
            if (Items != null)
            {
                Items.Where(n => n.SSoChungTuParent == SelectedChungTu.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }

        /// <summary>
        /// Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            try
            {
                int dialogType = (int)param;
                switch (dialogType)
                {
                    case (int)CapPhatTamUngPrintType.PRINT_INKEHOACH:
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableKehoach = false;
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableThongTri = true;
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableTongTop = true;
                        PrintCapPhatTamUngKCBBHYTViewModel.Init();
                        var view1 = new PrintCapPhatTamUngKCBBHYT
                        {
                            DataContext = PrintCapPhatTamUngKCBBHYTViewModel
                        };
                        DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)CapPhatTamUngPrintType.PRINT_INTHONGTRITONGHOP:
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableThongTri = true;
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableTongTop = false;
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableKehoach = true;
                        PrintCapPhatTamUngKCBBHYTViewModel.Init();
                        var view2 = new PrintCapPhatTamUngKCBBHYT
                        {
                            DataContext = PrintCapPhatTamUngKCBBHYTViewModel
                        };
                        DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintType.PRINT_ALLOCATION_REQUEST:
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableThongTri = false;
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableTongTop = true;
                        PrintCapPhatTamUngKCBBHYTViewModel.IsEnableKehoach = true;
                        PrintCapPhatTamUngKCBBHYTViewModel.Init();
                        var view3 = new PrintCapPhatTamUngKCBBHYT
                        {
                            DataContext = PrintCapPhatTamUngKCBBHYTViewModel
                        };
                        DialogHost.Show(view3, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                        //case (int)AllocationPrintType.PRINT_ALLOCATION_TYPE:
                        //    PrintAllocationTypeViewModel.Init();
                        //    var view4 = new PrintAllocationType
                        //    {
                        //        DataContext = PrintAllocationTypeViewModel
                        //    };
                        //    DialogHost.Show(view4, SettlementScreen.ROOT_DIALOG, null, null);
                        //    break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(BhCptuBHYTModel.Selected))
            {
                OnPropertyChanged(nameof(IsEnableButtonExport));
                OnPropertyChanged(nameof(IsEnableButtonSummary));
            }

            if (args.PropertyName == nameof(BhCptuBHYTModel.IsCollapse))
            {
                ExpandChild();
            }
        }

        private List<BhCptuBHYTChiTietModel> LoadDataExportDetail(BhCptuBHYTModel item)
        {
            int iQuyKyTruoc = 0;
            int iNamKyTruoc = 0;

            if (item.IQuy == 1)
            {
                iQuyKyTruoc = 4;
                iNamKyTruoc = (_sessionInfo.YearOfWork - 1);
            }
            else
            {
                iQuyKyTruoc = item.IQuy - 1;
                iNamKyTruoc = _sessionInfo.YearOfWork;
            }

            List<BhCptuBHYTChiTietModel> lstResult = new List<BhCptuBHYTChiTietModel>();
            var lstChungTuChiTiet = _cptuChiTietRepository.FinChungTuChiTiet(item.Id, item.SDSLNS, item.SDSID_CoSoYTe, _sessionInfo.YearOfWork, iQuyKyTruoc, iNamKyTruoc, _sessionInfo.Principal).ToList();
            lstResult = _mapper.Map(lstChungTuChiTiet, lstResult).ToList();
            return lstResult;
        }


        private void CalculateData(List<BhCptuBHYTChiTietModel> listData)
        {
            listData.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FQTQuyTruoc = 0;
                    x.FTamUngQuyNay = 0;
                    x.FLuyKeCapDenCuoiQuy = 0;
                });

            foreach (var item in listData.Where(x => !x.BHangCha && (x.FQTQuyTruoc != 0 || x.FTamUngQuyNay != 0 || x.FLuyKeCapDenCuoiQuy != 0)))
            {
                item.FLuyKeCapDenCuoiQuy = item.FluyKeCap;
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<BhCptuBHYTChiTietModel> listData, BhCptuBHYTChiTietModel currentItem, BhCptuBHYTChiTietModel seftItem)
        {
            var parrentItem = listData.FirstOrDefault(x => x.IID_MLNS == currentItem.IID_MLNS_Cha);
            if (parrentItem == null) return;
            parrentItem.FQTQuyTruoc = (parrentItem.FQTQuyTruoc ?? 0) + (seftItem.FQTQuyTruoc ?? 0);
            parrentItem.FTamUngQuyNay = (parrentItem.FTamUngQuyNay ?? 0) + (seftItem.FTamUngQuyNay ?? 0);
            parrentItem.FLuyKeCapDenCuoiQuy = (parrentItem.FLuyKeCapDenCuoiQuy ?? 0) + (seftItem.FLuyKeCapDenCuoiQuy ?? 0);

            CalculateParent(listData, parrentItem, seftItem);
        }

        private void OnExportDataDialog()
        {
            /*ExportCapPhatTamUngKCBBHYTViewModel.Items = Items
            ExportCapPhatTamUngKCBBHYTViewModel.Init()
            var addView = new ExportCapPhatTamUngKCBBHYT() { DataContext = ExportCapPhatTamUngKCBBHYTViewModel }
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null)
            */
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFileName = ExportFileName.RP_BH_EXPORT_CAPPHATTAMUNGKCBBHYT;

                var namLamViec = _sessionService.Current.YearOfWork;

                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);
                var listMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS.StartsWith("904")).OrderBy(x => x.SXauNoiMa).ToList();


                //List<BhDmCoSoYTe> listCsYTe = new List<BhDmCoSoYTe>()
                //var predicate_csyte = PredicateBuilder.True<BhDmCoSoYTe>()
                //predicate_csyte = predicate_csyte.And(x => x.INamLamViec == namLamViec)
                //listCsYTe = _bhDmCoSoYTeService.FindByCondition(predicate_csyte).ToList()

                var itemsExport = Items.Where(x => x.Selected);

                foreach (var item in itemsExport)
                {
                    var dataExportDetail = LoadDataExportDetail(item);

                    List<string> lstCsYTe = item.SDSID_CoSoYTe.Split(",").ToList();
                    List<BhCptuBHYTChiTietModel> lstData = new List<BhCptuBHYTChiTietModel>();
                    foreach (var dv in lstCsYTe)
                    {
                        //var csYTe = listCsYTe.Where(x => x.IIDMaCoSoYTe == dv).FirstOrDefault()
                        var lstDataPrent = dataExportDetail.Where(x => x.BHangCha).ToList();
                        var lstDataChildbyCsYTe = dataExportDetail.Where(x => x.IID_MaCoSoYTe == dv && !x.BHangCha).ToList();

                        lstDataPrent.AddRange(lstDataChildbyCsYTe);
                        var listData = new List<BhCptuBHYTChiTietModel>();
                        listData = _mapper.Map(lstDataPrent, listData);
                        CalculateData(listData);
                        lstData.AddRange(listData.Where(x => (x.FQTQuyTruoc ?? 0) != 0 || (x.FLuyKeCapDenCuoiQuy ?? 0) != 0 || (x.FTamUngQuyNay ?? 0) != 0).OrderBy(x => x.SXauNoiMa).ToList());
                    }
                    var data = new Dictionary<string, object>();
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                    data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                    data.Add("TitleFirst", $"CẤP PHÁT TẠM ỨNG KINH PHÍ BHYT NĂM {_sessionService.Current.YearOfWork}");
                    data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");
                    //data.Add("HeaderTenDonVi", $"Cơ sở y tế: {csYTe?.IIDMaCoSoYTe.PadLeft(3, '0')}{StringUtils.DIVISION}{csYTe?.STenCoSoYTe}")
                    //data.Add("TenDonVi", $"{csYTe?.IIDMaCoSoYTe.PadLeft(3, '0')}{StringUtils.DIVISION}{csYTe?.STenCoSoYTe}")
                    //data.Add("SoChungTu", item.SSoChungTu)
                    //data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu))
                    //data.Add("SoQuyetDinh", item.SSoQuyetDinh)
                    //data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh))
                    //data.Add("MoTa", item.SMoTa)
                    //data.Add("NguoiTao", item.SNguoiTao)
                    //data.Add("NgayTao", DateUtils.Format(item.DNgayTao))
                    data.Add("Items", lstData);
                    data.Add("MLNS", listMucLucNganSach);

                    double? TotalQTQuyTruoc = lstData?.Where(x => !x.BHangCha).Sum(x => x.FQTQuyTruoc);
                    double? TotalLuyKeCapDenCuoiQuy = lstData?.Where(x => !x.BHangCha).Sum(x => x.FLuyKeCapDenCuoiQuy);
                    double? TotalTamUngQuyNay = lstData?.Where(x => !x.BHangCha).Sum(x => x.FTamUngQuyNay);
                    double? TotalQTThuaQuyTruocChuyenSang = lstData?.Where(x => !x.BHangCha).Sum(x => x.FCapThuaQuyTruocChuyenSang);
                    double? TotalPhaiTamUngQuyNay = lstData?.Where(x => !x.BHangCha).Sum(x => x.FPhaiCapTamUngQuyNay);

                    data.Add("TotalQTQuyTruoc", string.Format(StringUtils.FORMAT_ZERO, TotalQTQuyTruoc));
                    data.Add("TotalLuyKeCapDenCuoiQuy", string.Format(StringUtils.FORMAT_ZERO, TotalLuyKeCapDenCuoiQuy));
                    data.Add("TotalTamUngQuyNay", string.Format(StringUtils.FORMAT_ZERO, TotalTamUngQuyNay));
                    data.Add("TotalQTThuaQuyTruocChuyenSang", string.Format(StringUtils.FORMAT_ZERO, TotalQTThuaQuyTruocChuyenSang));
                    data.Add("TotalPhaiTamUngQuyNay", string.Format(StringUtils.FORMAT_ZERO, TotalPhaiTamUngQuyNay));

                    List<int> hideColumns = new List<int>();
                    //hideColumns.AddRange(ExportExcelHelper<BhCptuBHYTChiTietModel>.HideColumn(chiTietToi))

                    var xlsFile = _exportService.Export<BhCptuBHYTChiTietModel, BhDmMucLucNganSach>(templateFileName, data, hideColumns);
                    var nameRange = xlsFile.GetNamedRange(1);
                    nameRange.Comment = "Workbook";
                    xlsFile.SetNamedRange(nameRange);
                    xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                    xlsFile.SetCellValue(50, 50, "CheckSum");
                    xlsFile.SetRowHidden(50, true);
                    string fileNamePrefix = string.Format("{0}_{1}", item.SSoChungTu, item.SSoQuyetDinh);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
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

        public void OnExportData()
        {

        }

        private void OnUpload()
        {

        }
        public List<AllocationDetailModel> GetDetailDataExport(AllocationModel itemAllocation, string idDonVi)
        {
            return null;
        }

        private void OnImportData()
        {
            try
            {
                ImportCapPhatTamUngBHYTViewModel.Init();
                ImportCapPhatTamUngBHYTViewModel.SavedAction = obj =>
                {
                    _importCapPhatTamUngBHYT.Close();
                    this.LoadData();
                    //OnPropertyChanged(nameof(IsCensorship));
                    IsAllItemsSelected = false;
                    OnShowDetailAllocation((BhCptuBHYTModel)obj);
                };
                _importCapPhatTamUngBHYT = new ImportCapPhatTamUngBHYT { DataContext = ImportCapPhatTamUngBHYTViewModel };
                _importCapPhatTamUngBHYT.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhCptuBHYTModel)eventArgs.Parameter);
        }

        private void ConfirmAggregate()
        {
            List<BhCptuBHYTModel> selectedSktChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
            bool checkAllowAggregate = selectedSktChungTus.All(x => x.BIsKhoa);
            if (checkAllowAggregate)
            {
                OnAggregate();
            }
            else
            {
                string message = Resources.ConfirmAggregate;
                MessageBoxResult result = MessageBoxHelper.Confirm(message);
                if (result == MessageBoxResult.Yes)
                    OnAggregate();
            }
        }

        private void OnAggregate()
        {
            bool existTongHop = false;
            existTongHop = _cptuBHYTService.IsExistChungTuTongHop(_sessionInfo.YearOfWork);
            if (existTongHop)
            {
                MessageBoxResult result = MessageBoxHelper.Confirm(Resources.MesConfirmSaveAggregateDemand);
                if (result != MessageBoxResult.Yes)
                    return;
            }
            //kiểm tra trạng thái các bản ghi
            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                return;
            }
            List<BhCptuBHYTModel> selectedVouchers = Items.Where(x => x.Selected && x.BIsKhoa && !x.IsSummaryVocher).ToList();
            CapPhatTamUngKCBBHYTDialogViewModel.IsEditProcess = false;
            CapPhatTamUngKCBBHYTDialogViewModel.Name = "Thêm chứng từ";
            CapPhatTamUngKCBBHYTDialogViewModel.Description = "Tạo mới chứng từ tổng hợp";
            CapPhatTamUngKCBBHYTDialogViewModel.CpctBHYTModel = new BhCptuBHYTModel();
            CapPhatTamUngKCBBHYTDialogViewModel.IsSummary = true;
            CapPhatTamUngKCBBHYTDialogViewModel.ListIdsChungTuSummary = selectedVouchers;
            CapPhatTamUngKCBBHYTDialogViewModel.Init();
            CapPhatTamUngKCBBHYTDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhCptuBHYTModel)obj);
            };
            var addView = new CapPhatTamUngKCBBHYTDialog() { DataContext = CapPhatTamUngKCBBHYTDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void OpenDetailDialog(BhCptuBHYTModel modelVoucher, params bool[] isNew)
        {
            CapPhatTamUngKCBBHYTDetailViewModel.Model = ObjectCopier.Clone(modelVoucher);
            CapPhatTamUngKCBBHYTDetailViewModel.IsVoucherSummary = !string.IsNullOrEmpty(modelVoucher.SDSSoChungTuTongHop);

            CapPhatTamUngKCBBHYTDetailViewModel.Init();
            var view = new CapPhatTamUngKCBBHYTDetail() { DataContext = CapPhatTamUngKCBBHYTDetailViewModel };
            view.ShowDialog();
        }
    }
}
