using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.DefenseBudget.ExportDefenseBudget
{
    public class ExportDefenseBudgetViewModel : DialogViewModelBase<SettlementVoucherModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly INsQtChungTuChiTietService _chungTuChiTietService;
        private readonly IExportService _exportService;

        private List<NsMucLucNganSach> _mucLucNganSaches;
        public ObservableCollection<SettlementVoucherModel> _settlementVoucherSummaries;
        public ObservableCollection<SettlementVoucherModel> _settlementVouchers;
        public List<SettlementVoucherDetailModel> _settlementVoucherDetailExports;


        public override Type ContentType => typeof(View.Budget.Settlement.DefenseBudget.ExportDefenseBudget.ExportDefenseBudget);
        private ObservableCollection<ComboboxItem> _bTieuChiItems = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> BTieuChiItems
        {
            get => _bTieuChiItems;
            set => SetProperty(ref _bTieuChiItems, value);
        }

        private ComboboxItem _bTieuChiSelected;
        public ComboboxItem BTieuChiSelected
        {
            get => _bTieuChiSelected;
            set
            {
                SetProperty(ref _bTieuChiSelected, value);
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _agencySelected;
        public ComboboxItem AgencySelected
        {
            get => _agencySelected;
            set
            {
                SetProperty(ref _agencySelected, value);
            }
        }

        private List<ComboboxItem> _quarters;

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
                LoadValueQuarter();
            }
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        private List<int> ValueQuarter;

        public ImportTabIndex TabIndex;
        public bool IsAggregate;
        public DonVi AggregateAgency;
        public RelayCommand ExportCommand { get; }
        public void LoadTieuChis()
        {

            List<DanhMuc> danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                DanhMuc danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                BTieuChiItems = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi, false));
                _bTieuChiSelected = BTieuChiItems != null ? BTieuChiItems[0] : null;
            }
        }

        /// <summary>
        /// Tạo data cho combobox đơn vị
        /// </summary>
        private void LoadAgencies()
        {
            List<DonVi> listDonVi = new List<DonVi>();
            if (TabIndex == ImportTabIndex.Aggregate)
            {
                listDonVi.Add(AggregateAgency);
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                _agencySelected = _agencies.FirstOrDefault(f => f.ValueItem == _settlementVoucherSummaries.FirstOrDefault().IIdMaDonVi);
                if (_settlementVouchers.Any(s => s.Selected))
                    _agencySelected = _agencies.FirstOrDefault(f => f.ValueItem == _settlementVoucherSummaries.FirstOrDefault(x => x.Selected).IIdMaDonVi);
            }
            else
            {
                listDonVi = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO).ToList();
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                _agencySelected = _agencies.FirstOrDefault(f => f.ValueItem == _settlementVouchers.FirstOrDefault().IIdMaDonVi);
                if (_settlementVouchers.Any(s => s.Selected))
                    _agencySelected = _agencies.FirstOrDefault(f => f.ValueItem == _settlementVouchers.FirstOrDefault(x => x.Selected).IIdMaDonVi);
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
                else
                    quarterMonthValue = 12;
            }
            else
            {
                QuarterMonths = _months;
                QuarterMonthHint = "Chọn tháng";
                quarterMonthValue = month;
            }

            if (TabIndex == ImportTabIndex.Aggregate)
            {
                if (_settlementVoucherSummaries.Any(w => w.Selected))
                {
                    SelectedQuarterMonth = QuarterMonths.FirstOrDefault(x => x.ValueItem == _settlementVoucherSummaries.FirstOrDefault(w => w.Selected).IThangQuy.ToString());
                    if (QuarterMonthValue == QuarterMonth.QUARTER)
                    {
                        int quarterMonthSelect = _settlementVoucherSummaries.FirstOrDefault(s => s.Selected).IThangQuy;

                        if (quarterMonthSelect <= 3)
                            quarterMonthSelect = 3;
                        else if (3 < quarterMonthSelect && quarterMonthSelect <= 6)
                            quarterMonthSelect = 6;
                        else if (6 < quarterMonthSelect && quarterMonthSelect <= 9)
                            quarterMonthSelect = 9;
                        else
                            quarterMonthSelect = 12;

                        SelectedQuarterMonth = QuarterMonths.FirstOrDefault(x => x.ValueItem == quarterMonthSelect.ToString());

                    }
                }
                else
                    SelectedQuarterMonth = QuarterMonths.FirstOrDefault(x => x.ValueItem == quarterMonthValue.ToString());
            }
            else
            {
                if (_settlementVouchers.Any(w => w.Selected))
                {
                    SelectedQuarterMonth = QuarterMonths.FirstOrDefault(x => x.ValueItem == _settlementVouchers.FirstOrDefault(w => w.Selected).IThangQuy.ToString());
                    if (QuarterMonthValue == QuarterMonth.QUARTER)
                    {
                        int quarterMonthSelect = _settlementVouchers.FirstOrDefault(s => s.Selected).IThangQuy;

                        if (quarterMonthSelect <= 3)
                            quarterMonthSelect = 3;
                        else if (3 < quarterMonthSelect && quarterMonthSelect <= 6)
                            quarterMonthSelect = 6;
                        else if (6 < quarterMonthSelect && quarterMonthSelect <= 9)
                            quarterMonthSelect = 9;
                        else
                            quarterMonthSelect = 12;

                        SelectedQuarterMonth = QuarterMonths.FirstOrDefault(x => x.ValueItem == quarterMonthSelect.ToString());

                    }
                }
                else
                    SelectedQuarterMonth = QuarterMonths.FirstOrDefault(x => x.ValueItem == quarterMonthValue.ToString());
            }
        }

        private void LoadValueQuarter()
        {
            int month = SelectedQuarterMonth != null ? Int32.Parse(SelectedQuarterMonth.ValueItem) : 0;
            if (QuarterMonthValue == QuarterMonth.QUARTER)
            {
                if (month <= 3)
                    ValueQuarter = new List<int> { 1, 3 };
                else if (3 < month && month <= 6)
                    ValueQuarter = new List<int> { 4, 6 };
                else if (6 < month && month <= 9)
                    ValueQuarter = new List<int> { 7, 9 };
                else
                    ValueQuarter = new List<int> { 10, 12 };
            }
        }

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

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public ExportDefenseBudgetViewModel(ILog logger,
                                            IMapper mapper,
                                            ISessionService sessionService,
                                            IDanhMucService danhMucService,
                                            INsDonViService donViService,
                                            INsMucLucNganSachService mucLucNganSachService,
                                            INsQtChungTuChiTietService chungTuChiTietService,
                                            IExportService exportService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _chungTuChiTietService = chungTuChiTietService;

            ExportCommand = new RelayCommand(obj => OnExportAggregateDataByQuarter());
        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            _sessionInfo = _sessionService.Current;
            _mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            LoadAgencies();
            LoadMonths();
            LoadQuarters();
            LoadQuarterMonths();
        }

        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_TONGHOP);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    string donVi1 = _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper();
                    string donVi2 = _donViService.FindByMaDonViAndNamLamViec(AgencySelected.ValueItem, _sessionInfo.YearOfWork).TenDonVi;

                    if (TabIndex == ImportTabIndex.Aggregate)
                    {
                        List<SettlementVoucherModel> settlementVouchers = _settlementVoucherSummaries.Where(x => x.Selected && IsDonViRoot(x.IIdMaDonVi)).ToList();
                        foreach (SettlementVoucherModel item in settlementVouchers)
                        {
                            _settlementVoucherDetailExports = GetSettlementVoucherDetail(item);
                            CalculateData();
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                            || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0).OrderBy(x => x.SXauNoiMa).ToList();
                            _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
                            switch (BTieuChiSelected.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }
                            RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                            {
                                DonVi1 = donVi1,
                                DonVi2 = donVi2,
                                TieuDe1 = "Chứng từ tổng hợp",
                                TieuDe2 = "Quyết toán ngân sách quốc phòng",
                                ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.ToString("dd/MM/yyyy")),
                                Items = _settlementVoucherDetailExports,
                                MLNS = _mucLucNganSaches,
                            };
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(ctTongHop));
                            }

                            fileNamePrefix = $"{item.SSoChungTu}_{StringUtils.ConvertVN(item.STenDonVi)}";
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    else if (TabIndex == ImportTabIndex.Data)
                    {
                        List<SettlementVoucherModel> settlementVouchers = _settlementVouchers.Where(x => x.Selected).ToList();
                        foreach (SettlementVoucherModel item in settlementVouchers)
                        {
                            _settlementVoucherDetailExports = GetSettlementVoucherDetail(item);
                            CalculateData();
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                            || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0).OrderBy(x => x.SXauNoiMa).ToList();
                            _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
                            switch (BTieuChiSelected.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }
                            RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                            {
                                DonVi1 = donVi1,
                                DonVi2 = donVi2,
                                TieuDe1 = "Chứng từ",
                                TieuDe2 = "Quyết toán ngân sách quốc phòng",
                                ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.ToString("dd/MM/yyyy")),
                                Items = _settlementVoucherDetailExports,
                                MLNS = _mucLucNganSaches
                            };
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(ctTongHop));
                            }

                            fileNamePrefix = $"{item.SSoChungTu}_{StringUtils.ConvertVN(item.STenDonVi)}";
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        _exportService.OpenEncrypt(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExportAggregateDataByQuarter()
        {
            try
            {
                List<SettlementVoucherModel> settlementVouchers = new List<SettlementVoucherModel>();
                int iThangQuyEx = SelectedQuarterMonth != null ? Int32.Parse(SelectedQuarterMonth.ValueItem) : 0;
                if (TabIndex == ImportTabIndex.Aggregate)
                {
                    if (QuarterMonthValue == QuarterMonth.QUARTER)
                        settlementVouchers = _settlementVoucherSummaries.Where(w => w.IThangQuy >= ValueQuarter.FirstOrDefault() && w.IThangQuy <= ValueQuarter.LastOrDefault() && w.IIdMaDonVi == AgencySelected.ValueItem).ToList();
                    else
                        settlementVouchers = _settlementVoucherSummaries.Where(w => w.IThangQuy == iThangQuyEx && w.IIdMaDonVi == AgencySelected.ValueItem).ToList();
                }
                else
                {
                    if (QuarterMonthValue == QuarterMonth.QUARTER)
                        settlementVouchers = _settlementVouchers.Where(w => w.IThangQuy >= ValueQuarter.FirstOrDefault() && w.IThangQuy <= ValueQuarter.LastOrDefault() && w.IIdMaDonVi == AgencySelected.ValueItem).ToList();
                    else
                        settlementVouchers = _settlementVouchers.Where(w => w.IThangQuy == iThangQuyEx && w.IIdMaDonVi == AgencySelected.ValueItem).ToList();
                }
                if (!settlementVouchers.Any())
                {
                    MessageBoxHelper.Info("Dữ liệu chọn không có chứng từ!");

                }


                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_TONGHOP);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    string donVi1 = _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper();
                    string donVi2 = _donViService.FindByMaDonViAndNamLamViec(AgencySelected.ValueItem, _sessionInfo.YearOfWork).TenDonVi;

                    if (TabIndex == ImportTabIndex.Aggregate)
                    {
                        if (QuarterMonthValue == QuarterMonth.QUARTER)
                        {
                            _settlementVoucherDetailExports = GetSettlementVoucherDetailByQuarter(settlementVouchers);
                            List<SettlementVoucherDetailModel> dataParentGroups = _settlementVoucherDetailExports.Where(x => x.BHangCha).GroupBy(g => new { g.SXauNoiMa, g.SMoTa }).Select(s => s.First()).ToList();
                            IEnumerable<SettlementVoucherDetailModel> dataChildGroups = _settlementVoucherDetailExports.Where(x => !x.BHangCha).GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SXauNoiMa, g.SLns, g.SL, g.SK, g.SM, g.STm, g.STtm, g.SNg, g.STng, g.STng1, g.STng2, g.STng3, g.SMoTa, g.IsHangCha }).Select(x => new SettlementVoucherDetailModel
                            {
                                IIdMlns = x.Key.IIdMlns,
                                IIdMlnsCha = x.Key.IIdMlnsCha,
                                SXauNoiMa = x.Key.SXauNoiMa,
                                SLns = x.Key.SLns,
                                SL = x.Key.SL,
                                SK = x.Key.SK,
                                SM = x.Key.SM,
                                STm = x.Key.STm,
                                STtm = x.Key.STtm,
                                SNg = x.Key.SNg,
                                STng = x.Key.STng,
                                STng1 = x.Key.STng1,
                                STng2 = x.Key.STng2,
                                STng3 = x.Key.STng3,
                                SMoTa = x.Key.SMoTa,
                                FDuToan = x.Sum(x => x.FDuToan),
                                FDaQuyetToan = x.Sum(x => x.FDaQuyetToan),
                                FSoNgay = x.Sum(x => x.FSoNgay),
                                FSoNguoi = x.Sum(x => x.FSoNguoi),
                                FSoLuot = x.Sum(x => x.FSoLuot),
                                FTuChiDeNghi = x.Sum(x => x.FTuChiDeNghi),
                                FTuChiPheDuyet = x.Sum(x => x.FTuChiPheDuyet),
                                FDuToanOrigin = x.Sum(x => x.FDuToanOrigin),
                                SGhiChu = x.First().SGhiChu,
                                IIdMaDonVi = x.First().IIdMaDonVi,
                                STenDonVi = x.First().STenDonVi,
                                IThangQuy = x.First().IThangQuy,
                                SChiTietToi = x.First().SChiTietToi,
                                FDeNghiChuyenNamSau = x.Sum(x => x.FDeNghiChuyenNamSau),
                                FChuyenNamSauDaCap = x.Sum(x => x.FChuyenNamSauDaCap),
                            });
                            List<SettlementVoucherDetailModel> dataGroups = dataParentGroups;
                            dataGroups.AddRange(dataChildGroups);
                            _settlementVoucherDetailExports = dataGroups.OrderBy(s => s.SXauNoiMa).ToList();
                            CalculateData();
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0
                                                                || x.FSoNgay != 0 || x.FSoNguoi != 0 || x.FSoLuot != 0
                                                                || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0 || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0
                                                                || x.FDeNghiChuyenNamSau != 0).OrderBy(x => x.SXauNoiMa).ToList();
                            _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
                            switch (BTieuChiSelected.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }
                            RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                            {
                                DonVi1 = donVi1,
                                DonVi2 = donVi2,
                                TieuDe1 = "Chứng từ",
                                TieuDe2 = "Quyết toán ngân sách quốc phòng",
                                ThoiGian = string.Format("Ngày chứng từ: {0}", settlementVouchers.FirstOrDefault().DNgayChungTu.ToString("dd/MM/yyyy")),
                                Items = _settlementVoucherDetailExports,
                                MLNS = _mucLucNganSaches
                            };
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(ctTongHop));
                            }

                            fileNamePrefix = $"{NSConstants.QUYET_TOAN_QUY}_{StringUtils.ConvertVN(donVi2)}";
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                        }
                        else
                        {

                            foreach (SettlementVoucherModel item in settlementVouchers)
                            {
                                _settlementVoucherDetailExports = GetSettlementVoucherDetail(item);
                                CalculateData();
                                _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                    || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0
                                                                    || x.FSoNgay != 0 || x.FSoNguoi != 0 || x.FSoLuot != 0
                                                                    || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0 || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0 || x.FDeNghiChuyenNamSau != 0).OrderBy(x => x.SXauNoiMa).ToList();
                                _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
                                switch (BTieuChiSelected.ValueItem)
                                {
                                    case nameof(MLNSFiled.NG):
                                        _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                        _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                        break;
                                    case nameof(MLNSFiled.TNG):
                                        _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                        _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                        break;
                                    case nameof(MLNSFiled.TNG1):
                                        _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                        _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                        break;
                                    case nameof(MLNSFiled.TNG2):
                                        _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                        _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                        break;
                                }
                                RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                                {
                                    DonVi1 = donVi1,
                                    DonVi2 = donVi2,
                                    TieuDe1 = "Chứng từ",
                                    TieuDe2 = "Quyết toán ngân sách quốc phòng",
                                    ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.ToString("dd/MM/yyyy")),
                                    Items = _settlementVoucherDetailExports,
                                    MLNS = _mucLucNganSaches
                                };
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                                {
                                    data.Add(prop.Name, prop.GetValue(ctTongHop));
                                }

                                fileNamePrefix = $"{item.SSoChungTu}_{StringUtils.ConvertVN(item.STenDonVi)}";
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                                results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                            }
                        }
                    }
                    else if (TabIndex == ImportTabIndex.Data)
                    {
                        if (QuarterMonthValue == QuarterMonth.QUARTER)
                        {
                            _settlementVoucherDetailExports = GetSettlementVoucherDetailByQuarter(settlementVouchers);
                            List<SettlementVoucherDetailModel> dataParentGroups = _settlementVoucherDetailExports.Where(x => x.BHangCha).GroupBy(g => new { g.SXauNoiMa, g.SMoTa }).Select(s => s.First()).ToList();
                            IEnumerable<SettlementVoucherDetailModel> dataChildGroups = _settlementVoucherDetailExports.Where(x => !x.BHangCha).GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SXauNoiMa, g.SLns, g.SL, g.SK, g.SM, g.STm, g.STtm, g.SNg, g.STng, g.STng1, g.STng2, g.STng3, g.SMoTa, g.IsHangCha }).Select(x => new SettlementVoucherDetailModel
                            {
                                IIdMlns = x.Key.IIdMlns,
                                IIdMlnsCha = x.Key.IIdMlnsCha,
                                SXauNoiMa = x.Key.SXauNoiMa,
                                SLns = x.Key.SLns,
                                SL = x.Key.SL,
                                SK = x.Key.SK,
                                SM = x.Key.SM,
                                STm = x.Key.STm,
                                STtm = x.Key.STtm,
                                SNg = x.Key.SNg,
                                STng = x.Key.STng,
                                STng1 = x.Key.STng1,
                                STng2 = x.Key.STng2,
                                STng3 = x.Key.STng3,
                                SMoTa = x.Key.SMoTa,
                                FDuToan = x.Sum(x => x.FDuToan),
                                FDaQuyetToan = x.Sum(x => x.FDaQuyetToan),
                                FSoNgay = x.Sum(x => x.FSoNgay),
                                FSoNguoi = x.Sum(x => x.FSoNguoi),
                                FSoLuot = x.Sum(x => x.FSoLuot),
                                FTuChiDeNghi = x.Sum(x => x.FTuChiDeNghi),
                                FTuChiPheDuyet = x.Sum(x => x.FTuChiPheDuyet),
                                FDuToanOrigin = x.Sum(x => x.FDuToanOrigin),
                                SGhiChu = x.First().SGhiChu,
                                IIdMaDonVi = x.First().IIdMaDonVi,
                                STenDonVi = x.First().STenDonVi,
                                IThangQuy = x.First().IThangQuy,
                                SChiTietToi = x.First().SChiTietToi,
                                FDeNghiChuyenNamSau = x.Sum(x => x.FDeNghiChuyenNamSau),
                                FChuyenNamSauDaCap = x.Sum(x => x.FChuyenNamSauDaCap),
                            });
                            List<SettlementVoucherDetailModel> dataGroups = dataParentGroups;
                            dataGroups.AddRange(dataChildGroups);
                            _settlementVoucherDetailExports = dataGroups.OrderBy(s => s.SXauNoiMa).ToList();
                            CalculateData();
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0
                                                                || x.FSoNgay != 0 || x.FSoNguoi != 0 || x.FSoLuot != 0
                                                                || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0 || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0 || x.FDeNghiChuyenNamSau != 0).OrderBy(x => x.SXauNoiMa).ToList();
                            _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
                            switch (BTieuChiSelected.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                    _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }
                            RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                            {
                                DonVi1 = donVi1,
                                DonVi2 = donVi2,
                                TieuDe1 = "Chứng từ",
                                TieuDe2 = "Quyết toán ngân sách quốc phòng",
                                ThoiGian = string.Format("Ngày chứng từ: {0}", settlementVouchers.FirstOrDefault().DNgayChungTu.ToString("dd/MM/yyyy")),
                                Items = _settlementVoucherDetailExports,
                                MLNS = _mucLucNganSaches
                            };
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(ctTongHop));
                            }

                            fileNamePrefix = $"{NSConstants.QUYET_TOAN_QUY}_{StringUtils.ConvertVN(donVi2)}";
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                        }
                        else
                        {

                            foreach (SettlementVoucherModel item in settlementVouchers)
                            {
                                _settlementVoucherDetailExports = GetSettlementVoucherDetail(item);
                                CalculateData();
                                _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                    || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0
                                                                    || x.FSoNgay != 0 || x.FSoNguoi != 0 || x.FSoLuot != 0
                                                                    || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0 || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0 || x.FDeNghiChuyenNamSau != 0).OrderBy(x => x.SXauNoiMa).ToList();
                                _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);
                                switch (BTieuChiSelected.ValueItem)
                                {
                                    case nameof(MLNSFiled.NG):
                                        _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                        _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                        break;
                                    case nameof(MLNSFiled.TNG):
                                        _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                        _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                        break;
                                    case nameof(MLNSFiled.TNG1):
                                        _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                        _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                        break;
                                    case nameof(MLNSFiled.TNG2):
                                        _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                        _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                        break;
                                }
                                RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                                {
                                    DonVi1 = donVi1,
                                    DonVi2 = donVi2,
                                    TieuDe1 = "Chứng từ",
                                    TieuDe2 = "Quyết toán ngân sách quốc phòng",
                                    ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.ToString("dd/MM/yyyy")),
                                    Items = _settlementVoucherDetailExports,
                                    MLNS = _mucLucNganSaches
                                };
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                                {
                                    data.Add(prop.Name, prop.GetValue(ctTongHop));
                                }

                                fileNamePrefix = $"{item.SSoChungTu}_{StringUtils.ConvertVN(item.STenDonVi)}";
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                                results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                            }
                        }

                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        _exportService.OpenEncrypt(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private List<SettlementVoucherDetailModel> GetSettlementVoucherDetail(SettlementVoucherModel settlementVoucher)
        {
            string iIdMaDonViExport = AgencySelected.ValueItem;
            int iThangQuyEx = SelectedQuarterMonth != null ? Int32.Parse(SelectedQuarterMonth.ValueItem) : 0;
            SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
            {
                VoucherId = settlementVoucher.Id,
                LNS = string.Join(",", settlementVoucher.SDslns),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                Type = settlementVoucher.SLoai,
                BudgetSource = 1,
                AgencyId = iIdMaDonViExport,
                VoucherDate = settlementVoucher.DNgayChungTu,
                UserName = _sessionInfo.Principal,
                QuarterMonth = iThangQuyEx.ToString(),
            };
            List<QtChungTuChiTietQuery> listChungTuChiTiet = _chungTuChiTietService.FindByCondition(searchCondition);
            if (QuarterMonthValue == QuarterMonth.QUARTER)
            {
                List<QtChungTuChiTietQuery> listRemove = listChungTuChiTiet.Where(w => !w.BHangCha && !(w.IThangQuy >= ValueQuarter.FirstOrDefault() && w.IThangQuy <= ValueQuarter.LastOrDefault() && w.IIdMaDonVi == iIdMaDonViExport)).ToList();
                listChungTuChiTiet = listChungTuChiTiet.Except(listRemove).ToList();
            }
            else
            {
                List<QtChungTuChiTietQuery> listRemove = listChungTuChiTiet.Where(w => !w.BHangCha && w.IThangQuy != iThangQuyEx).ToList();
                listChungTuChiTiet = listChungTuChiTiet.Except(listRemove).ToList();
            }
            return _mapper.Map<List<SettlementVoucherDetailModel>>(listChungTuChiTiet);
        }

        private List<SettlementVoucherDetailModel> GetSettlementVoucherDetailByQuarter(List<SettlementVoucherModel> settlementVouchers)
        {
            string iIdMaDonViExport = AgencySelected.ValueItem;
            int iThangQuyEx = SelectedQuarterMonth != null ? Int32.Parse(SelectedQuarterMonth.ValueItem) : 0;
            List<QtChungTuChiTietQuery> listChungTuChiTiet = new List<QtChungTuChiTietQuery>();
            foreach (SettlementVoucherModel item in settlementVouchers)
            {
                SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
                {
                    VoucherId = item.Id,
                    LNS = string.Join(",", item.SDslns),
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    Type = item.SLoai,
                    BudgetSource = 1,
                    AgencyId = iIdMaDonViExport,
                    VoucherDate = item.DNgayChungTu,
                    UserName = _sessionInfo.Principal,
                    QuarterMonth = iThangQuyEx.ToString(),
                };
                List<QtChungTuChiTietQuery> listChungTuChiTietItem = _chungTuChiTietService.FindByCondition(searchCondition);
                if (QuarterMonthValue == QuarterMonth.QUARTER)
                {
                    List<QtChungTuChiTietQuery> listRemove = listChungTuChiTietItem.Where(w => !w.BHangCha && !(w.IThangQuy >= ValueQuarter.FirstOrDefault() && w.IThangQuy <= ValueQuarter.LastOrDefault() && w.IIdMaDonVi == iIdMaDonViExport)).ToList();
                    listChungTuChiTietItem = listChungTuChiTietItem.Except(listRemove).ToList();
                    listChungTuChiTiet.AddRange(listChungTuChiTietItem);
                }
                else
                {
                    List<QtChungTuChiTietQuery> listRemove = listChungTuChiTietItem.Where(w => !w.BHangCha && w.IThangQuy != iThangQuyEx).ToList();
                    listChungTuChiTietItem = listChungTuChiTietItem.Except(listRemove).ToList();
                    listChungTuChiTiet.AddRange(listChungTuChiTietItem);

                }
            }

            return _mapper.Map<List<SettlementVoucherDetailModel>>(listChungTuChiTiet);
        }

        private void CalculateData()
        {
            // Reset value parrent
            _settlementVoucherDetailExports.Where(x => x.IsHangCha)
                .Select(x => { x.FDuToan = x.FDuToanOrigin != 0 ? x.FDuToanOrigin : 0; x.FDaQuyetToan = 0; x.FTuChiDeNghi = 0; x.FTuChiPheDuyet = 0; x.FChuyenNamSauDaCap = 0; x.FDeNghiChuyenNamSau = 0; return x; }).ToList();
            // Caculate value child
            foreach (SettlementVoucherDetailModel item in _settlementVoucherDetailExports.Where(x => x.FDuToanOrigin != 0 || (x.IsEditable && (x.FDaQuyetToan != 0 || x.FTuChiPheDuyet != 0 || x.FTuChiDeNghi != 0 || x.FChuyenNamSauDaCap.GetValueOrDefault() != 0 || x.FChuyenNamSauChuaCap.GetValueOrDefault() != 0 || x.FDeNghiChuyenNamSau != 0))))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(SettlementVoucherDetailModel currentItem, SettlementVoucherDetailModel selfItem)
        {
            SettlementVoucherDetailModel parentItem = _settlementVoucherDetailExports.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            if (selfItem.FDuToanOrigin != 0)
                parentItem.FDuToan += selfItem.FDuToan;
            if (parentItem.FDuToan != 0 && currentItem.FDuToan == 0)
            {
                currentItem.IsCalculateConLai = false;
                OnPropertyChanged(nameof(currentItem.FConLai));
            }
            parentItem.FDaQuyetToan += selfItem.FDaQuyetToan;
            parentItem.FTuChiDeNghi += selfItem.FTuChiDeNghi;
            parentItem.FTuChiPheDuyet += selfItem.FTuChiPheDuyet;
            parentItem.FDeNghiChuyenNamSau += selfItem.FDeNghiChuyenNamSau;
            parentItem.FChuyenNamSauDaCap += selfItem.FChuyenNamSauDaCap.GetValueOrDefault();
            CalculateParent(parentItem, selfItem);
        }
    }
}
