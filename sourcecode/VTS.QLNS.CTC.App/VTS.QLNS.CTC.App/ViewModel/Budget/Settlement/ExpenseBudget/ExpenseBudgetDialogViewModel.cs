using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Converters;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.ExpenseBudget
{
    public class ExpenseBudgetDialogViewModel : DialogViewModelBase<SettlementVoucherModel>
    {
        private INsQtChungTuService _chungTuService;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsDonViService _donViService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private INsPhongBanService _phongBanService;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private List<ComboboxItem> _quarters;
        private List<ComboboxItem> _months;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private SessionInfo _sessionInfo;

        public Action<SettlementVoucherModel> SavedAction;
        public override string Name => "Quyết toán - Thêm chứng từ";
        public override Type ContentType => typeof(View.Budget.Settlement.ExpenseBudget.ExpenseBudgetDialog);
        public override string Title => Model.Id == Guid.Empty ? "Thêm chứng từ" : "Sửa chứng từ";
        public override string Description => Model.Id == Guid.Empty ? "Thêm mới chứng từ quyết toán" : "Cập nhật thông tin từ quyết toán";

        public bool bDeleteDetail { get; set; }
        public bool IsAdjustChecked { get; set; } = false;
        public bool IsAdjustEnabled { get; set; } = true;

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
                    OnPropertyChanged(nameof(SelectedAgencyCount));
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
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

        private List<ComboboxItem> _departments;
        public List<ComboboxItem> Departments
        {
            get => _departments;
            set => SetProperty(ref _departments, value);
        }

        private ComboboxItem _selectedDepartment;
        public ComboboxItem SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (SetProperty(ref _selectedDepartment, value))
                {
                    _listBudgetIndex.Refresh();
                }
            }
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

        public ExpenseBudgetDialogViewModel(INsQtChungTuService chungTuService,
                                          INsDonViService donViService,
                                          IMapper mapper,
                                          ISessionService sessionService,
                                          INsMucLucNganSachService mucLucNganSachService,
                                          INsPhongBanService phongBanService,
                                          INsQtChungTuChiTietService chungTuChiTietService)
        {
            _chungTuService = chungTuService;
            _donViService = donViService;
            _mapper = mapper;
            _sessionService = sessionService;
            _mucLucNganSachService = mucLucNganSachService;
            _phongBanService = phongBanService;
            _chungTuChiTietService = chungTuChiTietService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _searchAgencyText = string.Empty;
            _searchBudgetIndexText = string.Empty;
            LoadMonths();
            LoadQuarters();
            LoadAgencies();
            LoadBudgetIndexes();
            LoadDepartments();
            LoadData();
        }

        private void LoadAgencies()
        {
            List<DonVi> listDonVi = new List<DonVi>();
            if (IsAggregate)
                listDonVi.Add(AggregateAgency);
            else
                listDonVi = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO + "," + LoaiDonVi.ROOT).ToList();

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
                        OnPropertyChanged(nameof(SelectedAgencyCount));
                    }
                };
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        /// <summary>
        /// Tạo dữ liệu combobox tháng
        /// </summary>
        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
        }

        /// <summary>
        /// Tạo dữ liệu combobox quý
        /// </summary>
        private void LoadQuarters()
        {
            _quarters = new List<ComboboxItem>();
            _quarters.Add(new ComboboxItem("Quý I", "3"));
            _quarters.Add(new ComboboxItem("Quý II", "6"));
            _quarters.Add(new ComboboxItem("Quý III", "9"));
            _quarters.Add(new ComboboxItem("Quý IV", "12"));
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
            if (Model.IThangQuy != 0)
                SelectedQuarterMonth = QuarterMonths.Where(x => x.ValueItem == Model.IThangQuy.ToString()).FirstOrDefault();
            else
                SelectedQuarterMonth = QuarterMonths.Where(x => x.ValueItem == quarterMonthValue.ToString()).FirstOrDefault();
        }

        private void LoadBudgetIndexes()
        {
            //string[] arrLNS = new string[] { "4", "5" };
            var listMLNS = _mucLucNganSachService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            var listMLNSExcept = listMLNS.Select(n => n.Lns).Where(x => x.StartsWith(LNSValue.LNS_1) || x.StartsWith(LNSValue.LNS_2) || x.StartsWith(LNSValue.LNS_3));

            List<NsMucLucNganSach> listMucLucNganSach = _mucLucNganSachService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, string.Empty, string.Join(StringUtils.COMMA, listMLNSExcept));

            //tổng hợp chứng từ
            if (IsAggregate)
            {
                List<string> listAggregateLNS = AggregateLNS.Split(StringUtils.COMMA).ToList();
                listMucLucNganSach = listMucLucNganSach.Where(x => listAggregateLNS.Contains(x.Lns)).ToList();
            }

            BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLucNganSach.Where(x => x.ITrangThai == StatusType.ACTIVE));

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
            if (_selectedDepartment != null)
                result = result && item.IdPhongBan == _selectedDepartment.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void LoadDepartments()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            // string[] arrDepartmentId = new string[] { "06", "07", "08", "10", "17" };
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            // predicate = predicate.And(x => arrDepartmentId.Contains(x.IIDMaBQuanLy) && x.INamLamViec == yearOfWork);
            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
            List<DmBQuanLy> listPhongBan = _phongBanService.FindByCondition(predicate);

            Departments = new List<ComboboxItem>();
            Departments = _mapper.Map<List<ComboboxItem>>(listPhongBan);
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                Model = new SettlementVoucherModel
                {
                    SSoChungTu = "QT-" + VoucherNoIndex.ToString().PadLeft(3, '0'),
                    ISoChungTuIndex = VoucherNoIndex,
                    DNgayChungTu = DateTime.Now,
                    IIdMaDonVi = string.Empty,
                    STenDonVi = string.Empty
                };
                QuarterMonthValue = QuarterMonth.QUARTER;

                //tổng hợp chứng từ
                if (IsAggregate)
                {
                    Model.IIdMaDonVi = AggregateAgency.IIDMaDonVi;
                    Model.STenDonVi = AggregateAgency.IIDMaDonVi + " - " + AggregateAgency.TenDonVi;
                    Model.IThangQuy = AggregateSettlementVouchers.Select(x => x.IThangQuy).FirstOrDefault();
                }
            }
            else
            {
                NsQtChungTu chungTu = _chungTuService.FindById(Id);
                Model = _mapper.Map<SettlementVoucherModel>(chungTu);
                if (!string.IsNullOrEmpty(Model.IIdMaDonVi))
                {
                    AgencyModel agency = _agencies.Where(x => x.Id == Model.IIdMaDonVi).FirstOrDefault();
                    if (agency != null)
                        agency.Selected = true;
                }
                BudgetCatalogSelectedToStringConvert.SetCheckboxSelected(BudgetIndexes, Model.SDslns);
                if (Model.IThangQuyLoai == (int)QuarterMonth.MONTH)
                    QuarterMonthValue = QuarterMonth.MONTH;
                else QuarterMonthValue = QuarterMonth.QUARTER;

                List<string> listLnsHasData = _chungTuChiTietService.GetLnsHasData(new List<Guid> { Model.Id }).ToList();
                BudgetIndexes.Where(x => listLnsHasData.Contains(x.Lns)).ToList().ForEach(x => x.IsHitTestVisible = false);
            }
            if (!string.IsNullOrEmpty(Model.IIdMaDonVi))
            {
                _agencies.ToList().ForEach(x => x.IsHitTestVisible = false);
                AgencyModel agency = _agencies.Where(x => x.Id == Model.IIdMaDonVi).FirstOrDefault();
                if (agency != null)
                    agency.Selected = true;
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
            if (!BudgetIndexes.Any(x => x.IsSelected))
            {
                MessageBoxHelper.Warning(Resources.AlertLNSEmpty);
                return;
            }
            if (SelectedQuarterMonth == null)
            {
                MessageBoxHelper.Warning(Resources.AlertQuarterMonthEmpty);
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
                var condition = PredicateBuilder.True<NsQtChungTuChiTiet>();
                condition = condition.And(x => x.IIdQtchungTu == Model.Id);
                condition = condition.And(x => listLNSHasDataUnchecked.Contains(x.SLns));
                var listChungTuChiTiet = _chungTuChiTietService.FindByCondition(condition).ToList();
                _chungTuChiTietService.RemoveRange(listChungTuChiTiet);

                Model.FTongTuChiDeNghi -= listChungTuChiTiet.Sum(x => x.FTuChiDeNghi);
                Model.FTongTuChiPheDuyet -= listChungTuChiTiet.Sum(x => x.FTuChiPheDuyet);
            }


            NsQtChungTu chungTu = new NsQtChungTu();
            Model.SThangQuyMoTa = SelectedQuarterMonth == null ? string.Empty : SelectedQuarterMonth.DisplayItem;
            Model.IThangQuy = SelectedQuarterMonth == null ? 0 : Convert.ToInt32(SelectedQuarterMonth.ValueItem);
            Model.IThangQuyLoai = (int)QuarterMonthValue;
            Model.SDslns = BudgetCatalogSelectedToStringConvert.GetValueSelected(BudgetIndexes, true);

            //kiểm tra tồn tại chứng từ theo đơn vị, tháng, LNS
            List<string> listLNSExist = _chungTuService.FindLNSExist(
                new SettlementVoucherCriteria
                {
                    SettlementType = SettlementType.EXPENSE_BUDGET,
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    QuarterMonth = Convert.ToInt32(Model.IThangQuy),
                    QuarterMonthType = Model.IThangQuyLoai,
                    AgencyId = Model.IIdMaDonVi,
                    AdjustType = 1
                }
                , Model.Id, BudgetIndexes.Where(x => x.IsSelected && !x.IsParent).Select(x => x.Lns).ToList());

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

            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                Model.INamNganSach = _sessionInfo.YearOfBudget;
                Model.INamLamViec = _sessionInfo.YearOfWork;
                Model.IIdMaNguonNganSach = _sessionInfo.Budget;
                Model.SLoai = SettlementType.EXPENSE_BUDGET;
                Model.SNguoiTao = _sessionInfo.Principal;
                Model.DNgayTao = DateTime.Now;

                if (IsAdjustChecked)
                {
                    Model.ILoaiChungTu = 2;
                    List<string> listLNSAdjustExist = _chungTuService.FindLNSExist(
                        new SettlementVoucherCriteria
                        {
                            SettlementType = SettlementType.EXPENSE_BUDGET,
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
                            SettlementType = SettlementType.EXPENSE_BUDGET,
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
                    Model.ILoaiChungTu = 1;
                }

                //ở trạng thái tổng hợp
                if (IsAggregate)
                {
                    Model.FTongTuChiDeNghi = AggregateSettlementVouchers.Sum(x => x.FTongTuChiDeNghi);
                    Model.FTongTuChiPheDuyet = AggregateSettlementVouchers.Sum(x => x.FTongTuChiPheDuyet);
                    Model.STongHop = string.Join(",", AggregateSettlementVouchers.Select(x => x.SSoChungTu).OrderBy(x => x).ToList());
                }

                chungTu = _mapper.Map<NsQtChungTu>(Model);
                _chungTuService.Add(chungTu);

                //tạo chứng từ chi tiết khi tổng hợp
                if (IsAggregate)
                    CreateSettlementVoucherDetail(_mapper.Map<SettlementVoucherModel>(chungTu));
            }
            else
            {
                Model.SNguoiSua = _sessionInfo.Principal;
                Model.DNgaySua = DateTime.Now;
                chungTu = _chungTuService.FindById(Model.Id);
                _mapper.Map(Model, chungTu);
                _chungTuService.Update(chungTu);
                _chungTuChiTietService.UpdateMonth(Model.Id, Convert.ToInt32(SelectedQuarterMonth.ValueItem), (int)QuarterMonthValue, _sessionInfo.Principal);
            }

            DialogHost.Close(SettlementScreen.ROOT_DIALOG);
            //DialogHost.CloseDialogCommand.Execute(null, null);
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
                VoucherIds = string.Join(",", AggregateSettlementVouchers.Select(x => x.Id.ToString()).ToList()),
                VoucherId = settlementVoucher.Id.ToString(),
                YearOfBudget = settlementVoucher.INamNganSach,
                BudgetSource = settlementVoucher.IIdMaNguonNganSach,
                YearOfWork = settlementVoucher.INamLamViec,
                Type = SettlementType.EXPENSE_BUDGET,
                QuarterMonthType = settlementVoucher.IThangQuyLoai,
                QuarterMonth = settlementVoucher.IThangQuy,
                AgencyId = settlementVoucher.IIdMaDonVi,
                UserName = _sessionInfo.Principal
            };
            _chungTuChiTietService.AddAggregateVoucherDetail(creation);
        }
    }
}
