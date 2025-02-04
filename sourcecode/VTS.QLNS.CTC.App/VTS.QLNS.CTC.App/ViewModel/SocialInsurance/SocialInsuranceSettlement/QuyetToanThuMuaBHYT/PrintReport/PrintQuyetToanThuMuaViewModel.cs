using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.PrintReport
{
    public class PrintQuyetToanThuMuaViewModel : ViewModelBase
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
        private IBhDmMucLucNganSachService _mucLucNganSachService;
        private IQttmBHYTService _chungTuService;
        private IQttmBHYTChiTietService _chungTuChiTietService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _isInitReport;
        private bool _checkAllAgencies;
        private string _quarterMonth;
        private int _quarterMonthType;
        private string _quarterMonthBefore;
        public BhQttmBHYTModel SettlementVoucher;
        public List<BhQttmBHYTChiTietModel> SettlementVoucherDetails;
        public bool IsShowAll { get; set; }
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public string name { get; set; }
        public int SettlementTypeValue;
        private string _typeChuky;
        private string SettlementName
        {
            get
            {
                switch (SettlementTypeValue)
                {
                    case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD:
                        name = "Quyết toán thu mua BHYT thân nhân";
                        break;
                    case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN:
                        name = "In bản quyết toán thu BHYT thân nhân";
                        break;
                    case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB:
                        name = "In bảng quyết toán thu BHYT HSSV HVQS xã phường và SQDB";
                        break;
                }
                return name;
            }
        }

        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();

        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
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
                OnPropertyChanged(nameof(IsEnabledExplanation));
                IsSummary = false;
                IsSummaryAgency = false;
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

        public bool IsEnabledExplanation => (IsEnableCheckBoxSummary && IsSummary) || (IsEnableCheckBoxSummary && IsSummaryAgency);

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

        private bool _isEnableInTheo;
        public bool IsEnableInTheo
        {
            get => _isEnableInTheo;
            set => SetProperty(ref _isEnableInTheo, value);
        }

        public bool IsExportEnable
        {
            get
            {
                if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN || SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB)
                {
                    return true;
                }
                else
                {
                    return (_isData || IsVerbalExplanation);
                }
            }
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

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set
            {
                if (SetProperty(ref _isSummary, value))
                {
                    OnPropertyChanged(nameof(IsEnabledExplanation));
                    LoadAgencies();
                }
            }
        }

        private bool _isSummaryAgency;
        public bool IsSummaryAgency
        {
            get => _isSummaryAgency;
            set
            {
                if (SetProperty(ref _isSummaryAgency, value))
                {
                    OnPropertyChanged(nameof(IsEnabledExplanation));
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

        public PrintQuyetToanThuMuaViewModel(IBhDmMucLucNganSachService mucLucNganSachService,
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IQttmBHYTService chungTuService,
            IQttmBHYTChiTietService chungTuChiTietService,
            IExportService exportService,
            INsDonViService donViService,
            INsNguoiDungDonViService nguoiDungDonViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            INsPhongBanService phongBanService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _mucLucNganSachService = mucLucNganSachService;
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
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _isInitReport = true;
            _agencies = new ObservableCollection<AgencyModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            IsDataInterpretation = false;
            ResetCondition();
            LoadTieuDe();
            LoadReportType();
            LoadQuarterYears();
            LoadDanhMuc();
            LoadAgencies();
            LoadTypeChuKy();
            LoadDiaDiem();
            _isInitReport = false;
            IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
        }

        private void LoadTypeChuKy()
        {
            switch (SettlementTypeValue)
            {
                case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD:
                    _typeChuky = TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD;
                    Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTTM_PHU_LUC_THU_BHYT_TN;
                    break;
                case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN:
                    _typeChuky = TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN;
                    Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTTM_PHU_LUC_BHYT_TN;
                    break;
                case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB:
                    _typeChuky = TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB;
                    Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTTM_PHU_LUC_BHYT_HSSV;
                    break;
            }
        }

        private void LoadAgencies()
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
                else if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && (IsSummaryAgency || (!IsSummaryAgency && !IsSummary)))
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

        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var selectedQuarter = Int32.Parse(QuarterMonthSelected?.ValueItem ?? "3");
            List<BhQttmBHYTQuery> listChungTuDuocXem = new List<BhQttmBHYTQuery>();
            if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD)
            {
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter).ToList();
                }
                else
                {
                    if (IsSummary)
                    {
                        listChungTuDuocXem = _chungTuService.FindChungTuDonViTongHop(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter).ToList();
                    }
                    else if (IsSummaryAgency && !IsSummary)
                    {
                        listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter).ToList();
                    }
                    else if (!IsSummaryAgency && !IsSummary)
                    {
                        listChungTuDuocXem = _chungTuService.FindChungTuDonVi(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, selectedQuarter).ToList();
                    }
                    //else
                    //{
                    //    listChungTuDuocXem = _chungTuService.FindChungTuDonViTongHop(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, _sessionService.Current.Principal, selectedQuarter).ToList();
                    //}
                }
            }
            else if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN)
            {
                listChungTuDuocXem = _chungTuService.FindAllChungTuDonVi(yearOfWork, yearOfWork).ToList();
            }
            else
            {
                listChungTuDuocXem = _chungTuService.FindAllChungTuDonVi(yearOfWork, yearOfWork).ToList();
            }    
            var lstIdDonVi = listChungTuDuocXem.Select(x => x.IIDMaDonVi).Distinct().ToList();
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
        }

        private void LoadTieuDe()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD => TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD,
                (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN => TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN,
                (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB => TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB
            };
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        public void LoadDiaDiem()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
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

        private void LoadQuarterYears()
        {
            if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD)
            {
                _quarterMonths = new List<ComboboxItem>();
                _quarterMonths.Add(new ComboboxItem("Quý I", "3"));
                _quarterMonths.Add(new ComboboxItem("Quý II", "6"));
                _quarterMonths.Add(new ComboboxItem("Quý III", "9"));
                _quarterMonths.Add(new ComboboxItem("Quý IV", "12"));
                _quarterMonths.Add(new ComboboxItem("Năm " + _sessionService.Current.YearOfWork, _sessionService.Current.YearOfWork.ToString()));
                QuarterMonthSelected = _quarterMonths.First();
            }
            else
            {
                var voucherYears = _chungTuService.GetVoucherYears(_sessionService.Current.YearOfWork);
                _quarterMonths = new List<ComboboxItem>();
                foreach (var year in voucherYears)
                {
                    _quarterMonths.Add(new ComboboxItem("Năm " + year, year.ToString()));
                }
                if (_quarterMonths.Count > 0)
                {
                    QuarterMonthSelected = _quarterMonths?.OrderByDescending(x => x.ValueItem).First() ?? null;
                }
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
            string input = "";
            var isYear = QuarterMonthSelected.DisplayItem.Contains("Năm");
            if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD && !isYear)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD);
            }
            else if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD && isYear)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD_NAM);
            }
            else if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QQUYET_TOAN_THU_MUA_BHYT_THAN_NHAN);
            }
            else
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QQUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB);
            }
            return Path.Combine(ExportPrefix.PATH_BH_QTTM, input + FileExtensionFormats.Xlsx);
        }
        private void OnConfigSign()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD => TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD,
                (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN => TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN,
                (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB => TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB
            };

            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = typeChuKy;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD)
            {
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.HasAddedSign5 = true;
            }
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
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

            if (QuarterMonthSelected == null)
            {
                MessageBoxHelper.Warning(Resources.ErrorQuarterEmpty);
                return;
            }

            if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD && IsData
                && (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString()
                || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummary)))
            {
                ExportQuyetToanThuMuaBhytThanNhanNLD(exportType);
            }
            else if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD && IsData
                && ((SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummaryAgency && !IsSummary)
                || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && !IsSummaryAgency && !IsSummary)))
            {
                ExportQttmBhytThanNhanNLDTongHopDonVi(exportType);
            }
            else
            {
                ExportQuyetToanThuMuaBhytThanNhan(exportType);
            }
        }

        private void ExportQuyetToanThuMuaBhytThanNhanNLD(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var selectedQuarter = Int32.Parse(QuarterMonthSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    foreach (var dv in lstIdDonVi)
                    {
                        bool isTongHop = true;
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                        {
                            isTongHop = false;
                        }
                        List<BhQttmBHYTChiTietQuery> lstData = new List<BhQttmBHYTChiTietQuery>();
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() ||
                        (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummaryAgency && !IsSummary))
                        {
                            lstData = _chungTuChiTietService.ExportQuyetToanThuMuaBhytThanNhanNLD(yearOfWork, dv.Id, donViTinh, isTongHop, selectedQuarter).ToList();
                        }
                        else
                        {
                            lstData = _chungTuChiTietService.ExportQTTMBhytThanNhanNLDTongHop(yearOfWork, dv.Id, donViTinh, isTongHop, selectedQuarter).ToList();
                        }
                        var donVi = _donViService.FindByMaDonViAndNamLamViec(dv.Id, yearOfWork);
                        CalculateData(lstData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                        data.Add("ListData", lstData.Where(x => x.HasDataToPrint));
                        data.Add("YearWork", yearOfWork);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Year", lstData.First().INamLamViec);
                        data.Add("Quarter", QuarterMonthSelected.DisplayItem);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("DonViChungTu", donVi.TenDonVi);
                        //Tính tổng cộng
                        data.Add("TongDuToan", lstData.Where(n => !n.BHangCha).Sum(x => x.FDuToan));
                        data.Add("TongDaQuyetToan", lstData.Where(n => !n.BHangCha).Sum(x => x.FDaQuyetToan));
                        data.Add("TongConLai", lstData.Where(n => !n.BHangCha).Sum(x => x.FConLai));
                        data.Add("TongSoPhaiThu", lstData.Where(n => !n.BHangCha).Sum(x => x.FSoPhaiThu));
                        if (IsSummary)
                            data.Add("IsAggregate", true);
                        AddChuKy(data, _typeChuky);

                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhQttmBHYTChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN THU MUA BHYT THÂN NHÂN, NLĐ " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }

                    e.Result = results;
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

        private void ExportQttmBhytThanNhanNLDTongHopDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var selectedQuarter = Int32.Parse(QuarterMonthSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    if (lstIdDonVi != null)
                    {
                        var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                        List<BhQttmBHYTChiTietQuery> lstData = new List<BhQttmBHYTChiTietQuery>();
                        lstData = _chungTuChiTietService.ExportQTTMBhytThanNhanNLDTongHopDonVi(yearOfWork, selectedUnits, donViTinh, selectedQuarter).ToList();
                        CalculateData(lstData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                        data.Add("ListData", lstData.Where(x => x.HasDataToPrint));
                        data.Add("YearWork", yearOfWork);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Year", lstData.First().INamLamViec);
                        data.Add("Quarter", QuarterMonthSelected.DisplayItem);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("DonViChungTu", "");
                        data.Add("IsAggregate", true);
                        //Tính tổng cộng
                        data.Add("TongDuToan", lstData.Where(n => !n.BHangCha).Sum(x => x.FDuToan));
                        data.Add("TongDaQuyetToan", lstData.Where(n => !n.BHangCha).Sum(x => x.FDaQuyetToan));
                        data.Add("TongConLai", lstData.Where(n => !n.BHangCha).Sum(x => x.FConLai));
                        data.Add("TongSoPhaiThu", lstData.Where(n => !n.BHangCha).Sum(x => x.FSoPhaiThu));
                        AddChuKy(data, _typeChuky);
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhQttmBHYTChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN THU MUA BHYT THÂN NHÂN, NLĐ " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }
                    e.Result = results;
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

        private void ExportQuyetToanThuMuaBhytThanNhan(ExportType exportType)
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
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = "";
                    string fileNamePrefix;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());
                    List<BhQttmBHYTChiTietQuery> lstBHYT = new List<BhQttmBHYTChiTietQuery>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();

                    // BHYT thân nhân
                    if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN)
                    {
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                        {
                            lstBHYT = _chungTuChiTietService.ExportQuyetToanThuMuaBhytThanNhan(yearOfWork, isTongHop, lstSelectedUnitID, BhxhMLNS.THU_MUA_BHYT_TNQN,
                            BhxhMLNS.THU_MUA_BHYT_CNVQP, donViTinh).ToList();
                        }
                        else
                        {
                            lstBHYT = _chungTuChiTietService.ExportQuyetToanThuMuaBhytThanNhanTongHop(yearOfWork, isTongHop, lstSelectedUnitID, BhxhMLNS.THU_MUA_BHYT_TNQN,
                            BhxhMLNS.THU_MUA_BHYT_CNVQP, donViTinh).ToList();
                        }
                        templateFileName = GetTemplate();
                        data.Add("TongSoPhaiThuTNQN", lstBHYT.Sum(x => x.FSoPhaiThuTNQN));
                        data.Add("TongSoPhaiThuTNCNVQP", lstBHYT.Sum(x => x.FSoPhaiThuTNCNVQP));
                        data.Add("TongCong", lstBHYT.Sum(x => x.FTongCong));
                    }
                    // BHYT HSSV HVQS SQDB
                    if (SettlementTypeValue == (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB)
                    {
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                        {
                            lstBHYT = _chungTuChiTietService.ExportQuyetToanThuMuaBhytHSSV(yearOfWork, isTongHop, lstSelectedUnitID, BhxhMLNS.SLNS_HSSV,
                            BhxhMLNS.SLNS_LUU_HS, BhxhMLNS.SLNS_HVQS, BhxhMLNS.SLNS_SQ_DU_BI, donViTinh).ToList();
                        }
                        else
                        {
                            lstBHYT = _chungTuChiTietService.ExportQuyetToanThuMuaBhytHSSVTongHop(yearOfWork, isTongHop, lstSelectedUnitID, BhxhMLNS.SLNS_HSSV,
                             BhxhMLNS.SLNS_LUU_HS, BhxhMLNS.SLNS_HVQS, BhxhMLNS.SLNS_SQ_DU_BI, donViTinh).ToList();
                        }
                        templateFileName = GetTemplate();
                        data.Add("TongHSSV", lstBHYT.Sum(x => x.FHSSV));
                        data.Add("TongLuuHS", lstBHYT.Sum(x => x.FLuuHS));
                        data.Add("TongHSSVLuuHS", lstBHYT.Sum(x => x.FTongHSSV));
                        data.Add("TongHVQS", lstBHYT.Sum(x => x.FHVQS));
                        data.Add("TongSQDuBi", lstBHYT.Sum(x => x.FSQDuBi));
                        data.Add("TongCong", lstBHYT.Sum(x => x.FTongCongHSSV));
                    }
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstBHYT.Where(x => x.HasDataToPrint));
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("Title1", Title1);
                    data.Add("Title2", Title2);
                    data.Add("Title3", Title3);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", yearOfWork);
                    data.Add("DonViChungTu", "");
                    AddChuKy(data, _typeChuky);

                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                    int stt = 1;
                    foreach (var i in lstBHYT)
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhQttmBHYTChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH THU MUA BHYT " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
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

        private void CalculateData(List<BhQttmBHYTChiTietQuery> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FDuToan = 0;
                    x.FDaQuyetToan = 0;
                    x.FConLai = 0;
                    x.FSoPhaiThu = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, BhQttmBHYTChiTietQuery item, List<BhQttmBHYTChiTietQuery> lstChungTuChiTiet)
        {
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.FDuToan = (model.FDuToan ?? 0) + (item.FDuToan ?? 0);
            model.FDaQuyetToan = (model.FDaQuyetToan ?? 0) + (item.FDaQuyetToan ?? 0);
            model.FConLai = (model.FConLai ?? 0) + (item.FConLai ?? 0);
            model.FSoPhaiThu = (model.FSoPhaiThu ?? 0) + (item.FSoPhaiThu ?? 0);
            CalculateParent(model.IIDMLNSCha, item, lstChungTuChiTiet);
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
        }
    }
}
