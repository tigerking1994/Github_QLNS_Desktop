using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH
{
    public class DieuChinhDuToanBHXHDialogViewModel : DialogViewModelBase<BhDtcDcdToanChiModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhDtcDcdToanChiService _bhDtcDcdToanChiService;
        private readonly IBhDtcDcdToanChiChiTietService _bhDtcDcdTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhMucLoaiChiService;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _listBudgetIndex;
        private ICollectionView _dataLNSView;
        private SessionInfo _sessionInfo;
        #endregion

        #region Property
        public bool IsDetail { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public string SSoQuyetDinh { get; set; }
        public bool IsSummary { get; set; }

        public string AggregateLNS;
        public override Type ContentType => typeof(DieuChinhDuToanBHXHDialog);
        //public override string Name => Model.IID_BH_DTC.IsNullOrEmpty() ? "THÊM MỚI" : "CẬP NHẬT";
        //public override string Description => Model.IID_BH_DTC.IsNullOrEmpty() ? "Tạo mới điều chỉnh dự toán" : "Cập nhật điều chỉnh dự toán";
        public string HeaderKehoachThucHienNam => "Tổng kế hoạch năm " + (_sessionService.Current.YearOfWork);
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public bool IsSaveEnable => _dataLNS != null && _dataLNS.Any(x => x.IsChecked) && NsDonViModelItems.Any(x => x.IsSelected);
        private bool _isAgregate;
        public bool IsAgregate
        {
            get => _isAgregate;
            set => SetProperty(ref _isAgregate, value);
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
        public List<string> ListIdDonViHasCt { get; set; }

        private bool NsDonViFilter(object obj)
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

        public bool IsEdit => Model.IID_BH_DTC.IsNullOrEmpty();

        public List<BhDtcDcdToanChiModel> ListIdsBhDtcDcdToanChiModel { get; set; }

        private BhDtcDcdToanChiModel _bhBhDtcDcdToanChiModel;
        public BhDtcDcdToanChiModel BhDtcDcdToanChiModel
        {
            get => _bhBhDtcDcdToanChiModel;
            set
            {
                SetProperty(ref _bhBhDtcDcdToanChiModel, value);
                OnPropertyChanged(nameof(IsSaveEnable));
            }
        }

        private ObservableCollection<DonViModel> _nsDonViModelItems;
        public ObservableCollection<DonViModel> NsDonViModelItems
        {
            get => _nsDonViModelItems;
            set
            {
                SetProperty(ref _nsDonViModelItems, value);
                OnPropertyChanged();
            }
        }
        public string SelectedCountNsDonVi
        {
            get
            {
                var totalCount = NsDonViModelItems != null ? NsDonViModelItems.Count() : 0;
                var totalSelected = NsDonViModelItems != null ? NsDonViModelItems.Count(item => item.Selected) : 0;
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
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
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

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (_selectedDanhMucLoaiChi != null)
                {
                    LoadLNS();
                    LoadBhDonVis();
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

        #region Constructor
        public DieuChinhDuToanBHXHDialogViewModel(IMapper mapper,
            ILog logger,
           ISessionService sessionService,
           INsDonViService nsDonViService,
           IBhDtcDcdToanChiService bhDtcDcdToanChiService,
           IBhDmMucLucNganSachService bhDmMucLucNganSachService,
           IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
           IBhDtcDcdToanChiChiTietService bhDtcDcdTietService)
        {
            _mapper = mapper;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _sessionService = sessionService;
            _bhDtcDcdToanChiService = bhDtcDcdToanChiService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhMucLoaiChiService = bhDanhMucLoaiChiService;
            _bhDtcDcdTietService = bhDtcDcdTietService;
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                LoadBhDonVis();
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
        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.SMaLoaiChi,
                    HiddenValue = n.SLNS.ToString(),
                    Id = n.Id
                }));

                if (!Model.IID_LoaiCap.IsNullOrEmpty())
                {
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.Where(x => x.Id == Model.IID_LoaiCap).FirstOrDefault();
                }
                else
                {
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
                }
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private void LoadBhDonVis()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = KhcStatusType.ACTIVE;
            int yearOfBudget = _sessionService.Current.YearOfBudget;
            int budgetSource = _sessionService.Current.Budget;
            var lstChungTu = _bhDtcDcdToanChiService.FindIndex(yearOfWork);

            if (SelectedDanhMucLoaiChi != null)
            {
                lstChungTu = lstChungTu.Where(x => x.IID_LoaiCap == SelectedDanhMucLoaiChi.Id);
            }

            ListIdDonViHasCt = lstChungTu.Select(item => item.IID_MaDonVi).ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);


            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
                if (Model.IID_BH_DTC == Guid.Empty)
                {
                    predicate = predicate.And(x => ListIdDonViHasCt.All(y => y == x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
                    predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                }
            }
            else
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
                if (Model.IID_BH_DTC == Guid.Empty)
                {
                    predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
                    predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                }
            }

            var listUnit = _nsDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
            var listDonViByUser = _nsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);
            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
            if (!string.IsNullOrEmpty(Model.IID_MaDonVi))
            {
                NsDonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
            _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                ListSortDirection.Ascending));
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                ListSortDirection.Ascending));
            _nsDonViModelsView.Filter = NsDonViFilter;
            foreach (var model in NsDonViModelItems)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DonViModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedCountNsDonVi));
                        if (model.Selected && !IsAgregate)
                        {
                            OnPropertyChanged(nameof(IsSaveEnable));
                        }
                    }
                };
            }

            OnPropertyChanged(nameof(IsSaveEnable));
            //OnPropertyChanged(nameof(IsEdit));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            try
            {
                SearchNsDonVi = string.Empty;

                if (IsAgregate)
                {
                    if (Model.IID_BH_DTC.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _bhDtcDcdToanChiService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                        IconKind = PackIconKind.PlaylistPlus;
                        Name = "TỔNG HỢP";
                        Description = "Tổng hợp điều chỉnh dự toán chi";
                        SSoChungTu = "DC-" + soChungTuIndex.ToString("D3");
                        INamChungTu = _sessionService.Current.YearOfWork;
                        DNgayChungTu = DateTime.Now;
                        DNgayQuyetDinh = DateTime.Now;
                        SMoTa = string.Empty;
                    }
                    else
                    {
                        BhDtcDcdToanChi entity = _bhDtcDcdToanChiService.FindById(Model.IID_BH_DTC);
                        Model = _mapper.Map<BhDtcDcdToanChiModel>(entity);

                        IconKind = PackIconKind.NoteEditOutline;
                        Name = "CẬP NHẬT";
                        Description = "Cập nhật điều chỉnh dự toán chi";

                        DonViModel donViSelected = NsDonViModelItems.Where(n => n.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                        if (donViSelected != null)
                        {
                            donViSelected.Selected = true;
                        }
                        SSoChungTu = Model.SSoChungTu;
                        DNgayChungTu = Model.DNgayChungTu;
                        DNgayQuyetDinh = Model.DNgayQuyetDinh;
                        INamChungTu = Model.INamLamViec;
                        SSoQuyetDinh = Model.SSoQuyetDinh;
                        SMoTa = Model.SMoTa;
                        OnPropertyChanged(nameof(NsDonViModelItems));
                    }
                }
                else
                {
                    if (Model.IID_BH_DTC.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _bhDtcDcdToanChiService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);

                        IconKind = PackIconKind.PlaylistPlus;
                        Name = "THÊM MỚI";
                        Description = "Thêm mới điều chỉnh dự toán chi";
                        SSoChungTu = "DC-" + soChungTuIndex.ToString("D3");
                        DNgayQuyetDinh = DateTime.Now;
                        DNgayChungTu = DateTime.Now;
                        INamChungTu = DateTime.Now.Year;
                        SMoTa = string.Empty;
                    }
                    else
                    {
                        BhDtcDcdToanChi entity = _bhDtcDcdToanChiService.FindById(Model.IID_BH_DTC);
                        Model = _mapper.Map<BhDtcDcdToanChiModel>(entity);

                        IconKind = PackIconKind.NoteEditOutline;
                        Name = "CẬP NHẬT";
                        Description = "Cập nhật điều chỉnh dự toán chi";

                        DonViModel donViSelected = NsDonViModelItems.Where(n => n.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                        if (donViSelected != null)
                        {
                            donViSelected.Selected = true;
                        }
                        SetCheckboxSelected(_dataLNS, Model.SLNS);
                        SSoChungTu = Model.SSoChungTu;
                        DNgayChungTu = Model.DNgayChungTu;
                        DNgayQuyetDinh = Model.DNgayQuyetDinh;
                        INamChungTu = Model.INamLamViec;
                        SSoQuyetDinh = Model.SSoQuyetDinh;
                        SMoTa = Model.SMoTa;
                        OnPropertyChanged(nameof(NsDonViModelItems));
                        OnPropertyChanged(nameof(_dataLNS));
                    }
                }

                OnPropertyChanged(nameof(IsEnabled));
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
                            OnPropertyChanged(nameof(IsSaveEnable));
                        }
                    };
                }
            }

            OnPropertyChanged(nameof(IsSaveEnable));
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

        #region On save
        public override void OnSave()
        {
            base.OnSave();
            string message = GetMessageValidate();
            DonVi donVi = GetNsDonViOfCurrentUser();
            BhDtcDcdToanChi entity;
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
                return;
            }

            if (Model.Id == Guid.Empty)
            {
                // Add
                entity = new BhDtcDcdToanChi();

                Model.SNguoiTao = _sessionService.Current.Principal;
                Model.INamLamViec = _sessionService.Current.YearOfWork;
                Model.SMoTa = SMoTa?.Trim() ?? string.Empty;
                Model.DNgayTao = DateTime.Now;
                Model.SSoQuyetDinh = SSoQuyetDinh?.Trim() ?? string.Empty;
                Model.BIsKhoa = false;
                Model.DNgayQuyetDinh = DNgayQuyetDinh;
                Model.DNgayChungTu = DNgayChungTu;
                Model.SSoChungTu = SSoChungTu;

                //ở trạng thái tổng hợp

                if (IsAgregate)
                {
                    Guid iDLoaiCap = ListIdsBhDtcDcdToanChiModel[0].IID_LoaiCap;
                    var namLamViec = _sessionInfo.YearOfWork;
                    var listSoChungTuTongHopString = string.Join(",", ListIdsBhDtcDcdToanChiModel.Select(x => x.SSoChungTu).ToList());
                    var predicateSummary = PredicateBuilder.True<BhDtcDcdToanChi>();
                    predicateSummary = predicateSummary.And(x => x.IID_LoaiCap == iDLoaiCap);
                    predicateSummary = predicateSummary.And(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.IID_MaDonVi == donVi.IIDMaDonVi);
                    var lstChungTu = _bhDtcDcdToanChiService.FindByCondition(predicateSummary);
                    if (lstChungTu.Any())
                    {
                        var firstChungTuSummary = lstChungTu.FirstOrDefault();
                        if (!firstChungTuSummary.BIsKhoa)
                        {
                            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                            if (messageBoxResult == MessageBoxResult.No)
                            {
                                return;
                            }

                            var idChungTuSummary = firstChungTuSummary.Id;
                            var chungTuSummary = _bhDtcDcdToanChiService.FindById(idChungTuSummary);
                            _bhDtcDcdToanChiService.Delete(idChungTuSummary);
                            var predicateSummaryDetail = PredicateBuilder.True<BhDtcDcdToanChiChiTiet>();
                            predicateSummaryDetail = predicateSummaryDetail.And(x => x.Id == idChungTuSummary);
                            var chungTuChiTiet = _bhDtcDcdTietService.FindByCondition(predicateSummaryDetail);
                            _bhDtcDcdTietService.RemoveRange(chungTuChiTiet);
                        }
                        else
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                            return;
                        }
                    }
                    _mapper.Map(Model, entity);
                    entity.STongHop = string.Join(",", ListIdsBhDtcDcdToanChiModel.Select(x => x.SSoChungTu).OrderBy(x => x).ToList());
                    entity.IID_MaDonVi = donVi.IIDMaDonVi;
                    entity.SLNS = string.Join(",", ListIdsBhDtcDcdToanChiModel.Select(x => x.SLNS).Distinct().OrderBy(x => x).ToList().Distinct());
                    entity.IIdDonViId = donVi.Id;
                    entity.ILoaiTongHop = DtDcDtBhxhLoaiChungTu.BhxhChungTuTongHop;
                    entity.IID_LoaiCap = iDLoaiCap;
                    entity.SMaLoaiChi = ListIdsBhDtcDcdToanChiModel[0].SMaLoaiChi;
                    entity.Id = Guid.NewGuid();
                    entity.IID_TongHopID = Guid.NewGuid();
                }
                else
                {
                    if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002)
                    {
                        Model.SLNS = LNSValue.LNS_9010001_9010002;
                    }
                    else
                    {
                        Model.SLNS = SelectedDanhMucLoaiChi.HiddenValue;
                    }
                    _mapper.Map(Model, entity);
                    var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
                    if (donViSelected == null)
                    {
                        MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                        return;
                    }

                    entity.IID_MaDonVi = donViSelected.IIDMaDonVi;
                    entity.IIdDonViId = donViSelected.Id;

                    if (donViSelected.Loai == LoaiDonVi.ROOT)
                    {
                        entity.ILoaiTongHop = DtDcDtBhxhLoaiChungTu.BhxhChungTuTongHop;
                        entity.IID_TongHopID = Guid.NewGuid();
                    }
                    else
                    {
                        entity.ILoaiTongHop = DtDcDtBhxhLoaiChungTu.BhxhChungTu;
                    }

                    entity.IID_LoaiCap = SelectedDanhMucLoaiChi.Id;
                    entity.SMaLoaiChi = SelectedDanhMucLoaiChi.ValueItem;
                }

                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _bhDtcDcdToanChiService.Add(entity);

                // tạo chứng từ chi tiết khi tổng hợp
                if (IsAgregate)
                {
                    CreateDieuChinhDuToanDetail(_mapper.Map<BhDtcDcdToanChiModel>(entity));
                    BhDtcDcdToanChiChiTietCriteria searchCondition = new BhDtcDcdToanChiChiTietCriteria();
                    searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
                    searchCondition.IdDonVi = entity.IID_MaDonVi;
                    searchCondition.ILoaiDanhMucChi = entity.IID_LoaiCap;
                    searchCondition.DtcDcdToanChiId = entity.Id;
                    searchCondition.LNS = entity.SLNS;
                    searchCondition.NgayChungTu = entity.DNgayChungTu;
                    searchCondition.ILoaiTongHop = entity.ILoaiTongHop;
                    searchCondition.MaLoaiChi = entity.SMaLoaiChi;
                    var listDieuChinhDuToanChiTiet =
                        _bhDtcDcdTietService.FindByConditionForChildUnit(searchCondition).ToList();

                    var lstNhanPhanBo = _bhDtcDcdTietService.FindNPBChiChiTiet(searchCondition);

                    if (listDieuChinhDuToanChiTiet.Count > 0)
                    {
                        //listDieuChinhDuToanChiTiet= listDieuChinhDuToanChiTiet.Where(x=>string.IsNullOrEmpty(x.SM)).ToList();
                        entity.FTienDuToanDuocGiao = listDieuChinhDuToanChiTiet.Sum(item => item.FTienDuToanDuocGiao);

                        entity.FTienThucHien06ThangDauNam = listDieuChinhDuToanChiTiet.Sum(item => item.FTienThucHien06ThangDauNam);
                        entity.FTienUocThucHien06ThangCuoiNam = listDieuChinhDuToanChiTiet.Sum(item => item.FTienUocThucHien06ThangCuoiNam);
                        entity.FTienUocThucHienCaNam = entity.FTienThucHien06ThangDauNam + entity.FTienUocThucHien06ThangCuoiNam;
                        entity.FTienSoSanhTang = (entity.FTienUocThucHienCaNam - entity.FTienDuToanDuocGiao) > 0 ? entity.FTienUocThucHienCaNam - entity.FTienDuToanDuocGiao : 0;
                        entity.FTienSoSanhGiam = (entity.FTienDuToanDuocGiao - entity.FTienUocThucHienCaNam) > 0 ? entity.FTienDuToanDuocGiao - entity.FTienUocThucHienCaNam : 0;
                        _bhDtcDcdToanChiService.Update(entity);
                    }
                }
            }
            else
            {
                // Update
                entity = _bhDtcDcdToanChiService.FindById(Model.Id);
                _mapper.Map(Model, entity);
                entity.SSoQuyetDinh = SSoQuyetDinh?.Trim() ?? string.Empty; ;
                entity.DNgayChungTu = DNgayChungTu;
                entity.DNgayQuyetDinh = DNgayQuyetDinh;
                entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                entity.IIdDonViId = donVi.Id;
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _bhDtcDcdToanChiService.Update(entity);
            }

            SSoQuyetDinh = string.Empty;
            SMoTa = string.Empty;
            DialogHost.CloseDialogCommand.Execute(null, null);

            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<BhDtcDcdToanChiModel>(entity));
        }

        private void CreateDieuChinhDuToanDetail(BhDtcDcdToanChiModel bhDtcDcdToanChiModel)
        {
            BhDtcDcdToanChiChiTietCriteria criteria = new BhDtcDcdToanChiChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsBhDtcDcdToanChiModel.Select(x => x.IID_BH_DTC.ToString()).ToList()),
                IdChungTu = bhDtcDcdToanChiModel.Id.ToString(),
                IdDonVi = bhDtcDcdToanChiModel.IID_MaDonVi,
                TenDonVi = bhDtcDcdToanChiModel.STenDonVi,
                ILoaiDanhMucChi = bhDtcDcdToanChiModel.IID_LoaiCap,
                LoaiChungTu = bhDtcDcdToanChiModel.ILoaiTongHop.GetValueOrDefault(2),
                NamLamViec = bhDtcDcdToanChiModel.INamLamViec.GetValueOrDefault(_sessionService.Current.YearOfWork),
                NguoiTao = bhDtcDcdToanChiModel.SNguoiTao,
            };
            _bhDtcDcdTietService.AddAggregate(criteria);
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (DNgayChungTu == DateTime.MinValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (!IsAgregate)
            {
                var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
                if (donViSelected == null)
                {
                    messages.Add(Resources.MsgCheckDonVi);
                }
            }

            return string.Join(Environment.NewLine, messages);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        #endregion

        #region On close
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
