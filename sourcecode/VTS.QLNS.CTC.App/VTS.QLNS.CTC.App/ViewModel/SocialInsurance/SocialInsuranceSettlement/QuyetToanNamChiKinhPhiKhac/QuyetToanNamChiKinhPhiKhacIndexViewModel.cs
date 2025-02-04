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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.Import;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac
{
    public class QuyetToanNamChiKinhPhiKhacIndexViewModel : GridViewModelBase<BhQtcNamKinhPhiKhacModel>
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
        private readonly IBhQtcNamKinhPhiKhacService _kinhPhiKhacService;
        private readonly IBhQtcNamKinhPhiKhacChiTietService _kinhPhiKhacChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        #endregion

        #region Property
        private ImportQuyetToanNamChiKinhPhiKhac _importQuyetToanNamChiKinhPhiKhac;
        public override string Name => "QT chi kinh phí khác";
        public override string Description => "QT chi năm kinh phí khác " + _sessionInfo.YearOfWork;
        public override string Title => "Danh sách chứng từ - Quyết toán chi kinh phí khác";
        public override Type ContentType => typeof(QuyetToanNamChiKinhPhiKhacIndex);
        public override string GroupName => MenuItemContants.GROUP_QT_CHI_NAM;
        public override PackIconKind IconKind => PackIconKind.BankTransferOut;
        public List<BhQtcNamKinhPhiKhacChiTietQuery> _listChungTuChiTiet;
        public List<BhQtcNamKinhPhiKhacChiTietQuery> _listChungTuChiTietTheoQuy;
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

        private DateTime? _dNgayLap;
        public DateTime? DNgayLap
        {
            get => _dNgayLap;
            set
            {
                SetProperty(ref _dNgayLap, value);
                if (_bhQtcChungTuView != null) _bhQtcChungTuView.Refresh();
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
        private List<BhQtcNamKinhPhiKhacModel> _lstChungTuOrigin;
        public List<BhQtcNamKinhPhiKhacModel> LstChungTuOrigin
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
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                    //if (Items != null)
                    //{
                    //    Items.Where(x => x.IsExpand).ForAll(c => c.IsSelected = value.Value);
                    //}
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

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (_selectedDanhMucLoaiChi != null)
                {
                    SearchData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
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
        #endregion

        #region View model
        public QuyetToanNamChiKinhPhiKhacDialogViewModel QuyetToanNamChiKinhPhiKhacDialogViewModel { get; set; }
        public QuyetToanNamChiKinhPhiKhacDetailViewModel QuyetToanNamChiKinhPhiKhacDetailViewModel { get; set; }
        public ImportQuyetToanNamChiKinhPhiKhacViewModel ImportQuyetToanNamChiKinhPhiKhacViewModel { get; set; }
        public PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel { get; set; }
        #endregion

        #region Constructor
        public QuyetToanNamChiKinhPhiKhacIndexViewModel(
             ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IExportService exportService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IBhQtcNamKinhPhiKhacService bhQtcNamKinhPhiKhacService,
            IBhQtcNamKinhPhiKhacChiTietService bhQtcNamKinhPhiKhacChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            QuyetToanNamChiKinhPhiKhacDialogViewModel quyetToanNamChiKinhPhiKhacDialogViewModel,
            QuyetToanNamChiKinhPhiKhacDetailViewModel quyetToanNamChiKinhPhiKhacDetailViewModel,
            ImportQuyetToanNamChiKinhPhiKhacViewModel importQuyetToanNamChiKinhPhiKhacViewModel,
            PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel printQuyetToanChiNamKinhPhiKhacNoticeViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _kinhPhiKhacService = bhQtcNamKinhPhiKhacService;
            _kinhPhiKhacChiTietService = bhQtcNamKinhPhiKhacChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            RefreshCommand = new RelayCommand(obj => OnResetFilter());

            QuyetToanNamChiKinhPhiKhacDialogViewModel = quyetToanNamChiKinhPhiKhacDialogViewModel;
            QuyetToanNamChiKinhPhiKhacDetailViewModel = quyetToanNamChiKinhPhiKhacDetailViewModel;
            ImportQuyetToanNamChiKinhPhiKhacViewModel = importQuyetToanNamChiKinhPhiKhacViewModel;
            PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel = printQuyetToanChiNamKinhPhiKhacNoticeViewModel;
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
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                QuyetToanNamChiKinhPhiKhacDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
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

        #region Load data
        public override void LoadData(params object[] args)
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                var listChungTu = _kinhPhiKhacService.FindIndex(yearOfWork);
                _lstChungTuOrigin = _mapper.Map<List<BhQtcNamKinhPhiKhacModel>>(listChungTu);
                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<BhQtcNamKinhPhiKhacModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == AllocationTypeLoaiChungTu.ChungTu && !x.BDaTongHop));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.ILoaiTongHop == SettlementTypeLoaiChungTu.ChungTuTongHop && x.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi)).ToList();
                        var listTongHop = new List<BhQtcNamKinhPhiKhacModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhQtcNamKinhPhiKhacModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                var listChild = _mapper.Map<List<BhQtcNamKinhPhiKhacModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }
                        Items = _mapper.Map<ObservableCollection<BhQtcNamKinhPhiKhacModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhQtcNamKinhPhiKhacModel>>(listChungTu.Where(x => x.ILoaiTongHop == AllocationTypeLoaiChungTu.ChungTu && !x.BDaTongHop));
                }


                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhQtcNamKinhPhiKhacModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhQtcNamKinhPhiKhacModel.IsCollapse))
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

        private bool QTCChungTuViewFilter(object obj)
        {
            if (!(obj is BhQtcNamKinhPhiKhacModel temp)) return true;
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

            if (DNgayLap != null)
            {
                condition2 = condition2 && temp.DNgayQuyetDinh.HasValue && temp.DNgayQuyetDinh.Value.ToString("yyyy-MM-dd").ToLower().Contains(DNgayLap.Value.ToString("yyyy-MM-dd"));
            }

            if (SelectedDanhMucLoaiChi != null)
            {
                condition2 = condition2 && temp.IID_LoaiChi == SelectedDanhMucLoaiChi.Id;
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

        private void SelectAll(bool select, ObservableCollection<BhQtcNamKinhPhiKhacModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.IsSelected = select;
            }
        }


        private void SearchData()
        {
            if (_bhQtcChungTuView != null)
            {
                _bhQtcChungTuView.Refresh();
            }
        }


        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            listDanhMucLoaiChi = listDanhMucLoaiChi.Where(x => x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBBHYT || x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBTS || x.SMaLoaiChi == MaLoaiChiBHXH.SMAHSSVNLD
                                                                      || x.SMaLoaiChi == MaLoaiChiBHXH.SMAMSTTBYT || x.SMaLoaiChi == MaLoaiChiBHXH.SMABHTN);
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
        #endregion

        #region On Reset 
        private void OnResetFilter()
        {
            try
            {
                SelectedNsDonViModel = null;
                SearchText = string.Empty;
                SelectedDanhMucLoaiChi = null;
                LoadData();
                _bhQtcChungTuView?.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
                    var entity = _kinhPhiKhacService.FindById(SelectedItem.Id);

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
                    var item = _kinhPhiKhacService.FindById(SelectedItem.Id);
                    _kinhPhiKhacService.Delete(item);

                    if (string.IsNullOrEmpty(SelectedItem.STongHop))
                    {
                        var lstKeHoachChiTiet = _kinhPhiKhacChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                        if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                        {
                            _kinhPhiKhacChiTietService.RemoveRange(lstKeHoachChiTiet);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(SelectedItem.STongHop))
                        {
                            var lstKeHoachChiTiet = _kinhPhiKhacChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                _kinhPhiKhacChiTietService.RemoveRange(lstKeHoachChiTiet);
                            }
                        }
                        else
                        {
                            var lstSoCtChild = SelectedItem.STongHop.Split(",");
                            foreach (var soct in lstSoCtChild)
                            {
                                var ctChild = _kinhPhiKhacService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                        && x.INamLamViec == _sessionService.Current.YearOfWork).FirstOrDefault();
                                if (ctChild != null)
                                {
                                    ctChild.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                                    ctChild.BDaTongHop = false;
                                    ctChild.DNgaySua = dtNow;
                                    ctChild.SNguoiSua = _sessionInfo.Principal;
                                    _kinhPhiKhacService.Update(ctChild);
                                }
                            }

                            var lstKeHoachChiTiet = _kinhPhiKhacChiTietService.FindByIdChiTiet(SelectedItem.Id).ToList();
                            if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                            {
                                _kinhPhiKhacChiTietService.RemoveRange(lstKeHoachChiTiet);
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
                    case (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM:
                    case (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN:
                        PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel.SettlementTypeValue = dialogType;
                        PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel.Init();
                        var view2 = new PrintQuyetToanNamChiKinhPhiKhacNotice
                        {
                            DataContext = PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel
                        };
                        DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Import 
        private void OnImportData()
        {
            try
            {
                ImportQuyetToanNamChiKinhPhiKhacViewModel.Init();
                ImportQuyetToanNamChiKinhPhiKhacViewModel.SavedAction = obj =>
                {
                    _importQuyetToanNamChiKinhPhiKhac.Close();
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhQtcNamKinhPhiKhacModel)obj);
                };

                _importQuyetToanNamChiKinhPhiKhac = new ImportQuyetToanNamChiKinhPhiKhac
                {
                    DataContext = ImportQuyetToanNamChiKinhPhiKhacViewModel
                };
                _importQuyetToanNamChiKinhPhiKhac.ShowDialog();
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

                    List<BhQtcNamKinhPhiKhacModel> lstQuyKinhPhiQuanLyModel = Items.Where(x => x.IsSelected).ToList();
                    var currentDonVi = GetNsDonViOfCurrentUser();
                    foreach (var item in lstQuyKinhPhiQuanLyModel)
                    {
                        DonVi donViChild = _nsDonViService.FindByIdDonVi(item.IID_MaDonVi, yearOfWork);
                        var sLNS = danhMucLoaiChi.Where(x => x.Id.Equals(item.IID_LoaiChi)).Select(x => x.SLNS).FirstOrDefault();
                        var lstLNS = sLNS.Split(",");
                        var lstMucLuc = _bhDmMucLucNganSachService.FindByCondition(x => x.INamLamViec == yearOfWork && lstLNS.Contains(x.SLNS));
                        QtcNamKinhPhiKhacCriteria searchCondition = new QtcNamKinhPhiKhacCriteria();
                        searchCondition.NamLamViec = yearOfWork;
                        searchCondition.IDMaDonVi = item.IID_MaDonVi;
                        searchCondition.IDDonVi = item.IID_DonVi;
                        searchCondition.SNguoiTao = item.SNguoiTao;
                        searchCondition.LoaiChungTu = !IsDonViRoot(item.IID_MaDonVi) ? BhxhLoaiChungTu.BhxhChungTu : BhxhLoaiChungTu.BhxhChungTuTongHop;
                        searchCondition.ID = item.Id;
                        searchCondition.SLNS = sLNS;
                        searchCondition.IDLoaiChi = item.IID_LoaiChi;
                        searchCondition.DNgayChungTu = DateTime.Now;
                        searchCondition.IsTongHop4Quy = item.BThucChiTheo4Quy;
                        _listChungTuChiTiet = _kinhPhiKhacChiTietService.FindChungTuChiTiet(searchCondition).ToList();

                        var lstChungTuChiTiet = _mapper.Map<ObservableCollection<BhQtcNamKinhPhiKhacChiTietModel>>(_listChungTuChiTiet).ToList();
                        CalculateData(lstChungTuChiTiet);
                        lstChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi) && x.IsHangCha).Select(x =>
                        {
                            x.FTien_DuToanNamTruocChuyenSang += x.FDuToanNamTruocChuyenSang;
                            return x;
                        }).ToList();
                        var listkhcMucLucsOrders = lstChungTuChiTiet;
                        lstChungTuChiTiet = lstChungTuChiTiet.Where(x => x.IsHasData).ToList();
                        Dictionary<string, object> Data = new Dictionary<string, object>();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);

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

                        //Data.Add("TitleFirst", $"QUYẾT TOÁN CHI KINH PHÍ KHÁC NĂM {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})");
                        Data.Add("TxtTitleThird", string.Empty);
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("Cap1", _sessionService.Current.TenDonViTrucThuoc);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("h2", item.STenDonVi);
                        Data.Add("h1", item.STenDonVi);
                        Data.Add("ListData", lstChungTuChiTiet);
                        Data.Add("SKTML", lstMucLuc.OrderBy(x => x.SXauNoiMa));
                        //Data.Add("SKTML", listkhcMucLucsOrders);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_NAMKPK, ExportFileName.RPT_BH_QTC_NKPK_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = item.SSoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhQtcNamKinhPhiKhacModel, BhDmMucLucNganSach, BhQtcNamKinhPhiKhacChiTietModel>(templateFileName, Data);
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

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;
        private void CalculateData(List<BhQtcNamKinhPhiKhacChiTietModel> lstChungTuChiTiet)
        {
            try
            {
                lstChungTuChiTiet.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     x.FTien_DuToanNamTruocChuyenSang = 0;
                     //x.FTien_DuToanGiaoNamNay = 0
                     x.FTien_ThucChi = 0;
                 });

                var temp = lstChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
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

        private void CalculateParent(Guid idParent, BhQtcNamKinhPhiKhacChiTietModel item, Dictionary<Guid?, BhQtcNamKinhPhiKhacChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
            model.FTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
            model.FTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;
            model.FTien_ThucChi += item.FTien_ThucChi;

            CalculateParent(model.IdParent, item, dictByMlns);
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
            OpenDetailDialog((BhQtcNamKinhPhiKhacModel)obj);
        }

        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhQtcNamKinhPhiKhacModel)obj;
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

        #region Add chung tu
        protected override void OnAdd()
        {
            try
            {
                QuyetToanNamChiKinhPhiKhacDialogViewModel.Model = new BhQtcNamKinhPhiKhacModel();
                QuyetToanNamChiKinhPhiKhacDialogViewModel.IsAgregate = false;
                QuyetToanNamChiKinhPhiKhacDialogViewModel.Init();
                QuyetToanNamChiKinhPhiKhacDialogViewModel.SavedAction = obj =>
                {
                    var chungTu = (BhQtcNamKinhPhiKhacModel)obj;
                    this.LoadData();
                    if (chungTu != null)
                    {
                        OpenDetailDialog(chungTu);
                    }
                };
                var exportView = new QuyetToanNamChiKinhPhiKhacDialog() { DataContext = QuyetToanNamChiKinhPhiKhacDialogViewModel };
                DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void OpenDetailDialog(BhQtcNamKinhPhiKhacModel chungTu)
        {
            try
            {
                QuyetToanNamChiKinhPhiKhacDetailViewModel.Model = ObjectCopier.Clone(chungTu);
                QuyetToanNamChiKinhPhiKhacDetailViewModel.Init();
                var view = new QuyetToanNamChiKinhPhiKhacDetail()
                {
                    DataContext = QuyetToanNamChiKinhPhiKhacDetailViewModel
                };

                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region OnUpdate
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
                    QuyetToanNamChiKinhPhiKhacDialogViewModel.IsAgregate = false;
                    QuyetToanNamChiKinhPhiKhacDialogViewModel.IsSummary = false;
                    QuyetToanNamChiKinhPhiKhacDialogViewModel.Model = SelectedItem;
                    QuyetToanNamChiKinhPhiKhacDialogViewModel.Init();
                    QuyetToanNamChiKinhPhiKhacDialogViewModel.SavedAction = obj => this.OnRefresh();
                    QuyetToanNamChiKinhPhiKhacDialogViewModel.ShowDialogHost();
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAggregateEdit()
        {
            try
            {
                //kiểm tra trạng thái các bản ghi
                List<BhQtcNamKinhPhiKhacModel> selectedSKhcChungTus
                                                                        = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop)
                                                                          && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();
                QuyetToanNamChiKinhPhiKhacDialogViewModel.IsSummary = true;
                QuyetToanNamChiKinhPhiKhacDialogViewModel.IsAgregate = true;
                QuyetToanNamChiKinhPhiKhacDialogViewModel.LstNamKinhPhiKhacModel = selectedSKhcChungTus;
                QuyetToanNamChiKinhPhiKhacDialogViewModel.Model = SelectedItem;
                QuyetToanNamChiKinhPhiKhacDialogViewModel.Init();
                QuyetToanNamChiKinhPhiKhacDialogViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhQtcNamKinhPhiKhacModel)obj);
                };
                var addView = new QuyetToanNamChiKinhPhiKhacDialog()
                {
                    DataContext = QuyetToanNamChiKinhPhiKhacDialogViewModel
                };
                DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);

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
                OpenDetailDialog((BhQtcNamKinhPhiKhacModel)eventArgs.Parameter);
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
                    _kinhPhiKhacService.LockOrUnLock(item.Id, isLock);
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
            List<BhQtcNamKinhPhiKhacModel> lstNamKinhPhiQuanLyModel = Items.Where(x => x.IsSelected).ToList();
            bool checkAllowAggregate = lstNamKinhPhiQuanLyModel.Any(x => x.BIsKhoa);
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
            if (Items.Where(x => x.IsSelected && x.IsFilter).GroupBy(x => new { x.IID_LoaiChi }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopLoaiKeHoachChi, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra cùng giai đoạn 
            if (Items.Where(x => x.IsSelected && x.IsFilter).GroupBy(x => x.INamLamViec).ToList().Count > 1)
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

            List<BhQtcNamKinhPhiKhacModel> lstNamKinhPhiQuanLyModel = Items.Where(x => x.IsSelected).ToList();
            QuyetToanNamChiKinhPhiKhacDialogViewModel.IsSummary = true;
            QuyetToanNamChiKinhPhiKhacDialogViewModel.IsAgregate = true;
            QuyetToanNamChiKinhPhiKhacDialogViewModel.LstNamKinhPhiKhacModel = lstNamKinhPhiQuanLyModel.ToList();
            QuyetToanNamChiKinhPhiKhacDialogViewModel.Model = new BhQtcNamKinhPhiKhacModel();
            QuyetToanNamChiKinhPhiKhacDialogViewModel.Init();
            QuyetToanNamChiKinhPhiKhacDialogViewModel.SavedAction = obj =>
            {
                TabIndex = VoucherTabIndex.VOUCHER;
                this.OnRefresh();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcNamKinhPhiKhacModel)obj);
            };

            var view = new QuyetToanNamChiKinhPhiKhacDialog
            {
                DataContext = QuyetToanNamChiKinhPhiKhacDialogViewModel
            };

            DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
        }
        #endregion
    }
}
