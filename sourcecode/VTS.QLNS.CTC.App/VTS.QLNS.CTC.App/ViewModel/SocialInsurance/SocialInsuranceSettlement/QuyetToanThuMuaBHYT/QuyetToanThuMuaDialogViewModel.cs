using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT
{
    public class QuyetToanThuMuaDialogViewModel : DialogViewModelBase<BhQttmBHYTModel>
    {
        private readonly IQttmBHYTService _chungTuService;
        private readonly IQttmBHYTChiTietService _chungTuChiTietService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IBhDmMucLucNganSachService _mucLucNganSachService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private List<ComboboxItem> _quarters;
        private List<ComboboxItem> _months;
        private List<ComboboxItem> _currentyear;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public override Type ContentType => typeof(QuyetToanThuMuaDialog);
        public bool bDeleteDetail { get; set; }

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
                    Model.IIDMaDonVi = agency.Id;
                    Model.STenDonVi = agency.MaTenDonVi;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        #endregion

        #region list LNS
        private ObservableCollection<BhDmMucLucNganSachModel> _budgetTypeIndexes;
        public ObservableCollection<BhDmMucLucNganSachModel> BudgetTypeIndexes
        {
            get => _budgetTypeIndexes;
            set => SetProperty(ref _budgetTypeIndexes, value);
        }

        private string _searchBhIndexText;
        public string SearchBhIndexText
        {
            set
            {
                if (SetProperty(ref _searchBhIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                    OnPropertyChanged(nameof(SelectedBudgetTypeIndexCount));
                }
            }
        }

        public string SelectedBudgetTypeIndexCount
        {
            get
            {
                int totalCount = BudgetTypeIndexes != null ? BudgetTypeIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetTypeIndexes != null ? BudgetTypeIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetTypeIndex;
        public bool IsSelectAllBudgetTypeIndex
        {
            get => BudgetTypeIndexes.Where(x => x.IsFilter).All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetTypeIndex, value);
                foreach (BhDmMucLucNganSachModel item in BudgetTypeIndexes.Where(x => x.IsFilter))
                {
                    item.IsSelected = _isSelectAllBudgetTypeIndex;
                }
            }
        }
        #endregion

        #region combobox quý, năm
        private List<ComboboxItem> _QuarterYears;
        public List<ComboboxItem> QuarterYears
        {
            get => _QuarterYears;
            set => SetProperty(ref _QuarterYears, value);
        }

        private QuarterMonth _QuarterYearValue;
        public QuarterMonth QuarterYearValue
        {
            get => _QuarterYearValue;
            set
            {
                SetProperty(ref _QuarterYearValue, value);
                LoadQuarterYears();
            }
        }

        private string _QuarterYearHint;
        public string QuarterYearHint
        {
            get => _QuarterYearHint;
            set => SetProperty(ref _QuarterYearHint, value);
        }

        private ComboboxItem _selectedQuarterYear;
        public ComboboxItem SelectedQuarterYear
        {
            get => _selectedQuarterYear;
            set
            {
                SetProperty(ref _selectedQuarterYear, value);
                LoadAgencies();
            }
        }
        #endregion

        public Guid Id;
        public int VoucherNoIndex;
        public DonVi AggregateAgency;
        public List<BhQttmBHYTModel> AggregateBhVouchers { get; set; }
        public string AggregateLNS;

        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }

        private bool _isAggregate;
        public bool IsAggregate
        {
            get => _isAggregate;
            set
            {
                SetProperty(ref _isAggregate, value);
                _isEnable = !_isAggregate;
            }
        }

        public bool IsEdit => Id == Guid.Empty && !IsAggregate;
        public QuyetToanThuMuaDialogViewModel(IQttmBHYTService chungTuService,
                                            INsDonViService donViService,
                                            IMapper mapper,
                                            ISessionService sessionService,
                                            IBhDmMucLucNganSachService mucLucNganSachService,
                                            IQttmBHYTChiTietService chungTuChiTietService)
        {
            _chungTuService = chungTuService;
            _donViService = donViService;
            _mapper = mapper;
            _sessionService = sessionService;
            _mucLucNganSachService = mucLucNganSachService;
            _chungTuChiTietService = chungTuChiTietService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _searchAgencyText = string.Empty;
            _searchBhIndexText = string.Empty;
            LoadCurrentYear();
            LoadQuarters();
            LoadData();
        }

        private void LoadAgencies()
        {
            List<DonVi> listDonVi = new List<DonVi>();
            //Create
            if (Id == Guid.Empty)
            {
                if (IsAggregate)
                {
                    listDonVi.Add(AggregateAgency);
                }
                    
                else
                {
                    var selectedQuarterYear = SelectedQuarterYear != null ? Convert.ToInt32(SelectedQuarterYear.ValueItem) : 0;
                    var lstCurrentUnit = _chungTuService.FindCurrentUnits(_sessionInfo.YearOfWork, selectedQuarterYear, (int)QuarterYearValue);
                    listDonVi = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO + StringUtils.COMMA + LoaiDonVi.ROOT)
                        .Where(y => !lstCurrentUnit.Contains(y.IIDMaDonVi)).ToList();
                }
                Agencies = new ObservableCollection<AgencyModel>();
                Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
            }
            //Update
            else
            {
                listDonVi = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO + StringUtils.COMMA + LoaiDonVi.ROOT).ToList();
                Agencies = new ObservableCollection<AgencyModel>();
                Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
            }
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
        private void LoadCurrentYear()
        {
            _currentyear = new List<ComboboxItem>();
            ComboboxItem month = new ComboboxItem("Năm " + _sessionInfo.YearOfWork, _sessionInfo.YearOfWork.ToString());
            _currentyear.Add(month);

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

        private void LoadQuarterYears()
        {
            int quarterYearValue = 0;
            int month = _sessionInfo.Month;
            int year = _sessionInfo.YearOfWork;
            if (QuarterYearValue == QuarterMonth.QUARTER)
            {
                QuarterYears = _quarters;
                QuarterYearHint = "Chọn quý";
                if (month <= 3)
                    quarterYearValue = 3;
                else if (3 < month && month <= 6)
                    quarterYearValue = 6;
                else if (6 < month && month <= 9)
                    quarterYearValue = 9;
                else quarterYearValue = 12;
            }
            else
            {
                QuarterYears = _currentyear;
                QuarterYearHint = "Chọn năm";
                quarterYearValue = year;
            }
            if (Model.IQuyNam != 0)
                SelectedQuarterYear = QuarterYears.Where(x => x.ValueItem == Model.IQuyNam.ToString()).FirstOrDefault();
            else
                SelectedQuarterYear = QuarterYears.Where(x => x.ValueItem == quarterYearValue.ToString()).FirstOrDefault();
        }

        private void LoadBudgetTypeIndexes()
        {
            List<BhDmMucLucNganSach> listMucLucNganSach = new List<BhDmMucLucNganSach>();

            //tổng hợp chứng từ
            if (IsAggregate)
                listMucLucNganSach = _mucLucNganSachService.FindByListLnsDonVi(AggregateLNS, _sessionInfo.YearOfWork).ToList();
            else
                listMucLucNganSach = _mucLucNganSachService.GetListBhytMucLucNs(_sessionInfo.YearOfWork, BhxhMLNS.THU_MUA_BHYT).ToList();

            BudgetTypeIndexes = new ObservableCollection<BhDmMucLucNganSachModel>();
            BudgetTypeIndexes = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(listMucLucNganSach.Where(x => x.ITrangThai == StatusType.ACTIVE));

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetTypeIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetTypeIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                    {
                        foreach (BhDmMucLucNganSachModel item in BudgetTypeIndexes)
                        {
                            if (item.IIDMLNSCha == model.IIDMLNS)
                            {
                                if (item.IsHitTestVisible)
                                    item.IsSelected = model.IsSelected;
                            }
                        }
                        OnPropertyChanged(nameof(SelectedBudgetTypeIndexCount));
                        OnPropertyChanged(nameof(IsSelectAllBudgetTypeIndex));
                    }
                };
            }

            if (IsAggregate)
                BudgetTypeIndexes.ToList().ForEach(x => x.IsSelected = true);
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchBhIndexText))
                result = item.LNSDisplay.ToLower().Contains(_searchBhIndexText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                Model = new BhQttmBHYTModel
                {
                    SSoChungTu = "QTT-" + VoucherNoIndex.ToString().PadLeft(3, '0'),
                    DNgayChungTu = DateTime.Now,
                    IIDMaDonVi = string.Empty
                };
                QuarterYearValue = QuarterMonth.QUARTER;

                //tổng hợp chứng từ
                if (IsAggregate)
                {
                    Model.SSoChungTu = "QTT-" + VoucherNoIndex.ToString().PadLeft(3, '0');
                    Model.IIDMaDonVi = AggregateAgency.IIDMaDonVi;
                    Model.IQuyNam = AggregateBhVouchers.Select(x => x.IQuyNam).FirstOrDefault();
                    Model.IQuyNamLoai = AggregateBhVouchers.Select(x => x.IQuyNamLoai).FirstOrDefault();
                    Model.SQuyNamMoTa = AggregateBhVouchers.Select(x => x.SQuyNamMoTa).FirstOrDefault();
                }
            }
            else
            {
                BhQttmBHYT chungTu = _chungTuService.FindById(Id);
                Model = _mapper.Map<BhQttmBHYTModel>(chungTu);
                if (Model.IQuyNamLoai == (int)QuarterMonth.YEAR)
                {
                    QuarterYearValue = QuarterMonth.YEAR;
                }
                else if (Model.IQuyNamLoai == (int)QuarterMonth.QUARTER)
                {
                    QuarterYearValue = QuarterMonth.QUARTER;
                }
                else
                {
                    QuarterYearValue = QuarterMonth.MONTH;
                }
                if (!string.IsNullOrEmpty(Model.IIDMaDonVi))
                {
                    AgencyModel agency = _agencies.Where(x => x.Id == Model.IIDMaDonVi).FirstOrDefault();
                    if (agency != null)
                        agency.Selected = true;
                }
                if (Model.IQuyNamLoai == (int)QuarterMonth.YEAR)
                    QuarterYearValue = QuarterMonth.YEAR;
                else QuarterYearValue = QuarterMonth.QUARTER;

                List<string> listLnsHasData = _chungTuChiTietService.GetLnsHasData(new List<Guid> { Model.Id }).ToList();
            }

            if (!string.IsNullOrEmpty(Model.IIDMaDonVi))
            {
                _agencies.ToList().ForEach(x => x.IsHitTestVisible = false);
                AgencyModel agency = _agencies.Where(x => x.Id == Model.IIDMaDonVi).FirstOrDefault();
                if (agency != null)
                    agency.Selected = true;
            }
        }

        private string GetMessageValidateCheckBoxLNS()
        {
            var listLNSHasDataUnchecked = BudgetTypeIndexes.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.SLNS).ToList();
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
            if (string.IsNullOrEmpty(Model.IIDMaDonVi))
            {
                MessageBoxHelper.Warning(Resources.AlertAgencyEmpty);
                return;
            }
            if (SelectedQuarterYear == null && !IsAggregate)
            {
                MessageBoxHelper.Warning(Resources.AlertQuarterYearEmpty);
                return;
            }
            var selectedQuarterYear = SelectedQuarterYear == null ? 0 : Convert.ToInt32(SelectedQuarterYear.ValueItem);

            if (bDeleteDetail)
            {
                var condition = PredicateBuilder.True<BhQttmBHYTChiTiet>();
                condition = condition.And(x => x.VoucherId == Model.Id);
                var listChungTuChiTiet = _chungTuChiTietService.FindByCondition(condition).ToList();
                _chungTuChiTietService.RemoveRange(listChungTuChiTiet);

                Model.FDuToan -= listChungTuChiTiet.Sum(x => x.FDuToan);
                Model.FDaQuyetToan -= listChungTuChiTiet.Sum(x => x.FDaQuyetToan);
                Model.FConLai -= listChungTuChiTiet.Sum(x => x.FConLai);
                Model.FSoPhaiThu -= listChungTuChiTiet.Sum(x => x.FSoPhaiThu);
            }

            BhQttmBHYT chungTu = new BhQttmBHYT();
            Model.IQuyNam = selectedQuarterYear;
            Model.IQuyNamLoai = (int)QuarterYearValue;
            Model.SQuyNamMoTa = SelectedQuarterYear == null ? string.Empty : SelectedQuarterYear.DisplayItem;
            var quynamValue = (IsAggregate && AggregateBhVouchers != null) ? AggregateBhVouchers.FirstOrDefault().IQuyNam : Convert.ToInt32(SelectedQuarterYear.ValueItem);
            var quynamLoai = (IsAggregate && AggregateBhVouchers != null) ? AggregateBhVouchers.FirstOrDefault().IQuyNamLoai : (int)QuarterYearValue;
            var quynamMoTa = (IsAggregate && AggregateBhVouchers != null) ? AggregateBhVouchers.FirstOrDefault().SQuyNamMoTa : SelectedQuarterYear.DisplayItem;
            //kiểm tra tồn tại chứng từ theo đơn vị, qúy, năm
            List<string> listLNSExist = _chungTuService.FindVoucherLNSExist(
                new BhQttmBHYTChiTietCriteria
                {
                    INamLamViec = _sessionInfo.YearOfWork,
                    IQuyNam = quynamValue,
                    IQuyNamLoai = quynamLoai,
                    IIDMaDonVi = Model.IIDMaDonVi
                }
                , Model.Id, BhxhLoaiChungTu.BhxhChungTu);
            if (listLNSExist != null && listLNSExist.Count > 0)
            {
                MessageBoxHelper.Warning(string.Format(Resources.AlertExistSettlementMonthVoucher, Model.IIDMaDonVi, quynamMoTa, string.Join(",", listLNSExist)));
                return;
            }

            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                Model.INamLamViec = _sessionInfo.YearOfWork;
                Model.SNguoiTao = _sessionInfo.Principal;
                Model.DNgayTao = DateTime.Now;
                Model.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTu;
                //ở trạng thái tổng hợp
                List<BhQttmBHYT> listVoucherSummary = new List<BhQttmBHYT>();
                var quynam = AggregateBhVouchers != null ? AggregateBhVouchers.FirstOrDefault().IQuyNam : Convert.ToInt32(SelectedQuarterYear.ValueItem);
                listVoucherSummary = _chungTuService.FindByCondition(_sessionInfo.YearOfWork, quynam, BhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
                if (IsAggregate)
                {
                    if (listVoucherSummary.Any())
                    {
                        var firstVoucherSummary = listVoucherSummary.FirstOrDefault();
                        if (!firstVoucherSummary.BIsKhoa)
                        {
                            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmReplaceAggregateVoucherBHXH, firstVoucherSummary.SQuyNamMoTa));
                            if (messageBoxResult == MessageBoxResult.No)
                            {
                                return;
                            }
                            var idVoucherSummary = firstVoucherSummary.Id;
                            var chungTuSummary = _chungTuService.FindById(idVoucherSummary);
                            UpdateChungTuDaTongHop(chungTuSummary);
                            _chungTuService.Delete(chungTuSummary);
                            BhQttmBHYTChiTietCriteria searchCondition = new BhQttmBHYTChiTietCriteria();
                            searchCondition.VoucherID = idVoucherSummary;
                            var khtChungTuChiTiets = _chungTuChiTietService.FindVoucherDetailById(searchCondition);
                            _chungTuChiTietService.RemoveRange(khtChungTuChiTiets);
                        }
                        else
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                            return;
                        }
                    }
                    Model.FDuToan = AggregateBhVouchers.Sum(x => x.FDuToan);
                    Model.FDaQuyetToan = AggregateBhVouchers.Sum(x => x.FDaQuyetToan);
                    Model.FConLai = AggregateBhVouchers.Sum(x => x.FConLai);
                    Model.FSoPhaiThu = AggregateBhVouchers.Sum(x => x.FSoPhaiThu);
                    Model.STongHop = string.Join(",", AggregateBhVouchers.Select(x => x.SSoChungTu).OrderBy(x => x).ToList());
                    Model.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    Model.IQuyNam = AggregateBhVouchers.Select(x => x.IQuyNam).FirstOrDefault();
                    Model.IQuyNamLoai = AggregateBhVouchers.Select(x => x.IQuyNamLoai).FirstOrDefault();
                    Model.SQuyNamMoTa = AggregateBhVouchers.Select(x => x.SQuyNamMoTa).FirstOrDefault();
                }
                else
                {
                    var donViSelected = Agencies.FirstOrDefault(n => n.Selected);
                    Model.ILoaiTongHop = donViSelected.Loai == LoaiDonVi.ROOT ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                    //if (donViSelected.Loai == "0")
                    //{
                    //    if (listVoucherSummary.Any())
                    //    {
                    //        var firstVoucherSummary = listVoucherSummary.FirstOrDefault();
                    //        if (!firstVoucherSummary.BIsKhoa)
                    //        {
                    //            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                    //            if (messageBoxResult == MessageBoxResult.No)
                    //            {
                    //                return;
                    //            }
                    //            var idVoucherSummary = firstVoucherSummary.Id;
                    //            var chungTuSummary = _chungTuService.FindById(idVoucherSummary);
                    //            UpdateChungTuDaTongHop(chungTuSummary);
                    //            _chungTuService.Delete(chungTuSummary);
                    //            BhQttmBHYTChiTietCriteria searchCondition = new BhQttmBHYTChiTietCriteria();
                    //            searchCondition.VoucherID = idVoucherSummary;
                    //            var khtChungTuChiTiets = _chungTuChiTietService.FindVoucherDetailById(searchCondition);
                    //            _chungTuChiTietService.RemoveRange(khtChungTuChiTiets);
                    //        }
                    //        else
                    //        {
                    //            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                    //            return;
                    //        }
                    //    }
                    //}
                }

                chungTu = _mapper.Map<BhQttmBHYT>(Model);
                _chungTuService.Add(chungTu);

                //tạo chứng từ chi tiết khi tổng hợp
                if (IsAggregate)
                    CreateSettlementVoucherDetail(_mapper.Map<BhQttmBHYTModel>(chungTu));
            }
            else
            {
                Model.SNguoiSua = _sessionInfo.Principal;
                Model.DNgaySua = DateTime.Now;
                chungTu = _chungTuService.FindById(Model.Id);
                _mapper.Map(Model, chungTu);
                _chungTuService.Update(chungTu);
            }

            DialogHost.Close(SystemConstants.ROOT_DIALOG);
            BhQttmBHYTModel settlementVoucher = _mapper.Map<BhQttmBHYTModel>(chungTu);
            SavedAction?.Invoke(settlementVoucher);
        }

        private void UpdateChungTuDaTongHop(BhQttmBHYT chungtu)
        {
            if (!string.IsNullOrEmpty(chungtu.STongHop))
            {
                var lstSoCtChild = chungtu.STongHop.Split(",");
                foreach (var soct in lstSoCtChild)
                {
                    var ctChild = _chungTuService.FindChungTuDaTongHopBySCT(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                    if (ctChild != null)
                    {
                        ctChild.BDaTongHop = false;
                        _chungTuService.Update(ctChild);
                    }
                }
            }
        }

        /// <summary>
        /// Tạo chứng từ chi tiết
        /// </summary>
        /// <param name="voucher"></param>
        private void CreateSettlementVoucherDetail(BhQttmBHYTModel voucher)
        {
            BhQttmBHYTChiTietCriteria creation = new BhQttmBHYTChiTietCriteria()
            {
                VoucherIds = string.Join(StringUtils.COMMA, AggregateBhVouchers.Select(x => x.Id.ToString()).ToList()),
                VoucherId = voucher.Id.ToString(),
                INamLamViec = voucher.INamLamViec,
                IQuyNamLoai = voucher.IQuyNamLoai,
                IQuyNam = voucher.IQuyNam,
                IIDMaDonVi = voucher.IIDMaDonVi,
                UserName = _sessionInfo.Principal
            };
            _chungTuChiTietService.AddAggregateVoucherDetail(creation);
        }

        private static void SetCheckboxSelected(ObservableCollection<BhDmMucLucNganSachModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").ToList();
            foreach (BhDmMucLucNganSachModel item in data)
            {
                item.IsSelected = selectedValues.Contains(item.SLNS);
            }
        }

        private static string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }
    }
}
