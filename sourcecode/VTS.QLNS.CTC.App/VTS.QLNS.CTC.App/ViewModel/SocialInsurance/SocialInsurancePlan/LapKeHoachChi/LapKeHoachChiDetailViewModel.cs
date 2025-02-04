using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi
{
    public class LapKeHoachChiDetailViewModel : DetailViewModelBase<BhKhcCheDoBhXhModel, BhKhcCheDoBhXhChiTietModel>
    {
        #region Interface
        private readonly ISessionService _sessionService;
        private readonly IBhKhcCheDoBhXhChiTietService _khcCheDoBhXhChiTietService;
        private readonly IBhKhcCheDoBhXhService _bhKhcCheDoBhXhService;
        private readonly INsDonViService _iNsDonViService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private ICollection<BhKhcCheDoBhXhChiTietModel> _lstFilterResult = new HashSet<BhKhcCheDoBhXhChiTietModel>();
        private ICollectionView _khcBHXHChiTietModelsView { get; set; }
        #endregion

        #region Property
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        private string xnmConcatenation = "";
        private BhKhcCheDoBhXhModel _ctTongHop;
        public BhKhcCheDoBhXhModel CtTongHop
        {
            get => _ctTongHop;
            set => SetProperty(ref _ctTongHop, value);
        }

        public bool IsPropertyChange { get; set; }
        public bool IsVoucherSummary { get; set; }
        public bool IsOpenPrintPopup = true;
        public bool isChungTuTongHop { get; set; }
        public bool IsAggregate => !string.IsNullOrEmpty(Model.STongHop);
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set
            {
                SetProperty(ref _isLock, value);
                OnPropertyChanged(nameof(IsEnabledDelete));
            }
        }
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                                || Items.Any(x => !x.IsHangCha);
        public int IndexDataState { get; set; }
        public PrintReportLapKeHoachChiViewModel PrintReportLapKeHoachChiViewModel { get; set; }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
                BeForeRefresh();
                _khcBHXHChiTietModelsView.Refresh();
                CalculateData();
            }
        }
        public string HeaderSoDaThucHienNam => "Số đã thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderUocThucHienNam => "Ước thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderKehoachThucHienNam => "KH thực hiện năm " + (_sessionService.Current.YearOfWork);
        public bool IsChungTuTongHop => Model != null;
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public bool IsInit { get; set; }
        public override Type ContentType => typeof(LapKeHoachChiDetail);

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private ComboboxItem _typeDisplaysSelected;
        public ComboboxItem TypeDisplaysSelected
        {
            get => _typeDisplaysSelected;
            set
            {
                if (SetProperty(ref _typeDisplaysSelected, value) && _khcBHXHChiTietModelsView != null)
                {
                    if (_typeDisplaysSelected != null && _typeDisplaysSelected.ValueItem == TypeDisplay.CHITIET_DONVI)
                        _isShowColumnUnit = true;
                    else _isShowColumnUnit = false;
                    LoadData();
                    OnPropertyChanged(nameof(IsShowColumnUnit));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _typeShows;
        public ObservableCollection<ComboboxItem> TypeShows
        {
            get => _typeShows;
            set => SetProperty(ref _typeShows, value);
        }

        private ComboboxItem _typeShowsSelected;
        public ComboboxItem TypeShowsSelected
        {
            get { return _typeShowsSelected; }
            set
            {
                if (SetProperty(ref _typeShowsSelected, value) && _khcBHXHChiTietModelsView != null)
                {
                    OnRefresh();
                    _khcBHXHChiTietModelsView.Refresh();
                }
            }
        }

        public bool IsShowAgencyFilter => IsVoucherSummary && TypeDisplaysSelected != null && TypeDisplaysSelected.ValueItem == TypeDisplay.CHITIET_DONVI;
        private bool _isShowColumnUnit;
        public bool IsShowColumnUnit
        {
            get => _isShowColumnUnit;
            set => SetProperty(ref _isShowColumnUnit, value);
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhKhcCheDoBhXhChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhKhcCheDoBhXhChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhKhcCheDoBhXhChiTietModel _selectedPopupItem;
        public BhKhcCheDoBhXhChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SMoTa;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<BhKhcCheDoBhXhChiTietModel> _dataSearch;
        public ObservableCollection<BhKhcCheDoBhXhChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        #endregion

        #region Constructor
        public LapKeHoachChiDetailViewModel(
            IBhKhcCheDoBhXhChiTietService khcBHXHChiTietService,
            IBhKhcCheDoBhXhService khcBHXHService,
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            PrintReportLapKeHoachChiViewModel printReportDemandOrgViewModel,
            INsDonViService iNsDonViService,
            ISysAuditLogService log)
        {
            _khcCheDoBhXhChiTietService = khcBHXHChiTietService;
            _bhKhcCheDoBhXhService = khcBHXHService;
            _iNsDonViService = iNsDonViService;
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            PrintReportLapKeHoachChiViewModel = printReportDemandOrgViewModel;

            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(obj => OnPrintDetal(obj));
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            IsPropertyChange = false;
            _sessionInfo = _sessionService.Current;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            IsInit = true;
            LoadTypeDisplay();
            LoadTypeShow();
            LoadData();
            IsInit = false;
        }
        #endregion

        #region Close
        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            _khcBHXHChiTietModelsView.Refresh();
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }
        #endregion

        #region On save
        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }
            Func<BhKhcCheDoBhXhChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<BhKhcCheDoBhXhChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhKhcCheDoBhXhChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhKhcCheDoBhXhChiTiet>();

                detailsAdd.ForEach(x =>
                {
                    x.INamLamViec = Model.INamLamViec.Value;
                    x.IIDMaDonVi = Model.IID_MaDonVi;
                    x.DNgayTao = DateTime.Now;
                    x.SNguoiTao = _sessionInfo.Principal;
                });

                _mapper.Map(detailsAdd, addItems);
                _khcCheDoBhXhChiTietService.AddRange(addItems);
                var khcCheDoBhXhByID = _bhKhcCheDoBhXhService.FindById(detailsAdd[0].IID_KHC_CheDoBHXH);
                OnUpdateKhcCheDoBhXh(khcCheDoBhXhByID);

                Items.Where(isAdd).Select(x =>
                {
                    x.IsModified = false;
                    x.IsAdd = false;
                    return x;
                }).ToList();
            }

            //cập chứng từ chi tiết
            if (detailsUpdate.Count > 0)
            {
                foreach (var updateItem in detailsUpdate)
                {
                    var khtBHXHChiTiet = _khcCheDoBhXhChiTietService.FindById(updateItem.Id);
                    updateItem.IIDMaDonVi = Model.IID_MaDonVi;
                    updateItem.INamLamViec = Model.INamLamViec.Value;
                    updateItem.SNguoiSua = _sessionInfo.Principal;
                    updateItem.DNgaySua = DateTime.Now;
                    _mapper.Map(updateItem, khtBHXHChiTiet);
                    _khcCheDoBhXhChiTietService.Update(khtBHXHChiTiet);
                    var khcCheDoBhXhByID = _bhKhcCheDoBhXhService.FindById(detailsUpdate[0].IID_KHC_CheDoBHXH);
                    OnUpdateKhcCheDoBhXh(khcCheDoBhXhByID);
                    updateItem.IsModified = false;
                }

            }


            OnRefresh();
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            var message = Resources.MsgSaveDone;
            var messageBox = new NSMessageBoxViewModel(message);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            DialogHost.Show(messageBox.Content, DemandCheckScreen.DETAIL_DIALOG);
        }
        private void OnUpdateKhcCheDoBhXh(BhKhcCheDoBhXh khcCheDoBhXhByID)
        {
            try
            {
                khcCheDoBhXhByID.ITongSoDaThucHienNamTruoc = Model.ITongSoDaThucHienNamTruoc;
                khcCheDoBhXhByID.FTongTienDaThucHienNamTruoc = Model.FTongTienDaThucHienNamTruoc;

                khcCheDoBhXhByID.ITongSoUocThucHienNamTruoc = Model.ITongSoUocThucHienNamTruoc;
                khcCheDoBhXhByID.FTongTienUocThucHienNamTruoc = Model.FTongTienUocThucHienNamTruoc;

                khcCheDoBhXhByID.ITongSoKeHoachThucHienNamNay = Model.ITongSoKeHoachThucHienNamNay;
                khcCheDoBhXhByID.FTongTienKeHoachThucHienNamNay = Model.FTongTienKeHoachThucHienNamNay;

                khcCheDoBhXhByID.ITongSoSQ = Model.ITongSoSQ;
                khcCheDoBhXhByID.FTongTienSQ = Model.FTongTienSQ;

                khcCheDoBhXhByID.ITongSoQNCN = Model.ITongSoQNCN;
                khcCheDoBhXhByID.FTongTienQNCN = Model.FTongTienQNCN;

                khcCheDoBhXhByID.ITongSoCNVQP = Model.ITongSoCNVQP;
                khcCheDoBhXhByID.FTongTienQNCN = Model.FTongTienQNCN;

                khcCheDoBhXhByID.ITongSoLDHD = Model.ITongSoLDHD;
                khcCheDoBhXhByID.FTongTienLDHD = Model.FTongTienLDHD;

                khcCheDoBhXhByID.ITongSoHSQBS = Model.ITongSoHSQBS;
                khcCheDoBhXhByID.FTongTienHSQBS = Model.FTongTienHSQBS;

                khcCheDoBhXhByID.SNguoiSua = _sessionService.Current.Principal;
                khcCheDoBhXhByID.DNgaySua = DateTime.Now;
                _bhKhcCheDoBhXhService.Update(khcCheDoBhXhByID);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Refresh
        protected override void OnRefresh()
        {
            IsInit = true;
            LoadTypeDisplay();
            LoadData();
            IsInit = false;
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            List<BhKhcCheDoBhXhChiTiet> listChungTuChiTiet = new List<BhKhcCheDoBhXhChiTiet>();
            Items = new ObservableCollection<BhKhcCheDoBhXhChiTietModel>();
            KhcCheDoBhXhChiTietCriteria searchCondition = new KhcCheDoBhXhChiTietCriteria();
            searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var donViOfCurrentUser = GetNsDonViOfCurrentUser();
                if (IsVoucherSummary && TypeDisplaysSelected != null && TypeDisplaysSelected.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    List<string> soChungTus = Model.STongHop.Split(",").ToList();
                    var predicate = PredicateBuilder.True<BhKhcCheDoBhXh>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && soChungTus.Contains(x.SSoChungTu));
                    List<BhKhcCheDoBhXh> lstChungTu = _bhKhcCheDoBhXhService.FindByCondition(predicate).ToList();
                    List<BhKhcCheDoBhXhChiTiet> listChungTuChiTietParent = new List<BhKhcCheDoBhXhChiTiet>();
                    List<BhKhcCheDoBhXhChiTiet> listChungTuChiTietChildren = new List<BhKhcCheDoBhXhChiTiet>();
                    foreach (var chungTu in lstChungTu)
                    {
                        searchCondition.KhcBhxhId = chungTu.Id;
                        searchCondition.IdDonVi = chungTu.IID_MaDonVi;
                        var lstQuery = _khcCheDoBhXhChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                        listChungTuChiTietParent.AddRange(lstQuery.Where(x => x.IsHangCha));
                        listChungTuChiTietChildren.AddRange(lstQuery.Where(x => !x.IsHangCha));
                    }
                    var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                    listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa)))
                                                                        .GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct()
                                                                        .ToList();
                    listChungTuChiTiet.AddRange(listChungTuChiTietParent);
                    listChungTuChiTiet.AddRange(listChungTuChiTietChildren);
                    listChungTuChiTiet = listChungTuChiTiet.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIdMaDonVi).ToList();
                    string agencyIds = string.Join(",", lstChungTu.Select(x => x.IID_MaDonVi));
                    LoadAgencies(agencyIds);

                    listChungTuChiTiet.Where(x => x.IsHangCha).ForAll
                    (x =>
                    {
                        x.STenDonVi = string.Empty;
                    });
                }
                else
                {
                    searchCondition.IdDonVi = Model.IID_MaDonVi;
                    searchCondition.KhcBhxhId = Model.Id.IsNullOrEmpty() ? Model.IID_BH_KHC_CheDoBHXH : Model.Id;
                    listChungTuChiTiet = _khcCheDoBhXhChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                }

                var existBhChiTiet = _khcCheDoBhXhChiTietService.ExistBHXHChiTiet(Model.Id);
                foreach (var item in listChungTuChiTiet)
                {
                    item.IsAuToFillTuChi = !existBhChiTiet;
                }

            }, (s, e) =>
            {
                Items = _mapper.Map<ObservableCollection<BhKhcCheDoBhXhChiTietModel>>(listChungTuChiTiet);
                CalculateData();
                _khcBHXHChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
                _khcBHXHChiTietModelsView.Filter = ItemsViewFilter;
                foreach (var khcBhxhChiTietModel in Items)
                {
                    khcBhxhChiTietModel.IsFilter = true;
                    if (!khcBhxhChiTietModel.IsHangCha)
                    {
                        khcBhxhChiTietModel.PropertyChanged += (sender, args) =>
                        {
                            BhKhcCheDoBhXhChiTietModel item = (BhKhcCheDoBhXhChiTietModel)sender;
                            item.IsModified = true;
                            if (IsPropertyChange)
                            {
                                CalculateData();
                            }
                            khcBhxhChiTietModel.IsModified = true;
                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsOpenPrintPopup));
                            OnPropertyChanged(nameof(BhKhcCheDoBhXhChiTietModel.FTienKeHoachThucHienNamNay));

                        };
                    }
                }
            });
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhKhcCheDoBhXhChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
                result = DataSearch.Any(x => x.Id.Equals(item.Id));
            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (BhKhcCheDoBhXhChiTietModel)obj;

            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && (item.FTienKeHoachThucHienNamNay.GetValueOrDefault(0) != 0 || item.ISoKeHoachThucHienNamNay.GetValueOrDefault(0) != 0);
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsHadDataChil;
            }

            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void CalculateData()
        {
            IsPropertyChange = false;
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.ISoDaThucHienNamTruoc = 0;
                    x.FTienDaThucHienNamTruoc = 0;
                    x.ISoUocThucHienNamTruoc = 0;
                    x.FTienUocThucHienNamTruoc = 0;
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
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }
            IsPropertyChange = true;
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            Model.ITongSoDaThucHienNamTruoc = 0;
            Model.FTongTienDaThucHienNamTruoc = 0;

            Model.ITongSoUocThucHienNamTruoc = 0;
            Model.FTongTienUocThucHienNamTruoc = 0;

            Model.ITongSoKeHoachThucHienNamNay = 0;
            Model.FTongTienKeHoachThucHienNamNay = 0;

            Model.ITongSoSQ = 0;
            Model.FTongTienSQ = 0;

            Model.ITongSoQNCN = 0;
            Model.FTongTienQNCN = 0;

            Model.ITongSoCNVQP = 0;
            Model.FTongTienCNVQP = 0;

            Model.ITongSoLDHD = 0;
            Model.FTongTienLDHD = 0;

            Model.ITongSoHSQBS = 0;
            Model.FTongTienHSQBS = 0;

            var roots = Items.Where(t => t.IdParent.Equals(Guid.Empty)).ToList();

            foreach (var item in roots)
            {
                Model.ITongSoDaThucHienNamTruoc += item.ISoDaThucHienNamTruoc;
                Model.FTongTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;

                Model.ITongSoUocThucHienNamTruoc += item.ISoUocThucHienNamTruoc;
                Model.FTongTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;

                Model.ITongSoKeHoachThucHienNamNay += item.ISoKeHoachThucHienNamNay;
                Model.FTongTienKeHoachThucHienNamNay += item.FTienKeHoachThucHienNamNay;

                Model.ITongSoSQ += item.ISoSQ;
                Model.FTongTienSQ += item.FTienSQ;

                Model.ITongSoQNCN += item.ISoQNCN;
                Model.FTongTienQNCN += item.FTienQNCN;

                Model.ITongSoCNVQP += item.ISoCNVQP;
                Model.FTongTienCNVQP += item.FTienCNVQP;

                Model.ITongSoLDHD += item.ISoLDHD;
                Model.FTongTienLDHD += item.FTienLDHD;

                Model.ITongSoHSQBS += item.ISoHSQBS;
                Model.FTongTienHSQBS += item.FTienHSQBS;
            }
        }

        private void CalculateParent(Guid idParent, BhKhcCheDoBhXhChiTietModel item, Dictionary<Guid?, BhKhcCheDoBhXhChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.ISoDaThucHienNamTruoc += item.ISoDaThucHienNamTruoc;
            model.FTienDaThucHienNamTruoc += item.FTienDaThucHienNamTruoc;

            model.ISoUocThucHienNamTruoc += item.ISoUocThucHienNamTruoc;
            model.FTienUocThucHienNamTruoc += item.FTienUocThucHienNamTruoc;

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

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            TypeDisplaysSelected = TypeDisplays.FirstOrDefault();
            OnPropertyChanged(nameof(TypeDisplaysSelected));
        }

        private void LoadTypeShow()
        {
            TypeShows = new ObservableCollection<ComboboxItem>();
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.BH_TAT_CA, DisplayItem = TypeDisplay.BH_TAT_CA });
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_SO_LIEU, DisplayItem = TypeDisplay.CO_SO_LIEU });
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.CHUA_CO_SO_LIEU, DisplayItem = TypeDisplay.CHUA_CO_SO_LIEU });
            TypeShowsSelected = TypeShows.FirstOrDefault();
            OnPropertyChanged(nameof(TypeShowsSelected));
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _iNsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _iNsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        private void BeForeRefresh()
        {
            _lstFilterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _lstFilterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }
        #endregion

        #region  SelectedItemChanged
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }
        #endregion

        #region Filter
        private void OnSearch()
        {
            SearchTextFilter();
        }

        #endregion

        #region Print
        private void OnPrintDetal(object param)
        {
            var khcBhXhCheckPrintType = (KhcCheckPrintType)((int)param);
            object content;
            switch (khcBhXhCheckPrintType)
            {
                case KhcCheckPrintType.KHCBHXHCT:
                    PrintReportLapKeHoachChiViewModel.KhcCheckPrintType = khcBhXhCheckPrintType;
                    PrintReportLapKeHoachChiViewModel.IsShowTheoTongHop = true;
                    PrintReportLapKeHoachChiViewModel.IsSummary = false;
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
                    PrintReportLapKeHoachChiViewModel.KhcCheckPrintType = khcBhXhCheckPrintType;
                    PrintReportLapKeHoachChiViewModel.IsShowTheoTongHop = false;
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
                DialogHost.Show(content, SystemConstants.DETAIL_DIALOG, null, null);
            }
        }
        #endregion

        #region Search

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhKhcCheDoBhXhChiTietModel> results = new List<BhKhcCheDoBhXhChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = Items.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = Items.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = Items.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhKhcCheDoBhXhChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhKhcCheDoBhXhChiTietModel>();
            }
            _khcBHXHChiTietModelsView.Refresh();
        }

        private List<BhKhcCheDoBhXhChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhKhcCheDoBhXhChiTietModel> result = new List<BhKhcCheDoBhXhChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhKhcCheDoBhXhChiTietModel> lstInput, List<BhKhcCheDoBhXhChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhKhcCheDoBhXhChiTietModel>() { item }, results);
                }
            }
        }

        #endregion
    }
}