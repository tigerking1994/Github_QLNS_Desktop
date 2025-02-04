using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.RegularBudget
{
    public class RegularBudgetDialogViewModel : DialogViewModelBase<SettlementVoucherModel>
    {
        private readonly INsQtChungTuService _chungTuService;
        private readonly INsQtChungTuChiTietService _chungTuChiTietService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private const string SELECTED_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private List<ComboboxItem> _quarters;

        public override string Name => "Quyết toán - Thêm chứng từ";
        public override string Title => Model.Id == Guid.Empty ? "Thêm chứng từ" : "Sửa chứng từ";
        public override string Description => Model.Id == Guid.Empty ? "Thêm mới chứng từ quyết toán" : "Cập nhật thông tin từ quyết toán";
        public override Type ContentType => typeof(View.Budget.Settlement.RegularBudget.RegularBudgetDialog);

        public bool IsOpenListView { get; set; }

        #region list đơn vị
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    _listAgency.Refresh();
                    OnPropertyChanged(nameof(SelectedCount));
                }
            }
        }
        public bool IsAdjustChecked { get; set; } = false;
        public bool IsAdjustEnabled { get; set; } = true;

        public string SelectedCount
        {
            get
            {
                int totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = Agencies != null ? Agencies.Count(x => x.Selected) : 0;
                AgencyModel agency = Agencies.Where(x => x.Selected).FirstOrDefault();
                if (agency != null)
                {
                    Model.IIdMaDonVi = agency.Id;
                    Model.STenDonVi = agency.AgencyName;
                }
                return string.Format(SELECTED_COUNT_STR, totalSelected, totalCount);
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

        bool bDeleteDetail { get; set; }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }
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

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        public Guid Id;
        public int VoucherNoIndex;
        public DonVi AggregateAgency;
        public List<SettlementVoucherModel> AggregateSettlementVouchers;

        public string AggregateLNS;

        private bool _isAggregate;
        public bool IsAggregate
        {
            get => _isAggregate;
            set => SetProperty(ref _isAggregate, value);
        }

        public RegularBudgetDialogViewModel(INsQtChungTuService chungTuService,
                                            INsQtChungTuChiTietService chungTuChiTietService,
                                            INsDonViService donViService,
                                            IMapper mapper,
                                            INsMucLucNganSachService mucLucNganSachService,
                                            ISessionService sessionService)
        {
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _donViService = donViService;
            _mapper = mapper;
            _mucLucNganSachService = mucLucNganSachService;
            _sessionService = sessionService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _searchText = string.Empty;
            LoadMonths();
            LoadQuarters();
            LoadData();
            LoadAgencies();
            LoadBudgetIndexes();
        }
        private void LoadQuarters()
        {
            _quarters = new List<ComboboxItem>();
            _quarters.Add(new ComboboxItem("Quý I", "3"));
            _quarters.Add(new ComboboxItem("Quý II", "6"));
            _quarters.Add(new ComboboxItem("Quý III", "9"));
            _quarters.Add(new ComboboxItem("Quý IV", "12"));
        }

        private void LoadBudgetIndexes()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);

            var listDonVi = _donViService.FindByCondition(predicate).ToList();
            /*
            if (listDonVi.Count > 0 && listDonVi.Any(n => (n.Khoi == KhoiDonVi.BENH_VIEN_TU_CHU
                                                        || n.Khoi == KhoiDonVi.DOANH_NGHIEP)
                                                        && n.Loai != LoaiDonVi.TOAN_QUAN))
            {
                IsOpenListView = true;
            }
            else
            {
                IsOpenListView = false;
            }
            */
            IsOpenListView = true;

            string[] arrLNS = new string[] { LNSValue.LNS_1010000, LNSValue.LNS_1010001, LNSValue.LNS_1010002 };
            DonVi donVi = null;
            var donViSelected = Agencies?.FirstOrDefault(x => x.Selected);
            if (donViSelected != null)
            {
                donVi = _donViService.FindByMaDonViAndNamLamViec(donViSelected.Id, _sessionInfo.YearOfWork);
            }

            if (donVi != null)
            {
                if (!string.IsNullOrEmpty(donVi.Khoi) && donVi.Khoi.Equals(KhoiDonVi.BENH_VIEN_TU_CHU))
                {
                    arrLNS = new string[] { LNSValue.LNS_1010000, LNSValue.LNS_1010100, LNSValue.LNS_1010200, LNSValue.LNS_1010300, LNSValue.LNS_1010001, LNSValue.LNS_1010002 };
                }
                else if (!string.IsNullOrEmpty(donVi.Khoi) && donVi.Khoi.Equals(KhoiDonVi.DOANH_NGHIEP))
                {
                    arrLNS = new string[] { LNSValue.LNS_1010000, LNSValue.LNS_1010400, LNSValue.LNS_1010001, LNSValue.LNS_1010002 };
                }
            }
            List<NsMucLucNganSach> listMucLucNganSach = _mucLucNganSachService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, string.Join(StringUtils.COMMA, arrLNS), string.Empty);


            //tổng hợp chứng từ
            if (IsAggregate)
            {
                List<string> listAggregateLNS = AggregateLNS.Split(StringUtils.COMMA).Except(new List<string>() { "1" }).ToList();
                listMucLucNganSach = _mucLucNganSachService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, string.Join(StringUtils.COMMA, listAggregateLNS), string.Empty);
                //listMucLucNganSach = listMucLucNganSach.Where(x => listAggregateLNS.Contains(x.Lns)).ToList();
            }

            BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLucNganSach.Where(x => x.ITrangThai == StatusType.ACTIVE));
            BudgetIndexes.Select(n => n.IsSelected = Model.SDslns?.Contains(n.Lns) ?? false).ToList();

            List<string> listLnsHasData = _chungTuChiTietService.GetLnsHasData(new List<Guid> { Model.Id }).ToList();
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

        private bool ListBudgetIndexFilter(object obj)
        {
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchBudgetIndexText))
                result = item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
            item.IsFilter = result;
            return result;
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
                if (Model.IThangQuy != 0)
                    SelectedQuarterMonth = QuarterMonths.Where(x => x.ValueItem == Model.IThangQuy.ToString()).FirstOrDefault();
                else
                    SelectedQuarterMonth = QuarterMonths.Where(x => x.ValueItem == quarterMonthValue.ToString()).FirstOrDefault();
            }
        }
        private void LoadAgencies()
        {
            List<DonVi> listDonVi = new List<DonVi>();
            if (IsAggregate)
                listDonVi.Add(AggregateAgency);
            else
                listDonVi = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO + StringUtils.COMMA + LoaiDonVi.ROOT).ToList();

            Agencies = new ObservableCollection<AgencyModel>();
            Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
            _listAgency = CollectionViewSource.GetDefaultView(Agencies);
            _listAgency.Filter = ListAgencyFilter;

            foreach (var model in Agencies)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(AgencyModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedCount));
                        LoadBudgetIndexes();
                    }
                };
            }
            if (!string.IsNullOrEmpty(Model?.IIdMaDonVi))
            {
                _agencies.ToList().ForEach(x => x.IsHitTestVisible = false);
                AgencyModel agency = _agencies?.FirstOrDefault(x => x.Id == Model.IIdMaDonVi);
                if (agency != null)
                    agency.Selected = true;
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchText))
                result = item.AgencyName.ToLower().Contains(_searchText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            IsAdjustChecked = false;
            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                Model = new SettlementVoucherModel
                {
                    SSoChungTu = "QT-" + VoucherNoIndex.ToString().PadLeft(3, '0'),
                    ISoChungTuIndex = VoucherNoIndex,
                    DNgayChungTu = DateTime.Now,
                    IIdMaDonVi = string.Empty,
                    STenDonVi = string.Empty,
                    SDslns = ""
                    //SDslns = $"{LNSValue.LNS_1010000},{LNSValue.LNS_1010002}"
                };
                //tổng hợp chứng từ
                if (IsAggregate)
                {
                    Model.IIdMaDonVi = AggregateAgency.IIDMaDonVi;
                    Model.STenDonVi = AggregateAgency.IIDMaDonVi + StringUtils.DIVISION_SPLIT + AggregateAgency.TenDonVi;
                    Model.IThangQuy = AggregateSettlementVouchers.Select(x => x.IThangQuy).FirstOrDefault();
                    QuarterMonthValue = (QuarterMonth)AggregateSettlementVouchers.Select(x => x.IThangQuyLoai).FirstOrDefault();
                }
                else
                {
                    QuarterMonthValue = QuarterMonth.MONTH;
                }
            }
            else
            {
                NsQtChungTu chungTu = _chungTuService.FindById(Id);
                Model = _mapper.Map<SettlementVoucherModel>(chungTu);
                if (!string.IsNullOrEmpty(Model.IIdMaDonVi))
                {
                    AgencyModel agency = _agencies?.FirstOrDefault(x => x.Id == Model.IIdMaDonVi);
                    if (agency != null)
                        agency.Selected = true;
                }

                if (Model.IThangQuyLoai == (int)QuarterMonth.MONTH)
                    QuarterMonthValue = QuarterMonth.MONTH;
                else QuarterMonthValue = QuarterMonth.QUARTER;

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

        public override void OnSave()
        {
            base.OnSave();
            if (string.IsNullOrEmpty(Model.IIdMaDonVi))
            {
                MessageBoxHelper.Warning(Resources.AlertAgencyEmpty);
                return;
            }
            if (IsOpenListView && !BudgetIndexes.Any(x => x.IsSelected))
            {
                MessageBoxHelper.Warning(Resources.AlertLNSEmpty);
                return;
            }
            if (SelectedQuarterMonth == null)
            {
                MessageBoxHelper.Warning(Resources.AlertQuarterMonthEmpty);
                return;
            }

            NsQtChungTu chungTu = new NsQtChungTu();
            Model.SThangQuyMoTa = SelectedQuarterMonth?.DisplayItem ?? string.Empty;
            Model.IThangQuy = Convert.ToInt32(SelectedQuarterMonth?.ValueItem);
            Model.IThangQuyLoai = (int)QuarterMonthValue;

            //kiểm tra tồn tại chứng từ theo đơn vị, tháng, LNS
            List<string> listLNSExist = _chungTuService.FindLNSExist(
                new SettlementVoucherCriteria
                {
                    SettlementType = SettlementType.REGULAR_BUDGET,
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    QuarterMonth = Convert.ToInt32(Model.IThangQuy),
                    QuarterMonthType = Model.IThangQuyLoai,
                    AgencyId = Model.IIdMaDonVi,
                    AdjustType = IsAggregate ? AggregateSettlementVouchers.Select(x => x.ILoaiChungTu).Distinct().FirstOrDefault() : 1,

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
                        if (tempDict.ContainsKey(Model.IThangQuy))
                        {
                            time = $"quý {tempDict[Model.IThangQuy]}";
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
                var condition = PredicateBuilder.True<NsQtChungTuChiTiet>();
                condition = condition.And(x => x.IIdQtchungTu == Model.Id);
                condition = condition.And(x => listLNSHasDataUnchecked.Contains(x.SLns));
                var listChungTuChiTiet = _chungTuChiTietService.FindByCondition(condition).ToList();
                _chungTuChiTietService.RemoveRange(listChungTuChiTiet);

                Model.FTongTuChiDeNghi -= listChungTuChiTiet.Sum(x => x.FTuChiDeNghi);
                Model.FTongTuChiPheDuyet -= listChungTuChiTiet.Sum(x => x.FTuChiPheDuyet);
            }

            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                Model.INamNganSach = _sessionInfo.YearOfBudget;
                Model.INamLamViec = _sessionInfo.YearOfWork;
                Model.IIdMaNguonNganSach = _sessionInfo.Budget;
                Model.SLoai = SettlementType.REGULAR_BUDGET;
                Model.SNguoiTao = _sessionInfo.Principal;
                Model.DNgayTao = DateTime.Now;
                Model.SDslns = IsOpenListView ? string.Join(StringUtils.COMMA, BudgetIndexes.Where(n => n.IsSelected).Select(n => n.Lns)) : LNSValue.LNS_1010000;
                if (IsAdjustChecked)
                {
                    Model.ILoaiChungTu = 2;
                    List<string> listLNSAdjustExist = _chungTuService.FindLNSExist(
                        new SettlementVoucherCriteria
                        {
                            SettlementType = SettlementType.REGULAR_BUDGET,
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            BudgetSource = _sessionInfo.Budget,
                            QuarterMonth = Convert.ToInt32(Model.IThangQuy),
                            QuarterMonthType = Model.IThangQuyLoai,
                            AgencyId = Model.IIdMaDonVi,
                            AdjustType = 2
                        }, Model.Id, BudgetIndexes.Where(n => n.IsSelected && !n.IsParent).Select(n => n.Lns).ToList());
                    if (listLNSAdjustExist.Count == 0)
                    {
                        Model.ILanDieuChinh = 1;
                    }
                    else
                    {
                        Model.ILanDieuChinh = _chungTuService.CreateAdjustVoucherIndex(new SettlementVoucherCriteria
                        {
                            SettlementType = SettlementType.REGULAR_BUDGET,
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            BudgetSource = _sessionInfo.Budget,
                            QuarterMonth = Convert.ToInt32(Model.IThangQuy),
                            QuarterMonthType = Model.IThangQuyLoai,
                            AgencyId = Model.IIdMaDonVi,
                            AdjustType = 2
                        });
                    }
                }
                else
                {
                    Model.ILoaiChungTu = IsAggregate ? AggregateSettlementVouchers.Select(x => x.ILoaiChungTu).Distinct().FirstOrDefault() : 1;
                }
                //ở trạng thái tổng hợp
                if (IsAggregate)
                {
                    Model.FTongTuChiDeNghi = AggregateSettlementVouchers.Sum(x => x.FTongTuChiDeNghi);
                    Model.FTongTuChiPheDuyet = AggregateSettlementVouchers.Sum(x => x.FTongTuChiPheDuyet);
                    Model.STongHop = string.Join(StringUtils.COMMA, AggregateSettlementVouchers.Select(x => x.SSoChungTu).OrderBy(x => x).ToList());
                }

                _mapper.Map(Model, chungTu);
                _chungTuService.Add(chungTu);

                //tạo chứng từ chi tiết khi tổng hợp
                if (IsAggregate)
                    CreateSettlementVoucherDetail(_mapper.Map<SettlementVoucherModel>(chungTu));
            }
            else
            {
                Model.SNguoiSua = _sessionInfo.Principal;
                Model.DNgaySua = DateTime.Now;
                Model.SDslns = IsOpenListView ? string.Join(StringUtils.COMMA, BudgetIndexes.Where(n => n.IsSelected && !n.IsParent).Select(n => n.Lns)) : LNSValue.LNS_1010000;
                chungTu = _chungTuService.FindById(Model.Id);
                _mapper.Map(Model, chungTu);
                _chungTuService.Update(chungTu);
                //_chungTuChiTietService.UpdateMonth(Model.Id, Convert.ToInt32(MonthSelected.ValueItem), (int)QuarterMonth.MONTH, _sessionInfo.Principal);
                _chungTuChiTietService.UpdateMonth(Model.Id, Convert.ToInt32(SelectedQuarterMonth.ValueItem), (int)QuarterMonth.MONTH, _sessionInfo.Principal);
            }

            DialogHost.Close(SettlementScreen.ROOT_DIALOG);
            // DialogHost.CloseDialogCommand.Execute(Model, null);
            SettlementVoucherModel settlementVoucher = _mapper.Map<SettlementVoucherModel>(chungTu);
            settlementVoucher.STenDonVi = Model.STenDonVi;
            SavedAction?.Invoke(settlementVoucher);
        }

        /// <summary>
        /// Tạo chứng từ chi tiết
        /// </summary>
        /// <param name="settlementVoucher"></param>
        private void CreateSettlementVoucherDetail(SettlementVoucherModel settlementVoucher)
        {
            SettlementVoucherDetailCriteria creation = new SettlementVoucherDetailCriteria()
            {
                VoucherIds = string.Join(StringUtils.COMMA, AggregateSettlementVouchers.Select(x => x.Id.ToString()).ToList()),
                VoucherId = settlementVoucher.Id.ToString(),
                YearOfBudget = settlementVoucher.INamNganSach,
                BudgetSource = settlementVoucher.IIdMaNguonNganSach,
                YearOfWork = settlementVoucher.INamLamViec,
                Type = SettlementType.REGULAR_BUDGET,
                QuarterMonthType = settlementVoucher.IThangQuyLoai,
                QuarterMonth = settlementVoucher.IThangQuy,
                AgencyId = settlementVoucher.IIdMaDonVi,
                UserName = _sessionInfo.Principal
            };
            _chungTuChiTietService.AddAggregateVoucherDetail(creation);
        }
    }
}
