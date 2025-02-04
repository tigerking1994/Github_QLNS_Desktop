using AutoMapper;
using FlexCel.Core;
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
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.PrintReport
{
    public class PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel : ViewModelBase
    {
        #region
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IBhQtcNamKinhPhiKhacService _kinhPhiKhacService;
        private readonly IBhQtcNamKinhPhiKhacChiTietService _kinhPhiKhacChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ICollectionView _listDonViView;
        private readonly INsPhongBanService _phongBanService;
        private ICollectionView _listAgency;
        private ICollectionView _listLNSView;
        #endregion

        #region Property
        private readonly DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private readonly List<BhQtcNamKinhPhiKhacQuery> _listChungTu;
        private readonly List<BhQtcNamKinhPhiKhacQuery> _listChungTuDotCap;
        private readonly BhQtcNamKinhPhiKhacQuery _chungTuSelected;
        private SessionInfo _sessionInfo;
        private List<ReportBHQTCNKPKhacPhuLucQuery> _listPhuLuc;
        public List<BhQtcNamKinhPhiKhacChiTietQuery> _reportData;
        IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi;
        private readonly bool _isCapPhatToanDonVi;
        private string _diaDiem;
        private string _typeChuKy;
        private bool _isInitReport;
        private bool _checkAllAgencies;
        private string _cap1;

        private ObservableCollection<ComboboxItem> _dataKieuGiay;
        public ObservableCollection<ComboboxItem> DataKieuGiay
        {
            get => _dataKieuGiay;
            set => SetProperty(ref _dataKieuGiay, value);
        }

        private ComboboxItem _selectedKieuGiay;
        public ComboboxItem SelectedKieuGiay
        {
            get => _selectedKieuGiay;
            set => SetProperty(ref _selectedKieuGiay, value);
        }

        public bool IsShowAll { get; set; }
        public override Type ContentType => typeof(PrintQuyetToanNamChiKinhPhiKhacNotice);
        public override string Name
        {
            get
            {
                if (SettlementTypeValue != (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN)
                {
                    return "IN PHỤ LỤC QUYẾT TOÁN CHI NĂM KINH PHÍ KHÁC";
                }
                else
                {
                    return "IN BÁO CÁO QUYẾT TOÁN CHI NĂM KINH PHÍ KHÁC";
                }
            }
        }
        public override string Description
        {
            get
            {
                if (SettlementTypeValue != (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN)
                {
                    return "IN PHỤ LỤC QUYẾT TOÁN CHI NĂM KINH PHÍ KHÁC";
                }
                else
                {
                    return "IN BÁO CÁO QUYẾT TOÁN CHI NĂM KINH PHÍ KHÁC";
                }
            }
        }
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public bool IsShowRadioLoaiHSSVNLD => _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002;
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();
        public int SettlementTypeValue { get; set; }
        private bool _isShowRadioLoaiChi;

        public bool IsShowRadioLoaiChi
        {
            get => _isShowRadioLoaiChi;
            set
            {
                SetProperty(ref _isShowRadioLoaiChi, value);
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
        private LoaiChi _loaiChi;
        public LoaiChi LoaiChi
        {
            get => _loaiChi;
            set
            {
                SetProperty(ref _loaiChi, value);
                LoadLNS();
                LoadTieuDe();
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
                    OnPropertyChanged(nameof(IsExportEnable));
                    LoadLNS();
                }
                _checkAllAgencies = false;
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

        public bool IsExportEnable => Agencies != null && Agencies.Where(x => x.Selected).Count() > 0;

        public string Title1Default;
        public string Title2Default;
        public string Title3Default;
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

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);

                if (_selectedDanhMucLoaiChi != null)
                {
                    LoadTieuDe();
                    LoadAgencies();

                    if (SettlementTypeValue == (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM)
                    {
                        OnPropertyChanged(nameof(IsShowRadioLoaiHSSVNLD));
                    }
                }
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }
        public bool IshowLoaiBaoCao { get; set; }
        public bool IsShowTheoTongHop => IshowLoaiBaoCao;
        #endregion

        #region RelayCommand
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region 
        public PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel(
                    INsDonViService donViService,
                    IMapper mapper,
                    ISessionService sessionService,
                    ILog logger,
                    IDanhMucService danhMucService,
                    IExportService exportService,
                    IDmChuKyService dmChuKyService,
                    DmChuKyDialogViewModel dmChuKyDialogViewModel,
                    INsNguoiDungDonViService nguoiDungDonViService,
                    IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
                    IBhQtcNamKinhPhiKhacService kinhPhiKhacService,
                    IBhQtcNamKinhPhiKhacChiTietService kinhPhiKhacChiTietService,
                    IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                    INsPhongBanService nsPhongBanService
            )
        {
            _donViService = donViService;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _kinhPhiKhacService = kinhPhiKhacService;
            _kinhPhiKhacChiTietService = kinhPhiKhacChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _phongBanService = nsPhongBanService;

            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }
        #endregion

        #region  Init
        public override void Init()
        {
            try
            {
                base.Init();
                _sessionInfo = _sessionService.Current;
                InitReportDefaultDate();
                _agencies = new ObservableCollection<AgencyModel>();
                ShowIsTongHop();
                LoadReportType();
                LoadDanhMucLoaiChi();
                IsSummary = false;
                IsSummaryAgency = false;
                IsDataInterpretation = false;
                LoadTieuDe();
                LoadDanhMuc();
                LoadBQuanLy();
                LoadAgencies();
                _isInitReport = false;
                IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
                LoadKieuGiay();
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        private void ShowIsTongHop()
        {
            if (SettlementTypeValue != (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN)
            {
                IshowLoaiBaoCao = false;
                OnPropertyChanged(nameof(IsShowTheoTongHop));
            }
            else
            {
                IshowLoaiBaoCao = true;
                OnPropertyChanged(nameof(IsShowTheoTongHop));
            }
        }

        private void LoadKieuGiay()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.DOC, DisplayItem = LoaiGiay.DOC });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG_A3, DisplayItem = LoaiGiay.NGANG_A3 });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();
        }

        #region Load data
        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                int yearOfWork = _sessionInfo.YearOfWork;
                List<DonVi> lstDonVis = new List<DonVi>();
                Guid IdLoaiChi = SelectedDanhMucLoaiChi.Id;
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    lstDonVis = _kinhPhiKhacService.FindByDonViForNamLamViec(yearOfWork, SettlementTypeLoaiChungTu.ChungTu, IdLoaiChi).ToList();
                    lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();

                }
                else
                {
                    lstDonVis = _kinhPhiKhacService.FindByDonViForNamLamViec(yearOfWork, SettlementTypeLoaiChungTu.ChungTuTongHop, IdLoaiChi).ToList();
                    if (!IsInTheoTongHop)
                        lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    else
                        lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
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
                foreach (AgencyModel model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected) && !_checkAllAgencies)
                        {
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                            OnPropertyChanged(nameof(IsSelectedAllAgency));
                            LoadLNS();
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

        private void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            DateTime dt = DateTime.Now;
            Guid IdLoaiChi = SelectedDanhMucLoaiChi.Id;
            List<BhDmMucLucNganSach> listMLNS = new List<BhDmMucLucNganSach>();
            string agencyIds = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (agencyIds.Length > 0)
            {
                listMLNS = _bhDmMucLucNganSachService.FindSLNSForQTCNKPK(yearOfWork, agencyIds
                                                                        , dt, _sessionInfo.Principal
                                                                        , IdLoaiChi).ToList();
                if (SettlementTypeValue != (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT)
                {
                    if (SelectedDanhMucLoaiChi.HiddenValue != LNSValue.LNS_9010006_9010007)
                    {
                        if (LoaiChi == LoaiChi.Loai_HSSV)
                            listMLNS = listMLNS.Where(x => x.SLNS == LNSValue.LNS_9050002).ToList();
                        else
                            listMLNS = listMLNS.Where(x => x.SLNS == LNSValue.LNS_9050001).ToList();
                    }
                }
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

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            CheckBoxItem item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.ValueItem.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
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

        private void LoadBQuanLy()
        {
            System.Linq.Expressions.Expression<Func<DmBQuanLy, bool>> predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            List<DmBQuanLy> listPhongBan = _phongBanService.FindByCondition(predicate);
            _bQuanLy = _mapper.Map<List<ComboboxItem>>(listPhongBan);
            if (_bQuanLy.Count() > 0)
            {
                _bQuanLy.Insert(0, new ComboboxItem("Tất cả", string.Empty));
                SelectedBQuanLy = _bQuanLy.First();
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            AgencyModel item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadTieuDe()
        {
            string sLNSSelected = SelectedDanhMucLoaiChi.HiddenValue;
            GetTitleDefault(sLNSSelected);

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            Title1 = !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa) ? _dmChuKy.TieuDe1MoTa : Title1Default;
            Title2 = !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa) ? _dmChuKy.TieuDe2MoTa : Title2Default;
            Title3 = !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa) ? _dmChuKy.TieuDe3MoTa : Title3Default;

        }

        private void GetTitleDefault(string sLNSSelected)
        {
            if (SettlementTypeValue == (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM)
            {
                if (sLNSSelected == LNSValue.LNS_9050001_9050002)
                {
                    if (LoaiChi == LoaiChi.Loai_HSSV)
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_HSSV_DONVI_PHULUC;
                        Title1Default = SettlementTitle.Title1QTCNKPKHSSVPhuLuc;
                        Title2Default = SettlementTitle.Title2QTCNKPKHSSVPhuLuc;
                        Title3Default = SettlementTitle.Title1QTCNTEMPLATEPhuLuc;
                    }
                    else
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_NLD_DONVI_PHULUC;
                        Title1Default = SettlementTitle.Title1QTCNKPKNLDPhuLuc;
                        Title2Default = SettlementTitle.Title2QTCNKPKNLDPhuLuc;
                        Title3Default = SettlementTitle.Title1QTCNTEMPLATEPhuLuc;

                    }

                }
                else if (sLNSSelected == LNSValue.LNS_9010008)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_KCBBHYTQN_DONVI_PHULUC;
                    Title1Default = SettlementTitle.Title1QTCNKCBBHYTQNPhuLuc;
                    Title2Default = SettlementTitle.Title2QTCNKCBBHYTQNPhuLuc;
                    Title3Default = SettlementTitle.Title1QTCNTEMPLATEPhuLuc;

                }
                else if (sLNSSelected == LNSValue.LNS_9010009)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_MSTTBYT_DONVI_PHULUC;
                    Title1Default = SettlementTitle.Title1QTCNMSTTBYTPhuLuc;
                    Title2Default = SettlementTitle.Title2QTCNMSTTBYTPhuLuc;
                    Title3Default = SettlementTitle.Title1QTCNTEMPLATEPhuLuc;

                }
                else if (sLNSSelected == LNSValue.LNS_9010010)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_CHTBHTN_DONVI_PHULUC;
                    Title1Default = SettlementTitle.Title1QTCNCHTBHTNPhuLuc;
                    Title2Default = SettlementTitle.Title2QTCNCHTBHTNPhuLuc;
                    Title3Default = SettlementTitle.Title1QTCNTEMPLATEPhuLuc;

                }
                else
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_TSDK_DONVI_PHULUC;
                    Title1Default = SettlementTitle.Title1QTCNKPKTSDKPhuLuc;
                    Title2Default = SettlementTitle.Title2QTCNKPKTSDKPhuLuc;
                    Title3Default = SettlementTitle.Title1QTCNTEMPLATEPhuLuc;

                }
            }
            else
            {
                if (sLNSSelected == LNSValue.LNS_9050001_9050002)
                {
                    if (LoaiChi == LoaiChi.Loai_HSSV)
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_HSSV_NLD_CHITIET;
                        Title1Default = SettlementTitle.Title1QTCNTEMPLATEKeHoach;
                        Title2Default = SettlementTitle.Title2QTCNKPKHSSVNLDKeHoach;
                        Title3Default = string.Empty;

                    }
                    else
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_HSSV_NLD_CHITIET;
                        Title1Default = SettlementTitle.Title1QTCNTEMPLATEKeHoach;
                        Title2Default = SettlementTitle.Title2QTCNKPKHSSVNLDKeHoach;
                        Title3Default = string.Empty;

                    }

                }
                else if (sLNSSelected == LNSValue.LNS_9010008)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_KCBBHYTQN_CHITIET;
                    Title1Default = SettlementTitle.Title1QTCNTEMPLATEKeHoach;
                    Title2Default = SettlementTitle.Title2QTCNKCBBHYTQNKeHoach;
                    Title3Default = string.Empty;

                }
                else if (sLNSSelected == LNSValue.LNS_9010009)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_STTBYT_CHITIET;
                    Title1Default = SettlementTitle.Title1QTCNTEMPLATEKeHoach;
                    Title2Default = SettlementTitle.Title2QTCNMSTTBYTKeHoach;
                    Title3Default = string.Empty;

                }
                else if (sLNSSelected == LNSValue.LNS_9010010)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_CHTBHTN_CHITIET;
                    Title1Default = SettlementTitle.Title1QTCNCHTBHTNKeHoach;
                    Title2Default = SettlementTitle.Title2QTCNCHTBHTNKeHoach;
                    Title3Default = SettlementTitle.Title3QTCNCHTBHTNKeHoach;

                }
                else
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_TSDK_CHITIET;
                    Title1Default = SettlementTitle.Title1QTCNTEMPLATEKeHoach;
                    Title2Default = SettlementTitle.Title2QTCNKPKTSDKKeHoach;
                    Title3Default = string.Empty;
                }
            }
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

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            listDanhMucLoaiChi = null;
            listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            listDanhMucLoaiChi = listDanhMucLoaiChi.Where(x => x.SLNS == LNSValue.LNS_9010006_9010007
                                                            || x.SLNS == LNSValue.LNS_9050001_9050002
                                                            || x.SLNS == LNSValue.LNS_9010008
                                                            || x.SLNS == LNSValue.LNS_9010009
                                                            || x.SLNS == LNSValue.LNS_9010010);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.SMaLoaiChi,
                    HiddenValue = n.SLNS,
                    Id = n.Id,
                }));

                SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
            }

            foreach (ComboboxItem model in ItemsDanhMucLoaiChi)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
            OnPropertyChanged(nameof(IsExportEnable));
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SGiaTri)
                .ToList();
            if (listDonViTinh.Count == 0)
                _units.Add(new ComboboxItem("Đồng", "1"));
            foreach (DanhMuc dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            OnPropertyChanged(nameof(Units));
            _selectedUnit = Units.ElementAt(0);

            DanhMuc danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            DanhMuc danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        
        #endregion

        #region Add Chu ky
        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuKy;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                Title1 = chuKy.TieuDe1MoTa;
                Title2 = chuKy.TieuDe2MoTa;
                Title3 = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
        #endregion

        #region Export
        private void ExportFile(bool isPdf)
        {
            int typePrint = SettlementTypeValue;
            switch (typePrint)
            {
                case (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM:
                    OnPrintReportPhucLuc(isPdf);
                    break;
                case (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN:
                    OnPrintReportKeHoach(isPdf);
                    break;
                default:
                    break;
            }
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy == null) return string.Empty;
            string loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            Dictionary<string, DanhMuc> danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

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
        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        private void OnPrintReportKeHoach(bool isPdf)
        {
            try
            {
                _ = BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    string fileName = GetFileName();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                    string templateFileName = string.Empty;
                    int yearOfWork = _sessionInfo.YearOfWork;

                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        foreach (AgencyModel donvi in Agencies.Where(n => n.Selected))
                        {
                            QtcNamKinhPhiKhacCriteria searchCondition = new QtcNamKinhPhiKhacCriteria();
                            searchCondition.NamLamViec = yearOfWork;
                            searchCondition.IDMaDonVi = donvi.Id;
                            searchCondition.SNguoiTao = _sessionInfo.Principal;
                            searchCondition.SLNS = SelectedDanhMucLoaiChi.HiddenValue;
                            searchCondition.IDLoaiChi = SelectedDanhMucLoaiChi.Id;
                            searchCondition.DonViTinh = donViTinh;
                            searchCondition.LoaiChungTu = !IsDonViRoot(donvi.Id) ? BhxhLoaiChungTu.BhxhChungTu : BhxhLoaiChungTu.BhxhChungTuTongHop;
                            int stt = 1;
                            double? SumFTienDuToanNamTruocChuyenSang = 0;
                            double? SumFTienDuToanGiaoNamNay = 0;
                            double? SumFTienThucChi = 0;
                            double? SumFTongDuToanDuocGiao = 0;
                            double? SumTienThua = 0;
                            double? SumFTienThieu = 0;
                            double? SumTile = 0;

                            _reportData = _kinhPhiKhacChiTietService.FindGetReportKeHoach(searchCondition).ToList();
                            //var lstDataKeHoach = _mapper.Map<List<BhQtcNamKinhPhiKhacChiTietModel>>(_reportData);
                            DonVi donViChild = _donViService.FindByIdDonVi(donvi.Id, yearOfWork);
                            CalculateData(_reportData);
                            List<BhQtcNamKinhPhiKhacChiTietQuery> lstDataKeHoach = _reportData.Where(x => x.IsHasData).ToList();
                            lstDataKeHoach.ForEach(x =>
                            {
                                x.STT = stt;

                                stt++;
                                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                                {
                                    x.FTien_TongDuToanDuocGiao = x.FDuToanNamTruocChuyenSang.GetValueOrDefault() + x.FTien_DuToanGiaoNamNay.GetValueOrDefault();
                                    x.FTienThua = x.FTien_TongDuToanDuocGiao.GetValueOrDefault() > x.FTien_ThucChi.GetValueOrDefault() ? x.FTien_TongDuToanDuocGiao.GetValueOrDefault() - x.FTien_ThucChi.GetValueOrDefault() : 0;
                                    x.FTienThieu = x.FTien_TongDuToanDuocGiao.GetValueOrDefault() < x.FTien_ThucChi.GetValueOrDefault() ? x.FTien_ThucChi.GetValueOrDefault() - x.FTien_TongDuToanDuocGiao.GetValueOrDefault() : 0;
                                    x.FTiLeThucHienTrenDuToan = x.FTien_TongDuToanDuocGiao.GetValueOrDefault() > 0 ? (x.FTien_ThucChi.GetValueOrDefault() / x.FTien_TongDuToanDuocGiao.GetValueOrDefault()) * 100 : 0;
                                }
                                else
                                {
                                    x.FTien_TongDuToanDuocGiao = x.FDuToanNamTruocChuyenSang.GetValueOrDefault() + x.FTien_DuToanGiaoNamNay.GetValueOrDefault();
                                }
                            });

                            SumFTienDuToanNamTruocChuyenSang = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FDuToanNamTruocChuyenSang);
                            SumFTienDuToanGiaoNamNay = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTien_DuToanGiaoNamNay);
                            SumFTienThucChi = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTien_ThucChi);
                            SumFTongDuToanDuocGiao = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTien_TongDuToanDuocGiao);
                            SumTienThua = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienThua);
                            SumFTienThieu = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienThieu);
                            SumTile = SumFTongDuToanDuocGiao > 0 ? (SumFTienThucChi / SumFTongDuToanDuocGiao) * 100 : 0;

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("FormatNumber", formatNumber);
                            data.Add("TieuDe1", Title1);
                            data.Add("TieuDe2", Title2);
                            data.Add("TieuDe3", Title3);
                            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                            data.Add("Cap2", !string.IsNullOrEmpty(sCap1) ? sCap1 : _sessionInfo.TenDonVi);
                            data.Add("SumFTienDuToanNamTruocChuyenSang", SumFTienDuToanNamTruocChuyenSang);
                            data.Add("SumFTienDuToanGiaoNamNay", SumFTienDuToanGiaoNamNay);
                            data.Add("SumFTienThucChi", SumFTienThucChi);
                            data.Add("SumFTongDuToanDuocGiao", SumFTongDuToanDuocGiao);
                            data.Add("SumFTienThieu", SumFTienThieu);
                            data.Add("SumTienThua", SumTienThua);
                            data.Add("SumTile", SumTile);
                            data.Add("DonVi", donvi.TenDonVi);
                            data.Add("YearWork", _sessionInfo.YearOfWork);
                            data.Add("ListData", lstDataKeHoach);
                            data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                            data.Add("TienBangChu", StringUtils.NumberToText(SumFTienThucChi.Value * donViTinh, true));

                            data.Add("Ngay", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(fileName));
                            ExcelFile xlsFile = _exportService.Export<BhQtcNamKinhPhiKhacChiTietQuery>(templateFileName, data);
                            //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                            string fileNameWithoutExtension = string.Format("rptBH_QTC_NKPQL_ChungTu_ChiTiet_{0}_{1}", donvi.Id, DateTime.Now.ToStringTimeStamp());
                            results.Add(new ExportResult(donvi.AgencyName, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    else
                    {
                        int Loai = BhxhLoaiChungTu.BhxhChungTu;
                        string lstIdDonViCheckd = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id).ToList());
                        if (IsInTheoTongHop)
                        {
                            Loai = BhxhLoaiChungTu.BhxhChungTuTongHop;
                            //var lstChungTu = _kinhPhiKhacService.FindIndex(yearOfWork).Where(x => x.IID_MaDonVi == lstIdDonViCheckd).FirstOrDefault();
                            //if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                            //{
                            //    var sSoChungTu = lstChungTu.STongHop.Split(",");
                            //    var lstChungTuChild = _kinhPhiKhacService.FindIndex(yearOfWork).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                            //    var lsdMaDonVi = lstChungTuChild.Select(x => x.IID_MaDonVi).Distinct().ToList();
                            //    lstIdDonViCheckd = string.Join(",", lsdMaDonVi);
                            //}
                        }

                        QtcNamKinhPhiKhacCriteria searchCondition = new QtcNamKinhPhiKhacCriteria();
                        searchCondition.NamLamViec = yearOfWork;
                        searchCondition.IDMaDonVi = lstIdDonViCheckd;
                        searchCondition.SNguoiTao = _sessionInfo.Principal;
                        searchCondition.SLNS = string.Join(",", SelectedDanhMucLoaiChi.HiddenValue);
                        searchCondition.IDLoaiChi = SelectedDanhMucLoaiChi.Id;
                        searchCondition.DonViTinh = donViTinh;
                        searchCondition.LoaiChungTu = Loai;
                        int stt = 1;
                        double? SumFTienDuToanNamTruocChuyenSang = 0;
                        double? SumFTienDuToanGiaoNamNay = 0;
                        double? SumFTienThucChi = 0;
                        double? SumFTongDuToanDuocGiao = 0;
                        double? SumTienThua = 0;
                        double? SumFTienThieu = 0;
                        double? SumTile = 0;

                        _reportData = _kinhPhiKhacChiTietService.FindGetReportKeHoach(searchCondition).ToList();
                        //var lstDataKeHoach = _mapper.Map<List<BhQtcNamKinhPhiKhacChiTietModel>>(_reportData);

                        DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                        CalculateData(_reportData);
                        List<BhQtcNamKinhPhiKhacChiTietQuery> lstDataKeHoach = _reportData.Where(x => x.IsHasData).ToList();
                        lstDataKeHoach.ForEach(x =>
                        {
                            x.STT = stt;

                            stt++;
                            if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                            {
                                x.FTien_TongDuToanDuocGiao = x.FTien_DuToanNamTruocChuyenSang.GetValueOrDefault() + x.FTien_DuToanGiaoNamNay.GetValueOrDefault();
                                x.FTienThua = x.FTien_TongDuToanDuocGiao.GetValueOrDefault() > x.FTien_ThucChi.GetValueOrDefault() ? x.FTien_TongDuToanDuocGiao.GetValueOrDefault() - x.FTien_ThucChi.GetValueOrDefault() : 0;
                                x.FTienThieu = x.FTien_TongDuToanDuocGiao.GetValueOrDefault() < x.FTien_ThucChi.GetValueOrDefault() ? x.FTien_ThucChi.GetValueOrDefault() - x.FTien_TongDuToanDuocGiao.GetValueOrDefault() : 0;
                                x.FTiLeThucHienTrenDuToan = x.FTien_TongDuToanDuocGiao.GetValueOrDefault() > 0 ? (x.FTien_ThucChi.GetValueOrDefault() / x.FTien_TongDuToanDuocGiao.GetValueOrDefault()) * 100 : 0;
                            }
                            else
                            {
                                x.FTien_TongDuToanDuocGiao = x.FTien_DuToanNamTruocChuyenSang.GetValueOrDefault() + x.FTien_DuToanGiaoNamNay.GetValueOrDefault();
                            }
                        });

                        SumFTienDuToanNamTruocChuyenSang = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTien_DuToanNamTruocChuyenSang);
                        SumFTienDuToanGiaoNamNay = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTien_DuToanGiaoNamNay);
                        SumFTienThucChi = lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTien_ThucChi);
                        SumFTongDuToanDuocGiao = SumFTienDuToanNamTruocChuyenSang + SumFTienDuToanGiaoNamNay;
                        SumTienThua = SumFTongDuToanDuocGiao > SumFTienThucChi ? SumFTongDuToanDuocGiao - SumFTienThucChi : 0;
                        SumFTienThieu = SumFTongDuToanDuocGiao < SumFTienThucChi ? SumFTienThucChi - SumFTongDuToanDuocGiao : 0;
                        SumTile = SumFTongDuToanDuocGiao > 0 ? (SumFTienThucChi / SumFTongDuToanDuocGiao) * 100 : 0;

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("YearWork", _sessionInfo.YearOfWork);
                        data.Add("SumFTienDuToanNamTruocChuyenSang", SumFTienDuToanNamTruocChuyenSang);
                        data.Add("SumFTienDuToanGiaoNamNay", SumFTienDuToanGiaoNamNay);
                        data.Add("SumFTienThucChi", SumFTienThucChi);
                        data.Add("SumFTongDuToanDuocGiao", SumFTongDuToanDuocGiao);
                        data.Add("SumFTienThieu", SumFTienThieu);
                        data.Add("SumTienThua", SumTienThua);
                        data.Add("SumTile", SumTile);
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("ListData", lstDataKeHoach);
                        data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                        data.Add("TienBangChu", StringUtils.NumberToText(SumFTienThucChi.Value * donViTinh, true));
                        data.Add("Ngay", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(fileName));
                        ExcelFile xlsFile = _exportService.Export<BhQtcNamKinhPhiKhacChiTietQuery>(templateFileName, data);
                        //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                        string fileNameWithoutExtension = string.Format("rptBH_QTC_NKPQL_ChungTu_ChiTiet_{0}_{1}", _sessionInfo.IdDonVi, DateTime.Now.ToStringTimeStamp());
                        results.Add(new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile));

                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
                    }
                    else
                        _logger.Error(e.Error.Message);
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateData(List<BhQtcNamKinhPhiKhacChiTietQuery> lstDataKeHoach)
        {
            try
            {
                lstDataKeHoach.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     //x.FTien_DuToanNamTruocChuyenSang = 0;
                     //x.FTien_DuToanGiaoNamNay = 0;
                     x.FTien_ThucChi = 0;
                 });

                List<BhQtcNamKinhPhiKhacChiTietQuery> temp = lstDataKeHoach.Where(x => !x.IsHangCha).ToList();
                Dictionary<Guid?, BhQtcNamKinhPhiKhacChiTietQuery> dictByMlns = lstDataKeHoach.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
                foreach (BhQtcNamKinhPhiKhacChiTietQuery item in temp)
                {
                    CalculateParent(item.IdParent, item, dictByMlns);
                }

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParent(Guid idParent, BhQtcNamKinhPhiKhacChiTietQuery item, Dictionary<Guid?, BhQtcNamKinhPhiKhacChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            BhQtcNamKinhPhiKhacChiTietQuery model = dictByMlns[idParent];
            //model.FTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
            model.FTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
            model.FTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;
            model.FTien_ThucChi += item.FTien_ThucChi;

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        private void OnPrintReportPhucLuc(bool isPdf)
        {
            try
            {
                string fileName = GetFileName();

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    DonVi donViCurrent = GetDonViOfCurrentUser();
                    List<ExportResult> results = new List<ExportResult>();
                    string listDonVi = string.Join(",", Agencies.Where(item => item.Selected).Select(x => x.Id).ToList());
                    string templateFileName;
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int stt = 1;
                    List<string> lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();

                    if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
                    {
                        string sLNS = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                        _listPhuLuc = _kinhPhiKhacChiTietService.FindGetReportPhuLuc(yearOfWork, listDonVi, sLNS).ToList();
                    }
                    else
                    {
                        _listPhuLuc = _kinhPhiKhacChiTietService.FindGetReportPhuLuc(yearOfWork, listDonVi, SelectedDanhMucLoaiChi.HiddenValue).ToList();
                    }

                    _listPhuLuc.ForEach(x =>
                    {
                        x.STT = stt;
                        //x.IsHangCha = false;
                        x.FTienDaThucHienNamTruoc /= donViTinh;
                        x.FTienNamNay /= donViTinh;
                        x.FTienQuyetToan /= donViTinh;
                        x.FTienCong /= donViTinh;
                        //x.FTienThua = x.FTienCong > x.FTienQuyetToan ? ((x.FTienCong - x.FTienQuyetToan)) : 0;
                        //x.FTienThieu = x.FTienQuyetToan > x.FTienCong ? ((x.FTienQuyetToan - x.FTienCong)) : 0;

                        stt++;

                    });

                    double sumFTienDaThucHienNamTruoc = _listPhuLuc.Sum(x => x.FTienDaThucHienNamTruoc);
                    double sumFTienNamNay = _listPhuLuc.Sum(x => x.FTienNamNay);
                    double sumFTienQuyetToan = _listPhuLuc.Sum(x => x.FTienQuyetToan);
                    double sumFTienThieu = _listPhuLuc.Sum(x => x.FTienThieu);
                    double sumFTienThua = _listPhuLuc.Sum(x => x.FTienThua);
                    double sumFTienCong = _listPhuLuc.Sum(x => x.FTienCong);
                    double TongSoTien = _listPhuLuc.Sum(x => x.FTienQuyetToan);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);

                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("DonViTinh", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", _listPhuLuc);
                    data.Add("sumFTienDaThucHienNamTruoc", sumFTienDaThucHienNamTruoc);
                    data.Add("sumFTienNamNay", sumFTienNamNay);
                    data.Add("sumFTienQuyetToan", sumFTienQuyetToan);
                    data.Add("sumFTienThieu", sumFTienThieu);
                    data.Add("sumFTienThua", sumFTienThua);
                    data.Add("sumFTienCong", sumFTienCong);
                    data.Add("TongSoTien", TongSoTien != 0 ? StringUtils.NumberToText(TongSoTien * donViTinh, true) : string.Empty);

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(fileName));
                    ExcelFile xlsFile = _exportService.Export<ReportBHQTCNKPKhacPhuLucQuery, DonViModel>(templateFileName, data);
                    //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                    string fileNameWithoutExtension = string.Format(fileName + "_{0}_{1}", donViCurrent.TenDonVi, DateTime.Now.ToStringTimeStamp());
                    results.Add(new ExportResult(donViCurrent.TenDonVi, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private DonVi GetDonViOfCurrentUser()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            string currentIdDonVi = _sessionService.Current.IdDonVi;
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            DonVi nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private string GetTemplate(string input)
        {
            input += "_Doc";

            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG_A3)
            {
                return Path.Combine(ExportPrefix.PATH_BH_QTC_NAMKPK, input + "_A3" + FileExtensionFormats.Xlsx);
            }
            else
            {
                return Path.Combine(ExportPrefix.PATH_BH_QTC_NAMKPK, input + FileExtensionFormats.Xlsx);
            }
        }

        private string GetFileName()
        {
            string sFileName = string.Empty;

            string sMaLoaiChi = SelectedDanhMucLoaiChi.ValueItem;

            int typePrint = SettlementTypeValue;
            if (typePrint == (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM)
            {
                sFileName = ExportFileName.RPT_BH_QTC_NKPK_CHUNGTU_DONVI_PHULUC_BHXH;
            }
            else
            {

                if (sMaLoaiChi == MaLoaiChiBHXH.SMABHTN)
                {
                    sFileName = ExportFileName.RPT_BH_QTC_NKPKBHTN_CHITIET;
                }
                else if (sMaLoaiChi == MaLoaiChiBHXH.SMAKCBTS)
                {
                    sFileName = ExportFileName.RPT_BH_QTC_NKPK_CHITIET;
                }
                else if (sMaLoaiChi == MaLoaiChiBHXH.SMAKCBBHYT)
                {
                    sFileName = ExportFileName.RPT_BH_QTC_NKPK_KCB_BHYT_CHITIET;
                }
                else if (sMaLoaiChi == MaLoaiChiBHXH.SMAHSSVNLD)
                {
                    sFileName = ExportFileName.RPT_BH_QTC_NKPK_HSSV_NLD_CHITIET;
                }
                else if (sMaLoaiChi == MaLoaiChiBHXH.SMAMSTTBYT)
                {
                    sFileName = ExportFileName.RPT_BH_QTC_NKPK_MSTTBYT_CHITIET;
                }
            }

            return sFileName;
        }
        #endregion
    }
}
