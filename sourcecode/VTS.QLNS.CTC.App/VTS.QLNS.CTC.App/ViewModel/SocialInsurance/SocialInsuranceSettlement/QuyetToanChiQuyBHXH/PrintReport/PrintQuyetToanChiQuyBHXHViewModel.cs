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
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Explanation;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.PrintReportQtcqBHXH;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Explanation;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.PritnReport
{
    public class PrintQuyetToanChiQuyBHXHViewModel : ViewModelBase
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
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsPhongBanService _phongBanService;
        private IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private IQtcqBHXHService _qtcqBHXHService;
        private IQtcqBHXHChiTietService _qtcqBHXHChiTietService;
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private List<ReportQtChungTuChiTietQuery> _reportQtChungTuChiTiets;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private ICollectionView _listLNSView;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _isInitReport;
        private bool _checkAllAgencies;
        private string _typeChuky;
        public SettlementVoucherModel SettlementVoucher;
        public List<SettlementVoucherDetailModel> SettlementVoucherDetails;
        public List<string> ListIdDonViHasCt { get; set; }
        public int SettlementTypeValue { get; set; }
        public bool IsShowAll { get; set; }
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public string name { get; set; }
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
        private bool _isShowLoaiBaoCao;
        public bool IsShowLoaiBaoCao
        {
            get => _isShowLoaiBaoCao;
            set => SetProperty(ref _isShowLoaiBaoCao, value);
        }

        private bool _isShowDisplay;
        public bool IsShowDisplay
        {
            get => _isShowDisplay;
            set => SetProperty(ref _isShowDisplay, value);
        }

        private bool _isInDetailTCXN;
        public bool IsInDetailTCXN
        {
            get => _isInDetailTCXN;
            set => SetProperty(ref _isInDetailTCXN, value);
        }

        private bool _isInNhomDoiTuong;
        public bool IsInNhomDoiTuong
        {
            get => _isInNhomDoiTuong;
            set => SetProperty(ref _isInNhomDoiTuong, value);
        }
        private bool _isInTongHopGiaiThichTroCap;
        public bool IsInTongHopGiaiThichTroCap
        {
            get => _isInTongHopGiaiThichTroCap;
            set
            {
                SetProperty(ref _isInTongHopGiaiThichTroCap, value);
                LoadAgencies();
            }
        }

        public bool InMotToChecked { get; set; }
        public bool IsEnableCheckBoxInMotTo => !IsEnableCheckBoxSummary;
        public bool IsShowTheoTongHop => !IsShowLoaiBaoCao && IsShowTheoTongHopCuaGiaiThich;
        public bool IsShowDetailTCXN { get; set; }
        public bool IsShowBCTHGiaiThichTroCap { get; set; }
        public bool IsShowTheoTongHopCuaGiaiThich { get; set; }
        private bool _isShowNhomTheoDoiTuong;
        public bool IsShowNhomTheoDoiTuong
        {
            get => _isShowNhomTheoDoiTuong;
            set
            {
                SetProperty(ref _isShowNhomTheoDoiTuong, value);
            }
        }
        private string SettlementName
        {
            get
            {
                switch (SettlementTypeValue)
                {
                    case (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                        name = "In báo cáo quyết toán chi các chế độ BHXH";
                        _isShowLoaiBaoCao = true;
                        IsShowTheoTongHopCuaGiaiThich = true;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU:
                        name = "In giải thích trợ cấp ốm đau (mẫu 04a/BHXH)";
                        _isShowLoaiBaoCao = false;
                        IsShowTheoTongHopCuaGiaiThich = false;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN:
                        name = "In giải thích trợ cấp thai sản (mẫu 04b/BHXH)";
                        _isShowLoaiBaoCao = false;
                        IsShowTheoTongHopCuaGiaiThich = false;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP:
                        name = "In giải thích trợ cấp tai nạn lao động, nghề nghiệp";
                        _isShowLoaiBaoCao = false;
                        IsShowTheoTongHopCuaGiaiThich = false;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU:
                        name = "In giải thích trợ cấp hưu trí, phục viên, xuất ngũ, thôi việc";
                        _isShowLoaiBaoCao = false;
                        IsShowTheoTongHopCuaGiaiThich = false;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH:
                        name = "In thông tri xác nhận quyết toán chi các chế độ BHXH";
                        _isShowLoaiBaoCao = false;
                        IsShowTheoTongHopCuaGiaiThich = true;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC:
                        name = "In danh sách người lao động nghỉ việc";
                        _isShowLoaiBaoCao = false;
                        IsShowTheoTongHopCuaGiaiThich = true;
                        break;
                    default:
                        name = "tổng hợp tất cả các LNS";
                        _isShowLoaiBaoCao = false;
                        IsShowTheoTongHopCuaGiaiThich = true;
                        break;
                }
                OnPropertyChanged(nameof(IsShowLoaiBaoCao));
                return name;
            }
        }

        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
        public override System.Type ContentType => typeof(PrintQuyetToanChiQuyBHXH);

        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();

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
                OnPropertyChanged(nameof(IsEnableCheckBoxInMotTo));
                IsShowCheckBoxNhomDT();
                LoadAgencies();
            }
        }

        #region list agency
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
                }
            }
        }
        private bool _isShowCheDoBHXH;
        public bool IsShowCheDoBHXH
        {
            get
            {
                if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set => SetProperty(ref _isShowCheDoBHXH, value);
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
                    //LoadLNS();
                }
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(SelectedAgencyCount));
            }
        }
        #endregion

        #region list LNS

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = ListLNS != null ? ListLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListLNS != null ? ListLNS.Count(item => item.IsChecked) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _listLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => ListLNS != null && ListLNS.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                foreach (CheckBoxItem item in ListLNS.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllLNS;
                }
            }
        }

        private ObservableCollection<CheckBoxTreeItem> _listLNS;
        public ObservableCollection<CheckBoxTreeItem> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        #endregion

        public bool IsExportEnable
        {
            get
            {
                if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH)
                {
                    return Agencies != null && Agencies.Where(x => x.Selected).Count() > 0 && (_isCoverSheet || _isData || _isVerbalExplanation || _isDataInterpretation || _isCheckAll);
                }
                else
                {
                    return Agencies != null && Agencies.Where(x => x.Selected).Count() > 0;
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

        private string _noiDungChi;
        public string NoiDungChi
        {
            get => _noiDungChi;
            set => SetProperty(ref _noiDungChi, value);
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
        private bool _isCheckAll;
        public bool IsCheckAll
        {
            get => _isCheckAll;
            set
            {
                SetProperty(ref _isCheckAll, value);
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

        private List<ComboboxItem> _bQuanLy;
        public List<ComboboxItem> BQuanLy
        {
            get => _bQuanLy;
            set => SetProperty(ref _bQuanLy, value);
        }

        private ComboboxItem _selectedBQuanLy;
        public ComboboxItem SelectedBQuanLy
        {
            get => _selectedBQuanLy;
            set => SetProperty(ref _selectedBQuanLy, value);
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
                    if (!_isInitReport)
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
                }
            }
        }

        private ComboboxItem _cbxQuaterSelected;
        public ComboboxItem CbxQuaterSelected
        {
            get => _cbxQuaterSelected;
            set
            {
                SetProperty(ref _cbxQuaterSelected, value);
                if (_cbxQuaterSelected != null)
                {
                    LoadAgencies();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuater;
        public ObservableCollection<ComboboxItem> CbxQuater
        {
            get => _cbxQuater;
            set => SetProperty(ref _cbxQuater, value);
        }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel { get; set; }

        public PrintQuyetToanChiQuyBHXHViewModel(INsMucLucNganSachService mucLucNganSachService,
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            INsDonViService donViService,
            INsNguoiDungDonViService nguoiDungDonViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            INsPhongBanService phongBanService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IQtcqBHXHChiTietService qtcqBHXHChiTietService,
            IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService,
            IQtcqBHXHService qtcqBHXHService,
            QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel quyetToanChiGiaiThichBangLoiQuyBHXHViewModel)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            _donViService = donViService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _phongBanService = phongBanService;
            _qtcqBHXHChiTietService = qtcqBHXHChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _qtcqBHXHService = qtcqBHXHService;
            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            ExportExcelCommand = new RelayCommand(obj => OnExportFile(false));
            ExportPDFCommand = new RelayCommand(obj =>
            {
                OnExportFile(true);
            });
            PrintCommand = new RelayCommand(obj =>
            {
                OnExportFile(true);
            });
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel = quyetToanChiGiaiThichBangLoiQuyBHXHViewModel;
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _isInitReport = true;
            IsShowLoaiBaoCao = false;
            IsShowTheoTongHopCuaGiaiThich = false;
            IsShowBCTHGiaiThichTroCap = false;
            IsShowNhomTheoDoiTuong = false;
            IsShowDetailTCXN = false;
            LoadReportType();
            LoadQuater();
            _agencies = new ObservableCollection<AgencyModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            IsDataInterpretation = false;
            LoadTieuDe();
            LoadDanhMuc();
            LoadBQuanLy();
            LoadAgencies();
            _isInitReport = false;
            IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
        }

        private void OnOpenVerbalExplanationDialog()
        {
            var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (!lstDonViChecked.Any())
            {
                MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                return;
            }
            var lstChungTu = _qtcqBHXHService.FindByCondition(x => x.INamChungTu == _sessionInfo.YearOfWork && x.IQuyChungTu == int.Parse(_cbxQuaterSelected.ValueItem) && x.IIdMaDonVi == lstDonViChecked);
            var BhQtcqBHXHModel = _mapper.Map<ObservableCollection<BhQtcqBHXHModel>>(lstChungTu);
            QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel.BhQtcqBHXHModel = BhQtcqBHXHModel.FirstOrDefault();
            QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel.Init();
            var view = new VerbalExplanation { DataContext = QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel };
            view.ShowDialog();
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
            DateTime dt = DateTime.Now;
            List<BhDmMucLucNganSach> listMLNS = new List<BhDmMucLucNganSach>();
            string agencyIds = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (agencyIds.Length > 0)
            {
                listMLNS = _bhDmMucLucNganSachService.FindSLNSForQTCQBHXH(yearOfWork, agencyIds, iQuy, dt, _sessionInfo.Principal).ToList();
            }

            ListLNS = new ObservableCollection<CheckBoxTreeItem>();
            ListLNS = _mapper.Map<ObservableCollection<CheckBoxTreeItem>>(listMLNS);
            // Filter
            _listLNSView = CollectionViewSource.GetDefaultView(ListLNS);
            _listLNSView.Filter = ListLNSFilter;
            foreach (CheckBoxTreeItem model in ListLNS)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        FindChildCheckbox(model);
                        OnPropertyChanged(nameof(IsExportEnable));
                        OnPropertyChanged(nameof(SelectedCountLNS));
                        OnPropertyChanged(nameof(SelectAllLNS));
                    }
                };
            }

            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(SelectedCountLNS));
            OnPropertyChanged(nameof(SelectAllLNS));
            IsLoading = false;
        }

        private void LoadQuater()
        {
            CbxQuater = new ObservableCollection<ComboboxItem>();
            CbxQuater.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Quý I" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Quý II" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Quý III" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "4", DisplayItem = "Quý IV" });
            CbxQuaterSelected = CbxQuater.ElementAt(0);

        }
        public void FindChildCheckbox(CheckBoxTreeItem parent)
        {
            ListLNS.Where(n => n.ParentId == parent.Id).Select(n => { n.IsChecked = parent.IsChecked; return n; }).ToList();
            if (ListLNS.Where(n => n.ParentId == parent.Id && n.IsChecked == !parent.IsChecked).ToList().Count == 0)
            {
                return;
            }
            else
            {
                foreach (CheckBoxTreeItem item in ListLNS.Where(n => n.ParentId == parent.Id))
                {
                    FindChildCheckbox(item);
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.ValueItem.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadReportType()
        {
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH)
            {
                _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
                new ComboboxItem { DisplayItem = "Theo loại chi", ValueItem = SummaryLNSReportType.Type.ToString() }
            };
            }
            else if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP
                || SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN
                || SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU
                || SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU)
            {
                _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị - Nhóm theo khối", ValueItem = SummaryLNSReportType.AgencySummaryBlock.ToString() },

            };
            }
            else
            {
                _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
                //new ComboboxItem { DisplayItem = "Theo loại chi", ValueItem = SummaryLNSReportType.Type.ToString() }
            };
            }


            _selectedReportType = _reportTypes.First();

            switch (SettlementTypeValue)
            {
                case (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                case (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC:
                    _selectedReportType = _reportTypes.ElementAt(0);
                    IsShowDetailTCXN = false;
                    IsShowDisplay = true;
                    IsShowBCTHGiaiThichTroCap = false;
                    break;
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU:
                    _selectedReportType = _reportTypes.ElementAt(0);
                    IsShowDetailTCXN = false;
                    IsShowDisplay = true;
                    IsShowBCTHGiaiThichTroCap = true;
                    break;
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN:
                    _selectedReportType = _reportTypes.ElementAt(0);
                    IsShowDetailTCXN = false;
                    IsShowDisplay = true;
                    IsShowBCTHGiaiThichTroCap = true;
                    break;
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP:
                    _selectedReportType = _reportTypes.ElementAt(0);
                    IsShowDetailTCXN = false;
                    IsShowDisplay = true;
                    IsShowBCTHGiaiThichTroCap = true;
                    break;
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU:
                    _selectedReportType = _reportTypes.ElementAt(0);
                    IsShowDetailTCXN = true;
                    IsShowDisplay = true;
                    IsShowBCTHGiaiThichTroCap = true;
                    break;
                case (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH:
                    _selectedReportType = _reportTypes.ElementAt(0);
                    IsShowDetailTCXN = false;
                    IsShowDisplay = true;
                    IsShowBCTHGiaiThichTroCap = false;
                    break;
            }
        }

        private void IsShowCheckBoxNhomDT()
        {
            if ((SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU
                || SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN
                || SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP
                || SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU)
                && SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryBlock.ToString())
                _isShowNhomTheoDoiTuong = true;
            else _isShowNhomTheoDoiTuong = false;
            OnPropertyChanged(nameof(IsShowNhomTheoDoiTuong));
        }
        private void LoadBQuanLy()
        {
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            List<DmBQuanLy> listPhongBan = _phongBanService.FindByCondition(predicate);
            _bQuanLy = _mapper.Map<List<ComboboxItem>>(listPhongBan);
            if (_bQuanLy.Count() > 0)
            {
                _bQuanLy.Insert(0, new ComboboxItem("Tất cả", string.Empty));
                SelectedBQuanLy = _bQuanLy.First();
            }
        }


        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var yearOfWork = _sessionInfo.YearOfWork;
                int iQuy = int.Parse(CbxQuaterSelected.ValueItem);

                List<DonVi> lstDonVis = new List<DonVi>();
                if (SettlementTypeValue != (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH)
                {
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        lstDonVis = _qtcqBHXHService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu).ToList();
                        lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                    else
                    {
                        lstDonVis = _qtcqBHXHService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTuTongHop).ToList();
                        // In thong tri
                        if (IsShowTheoTongHopCuaGiaiThich)
                        {
                            if (!IsInTheoTongHop)
                                lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                            else
                                lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                        }
                        else
                        {
                            // in theo bao cao tong hop cho giai thich tro cap
                            if (IsShowBCTHGiaiThichTroCap)
                            {
                                if (IsInTongHopGiaiThichTroCap)
                                {
                                    lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                                }
                                else
                                {
                                    lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                                }
                            }
                            else
                            {
                                lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                            }
                        }
                    }
                }
                else
                {
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        lstDonVis = _qtcqBHXHService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu).ToList();
                        lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                    else
                    {
                        lstDonVis = _qtcqBHXHService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTuTongHop).ToList();
                        if (IsSummary)
                        {
                            lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                        }
                        else if (IsSummaryAgency)
                        {
                            lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                        }
                        else if (IsSummary && IsSummaryAgency)
                        {
                            lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                        }
                        else
                        {
                            lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                        }
                    }
                }
                lstDonVis = lstDonVis.OrderBy(x => x.IIDMaDonVi).ToList();
                e.Result = lstDonVis;
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
                            //LoadLNS();
                        }
                    };
                }

                _listLNS = new ObservableCollection<CheckBoxTreeItem>();
                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsSelectedAllAgency));
                OnPropertyChanged(nameof(SelectedAgencyCount));
                OnPropertyChanged(nameof(IsExportEnable));
                IsLoading = false;
            });
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

        private void OnExportFile(bool isPdf)
        {
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH)
            {
                ExportBaoCaoQuyetToanChiCacCheDoBHXH(isPdf);
            }
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU)
            {
                ExportBaoCaoGiaiThichTroCapOmDau(isPdf ? ExportType.PDF : ExportType.EXCEL);
            }
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN)
            {
                ExportBaoCaoGiaiThichTroCapThaiSan(isPdf);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP)
            {
                ExportBaoCaoGiaiThichTroCapTaiNanLD(isPdf);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU)
            {
                ExportBaoCaoGiaiThichTroCapHuuTriPVXNTVTT(isPdf);
            }
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH)
            {
                ExportBaoCaoThongTriQuyetToanChiCacCheDoBHXH(isPdf);
            }
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC
                && SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString())
            {
                ExportDanhSachNguoiLaoDongNghiViec(isPdf ? ExportType.PDF : ExportType.EXCEL);
            }
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC
               && SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
            {
                ExportDanhSachNguoiLaoDongNghiViecDonVi(isPdf ? ExportType.PDF : ExportType.EXCEL);
            }
        }

        private void OnConfigSign()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH => TypeChuKy.RPT_BH_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXH,
                (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU => TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPOMDAU,
                (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN => TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPTHAISAN,
                (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP => TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTAINHANLAODONGNGHENGHIEP,
                (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU => TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPHUUTRIXUATNGU,
                (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH => TypeChuKy.RPT_BH_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANBHXH,
                _ => TypeChuKy.RPT_NS_QUYETTOAN_TATCA_TONGHOP
            };

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC)
            {
                _typeChuky = TypeChuKy.PRINT_DANHSACHNLDNGHIVIEC;
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            }
            else
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            }
            DmChuKyModel chuKyModel = new DmChuKyModel();

            if (_dmChuKy == null)
                chuKyModel.IdType = typeChuKy;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.HasAddedSign4 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign5 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign6 = IsVerbalExplanation;
            DmChuKyDialogViewModel.IsShowNoiDungChi = (SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString());
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC)
            {
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.HasAddedSign5 = true;
            }
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadTieuDe()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH => TypeChuKy.RPT_BH_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXH,
                (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU => TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPOMDAU,
                (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN => TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPTHAISAN,
                (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP => TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTAINHANLAODONGNGHENGHIEP,
                (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU => TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPHUUTRIXUATNGU,
                (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH => TypeChuKy.RPT_BH_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANBHXH,
                (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC => TypeChuKy.PRINT_DANHSACHNLDNGHIVIEC,
                _ => TypeChuKy.RPT_NS_QUYETTOAN_TATCA_TONGHOP
            };
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
            {
                Title1 = _dmChuKy.TieuDe1MoTa;
            }
            else
            {
                switch (SettlementTypeValue)
                {
                    case (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                        Title1 = SettlementTitle.Title1QTCQBHXHKeHoach;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU:
                        Title1 = SettlementTitle.Title1QTCQBHXHTCOD;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN:
                        Title1 = SettlementTitle.Title1QTCQBHXHTCTS;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP:
                        Title1 = SettlementTitle.Title1QTCQGTTCTN;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU:
                        Title1 = SettlementTitle.Title1QTCQGTHTPVXNTVTT;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH:
                        Title1 = SettlementTitle.Title1QTCQBHXHTT;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC:
                        Title1 = SettlementTitle.Title1DSNLDNV;
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
            {
                Title2 = _dmChuKy.TieuDe2MoTa;
            }
            else
            {
                switch (SettlementTypeValue)
                {
                    case (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                        Title2 = string.Empty;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU:
                        Title2 = SettlementTitle.Title2QTCQBHXHTCOD;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN:
                        Title2 = SettlementTitle.Title2QTCQBHXHTCTS;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP:
                        Title2 = SettlementTitle.Title2QTCQGTTCTN;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU:
                        Title2 = SettlementTitle.Title2QTCQGTHTPVXNTVTT; ;
                        break;
                    case (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH:
                        Title2 = SettlementTitle.Title2QTCQBHXHTT;
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.Ten6MoTa))
                NoiDungChi = _dmChuKy.Ten6MoTa;
            else
                NoiDungChi = "Xác nhận chi các chế độ bảo hiểm xã hội";
        }

        private void ExportBaoCaoQuyetToanChiCacCheDoBHXH(bool isPdf)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    if (IsCoverSheet)
                    {
                        var dataResult = OnExportCoverSheet();
                        if (dataResult != null)
                            results.Add(dataResult);
                    }

                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                        if (lstIdDonVi.Any())
                        {
                            foreach (var donVi in lstIdDonVi)
                            {
                                if (IsData)
                                {
                                    string fileName = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXH);
                                    results.AddRange(ProcessFile(yearOfWork, donViTinh, donVi, isPdf ? ExportType.PDF : ExportType.EXCEL, fileName));
                                }
                                if (IsVerbalExplanation)
                                {
                                    string fileName = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXHSOLIEU);
                                    results.AddRange(ProcessFile(yearOfWork, donViTinh, donVi, isPdf ? ExportType.PDF : ExportType.EXCEL, fileName));
                                }
                            }
                        }
                    }
                    else
                    {
                        var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));


                        if (IsData)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXH);
                            results.AddRange(ProcessFileForDonVi(yearOfWork, donViTinh, lstDonViChecked, isPdf ? ExportType.PDF : ExportType.EXCEL, fileName));
                        }
                        if (IsVerbalExplanation)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXHSOLIEU);
                            results.AddRange(ProcessFileForDonVi(yearOfWork, donViTinh, lstDonViChecked, isPdf ? ExportType.PDF : ExportType.EXCEL, fileName));
                        }


                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
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

        private List<ExportResult> ProcessFile(int yearOfWork, int donViTinh, string sMaDonVi, ExportType exportType, string fileName)
        {
            List<ExportResult> results = new List<ExportResult>();
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
            bool isTongHop = false;
            int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
            BhQtcQBHXHChiTietGiaiThichQuery lstGiaiThichBangLoi = new BhQtcQBHXHChiTietGiaiThichQuery();
            if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
            {
                lstGiaiThichBangLoi = _qtcQBHXHChiTietGiaiThichService.GetGiaiThichBangLoiTheoDonVi(yearOfWork, sMaDonVi, iQuy, MaLoaiChiBHXH.SMABHXH).FirstOrDefault();
            }

            DonVi donViChild = _donViService.FindByIdDonVi(sMaDonVi, yearOfWork);
            var lstQuery = _qtcqBHXHChiTietService.BaoCaoQuyetToanChiQuyBHXH(_sessionService.Current.YearOfWork, sMaDonVi, LNSValue.LNS_9010001_9010002, isTongHop, iQuy, donViTinh);
            var lstData = _mapper.Map<List<BhQtcqBHXHChiTietModel>>(lstQuery);
            var lstDataDuToan = lstData.Where(x => string.IsNullOrEmpty(x.SL) || x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).ToList();

            lstDataDuToan.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                {
                    x.BHangCha = false;
                }
            });

            CalculateDataDuToan(lstDataDuToan);
            lstDataDuToan.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                {
                    x.BHangCha = true;
                }
            });

            CalculateData(lstData);
            lstData = lstData.Where(x => (x.FTienDuToanDuyet ?? 0) != 0 || (x.ISoLuyKeCuoiQuyNay ?? 0) != 0
                                         || (x.FTienLuyKeCuoiQuyNay ?? 0) != 0 || (x.ISoSQDeNghi ?? 0) != 0
                                         || (x.FTienSQDeNghi ?? 0) != 0 || (x.ISoQNCNDeNghi ?? 0) != 0
                                         || (x.FTienQNCNDeNghi ?? 0) != 0 || (x.ISoCNVCQPDeNghi ?? 0) != 0
                                         || (x.FTienCNVCQPDeNghi ?? 0) != 0 || (x.ISoHSQBSDeNghi ?? 0) != 0
                                         || (x.FTienHSQBSDeNghi ?? 0) != 0 || (x.ISoLDHDDeNghi ?? 0) != 0
                                         || (x.FTienLDHDDeNghi ?? 0) != 0 || (x.FTongTienPheDuyet ?? 0) != 0).ToList();

            lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("TieuDe3", Title3);
            data.Add("YearWork", yearOfWork);
            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
            data.Add("DonVi", donViChild != null ? donViChild.TenDonVi : string.Empty);
            data.Add("ListData", lstData);
            data.Add("Quy", CbxQuaterSelected.DisplayItem);
            data.Add("MoTaTinhHinh", lstGiaiThichBangLoi?.SMoTa_TinhHinh);
            data.Add("MoTaKienNghi", lstGiaiThichBangLoi?.SMoTa_KienNghi);
            AddChuKy(data);

            //Tính tổng
            Double? FTongTienDuToanDuyet = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanDuyet).Sum();
            int? ITongSoLuyKeCuoiQuyNay = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoLuyKeCuoiQuyNay).Sum();
            Double? FTongTienLuyKeCuoiQuyNay = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienLuyKeCuoiQuyNay).Sum();
            int? ITongSoSQDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoSQDeNghi).Sum();
            Double? FTongTienSQDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienSQDeNghi).Sum();
            int? ITongSoQNCNDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoQNCNDeNghi).Sum();
            Double? FTongTienQNCNDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienQNCNDeNghi).Sum();
            int? ITongSoCNVCQPDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoCNVCQPDeNghi).Sum();
            Double? FTongTienCNVCQPDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienCNVCQPDeNghi).Sum();
            int? ITongSoHSQBSDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoHSQBSDeNghi).Sum();
            Double? FTongTienHSQBSDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienHSQBSDeNghi).Sum();
            int? ITongSoLDHDDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoLDHDDeNghi).Sum();
            Double? FTongTienLDHDDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienLDHDDeNghi).Sum();
            Double? FTongTienDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienDeNghi).Sum();
            int? ITongSoDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ITongSoDeNghi).Sum();
            Double? FTongTienPheDuyet = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienPheDuyet).Sum();

            data.Add("FTongTienDuToanDuyet", FTongTienDuToanDuyet);
            data.Add("ITongSoLuyKeCuoiQuyNay", ITongSoLuyKeCuoiQuyNay);
            data.Add("FTongTienLuyKeCuoiQuyNay", FTongTienLuyKeCuoiQuyNay);
            data.Add("ITongSoSQDeNghi", ITongSoSQDeNghi);
            data.Add("FTongTienSQDeNghi", FTongTienSQDeNghi);
            data.Add("ITongSoQNCNDeNghi", ITongSoQNCNDeNghi);
            data.Add("FTongTienQNCNDeNghi", FTongTienQNCNDeNghi);
            data.Add("ITongSoCNVCQPDeNghi", ITongSoCNVCQPDeNghi);
            data.Add("FTongTienCNVCQPDeNghi", FTongTienCNVCQPDeNghi);
            data.Add("ITongSoHSQBSDeNghi", ITongSoHSQBSDeNghi);
            data.Add("FTongTienHSQBSDeNghi", FTongTienHSQBSDeNghi);
            data.Add("ITongSoLDHDDeNghi", ITongSoLDHDDeNghi);
            data.Add("FTongTienLDHDDeNghi", FTongTienLDHDDeNghi);
            data.Add("FTongTienDeNghi", FTongTienDeNghi);
            data.Add("ITongSoDeNghi", ITongSoDeNghi);
            data.Add("FTongTienPheDuyet", FTongTienPheDuyet);
            data.Add("HEADER2", string.Empty);
            data.Add("FormatNumber", formatNumber);
            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
            data.Add("TienBangChu", StringUtils.NumberToText(FTongTienDeNghi.Value * donViTinh, true));
            data.Add("Year", _sessionInfo.YearOfWork);
            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
            string fileNamePrefix;
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQBHXH, fileName + FileExtensionFormats.Xlsx);
            fileNamePrefix = Path.GetFileNameWithoutExtension(fileName);
            string filename = StringUtils.CreateExportFileName(fileNamePrefix + (donViChild != null ? ("_" + donViChild.TenDonVi) : string.Empty) + "_" + _sessionInfo.TenDonVi);
            var xlsFile = _exportService.Export<BhQtcqBHXHChiTietModel>(templateFileName, data);
            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
            return results;
        }

        private List<ExportResult> ProcessFileForDonVi(int yearOfWork, int donViTinh, string sMaDonVi, ExportType exportType, string fileName)
        {
            List<ExportResult> results = new List<ExportResult>();

            var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
            bool isTongHop = IsSummary;
            int iQuy = int.Parse(CbxQuaterSelected.ValueItem);

            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);

            BhQtcQBHXHChiTietGiaiThichQuery lstGiaiThichBangLoi = new BhQtcQBHXHChiTietGiaiThichQuery();
            if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummary)
            {
                lstGiaiThichBangLoi = _qtcQBHXHChiTietGiaiThichService.GetGiaiThichBangLoiTheoDonVi(yearOfWork, sMaDonVi, iQuy, MaLoaiChiBHXH.SMABHXH).FirstOrDefault();
            }


            DonVi donViChild = _donViService.FindByIdDonVi(sMaDonVi, yearOfWork);
            var lstData = _mapper.Map<List<BhQtcqBHXHChiTietModel>>(_qtcqBHXHChiTietService.BaoCaoQuyetToanChiQuyBHXH(_sessionService.Current.YearOfWork, sMaDonVi, LNSValue.LNS_9010001_9010002, isTongHop, iQuy, donViTinh));
            var lstDataDuToan = lstData.Where(x => string.IsNullOrEmpty(x.SL) || x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).ToList();

            lstDataDuToan.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                {
                    x.BHangCha = false;
                    x.BHangCha = false;
                }
            });

            CalculateDataDuToan(lstDataDuToan);
            lstDataDuToan.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                {
                    x.BHangCha = true;
                    x.BHangCha = true;
                }
            });
            CalculateData(lstData);
            lstData = lstData.Where(x => (x.FTienDuToanDuyet ?? 0) != 0 || (x.ISoLuyKeCuoiQuyNay ?? 0) != 0
                                         || (x.FTienLuyKeCuoiQuyNay ?? 0) != 0 || (x.ISoSQDeNghi ?? 0) != 0
                                         || (x.FTienSQDeNghi ?? 0) != 0 || (x.ISoQNCNDeNghi ?? 0) != 0
                                         || (x.FTienQNCNDeNghi ?? 0) != 0 || (x.ISoCNVCQPDeNghi ?? 0) != 0
                                         || (x.FTienCNVCQPDeNghi ?? 0) != 0 || (x.ISoHSQBSDeNghi ?? 0) != 0
                                         || (x.FTienHSQBSDeNghi ?? 0) != 0 || (x.ISoLDHDDeNghi ?? 0) != 0
                                         || (x.FTienLDHDDeNghi ?? 0) != 0 || (x.FTongTienPheDuyet ?? 0) != 0).ToList();

            lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("TieuDe3", Title3);
            data.Add("YearWork", yearOfWork);
            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
            data.Add("DonVi", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
            data.Add("ListData", lstData);
            data.Add("Quy", CbxQuaterSelected.DisplayItem);
            data.Add("IsAggregate", true);
            data.Add("MoTaTinhHinh", lstGiaiThichBangLoi?.SMoTa_TinhHinh);
            data.Add("MoTaKienNghi", lstGiaiThichBangLoi?.SMoTa_KienNghi);

            AddChuKy(data);

            //Tính tổng
            Double? FTongTienDuToanDuyet = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanDuyet).Sum();
            int? ITongSoLuyKeCuoiQuyNay = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoLuyKeCuoiQuyNay).Sum();
            Double? FTongTienLuyKeCuoiQuyNay = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienLuyKeCuoiQuyNay).Sum();
            int? ITongSoSQDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoSQDeNghi).Sum();
            Double? FTongTienSQDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienSQDeNghi).Sum();
            int? ITongSoQNCNDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoQNCNDeNghi).Sum();
            Double? FTongTienQNCNDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienQNCNDeNghi).Sum();
            int? ITongSoCNVCQPDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoCNVCQPDeNghi).Sum();
            Double? FTongTienCNVCQPDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienCNVCQPDeNghi).Sum();
            int? ITongSoHSQBSDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoHSQBSDeNghi).Sum();
            Double? FTongTienHSQBSDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienHSQBSDeNghi).Sum();
            int? ITongSoLDHDDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoLDHDDeNghi).Sum();
            Double? FTongTienLDHDDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienLDHDDeNghi).Sum();
            Double? FTongTienDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienDeNghi).Sum();
            int? ITongSoDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.ITongSoDeNghi).Sum();
            Double? FTongTienPheDuyet = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienPheDuyet).Sum();

            data.Add("FTongTienDuToanDuyet", FTongTienDuToanDuyet);
            data.Add("ITongSoLuyKeCuoiQuyNay", ITongSoLuyKeCuoiQuyNay);
            data.Add("FTongTienLuyKeCuoiQuyNay", FTongTienLuyKeCuoiQuyNay);
            data.Add("ITongSoSQDeNghi", ITongSoSQDeNghi);
            data.Add("FTongTienSQDeNghi", FTongTienSQDeNghi);
            data.Add("ITongSoQNCNDeNghi", ITongSoQNCNDeNghi);
            data.Add("FTongTienQNCNDeNghi", FTongTienQNCNDeNghi);
            data.Add("ITongSoCNVCQPDeNghi", ITongSoCNVCQPDeNghi);
            data.Add("FTongTienCNVCQPDeNghi", FTongTienCNVCQPDeNghi);
            data.Add("ITongSoHSQBSDeNghi", ITongSoHSQBSDeNghi);
            data.Add("FTongTienHSQBSDeNghi", FTongTienHSQBSDeNghi);
            data.Add("ITongSoLDHDDeNghi", ITongSoLDHDDeNghi);
            data.Add("FTongTienLDHDDeNghi", FTongTienLDHDDeNghi);
            data.Add("FTongTienDeNghi", FTongTienDeNghi);
            data.Add("ITongSoDeNghi", ITongSoDeNghi);
            data.Add("FTongTienPheDuyet", FTongTienPheDuyet);
            data.Add("HEADER2", string.Empty);
            data.Add("FormatNumber", formatNumber);
            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
            data.Add("TienBangChu", StringUtils.NumberToText(FTongTienDeNghi.Value * donViTinh, true));
            data.Add("Year", _sessionInfo.YearOfWork);
            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
            string fileNamePrefix;
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQBHXH, fileName + FileExtensionFormats.Xlsx);
            fileNamePrefix = Path.GetFileNameWithoutExtension(fileName);
            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
            var xlsFile = _exportService.Export<BhQtcqBHXHChiTietModel>(templateFileName, data);
            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, fileNamePrefix, null, xlsFile));
            return results;
        }

        private ExportResult OnExportCoverSheet()
        {
            RptBhQtcChiQuyBHXHQuyetToanToBia rptToBia = new RptBhQtcChiQuyBHXHQuyetToanToBia
            {
                Cap1 = _cap1,
                Cap2 = _sessionInfo.TenDonVi,
                TieuDe = _dmChuKy?.Ten,
                ThoiGian = string.Format("{0} năm {1}", CbxQuaterSelected.DisplayItem, _sessionInfo.YearOfWork),
                Ngay = DateUtils.FormatDateReport(ReportDate),
                DiaDiem = _diaDiem
            };
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (var prop in rptToBia.GetType().GetProperties())
            {
                data.Add(prop.Name, prop.GetValue(rptToBia));
            }
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQBHXH, ExportFileName.RPT_BH_QTC_QBHXH_TOBIA);
            string fileNamePrefix = ExportFileName.RPT_BH_QTC_QBHXH_TOBIA.Split(".").First() + "_" + CbxQuaterSelected.DisplayItem;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private void ExportBaoCaoGiaiThichTroCapOmDau(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                    var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
                    bool isTongHop = true;
                    int iQuy = int.Parse(CbxQuaterSelected.ValueItem);

                    List<BhBaoCaoQuyetToanChiQuyQuery> lstData = new List<BhBaoCaoQuyetToanChiQuyQuery>();
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString())
                    {
                        isTongHop = false;

                        lstData = _qtcqBHXHChiTietService.BaoCaoGiaiThichTroCapOmDau(_sessionService.Current.YearOfWork, string.Join(",", lstIdDonVi), LNSValue.LNS_9010001_9010002, isTongHop, iQuy, donViTinh).ToList();
                    }
                    else
                    {
                        if (IsInNhomDoiTuong)
                            lstData = _qtcqBHXHChiTietService.BaoCaoGiaiThichTroCapOmDauKhoi(_sessionService.Current.YearOfWork, string.Join(",", lstIdDonVi), LNSValue.LNS_9010001_9010002, isTongHop, iQuy, donViTinh).ToList();
                        else
                            lstData = _qtcqBHXHChiTietService.BaoCaoGiaiThichTroCapOmDauKhoiNhomDT(_sessionService.Current.YearOfWork, string.Join(",", lstIdDonVi), LNSValue.LNS_9010001_9010002, isTongHop, iQuy, donViTinh).ToList();

                    }

                    lstData = lstData.Where(x => (x.FTongTien ?? 0) != 0).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("YearWork", yearOfWork);
                    data.Add("ListData", lstData);
                    data.Add("Quy", CbxQuaterSelected.DisplayItem);

                    //Tính tổng
                    int? ITongSoNgayDuoi14BenhDaiNgay = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgayDuoi14BenhDaiNgay).Sum();
                    Double? FTongSoTienDuoi14BenhDaiNgay = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienDuoi14BenhDaiNgay).Sum();
                    int? ITongSoNgayTren14BenhDaiNgay = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgayTren14BenhDaiNgay).Sum();
                    Double? FTongSoTienTren14BenhDaiNgay = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienTren14BenhDaiNgay).Sum();
                    int? ITongSoNgayDuoi14OmKhac = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgayDuoi14OmKhac).Sum();
                    Double? FTongSoTienDuoi14OmKhac = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienDuoi14OmKhac).Sum();
                    int? ITongSoNgayTren14OmKhac = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgayTren14OmKhac).Sum();
                    Double? FTongSoTienTren14OmKhac = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienTren14OmKhac).Sum();
                    int? ITongSoNgayConOm = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgayConOm).Sum();
                    Double? FTongSoTienConOm = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienConOm).Sum();
                    int? ITongSoNgayPHSK = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgayPHSK).Sum();
                    Double? FTongSoTienPHSK = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienPHSK).Sum();
                    Double? FTongTien = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTien).Sum();

                    data.Add("ITongSoNgayDuoi14BenhDaiNgay", ITongSoNgayDuoi14BenhDaiNgay);
                    data.Add("FTongSoTienDuoi14BenhDaiNgay", FTongSoTienDuoi14BenhDaiNgay);
                    data.Add("ITongSoNgayTren14BenhDaiNgay", ITongSoNgayTren14BenhDaiNgay);
                    data.Add("FTongSoTienTren14BenhDaiNgay", FTongSoTienTren14BenhDaiNgay);
                    data.Add("ITongSoNgayDuoi14OmKhac", ITongSoNgayDuoi14OmKhac);
                    data.Add("FTongSoTienDuoi14OmKhac", FTongSoTienDuoi14OmKhac);
                    data.Add("ITongSoNgayTren14OmKhac", ITongSoNgayTren14OmKhac);
                    data.Add("FTongSoTienTren14OmKhac", FTongSoTienTren14OmKhac);
                    data.Add("ITongSoNgayConOm", ITongSoNgayConOm);
                    data.Add("FTongSoTienConOm", FTongSoTienConOm);
                    data.Add("ITongSoNgayPHSK", ITongSoNgayPHSK);
                    data.Add("FTongSoTienPHSK", FTongSoTienPHSK);
                    data.Add("FTongTien", FTongTien);
                    data.Add("FormatNumber", formatNumber);
                    AddChuKy(data);
                    data.Add("DiaDiem", string.Empty);
                    data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", _sessionInfo.YearOfWork);

                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhBaoCaoQuyetToanChiQuyQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void AddChuKy(Dictionary<string, object> data)
        {
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);

            if (IsVerbalExplanation)
            {
                data.Add("Ten4", _dmChuKy != null ? _dmChuKy.Ten4MoTa : string.Empty);
                data.Add("Ten5", _dmChuKy != null ? _dmChuKy.Ten5MoTa : string.Empty);
                data.Add("Ten6", _dmChuKy != null ? _dmChuKy.Ten6MoTa : string.Empty);
                data.Add("ChucDanh4", _dmChuKy != null ? _dmChuKy.ChucDanh4MoTa : string.Empty);
                data.Add("ChucDanh5", _dmChuKy != null ? _dmChuKy.ChucDanh5MoTa : string.Empty);
                data.Add("ChucDanh6", _dmChuKy != null ? _dmChuKy.ChucDanh6MoTa : string.Empty);
                data.Add("ThuaLenh4", _dmChuKy != null ? _dmChuKy.ThuaLenh4MoTa : string.Empty);
                data.Add("ThuaLenh5", _dmChuKy != null ? _dmChuKy.ThuaLenh5MoTa : string.Empty);
                data.Add("ThuaLenh6", _dmChuKy != null ? _dmChuKy.ThuaLenh6MoTa : string.Empty);

            }
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC)
            {
                data.Add("Ten4", _dmChuKy != null ? _dmChuKy.Ten4MoTa : string.Empty);
                data.Add("ThuaLenh4", _dmChuKy == null ? string.Empty : _dmChuKy.ThuaLenh4MoTa);
                data.Add("ChucDanh4", _dmChuKy == null ? string.Empty : _dmChuKy.ChucDanh4MoTa);
                data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
                data.Add("Ten5", _dmChuKy != null ? _dmChuKy.Ten5MoTa : string.Empty);
                data.Add("ThuaLenh5", _dmChuKy == null ? string.Empty : _dmChuKy.ThuaLenh5MoTa);
                data.Add("ChucDanh5", _dmChuKy == null ? string.Empty : _dmChuKy.ChucDanh5MoTa);
                data.Add("GhiChuKy5", "(Ký, họ tên, đóng dấu)");
                if (_dmChuKy != null && (!_dmChuKy.ThuaLenh4MoTa.IsEmpty() || !_dmChuKy.ChucDanh4MoTa.IsEmpty() || !_dmChuKy.Ten4MoTa.IsEmpty()))
                {
                    data.Add("Co4ChuKy", true);
                }
            }
        }

        private void AddChuKyCheDoBHXH(Dictionary<string, object> data, string idType)
        {
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("Ten1", dmChyKy != null ? dmChyKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", dmChyKy != null ? dmChyKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", dmChyKy != null ? dmChyKy.Ten3MoTa : string.Empty);
            data.Add("ChucDanh1", dmChyKy != null ? dmChyKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", dmChyKy != null ? dmChyKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", dmChyKy != null ? dmChyKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", dmChyKy != null ? dmChyKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", dmChyKy != null ? dmChyKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", dmChyKy != null ? dmChyKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten4", dmChyKy != null ? dmChyKy.Ten4MoTa : string.Empty);
            data.Add("ThuaLenh4", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh4MoTa);
            data.Add("ChucDanh4", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh4MoTa);
            data.Add("GhiChuKy4", dmChyKy != null ? dmChyKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten5", dmChyKy != null ? dmChyKy.Ten5MoTa : string.Empty);
            data.Add("ThuaLenh5", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh5MoTa);
            data.Add("ChucDanh5", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh5MoTa);
            data.Add("GhiChuKy5", "(Ký, họ tên, đóng dấu)");
            if (dmChyKy != null && (!dmChyKy.ThuaLenh4MoTa.IsEmpty() || !dmChyKy.ChucDanh4MoTa.IsEmpty() || !dmChyKy.Ten4MoTa.IsEmpty()))
            {
                data.Add("Co4ChuKy", true);
            }
        }

        private void ExportBaoCaoGiaiThichTroCapTaiNanLD(bool isPdf)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                    var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
                    int iQuy = int.Parse(CbxQuaterSelected.ValueItem);


                    List<BhQtcqGiaiThichTroCapTaiNanQuery> lstData = new List<BhQtcqGiaiThichTroCapTaiNanQuery>();
                    List<BhQtcqGiaiThichTroCapTaiNanQuery> lstDataTruyLinh = new List<BhQtcqGiaiThichTroCapTaiNanQuery>();

                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString())
                    {
                        lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapTaiNan(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                    }
                    else
                    {
                        if (IsInNhomDoiTuong)
                            lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapTaiNanNNTheoKhoi(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                        else lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapTaiNanNNTheoKhoiNhomDT(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                    }

                    lstData = lstData.Where(x => x.IsHadData).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("ListData", lstData);
                    data.Add("Quy", CbxQuaterSelected.DisplayItem);

                    //Tính tổng
                    double? FTienTongCong = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTongTienCong).Sum();
                    double? FTienTongCongTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTongTienCongTL).Sum();
                    double? FTongTienGiamDinh = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienGiamDinh).Sum();
                    double? FTongTienGiamDinhTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienGiamDinhTL).Sum();
                    double? FTongTienTroCap1Lan = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTroCap1Lan).Sum();
                    double? FTongTienTroCap1LanTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTroCap1LanTL).Sum();
                    double? FTongTienTCTP = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTCTP).Sum();
                    double? FTongTienTCTPTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTCTPTL).Sum();
                    double? FTongTienTCHangThang = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTCHangThang).Sum();
                    double? FTongTienTCHangThangTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTCHangThangTL).Sum();
                    double? FTongTienTCPHCNvPV = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTCPHCNvPV).Sum();
                    double? FTongTienTCPHCNvPVTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTCPHCNvPVTL).Sum();
                    double? FTongTienTCCDTNLD = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTCCDTNLD).Sum();
                    double? FTongTienTCCDTNLDTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienTCCDTNLDTL).Sum();

                    int? ITongISoNgayDSPHSK = lstData?.Where(x => !x.IsHangCha).Select(x => x.ISoNgayDSPHSK).Sum();
                    int? ITongSoNgayDSPHSKTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.ISoNgayDSPHSKTL).Sum();
                    double? FTongTienDSPHSK = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienDSPHSK).Sum();
                    double? FTongTienDSPHSKTL = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTienDSPHSKTL).Sum();
                    double? FTotal = lstData?.Where(x => !x.IsHangCha).Select(x => x.FTongCong).Sum();

                    data.Add("FTienTongCong", FTienTongCong);
                    data.Add("FTienTongCongTL", FTienTongCongTL);
                    data.Add("FTongTienGiamDinh", FTongTienGiamDinh);
                    data.Add("FTongTienGiamDinhTL", FTongTienGiamDinhTL);
                    data.Add("FTongTienTroCap1Lan", FTongTienTroCap1Lan);
                    data.Add("FTongTienTroCap1LanTL", FTongTienTroCap1LanTL);
                    data.Add("FTongTienTCTP", FTongTienTCTP);
                    data.Add("FTongTienTCTPTL", FTongTienTCTPTL);
                    data.Add("FTongTienTCHangThang", FTongTienTCHangThang);
                    data.Add("FTongTienTCHangThangTL", FTongTienTCHangThangTL);
                    data.Add("FTongTienTCPHCNvPV", FTongTienTCPHCNvPV);
                    data.Add("FTongTienTCPHCNvPVTL", FTongTienTCPHCNvPVTL);
                    data.Add("FTongTienTCCDTNLD", FTongTienTCCDTNLD);
                    data.Add("FTongTienTCCDTNLDTL", FTongTienTCCDTNLDTL);
                    data.Add("ITongISoNgayDSPHSK", ITongISoNgayDSPHSK);
                    data.Add("ITongSoNgayDSPHSKTL", ITongSoNgayDSPHSKTL);
                    data.Add("FTongTienDSPHSK", FTongTienDSPHSK);
                    data.Add("FTongTienDSPHSKTL", FTongTienDSPHSKTL);
                    data.Add("FTotal", FTotal);
                    data.Add("YearWork", yearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    AddChuKy(data);

                    data.Add("DiaDiem", string.Empty);
                    data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhQtcqGiaiThichTroCapTaiNanQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
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

        private void ExportBaoCaoGiaiThichTroCapHuuTriPVXNTVTT(bool isPdf)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                    var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
                    int iQuy = int.Parse(CbxQuaterSelected.ValueItem);


                    List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> lstData = new List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery>();

                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString())
                    {
                        if (IsInDetailTCXN)
                        {
                            lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapHTPVXNTVTTDetailXN(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                        }
                        else
                        {
                            lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapHTPVXNTVTT(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                        }

                    }
                    else
                    {
                        if (IsInNhomDoiTuong)
                        {
                            if (IsInDetailTCXN)
                            {
                                lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapHTPVXNTVTTKhoiDetailXN(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                            }
                            else
                            {
                                lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapHTPVXNTVTTKhoi(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                            }
                        }
                        else
                        {
                            if (IsInDetailTCXN)
                            {
                                lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDTDetail(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                            }
                            else
                            {
                                lstData = _qtcqBHXHChiTietService.ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDT(yearOfWork, donViTinh, iQuy, string.Join(",", lstIdDonVi)).ToList();
                            }
                        }

                    }

                    lstData = lstData.Where(x => x.IsNotData).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("ListData", lstData);
                    data.Add("Quy", CbxQuaterSelected.DisplayItem);

                    //Tính tổng
                    double? FTienTongCong = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTien).Sum();
                    double? FTienTongCongTL = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienTL).Sum();
                    double? FTongTienTroCap1Lan = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienTroCap1Lan).Sum();
                    double? FTongTienTroCap1LanTL = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienTroCap1LanTL).Sum();
                    double? FTongTienTroCapKV = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienTroCapKV).Sum();
                    double? FTongTienTroCapKVTL = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienTroCapKVTL).Sum();
                    double? FTongTienTroCapMT = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienTroCapMT).Sum();
                    double? FTongTienTroCapMTTL = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienTroCapMTTL).Sum();
                    double? FTongTienALL = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienAll).Sum();

                    data.Add("FTienTongCong", FTienTongCong);
                    data.Add("FTienTongCongTL", FTienTongCongTL);
                    data.Add("FTongTienTroCap1Lan", FTongTienTroCap1Lan);
                    data.Add("FTongTienTroCap1LanTL", FTongTienTroCap1LanTL);
                    data.Add("FTongTienTroCapKV", FTongTienTroCapKV);
                    data.Add("FTongTienTroCapKVTL", FTongTienTroCapKVTL);
                    data.Add("FTongTienTroCapMT", FTongTienTroCapMT);
                    data.Add("FTongTienTroCapMTTL", FTongTienTroCapMTTL);
                    data.Add("FTongTienALL", FTongTienALL);

                    data.Add("YearWork", yearOfWork);
                    data.Add("FormatNumber", formatNumber);
                    AddChuKy(data);

                    data.Add("DiaDiem", string.Empty);
                    data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
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

        private void ExportBaoCaoGiaiThichTroCapThaiSan(bool isPdf)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                    var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
                    bool isTongHop = true;
                    int iQuy = int.Parse(CbxQuaterSelected.ValueItem);

                    List<BhBaoCaoQuyetToanChiQuyQuery> lstData = new List<BhBaoCaoQuyetToanChiQuyQuery>();
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString())
                    {
                        lstData = _qtcqBHXHChiTietService.BaoCaoGiaiThichTroCapThaiSan(yearOfWork, string.Join(",", lstIdDonVi), LNSValue.LNS_9010001_9010002, isTongHop, iQuy, donViTinh).ToList();
                        lstData = lstData.OrderBy(x => x.IID_MaDonVi).ToList();
                    }
                    else
                    {
                        if (IsInNhomDoiTuong)
                            lstData = _qtcqBHXHChiTietService.BaoCaoGiaiThichTroCapThaiSanFollowKhoi(yearOfWork, string.Join(",", lstIdDonVi), iQuy, donViTinh).ToList();
                        else
                            lstData = _qtcqBHXHChiTietService.BaoCaoGiaiThichTroCapThaiSanFollowKhoiNhomDT(yearOfWork, string.Join(",", lstIdDonVi), iQuy, donViTinh).ToList();
                    }
                    lstData = lstData.Where(x => (x.FTongTien ?? 0) != 0).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("YearWork", yearOfWork);
                    data.Add("ListData", lstData);
                    data.Add("Quy", CbxQuaterSelected.DisplayItem);

                    //Tính tổng
                    int? ITongSoNgaySinhConNNuoiCon = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgaySinhConNNuoiCon).Sum();
                    Double? FTongSoTienSinhConNNuoiCon = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienSinhConNNuoiCon).Sum();
                    int? ITongSoNgaySinhTroCapSinhCon = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgaySinhTroCapSinhCon).Sum();
                    Double? FTongSoTienSinhTroCapSinhCon = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienSinhTroCapSinhCon).Sum();
                    int? ITongSoNgayKhamThaiKHHGD = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgayKhamThaiKHHGD).Sum();
                    Double? FTongSoTienKhamThaiKHHGD = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienKhamThaiKHHGD).Sum();
                    int? ITongSoNgayPHSKThaiSan = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoNgayPHSKThaiSan).Sum();
                    Double? FTongSoTienPHSKThaiSan = lstData?.Where(x => !x.BHangCha).Select(x => x.FSoTienPHSKThaiSan).Sum();
                    Double? FTongTien = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTien).Sum();


                    data.Add("ITongSoNgaySinhConNNuoiCon", ITongSoNgaySinhConNNuoiCon);
                    data.Add("FTongSoTienSinhConNNuoiCon", FTongSoTienSinhConNNuoiCon);
                    data.Add("ITongSoNgaySinhTroCapSinhCon", ITongSoNgaySinhTroCapSinhCon);
                    data.Add("FTongSoTienSinhTroCapSinhCon", FTongSoTienSinhTroCapSinhCon);
                    data.Add("ITongSoNgayKhamThaiKHHGD", ITongSoNgayKhamThaiKHHGD);
                    data.Add("FTongSoTienKhamThaiKHHGD", FTongSoTienKhamThaiKHHGD);
                    data.Add("ITongSoNgayPHSKThaiSan", ITongSoNgayPHSKThaiSan);
                    data.Add("FTongSoTienPHSKThaiSan", FTongSoTienPHSKThaiSan);
                    data.Add("FTongTien", FTongTien);
                    data.Add("FormatNumber", formatNumber);
                    AddChuKy(data);

                    data.Add("DiaDiem", string.Empty);
                    data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", _sessionInfo.YearOfWork);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhBaoCaoQuyetToanChiQuyQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
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

        private void ExportBaoCaoThongTriQuyetToanChiCacCheDoBHXH(bool isPdf)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
                    var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
                    bool isTongHop = true;
                    int Stt = 1;
                    int iLoaiChungTu = 2;
                    int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
                    var donViCurrent = GetDonViOfCurrentUser();
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);

                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                        foreach (var donVi in lstIdDonVi)
                        {
                            DonVi donViChild = _donViService.FindByIdDonVi(donVi, yearOfWork);

                            var lstData = _mapper.Map<List<BhQtcqBHXHChiTietModel>>(_qtcqBHXHChiTietService.BaoCaoQuyetToanChiQuyBHXH(_sessionService.Current.YearOfWork, donVi, LNSValue.LNS_9010001_9010002, isTongHop, iQuy, donViTinh));

                            CalculateData(lstData);

                            lstData.ForEach(x =>
                            {
                                x.SLNS = string.Empty;
                                x.SL = string.Empty;
                                x.SK = string.Empty;
                                x.SM = string.Empty;
                                x.STM = string.Empty;
                                x.STTM = string.Empty;
                                x.SNG = string.Empty;

                            });

                            lstData = lstData.Where(x => (x.FTongTienDeNghi ?? 0) != 0).ToList();
                            lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                            data.Add("TieuDe1", Title1);
                            data.Add("TieuDe2", Title2);
                            data.Add("TieuDe3", Title3);
                            data.Add("YearWork", yearOfWork);
                            data.Add("DonVi", donViChild != null ? donViChild.TenDonVi : string.Empty);
                            data.Add("ListData", lstData);
                            data.Add("Quy", CbxQuaterSelected.DisplayItem);

                            //Tính tổng
                            Double? FTongTienDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienDeNghi).Sum();

                            data.Add("FTongTienDeNghi", Math.Round((double)FTongTienDeNghi));
                            data.Add("TongSoTien", FTongTienDeNghi != null ? StringUtils.NumberToText(Math.Round((double)FTongTienDeNghi, 0), true) : string.Empty);
                            data.Add("FormatNumber", formatNumber);

                            AddChuKy(data);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", _sessionInfo.YearOfWork);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQtcqBHXHChiTietModel>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                    }
                    else if (SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString())
                    {
                        isTongHop = false;
                        foreach (var donVi in lstIdDonVi)
                        {
                            DonVi donViChild = _donViService.FindByIdDonVi(donVi, yearOfWork);

                            var temp = _qtcqBHXHChiTietService.BaoCaoQuyetToanChiQuyBHXH(_sessionService.Current.YearOfWork, donVi, LNSValue.LNS_9010001_9010002, isTongHop, iQuy, donViTinh).ToList();
                            var listDataModel = _mapper.Map<List<BhQtcqBHXHChiTietModel>>(temp);
                            //CalculateData(lstData);
                            var lstData = new List<BhQtcqBHXHChiTietModel>()
                            {
                                new BhQtcqBHXHChiTietModel()
                                {
                                    FTienDeNghi = listDataModel.Where(x => !x.BHangCha).Sum(x => x.FTongTienDeNghi),
                                    FTienPheDuyet = listDataModel.Where(x => !x.BHangCha).Sum(x => x.FTongTienPheDuyet),
                                    SLoaiTroCap = NoiDungChi
                                }
                            };

                            lstData.ForEach(x =>
                            {
                                x.SLNS = string.Empty;
                                x.SL = string.Empty;
                                x.SK = string.Empty;
                                x.SM = string.Empty;
                                x.STM = string.Empty;
                                x.STTM = string.Empty;
                                x.SNG = string.Empty;

                            });

                            AddEmptyItems(lstData);
                            //lstData = lstData.Where(x => (x.FTongTienDeNghi ?? 0) != 0).ToList();
                            //lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                            data.Add("TieuDe1", Title1);
                            data.Add("TieuDe2", Title2);
                            data.Add("TieuDe3", Title3);
                            data.Add("YearWork", yearOfWork);
                            data.Add("DonVi", donViChild != null ? donViChild.TenDonVi : string.Empty);
                            data.Add("ListData", lstData);
                            data.Add("Quy", CbxQuaterSelected.DisplayItem);

                            //Tính tổng
                            Double? FTongTienDeNghi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienDeNghi).Sum();
                            data.Add("FTongTienDeNghi", FTongTienDeNghi);
                            data.Add("TongSoTien", FTongTienDeNghi != null ? StringUtils.NumberToText((double)FTongTienDeNghi, true) : string.Empty);
                            data.Add("FormatNumber", formatNumber);

                            AddChuKy(data);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", _sessionInfo.YearOfWork);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            string templateFileName;
                            //templateFileName = GetTemplate();
                            templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQBHXH, ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANQUYTHEOLOAICHIBHXH);
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQtcqBHXHChiTietModel>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                    }
                    else
                    {
                        var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));

                        if (IsInTheoTongHop)
                        {
                            var lstChungTu = _qtcqBHXHService.GetDanhSachQuyetToanQuyBHXH(yearOfWork).Where(x => x.IIdMaDonVi == lstDonViChecked).FirstOrDefault();
                            if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                            {
                                var sSoChungTu = lstChungTu.STongHop.Split(",");
                                var lstChungTuChild = _qtcqBHXHService.GetDanhSachQuyetToanQuyBHXH(yearOfWork).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                                var lsdMaDonVi = lstChungTuChild.Select(x => x.IIdMaDonVi).Distinct().ToList();
                                lstDonViChecked = string.Join(",", lsdMaDonVi);
                            }
                        }

                        List<ReportBHQTCQBHXHThongTriQuery> lstData = new List<ReportBHQTCQBHXHThongTriQuery>();
                        lstData = _qtcqBHXHChiTietService.GetDataThongTriForDonVi(_sessionService.Current.YearOfWork, CbxQuaterSelected.ValueItem, lstDonViChecked, _sessionInfo.Principal, iLoaiChungTu, donViTinh).ToList();

                        lstData.ForEach(x =>
                        {
                            x.Stt = Stt.ToString();
                            Stt++;
                        });

                        AddEmptyItems(lstData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("DonVi", !string.IsNullOrEmpty(sCap2) ? sCap2 : donViCurrent.TenDonVi);
                        data.Add("Items", lstData);
                        data.Add("Ve", string.Format("quý {0} năm {1}", CbxQuaterSelected.ValueItem, yearOfWork));

                        Double? FTongTienDeNghi = lstData?.Select(x => x.FTongTienDeNghi).Sum();
                        data.Add("TienBangChu", FTongTienDeNghi != null ? StringUtils.NumberToText((double)FTongTienDeNghi, true) : string.Empty);
                        data.Add("TongChiTieu", FTongTienDeNghi);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("GhiChu", string.Empty);
                        AddChuKy(data);
                        data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                        data.Add("Year", _sessionInfo.YearOfWork);
                        data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                        string templateFileName;
                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQBHXH, ExportFileName.RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOTHONGTRITHEODONVIBHXH);
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<ReportBHQTCQBHXHThongTriQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        ExportType exportType = isPdf ? ExportType.PDF : ExportType.EXCEL;
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() || SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString())
                        {
                            if (InMotToChecked)
                            {
                                if (isPdf)
                                {
                                    exportType = ExportType.PDF_ONE_PAPER;
                                }
                                else
                                {
                                    exportType = ExportType.EXCEL_ONE_PAPER;
                                }
                            }
                        }
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


        private void CalculateDataDuToan(List<BhQtcqBHXHChiTietModel> lstData)
        {
            lstData.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTienDuToanDuyet = 0;
                });
            var dictByMlns = lstData.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = lstData.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {

                CalculateParentDuToan(item.IID_MLNS_Cha, item, dictByMlns);
            }

        }

        private void CalculateParentDuToan(Guid? idParent, BhQtcqBHXHChiTietModel item, Dictionary<Guid?, BhQtcqBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);
            CalculateParentDuToan(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void ExportDanhSachNguoiLaoDongNghiViec(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
                    string lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));

                    if (IsInTheoTongHop)
                    {
                        var lstChungTu = _qtcqBHXHService.GetDanhSachQuyetToanQuyBHXH(yearOfWork).Where(x => x.IIdMaDonVi == lstDonViChecked).FirstOrDefault();
                        if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                        {
                            var sSoChungTu = lstChungTu.STongHop.Split(",");
                            var lstChungTuChild = _qtcqBHXHService.GetDanhSachQuyetToanQuyBHXH(yearOfWork).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                            var lsdMaDonVi = lstChungTuChild.Select(x => x.IIdMaDonVi).Distinct().ToList();
                            lstDonViChecked = string.Join(",", lsdMaDonVi);
                        }
                    }

                    List<ExportResult> results = new List<ExportResult>();
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    var lstData = _qtcqBHXHChiTietService.ExportDanhSachNguoiLaoDongNghiViec(yearOfWork, donViTinh, iQuy, lstDonViChecked);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("ListData", lstData);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                    data.Add("DonVi", "");
                    data.Add("IsAggregate", true);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("YearOfWork", yearOfWork);
                    data.Add("Quy", CbxQuaterSelected.DisplayItem);
                    data.Add("FormatNumber", formatNumber);
                    //Tinh tong
                    data.Add("TongTienLuongThangDongBHXH", lstData.Where(n => !n.IsHangCha).Sum(x => x.FTienLuongThangDongBHXH));
                    data.Add("TongSoNgayHuong", lstData.Where(n => !n.IsHangCha).Sum(x => x.ISoNgayHuong));
                    data.Add("TongSoTien", lstData.Where(n => !n.IsHangCha).Sum(x => x.FSoTien));
                    AddChuKy(data);
                    data.Add("Year", _sessionInfo.YearOfWork);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhQtcqGiaiThichTroCapQuery>(templateFileName, data);
                    results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI DANH SÁCH NLĐ NGHỈ VIỆC " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void ExportDanhSachNguoiLaoDongNghiViecDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    foreach (var dv in lstIdDonVi)
                    {
                        var donvi = _donViService.FindByMaDonViAndNamLamViec(dv.Id, yearOfWork);
                        var lstData = _qtcqBHXHChiTietService.ExportDanhSachNguoiLaoDongNghiViec(yearOfWork, donViTinh, iQuy, dv.Id);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("ListData", lstData);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("DonVi", donvi.TenDonVi);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("YearOfWork", yearOfWork);
                        data.Add("Quy", CbxQuaterSelected.DisplayItem);
                        data.Add("FormatNumber", formatNumber);
                        //Tinh tong
                        data.Add("TongTienLuongThangDongBHXH", lstData.Where(n => !n.IsHangCha).Sum(x => x.FTienLuongThangDongBHXH));
                        data.Add("TongSoNgayHuong", lstData.Where(n => !n.IsHangCha).Sum(x => x.ISoNgayHuong));
                        data.Add("TongSoTien", lstData.Where(n => !n.IsHangCha).Sum(x => x.FSoTien));
                        AddChuKy(data);
                        data.Add("Year", _sessionInfo.YearOfWork);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhQtcqGiaiThichTroCapQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI DANH SÁCH NLĐ NGHỈ VIỆC " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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

        private DonVi GetDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy == null) return string.Empty;
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private void CalculateData(List<BhQtcqBHXHChiTietModel> lstNdtChungTuChiTiet)
        {
            lstNdtChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.ISoLuyKeCuoiQuyTruoc = 0;
                    x.FTienLuyKeCuoiQuyTruoc = 0;
                    x.ISoSQDeNghi = 0;
                    x.FTienSQDeNghi = 0;
                    x.ISoQNCNDeNghi = 0;
                    x.FTienQNCNDeNghi = 0;
                    x.ISoCNVCQPDeNghi = 0;
                    x.FTienCNVCQPDeNghi = 0;
                    x.ISoHSQBSDeNghi = 0;
                    x.FTienHSQBSDeNghi = 0;
                    x.ISoLDHDDeNghi = 0;
                    x.FTienLDHDDeNghi = 0;
                    x.FTongTienPheDuyet = 0;
                    return x;
                }).ToList();
            var temp = lstNdtChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, BhQtcqBHXHChiTietModel item, List<BhQtcqBHXHChiTietModel> lstNdtChungTuChiTiet)
        {
            var dictByMlns = lstNdtChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];

            model.ISoLuyKeCuoiQuyTruoc = (model.ISoLuyKeCuoiQuyNay ?? 0) + (item.ISoLuyKeCuoiQuyNay ?? 0);
            model.FTienLuyKeCuoiQuyTruoc = (model.FTienLuyKeCuoiQuyNay ?? 0) + (item.FTienLuyKeCuoiQuyNay ?? 0);
            model.ISoSQDeNghi = (model.ISoSQDeNghi ?? 0) + (item.ISoSQDeNghi ?? 0);
            model.FTienSQDeNghi = (model.FTienSQDeNghi ?? 0) + (item.FTienSQDeNghi ?? 0);
            model.ISoQNCNDeNghi = (model.ISoQNCNDeNghi ?? 0) + (item.ISoQNCNDeNghi ?? 0);
            model.FTienQNCNDeNghi = (model.FTienQNCNDeNghi ?? 0) + (item.FTienQNCNDeNghi ?? 0);
            model.ISoCNVCQPDeNghi = (model.ISoCNVCQPDeNghi ?? 0) + (item.ISoCNVCQPDeNghi ?? 0);
            model.FTienCNVCQPDeNghi = (model.FTienCNVCQPDeNghi ?? 0) + (item.FTienCNVCQPDeNghi ?? 0);
            model.ISoHSQBSDeNghi = (model.ISoHSQBSDeNghi ?? 0) + (item.ISoHSQBSDeNghi ?? 0);
            model.FTienHSQBSDeNghi = (model.FTienHSQBSDeNghi ?? 0) + (item.FTienHSQBSDeNghi ?? 0);
            model.ISoLDHDDeNghi = (model.ISoLDHDDeNghi ?? 0) + (item.ISoLDHDDeNghi ?? 0);
            model.FTienLDHDDeNghi = (model.FTienLDHDDeNghi ?? 0) + (item.FTienLDHDDeNghi ?? 0);
            model.FTongTienPheDuyet = (model.FTongTienPheDuyet ?? 0) + (item.FTongTienPheDuyet ?? 0);

            CalculateParent(model.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
        }

        private string GetTemplate()
        {
            string input = "";
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXH);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPOMDAU04A);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPTHAISAN04B);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPTAINANNGHENGHIEP);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPHUUTRIPHUCVU);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANQUYBHXH);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_DANH_SACH_NLD_NGHI_VIEC);
            }

            return Path.Combine(ExportPrefix.PATH_BH_QTCQBHXH, input + FileExtensionFormats.Xlsx);
        }

    }
}
