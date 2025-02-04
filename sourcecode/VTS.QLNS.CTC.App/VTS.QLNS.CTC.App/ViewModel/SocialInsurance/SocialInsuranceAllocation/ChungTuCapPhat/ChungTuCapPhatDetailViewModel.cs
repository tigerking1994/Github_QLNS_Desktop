using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat
{
    public class ChungTuCapPhatDetailViewModel : DetailViewModelBase<BhCpChungTuModel, BhCpChungTuChiTietModel>
    {
        #region Interface
        private readonly IBhCpChungTuChiTietService _bhCpChungTuChiTietService;
        private readonly IBhCpChungTuService _bhCpChungTuService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhDanhMucLoaiChiService _bhMucLoaiChiService;
        private ICollectionView _itemViews;
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsOpenPrintPopup = true;
        public bool IsAnotherUserCreate { get; set; }
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
        List<BhCpChungTuChiTietQuery> _listChungTuChiTiet;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                     || Items.Any(x => !x.IsHangCha);
        public bool IsInit { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public override Type ContentType => typeof(ChungTuCapPhatDetail);

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set
            {
                if (SetProperty(ref _sNoiDungSearch, value))
                {
                    SearchTextFilter();
                    _itemViews.Refresh();
                }
            }
        }

        private string _sM;
        public string SM
        {
            get => _sM;
            set
            {
                if (SetProperty(ref _sM, value))
                {
                    SearchTextFilter();
                    _itemViews.Refresh();
                }
            }
        }

        private string _sTM;
        public string STM
        {
            get => _sTM;
            set
            {
                if (SetProperty(ref _sTM, value))
                {
                    //SearchTextFilter();
                    //_itemViews.Refresh();
                }
            }
        }

        private ObservableCollection<BhCpChungTuChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhCpChungTuChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhCpChungTuChiTietModel _selectedPopupItem;
        public BhCpChungTuChiTietModel SelectedPopupItem
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

        private bool _isPopupOpen;
        private bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(SM) && string.IsNullOrEmpty(STM) && string.IsNullOrEmpty(SNoiDungSearch);
            }
        }
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<BhCpChungTuChiTietModel> _dataSearch;
        public ObservableCollection<BhCpChungTuChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }
        public Visibility ShowDuToanAndConLai => (Model.SLNS == LNSValue.LNS_9010003 || Model.SLNS == LNSValue.LNS_9010001_9010002 || Model.SLNS == LNSValue.LNS_9010006_9010007 || Model.SLNS == LNSValue.LNS_9010004_9010005 || Model.SLNS == LNSValue.LNS_901_9010001_9010002) ? Visibility.Visible : Visibility.Collapsed;
        #endregion

        #region RelayCommand
        public RelayCommand RefreshCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand SearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        #endregion

        #region Model
        public PrintChungTuCapPhatNoticeViewModel PrintChungTuCapPhatNoticeViewModel { get; set; }
        public PrintChungTuCapPhatDonViViewModel PrintChungTuCapPhatDonViViewModel { get; set; }
        public PrintChungTuCapPhatThongTriViewModel PrintChungTuCapPhatThongTriViewModel { get; set; }
        #endregion

        #region Constructor
        public ChungTuCapPhatDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog loger,
            IBhCpChungTuService bhCpChungTuService,
            IBhCpChungTuChiTietService bhCpChungTuChiTietService,
            INsDonViService nsDonViService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            PrintChungTuCapPhatNoticeViewModel printChungTuCapPhatNoticeViewModel,
            PrintChungTuCapPhatDonViViewModel printChungTuCapPhatDonViViewModel,
            PrintChungTuCapPhatThongTriViewModel printChungTuCapPhatThongTriViewModel)
        {
            _mapper = mapper;
            _logger = loger;
            _sessionService = sessionService;
            _bhCpChungTuChiTietService = bhCpChungTuChiTietService;
            _bhCpChungTuService = bhCpChungTuService;
            _nsDonViService = nsDonViService;
            _bhMucLoaiChiService = bhDanhMucLoaiChiService;

            SaveCommand = new RelayCommand(obj => OnSave());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintReportCommand = new RelayCommand(OnPrintDetal);
            RefreshCommand = new RelayCommand(obj => Init());
            _iNguoiDungDonViService = iNguoiDungDonViService;
            PrintChungTuCapPhatNoticeViewModel = printChungTuCapPhatNoticeViewModel;
            PrintChungTuCapPhatDonViViewModel = printChungTuCapPhatDonViViewModel;
            PrintChungTuCapPhatThongTriViewModel = printChungTuCapPhatThongTriViewModel;
            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }
        #endregion

        #region Init
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }

            IsInit = true;
            LoadData();
            //OnPropertyChanged(nameof(ShowSMandSTM));
            IsInit = false;
            OnClearSearch(false);
        }
        #endregion

        #region Load Data

        public override void LoadData(params object[] args)
        {
            BhCpChungTuChiChiTietCriteria searchCondition = new BhCpChungTuChiChiTietCriteria();
            searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
            searchCondition.SIdDonVi = Model.SID_MaDonVi;
            searchCondition.ILoaiDanhMucChi = Model.IID_LoaiCap;
            searchCondition.CpChungTuChiId = Model.Id;
            searchCondition.SLNS = Model.SLNS;
            searchCondition.NgayChungTu = Model.DNgayChungTu;
            searchCondition.ILoaiTongHop = Model.ILoaiTongHop;

            if (Model.SLNS == LNSValue.LNS_9010001_9010002 || Model.SLNS == LNSValue.LNS_901_9010001_9010002)
            {
                searchCondition.SLNS = LNSValue.LNS_901_9010001_9010002;
                _listChungTuChiTiet = _bhCpChungTuChiTietService.FindBhCpChungTuCheDoBHXHChiTiet(searchCondition).ToList();
            }
            else if (Model.SLNS == LNSValue.LNS_9050001_9050002)
            {
                _listChungTuChiTiet = _bhCpChungTuChiTietService.FindBhCpChungTuCSSKHSSVandNLDChiTiet(searchCondition).ToList();
            }

            else
            {
                _listChungTuChiTiet = _bhCpChungTuChiTietService.FindBhCpChungTuChiTiet(searchCondition).ToList();
            }

            var model = _bhCpChungTuService.FindById(Model.Id);
            _listChungTuChiTiet.ForEach(x =>
            {
                if (Model.SLNS != LNSValue.LNS_9050001_9050002)
                {
                    if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                    {
                        x.IsHangCha = false;
                        x.BHangCha = false;
                    }
                    if (Model.SLNS == LNSValue.LNS_9010001_9010002 || Model.SLNS == LNSValue.LNS_901_9010001_9010002)
                    {
                        x.IsHangCha = false;
                        x.BHangCha = false;
                    }

                    if (Model.SLNS == LNSValue.LNS_9010001_9010002 || Model.SLNS == LNSValue.LNS_9010003 || Model.SLNS == LNSValue.LNS_9010006_9010007 || Model.SLNS == LNSValue.LNS_9010004_9010005)
                    {
                        if (x.Id.IsNullOrEmpty() && model != null)
                        {
                            x.FTienKeHoachCap = model.FTyLeThu * x.FTienDuToan / 100;
                        }
                    }
                }
            });

            if (Model.SLNS == LNSValue.LNS_9010006_9010007)
            {
                _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.SL)).ToList();
            }
            var existBhChiTiet = _bhCpChungTuChiTietService.ExistCpChungTuChiTiet(searchCondition);
            foreach (var item in _listChungTuChiTiet)
            {
                item.IsAuToFillTuChi = !existBhChiTiet;
            }
            if (!string.IsNullOrEmpty(Model.SMaLoaiChi) && Model.SMaLoaiChi.Equals(MaDanhMucLoaiChi.CHI_KINH_PHI_QUAN_Y_DON_VI))
                _listChungTuChiTiet = _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).ToList();

            Items = _mapper.Map<ObservableCollection<BhCpChungTuChiTietModel>>(_listChungTuChiTiet);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhCpChungTuChiTietModel>>(_listChungTuChiTiet);
            _itemViews = CollectionViewSource.GetDefaultView(Items);
            _itemViews.Filter = ItemsViewFilter;

            CalculateData();
            foreach (var cpChungTuChiTietModel in Items)
            {
                cpChungTuChiTietModel.IsFilter = true;
                if (!cpChungTuChiTietModel.IsHangCha)
                {
                    if (NumberUtils.DoubleIsNullOrZero(cpChungTuChiTietModel.FTienKeHoachCap) && !cpChungTuChiTietModel.Id.IsNullOrEmpty())
                        cpChungTuChiTietModel.IsModified = true;
                    cpChungTuChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        BhCpChungTuChiTietModel item = (BhCpChungTuChiTietModel)sender;
                        if (!IsInit)
                        {
                            if (args.PropertyName.Equals(nameof(BhCpChungTuChiTietModel.SGhiChu)) || args.PropertyName.Equals(nameof(BhCpChungTuChiTietModel.FTienKeHoachCap)))
                            {
                                cpChungTuChiTietModel.IsModified = true;
                                item.IsModified = true;
                                CalculateData();

                            }

                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsOpenPrintPopup));
                        }
                    };
                }
                if (!existBhChiTiet && cpChungTuChiTietModel.FTienDuToan.GetValueOrDefault(0) != 0)
                {
                    cpChungTuChiTietModel.IsModified = true;
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
        }

        private void CalculateData()
        {
            try
            {
                Items.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     x.FTienDaCaQuyTruoc = 0;
                     x.FTienDuToan = 0;
                     x.FTienKeHoachCap = 0;
                 });

                var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
                var dictByMlns = Items.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
                foreach (var item in temp)
                {
                    item.FConLai = item.FConLaiChange;
                    CalculateParent(item.IdParent, item, dictByMlns);
                }

                UpdateTotal();
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void UpdateTotal()
        {
            Model.FTienDaCap = 0;
            Model.FTienKeHoachCap = 0;
            Model.FTienDuToan = 0;
            var roots = Items.Where(t => !t.IsHangCha).ToList();
            foreach (var item in roots)
            {
                Model.FTienDaCap += item.FTienDaCap;
                Model.FTienDaCap += item.FTienDaCap;
                Model.FTienKeHoachCap = Model.FTienKeHoachCap + item.FTienKeHoachCap.GetValueOrDefault(0);
                Model.FTienDuToan += item.FTienDuToan;

            }
            Model.FTongTienConLai = roots.Where(t => !t.IsHangCha).Sum(x => x.FConLai);
            Model.FTienDuToan = roots.Where(t => !t.IsHangCha).Sum(x => x.FTienDuToan);
        }

        private void CalculateParent(Guid idParent, BhCpChungTuChiTietModel item, Dictionary<Guid?, BhCpChungTuChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienDuToan += item.FTienDuToan;
            model.FTienDaCaQuyTruoc += item.FTienDaCap;
            model.FTienKeHoachCap += item.FTienKeHoachCap;
            model.FConLai = model.FConLaiChange;

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }
        #endregion

        #region PrintReport
        private void OnPrintDetal(object obj)
        {
            try
            {
                if (!_sessionService.Current.IsQuanLyDonViCha)
                {
                    MessageBoxHelper.Warning(Resources.AlertRolePrintReportAllocation);
                    return;
                }

                int dialogType = (int)obj;
                switch (dialogType)
                {
                    case (int)AllocationPrintTypeOfBH.PRINT_AllOCATION_NOTICE:
                        PrintChungTuCapPhatNoticeViewModel.IsShowDotCap = true;
                        PrintChungTuCapPhatNoticeViewModel.AllocationPrintType = (AllocationPrintTypeOfBH)dialogType;
                        PrintChungTuCapPhatNoticeViewModel.IsEnableTongTop = true;
                        PrintChungTuCapPhatNoticeViewModel.Init();
                        var view1 = new PrintChungTuCapPhatNotice
                        {
                            DataContext = PrintChungTuCapPhatNoticeViewModel
                        };
                        DialogHost.Show(view1, SettlementScreen.DETAIL_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintTypeOfBH.PRINT_ALLOCATION_PLAN:
                        PrintChungTuCapPhatNoticeViewModel.IsShowDotCap = false;
                        PrintChungTuCapPhatNoticeViewModel.AllocationPrintType = (AllocationPrintTypeOfBH)dialogType;
                        PrintChungTuCapPhatNoticeViewModel.Init();
                        var view2 = new PrintChungTuCapPhatNotice
                        {
                            DataContext = PrintChungTuCapPhatNoticeViewModel
                        };
                        DialogHost.Show(view2, SettlementScreen.DETAIL_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintTypeOfBH.PRINT_ALLOCATION_AGENCY:
                        PrintChungTuCapPhatDonViViewModel.Init();
                        var view3 = new PrintChungTuCapPhatDonVi
                        {
                            DataContext = PrintChungTuCapPhatDonViViewModel
                        };
                        DialogHost.Show(view3, SettlementScreen.DETAIL_DIALOG, null, null);
                        break;
                    case (int)AllocationPrintTypeOfBH.PRINT_ALLOCATION_TYPES:
                        PrintChungTuCapPhatThongTriViewModel.Init();
                        var view4 = new PrintChungTuCapPhatThongTri
                        {
                            DataContext = PrintChungTuCapPhatThongTriViewModel
                        };
                        DialogHost.Show(view4, SettlementScreen.DETAIL_DIALOG, null, null);
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

        #region close
        public override void OnClose(object obj)
        {
            ((Window)obj).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }
        #endregion

        #region OnSave
        public override void OnSave()
        {
            try
            {
                if (!IsSaveData)
                {
                    return;
                }

                Func<BhCpChungTuChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.Id.IsNullOrEmpty() && !x.IsHangCha;
                Func<BhCpChungTuChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.Id.IsNullOrEmpty() && !x.IsHangCha;
                Func<BhCpChungTuChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                var detailsUpdate = Items.Where(isUpdate).ToList();
                var detailsDelete = Items.Where(isDelete).ToList();

                //thêm mới chứng từ chi tiết
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhCpChungTuChiTiet>();
                    _mapper.Map(detailsAdd, addItems);
                    _bhCpChungTuChiTietService.AddRange(addItems);

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
                        var khtBHXHChiTiet = _bhCpChungTuChiTietService.FindById(updateItem.Id);
                        _mapper.Map(updateItem, khtBHXHChiTiet);
                        _bhCpChungTuChiTietService.Update(khtBHXHChiTiet);
                        updateItem.IsModified = false;
                    }
                }

                if (detailsAdd.Count > 0 || detailsUpdate.Count > 0)
                {
                    Guid ID = Model.Id;
                    //cập nhật tổng cộng chứng từ
                    var cpChungTuChiTiet = _bhCpChungTuService.FindById(ID);
                    cpChungTuChiTiet.FTienDuToan = Model.FTienDuToan;
                    cpChungTuChiTiet.FTienDaCap = Model.FTienDaCap;
                    cpChungTuChiTiet.FTienKeHoachCap = Model.FTienKeHoachCap;
                    _bhCpChungTuService.Update(cpChungTuChiTiet);

                    OnRefresh();
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
                    var message = Resources.MsgSaveDone;
                    MessageBoxHelper.Info(message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Search

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhCpChungTuChiTietModel)obj;
            //result = VoucherDetailFilter(item);
            //if (!result && item.IsHangCha)
            //{
            //    result = xnmConcatenation.Contains(item.SXauNoiMa);
            //}
            if (result)
                item.IsFilter = result;
            if (!IsDefault)
            {
                result = DataSearch.Any(x => x.IID_MucLucNganSach.Equals(item.IID_MucLucNganSach));

            }
            return result;
        }


        private void OnSearch()
        {
            SearchTextFilter();
        }


        private void LoadDefault()
        {
            SM = string.Empty;
            STM = string.Empty;
            SNoiDungSearch = string.Empty;
            DataSearch = new ObservableCollection<BhCpChungTuChiTietModel>();
        }

        private void OnClearSearch(object obj)
        {
            LoadDefault();
            _itemViews.Refresh();
        }

        private void SearchTextFilter()
        {
            List<string> lstParents = new List<string>();
            List<BhCpChungTuChiTietModel> lstChildSearch = new List<BhCpChungTuChiTietModel>();
            List<BhCpChungTuChiTietModel> lstParentSearch = new List<BhCpChungTuChiTietModel>();
            List<BhCpChungTuChiTietModel> results = new List<BhCpChungTuChiTietModel>();
            if (!string.IsNullOrEmpty(SM))
            {
                lstChildSearch = Items.Where(x => x.SM.ToLower().Contains(SM.ToLower()) && !x.IsHangCha).ToList();
                lstParentSearch = Items.Where(x => x.SM.ToLower().Contains(SM.ToLower()) && x.IsHangCha).ToList();
            }

            if (!string.IsNullOrEmpty(STM))
            {
                if (!string.IsNullOrEmpty(SM))
                {
                    lstChildSearch = lstChildSearch.Where(x => x.STM.ToLower().Contains(STM.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = lstParentSearch.Where(x => x.STM.ToLower().Contains(STM.ToLower()) && x.IsHangCha).ToList();
                }
                else
                {
                    lstChildSearch = Items.Where(x => x.STM.ToLower().Contains(STM.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = Items.Where(x => x.STM.ToLower().Contains(STM.ToLower()) && x.IsHangCha).ToList();
                }
            }
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                if (!string.IsNullOrEmpty(SM))
                {
                    lstChildSearch = lstChildSearch.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = lstParentSearch.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
                }
                else if (!string.IsNullOrEmpty(STM))
                {
                    lstChildSearch = lstChildSearch.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = lstParentSearch.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
                }
                else
                {
                    lstChildSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).ToList();
                    lstParentSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).ToList();
                }

            }
            if (!lstChildSearch.IsEmpty())
            {
                lstParents.AddRange(StringUtils.GetListKyHieuParent(lstChildSearch.Select(x => x.SXauNoiMa).Distinct().ToList()));
                lstParents.Add(lstParents[0].Substring(0, 1));
                lstParents.Add(lstParents[0].Substring(0, 3));
                results = Items.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
            }
            if (!lstParentSearch.IsEmpty())
            {
                if (results.IsEmpty())
                    results = GetDataParent(lstParentSearch.Select(x => x.SXauNoiMa).Distinct().ToList());
                else
                    results.AddRange(GetDataParent(lstParentSearch.Select(x => x.SXauNoiMa).Distinct().Where(x => !lstParents.Contains(x)).ToList()));
            }
            DataSearch = new ObservableCollection<BhCpChungTuChiTietModel>(results);
            _itemViews.Refresh();
        }

        private List<BhCpChungTuChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhCpChungTuChiTietModel> result = new List<BhCpChungTuChiTietModel>();
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

        private void GetListChild(List<BhCpChungTuChiTietModel> lstInput, List<BhCpChungTuChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IID_MucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IID_MucLucNganSach ?? Guid.Empty)))
                {
                    GetListChild(new List<BhCpChungTuChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
