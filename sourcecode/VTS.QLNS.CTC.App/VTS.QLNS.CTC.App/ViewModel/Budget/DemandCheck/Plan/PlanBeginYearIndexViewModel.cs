using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan.ExportPlanBeginYear;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan.SendDataPlanBeginYear;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PlanBeginYearIndexViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly ISktSoLieuChiTietCanCuService _sktSoLieuChiTietCanCuService;
        private readonly ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private readonly ICryptographyService _iCryptographyService;
        private PlanBeginYearDetail view;
        private View.Budget.DemandCheck.Plan.SendDataPlanBeginYear.SendDataPlanBeginYear viewSendData;
        private readonly IExportService _exportService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ISktSoLieuChungTuService _sktChungTuService;
        private readonly IDanhMucService _iDanhMucService;
        private ImportPlanBeginYear _importView;
        private PlanBeginYearImportJson _importJsonView;
        private readonly DanhMucCauHinhHeThongService _danhMucCauHinhHeThongService;
        private readonly ILog _logger;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ISktChungTuService _iSktChungTuService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly FtpStorageService _ftpStorageService;
        private IHTTPUploadFileService _hTTPUploadFileService;
        public string _cap1 { get; set; }

        public override string FuncCode => NSFunctionCode.BUDGET_DEMANDCHECK_PLAN;
        public override string Name => "Lập dự toán đầu năm";
        public override Type ContentType => typeof(PlanBeginYear);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxTickOutline;
        public override string Title => "CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork;
        public override string Description => "CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork;
        public string HeaderQuyetToan => "Quyết toán " + (_sessionService.Current.YearOfWork - 2);
        public string HeaderDuToan => "Dự toán " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderThucHien => "Ước t.hiện " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderChiTiet => "Chi tiết " + _sessionService.Current.YearOfWork;
        public List<ReportDuToanDauNamTongHopQuery> DataReportTongHop;
        public double TongQuyetToan;
        public double TongDuToan;
        public double TongTuChi;
        public double TongDonVi1;
        public double TongDonVi2;
        public double TongDonVi3;
        public double TongDonVi4;
        public double TongDonVi5;
        public double TongDonVi6;

        public PlanBeginYearDetailViewModel PlanBeginYearDetailViewModel { get; }
        public PrintReportChiTietDuToanDonViViewModel PrintReportChiTietDuToanDonViViewModel { get; }
        public PrintReportTongHopDuToanViewModel PrintReportTongHopDuToanViewModel { get; }
        public PrintReportCompareDemandCheckViewModel PrintReportCompareDemandCheckViewModel { get; }
        public ImportPlanBeginYearViewModel ImportPlanBeginYearViewModel;
        public PlanBeginYearImportJsonViewModel PlanBeginYearImportJsonViewModel;
        public ExportPlanBeginYearViewModel ExportPlanBeginYearViewModel;
        public SendDataPlanBeginYearViewModel SendDataPlanBeginYearViewModel;
        public PlanBeginYearDialogViewModel PlanBeginYearDialogViewModel { get; }
        public PlanBeginYearSummaryViewModel PlanBeginYearSummaryViewModel { get; }
        public PrintReportChiThuongXuyenQuocPhongViewModel PrintReportChiThuongXuyenQuocPhongViewModel { get; }
        public PrintReportDuToanNganSachViewModel PrintReportDuToanNganSachViewModel { get; }
        public PrintReportCompareSKTDTDNViewModel PrintReportCompareSKTDTDNViewModel { get; }
        public PrintReportChiTietDuToanTheoNganhViewModel PrintReportChiTietDuToanTheoNganhViewModel { get; }

        public bool IsDonViC4Only { get; set; } = false;
        public bool IsEdit => (TabIndex == ImportTabIndex.Data) ? (SelectedPlan != null && !SelectedPlan.IsLocked) : (SelectedPlanSummary != null && !SelectedPlanSummary.IsLocked);
        public bool IsEnableButtonDataShow => TabIndex != ImportTabIndex.Data;
        private ObservableCollection<PlanBeginYearModel> _dataPlan;
        public ObservableCollection<PlanBeginYearModel> DataPlan
        {
            get => _dataPlan;
            set => SetProperty(ref _dataPlan, value);
        }

        private PlanBeginYearModel _selectedPlan;
        public PlanBeginYearModel SelectedPlan
        {
            get => _selectedPlan;
            set
            {
                SetProperty(ref _selectedPlan, value);
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsLockButton));
            }
        }

        private ObservableCollection<PlanBeginYearModel> _dataPlanSummary;
        public ObservableCollection<PlanBeginYearModel> DataPlanSummary
        {
            get => _dataPlanSummary;
            set => SetProperty(ref _dataPlanSummary, value);
        }

        private PlanBeginYearModel _selectedPlanSummary;
        public PlanBeginYearModel SelectedPlanSummary
        {
            get => _selectedPlanSummary;
            set
            {
                SetProperty(ref _selectedPlanSummary, value);
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsLockButton));
                OnPropertyChanged(nameof(IsEnableButtonExportExcel));
            }
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (DataPlan != null)
                {
                    var selected = DataPlan.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, DataPlan);
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsEnableButtonExport));
                }
            }
        }

        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (DataPlanSummary != null)
                {
                    var selected = DataPlanSummary.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, DataPlanSummary);
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsEnableButtonExport));
                }
            }
        }



        private static void SelectAll(bool select, IEnumerable<PlanBeginYearModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }
        public bool IsEnableButtonExport
        {
            get
            {
                if (DataPlan == null) return false;
                var itemSelected = DataPlan.Where(x => x.Selected);
                return itemSelected.Any() && itemSelected.All(x => x.IsLocked) && itemSelected.Select(x => x.ILoaiNguonNganSach).Distinct().Count() == 1;
            }
        }
        public bool IsEnableButtonExportExcel
        {
            get
            {
                var lstItem = (TabIndex == ImportTabIndex.Data) ? DataPlan : DataPlanSummary;
                if (lstItem == null) return false;
                var lstSelected = lstItem.Where(x => x.Selected).ToList();
                return lstSelected.Any();
            }
        }

        private ObservableCollection<ComboboxItem> _dataLoaiNganSach;
        public ObservableCollection<ComboboxItem> DataLoaiNganSach
        {
            get => _dataLoaiNganSach;
            set => SetProperty(ref _dataLoaiNganSach, value);
        }

        private ComboboxItem _selectedLoaiNganSach;
        public ComboboxItem SelectedLoaiNganSach
        {
            get => _selectedLoaiNganSach;
            set
            {
                if (SetProperty(ref _selectedLoaiNganSach, value))
                {
                    if (!IsLoading) OnRefresh();
                }
            }
        }

        public bool IsEnableButtonChiTiet
        {
            get => DataPlan != null && DataPlan.Count() > 0;
        }

        private bool _isLockButton;
        public bool IsLockButton
        {
            get => _isLockButton;
            set => SetProperty(ref _isLockButton, value);

        }
        public bool IsButtonEnable
        {
            get
            {
                var result = false;
                var lstItem = (TabIndex == ImportTabIndex.Data) ? DataPlan : DataPlanSummary;
                if (lstItem == null) return false;
                var lstSelected = lstItem.Where(x => x.Selected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Any())
                {
                    result = true;
                }
                else
                {
                    result = lstSelected.Any() && (lstSelected.All(x => x.IsLocked) || lstSelected.All(x => !x.IsLocked));
                    IsLockButton = lstSelected.Any(x => x.IsLocked);
                }
                return result;
            }
        }

        private string _loaiChungTu;
        public string LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                if (DataPlan != null)
                    DataPlan.ForAll(x => x.Selected = false);
                if (DataPlanSummary != null)
                    DataPlanSummary.ForAll(x => x.Selected = false);

                OnPropertyChanged(nameof(IsLockButton));
                OnPropertyChanged(nameof(IsButtonEnable));
                OnPropertyChanged(nameof(SelectedPlan));
                OnPropertyChanged(nameof(SelectedPlanSummary));
                OnPropertyChanged(nameof(IsEnableButtonExportExcel));
                OnPropertyChanged(nameof(IsEnableButtonExport));
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsAllItemsSelected));
                OnPropertyChanged(nameof(IsAllItemSummariesSelected));
            }
        }


        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        private ComboboxItem _budgetSourceTypeSelected;

        public ComboboxItem BudgetSourceTypeSelected
        {
            get => _budgetSourceTypeSelected;
            set
            {
                if (SetProperty(ref _budgetSourceTypeSelected, value))
                {
                    if (!IsLoading) OnRefresh();
                }
            }
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
                if (SetProperty(ref _lockStatusSelected, value))
                {
                    if (!IsLoading) OnRefresh();
                    OnPropertyChanged(nameof(IsButtonEnable));
                    if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                    {
                        IsLockButton = true;
                    }
                    else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                    {
                        IsLockButton = false;
                    }
                }
            }
        }

        private ObservableCollection<SktSoLieuChiTietMLNSModel> _dataPlanBeginYearExport;
        public ObservableCollection<SktSoLieuChiTietMLNSModel> DataPlanBeginYearExport
        {
            get => _dataPlanBeginYearExport;
            set
            {
                SetProperty(ref _dataPlanBeginYearExport, value);
            }
        }

        public RelayCommand RefeshCommand { get; }
        public RelayCommand ShowPopupDetailCommand { get; }
        public RelayCommand ShowPopupDetailSummaryCommand { get; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ShowPopupReportChiTietCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ShowPopupReportCompareCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand ShowPopupAddCommand { get; }
        public RelayCommand ShowPopupEditCommand { get; }
        public RelayCommand SummaryProcessCommand { get; }
        public RelayCommand ShowPopupReportChiThuongXuyenCommand { get; }
        public RelayCommand ShowPopupReportChiTietTheoNganhCommand { get; }
        public RelayCommand ShowPopupReportDuToanNSCommand { get; }
        public RelayCommand ShowPopupCompareSKTDTDNCommand { get; }
        public RelayCommand UploadFileCommandHTTP { get; }
        public RelayCommand UploadFileCommandFTP { get; }
        public RelayCommand ImportJsonCommand { get; }
        public RelayCommand ExportJsonCommand { get; }


        public PlanBeginYearIndexViewModel(IMapper mapper,
           ISessionService sessionService,
           INsDonViService nsDonViService,
           ISktSoLieuService sktSoLieuService,
           IExportService exportService,
           ISktSoLieuChungTuService sktChungTuService,
           ILog logger,
           INsMucLucNganSachService mucLucNganSachService,
           INsNguoiDungDonViService nsNguoiDungDonViService,
           ISktChungTuService iSktChungTuService,
           ICauHinhCanCuService iCauHinhCanCuService,
           IDanhMucService iDanhMucService,
           ICryptographyService iCryptographyService,
           PlanBeginYearDetailViewModel planBeginYearDetailViewModel,
           PrintReportChiTietDuToanDonViViewModel printReportChiTietDuToanDonViViewModel,
           PrintReportTongHopDuToanViewModel printReportTongHopDuToanViewModel,
           DanhMucCauHinhHeThongService danhMucCauHinhHeThongService,
           PrintReportCompareDemandCheckViewModel printReportCompareDemandCheckViewModel,
           PlanBeginYearDialogViewModel planBeginYearDialogViewModel,
           PlanBeginYearSummaryViewModel planBeginYearSummaryViewModel,
           ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
           ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
           ISktSoLieuChiTietCanCuService sktSoLieuChiTietCanCuService,
           PrintReportChiThuongXuyenQuocPhongViewModel printReportChiThuongXuyenQuocPhongViewModel,
           PrintReportChiTietDuToanTheoNganhViewModel printReportChiTietDuToanTheoNganhViewModel,
           PrintReportDuToanNganSachViewModel printReportDuToanNganSachViewModel,
           PrintReportCompareSKTDTDNViewModel printReportCompareSKTDTDNViewModel,
           IDanhMucService danhMucService,
           IVdtFtpRootService ftpService,
           FtpStorageService ftpStorageService,
           ImportPlanBeginYearViewModel importPlanBeginYearViewModel,
           PlanBeginYearImportJsonViewModel planBeginYearImportJsonViewModel,
           ExportPlanBeginYearViewModel exportPlanBeginYearViewModel,
           SendDataPlanBeginYearViewModel sendDataPlanBeginYearViewModel,
           IHTTPUploadFileService hTTPUploadFileService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _sktSoLieuService = sktSoLieuService;
            _exportService = exportService;
            _sktChungTuService = sktChungTuService;
            _iDanhMucService = iDanhMucService;
            _iCryptographyService = iCryptographyService;
            _mucLucNganSachService = mucLucNganSachService;
            _logger = logger;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _iSktChungTuService = iSktChungTuService;
            _sktSoLieuChiTietCanCuService = sktSoLieuChiTietCanCuService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            ImportPlanBeginYearViewModel = importPlanBeginYearViewModel;
            PlanBeginYearDetailViewModel = planBeginYearDetailViewModel;
            PrintReportCompareDemandCheckViewModel = printReportCompareDemandCheckViewModel;
            PrintReportChiTietDuToanDonViViewModel = printReportChiTietDuToanDonViViewModel;
            PrintReportTongHopDuToanViewModel = printReportTongHopDuToanViewModel;
            PrintReportChiThuongXuyenQuocPhongViewModel = printReportChiThuongXuyenQuocPhongViewModel;
            PrintReportChiTietDuToanTheoNganhViewModel = printReportChiTietDuToanTheoNganhViewModel;
            PrintReportDuToanNganSachViewModel = printReportDuToanNganSachViewModel;
            PrintReportCompareSKTDTDNViewModel = printReportCompareSKTDTDNViewModel;
            PlanBeginYearSummaryViewModel = planBeginYearSummaryViewModel;
            PlanBeginYearDialogViewModel = planBeginYearDialogViewModel;
            PlanBeginYearImportJsonViewModel = planBeginYearImportJsonViewModel;
            ExportPlanBeginYearViewModel = exportPlanBeginYearViewModel;
            SendDataPlanBeginYearViewModel = sendDataPlanBeginYearViewModel;
            _danhMucCauHinhHeThongService = danhMucCauHinhHeThongService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _danhMucService = danhMucService;
            _ftpService = ftpService;
            _ftpStorageService = ftpStorageService;
            _hTTPUploadFileService = hTTPUploadFileService;

            RefeshCommand = new RelayCommand(o => OnRefresh());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
            ShowPopupDetailCommand = new RelayCommand(o => OnShowPopupDetail(true, null));
            ShowPopupDetailSummaryCommand = new RelayCommand(o => OnShowPopupDetailSummary());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateDataDialog());
            //ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ShowPopupReportChiThuongXuyenCommand = new RelayCommand(obj => OnShowPopupReportChiThuongXuyen(obj));
            ShowPopupReportChiTietTheoNganhCommand = new RelayCommand(obj => OnShowPopupReportChiTietTheoNganh(obj));
            ShowPopupReportDuToanNSCommand = new RelayCommand(obj => OnShowPopupReportDuToanNganSah(obj));
            ShowPopupCompareSKTDTDNCommand = new RelayCommand(obj => OnShowPopupCompareSKTDTDN(obj));
            ShowPopupReportChiTietCommand = new RelayCommand(obj => OnShowPopupReportChiTiet(obj));
            ShowPopupReportCompareCommand = new RelayCommand(obj => OnShowPopupReportCompare());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            DeleteCommand = new RelayCommand(obj => OnDelete());
            ShowPopupAddCommand = new RelayCommand(o => OnShowPopupAdd());
            ShowPopupEditCommand = new RelayCommand(o => OnShowPopupEdit());
            SummaryProcessCommand = new RelayCommand(o => OnShowPopupSummary());
            UploadFileCommandHTTP = new RelayCommand(obj => OnUploadDialog(true));
            UploadFileCommandFTP = new RelayCommand(obj => OnUploadDialog(false));
            //UploadFileCommand = new RelayCommand(obj => OnUpload());
            ImportJsonCommand = new RelayCommand(obj => OnImportJson());
            ExportJsonCommand = new RelayCommand(obj => OnExportJson());
        }

        private async void OnShowPopupSummary()
        {
            try
            {
                //check quyền được tổng hợp
                if (DataPlan == null || DataPlan.Count(n => n.IsLocked && n.Selected) == 0)
                {
                    return;
                }

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                    return;
                }

                if (DataPlan.Count(n => !n.IsLocked && n.Selected) > 0)
                {
                    MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.MsgConfirmSummary);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        PlanBeginYearSummaryViewModel.Model = new PlanBeginYearModel();
                        PlanBeginYearSummaryViewModel.LoaiChungTu = LoaiChungTu;
                        PlanBeginYearSummaryViewModel.DataPlan = new ObservableCollection<PlanBeginYearModel>(DataPlan.Where(n => n.IsLocked && n.Selected).ToList());
                        PlanBeginYearSummaryViewModel.Init();
                        PlanBeginYearSummaryViewModel.SavedAction = obj =>
                        {
                            if (!IsLoading) OnRefresh();
                        };
                        var view = new PlanBeginYearSummary
                        {
                            DataContext = PlanBeginYearSummaryViewModel
                        };
                        var result = await DialogHost.Show(view, "RootDialog", null, null);
                    }
                }
                else
                {
                    PlanBeginYearSummaryViewModel.Model = new PlanBeginYearModel();
                    PlanBeginYearSummaryViewModel.LoaiChungTu = LoaiChungTu;
                    PlanBeginYearSummaryViewModel.DataPlan = new ObservableCollection<PlanBeginYearModel>(DataPlan.Where(n => n.IsLocked && n.Selected).ToList());
                    PlanBeginYearSummaryViewModel.Init();
                    PlanBeginYearSummaryViewModel.SavedAction = obj =>
                    {
                        if (!IsLoading) OnRefresh();
                        TabIndex = ImportTabIndex.MLNS;
                        this.LoadData();
                    };
                    var view = new PlanBeginYearSummary
                    {
                        DataContext = PlanBeginYearSummaryViewModel
                    };
                    var result = await DialogHost.Show(view, "RootDialog", null, null);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private async void OnShowPopupAdd()
        {
            try
            {
                //check quyền được tạo mới
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                PlanBeginYearDialogViewModel.Model = new PlanBeginYearModel();
                PlanBeginYearDialogViewModel.Init();
                PlanBeginYearDialogViewModel.SavedAction = obj =>
                {
                    PlanBeginYearModel objValue = (PlanBeginYearModel)obj;
                    if (objValue != null && objValue.LoaiNganSach == VoucherType.NSBD_Key.ToString())
                    {
                        SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault(n => n.ValueItem == VoucherType.NSBD_Key.ToString());
                    }
                    else
                    {
                        SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
                    }
                    if (!IsLoading) OnRefresh();
                    OnShowPopupDetail(false, objValue);
                };
                var view = new PlanBeginYearDialog
                {
                    DataContext = PlanBeginYearDialogViewModel
                };
                var result = await DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Tất cả", ValueItem = TypeLoaiNNS.TAT_CA.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() }
            };
            BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
        }

        private async void OnShowPopupEdit()
        {
            try
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (SelectedPlan != null)
                    {
                        if (SelectedPlan.NguoiTao != _sessionService.Current.Principal)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedPlan.NguoiTao));
                            return;
                        }
                        this.PlanBeginYearDialogViewModel.Model = SelectedPlan;
                        PlanBeginYearDialogViewModel.IsSummary = false;
                        PlanBeginYearDialogViewModel.Init();
                        PlanBeginYearDialogViewModel.SavedAction = obj =>
                        {
                            PlanBeginYearModel objValue = (PlanBeginYearModel)obj;
                            if (objValue != null && objValue.LoaiNganSach == VoucherType.NSBD_Key.ToString())
                            {
                                SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault(n => n.ValueItem == VoucherType.NSBD_Key.ToString());
                            }
                            else
                            {
                                SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
                            }
                            if (!IsLoading) OnRefresh();
                        };
                        var view = new PlanBeginYearDialog
                        {
                            DataContext = PlanBeginYearDialogViewModel
                        };
                        var result = await DialogHost.Show(view, "RootDialog", null, null);
                    }
                }
                else
                {
                    if (SelectedPlanSummary != null)
                    {
                        if (SelectedPlanSummary.NguoiTao != _sessionService.Current.Principal)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedPlanSummary.NguoiTao));
                            return;
                        }
                        PlanBeginYearDialogViewModel.Model = SelectedPlanSummary;
                        PlanBeginYearDialogViewModel.IsSummary = true;
                        PlanBeginYearDialogViewModel.Init();
                        PlanBeginYearDialogViewModel.SavedAction = obj =>
                        {
                            if (!IsLoading) OnRefresh();
                        };
                        var view = new PlanBeginYearDialog
                        {
                            DataContext = PlanBeginYearDialogViewModel
                        };
                        var result = await DialogHost.Show(view, "RootDialog", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
                IsLoading = true;
                TabIndex = ImportTabIndex.Data;
                LoadLockStatus();
                LoadCombobox();
                LoadBudgetSourceTypes();
                List<DonVi> listDonVi = _nsDonViService.FindByLoai(_sessionService.Current.YearOfWork, LoaiDonVi.NOI_BO).ToList();
                IsDonViC4Only = listDonVi.Count > 0;
                OnRefresh();
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Description));
                LoadDanhMuc();
                IsLoading = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadDanhMuc()
        {
            var danhMucQuanLy = _iDanhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY);
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
        }

        private bool CheckQuanLyDonVi0()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            var nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            DonVi donViTongHop = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            if (nsDungDonVis.Count > 0 && donViTongHop != null)
            {
                if (nsDungDonVis.Select(n => n.IIdMaDonVi).ToList().Contains(donViTongHop.IIDMaDonVi))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnShowPopupDetailSummary()
        {
            try
            {
                if (SelectedPlanSummary == null)
                    return;
                PlanBeginYearDetailViewModel.Model = SelectedPlanSummary;
                PlanBeginYearDetailViewModel.LoaiChungTu = LoaiChungTu;
                PlanBeginYearDetailViewModel.LstCanCu = null;
                PlanBeginYearDetailViewModel.IsLocked = SelectedPlanSummary.IsLocked;
                PlanBeginYearDetailViewModel.ListNguoiDungDonVi = GetListDonViPhanQuyen();
                PlanBeginYearDetailViewModel.Init();
                view = new PlanBeginYearDetail
                {
                    DataContext = PlanBeginYearDetailViewModel
                };
                PlanBeginYearDetailViewModel.ClosePopup += RefreshAfterClosePopup;
                PlanBeginYearDetailViewModel.RefeshIndexWindow += RefreshGrid;
                view.ShowDialog();
                PlanBeginYearDetailViewModel.ClosePopup -= RefreshAfterClosePopup;
                PlanBeginYearDetailViewModel.RefeshIndexWindow -= RefreshGrid;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupDetail(bool isShowSelected = true, PlanBeginYearModel obj = null)
        {
            try
            {
                if (isShowSelected)
                {
                    if (SelectedPlan == null)
                        return;
                    PlanBeginYearDetailViewModel.Model = SelectedPlan;
                    PlanBeginYearDetailViewModel.LoaiChungTu = LoaiChungTu;
                    PlanBeginYearDetailViewModel.LstCanCu = null;
                    PlanBeginYearDetailViewModel.IsLocked = SelectedPlan.IsLocked;
                    PlanBeginYearDetailViewModel.ListNguoiDungDonVi = GetListDonViPhanQuyen();
                    PlanBeginYearDetailViewModel.Init();
                    view = new PlanBeginYearDetail
                    {
                        DataContext = PlanBeginYearDetailViewModel
                    };
                    PlanBeginYearDetailViewModel.RefeshIndexWindow += RefreshGrid;
                    PlanBeginYearDetailViewModel.ClosePopup += RefreshAfterClosePopup;
                    view.ShowDialog();
                    PlanBeginYearDetailViewModel.RefeshIndexWindow -= RefreshGrid;
                    PlanBeginYearDetailViewModel.ClosePopup -= RefreshAfterClosePopup;
                }
                else
                {
                    if (obj == null)
                        return;
                    PlanBeginYearDetailViewModel.Model = obj;
                    PlanBeginYearDetailViewModel.LoaiChungTu = LoaiChungTu;
                    PlanBeginYearDetailViewModel.LstCanCu = null;
                    PlanBeginYearDetailViewModel.IsLocked = obj.IsLocked;
                    PlanBeginYearDetailViewModel.ListNguoiDungDonVi = GetListDonViPhanQuyen();
                    PlanBeginYearDetailViewModel.Init();
                    view = new PlanBeginYearDetail
                    {
                        DataContext = PlanBeginYearDetailViewModel
                    };
                    PlanBeginYearDetailViewModel.RefeshIndexWindow += RefreshGrid;
                    PlanBeginYearDetailViewModel.ClosePopup += RefreshAfterClosePopup;
                    view.ShowDialog();
                    PlanBeginYearDetailViewModel.RefeshIndexWindow -= RefreshGrid;
                    PlanBeginYearDetailViewModel.ClosePopup -= RefreshAfterClosePopup;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            view.Close();
            //OnRefresh();
        }

        private void RefreshGrid(object sender, EventArgs e)
        {
            if (!IsLoading) OnRefresh();
        }

        public void OnLockUnLock()
        {
            try
            {
                var session = _sessionService.Current;
                DonVi donViTongHop = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (DataPlanSummary != null && DataPlanSummary.Count > 0)
                    {
                        var chungTuDonViTongHop = DataPlanSummary.FirstOrDefault(n => n.Loai == LoaiDonVi.ROOT);
                        if (chungTuDonViTongHop != null && donViTongHop != null && donViTongHop.Loai == chungTuDonViTongHop.Loai)
                        {
                            var listDonVi = string.Join(", ", DataPlan.Where(n => n.Selected && n.IsLocked && !CheckCanUnLock(donViTongHop.IIDMaDonVi, n.Id_DonVi)).Select(n => n.SSoChungTu));
                            if (!string.IsNullOrEmpty(listDonVi))
                            {
                                //MessageBoxHelper.Warning(Resources.MsgVoucherHasLock);
                                MessageBoxHelper.Warning(string.Format("Chứng từ tổng hợp của chứng từ {0} đang bị khóa", listDonVi));
                                return;
                            }
                        }
                    }

                    var listSoChungTuInvalid = string.Join(", ", DataPlan.Where(n => n.Selected && !n.IsLocked && n.NguoiTao != _sessionService.Current.Principal).Select(n => n.SSoChungTu));

                    if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                    {
                        //MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, item.NguoiTao));
                        MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listSoChungTuInvalid));
                        return;
                    }

                    var listSoChungTu = string.Join(", ", DataPlan.Where(n => n.Selected && n.IsLocked).Select(n => n.SSoChungTu));
                    if (!CheckQuanLyDonVi0() & !string.IsNullOrEmpty(listSoChungTu))
                    {
                        //MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                        MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTu));
                        return;
                    }

                    var listSoChungTuDaTongHop = string.Join(", ", DataPlan.Where(n => n.Selected && n.IsLocked && n.BDaTongHop.GetValueOrDefault()).Select(n => n.IsLocked));
                    if (!string.IsNullOrEmpty(listSoChungTuDaTongHop))
                    {
                        //MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                        MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do đã gửi lên tổng hợp", listSoChungTuDaTongHop));
                        return;
                    }

                    var listDataSelected = DataPlan.Where(n => n.Selected);
                    foreach (var item in listDataSelected)
                    {
                        HandlerLockDonVi(item);
                    }
                    if (!listDataSelected.FirstOrDefault().IsLocked)
                    {
                        MessageBoxHelper.Info(Resources.MsgLockDone);
                        LockStatusSelected = LockStatus.ElementAt(0);
                    }
                    else
                    {
                        MessageBoxHelper.Info(Resources.MsgUnLockDone);
                        LockStatusSelected = LockStatus.ElementAt(0);
                    }

                    IsLockButton = !IsLockButton;
                    if (!IsLoading) OnRefresh();
                }
                else if (TabIndex == ImportTabIndex.MLNS)
                {
                    var listDataSummarySelected = DataPlanSummary.Where(n => n.Selected);

                    if (DataPlanSummary != null && DataPlanSummary.Count > 0)
                    {
                        var chungTuDonViTongHop = DataPlanSummary.FirstOrDefault(n => n.Loai == LoaiDonVi.ROOT);
                        if (chungTuDonViTongHop != null && donViTongHop != null && donViTongHop.Loai == chungTuDonViTongHop.Loai)
                        {
                            string listDonVi = string.Join(", ", listDataSummarySelected.Where(n => n.IsLocked && !CheckCanUnLock(donViTongHop.IIDMaDonVi, n.Id_DonVi)).Select(n => n.SSoChungTu));
                            if (!string.IsNullOrEmpty(listDonVi))
                            {
                                //MessageBoxHelper.Warning(Resources.MsgVoucherHasLock);
                                MessageBoxHelper.Warning(string.Format("Chứng từ tổng hợp của chứng từ {0} đang bị khóa", listDonVi));
                                return;
                            }
                        }
                    }

                    var listSoChungTuInvalid = string.Join(", ", listDataSummarySelected.Where(n => !n.IsLocked && n.NguoiTao != _sessionService.Current.Principal).Select(n => n.SSoChungTu));
                    if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                    {
                        //MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedPlanSummaryItem.NguoiTao));
                        MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listSoChungTuInvalid));
                        return;
                    }


                    var listSoChungTuLocked = string.Join(", ", listDataSummarySelected.Where(n => n.IsLocked).Select(n => n.SSoChungTu));
                    if (!CheckQuanLyDonVi0() && !string.IsNullOrEmpty(listSoChungTuLocked))
                    {
                        //MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                        MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTuLocked));
                        return;
                    }

                    foreach (var item in listDataSummarySelected)
                    {
                        HandlerLockDonVi(item);
                    }

                    if (!listDataSummarySelected.FirstOrDefault().IsLocked)
                    {
                        MessageBoxHelper.Info(Resources.MsgLockDone);
                        LockStatusSelected = LockStatus.ElementAt(0);
                    }
                    else
                    {
                        MessageBoxHelper.Info(Resources.MsgUnLockDone);
                        LockStatusSelected = LockStatus.ElementAt(0);

                    }

                    IsLockButton = !IsLockButton;
                    if (!IsLoading) OnRefresh();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool CheckCanUnLock(string maDonViTongHop, string maDonVi)
        {
            var predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.BKhoa);
            predicate = predicate.And(x => x.IIdMaDonVi == maDonViTongHop);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            NsDtdauNamChungTu chungTu = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
            if (chungTu != null)
            {
                if (!string.IsNullOrEmpty(chungTu.SDSDonViTongHop) && chungTu.SDSDonViTongHop.Split(",").ToList().Contains(maDonVi))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        private string GetListDonViTongHop()
        {
            List<string> listChungTu = DataPlan.Where(n => n.Selected && n.IsLocked).Select(n => n.Id_DonVi).ToList();
            if (listChungTu.Count > 0)
            {
                return string.Join(",", listChungTu);
            }
            else
            {
                return string.Empty;
            }
        }

        public void HandlerLockDonVi(PlanBeginYearModel obj)
        {
            if (obj == null)
                return;
            var predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.IIdMaDonVi == obj.Id_DonVi);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            predicate = predicate.And(x => x.Id == obj.Id);

            NsDtdauNamChungTu chungTu = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
            if (chungTu != null)
            {
                NsDtdauNamChungTu entity = _sktChungTuService.Find(chungTu.Id);
                entity.BKhoa = !obj.IsLocked;
                //if (SelectedPlan != null && !SelectedPlan.IsLocked && SelectedPlan.Loai == LoaiDonVi.ROOT)
                //{
                //    entity.SDSDonViTongHop = GetListDonViTongHop();
                //}
                _sktChungTuService.Update(entity);
                List<NsDtdauNamChungTuChiTiet> entityChiTiet = _sktSoLieuService.FindByCondition(n => n.IIdCtdtdauNam == chungTu.Id).ToList();
                entityChiTiet.ForAll(n => n.BKhoa = !obj.IsLocked);
                _sktSoLieuService.UpdateRange(entityChiTiet);
            }
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set
            {
                SetProperty(ref _isCollapse, value);
                if (!IsLoading) OnRefresh();
            }
        }

        private void LoadCombobox()
        {
            DataLoaiNganSach = new ObservableCollection<ComboboxItem>();
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = VoucherType.NSSD_Value });
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = VoucherType.NSBD_Value });
            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
        }

        private void OnDelete()
        {
            try
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (SelectedPlan == null)
                    {
                        return;
                    }
                    if (SelectedPlan.NguoiTao != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedPlan.NguoiTao));
                        return;
                    }
                    MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.MsgDeleteRecord);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
                        predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                        predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                        predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                        predicate = predicate.And(x => x.IIdMaDonVi == SelectedPlan.Id_DonVi);
                        predicate = predicate.And(x => x.ILoaiChungTu == LoaiChungTu);
                        predicate = predicate.And(x => x.IIdCtdtdauNam == SelectedPlan.Id);

                        var predicateCanCu = PredicateBuilder.True<NsDtdauNamChungTuChungTuCanCu>();
                        predicateCanCu = predicateCanCu.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                        predicateCanCu = predicateCanCu.And(x => x.IIdMaDonVi == SelectedPlan.Id_DonVi);
                        predicateCanCu = predicateCanCu.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));

                        var predicateChiTietCanCu = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.IIdMaDonVi == SelectedPlan.Id_DonVi);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.IID_CTDTDauNam == SelectedPlan.Id);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));

                        List<NsDtdauNamChungTuChiTiet> listDelete = _sktSoLieuService.FindByCondition(predicate).ToList();
                        List<NsDtdauNamChungTuChiTietCanCu> listDeleteCanCu = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateChiTietCanCu).ToList();
                        List<NsDtdauNamChungTuChungTuCanCu> listDeleteChungTuCanCu = _sktSoLieuChiTietCanCuService.FindByCondition(predicateCanCu).ToList();
                        _sktSoLieuService.RemoveRange(listDelete);
                        _sktSoLieuChiTietCanCuDataService.RemoveRange(listDeleteCanCu);
                        _sktSoLieuChiTietCanCuService.RemoveRange(listDeleteChungTuCanCu);
                        _sktChungTuService.Delete(SelectedPlan.Id);
                        if (!IsLoading) OnRefresh();
                    }
                }
                else
                {
                    if (SelectedPlanSummary == null)
                    {
                        return;
                    }
                    if (SelectedPlanSummary.NguoiTao != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedPlanSummary.NguoiTao));
                        return;
                    }
                    MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.MsgDeleteRecord);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        // bo trang thai da tong hop cua chung tu con
                        ResetTrangThaiChungTuCon(SelectedPlanSummary);

                        var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
                        predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                        predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                        predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                        predicate = predicate.And(x => x.IIdMaDonVi == SelectedPlanSummary.Id_DonVi);
                        predicate = predicate.And(x => x.ILoaiChungTu == LoaiChungTu);
                        predicate = predicate.And(x => x.IIdCtdtdauNam == SelectedPlanSummary.Id);

                        var predicateCanCu = PredicateBuilder.True<NsDtdauNamChungTuChungTuCanCu>();
                        predicateCanCu = predicateCanCu.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                        predicateCanCu = predicateCanCu.And(x => x.IIdMaDonVi == SelectedPlanSummary.Id_DonVi);
                        predicateCanCu = predicateCanCu.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));

                        var predicateChiTietCanCu = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.IIdMaDonVi == SelectedPlanSummary.Id_DonVi);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.IID_CTDTDauNam == SelectedPlanSummary.Id);
                        predicateChiTietCanCu = predicateChiTietCanCu.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));

                        List<NsDtdauNamChungTuChiTiet> listDelete = _sktSoLieuService.FindByCondition(predicate).ToList();
                        List<NsDtdauNamChungTuChiTietCanCu> listDeleteCanCu = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateChiTietCanCu).ToList();
                        List<NsDtdauNamChungTuChungTuCanCu> listDeleteChungTuCanCu = _sktSoLieuChiTietCanCuService.FindByCondition(predicateCanCu).ToList();
                        List<NsDtdauNamPhanCap> listDeletePhanCap = _soLieuChiTietPhanCapService.FindAll().Where(n => n.IIdCtdtDauNam == SelectedPlanSummary.Id
                        && n.INamLamViec == _sessionService.Current.YearOfWork && n.IIdMaDonVi == SelectedPlanSummary.Id_DonVi).ToList();
                        _sktSoLieuService.RemoveRange(listDelete);
                        _sktSoLieuChiTietCanCuDataService.RemoveRange(listDeleteCanCu);
                        _sktSoLieuChiTietCanCuService.RemoveRange(listDeleteChungTuCanCu);
                        _soLieuChiTietPhanCapService.RemoveRange(listDeletePhanCap);
                        _sktChungTuService.Delete(SelectedPlanSummary.Id);
                        if (!IsLoading) OnRefresh();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ResetTrangThaiChungTuCon(PlanBeginYearModel chungTuTongHop)
        {
            if (!string.IsNullOrEmpty(chungTuTongHop.DSSoChungTuTongHop))
            {
                var lstSoCtChild = chungTuTongHop.DSSoChungTuTongHop.Split(",");
                foreach (var soct in lstSoCtChild)
                {
                    var ctChild = _sktChungTuService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                                                          && x.ILoaiChungTu == int.Parse(chungTuTongHop.LoaiNganSach)
                                                                          && x.INamLamViec == _sessionService.Current.YearOfWork
                                                                          && x.INamNganSach == _sessionService.Current.YearOfBudget
                                                                          && x.IIdMaNguonNganSach == _sessionService.Current.Budget).FirstOrDefault();
                    if (ctChild != null)
                    {
                        ctChild.BDaTongHop = false;
                        _sktChungTuService.Update(ctChild);
                    }
                }
            }
        }

        private void OnRefresh()
        {
            LoaiChungTu = (SelectedLoaiNganSach?.ValueItem == VoucherType.NSSD_Key) ? VoucherType.NSSD_Key : VoucherType.NSBD_Key;

            List<DonViPlanBeginYearQuery> data = _nsDonViService.FindPlanBeginYearByConditon
                (_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, LoaiChungTu, 0, _sessionService.Current.Principal).ToList();

            if (BudgetSourceTypeSelected?.ValueItem == TypeLoaiNNS.DU_TOAN.ToString())
            {
                data = data.Where(x => x.ILoaiNguonNganSach == TypeLoaiNNS.DU_TOAN).ToList();
            }

            if (BudgetSourceTypeSelected?.ValueItem == TypeLoaiNNS.BENH_VIEN.ToString())
            {
                data = data.Where(x => x.ILoaiNguonNganSach == TypeLoaiNNS.BENH_VIEN).ToList();
            }

            if (BudgetSourceTypeSelected?.ValueItem == TypeLoaiNNS.DOANH_NGHIEP.ToString())
            {
                data = data.Where(x => x.ILoaiNguonNganSach == TypeLoaiNNS.DOANH_NGHIEP).ToList();
            }

            //check user
            List<NguoiDungDonVi> listDonViByUserName = GetListDonViPhanQuyen();
            if (listDonViByUserName != null && listDonViByUserName.Count > 0)
            {
                NguoiDungDonVi itemNguoiDungDonVi = listDonViByUserName.FirstOrDefault(n => n.IIdMaDonVi == _sessionService.Current.IdDonVi);
                if (itemNguoiDungDonVi != null)
                {
                    List<string> listIdDonVi = listDonViByUserName.Select(n => n.IIdMaDonVi).ToList();
                    List<DonViPlanBeginYearQuery> listDataView = data.Where(n => !listIdDonVi.Contains(n.Id_DonVi) && !n.IsLocked).ToList();
                    listDataView.Select(n => { n.SoDuToan = 0; n.SoKiemTra = 0; n.Tang = 0; n.Giam = 0; return n; }).ToList();
                }
            }

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0"))
            {
                DataPlan = _mapper.Map<ObservableCollection<PlanBeginYearModel>>(data.Where(n => n.Id_DonVi != _sessionService.Current.IdDonVi));
            }
            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                DataPlan = _mapper.Map<ObservableCollection<PlanBeginYearModel>>(data.Where(n => n.Id_DonVi != _sessionService.Current.IdDonVi && n.IsLocked));
            }
            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                DataPlan = _mapper.Map<ObservableCollection<PlanBeginYearModel>>(data.Where(n => n.Id_DonVi != _sessionService.Current.IdDonVi && !n.IsLocked));
            }
            if (data.Count(n => n.Id_DonVi == _sessionService.Current.IdDonVi) > 0)
            {
                // danh sách chứng từ tổng hợp
                var itemChungTuTongHop = data.Where(n => n.Id_DonVi == _sessionService.Current.IdDonVi);
                DataPlan = new ObservableCollection<PlanBeginYearModel>(DataPlan.Where(n => itemChungTuTongHop
                    .All(m => (!string.IsNullOrEmpty(m.DSSoChungTuTongHop) && !m.DSSoChungTuTongHop.Split(",").Contains(n.SSoChungTu)) || string.IsNullOrEmpty(m.DSSoChungTuTongHop))).ToList());

                List<DonViPlanBeginYearQuery> dataSumary = new List<DonViPlanBeginYearQuery>();

                var listChungTuCha = data.Where(x => x.Id_DonVi == _sessionService.Current.IdDonVi).OrderByDescending(x => x.SSoChungTu);
                foreach (var chungTu in listChungTuCha)
                {
                    var parent = _mapper.Map<DonViPlanBeginYearQuery>(chungTu);
                    parent.IsExpand = true;
                    dataSumary.Add(parent);
                    if (string.IsNullOrEmpty(chungTu.DSSoChungTuTongHop) || chungTu.DSSoChungTuTongHop.Equals(chungTu.SSoChungTu))
                    {
                        continue;
                    }
                    List<DonViPlanBeginYearQuery> listChild = new List<DonViPlanBeginYearQuery>();

                    if (!string.IsNullOrEmpty(chungTu.DSSoChungTuTongHop))
                    {
                        listChild = _mapper.Map<List<DonViPlanBeginYearQuery>>(data.Where(x => (string.IsNullOrEmpty(x.DSSoChungTuTongHop) && chungTu.DSSoChungTuTongHop.Split(",").Contains(x.SSoChungTu))));
                    }
                    else
                    {
                        listChild = _mapper.Map<List<DonViPlanBeginYearQuery>>(data.Where(x => !string.IsNullOrEmpty(x.DSSoChungTuTongHop)));
                    }

                    listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = chungTu.SSoChungTu; });
                    dataSumary.AddRange(listChild);
                }

                if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0"))
                {
                    DataPlanSummary = _mapper.Map<ObservableCollection<PlanBeginYearModel>>(dataSumary);
                }
                if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
                {
                    DataPlanSummary = _mapper.Map<ObservableCollection<PlanBeginYearModel>>(dataSumary.Where(n => n.IsLocked));
                }
                if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
                {
                    DataPlanSummary = _mapper.Map<ObservableCollection<PlanBeginYearModel>>(dataSumary.Where(n => !n.IsLocked));
                }
            }
            else
            {
                DataPlanSummary = new ObservableCollection<PlanBeginYearModel>();
            }

            foreach (PlanBeginYearModel model in DataPlan)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
            }

            foreach (PlanBeginYearModel model in DataPlanSummary)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
                model.TypeIcon = model.IsSent ? "CheckBold" : "CancelBold";
            }

            if (DataPlan != null && DataPlan.Count > 0)
            {
                DataPlan.Select(n => { n.Stt = DataPlan.IndexOf(n) + 1; n.TenLoaiNganSach = SelectedLoaiNganSach != null ? SelectedLoaiNganSach.DisplayItem : string.Empty; return n; }).ToList();
            }

            if (DataPlanSummary != null && DataPlanSummary.Count > 0)
            {
                DataPlanSummary.Select(n => { n.Stt = DataPlanSummary.IndexOf(n) + 1; n.TenLoaiNganSach = SelectedLoaiNganSach != null ? SelectedLoaiNganSach.DisplayItem : string.Empty; return n; }).ToList();

                PlanBeginYearModel donviLoai0 = DataPlanSummary.FirstOrDefault(n => n.Loai == LoaiDonVi.ROOT);
                if (donviLoai0 != null)
                {
                    if (IsLockButton)
                    {
                        List<NsDtdauNamChungTuChiTiet> listData = _sktSoLieuService.FindDataDonViLoai0ByCondition
                            (_sessionService.Current.YearOfWork, LoaiChungTu, _sessionService.Current.IdDonVi).ToList();
                        double soDuToan = 0;

                        if (LoaiChungTu == VoucherType.NSSD_Key)
                        {
                            soDuToan = listData.Sum(x => x.FTuChi);
                        }
                        else if (LoaiChungTu == VoucherType.NSBD_Key)
                        {
                            soDuToan = listData.Sum(x => x.FHangNhap) + listData.Sum(x => x.FHangMua) +
                                       listData.Sum(x => x.FPhanCap);
                        }
                    }
                    else
                    {
                        double soDuToan;
                        if (IsDonViC4Only)
                        {
                            soDuToan = DataPlan.Where(x => x.Loai != LoaiDonVi.ROOT).Sum(x => x.SoDuToan);
                        }
                        else
                        {
                            soDuToan = DataPlan.Sum(x => x.SoDuToan);
                        }
                    }

                    foreach (var dv in DataPlan)
                    {
                        dv.Tang = 0;
                        dv.Giam = 0;
                        if (dv.SoDuToan > dv.SoKiemTra)
                            dv.Tang = dv.SoDuToan - dv.SoKiemTra;
                        if (dv.SoDuToan < dv.SoKiemTra)
                            dv.Giam = dv.SoKiemTra - dv.SoDuToan;
                    }
                }
            }

            if (DataPlan != null && DataPlan.Count > 0)
            {
                SelectedPlan = DataPlan.FirstOrDefault();
            }

            if (DataPlanSummary != null && DataPlanSummary.Count > 0)
            {
                SelectedPlanSummary = DataPlanSummary.FirstOrDefault();
            }
            OnPropertyChanged(nameof(IsEnableButtonChiTiet));
            OnPropertyChanged(nameof(IsButtonEnable));
            OnPropertyChanged(nameof(IsEnableButtonExportExcel));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(PlanBeginYearModel.Selected))
            {
                OnPropertyChanged(nameof(IsEnableButtonExport));
                OnPropertyChanged(nameof(IsButtonEnable));
                OnPropertyChanged(nameof(IsLockButton));
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsEnableButtonExportExcel));
            }

            if (args.PropertyName == nameof(PlanBeginYearModel.IsCollapse))
            {
                ExpandChild();
            }
        }

        private void ExpandChild()
        {
            if (SelectedPlanSummary != null)
            {
                DataPlanSummary.Where(n => n.SoChungTuParent == SelectedPlanSummary.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
            OnPropertyChanged(nameof(DataPlanSummary));
        }

        private List<NguoiDungDonVi> GetListDonViPhanQuyen()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.IIDMaNguoiDung == _sessionService.Current.Principal);
            return _nsNguoiDungDonViService.FindAll(predicate).ToList();
        }

        private void OnExportAggregateDataDialog()
        {
            List<PlanBeginYearModel> listSelectedVoucher = new List<PlanBeginYearModel>();
            if (TabIndex == ImportTabIndex.Data)
                listSelectedVoucher = DataPlan.Where(x => x.Selected).ToList();
            else
                listSelectedVoucher = DataPlanSummary.Where(x => x.Selected).ToList();
            ExportPlanBeginYearViewModel.ListSelectedPlanVoucher = listSelectedVoucher;

            ExportPlanBeginYearViewModel.SelectedLoaiNganSach = SelectedLoaiNganSach;
            ExportPlanBeginYearViewModel.LoaiChungTu = LoaiChungTu;
            ExportPlanBeginYearViewModel._cap1 = _cap1;
            ExportPlanBeginYearViewModel.HeaderThucHien = HeaderThucHien;
            ExportPlanBeginYearViewModel.HeaderChiTiet = HeaderChiTiet;
            ExportPlanBeginYearViewModel.Init();
            var addView = new View.Budget.DemandCheck.Plan.ExportPlanBeginYear.ExportPlanBeginYear() { DataContext = ExportPlanBeginYearViewModel };
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }

        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();

                    int namLamViec = _sessionService.Current.YearOfWork;
                    List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
                    List<string> lstHeader = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                    List<bool> lstCheckData = new List<bool>() { false, false, false, false, false };

                    GetDataPlanBeginYearExport(ref lstHeader, ref lstCheckData);

                    DataPlanBeginYearExport = new ObservableCollection<SktSoLieuChiTietMLNSModel>(DataPlanBeginYearExport.Where(n => n.ChiTiet != 0 || n.UocThucHien != 0 || n.QuyetToan != 0 || n.DuToan != 0
                        || n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0
                        || n.FTuChi1 != 0 || n.FTuChi2 != 0 || n.FTuChi3 != 0 || n.FTuChi4 != 0 || n.FTuChi5 != 0
                        || n.FMHHV1 != 0 || n.FMHHV2 != 0 || n.FMHHV3 != 0 || n.FMHHV4 != 0 || n.FMHHV5 != 0
                        || n.FPhanCap1 != 0 || n.FPhanCap2 != 0 || n.FPhanCap3 != 0 || n.FPhanCap4 != 0 || n.FPhanCap5 != 0
                        ).ToList());


                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Header1", _cap1 != null ? _cap1.ToUpper() : "");
                    data.Add("Header2", _sessionService.Current.TenDonVi.ToUpper());

                    data.Add("Title1", lstHeader[0]);
                    data.Add("Title2", lstHeader[1]);
                    data.Add("Title3", lstHeader[2]);
                    data.Add("Title4", lstHeader[3]);
                    data.Add("Title5", lstHeader[4]);

                    data.Add("HeaderThucHien", HeaderThucHien);
                    data.Add("HeaderChiTiet", HeaderChiTiet);
                    data.Add("Items", DataPlanBeginYearExport);
                    data.Add("MLNS", mucLucNganSaches);
                    data.Add("NamLamViec", _sessionService.Current.YearOfWork);

                    data.Add("ThoiGian", string.Format("TP.Hà Nội, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));

                    string tenNganSach;
                    string templateFileName;
                    if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                    {
                        double tongTien = (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0) ? DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChiTiet).Sum() : 0;
                        data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                        tenNganSach = "NSSD_";
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSSD);
                    }
                    else
                    {
                        double tongTien = 0;
                        if (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0)
                        {
                            tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.HangNhap).Sum();
                            tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.HangMua).Sum();
                            tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.PhanCap).Sum();
                            tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChuaPhanCap).Sum();
                        }
                        data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                        var dataPhanCap = _soLieuChiTietPhanCapService.GetSoLieuChiTietPhanCapDonVi0_1(_sessionService.Current.YearOfWork, SelectedPlanSummary.Id).ToList();
                        ObservableCollection<SktSoLieuPhanCapModel> dataPhanCapExport = _mapper.Map<ObservableCollection<SktSoLieuPhanCapModel>>(dataPhanCap);
                        double tongTienPhanCap = (dataPhanCap != null && dataPhanCap.Count > 0) ? dataPhanCap.Where(n => !n.bHangCha).Select(n => n.TuChi).Sum() : 0;
                        data.Add("TongTienBangChuPhanCap", StringUtils.NumberToText(tongTienPhanCap, true));
                        //if (dataPhanCapExport == null || !dataPhanCapExport.Any())
                        //{
                        //    MessageBoxHelper.Warning(string.Format("Không có dữ liệu phân cấp"));
                        //    return;
                        //}

                        data.Add("ItemPhanCaps", dataPhanCapExport);
                        tenNganSach = "NSBD_";
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSBD);
                    }
                    string fileNamePrefix = string.Format("rptDuToan_DauNam_ChungTu_TongHop_{0}", tenNganSach);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);

                    if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                    {
                        List<int> columnHidden = new List<int>();
                        int startIndex = 13;
                        for (int i = 0; i < 5; i++)
                        {
                            if (!lstCheckData[i])
                            {
                                columnHidden.Add(startIndex);
                            }
                            startIndex += 1;
                        }

                        var xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach>(templateFileName, data, columnHidden);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        List<int> columnHidden = new List<int>();
                        int startIndex = 13;
                        for (int i = 0; i < 5; i++)
                        {
                            if (!lstCheckData[i])
                            {
                                columnHidden.AddRange(new List<int> { startIndex, startIndex + 1 });
                            }
                            startIndex += 2;
                        }

                        var xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach, SktSoLieuPhanCapModel>(templateFileName, data, columnHidden);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;

                        if (result != null)
                        {
                            _exportService.Open(result, ExportType.EXCEL);
                        }
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

        private string GetIdChungTuChild(string idDonVi)
        {
            var predicateSummary = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicateSummary = predicateSummary.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicateSummary = predicateSummary.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicateSummary = predicateSummary.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicateSummary = predicateSummary.And(x => x.IIdMaDonVi == idDonVi);
            predicateSummary = predicateSummary.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            NsDtdauNamChungTu chungTu = new NsDtdauNamChungTu();
            chungTu = _sktChungTuService.FindByCondition(predicateSummary).FirstOrDefault();
            List<string> listChungTu = new List<string>();
            if (chungTu != null && !string.IsNullOrEmpty(chungTu.SDSSoChungTuTongHop))
            {
                listChungTu = chungTu.SDSSoChungTuTongHop.Split(",").ToList();
            }
            if (chungTu != null)
            {
                if (listChungTu.Count > 0)
                {
                    var predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                    predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => listChungTu.Contains(x.SSoChungTu));
                    predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                    List<NsDtdauNamChungTu> chungTuChild = _sktChungTuService.FindByCondition(predicate).ToList();
                    if (chungTuChild.Count > 0)
                    {
                        return string.Join(",", chungTuChild.Select(n => n.Id.ToString()).ToList());
                    }
                }
            }
            return string.Empty;
        }

        public void GetDataPlanBeginYearExport(ref List<string> lstHeader, ref List<bool> lstCheckData)
        {
            List<SktSoLieuChiTietMlnsQuery> dataPlanExport = _sktSoLieuService.FindByConditionDonVi0(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
             _sessionService.Current.Budget, 0, 0, SelectedPlanSummary.Id_DonVi, LoaiChungTu, SelectedPlanSummary.Id.ToString(), SelectedPlanSummary.DsLNS).ToList();
            //DataPlanBeginYearExport = _mapper.Map<ObservableCollection<Model.SktSoLieuChiTietMLNSModel>>(dataPlanExport);
            ObservableCollection<SktSoLieuChiTietMLNSModel> data = _mapper.Map<ObservableCollection<SktSoLieuChiTietMLNSModel>>(dataPlanExport);
            LoadDataCanCuExport(ref data, ref lstHeader, ref lstCheckData);
            DataPlanBeginYearExport = new ObservableCollection<SktSoLieuChiTietMLNSModel>(data.ToList());
        }

        private void CalculateData(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data)
        {
            data.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.ChiTiet = 0; x.UocThucHien = 0; x.HangMua = 0; x.HangNhap = 0; x.PhanCap = 0; x.ChuaPhanCap = 0;
                    x.FTuChi1 = 0; x.FTuChi2 = 0; x.FTuChi3 = 0; x.FTuChi4 = 0; x.FTuChi5 = 0;
                    return x;
                }).ToList();
            foreach (var item in data.Where(x => !x.IsHangCha &&
            (x.ChiTiet != 0 || x.UocThucHien != 0 || x.HangMua != 0 || x.HangNhap != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0
             || x.FTuChi1 != 0 || x.FTuChi2 != 0 || x.FTuChi3 != 0 || x.FTuChi4 != 0 || x.FTuChi5 != 0)))
            {
                CalculateParent(ref data, item, item);
            }
        }

        private void CalculateParent(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data, SktSoLieuChiTietMLNSModel currentItem, SktSoLieuChiTietMLNSModel selfItem)
        {
            var parentItem = data.FirstOrDefault(x => x.MlnsId == currentItem.MlnsIdParent);
            if (parentItem == null) return;
            parentItem.ChiTiet += selfItem.ChiTiet;
            parentItem.UocThucHien += selfItem.UocThucHien;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;
            parentItem.FTuChi1 += selfItem.FTuChi1;
            parentItem.FTuChi2 += selfItem.FTuChi2;
            parentItem.FTuChi3 += selfItem.FTuChi3;
            parentItem.FTuChi4 += selfItem.FTuChi4;
            parentItem.FTuChi5 += selfItem.FTuChi5;
            CalculateParent(ref data, parentItem, selfItem);
        }

        private void OnImportData()
        {
            try
            {
                ImportPlanBeginYearViewModel.LoaiChungTu = LoaiChungTu;
                ImportPlanBeginYearViewModel.Init();
                ImportPlanBeginYearViewModel.SavedAction = obj =>
                {
                    DuToanDauNamDonViChungTu modelImport = (DuToanDauNamDonViChungTu)obj;
                    if (modelImport.LoaiChungTu == VoucherType.NSSD_Key)
                    {
                        SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault(n => n.ValueItem == VoucherType.NSSD_Key);
                    }
                    else
                    {
                        SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault(n => n.ValueItem == VoucherType.NSBD_Key);
                    }
                    if (!IsLoading) OnRefresh();
                    SelectedPlan = DataPlan != null ? DataPlan.FirstOrDefault(n => n.Id_DonVi == modelImport.IdDonVi) : null;
                    _importView.Close();
                    //OnShowPopupDetail(true, null);
                };
                _importView = new ImportPlanBeginYear { DataContext = ImportPlanBeginYearViewModel };
                _importView.Show();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private async void OnUploadDialog(bool isSendHTTP)
        {
            if (DataPlanSummary == null || !DataPlanSummary.Any(n => n.Selected) || DataPlanSummary.Where(x => x.Selected).Count() > 1)
            {
                MessageBox.Show("Vui lòng chọn 1 bản ghi !".ToString());
                return;
            }
            IsLoading = true;
            try
            {
                var info = await _hTTPUploadFileService.GetToken(isSendHTTP);
                if (info.Item1 != 200)
                {
                    IsLoading = false;
                    new NSMessageBoxViewModel(info.Item2).ShowDialogHost();
                    return;
                }
                else if (string.IsNullOrEmpty(info.Item2))
                {
                    IsLoading = false;
                    new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                    return;
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                IsLoading = false;
                new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                return;
            }
            SendDataPlanBeginYearViewModel.DataPlanSummary = DataPlanSummary;
            SendDataPlanBeginYearViewModel.SelectedPlanSummary = SelectedPlanSummary;
            //ExportPlanBeginYearViewModel.DataPlanBeginYearExport = DataPlanBeginYearExport.ToList();
            SendDataPlanBeginYearViewModel.SelectedLoaiNganSach = SelectedLoaiNganSach;
            SendDataPlanBeginYearViewModel.LoaiChungTu = LoaiChungTu;
            SendDataPlanBeginYearViewModel._cap1 = _cap1;
            SendDataPlanBeginYearViewModel.HeaderThucHien = HeaderThucHien;
            SendDataPlanBeginYearViewModel.HeaderChiTiet = HeaderChiTiet;
            SendDataPlanBeginYearViewModel.SelectedPlan = SelectedPlan;
            SendDataPlanBeginYearViewModel.IsSendHTTP = isSendHTTP;
            SendDataPlanBeginYearViewModel.Init();
            SendDataPlanBeginYearViewModel.ClosePopup += RefreshAfterClosePopupSendData;
            var addView = new View.Budget.DemandCheck.Plan.SendDataPlanBeginYear.SendDataPlanBeginYear() { DataContext = SendDataPlanBeginYearViewModel };
            IsLoading = false;
            await DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
            SendDataPlanBeginYearViewModel.ClosePopup -= RefreshAfterClosePopupSendData;
        }

        private void RefreshAfterClosePopupSendData(object sender, EventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            if (!IsLoading) OnRefresh();
        }

        private void OnUpload()
        {

            if (DataPlanSummary == null || !DataPlanSummary.Any(n => n.Selected) || DataPlanSummary.Where(x => x.Selected).Count() > 1)
            {
                System.Windows.MessageBox.Show("Vui lòng chọn 1 bản ghi !".ToString());
                return;
            }
            int namLamViec = _sessionService.Current.YearOfWork;
            List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
            List<string> lstHeader = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
            List<bool> lstCheckData = new List<bool>() { false, false, false, false, false };

            GetDataPlanBeginYearExport(ref lstHeader, ref lstCheckData);

            DataPlanBeginYearExport = new ObservableCollection<SktSoLieuChiTietMLNSModel>(DataPlanBeginYearExport.Where(n => n.ChiTiet != 0 || n.UocThucHien != 0 || n.QuyetToan != 0 || n.DuToan != 0
                || n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0 || n.FTuChi1 != 0 || n.FTuChi2 != 0 || n.FTuChi3 != 0 || n.FTuChi4 != 0 || n.FTuChi5 != 0).ToList());

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Header1", _cap1 != null ? _cap1.ToUpper() : "");
            data.Add("Header2", _sessionService.Current.TenDonVi.ToUpper());

            data.Add("Title1", lstHeader[0]);
            data.Add("Title2", lstHeader[1]);
            data.Add("Title3", lstHeader[2]);
            data.Add("Title4", lstHeader[3]);
            data.Add("Title5", lstHeader[4]);

            data.Add("HeaderThucHien", HeaderThucHien);
            data.Add("HeaderChiTiet", HeaderChiTiet);
            data.Add("Items", DataPlanBeginYearExport.Where(n => n.ChiTiet != 0 || n.UocThucHien != 0 || n.QuyetToan != 0 || n.DuToan != 0
            || n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0));
            data.Add("MLNS", mucLucNganSaches);
            data.Add("NamLamViec", _sessionService.Current.YearOfWork);
            double tongTien = (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0) ? DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChiTiet).Sum() : 0;
            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));
            data.Add("ThoiGian", string.Format("TP.Hà Nội, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));

            PlanBeginYearModel item = DataPlanSummary.FirstOrDefault(x => x.Selected);
            string tenNganSach;
            string templateFileName;
            var section = SelectedLoaiNganSach.ValueItem;
            if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
            {
                tenNganSach = "NSSD_";
                templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSSD);
            }
            else
            {
                tenNganSach = "NSBD_";
                templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSBD);
            }
            string fileNamePrefix = string.Format("rptDuToan_DauNam_ChungTu_TongHop_{0}", tenNganSach);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach>(templateFileName, data);
            var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            string sStage = string.Empty;
            if (SelectedPlan != null)
            {
                if (section.Equals("1"))
                {
                    sStage = StringUtils.UCS2Convert(VoucherType.NSSD_Value);
                }
                else
                {
                    sStage = StringUtils.UCS2Convert(VoucherType.NSBD_Value);
                }

            }
            string filePathLocal = string.Empty;
            _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
            string sNameUnit = string.Empty;
            sNameUnit = StringUtils.UCS2Convert(item.TenDonViDisplay).Replace("---", "-");
            string sFolderRoot = ConstantUrlPathPhanHe.UrlQlnsDtDnformReceive;
            var strUrl = string.Format("{0}/{1}/{2}", sNameUnit, sFolderRoot, sStage);
            if (!File.Exists(strUrl))
            {
                string strActiveFileName = "";
                string[] splitActiveFiName = xlsFile.ActiveFileName.Split("\\");
                if (strActiveFileName != null && splitActiveFiName.Length != 0)
                {
                    strActiveFileName = splitActiveFiName[splitActiveFiName.Length - 1];
                }
                VdtFtpRoot dataRoot = new VdtFtpRoot();
                List<string> configCodes = new List<string>()
                {
                    STORAGE_CONFIG.FTP_HOST
                };
                var rs = _danhMucService.FindByCodes(configCodes).ToList();
                var SIpAddress = rs.FirstOrDefault(x => STORAGE_CONFIG.FTP_HOST.Equals(x.IIDMaDanhMuc)).SGiaTri;
                // Lấy ra 1 bản ghi trong bảng VDT_FtpRoot để kiểm tra xem đã tồn tại hay chưa.
                dataRoot = _ftpService.GetVdtFtpRoot(item.Id_DonVi, SIpAddress, sFolderRoot);
                if (dataRoot == null)
                {
                    dataRoot = new VdtFtpRoot()
                    {
                        SMaDonVi = item.Id_DonVi,
                        SIpAddress = SIpAddress, // vd: ftp:\\10.60.108.246
                        SFolderRoot = sFolderRoot,
                        SNguoiTao = _sessionService.Current.Principal,
                        DNgayTao = DateTime.Now
                    };
                    _ftpService.Add(dataRoot);
                }
                var result = _ftpStorageService.UploadCommand(dataRoot.Id, filePathLocal, strActiveFileName, strUrl);
                if (result != 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Gửi dữ liệu thất bại");
                    System.Windows.MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                else
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Gửi dữ liệu thành công");
                    System.Windows.MessageBox.Show(messageBuilder.ToString());
                    return;
                }
            }

        }

        private void OnShowPopupReportChiThuongXuyen(object input)
        {
            try
            {
                PrintReportChiThuongXuyenQuocPhongViewModel.Init();
                PrintReportChiThuongXuyenQuocPhong view = new PrintReportChiThuongXuyenQuocPhong()
                {
                    DataContext = PrintReportChiThuongXuyenQuocPhongViewModel
                };
                DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupReportDuToanNganSah(object input)
        {
            try
            {
                PrintReportDuToanNganSachViewModel.Init();
                PrintReportDuToanNganSachViewModel.VoucherTypes = SelectedLoaiNganSach;
                PrintReportDuToanNganSach view = new PrintReportDuToanNganSach()
                {
                    DataContext = PrintReportDuToanNganSachViewModel
                };
                DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupCompareSKTDTDN(object input)
        {
            try
            {
                PrintReportCompareSKTDTDNViewModel.Init();
                PrintReportCompareSKTDTDNViewModel.VoucherTypes = SelectedLoaiNganSach;
                PrintReportCompareSKTDTDNViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupReportChiTietTheoNganh(object input)
        {
            try
            {
                PrintReportChiTietDuToanTheoNganhViewModel.Init();
                PrintReportChiTietDuToanTheoNganh view = new PrintReportChiTietDuToanTheoNganh()
                {
                    DataContext = PrintReportChiTietDuToanTheoNganhViewModel
                };
                DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupReportChiTiet(object input)
        {
            try
            {
                switch (input.ToString())
                {
                    case LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI:
                        PrintReportChiTietDuToanDonViViewModel.TypeReport = LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI;
                        break;
                    case LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN:
                        PrintReportTongHopDuToanViewModel.TypeReport = LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN;
                        break;
                    case LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN:
                        PrintReportChiTietDuToanDonViViewModel.TypeReport = LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN;
                        break;
                    default:
                        break;
                }

                if (input.ToString().Equals(LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN))
                {
                    PrintReportTongHopDuToanViewModel.LoaiChungTu = LoaiChungTu;
                    PrintReportTongHopDuToanViewModel.Init();
                    PrintReportTongHopDuToan view = new PrintReportTongHopDuToan()
                    {
                        DataContext = PrintReportTongHopDuToanViewModel
                    };
                    DialogHost.Show(view, "RootDialog", null, null);
                }
                else
                {
                    PrintReportChiTietDuToanDonViViewModel.LoaiChungTu = LoaiChungTu;
                    PrintReportChiTietDuToanDonViViewModel.Init();
                    PrintReportChiTietDuToanDonVi view = new PrintReportChiTietDuToanDonVi()
                    {
                        DataContext = PrintReportChiTietDuToanDonViViewModel
                    };
                    DialogHost.Show(view, "RootDialog", null, null);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnShowPopupReportCompare()
        {
            try
            {
                PrintReportCompareDemandCheckViewModel.LoaiChungTu = LoaiChungTu;
                PrintReportCompareDemandCheckViewModel.Init();
                PrintReportCompareDemandCheck view = new PrintReportCompareDemandCheck()
                {
                    DataContext = PrintReportCompareDemandCheckViewModel
                };
                var result = DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDataCanCuExport(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data, ref List<string> lstHeader, ref List<bool> lstCheckData)
        {
            var loaiChungTu = LoaiChungTu;
            int yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
            if (listCanCu == null) return;
            int DuToanNamTruocIndex = -1;
            int count = 0;
            foreach (var item in listCanCu)
            {
                if (item.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE) && item.INamCanCu == yearOfWork - 1)
                {
                    DuToanNamTruocIndex = count;
                }
                var predicateCc = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                predicateCc = predicateCc.And(x => x.IIdMaDonVi.Equals(SelectedPlanSummary.Id_DonVi));
                predicateCc = predicateCc.And(x => x.IIdCanCu.HasValue && x.IIdCanCu.Equals(item.Id));
                predicateCc = predicateCc.And(x => x.IID_CTDTDauNam == SelectedPlanSummary.Id);
                //predicateCc = predicateCc.And(x => x.LoaiChungTu == loaiChungTu);
                var lstCanCu = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCc);
                lstCheckData[count] = true;
                lstHeader[count] = item.STenCot;
                foreach (var cc in lstCanCu)
                {
                    var mucLuc = data.FirstOrDefault(x => x.XauNoiMa == cc.SXauNoiMa);
                    if (mucLuc != null)
                    {

                        if (count == 0)
                        {
                            // Lay so lieu
                            mucLuc.FTuChi1 = cc.FTuChi;
                            mucLuc.FPhanCap1 = cc.FPhanCap;
                            mucLuc.FMHHV1 = cc.FHangNhap + cc.FHangMua;

                        }

                        if (count == 1)
                        {
                            // Lay so lieu
                            mucLuc.FTuChi2 = cc.FTuChi;
                            mucLuc.FPhanCap2 = cc.FPhanCap;
                            mucLuc.FMHHV2 = cc.FHangNhap + cc.FHangMua;
                        }

                        if (count == 2)
                        {
                            // Lay so lieu
                            mucLuc.FTuChi3 = cc.FTuChi;
                            mucLuc.FPhanCap3 = cc.FPhanCap;
                            mucLuc.FMHHV3 = cc.FHangNhap + cc.FHangMua;
                        }

                        if (count == 3)
                        {
                            // Lay so lieu
                            mucLuc.FTuChi4 = cc.FTuChi;
                            mucLuc.FPhanCap4 = cc.FPhanCap;
                            mucLuc.FMHHV4 = cc.FHangNhap + cc.FHangMua;
                        }

                        if (count == 4)
                        {
                            // Lay so lieu
                            mucLuc.FTuChi5 = cc.FTuChi;
                            mucLuc.FPhanCap5 = cc.FPhanCap;
                            mucLuc.FMHHV5 = cc.FHangNhap + cc.FHangMua;
                        }
                    }
                }
                count++;
            }
            CalculateData(ref data);
        }

        #region Json
        private void OnImportJson()
        {
            PlanBeginYearImportJsonViewModel.Init();
            PlanBeginYearImportJsonViewModel.SavedAction = obj =>
            {
                if (!IsLoading) OnRefresh();
                _importJsonView.Close();
            };
            _importJsonView = new PlanBeginYearImportJson { DataContext = PlanBeginYearImportJsonViewModel };
            _importJsonView.Show();
        }

        private void OnExportJson()
        {
            if (!DataPlan.Any(n => n.Selected))
            {
                MessageBoxHelper.Error(Resources.MsgRecordEmpty);
                return;
            }
            List<NsDtdauNamChungTu> lstData = GetDataExportJson();
            _exportService.OpenJson(lstData);
        }

        private List<NsDtdauNamChungTu> GetDataExportJson()
        {
            List<NsDtdauNamChungTu> lstData = new List<NsDtdauNamChungTu>();
            if (!DataPlan.Any(n => n.Selected)) return lstData;
            List<Guid> lstIdChungTu = DataPlan.Where(n => n.Selected).Select(n => n.Id).ToList();
            return _sktChungTuService.GetDataExportJson(lstIdChungTu); ;
        }


        #endregion
    }
}
