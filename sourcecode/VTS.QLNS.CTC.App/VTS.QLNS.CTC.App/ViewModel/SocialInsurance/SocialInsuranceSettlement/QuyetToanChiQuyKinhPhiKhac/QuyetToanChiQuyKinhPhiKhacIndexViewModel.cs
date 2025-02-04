using AutoMapper;
using FlexCel.Core;
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
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Import;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac
{
    public class QuyetToanChiQuyKinhPhiKhacIndexViewModel : GridViewModelBase<BhQtcQuyKPKModel>
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
        private readonly IBhQtcQuyKPKService _quyKCBService;
        private readonly IBhQtcQuyKPKChiTietService _quyKCBChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;
        #endregion

        #region Property
        private ImportQuyetToanChiQuyKinhPhiKhac _importQuyetToanChiQuyKinhPhiKhac;
        public override string Name => "QT chi kinh phí khác";
        public override string Description => "Danh sách báo cáo QT chi kinh phí khác";
        public override string Title => "Danh sách chứng từ - Quyết toán chi quý kinh phí khác";
        public override Type ContentType => typeof(QuyetToanChiQuyKinhPhiKhacIndex);
        public override string GroupName => MenuItemContants.GROUP_QT_CHI_QUY;
        public override PackIconKind IconKind => PackIconKind.BankTransferOut;

        public List<BhQtcQuyKPKChiTietQuery> _listChungTuChiTiet;
        public List<BhQtcQuyKPKChiTietQuery> _listChungTuChiTietTheoQuy;
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

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.IsChecked);
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsAggregate => Items.Any(x => x.IsSelected);
        public bool IsEnableLock => SelectedItem != null;
        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                SearchData();
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }

        private ObservableCollection<DonViModel> _donViModelItems;
        public ObservableCollection<DonViModel> DonViModelItems
        {
            get => _donViModelItems;
            set => SetProperty(ref _donViModelItems, value);
        }
        private List<BhQtcQuyKPKModel> _lstChungTuOrigin;
        public List<BhQtcQuyKPKModel> LstChungTuOrigin
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

        private void SearchData()
        {
            _bhQtcChungTuView?.Refresh();
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
                    var selected = Items.Select(item => item.IsChecked).Distinct().ToList();
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
                    var selected = Items.Select(item => item.IsChecked).Distinct().ToList();
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
                    }
                }
            }
        }

        private void SelectAll(bool select, ObservableCollection<BhQtcQuyKPKModel> items)
        {
            foreach (var model in items.Where(x => x.IsFilter))
            {
                model.IsChecked = select;
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
                var lstSelected = Items.Where(x => x.IsChecked).ToList();
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
                var itemSelected = Items.Where(x => x.IsChecked);
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
            set => SetProperty(ref _searchText, value);
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

        #region View mode
        public QuyetToanChiQuyKinhPhiKhacDialogViewModel QuyetToanChiQuyKinhPhiKhacDialogViewModel { get; set; }
        public QuyetToanChiQuyKinhPhiKhacDetailViewModel QuyetToanChiQuyKinhPhiKhacDetailViewModel { get; set; }
        public ImportQuyetToanChiQuyKinhPhiKhacViewModel ImportQuyetToanChiQuyKinhPhiKhacViewModel { get; set; }
        public PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel { get; set; }
        #endregion

        #region Constructor
        public QuyetToanChiQuyKinhPhiKhacIndexViewModel(
             ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IExportService exportService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhQtcQuyKPKService bhQtcQuyKPKService,
            IBhQtcQuyKPKChiTietService bhQtcQuyKPKChiTietService,
            QuyetToanChiQuyKinhPhiKhacDialogViewModel quyetToanChiQuyKinhPhiKhacDialogViewModel,
            QuyetToanChiQuyKinhPhiKhacDetailViewModel quyetToanChiQuyKinhPhiKhacDetailViewModel,
            ImportQuyetToanChiQuyKinhPhiKhacViewModel importQuyetToanChiQuyKinhPhiKhacViewModel,
            PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel printQuyetToanChiQuyKinhPhiKhacNoticeViewModel,
            IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _quyKCBService = bhQtcQuyKPKService;
            _quyKCBChiTietService = bhQtcQuyKPKChiTietService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            RefreshCommand = new RelayCommand(obj => OnResetFilter());

            QuyetToanChiQuyKinhPhiKhacDialogViewModel = quyetToanChiQuyKinhPhiKhacDialogViewModel;
            QuyetToanChiQuyKinhPhiKhacDetailViewModel = quyetToanChiQuyKinhPhiKhacDetailViewModel;
            ImportQuyetToanChiQuyKinhPhiKhacViewModel = importQuyetToanChiQuyKinhPhiKhacViewModel;
            PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel = printQuyetToanChiQuyKinhPhiKhacNoticeViewModel;
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
                LoadDanhMucLoaiChi();
                LoadData();
                LoadQuater();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                QuyetToanChiQuyKinhPhiKhacDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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

        #region Add chung tu
        protected override void OnAdd()
        {
            try
            {
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.Model = new BhQtcQuyKPKModel();
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.IsAgregate = false;
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.Init();
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.SavedAction = obj =>
                {
                    var chungTu = (BhQtcQuyKPKModel)obj;
                    this.LoadData();
                    if (chungTu != null)
                    {
                        OpenDetailDialog(chungTu);
                    }
                };
                var exportView = new QuyetToanChiQuyKinhPhiKhacDialog() { DataContext = QuyetToanChiQuyKinhPhiKhacDialogViewModel };
                DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OpenDetailDialog(BhQtcQuyKPKModel chungTu)
        {
            try
            {
                QuyetToanChiQuyKinhPhiKhacDetailViewModel.Model = ObjectCopier.Clone(chungTu);
                QuyetToanChiQuyKinhPhiKhacDetailViewModel.Init();
                var view = new QuyetToanChiQuyKinhPhiKhacDetail()
                {
                    DataContext = QuyetToanChiQuyKinhPhiKhacDetailViewModel
                };

                view.ShowDialog();
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
                var lstSelected = Items.Where(x => x.IsChecked).ToList();
                if (lstSelected.Count <= 0) return;
                if (SelectedItem.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi))
                {
                    OnAggregateEdit();
                }
                else
                {
                    QuyetToanChiQuyKinhPhiKhacDialogViewModel.IsAgregate = false;
                    QuyetToanChiQuyKinhPhiKhacDialogViewModel.Model = SelectedItem;
                    QuyetToanChiQuyKinhPhiKhacDialogViewModel.Init();
                    QuyetToanChiQuyKinhPhiKhacDialogViewModel.SavedAction = obj => this.OnRefresh();
                    QuyetToanChiQuyKinhPhiKhacDialogViewModel.ShowDialogHost();
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
            List<BhQtcQuyKPKModel> selectedSKhcChungTus = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();

            QuyetToanChiQuyKinhPhiKhacDialogViewModel.IsAgregate = true;
            QuyetToanChiQuyKinhPhiKhacDialogViewModel.ListBhQtcChungTuModel = selectedSKhcChungTus;
            QuyetToanChiQuyKinhPhiKhacDialogViewModel.Model = SelectedItem;
            QuyetToanChiQuyKinhPhiKhacDialogViewModel.Init();
            QuyetToanChiQuyKinhPhiKhacDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcQuyKPKModel)obj);
            };
            var addView = new QuyetToanChiQuyKinhPhiKhacDialog() { DataContext = QuyetToanChiQuyKinhPhiKhacDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhQtcQuyKPKModel)eventArgs.Parameter);
        }
        #endregion

        #region On delete
        protected override void OnDelete()
        {
            try
            {
                var lstSelected = Items.Where(x => x.IsChecked).ToList();
                if (lstSelected.Count <= 0) return;
                if (SelectedItem != null && (SelectedItem.BIsKhoa)) return;
                if (SelectedItem != null)
                {
                    var entity = _quyKCBService.FindById(SelectedItem.Id);

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
                    _quyKCBService.Delete(SelectedItem.Id);
                    _qtcQBHXHChiTietGiaiThichService.RemoveGiaiThichBangLoiTheoChungTu(SelectedItem.Id);
                    if (SelectedItem.IID_TongHopID.IsNullOrEmpty())
                    {
                        var lstKeHoachChiTiet = _quyKCBChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                        if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                        {
                            _quyKCBChiTietService.RemoveRange(lstKeHoachChiTiet);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(SelectedItem.STongHop))
                        {
                            var lstKeHoachChiTiet = _quyKCBChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                _quyKCBChiTietService.RemoveRange(lstKeHoachChiTiet);
                            }
                        }
                        else
                        {
                            var lstSoCtChild = SelectedItem.STongHop.Split(",");
                            foreach (var soct in lstSoCtChild)
                            {
                                var ctChild = _quyKCBService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                        && x.INamChungTu == _sessionService.Current.YearOfWork).FirstOrDefault();
                                if (ctChild != null)
                                {
                                    ctChild.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                                    ctChild.DNgaySua = dtNow;
                                    ctChild.SNguoiSua = _sessionInfo.Principal;
                                    _quyKCBService.Update(ctChild);
                                }
                            }

                            var lstKeHoachChiTiet = _quyKCBChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                _quyKCBChiTietService.RemoveRange(lstKeHoachChiTiet);
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
                var lstSelected = Items.Where(x => x.IsChecked).ToList();

                var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
                foreach (var item in lstSelected)
                {
                    _quyKCBService.LockOrUnlock(item.Id, isLock);
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

        #region Load data

        public override void LoadData(params object[] args)
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                var listChungTu = _quyKCBService.FindIndex(yearOfWork).OrderBy(x => x.IQuyChungTu).ThenBy(x => x.SSoChungTu);
                _lstChungTuOrigin = _mapper.Map<List<BhQtcQuyKPKModel>>(listChungTu);

                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<BhQtcQuyKPKModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == AllocationTypeLoaiChungTu.ChungTu));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.ILoaiTongHop == SettlementTypeLoaiChungTu.ChungTuTongHop && x.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi)).ToList();
                        var listTongHop = new List<BhQtcQuyKPKModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhQtcQuyKPKModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                var listChild = _mapper.Map<List<BhQtcQuyKPKModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }
                        Items = _mapper.Map<ObservableCollection<BhQtcQuyKPKModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhQtcQuyKPKModel>>(listChungTu);
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhQtcQuyKPKModel.IsChecked))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhQtcQuyKPKModel.IsCollapse))
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

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            listDanhMucLoaiChi = listDanhMucLoaiChi.Where(x => x.SLNS == LNSValue.LNS_9010006_9010007
                                               || x.SLNS == LNSValue.LNS_9010008
                                               || x.SLNS == LNSValue.LNS_9010009
                                                || x.SLNS == LNSValue.LNS_9010010
                                                || x.SLNS == LNSValue.LNS_9050001_9050002);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.Id.ToString(),
                    HiddenValue = n.SLNS,
                    Id = n.Id,
                }));
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private bool QTCChungTuViewFilter(object obj)
        {
            if (!(obj is BhQtcQuyKPKModel temp)) return true;
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

            if (SelectedDanhMucLoaiChi != null)
            {
                condition2 = condition2 && temp.IID_LoaiChi == SelectedDanhMucLoaiChi.Id;
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

        private void ExpandChild()
        {
            Items?.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
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

        private void OnResetFilter()
        {
            try
            {
                SelectedNsDonViModel = null;
                SearchText = string.Empty;
                CbxQuaterSelected = null;
                SelectedDanhMucLoaiChi = null;
                //LockStatusSelected = null;
                LoadData();
                _bhQtcChungTuView?.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadQuater()
        {
            CbxQuater = new ObservableCollection<ComboboxItem>();
            CbxQuater.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Quý I" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Quý II" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Quý III" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "4", DisplayItem = "Quý IV" });
        }
        #endregion

        #region Add chung tu tong hop
        private void ConfirmAggregate()
        {
            List<BhQtcQuyKPKModel> lstQuyKinhPhikhacModel = Items.Where(x => x.IsChecked).ToList();
            bool checkAllowAggregate = lstQuyKinhPhikhacModel.Any(x => x.BIsKhoa);
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
            if (Items.Where(x => x.IsChecked && x.IsFilter).Any(x => !x.BIsKhoa))
            {
                MessageBox.Show(Resources.AlertAggregateUnLocked, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra cùng giai đoạn
            if (Items.Where(x => x.IsChecked && x.IsFilter).GroupBy(x => new { x.INamChungTu }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopKeHoachVonUng, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra cùng giai đoạn 
            if (Items.Where(x => x.IsChecked && x.IsFilter).GroupBy(x => x.IQuyChungTu).ToList().Count > 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateQuarterYear);
                return;
            }

            //kiểm tra cùng giai đoạn
            if (Items.Where(x => x.IsChecked && x.IsFilter).GroupBy(x => new { x.IID_LoaiChi }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopLoaiKeHoachChi, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            OnAddTongHopChungTu();
        }

        private void OnAddTongHopChungTu()
        {
            try
            {
                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                    return;
                }

                List<BhQtcQuyKPKModel> lstQuyKinhPhiKhacModel = Items.Where(x => x.IsChecked).ToList();
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.IsAgregate = true;
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.ListBhQtcChungTuModel = lstQuyKinhPhiKhacModel;
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.Model = new BhQtcQuyKPKModel();
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.Init();
                QuyetToanChiQuyKinhPhiKhacDialogViewModel.SavedAction = obj =>
                {
                    TabIndex = VoucherTabIndex.VOUCHER;
                    this.OnRefresh();
                    OnPropertyChanged(nameof(IsCensorship));
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhQtcQuyKPKModel)obj);
                };

                var view = new QuyetToanChiQuyKinhPhiKhacDialog
                {
                    DataContext = QuyetToanChiQuyKinhPhiKhacDialogViewModel
                };

                DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region On open report
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
                        PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel.SettlementTypeValue = dialogType;
                        PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel.Init();
                        var view1 = new PrintQuyetToanChiQuyKinhPhiKhacNotice
                        {
                            DataContext = PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel
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

        #region On import data
        private void OnImportData()
        {
            try
            {
                ImportQuyetToanChiQuyKinhPhiKhacViewModel.Init();
                ImportQuyetToanChiQuyKinhPhiKhacViewModel.SavedAction = obj =>
                {
                    _importQuyetToanChiQuyKinhPhiKhac.Close();
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhQtcQuyKPKModel)obj);
                };

                _importQuyetToanChiQuyKinhPhiKhac = new ImportQuyetToanChiQuyKinhPhiKhac
                {
                    DataContext = ImportQuyetToanChiQuyKinhPhiKhacViewModel
                };

                _importQuyetToanChiQuyKinhPhiKhac.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Excel
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

                    List<BhQtcQuyKPKModel> lstQuyKinhPhiQuanLyModel = Items.Where(x => x.IsChecked).ToList();
                    var currentDonVi = GetNsDonViOfCurrentUser();
                    foreach (var item in lstQuyKinhPhiQuanLyModel)
                    {
                        DonVi donViChild = _nsDonViService.FindByIdDonVi(item.IID_MaDonVi, yearOfWork);
                        string sLNS = danhMucLoaiChi.Where(x => x.Id.Equals(item.IID_LoaiChi)).Select(x => x.SLNS).FirstOrDefault();
                        var lstLNS = sLNS.Split(",");
                        var lstMucLuc = _bhDmMucLucNganSachService.FindByCondition(x => x.INamLamViec == yearOfWork && lstLNS.Contains(x.SLNS)).OrderBy(x => x.SXauNoiMa).ToList();

                        QtcQuyKCBCriteria searchCondition = new QtcQuyKCBCriteria();
                        searchCondition.NamLamViec = yearOfWork;
                        searchCondition.IDMaDonVi = item.IID_MaDonVi;
                        searchCondition.IDDonVi = item.IID_DonVi;
                        searchCondition.SNguoiTao = item.SNguoiTao;
                        searchCondition.LoaiChungTu = item.ILoaiTongHop;
                        searchCondition.ID = item.Id;
                        searchCondition.SLNS = sLNS;
                        searchCondition.IDLoaiChi = item.IID_LoaiChi;
                        searchCondition.DNgayChungTu = DateTime.Now;
                        searchCondition.IQuyChungTu = item.IQuyChungTu;
                        _listChungTuChiTiet = _quyKCBChiTietService.FindChungTuChiTiet(searchCondition).ToList();

                        CalculateData(_listChungTuChiTiet);
                        var listkhcMucLucsOrders = _listChungTuChiTiet;
                        _listChungTuChiTiet.ForEach(x =>
                        {
                            x.FTien_TongDuToanDuocGiao = x.FTien_DuToanGiaoNamNay + x.FTien_DuToanNamTruocChuyenSang;
                        });
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => x.IsHasData).ToList();
                        Dictionary<string, object> Data = new Dictionary<string, object>();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        var FTongTienDuToanNamTruocChuyenSang = _listChungTuChiTiet?.Where(x => x.IsHangCha).Sum(x => x.FTien_DuToanNamTruocChuyenSang);
                        var FTongTienDuToanGiaoNamNay = _listChungTuChiTiet?.Where(x => x.IsHangCha).Sum(x => x.FTien_DuToanGiaoNamNay);
                        var FTongTienTongDuToanDuocGiao = FTongTienDuToanNamTruocChuyenSang + FTongTienDuToanGiaoNamNay;
                        var FTongTienThucChi = _listChungTuChiTiet?.Where(x => !x.IsHangCha).Sum(x => x.FTienThucChi);
                        var FTongTienQuyetToanDaDuyet = _listChungTuChiTiet?.Where(x => !x.IsHangCha).Sum(x => x.FTienQuyetToanDaDuyet);
                        var FTongTienDeNghiQuyetToanQuyNay = _listChungTuChiTiet?.Where(x => !x.IsHangCha).Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                        var FTongTienXacNhanQuyetToanQuyNay = _listChungTuChiTiet?.Where(x => !x.IsHangCha).Sum(x => x.FTienXacNhanQuyetToanQuyNay);
                        switch (item.SDSLNS)
                        {
                            case LNSValue.LNS_9010006_9010007:
                                Data.Add("TitleFirst", $"QUYẾT TOÁN CHI KINH PHÍ KCB TẠI TRƯỜNG SA NĂM {_sessionService.Current.YearOfWork}");
                                break;
                            case LNSValue.LNS_9050001_9050002:
                                Data.Add("TitleFirst", $"QUYẾT TOÁN CHI KINH PHÍ CHĂM SÓC SỨC KHỎE BAN ĐẦU HSSV & NLĐ NĂM {_sessionService.Current.YearOfWork}");
                                break;
                            case LNSValue.LNS_9010008:
                                Data.Add("TitleFirst", $"QUYẾT TOÁN CHI TỪ NGUỒN KẾ DƯ QUỸ KCB BHYT QUÂN NHÂN NĂM {_sessionService.Current.YearOfWork}");
                                break;
                            case LNSValue.LNS_9010009:
                                Data.Add("TitleFirst", $"QUYẾT TOÁN CHI KINH PHÍ MUA SẮM TRANG THIẾT BỊ Y TẾ NĂM {_sessionService.Current.YearOfWork}");
                                break;
                            case LNSValue.LNS_9010010:
                                Data.Add("TitleFirst", $"QUYẾT TOÁN CHI HỖ TRỢ BHTN NĂM {_sessionService.Current.YearOfWork}");
                                break;
                        }

                        //Data.Add("TitleFirst", $"QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", "Quý " + item.IQuyChungTu);
                        Data.Add("TxtTitleThird", $"Ngày chứng từ: {DateUtils.Format(item.DNgayChungTu)}");
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("Cap1", _sessionInfo.TenDonViTrucThuoc);
                        Data.Add("Cap2", _sessionService.Current.TenDonVi);
                        Data.Add("DonVi", donViChild.TenDonVi);
                        Data.Add("h2", item.STenDonVi);
                        Data.Add("h1", item.STenDonVi);
                        Data.Add("FTongTienDuToanNamTruocChuyenSang", FTongTienDuToanNamTruocChuyenSang);
                        Data.Add("FTongTienDuToanGiaoNamNay", FTongTienDuToanGiaoNamNay);
                        Data.Add("FTongTienTongDuToanDuocGiao", FTongTienTongDuToanDuocGiao);
                        Data.Add("FTongTienThucChi", FTongTienThucChi);
                        Data.Add("FTongTienQuyetToanDaDuyet", FTongTienQuyetToanDaDuyet);
                        Data.Add("FTongTienDeNghiQuyetToanQuyNay", FTongTienDeNghiQuyetToanQuyNay);
                        Data.Add("FTongTienXacNhanQuyetToanQuyNay", FTongTienXacNhanQuyetToanQuyNay);
                        Data.Add("ListData", _listChungTuChiTiet);
                        Data.Add("SKTML", lstMucLuc);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_QUYKPK, ExportFileName.RPT_BH_QTC_QKPK_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = StringUtils.ConvertVN(item.SSoChungTu + "_" + item.IID_MaDonVi + "_" + item.STenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhQtcQuyKPKModel, BhDmMucLucNganSach, BhQtcQuyKPKChiTietQuery>(templateFileName, Data);
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

        private void CalculateData(List<BhQtcQuyKPKChiTietQuery> lstChungTuChiTiet)
        {
            try
            {
                lstChungTuChiTiet.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     //x.FTien_DuToanNamTruocChuyenSang = 0;
                     //x.FTien_DuToanGiaoNamNay = 0;
                     x.FTien_TongDuToanDuocGiao = 0;
                     x.FTienThucChi = 0;
                     x.FTienQuyetToanDaDuyet = 0;
                     x.FTienDeNghiQuyetToanQuyNay = 0;
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

        private void CalculateParent(Guid? idParent, BhQtcQuyKPKChiTietQuery item, Dictionary<Guid?, BhQtcQuyKPKChiTietQuery> dictByMlns)
        {
            try
            {
                if (!dictByMlns.ContainsKey(idParent))
                {
                    return;
                }

                var model = dictByMlns[idParent];
                //model.FTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
                //model.FTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
                model.FTienThucChi += item.FTienThucChi;
                model.FTienQuyetToanDaDuyet += item.FTienQuyetToanDaDuyet;
                model.FTienDeNghiQuyetToanQuyNay += item.FTienDeNghiQuyetToanQuyNay;
                model.FTienXacNhanQuyetToanQuyNay += item.FTienXacNhanQuyetToanQuyNay;

                CalculateParent(model.IdParent, item, dictByMlns);
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
        #endregion

        #region OnSelectionDoubleClick
        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhQtcQuyKPKModel)obj);
        }

        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhQtcQuyKPKModel)obj;
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
    }
}
