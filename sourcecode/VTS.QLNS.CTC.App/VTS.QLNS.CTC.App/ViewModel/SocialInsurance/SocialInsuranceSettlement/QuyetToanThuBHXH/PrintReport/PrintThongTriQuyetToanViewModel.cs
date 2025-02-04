using AutoMapper;
using ControlzEx.Standard;
using FlexCel.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport
{
    public class PrintThongTriQuyetToanViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private ILog _logger;
        private IMapper _mapper;
        private INsDonViService _donViService;
        private INsNguoiDungDonViService _nguoiDungDonViService;
        private IQttBHXHService _chungTuService;
        private IQttBHXHChiTietService _chungTuChiTietService;
        private IQttBHXHChiTietGiaiThichService _explainService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _checkAllAgencies;
        private string _quarterMonth;
        private int _quarterMonthType;
        private string _quarterMonthBefore;
        public BhQttBHXHModel SettlementVoucher;
        public List<BhQttBHXHChiTietModel> SettlementVoucherDetails;
        public bool IsShowAll { get; set; }
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public int SettlementTypeValue;
        private string _typeChuky;
        public override string Name => "Thông tri thu BHXH, BHYT, BHTN";
        public override string Title => "Thông tri thu BHXH, BHYT, BHTN";
        public override string Description => "Thông tri thu BHXH, BHYT, BHTN";
        public override Type ContentType => typeof(PrintThongTriQuyetToan);
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() || _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString();

        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthQTTSelected
        {
            get => _quarterMonthSelected;
            set
            {
                SetProperty(ref _quarterMonthSelected, value);
                LoadAgencies();
            }
        }

        private List<ComboboxItem> _reportTypes;
        public List<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                OnPropertyChanged(nameof(IsEnableCheckBoxSummary));
                LoadAgencies();
            }
        }

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadAgencies();
            }
        }

        #region list agency
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
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
                int totalCount = 0;
                int totalSelected = 0;
                if (_agencies != null)
                {
                    totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies.Count > 0 && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                _checkAllAgencies = true;
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                }
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
                OnPropertyChanged(nameof(IsExportEnable));
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
            get => BudgetIndexes.Count > 0 && BudgetIndexes.Where(x => x.IsFilter).All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        private bool _isEnableLoaiThu;
        public bool IsEnableLoaiThu
        {
            get => _isEnableLoaiThu;
            set => SetProperty(ref _isEnableLoaiThu, value);
        }

        private bool _isEnableLoaiThongTriForAll;
        public bool IsEnableLoaiThongTriForAll
        {
            get => _isEnableLoaiThongTriForAll;
            set => SetProperty(ref _isEnableLoaiThongTriForAll, value);
        }
        private bool _isEnableLoaiThongTri;
        public bool IsEnableLoaiThongTri
        {
            get => _isEnableLoaiThongTri;
            set => SetProperty(ref _isEnableLoaiThongTri, value);
        }

        private bool _isEnableReportType;
        public bool IsEnableReportType
        {
            get => _isEnableReportType;
            set => SetProperty(ref _isEnableReportType, value);
        }

        private bool _isEnableReportTypeYear;
        public bool IsEnableReportTypeYear
        {
            get => _isEnableReportTypeYear;
            set => SetProperty(ref _isEnableReportTypeYear, value);
        }

        private bool _isEnableInTheo;
        public bool IsEnableInTheo
        {
            get => _isEnableInTheo;
            set => SetProperty(ref _isEnableInTheo, value);
        }

        public bool IsExportEnable => Agencies.Any(x => x.Selected);

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

        private bool _isDatePeople;
        public bool IsDatePeople
        {
            get => _isDatePeople;
            set
            {
                SetProperty(ref _isDatePeople, value);
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

        private LoaiThu _loaiThu;
        public LoaiThu LoaiThu
        {
            get => _loaiThu;
            set
            {
                SetProperty(ref _loaiThu, value);
                LoadRadioSelected();
                _isEnableLoaiThongTri = _loaiThu == LoaiThu.All ? true : false;
                _isEnableLoaiThongTriForAll = _loaiThu == LoaiThu.All ? false : true;
                LoaiThongTriThu = LoaiThongTriThu.TongHop;
                OnPropertyChanged(nameof(IsEnableLoaiThongTri));
                OnPropertyChanged(nameof(IsEnableLoaiThongTriForAll));
                LoadTypeChuKy();
            }
        }

        private LoaiThongTriThu _loaiThongTriThu;
        public LoaiThongTriThu LoaiThongTriThu
        {
            get => _loaiThongTriThu;
            set
            {
                SetProperty(ref _loaiThongTriThu, value);
                LoadTypeChuKy();
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

        private bool _isInMotTo;
        public bool IsInMotTo
        {
            get => _isInMotTo;
            set => SetProperty(ref _isInMotTo, value);
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public BhQttBHXHModel Model { get; internal set; }

        public PrintThongTriQuyetToanViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IQttBHXHService chungTuService,
            IQttBHXHChiTietService chungTuChiTietService,
            IQttBHXHChiTietGiaiThichService iQttBHXHChiTietGiaiThichService,
            IExportService exportService,
            INsDonViService donViService,
            INsNguoiDungDonViService nguoiDungDonViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _explainService = iQttBHXHChiTietGiaiThichService;
            _exportService = exportService;
            _donViService = donViService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

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
            //DataInterpretationCommand = new RelayCommand(obj => OnOpenDataInterpretationDialog());
            //VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _agencies = new ObservableCollection<AgencyModel>();
            IsDataInterpretation = false;
            ResetCondition();
            LoadReportType();
            LoadQuarterYears();
            LoadDanhMuc();
            LoadAgencies();
            LoadTypeChuKy();
            LoaiThu = LoaiThu.All;
            IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
        }

        private void LoadRadioSelected()
        {
            if (LoaiThongTriThu == LoaiThongTriThu.TongHopChung && LoaiThu != LoaiThu.All)
            {
                LoaiThongTriThu = LoaiThongTriThu.TongHop;
            }
        }
        private void LoadAgencies()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    var lstIdDonVi = GetListIdDonVi();
                    IsLoading = true;
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).Where(x => lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    agencies = IsInTheoTongHop
                                ? agencies.Where(x => x.Loai == LoaiDonVi.ROOT).ToList()
                                : agencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();

                    e.Result = agencies;
                }, (s, e) =>
                {
                    if (e.Result != null)
                    {
                        List<DonVi> agencies = (List<DonVi>)e.Result;
                        _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(agencies);
                    }
                    else
                        _agencies = new ObservableCollection<AgencyModel>();
                    _listAgency = CollectionViewSource.GetDefaultView(_agencies);
                    _listAgency.Filter = ListAgencyFilter;
                    foreach (var model in Agencies)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(AgencyModel.Selected) && !_checkAllAgencies)
                            {
                                OnPropertyChanged(nameof(SelectedAgencyCount));
                                OnPropertyChanged(nameof(IsSelectedAllAgency));
                                OnPropertyChanged(nameof(IsExportEnable));
                            }
                        };
                    }
                    OnPropertyChanged(nameof(Agencies));
                    OnPropertyChanged(nameof(IsSelectedAllAgency));
                    OnPropertyChanged(nameof(SelectedAgencyCount));
                    OnPropertyChanged(nameof(IsExportEnable));
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                _agencies = new ObservableCollection<AgencyModel>();
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
            }
        }

        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected?.ValueItem ?? "3");
            var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected?.HiddenValue ?? "1");
            //List<BhQttBHXHQuery> listChungTuDuocXem = new List<BhQttBHXHQuery>();
            var lstIdDonVi = IsInTheoTongHop
                    ? _chungTuChiTietService.FindChiTietDonViTongHopThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter, selectedQuarterType) :
                    _chungTuChiTietService.FindChiTietDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter, selectedQuarterType);
            return lstIdDonVi;
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

        private void ResetCondition()
        {
            _quarterMonthSelected = new ComboboxItem();
            _searchAgencyText = string.Empty;
            _searchBudgetIndexText = string.Empty;
        }

        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
            };
            _selectedReportType = _reportTypes.First();
        }

        private void LoadQuarterYears()
        {
            _quarterMonths = new List<ComboboxItem>();
            _quarterMonths.Add(new ComboboxItem { DisplayItem = "Quý I", ValueItem = "3", HiddenValue = "1" });
            _quarterMonths.Add(new ComboboxItem { DisplayItem = "Quý II", ValueItem = "6", HiddenValue = "1" });
            _quarterMonths.Add(new ComboboxItem { DisplayItem = "Quý III", ValueItem = "9", HiddenValue = "1" });
            _quarterMonths.Add(new ComboboxItem { DisplayItem = "Quý IV", ValueItem = "12", HiddenValue = "1" });
            for (int i = 1; i <= 12; i++)
            {
                _quarterMonths.Add(new ComboboxItem { DisplayItem = "Tháng " + i, ValueItem = i.ToString(), HiddenValue = "0" });
            }
            if (Model is null)
            {
                QuarterMonthQTTSelected = _quarterMonths.FirstOrDefault();
            }
            else
            {
                QuarterMonthQTTSelected = _quarterMonths.FirstOrDefault(x => x.HiddenValue == Model.IQuyNamLoai.ToString()
                    && x.ValueItem == Model.IQuyNam.ToString());
            }

        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SGiaTri)
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

        private string GetTemplate()
        {
            string input = SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString()
                ? Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THONG_TRI_QUYET_TOAN_THU_TONG_HOP)
                : Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THONG_TRI_QUYET_TOAN_THU);
            if (LoaiThongTriThu == LoaiThongTriThu.TongHopChung || LoaiThongTriThu == LoaiThongTriThu.TongHopChungNLD || LoaiThongTriThu == LoaiThongTriThu.TongHopChungNSD)
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THONG_TRI_QUYET_TOAN_THU_ALL_THC);
            return Path.Combine(ExportPrefix.PATH_BH_QTT, input + FileExtensionFormats.Xlsx);
        }
        private void OnConfigSign()
        {
            LoadTypeChuKy();
            DmChuKyModel chuKyModel = new DmChuKyModel();

            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            if (_typeChuky == TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY)
            {
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.HasAddedSign5 = true;
                DmChuKyDialogViewModel.HasAddedSign6 = true;
            }
            else
            {
                DmChuKyDialogViewModel.HasAddedSign4 = false;
                DmChuKyDialogViewModel.HasAddedSign5 = false;
                DmChuKyDialogViewModel.HasAddedSign6 = false;
            }
            DmChuKyDialogViewModel.SavedAction = obj => LoadTypeChuKy();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void OnExportFile(ExportType exportType)
        {
            if (Agencies.Where(item => item.Selected).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            if (QuarterMonthQTTSelected == null)
            {
                MessageBoxHelper.Warning(Resources.ErrorQuarterEmpty);
                return;
            }
            if (IsInMotTo)
                exportType = ExportType.PDF_ONE_PAPER;


            bool isTongHopDonVi = SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();
            if (LoaiThongTriThu != LoaiThongTriThu.TongHopChung && LoaiThongTriThu != LoaiThongTriThu.TongHopChungNLD && LoaiThongTriThu != LoaiThongTriThu.TongHopChungNSD)
            {
                if (isTongHopDonVi)
                {
                    ExportQuyetToanThuBHYTTongHop(exportType);
                }
                else
                {
                    ExportQuyetToanThuBHYT(exportType);
                }
            }
            else
            {
                if (isTongHopDonVi)
                {
                    ExportThongTriThuBHYTTongHop(exportType);
                }
                else
                {
                    ExportThongTriThuBHYTChiTiet(exportType);
                }
            }
        }

        private void ExportThongTriThuBHYTChiTiet(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected?.HiddenValue ?? "1");
                    var sQuarterMonth = ConvertQuarterMonthText(selectedQuarterType, selectedQuarter);

                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    List<AgencyModel> lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    string cap2 = GetLevelTitle(_dmChuKy, 2);
                    foreach (var item in lstDonViSelected)
                    {
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        CurrencyToText currencyToText = new CurrencyToText();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
                        searchCondition.INamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IQuyNamLoai = selectedQuarterType;
                        searchCondition.IQuyNam = selectedQuarter;
                        searchCondition.IdDonViFilter = item.IIDMaDonVi;
                        searchCondition.IIDMaDonVi = item.IIDMaDonVi;
                        searchCondition.IsDonViCha = _sessionService.Current.IdDonVi == item.IIDMaDonVi;
                        searchCondition.ILoaiThongTri = LoaiThongTriThu == LoaiThongTriThu.TongHopChung ? (int)LoaiThongTriThu.TongHopChung : LoaiThongTriThu == LoaiThongTriThu.TongHopChungNLD ? (int)LoaiThongTriThu.TongHopChungNLD : LoaiThongTriThu == LoaiThongTriThu.TongHopChungNSD ? (int)LoaiThongTriThu.TongHopChungNSD : (int)LoaiThongTriThu.TongHopChung;
                        searchCondition.DonViTinh = donViTinh;
                        var lstData = _chungTuChiTietService.FindVoucherDetailsThongTri(searchCondition).ToList();
                        ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        data.Add("Items", lstData);

                        data.Add("DonVi", item.TenDonVi);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("TongSoTien", lstData.Sum(x => x.FSoTien));
                        data.Add("currencyToText", currencyToText);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                        data.Add("Cap2", string.IsNullOrEmpty(cap2) ? _sessionInfo.TenDonVi : cap2);
                        data.Add("h1", "");
                        data.Add("h2", "");
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Nam", DateTime.Now.Year);
                        data.Add("ThoiGian", sQuarterMonth);
                        data.Add("TienBangChu", StringUtils.NumberToText(lstData.Sum(x => x.FSoTien) * Convert.ToInt32(SelectedUnit.ValueItem)));
                        AddChuKy(data, _typeChuky);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                        var xlsFile = _exportService.Export<BhReportQttBHXHChiTietQuery>(templateFileName, data);
                        exportResults.Add(new ExportResult("Thu bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }

                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

                _logger.Error($"Error: {ex.Message}", ex);
            }

        }

        private void ExportThongTriThuBHYTTongHop(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected?.HiddenValue ?? "1");
                    string cap2 = GetLevelTitle(_dmChuKy, 2);
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    List<AgencyModel> lstDonViSelected;
                    if (IsInTheoTongHop)
                    {
                        var listIdDaTongHop = _chungTuChiTietService.FindChiTietDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, true, selectedQuarter, selectedQuarterType);
                        var temp = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).Where(x => listIdDaTongHop.Contains(x.IIDMaDonVi) && x.Loai != LoaiDonVi.ROOT).ToList();
                        lstDonViSelected = _mapper.Map<List<AgencyModel>>(temp);
                    }
                    else
                    {
                        lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    }

                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());
                    var sQuarterMonth = ConvertQuarterMonthText(selectedQuarterType, selectedQuarter);
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
                    searchCondition.INamLamViec = _sessionInfo.YearOfWork;
                    searchCondition.IQuyNamLoai = selectedQuarterType;
                    searchCondition.IQuyNam = selectedQuarter;
                    searchCondition.IIDMaDonVi = lstSelectedUnitID;
                    searchCondition.IsDonViCha = true;
                    searchCondition.ILoaiThongTri = LoaiThongTriThu == LoaiThongTriThu.TongHopChung ? (int)LoaiThongTriThu.TongHopChung : LoaiThongTriThu == LoaiThongTriThu.TongHopChungNLD ? (int)LoaiThongTriThu.TongHopChungNLD : LoaiThongTriThu == LoaiThongTriThu.TongHopChungNSD ? (int)LoaiThongTriThu.TongHopChungNSD : (int)LoaiThongTriThu.TongHopChung;
                    searchCondition.DonViTinh = donViTinh;
                    var lstData = _chungTuChiTietService.FindVoucherDetailsThongTri(searchCondition).ToList();
                    templateFileName = GetTemplate();
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);
                    data.Add("Items", lstData);
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("TongSoTien", lstData.Sum(x => x.FSoTien));
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", string.IsNullOrEmpty(cap2) ? _sessionInfo.TenDonVi : cap2);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Nam", DateTime.Now.Year);
                    data.Add("ThoiGian", sQuarterMonth);
                    data.Add("TienBangChu", StringUtils.NumberToText(lstData.Sum(x => x.FSoTien) * Convert.ToInt32(SelectedUnit.ValueItem)));
                    AddChuKy(data, _typeChuky);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhReportQttBHXHChiTietQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("Thu bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

                _logger.Error($"Error: {ex.Message}", ex);
            }
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => "CÁC ĐƠN VỊ",
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        public void ExportQuyetToanThuBHYTTongHop(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected?.HiddenValue ?? "1");

                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    List<AgencyModel> lstDonViSelected;
                    if (IsInTheoTongHop)
                    {
                        var listIdDaTongHop = _chungTuChiTietService.FindChiTietDonViThangQuy(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, true, selectedQuarter, selectedQuarterType);
                        var temp = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).Where(x => listIdDaTongHop.Contains(x.IIDMaDonVi) && x.Loai != LoaiDonVi.ROOT).ToList();
                        lstDonViSelected = _mapper.Map<List<AgencyModel>>(temp);
                    }
                    else
                    {
                        lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    }

                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());

                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    Dictionary<string, object> data = new Dictionary<string, object>();

                    var listDataDonViRow = new List<RptBhThongTriQttBHXHModel>();
                    var thoigian = selectedQuarterType == 0 ? $"Tháng {selectedQuarter} năm {_sessionInfo.YearOfWork}" : $"Quý {selectedQuarterType} năm {_sessionInfo.YearOfWork}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    foreach (var item in lstDonViSelected)
                    {
                        BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
                        searchCondition.INamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IQuyNamLoai = selectedQuarterType;
                        searchCondition.IQuyNam = selectedQuarter;
                        searchCondition.IdDonViFilter = item.IIDMaDonVi;
                        searchCondition.IIDMaDonVi = item.IIDMaDonVi;
                        searchCondition.IsDonViCha = _sessionService.Current.IdDonVi == item.IIDMaDonVi;

                        var temp = _mapper.Map<List<BhQttBHXHChiTietModel>>(_chungTuChiTietService.FindVoucherDetailsByCondition(searchCondition));
                        temp = temp.Where(x => !x.STenBhMLNS.StartsWith("Thu")).ToList();
                        var dataTruyThu = _explainService.GetGiaiThichTruyThuDonVi(_sessionInfo.YearOfWork, item.IIDMaDonVi, selectedQuarter, selectedQuarterType);
                        var listData = temp.Select(x => new RptBhThongTriQttBHXHModel()
                        {
                            IIDMLNS = x.IIDMLNS,
                            IIDMLNSCha = x.IIDMLNSCha,
                            NguoiLD = (long)Math.Round((LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong ? 0 : LoaiThu switch
                            {
                                LoaiThu.BHXH => x.FThuBHXHNLD.GetValueOrDefault(0),
                                LoaiThu.BHYT => x.FThuBHYTNLD.GetValueOrDefault(0),
                                LoaiThu.BHTN => x.FThuBHTNNLD.GetValueOrDefault(0),
                                _ => x.FThuBHXHNLD.GetValueOrDefault(0) + x.FThuBHYTNLD.GetValueOrDefault(0) + x.FThuBHTNNLD.GetValueOrDefault(0),
                            }) / donViTinh),
                            NguoiSDLD = (long)Math.Round((LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong ? 0 : LoaiThu switch
                            {
                                LoaiThu.BHXH => x.FThuBHXHNSD.GetValueOrDefault(0),
                                LoaiThu.BHYT => x.FThuBHYTNSD.GetValueOrDefault(0),
                                LoaiThu.BHTN => x.FThuBHTNNSD.GetValueOrDefault(0),
                                _ => x.FThuBHXHNSD.GetValueOrDefault(0) + x.FThuBHYTNSD.GetValueOrDefault(0) + x.FThuBHTNNSD.GetValueOrDefault(0),
                            }) / donViTinh),
                            MoTa = x.STenBhMLNS,
                            L = x.SL,
                            K = x.SK,
                            M = x.SM,
                            TM = x.STM,
                            TTM = x.STTM,
                            NG = x.SNG,
                            TNG = x.STNG,
                            IsHangCha = x.IsHangCha
                        }).ToList();

                        listDataDonViRow.Add(new RptBhThongTriQttBHXHModel()
                        {
                            SMaDonVi = item.IIDMaDonVi,
                            MoTa = item.AgencyName,
                            //NguoiLD = listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiLD),
                            NguoiLD = LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong ? 0 : LoaiThu switch
                            {
                                LoaiThu.BHXH => listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHXHNLD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                LoaiThu.BHYT => listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHYTNLD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                LoaiThu.BHTN => listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHTNNLD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                _ => listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiLD)
                                + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHXHNLD.GetValueOrDefault(0) ?? 0) / donViTinh)
                                + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHYTNLD.GetValueOrDefault(0) ?? 0) / donViTinh)
                                + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHTNNLD.GetValueOrDefault(0) ?? 0) / donViTinh),
                            },
                            //NguoiSDLD = listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiSDLD)
                            NguoiSDLD = LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong ? 0 : LoaiThu switch
                            {
                                LoaiThu.BHXH => listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiSDLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHXHNSD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                LoaiThu.BHYT => listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiSDLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHYTNSD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                LoaiThu.BHTN => listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiSDLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHTNNSD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                _ => listData.Where(x => !x.IsHangCha).Sum(x => x.NguoiSDLD)
                                + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHXHNSD.GetValueOrDefault(0) ?? 0) / donViTinh)
                                + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHYTNSD.GetValueOrDefault(0) ?? 0) / donViTinh)
                                + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.SMaDonVi == item.IIDMaDonVi)?.FTruyThuBHTNNSD.GetValueOrDefault(0) ?? 0) / donViTinh),
                            }
                        });


                    }

                    string cap1 = GetLevelTitle(_dmChuKy, 1);
                    string cap2 = GetLevelTitle(_dmChuKy, 2);
                    templateFileName = GetTemplate();
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    data.Add("Items", listDataDonViRow.Where(x => x.NguoiLD != 0 || x.NguoiSDLD != 0));
                    data.Add("DonVi", "Tổng hợp đơn vị");
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("TongNguoiLD", listDataDonViRow.Where(x => !x.IsHangCha).Sum(x => x.NguoiLD));
                    data.Add("TongNguoiSDLD", listDataDonViRow.Where(x => !x.IsHangCha).Sum(x => x.NguoiSDLD));
                    data.Add("TongCong", listDataDonViRow.Where(x => !x.IsHangCha).Sum(x => x.TongCong));
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);

                    data.Add("Cap1", !string.IsNullOrEmpty(cap1) ? cap1 : (itemDanhMuc != null ? itemDanhMuc.SGiaTri : ""));
                    data.Add("Cap2", !string.IsNullOrEmpty(cap2) ? cap2 : _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Nam", DateTime.Now.Year);
                    data.Add("ThoiGian", thoigian);
                    data.Add("TienTuChi", StringUtils.NumberToText(listDataDonViRow.Where(x => !x.IsHangCha).Sum(x => x.TongCong) * Convert.ToInt32(SelectedUnit.ValueItem)));
                    AddChuKy(data, _typeChuky);

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    if (LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong)
                    {
                        var xlsFile = _exportService.Export<RptBhThongTriQttBHXHModel>(templateFileName, data, new List<int> { 12 });
                        exportResults.Add(new ExportResult("Thông tri quyết toán thu " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }
                    else if (LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong)
                    {
                        var xlsFile = _exportService.Export<RptBhThongTriQttBHXHModel>(templateFileName, data, new List<int> { 11 });
                        exportResults.Add(new ExportResult("Thông tri quyết toán thu " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }
                    else
                    {
                        var xlsFile = _exportService.Export<RptBhThongTriQttBHXHModel>(templateFileName, data);
                        exportResults.Add(new ExportResult("Thông tri quyết toán thu " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }

                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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


        public void ExportQuyetToanThuBHYT(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";

                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    var selectedQuarterType = Int32.Parse(QuarterMonthQTTSelected?.HiddenValue ?? "1");
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    string cap1 = GetLevelTitle(_dmChuKy, 1);
                    string cap2 = GetLevelTitle(_dmChuKy, 2);
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    foreach (var item in lstDonViSelected)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        var thoigian = $"{QuarterMonthQTTSelected.DisplayItem} năm {_sessionInfo.YearOfWork}";

                        BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
                        searchCondition.INamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IQuyNamLoai = selectedQuarterType;
                        searchCondition.IQuyNam = selectedQuarter;
                        searchCondition.IdDonViFilter = item.IIDMaDonVi;
                        searchCondition.IIDMaDonVi = item.IIDMaDonVi;
                        searchCondition.IsDonViCha = _sessionService.Current.IdDonVi == item.IIDMaDonVi;
                        var dataTruyThu = _explainService.GetGiaiThichTruyThu(_sessionInfo.YearOfWork, item.IIDMaDonVi, selectedQuarter, selectedQuarterType);
                        var temp = _mapper.Map<List<BhQttBHXHChiTietModel>>(_chungTuChiTietService.FindVoucherDetailsByCondition(searchCondition));
                        temp = temp.Where(x => !x.STenBhMLNS.StartsWith("Thu")).ToList();
                        var listData = temp.Select(x => new RptBhThongTriQttBHXHModel()
                        {
                            IIDMLNS = x.IIDMLNS,
                            IIDMLNSCha = x.IIDMLNSCha,
                            NguoiLD = (long)Math.Round((LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong ? 0 : LoaiThu switch
                            {
                                LoaiThu.BHXH => x.FThuBHXHNLD.GetValueOrDefault(0),
                                LoaiThu.BHYT => x.FThuBHYTNLD.GetValueOrDefault(0),
                                LoaiThu.BHTN => x.FThuBHTNNLD.GetValueOrDefault(0),
                                _ => x.FThuBHXHNLD.GetValueOrDefault(0) + x.FThuBHYTNLD.GetValueOrDefault(0) + x.FThuBHTNNLD.GetValueOrDefault(0),
                            }) / donViTinh),
                            NguoiSDLD = (long)Math.Round((LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong ? 0 : LoaiThu switch
                            {
                                LoaiThu.BHXH => x.FThuBHXHNSD.GetValueOrDefault(0),
                                LoaiThu.BHYT => x.FThuBHYTNSD.GetValueOrDefault(0),
                                LoaiThu.BHTN => x.FThuBHTNNSD.GetValueOrDefault(0),
                                _ => x.FThuBHXHNSD.GetValueOrDefault(0) + x.FThuBHYTNSD.GetValueOrDefault(0) + x.FThuBHTNNSD.GetValueOrDefault(0),
                            }) / donViTinh),
                            MoTa = x.STenBhMLNS,
                            L = x.SL,
                            K = x.SK,
                            M = x.SM,
                            TM = x.STM,
                            TTM = x.STTM,
                            NG = x.SNG,
                            TNG = x.STNG,
                            IsHangCha = x.IsHangCha
                        }).ToList();

                        var listGroupData = listData.GroupBy(g => g.IIDMLNS)
                            .Select(x => new RptBhThongTriQttBHXHModel()
                            {
                                IIDMLNS = x.First().IIDMLNS,
                                IIDMLNSCha = x.First().IIDMLNSCha,
                                NguoiLD = LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong ? 0 : LoaiThu switch
                                {
                                    LoaiThu.BHXH => x.Sum(s => s.NguoiLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHXHNLD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                    LoaiThu.BHYT => x.Sum(s => s.NguoiLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHYTNLD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                    LoaiThu.BHTN => x.Sum(s => s.NguoiLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHTNNLD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                    _ => x.Sum(s => s.NguoiLD)
                                    + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHXHNLD.GetValueOrDefault(0) ?? 0) / donViTinh)
                                    + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHYTNLD.GetValueOrDefault(0) ?? 0) / donViTinh)
                                    + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHTNNLD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                },
                                NguoiSDLD = LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong ? 0 : LoaiThu switch
                                {
                                    LoaiThu.BHXH => x.Sum(s => s.NguoiSDLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHXHNSD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                    LoaiThu.BHYT => x.Sum(s => s.NguoiSDLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHYTNSD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                    LoaiThu.BHTN => x.Sum(s => s.NguoiSDLD) + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHTNNSD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                    _ => x.Sum(s => s.NguoiSDLD)
                                    + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHXHNSD.GetValueOrDefault(0) ?? 0) / donViTinh)
                                    + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHYTNSD.GetValueOrDefault(0) ?? 0) / donViTinh)
                                    + (long)Math.Round((dataTruyThu.FirstOrDefault(d => d.IIDMLNS == x.First().IIDMLNS)?.FTruyThuBHTNNSD.GetValueOrDefault(0) ?? 0) / donViTinh),
                                },
                                MoTa = x.First().MoTa,
                                L = x.First().L,
                                K = x.First().K,
                                M = x.First().M,
                                TM = x.First().TM,
                                TTM = x.First().TTM,
                                NG = x.First().NG,
                                TNG = x.First().TNG,
                                IsHangCha = x.First().IsHangCha
                            }).ToList();

                        CalculateData(listGroupData);

                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        data.Add("Items", listGroupData.Where(x => x.NguoiLD != 0 || x.NguoiSDLD != 0));
                        data.Add("DonVi", item.TenDonVi);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("TongNguoiLD", listGroupData.Where(x => !x.IsHangCha).Sum(x => x.NguoiLD));
                        data.Add("TongNguoiSDLD", listGroupData.Where(x => !x.IsHangCha).Sum(x => x.NguoiSDLD));
                        data.Add("TongCong", listGroupData.Where(x => !x.IsHangCha).Sum(x => x.TongCong));
                        data.Add("currencyToText", currencyToText);
                        data.Add("FormatNumber", formatNumber);

                        //data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                        //data.Add("Cap2", _sessionInfo.TenDonVi);
                        data.Add("Cap1", !string.IsNullOrEmpty(cap1) ? cap1 : (itemDanhMuc != null ? itemDanhMuc.SGiaTri : ""));
                        data.Add("Cap2", !string.IsNullOrEmpty(cap2) ? cap2 : _sessionInfo.TenDonVi);
                        data.Add("h1", "");
                        data.Add("h2", "");
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Nam", DateTime.Now.Year);
                        data.Add("ThoiGian", thoigian);
                        data.Add("TienTuChi", StringUtils.NumberToText(listGroupData.Where(x => !x.IsHangCha).Sum(x => x.TongCong) * Convert.ToInt32(SelectedUnit.ValueItem)));
                        AddChuKy(data, _typeChuky);

                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                        if (LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong)
                        {
                            var xlsFile = _exportService.Export<RptBhThongTriQttBHXHModel>(templateFileName, data, new List<int> { 12 });
                            exportResults.Add(new ExportResult("Thông tri quyết toán thu " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                        else if (LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong)
                        {
                            var xlsFile = _exportService.Export<RptBhThongTriQttBHXHModel>(templateFileName, data, new List<int> { 11 });
                            exportResults.Add(new ExportResult("Thông tri quyết toán thu " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                        else
                        {
                            var xlsFile = _exportService.Export<RptBhThongTriQttBHXHModel>(templateFileName, data);
                            exportResults.Add(new ExportResult("Thông tri quyết toán thu " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                    }
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        private void CalculateParent(Guid idParent, RptBhThongTriQttBHXHModel item, List<RptBhThongTriQttBHXHModel> listData)
        {
            var dictByMlns = listData.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.NguoiLD += item.NguoiLD;
            model.NguoiSDLD += item.NguoiSDLD;
            CalculateParent(model.IIDMLNSCha, item, listData);
        }

        private void CalculateData(List<RptBhThongTriQttBHXHModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.NguoiSDLD = 0;
                    x.NguoiLD = 0;
                });

            var temp = listData.Where(x => !x.IsHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, listData);
            }
        }

        private void CalculateDataExport(IEnumerable<BhReportQttBHXHChiTietQuery> lstData)
        {
            if (lstData.IsEmpty())
                return;
            var lstParentRecal = lstData.Where(x => lstData.Where(w => !w.IIdParent.IsNullOrEmpty()).Select(x => x.IIdParent).Distinct().Contains(x.IIdChungTu)).OrderByDescending(o => o.ILevel).ToList();
            foreach (var item in lstParentRecal)
            {
                item.FDuToan = lstData.Where(x => x.IIdParent.Equals(item.IIdChungTu)).Sum(s => s.FDuToan);
                item.FHachToan = lstData.Where(x => x.IIdParent.Equals(item.IIdChungTu)).Sum(s => s.FHachToan);
            }
            if (lstData.Any(x => x.IKinhPhiKCB == 3))
            {
                var itemKCB3 = lstData.FirstOrDefault(x => x.IKinhPhiKCB == 3);
                var itemKCB2 = lstData.FirstOrDefault(x => x.IKinhPhiKCB == 2);
                var itemKCB1 = lstData.FirstOrDefault(x => x.IKinhPhiKCB == 1);
                itemKCB3.FDuToan = itemKCB1.FDuToan - itemKCB2.FDuToan;
                itemKCB3.FHachToan = itemKCB1.FHachToan - itemKCB2.FHachToan;
            }
        }
        private void CalculateData(List<BhQttBHXHChiTietQuery> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.IQSBQNam = 0;
                    x.FLuongChinh = 0;
                    x.FPhuCapChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                    x.FTongQuyTienLuongNam = 0;
                    x.FDuToan = 0;
                    x.FDaQuyetToan = 0;
                    x.FConLai = 0;
                    x.FThuBHXHNLD = 0;
                    x.FThuBHXHNSD = 0;
                    x.FTongSoPhaiThuBHXH = 0;
                    x.FThuBHYTNLD = 0;
                    x.FThuBHYTNSD = 0;
                    x.FTongSoPhaiThuBHYT = 0;
                    x.FThuBHTNNLD = 0;
                    x.FThuBHTNNSD = 0;
                    x.FTongSoPhaiThuBHTN = 0;
                    x.FTongNLD = 0;
                    x.FTongNSD = 0;
                    x.FTongCong = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, BhQttBHXHChiTietQuery item, List<BhQttBHXHChiTietQuery> lstChungTuChiTiet)
        {
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.IQSBQNam = (model.IQSBQNam ?? 0) + (item.IQSBQNam ?? 0);
            model.FLuongChinh = (model.FLuongChinh ?? 0) + (item.FLuongChinh ?? 0);
            model.FPhuCapChucVu = (model.FPhuCapChucVu ?? 0) + (item.FPhuCapChucVu ?? 0);
            model.FPCTNNghe = (model.FPCTNNghe ?? 0) + (item.FPCTNNghe ?? 0);
            model.FPCTNVuotKhung = (model.FPCTNVuotKhung ?? 0) + (item.FPCTNVuotKhung ?? 0);
            model.FNghiOm = (model.FNghiOm ?? 0) + (item.FNghiOm ?? 0);
            model.FHSBL = (model.FHSBL ?? 0) + (item.FHSBL ?? 0);
            model.FTongQuyTienLuongNam = (model.FTongQuyTienLuongNam ?? 0) + (item.FTongQuyTienLuongNam ?? 0);
            model.FDuToan = (model.FDuToan ?? 0) + (item.FDuToan ?? 0);
            model.FDaQuyetToan = (model.FDaQuyetToan ?? 0) + (item.FDaQuyetToan ?? 0);
            model.FConLai = (model.FConLai ?? 0) + (item.FConLai ?? 0);
            model.FThuBHXHNLD = (model.FThuBHXHNLD ?? 0) + (item.FThuBHXHNLD ?? 0);
            model.FThuBHXHNSD = (model.FThuBHXHNSD ?? 0) + (item.FThuBHXHNSD ?? 0);
            model.FTongSoPhaiThuBHXH = (model.FTongSoPhaiThuBHXH ?? 0) + (item.FTongSoPhaiThuBHXH ?? 0);
            model.FThuBHYTNLD = (model.FThuBHYTNLD ?? 0) + (item.FThuBHYTNLD ?? 0);
            model.FThuBHYTNSD = (model.FThuBHYTNSD ?? 0) + (item.FThuBHYTNSD ?? 0);
            model.FTongSoPhaiThuBHYT = (model.FTongSoPhaiThuBHYT ?? 0) + (item.FTongSoPhaiThuBHYT ?? 0);
            model.FThuBHTNNLD = (model.FThuBHTNNLD ?? 0) + (item.FThuBHTNNLD ?? 0);
            model.FThuBHTNNSD = (model.FThuBHTNNSD ?? 0) + (item.FThuBHTNNSD ?? 0);
            model.FTongSoPhaiThuBHTN = (model.FTongSoPhaiThuBHTN ?? 0) + (item.FTongSoPhaiThuBHTN ?? 0);
            model.FTongNLD = (model.FTongNLD ?? 0) + (item.FTongNLD ?? 0);
            model.FTongNSD = (model.FTongNSD ?? 0) + (item.FTongNSD ?? 0);
            model.FTongCong = (model.FTongCong ?? 0) + (item.FTongCong ?? 0);
            CalculateParent(model.IIDMLNSCha, item, lstChungTuChiTiet);
        }

        private void CalculateDataTruyThu(List<BhQttBHXHChiTietGiaiThichQuery> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FTruyThuBhxhNldDT = 0;
                    x.FTruyThuBhxhNsdDT = 0;
                    x.FTruyThuBhxhTongCongDT = 0;
                    x.FTruyThuBhytNldDT = 0;
                    x.FTruyThuBhytNsdDT = 0;
                    x.FTruyThuBhytTongCongDT = 0;
                    x.FTruyThuBhtnNldDT = 0;
                    x.FTruyThuBhtnNsdDT = 0;
                    x.FTruyThuBhtnTongCongDT = 0;
                    x.FTruyThuBhxhNldHT = 0;
                    x.FTruyThuBhxhNsdHT = 0;
                    x.FTruyThuBhxhTongCongHT = 0;
                    x.FTruyThuBhytNldHT = 0;
                    x.FTruyThuBhytNsdHT = 0;
                    x.FTruyThuBhytTongCongHT = 0;
                    x.FTruyThuBhtnNldHT = 0;
                    x.FTruyThuBhtnNsdHT = 0;
                    x.FTruyThuBhtnTongCongHT = 0;
                    x.FTongCongTruyThuBHXH = 0;
                    x.FTongCongTruyThuBHYT = 0;
                    x.FTongCongTruyThuBHTN = 0;
                    x.FTongTruyThu = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParentTruyThu(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }
        }

        private void CalculateParentTruyThu(Guid? idParent, BhQttBHXHChiTietGiaiThichQuery item, List<BhQttBHXHChiTietGiaiThichQuery> lstChungTuChiTiet)
        {
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.FTruyThuBhxhNldDT = (model.FTruyThuBhxhNldDT.GetValueOrDefault()) + (item.FTruyThuBhxhNldDT.GetValueOrDefault());
            model.FTruyThuBhxhNsdDT = (model.FTruyThuBhxhNsdDT.GetValueOrDefault()) + (item.FTruyThuBhxhNsdDT.GetValueOrDefault());
            model.FTruyThuBhxhTongCongDT = (model.FTruyThuBhxhTongCongDT.GetValueOrDefault()) + (item.FTruyThuBhxhTongCongDT.GetValueOrDefault());
            model.FTruyThuBhytNldDT = (model.FTruyThuBhytNldDT.GetValueOrDefault()) + (item.FTruyThuBhytNldDT.GetValueOrDefault());
            model.FTruyThuBhytNsdDT = (model.FTruyThuBhytNsdDT.GetValueOrDefault()) + (item.FTruyThuBhytNsdDT.GetValueOrDefault());
            model.FTruyThuBhytTongCongDT = (model.FTruyThuBhytTongCongDT.GetValueOrDefault()) + (item.FTruyThuBhytTongCongDT.GetValueOrDefault());
            model.FTruyThuBhtnNldDT = (model.FTruyThuBhtnNldDT.GetValueOrDefault()) + (item.FTruyThuBhtnNldDT.GetValueOrDefault());
            model.FTruyThuBhtnNsdDT = (model.FTruyThuBhtnNsdDT.GetValueOrDefault()) + (item.FTruyThuBhtnNsdDT.GetValueOrDefault());
            model.FTruyThuBhtnTongCongDT = (model.FTruyThuBhtnTongCongDT.GetValueOrDefault()) + (item.FTruyThuBhtnTongCongDT.GetValueOrDefault());
            model.FTruyThuBhxhNldHT = (model.FTruyThuBhxhNldHT.GetValueOrDefault()) + (item.FTruyThuBhxhNldHT.GetValueOrDefault());
            model.FTruyThuBhxhNsdHT = (model.FTruyThuBhxhNsdHT.GetValueOrDefault()) + (item.FTruyThuBhxhNsdHT.GetValueOrDefault());
            model.FTruyThuBhxhTongCongHT = (model.FTruyThuBhxhTongCongHT.GetValueOrDefault()) + (item.FTruyThuBhxhTongCongHT.GetValueOrDefault());
            model.FTruyThuBhytNldHT = (model.FTruyThuBhytNldHT.GetValueOrDefault()) + (item.FTruyThuBhytNldHT.GetValueOrDefault());
            model.FTruyThuBhytNsdHT = (model.FTruyThuBhytNsdHT.GetValueOrDefault()) + (item.FTruyThuBhytNsdHT.GetValueOrDefault());
            model.FTruyThuBhytTongCongHT = (model.FTruyThuBhytTongCongHT.GetValueOrDefault()) + (item.FTruyThuBhytTongCongHT.GetValueOrDefault());
            model.FTruyThuBhtnNldHT = (model.FTruyThuBhtnNldHT.GetValueOrDefault()) + (item.FTruyThuBhtnNldHT.GetValueOrDefault());
            model.FTruyThuBhtnNsdHT = (model.FTruyThuBhtnNsdHT.GetValueOrDefault()) + (item.FTruyThuBhtnNsdHT.GetValueOrDefault());
            model.FTruyThuBhtnTongCongHT = (model.FTruyThuBhtnTongCongHT.GetValueOrDefault()) + (item.FTruyThuBhtnTongCongHT.GetValueOrDefault());
            model.FTongCongTruyThuBHXH = (model.FTongCongTruyThuBHXH.GetValueOrDefault()) + (item.FTongCongTruyThuBHXH.GetValueOrDefault());
            model.FTongCongTruyThuBHYT = (model.FTongCongTruyThuBHYT.GetValueOrDefault()) + (item.FTongCongTruyThuBHYT.GetValueOrDefault());
            model.FTongCongTruyThuBHTN = (model.FTongCongTruyThuBHTN.GetValueOrDefault()) + (item.FTongCongTruyThuBHTN.GetValueOrDefault());
            model.FTongTruyThu = (model.FTongTruyThu.GetValueOrDefault()) + (item.FTongTruyThu.GetValueOrDefault());
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ThuaUyQuyen1", dmChyKy == null ? string.Empty : dmChyKy.ThuaUyQuyen1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);

            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ThuaUyQuyen2", dmChyKy == null ? string.Empty : dmChyKy.ThuaUyQuyen2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);

            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ThuaUyQuyen3", dmChyKy == null ? string.Empty : dmChyKy.ThuaUyQuyen3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);

            data.Add("ThuaLenh4", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh4MoTa);
            data.Add("ChucDanh4", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh4MoTa);
            data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten4", dmChyKy == null ? string.Empty : dmChyKy.Ten4MoTa);

            data.Add("ThuaLenh5", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh5MoTa);
            data.Add("ChucDanh5", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh5MoTa);
            data.Add("GhiChuKy5", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten5", dmChyKy == null ? string.Empty : dmChyKy.Ten5MoTa);

            data.Add("ThuaLenh6", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh6MoTa);
            data.Add("ChucDanh6", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh6MoTa);
            data.Add("GhiChuKy6", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten6", dmChyKy == null ? string.Empty : dmChyKy.Ten6MoTa);
            if (dmChyKy != null && (!dmChyKy.ThuaLenh4MoTa.IsEmpty() || !dmChyKy.ChucDanh4MoTa.IsEmpty() || !dmChyKy.Ten4MoTa.IsEmpty()))
            {
                data.Add("Co6ChuKy", true);
            }
        }

        private void LoadTypeChuKy()
        {
            if (LoaiThu == LoaiThu.All && LoaiThongTriThu == LoaiThongTriThu.TongHop)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL;

            }
            else if (LoaiThu == LoaiThu.BHXH && LoaiThongTriThu == LoaiThongTriThu.TongHop)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHXH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHXH;
            }
            else if (LoaiThu == LoaiThu.BHYT && LoaiThongTriThu == LoaiThongTriThu.TongHop)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHYT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHYT;
            }
            else if (LoaiThu == LoaiThu.BHTN && LoaiThongTriThu == LoaiThongTriThu.TongHop)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHTN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHTN;
            }
            else if (LoaiThu == LoaiThu.All && LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_ALL_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_ALL_NLD;
            }
            else if (LoaiThu == LoaiThu.BHXH && LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHXH_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHXH_NLD;
            }
            else if (LoaiThu == LoaiThu.BHYT && LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHYT_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHYT_NLD;
            }
            else if (LoaiThu == LoaiThu.BHTN && LoaiThongTriThu == LoaiThongTriThu.NguoiLaoDong)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHTN_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHTN_NLD;
            }
            else if (LoaiThu == LoaiThu.All && LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_ALL_NSD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_ALL_NSD;
            }
            else if (LoaiThu == LoaiThu.BHXH && LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHXH_NSD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHXH_NSD;
            }
            else if (LoaiThu == LoaiThu.BHYT && LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHYT_NSD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHYT_NSD;
            }
            else if (LoaiThu == LoaiThu.BHTN && LoaiThongTriThu == LoaiThongTriThu.NguoiSuDungLaoDong)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHTN_NSD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHTN_NSD;
            }
            else if (LoaiThu == LoaiThu.All && LoaiThongTriThu == LoaiThongTriThu.TongHopChung)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC;
            }
            else if (LoaiThu == LoaiThu.All && LoaiThongTriThu == LoaiThongTriThu.TongHopChungNLD)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC_NLD;
            }
            else if (LoaiThu == LoaiThu.All && LoaiThongTriThu == LoaiThongTriThu.TongHopChungNSD)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC_NSD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC_NSD;
            }

            if (_dmChuKy is null)
                _dmChuKy = new DmChuKy();
            Title1 = !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa) ? _dmChuKy.TieuDe1MoTa : "THÔNG TRI";

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
            {
                Title2 = _dmChuKy.TieuDe2MoTa;
            }
            else
            {
                Title2 = $@"{LoaiThongTriThu switch
                {
                    LoaiThongTriThu.NguoiLaoDong => "Xác nhận số thu người LĐ",
                    LoaiThongTriThu.NguoiSuDungLaoDong => "Xác nhận số thu người SDLĐ",
                    _ => "Tổng hợp số thu"
                }} {LoaiThu switch
                {
                    LoaiThu.BHXH => "Bảo hiểm xã hội",
                    LoaiThu.BHYT => "Bảo hiểm y tế",
                    LoaiThu.BHTN => "Bảo hiểm thân nhân",
                    _ => "BHXH, BHYT, BHTN"
                }}";

                if (LoaiThongTriThu == LoaiThongTriThu.TongHopChung && LoaiThu == LoaiThu.All)
                    Title2 = "Thu bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp";
            }
        }

        private string ConvertQuarterMonthText(int type, int input)
        {
            string sQuarterMonth = string.Empty;
            int year = _sessionInfo.YearOfWork;
            if (type == 0)
                sQuarterMonth = $"Tháng {input.ToString()} năm {year}";
            else if (type == 1 && input == 3)
                sQuarterMonth = $"Quý I năm {year}";
            else if (type == 1 && input == 6)
                sQuarterMonth = $"Quý II năm {year}";
            else if (type == 1 && input == 9)
                sQuarterMonth = $"Quý III năm {year}";
            else if (type == 1 && input == 12)
                sQuarterMonth = $"Quý IV năm {year}";

            return sQuarterMonth;
        }
    }
}
