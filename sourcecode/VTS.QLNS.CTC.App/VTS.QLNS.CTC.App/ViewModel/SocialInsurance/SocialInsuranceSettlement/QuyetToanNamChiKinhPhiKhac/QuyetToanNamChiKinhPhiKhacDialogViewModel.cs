using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac
{
    public class QuyetToanNamChiKinhPhiKhacDialogViewModel : DialogViewModelBase<BhQtcNamKinhPhiKhacModel>
    {
        #region Interface
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhQtcNamKinhPhiKhacService _kinhPhiKhacService;
        private readonly IBhQtcNamKinhPhiKhacChiTietService _kinhPhiKhacChiTietService;
        private readonly IBhDanhMucLoaiChiService _loaiChiService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly INsDonViService _nSDonViService;
        private readonly IBhQtcQuyKPKService _quyKCBService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _dataLNSView;
        #endregion

        #region Property
        public override Type ContentType => typeof(QuyetToanNamChiKinhPhiKhacDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới quyết toán chi năm kinh phí khác" : "Cập nhật quyết toán chi năm kinh phí khác";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public bool IsSummary { get; set; }
        public bool IsAgregate { get; set; }
        public List<string> ListIdDonViHasCt { get; set; }
        public bool IsShowBThucChiTheo4Quy => Guid.Empty.Equals(Model.Id) && !IsAgregate;
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public List<BhQtcNamKinhPhiKhacModel> LstNamKinhPhiKhacModel { get; set; }
        private List<BhQtcNamKinhPhiKhacModel> _lstChungTu;
        public List<BhQtcNamKinhPhiKhacModel> LstChungTu
        {
            get => _lstChungTu;
            set => SetProperty(ref _lstChungTu, value);
        }
        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }
        public bool IsEdit => Model.Id == Guid.Empty && !IsSummary;

        private bool _isBThucChiTheo4Quy;
        public bool IsBThucChiTheo4Quy
        {
            get => _isBThucChiTheo4Quy;
            set
            {
                SetProperty(ref _isBThucChiTheo4Quy, value);
            }
        }
        private string _searchNsDonVi;
        public string SearchNsDonVi
        {
            get => _searchNsDonVi;
            set
            {
                if (SetProperty(ref _searchNsDonVi, value))
                {
                    _nsDonViModelsView.Refresh();
                }
            }
        }

        private ObservableCollection<DonViModel> _donViModelItems;
        public ObservableCollection<DonViModel> DonViModelItems
        {
            get => _donViModelItems;
            set
            {
                SetProperty(ref _donViModelItems, value);
                OnPropertyChanged();
            }
        }
        public string SelectedCountNsDonVi
        {
            get
            {
                var totalCount = DonViModelItems != null ? DonViModelItems.Count() : 0;
                var totalSelected = DonViModelItems != null ? DonViModelItems.Count(item => item.Selected) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }
        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                LoadUnits();
                if (_selectedDanhMucLoaiChi != null)
                {
                    LoadLNS();
                }
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }
        #region list LNS

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;
        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Any()) ? false : DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _dataLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public QuyetToanNamChiKinhPhiKhacDialogViewModel(
                        IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                        IMapper mapper,
                        ISessionService sessionService,
                        INsDonViService nSDonViService,
                        ISysAuditLogService log,
                        ILog logger,
                        IBhQtcNamKinhPhiKhacService bhQtcNamKinhPhikhacService,
                        IBhQtcNamKinhPhiKhacChiTietService bhQtcNamKinhPhikhacChiTietService,
                        IBhDanhMucLoaiChiService loaiChiService,
                        IBhQtcQuyKPKService quyKCBService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nSDonViService = nSDonViService;
            _log = log;
            _logger = logger;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _kinhPhiKhacService = bhQtcNamKinhPhikhacService;
            _kinhPhiKhacChiTietService = bhQtcNamKinhPhikhacChiTietService;
            _loaiChiService = loaiChiService;
            _quyKCBService = quyKCBService;
        }
        #endregion

        #region Init
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            IsSaveData = true;
            LoadLNS();
            LoadUnits();
            LoadDanhMucLoaiChi();
            LoadData();
        }
        #endregion

        #region Load data
        private void LoadUnits()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = KhcStatusType.ACTIVE;

            var lstChungTu = _kinhPhiKhacService.FindIndex(yearOfWork);
            if (SelectedDanhMucLoaiChi != null)
            {
                lstChungTu = lstChungTu.Where(x => x.IID_LoaiChi == SelectedDanhMucLoaiChi.Id).ToList();
            }

            ListIdDonViHasCt = lstChungTu.Select(item => item.IID_MaDonVi).ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);
            bool isDvCap4 = _nSDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
                if (Model.Id == Guid.Empty)
                {
                    predicate = predicate.And(x => !ListIdDonViHasCt.Any(y => y == x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
                    predicate = predicate.And(x => !idDonVisExclude.Any(y => y == x.IIDMaDonVi));
                }
            }
            else
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
                if (Model.Id == Guid.Empty)
                {
                    predicate = predicate.And(x => !ListIdDonViHasCt.Any(y => y == x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
                    predicate = predicate.And(x => !idDonVisExclude.Any(y => y == x.IIDMaDonVi));
                }
            }
            var listUnit = _nSDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
            var listDonViByUser = _nSDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);

            DonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
            if (!string.IsNullOrEmpty(Model.IID_MaDonVi))
            {
                DonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
            _nsDonViModelsView = CollectionViewSource.GetDefaultView(DonViModelItems);
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                ListSortDirection.Ascending));
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                ListSortDirection.Ascending));
            _nsDonViModelsView.Filter = DonViFilter;
            foreach (var model in DonViModelItems)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DonViModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedCountNsDonVi));
                    }

                };
            }
        }

        private bool DonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchNsDonVi))
            {
                return true;
            }
            var item = (DonViModel)obj;
            var condition = item.TenDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower()) ||
                            item.IIDMaDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower());
            return condition;
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                if (Model == null || Model.Id == Guid.Empty)
                {
                    Model.DNgayChungTu = DateTime.Now;
                    Model.DNgayQuyetDinh = DateTime.Now;
                    Model.INamLamViec = _sessionInfo.YearOfWork;
                    LoadChungTuIndex();
                }
                else
                {
                    var loaiChiOld = ItemsDanhMucLoaiChi.Where(x => x.Id == Model.IID_LoaiChi).FirstOrDefault();
                    if (loaiChiOld != null)
                    {
                        SelectedDanhMucLoaiChi = loaiChiOld;
                    }

                    DonViModel donViModel = DonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                    if (donViModel != null)
                    {
                        donViModel.Selected = true;
                    }

                    //SetCheckboxSelected(_dataLNS, Model.SDSLNS);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public static void SetCheckboxSelected(ObservableCollection<BhDmMucLucNganSachModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").ToList();
            foreach (BhDmMucLucNganSachModel item in data)
            {
                item.IsSelected = selectedValues.Contains(item.SLNS);
            }
        }

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _loaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            listDanhMucLoaiChi = listDanhMucLoaiChi.Where(x => x.SLNS == LNSValue.LNS_9010006_9010007
                                                            || x.SLNS == LNSValue.LNS_9050001_9050002
                                                            || x.SLNS == LNSValue.LNS_9010008
                                                            || x.SLNS == LNSValue.LNS_9010009
                                                            || x.SLNS == LNSValue.LNS_9010010);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.SMaLoaiChi,
                    HiddenValue = n.SLNS,
                    Id = n.Id,
                }));

                if (Model.Id.IsNullOrEmpty())
                {
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
                }
                else
                {
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.Where(z => z.Id == Model.IID_LoaiChi).FirstOrDefault();
                }
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            List<BhDmMucLucNganSach> listMLNS;
            if (SelectedDanhMucLoaiChi != null)
            {
                listMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(yearOfWork, SelectedDanhMucLoaiChi.HiddenValue).ToList();
            }
            else listMLNS = new List<BhDmMucLucNganSach>();

            DataLNS = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(listMLNS);
            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;

            if (_dataLNS != null && _dataLNS.Count > 0)
            {
                foreach (var model in _dataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                        {
                            foreach (var item in _dataLNS)
                            {
                                if (item.IIDMLNSCha == model.IIDMLNS)
                                {
                                    item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(SelectedCountLNS));
                        }
                    };
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.LNSDisplay.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadChungTuIndex()
        {
            int soChungTuIndex = _kinhPhiKhacService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            Model.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
        }
        #endregion

        #region On Save
        public override void OnSave()
        {
            try
            {
                base.OnSave();
                DateTime dtNow = DateTime.Now;
                var iNamChungTu = _sessionInfo.YearOfWork;

                var donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);

                if (!Validate()) return;

                BhQtcNamKinhPhiKhac entity;
                var lstDonVi = _nSDonViService.FindAll().Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                var donVi = GetDonViOfCurrentUser();

                // Add
                if (IsAgregate)
                {
                    var lstLNS = string.Join(",", LstNamKinhPhiKhacModel.Select(x => x.SDSLNS).ToList());
                    var loaiChungTu = LstNamKinhPhiKhacModel.Count > 0 ? LstNamKinhPhiKhacModel[0].ILoaiTongHop : -1;
                    if (Model.Id.IsNullOrEmpty())
                    {
                        var lstSoChungTu = string.Join(",", LstNamKinhPhiKhacModel.Select(x => x.SSoChungTu).ToList());
                        var predicateSummary = PredicateBuilder.True<BhQtcNamKinhPhiKhac>();
                        predicateSummary = predicateSummary.And(x => x.INamLamViec == iNamChungTu && x.ILoaiTongHop == loaiChungTu && x.IID_MaDonVi == donVi.IIDMaDonVi
                                                            && x.IID_LoaiChi == LstNamKinhPhiKhacModel.Select(x => x.IID_LoaiChi).FirstOrDefault());
                        var lstQtcChungTu = _kinhPhiKhacService.FindByCondition(predicateSummary);
                        if (lstQtcChungTu.Any())
                        {
                            var firstQtcChungTu = lstQtcChungTu.FirstOrDefault();
                            if (!firstQtcChungTu.BIsKhoa)
                            {
                                MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                                if (messageBoxResult == MessageBoxResult.No)
                                {
                                    return;
                                }
                                var idQtcChungTu = firstQtcChungTu.Id;
                                var qtcChungTu = _kinhPhiKhacService.FindById(idQtcChungTu);
                                _kinhPhiKhacService.Delete(qtcChungTu);
                                var predicateSummaryDetail = PredicateBuilder.True<BhQtcNamKinhPhiKhacChiTiet>();
                                predicateSummaryDetail = predicateSummaryDetail.And(x => x.Id == idQtcChungTu);
                                var qtcChungTuChiTiet = _kinhPhiKhacChiTietService.FindByCondition(predicateSummaryDetail);
                                _kinhPhiKhacChiTietService.RemoveRange(qtcChungTuChiTiet);
                            }
                            else
                            {
                                MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                                return;
                            }
                        }
                        entity = new BhQtcNamKinhPhiKhac();
                        _mapper.Map(Model, entity);
                        entity.IID_DonVi = donVi.Id;
                        entity.IID_MaDonVi = donVi.IIDMaDonVi;
                        entity.IID_TongHopID = Guid.NewGuid();
                        entity.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTuTongHop;
                        entity.IID_LoaiChi = LstNamKinhPhiKhacModel[0].IID_LoaiChi;
                        entity.STongHop = lstSoChungTu;
                        entity.BIsKhoa = false;
                        entity.SDSLNS = lstLNS;
                        entity.BThucChiTheo4Quy = false;
                        entity.SNguoiTao = _sessionInfo.Principal;
                        entity.DNgayTao = DateTime.Now;
                        _kinhPhiKhacService.Add(entity);
                        CreateQtcKinhphiQuanlyVoucherDetail(_mapper.Map<BhQtcNamKinhPhiKhacModel>(entity));
                        QtcNamKinhPhiKhacCriteria searchCondition = new QtcNamKinhPhiKhacCriteria();
                        searchCondition.ID = entity.Id;
                        searchCondition.IDMaDonVi = entity.IID_MaDonVi;
                        searchCondition.LoaiChungTu = BhxhLoaiChungTu.BhxhChungTuTongHop;
                        searchCondition.SLNS = entity.SDSLNS;
                        searchCondition.IsTongHop4Quy = entity.BThucChiTheo4Quy;
                        searchCondition.NamLamViec = entity.INamLamViec;
                        var lstQtcKinhphiQuanlyDetail =
                            _kinhPhiKhacChiTietService.FindChungTuChiTiet(searchCondition);
                        if (lstQtcKinhphiQuanlyDetail.Any())
                        {
                            entity.FTongTien_DuToanGiaoNamNay = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTien_DuToanGiaoNamNay);
                            entity.FTongTien_DuToanNamTruocChuyenSang = lstQtcKinhphiQuanlyDetail.Sum(x => x.FDuToanNamTruocChuyenSang);
                            entity.FTongTien_TongDuToanDuocGiao = entity.FTongTien_DuToanGiaoNamNay + entity.FTongTien_DuToanNamTruocChuyenSang;
                            entity.FTongTien_ThucChi = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTien_ThucChi);
                            entity.FTongTienThieu = entity.FTongTien_ThucChi > entity.FTongTien_TongDuToanDuocGiao ? entity.FTongTien_ThucChi - entity.FTongTien_TongDuToanDuocGiao : 0;
                            entity.FTongTienThua = entity.FTongTien_TongDuToanDuocGiao > entity.FTongTien_ThucChi ? entity.FTongTien_TongDuToanDuocGiao - entity.FTongTien_ThucChi : 0;
                            entity.FTiLeThucHienTrenDuToan = entity.FTongTien_TongDuToanDuocGiao > entity.FTongTien_ThucChi ? entity.FTongTien_ThucChi / entity.FTongTien_TongDuToanDuocGiao : 0;
                            _kinhPhiKhacService.Update(entity);
                        }
                    }
                    else
                    {
                        entity = _kinhPhiKhacService.FindById(Model.Id);
                        _mapper.Map(Model, entity);
                        entity.DNgaySua = DateTime.Now;
                        entity.SNguoiSua = _sessionService.Current.Principal;
                    }
                }
                else
                {
                    if (Model.Id.IsNullOrEmpty())
                    {
                        var lstSLNS = DataLNS.Where(n => n.IsSelected).ToList();
                        if (donViSelected == null)
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                            return;
                        }

                        if (lstSLNS == null)
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLNS);
                            return;
                        }

                        entity = new BhQtcNamKinhPhiKhac();
                        _mapper.Map(Model, entity);
                        entity.DNgayTao = DateTime.Now;
                        entity.SDSLNS = SelectedDanhMucLoaiChi.HiddenValue;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        entity.DNgaySua = null;
                        entity.IID_DonVi = donViSelected?.Id;
                        entity.IID_MaDonVi = donViSelected?.IIDMaDonVi;
                        entity.IID_LoaiChi = SelectedDanhMucLoaiChi.Id;
                        if (donViSelected != null && donViSelected.Loai == LoaiDonVi.ROOT)
                        {
                            entity.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTuTongHop;
                            entity.IID_TongHopID = Guid.NewGuid();
                        }
                        else
                        {
                            entity.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                        }

                        entity.BThucChiTheo4Quy = IsBThucChiTheo4Quy;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _kinhPhiKhacService.Add(entity);

                        if (IsBThucChiTheo4Quy)
                        {
                            _kinhPhiKhacService.CreateQTCNamKPKFor4Quy(entity.Id, entity.IID_MaDonVi,
                                                                        _sessionInfo.YearOfWork, entity.SNguoiTao, entity.IID_LoaiChi);
                            QtcNamKinhPhiKhacCriteria searchCondition = new QtcNamKinhPhiKhacCriteria();
                            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                            searchCondition.SNguoiTao = entity.SNguoiTao;
                            searchCondition.SLNS = entity.SDSLNS;
                            searchCondition.DNgayChungTu = DateTime.Now;
                            searchCondition.IsTongHop4Quy = entity.BThucChiTheo4Quy;
                            searchCondition.IDMaDonVi = entity.IID_MaDonVi;
                            searchCondition.ID = entity.Id;
                            searchCondition.LoaiChungTu = !IsDonViRoot(entity.IID_MaDonVi) ? BhxhLoaiChungTu.BhxhChungTu : BhxhLoaiChungTu.BhxhChungTuTongHop;
                            //update chứng từ
                            var chungtu = _kinhPhiKhacService.FindById(entity.Id);
                            var lstChungTu = _kinhPhiKhacChiTietService.FindByCondition(x => x.IID_QTC_Nam_KPK == chungtu.Id);

                            var _listChungTuChiTiet = _kinhPhiKhacChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                            if (lstChungTu.Any())
                            {
                                chungtu.FTongTien_ThucChi = lstChungTu.Sum(x => x.FTien_ThucChi ?? 0);
                                chungtu.FTongTien_DuToanNamTruocChuyenSang = _listChungTuChiTiet?.FirstOrDefault().FDuToanNamTruocChuyenSang ?? 0;
                                chungtu.FTongTien_DuToanGiaoNamNay = _listChungTuChiTiet?.FirstOrDefault().FTien_DuToanGiaoNamNay ?? 0;
                                chungtu.FTongTien_TongDuToanDuocGiao = chungtu.FTongTien_DuToanNamTruocChuyenSang + chungtu.FTongTien_DuToanGiaoNamNay;
                                chungtu.FTongTienThua = chungtu.FTongTien_TongDuToanDuocGiao > chungtu.FTongTien_ThucChi ? chungtu.FTongTien_TongDuToanDuocGiao - chungtu.FTongTien_ThucChi : 0;
                                chungtu.FTongTienThieu = chungtu.FTongTien_ThucChi > chungtu.FTongTien_TongDuToanDuocGiao ? chungtu.FTongTien_ThucChi - chungtu.FTongTien_TongDuToanDuocGiao : 0;
                                chungtu.FTiLeThucHienTrenDuToan = chungtu.FTongTien_TongDuToanDuocGiao > chungtu.FTongTien_ThucChi ? chungtu.FTongTien_ThucChi / chungtu.FTongTien_TongDuToanDuocGiao : 0;
                            }

                            _kinhPhiKhacService.Update(chungtu);
                        }
                    }
                    else
                    {
                        entity = _kinhPhiKhacService.FindById(Model.Id);
                        _mapper.Map(Model, entity);
                        entity.SDSLNS = LNSValue.LNS_9010004_9010005;
                        entity.DNgaySua = DateTime.Now;
                        entity.SNguoiSua = _sessionService.Current.Principal;
                    }
                }

                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhQtcNamKinhPhiKhacModel>(entity));

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        private string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> dataLNS)
        {
            if (dataLNS.Count > 0)
            {
                return string.Join(",", dataLNS.Where(n => n.IsSelected).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }

        private void CreateQtcKinhphiQuanlyVoucherDetail(BhQtcNamKinhPhiKhacModel bhQtcNamKinhPhiQuanLyModel)
        {
            QtcNamKinhPhiKhacCriteria criteria = new QtcNamKinhPhiKhacCriteria();
            criteria.ID = bhQtcNamKinhPhiQuanLyModel.Id;
            criteria.IDDonVi = bhQtcNamKinhPhiQuanLyModel.IID_DonVi;
            criteria.IDMaDonVi = bhQtcNamKinhPhiQuanLyModel.IID_MaDonVi;
            criteria.LoaiChungTu = bhQtcNamKinhPhiQuanLyModel.ILoaiTongHop;
            criteria.NamLamViec = bhQtcNamKinhPhiQuanLyModel.INamLamViec;
            criteria.SNguoiTao = bhQtcNamKinhPhiQuanLyModel.SNguoiTao;
            criteria.LstSoChungTu = string.Join(",", LstNamKinhPhiKhacModel.Select(x => x.Id.ToString()).ToList());
            _kinhPhiKhacChiTietService.AddAggregate(criteria);
        }

        private DonVi GetDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nSDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private bool Validate()
        {
            StringBuilder messageBuilder = new StringBuilder();

            if (Model.DNgayChungTu == null)
            {
                messageBuilder.AppendFormat(Resources.AlertNgayChungTuEmpty);
            }

            if (Model.DNgayQuyetDinh == null)
            {
                messageBuilder.AppendFormat(Resources.AlertNgayQuyetDinhEmpty);
            }

            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return false;
            }

            if (!IsAgregate)
            {
                if (SelectedDanhMucLoaiChi == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgCheckLoaiKeHoachChi);
                }

                var donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);
                if (donViSelected == null)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return false;
                }

                //Check đã tồn tại đơn vị
                var predicate = PredicateBuilder.True<BhQtcNamKinhPhiKhac>();
                predicate = predicate.And(x => x.IID_MaDonVi == donViSelected.IIDMaDonVi);
                predicate = predicate.And(x => x.IID_LoaiChi == SelectedDanhMucLoaiChi.Id);
                predicate = predicate.And(x => x.IID_DonVi == donViSelected.Id);
                predicate = predicate.And(x => x.INamLamViec == Model.INamLamViec);

                var chungtu = _kinhPhiKhacService.FindByCondition(predicate).FirstOrDefault();
                if (chungtu != null)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementMonthVoucher, donViSelected.TenDonViDisplay, Model.INamLamViec, SelectedDanhMucLoaiChi.HiddenValue));
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region close
        public override void OnClose(object obj)
        {
            try
            {
                base.OnClose(obj);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
