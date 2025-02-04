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
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac
{
    public class QuyetToanChiQuyKinhPhiKhacDialogViewModel : DialogViewModelBase<BhQtcQuyKPKModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhQtcQuyKPKService _quyKCBService;
        private readonly IBhQtcQuyKPKChiTietService _quyKCBChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ISysAuditLogService _log;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _dataLNSView;
        private SessionInfo _sessionInfo;
        #endregion

        #region Property
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(QuyetToanChiQuyKinhPhiKhacDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới quyết toán chi khác" : "Cập nhật quyết toán chi khác";
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public bool IsEnableValue => !IsAgregate;
        public bool IsAgregate { get; set; }
        public bool IsEdit => Model.Id.IsNullOrEmpty();
        private bool IsCapPhatToanDonVi;
        public DateTime? DNgayChungTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        private ICollectionView _listBudgetIndex;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
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
        public List<BhQtcQuyKPKModel> ListBhQtcChungTuModel { get; set; }

        private ComboboxItem _cbxQuaterSelected;
        public ComboboxItem CbxQuaterSelected
        {
            get => _cbxQuaterSelected;
            set
            {
                SetProperty(ref _cbxQuaterSelected, value);
                if (_cbxQuaterSelected != null)
                {
                    LoadDonVis();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuater;
        public ObservableCollection<ComboboxItem> CbxQuater
        {
            get => _cbxQuater;
            set => SetProperty(ref _cbxQuater, value);
        }

        public List<string> ListIdDonViHasCt { get; set; }

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
                if (_selectedDanhMucLoaiChi != null)
                {
                    LoadDonVis();
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

        #region Constructer
        public QuyetToanChiQuyKinhPhiKhacDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log,
            ILog logger,
            IDanhMucService danhMucService,
            INsDonViService nsDonViService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhQtcQuyKPKService BhQtcQuyKPKservice,
            IBhQtcQuyKPKChiTietService bhQtcQuyKPKChiTietService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _log = log;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _danhMucService = danhMucService;
            _nguoiDungDonViService = nsNguoiDungDonViService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _quyKCBService = BhQtcQuyKPKservice;
            _quyKCBChiTietService = bhQtcQuyKPKChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                LoadDonVis();
                LoadQuater();
                LoadDanhMucLoaiChi();
                LoadLNS();
                LoadData();
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
            base.LoadData(args);
            try
            {
                SearchNsDonVi = string.Empty;
                IconKind = Model.Id.IsNullOrEmpty() ? PackIconKind.PlaylistPlus : PackIconKind.NoteEditOutline;
                if (IsAgregate)
                {
                    if (Model.Id.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _quyKCBService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                        Model.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
                        Model.INamChungTu = _sessionInfo.YearOfWork;
                        Model.DNgayChungTu = DateTime.Now;
                        Model.DNgayQuyetDinh = DateTime.Now;
                        Model.DNgayTao = DateTime.Now;
                        Model.SNguoiTao = _sessionInfo.Principal;
                    }
                    else
                    {
                        DonViModel donViModel = DonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                        if (donViModel != null)
                        {
                            donViModel.Selected = true;
                        }

                        DNgayChungTu = Model.DNgayChungTu;
                        DNgayQuyetDinh = Model.DNgayQuyetDinh;
                    }
                }
                else
                {
                    if (Model.Id.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _quyKCBService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                        Model.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
                        Model.INamChungTu = _sessionInfo.YearOfWork;
                        Model.DNgayChungTu = DateTime.Now;
                        Model.DNgayQuyetDinh = DateTime.Now;
                        Model.DNgayTao = DateTime.Now;
                        Model.SNguoiTao = _sessionInfo.Principal;
                    }
                    else
                    {
                        SetCheckboxSelected(_dataLNS, Model.SDSLNS);
                        DonViModel donViModel = DonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                        if (donViModel != null)
                        {
                            donViModel.Selected = true;
                        }

                        DNgayChungTu = Model.DNgayChungTu;
                        DNgayQuyetDinh = Model.DNgayQuyetDinh;
                    }
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

        private void LoadQuater()
        {
            CbxQuater = new ObservableCollection<ComboboxItem>();
            CbxQuater.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Quý I" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Quý II" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Quý III" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "4", DisplayItem = "Quý IV" });

            if (Model.Id.IsNullOrEmpty())
            {
                CbxQuaterSelected = CbxQuater.ElementAt(0);
            }
            else
            {
                CbxQuaterSelected = CbxQuater.Where(x => x.ValueItem == Model.IQuyChungTu.ToString()).FirstOrDefault();
            }

            OnPropertyChanged(nameof(IsEdit));
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

                if (Model.Id.IsNullOrEmpty())
                {
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
                }
                else
                {
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.Where(x => x.Id == Model.IID_LoaiChi).FirstOrDefault();
                }
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
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

        private void LoadDonVis()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = KhcStatusType.ACTIVE;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var lstChungTu = _quyKCBService.FindIndex(yearOfWork);

            if (CbxQuaterSelected != null)
            {
                lstChungTu = lstChungTu.Where(x => x.IQuyChungTu == int.Parse(CbxQuaterSelected.ValueItem)).ToList();
            }

            if (SelectedDanhMucLoaiChi != null)
            {
                lstChungTu = lstChungTu.Where(x => x.IID_LoaiChi == SelectedDanhMucLoaiChi.Id).ToList();
            }
            ListIdDonViHasCt = lstChungTu.Select(item => item.IID_MaDonVi).Distinct().ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);

            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
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

            var listUnit = _nsDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
            var listDonViByUser = _nsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);
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

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            List<BhDmMucLucNganSach> listMLNS;
            listMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(yearOfWork, SelectedDanhMucLoaiChi.HiddenValue).ToList();
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
        #endregion

        #region Onsave
        public override void OnSave()
        {
            try
            {
                if (!Validate())
                {
                    return;
                }

                Model ??= new BhQtcQuyKPKModel();
                Model.INamChungTu = _sessionInfo.YearOfWork;
                Model.DNgayChungTu = DNgayChungTu;
                Model.DNgayQuyetDinh = DNgayQuyetDinh;
                var donVi = GetDonViOfCurrentUser();

                BhQtcQuyKPK entity;
                if (IsAgregate)
                {
                    var iNamChungTu = _sessionInfo.YearOfWork;

                    var lstSLNS = string.Join(",", ListBhQtcChungTuModel.Select(x => x.SDSLNS).Distinct().ToList());
                    if (Model.Id.IsNullOrEmpty())
                    {
                        var lstSoChungTu = string.Join(",", ListBhQtcChungTuModel.Select(x => x.SSoChungTu).ToList());
                        var predicateSummary = PredicateBuilder.True<BhQtcQuyKPK>();
                        predicateSummary = predicateSummary.And(x => x.INamChungTu == iNamChungTu && x.ILoaiTongHop == SettlementTypeLoaiChungTu.ChungTuTongHop
                                                                && x.IID_MaDonVi == donVi.IIDMaDonVi && x.IQuyChungTu == ListBhQtcChungTuModel.Select(s => s.IQuyChungTu).FirstOrDefault()
                                                                && x.IID_LoaiChi == ListBhQtcChungTuModel.Select(s => s.IID_LoaiChi).FirstOrDefault());
                        var lstQtcChungTu = _quyKCBService.FindByCondition(predicateSummary);
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
                                var qtcChungTu = _quyKCBService.FindById(idQtcChungTu);
                                _quyKCBService.Delete(idQtcChungTu);
                                var predicateSummaryDetail = PredicateBuilder.True<BhQtcQuyKPKChiTiet>();
                                predicateSummaryDetail = predicateSummaryDetail.And(x => x.Id == idQtcChungTu);
                                var qtcChungTuChiTiet = _quyKCBChiTietService.FindByCondition(predicateSummaryDetail);
                                _quyKCBChiTietService.RemoveRange(qtcChungTuChiTiet);
                            }
                            else
                            {
                                MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                                return;
                            }
                        }

                        // Add
                        entity = new BhQtcQuyKPK();
                        _mapper.Map(Model, entity);
                        entity.IID_DonVi = donVi.Id;
                        entity.IID_MaDonVi = donVi.IIDMaDonVi;
                        entity.IID_TongHopID = Guid.NewGuid();
                        entity.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTuTongHop;
                        entity.STongHop = lstSoChungTu;
                        entity.BIsKhoa = false;
                        entity.IID_LoaiChi = ListBhQtcChungTuModel[0].IID_LoaiChi;
                        entity.IQuyChungTu = ListBhQtcChungTuModel[0].IQuyChungTu;
                        entity.SDSLNS = lstSLNS;
                        entity.SNguoiTao = _sessionInfo.Principal;
                        entity.DNgayTao = DateTime.Now;
                        _quyKCBService.Add(entity);
                        CreateQtcKinhPhiKhacVoucherDetail(_mapper.Map<BhQtcQuyKPKModel>(entity));
                        QtcQuyKCBCriteria searchCondition = new QtcQuyKCBCriteria();
                        searchCondition.NamLamViec = entity.INamChungTu;
                        searchCondition.IDMaDonVi = entity.IID_MaDonVi;
                        searchCondition.IDDonVi = entity.IID_DonVi;
                        searchCondition.SNguoiTao = entity.SNguoiTao;
                        searchCondition.ID = entity.Id;
                        searchCondition.SLNS = entity.SDSLNS;
                        searchCondition.IDLoaiChi = entity.IID_LoaiChi;
                        searchCondition.DNgayChungTu = entity.DNgayChungTu;
                        searchCondition.IQuyChungTu = entity.IQuyChungTu;
                        searchCondition.LoaiChungTu = entity.ILoaiTongHop;
                        var lstQtcKinhphiQuanlyDetail =
                            _quyKCBChiTietService.FindChungTuChiTiet(searchCondition).ToList();
                        if (lstQtcKinhphiQuanlyDetail.Count > 0)
                        {
                            entity.FTongTien_DuToanNamTruocChuyenSang = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTien_DuToanNamTruocChuyenSang);
                            entity.FTongTien_DuToanGiaoNamNay = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTien_DuToanGiaoNamNay);
                            entity.FTongTien_TongDuToanDuocGiao = entity.FTongTien_DuToanNamTruocChuyenSang + entity.FTongTien_DuToanGiaoNamNay;
                            entity.FTongTienDeNghiQuyetToanQuyNay = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                            entity.FTongTienThucChi = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTienThucChi);
                            entity.FTongTienQuyetToanDaDuyet = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTienQuyetToanDaDuyet);
                            entity.FTongTienXacNhanQuyetToanQuyNay = lstQtcKinhphiQuanlyDetail.Sum(x => x.FTienXacNhanQuyetToanQuyNay);

                            _quyKCBService.Update(entity);
                        }
                    }
                    else
                    {
                        entity = new BhQtcQuyKPK();
                        entity = _quyKCBService.FindById(Model.Id);
                        _mapper.Map(Model, entity);
                        entity.SDSLNS = lstSLNS;
                        entity.DNgaySua = DateTime.Now;
                        entity.SNguoiSua = _sessionInfo.Principal;
                        _quyKCBService.Update(entity);
                    }
                }
                else
                {
                    if (Model.Id.IsNullOrEmpty())
                    {
                        var donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);

                        entity = new BhQtcQuyKPK();
                        _mapper.Map(Model, entity);
                        entity.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                        entity.IID_DonVi = donViSelected?.Id;
                        entity.IID_MaDonVi = donViSelected?.IIDMaDonVi;
                        entity.SDSLNS = SelectedDanhMucLoaiChi.HiddenValue;
                        if (donViSelected.Loai == LoaiDonVi.ROOT)
                        {
                            entity.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTuTongHop;
                            entity.IID_TongHopID = Guid.NewGuid();
                        }
                        else
                        {
                            entity.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                        }
                        entity.BIsKhoa = false;
                        entity.SNguoiTao = _sessionInfo.Principal;
                        entity.DNgayTao = DateTime.Now;
                        entity.INamChungTu = _sessionInfo.YearOfWork;
                        entity.IQuyChungTu = Convert.ToInt32(CbxQuaterSelected?.ValueItem);
                        entity.IID_LoaiChi = SelectedDanhMucLoaiChi.Id;
                        _quyKCBService.Add(entity);

                        if (entity.IQuyChungTu > SettlementTypeQuy.Quy)
                        {
                            _quyKCBChiTietService.CreateVoudcherForQuaterBefore(entity);
                        }
                    }
                    else
                    {
                        entity = new BhQtcQuyKPK();
                        entity = _quyKCBService.FindById(Model.Id);
                        _mapper.Map(Model, entity);
                        entity.SDSLNS = SelectedDanhMucLoaiChi.HiddenValue;
                        entity.IQuyChungTu = Convert.ToInt32(CbxQuaterSelected?.ValueItem);
                        entity.IID_LoaiChi = SelectedDanhMucLoaiChi.Id;
                        entity.DNgaySua = DateTime.Now;
                        entity.SNguoiSua = _sessionInfo.Principal;
                        _quyKCBService.Update(entity);
                    }
                }

                DialogHost.Close(SystemConstants.ROOT_DIALOG);

                DialogHost.CloseDialogCommand.Execute(null, null);

                // Show detail page when saved
                SavedAction?.Invoke(_mapper.Map<BhQtcQuyKPKModel>(entity));
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
                return string.Join(",", dataLNS.Where(n => n.IsSelected == true).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }


        private void CreateQtcKinhPhiKhacVoucherDetail(BhQtcQuyKPKModel bhQtcQuyKCBModel)
        {
            QtcQuyKCBCriteria criteria = new QtcQuyKCBCriteria();
            criteria.ID = bhQtcQuyKCBModel.Id;
            criteria.IDDonVi = bhQtcQuyKCBModel.IID_DonVi;
            criteria.IDMaDonVi = bhQtcQuyKCBModel.IID_MaDonVi;
            criteria.LoaiChungTu = bhQtcQuyKCBModel.ILoaiTongHop;
            criteria.NamLamViec = bhQtcQuyKCBModel.INamChungTu;
            criteria.SNguoiTao = bhQtcQuyKCBModel.SNguoiTao;
            criteria.LstSoChungTu = string.Join(",", ListBhQtcChungTuModel.Select(x => x.Id.ToString()).ToList());

            _quyKCBChiTietService.AddAggregate(criteria);
        }

        private bool Validate()
        {
            StringBuilder messageBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(Model.SSoChungTu))
            {
                messageBuilder.AppendFormat(Resources.AlertSoChungTuEmpty);
            }

            if (!DNgayChungTu.HasValue)
            {
                messageBuilder.AppendFormat(Resources.AlertNgayChungTuEmpty);
            }

            if (SelectedDanhMucLoaiChi == null)
            {
                messageBuilder.AppendFormat(Resources.MsgCheckLoaiKeHoachChi);
            }

            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return false;
            }

            if (!IsAgregate)
            {
                if (Model.Id.IsNullOrEmpty())
                {
                    var kinhPhiQuanly = _quyKCBService.FindIndex(_sessionInfo.YearOfWork);
                    var donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);
                    if (donViSelected == null)
                    {
                        MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                        return false;
                    }

                    if (kinhPhiQuanly.Any(x => x.IID_MaDonVi.Equals(donViSelected.IIDMaDonVi)
                                                && x.IID_DonVi.Equals(donViSelected.Id)
                                                && x.IQuyChungTu == int.Parse(CbxQuaterSelected.ValueItem) && x.IID_LoaiChi.Equals(SelectedDanhMucLoaiChi.Id)))
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementQuarterVoucher, donViSelected.IIDMaDonVi, CbxQuaterSelected.ValueItem, SelectedDanhMucLoaiChi.HiddenValue));
                        return false;
                    }
                }
            }
            return true;
        }

        private DonVi GetDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        #endregion

        #region Close
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
