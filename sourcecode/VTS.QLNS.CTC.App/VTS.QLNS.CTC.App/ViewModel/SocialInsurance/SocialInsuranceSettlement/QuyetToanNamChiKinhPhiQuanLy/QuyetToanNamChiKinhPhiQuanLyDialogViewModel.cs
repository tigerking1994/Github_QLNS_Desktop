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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy
{
    public class QuyetToanNamChiKinhPhiQuanLyDialogViewModel : DialogViewModelBase<BhQtcNamKinhPhiQuanLyModel>
    {
        #region Interface
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhQtcNamKinhPhiQuanLyService _kinhPhiQuanLyService;
        private readonly IBhQtcNamKinhPhiQuanLyChiTietService _kinhPhiQuanLyServiceChiTietService;
        private readonly IBhQtcQuyKinhPhiQuanLyService _bhQtcQKPQuanLyService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly INsDonViService _nSDonViService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _dataLNSView;
        #endregion

        #region Property
        public override Type ContentType => typeof(QuyetToanNamChiKinhPhiQuanLyDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới quyết toán chi năm kinh phí quản lý " : "Cập nhật quyết toán chi năm kinh phí quản lý";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public bool IsSummary { get; set; }
        public bool IsAgregate { get; set; }
        public List<string> ListIdDonViHasCt { get; set; }
        public bool IsShowBThucChiTheo4Quy => Guid.Empty.Equals(Model.Id) && !IsAgregate;
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public List<BhQtcNamKinhPhiQuanLyModel> LstNamKinhPhiQuanLyModel { get; set; }
        private List<BhQtcNamKinhPhiQuanLyModel> _lstChungTu;
        public List<BhQtcNamKinhPhiQuanLyModel> LstChungTu
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
        public QuyetToanNamChiKinhPhiQuanLyDialogViewModel(
                        IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                        IMapper mapper,
                        ISessionService sessionService,
                        INsDonViService nSDonViService,
                        ISysAuditLogService log,
                        ILog logger,
                        IBhQtcNamKinhPhiQuanLyService bhQtcNamKinhPhiQuanLyService,
                        IBhQtcNamKinhPhiQuanLyChiTietService bhQtcNamKinhPhiQuanLyChiTietService,
                        IBhQtcQuyKinhPhiQuanLyService bhQtcQKPQuanLyService,
                        IBhDanhMucLoaiChiService bhDanhMucLoaiChiService)
        {
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _mapper = mapper;
            _sessionService = sessionService;
            _nSDonViService = nSDonViService;
            _log = log;
            _logger = logger;
            _kinhPhiQuanLyService = bhQtcNamKinhPhiQuanLyService;
            _kinhPhiQuanLyServiceChiTietService = bhQtcNamKinhPhiQuanLyChiTietService;
            _bhQtcQKPQuanLyService = bhQtcQKPQuanLyService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
        }
        #endregion

        #region Init
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            IsSaveData = true;
            LoadUnits();
            //LoadLNS();
            LoadData();
        }
        #endregion

        #region Load data
        private void LoadUnits()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstChungTu = _kinhPhiQuanLyService.FindIndex(yearOfWork);
            ListIdDonViHasCt = lstChungTu.Select(item => item.IID_MaDonVi).ToList();
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = KhcStatusType.ACTIVE;
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

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            List<BhDmMucLucNganSach> listMLNS;
            listMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(yearOfWork, LNSValue.LNS_9010003).ToList();
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
                    DonViModel donViModel = DonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                    if (donViModel != null)
                    {
                        donViModel.Selected = true;
                    }
                    SetCheckboxSelected(_dataLNS, Model.SDSLNS);
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

        private void LoadChungTuIndex()
        {
            int soChungTuIndex = _kinhPhiQuanLyService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
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

                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BhQtcNamKinhPhiQuanLy entity;
                var lstDonVi = _nSDonViService.FindAll().Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                var donVi = GetDonViOfCurrentUser();

                // Add
                if (IsAgregate)
                {
                    var loaiChungTu = LstNamKinhPhiQuanLyModel.Count > 0 ? LstNamKinhPhiQuanLyModel[0].ILoaiTongHop : -1;
                    var lstLNS = string.Join(",", LstNamKinhPhiQuanLyModel.Select(x => x.SDSLNS).ToList());
                    if (Model.Id.IsNullOrEmpty())
                    {
                        var lstSoChungTu = string.Join(",", LstNamKinhPhiQuanLyModel.Select(x => x.SSoChungTu).ToList());
                        var predicateSummary = PredicateBuilder.True<BhQtcNamKinhPhiQuanLy>();
                        predicateSummary = predicateSummary.And(x => x.INamLamViec == iNamChungTu && x.ILoaiTongHop == loaiChungTu && x.IID_MaDonVi == donVi.IIDMaDonVi);
                        var lstQtcChungTu = _kinhPhiQuanLyService.FindByCondition(predicateSummary);
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
                                var qtcChungTu = _kinhPhiQuanLyService.FindById(idQtcChungTu);
                                _kinhPhiQuanLyService.Delete(qtcChungTu);
                                var predicateSummaryDetail = PredicateBuilder.True<BhQtcNamKinhPhiQuanLyChiTiet>();
                                predicateSummaryDetail = predicateSummaryDetail.And(x => x.Id == idQtcChungTu);
                                var qtcChungTuChiTiet = _kinhPhiQuanLyServiceChiTietService.FindByCondition(predicateSummaryDetail);
                                _kinhPhiQuanLyServiceChiTietService.RemoveRange(qtcChungTuChiTiet);
                            }
                            else
                            {
                                MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                                return;
                            }
                        }
                        entity = new BhQtcNamKinhPhiQuanLy();
                        _mapper.Map(Model, entity);
                        entity.IID_DonVi = donVi.Id;
                        entity.IID_MaDonVi = donVi.IIDMaDonVi;
                        entity.IID_TongHopID = Guid.NewGuid();
                        entity.SDSLNS = lstLNS;
                        entity.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTuTongHop;
                        entity.STongHop = lstSoChungTu;
                        entity.BIsKhoa = false;
                        entity.BThucChiTheo4Quy = false;
                        entity.SNguoiTao = _sessionInfo.Principal;
                        entity.DNgayTao = DateTime.Now;
                        _kinhPhiQuanLyService.Add(entity);
                        CreateQtcKinhphiQuanlyVoucherDetail(_mapper.Map<BhQtcNamKinhPhiQuanLyModel>(entity));

                        QtcNamKinhPhiQuanLyCriteria searchCondition = new QtcNamKinhPhiQuanLyCriteria();
                        searchCondition.NamLamViec = entity.INamLamViec;
                        searchCondition.IDMaDonVi = entity.IID_MaDonVi;
                        searchCondition.IDDonVi = entity.IID_DonVi;
                        searchCondition.SNguoiTao = entity.SNguoiTao;
                        searchCondition.LoaiChungTu = entity.ILoaiTongHop;
                        searchCondition.ID = entity.Id;
                        searchCondition.SLNS = LNSValue.LNS_9010003;
                        //searchCondition.IDLoaiChi = loaiChi.Id;
                        searchCondition.DNgayChungTu = entity.DNgayChungTu;
                        searchCondition.IsTongHop4Quy = entity.BThucChiTheo4Quy;
                        //searchCondition.SMaLoaiChi = loaiChi.SMaLoaiChi;
                        var lstQtcKinhphiQuanlyDetail =
                            _kinhPhiQuanLyServiceChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                        if (lstQtcKinhphiQuanlyDetail.Any())
                        {
                            entity.FTongTien_DuToanGiaoNamNay = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTien_DuToanGiaoNamNay);
                            entity.FTongTien_DuToanNamTruocChuyenSang = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTien_DuToanNamTruocChuyenSang);
                            entity.FTongTien_TongDuToanDuocGiao = entity.FTongTien_DuToanGiaoNamNay + entity.FTongTien_DuToanNamTruocChuyenSang;
                            entity.FTongTien_ThucChi = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTien_ThucChi);
                            entity.FTiLeThucHienTrenDuToan = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTiLeThucHienTrenDuToan);
                            entity.FTongTienThieu = entity.FTongTien_ThucChi > entity.FTongTien_TongDuToanDuocGiao ? entity.FTongTien_ThucChi - entity.FTongTien_TongDuToanDuocGiao : 0;
                            entity.FTongTienThua = entity.FTongTien_TongDuToanDuocGiao > entity.FTongTien_ThucChi ? entity.FTongTien_TongDuToanDuocGiao - entity.FTongTien_ThucChi : 0; ;
                            _kinhPhiQuanLyService.Update(entity);
                        }
                    }
                    else
                    {
                        entity = _kinhPhiQuanLyService.FindById(Model.Id);
                        _mapper.Map(Model, entity);
                        entity.DNgaySua = DateTime.Now;
                        entity.SNguoiSua = _sessionService.Current.Principal;
                    }
                }
                else
                {
                    if (Model.Id.IsNullOrEmpty())
                    {
                        if (donViSelected == null)
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                            return;
                        }
                        entity = new BhQtcNamKinhPhiQuanLy();
                        _mapper.Map(Model, entity);
                        entity.DNgayTao = DateTime.Now;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        entity.DNgaySua = null;
                        entity.SDSLNS = LNSValue.LNS_9010003;
                        entity.IID_DonVi = donViSelected?.Id;
                        entity.IID_MaDonVi = donViSelected?.IIDMaDonVi;
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
                        _kinhPhiQuanLyService.Add(entity);

                        if (IsBThucChiTheo4Quy)
                        {
                            _kinhPhiQuanLyService.CreateQTCNamKPQLFor4Quy(entity.Id, entity.IID_MaDonVi, _sessionInfo.YearOfWork, entity.SNguoiTao);
                            DonVi donViParent = _nSDonViService.FindByLoai(LoaiDonVi.ROOT, iNamChungTu);
                            var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamChungTu).ToList();
                            var loaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(SettlementTypeSLNS.SLNS)).FirstOrDefault();
                            QtcNamKinhPhiQuanLyCriteria searchCondition = new QtcNamKinhPhiQuanLyCriteria();
                            searchCondition.NamLamViec = entity.INamLamViec;
                            searchCondition.IDMaDonVi = entity.IID_MaDonVi;
                            searchCondition.IDDonVi = entity.IID_DonVi;
                            searchCondition.SNguoiTao = entity.SNguoiTao;
                            searchCondition.ID = entity.Id;
                            searchCondition.SLNS = LNSValue.LNS_9010003;
                            searchCondition.IDLoaiChi = loaiChi.Id;
                            searchCondition.DNgayChungTu = entity.DNgayChungTu;
                            searchCondition.IsTongHop4Quy = entity.BThucChiTheo4Quy;
                            searchCondition.SMaLoaiChi = loaiChi.SMaLoaiChi;
                            searchCondition.LoaiChungTu = entity.IID_MaDonVi == donViParent.IIDMaDonVi ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                             var _listChungTuChiTiet = _kinhPhiQuanLyServiceChiTietService.FindChungTuChiTiet(searchCondition).ToList();

                            //update chứng từ
                            var chungtu = _kinhPhiQuanLyService.FindById(entity.Id);

                            var lstChungTu = _kinhPhiQuanLyServiceChiTietService.FindByCondition(x => x.IID_QTC_Nam_KinhPhiQuanLy == entity.Id);

                            if (lstChungTu.Any())
                            {
                                chungtu.FTongTien_ThucChi = lstChungTu.Sum(x => x.FTien_ThucChi).GetValueOrDefault();
                                chungtu.FTongTien_DuToanGiaoNamNay = _listChungTuChiTiet?.FirstOrDefault().FTien_DuToanGiaoNamNay.GetValueOrDefault();
                                chungtu.FTongTien_DuToanNamTruocChuyenSang = _listChungTuChiTiet?.FirstOrDefault().FDuToanNamTruocChuyenSang.GetValueOrDefault();
                                chungtu.FTongTien_TongDuToanDuocGiao = chungtu.FTongTien_DuToanGiaoNamNay.GetValueOrDefault() + chungtu.FTongTien_DuToanNamTruocChuyenSang.GetValueOrDefault();
                                chungtu.FTongTienThua = chungtu.FTongTien_TongDuToanDuocGiao.GetValueOrDefault() > chungtu.FTongTien_ThucChi.GetValueOrDefault() ? chungtu.FTongTien_TongDuToanDuocGiao.GetValueOrDefault() - chungtu.FTongTien_ThucChi.GetValueOrDefault() : 0;
                                chungtu.FTongTienThieu = chungtu.FTongTien_TongDuToanDuocGiao.GetValueOrDefault() < chungtu.FTongTien_ThucChi.GetValueOrDefault() ? chungtu.FTongTien_ThucChi.GetValueOrDefault() - chungtu.FTongTien_TongDuToanDuocGiao.GetValueOrDefault() : 0;
                            }

                            _kinhPhiQuanLyService.Update(chungtu);
                        }
                    }
                    else
                    {
                        entity = _kinhPhiQuanLyService.FindById(Model.Id);
                        _mapper.Map(Model, entity);
                        entity.DNgaySua = DateTime.Now;
                        entity.SNguoiSua = _sessionService.Current.Principal;
                        entity.SDSLNS = LNSValue.LNS_9010003;
                    }
                }

                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhQtcNamKinhPhiQuanLyModel>(entity));

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> dataLNS)
        {
            if (dataLNS.Count > 0)
            {
                return string.Join(",", dataLNS.Where(n => n.IsSelected).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }

        private void CreateQtcKinhphiQuanlyVoucherDetail(BhQtcNamKinhPhiQuanLyModel bhQtcNamKinhPhiQuanLyModel)
        {
            QtcNamKinhPhiQuanLyCriteria criteria = new QtcNamKinhPhiQuanLyCriteria();
            criteria.ID = bhQtcNamKinhPhiQuanLyModel.Id;
            criteria.IDDonVi = bhQtcNamKinhPhiQuanLyModel.IID_DonVi;
            criteria.IDMaDonVi = bhQtcNamKinhPhiQuanLyModel.IID_MaDonVi;
            criteria.LoaiChungTu = bhQtcNamKinhPhiQuanLyModel.ILoaiTongHop;
            criteria.NamLamViec = bhQtcNamKinhPhiQuanLyModel.INamLamViec;
            criteria.SNguoiTao = bhQtcNamKinhPhiQuanLyModel.SNguoiTao;
            criteria.LstSoChungTu = string.Join(",", LstNamKinhPhiQuanLyModel.Select(x => x.Id.ToString()).ToList());
            _kinhPhiQuanLyServiceChiTietService.AddAggregate(criteria);
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

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (Model.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (Model.DNgayQuyetDinh == null)
            {
                messages.Add(Resources.AlertNgayQuyetDinhEmpty);
            }

            if (!IsAgregate)
            {
                var donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);
                if (donViSelected == null)
                {
                    messages.Add(string.Format(Resources.MsgCheckDonVi));
                }
                else
                {
                    var predicate = PredicateBuilder.True<BhQtcNamKinhPhiQuanLy>();
                    predicate = predicate.And(x => x.IID_DonVi == donViSelected.Id);
                    predicate = predicate.And(x => x.IID_MaDonVi == donViSelected.IIDMaDonVi);
                    predicate = predicate.And(x => x.INamLamViec == Model.INamLamViec);

                    var chungtu = _kinhPhiQuanLyService.FindByCondition(predicate).FirstOrDefault();
                    if (chungtu != null)
                    {
                        messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, donViSelected.TenDonViDisplay, Model.INamLamViec, ""));
                    }
                }
            }
            return string.Join(Environment.NewLine, messages);
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
