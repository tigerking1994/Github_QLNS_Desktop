using AutoMapper;
using FlexCel.Core;
using log4net;
using log4net.Filter;
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
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.ImportChiKinhPhiQuanLy;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.ImportChiKinhPhiQuanLy;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy
{
    public class QuyetToanChiKinhPhiQuanLyIndexViewModel : GridViewModelBase<BhQtcQuyKinhPhiQuanLyModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private ICollectionView _bhQtcChungTuView;
        private ICollectionView _nsDonViModelsView;
        private readonly IBhQtcQuyKinhPhiQuanLyService _bhQtcQKPQuanLyService;
        private readonly IBhQtcQuyKinhPhiQuanLyChiTietService _bhQtcQKPQuanLyChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;
        #endregion

        #region Property
        private ImportQuyetToanChiKinhPhiQuanLy _importQuyetToanChiKinhPhiQuanLy;
        public override string Name => "QT chi kinh phí quản lý";
        public override string Description => "Danh sách báo cáo QT chi kinh phí quản lý ";
        public override string Title => "Danh sách chứng từ - Quyết toán chi kinh phí quản lý";
        public override Type ContentType => typeof(QuyetToanChiKinhPhiQuanLyIndex);
        public override string GroupName => MenuItemContants.GROUP_QT_CHI_QUY;
        public override PackIconKind IconKind => PackIconKind.BankTransferOut;
        public List<BhQtcQuyKinhPhiQuanLyChiTietQuery> _listChungTuChiTiet;
        public List<BhQtcQuyKinhPhiQuanLyChiTietQuery> _listChungTuChiTietTheoQuy;
        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set
            {
                SetProperty(ref _isCollapse, value);
                LoadData();
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.IsSelected);
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsAggregate => Items.Any(x => x.IsSelected);
        public bool IsEnableLock => SelectedItem != null;


        private ObservableCollection<DonViModel> _donViModelItems;
        public ObservableCollection<DonViModel> DonViModelItems
        {
            get => _donViModelItems;
            set => SetProperty(ref _donViModelItems, value);
        }
        private List<BhQtcQuyKinhPhiQuanLyModel> _lstChungTuOrigin;
        public List<BhQtcQuyKinhPhiQuanLyModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
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

        private ComboboxItem _cbxQuaterSelected;
        public ComboboxItem CbxQuaterSelected
        {
            get => _cbxQuaterSelected;
            set
            {
                SetProperty(ref _cbxQuaterSelected, value);
                SearchData();
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuater;
        public ObservableCollection<ComboboxItem> CbxQuater
        {
            get => _cbxQuater;
            set => SetProperty(ref _cbxQuater, value);
        }
        private DonViModel _selectedNsDonViModel;
        public DonViModel SelectedNsDonViModel
        {
            get => _selectedNsDonViModel;
            set
            {
                SetProperty(ref _selectedNsDonViModel, value);
                SearchData();
            }
        }
        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.IsSelected).Distinct().ToList();
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
                    OnPropertyChanged(nameof(IsEnableButtonDataShow));
                }
            }
        }

        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (Items != null)
                    {
                        SelectAll(value.Value, Items);
                        OnPropertyChanged();
                        //Items.Where(x => x.IsExpand).ForAll(c => c.IsSelected = value.Value);
                    }
                }
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
                OnRefresh();
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
            }
        }

        public bool IsButtonEnable
        {
            get
            {
                var result = false;
                var lstSelected = Items.Where(x => x.IsSelected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    var lstSelectedKhoa = lstSelected.Where(x => x.BIsKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BIsKhoa).ToList();
                    if (lstSelectedKhoa.Count() > 0 && lstSelectedMo.Count() > 0)
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Count() > 0)
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Count() > 0)
                    {
                        IsLock = false;
                        result = true;
                    }

                }
                return result;

            }
        }

        public bool IsCensorship
        {
            get
            {
                var itemSelected = Items.Where(x => x.IsSelected);
                return itemSelected.Any() && itemSelected.All(x => x.BIsKhoa);
            }
        }

        private VoucherTabIndex _tabIndex;

        public VoucherTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
            }
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                SearchData();
            }
        }
        #endregion

        #region RelayCommand
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand ExportAggregateDataCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        #endregion RelayCommand

        #region View model
        public QuyetToanChiKinhPhiQuanLyDialogViewModel QuyetToanChiKinhPhiQuanLyDialogViewModel { get; set; }
        public QuyetToanChiKinhPhiQuanLyDetailViewModel QuyetToanChiKinhPhiQuanLyDetailViewModel { get; set; }
        public ImportQuyetToanChiKinhPhiQuanLyViewModel ImportQuyetToanChiKinhPhiQuanLyViewModel { get; set; }
        public PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel { get; set; }
        #endregion

        #region Constructor
        public QuyetToanChiKinhPhiQuanLyIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IExportService exportService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IBhQtcQuyKinhPhiQuanLyService bhQtcQuyKinhPhiQuanLyService,
            IBhQtcQuyKinhPhiQuanLyChiTietService bhQtcQuyKinhPhiQuanLyChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            QuyetToanChiKinhPhiQuanLyDialogViewModel quyetToanChiKinhPhiQuanLyDialogViewModel,
            QuyetToanChiKinhPhiQuanLyDetailViewModel quyetToanChiKinhPhiQuanLyDetailViewModel,
            ImportQuyetToanChiKinhPhiQuanLyViewModel importQuyetToanChiKinhPhiQuanLyViewModel,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel printQuyetToanChiKinhPhiQuanLyNoticeViewModel,
            IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhQtcQKPQuanLyService = bhQtcQuyKinhPhiQuanLyService;
            _bhQtcQKPQuanLyChiTietService = bhQtcQuyKinhPhiQuanLyChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            RefreshCommand = new RelayCommand(obj => OnResetFilter());

            QuyetToanChiKinhPhiQuanLyDialogViewModel = quyetToanChiKinhPhiQuanLyDialogViewModel;
            QuyetToanChiKinhPhiQuanLyDetailViewModel = quyetToanChiKinhPhiQuanLyDetailViewModel;
            ImportQuyetToanChiKinhPhiQuanLyViewModel = importQuyetToanChiKinhPhiQuanLyViewModel;
            PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel = printQuyetToanChiKinhPhiQuanLyNoticeViewModel;
        }

        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                _tabIndex = VoucherTabIndex.VOUCHER;
                _sessionInfo = _sessionService.Current;
                LoadLockStatus();
                OnResetFilter();
                LoadData();
                //LoadNsDonVi();
                LoadQuater();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                QuyetToanChiKinhPhiQuanLyDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Add chung tu thuong
        protected override void OnAdd()
        {
            try
            {
                QuyetToanChiKinhPhiQuanLyDialogViewModel.Model = new BhQtcQuyKinhPhiQuanLyModel();
                QuyetToanChiKinhPhiQuanLyDialogViewModel.IsDetail = true;
                QuyetToanChiKinhPhiQuanLyDialogViewModel.IsAgregate = false;
                QuyetToanChiKinhPhiQuanLyDialogViewModel.IsSummary = false;
                QuyetToanChiKinhPhiQuanLyDialogViewModel.Init();
                QuyetToanChiKinhPhiQuanLyDialogViewModel.SavedAction = obj =>
                {
                    var chungTu = (BhQtcQuyKinhPhiQuanLyModel)obj;
                    this.LoadData();
                    if (chungTu != null)
                    {
                        OpenDetailDialog(chungTu);
                    }
                };
                var exportView = new QuyetToanChiKinhPhiQuanLyDialog() { DataContext = QuyetToanChiKinhPhiQuanLyDialogViewModel };
                DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OpenDetailDialog(BhQtcQuyKinhPhiQuanLyModel khcChungTu)
        {
            QuyetToanChiKinhPhiQuanLyDetailViewModel.Model = ObjectCopier.Clone(khcChungTu);
            QuyetToanChiKinhPhiQuanLyDetailViewModel.Init();
            var view = new QuyetToanChiKinhPhiQuanLyDetail()
            {
                DataContext = QuyetToanChiKinhPhiQuanLyDetailViewModel
            };

            view.ShowDialog();
        }
        #endregion

        #region Refresh Data
        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }
        protected override void OnRefresh()
        {
            LoadData();
        }
        #endregion

        #region Lock chung tu
        protected override void OnLockUnLock()
        {
            try
            {
                if (IsLock)
                {
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem != null && SelectedItem.SNguoiTao != _sessionService.Current.Principal)
                    {
                        MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                if (MessageBoxHelper.Confirm(message) == MessageBoxResult.Yes)
                    LockConfirmEventHandler();
                LockStatusSelected = LockStatus.ElementAt(0);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            try
            {
                var lstSelected = Items.Where(x => x.IsSelected).ToList();
                var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
                foreach (var item in lstSelected)
                {
                    _bhQtcQKPQuanLyService.LockOrUnlock(item.Id, isLock);
                    item.BIsKhoa = !item.BIsKhoa;
                }

                LoadData();
                OnPropertyChanged(nameof(IsLock));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Chung tu tong hop
        private void ConfirmAggregate()
        {
            List<BhQtcQuyKinhPhiQuanLyModel> lstQuyKinhPhiQuanLyModel = Items.Where(x => x.IsSelected).ToList();
            bool checkAllowAggregate = lstQuyKinhPhiQuanLyModel.Any(x => x.BIsKhoa);
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
            // check quyền được tổng hợp
            List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBox.Show(Resources.MsgRoleAggregate, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //kiểm tra trạng thái các bản ghi
            if (Items.Where(x => x.IsSelected && x.IsFilter).Any(x => !x.BIsKhoa))
            {
                MessageBox.Show(Resources.AlertAggregateUnLocked, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra cùng giai đoạn
            if (Items.Where(x => x.IsSelected && x.IsFilter).GroupBy(x => new { x.INamChungTu }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopKeHoachVonUng, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra cùng giai đoạn 
            if (Items.Where(x => x.IsSelected && x.IsFilter).GroupBy(x => x.IQuyChungTu).ToList().Count > 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateQuarterYear);
                return;
            }

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            OnAddTongHopChungTu();
        }

        private void OnAddTongHopChungTu()
        {
            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                return;
            }

            List<BhQtcQuyKinhPhiQuanLyModel> lstQuyKinhPhiQuanLyModel = Items.Where(x => x.IsSelected).ToList();
            QuyetToanChiKinhPhiQuanLyDialogViewModel.IsSummary = true;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.IsDetail = false;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.ListBhQtcChungTuModel = lstQuyKinhPhiQuanLyModel;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.Model = new BhQtcQuyKinhPhiQuanLyModel();
            QuyetToanChiKinhPhiQuanLyDialogViewModel.IsAgregate = true;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.Init();
            QuyetToanChiKinhPhiQuanLyDialogViewModel.SavedAction = obj =>
            {
                TabIndex = VoucherTabIndex.VOUCHER;
                this.OnRefresh();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcQuyKinhPhiQuanLyModel)obj);
            };

            var view = new QuyetToanChiKinhPhiQuanLyDialog
            {
                DataContext = QuyetToanChiKinhPhiQuanLyDialogViewModel
            };

            DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
        }
        #endregion

        #region Open Report
        private void OnOpenReport(object param)
        {
            try
            {
                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRolePrintReportAllocation);
                    return;
                }

                int dialogType = (int)param;

                switch (dialogType)
                {
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel.SettlementTypeValue = dialogType;
                        PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel.Init();
                        var view1 = new PrintQuyetToanChiKinhPhiQuanLyNotice
                        {
                            DataContext = PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel
                        };
                        DialogHost.Show(view1, SystemConstants.ROOT_DIALOG, null, null);
                        break;
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Import data
        private void OnImportData()
        {
            try
            {
                ImportQuyetToanChiKinhPhiQuanLyViewModel.Init();
                ImportQuyetToanChiKinhPhiQuanLyViewModel.SavedAction = obj =>
                {
                    _importQuyetToanChiKinhPhiQuanLy.Close();
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhQtcQuyKinhPhiQuanLyModel)obj);
                };

                _importQuyetToanChiKinhPhiQuanLy = new ImportQuyetToanChiKinhPhiQuanLy
                {
                    DataContext = ImportQuyetToanChiKinhPhiQuanLyViewModel
                };
                _importQuyetToanChiKinhPhiQuanLy.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Export excel
        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(yearOfWork).ToList();
                    var iDDmLoaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(SettlementTypeSLNS.SLNS)).Select(x => x.Id).FirstOrDefault();
                    List<BhQtcQuyKinhPhiQuanLyModel> lstQuyKinhPhiQuanLyModel = Items.Where(x => x.IsSelected).ToList();
                    var currentDonVi = GetNsDonViOfCurrentUser();
                    foreach (var item in lstQuyKinhPhiQuanLyModel)
                    {
                        QtcQuyKinhPhiQuanLyCriteria searchCondition = new QtcQuyKinhPhiQuanLyCriteria();
                        searchCondition.NamLamViec = yearOfWork;
                        searchCondition.IDMaDonVi = item.IID_MaDonVi;
                        searchCondition.IDDonVi = item.IID_DonVi;
                        searchCondition.SNguoiTao = item.SNguoiTao;
                        searchCondition.LoaiChungTu = item.ILoaiTongHop;
                        searchCondition.ID = item.Id;
                        searchCondition.SLNS = SettlementTypeSLNS.SLNS;
                        searchCondition.IDLoaiChi = iDDmLoaiChi;
                        searchCondition.DNgayChungTu = DateTime.Now;
                        searchCondition.IQuyChungTu = item.IQuyChungTu;
                        _listChungTuChiTiet = _bhQtcQKPQuanLyChiTietService.FindChungTuChiTiet(searchCondition).ToList();

                        //if (item.IQuyChungTu > SettlementTypeQuy.Quy)
                        //{
                        //    _listChungTuChiTietTheoQuy = _bhQtcQKPQuanLyChiTietService.FindSoTienQuyetToanDaDuocDuyetTheoQuy(searchCondition).ToList();

                        //    foreach (var chungTu in _listChungTuChiTiet)
                        //    {
                        //        foreach (var itemChungTu in _listChungTuChiTietTheoQuy)
                        //        {
                        //            if (chungTu.IID_MucLucNganSach.Equals(itemChungTu.IID_MucLucNganSach))
                        //            {
                        //                chungTu.FTienQuyetToanDaDuyet = itemChungTu.FTienQuyetToanDaDuyet;
                        //            }
                        //        }
                        //    }
                        //}

                        var lstChungTuChiTiet = _mapper.Map<ObservableCollection<BhQtcQuyKinhPhiQuanLyChiTietModel>>(_listChungTuChiTiet).ToList();
                        CalculateData(lstChungTuChiTiet);
                        lstChungTuChiTiet = lstChungTuChiTiet.Where(x => x.IsHasData).ToList();
                        Dictionary<string, object> Data = new Dictionary<string, object>();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("Cap1", _sessionInfo.TenDonViTrucThuoc);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("h2", item.STenDonVi);
                        Data.Add("h1", item.STenDonVi);
                        Data.Add("ListData", lstChungTuChiTiet);
                        Data.Add("SKTML", lstChungTuChiTiet);
                        Data.Add("TieuDe1", $"Quyết toán chi kinh phí quản lý BHXH, BHYT {_sessionService.Current.YearOfWork}");
                        Data.Add("TieuDe2", $"Quý {item.IQuyChungTu}");
                        Data.Add("TieuDe3", $"Ngày chứng từ: {DateTime.Now.ToString("dd/MM/yyyy")}");


                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_QUYKPQL, ExportFileName.RPT_BH_QTC_KPQL_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = $"{item.SSoChungTu}_{StringUtils.ConvertVN(item.STenDonVi)}";
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhQtcQuyKinhPhiQuanLyModel, BhDmMucLucNganSach, BhQtcQuyKinhPhiQuanLyChiTietModel>(templateFileName, Data);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
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

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private void CalculateData(List<BhQtcQuyKinhPhiQuanLyChiTietModel> lstChungTuChiTiet)
        {
            try
            {
                lstChungTuChiTiet.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     x.FTienDeNghiQuyetToanQuyNay = 0;
                     //x.FTienDuToanDuocGiao = 0;
                     x.FTienQuyetToanDaDuyet = 0;
                     x.FTienXacNhanQuyetToanQuyNay = 0;
                 });

                var temp = lstChungTuChiTiet.Where(x => !x.IsHangCha).ToList();
                var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
                foreach (var item in temp)
                {
                    CalculateParent(item.IdParent, item, dictByMlns);
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParent(Guid? idParent, BhQtcQuyKinhPhiQuanLyChiTietModel item, Dictionary<Guid?, BhQtcQuyKinhPhiQuanLyChiTietModel> dictByMlns)
        {
            try
            {
                if (!dictByMlns.ContainsKey(idParent))
                {
                    return;
                }

                var model = dictByMlns[idParent];
                model.FTienXacNhanQuyetToanQuyNay += item.FTienXacNhanQuyetToanQuyNay.GetValueOrDefault(0);
                model.FTienDeNghiQuyetToanQuyNay += item.FTienDeNghiQuyetToanQuyNay.GetValueOrDefault(0);
                //model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao;
                model.FTienQuyetToanDaDuyet += item.FTienQuyetToanDaDuyet.GetValueOrDefault(0);

                CalculateParent(model.IdParent, item, dictByMlns);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Search data
        private void SearchData()
        {
            if (_bhQtcChungTuView != null)
            {
                _bhQtcChungTuView.Refresh();
            }
        }
        private void OnResetFilter()
        {
            try
            {
                SelectedNsDonViModel = null;
                SearchText = string.Empty;
                CbxQuaterSelected = null;
                //LockStatusSelected = null;
                LoadData();
                _bhQtcChungTuView?.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhQtcQuyKinhPhiQuanLyModel)obj;
            if (SelectedItem is { BIsKhoa: true } || SelectedItem == null)
            {
                IsEdit = false;
            }
            else
            {
                OnPropertyChanged(nameof(IsExportAggregateData));
                IsEdit = true;
            }
        }
        #endregion

        #region Load data
        private void LoadQuater()
        {
            CbxQuater = new ObservableCollection<ComboboxItem>();
            CbxQuater.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Quý I" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Quý II" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Quý III" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "4", DisplayItem = "Quý IV" });
        }

        private void SelectAll(bool select, ObservableCollection<BhQtcQuyKinhPhiQuanLyModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.IsSelected = select;
            }
        }

        

        private void LoadNsDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                var idDonVis = Items.Select(x => x.IID_MaDonVi).ToList();
                predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));
                var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
                DonViModelItems = new ObservableCollection<DonViModel>();
                DonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                _nsDonViModelsView = CollectionViewSource.GetDefaultView(DonViModelItems);
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                    ListSortDirection.Ascending));
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                var listChungTu = _bhQtcQKPQuanLyService.FindIndex(yearOfWork).OrderBy(x => x.IQuyChungTu).ThenBy(x => x.SSoChungTu);
                _lstChungTuOrigin = _mapper.Map<List<BhQtcQuyKinhPhiQuanLyModel>>(listChungTu);
                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<BhQtcQuyKinhPhiQuanLyModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == AllocationTypeLoaiChungTu.ChungTu));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.ILoaiTongHop == SettlementTypeLoaiChungTu.ChungTuTongHop && x.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi)).ToList();
                        var listTongHop = new List<BhQtcQuyKinhPhiQuanLyModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhQtcQuyKinhPhiQuanLyModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                var listChild = _mapper.Map<List<BhQtcQuyKinhPhiQuanLyModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }
                        Items = _mapper.Map<ObservableCollection<BhQtcQuyKinhPhiQuanLyModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhQtcQuyKinhPhiQuanLyModel>>(listChungTu);
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhQtcQuyKinhPhiQuanLyModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhQtcQuyKinhPhiQuanLyModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }

                _bhQtcChungTuView = CollectionViewSource.GetDefaultView(Items);
                _bhQtcChungTuView.Filter = QTCChungTuViewFilter;
                LoadNsDonVi();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExpandChild()
        {
            Items?.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
        }
        #endregion

        #region Filter
        private bool QTCChungTuViewFilter(object obj)
        {
            if (!(obj is BhQtcQuyKinhPhiQuanLyModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty.Trim().ToLower();
            var condition1 = false;
            var condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SSoQuyetDinh))
                    condition1 = condition1 || temp.SSoQuyetDinh.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SMoTa))
                    condition1 = condition1 || temp.SMoTa.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SNguoiTao))
                    condition1 = condition1 || temp.SNguoiTao.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.STenDonVi))
                    condition1 = condition1 || temp.STenDonVi.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
            }

            if (SelectedNsDonViModel != null)
            {
                condition2 = condition2 && temp.IID_MaDonVi == SelectedNsDonViModel.IIDMaDonVi;
            }

            if (CbxQuaterSelected != null)
            {
                condition2 = condition2 && temp.IQuyChungTu == Convert.ToInt32(CbxQuaterSelected.ValueItem);
            }

            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    condition2 = condition2 && temp.BIsKhoa == true;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    condition2 = condition2 && temp.BIsKhoa == false;
                }
            }

            var result = condition1 && condition2;
            temp.IsFilter = result;
            return result;
        }
        #endregion

        #region On delete
        protected override void OnDelete()
        {
            try
            {
                var lstSelected = Items.Where(x => x.IsSelected).ToList();
                if (lstSelected.Count <= 0) return;
                if (SelectedItem != null && (SelectedItem.BIsKhoa)) return;
                if (SelectedItem != null)
                {
                    var entity = _bhQtcQKPQuanLyService.FindById(SelectedItem.Id);

                    if (entity != null && !string.IsNullOrEmpty(entity.SNguoiTao) && !entity.SNguoiTao.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SNguoiTao), Resources.Alert);
                        return;
                    }
                }

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? DateTimeExtension.ToStringDate(SelectedItem.DNgayChungTu.Value) : string.Empty);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            try
            {
                if (result != NSDialogResult.Yes) return;
                DateTime dtNow = DateTime.Now;
                if (SelectedItem != null)
                {
                    _bhQtcQKPQuanLyService.Delete(SelectedItem.Id);
                    _qtcQBHXHChiTietGiaiThichService.RemoveGiaiThichBangLoiTheoChungTu(SelectedItem.Id);
                    if (SelectedItem.IID_TongHopID.IsNullOrEmpty())
                    {
                        var lstKeHoachChiTiet = _bhQtcQKPQuanLyChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                        if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                        {
                            _bhQtcQKPQuanLyChiTietService.RemoveRange(lstKeHoachChiTiet);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(SelectedItem.STongHop))
                        {
                            var lstKeHoachChiTiet = _bhQtcQKPQuanLyChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                _bhQtcQKPQuanLyChiTietService.RemoveRange(lstKeHoachChiTiet);
                            }
                        }
                        else
                        {
                            var lstSoCtChild = SelectedItem.STongHop.Split(",");
                            foreach (var soct in lstSoCtChild)
                            {
                                var ctChild = _bhQtcQKPQuanLyService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                        && x.INamChungTu == _sessionService.Current.YearOfWork).FirstOrDefault();
                                if (ctChild != null)
                                {
                                    ctChild.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                                    ctChild.DNgaySua = dtNow;
                                    ctChild.SNguoiSua = _sessionInfo.Principal;
                                    _bhQtcQKPQuanLyService.Update(ctChild);
                                }
                            }

                            var lstKeHoachChiTiet = _bhQtcQKPQuanLyChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                _bhQtcQKPQuanLyChiTietService.RemoveRange(lstKeHoachChiTiet);
                            }
                        }
                    }
                }

                var itemDeleted = Items.Where(x => x.Id == SelectedItem.Id).First();
                Items.Remove(itemDeleted);
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Update
        protected override void OnUpdate()
        {
            try
            {
                var lstSelected = Items.Where(x => x.IsSelected).ToList();
                if (lstSelected.Count <= 0) return;
                if (SelectedItem.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi))
                {
                    OnAggregateEdit();
                }
                else
                {
                    QuyetToanChiKinhPhiQuanLyDialogViewModel.IsDetail = true;
                    QuyetToanChiKinhPhiQuanLyDialogViewModel.IsAgregate = false;
                    QuyetToanChiKinhPhiQuanLyDialogViewModel.IsSummary = false;
                    QuyetToanChiKinhPhiQuanLyDialogViewModel.Model = SelectedItem;
                    QuyetToanChiKinhPhiQuanLyDialogViewModel.Init();
                    QuyetToanChiKinhPhiQuanLyDialogViewModel.SavedAction = obj => this.OnRefresh();
                    QuyetToanChiKinhPhiQuanLyDialogViewModel.ShowDialogHost();
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhQtcQuyKinhPhiQuanLyModel> selectedSKhcChungTus = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();

            QuyetToanChiKinhPhiQuanLyDialogViewModel.IsAgregate = true;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.IsSummary = true;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.IsDetail = false;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.ListBhQtcChungTuModel = selectedSKhcChungTus;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.Model = SelectedItem;
            QuyetToanChiKinhPhiQuanLyDialogViewModel.Init();
            QuyetToanChiKinhPhiQuanLyDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcQuyKinhPhiQuanLyModel)obj);
            };
            var addView = new QuyetToanChiKinhPhiQuanLyDialog() { DataContext = QuyetToanChiKinhPhiQuanLyDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhQtcQuyKinhPhiQuanLyModel)eventArgs.Parameter);
        }
        #endregion

        #region OnSelectionDoubleClick
        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhQtcQuyKinhPhiQuanLyModel)obj);
        }
        #endregion
    }
}
