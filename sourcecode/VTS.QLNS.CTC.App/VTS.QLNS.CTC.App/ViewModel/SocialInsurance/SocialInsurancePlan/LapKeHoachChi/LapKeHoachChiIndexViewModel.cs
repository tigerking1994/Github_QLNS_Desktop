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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.ImportKhcBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi
{
    public class LapKeHoachChiIndexViewModel : GridViewModelBase<BhKhcCheDoBhXhModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IBhKhcCheDoBhXhService _bhKeHoachChiService;
        private readonly IBhKhcCheDoBhXhChiTietService _bhKhcCheDoBhXhChiTietService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private ICollectionView _bhKhKeHoachChiView;
        private ICollectionView _nsDonViModelsView;
        #endregion

        #region Property
        private VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.ImportKhcBHXH.ImportKhcBHXH _importKhcBHXH;
        public override string GroupName => MenuItemContants.GROUP_CHI;
        public override string Name => "KH chi chế độ BHXH";
        public override string Description => "Danh sách kế hoạch chi chế độ BHXH " + _sessionService.Current.YearOfWork;
        public override Type ContentType => typeof(LapKeHoachChiIndex);
        //public override PackIconKind IconKind => PackIconKind.Money;
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }
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

        private ObservableCollection<DonViModel> _nsDonViModelItems;
        public ObservableCollection<DonViModel> NsDonViModelItems
        {
            get => _nsDonViModelItems;
            set => SetProperty(ref _nsDonViModelItems, value);
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

        public string HeaderSoDaThucHienNam => "Số đã thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderUocThucHienNam => "Ước thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderKehoachThucHienNam => "KH thực hiện năm " + (_sessionService.Current.YearOfWork);
        public bool IsExportAggregateData => Items != null && Items.Any(n => n.IsSelected);
        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsAggregate => Items.Any(x => x.IsSelected);
        public bool IsEnableLock => SelectedItem != null;
        public bool IsExportDataFilter => _selectedBhLapKeHoachChiModel != null;
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);

        private ComboboxItem _voucherTypeSelected;
        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                SetProperty(ref _voucherTypeSelected, value);
                if (_voucherTypeSelected != null)
                {
                    LoadData();
                }
                IsAllItemsSelected = false;
                IsAllItemSummariesSelected = false;
                UnCheckBoxAll();
            }
        }

        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (Items != null)
                {
                    List<bool> selected = Items.Select(item => item.IsSelected).Distinct().ToList();
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
        private string _searchSoKeHoachText;
        public string SearchSoKeHoachText
        {
            get => _searchSoKeHoachText;
            set => SetProperty(ref _searchSoKeHoachText, value);
        }
        private List<BhKhcCheDoBhXhModel> _lstChungTuOrigin;
        public List<BhKhcCheDoBhXhModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }
        private void LoadLockStatus()
        {
            List<ComboboxItem> lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }
        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    List<bool> selected = Items.Select(item => item.IsSelected).Distinct().ToList();
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
                bool result = false;
                List<BhKhcCheDoBhXhModel> lstSelected = Items.Where(x => x.IsSelected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    List<BhKhcCheDoBhXhModel> lstSelectedKhoa = lstSelected.Where(x => x.BIsKhoa).ToList();
                    List<BhKhcCheDoBhXhModel> lstSelectedMo = lstSelected.Where(x => !x.BIsKhoa).ToList();
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

        private BhKhcCheDoBhXhModel _selectedBhLapKeHoachChiModel;
        public BhKhcCheDoBhXhModel SelectedBhLapKeHoachChiModel
        {
            get => _selectedBhLapKeHoachChiModel;
            set
            {
                SetProperty(ref _selectedBhLapKeHoachChiModel, value);
                if (_selectedBhLapKeHoachChiModel != null)
                {
                    IsLock = _selectedBhLapKeHoachChiModel.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                }

                if (_selectedBhLapKeHoachChiModel == null)
                {
                    IsEdit = false;
                }

                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private ObservableCollection<ComboboxItem> _projectTypeItems;
        public ObservableCollection<ComboboxItem> ProjectTypeItems
        {
            get => _projectTypeItems;
            set => SetProperty(ref _projectTypeItems, value);
        }

        private ComboboxItem _selectedProjectType;
        public ComboboxItem SelectedProjectType
        {
            get => _selectedProjectType;
            set
            {
                SetProperty(ref _selectedProjectType, value);
                if (_bhKhKeHoachChiView != null) _bhKhKeHoachChiView.Refresh();
            }
        }
        public bool IsCensorship
        {
            get
            {
                IEnumerable<BhKhcCheDoBhXhModel> itemSelected = Items.Where(x => x.IsSelected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BIsKhoa);
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

        #region ViewModel
        public LapKeHoachChiDialogViewModel LapKeHoachChiDialogViewModel { get; set; }
        public LapKeHoachChiDetailViewModel LapKeHoachChiDetailViewModel { get; set; }
        public ImportKhcBHXHViewModel ImportKhcBHXHViewModel { get; set; }
        public PrintReportLapKeHoachChiViewModel PrintReportLapKeHoachChiViewModel { get; set; }
        #endregion

        #region Constructor
        public LapKeHoachChiIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IBhKhcCheDoBhXhService bhKhKeHoachChiService,
            IBhKhcCheDoBhXhChiTietService bhKhcCheDoBhXhChiTietService,
            IExportService exportService,
            INsNguoiDungDonViService iNguoiDungDonViService,

            ImportKhcBHXHViewModel importKhcBHXHViewModel,
            LapKeHoachChiDialogViewModel lapKeHoachChiDialogViewModel,
            LapKeHoachChiDetailViewModel lapKeHoachChiDetailViewModel,
            PrintReportLapKeHoachChiViewModel printReportLapKeHoachChiViewModel,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _bhKeHoachChiService = bhKhKeHoachChiService;
            _bhKhcCheDoBhXhChiTietService = bhKhcCheDoBhXhChiTietService;

            ImportKhcBHXHViewModel = importKhcBHXHViewModel;
            LapKeHoachChiDialogViewModel = lapKeHoachChiDialogViewModel;
            LapKeHoachChiDetailViewModel = lapKeHoachChiDetailViewModel;
            PrintReportLapKeHoachChiViewModel = printReportLapKeHoachChiViewModel;
            _iNguoiDungDonViService = iNguoiDungDonViService;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            SearchCommand = new RelayCommand(obj => SearchData());
            RefreshCommand = new RelayCommand(obj => OnResetFilter());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
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
                LoadNsDonVi();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
                LapKeHoachChiDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
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
            if (_bhKhKeHoachChiView != null)
            {
                _bhKhKeHoachChiView.Refresh();
            }
        }

        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhKhcCheDoBhXhModel)obj;
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
        private void LoadNsDonVi()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                NsDonViModelItems = new ObservableCollection<DonViModel>();
                List<string> idDonVis = Items.Select(x => x.IID_MaDonVi).ToList();
                predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));

                List<DonVi> listUnit = _nsDonViService.FindByCondition(predicate).ToList();

                NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
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
                IOrderedEnumerable<Core.Domain.Query.BhKhcCheDoBhXhQuery> listChungTu = _bhKeHoachChiService.FindIndex().Where(x => x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.DNgayChungTu);
                _lstChungTuOrigin = _mapper.Map<List<BhKhcCheDoBhXhModel>>(listChungTu);
                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == VoucherTabIndex.VOUCHER)
                    {
                        Items = _mapper.Map<ObservableCollection<BhKhcCheDoBhXhModel>>(_lstChungTuOrigin.Where(x => x.ILoaiTongHop == KhcBhxhLoaiChungTu.BhxhChungTu && !x.BDaTongHop));
                    }
                    else
                    {
                        List<Core.Domain.Query.BhKhcCheDoBhXhQuery> listCTTongHop = listChungTu.Where(x => x.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi) && x.iLoaiTongHop == KhcBhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
                        List<BhKhcCheDoBhXhModel> listTongHop = new List<BhKhcCheDoBhXhModel>();
                        foreach (Core.Domain.Query.BhKhcCheDoBhXhQuery ctTongHop in listCTTongHop)
                        {
                            BhKhcCheDoBhXhModel parent = _mapper.Map<BhKhcCheDoBhXhModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);
                            if (!string.IsNullOrEmpty(ctTongHop.sTongHop))
                            {
                                List<BhKhcCheDoBhXhModel> listChild = _mapper.Map<List<BhKhcCheDoBhXhModel>>(listChungTu.Where(x => ctTongHop.sTongHop != null && ctTongHop.sTongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }
                        Items = _mapper.Map<ObservableCollection<BhKhcCheDoBhXhModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhKhcCheDoBhXhModel>>(listChungTu.Where(x => x.iLoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop));
                }

                LoadNsDonVi();

                foreach (BhKhcCheDoBhXhModel model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhKhcCheDoBhXhModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsLock));
                        }
                        if (args.PropertyName == nameof(BhKhcCheDoBhXhModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }


                _bhKhKeHoachChiView = CollectionViewSource.GetDefaultView(Items);
                _bhKhKeHoachChiView.Filter = BhKhKeHoachChiViewFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool BhKhKeHoachChiViewFilter(object obj)
        {
            if (!(obj is BhKhcCheDoBhXhModel temp)) return true;
            string keyword = SearchText?.Trim().ToLower() ?? string.Empty.Trim().ToLower();
            bool condition1 = false;
            bool condition2 = true;
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

            bool result = condition1 && condition2;
            temp.IsFilter = result;
            return result;
        }

        private void ExpandChild()
        {
            Items?.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        private void OnResetFilter()
        {
            try
            {
                SearchSoKeHoachText = string.Empty;
                NsDonViModelItems = null;
                LoadNsDonVi();
                LoadData();
                SelectedProjectType = null;
                if (_bhKhKeHoachChiView != null) _bhKhKeHoachChiView.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Delete
        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem != null && (SelectedItem.BIsKhoa)) return;
                if (SelectedItem != null)
                {
                    BhKhcCheDoBhXh entity = _bhKeHoachChiService.FindById(SelectedItem.IID_BH_KHC_CheDoBHXH);

                    if (entity != null && !string.IsNullOrEmpty(entity.SNguoiTao) && !entity.SNguoiTao.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SNguoiTao), Resources.Alert);
                        return;
                    }
                }

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? DateTimeExtension.ToStringDate(SelectedItem.DNgayChungTu.Value) : string.Empty);
                NSMessageBoxViewModel messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
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
                    _bhKeHoachChiService.Delete(SelectedItem.IID_BH_KHC_CheDoBHXH);
                }

                if (SelectedItem.IID_TongHopID.IsNullOrEmpty() || string.IsNullOrEmpty(SelectedItem.STongHop))
                {
                    List<BhKhcCheDoBhXhChiTiet> lstKeHoachChiTiet = _bhKhcCheDoBhXhChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_KHC_CheDoBHXH).ToList();
                    if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                    {
                        foreach (BhKhcCheDoBhXhChiTiet item in lstKeHoachChiTiet)
                        {
                            _bhKhcCheDoBhXhChiTietService.Delete(item.Id);
                        }
                    }
                }
                else
                {
                    string[] lstSoCtChild = SelectedItem.STongHop.Split(",");
                    foreach (string soct in lstSoCtChild)
                    {
                        BhKhcCheDoBhXh ctChild = _bhKeHoachChiService.FindByCondition(x => x.SSoChungTu.Equals(soct)
                                && x.INamLamViec == _sessionService.Current.YearOfWork).FirstOrDefault();
                        if (ctChild != null)
                        {
                            ctChild.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTu;
                            ctChild.BDaTongHop = false;
                            _bhKeHoachChiService.Update(ctChild);
                        }
                    }
                    List<BhKhcCheDoBhXhChiTiet> lstKeHoachChiTiet = _bhKhcCheDoBhXhChiTietService.FindByIdChiTiet(SelectedItem.IID_BH_KHC_CheDoBHXH).ToList();
                    if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                    {
                        foreach (BhKhcCheDoBhXhChiTiet item in lstKeHoachChiTiet)
                        {
                            _bhKhcCheDoBhXhChiTietService.Delete(item.Id);
                        }
                    }
                }

                BhKhcCheDoBhXhModel itemDeleted = Items.Where(x => x.IID_BH_KHC_CheDoBHXH == SelectedItem.IID_BH_KHC_CheDoBHXH).First();
                Items.Remove(itemDeleted);
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region SelectedItemChanged
        protected override void OnSelectedItemChanged()
        {
            if (SelectedItem != null)
            {
                OnPropertyChanged(nameof(IsAggregate));
            }
        }
        #endregion

        #region LockUnLock
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
                List<BhKhcCheDoBhXhModel> lstSelected = Items.Where(x => x.IsSelected).ToList();
                bool isLock = !lstSelected.FirstOrDefault().BIsKhoa;
                foreach (BhKhcCheDoBhXhModel item in lstSelected)
                {
                    _bhKeHoachChiService.LockOrUnlock(item.IID_BH_KHC_CheDoBHXH, isLock);
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

        #region ConfirmAggregate
        private void ConfirmAggregate()
        {
            List<BhKhcCheDoBhXhModel> selectedBhDmCheDoBhXhChungtu = Items.Where(x => x.IsSelected).ToList();
            bool checkAllowAggregate = selectedBhDmCheDoBhXhChungtu.All(x => x.BIsKhoa);
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
            if (Items.Where(x => x.IsSelected).Any(x => !x.BIsKhoa))
            {
                MessageBox.Show(Resources.AlertAggregateUnLocked, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra cùng giai đoạn
            if (Items.Where(x => x.IsSelected).GroupBy(x => new { x.INamLamViec }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopKeHoachVonUng, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
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

            List<BhKhcCheDoBhXhModel> selectedKhcCheDoBhXhChungTus = Items.Where(x => x.IsSelected && x.BIsKhoa).ToList();

            LapKeHoachChiDialogViewModel.IsSummary = true;
            LapKeHoachChiDialogViewModel.IsDetail = false;
            LapKeHoachChiDialogViewModel.ListIdsBhKhcCheDoBhXhModel = selectedKhcCheDoBhXhChungTus;
            LapKeHoachChiDialogViewModel.Model = new BhKhcCheDoBhXhModel();
            LapKeHoachChiDialogViewModel.Name = "THÊM MỚI KẾ HOACH CHI";
            LapKeHoachChiDialogViewModel.Description = "Tạo mới chứng từ tổng hợp kế hoạch chi chế độ BHXH";
            LapKeHoachChiDialogViewModel.IsAgregate = true;
            LapKeHoachChiDialogViewModel.Init();
            LapKeHoachChiDialogViewModel.SavedAction = obj =>
            {
                TabIndex = VoucherTabIndex.VOUCHER;
                this.OnRefresh();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhcCheDoBhXhModel)obj);
            };

            LapKeHoachChiDialog view = new LapKeHoachChiDialog
            {
                DataContext = LapKeHoachChiDialogViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG);
        }
        #endregion

        #region On add
        protected override void OnAdd()
        {
            try
            {
                LapKeHoachChiDialogViewModel.Model = new BhKhcCheDoBhXhModel();
                LapKeHoachChiDialogViewModel.BhKhcCheDoBhXhModel = new BhKhcCheDoBhXhModel();
                LapKeHoachChiDialogViewModel.IsDetail = true;
                LapKeHoachChiDialogViewModel.IsAgregate = false;
                LapKeHoachChiDialogViewModel.IsSummary = false;
                LapKeHoachChiDialogViewModel.Name = "THÊM MỚI KẾ HOACH CHI";
                LapKeHoachChiDialogViewModel.Description = "Tạo mới kế hoạch chi chế độ BHXH";
                LapKeHoachChiDialogViewModel.Init();
                LapKeHoachChiDialogViewModel.SavedAction = obj =>
                {
                    BhKhcCheDoBhXhModel khcChungTu = (BhKhcCheDoBhXhModel)obj;
                    this.LoadData();
                    if (khcChungTu != null)
                    {
                        OpenDetailDialog(khcChungTu);
                    }
                };
                LapKeHoachChiDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Update
        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhKhcCheDoBhXhModel> selectedSKhcChungTus = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();

            LapKeHoachChiDialogViewModel.IsAgregate = true;
            LapKeHoachChiDialogViewModel.IsSummary = true;
            LapKeHoachChiDialogViewModel.IsDetail = false;
            LapKeHoachChiDialogViewModel.ListIdsBhKhcCheDoBhXhModel = selectedSKhcChungTus;
            LapKeHoachChiDialogViewModel.BhKhcCheDoBhXhModel = new BhKhcCheDoBhXhModel();
            LapKeHoachChiDialogViewModel.Name = "CẬP NHẬT KẾ HOẠCH CHI";
            LapKeHoachChiDialogViewModel.Description = "Cập nhật chứng từ tổng hợp kế hoạch chi chế độ BHXH";
            LapKeHoachChiDialogViewModel.Model = SelectedItem;
            LapKeHoachChiDialogViewModel.Init();
            LapKeHoachChiDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhcCheDoBhXhModel)obj);

            };
            LapKeHoachChiDialog addView = new LapKeHoachChiDialog() { DataContext = LapKeHoachChiDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhKhcCheDoBhXhModel)eventArgs.Parameter);
        }

        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem.IID_MaDonVi.Equals(_sessionService.Current.IdDonVi))
                {
                    OnAggregateEdit();
                }
                else
                {
                    LapKeHoachChiDialogViewModel.IsDetail = true;
                    LapKeHoachChiDialogViewModel.IsSummary = false;
                    LapKeHoachChiDialogViewModel.Model = SelectedItem;
                    LapKeHoachChiDialogViewModel.BhKhcCheDoBhXhModel = SelectedItem;
                    LapKeHoachChiDialogViewModel.Name = "CẬP NHẬT KẾ HOẠCH CHI";
                    LapKeHoachChiDialogViewModel.Description = "Cập nhật kế hoạch chi chế độ BHXH";
                    LapKeHoachChiDialogViewModel.Init();
                    LapKeHoachChiDialogViewModel.SavedAction = obj => this.OnRefresh();
                    LapKeHoachChiDialogViewModel.ShowDialogHost();
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region  Open Detail
        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhKhcCheDoBhXhModel)obj, false);
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="bhKhcCheDoBhXhModel"></param>
        private void OpenDetailDialog(BhKhcCheDoBhXhModel bhKhcCheDoBhXhModel, params bool[] isNew)
        {
            DonVi idDonViCurrent = GetNsDonViOfCurrentUser();
            BhKhcCheDoBhXhModel chungTuTH = Items.FirstOrDefault(item => item.IID_MaDonVi.Equals(idDonViCurrent.IIDMaDonVi));
            LapKeHoachChiDetailViewModel.Model = ObjectCopier.Clone(bhKhcCheDoBhXhModel);
            LapKeHoachChiDetailViewModel.CtTongHop = chungTuTH;
            LapKeHoachChiDetailViewModel.IsVoucherSummary = bhKhcCheDoBhXhModel.IID_MaDonVi.Equals(idDonViCurrent.IIDMaDonVi) && !string.IsNullOrEmpty(bhKhcCheDoBhXhModel.STongHop);
            LapKeHoachChiDetailViewModel.Init();

            LapKeHoachChiDetail view = new LapKeHoachChiDetail() { DataContext = LapKeHoachChiDetailViewModel };
            view.ShowDialog();
        }

        #endregion

        #region SelectAll
        protected override void OnRefresh()
        {
            this.LoadData();
        }

        private void UnCheckBoxAll()
        {
            foreach (BhKhcCheDoBhXhModel item in Items)
            {
                item.IsSelected = false;
            }
        }

        private static void SelectAll(bool select, IEnumerable<BhKhcCheDoBhXhModel> models)
        {
            foreach (BhKhcCheDoBhXhModel model in models.Where(x => x.IsFilter))
            {
                model.IsSelected = select;
            }
        }

        #endregion

        #region Import data
        private void OnImportData()
        {
            try
            {
                ImportKhcBHXHViewModel.SavedAction = obj =>
                {
                    _importKhcBHXH.Close();
                    this.OnRefresh();
                    OpenDetailDialog((BhKhcCheDoBhXhModel)obj);
                };
                ImportKhcBHXHViewModel.Init();
                _importKhcBHXH = new View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.ImportKhcBHXH.ImportKhcBHXH { DataContext = ImportKhcBHXHViewModel };
                _importKhcBHXH.ShowDialog();
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
            KhcCheckPrintType khcBhXhCheckPrintType = (KhcCheckPrintType)((int)param);
            object content;
            switch (khcBhXhCheckPrintType)
            {
                case KhcCheckPrintType.KHCBHXHCT:
                    PrintReportLapKeHoachChiViewModel.KhcCheckPrintType = khcBhXhCheckPrintType;

                    PrintReportLapKeHoachChiViewModel.IsSummary = false;
                    PrintReportLapKeHoachChiViewModel.IsShowTheoTongHop = true;
                    PrintReportLapKeHoachChiViewModel.Name = "In kế hoạch chi các chế độ BHXH";
                    PrintReportLapKeHoachChiViewModel.Description = "In kế hoạch chi các chế độ BHXH";
                    PrintReportLapKeHoachChiViewModel.Init();

                    content = new PrintReportKeHoachChiCheDoBhXhChiTiet
                    {
                        DataContext = PrintReportLapKeHoachChiViewModel
                    };

                    break;
                case KhcCheckPrintType.KHCBHXHTH:
                    PrintReportLapKeHoachChiViewModel.Name = " In dự toán chi các chế độ BHXH";
                    PrintReportLapKeHoachChiViewModel.Description = "In dự toán chi các chế độ BHXH";
                    PrintReportLapKeHoachChiViewModel.IsShowTheoTongHop = false;
                    PrintReportLapKeHoachChiViewModel.KhcCheckPrintType = khcBhXhCheckPrintType;
                    PrintReportLapKeHoachChiViewModel.Init();
                    PrintReportLapKeHoachChiViewModel.IsSummary = true;
                    content = new PrintReportKeHoachChiCheDoBhXhChiTiet
                    {
                        DataContext = PrintReportLapKeHoachChiViewModel
                    };
                    break;

                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.ROOT_DIALOG, null, null);
            }
        }
        #endregion

        #region Excel
        /// <summary>
        /// Xuất excel chứng từ lập kế hoạch thu BHXH
        /// </summary>
        private void OnExportAggregateData()
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

                    List<BhKhcCheDoBhXhModel> khcBhxhModelsSummary = Items.Where(x => x.IsSelected).ToList();

                    int yearOfWork = _sessionService.Current.YearOfWork;

                    foreach (BhKhcCheDoBhXhModel item in khcBhxhModelsSummary)
                    {
                        DonVi currentDonVi = GetNsDonViOfCurrentUser();

                        KhcCheDoBhXhChiTietCriteria searchCondition = new KhcCheDoBhXhChiTietCriteria();
                        searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                        searchCondition.IdDonVi = item.IID_MaDonVi;
                        searchCondition.KhcBhxhId = item.IID_BH_KHC_CheDoBHXH;
                        List<BhKhcCheDoBhXhChiTiet> khcMucLucsOrder = _bhKhcCheDoBhXhChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                        List<BhDmMucLucNganSach> lstMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(searchCondition.NamLamViec, LNSValue.LNS_9010001_9010002);
                        List<BhKhcCheDoBhXhChiTietModel> listkhcMucLucsOrders = _mapper.Map<ObservableCollection<BhKhcCheDoBhXhChiTietModel>>(khcMucLucsOrder).ToList();

                        CalculateData(listkhcMucLucsOrders);
                        listkhcMucLucsOrders = listkhcMucLucsOrders.Where(x => x.ISoDaThucHienNamTruoc > 0 || x.FTienDaThucHienNamTruoc > 0
                                                                            || x.ISoUocThucHienNamTruoc > 0 || x.FTienUocThucHienNamTruoc > 0
                                                                            || x.ISoKeHoachThucHienNamNay > 0 || x.FTienKeHoachThucHienNamNay > 0
                                                                            || x.ISoSQ > 0 || x.FTienSQ > 0
                                                                            || x.ISoQNCN > 0 || x.FTienQNCN > 0
                                                                            || x.ISoCNVQP > 0 || x.FTienCNVQP > 0
                                                                            || x.ISoLDHD > 0 || x.FTienLDHD > 0
                                                                            || x.ISoHSQBS > 0 || x.FTienHSQBS > 0).ToList();

                        Dictionary<string, object> Data = new Dictionary<string, object>();


                        int? ITongSoNgayNamTruoc = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.ISoUocThucHienNamTruoc).Sum();
                        double? FTongTienNamTruoc = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienUocThucHienNamTruoc).Sum();
                        int? ITongSoNgayNamNay = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.ISoKeHoachThucHienNamNay).Sum();
                        double? FTongTienNamNay = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienKeHoachThucHienNamNay).Sum();
                        int? ITongSoSQ = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.ISoSQ).Sum();
                        double? FTongTienSQ = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienSQ).Sum();
                        int? ITongSoQNCN = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.ISoQNCN).Sum();
                        double? FTongTienQNCN = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienQNCN).Sum();
                        int? ITongSoCNVQP = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.ISoCNVQP).Sum();
                        double? FTongTienCNVQP = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienCNVQP).Sum();
                        int? ITongSoLDHD = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.ISoLDHD).Sum();
                        double? FTongTienLDHD = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienLDHD).Sum();
                        int? ITongSoHSQBS = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.ISoHSQBS).Sum();
                        double? FTongTienHSQBS = listkhcMucLucsOrders.Where(x => !x.IsHangCha).Select(x => x.FTienHSQBS).Sum();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"KẾ HOẠCH CHI CHẾ ĐỘ BHXH {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", $"Ngày chứng từ: {DateUtils.Format(item.DNgayChungTu)}");
                        Data.Add("TxtTitleThird", string.Empty);
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("ITongSoNgayNamTruoc", ITongSoNgayNamTruoc);
                        Data.Add("FTongTienNamTruoc", FTongTienNamTruoc);
                        Data.Add("ITongSoNgayNamNay", ITongSoNgayNamNay);
                        Data.Add("FTongTienNamNay", FTongTienNamNay);
                        Data.Add("ITongSoSQ", ITongSoSQ);
                        Data.Add("FTongTienSQ", FTongTienSQ);
                        Data.Add("ITongSoQNCN", ITongSoQNCN);
                        Data.Add("FTongTienQNCN", FTongTienQNCN);
                        Data.Add("ITongSoCNVQP", ITongSoCNVQP);
                        Data.Add("FTongTienCNVQP", FTongTienCNVQP);
                        Data.Add("ITongSoLDHD", ITongSoLDHD);
                        Data.Add("FTongTienLDHD", FTongTienLDHD);
                        Data.Add("ITongSoHSQBS", ITongSoHSQBS);
                        Data.Add("FTongTienHSQBS", FTongTienHSQBS);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap2", currentDonVi.TenDonVi);
                        Data.Add("Cap1", _sessionInfo.TenDonViTrucThuocReportHeader);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("h2", "Lữ đoàn X");
                        Data.Add("h1", "Lữ đoàn X");
                        Data.Add("ListData", listkhcMucLucsOrders);
                        Data.Add("SKTML", lstMLNS);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHC, ExportFileName.RPT_BH_KHC_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = StringUtils.ConvertVN(item.SSoChungTu + "_" + item.IID_MaDonVi + "_" + item.STenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        ExcelFile xlsFile = _exportService.Export<BhKhcCheDoBhXhModel, BhDmMucLucNganSach, BhKhcCheDoBhXhChiTietModel>(templateFileName, Data);
                        TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
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

        private DonVi GetNsDonViOfCurrentUser()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            DonVi nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private void CalculateData(List<BhKhcCheDoBhXhChiTietModel> lstKhcChungTuChiTiet)
        {
            lstKhcChungTuChiTiet.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.ISoDaThucHienNamTruoc = 0;
                    x.FTienDaThucHienNamTruoc = 0;
                    x.ISoUocThucHienNamTruoc = 0;
                    x.FTienUocThucHienNamTruoc = 0;
                    //x.ISoKeHoachThucHienNamNay = 0;
                    // x.FTienKeHoachThucHienNamNay = 0;
                    x.ISoSQ = 0;
                    x.FTienSQ = 0;
                    x.ISoQNCN = 0;
                    x.FTienQNCN = 0;
                    x.ISoCNVQP = 0;
                    x.FTienCNVQP = 0;
                    x.ISoLDHD = 0;
                    x.FTienLDHD = 0;
                    x.ISoHSQBS = 0;
                    x.FTienHSQBS = 0;
                    return x;
                }).ToList();
            IEnumerable<BhKhcCheDoBhXhChiTietModel> temp = lstKhcChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (BhKhcCheDoBhXhChiTietModel item in temp)
            {
                CalculateParent(item.IdParent, item, lstKhcChungTuChiTiet);
            }
        }
        private void CalculateParent(Guid idParent, BhKhcCheDoBhXhChiTietModel item, List<BhKhcCheDoBhXhChiTietModel> lstKhcChungTuChiTiet)
        {
            Dictionary<Guid?, BhKhcCheDoBhXhChiTietModel> dictByMlns = lstKhcChungTuChiTiet.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }
            BhKhcCheDoBhXhChiTietModel model = dictByMlns[idParent];
            model.ISoDaThucHienNamTruoc += item.ISoDaThucHienNamTruoc;
            model.FTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;

            model.ISoUocThucHienNamTruoc += item.ISoUocThucHienNamTruoc;
            model.FTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;

            //model.ISoKeHoachThucHienNamNay += item.ISoKeHoachThucHienNamNay;
            // model.FTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;

            model.ISoSQ += item.ISoSQ;
            model.FTienSQ += item.FTienSQ;

            model.ISoQNCN += item.ISoQNCN;
            model.FTienQNCN += item.FTienQNCN;

            model.ISoCNVQP += item.ISoCNVQP;
            model.FTienCNVQP += item.FTienCNVQP;

            model.ISoLDHD += item.ISoLDHD;
            model.FTienLDHD += item.FTienLDHD;

            model.ISoHSQBS += item.ISoHSQBS;
            model.FTienHSQBS += item.FTienHSQBS;

            CalculateParent(model.IdParent, item, lstKhcChungTuChiTiet);
        }
        #endregion
    }
}
