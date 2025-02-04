using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RealRevenueExpenditure;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RealRevenueExpenditure
{
    public class RealRevenueExpenditureDialogViewModel : DialogViewModelBase<TnQtChungTuHD4554Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ITnQtChungTuHD4554Service _tnQtChungTuService;
        private readonly ITnQtChungTuChiTietHD4554Service _tnQtChungTuChiTietService;
        private readonly ITTnDanhMucLoaiHinhService _tTnDanhMucLoaiHinhService;
        private SessionInfo _sessionInfo;
        private INsMucLucNganSachService _mucLucNganSachService;
        private readonly ILog _logger;
        private List<ComboboxItem> _months;
        private List<ComboboxItem> _quarters;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";

        public override Type ContentType => typeof(RealRevenueExpenditureDialog);
        public override string Name => Model.Id == Guid.Empty ? "THÊM CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Model.Id == Guid.Empty ? "Tạo mới chứng từ thực thu" : "Cập nhật chứng từ thực thu";

        #region list đơn vị  
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private string _searchAgencyText;
        public string SearchAgencyText
        {
            get => _searchAgencyText;
            set
            {
                if (SetProperty(ref _searchAgencyText, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = Agencies != null ? Agencies.Count : 0;
                int totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                AgencyModel agency = Agencies.Where(x => x.Selected).FirstOrDefault();
                if (agency != null)
                {
                    Model.IIdMaDonVi = agency.Id;
                    Model.STenDonVi = agency.AgencyName;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        #endregion

        //#region list LOAI HINH
        //private ObservableCollection<TnDanhMucLoaiHinhModel> _budgetIndexes;
        //public ObservableCollection<TnDanhMucLoaiHinhModel> BudgetIndexes
        //{
        //    get => _budgetIndexes;
        //    set => SetProperty(ref _budgetIndexes, value);
        //}

        //private string _searchBudgetIndexText;
        //public string SearchBudgetIndexText
        //{
        //    set
        //    {
        //        if (SetProperty(ref _searchBudgetIndexText, value))
        //        {
        //            _listBudgetIndex.Refresh();
        //        }
        //    }
        //}

        //public string SelectedBudgetIndexCount
        //{
        //    get
        //    {
        //        int totalCount = BudgetIndexes != null ? BudgetIndexes.Count : 0;
        //        int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
        //        return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
        //    }
        //}

        //private bool _isSelectAllBudgetIndex;
        //public bool IsSelectAllBudgetIndex
        //{
        //    get => (BudgetIndexes == null || !BudgetIndexes.Any()) ? false : BudgetIndexes.All(item => item.IsSelected);
        //    set
        //    {
        //        SetProperty(ref _isSelectAllBudgetIndex, value);
        //        foreach (TnDanhMucLoaiHinhModel item in BudgetIndexes)
        //        {
        //            item.IsSelected = _isSelectAllBudgetIndex;
        //        }
        //    }
        //}
        //#endregion

        #region combobox tháng, quý
        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private QuarterMonth _quarterMonthValue;
        public QuarterMonth QuarterMonthValue
        {
            get => _quarterMonthValue;
            set
            {
                SetProperty(ref _quarterMonthValue, value);
                LoadQuarterMonths();
            }
        }

        private string _quarterMonthHint;
        public string QuarterMonthHint
        {
            get => _quarterMonthHint;
            set => SetProperty(ref _quarterMonthHint, value);
        }

        private ComboboxItem _selectedQuarterMonth;
        public ComboboxItem SelectedQuarterMonth
        {
            get => _selectedQuarterMonth;
            set
            {
                SetProperty(ref _selectedQuarterMonth, value);
            }
        }
        #endregion

        #region list LNS

        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes;
        public ObservableCollection<NsMuclucNgansachModel> BudgetIndexes
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
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Where(x => x.IsFilter).All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes.Where(x => x.IsFilter))
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        private bool _isAggregate;
        public bool IsAggregate
        {
            get => _isAggregate;
            set => SetProperty(ref _isAggregate, value);
        }
        bool bDeleteDetail { get; set; }
        public bool IsAdjustChecked { get; set; } = false;
        public bool IsAdjustEnabled { get; set; } = true;
        public Visibility VisibilityDataLns
        {
            get => _sessionService.Current.Authorities.Contains(Role.TRO_LY_TONG_HOP) ? Visibility.Collapsed : Visibility.Visible;
        }
        public string AggregateLNS { get; set; }
        public DonVi AggregateAgency;
        public List<TnQtChungTuHD4554Model> AggregateSettlementVouchers;

        public RealRevenueExpenditureDialogViewModel(IMapper mapper,
            ISessionService sessionService,
            INsDonViService donViService,
            ITnQtChungTuHD4554Service tnQtChungTuService,
            ITTnDanhMucLoaiHinhService tTnDanhMucLoaiHinhService,
            ITnQtChungTuChiTietHD4554Service tnQtChungTuChiTietService,
            INsMucLucNganSachService nsMucLucNganSachService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _donViService = donViService;
            _tnQtChungTuService = tnQtChungTuService;
            _tTnDanhMucLoaiHinhService = tTnDanhMucLoaiHinhService;
            _tnQtChungTuChiTietService = tnQtChungTuChiTietService;
            _mucLucNganSachService = nsMucLucNganSachService;
            _logger = logger;
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            base.Init();
            LoadAgencies();
            LoadMonths();
            LoadQuarters();
            LoadBudgetIndexes();
            LoadData();
            //LoadQuarterMonths();
        }

        private void LoadMonths()
        {
            try
            {
                _months = new List<ComboboxItem>();

                int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                var idDonVi = Agencies.Where(x => x.Selected);

                if (Model.Id != Guid.Empty)
                {
                    if (idDonVi.Count() > 0)
                    {
                        var lstMonthCreated = _tnQtChungTuService.FindByIdDonVi(idDonVi.LastOrDefault().Id, 0).Select(x => (int)(x.IThangQuy ?? 0)).ToArray();
                        lstMonthCreated = lstMonthCreated.Where(x => !x.Equals((int)Model.IThangQuy)).ToArray();

                        months = months.Where(x => !lstMonthCreated.Contains(x)).ToArray();
                    }
                }
                else
                {
                    if (idDonVi.Count() > 0)
                    {
                        var lstMonthCreated = _tnQtChungTuService.FindByIdDonVi(idDonVi.LastOrDefault().Id, 0).Select(x => (int)(x.IThangQuy ?? 0)).ToArray();
                        months = months.Where(x => !lstMonthCreated.Contains(x)).ToArray();
                    }
                }

                foreach (var item in months)
                {
                    ComboboxItem month = new ComboboxItem("Tháng " + item, item.ToString());
                    _months.Add(month);
                }

                //LoadQuarterMonths();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadQuarters()
        {
            try
            {
                _quarters = new List<ComboboxItem>();

                int[] quaters = { 3, 6, 9, 12 };

                var idDonVi = Agencies.Where(x => x.Selected);

                if (Model.Id != Guid.Empty)
                {
                    if (idDonVi.Count() > 0)
                    {
                        var lstQuaterCreated = _tnQtChungTuService.FindByIdDonVi(idDonVi.LastOrDefault().Id, 1).Select(x => (int)(x.IThangQuy ?? 0)).ToArray();
                        lstQuaterCreated = lstQuaterCreated.Where(x => Model.IThangQuy != null && !x.Equals((int)Model.IThangQuy)).ToArray();

                        quaters = quaters.Where(x => !lstQuaterCreated.Contains(x)).ToArray();
                    }
                }
                else
                {
                    if (idDonVi.Count() > 0)
                    {
                        var lstMonthCreated = _tnQtChungTuService.FindByIdDonVi(idDonVi.LastOrDefault().Id, 1).Select(x => (int)(x.IThangQuy ?? 0)).ToArray();
                        quaters = quaters.Where(x => !lstMonthCreated.Contains(x)).ToArray();
                    }
                }

                foreach (var item in quaters)
                {
                    if (item.Equals(3))
                    {
                        ComboboxItem quater = new ComboboxItem("Quý I ", item.ToString());
                        _quarters.Add(quater);
                    }
                    else if (item.Equals(6))
                    {
                        ComboboxItem quater = new ComboboxItem("Quý II ", item.ToString());
                        _quarters.Add(quater);
                    }
                    else if (item.Equals(9))
                    {
                        ComboboxItem quater = new ComboboxItem("Quý III ", item.ToString());
                        _quarters.Add(quater);
                    }
                    else if (item.Equals(12))
                    {
                        ComboboxItem quater = new ComboboxItem("Quý IV ", item.ToString());
                        _quarters.Add(quater);
                    }
                }

                //LoadQuarterMonths();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadQuarterMonths()
        {
            int quarterMonthValue = 0;
            int month = DateTime.Now.Month;
            if (QuarterMonthValue == QuarterMonth.QUARTER)
            {
                QuarterMonths = _quarters;
                QuarterMonthHint = "Chọn quý";
                if (month <= 3)
                    quarterMonthValue = 3;
                else if (3 < month && month <= 6)
                    quarterMonthValue = 6;
                else if (6 < month && month <= 9)
                    quarterMonthValue = 9;
                else quarterMonthValue = 12;
            }
            else
            {
                QuarterMonths = _months;
                QuarterMonthHint = "Chọn tháng";
                quarterMonthValue = month;
            }
            if (IsAggregate)
            {
                SelectedQuarterMonth = QuarterMonths.FirstOrDefault(x => x.ValueItem == AggregateSettlementVouchers.FirstOrDefault().IThangQuy.ToString());
            }
            else
            {
                if (Model.IThangQuy != null)
                    SelectedQuarterMonth = QuarterMonths.Where(x => x.ValueItem == Model.IThangQuy.ToString()).FirstOrDefault();
                else
                    SelectedQuarterMonth = QuarterMonths.Where(x => x.ValueItem == quarterMonthValue.ToString()).FirstOrDefault();
            }
        }

        private void LoadAgencies()
        {
            try
            {
                List<DonVi> listDonVi = new List<DonVi>();

                if (IsAggregate && AggregateAgency != null)
                    listDonVi.Add(AggregateAgency);
                else
                {
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    listDonVi = _donViService.FindByNamLamViec(yearOfWork).ToList();
                }

                List<string> lstDonViCreated = new List<string>();
                List<string> lstDonVi = _tnQtChungTuService.FindAll().Select(x => x.IIdMaDonVi).Distinct().ToList();

                if (lstDonVi.Count > 0)
                {
                    foreach (var item in lstDonVi)
                    {
                        var lstSumMonth = _tnQtChungTuService.FindByIdDonVi(item, 0);
                        var lstSumQuater = _tnQtChungTuService.FindByIdDonVi(item, 1);

                        if (lstSumMonth.Count().Equals(12) && lstSumQuater.Count().Equals(4))
                            lstDonViCreated.Add(item);
                    }
                }

                if (Model.Id == Guid.Empty)
                {
                    if (lstDonViCreated.Count > 0)
                    {
                        string[] arrayDonViCreated = lstDonViCreated.ToArray();
                        listDonVi = listDonVi.Where(x => !arrayDonViCreated.Contains(x.IIDMaDonVi)).ToList();
                    }
                }
                else
                {
                    if (lstDonViCreated.Count > 0)
                    {
                        string[] arrayDonViCreated = lstDonViCreated.ToArray();
                        arrayDonViCreated = arrayDonViCreated.Where(x => !x.Contains(Model.IIdMaDonVi)).ToArray();
                        listDonVi = listDonVi.Where(x => !arrayDonViCreated.Contains(x.IIDMaDonVi)).ToList();
                    }
                }

                Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
                _listAgency = CollectionViewSource.GetDefaultView(Agencies);
                _listAgency.Filter = ListAgencyFilter;
                foreach (var model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected))
                        {
                            LoadMonths();
                            LoadQuarters();
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
        }

        private void LoadBudgetIndexes()
        {
            try
            {
                var yearOfWork = _sessionService.Current.YearOfWork;
                string sLNS = "8";
                var listMucLucNganSach = _mucLucNganSachService.FindByMLNS(yearOfWork, sLNS);
                //tổng hợp chứng từ
                if (IsAggregate)
                {
                    List<string> listAggregateLNS = AggregateLNS.Split(StringUtils.COMMA).Distinct().Except(new List<string>() { "1" }).ToList();
                    listMucLucNganSach = listMucLucNganSach.Where(x => listAggregateLNS.Contains(x.Lns)).ToList();
                }

                BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
                BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLucNganSach.Where(x => x.ITrangThai == StatusType.ACTIVE));
                if (!string.IsNullOrEmpty(Model.SDSLNS))
                {
                    var lstSDSLNS = Model.SDSLNS.Split(",").Distinct();
                    BudgetIndexes.Select(n => n.IsSelected = lstSDSLNS?.Contains(n.Lns) ?? false).ToList();
                }
                List<string> listLnsHasData = _tnQtChungTuChiTietService.GetLnsHasData(new List<Guid> { Model.Id }).ToList();
                BudgetIndexes.Select(x => x.IsHitTestVisible = !listLnsHasData.Contains(x.Lns)).ToList();

                _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
                _listBudgetIndex.Filter = ListBudgetIndexFilter;
                foreach (var model in BudgetIndexes)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                        {
                            foreach (var item in BudgetIndexes)
                            {
                                if (item.MlnsIdParent == model.MlnsId)
                                {
                                    if (item.IsHitTestVisible)
                                        item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        }
                    };
                }

                if (IsAggregate)
                    BudgetIndexes.ToList().ForEach(x => x.IsSelected = true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);
                IsAdjustChecked = false;
                if (Model != null && Model.Id != Guid.Empty)
                {
                    TnQtChungTuHD4554 chungTu = _tnQtChungTuService.FindById(Model.Id);
                    Model = _mapper.Map<TnQtChungTuHD4554Model>(chungTu);

                    if (!string.IsNullOrEmpty(Model.IIdMaDonVi))
                    {
                        AgencyModel agency = _agencies.Where(x => x.Id == Model.IIdMaDonVi).FirstOrDefault();
                        if (agency != null)
                            agency.Selected = true;
                    }

                    if (Model.IThangQuyLoai == (int)QuarterMonth.MONTH)
                        QuarterMonthValue = QuarterMonth.MONTH;
                    else QuarterMonthValue = QuarterMonth.QUARTER;

                    //BudgetLhCatalogSelectedToStringConvert.SetCheckboxSelected(BudgetIndexes, Model.SDSLNS);
                }
                else
                {
                    _isSelectAllBudgetIndex = false;

                    var predicate = this.CreatePredicate();
                    int soChungTuIndex = _tnQtChungTuService.FindNextSoChungTuIndex(predicate);
                    Model = new TnQtChungTuHD4554Model()
                    {
                        SSoChungTu = "QT-" + soChungTuIndex.ToString("D3"),
                        ISoChungTuIndex = soChungTuIndex,
                        DNgayChungTu = DateTime.Now,
                        //DNgayQuyetDinh = DateTime.Now,
                        //SSoQuyetDinh = string.Empty,
                        SMoTa = string.Empty,
                        IIdMaDonVi = string.Empty,
                        STenDonVi = string.Empty
                    };


                    // tổng hợp chứng từ
                    if (IsAggregate)
                    {
                        Model.IIdMaDonVi = AggregateAgency.IIDMaDonVi;
                        Model.STenDonVi = AggregateAgency.IIDMaDonVi + "-" + AggregateAgency.TenDonVi;
                        Model.IThangQuy = AggregateSettlementVouchers.Select(x => x.IThangQuy).FirstOrDefault();
                        QuarterMonthValue = (QuarterMonth)AggregateSettlementVouchers.Select(x => x.IThangQuyLoai).FirstOrDefault();
                        AgencyModel agency = _agencies.FirstOrDefault();
                        if (agency != null)
                            agency.Selected = true;
                    }
                    else
                    {
                        QuarterMonthValue = QuarterMonth.MONTH;
                    }

                    OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                    OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Expression<Func<TnQtChungTuHD4554, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<TnQtChungTuHD4554>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.INguonNganSach == _sessionService.Current.Budget);

            return predicate;
        }

        public override void OnSave()
        {
            try
            {
                base.OnSave();
                TnQtChungTuHD4554 chungTu = new TnQtChungTuHD4554();

                Model.IThangQuy = SelectedQuarterMonth == null ? 0 : Int32.Parse(SelectedQuarterMonth.ValueItem);
                Model.IThangQuyLoai = (int)QuarterMonthValue;
                Model.SDSLNS = GetValueSelected(BudgetIndexes);
                Model.IIdMaDonVi = (from item in Agencies where item.Selected select item.Id != null ? item.Id : string.Empty).ToList().FirstOrDefault();

                Model.SThangQuyMoTa = SelectedQuarterMonth != null ? SelectedQuarterMonth.DisplayItem : string.Empty;


                List<string> listLNSExist = _tnQtChungTuService.FindLNSExist(new SettlementVoucherCriteria
                {
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    QuarterMonth = Convert.ToInt32(Model.IThangQuy),
                    QuarterMonthType = Model.IThangQuyLoai,
                    AgencyId = Model.IIdMaDonVi,
                }, Model.Id, BudgetIndexes.Where(n => n.IsSelected).Select(n => n.Lns).ToList());

                if (IsAdjustChecked)
                {
                    if (listLNSExist.Count == 0)
                    {
                        string time = "";
                        if (Model.IThangQuyLoai == 0) time = $"tháng {Model.IThangQuy}";
                        else
                        {
                            var tempDict = new Dictionary<int, string>()
                            {
                                [3] = "I",
                                [6] = "II",
                                [9] = "III",
                                [12] = "IV",
                            };
                            if (tempDict.ContainsKey(Model.IThangQuy.Value))
                            {
                                time = $"quý {tempDict[Model.IThangQuy.Value]}";
                            }
                        }

                        MessageBoxHelper.Warning(string.Format(Resources.AlertNonExistSettlementMonthVoucher, time, string.Join(StringUtils.COMMA, BudgetIndexes.Where(n => n.IsSelected).Select(n => n.Lns))));
                        return;
                    }
                }
                else
                {
                    if (listLNSExist.Count > 0)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementMonthVoucher, Model.STenDonVi, Model.IThangQuy, string.Join(StringUtils.COMMA, listLNSExist)));
                        return;
                    }
                }

                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string messageCheckBoxLNS = GetMessageValidateCheckBoxLNS();
                if (!string.IsNullOrEmpty(messageCheckBoxLNS))
                {
                    MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBoxLNS);
                    if (messageValidate.Equals(MessageBoxResult.Yes))
                    {
                        bDeleteDetail = true;
                    }
                    else
                    {
                        bDeleteDetail = false;
                        BudgetIndexes.Select(n =>
                        {
                            if (!n.IsHitTestVisible)
                            {
                                n.IsSelected = true;
                            }
                            return n;
                        }).ToList();
                        _listBudgetIndex.Refresh();
                        return;
                    }
                }

                if (bDeleteDetail)
                {
                    var listLNSHasDataUnchecked = BudgetIndexes.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.Lns).ToList();
                    var condition = PredicateBuilder.True<TnQtChungTuChiTietHD4554>();
                    condition = condition.And(x => x.IIdTnQtChungTu == Model.Id);
                    condition = condition.And(x => listLNSHasDataUnchecked.Contains(x.SLNS));
                    var listChungTuChiTiet = _tnQtChungTuChiTietService.FindByCondition(condition).ToList();
                    _tnQtChungTuChiTietService.RemoveRange(listChungTuChiTiet);
                    Model.FTongSoTien -= listChungTuChiTiet.Sum(x => x.FSoTien);
                }

                //trường hợp tạo mới
                if (Model.Id == Guid.Empty)
                {
                    //Add
                    Model.INamNganSach = _sessionService.Current.YearOfBudget;
                    Model.INamLamViec = _sessionService.Current.YearOfWork;
                    Model.INguonNganSach = _sessionService.Current.Budget;
                    Model.SNguoiTao = _sessionService.Current.Principal;
                    Model.DNgayTao = DateTime.Now;
                    Model.BKhoa = false;
                    Model.BDaTongHop = false;
                    // ở trạng thái tông hợp
                    if (IsAggregate)
                    {
                        Model.FTongSoTien = AggregateSettlementVouchers.Sum(x => x.FTongSoTien);
                        //Model.TongSoChiPhiSum = AggregateSettlementVouchers.Sum(x => x.TongSoChiPhiSum);
                        Model.STongHop = string.Join(",", AggregateSettlementVouchers.OrderBy(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList());
                        Model.SDSLNS = string.Join(",", AggregateSettlementVouchers.Select(x => x.SDSLNS).ToList());
                    }

                    chungTu = _mapper.Map<TnQtChungTuHD4554>(Model);
                    _tnQtChungTuService.Add(chungTu);

                    // tọa chứng từ chi tiết khi tổng hợp
                    if (IsAggregate)
                        CreateSettlementVoucherDetail(_mapper.Map<TnQtChungTuHD4554Model>(chungTu));
                }
                else
                {
                    //nếu chứng từ ở trạng thái mới hoặc từ chối kiểm duyệt thì cập nhật thành trạng thái mới
                    if (!IsAggregate)

                        Model.SNguoiSua = _sessionService.Current.Principal;
                    Model.DNgaySua = DateTime.Now;
                    Model.SDSLNS = string.Join(StringUtils.COMMA, BudgetIndexes.Where(n => n.IsSelected && !n.IsParent).Select(n => n.Lns));
                    chungTu = _tnQtChungTuService.FindById(Model.Id);
                    _mapper.Map(Model, chungTu);
                    _tnQtChungTuService.Update(chungTu);
                    _tnQtChungTuChiTietService.UpdateMonth(Model.Id, Convert.ToInt32(SelectedQuarterMonth.ValueItem), (int)QuarterMonth.MONTH, _sessionInfo.Principal);
                }

                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<TnQtChungTuHD4554Model>(chungTu));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetMessageValidateCheckBoxLNS()
        {
            var listLNSHasDataUnchecked = BudgetIndexes.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.Lns).ToList();
            string lnsText = string.Join(StringUtils.COMMA_SPLIT, listLNSHasDataUnchecked);

            if (!string.IsNullOrEmpty(lnsText))
            {
                return string.Format(Resources.SettlementHasDataLNS, lnsText);
            }
            else return "";

        }

        public static string GetValueSelected(ObservableCollection<NsMuclucNgansachModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.Lns).ToList());
            }
            return string.Empty;
        }

        public static void SetCheckboxSelected(ObservableCollection<NsMuclucNgansachModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").ToList();
            foreach (NsMuclucNgansachModel item in data)
            {
                if (selectedValues.Contains(item.Lns))
                {
                    item.IsSelected = true;
                }
            }
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();

            if (!Model.DNgayChungTu.HasValue)
            {
                messages.Add("Hãy nhập ngày chứng từ.");
            }

            if (BudgetIndexes.All(x => !x.IsSelected))
            {
                messages.Add("Đ/c chưa chọn Loại ngân sách");
            }

            if (Model.IIdMaDonVi == null)
            {
                messages.Add(Resources.AlertAgencyEmpty);
            }

            if (Model.IThangQuy.Equals(0))
            {
                messages.Add(Resources.AlterThangQuyEmpty);
            }

            return string.Join(Environment.NewLine, messages);
        }

        /// <summary>
        /// Tạo chứng từ chi tiết
        /// </summary>
        /// <param name="settlementVoucher"></param>
        private void CreateSettlementVoucherDetail(TnQtChungTuHD4554Model settlementVoucher)
        {
            SettlementVoucherDetailCriteria creation = new SettlementVoucherDetailCriteria()
            {
                VoucherIds = string.Join(",", AggregateSettlementVouchers.Select(x => x.Id.ToString()).ToList()),
                VoucherId = settlementVoucher.Id.ToString(),
                YearOfBudget = settlementVoucher.INamNganSach.Value,
                BudgetSource = settlementVoucher.INguonNganSach.Value,
                YearOfWork = settlementVoucher.INamLamViec != null ? settlementVoucher.INamLamViec.Value : _sessionService.Current.YearOfWork,
                Type = SettlementType.REGULAR_BUDGET,
                QuarterMonthType = settlementVoucher.IThangQuyLoai.Value,
                QuarterMonth = settlementVoucher.IThangQuy != null ? settlementVoucher.IThangQuy.Value : 0,
                AgencyId = settlementVoucher.IIdMaDonVi,
                AgencyName = settlementVoucher.STenDonVi,
                UserName = _sessionService.Current.Principal
            };

            _tnQtChungTuChiTietService.AddAggregateVoucherDetail(creation);
        }
    }
}
