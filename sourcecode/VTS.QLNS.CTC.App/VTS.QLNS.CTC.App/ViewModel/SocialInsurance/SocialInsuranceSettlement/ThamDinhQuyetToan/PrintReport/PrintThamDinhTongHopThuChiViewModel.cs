using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport
{
    public class PrintThamDinhTongHopThuChiViewModel : ViewModelBase
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
        private IBhThamDinhQuyetToanService _chungTuService;
        private IBhThamDinhQuyetToanChiTietService _chungTuChiTietService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private IBhBaoCaoGhiChuService _bhGhiChuService;
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
        public bool IsQuanLyDonViCha;
        public bool IsShowAll { get; set; }
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public int SettlementTypeValue;
        private string _typeChuky;
        private string _donViChungTu { get; set; }
        private string SettlementName => SettlementTypeValue switch
        {
            (int)BhThamDinhQuyetToanType.PRINT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI => "In thông báo phê duyệt quyết toán năm - Tổng hợp thu, chi",
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN => "In báo cáo tổng hợp quyết toán thu, chi BHXH, BHYT, BHTN",
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU => "In dự toán kinh phí BHXH, BHYT, BHTN chuyển năm sau",
            _ => ""
        };

        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
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

        #region list agency
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
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

        private bool _isEnableReportType;
        public bool IsEnableReportType
        {
            get => _isEnableReportType;
            set => SetProperty(ref _isEnableReportType, value);
        }

        public bool IsEnableReportTypeYear => SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU;

        private bool _isEnableInTheo;
        public bool IsEnableInTheo
        {
            get => _isEnableInTheo;
            set => SetProperty(ref _isEnableInTheo, value);
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

        private bool _isDatePeople;
        public bool IsDatePeople
        {
            get => _isDatePeople;
            set
            {
                SetProperty(ref _isDatePeople, value);
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

        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();

        private bool IsReportTypeAgencyDetail => _selectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString();

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set
            {

                if (SetProperty(ref _isSummary, value))
                {
                    LoadAgencies();
                }
            }
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }

        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }

        public PrintThamDinhTongHopThuChiViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IBhThamDinhQuyetToanService chungTuService,
            IBhThamDinhQuyetToanChiTietService chungTuChiTietService,
            IExportService exportService,
            INsDonViService donViService,
            INsNguoiDungDonViService nguoiDungDonViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _exportService = exportService;
            _donViService = donViService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;
            _bhGhiChuService = bhBaoCaoGhiChuService;

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
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }

        public override void Init()
        {
            base.Init();
            IsSummary = false;
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _agencies = new ObservableCollection<AgencyModel>();
            LoadReportType();
            LoadDanhMuc();
            LoadAgencies();
            LoadTypeChuKy();
        }

        private void LoadAgencies()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    var lstIdDonVi = GetListIdDonVi();
                    IsLoading = true;
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        agencies = agencies.Where(x => x.Loai != LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    else if (IsEnableCheckBoxSummary && !IsSummary && !IsReportTypeAgencyDetail)
                    {
                        agencies = agencies.Where(x => lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    else
                    {
                        agencies = agencies.Where(x => x.Loai == LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
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
                            }
                        };
                    }
                    OnPropertyChanged(nameof(Agencies));
                    OnPropertyChanged(nameof(IsSelectedAllAgency));
                    OnPropertyChanged(nameof(SelectedAgencyCount));
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
            List<BhThamDinhQuyetToan> listChungTuDuocXem = new List<BhThamDinhQuyetToan>();
            if (IsSummary && !IsReportTypeAgencyDetail)
            {
                listChungTuDuocXem = _chungTuService.FindUnitAggregateVoucher(yearOfWork).ToList();
            }
            else
            {
                listChungTuDuocXem = _chungTuService.FindUnitVoucher(yearOfWork).ToList();
            }
            var lstIdDonVi = listChungTuDuocXem.Select(x => x.IID_MaDonVi).Distinct().ToList();
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

        private void LoadTieuDe()
        {
            LoadTypeChuKy();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() }
            };
            _selectedReportType = _reportTypes.First();
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
            string input = "";
            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI);
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN);
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_DU_TOAN_KINH_PHI_CHUYEN_NAM_SAU);
            }

            return Path.Combine(ExportPrefix.PATH_BH_THAMDINHQUYETTOAN, input + FileExtensionFormats.Xlsx);
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

            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI)
            {
                ExportPheDuyetQuyetToanNamTongHopThuChi(exportType);
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN)
            {
                ExportTongHopQuyetToanThuChi(exportType);
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU)
            {
                ExportDuToanKinhPhiChuyenNamSau(exportType);
            }
        }

        //Phê duyệt quyết toán
        public void ExportPheDuyetQuyetToanNamTongHopThuChi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    bool isTongHop = true;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                    }
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    var lstMaDonVi = string.Join(",", Agencies.Any(x => x.Selected) ? Agencies.Where(item => item.Selected).Select(x => x.IIDMaDonVi) : new List<string>());
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    if (isTongHop || (IsSummary && !IsReportTypeAgencyDetail))
                    {
                        exportResults.Add(ExportResultReport(donViTinh, exportType, lstMaDonVi, isTongHop));
                    }
                    else
                    {
                        foreach (var item in Agencies.Where(x => x.Selected))
                        {
                            _donViChungTu = item.TenDonVi;
                            exportResults.Add(ExportResultReport(donViTinh, exportType, item.IIDMaDonVi, isTongHop));
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

        private ExportResult ExportResultReport(int donViTinh, ExportType exportType, string lstDonVi, bool isTongHop)
        {
            try
            {
                string templateFileName = "", fileNamePrefix = "";
                templateFileName = GetTemplate();
                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                CurrencyToText currencyToText = new CurrencyToText();
                data.Add("currencyToText", currencyToText);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", _sessionInfo.TenDonVi);
                data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                data.Add("DiaDiem", _diaDiem);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("Year", _sessionInfo.YearOfWork);
                data.Add("DonVi", _donViChungTu);
                AddChuKy(data, _typeChuky);
                if (isTongHop)
                {
                    AddNote(data, _typeChuky);
                }
                else
                {
                    data.Add("IsShowUnit", true);
                    AddNote(data, _typeChuky, lstDonVi);
                }

                var lstDataThu = _chungTuChiTietService.ExportThongBaoPheDuyetThuChi(_sessionInfo.YearOfWork, lstDonVi, donViTinh, (int)TypeSettlement.THU);
                var lstDataChi = _chungTuChiTietService.ExportThongBaoPheDuyetThuChi(_sessionInfo.YearOfWork, lstDonVi, donViTinh, (int)TypeSettlement.CHI);
                CalculateDataExport(lstDataThu);
                CalculateDataExport(lstDataChi);
                data.Add("TieuDe1", Title1);
                data.Add("TieuDe2", Title2);
                data.Add("TieuDe3", Title3);
                data.Add("TongSoTienChi", lstDataChi.Where(x => x.ILevel.Equals(1)).Sum(x => x.FTongSo));
                data.Add("TongSoTienThu", lstDataThu.Where(x => x.ILevel.Equals(1)).Sum(x => x.FTongSo));
                data.Add("TongDuToanChi", lstDataChi.Where(x => x.ILevel.Equals(1)).Sum(x => x.FDuToan));
                data.Add("TongDuToanThu", lstDataThu.Where(x => x.ILevel.Equals(1)).Sum(x => x.FDuToan));
                data.Add("TongHachToanChi", lstDataChi.Where(x => x.ILevel.Equals(1)).Sum(x => x.FHachToan));
                data.Add("TongHachToanThu", lstDataThu.Where(x => x.ILevel.Equals(1)).Sum(x => x.FHachToan));
                data.Add("ListDataThu", lstDataThu);
                data.Add("ListDataChi", lstDataChi);
                string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                var xlsFile = _exportService.Export<BhReportQttBHXHChiTietQuery>(templateFileName, data);
                return new ExportResult("QUYẾT TOÁN TỔNG HỢP NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw;
            }

        }

        //Tỏng hợp quyết toán thu chi bhxh, bhyt, bhtn
        public void ExportTongHopQuyetToanThuChi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    bool isTongHop = true;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                    }
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    var lstMaDonVi = string.Join(",", Agencies.Any(x => x.Selected) ? Agencies.Where(item => item.Selected).Select(x => x.IIDMaDonVi) : new List<string>());
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    if (isTongHop || (IsSummary && !IsReportTypeAgencyDetail))
                    {
                        exportResults.Add(ExportTHQTTCResultReport(donViTinh, exportType, lstMaDonVi, isTongHop));
                    }
                    else
                    {
                        foreach (var item in Agencies.Where(x => x.Selected))
                        {
                            _donViChungTu = item.TenDonVi;
                            exportResults.Add(ExportTHQTTCResultReport(donViTinh, exportType, item.IIDMaDonVi, isTongHop));
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

        private ExportResult ExportTHQTTCResultReport(int donViTinh, ExportType exportType, string lstDonVi, bool isTongHop)
        {
            try
            {
                string templateFileName = "", fileNamePrefix = "";
                templateFileName = GetTemplate();
                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                CurrencyToText currencyToText = new CurrencyToText();
                data.Add("currencyToText", currencyToText);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", _sessionInfo.TenDonVi);
                data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                data.Add("DiaDiem", _diaDiem);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("Year", _sessionInfo.YearOfWork);
                data.Add("DonVi", _donViChungTu);
                AddChuKy(data, _typeChuky);
                if (isTongHop)
                {
                    AddNote(data, _typeChuky);
                }
                else
                {
                    data.Add("IsShowUnit", true);
                    AddNote(data, _typeChuky, lstDonVi);
                }

                var lstData = _chungTuChiTietService.ExportTongHopQuyetToanThuChi(_sessionInfo.YearOfWork, lstDonVi, donViTinh, isTongHop);
                CalculateTHQTTCDataExport(lstData);
                data.Add("ListData", lstData);
                data.Add("TieuDe1", Title1);
                data.Add("TieuDe2", Title2);
                data.Add("TieuDe3", Title3);

                var tongCongThu = lstData.Where(x => x.ILevel.Equals(1) && x.IThuTu.Equals(1)).Sum(x => x.FSoTien);
                var tongCongChi = lstData.Where(x => x.ILevel.Equals(1) && x.IThuTu.Equals(2)).Sum(x => x.FSoTien);
                data.Add("TongCongThu", tongCongThu);
                data.Add("TongCongChi", tongCongChi);
                data.Add("TongCong", tongCongChi + tongCongThu);

                string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                var xlsFile = _exportService.Export<BhReportQttBHXHChiTietQuery>(templateFileName, data);
                return new ExportResult("QUYẾT TOÁN TỔNG HỢP NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw;
            }

        }

        private void ExportDuToanKinhPhiChuyenNamSau(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    if (lstIdDonVi != null)
                    {
                        var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                        List<BhThamDinhQuyetToanChiTietModel> lstData = new List<BhThamDinhQuyetToanChiTietModel>();
                        var tempData = _chungTuChiTietService.ExportDuToanKinhPhiChuyenNamSau(_sessionService.Current.YearOfWork, selectedUnits, donViTinh).OrderBy(x => x.IMa).ToList();
                        lstData = _mapper.Map<List<BhThamDinhQuyetToanChiTietModel>>(tempData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                        data.Add("ListDataDuToan", lstData.Where(x => x.ILoai == 2));
                        data.Add("ListDataHachToan", lstData.Where(x => x.ILoai == 1));
                        data.Add("YearWork", yearOfWork);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Year", lstData.First().INamLamViec);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);

                        data.Add("TongDuToanKPQL", lstData.Where(x => x.ILoai == 2).Sum(x => x.FKinhPhiQL));
                        data.Add("TongDuToanKPQY", lstData.Where(x => x.ILoai == 2).Sum(x => x.FKinhPhiKCBQuanY));
                        data.Add("TongDuToanKPQN", lstData.Where(x => x.ILoai == 2).Sum(x => x.FKinhPhiKCBQuanNhan));
                        data.Add("TongDuToan", lstData.Where(x => x.ILoai == 2).Sum(x => x.FTongCong));

                        data.Add("TongHachToanKPQL", lstData.Where(x => x.ILoai == 1).Sum(x => x.FKinhPhiQL));
                        data.Add("TongHachToanKPQY", lstData.Where(x => x.ILoai == 1).Sum(x => x.FKinhPhiKCBQuanY));
                        data.Add("TongHachToanKPQN", lstData.Where(x => x.ILoai == 1).Sum(x => x.FKinhPhiKCBQuanNhan));
                        data.Add("TongHachToan", lstData.Where(x => x.ILoai == 1).Sum(x => x.FTongCong));

                        data.Add("TongCongKPQL", lstData.Where(x => x.ILoai == 1).Sum(x => x.FKinhPhiQL) + lstData.Where(x => x.ILoai == 2).Sum(x => x.FKinhPhiQL));
                        data.Add("TongCongKPQY", lstData.Where(x => x.ILoai == 1).Sum(x => x.FKinhPhiKCBQuanY) + lstData.Where(x => x.ILoai == 2).Sum(x => x.FKinhPhiKCBQuanY));
                        data.Add("TongCongKPQN", lstData.Where(x => x.ILoai == 1).Sum(x => x.FKinhPhiKCBQuanNhan) + lstData.Where(x => x.ILoai == 2).Sum(x => x.FKinhPhiKCBQuanNhan));
                        data.Add("TongCongThu", lstData.Where(x => x.ILoai == 1).Sum(x => x.FTongCong) + lstData.Where(x => x.ILoai == 2).Sum(x => x.FTongCong));

                        AddChuKy(data, _typeChuky);
                        AddNote(data, _typeChuky);
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhThamDinhQuyetToanChiTietModel>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO THẨM ĐỊNH QUYẾT TOÁN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        e.Result = results;
                    }
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

        private void CalculateDataExport(IEnumerable<BhReportQttBHXHChiTietQuery> lstData)
        {
            if (lstData.IsEmpty())
                return;
            var lstParentRecal = lstData.Where(x => lstData.Where(w => !w.IIdParent.IsNullOrEmpty())
                .Select(x => x.IIdParent).Distinct().Contains(x.IIdChungTu) && x.STT != "II" && x.STT != "III").OrderByDescending(o => o.ILevel).ToList();
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

        private void CalculateTHQTTCDataExport(IEnumerable<BhReportQttBHXHChiTietQuery> lstData)
        {
            if (lstData.IsEmpty())
                return;
            var lstParentRecal = lstData.Where(x => lstData.Where(w => !w.IIdParent.IsNullOrEmpty())
                .Select(x => x.IIdParent).Distinct().Contains(x.IIdChungTu)).OrderByDescending(o => o.ILevel).ToList();
            foreach (var item in lstParentRecal)
            {
                item.FSoTien = lstData.Where(x => x.IIdParent.Equals(item.IIdChungTu)).Sum(s => s.FSoTien);
            }
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);

            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);

            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
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
            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : $"Phụ lục";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : $"Thông báo phê duyệt quyết toán thu, chi BHXH, BHYT, BHTN năm {_sessionInfo.YearOfWork}";
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : $"(Kèm theo Quyết định số:....../QĐ-BQP ngày....../....../...... của Bộ Quốc phòng)";
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_Bao_Cao_TH_Quyet_Toan_Thu_Chi_BHXH_BHYT_BHTN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_Bao_Cao_TH_Quyet_Toan_Thu_Chi_BHXH_BHYT_BHTN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : $"Phụ lục";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : $"Tổng hợp quyết toán thu, chi BHXH, BHYT, BHTN năm {_sessionInfo.YearOfWork}";
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : $"(Kèm theo Quyết định số:....../QĐ-BQP ngày....../....../...... của Bộ Quốc phòng)";
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BH_DU_TOAN_KINH_PHI_CHUYEN_NAM_SAU) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.RPT_BH_DU_TOAN_KINH_PHI_CHUYEN_NAM_SAU;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : $"Phụ lục XIV";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : $"Dự toán kinh phí BHXH, BHYT, BHTN năm {_sessionInfo.YearOfWork} chuyển sang năm {_sessionInfo.YearOfWork + 1}";
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : $"(Kèm theo Quyết định số:....../QĐ-BQP ngày....../....../...... của Bộ Quốc phòng)";
            }
        }

        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();

            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI)
            {
                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI };
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN)
            {
                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_Bao_Cao_TH_Quyet_Toan_Thu_Chi_BHXH_BHYT_BHTN };
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU)
            {
                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BH_DU_TOAN_KINH_PHI_CHUYEN_NAM_SAU };
            }

            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }

        public void AddNote(Dictionary<string, object> data, string idType, string idMaDonVi = null)
        {
            try
            {
                BhCauHinhBaoCao bhGhiChu;
                if (string.IsNullOrEmpty(idMaDonVi))
                {
                    bhGhiChu = _bhGhiChuService.FindByCondition(x => x.SMaBaoCao.Equals(idType) && x.INamLamViec == _sessionInfo.YearOfWork && x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencySummary)).FirstOrDefault();
                }
                else
                {
                    bhGhiChu = _bhGhiChuService.FindByCondition(x => x.SMaBaoCao.Equals(idType) && x.INamLamViec == _sessionInfo.YearOfWork && x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencyDetail) && x.IIdMaDonVi.Equals(idMaDonVi)).FirstOrDefault();
                }
                data.Add("GhiChu", bhGhiChu == null ? string.Empty : bhGhiChu.SGhiChu);

                if (bhGhiChu != null && !string.IsNullOrEmpty(bhGhiChu.SGhiChu))
                {
                    data.Add("ShowNote", true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
