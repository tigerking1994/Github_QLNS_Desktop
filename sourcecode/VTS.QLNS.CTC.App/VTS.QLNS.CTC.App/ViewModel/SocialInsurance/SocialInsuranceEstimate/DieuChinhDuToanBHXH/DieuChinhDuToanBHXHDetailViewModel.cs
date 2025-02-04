using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH
{
    public class DieuChinhDuToanBHXHDetailViewModel : DetailViewModelBase<BhDtcDcdToanChiModel, BhDtcDcdToanChiChiTietModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView _chungTuChiTietItemsView;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private readonly IBhDtcDcdToanChiChiTietService _bhDtcDcdToanChiChiTietService;
        private readonly IBhDtcDcdToanChiService _bhDcdToanChiService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhQtcQuyKPKService _qtcQuyKPKService;
        private readonly IBhQtcQuyKinhPhiQuanLyService _bhQtcQuyKinhPhiQuanLyService;
        private readonly IQtcqBHXHService _qtcqBHXHService;
        private readonly IQtcqKCBService _qtcqKCBService;
        private string xnmConcatenation = "";
        #endregion

        #region Property
        public bool IsAggregate => !string.IsNullOrEmpty(Model.STongHop);
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsOpenPrintPopup = true;
        public bool IsPropertyChange { get; set; }
        public bool IsShowAgencyFilter => IsAggregate && _selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI;
        public bool IsEnableQuater => !Model.BIsKhoa;
        public bool IsShowQuater => string.IsNullOrEmpty(Model.STongHop);
        private ICollection<BhDtcDcdToanChiChiTietModel> _filterResult = new HashSet<BhDtcDcdToanChiChiTietModel>();
        List<BhDtcDcdToanChiChiTietQuery> lstDtBhxhQuanNhan;
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


        public bool IsInit { get; set; }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set
            {
                if (SetProperty(ref _sNoiDungSearch, value))
                {
                    SearchTextFilter();
                    _ItemViews.Refresh();
                    //_budgetCatalogItemsView.Refresh();
                }
            }
        }

        private bool _isShowColumnUnit;
        public bool IsShowColumnUnit
        {
            get => _isShowColumnUnit;
            set => SetProperty(ref _isShowColumnUnit, value);
        }

        private ObservableCollection<ComboboxItem> _typeShowAgencyKHT;
        public ObservableCollection<ComboboxItem> TypeShowAgencyKHT
        {
            get => _typeShowAgencyKHT;
            set => SetProperty(ref _typeShowAgencyKHT, value);
        }

        private ComboboxItem _selectedTypeShowAgencyKHT;
        public ComboboxItem SelectedTypeShowAgencyKHT
        {
            get => _selectedTypeShowAgencyKHT;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgencyKHT, value))
                {
                    if (_selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI)
                        _isShowColumnUnit = true;
                    else _isShowColumnUnit = false;
                    LoadData();
                    OnPropertyChanged(nameof(IsShowColumnUnit));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }

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
                if (_selectedAgency != null)
                {
                    LoadData();
                }
            }
        }

        private ObservableCollection<BhDtcDcdToanChiChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhDtcDcdToanChiChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhDtcDcdToanChiChiTietModel _selectedPopupItem;
        public BhDtcDcdToanChiChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SNoiDung;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }
        private ICollectionView _ItemViews;

        private ObservableCollection<BhDtcDcdToanChiChiTietModel> _dataSearch;
        public ObservableCollection<BhDtcDcdToanChiChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        public bool IsChungTuTongHop => Model != null;
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(DieuChinhDuToanBHXHDetail);
        #endregion

        #region ViewModel
        PrintReportDieuChinhDuToanViewModel PrintReportDieuChinhDuToanViewModel { get; set; }
        PrintReportDieuChinhDuToanTheoLanViewModel PrintReportDieuChinhDuToanTheoLanViewModel { get; set; }
        #endregion

        #region RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand GetQuaterCommand { get; }
        #endregion

        #region Constructor
        public DieuChinhDuToanBHXHDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IBhDtcDcdToanChiChiTietService bhDtcDcdToanChiChiTietService,
            IBhDtcDcdToanChiService bhDcdToanChiService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog loger,
            PrintReportDieuChinhDuToanViewModel printReportDieuChinhDuToanViewModel,
            PrintReportDieuChinhDuToanTheoLanViewModel printReportDieuChinhDuToanTheoLanViewModel,
            IBhQtcQuyKPKService bhQtcQuyKPKService,
            IBhQtcQuyKinhPhiQuanLyService bhQtcQuyKinhPhiQuanLyService,
            IQtcqBHXHService qtcqBHXHService,
            IQtcqKCBService qtcqKCBService,
            INsDonViService nsDonViService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = loger;
            _bhDtcDcdToanChiChiTietService = bhDtcDcdToanChiChiTietService;
            _bhDcdToanChiService = bhDcdToanChiService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _nsDonViService = nsDonViService;
            _qtcqBHXHService = qtcqBHXHService;
            _qtcqKCBService = qtcqKCBService;
            _qtcQuyKPKService = bhQtcQuyKPKService;
            _bhQtcQuyKinhPhiQuanLyService = bhQtcQuyKinhPhiQuanLyService;

            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintCommand = new RelayCommand(obj => OnPrintDetal(obj));
            PrintReportDieuChinhDuToanViewModel = printReportDieuChinhDuToanViewModel;
            PrintReportDieuChinhDuToanTheoLanViewModel = printReportDieuChinhDuToanTheoLanViewModel;
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            GetQuaterCommand = new RelayCommand(obj => GetDataQuater(obj));
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            IsPropertyChange = false;
            _isShowColumnUnit = false;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            if (!string.IsNullOrEmpty(Model.STongHop))
            {
                LoadComboboxTypeShow();
            }
            IsInit = true;
            _selectedAgency = null;
            LoadData();
            IsInit = false;
            OnClearSearch(false);
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            lstDtBhxhQuanNhan = new List<BhDtcDcdToanChiChiTietQuery>();
            BhDtcDcdToanChiChiTietCriteria searchCondition = new BhDtcDcdToanChiChiTietCriteria();
            List<BhDtcDcdToanChiChiTietQuery> temp = new List<BhDtcDcdToanChiChiTietQuery>();
            searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
            searchCondition.IdDonVi = Model.IID_MaDonVi;
            searchCondition.ILoaiDanhMucChi = Model.IID_LoaiCap;
            searchCondition.DtcDcdToanChiId = Model.Id.IsNullOrEmpty() ? Model.IID_BH_DTC : Model.Id;
            searchCondition.LNS = Model.SLNS == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : Model.SLNS;
            searchCondition.NgayChungTu = Model.DNgayChungTu;
            searchCondition.ILoaiTongHop = Model.ILoaiTongHop;
            searchCondition.MaLoaiChi = Model.SMaLoaiChi;
            if (IsAggregate && SelectedTypeShowAgencyKHT != null && SelectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
            {
                var voucherNos = Model.STongHop.Split(",").ToList();
                List<BhDtcDcdToanChi> listChungTu = _bhDcdToanChiService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork).ToList();
                List<BhDtcDcdToanChiChiTietQuery> listChungTuChiTietParent = new List<BhDtcDcdToanChiChiTietQuery>();
                List<BhDtcDcdToanChiChiTietQuery> listChungTuChiTietChildren = new List<BhDtcDcdToanChiChiTietQuery>();

                foreach (var chungTu in listChungTu)
                {
                    searchCondition.DtcDcdToanChiId = chungTu.Id;
                    searchCondition.IdDonVi = chungTu.IID_MaDonVi;
                    List<BhDtcDcdToanChiChiTietQuery> listQuery = _bhDtcDcdToanChiChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                }
                var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                var listDataQuery = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x =>
                      new
                      {
                          Data = x.FirstOrDefault(),
                          FTienDuToanDuocGiao = x.Sum(x => x.FTienDuToanDuocGiao)
                      }
                  ).ToList();
                listDataQuery.ForEach(x =>
                {
                    x.Data.FTienDuToanDuocGiao = x.FTienDuToanDuocGiao;
                });
                listChungTuChiTietParent = listDataQuery.Select(x => x.Data).ToList();

                //listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).Distinct().ToList();
                temp.AddRange(listChungTuChiTietParent);
                temp.AddRange(listChungTuChiTietChildren);
                temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIdMaDonVi).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IID_MaDonVi));
                LoadAgencies(agencyIds);
            }
            else
            {
                if (_selectedAgency != null)
                {
                    searchCondition.IdDonVi = _selectedAgency.ValueItem;
                    var predicateCtDv = PredicateBuilder.True<BhDtcDcdToanChi>();
                    predicateCtDv = predicateCtDv.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    predicateCtDv = predicateCtDv.And(x => x.SMaLoaiChi == Model.SMaLoaiChi);
                    predicateCtDv = predicateCtDv.And(x => x.IID_MaDonVi == _selectedAgency.ValueItem);
                    var ctDonVi = _bhDcdToanChiService.FindByCondition(predicateCtDv).FirstOrDefault();
                    if (ctDonVi != null)
                    {
                        searchCondition.DtcDcdToanChiId = ctDonVi.Id;
                    }
                }
                temp = _bhDtcDcdToanChiChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
            }

            temp.ForEach(x =>
            {
                if (x.BHangCha)
                {
                    x.STenDonVi = string.Empty;
                }
            });

            var existBhChiTiet = _bhDtcDcdToanChiChiTietService.ExistDTChiTiet(searchCondition.DtcDcdToanChiId);
            Items = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(temp);

            var lstDuToan = Items.Where(x => x.BHangChaDuToan.HasValue).ToList();

            CalculateDataDuToan(lstDuToan);

            var listChungTuChiTietMap = _mapper.Map<List<BhDtcDcdToanChiChiTietModel>>(Items);

            Items = new ObservableCollection<BhDtcDcdToanChiChiTietModel>(listChungTuChiTietMap);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhDtcDcdToanChiChiTietModel>>(listChungTuChiTietMap);
            _ItemViews = CollectionViewSource.GetDefaultView(Items);
            _ItemViews.Filter = ItemsViewFilter;
            foreach (var khcKcbChiTietModel in Items)
            {
                khcKcbChiTietModel.IsFilter = true;
                if (!khcKcbChiTietModel.IsHangCha)
                {
                    khcKcbChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhDtcDcdToanChiChiTietModel item = (BhDtcDcdToanChiChiTietModel)sender;
                        if (args.PropertyName.Equals(nameof(BhDtcDcdToanChiChiTietModel.FTienThucHien06ThangDauNam))
                        || args.PropertyName.Equals(nameof(BhDtcDcdToanChiChiTietModel.SGhiChu))
                        || args.PropertyName.Equals(nameof(BhDtcDcdToanChiChiTietModel.FTienUocThucHien06ThangCuoiNam)))
                        {
                            if (!IsInit)
                            {
                                item.IsModified = true;
                                khcKcbChiTietModel.IsModified = true;
                                khcKcbChiTietModel.FTienUocThucHienCaNam = khcKcbChiTietModel.FTienThucHien06ThangDauNam.GetValueOrDefault(0) + khcKcbChiTietModel.FTienUocThucHien06ThangCuoiNam.GetValueOrDefault(0);
                                khcKcbChiTietModel.FTienSoSanhTang = (((khcKcbChiTietModel.FTienUocThucHienCaNam.GetValueOrDefault() - khcKcbChiTietModel.FTienDuToanDuocGiao.GetValueOrDefault()) > 0)
                                                                    ? khcKcbChiTietModel.FTienUocThucHienCaNam.GetValueOrDefault() - khcKcbChiTietModel.FTienDuToanDuocGiao.GetValueOrDefault() : 0);
                                khcKcbChiTietModel.FTienSoSanhGiam = (((khcKcbChiTietModel.FTienDuToanDuocGiao.GetValueOrDefault() - khcKcbChiTietModel.FTienUocThucHienCaNam.GetValueOrDefault()) > 0)
                                                                    ? khcKcbChiTietModel.FTienDuToanDuocGiao.GetValueOrDefault() - khcKcbChiTietModel.FTienUocThucHienCaNam.GetValueOrDefault() : 0);
                                CalculateData();
                            }

                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsOpenPrintPopup));
                        }

                    };
                    if (!existBhChiTiet && khcKcbChiTietModel.FTienThucHien06ThangDauNam.GetValueOrDefault(0) != 0)
                    {
                        khcKcbChiTietModel.FTienSoSanhTang = (((khcKcbChiTietModel.FTienUocThucHienCaNam.GetValueOrDefault() - khcKcbChiTietModel.FTienDuToanDuocGiao.GetValueOrDefault()) > 0) ? khcKcbChiTietModel.FTienUocThucHienCaNam - khcKcbChiTietModel.FTienDuToanDuocGiao : 0);
                        khcKcbChiTietModel.FTienSoSanhGiam = (((khcKcbChiTietModel.FTienDuToanDuocGiao.GetValueOrDefault() - khcKcbChiTietModel.FTienUocThucHienCaNam.GetValueOrDefault()) > 0) ? khcKcbChiTietModel.FTienDuToanDuocGiao - khcKcbChiTietModel.FTienUocThucHienCaNam : 0);
                        khcKcbChiTietModel.IsModified = true;
                        OnPropertyChanged(nameof(IsSaveData));
                    }
                }
            }

            CalculateData();
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgencyKHT = new ObservableCollection<ComboboxItem>();
            TypeShowAgencyKHT.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgencyKHT.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgencyKHT = TypeShowAgencyKHT.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgencyKHT));
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        private void CalculateData()
        {
            IsInit = true;
            Items.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FTienThucHien06ThangDauNam = 0;
                   x.FTienUocThucHien06ThangCuoiNam = 0;
                   x.FTienUocThucHienCaNam = 0;
                   x.FTienSoSanhTang = 0;
                   x.FTienSoSanhGiam = 0;
               });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }
            IsInit = false;


            foreach (var item in Items)
            {
                if (!((item.IsHangCha) && (item.BHangChaDuToan.HasValue)))
                {
                    item.FTienSoSanhGiam = 0;
                    item.FTienSoSanhTang = 0;
                }
                else
                {
                    item.FTienSoSanhTang = (((item.FTienUocThucHienCaNam.GetValueOrDefault() - item.FTienDuToanDuocGiao.GetValueOrDefault()) > 0) ? item.FTienUocThucHienCaNam - item.FTienDuToanDuocGiao : 0);
                    item.FTienSoSanhGiam = (((item.FTienDuToanDuocGiao.GetValueOrDefault() - item.FTienUocThucHienCaNam.GetValueOrDefault()) > 0) ? item.FTienDuToanDuocGiao - item.FTienUocThucHienCaNam : 0);
                }
            }


            UpdateTotal(temp);
        }

        private void CalculateDataDuToan(List<BhDtcDcdToanChiChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value)
               .ForAll(x =>
               {
                   x.FTienDuToanDuocGiao = 0;
               });

            var temp = lstChungTuChiTiet.Where(x => x.BHangChaDuToan.HasValue && !x.BHangChaDuToan.Value).ToList();
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParentDuToan(item.IdParent, item, dictByMlns);
            }

            //UpdateTotal(temp);
        }

        private void UpdateTotal(List<BhDtcDcdToanChiChiTietModel> temp)
        {
            Model.FTienDuToanDuocGiao = 0;
            Model.FTienThucHien06ThangDauNam = 0;
            Model.FTienUocThucHien06ThangCuoiNam = 0;
            Model.FTienUocThucHienCaNam = 0;
            Model.FTienSoSanhTang = 0;
            Model.FTienSoSanhGiam = 0;
            Model.FTienDuToanDuocGiao = Items.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanDuocGiao).Sum();
            Model.FTienThucHien06ThangDauNam = Items.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienThucHien06ThangDauNam).Sum();
            Model.FTienUocThucHien06ThangCuoiNam = Items.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienUocThucHien06ThangCuoiNam).Sum();
            Model.FTienUocThucHienCaNam = Model.FTienThucHien06ThangDauNam + Model.FTienUocThucHien06ThangCuoiNam;

            var lstItems = Items.Where(x => string.IsNullOrEmpty(x.SM)).ToList();
            foreach (var item in lstItems)
            {
                Model.FTienSoSanhTang = Model.FTienSoSanhTang + (((item.FTienUocThucHienCaNam.GetValueOrDefault() - item.FTienDuToanDuocGiao.GetValueOrDefault()) > 0) ? item.FTienUocThucHienCaNam.GetValueOrDefault() - item.FTienDuToanDuocGiao.GetValueOrDefault() : 0);
                Model.FTienSoSanhGiam = Model.FTienSoSanhGiam + (((item.FTienDuToanDuocGiao.GetValueOrDefault() - item.FTienUocThucHienCaNam.GetValueOrDefault()) > 0) ? item.FTienDuToanDuocGiao.GetValueOrDefault() - item.FTienUocThucHienCaNam.GetValueOrDefault() : 0);
            }
        }

        private void CalculateParentDuToan(Guid idParent, BhDtcDcdToanChiChiTietModel item, Dictionary<Guid, BhDtcDcdToanChiChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao.GetValueOrDefault(0);

            CalculateParentDuToan(model.IdParent, item, dictByMlns);
        }

        private void CalculateParent(Guid idParent, BhDtcDcdToanChiChiTietModel item, Dictionary<Guid, BhDtcDcdToanChiChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            //model.FTienDuToanDuocGiao += item.FTienDuToanDuocGiao.GetValueOrDefault(0);
            model.FTienThucHien06ThangDauNam += item.FTienThucHien06ThangDauNam.GetValueOrDefault(0);
            model.FTienUocThucHien06ThangCuoiNam += item.FTienUocThucHien06ThangCuoiNam.GetValueOrDefault(0);
            model.FTienUocThucHienCaNam += item.FTienUocThucHienCaNam.GetValueOrDefault(0);
            //model.FTienSoSanhTang += item.FTienSoSanhTang.GetValueOrDefault(0);
            //model.FTienSoSanhGiam += item.FTienSoSanhGiam.GetValueOrDefault(0);

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        private void GetDataQuater(object obj)
        {
            int quy = (int)(GetQuater)(int)obj;
            var namLamViec = _sessionInfo.YearOfWork;
            var sLns = Model.SLNS;
            List<BhDtcDcdToanChiChiTietQuery> lstData = new List<BhDtcDcdToanChiChiTietQuery>();
            var predicateBHXH = PredicateBuilder.True<BhQtcqBHXH>();
            var predicateKCB = PredicateBuilder.True<BhQtcqKCB>();
            var predicateKPQL = PredicateBuilder.True<BhQtcQuyKinhPhiQuanLy>();
            var predicateKhac = PredicateBuilder.True<BhQtcQuyKPK>();
            predicateBHXH = predicateBHXH.And(x => x.INamChungTu == namLamViec && x.BIsKhoa && x.IIdMaDonVi == Model.IID_MaDonVi && x.IQuyChungTu <= quy);
            predicateKCB = predicateKCB.And(x => x.INamChungTu == namLamViec && x.BIsKhoa && x.IIdMaDonVi == Model.IID_MaDonVi && x.IQuyChungTu <= quy);
            predicateKPQL = predicateKPQL.And(x => x.INamChungTu == namLamViec && x.BIsKhoa && x.IID_MaDonVi == Model.IID_MaDonVi && x.IQuyChungTu <= quy);
            predicateKhac = predicateKhac.And(x => x.INamChungTu == namLamViec && x.BIsKhoa && x.IID_MaDonVi == Model.IID_MaDonVi && x.IQuyChungTu <= quy);

            //var chiCheDo = _qtcqBHXHService.FindByCondition(predicateBHXH);
            //var chiKQPL = _bhQtcQuyKinhPhiQuanLyService.FindByCondition(predicateKPQL);
            //var chiKCB = _qtcqKCBService.FindByCondition(predicateKCB);
            var chiKhac = _qtcQuyKPKService.FindByCondition(predicateKhac);

            switch (Model.SLNS)
            {
                case LNSValue.LNS_9010001_9010002:
                case LNSValue.LNS_901_9010001_9010002:
                    var chiCheDo = _qtcqBHXHService.FindByCondition(predicateBHXH);
                    if (chiCheDo.Any())
                    {
                        lstData = _bhDtcDcdToanChiChiTietService.GetSettlementData(namLamViec, Model.IID_MaDonVi, quy, sLns).ToList();
                    }
                    else
                    {
                        MessageBoxHelper.Info(string.Format(Resources.MsgSettlementData, BhDttThagQuy.GetDTC(quy)));
                    }
                    break;
                case LNSValue.LNS_9010003:
                    var chiKQPL = _bhQtcQuyKinhPhiQuanLyService.FindByCondition(predicateKPQL);
                    if (chiKQPL.Any())
                    {
                        lstData = _bhDtcDcdToanChiChiTietService.GetSettlementData(namLamViec, Model.IID_MaDonVi, quy, sLns).ToList();
                    }
                    else
                    {
                        MessageBoxHelper.Info(string.Format(Resources.MsgSettlementData, BhDttThagQuy.GetDTC(quy)));
                    }
                    break;
                case LNSValue.LNS_9010004_9010005:
                    var chiKCB = _qtcqKCBService.FindByCondition(predicateKCB);
                    if (chiKCB.Any())
                    {
                        lstData = _bhDtcDcdToanChiChiTietService.GetSettlementData(namLamViec, Model.IID_MaDonVi, quy, sLns).ToList();
                    }
                    else
                    {
                        MessageBoxHelper.Info(string.Format(Resources.MsgSettlementData, BhDttThagQuy.GetDTC(quy)));
                    }
                    break;
                case LNSValue.LNS_9010006_9010007:
                    predicateKhac = predicateKhac.And(x => x.SDSLNS == LNSValue.LNS_9010006_9010007);
                    var chiKTS = _qtcQuyKPKService.FindByCondition(predicateKhac);
                    if (chiKTS.Any())
                    {
                        lstData = _bhDtcDcdToanChiChiTietService.GetSettlementData(namLamViec, Model.IID_MaDonVi, quy, sLns).ToList();
                    }
                    else
                    {
                        MessageBoxHelper.Info(string.Format(Resources.MsgSettlementData, BhDttThagQuy.GetDTC(quy)));
                    }
                    break;
                case LNSValue.LNS_9010008:
                    predicateKhac = predicateKhac.And(x => x.SDSLNS == LNSValue.LNS_9010008);
                    var chiKCBBHYT = _qtcQuyKPKService.FindByCondition(predicateKhac);
                    if (chiKCBBHYT.Any())
                    {
                        lstData = _bhDtcDcdToanChiChiTietService.GetSettlementData(namLamViec, Model.IID_MaDonVi, quy, sLns).ToList();
                    }
                    else
                    {
                        MessageBoxHelper.Info(string.Format(Resources.MsgSettlementData, BhDttThagQuy.GetDTC(quy)));
                    }
                    break;
                case LNSValue.LNS_9010009:
                    predicateKhac = predicateKhac.And(x => x.SDSLNS == LNSValue.LNS_9010009);
                    var chiMSTTYT = _qtcQuyKPKService.FindByCondition(predicateKhac);
                    if (chiMSTTYT.Any())
                    {
                        lstData = _bhDtcDcdToanChiChiTietService.GetSettlementData(namLamViec, Model.IID_MaDonVi, quy, sLns).ToList();
                    }
                    else
                    {
                        MessageBoxHelper.Info(string.Format(Resources.MsgSettlementData, BhDttThagQuy.GetDTC(quy)));
                    }
                    break;
                case LNSValue.LNS_9010010:
                    predicateKhac = predicateKhac.And(x => x.SDSLNS == LNSValue.LNS_9010010);
                    var chiBHTN = _qtcQuyKPKService.FindByCondition(predicateKhac);
                    if (chiBHTN.Any())
                    {
                        lstData = _bhDtcDcdToanChiChiTietService.GetSettlementData(namLamViec, Model.IID_MaDonVi, quy, sLns).ToList();
                    }
                    else
                    {
                        MessageBoxHelper.Info(string.Format(Resources.MsgSettlementData, BhDttThagQuy.GetDTC(quy)));
                    }
                    break;
                default:
                    break;
            }

            if (lstData.Any())
            {
                var itemFilter = Items.Where(x => !x.IsHangCha && lstData.Select(s => s.SXauNoiMa).ToList().Contains(x.SXauNoiMa));
                foreach (var item in itemFilter)
                {
                    item.FTienThucHien06ThangDauNam = lstData.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FTienThucHien06ThangDauNam.GetValueOrDefault()).FirstOrDefault();
                }
                //CalculateData();
            }

        }

        #endregion

        #region On save
        public override void OnSave()
        {
            try
            {
                if (!IsSaveData)
                {
                    return;
                }

                Func<BhDtcDcdToanChiChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha && x.IsDataNotNull && x.Id == Guid.Empty;
                Func<BhDtcDcdToanChiChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha && x.IsDataNotNull && x.Id != Guid.Empty;
                Func<BhDtcDcdToanChiChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                //var detailsAddDuToan = detailsAdd.Where(x => (x.Id == Guid.Empty) && x.BHangChaDuToan.HasValue && !x.BHangChaDuToan.Value).ToList();
                //detailsAdd.AddRange(detailsAddDuToan);
                //detailsAdd = detailsAdd.Distinct().ToList();
                //var detailsUpdateDuToan = Items.Where(x => x.Id != Guid.Empty && x.BHangChaDuToan.HasValue && !x.BHangChaDuToan.Value).ToList();
                var detailsUpdate = Items.Where(isUpdate).ToList();

                //detailsUpdate.AddRange(detailsUpdateDuToan);
                var detailsDelete = Items.Where(isDelete).ToList();
                //detailsAdd = detailsAdd.Where(x => x.IRemainRow.Equals(0)).ToList();
                //detailsUpdate = detailsUpdate.Where(x => x.IRemainRow.Equals(0)).ToList();
                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhDtcDcdToanChiChiTiet>();
                    _mapper.Map(detailsAdd, addItems);
                    addItems = detailsAdd.Select(x => new BhDtcDcdToanChiChiTiet
                    {
                        Id = Guid.NewGuid(),
                        DNgayTao = DateTime.Now,
                        DNgaySua = DateTime.Now,
                        SNguoiTao = _sessionInfo.Principal,
                        IID_BH_DTC = Model.IID_BH_DTC.IsNullOrEmpty() ? Model.Id : Model.IID_BH_DTC,
                        IIdMaDonVi = x.IIDMaDonVi,
                        IID_MucLucNganSach = x.IID_MucLucNganSach,
                        SLNS = x.SLNS,
                        SM = x.SM,
                        STM = x.STM,
                        SNoiDung = x.SNoiDung,
                        STTM = x.STTM,
                        SXauNoiMa = x.SXauNoiMa,
                        INamLamViec = _sessionInfo.YearOfWork,
                        FTienDuToanDuocGiao = !x.IsHangCha ? x.FTienDuToanDuocGiao ?? 0 : 0,
                        FTienThucHien06ThangDauNam = !x.IsHangCha ? x.FTienThucHien06ThangDauNam ?? 0 : 0,
                        FTienUocThucHien06ThangCuoiNam = !x.IsHangCha ? x.FTienUocThucHien06ThangCuoiNam ?? 0 : 0,
                        FTienUocThucHienCaNam = !x.IsHangCha ? (x.FTienThucHien06ThangDauNam ?? 0) + (x.FTienUocThucHien06ThangCuoiNam ?? 0) : 0,
                        FTienSoSanhTang = x.IsHangCha ? ((x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao) > 0 ? x.FTienUocThucHienCaNam - x.FTienDuToanDuocGiao : 0) : 0,
                        FTienSoSanhGiam = x.IsHangCha ? ((x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam) > 0 ? x.FTienDuToanDuocGiao - x.FTienUocThucHienCaNam : 0) : 0,
                    }).ToList();

                    //addItems.ForEach(x => x.FTienUocThucHienCaNam = x.FTienThucHien06ThangDauNam + x.FTienUocThucHien06ThangCuoiNam);
                    _bhDtcDcdToanChiChiTietService.AddRange(addItems);

                    Items.Where(isAdd).Select(x =>
                    {
                        x.IsModified = false;
                        x.IsAdd = false;
                        return x;
                    }).ToList();
                }

                //cập nhật chứng từ chi tiết
                if (detailsUpdate.Count > 0)
                {
                    foreach (var updateItem in detailsUpdate)
                    {
                        updateItem.FTienUocThucHien06ThangCuoiNam = !updateItem.BHangCha ? updateItem.FTienUocThucHien06ThangCuoiNam ?? 0 : 0;
                        updateItem.FTienThucHien06ThangDauNam = !updateItem.BHangCha ? updateItem.FTienThucHien06ThangDauNam ?? 0 : 0;
                        updateItem.FTienUocThucHienCaNam = !updateItem.IsHangCha ? (updateItem.FTienThucHien06ThangDauNam ?? 0) + (updateItem.FTienUocThucHien06ThangCuoiNam ?? 0) : 0;
                        var khtBHXHChiTiet = _bhDtcDcdToanChiChiTietService.FindById(updateItem.Id);
                        _mapper.Map(updateItem, khtBHXHChiTiet);

                        _bhDtcDcdToanChiChiTietService.Update(khtBHXHChiTiet);
                        updateItem.IsModified = false;
                    }
                }

                Guid ID = Model.Id.IsNullOrEmpty() ? Model.IID_BH_DTC : Model.Id;
                //cập nhật tổng cộng chứng từ
                var dtDieuchinhChungTu = _bhDcdToanChiService.FindById(ID);
                dtDieuchinhChungTu.FTienDuToanDuocGiao = Model.FTienDuToanDuocGiao;
                dtDieuchinhChungTu.FTienThucHien06ThangDauNam = Model.FTienThucHien06ThangDauNam;
                dtDieuchinhChungTu.FTienUocThucHien06ThangCuoiNam = Model.FTienUocThucHien06ThangCuoiNam;
                dtDieuchinhChungTu.FTienUocThucHienCaNam = Model.FTienThucHien06ThangDauNam + Model.FTienUocThucHien06ThangCuoiNam;
                dtDieuchinhChungTu.FTienSoSanhTang = (dtDieuchinhChungTu.FTienUocThucHienCaNam - dtDieuchinhChungTu.FTienDuToanDuocGiao) > 0 ? dtDieuchinhChungTu.FTienUocThucHienCaNam - dtDieuchinhChungTu.FTienDuToanDuocGiao : 0;
                dtDieuchinhChungTu.FTienSoSanhGiam = (dtDieuchinhChungTu.FTienDuToanDuocGiao - dtDieuchinhChungTu.FTienUocThucHienCaNam) > 0 ? dtDieuchinhChungTu.FTienDuToanDuocGiao - dtDieuchinhChungTu.FTienUocThucHienCaNam : 0;


                _bhDcdToanChiService.Update(dtDieuchinhChungTu);
                LoadData();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
                var message = Resources.MsgSaveDone;
                MessageBoxHelper.Info(message);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }
        #endregion

        #region On print detail
        private void OnPrintDetal(object obj)
        {
            var dtDcDtCheckPrintType = (DtDcDtCheckPrintType)((int)obj);
            object content;
            switch (dtDcDtCheckPrintType)
            {
                case DtDcDtCheckPrintType.DTDCTheoDonVi:
                    PrintReportDieuChinhDuToanViewModel.dtDcDtCheckPrintType = dtDcDtCheckPrintType;
                    PrintReportDieuChinhDuToanViewModel.IsShowInTheoTongHop = true;
                    //PrintReportDieuChinhDuToanViewModel. = false;
                    PrintReportDieuChinhDuToanViewModel.Name = "In điều chỉnh dự toán";
                    PrintReportDieuChinhDuToanViewModel.Description = "In điều chỉnh dự toán";
                    PrintReportDieuChinhDuToanViewModel.Init();

                    content = new PrintReportDieuChinhDuToanChiTiet
                    {
                        DataContext = PrintReportDieuChinhDuToanViewModel
                    };

                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, "DivisionEstimateDetailDialog", null, null);
            }
        }
        #endregion

        #region close
        public override void OnClose(object obj)
        {
            ((Window)obj).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }
        #endregion

        #region Search

        private void OnSearch()
        {
            SearchTextFilter();
        }

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                _ItemViews.Refresh();
            }
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhDtcDcdToanChiChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IID_MucLucNganSach.Equals(item.IID_MucLucNganSach));
            }

            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (BhDtcDcdToanChiChiTietModel)obj;
            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhDtcDcdToanChiChiTietModel> results = new List<BhDtcDcdToanChiChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = DataPopupSearchItems.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = DataPopupSearchItems.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = DataPopupSearchItems.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhDtcDcdToanChiChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhDtcDcdToanChiChiTietModel>();
            }
            _ItemViews.Refresh();
        }

        private List<BhDtcDcdToanChiChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhDtcDcdToanChiChiTietModel> result = new List<BhDtcDcdToanChiChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = DataPopupSearchItems.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhDtcDcdToanChiChiTietModel> lstInput, List<BhDtcDcdToanChiChiTietModel> results)
        {
            var itemChild = DataPopupSearchItems.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => DataPopupSearchItems.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach)))
                {
                    GetListChild(new List<BhDtcDcdToanChiChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
