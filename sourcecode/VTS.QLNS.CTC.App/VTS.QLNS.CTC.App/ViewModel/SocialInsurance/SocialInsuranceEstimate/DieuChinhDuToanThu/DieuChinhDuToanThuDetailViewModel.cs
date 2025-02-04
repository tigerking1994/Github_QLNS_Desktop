using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu;
using System.Windows;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.PrintReport;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.PrintReport;
using System.Windows.Data;
using ControlzEx.Standard;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu
{
    public class DieuChinhDuToanThuDetailViewModel : DetailViewModelBase<BhDcDuToanThuModel, BhDcDuToanThuChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private readonly IBhDcDuToanThuChiTietService _bhDcDuToanThuChiTietService;
        private readonly IBhDcDuToanThuService _bhDcDuToanThuService;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView ItemsView;

        private ICollection<BhDcDuToanThuChiTietModel> _filterResult = new HashSet<BhDcDuToanThuChiTietModel>();
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        private string xnmConcatenation = "";
        public bool IsOpenPrintPopup = true;
        private bool _isLock;
        public bool IsVoucherSummary { get; set; }
        public bool IsLock
        {
            get => _isLock;
            set
            {
                SetProperty(ref _isLock, value);
                OnPropertyChanged(nameof(IsEnabledDelete));
            }
        }

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
            set
            {
                if (SetProperty(ref _sNoiDungSearch, value))
                {
                    //SearchTextFilter();
                    //ItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<BhDcDuToanThuChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhDcDuToanThuChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhDcDuToanThuChiTietModel _selectedPopupItem;
        public BhDcDuToanThuChiTietModel SelectedPopupItem
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

        private ObservableCollection<ComboboxItem> _typeShowAgencyBHXH;
        public ObservableCollection<ComboboxItem> TypeShowAgencyBHXH
        {
            get => _typeShowAgencyBHXH;
            set => SetProperty(ref _typeShowAgencyBHXH, value);
        }

        private ComboboxItem _selectedTypeShowAgencyBHXH;
        public ComboboxItem SelectedTypeShowAgencyBHXH
        {
            get => _selectedTypeShowAgencyBHXH;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgencyBHXH, value))
                {
                    if (_selectedTypeShowAgencyBHXH != null && _selectedTypeShowAgencyBHXH.ValueItem == TypeDisplay.CHITIET_DONVI)
                        _isShowColumnUnit = true;
                    else _isShowColumnUnit = false;
                    LoadData();
                    OnPropertyChanged(nameof(IsShowColumnUnit));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
                BeForeRefresh();
                ItemsView.Refresh();
                CalculateData();
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ObservableCollection<BhDcDuToanThuChiTietModel> _dataSearch;
        public ObservableCollection<BhDcDuToanThuChiTietModel> DataSearch
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

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                        || Items.Any(x => !x.IsHangCha);

        public bool IsShowAgencyFilter => IsVoucherSummary && _selectedTypeShowAgencyBHXH != null && _selectedTypeShowAgencyBHXH.ValueItem == TypeDisplay.CHITIET_DONVI;
        public bool IsVisibilityGetSettlement => !IsVoucherSummary;
        public bool IsIsEnableSettlement => !Model.BIsKhoa;
        public bool IsInit { get; set; }

        public bool IsChungTuTongHop => Model != null;
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(DieuChinhDuToanThuDetail);
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand GetSettlementDataCommand { get; }
        public PrintReportDieuChinhDuToanThuViewModel PrintReportDieuChinhDuToanThuViewModel { get; }
        public DieuChinhDuToanThuDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IBhDcDuToanThuChiTietService bhDtcDcdToanChiChiTietService,
            IBhDcDuToanThuService bhDcdToanChiService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            PrintReportDieuChinhDuToanThuViewModel printReportDieuChinhDuToanThuViewModel,
            INsDonViService nsDonViService,
            ILog loger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = loger;
            _bhDcDuToanThuChiTietService = bhDtcDcdToanChiChiTietService;
            _bhDcDuToanThuService = bhDcdToanChiService;
            PrintReportDieuChinhDuToanThuViewModel = printReportDieuChinhDuToanThuViewModel;
            _nsDonViService = nsDonViService;

            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnCloseWindow);
            PrintCommand = new RelayCommand(OnPrintDetal);
            GetSettlementDataCommand = new RelayCommand(obj => OnGetSettlementDataCommand(obj));
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }


        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _selectedTypeShowAgencyBHXH = null;
            _isShowColumnUnit = false;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                //IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            IsInit = true;

            if (!string.IsNullOrEmpty(Model.STongHop))
            {
                LoadComboboxTypeShow();
            }

            LoadData();
            IsInit = false;
            OnClearSearch(false);
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgencyBHXH = new ObservableCollection<ComboboxItem>();
            TypeShowAgencyBHXH.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgencyBHXH.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgencyBHXH = TypeShowAgencyBHXH.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgencyBHXH));
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        public override void LoadData(params object[] args)
        {
            BhDcDuToanThuChiTietCriteria searchCondition = new BhDcDuToanThuChiTietCriteria();
            searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
            searchCondition.LNS = Model.SLNS;
            searchCondition.ILoaiTongHop = Model.ILoaiTongHop;
            searchCondition.NgayChungTu = Model.DNgayChungTu;
            searchCondition.BDaTongHop = Model.BDaTongHop;
            var temp = new List<BhDcDuToanThuChiTiet>();

            if (IsVoucherSummary && SelectedTypeShowAgencyBHXH != null && SelectedTypeShowAgencyBHXH.ValueItem == TypeDisplay.CHITIET_DONVI)
            {
                var voucherNos = Model.STongHop.Split(",").ToList();
                List<BhDcDuToanThu> listChungTu = _bhDcDuToanThuService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork).ToList();
                List<BhDcDuToanThuChiTiet> listChungTuChiTietParent = new List<BhDcDuToanThuChiTiet>();
                List<BhDcDuToanThuChiTiet> listChungTuChiTietChildren = new List<BhDcDuToanThuChiTiet>();
                foreach (var chungTu in listChungTu)
                {
                    searchCondition.BhDcDuToanThuId = chungTu.Id;
                    searchCondition.IdDonVi = chungTu.IIDMaDonVi;
                    searchCondition.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTu;
                    var listQuery = _bhDcDuToanThuChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                }

                var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
                temp.AddRange(listChungTuChiTietParent);
                temp.AddRange(listChungTuChiTietChildren);
                temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIdMaDonVi).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IIDMaDonVi));
                LoadAgencies(agencyIds);
            }
            else
            {
                searchCondition.IdDonVi = Model.IIDMaDonVi;
                searchCondition.BhDcDuToanThuId = Model.Id.IsNullOrEmpty() ? Model.IIDDttDieuChinh : Model.Id;
                temp = _bhDcDuToanThuChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
            }

            var existBhChiTiet = _bhDcDuToanThuChiTietService.ExistKhcKcbChiTiet(searchCondition.BhDcDuToanThuId);

            foreach (var item in temp)
            {
                item.IsAuToFillTuChi = !existBhChiTiet;
            }

            Items = _mapper.Map<ObservableCollection<BhDcDuToanThuChiTietModel>>(temp);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhDcDuToanThuChiTietModel>>(temp);
            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter = ItemsViewFilter;
            Parallel.ForEach(Items, khcKcbChiTietModel =>
            {
                khcKcbChiTietModel.IsFilter = true;
                if (!khcKcbChiTietModel.IsHangCha)
                {
                    khcKcbChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhDcDuToanThuChiTietModel item = (BhDcDuToanThuChiTietModel)sender;
                        if (args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHXHNLDQTDauNam) || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHXHNSDQTDauNam)
                            || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHYTNLDQTDauNam) || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHYTNSDQTDauNam)
                            || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHTNNLDQTDauNam) || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHTNNSDQTDauNam)
                            || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHXHNLDQTCuoiNam) || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHXHNSDQTCuoiNam)
                            || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHYTNLDQTCuoiNam) || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHYTNSDQTCuoiNam)
                            || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHTNNLDQTCuoiNam) || args.PropertyName == nameof(BhDcDuToanThuChiTietModel.FThuBHTNNSDQTCuoiNam)
                            || args.PropertyName.Equals(nameof(BhDcDuToanThuChiTietModel.SGhiChu)))
                        {

                            if (!IsInit)
                            {
                                item.IsModified = true;
                                khcKcbChiTietModel.IsModified = true;
                                CalculateData();
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }
            });
            CalculateData();
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
               .ForAll(x =>
               {
                   x.FThuBHXHNLD = 0;
                   x.FThuBHXHNSD = 0;
                   x.FThuBHYTNLD = 0;
                   x.FThuBHYTNSD = 0;
                   x.FThuBHTNNLD = 0;
                   x.FThuBHTNNSD = 0;
                   x.FThuBHXHNLDQTDauNam = 0;
                   x.FThuBHXHNSDQTDauNam = 0;
                   x.FThuBHYTNLDQTDauNam = 0;
                   x.FThuBHYTNSDQTDauNam = 0;
                   x.FThuBHTNNLDQTDauNam = 0;
                   x.FThuBHTNNSDQTDauNam = 0;
                   x.FThuBHXHNLDQTCuoiNam = 0;
                   x.FThuBHXHNSDQTCuoiNam = 0;
                   x.FThuBHYTNLDQTCuoiNam = 0;
                   x.FThuBHYTNSDQTCuoiNam = 0;
                   x.FThuBHTNNLDQTCuoiNam = 0;
                   x.FThuBHTNNSDQTCuoiNam = 0;
                   x.FThuBHXHNLDTang = 0;
                   x.FThuBHXHNSDTang = 0;
                   x.FThuBHXHTang = 0;
                   x.FThuBHYTNLDTang = 0;
                   x.FThuBHYTNSDTang = 0;
                   x.FThuBHYTTang = 0;
                   x.FThuBHTNNLDTang = 0;
                   x.FThuBHTNNSDTang = 0;
                   x.FThuBHXHNLDGiam = 0;
                   x.FThuBHXHNSDGiam = 0;
                   x.FThuBHYTNLDGiam = 0;
                   x.FThuBHYTNSDGiam = 0;
                   x.FThuBHTNNLDGiam = 0;
                   x.FThuBHTNNSDGiam = 0;
                   x.FThuBHTNTang = 0;
                   x.FThuBHTNGiam = 0;
               });

            Items.Where(x => !x.IsHangCha)
              .ForAll(x =>
              {
                  var fBhxhNLD = x.FThuBHXHNLDQTDauNam.GetValueOrDefault() + x.FThuBHXHNLDQTCuoiNam.GetValueOrDefault() - x.FThuBHXHNLD.GetValueOrDefault();
                  var fBhxhNSD = x.FThuBHXHNSDQTDauNam.GetValueOrDefault() + x.FThuBHXHNSDQTCuoiNam.GetValueOrDefault() - x.FThuBHXHNSD.GetValueOrDefault();
                  var fBhytNLD = x.FThuBHYTNLDQTDauNam.GetValueOrDefault() + x.FThuBHYTNLDQTCuoiNam.GetValueOrDefault() - x.FThuBHYTNLD.GetValueOrDefault();
                  var fBhytNSD = x.FThuBHYTNSDQTDauNam.GetValueOrDefault() + x.FThuBHYTNSDQTCuoiNam.GetValueOrDefault() - x.FThuBHYTNSD.GetValueOrDefault();
                  var fBhtnNLD = x.FThuBHTNNLDQTDauNam.GetValueOrDefault() + x.FThuBHTNNLDQTCuoiNam.GetValueOrDefault() - x.FThuBHTNNLD.GetValueOrDefault();
                  var fBhtnNSD = x.FThuBHTNNSDQTDauNam.GetValueOrDefault() + x.FThuBHTNNSDQTCuoiNam.GetValueOrDefault() - x.FThuBHTNNSD.GetValueOrDefault();

                  x.FThuBHXHNLDTang = fBhxhNLD > 0 ? fBhxhNLD : 0;
                  x.FThuBHXHNSDTang = fBhxhNSD > 0 ? fBhxhNSD : 0;
                  x.FThuBHYTNLDTang = fBhytNLD > 0 ? fBhytNLD : 0;
                  x.FThuBHYTNSDTang = fBhytNSD > 0 ? fBhytNSD : 0;
                  x.FThuBHTNNLDTang = fBhtnNLD > 0 ? fBhtnNLD : 0;
                  x.FThuBHTNNSDTang = fBhtnNSD > 0 ? fBhtnNSD : 0;

                  x.FThuBHXHNLDGiam = fBhxhNLD < 0 ? fBhxhNLD : 0;
                  x.FThuBHXHNSDGiam = fBhxhNSD < 0 ? fBhxhNSD : 0;
                  x.FThuBHYTNLDGiam = fBhytNLD < 0 ? fBhytNLD : 0;
                  x.FThuBHYTNSDGiam = fBhytNSD < 0 ? fBhytNSD : 0;
                  x.FThuBHTNNLDGiam = fBhtnNLD < 0 ? fBhtnNLD : 0;
                  x.FThuBHTNNSDGiam = fBhtnNSD < 0 ? fBhtnNSD : 0;
              });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            Model.FThuBHXHNLD = 0;
            Model.FThuBHXHNSD = 0;
            Model.FThuBHYTNLD = 0;
            Model.FThuBHYTNSD = 0;
            Model.FThuBHTNNLD = 0;
            Model.FThuBHTNNSD = 0;
            Model.FThuBHXHNLDQTDauNam = 0;
            Model.FThuBHXHNSDQTDauNam = 0;
            Model.FThuBHYTNLDQTDauNam = 0;
            Model.FThuBHYTNSDQTDauNam = 0;
            Model.FThuBHTNNLDQTDauNam = 0;
            Model.FThuBHTNNSDQTDauNam = 0;
            Model.FThuBHXHNLDQTCuoiNam = 0;
            Model.FThuBHXHNSDQTCuoiNam = 0;
            Model.FThuBHYTNLDQTCuoiNam = 0;
            Model.FThuBHYTNSDQTCuoiNam = 0;
            Model.FThuBHTNNLDQTCuoiNam = 0;
            Model.FThuBHTNNSDQTCuoiNam = 0;
            Model.FTongThuBHXHNLD = 0;
            Model.FTongThuBHXHNSD = 0;
            Model.FTongThuBHYTNLD = 0;
            Model.FTongThuBHYTNSD = 0;
            Model.FTongThuBHTNNLD = 0;
            Model.FTongThuBHTNNSD = 0;
            Model.FTongCong = 0;
            Model.FThuBHXHNLDTang = 0;
            Model.FThuBHXHNSDTang = 0;
            Model.FThuBHXHNLDGiam = 0;
            Model.FThuBHXHNSDGiam = 0;
            Model.FThuBHXHTang = 0;
            Model.FThuBHYTNLDTang = 0;
            Model.FThuBHYTNSDTang = 0;
            Model.FThuBHYTNLDGiam = 0;
            Model.FThuBHYTNSDGiam = 0;
            Model.FThuBHYTTang = 0;
            Model.FThuBHTNNLDTang = 0;
            Model.FThuBHTNNSDTang = 0;
            Model.FThuBHTNNLDGiam = 0;
            Model.FThuBHTNNSDGiam = 0;
            Model.FThuBHTNTang = 0;
            Model.FThuBHTNGiam = 0;
            Model.FThuBHYTGiam = 0;
            Model.FThuBHXHGiam = 0;

            var roots = Items.Where(t => !t.IsHangCha).ToList();

            foreach (var item in roots)
            {
                Model.FThuBHXHNLD += item.FThuBHXHNLD.GetValueOrDefault();
                Model.FThuBHXHNSD += item.FThuBHXHNSD.GetValueOrDefault();
                Model.FThuBHYTNLD += item.FThuBHYTNLD.GetValueOrDefault();
                Model.FThuBHYTNSD += item.FThuBHYTNSD.GetValueOrDefault();
                Model.FThuBHTNNLD += item.FThuBHTNNLD.GetValueOrDefault();
                Model.FThuBHTNNSD += item.FThuBHTNNSD.GetValueOrDefault();
                Model.FTongThuBHXHNLD += item.FTongThuBHXHNLD.GetValueOrDefault();
                Model.FTongThuBHXHNSD += item.FTongThuBHXHNSD.GetValueOrDefault();
                Model.FTongThuBHYTNLD += item.FTongThuBHYTNLD.GetValueOrDefault();
                Model.FTongThuBHYTNSD += item.FTongThuBHYTNSD.GetValueOrDefault();
                Model.FTongThuBHTNNLD += item.FTongThuBHTNNLD.GetValueOrDefault();
                Model.FTongThuBHTNNSD += item.FTongThuBHTNNSD.GetValueOrDefault();

                Model.FThuBHXHNLDQTDauNam += item.FThuBHXHNLDQTDauNam.GetValueOrDefault();
                Model.FThuBHXHNSDQTDauNam += item.FThuBHXHNSDQTDauNam.GetValueOrDefault();
                Model.FThuBHYTNLDQTDauNam += item.FThuBHYTNLDQTDauNam.GetValueOrDefault();
                Model.FThuBHYTNSDQTDauNam += item.FThuBHYTNSDQTDauNam.GetValueOrDefault();
                Model.FThuBHTNNLDQTDauNam += item.FThuBHTNNLDQTDauNam.GetValueOrDefault();
                Model.FThuBHTNNSDQTDauNam += item.FThuBHTNNSDQTDauNam.GetValueOrDefault();
                Model.FThuBHXHNLDQTCuoiNam += item.FThuBHXHNLDQTCuoiNam.GetValueOrDefault();
                Model.FThuBHXHNSDQTCuoiNam += item.FThuBHXHNSDQTCuoiNam.GetValueOrDefault();
                Model.FThuBHYTNLDQTCuoiNam += item.FThuBHYTNLDQTCuoiNam.GetValueOrDefault();
                Model.FThuBHYTNSDQTCuoiNam += item.FThuBHYTNSDQTCuoiNam.GetValueOrDefault();
                Model.FThuBHTNNLDQTCuoiNam += item.FThuBHTNNLDQTCuoiNam.GetValueOrDefault();
                Model.FThuBHTNNSDQTCuoiNam += item.FThuBHTNNSDQTCuoiNam.GetValueOrDefault();

                Model.FThuBHXHNLDTang += item.FThuBHXHNLDTang.GetValueOrDefault();
                Model.FThuBHXHNSDTang += item.FThuBHXHNSDTang.GetValueOrDefault();
                Model.FThuBHXHTang += item.FThuBHXHTang.GetValueOrDefault();
                Model.FThuBHYTNLDTang += item.FThuBHYTNLDTang.GetValueOrDefault();
                Model.FThuBHYTNSDTang += item.FThuBHYTNSDTang.GetValueOrDefault();
                Model.FThuBHYTTang += item.FThuBHYTTang.GetValueOrDefault();
                Model.FThuBHTNNLDTang += item.FThuBHTNNLDTang.GetValueOrDefault();
                Model.FThuBHTNNSDTang += item.FThuBHTNNSDTang.GetValueOrDefault();
                Model.FThuBHTNTang += item.FThuBHTNTang.GetValueOrDefault();
                Model.FThuBHXHNLDGiam += item.FThuBHXHNLDGiam.GetValueOrDefault();
                Model.FThuBHXHNSDGiam += item.FThuBHXHNSDGiam.GetValueOrDefault();
                Model.FThuBHXHGiam += item.FThuBHXHGiam.GetValueOrDefault();
                Model.FThuBHYTNLDGiam += item.FThuBHYTNLDGiam.GetValueOrDefault();
                Model.FThuBHYTNSDGiam += item.FThuBHYTNSDGiam.GetValueOrDefault();
                Model.FThuBHYTGiam += item.FThuBHYTGiam.GetValueOrDefault();
                Model.FThuBHTNNLDGiam += item.FThuBHTNNLDGiam.GetValueOrDefault();
                Model.FThuBHTNNSDGiam += item.FThuBHTNNSDGiam.GetValueOrDefault();
                Model.FThuBHTNGiam += item.FThuBHTNGiam.GetValueOrDefault();
                Model.FTongCong += item.FTongCong.GetValueOrDefault();
            }
        }

        private void CalculateParent(Guid idParent, BhDcDuToanThuChiTietModel item, Dictionary<Guid, BhDcDuToanThuChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FThuBHXHNLD += item.FThuBHXHNLD.GetValueOrDefault();
            model.FThuBHXHNSD += item.FThuBHXHNSD.GetValueOrDefault();
            model.FThuBHYTNLD += item.FThuBHYTNLD.GetValueOrDefault();
            model.FThuBHYTNSD += item.FThuBHYTNSD.GetValueOrDefault();
            model.FThuBHTNNLD += item.FThuBHTNNLD.GetValueOrDefault();
            model.FThuBHTNNSD += item.FThuBHTNNSD.GetValueOrDefault();

            model.FThuBHXHNLDQTDauNam += item.FThuBHXHNLDQTDauNam.GetValueOrDefault();
            model.FThuBHXHNSDQTDauNam += item.FThuBHXHNSDQTDauNam.GetValueOrDefault();
            model.FThuBHYTNLDQTDauNam += item.FThuBHYTNLDQTDauNam.GetValueOrDefault();
            model.FThuBHYTNSDQTDauNam += item.FThuBHYTNSDQTDauNam.GetValueOrDefault();
            model.FThuBHTNNLDQTDauNam += item.FThuBHTNNLDQTDauNam.GetValueOrDefault();
            model.FThuBHTNNSDQTDauNam += item.FThuBHTNNSDQTDauNam.GetValueOrDefault();
            model.FThuBHXHNLDQTCuoiNam += item.FThuBHXHNLDQTCuoiNam.GetValueOrDefault();
            model.FThuBHXHNSDQTCuoiNam += item.FThuBHXHNSDQTCuoiNam.GetValueOrDefault();
            model.FThuBHYTNLDQTCuoiNam += item.FThuBHYTNLDQTCuoiNam.GetValueOrDefault();
            model.FThuBHYTNSDQTCuoiNam += item.FThuBHYTNSDQTCuoiNam.GetValueOrDefault();
            model.FThuBHTNNLDQTCuoiNam += item.FThuBHTNNLDQTCuoiNam.GetValueOrDefault();
            model.FThuBHTNNSDQTCuoiNam += item.FThuBHTNNSDQTCuoiNam.GetValueOrDefault();

            model.FThuBHXHNLDTang += item.FThuBHXHNLDTang.GetValueOrDefault();
            model.FThuBHXHNSDTang += item.FThuBHXHNSDTang.GetValueOrDefault();
            model.FThuBHXHTang += item.FThuBHXHTang.GetValueOrDefault();
            model.FThuBHYTNLDTang += item.FThuBHYTNLDTang.GetValueOrDefault();
            model.FThuBHYTNSDTang += item.FThuBHYTNSDTang.GetValueOrDefault();
            model.FThuBHYTTang += item.FThuBHYTTang.GetValueOrDefault();
            model.FThuBHTNNLDTang += item.FThuBHTNNLDTang.GetValueOrDefault();
            model.FThuBHTNNSDTang += item.FThuBHTNNSDTang.GetValueOrDefault();
            model.FThuBHTNTang += item.FThuBHTNTang.GetValueOrDefault();
            model.FThuBHXHNLDGiam += item.FThuBHXHNLDGiam.GetValueOrDefault();
            model.FThuBHXHNSDGiam += item.FThuBHXHNSDGiam.GetValueOrDefault();
            model.FThuBHXHGiam += item.FThuBHXHGiam.GetValueOrDefault();
            model.FThuBHYTNLDGiam += item.FThuBHYTNLDGiam.GetValueOrDefault();
            model.FThuBHYTNSDGiam += item.FThuBHYTNSDGiam.GetValueOrDefault();
            model.FThuBHYTGiam += item.FThuBHYTGiam.GetValueOrDefault();
            model.FThuBHTNNLDGiam += item.FThuBHTNNLDGiam.GetValueOrDefault();
            model.FThuBHTNNSDGiam += item.FThuBHTNNSDGiam.GetValueOrDefault();
            model.FThuBHTNGiam += item.FThuBHTNGiam.GetValueOrDefault();

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        public override void OnSave()
        {
            try
            {
                if (!IsSaveData)
                {
                    return;
                }
                Func<BhDcDuToanThuChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
                Func<BhDcDuToanThuChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
                Func<BhDcDuToanThuChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                var detailsUpdate = Items.Where(isUpdate).ToList();
                var detailsDelete = Items.Where(isDelete).ToList();

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhDcDuToanThuChiTiet>();
                    _mapper.Map(detailsAdd, addItems);
                    _bhDcDuToanThuChiTietService.AddRange(addItems);

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
                        var khtBHXHChiTiet = _bhDcDuToanThuChiTietService.FindById(updateItem.Id);
                        _mapper.Map(updateItem, khtBHXHChiTiet);
                        _bhDcDuToanThuChiTietService.Update(khtBHXHChiTiet);
                        updateItem.IsModified = false;
                    }
                }

                Guid ID = Model.Id.IsNullOrEmpty() ? Model.IIDDttDieuChinh : Model.Id;
                //cập nhật tổng cộng chứng từ
                var dtDieuchinhChungTu = _bhDcDuToanThuService.FindById(ID);

                dtDieuchinhChungTu.FThuBHXHNLD = Model.FThuBHXHNLD;
                dtDieuchinhChungTu.FThuBHXHNSD = Model.FThuBHXHNSD;
                dtDieuchinhChungTu.FThuBHYTNLD = Model.FThuBHYTNLD;
                dtDieuchinhChungTu.FThuBHYTNSD = Model.FThuBHYTNSD;
                dtDieuchinhChungTu.FThuBHTNNLD = Model.FThuBHTNNLD;
                dtDieuchinhChungTu.FThuBHTNNSD = Model.FThuBHTNNSD;

                dtDieuchinhChungTu.FThuBHXHNLDQTDauNam = Model.FThuBHXHNLDQTDauNam;
                dtDieuchinhChungTu.FThuBHXHNSDQTDauNam = Model.FThuBHXHNSDQTDauNam;
                dtDieuchinhChungTu.FThuBHYTNLDQTDauNam = Model.FThuBHYTNLDQTDauNam;
                dtDieuchinhChungTu.FThuBHYTNSDQTDauNam = Model.FThuBHYTNSDQTDauNam;
                dtDieuchinhChungTu.FThuBHTNNLDQTDauNam = Model.FThuBHTNNLDQTDauNam;
                dtDieuchinhChungTu.FThuBHTNNSDQTDauNam = Model.FThuBHTNNSDQTDauNam;
                dtDieuchinhChungTu.FThuBHXHNLDQTCuoiNam = Model.FThuBHXHNLDQTCuoiNam;
                dtDieuchinhChungTu.FThuBHXHNSDQTCuoiNam = Model.FThuBHXHNSDQTCuoiNam;
                dtDieuchinhChungTu.FThuBHYTNLDQTCuoiNam = Model.FThuBHYTNLDQTCuoiNam;
                dtDieuchinhChungTu.FThuBHYTNSDQTCuoiNam = Model.FThuBHYTNSDQTCuoiNam;
                dtDieuchinhChungTu.FThuBHTNNLDQTCuoiNam = Model.FThuBHTNNLDQTCuoiNam;
                dtDieuchinhChungTu.FThuBHTNNSDQTCuoiNam = Model.FThuBHTNNSDQTCuoiNam;

                dtDieuchinhChungTu.FThuBHXHNLDTang = Model.FThuBHXHNLDTang;
                dtDieuchinhChungTu.FThuBHXHNSDTang = Model.FThuBHXHNSDTang;
                dtDieuchinhChungTu.FThuBHXHTang = Model.FThuBHXHTang;
                dtDieuchinhChungTu.FThuBHYTNLDTang = Model.FThuBHYTNLDTang;
                dtDieuchinhChungTu.FThuBHYTNSDTang = Model.FThuBHYTNSDTang;
                dtDieuchinhChungTu.FThuBHYTTang = Model.FThuBHYTTang;
                dtDieuchinhChungTu.FThuBHTNNLDTang = Model.FThuBHTNNLDTang;
                dtDieuchinhChungTu.FThuBHTNNSDTang = Model.FThuBHTNNSDTang;
                dtDieuchinhChungTu.FThuBHTNTang = Model.FThuBHTNTang;
                dtDieuchinhChungTu.FThuBHXHNLDGiam = Model.FThuBHXHNLDGiam;
                dtDieuchinhChungTu.FThuBHXHNSDGiam = Model.FThuBHXHNSDGiam;
                dtDieuchinhChungTu.FThuBHXHGiam = Model.FThuBHXHGiam;
                dtDieuchinhChungTu.FThuBHYTNLDGiam = Model.FThuBHYTNLDGiam;
                dtDieuchinhChungTu.FThuBHYTNSDGiam = Model.FThuBHYTNSDGiam;
                dtDieuchinhChungTu.FThuBHYTGiam = Model.FThuBHYTGiam;
                dtDieuchinhChungTu.FThuBHTNNLDGiam = Model.FThuBHTNNLDGiam;
                dtDieuchinhChungTu.FThuBHTNNSDGiam = Model.FThuBHTNNSDGiam;
                dtDieuchinhChungTu.FThuBHTNGiam = Model.FThuBHTNGiam;
                dtDieuchinhChungTu.FTongCong = Model.FTongCong;

                _bhDcDuToanThuService.Update(dtDieuchinhChungTu);

                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
                OnRefresh();
                var message = Resources.MsgSaveDone;
                MessageBoxHelper.Info(message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }

        private void OnPrintDetal(object obj)
        {
            var bhxhCheckPrintType = (BHXHCheckPrintType)((int)obj);
            object content;
            PrintReportDieuChinhDuToanThuViewModel.BHXHCheckPrintType = bhxhCheckPrintType;
            if (Items != null && Items.Count > 0)
            {
                PrintReportDieuChinhDuToanThuViewModel.DonViChaChungTu = GetParentUnit();
                PrintReportDieuChinhDuToanThuViewModel.DonViChungTu = Model.STenDonVi;
                PrintReportDieuChinhDuToanThuViewModel.IsShowVoucherType = true;
                PrintReportDieuChinhDuToanThuViewModel.DcDttId = Model.IIDDttDieuChinh == Guid.Empty ? Model.Id : Model.IIDDttDieuChinh;
                PrintReportDieuChinhDuToanThuViewModel.IsEnabledUnit = true;
                PrintReportDieuChinhDuToanThuViewModel.IsPrintByUnit = true;
            }
            PrintReportDieuChinhDuToanThuViewModel.Init();
            content = new PrintDieuChinhDuToanThu
            {
                DataContext = PrintReportDieuChinhDuToanThuViewModel
            };
            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.DETAIL_DIALOG, null, null);
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private string GetParentUnit()
        {
            string sParent = NSConstants.BO_QUOC_PHONG;
            DonVi donvi = _nsDonViService.FindByIdDonVi(Model.IIDMaDonVi, _sessionService.Current.YearOfWork);
            if (!"0".Equals(donvi?.Loai))
            {
                DonVi donViCapTren = _nsDonViService.FindByLoai("0", _sessionService.Current.YearOfWork);
                sParent = donViCapTren?.TenDonVi;
            }
            return sParent;
        }

        #region search

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhDcDuToanThuChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IIDMLNS.Equals(item.IIDMLNS));
            }

            item.IsFilter = result;
            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (BhDcDuToanThuChiTietModel)obj;
            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private void OnSearch()
        {
            SearchTextFilter();
        }

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                ItemsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhDcDuToanThuChiTietModel> results = new List<BhDcDuToanThuChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = DataPopupSearchItems.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = DataPopupSearchItems.Where(x => x.SMoTa.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
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
                DataSearch = new ObservableCollection<BhDcDuToanThuChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhDcDuToanThuChiTietModel>();
            }
            ItemsView.Refresh();
        }

        private List<BhDcDuToanThuChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhDcDuToanThuChiTietModel> result = new List<BhDcDuToanThuChiTietModel>();
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

        private void GetListChild(List<BhDcDuToanThuChiTietModel> lstInput, List<BhDcDuToanThuChiTietModel> results)
        {
            var itemChild = DataPopupSearchItems.Where(x => lstInput.Select(x => x.IIDMLNS).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => DataPopupSearchItems.Select(y => y.IdParent).Distinct().Contains(x.IIDMLNS)))
                {
                    GetListChild(new List<BhDcDuToanThuChiTietModel>() { item }, results);
                }
            }
        }
        #endregion

        private void OnGetSettlementDataCommand(object obj)
        {
            var param = (int)obj;
            int namLamViec = _sessionService.Current.YearOfWork;
            string maDonVi = Model.IIDMaDonVi;
            int thangQuy = param == 13 ? 3 : param == 14 ? 6 : param == 15 ? 9 : param == 16 ? 12 : param;
            int loaiThangQuy = (param == 13 || param == 14 || param == 15 || param == 16) ? 1 : 0;
            var settlementData = _bhDcDuToanThuChiTietService.GetSettlementData(namLamViec, maDonVi, thangQuy, loaiThangQuy).ToList();

            if (settlementData.Any())
            {
                var itemFilter = Items.Where(x => !x.IsHangCha);
                Parallel.ForEach(itemFilter, item =>
                {
                    var itemMap = settlementData.Where(x => x.SXauNoiMa == item.SXauNoiMa && x.IIdMaDonVi == item.IIdMaDonVi);
                    item.FThuBHXHNLDQTDauNam = itemMap.Any() ? itemMap.Select(x => x.FThuBHXHNLDQTDauNam.GetValueOrDefault()).FirstOrDefault() : 0;
                    item.FThuBHXHNSDQTDauNam = itemMap.Any() ? itemMap.Select(x => x.FThuBHXHNSDQTDauNam.GetValueOrDefault()).FirstOrDefault() : 0;
                    item.FThuBHYTNLDQTDauNam = itemMap.Any() ? itemMap.Select(x => x.FThuBHYTNLDQTDauNam.GetValueOrDefault()).FirstOrDefault() : 0;
                    item.FThuBHYTNSDQTDauNam = itemMap.Any() ? itemMap.Select(x => x.FThuBHYTNSDQTDauNam.GetValueOrDefault()).FirstOrDefault() : 0;
                    item.FThuBHTNNLDQTDauNam = itemMap.Any() ? itemMap.Select(x => x.FThuBHTNNLDQTDauNam.GetValueOrDefault()).FirstOrDefault() : 0;
                    item.FThuBHTNNSDQTDauNam = itemMap.Any() ? itemMap.Select(x => x.FThuBHTNNSDQTDauNam.GetValueOrDefault()).FirstOrDefault() : 0;
                });
            }
            else
            {
                MessageBoxHelper.Info(string.Format(Resources.MsgSettlementData, BhDttThagQuy.Get(param)));
            }
        }
    }
}
