using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Converters;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu
{
    public class DieuChinhDuToanThuDialogViewModel : DialogViewModelBase<BhDcDuToanThuModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhDcDuToanThuService _bhDcDuToanThuService;
        private readonly IBhDcDuToanThuChiTietService _bhDcDttChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _listBudgetIndex;
        private SessionInfo _sessionInfo;
        private bool IsInitEdit;
        public bool IsDetail { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public int? INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public string SSoQuyetDinh { get; set; }
        public bool IsSummary { get; set; }

        public string AggregateLNS;
        public override Type ContentType => typeof(DieuChinhDuToanThuDialog);

        public string HeaderKehoachThucHienNam => "Tổng kế hoạch năm " + (_sessionService.Current.YearOfWork);

        private bool _isAgregate;
        public bool IsAgregate
        {
            get => _isAgregate;
            set => SetProperty(ref _isAgregate, value);
        }
        public bool IsEditable => Model.Id.IsNullOrEmpty();

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

        public bool IsEdit => Model.Id == Guid.Empty && !IsSummary;

        public List<BhDcDuToanThuModel> ListIdsBhDcDuToanThuModel { get; set; }

        private BhDcDuToanThuModel _bhDcDuToanThuModel;
        public BhDcDuToanThuModel BhDcDuToanThuModel
        {
            get => _bhDcDuToanThuModel;
            set
            {
                SetProperty(ref _bhDcDuToanThuModel, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEdit));
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
        private ObservableCollection<BhDmMucLucNganSachModel> _budgetIndexes;
        public ObservableCollection<BhDmMucLucNganSachModel> BudgetIndexes
        {
            get => _budgetIndexes;
            set => SetProperty(ref _budgetIndexes, value);
        }

        private string _searchBudgetIndexText;
        public string SearchBudgetIndexText
        {
            set
            {
                if (SetProperty(ref _searchBudgetIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                    OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => false;
            //get => BudgetIndexes.Count > 0 ? BudgetIndexes.All(x => x.IsSelected) : false;
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (BhDmMucLucNganSachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }

        public DieuChinhDuToanThuDialogViewModel(IMapper mapper,
            ILog logger,
           ISessionService sessionService,
           INsDonViService nsDonViService,
           IBhDcDuToanThuService bhDcDuToanThuService,
           IBhDmMucLucNganSachService bhDmMucLucNganSachService,
           IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
           IBhDcDuToanThuChiTietService bhDtcDcdTietService)
        {
            _mapper = mapper;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _sessionService = sessionService;
            _bhDcDuToanThuService = bhDcDuToanThuService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDcDttChiTietService = bhDtcDcdTietService;
        }

        public override void Init()
        {
            try
            {
                LoadNsDonVis();
                LoadBudgetIndexes();
                LoadData();
                IsSelectAllBudgetIndex = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNsDonVis()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = KhcStatusType.ACTIVE;
            int yearOfBudget = _sessionService.Current.YearOfBudget;
            int budgetSource = _sessionService.Current.Budget;
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);
            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
            }
            else
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
            }
            var lstCurrentUnitBh = _bhDcDuToanThuService.FindCurrentUnits(yearOfWork);
            var listUnit = _nsDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
            var listDonViByUser = _nsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT))
                .Where(y => !lstCurrentUnitBh.Contains(y.IIDMaDonVi)).Select(x => x.IIDMaDonVi);
            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
            if (!string.IsNullOrEmpty(Model.IIDMaDonVi))
            {
                NsDonViModelItems.Where(x => x.IIDMaDonVi == Model.IIDMaDonVi).Select(x =>
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
                            //if (!IsInitEdit)
                            //    LoadBudgetIndexes(model.IIDMaDonVi);
                        }
                    }
                };
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            try
            {
                SearchNsDonVi = string.Empty;

                if (IsAgregate)
                {
                    if (Model.IIDDttDieuChinh.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _bhDcDuToanThuService.GetSoChungTuIndexByCondition();
                        IconKind = PackIconKind.PlaylistPlus;
                        Name = "THÊM MỚI";
                        Description = "Thêm mới điều chỉnh dự toán thu BHXH, BHYT, BHTN";
                        SSoChungTu = "DC-" + soChungTuIndex.ToString("D3");
                        INamChungTu = _sessionService.Current.YearOfWork;
                        DNgayChungTu = DateTime.Now;
                        SMoTa = string.Empty;
                        //LoadBudgetIndexes(string.Empty);
                    }
                    else
                    {
                        BhDcDuToanThu entity = _bhDcDuToanThuService.FindById(Model.IIDDttDieuChinh);
                        Model = _mapper.Map<BhDcDuToanThuModel>(entity);
                        //LoadBudgetIndexes(Model.IIDMaDonVi);
                        IconKind = PackIconKind.NoteEditOutline;
                        Name = "CẬP NHẬT";
                        Description = "Cập nhật điều chỉnh dự toán thu BHXH, BHYT, BHTN";

                        DonViModel donViSelected = NsDonViModelItems.Where(n => n.IIDMaDonVi == Model.IIDMaDonVi).FirstOrDefault();
                        if (donViSelected != null)
                        {
                            donViSelected.Selected = true;
                        }
                        SSoChungTu = Model.SSoChungTu;
                        DNgayChungTu = Model.DNgayChungTu;
                        INamChungTu = Model.INamLamViec;
                        SMoTa = Model.SMoTa;
                        OnPropertyChanged(nameof(NsDonViModelItems));
                    }
                }
                else
                {
                    if (Model.IIDDttDieuChinh.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _bhDcDuToanThuService.GetSoChungTuIndexByCondition();
                        //LoadBudgetIndexes(string.Empty);
                        IconKind = PackIconKind.PlaylistPlus;
                        Name = "THÊM MỚI";
                        Description = "Thêm mới điều chỉnh dự toán thu BHXH, BHYT, BHTN";
                        SSoChungTu = "DC-" + soChungTuIndex.ToString("D3");
                        DNgayChungTu = DateTime.Now;
                        INamChungTu = DateTime.Now.Year;
                        SMoTa = string.Empty;
                    }
                    else
                    {
                        BhDcDuToanThu entity = _bhDcDuToanThuService.FindById(Model.IIDDttDieuChinh);
                        Model = _mapper.Map<BhDcDuToanThuModel>(entity);
                        //LoadBudgetIndexes(Model.IIDMaDonVi);
                        IconKind = PackIconKind.NoteEditOutline;
                        Name = "CẬP NHẬT";
                        Description = "Cập nhật điều chỉnh dự toán thu BHXH, BHYT, BHTN";
                        var donvi = _nsDonViService.FindByIdDonVi(Model.IIDMaDonVi, Model.INamLamViec ?? 0);
                        //DonViModel donViSelected = NsDonViModelItems.Where(n => n.IIDMaDonVi == Model.IIDMaDonVi).FirstOrDefault();
                        if (donvi != null)
                        {
                            NsDonViModelItems.Insert(0, _mapper.Map<DonViModel>(donvi));
                            NsDonViModelItems.FirstOrDefault(x => x.IIDMaDonVi.Equals(donvi.IIDMaDonVi)).Selected = true;
                            NsDonViModelItems.OrderBy(x => x.IIDMaDonVi);
                        }
                        if (!string.IsNullOrEmpty(Model.SLNS))
                        {
                            SetCheckboxSelected(BudgetIndexes, Model.SLNS);
                        }

                        SSoChungTu = Model.SSoChungTu;
                        DNgayChungTu = Model.DNgayChungTu;
                        INamChungTu = Model.INamLamViec;
                        SMoTa = Model.SMoTa;
                        OnPropertyChanged(nameof(NsDonViModelItems));
                    }
                }

                //Model.PropertyChanged += (sender, args) =>
                //{
                //    if (args.PropertyName == nameof(BhDcDuToanThuModel.DNgayChungTu))
                //    {
                //        LoadBudgetIndexes(string.Empty);
                //    }
                //};
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadBudgetIndexes()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;

            var listMLNS = _bhDmMucLucNganSachService.GetListBhytMucLucNs(yearOfWork, BhxhMLNS.KHT_BHXH_BHYT_BHTN).ToList();
            BudgetIndexes = new ObservableCollection<BhDmMucLucNganSachModel>();
            BudgetIndexes = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(listMLNS);

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;

            if (BudgetIndexes != null && BudgetIndexes.Count > 0)
            {
                foreach (var model in BudgetIndexes)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                        {
                            foreach (var item in BudgetIndexes)
                            {
                                if (item.IIDMLNSCha == model.IIDMLNS)
                                {
                                    item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        }
                    };
                }
            }
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchBudgetIndexText))
                result = item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void OnSave()
        {
            base.OnSave();
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
                return;
            }

            if (Model == null) Model = new BhDcDuToanThuModel();
            Model.SLNS = string.Join(",", BudgetIndexes.Where(n => n.IsSelected).Select(n => n.SLNS).Distinct().ToList());
            Model.INamLamViec = _sessionService.Current.YearOfWork;
            Model.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTu;
            Model.SMoTa = SMoTa?.Trim() ?? string.Empty;
            Model.SNguoiTao = _sessionService.Current.Principal;
            Model.DNgayTao = DateTime.Now;
            Model.BIsKhoa = false;
            Model.DNgayChungTu = DNgayChungTu;
            Model.SSoChungTu = SSoChungTu;
            var rootUnit = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).FirstOrDefault(x => x.Loai == LoaiDonVi.ROOT);
            BhDcDuToanThu entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                entity = new BhDcDuToanThu();
                DonVi donVi = new DonVi();
                //ở trạng thái tổng hợp
                _mapper.Map(Model, entity);

                if (IsAgregate)
                {
                    var currentDV = GetNsDonViOfCurrentUser();
                    entity.STongHop = string.Join(",", ListIdsBhDcDuToanThuModel.Select(x => x.SSoChungTu).OrderBy(x => x).ToList());
                    var listVoucherSummary = _bhDcDuToanThuService.FindByCondition(_sessionService.Current.YearOfWork, currentDV.IIDMaDonVi, BhxhLoaiChungTu.BhxhChungTuTongHop);

                    if (listVoucherSummary.Any())
                    {
                        var firstKhtChungTuSummary = listVoucherSummary.FirstOrDefault();
                        if (!firstKhtChungTuSummary.BIsKhoa)
                        {
                            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                            if (messageBoxResult == MessageBoxResult.No)
                            {
                                return;
                            }
                            var idChungTuSummary = firstKhtChungTuSummary.Id;
                            var chungTuSummary = _bhDcDuToanThuService.FindById(idChungTuSummary);
                            UpdateChungTuDaTongHop(chungTuSummary);
                            _bhDcDuToanThuService.Delete(chungTuSummary);
                            var predicateSummaryDetail = PredicateBuilder.True<BhDcDuToanThuChiTiet>();
                            predicateSummaryDetail = predicateSummaryDetail.And(x => x.IIDDttDieuChinh == idChungTuSummary);
                            var chungTuChiTiets = _bhDcDttChiTietService.FindByCondition(predicateSummaryDetail);
                            _bhDcDttChiTietService.RemoveRange(chungTuChiTiets);
                        }
                        else
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                            return;
                        }

                    }

                    
                    entity.IIDMaDonVi = rootUnit.IIDMaDonVi;
                    entity.SLNS = string.Join(",", ListIdsBhDcDuToanThuModel.Select(x => x.SLNS).OrderBy(x => x).ToList().Distinct());
                    entity.IIdDonViId = rootUnit.Id;
                    entity.ILoaiTongHop = DtDcDtBhxhLoaiChungTu.BhxhChungTuTongHop;
                    entity.Id = Guid.NewGuid();
                    entity.IIDTongHopID = Guid.NewGuid();

                }
                else
                {
                    var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
                    if (donViSelected == null)
                    {
                        MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                        return;
                    }

                    entity.ILoaiTongHop = donViSelected.Loai == LoaiDonVi.ROOT ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                    entity.IIDMaDonVi = donViSelected.IIDMaDonVi;
                    entity.IIdDonViId = donViSelected.Id;
                }

                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _bhDcDuToanThuService.Add(entity);

                // tạo chứng từ chi tiết khi tổng hợp
                if (IsAgregate)
                {
                    CreateDieuChinhDuToanDetail(_mapper.Map<BhDcDuToanThuModel>(entity));
                    var listDieuChinhDuToanChiTiet =
                        _bhDcDttChiTietService.FindByCondition(item => item.IIDDttDieuChinh.Equals(entity.Id)).ToList();

                    if (listDieuChinhDuToanChiTiet.Count > 0)
                    {
                        foreach(var item in listDieuChinhDuToanChiTiet)
                        {
                            item.IIdMaDonVi = rootUnit.IIDMaDonVi;
                            item.INamLamViec = _sessionService.Current.YearOfWork;
                            _bhDcDttChiTietService.Update(item);
                        }
                        entity.FThuBHXHNLD = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNLD);
                        entity.FThuBHXHNSD = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNSD);
                        entity.FThuBHYTNLD = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNLD);
                        entity.FThuBHYTNSD = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNSD);
                        entity.FThuBHTNNLD = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNLD);
                        entity.FThuBHTNNSD = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNSD);
                        entity.FThuBHXHNLDQTDauNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNLDQTDauNam);
                        entity.FThuBHXHNSDQTDauNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNSDQTDauNam);
                        entity.FThuBHYTNLDQTDauNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNLDQTDauNam);
                        entity.FThuBHYTNSDQTDauNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNSDQTDauNam);
                        entity.FThuBHTNNLDQTDauNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNLDQTDauNam);
                        entity.FThuBHTNNSDQTDauNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNSDQTDauNam);
                        entity.FThuBHXHNLDQTCuoiNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNLDQTCuoiNam);
                        entity.FThuBHXHNSDQTCuoiNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNSDQTCuoiNam);
                        entity.FThuBHYTNLDQTCuoiNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNLDQTCuoiNam);
                        entity.FThuBHYTNSDQTCuoiNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNSDQTCuoiNam);
                        entity.FThuBHTNNLDQTCuoiNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNLDQTCuoiNam);
                        entity.FThuBHTNNSDQTCuoiNam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNSDQTCuoiNam);
                        entity.FTongThuBHXHNLD = listDieuChinhDuToanChiTiet.Sum(item => item.FTongThuBHXHNLD);
                        entity.FTongThuBHXHNSD = listDieuChinhDuToanChiTiet.Sum(item => item.FTongThuBHXHNSD);
                        entity.FTongThuBHYTNLD = listDieuChinhDuToanChiTiet.Sum(item => item.FTongThuBHYTNLD);
                        entity.FTongThuBHYTNSD = listDieuChinhDuToanChiTiet.Sum(item => item.FTongThuBHYTNSD);
                        entity.FTongThuBHTNNLD = listDieuChinhDuToanChiTiet.Sum(item => item.FTongThuBHTNNLD);
                        entity.FTongThuBHTNNSD = listDieuChinhDuToanChiTiet.Sum(item => item.FTongThuBHTNNSD);
                        entity.FTongCong = listDieuChinhDuToanChiTiet.Sum(item => item.FTongCong);
                        entity.FThuBHXHNLDTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNLDTang);
                        entity.FThuBHXHNSDTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNSDTang);
                        entity.FThuBHXHTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHTang);
                        entity.FThuBHYTNLDTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNLDTang);
                        entity.FThuBHYTNSDTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNSDTang);
                        entity.FThuBHYTTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTTang);
                        entity.FThuBHTNNLDTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNLDTang);
                        entity.FThuBHTNNSDTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNSDTang);
                        entity.FThuBHTNTang = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNTang);
                        entity.FThuBHXHNLDGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNLDGiam);
                        entity.FThuBHXHNSDGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHNSDGiam);
                        entity.FThuBHXHGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHXHGiam);
                        entity.FThuBHYTNLDGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNLDGiam);
                        entity.FThuBHYTNSDGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTNSDGiam);
                        entity.FThuBHYTGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHYTGiam);
                        entity.FThuBHTNNLDGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNLDGiam);
                        entity.FThuBHTNNSDGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNNSDGiam);
                        entity.FThuBHTNGiam = listDieuChinhDuToanChiTiet.Sum(item => item.FThuBHTNGiam);
                        _bhDcDuToanThuService.Update(entity);
                    }
                }
            }
            else
            {
                // Update
                entity = _bhDcDuToanThuService.FindById(Model.Id);
                _mapper.Map(Model, entity);

                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _bhDcDuToanThuService.Update(entity);
            }

            SSoQuyetDinh = string.Empty;
            SMoTa = string.Empty;
            DialogHost.CloseDialogCommand.Execute(null, null);

            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<BhDcDuToanThuModel>(entity));
        }

        private void CreateDieuChinhDuToanDetail(BhDcDuToanThuModel BhDcDuToanThuModel)
        {
            BhDcDuToanThuChiTietCriteria criteria = new BhDcDuToanThuChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsBhDcDuToanThuModel.Select(x => x.IIDDttDieuChinh.ToString()).ToList()),
                IdChungTu = BhDcDuToanThuModel.Id.ToString(),
                IdDonVi = BhDcDuToanThuModel.IIDMaDonVi,
                TenDonVi = BhDcDuToanThuModel.STenDonVi,
                LoaiChungTu = BhDcDuToanThuModel.ILoaiTongHop.GetValueOrDefault(2),
                NamLamViec = BhDcDuToanThuModel.INamLamViec.GetValueOrDefault(_sessionService.Current.YearOfWork),
                NguoiTao = BhDcDuToanThuModel.SNguoiTao,
            };
            _bhDcDttChiTietService.AddAggregate(criteria);
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (DNgayChungTu == DateTime.MinValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }


            if (BudgetIndexes.All(x => !x.IsSelected) && !IsAgregate)
            {
                messages.Add(Resources.AlertLNSEmpty);
            }

            List<string> listLNSExist = CheckExistLNS();
            if (listLNSExist.Count > 0)
            {
                messages.Add(string.Format(Resources.AlertExistAdjustedEstimate, Model.STenDonVi, string.Join(",", listLNSExist)));
            }
            return string.Join(Environment.NewLine, messages);
        }

        private List<string> CheckExistLNS()
        {
            var predicate = PredicateBuilder.True<BhDcDuToanThu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIDMaDonVi == Model.IIDMaDonVi);
            if (Model.Id != Guid.Empty)
                predicate = predicate.And(x => x.Id != Model.Id);
            List<BhDcDuToanThu> chungTus = _bhDcDuToanThuService.FindByCondition(predicate).ToList();
            List<string> listLNSExist = new List<string>();
            chungTus.ForEach(x =>
            {
                listLNSExist.AddRange(x.SLNS.Split(','));
            });
            List<string> listLNSSelected = BudgetIndexes.Where(x => x.IsSelected).Select(x => x.SLNS).ToList();
            return listLNSSelected.Where(x => listLNSExist.Contains(x)).ToList();
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

        private void UpdateChungTuDaTongHop(BhDcDuToanThu chungtu)
        {
            if (!string.IsNullOrEmpty(chungtu.STongHop))
            {
                var lstSoCtChild = chungtu.STongHop.Split(",");
                foreach (var soct in lstSoCtChild)
                {
                    var ctChild = _bhDcDuToanThuService.FindChungTuDaTongHopBySCT(soct, _sessionService.Current.YearOfWork).FirstOrDefault();
                    if (ctChild != null)
                    {
                        ctChild.BDaTongHop = false;
                        _bhDcDuToanThuService.Update(ctChild);
                    }
                }
            }
        }
    }
}
