using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
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
    public class PrintSettlementVoucherViewModel : ViewModelBase
    {
        private ILog _logger;
        private IExportService _exportService;
        private ISessionService _sessionService;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsQtChungTuChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private IMapper _mapper;
        private INsMucLucNganSachService _mucLucNganSachService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private SessionInfo _sessionInfo;
        private List<ReportQtChungTuChiTietQuery> _reportQtChungTuChiTiets;
        private VerbalExplanationViewModel VerbalExplanationViewModel;
        private DataInterpretationViewModel DataInterpretationViewModel;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private List<SettlementVoucherDetailModel> _settlementVoucherDetails;
        private ICollectionView _listBudgetIndex;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private string _typeChuKy;
        private string _coverSheetTitle;

        private string _title;
        public override string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description;
        public override string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintSettlementVoucher);

        private SettlementVoucherModel _settlementVoucher;
        public SettlementVoucherModel SettlementVoucher
        {
            get => _settlementVoucher;
            set => SetProperty(ref _settlementVoucher, value);
        }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
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

        private string _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
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
            get
            {
                bool isExport = _isCoverSheet || _isData || _isDataInterpretation || _isVerbalExplanation;
                if (BudgetIndexes != null)
                    return isExport && BudgetIndexes.Count(item => item.IsSelected) > 0;
                return isExport;
            }
        }

        private bool _isRegularSettlement;
        public bool IsRegularSettlement
        {
            get => _isRegularSettlement;
            set => SetProperty(ref _isRegularSettlement, value);
        }

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
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Count : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => _isSelectAllBudgetIndex;
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
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

        public PrintSettlementVoucherViewModel(IExportService exportService,
           ISessionService sessionService,
           INsQtChungTuChiTietService chungTuChiTietService,
           IMapper mapper,
           INsQtChungTuChiTietGiaiThichService chungTuChiTietGiaiThichService,
           IDanhMucService danhMucService,
           IDmChuKyService dmChuKyService,
           VerbalExplanationViewModel verbalExplanationViewModel,
           DataInterpretationViewModel dataInterpretationiewModel,
           DmChuKyDialogViewModel dmChuKyDialogViewModel,
           INsMucLucNganSachService mucLucNganSachService,
           ILog logger)
        {
            _exportService = exportService;
            _sessionService = sessionService;
            _chungTuChiTietService = chungTuChiTietService;
            _chungTuChiTietGiaiThichService = chungTuChiTietGiaiThichService;
            _mapper = mapper;
            _mucLucNganSachService = mucLucNganSachService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            VerbalExplanationViewModel = verbalExplanationViewModel;
            DataInterpretationViewModel = dataInterpretationiewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            ExportExcelCommand = new RelayCommand(obj => OnExportFile((int)ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            PrintCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            DataInterpretationCommand = new RelayCommand(obj => OnOpenDataInterpretationDialog());
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadDanhMuc();
            LoadTypeChuKy();
            LoadTieuDe();
            LoadChiTietToi();
            if (!_isRegularSettlement)
                LoadBudgetIndexes();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void LoadBudgetIndexes()
        {
            BudgetIndexForBudgetCriteria searchCondition = new BudgetIndexForBudgetCriteria
            {
                LNS = string.Join(",", SettlementVoucher.SDslns),
                YearOfWork = _sessionService.Current.YearOfWork,
                GenerateAgencyId = _sessionService.Current.IdDonVi,
                UserName = _sessionService.Current.Principal
            };

            List<NsMucLucNganSach> listMucLucNganSach = _mucLucNganSachService.FindByStateBudget(searchCondition);
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLucNganSach);

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                    {
                        foreach (NsMuclucNgansachModel item in BudgetIndexes)
                        {
                            if (item.MlnsIdParent == model.MlnsId)
                                item.IsSelected = model.IsSelected;
                        }
                        OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                        OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE)
                .OrderBy(n => n.SGiaTri).ToList();
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

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    if (IsCoverSheet)
                    {
                        var dataResult = OnExportCoverSheet(exportType);
                        if (dataResult != null)
                            results.Add(dataResult);
                    }
                    if (IsData)
                    {
                        var dataResult = OnExportData(exportType);
                        if (dataResult != null)
                            results.Add(dataResult);
                    }
                    if (IsDataInterpretation)
                    {
                        var interpretationResult = OnExportDataInterpretation();
                        if (interpretationResult != null)
                            results.Add(interpretationResult);
                    }
                    if (IsVerbalExplanation)
                    {
                        var verbalResult = OnExportVerbalExplaination(exportType);
                        if (verbalResult != null)
                            results.Add(verbalResult);
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
                MessageBox.Show(Resources.ErrorExportReport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ExportResult OnExportCoverSheet(ExportType exportType)
        {
            RptQuyetToanToBia rptToBia = new RptQuyetToanToBia
            {
                Cap1 = _cap1,
                Cap2 = _sessionInfo.TenDonVi,
                TieuDe = _coverSheetTitle,
                ThoiGian = string.Format("{0} năm {1}", SettlementVoucher.SThangQuyMoTa, _sessionInfo.YearOfWork),
                Ngay = DateUtils.FormatDateReport(ReportDate),
                DiaDiem = _diaDiem
            };
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (var prop in rptToBia.GetType().GetProperties())
            {
                data.Add(prop.Name, prop.GetValue(rptToBia));
            }
            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_TOBIA);
            string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_TOBIA.Split(".").First() + "_" + SettlementVoucher.STenDonVi;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private ExportResult OnExportData(ExportType exportType)
        {
            int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
            SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
            {
                VoucherId = SettlementVoucher.Id,
                LNS = IsRegularSettlement ? SettlementVoucher.SDslns : string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToArray()),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                AgencyId = SettlementVoucher.IIdMaDonVi,
                VoucherDate = SettlementVoucher.DNgayChungTu,
                QuarterMonthType = SettlementVoucher.IThangQuyLoai,
                QuarterMonth = SettlementVoucher.IThangQuy.ToString(),
                Type = SettlementVoucher.SLoai,
                Dvt = dvt
            };
            if (SettlementVoucher.IThangQuyLoai == (int)QuarterMonth.QUARTER)
                searchCondition.QuarterMonthBefore = string.Join(",", Enumerable.Range(0, SettlementVoucher.IThangQuy).Where(x => x % 3 == 0).ToList());
            else
                searchCondition.QuarterMonthBefore = string.Join(",", Enumerable.Range(0, SettlementVoucher.IThangQuy).ToList());
            _reportQtChungTuChiTiets = _chungTuChiTietService.FindForSettlementVoucherReport(searchCondition);
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            RptQuyetToanChungTu rptChungTu = new RptQuyetToanChungTu();
            rptChungTu.Cap1 = _cap1;
            rptChungTu.Cap2 = _sessionInfo.TenDonVi;
            rptChungTu.TieuDe1 = Title1;
            rptChungTu.TieuDe2 = Title2;
            rptChungTu.TieuDe3 = Title3;

            foreach (var item in _reportQtChungTuChiTiets.Where(x => !x.IsHangCha))
            {
                CalculateParent(item, item);
                rptChungTu.TongChiTieu += item.ChiTieu;
                rptChungTu.TongTuChi += item.TuChi;
                rptChungTu.TongTuChi2 += item.TuChi2;
                rptChungTu.TongThucChi += item.ThucChi;
            }
            switch (SelectedInToiMuc.ValueItem)
            {
                case nameof(MLNSFiled.NG):
                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                    _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => { x.IsHangCha = false; return x; }).ToList();
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
            _reportQtChungTuChiTiets = _reportQtChungTuChiTiets.Where(x => x.HasData).ToList();
            FormatDisplay();
            rptChungTu.Items = _reportQtChungTuChiTiets;
            rptChungTu.ThoiGian = SettlementVoucher.SThangQuyMoTa + " Năm " + SettlementVoucher.INamLamViec;
            rptChungTu.DonVi = SettlementVoucher.STenDonVi.Split(" - ").Last();
            rptChungTu.H2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
            rptChungTu.TienTuChi = StringUtils.NumberToText(rptChungTu.TongTuChi * dvt);
            rptChungTu.GhiChu = Note;
            rptChungTu.DiaDiem = _diaDiem;
            rptChungTu.Ngay = DateUtils.FormatDateReport(ReportDate);
            rptChungTu.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
            rptChungTu.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
            rptChungTu.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
            rptChungTu.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
            rptChungTu.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
            rptChungTu.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
            rptChungTu.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
            rptChungTu.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
            rptChungTu.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(dvt, exportType);
            data.Add("FormatNumber", formatNumber);
            foreach (var prop in rptChungTu.GetType().GetProperties())
            {
                data.Add(prop.Name, prop.GetValue(rptChungTu));
            }

            var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
            List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU);
            string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU.Split(".").First() + "_" + SettlementVoucher.STenDonVi;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data, hideColumns);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        /// <summary>
        /// Xuất tờ giải thích bằng số
        /// </summary>
        private ExportResult OnExportDataInterpretation()
        {
            SettlementVoucherDetailExplainCriteria condition = new SettlementVoucherDetailExplainCriteria
            {
                VoucherId = SettlementVoucher.Id,
                ExplainId = SettlementVoucher.Id.ToString(),
                AgencyId = SettlementVoucher.IIdMaDonVi,
                YearOfWork = _sessionInfo.YearOfWork
            };
            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(condition);
            if (chungTuChiTietGiaiThich == null)
                chungTuChiTietGiaiThich = new NsQtChungTuChiTietGiaiThich();
            RptQuyetToanGiaiThichSo rptGiaiThichSo = new RptQuyetToanGiaiThichSo();
            rptGiaiThichSo = _mapper.Map<RptQuyetToanGiaiThichSo>(chungTuChiTietGiaiThich);
            rptGiaiThichSo.DonVi = SettlementVoucher.STenDonVi.Split(" - ").Last();
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (var prop in rptGiaiThichSo.GetType().GetProperties())
            {
                data.Add(prop.Name, prop.GetValue(rptGiaiThichSo));
            }

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_GIAITHICH_SO);
            string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_GIAITHICH_SO.Split(".").First() + "_" + SettlementVoucher.STenDonVi;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        /// <summary>
        /// Xuất tờ giải thích bằng lời
        /// </summary>
        private ExportResult OnExportVerbalExplaination(ExportType exportType)
        {
            SettlementVoucherDetailExplainCriteria condition = new SettlementVoucherDetailExplainCriteria
            {
                VoucherId = SettlementVoucher.Id,
                ExplainId = SettlementVoucher.Id.ToString(),
                AgencyId = SettlementVoucher.IIdMaDonVi,
                YearOfWork = _sessionInfo.YearOfWork
            };
            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(condition);
            if (chungTuChiTietGiaiThich == null)
            {
                return null;
            }
            int dvt = 1;
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            RptQuyetToanGiaiThichLoi rptGiaiThichLoi = new RptQuyetToanGiaiThichLoi();
            SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
            {
                VoucherId = SettlementVoucher.Id,
                LNS = IsRegularSettlement ? SettlementVoucher.SDslns : string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToArray()),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                Type = SettlementVoucher.SLoai,
                BudgetSource = _sessionService.Current.Budget,
                AgencyId = SettlementVoucher.IIdMaDonVi,
                VoucherDate = SettlementVoucher.DNgayChungTu,
                Dvt = dvt
            };
            _reportQtChungTuChiTiets = _chungTuChiTietService.FindForSettlementVoucherReport(searchCondition);

            foreach (var item in _reportQtChungTuChiTiets.Where(x => !x.IsHangCha))
            {
                CalculateParent(item, item);
                rptGiaiThichLoi.Tien += item.TuChi;
            }
            //rptGiaiThichLoi.Tien = SettlementVoucher.FTongTuChiPheDuyet;
            rptGiaiThichLoi.TienTuChi = StringUtils.NumberToText(rptGiaiThichLoi.Tien);
            rptGiaiThichLoi.DonVi = SettlementVoucher.STenDonVi.Split(" - ").Last();
            rptGiaiThichLoi.MoTaTinhHinh = chungTuChiTietGiaiThich.SMoTaTinhHinh;
            rptGiaiThichLoi.MoTaKienNghi = chungTuChiTietGiaiThich.SMoTaKienNghi;
            rptGiaiThichLoi.DiaDiem = _diaDiem;
            rptGiaiThichLoi.Ngay = DateUtils.FormatDateReport(ReportDate);
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

            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(dvt, "0", exportType);
            data.Add("FormatNumber", formatNumber);
            foreach (var prop in rptGiaiThichLoi.GetType().GetProperties())
            {
                data.Add(prop.Name, prop.GetValue(rptGiaiThichLoi));
            }

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_GIAITHICH_LOI);
            string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_GIAITHICH_LOI.Split(".").First() + "_" + SettlementVoucher.STenDonVi;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private void CalculateParent(ReportQtChungTuChiTietQuery currentItem, ReportQtChungTuChiTietQuery selfItem)
        {
            var parentItem = _reportQtChungTuChiTiets.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.ChiTieu += selfItem.ChiTieu;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.TuChi2 += selfItem.TuChi2;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.SoLuot += selfItem.SoLuot;
            parentItem.ThucChi += selfItem.ThucChi;
            CalculateParent(parentItem, selfItem);
        }

        private void OnOpenDataInterpretationDialog()
        {
            DataInterpretationViewModel.SettlementVoucher = SettlementVoucher;
            if (_settlementVoucherDetails == null || _settlementVoucherDetails.Count == 0)
                LoadSettlementVoucherDetail();
            DataInterpretationViewModel.SettlementVoucherDetails = _settlementVoucherDetails.ToList();
            DataInterpretationViewModel.Init();
            var view = new DataInterpretation { DataContext = DataInterpretationViewModel };
            view.ShowDialog();
        }

        private void LoadSettlementVoucherDetail()
        {
            SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
            {
                VoucherId = SettlementVoucher.Id,
                LNS = string.Join(",", SettlementVoucher.SDslns),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                Type = SettlementVoucher.SLoai,
                BudgetSource = 1,
                AgencyId = SettlementVoucher.IIdMaDonVi,
                VoucherDate = SettlementVoucher.DNgayChungTu,
                UserName = _sessionInfo.Principal
            };
            List<QtChungTuChiTietQuery> _listChungTuChiTiet = _chungTuChiTietService.FindByCondition(searchCondition);
            _settlementVoucherDetails = _mapper.Map<List<SettlementVoucherDetailModel>>(_listChungTuChiTiet);
        }

        private void OnOpenVerbalExplanationDialog()
        {
            VerbalExplanationViewModel.SettlementVoucher = SettlementVoucher;
            VerbalExplanationViewModel.Init();
            var view = new VerbalExplanation { DataContext = VerbalExplanationViewModel };
            view.ShowDialog();
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

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuKy;
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

        private void LoadTypeChuKy()
        {
            _isRegularSettlement = false;
            switch (SettlementVoucher.SLoai)
            {
                case SettlementType.REGULAR_BUDGET:
                    _isRegularSettlement = true;
                    _title = "In báo cáo quyết toán thường xuyên";
                    _description = "In báo cáo quyết toán thường xuyên theo chứng từ";
                    _typeChuKy = TypeChuKy.RPT_NS_QUYETTOAN_CHUNGTU_THUONGXUYEN;
                    _coverSheetTitle = "Quyết toán ngân sách thường xuyên";
                    break;
                case SettlementType.DEFENSE_BUDGET:
                    _title = "In báo cáo quyết toán ngân sách quốc phòng";
                    _description = "In báo cáo quyết toán ngân sách quốc phòng theo chứng từ";
                    _typeChuKy = TypeChuKy.RPT_NS_QUYETTOAN_CHUNGTU_QUOCPHONG;
                    _coverSheetTitle = "Quyết toán ngân sách quốc phòng";
                    break;
                case SettlementType.STATE_BUDGET:
                    _title = "In báo cáo quyết toán ngân sách nhà nước";
                    _description = "In báo cáo quyết toán ngân sách nhà nước theo chứng từ";
                    _typeChuKy = TypeChuKy.RPT_NS_QUYETTOAN_CHUNGTU_NHANUOC;
                    _coverSheetTitle = "Qquyết toán ngân sách nhà nước";
                    break;
                case SettlementType.FOREX_BUDGET:
                    _title = "In báo cáo quyết toán ngân sách ngoại hối";
                    _description = "In báo cáo quyết toán ngân sách ngoại hối theo chứng từ";
                    _typeChuKy = TypeChuKy.RPT_NS_QUYETTOAN_CHUNGTU_NGOAIHOI;
                    _coverSheetTitle = "Quyết toán ngân sách ngoại hối";
                    break;
                case SettlementType.EXPENSE_BUDGET:
                    _title = "In báo cáo quyết toán ngân sách khác";
                    _description = "In báo cáo quyết toán ngân sách khác theo chứng từ";
                    _typeChuKy = TypeChuKy.RPT_NS_QUYETTOAN_CHUNGTU_KHAC;
                    _coverSheetTitle = "Quyết toán ngân sách khác";
                    break;
            }
        }
    }
}
