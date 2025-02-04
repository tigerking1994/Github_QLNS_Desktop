using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PlanAgency
{
    public class PlanAgencyBeginYearIndexViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private INsDonViService _nsDonViService;
        private ISktSoLieuService _sktSoLieuService;
        private VTS.QLNS.CTC.App.View.Budget.DemandCheck.PlanAgency.PlanBeginYearDetailAgency view;
        private IExportService _exportService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISktSoLieuChungTuService _sktChungTuService;
        private ImportPlanBeginYear _importView;
        private DanhMucCauHinhHeThongService _danhMucCauHinhHeThongService;
        private readonly ILog _logger;
        private INsNguoiDungDonViService _nsNguoiDungDonViService;
        private ISktChungTuService _iSktChungTuService;
        private ICollectionView _summaryView;

        public override string FuncCode => NSFunctionCode.BUDGET_DEMANDCHECK_PLAN;
        public override string Name => "Lập dự toán đầu năm";
        public override Type ContentType => typeof(View.Budget.DemandCheck.PlanAgency.PlanAgencyBeginYearIndex);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxTickOutline;
        public override string Title => "CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork.ToString();
        public override string Description => "CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork.ToString();
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

        public PlanBeginYearDetailAgencyViewModel PlanBeginYearDetailAgencyViewModel { get; }
        public PrintReportChiTietDuToanDonViViewModel PrintReportChiTietDuToanDonViViewModel { get; }
        public PrintReportCompareDemandCheckViewModel PrintReportCompareDemandCheckViewModel { get; }
        public ImportPlanBeginYearViewModel ImportPlanBeginYearViewModel;
        public PlanBeginYearDialogViewModel PlanBeginYearDialogViewModel { get; }
        public PlanBeginYearSummaryViewModel PlanBeginYearSummaryViewModel { get; }

        public DonVi DonviLoai0;
        public bool IsDonViC4Only = false;
        public bool IsEdit => (TabIndex == ImportTabIndex.Data) ? (SelectedPlan != null && !SelectedPlan.IsLocked) : (SelectedPlanSummary != null && !SelectedPlanSummary.IsLocked);

        private ObservableCollection<VTS.QLNS.CTC.App.Model.PlanBeginYearModel> _dataPlan;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.PlanBeginYearModel> DataPlan
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

        private ObservableCollection<VTS.QLNS.CTC.App.Model.PlanBeginYearModel> _dataPlanSummary;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.PlanBeginYearModel> DataPlanSummary
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

        private static void SelectAll(bool select, IEnumerable<VTS.QLNS.CTC.App.Model.PlanBeginYearModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        public bool IsEnableButtonExport => DataPlan != null && DataPlan.Where(n => n.Selected && n.IsLocked).Count() > 0;

        public bool IsEnableButtonExportExcel => (TabIndex != null && TabIndex == ImportTabIndex.Data) ? (_selectedPlan != null && _selectedPlan.Loai == LoaiDonVi.ROOT) : (_selectedPlanSummary != null && _selectedPlanSummary.Loai == LoaiDonVi.ROOT);

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
                    OnRefesh();
                }
            }
        }

        public bool IsEnableButtonChiTiet
        {
            get => DataPlan != null && DataPlan.Count() > 0;
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        public bool IsLockButton => TabIndex == ImportTabIndex.Data ? (SelectedPlan != null && SelectedPlan.IsLocked) : (SelectedPlanSummary != null && SelectedPlanSummary.IsLocked);

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
                OnPropertyChanged(nameof(IsLockButton));
                OnPropertyChanged(nameof(SelectedPlan));
                OnPropertyChanged(nameof(SelectedPlanSummary));
                OnPropertyChanged(nameof(IsEnableButtonExportExcel));
                OnPropertyChanged(nameof(IsEnableButtonExport));
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

        public PlanAgencyBeginYearIndexViewModel(IMapper mapper,
           ISessionService sessionService,
           INsDonViService nsDonViService,
           ISktSoLieuService sktSoLieuService,
           IExportService exportService,
           ISktSoLieuChungTuService sktChungTuService,
           ILog logger,
           INsMucLucNganSachService mucLucNganSachService,
           INsNguoiDungDonViService nsNguoiDungDonViService,
           ISktChungTuService iSktChungTuService,
           ISktChungTuChiTietService sktChungTuChiTietService,
           PlanBeginYearDetailAgencyViewModel planBeginYearDetailAgencyViewModel,
           PrintReportChiTietDuToanDonViViewModel printReportChiTietDuToanDonViViewModel,
           DanhMucCauHinhHeThongService danhMucCauHinhHeThongService,
           PrintReportCompareDemandCheckViewModel printReportCompareDemandCheckViewModel,
           PlanBeginYearDialogViewModel planBeginYearDialogViewModel,
           PlanBeginYearSummaryViewModel planBeginYearSummaryViewModel,
           ImportPlanBeginYearViewModel importPlanBeginYearViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _sktSoLieuService = sktSoLieuService;
            _exportService = exportService;
            _sktChungTuService = sktChungTuService;
            _mucLucNganSachService = mucLucNganSachService;
            _logger = logger;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _iSktChungTuService = iSktChungTuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            ImportPlanBeginYearViewModel = importPlanBeginYearViewModel;
            PlanBeginYearDetailAgencyViewModel = planBeginYearDetailAgencyViewModel;
            PrintReportCompareDemandCheckViewModel = printReportCompareDemandCheckViewModel;
            PrintReportChiTietDuToanDonViViewModel = printReportChiTietDuToanDonViViewModel;
            PlanBeginYearSummaryViewModel = planBeginYearSummaryViewModel;
            PlanBeginYearDialogViewModel = planBeginYearDialogViewModel;
            _danhMucCauHinhHeThongService = danhMucCauHinhHeThongService;
            RefeshCommand = new RelayCommand(o => OnRefesh());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
            ShowPopupDetailCommand = new RelayCommand(o => OnShowPopupDetail(true, null));
            ShowPopupDetailSummaryCommand = new RelayCommand(o => OnShowPopupDetailSummary());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ShowPopupReportChiTietCommand = new RelayCommand(obj => OnShowPopupReportChiTiet(obj));
            ShowPopupReportCompareCommand = new RelayCommand(obj => OnShowPopupReportCompare());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            DeleteCommand = new RelayCommand(obj => OnDelete());
            ShowPopupAddCommand = new RelayCommand(o => OnShowPopupAdd());
            ShowPopupEditCommand = new RelayCommand(o => OnShowPopupEdit());
            SummaryProcessCommand = new RelayCommand(o => OnShowPopupSummary());
        }

        private async void OnShowPopupSummary()
        {
            try
            {
                //check quyền được tổng hợp
                if (DataPlan == null || DataPlan.Where(n => n.IsLocked && n.Selected).Count() == 0)
                {
                    return;
                }

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    System.Windows.MessageBox.Show(Resources.MsgRoleSummary, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (DataPlan.Where(n => !n.IsLocked && n.Selected).Count() > 0)
                {
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(Resources.MsgConfirmSummary, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        PlanBeginYearSummaryViewModel.Model = new Model.PlanBeginYearModel();
                        PlanBeginYearSummaryViewModel.LoaiChungTu = LoaiChungTu;
                        PlanBeginYearSummaryViewModel.DataPlan = new ObservableCollection<PlanBeginYearModel>(DataPlan.Where(n => n.IsLocked && n.Selected).ToList());
                        PlanBeginYearSummaryViewModel.Init();
                        PlanBeginYearSummaryViewModel.SavedAction = obj =>
                        {
                            this.OnRefesh();
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
                    PlanBeginYearSummaryViewModel.Model = new Model.PlanBeginYearModel();
                    PlanBeginYearSummaryViewModel.LoaiChungTu = LoaiChungTu;
                    PlanBeginYearSummaryViewModel.DataPlan = new ObservableCollection<PlanBeginYearModel>(DataPlan.Where(n => n.IsLocked && n.Selected).ToList());
                    PlanBeginYearSummaryViewModel.Init();
                    PlanBeginYearSummaryViewModel.SavedAction = obj =>
                    {
                        this.OnRefesh();
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
                //if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                //{
                //    System.Windows.MessageBox.Show(Resources.MsgRoleAdd, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}
                PlanBeginYearDialogViewModel.Model = new Model.PlanBeginYearModel();
                PlanBeginYearDialogViewModel.Init();
                PlanBeginYearDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefesh();
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

        private async void OnShowPopupEdit()
        {
            try
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (SelectedPlan != null)
                    {
                        this.PlanBeginYearDialogViewModel.Model = SelectedPlan;
                        PlanBeginYearDialogViewModel.Init();
                        PlanBeginYearDialogViewModel.SavedAction = obj =>
                        {
                            this.OnRefesh();
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
                        this.PlanBeginYearDialogViewModel.Model = SelectedPlanSummary;
                        PlanBeginYearDialogViewModel.Init();
                        PlanBeginYearDialogViewModel.SavedAction = obj =>
                        {
                            this.OnRefesh();
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

        public override void Init()
        {
            try
            {
                TabIndex = ImportTabIndex.Data;
                DonviLoai0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                LoadCombobox();
                List<DonVi> listDonVi = _nsDonViService.FindByLoai(_sessionService.Current.YearOfWork, LoaiDonVi.NOI_BO).ToList();
                IsDonViC4Only = (listDonVi != null && listDonVi.Count > 0);
                OnRefesh();
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Description));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void GetLockStatus()
        {
            if (DataPlan != null && DataPlan.Count > 0 && DataPlan.Where(n => n.Loai == LoaiDonVi.ROOT).FirstOrDefault() != null)
            {
                IsLock = _sktSoLieuService.IsLockDonViStatus(DataPlan.Where(n => n.Loai == LoaiDonVi.ROOT).FirstOrDefault().Id_DonVi,
                    _sessionService.Current.YearOfWork, LoaiChungTu, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget);
            }
            OnPropertyChanged(nameof(IsLock));
        }

        private void OnShowPopupDetailSummary()
        {
            try
            {
                if (SelectedPlanSummary == null)
                    return;
                PlanBeginYearDetailAgencyViewModel.Model = SelectedPlanSummary;
                PlanBeginYearDetailAgencyViewModel.LoaiChungTu = LoaiChungTu;
                PlanBeginYearDetailAgencyViewModel.LstCanCu = null;
                PlanBeginYearDetailAgencyViewModel.IsGetDonVi0Lock = SelectedPlanSummary.Loai == LoaiDonVi.ROOT;
                PlanBeginYearDetailAgencyViewModel.IsLock = IsLock;
                PlanBeginYearDetailAgencyViewModel.IsReadOnlyData = SelectedPlanSummary.IsLocked;
                PlanBeginYearDetailAgencyViewModel.ListNguoiDungDonVi = GetListDonViPhanQuyen();
                PlanBeginYearDetailAgencyViewModel.Init();
                view = new VTS.QLNS.CTC.App.View.Budget.DemandCheck.PlanAgency.PlanBeginYearDetailAgency
                {
                    DataContext = PlanBeginYearDetailAgencyViewModel
                };
                PlanBeginYearDetailAgencyViewModel.ClosePopup += RefreshAfterClosePopup;
                PlanBeginYearDetailAgencyViewModel.RefeshIndexWindow += RefreshGrid;
                view.ShowDialog();
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
                    PlanBeginYearDetailAgencyViewModel.Model = SelectedPlan;
                    PlanBeginYearDetailAgencyViewModel.LoaiChungTu = LoaiChungTu;
                    PlanBeginYearDetailAgencyViewModel.LstCanCu = null;
                    PlanBeginYearDetailAgencyViewModel.IsGetDonVi0Lock = SelectedPlan.Loai == LoaiDonVi.ROOT;
                    PlanBeginYearDetailAgencyViewModel.IsLock = IsLock;
                    PlanBeginYearDetailAgencyViewModel.IsReadOnlyData = SelectedPlan.IsLocked;
                    PlanBeginYearDetailAgencyViewModel.ListNguoiDungDonVi = GetListDonViPhanQuyen();
                    PlanBeginYearDetailAgencyViewModel.Init();
                    view = new VTS.QLNS.CTC.App.View.Budget.DemandCheck.PlanAgency.PlanBeginYearDetailAgency
                    {
                        DataContext = PlanBeginYearDetailAgencyViewModel
                    };
                    PlanBeginYearDetailAgencyViewModel.ClosePopup += RefreshAfterClosePopup;
                    PlanBeginYearDetailAgencyViewModel.RefeshIndexWindow += RefreshGrid;
                    view.ShowDialog();
                }
                else
                {
                    if (obj == null)
                        return;
                    PlanBeginYearDetailAgencyViewModel.Model = obj;
                    PlanBeginYearDetailAgencyViewModel.LoaiChungTu = LoaiChungTu;
                    PlanBeginYearDetailAgencyViewModel.LstCanCu = null;
                    PlanBeginYearDetailAgencyViewModel.IsGetDonVi0Lock = obj.Loai == LoaiDonVi.ROOT;
                    PlanBeginYearDetailAgencyViewModel.IsLock = IsLock;
                    PlanBeginYearDetailAgencyViewModel.IsReadOnlyData = obj.IsLocked;
                    PlanBeginYearDetailAgencyViewModel.ListNguoiDungDonVi = GetListDonViPhanQuyen();
                    PlanBeginYearDetailAgencyViewModel.Init();
                    view = new VTS.QLNS.CTC.App.View.Budget.DemandCheck.PlanAgency.PlanBeginYearDetailAgency
                    {
                        DataContext = PlanBeginYearDetailAgencyViewModel
                    };
                    PlanBeginYearDetailAgencyViewModel.ClosePopup += RefreshAfterClosePopup;
                    PlanBeginYearDetailAgencyViewModel.RefeshIndexWindow += RefreshGrid;
                    view.ShowDialog();
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
            OnRefesh();
        }

        private void RefreshGrid(object sender, EventArgs e)
        {
            OnRefesh();
        }

        public void OnLockUnLock()
        {
            try
            {
                var session = _sessionService.Current;
                DonVi donViTongHop = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (SelectedPlan == null)
                        return;

                    if (DataPlanSummary != null && DataPlanSummary.Count > 0)
                    {
                        var chungTuDonViTongHop = DataPlanSummary.Where(n => n.Loai == LoaiDonVi.ROOT).FirstOrDefault();
                        if (chungTuDonViTongHop != null && donViTongHop != null && donViTongHop.Loai == chungTuDonViTongHop.Loai && SelectedPlan.IsLocked)
                        {
                            if (!CheckCanUnLock(donViTongHop.IIDMaDonVi, SelectedPlan.Id_DonVi))
                            {
                                System.Windows.MessageBox.Show(Resources.MsgVoucherHasLock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }
                    }

                    //if (!SelectedPlan.IsLocked && SelectedPlan.NguoiTao != _sessionService.Current.Principal)
                    //{
                    //    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedPlan.NguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    //    return;
                    //}

                    if (!_sessionService.Current.IsQuanLyDonViCha & SelectedPlan.IsLocked)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgErrorUnLock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    HandlerLockDonVi(SelectedPlan);
                    if (!SelectedPlan.IsLocked)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgLockDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(Resources.MsgUnLockDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    IsLock = !IsLock;
                    OnPropertyChanged(nameof(IsLock));
                    OnRefesh();
                }
                else if (TabIndex == ImportTabIndex.MLNS)
                {
                    if (SelectedPlanSummary == null)
                        return;
                    if (DataPlanSummary != null && DataPlanSummary.Count > 0)
                    {
                        var chungTuDonViTongHop = DataPlanSummary.Where(n => n.Loai == LoaiDonVi.ROOT).FirstOrDefault();
                        if (chungTuDonViTongHop != null && donViTongHop != null && donViTongHop.Loai == chungTuDonViTongHop.Loai && SelectedPlanSummary.IsLocked)
                        {
                            if (!CheckCanUnLock(donViTongHop.IIDMaDonVi, SelectedPlanSummary.Id_DonVi))
                            {
                                System.Windows.MessageBox.Show(Resources.MsgVoucherHasLock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }
                    }
                    //if (!SelectedPlanSummary.IsLocked && SelectedPlanSummary.NguoiTao != _sessionService.Current.Principal)
                    //{
                    //    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedPlanSummary.NguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    //    return;
                    //}

                    if (!_sessionService.Current.IsQuanLyDonViCha && SelectedPlanSummary.IsLocked)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgErrorUnLock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    HandlerLockDonVi(SelectedPlanSummary);
                    if (!SelectedPlanSummary.IsLocked)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgLockDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(Resources.MsgUnLockDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    IsLock = !IsLock;
                    OnPropertyChanged(nameof(IsLock));
                    OnRefesh();
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
            if (listChungTu != null && listChungTu.Count > 0)
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
            }
            else
            {
                int soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork,
                    _sessionService.Current.Budget, _sessionService.Current.YearOfBudget, int.Parse(LoaiChungTu));
                NsDtdauNamChungTu entity = new NsDtdauNamChungTu();

                entity.SSoChungTu = "DTDN-" + soChungTuIndex.ToString("D3");
                entity.ISoChungTuIndex = soChungTuIndex;
                entity.IIdMaDonVi = SelectedPlan.Id_DonVi;
                entity.INamLamViec = _sessionService.Current.YearOfWork;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                entity.BKhoa = !SelectedPlan.IsLocked; ;
                entity.ILoaiChungTu = int.Parse(LoaiChungTu);
                entity.INamNganSach = _sessionService.Current.YearOfBudget;
                entity.IIdMaNguonNganSach = _sessionService.Current.Budget;
                _sktChungTuService.Add(entity);
            }
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set
            {
                SetProperty(ref _isCollapse, value);
                OnRefesh();
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
                List<NguoiDungDonVi> listNguoiDung = GetListDonViPhanQuyen();
                if (TabIndex == ImportTabIndex.Data)
                {
                    if (SelectedPlan == null)
                    {
                        return;
                    }
                    if (!listNguoiDung.Select(n => n.IIdMaDonVi).ToList().Contains(SelectedPlan.Id_DonVi))
                    {
                        System.Windows.MessageBox.Show(Resources.MsgErrorDelete, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(Resources.MsgDeleteRecord, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();

                        predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                        predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                        predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                        predicate = predicate.And(x => x.IIdMaDonVi == SelectedPlan.Id_DonVi);
                        predicate = predicate.And(x => x.ILoaiChungTu == LoaiChungTu);
                        List<NsDtdauNamChungTuChiTiet> listDelete = _sktSoLieuService.FindByCondition(predicate).ToList();
                        _sktSoLieuService.RemoveRange(listDelete);
                        _sktChungTuService.Delete(SelectedPlan.Id);
                        OnRefesh();
                    }
                }
                else
                {
                    if (SelectedPlanSummary == null)
                    {
                        return;
                    }
                    if (!listNguoiDung.Select(n => n.IIdMaDonVi).ToList().Contains(SelectedPlanSummary.Id_DonVi))
                    {
                        System.Windows.MessageBox.Show(Resources.MsgErrorDelete, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(Resources.MsgDeleteRecord, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var predicate = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();

                        predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                        predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                        predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                        predicate = predicate.And(x => x.IIdMaDonVi == SelectedPlanSummary.Id_DonVi);
                        predicate = predicate.And(x => x.ILoaiChungTu == LoaiChungTu);
                        List<NsDtdauNamChungTuChiTiet> listDelete = _sktSoLieuService.FindByCondition(predicate).ToList();
                        _sktSoLieuService.RemoveRange(listDelete);
                        _sktChungTuService.Delete(SelectedPlanSummary.Id);
                        OnRefesh();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnRefesh()
        {
            LoaiChungTu = string.Empty;
            if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
            {
                LoaiChungTu = VoucherType.NSSD_Key;
            }
            else
            {
                LoaiChungTu = VoucherType.NSBD_Key;
            }
            GetLockStatus();
            List<DonViPlanBeginYearQuery> data = _nsDonViService.FindPlanBeginYearAgencyByConditon
                (_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, LoaiChungTu, _sessionService.Current.Principal).ToList();

            //check user
            List<NguoiDungDonVi> listDonViByUserName = GetListDonViPhanQuyen();
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            if (listDonViByUserName != null && listDonViByUserName.Count > 0 && donvi0 != null)
            {
                NguoiDungDonVi itemNguoiDungDonVi = listDonViByUserName.Where(n => n.IIdMaDonVi == donvi0.IIDMaDonVi).FirstOrDefault();
                if (itemNguoiDungDonVi != null)
                {
                    List<string> listIdDonVi = listDonViByUserName.Select(n => n.IIdMaDonVi).ToList();
                    List<DonViPlanBeginYearQuery> listDataView = data.Where(n => !listIdDonVi.Contains(n.Id_DonVi) && !n.IsLocked).ToList();
                    listDataView.Select(n => { n.SoDuToan = 0; n.SoKiemTra = 0; n.Tang = 0; n.Giam = 0; return n; }).ToList();
                }
            }
            //Ngu them
            var ctSkt = _iSktChungTuService.FindByCondition(x => x.INamLamViec == _sessionService.Current.YearOfWork
                                                                 && x.ILoaiChungTu == int.Parse(LoaiChungTu)
                                                                 && x.ILoai == DemandCheckType.CHECK
                                                                 && x.BKhoa).ToList();
            if (ctSkt == null || ctSkt.Count <= 0)
            {
                data.Select(n => { n.SoKiemTra = 0; return n; }).ToList();
            }
            // end
            DataPlan = _mapper.Map<ObservableCollection<Model.PlanBeginYearModel>>(data.Where(n => n.Id_DonVi != donvi0.IIDMaDonVi));
            if (data.Where(n => n.Id_DonVi == donvi0.IIDMaDonVi).Count() > 0)
            {
                var itemChungTuTongHop = data.Where(n => n.Id_DonVi == donvi0.IIDMaDonVi).FirstOrDefault();
                DataPlan.Where(n => !string.IsNullOrEmpty(itemChungTuTongHop.DSDonViTongHop) &&
                itemChungTuTongHop.DSDonViTongHop.Split(",").ToList().Contains(n.Id_DonVi)).Select(n => { n.TrangThaiTongHop = "Đã tổng hợp"; return n; }).ToList();
                List<DonViPlanBeginYearQuery> dataSumary = new List<DonViPlanBeginYearQuery>();
                dataSumary.Add(itemChungTuTongHop);

                if (IsCollapse && !string.IsNullOrEmpty(itemChungTuTongHop.DSDonViTongHop))
                {
                    dataSumary.AddRange(data.Where(n => n.Loai == LoaiDonVi.NOI_BO && itemChungTuTongHop.DSDonViTongHop.Split(",").Contains(n.Id_DonVi)));


                }
                DataPlanSummary = _mapper.Map<ObservableCollection<Model.PlanBeginYearModel>>(dataSumary);
                DataPlanSummary.Where(n => n.Id != itemChungTuTongHop.Id).Select(n => { n.IsChildSumary = true; return n; }).ToList();
            }
            else
            {
                DataPlanSummary = new ObservableCollection<PlanBeginYearModel>();
            }
            //span
            //foreach (var item in DataPlanSummary)
            //{
            //    if (item.Id_DonVi == donvi0.IIDMaDonVi)
            //    {
            //        item.ParentGroup = "Chứng từ tổng hợp";
            //    }
            //    else
            //    {
            //        item.ParentGroup = "Tổng hợp từ";
            //    }
            //}

            //_summaryView = CollectionViewSource.GetDefaultView(DataPlanSummary);
            //_summaryView.GroupDescriptions.Add(new PropertyGroupDescription("ParentGroup"));

            foreach (PlanBeginYearModel model in DataPlan)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
            }

            foreach (PlanBeginYearModel model in DataPlanSummary)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
            }

            if (DataPlan != null && DataPlan.Count > 0)
            {
                DataPlan.Select(n => { n.Stt = DataPlan.IndexOf(n) + 1; n.TenLoaiNganSach = SelectedLoaiNganSach != null ? SelectedLoaiNganSach.DisplayItem : string.Empty; return n; }).ToList();
            }

            if (DataPlanSummary != null && DataPlanSummary.Count > 0)
            {
                DataPlanSummary.Select(n => { n.Stt = DataPlanSummary.IndexOf(n) + 1; n.TenLoaiNganSach = SelectedLoaiNganSach != null ? SelectedLoaiNganSach.DisplayItem : string.Empty; return n; }).ToList();

                PlanBeginYearModel donviLoai0 = DataPlanSummary.Where(n => n.Loai == LoaiDonVi.ROOT).FirstOrDefault();
                if (donviLoai0 != null)
                {
                    if (IsLock)
                    {
                        List<NsDtdauNamChungTuChiTiet> listData = _sktSoLieuService.FindDataDonViLoai0ByCondition
                            (_sessionService.Current.YearOfWork, LoaiChungTu, _sessionService.Current.IdDonVi).ToList();
                        double soDuToan = 0;
                        if (listData != null)
                        {
                            if (LoaiChungTu == VoucherType.NSSD_Key)
                            {
                                soDuToan = listData.Sum(x => x.FTuChi);
                            }
                            else if (LoaiChungTu == VoucherType.NSBD_Key)
                            {
                                soDuToan = listData.Sum(x => x.FHangNhap) + listData.Sum(x => x.FHangMua) + listData.Sum(x => x.FPhanCap);
                            }
                        }
                        donviLoai0.SoDuToan = soDuToan;
                    }
                    else
                    {
                        double soDuToan = 0;
                        if (IsDonViC4Only)
                        {
                            soDuToan = DataPlan.Where(x => x.Loai != LoaiDonVi.ROOT).Sum(x => x.SoDuToan);
                        }
                        else
                        {
                            soDuToan = DataPlan.Sum(x => x.SoDuToan);
                        }
                        donviLoai0.SoDuToan = soDuToan;
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
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(PlanBeginYearModel.Selected))
            {
                OnPropertyChanged(nameof(IsEnableButtonExport));
            }
        }

        private List<NguoiDungDonVi> GetListDonViPhanQuyen()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.IIDMaNguoiDung == _sessionService.Current.Principal);
            List<NguoiDungDonVi> listNguoiDungDonVi = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        private void OnExportAggregateData()
        {
            try
            {
                AuthenticationInfo authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
                List<DanhMuc> listDanhMuc = _danhMucCauHinhHeThongService.FindAll(authenticationInfo).ToList();
                DanhMuc donViCapTrenItem = (listDanhMuc != null && listDanhMuc.Count > 0) ? listDanhMuc.Where(n => n.STen == "Tên đơn vị Cấp trên").FirstOrDefault() : null;
                DanhMuc donViSuDung = (listDanhMuc != null && listDanhMuc.Count > 0) ? listDanhMuc.Where(n => n.STen == "Tên đơn vị sd phần mềm").FirstOrDefault() : null;
                string header1 = donViCapTrenItem != null ? donViCapTrenItem.SGiaTri : string.Empty;
                string header2 = donViSuDung != null ? donViSuDung.SGiaTri : string.Empty;
                int namLamViec = _sessionService.Current.YearOfWork;
                List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
                GetDataPlanBeginYearExport();

                DataPlanBeginYearExport = new ObservableCollection<SktSoLieuChiTietMLNSModel>(DataPlanBeginYearExport.Where(n => n.ChiTiet != 0 || n.UocThucHien != 0 || n.QuyetToan != 0 || n.DuToan != 0
                 || n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0).ToList());

                string tenNganSach = string.Empty;
                string templateFileName = string.Empty;
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
                var ext = FileExtensionFormats.Xlsx;
                using (var dialog = new SaveFileDialog())
                {
                    dialog.Title = Resources.MsgSaveFile;
                    dialog.RestoreDirectory = true;
                    dialog.FileName = "rptDuToan_DauNam_ChungTu_TongHop_" + tenNganSach + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                    dialog.Filter = IOExtensions.FileDialogFilterByExtension(ext);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        BackgroundWorkerHelper.Run((s, e) =>
                        {
                            IsLoading = true;
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("Header1", header1.ToUpper());
                            data.Add("Header2", header2.ToUpper());
                            data.Add("HeaderQuyetToan", HeaderQuyetToan);
                            data.Add("HeaderDuToan", HeaderDuToan);
                            data.Add("HeaderThucHien", HeaderThucHien);
                            data.Add("HeaderChiTiet", HeaderChiTiet);
                            data.Add("Items", DataPlanBeginYearExport.Where(n => n.ChiTiet != 0 || n.UocThucHien != 0 || n.QuyetToan != 0 || n.DuToan != 0
                            || n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0));
                            data.Add("MLNS", mucLucNganSaches);
                            data.Add("NamLamViec", _sessionService.Current.YearOfWork);
                            double tongTien = (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0) ? DataPlanBeginYearExport.Select(n => n.ChiTiet).Sum() : 0;
                            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));
                            data.Add("ThoiGian", string.Format("TP.Hà Nội, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));

                            string fileName = dialog.FileName;
                            var xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach>(templateFileName, data);
                            _exportService.ExportExcel(xlsFile, fileName);
                            e.Result = new ExportResult(fileName, xlsFile);
                        }, (s, e) =>
                        {
                            if (e.Error == null)
                            {
                                var result = (ExportResult)e.Result;
                                if (result != null)
                                {
                                    IOExtensions.OpenFiles(result.FileName);
                                }
                            }
                            else
                            {
                                _logger.Error(e.Error.Message);
                            }
                            IsLoading = false;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void GetDataPlanBeginYearExport()
        {
            //List<SktSoLieuChiTietMLNS> dataPlanExport = _sktSoLieuService.FindByConditionDonVi0(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
            // _sessionService.Current.Budget, 0, 0, SelectedPlanSummary.Id_DonVi, LoaiChungTu).ToList();
            ////DataPlanBeginYearExport = _mapper.Map<ObservableCollection<Model.SktSoLieuChiTietMLNSModel>>(dataPlanExport);
            //ObservableCollection<SktSoLieuChiTietMLNSModel> data = _mapper.Map<ObservableCollection<Model.SktSoLieuChiTietMLNSModel>>(dataPlanExport);
            //CalculateData(ref data);
            //DataPlanBeginYearExport = new ObservableCollection<SktSoLieuChiTietMLNSModel>(data.ToList());
        }

        private void CalculateData(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data)
        {
            data.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.ChiTiet = 0; x.UocThucHien = 0; x.HangMua = 0; x.HangNhap = 0; x.PhanCap = 0; x.ChuaPhanCap = 0;
                    return x;
                }).ToList();
            foreach (var item in data.Where(x => !x.IsHangCha && (x.ChiTiet != 0 || x.UocThucHien != 0 || x.HangMua != 0 || x.HangNhap != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0)))
            {
                CalculateParent(ref data, item, item);
            }
        }

        private void CalculateParent(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data, SktSoLieuChiTietMLNSModel currentItem, SktSoLieuChiTietMLNSModel selfItem)
        {
            var parentItem = data.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.ChiTiet += selfItem.ChiTiet;
            parentItem.UocThucHien += selfItem.UocThucHien;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;
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
                        SelectedLoaiNganSach = DataLoaiNganSach.Where(n => n.ValueItem == VoucherType.NSSD_Key).FirstOrDefault();
                    }
                    else
                    {
                        SelectedLoaiNganSach = DataLoaiNganSach.Where(n => n.ValueItem == VoucherType.NSBD_Key).FirstOrDefault();
                    }
                    OnRefesh();
                    SelectedPlan = DataPlan != null ? DataPlan.Where(n => n.Id_DonVi == modelImport.IdDonVi).FirstOrDefault() : null;
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
                        PrintReportChiTietDuToanDonViViewModel.TypeReport = LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN;
                        break;
                    default:
                        break;
                }
                PrintReportChiTietDuToanDonViViewModel.LoaiChungTu = LoaiChungTu;
                PrintReportChiTietDuToanDonViViewModel.Init();
                PrintReportChiTietDuToanDonVi view = new PrintReportChiTietDuToanDonVi()
                {
                    DataContext = PrintReportChiTietDuToanDonViViewModel
                };
                DialogHost.Show(view, "RootDialog", null, null);
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
                VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan.PrintReportCompareDemandCheck view = new View.Budget.DemandCheck.Plan.PrintReportCompareDemandCheck()
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
    }
}

