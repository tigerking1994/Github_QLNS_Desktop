using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.SqlServer.Management.Smo;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH
{
    public class QuyetToanThuDialogViewModel : DialogViewModelBase<BhQttBHXHModel>
    {
        private readonly IQttBHXHService _chungTuService;
        private readonly IQttBHXHChiTietService _chungTuChiTietService;
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
        public override Type ContentType => typeof(QuyetToanThuDialog);
        public bool bDeleteDetail { get; set; }
        public bool IsInBudget { get; set; }
        public bool IsVisibleReportType { get; set; }
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
                if (agency != null && !IsAggregate)
                {
                    Model.IIDMaDonVi = agency.Id;
                    //Model.STenDonVi = agency.AgencyName;
                    CheckParentUnit();
                }
                else
                {
                    IsVisibleReportType = false;
                }
                OnPropertyChanged(nameof(IsVisibleReportType));
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
                LoadCurrentYear();
                LoadMonths();
                LoadQuarters();
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
                OnPropertyChanged(nameof(SelectedAgencyCount));
            }
        }
        #endregion

        public Guid Id;
        public int VoucherNoIndex;
        public DonVi AggregateAgency;
        public List<BhQttBHXHModel> AggregateQTTVouchers { get; set; }
        public string AggregateLNS;

        private bool _isAggregate;
        public bool IsAggregate
        {
            get => _isAggregate;
            set => SetProperty(ref _isAggregate, value);
        }

        private ObservableCollection<ComboboxItem> _cbxQttLoaiBaoCao;
        public ObservableCollection<ComboboxItem> CbxQttLoaiBaoCao
        {
            get => _cbxQttLoaiBaoCao;
            set
            {
                SetProperty(ref _cbxQttLoaiBaoCao, value);
            }
        }

        private ComboboxItem _cbxQttLoaiBaoCaoSelected;
        public ComboboxItem QttLoaiBaoCaoSelected
        {
            get => _cbxQttLoaiBaoCaoSelected;
            set
            {
                SetProperty(ref _cbxQttLoaiBaoCaoSelected, value);
            }
        }

        public bool IsEdit => Id == Guid.Empty && !IsAggregate;

        public QuyetToanThuDialogViewModel(IQttBHXHService chungTuService,
                                            INsDonViService donViService,
                                            IMapper mapper,
                                            ISessionService sessionService,
                                            IBhDmMucLucNganSachService mucLucNganSachService,
                                            IQttBHXHChiTietService chungTuChiTietService)
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
            LoadMonths();
            LoadQuarters();
            LoadData();
            LoadReportType();
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
                    var month = _sessionInfo.Month;
                    ComboboxItem selectedComboboxItem = new ComboboxItem();
                    if (QuarterYearValue == QuarterMonth.MONTH)
                    {
                        selectedComboboxItem = SelectedQuarterYear != null ? SelectedQuarterYear : QuarterYears.Where(x => x.ValueItem == month.ToString()).FirstOrDefault();
                    }
                    else if (QuarterYearValue == QuarterMonth.QUARTER)
                    {
                        int quarterYearValue = 0;
                        if (month <= 3)
                            quarterYearValue = 3;
                        else if (3 < month && month <= 6)
                            quarterYearValue = 6;
                        else if (6 < month && month <= 9)
                            quarterYearValue = 9;
                        else quarterYearValue = 12;
                        selectedComboboxItem = SelectedQuarterYear != null ? SelectedQuarterYear : QuarterYears.Where(x => x.ValueItem == quarterYearValue.ToString()).FirstOrDefault();
                    }
                    else
                    {
                        selectedComboboxItem = SelectedQuarterYear != null ? SelectedQuarterYear : QuarterYears.Where(x => x.ValueItem == _sessionInfo.YearOfWork.ToString()).FirstOrDefault();
                    }

                    var selectedMonthItem = Convert.ToInt32(selectedComboboxItem.ValueItem);
                    var lstCurrentUnit = _chungTuService.FindCurrentUnits(_sessionInfo.YearOfWork, selectedMonthItem, (int)QuarterYearValue, IsInBudget);
                    listDonVi = _donViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT))
                        .Where(y => !lstCurrentUnit.Contains(y.IIDMaDonVi)).ToList();
                }
                Agencies = new ObservableCollection<AgencyModel>();
                Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
            }
            //Update
            else
            {
                listDonVi = _donViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).ToList();
                Agencies = new ObservableCollection<AgencyModel>();
                Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
                var chungTu = _chungTuService.FindById(Id);
                if (!string.IsNullOrEmpty(chungTu.IIDMaDonVi))
                {
                    var agency = Agencies.Where(x => x.IIDMaDonVi == chungTu.IIDMaDonVi).FirstOrDefault();
                    if (agency != null)
                        agency.Selected = true;
                }
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

        private void LoadReportType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = SettlementReportType.ReportType[SettlementReportTypeNum.Detail], ValueItem = ((int)SettlementReportTypeNum.Detail).ToString()},
                new ComboboxItem {DisplayItem = SettlementReportType.ReportType[SettlementReportTypeNum.Aggregate], ValueItem = ((int)SettlementReportTypeNum.Aggregate).ToString()}
            };

            CbxQttLoaiBaoCao = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.ILoai.HasValue)
            {
                _cbxQttLoaiBaoCaoSelected = CbxQttLoaiBaoCao.Single(item => item.ValueItem.Equals(Model.ILoai.ToString()));
            }
            else _cbxQttLoaiBaoCaoSelected = CbxQttLoaiBaoCao.Last();
        }

        private bool CheckParentUnit()
        {
            if (Model.IIDMaDonVi != null)
            {
                IsVisibleReportType = _donViService.IsDonViCha(Model.IIDMaDonVi, _sessionInfo.YearOfWork);
            }
            return IsVisibleReportType;
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

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
        }

        private void LoadQuarterYears()
        {
            int quarterYearValue = 0;
            int month = _sessionInfo.Month;
            int year = _sessionInfo.YearOfWork;

            if (QuarterYearValue == QuarterMonth.MONTH)
            {
                QuarterYears = _months;
                QuarterYearHint = "Chọn tháng";
                quarterYearValue = month;
            }
            else if (QuarterYearValue == QuarterMonth.QUARTER)
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
            if (Id != Guid.Empty)
                SelectedQuarterYear = QuarterYears.Where(x => x.ValueItem == Model.IQuyNam.ToString()).FirstOrDefault();
            else
                SelectedQuarterYear = QuarterYears.Where(x => x.ValueItem == quarterYearValue.ToString()).FirstOrDefault();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                Model = new BhQttBHXHModel
                {
                    SSoChungTu = "QTT-" + VoucherNoIndex.ToString().PadLeft(3, '0'),
                    DNgayChungTu = DateTime.Now,
                    IIDMaDonVi = string.Empty
                };
                QuarterYearValue = QuarterMonth.MONTH;

                //tổng hợp chứng từ
                if (IsAggregate)
                {
                    Model.SSoChungTu = "QTT-" + VoucherNoIndex.ToString().PadLeft(3, '0');
                    Model.IIDMaDonVi = AggregateAgency.IIDMaDonVi;
                    Model.IQuyNam = AggregateQTTVouchers.Select(x => x.IQuyNam).FirstOrDefault();
                    Model.IQuyNamLoai = AggregateQTTVouchers.Select(x => x.IQuyNamLoai).FirstOrDefault();
                    Model.SQuyNamMoTa = AggregateQTTVouchers.Select(x => x.SQuyNamMoTa).FirstOrDefault();
                }
            }
            else
            {
                BhQttBHXH chungTu = _chungTuService.FindById(Id);
                Model = _mapper.Map<BhQttBHXHModel>(chungTu);
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
            if (!Agencies.Where(x => x.Selected).Any())
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            var selectedQuarterYear = SelectedQuarterYear == null ? 0 : Convert.ToInt32(SelectedQuarterYear.ValueItem);

            if (bDeleteDetail)
            {
                var condition = PredicateBuilder.True<BhQttBHXHChiTiet>();
                condition = condition.And(x => x.QttBHXHId == Model.Id);
                var listChungTuChiTiet = _chungTuChiTietService.FindByCondition(condition).ToList();
                _chungTuChiTietService.RemoveRange(listChungTuChiTiet);

                Model.FDuToan -= listChungTuChiTiet.Sum(x => x.FDuToan);
                Model.FDaQuyetToan -= listChungTuChiTiet.Sum(x => x.FDaQuyetToan);
                Model.FConLai -= listChungTuChiTiet.Sum(x => x.FConLai);
                Model.FThuBHXHNLD -= listChungTuChiTiet.Sum(x => x.FThuBHXHNLD);
                Model.FThuBHXHNSD -= listChungTuChiTiet.Sum(x => x.FThuBHXHNSD);
                Model.FTongSoPhaiThuBHXH -= listChungTuChiTiet.Sum(x => x.FTongSoPhaiThuBHXH);
                Model.FThuBHYTNLD -= listChungTuChiTiet.Sum(x => x.FThuBHYTNLD);
                Model.FThuBHYTNSD -= listChungTuChiTiet.Sum(x => x.FThuBHYTNSD);
                Model.FTongSoPhaiThuBHYT -= listChungTuChiTiet.Sum(x => x.FTongSoPhaiThuBHYT);
                Model.FThuBHTNNLD -= listChungTuChiTiet.Sum(x => x.FThuBHTNNLD);
                Model.FThuBHTNNSD -= listChungTuChiTiet.Sum(x => x.FThuBHTNNSD);
                Model.FTongSoPhaiThuBHTN -= listChungTuChiTiet.Sum(x => x.FTongSoPhaiThuBHTN);
                Model.FTongCong -= listChungTuChiTiet.Sum(x => x.FTongCong);
            }

            BhQttBHXH chungTu = new BhQttBHXH();
            Model.IQuyNam = selectedQuarterYear;
            Model.IQuyNamLoai = (int)QuarterYearValue;
            //Model.sDSMLNS = GetValueSelected(BudgetTypeIndexes);
            Model.SQuyNamMoTa = SelectedQuarterYear == null ? string.Empty : SelectedQuarterYear.DisplayItem;
            var quynam = (IsAggregate && AggregateQTTVouchers != null) ? AggregateQTTVouchers.FirstOrDefault().IQuyNam : Convert.ToInt32(SelectedQuarterYear.ValueItem);
            var quynamLoai = (IsAggregate && AggregateQTTVouchers != null) ? AggregateQTTVouchers.FirstOrDefault().IQuyNamLoai : (int)QuarterYearValue;
            var quynamMoTa = (IsAggregate && AggregateQTTVouchers != null) ? AggregateQTTVouchers.FirstOrDefault().SQuyNamMoTa : SelectedQuarterYear.DisplayItem;
            //kiểm tra tồn tại chứng từ theo đơn vị, quy, nam
            List<string> listLNSExist = _chungTuService.FindVoucherLNSExist(
                new BhQttBHXHChiTietCriteria
                {
                    INamLamViec = _sessionInfo.YearOfWork,
                    IQuyNam = quynam,
                    IQuyNamLoai = quynamLoai,
                    IIDMaDonVi = Model.IIDMaDonVi
                }
                //, Model.Id, BhxhLoaiChungTu.BhxhChungTu);
                , Model.Id, IsAggregate ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu);

            if (listLNSExist != null && listLNSExist.Count > 0 && !IsAggregate)
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
                List<BhQttBHXH> listVoucherSummary = new List<BhQttBHXH>();
                quynam = AggregateQTTVouchers != null ? AggregateQTTVouchers.FirstOrDefault().IQuyNam : Convert.ToInt32(SelectedQuarterYear.ValueItem);
                quynamLoai = AggregateQTTVouchers != null ? AggregateQTTVouchers.FirstOrDefault().IQuyNamLoai : Convert.ToInt32(SelectedQuarterYear.ValueItem);
                listVoucherSummary = _chungTuService.FindByCondition(_sessionInfo.YearOfWork, quynam, quynamLoai, BhxhLoaiChungTu.BhxhChungTuTongHop).ToList();
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
                            BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
                            searchCondition.IdQttBhxh = idVoucherSummary;
                            var khtChungTuChiTiets = _chungTuChiTietService.FindVoucherDetailById(searchCondition);
                            _chungTuChiTietService.RemoveRange(khtChungTuChiTiets);
                        }
                        else
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                            return;
                        }
                    }
                    Model.IQSBQNam = AggregateQTTVouchers.Sum(x => x.IQSBQNam);
                    Model.FLuongChinh = AggregateQTTVouchers.Sum(x => x.FLuongChinh);
                    Model.FPhuCapChucVu = AggregateQTTVouchers.Sum(x => x.FPhuCapChucVu);
                    Model.FPCTNNghe = AggregateQTTVouchers.Sum(x => x.FPCTNNghe);
                    Model.FPCTNVuotKhung = AggregateQTTVouchers.Sum(x => x.FPCTNVuotKhung);
                    Model.FNghiOm = AggregateQTTVouchers.Sum(x => x.FNghiOm);
                    Model.FHSBL = AggregateQTTVouchers.Sum(x => x.FHSBL);
                    Model.FTongQuyTienLuongNam = AggregateQTTVouchers.Sum(x => x.FTongQuyTienLuongNam);
                    Model.FDuToan = AggregateQTTVouchers.Sum(x => x.FDuToan);
                    Model.FDaQuyetToan = AggregateQTTVouchers.Sum(x => x.FDaQuyetToan);
                    Model.FConLai = AggregateQTTVouchers.Sum(x => x.FConLai);
                    Model.FThuBHXHNLD = AggregateQTTVouchers.Sum(x => x.FThuBHXHNLD);
                    Model.FThuBHXHNSD = AggregateQTTVouchers.Sum(x => x.FThuBHXHNSD);
                    Model.FTongSoPhaiThuBHXH = AggregateQTTVouchers.Sum(x => x.FTongSoPhaiThuBHXH);
                    Model.FThuBHYTNLD = AggregateQTTVouchers.Sum(x => x.FThuBHYTNLD);
                    Model.FThuBHYTNSD = AggregateQTTVouchers.Sum(x => x.FThuBHYTNSD);
                    Model.FTongSoPhaiThuBHYT = AggregateQTTVouchers.Sum(x => x.FTongSoPhaiThuBHYT);
                    Model.FThuBHTNNLD = AggregateQTTVouchers.Sum(x => x.FThuBHTNNLD);
                    Model.FThuBHTNNSD = AggregateQTTVouchers.Sum(x => x.FThuBHTNNSD);
                    Model.FTongSoPhaiThuBHTN = AggregateQTTVouchers.Sum(x => x.FTongSoPhaiThuBHTN);
                    Model.FTongCong = AggregateQTTVouchers.Sum(x => x.FTongCong);
                    Model.IQuyNam = AggregateQTTVouchers.Select(x => x.IQuyNam).FirstOrDefault();
                    Model.IQuyNamLoai = AggregateQTTVouchers.Select(x => x.IQuyNamLoai).FirstOrDefault();
                    Model.SQuyNamMoTa = AggregateQTTVouchers.Select(x => x.SQuyNamMoTa).FirstOrDefault();
                    Model.STongHop = string.Join(",", AggregateQTTVouchers.Select(x => x.SSoChungTu).OrderBy(x => x).ToList());
                    Model.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                }

                if (_cbxQttLoaiBaoCaoSelected != null && !IsAggregate)
                {
                    var donViSelected = Agencies.FirstOrDefault(n => n.Selected);
                    Model.ILoaiTongHop = (donViSelected.Loai == LoaiDonVi.ROOT && _cbxQttLoaiBaoCaoSelected.ValueItem == ((int)SettlementReportTypeNum.Aggregate).ToString()) ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;
                    if (donViSelected.Loai == "0")
                    {
                        Model.ILoai = int.Parse(_cbxQttLoaiBaoCaoSelected.ValueItem);
                        //if (listVoucherSummary.Any())
                        //{
                        //    var firstVoucherSummary = listVoucherSummary.FirstOrDefault();
                        //    if (!firstVoucherSummary.BIsKhoa)
                        //    {
                        //        MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                        //        if (messageBoxResult == MessageBoxResult.No)
                        //        {
                        //            return;
                        //        }
                        //        var idVoucherSummary = firstVoucherSummary.Id;
                        //        var chungTuSummary = _chungTuService.FindById(idVoucherSummary);
                        //        UpdateChungTuDaTongHop(chungTuSummary);
                        //        _chungTuService.Delete(chungTuSummary);
                        //        BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
                        //        searchCondition.IdQttBhxh = idVoucherSummary;
                        //        var khtChungTuChiTiets = _chungTuChiTietService.FindVoucherDetailById(searchCondition);
                        //        _chungTuChiTietService.RemoveRange(khtChungTuChiTiets);
                        //    }
                        //    else
                        //    {
                        //        MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                        //        return;
                        //    }
                        //}
                    }
                }

                chungTu = _mapper.Map<BhQttBHXH>(Model);
                _chungTuService.Add(chungTu);

                //tạo chứng từ chi tiết khi tổng hợp
                if (IsAggregate)
                    CreateSettlementVoucherDetail(_mapper.Map<BhQttBHXHModel>(chungTu));
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
            BhQttBHXHModel settlementVoucher = _mapper.Map<BhQttBHXHModel>(chungTu);
            SavedAction?.Invoke(settlementVoucher);
        }

        /// <summary>
        /// Tạo chứng từ chi tiết
        /// </summary>
        /// <param name="voucher"></param>
        private void CreateSettlementVoucherDetail(BhQttBHXHModel voucher)
        {
            BhQttBHXHChiTietCriteria creation = new BhQttBHXHChiTietCriteria()
            {
                VoucherIds = string.Join(StringUtils.COMMA, AggregateQTTVouchers.Select(x => x.Id.ToString()).ToList()),
                VoucherId = voucher.Id.ToString(),
                INamLamViec = voucher.INamLamViec,
                IQuyNamLoai = voucher.IQuyNamLoai,
                IQuyNam = voucher.IQuyNam,
                IIDMaDonVi = voucher.IIDMaDonVi,
                UserName = _sessionInfo.Principal
            };
            _chungTuChiTietService.AddAggregateVoucherDetail(creation);
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

        public static string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }

        private void UpdateChungTuDaTongHop(BhQttBHXH chungtu)
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
    }
}
