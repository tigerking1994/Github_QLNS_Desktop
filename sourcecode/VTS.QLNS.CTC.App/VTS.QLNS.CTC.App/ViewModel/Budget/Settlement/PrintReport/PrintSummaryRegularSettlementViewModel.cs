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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintSummaryRegularSettlementViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private INsQtChungTuService _chungTuService;
        private INsDonViService _donViService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private SessionInfo _sessionInfo;
        private DataInterpretationViewModel DataInterpretationViewModel;
        private VerbalExplanationViewModel VerbalExplanationViewModel;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsQtChungTuChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private List<ReportQtChungTuChiTietQuery> _reportQtChungTuChiTiets;
        private ILog _logger;
        private IMapper _mapper;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;

        public override string Name => "In báo cáo quyết toán thường xuyên - Tổng hợp";
        public override string Title => "In báo cáo quyết toán thường xuyên - Tổng hợp";
        public override string Description => "In báo cáo quyết toán thường xuyên - Tổng hợp tháng hoặc quý";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintSummaryRegularSettlement);

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set => SetProperty(ref _selectedMonth, value);
        }

        private List<ComboboxItem> _quarters;
        public List<ComboboxItem> Quarters
        {
            get => _quarters;
            set => SetProperty(ref _quarters, value);
        }

        private ComboboxItem _selectedQuarter;
        public ComboboxItem SelectedQuarter
        {
            get => _selectedQuarter;
            set => SetProperty(ref _selectedQuarter, value);
        }

        private QuarterMonth _quarterMonthValue;
        public QuarterMonth QuarterMonthValue
        {
            get => _quarterMonthValue;
            set => SetProperty(ref _quarterMonthValue, value);
        }

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set => SetProperty(ref _isSummary, value);
        }

        private string _title1;
        public string Title1
        {
            get => _title1;
            set => SetProperty(ref _title1, value);
        }
        private string _title2;
        public string Title2
        {
            get => _title2;
            set => SetProperty(ref _title2, value);
        }
        private string _title3;
        public string Title3
        {
            get => _title3;
            set => SetProperty(ref _title3, value);
        }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        private bool _isCoverSheet;
        public bool IsCoverSheet
        {
            get => _isCoverSheet;
            set
            {
                SetProperty(ref _isCoverSheet, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool _isData;
        public bool IsData
        {
            get => _isData;
            set
            {
                SetProperty(ref _isData, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool _isDataInterpretation;
        public bool IsDataInterpretation
        {
            get => _isDataInterpretation;
            set
            {
                SetProperty(ref _isDataInterpretation, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool _isVerbalExplanation;
        public bool IsVerbalExplanation
        {
            get => _isVerbalExplanation;
            set
            {
                SetProperty(ref _isVerbalExplanation, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        public bool IsExportEnable
        {
            get => _isCoverSheet || _isData || _isDataInterpretation || _isVerbalExplanation;
        }

        private List<ComboboxItem> _units;
        public List<ComboboxItem> Units
        {
            get => _units;
            set => SetProperty(ref _units, value);
        }

        private ComboboxItem _selectedUnit;
        public ComboboxItem SelectedUnit
        {
            get => _selectedUnit;
            set => SetProperty(ref _selectedUnit, value);
        }

        private ObservableCollection<ComboboxItem> _dataInToiMuc;
        public ObservableCollection<ComboboxItem> DataInToiMuc
        {
            get => _dataInToiMuc;
            set => SetProperty(ref _dataInToiMuc, value);
        }

        private ComboboxItem _selectedInToiMuc;
        public ComboboxItem SelectedInToiMuc
        {
            get => _selectedInToiMuc;
            set => SetProperty(ref _selectedInToiMuc, value);
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintSummaryRegularSettlementViewModel(DataInterpretationViewModel dataInterpretationViewModel,
            VerbalExplanationViewModel verbalExplanationViewModel,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            ISessionService sessionService,
            INsQtChungTuChiTietService chungTuChiTietService,
            IExportService exportService,
            INsQtChungTuChiTietGiaiThichService chungTuChiTietGiaiThichService,
            INsQtChungTuService chungTuService,
            INsDonViService donViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            ILog logger,
            IMapper mapper)
        {
            DataInterpretationViewModel = dataInterpretationViewModel;
            VerbalExplanationViewModel = verbalExplanationViewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _sessionService = sessionService;
            _chungTuChiTietService = chungTuChiTietService;
            _exportService = exportService;
            _chungTuChiTietGiaiThichService = chungTuChiTietGiaiThichService;
            _chungTuService = chungTuService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            _mapper = mapper;

            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            ExportExcelCommand = new RelayCommand(obj => OnExportFile((int)ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj =>
            {
                OnExportFile(ExportType.PDF);
            });
            PrintCommand = new RelayCommand(obj =>
            {
                OnExportFile(ExportType.PDF);
            });
            DataInterpretationCommand = new RelayCommand(obj => OnOpenDataInterpretationDialog());
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            QuarterMonthValue = QuarterMonth.QUARTER;
            IsSummary = false;
            LoadTieuDe();
            LoadQuarterMonth();
            LoadDanhMuc();
            LoadChiTietToi();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe1MoTa;
        }

        private void LoadQuarterMonth()
        {
            _quarters = new List<ComboboxItem>();
            _quarters.Add(new ComboboxItem("Quý I", "3"));
            _quarters.Add(new ComboboxItem("Quý II", "6"));
            _quarters.Add(new ComboboxItem("Quý III", "9"));
            _quarters.Add(new ComboboxItem("Quý IV", "12"));
            _selectedQuarter = _quarters.First();

            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
            _selectedMonth = _months.First();
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE).OrderBy(n => n.SGiaTri)
                .ToList();
            if (listDonViTinh.Count == 0)
                _units.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            OnPropertyChanged(nameof(Units));
            _selectedUnit = Units.ElementAt(0);

            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
            }
        }

        private void OnOpenDataInterpretationDialog()
        {
            string quarterMonth = String.Empty;
            int quarterMonthType = (int)QuarterMonthValue;
            if (QuarterMonthValue == QuarterMonth.QUARTER)
                quarterMonth = SelectedQuarter.ValueItem;
            else quarterMonth = SelectedMonth.ValueItem;

            DataInterpretationViewModel.SettlementVoucher = null;
            DataInterpretationViewModel.SettlementVoucherDetails = null;
            DataInterpretationViewModel.ExplainId = "TX_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType;
            DataInterpretationViewModel.QuarterMonth = Convert.ToInt32(quarterMonth);
            DataInterpretationViewModel.QuarterMonthType = quarterMonthType;
            DataInterpretationViewModel.AgencyId = _sessionInfo.IdDonVi;
            DataInterpretationViewModel.Init();
            var view = new DataInterpretation { DataContext = DataInterpretationViewModel };
            view.ShowDialog();
        }

        private void OnOpenVerbalExplanationDialog()
        {
            string quarterMonth = String.Empty;
            int quarterMonthType = (int)QuarterMonthValue;
            if (QuarterMonthValue == QuarterMonth.QUARTER)
                quarterMonth = SelectedQuarter.ValueItem;
            else quarterMonth = SelectedMonth.ValueItem;

            VerbalExplanationViewModel.SettlementVoucher = null;
            VerbalExplanationViewModel.ExplainId = "TX_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType;
            VerbalExplanationViewModel.QuarterMonth = Convert.ToInt32(quarterMonth);
            VerbalExplanationViewModel.QuarterMonthType = quarterMonthType;
            VerbalExplanationViewModel.AgencyId = _sessionInfo.IdDonVi;
            VerbalExplanationViewModel.Init();
            var view = new VerbalExplanation { DataContext = VerbalExplanationViewModel };
            view.ShowDialog();
        }

        private void OnExportFile(ExportType exportType)
        {

            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = string.Empty;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                    string quarterMonth = string.Empty;
                    string quarterMonthStr = string.Empty;
                    string quarterMonthBefore = string.Empty;
                    int quarterMonthType = (int)QuarterMonth.MONTH;
                    if (QuarterMonthValue == QuarterMonth.QUARTER)
                    {
                        quarterMonthStr = SelectedQuarter.DisplayItem;
                        quarterMonth = string.Join(",", Enumerable.Range(Convert.ToInt32(SelectedQuarter.ValueItem) - 2, 3).ToList());
                        quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(SelectedQuarter.ValueItem) - 2).ToList());
                    }
                    else
                    {
                        quarterMonthStr = SelectedMonth.DisplayItem;
                        quarterMonth = SelectedMonth.ValueItem;
                        quarterMonthBefore = string.Join(",", Enumerable.Range(0, Convert.ToInt32(SelectedMonth.ValueItem)).ToList());
                    }

                    if (IsCoverSheet)
                    {
                        var exportResult = OnExportCoverSheet(quarterMonthStr);
                        if (exportResult != null)
                            results.Add(exportResult);
                    }

                    if (IsDataInterpretation)
                    {
                        var exportResult = OnExportDataInterpretation(exportType, quarterMonth, quarterMonthType);
                        if (exportResult != null)
                            results.Add(exportResult);
                    }

                    ReportSettlementCriteria condition = new ReportSettlementCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget,
                        QuarterMonthType = quarterMonthType,
                        QuarterMonth = quarterMonth,
                        QuarterMonthBefore = quarterMonthBefore,
                        VoucherDate = DateTime.Now,
                        LNS = "1010000",
                        SettlementType = SettlementType.REGULAR_BUDGET,
                        Dvt = dvt
                    };

                    //lấy ra đơn vị theo tháng tổng hợp

                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                    List<string> voucherAgencyIds = new List<string>();
                    if (!IsSummary)
                        voucherAgencyIds = _chungTuService.FindAgencyIdByMonth(condition);
                    else voucherAgencyIds.AddRange(agencies.Where(x => x.Loai == LoaiDonVi.ROOT).Select(x => x.IIDMaDonVi));

                    List<DonVi> agencyReports = agencies.Where(x => x.Loai == LoaiDonVi.ROOT || (x.Loai != LoaiDonVi.ROOT && voucherAgencyIds.Contains(x.IIDMaDonVi))).ToList();
                    foreach (var agency in agencyReports)
                    {
                        if (agency.Loai == LoaiDonVi.ROOT)
                        {
                            condition.EstimateAgencyId = agency.IIDMaDonVi;
                            condition.AgencyId = string.Empty;
                        }
                        else
                        {
                            condition.EstimateAgencyId = agency.IIDMaDonVi;
                            condition.AgencyId = agency.IIDMaDonVi;
                        }
                        _reportQtChungTuChiTiets = _chungTuChiTietService.FindForReportQuarterlySummary(condition);

                        RptQuyetToanChungTu rptChungTu = new RptQuyetToanChungTu();
                        rptChungTu.Cap1 = _cap1;
                        rptChungTu.Cap2 = _sessionInfo.TenDonVi;
                        rptChungTu.TieuDe1 = Title1;
                        rptChungTu.TieuDe3 = Title3;
                        rptChungTu.TieuDe2 = Title2;
                        foreach (var item in _reportQtChungTuChiTiets.Where(x => !x.IsHangCha))
                        {
                            CalculateParent(item, item);
                            rptChungTu.TongChiTieu += item.ChiTieu;
                            rptChungTu.TongTuChi += item.TuChi;
                            rptChungTu.TongTuChi2 += item.TuChi2;
                            rptChungTu.TongThucChi += item.ThucChi;
                        }
                        _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => x.HasData).ToList();
                        switch (SelectedInToiMuc.ValueItem)
                        {
                            case nameof(MLNSFiled.NG):
                                _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                break;
                            case nameof(MLNSFiled.TNG):
                                _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                break;
                            case nameof(MLNSFiled.TNG1):
                                _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                break;
                            case nameof(MLNSFiled.TNG2):
                                _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                break;
                        }
                        FormatDisplay();
                        rptChungTu.Items = _reportQtChungTuChiTiets;
                        rptChungTu.TienTuChi = StringUtils.NumberToText(rptChungTu.TongTuChi * dvt);
                        rptChungTu.ThoiGian = quarterMonthStr + " Năm " + _sessionInfo.YearOfWork;
                        rptChungTu.ThangQuy = QuarterMonthValue == QuarterMonth.QUARTER ? "quý" : "tháng";
                        rptChungTu.H2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                        rptChungTu.Ngay = DateUtils.FormatDateReport(ReportDate);
                        rptChungTu.DiaDiem = _diaDiem;
                        rptChungTu.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                        rptChungTu.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                        rptChungTu.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                        rptChungTu.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                        rptChungTu.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                        rptChungTu.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                        rptChungTu.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                        rptChungTu.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                        rptChungTu.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                        if (IsData)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                            data.Add("FormatNumber", formatNumber);
                            foreach (var prop in rptChungTu.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(rptChungTu));
                            }

                            var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                            List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP);
                            fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP.Split(".").First() + "_" + agency.TenDonVi + "_" + DateTime.Now.Millisecond;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data, hideColumns);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }

                        if (IsVerbalExplanation)
                        {
                            SettlementVoucherDetailExplainCriteria explainCondition = new SettlementVoucherDetailExplainCriteria
                            {
                                VoucherId = Guid.Empty,
                                ExplainId = "TX_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType,
                                AgencyId = _sessionInfo.IdDonVi,
                                YearOfWork = _sessionInfo.YearOfWork
                            };
                            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(explainCondition);
                            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                            RptQuyetToanGiaiThichLoi rptGiaiThichLoi = new RptQuyetToanGiaiThichLoi();
                            rptGiaiThichLoi.Tien = rptChungTu.TongTuChi;
                            rptGiaiThichLoi.TienTuChi = StringUtils.NumberToText(rptChungTu.TongTuChi * Convert.ToInt32(SelectedUnit.ValueItem));
                            rptGiaiThichLoi.MoTaTinhHinh = chungTuChiTietGiaiThich == null ? string.Empty : chungTuChiTietGiaiThich.SMoTaTinhHinh;
                            rptGiaiThichLoi.MoTaKienNghi = chungTuChiTietGiaiThich == null ? string.Empty : chungTuChiTietGiaiThich.SMoTaKienNghi;
                            rptGiaiThichLoi.DiaDiem = _diaDiem;
                            rptGiaiThichLoi.Ngay = StringUtils.CreateDateTimeString();
                            rptGiaiThichLoi.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                            rptGiaiThichLoi.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                            rptGiaiThichLoi.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                            rptGiaiThichLoi.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                            rptGiaiThichLoi.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                            rptGiaiThichLoi.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                            rptGiaiThichLoi.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                            rptGiaiThichLoi.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                            rptGiaiThichLoi.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;
                            rptGiaiThichLoi.ChucDanh4 = _dmChuKy != null ? _dmChuKy.ChucDanh4MoTa : string.Empty;
                            rptGiaiThichLoi.ChucDanh5 = _dmChuKy != null ? _dmChuKy.ChucDanh5MoTa : string.Empty;
                            rptGiaiThichLoi.ChucDanh6 = _dmChuKy != null ? _dmChuKy.ChucDanh6MoTa : string.Empty;
                            rptGiaiThichLoi.ThuaLenh4 = _dmChuKy != null ? _dmChuKy.ThuaLenh4MoTa : string.Empty;
                            rptGiaiThichLoi.ThuaLenh5 = _dmChuKy != null ? _dmChuKy.ThuaLenh5MoTa : string.Empty;
                            rptGiaiThichLoi.ThuaLenh6 = _dmChuKy != null ? _dmChuKy.ThuaLenh6MoTa : string.Empty;
                            rptGiaiThichLoi.Ten4 = _dmChuKy != null ? _dmChuKy.Ten4MoTa : string.Empty;
                            rptGiaiThichLoi.Ten5 = _dmChuKy != null ? _dmChuKy.Ten5MoTa : string.Empty;
                            rptGiaiThichLoi.Ten6 = _dmChuKy != null ? _dmChuKy.Ten6MoTa : string.Empty;
                            if (QuarterMonthValue == QuarterMonth.QUARTER)
                            {
                                rptGiaiThichLoi.LoaiThangQuy = "Quý";
                            }
                            else
                            {
                                rptGiaiThichLoi.LoaiThangQuy = "Tháng";
                            }

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                            data.Add("FormatNumber", formatNumber);
                            foreach (var prop in rptGiaiThichLoi.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(rptGiaiThichLoi));
                            }

                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_LOI);
                            fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_LOI.Split(".").First() + "_" + agency.TenDonVi + "_" + DateTime.Now.Millisecond;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result.Count() == 0)
                            MessageBoxHelper.Info(Resources.AlertEmptyReport);
                        else _exportService.Open(result, exportType);
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

        private ExportResult OnExportCoverSheet(string quarterMonthStr)
        {
            RptQuyetToanToBia rptToBia = new RptQuyetToanToBia
            {
                Cap1 = _cap1,
                Cap2 = _sessionInfo.TenDonVi,
                TieuDe = "Tổng hợp quyết toán thường xuyên",
                ThoiGian = string.Format("{0} năm {1}", quarterMonthStr, _sessionInfo.YearOfWork),
                Ngay = DateUtils.FormatDateReport(ReportDate),
                DiaDiem = _diaDiem
            };
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (var prop in rptToBia.GetType().GetProperties())
            {
                data.Add(prop.Name, prop.GetValue(rptToBia));
            }
            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_TOBIA);
            string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_TOBIA.Split(".").First() + "_" + quarterMonthStr;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private ExportResult OnExportDataInterpretation(ExportType exportType, string quarterMonth, int quarterMonthType)
        {
            SettlementVoucherDetailExplainCriteria condition = new SettlementVoucherDetailExplainCriteria
            {
                VoucherId = Guid.Empty,
                ExplainId = "TX_" + _sessionInfo.YearOfWork + "_" + _sessionInfo.IdDonVi + "_" + quarterMonth + "_" + quarterMonthType,
                AgencyId = _sessionInfo.IdDonVi,
                YearOfWork = _sessionInfo.YearOfWork
            };
            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(condition);
            RptQuyetToanGiaiThichSo rptGiaiThichSo = new RptQuyetToanGiaiThichSo();
            rptGiaiThichSo = _mapper.Map<RptQuyetToanGiaiThichSo>(chungTuChiTietGiaiThich);

            if (rptGiaiThichSo == null)
                return null;
            else
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in rptGiaiThichSo.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(rptGiaiThichSo));
                }

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_SO);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_SO.Split(".").First();
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
        }

        private void CalculateParent(ReportQtChungTuChiTietQuery currentItem, ReportQtChungTuChiTietQuery selfItem)
        {
            var parentItem = _reportQtChungTuChiTiets.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.ChiTieu += selfItem.ChiTieu;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.TuChi2 += selfItem.TuChi2;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.ThucChi += selfItem.ThucChi;
            CalculateParent(parentItem, selfItem);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.HasAddedSign4 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign5 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign6 = IsVerbalExplanation;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportQtChungTuChiTiets.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportQtChungTuChiTiets.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();
                if (parent != null)
                {
                    item.L = string.Empty;
                    item.K = string.Empty;
                    item.LNS = string.Empty;
                    if (!string.IsNullOrEmpty(parent.M))
                        item.M = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TM))
                        item.TM = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TTM))
                        item.TTM = string.Empty;
                    if (!string.IsNullOrEmpty(parent.NG))
                        item.NG = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG))
                        item.TNG = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG1))
                        item.TNG1 = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG2))
                        item.TNG2 = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG3))
                        item.TNG3 = string.Empty;
                }
            }
        }
    }
}
