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
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Explanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Explanation;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.PrintReport
{
    public class PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel : ViewModelBase
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsDonViService _donViService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IBhQtcQuyKPKService _quyKCBService;
        private readonly IBhQtcQuyKPKChiTietService _quyKCBChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;
        private INsPhongBanService _phongBanService;
        private ICollectionView _listAgency;
        private ICollectionView _listLNSView;
        #endregion

        #region Property
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private List<BhQtcQuyKPKQuery> _listChungTu;
        private List<BhQtcQuyKPKQuery> _listChungTuDotCap;
        private BhQtcQuyKPKQuery _chungTuSelected;
        private SessionInfo _sessionInfo;
        private List<BhQtcQuyKPKChiTietQuery> _listChungTuChiTiet;
        private List<ReportBHQTCQKPKThongTriQuery> _reportData;
        private List<ReportBHQTCQKPKThongTriQuery> _reportDataKeHoach;
        List<BhQtcQuyKPKChiTietModel> lstDataKeHoach;
        private bool _isCapPhatToanDonVi;
        private string _cap1;
        private string _diaDiem;
        private string _typeChuKy;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private bool _isInitReport;
        private bool _checkAllAgencies;
        public int SettlementTypeValue { get; set; }
        public bool IsShowAll { get; set; }
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public string NamePrint { get; set; }
        public string DescriptionPrint { get; set; }
        public string SettlementPrintName
        {
            get
            {
                switch (SettlementTypeValue)
                {
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                        NamePrint = "BAO CÁO THÔNG TRI QUÝ CHI KINH PHÍ KHÁC";
                        _isShowLoaiBaoCao = false;
                        break;
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        NamePrint = "BÁO CÁO QUYẾT TOÁN CHI QUÝ KINH PHÍ KHÁC";
                        _isShowLoaiBaoCao = true;
                        break;
                    default:
                        break;
                }
                return NamePrint;
            }
        }
        public override Type ContentType => typeof(PrintQuyetToanChiKinhPhiQuanLyNotice);
        public override string Name => SettlementPrintName;
        public override string Description => SettlementPrintName;
        public override string Title => SettlementPrintName;
        public bool IsShowRadioLoaiHSSVNLD => _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002;
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();
        public bool InMotToChecked { get; set; }
        public bool IsEnableCheckBoxInMotTo => !IsEnableCheckBoxSummary;
        public bool IsShowLoiGiaiThich
        {
            get
            {
                bool _isShowLoiGiaiThich = true;
                int iTypePrint = SettlementTypeValue;
                switch (iTypePrint)
                {
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                        _isShowLoiGiaiThich = false;
                        break;
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                        _isShowLoiGiaiThich = false;
                        break;
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        _isShowLoiGiaiThich = true;
                        break;
                }
                return _isShowLoiGiaiThich;
            }
        }

        private bool _isShowLoaiBaoCao;
        public bool IsShowLoaiBaoCao
        {
            get => _isShowLoaiBaoCao;
            set => SetProperty(ref _isShowLoaiBaoCao, value);
        }
        public bool IsShowTheoTongHop => !IsShowLoaiBaoCao;

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

                if (_selectedReportType != null)
                {
                    LoadAgencies();
                    OnPropertyChanged(nameof(IsEnableCheckBoxInMotTo));
                }
            }
        }
        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn;

        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
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

                }
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
                OnPropertyChanged(nameof(IsExportEnable));
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
                if (SettlementTypeValue == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS || SettlementTypeValue == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY)
                {
                    return Agencies != null && Agencies.Where(x => x.Selected).Count() > 0;
                }
                else
                {
                    return Agencies != null && Agencies.Where(x => x.Selected).Count() > 0 && (_isCoverSheet || _isData || _isVerbalExplanation || _isDataInterpretation || _isCheckAll);
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
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuater;
        public ObservableCollection<ComboboxItem> CbxQuater
        {
            get => _cbxQuater;
            set => SetProperty(ref _cbxQuater, value);
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
                    if (SettlementTypeValue != (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS
                        || SettlementTypeValue != (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY)
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


        #endregion

        #region RelayCommand
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        #endregion

        #region viewModel
        public QuyetToanChiGiaiThichBangLoiQuyKhacViewModel QuyetToanChiGiaiThichBangLoiQuyKhacViewModel { get; set; }
        #endregion

        #region Constructor
        public PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel(
                    INsDonViService donViService,
                    IMapper mapper,
                    ISessionService sessionService,
                    ILog logger,
                    IDanhMucService danhMucService,
                    IExportService exportService,
                    IDmChuKyService dmChuKyService,
                    DmChuKyDialogViewModel dmChuKyDialogViewModel,
                    IBhQtcQuyKPKService bhQtcQuyKPKService,
                    IBhQtcQuyKPKChiTietService bhQtcQuyKPKChiTietService,
                     INsNguoiDungDonViService nguoiDungDonViService,
                    IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
                    IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                    INsPhongBanService phongBanService,
                    IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService,
                     QuyetToanChiGiaiThichBangLoiQuyKhacViewModel toanChiGiaiThichBangLoiQuyKhacViewModel)
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
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _quyKCBService = bhQtcQuyKPKService;
            _quyKCBChiTietService = bhQtcQuyKPKChiTietService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;

            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            _phongBanService = phongBanService;
            QuyetToanChiGiaiThichBangLoiQuyKhacViewModel = toanChiGiaiThichBangLoiQuyKhacViewModel;

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
                LoadQuater();
                _agencies = new ObservableCollection<AgencyModel>();
                LoadReportType();
                LoadDanhMucLoaiChi();
                IsSummary = false;
                IsSummaryAgency = false;
                IsDataInterpretation = false;

                LoadTieuDe();
                LoadDanhMuc();
                LoadBQuanLy();
                LoadAgencies();
                LoadKieuGiayIn();
                _isInitReport = false;
                IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                int yearOfWork = _sessionInfo.YearOfWork;
                int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
                Guid IdLoaiChi = SelectedDanhMucLoaiChi.Id;
                List<DonVi> lstAgencies = new List<DonVi>();
                if (SettlementTypeValue != (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT)
                {
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        lstAgencies = _quyKCBService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu, IdLoaiChi).ToList();
                        lstAgencies = lstAgencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                    else
                    {
                        lstAgencies = _quyKCBService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTuTongHop, IdLoaiChi).ToList();
                        if (!IsInTheoTongHop)
                            lstAgencies = lstAgencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                        else
                            lstAgencies = lstAgencies.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                    }
                }
                else
                {
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        lstAgencies = _quyKCBService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu, IdLoaiChi).ToList();
                        lstAgencies = lstAgencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                    else
                    {
                        lstAgencies = _quyKCBService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTuTongHop, IdLoaiChi).ToList();
                        if (IsSummary)
                        {
                            lstAgencies = lstAgencies.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                        }
                        else if (IsSummaryAgency)
                        {
                            lstAgencies = lstAgencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                        }
                        else if (IsSummary && IsSummaryAgency)
                        {
                            lstAgencies = lstAgencies.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                        }
                        else
                        {
                            lstAgencies = lstAgencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                        }
                    }
                }
                lstAgencies = lstAgencies.OrderBy(x => x.IIDMaDonVi).ToList();
                e.Result = lstAgencies;
            }, (s, e) =>
            {
                if (e.Result != null)
                {
                    List<DonVi> lstAgencies = (List<DonVi>)e.Result;
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(lstAgencies);
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
                            LoadLNS();
                        }
                    };
                }

                OnPropertyChanged(nameof(IsShowLoiGiaiThich));
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

        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
                //new ComboboxItem { DisplayItem = "Theo loại chi", ValueItem = SummaryLNSReportType.Type.ToString() }
            };
            _selectedReportType = _reportTypes.First();
        }

        private void LoadTieuDe()
        {
            int iTypePrint = SettlementTypeValue;
            switch (iTypePrint)
            {
                case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                    GetTypeChuKyThongTri();
                    break;
                case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                    GetTypeChuKyKeHoach();
                    break;
            }

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
            {
                Title1 = _dmChuKy.TieuDe1MoTa;
            }
            else
            {
                switch (iTypePrint)
                {
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                        Title1 = SettlementTitle.Title1QTCQKTT;
                        break;
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        Title1 = SettlementTitle.Title1QTCQKKeHoach;
                        break;
                }
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
            {
                Title2 = _dmChuKy.TieuDe2MoTa;
            }
            else
            {
                switch (iTypePrint)
                {
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                        Title2 = GetTitle2ThongTri();
                        break;
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        Title2 = GetTitle2KeHoach();
                        break;
                }
            }

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.Ten6MoTa))
                NoiDungChi = _dmChuKy.Ten6MoTa;
            else
                NoiDungChi = SelectedDanhMucLoaiChi.DisplayItem;
        }

        private string GetTitle2ThongTri()
        {
            string sTitle2 = string.Empty;
            if (SelectedDanhMucLoaiChi != null)
            {
                string sLNS = SelectedDanhMucLoaiChi.HiddenValue;
                // TSDK
                if (sLNS == LNSValue.LNS_9010006_9010007)
                {
                    sTitle2 = SettlementTitle.Title2QTCQKTSTT;
                }
                // HSSV-NLD
                else if (sLNS == LNSValue.LNS_9050001_9050002)
                {
                    if (LoaiChi == LoaiChi.Loai_HSSV)
                    {
                        sTitle2 = SettlementTitle.Title2QTCQKHSSVTT;
                    }
                    else
                    {
                        sTitle2 = SettlementTitle.Title2QTCQKNLDTT;
                    }
                }
                // Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
                else if (sLNS == LNSValue.LNS_9010008)
                {
                    sTitle2 = SettlementTitle.Title2QTCQKNKDQKCBBHYTQNTT;
                }
                // Kinh phí mua sắm trang thiết bị y tế
                else if (sLNS == LNSValue.LNS_9010009)
                {
                    sTitle2 = SettlementTitle.Title2QTCQKMSTT;
                }
                //  Chi hỗ trợ BHTN
                else
                {
                    sTitle2 = SettlementTitle.Title2QTCQKHTBHTNTT;
                }
            }
            return sTitle2;
        }

        public string GetTitle2KeHoach()
        {
            string sTitle2 = string.Empty;
            if (SelectedDanhMucLoaiChi != null)
            {
                string sLNS = SelectedDanhMucLoaiChi.HiddenValue;
                // TSDK
                if (sLNS == LNSValue.LNS_9010006_9010007)
                {
                    sTitle2 = SettlementTitle.Title2QTCQKTSDKKeHoach;
                }
                // HSSV-NLD
                else if (sLNS == LNSValue.LNS_9050001_9050002)
                {
                    if (LoaiChi == LoaiChi.Loai_HSSV)
                    {
                        sTitle2 = SettlementTitle.Title2QTCQKHSSVKeHoach;
                    }
                    else
                    {
                        sTitle2 = SettlementTitle.Title2QTCQKNLDKeHoach;
                    }
                }
                // Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
                else if (sLNS == LNSValue.LNS_9010008)
                {
                    sTitle2 = SettlementTitle.Title2QTCQKKCBQNKeHoach;
                }
                // Kinh phí mua sắm trang thiết bị y tế
                else if (sLNS == LNSValue.LNS_9010009)
                {
                    sTitle2 = SettlementTitle.Title2QTCQKMSTTBYTKeHoach;
                }
                //  Chi hỗ trợ BHTN
                else
                {
                    sTitle2 = SettlementTitle.Title2QTCQKHTBHTNKeHoach;
                }
            }

            return sTitle2;
        }

        private void GetTypeChuKyThongTri()
        {
            if (SelectedDanhMucLoaiChi != null)
            {
                int iTypyeChuKy = SettlementTypeValue;
                string sLNS = SelectedDanhMucLoaiChi.HiddenValue;
                if (iTypyeChuKy == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS)
                {
                    // TSDK
                    if (sLNS == LNSValue.LNS_9010006_9010007)
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI2;
                    }
                    // Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
                    else if (sLNS == LNSValue.LNS_9050001_9050002)
                    {
                        // HSSV
                        if (LoaiChi == LoaiChi.Loai_HSSV)
                        {
                            _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI2;
                        }
                        // NLD
                        else
                        {
                            _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_NLD_THONGTRILOAI2;
                        }
                    }
                    else if (sLNS == LNSValue.LNS_9010008)
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_TNKDQKCBBHYTQNTT_THONGTRILOAI2;
                    }
                    // Kinh phí mua sắm trang thiết bị y tế
                    else if (sLNS == LNSValue.LNS_9010009)
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_MSTT_THONGTRILOAI2;
                    }
                    //  Chi hỗ trợ BHTN
                    else
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_HTBHTNTT_THONGTRILOAI2;
                    }
                }

                if (iTypyeChuKy == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY)
                {
                    // TSDK
                    if (sLNS == LNSValue.LNS_9010006_9010007)
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI1;
                    }
                    else if (sLNS == LNSValue.LNS_9050001_9050002)
                    {
                        // HSSV
                        if (LoaiChi == LoaiChi.Loai_HSSV)
                        {
                            _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI1;
                        }
                        // NLD
                        else
                        {
                            _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_NLD_THONGTRILOAI1;
                        }
                    }
                    // Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
                    else if (sLNS == LNSValue.LNS_9010008)
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_TNKDQKCBBHYTQNTT_THONGTRILOAI1;
                    }
                    // Kinh phí mua sắm trang thiết bị y tế
                    else if (sLNS == LNSValue.LNS_9010009)
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_MSTT_THONGTRILOAI1;
                    }
                    //  Chi hỗ trợ BHTN
                    else
                    {
                        _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_HTBHTNTT_THONGTRILOAI1;
                    }
                }
            }
        }

        public void GetTypeChuKyKeHoach()
        {
            if (SelectedDanhMucLoaiChi != null)
            {
                string sLNS = SelectedDanhMucLoaiChi.HiddenValue;
                // TSDK
                if (sLNS == LNSValue.LNS_9010006_9010007)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_TSDK_KEHOACH;
                }
                // Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
                else if (sLNS == LNSValue.LNS_9010008)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_TNKDQKCBBHYTQNTT_KEHOACH;
                }
                // Kinh phí mua sắm trang thiết bị y tế
                else if (sLNS == LNSValue.LNS_9010009)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_TNKDQKCBBHYTQNTT_KEHOACH;
                }
                //  Chi hỗ trợ BHTN
                else if (sLNS == LNSValue.LNS_9010010)
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_HTBHTNTT_KEHOACH;
                }
                // HSSV-NLD
                else
                {
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPK_HSSV_NLD_KEHOACH;
                }
            }
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

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            listDanhMucLoaiChi = listDanhMucLoaiChi.Where(x => x.SLNS == LNSValue.LNS_9010006_9010007
                                                                    || x.SLNS == LNSValue.LNS_9010008
                                                                    || x.SLNS == LNSValue.LNS_9010009
                                                                     || x.SLNS == LNSValue.LNS_9010010
                                                                     || x.SLNS == LNSValue.LNS_9050001_9050002);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.SMaLoaiChi.ToString(),
                    HiddenValue = n.SLNS,
                    Id = n.Id,
                }));

                SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
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
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionInfo.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        #endregion

        private void OnOpenVerbalExplanationDialog()
        {
            var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (!lstDonViChecked.Any())
            {
                MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                return;
            }
            var lstChungTu = _quyKCBService.FindByCondition(x => x.INamChungTu == _sessionInfo.YearOfWork && x.IQuyChungTu == int.Parse(_cbxQuaterSelected.ValueItem) && x.IID_MaDonVi == lstDonViChecked);
            var BhQtcqKPKModel = _mapper.Map<ObservableCollection<BhQtcQuyKPKModel>>(lstChungTu);
            QuyetToanChiGiaiThichBangLoiQuyKhacViewModel.BhQtcQuyKPKModel = BhQtcqKPKModel.FirstOrDefault();
            QuyetToanChiGiaiThichBangLoiQuyKhacViewModel.Init();
            var view = new VerbalExplanation { DataContext = QuyetToanChiGiaiThichBangLoiQuyKhacViewModel };
            view.ShowDialog();
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
            DateTime dt = DateTime.Now;
            Guid IdLoaiChi = SelectedDanhMucLoaiChi.Id;
            List<BhDmMucLucNganSach> listMLNS = new List<BhDmMucLucNganSach>();
            string agencyIds = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (agencyIds.Length > 0)
            {
                listMLNS = _bhDmMucLucNganSachService.FindSLNSForQTCQKPK(yearOfWork, agencyIds
                                                                        , iQuy, dt, _sessionInfo.Principal
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
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.ValueItem.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
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
            DmChuKyDialogViewModel.IsShowNoiDungChi = (SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString());
            DmChuKyDialogViewModel.HasAddedSign4 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign5 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign6 = IsVerbalExplanation;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void ExportFile(bool isPdf)
        {
            int iPrintType = SettlementTypeValue;
            switch (iPrintType)
            {
                case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    ExportThongTriTheoLNS(isPdf);
                    break;
                case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                    ExportKeHoachQuyetToan(isPdf);
                    break;
                default:
                    break;
            }
        }

        private void ExportKeHoachQuyetToan(bool isPdf)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                if (IsCoverSheet)
                {
                    var dataResult = OnExportCoverSheet();
                    if (dataResult != null)
                        results.Add(dataResult);
                }

                if (IsData || IsVerbalExplanation || IsDataInterpretation)
                {
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(yearOfWork).ToList();
                    var iDDmLoaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(SettlementTypeSLNS.SLNS)).Select(x => x.Id).FirstOrDefault();
                    int iLoaiChungTu = SettlementTypeLoaiChungTu.ChungTuTongHop;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        iLoaiChungTu = SettlementTypeLoaiChungTu.ChungTu;
                        foreach (var agency in Agencies.Where(n => n.Selected))
                        {
                            if (IsData)
                            {
                                string sFileName = GetFileNameIsDataReport();
                                results.AddRange(ProcessFile(yearOfWork, donViTinh, isPdf, agency, sFileName, SelectedDanhMucLoaiChi.Id, iLoaiChungTu));
                            }

                            if (IsVerbalExplanation)
                            {
                                string sFileName = GetFileNameIsVerbalExplanationReport();
                                results.AddRange(ProcessFile(yearOfWork, donViTinh, isPdf, agency, sFileName, SelectedDanhMucLoaiChi.Id, iLoaiChungTu));
                            }
                        }
                    }
                    else
                    {
                        var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                        iLoaiChungTu = IsSummary ? SettlementTypeLoaiChungTu.ChungTuTongHop : SettlementTypeLoaiChungTu.ChungTu;
                        if (IsData)
                        {
                            string sFileName = GetFileNameIsDataReport();
                            results.AddRange(ProcessFileForDonVi(yearOfWork, donViTinh, isPdf, lstDonViChecked, sFileName, SelectedDanhMucLoaiChi.Id, iLoaiChungTu));
                        }

                        if (IsVerbalExplanation)
                        {
                            string sFileName = GetFileNameIsVerbalExplanationReport();
                            results.AddRange(ProcessFileForDonVi(yearOfWork, donViTinh, isPdf, lstDonViChecked, sFileName, SelectedDanhMucLoaiChi.Id, iLoaiChungTu));
                        }
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
                    _logger.Error(e.Error.Message);
                IsLoading = false;
            });
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

        private List<ExportResult> ProcessFileForDonVi(int yearOfWork, int donViTinh, bool isPdf, string lstDonViChecked, string sFileName, Guid iDDmLoaiChi, int iLoaiChungTu)
        {
            List<ExportResult> results = new List<ExportResult>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);

            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
            string templateFileName = string.Empty;
            double? tongTien = 0;
            double? SumFTienDuToanChuyenSang = 0;
            double? SumFTienDuToanDuocGiao = 0;
            double? SumFTongTien = 0;
            double? SumFTienThucChi = 0;
            double? SumFTienQuyetToanDaDuyet = 0;
            double? SumFTienDeNghiQuyetToanQuyNay = 0;
            double? SumFTienXacNhanQuyetToanQuyNay = 0;

            BhQtcQBHXHChiTietGiaiThichQuery lstGiaiThichBangLoi = new BhQtcQBHXHChiTietGiaiThichQuery();
            if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummary)
            {
                lstGiaiThichBangLoi = _qtcQBHXHChiTietGiaiThichService.GetGiaiThichBangLoiTheoDonVi(yearOfWork, lstDonViChecked, int.Parse(CbxQuaterSelected.ValueItem), SelectedDanhMucLoaiChi.ValueItem).FirstOrDefault();
            }

            QtcQuyKCriteria searchCondition = new QtcQuyKCriteria();
            searchCondition.NamLamViec = yearOfWork;
            searchCondition.IDMaDonVi = lstDonViChecked;
            searchCondition.SNguoiTao = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = iLoaiChungTu;
            searchCondition.SLNS = SelectedDanhMucLoaiChi.HiddenValue;
            searchCondition.DNgayChungTu = DateTime.Now;
            searchCondition.IDLoaiChi = iDDmLoaiChi;
            searchCondition.IQuyChungTu = int.Parse(CbxQuaterSelected.ValueItem);
            searchCondition.DonViTinh = donViTinh;

            _reportDataKeHoach = _quyKCBChiTietService.GetDataReportKeHoach(searchCondition).ToList();


            if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
            {
                var SDSLNS = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                _reportDataKeHoach = _reportDataKeHoach.Where(x => x.SLNS == SDSLNS || x.SLNS == LNSValue.LNS_905).ToList();
            }

            CalculateData();
            FormatDisplay();
            tongTien = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienXacNhanQuyetToanQuyNay);
            SumFTienDuToanChuyenSang = _reportDataKeHoach.Where(x => x.IsHangCha).Sum(x => x.FTien_DuToanNamTruocChuyenSang);
            SumFTienDuToanDuocGiao = _reportDataKeHoach.FirstOrDefault().FTien_DuToanGiaoNamNay;
            SumFTongTien = SumFTienDuToanChuyenSang + SumFTienDuToanDuocGiao;
            SumFTienThucChi = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienThucChi);
            SumFTienQuyetToanDaDuyet = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienQuyetToanDaDuyet);
            SumFTienDeNghiQuyetToanQuyNay = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienDeNghiQuyetToanQuyNay);
            SumFTienXacNhanQuyetToanQuyNay = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienXacNhanQuyetToanQuyNay);

            _reportDataKeHoach.ForEach(x =>
            {
                x.FTien_TongDuToanDuocGiao = x.FTien_DuToanNamTruocChuyenSang + x.FTien_DuToanGiaoNamNay;
            });
            _reportDataKeHoach = _reportDataKeHoach.Where(x => x.IsNotData).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("FormatNumber", formatNumber);
            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);

            data.Add("SumFTienDuToanChuyenSang", SumFTienDuToanChuyenSang);
            data.Add("SumFTongTien", SumFTongTien);
            data.Add("SumFTienDuToanDuocGiao", SumFTienDuToanDuocGiao);
            data.Add("SumFTienThucChi", SumFTienThucChi);
            data.Add("SumFTienQuyetToanDaDuyet", SumFTienQuyetToanDaDuyet);
            data.Add("SumFTienDeNghiQuyetToanQuyNay", SumFTienDeNghiQuyetToanQuyNay);
            data.Add("SumFTienXacNhanQuyetToanQuyNay", SumFTienXacNhanQuyetToanQuyNay);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("Quy", CbxQuaterSelected.DisplayItem);
            data.Add("YearWork", _sessionInfo.YearOfWork);
            data.Add("DonVi", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
            //data.Add("Ve", string.Format("quý {0} năm {1}", QuartersSelected.ValueItem, yearOfWork));
            data.Add("ListData", _reportDataKeHoach);
            data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
            data.Add("TienBangChu", StringUtils.NumberToText(SumFTienDeNghiQuyetToanQuyNay.Value * donViTinh, true));
            data.Add("Header2", string.Empty);
            data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("IsAggregate", true);

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
                data.Add("MoTaTinhHinh", lstGiaiThichBangLoi?.SMoTa_TinhHinh);
                data.Add("MoTaKienNghi", lstGiaiThichBangLoi?.SMoTa_KienNghi);
            }

            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
            ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPKThongTriQuery>(templateFileName, data);
            //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
            string fileNameWithoutExtension = string.Format(sFileName + "_{0}_{1}", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi), DateTime.Now.ToStringTimeStamp());
            results.Add(new ExportResult(!string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi), fileNameWithoutExtension, null, xlsFile));
            return results;
        }

        private List<ExportResult> ProcessFile(int yearOfWork, int donViTinh, bool isPdf, AgencyModel agency, string sFileName, Guid iDDmLoaiChi, int iLoaiChungTu)
        {
            List<ExportResult> results = new List<ExportResult>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
            DonVi donViChild = _donViService.FindByIdDonVi(agency.Id, yearOfWork);
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            string templateFileName = string.Empty;
            double? tongTien = 0;
            double? SumFTienDuToanChuyenSang = 0;
            double? SumFTienDuToanDuocGiao = 0;
            double? SumFTongTien = 0;
            double? SumFTienThucChi = 0;
            double? SumFTienQuyetToanDaDuyet = 0;
            double? SumFTienDeNghiQuyetToanQuyNay = 0;
            double? SumFTienXacNhanQuyetToanQuyNay = 0;

            BhQtcQBHXHChiTietGiaiThichQuery lstGiaiThichBangLoi = new BhQtcQBHXHChiTietGiaiThichQuery();

            lstGiaiThichBangLoi = _qtcQBHXHChiTietGiaiThichService.GetGiaiThichBangLoiTheoDonVi(yearOfWork, agency.Id, int.Parse(CbxQuaterSelected.ValueItem), SelectedDanhMucLoaiChi.ValueItem).FirstOrDefault();

            QtcQuyKCriteria searchCondition = new QtcQuyKCriteria();
            searchCondition.NamLamViec = yearOfWork;
            searchCondition.IDMaDonVi = agency.Id;
            searchCondition.SNguoiTao = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = iLoaiChungTu;
            searchCondition.SLNS = SelectedDanhMucLoaiChi.HiddenValue;
            searchCondition.DNgayChungTu = DateTime.Now;
            searchCondition.IDLoaiChi = iDDmLoaiChi;
            searchCondition.IQuyChungTu = int.Parse(CbxQuaterSelected.ValueItem);
            searchCondition.DonViTinh = donViTinh;

            _reportDataKeHoach = _quyKCBChiTietService.GetDataReportKeHoach(searchCondition).ToList();

            if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
            {
                string SLNS = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                _reportDataKeHoach = _reportDataKeHoach.Where(x => x.SLNS == SLNS || x.SLNS == LNSValue.LNS_905).ToList();
            }

            CalculateData();
            FormatDisplay();

            _reportDataKeHoach.ForEach(x =>
            {
                x.FTien_TongDuToanDuocGiao = x.FTien_DuToanGiaoNamNay + x.FTien_DuToanNamTruocChuyenSang;
            });
            tongTien = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienXacNhanQuyetToanQuyNay);
            SumFTienDuToanChuyenSang = _reportDataKeHoach.Where(x => x.IsHangCha).Sum(x => x.FTien_DuToanNamTruocChuyenSang);
            //SumFTienDuToanChuyenSang = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTien_DuToanNamTruocChuyenSang);
            SumFTienDuToanDuocGiao = _reportDataKeHoach.Where(x => x.IsHangCha).Sum(x => x.FTien_DuToanGiaoNamNay);
            //SumFTienDuToanDuocGiao = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTien_DuToanGiaoNamNay);
            SumFTongTien = SumFTienDuToanChuyenSang + SumFTienDuToanDuocGiao;
            SumFTienThucChi = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienThucChi);
            SumFTienQuyetToanDaDuyet = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienQuyetToanDaDuyet);
            SumFTienDeNghiQuyetToanQuyNay = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienDeNghiQuyetToanQuyNay);
            SumFTienXacNhanQuyetToanQuyNay = _reportDataKeHoach.Where(x => !x.IsHangCha).Sum(x => x.FTienXacNhanQuyetToanQuyNay);


            _reportDataKeHoach = _reportDataKeHoach.Where(x => x.IsNotData).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("FormatNumber", formatNumber);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
            data.Add("SumFTienDuToanChuyenSang", SumFTienDuToanChuyenSang);
            data.Add("SumFTongTien", SumFTongTien);
            data.Add("SumFTienDuToanDuocGiao", SumFTienDuToanDuocGiao);
            data.Add("SumFTienThucChi", SumFTienThucChi);
            data.Add("SumFTienQuyetToanDaDuyet", SumFTienQuyetToanDaDuyet);
            data.Add("SumFTienDeNghiQuyetToanQuyNay", SumFTienDeNghiQuyetToanQuyNay);
            data.Add("SumFTienXacNhanQuyetToanQuyNay", SumFTienXacNhanQuyetToanQuyNay);
            data.Add("Quy", CbxQuaterSelected.DisplayItem);
            data.Add("YearWork", _sessionInfo.YearOfWork);
            data.Add("DonVi", donViChild.TenDonVi);
            //data.Add("Ve", string.Format("quý {0} năm {1}", QuartersSelected.ValueItem, yearOfWork));
            data.Add("ListData", _reportDataKeHoach);
            data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
            data.Add("Header2", string.Empty);
            data.Add("TienBangChu", StringUtils.NumberToText(SumFTienDeNghiQuyetToanQuyNay.Value * donViTinh, true));
            data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("IsAggregate", false);

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
                data.Add("MoTaTinhHinh", lstGiaiThichBangLoi?.SMoTa_TinhHinh);
                data.Add("MoTaKienNghi", lstGiaiThichBangLoi?.SMoTa_KienNghi);
            }

            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
            ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPKThongTriQuery>(templateFileName, data);
            //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
            string fileNameWithoutExtension = string.Format(sFileName + "_{0}_{1}", agency.MaTenDonVi, DateTime.Now.ToStringTimeStamp());
            results.Add(new ExportResult(agency.AgencyName, fileNameWithoutExtension, null, xlsFile));
            return results;
        }

        private void ExportThongTriTheoLNS(bool isPdf)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                if (IsCoverSheet)
                {
                    var dataResult = OnExportCoverSheet();
                    if (dataResult != null)
                        results.Add(dataResult);
                }

                string sCap1 = GetLevelTitle(_dmChuKy, 1);
                string sCap2 = GetLevelTitle(_dmChuKy, 2);
                int donViTinh = int.Parse(SelectedUnit.ValueItem);
                FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                string lstLns = SelectedDanhMucLoaiChi.HiddenValue;
                string templateFileName = string.Empty;
                var yearOfWork = _sessionInfo.YearOfWork;
                string sFileName = GetFileNameReport();
                var donViCurrent = GetDonViOfCurrentUser();
                int Stt = 1;
                int iLoaiChungTu = SettlementTypeLoaiChungTu.ChungTuTongHop;
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    iLoaiChungTu = SettlementTypeLoaiChungTu.ChungTu;
                    foreach (var donvi in Agencies.Where(n => n.Selected))
                    {
                        DonVi donViChild = _donViService.FindByIdDonVi(donvi.Id, yearOfWork);
                        DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                        double? tongTien = 0;

                        if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
                        {
                            _reportData = _quyKCBChiTietService.GetDataReportQtcQuyKPKThongTri2(yearOfWork, CbxQuaterSelected.ValueItem, donvi.Id, lstLns, _sessionInfo.Principal, SelectedDanhMucLoaiChi.Id, iLoaiChungTu, donViTinh).ToList();
                            string SLNS = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                            _reportData = _reportData.Where(x => x.SLNS == SLNS).ToList();
                        }
                        else
                        {
                            _reportData = _quyKCBChiTietService.GetDataReportQtcQuyKPKThongTri2(yearOfWork, CbxQuaterSelected.ValueItem, donvi.Id, lstLns, _sessionInfo.Principal, SelectedDanhMucLoaiChi.Id, iLoaiChungTu, donViTinh).ToList();
                        }


                        tongTien = _reportData.Sum(x => x.FTienDeNghiQuyetToanQuyNay);

                        if (SelectedDanhMucLoaiChi.HiddenValue != LNSValue.LNS_9050001_9050002)
                        {
                            CalculateData();
                            FormatDisplay();
                            tongTien = _reportData.Where(x => string.IsNullOrEmpty(x.SM) && !string.IsNullOrEmpty(x.SLNS)).Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                        }

                        _reportData.ForEach(x =>
                        {
                            x.SLNS = string.Empty;
                            x.SL = string.Empty;
                            x.SK = string.Empty;
                            x.SM = string.Empty;
                            x.STM = string.Empty;
                            x.STTM = string.Empty;
                            x.SNG = string.Empty;

                        });

                        _reportData = _reportData.Where(_x => _x.FTienDeNghiQuyetToanQuyNay != 0).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("Nam", DateTime.Now.Year.ToString());
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("DonVi", donViChild != null ? donViChild.TenDonVi : string.Empty);
                        data.Add("Ve", string.Format("quý {0} năm {1}", CbxQuaterSelected.ValueItem, yearOfWork));
                        data.Add("TongChiTieu", tongTien);
                        data.Add("Items", _reportData);
                        data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                        data.Add("TienBangChu", StringUtils.NumberToText(tongTien.Value * donViTinh, true));
                        data.Add("GhiChu", string.Empty);
                        data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
                        ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPKThongTriQuery>(templateFileName, data);
                        //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                        string fileNameWithoutExtension = string.Format(sFileName + "_{0}_{1}", donvi.Id, DateTime.Now.ToStringTimeStamp());
                        results.Add(new ExportResult(donvi.MaTenDonVi, fileNameWithoutExtension, null, xlsFile));
                    }
                }
                else if (SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString())
                {
                    iLoaiChungTu = SettlementTypeLoaiChungTu.ChungTu;
                    foreach (var donvi in Agencies.Where(n => n.Selected))
                    {
                        DonVi donViChild = _donViService.FindByIdDonVi(donvi.Id, yearOfWork);
                        DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                        double? tongTien = 0;

                        if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002)
                        {
                            _reportData = _quyKCBChiTietService.GetDataReportQtcQuyKPKThongTri2(yearOfWork, CbxQuaterSelected.ValueItem, donvi.Id, lstLns, _sessionInfo.Principal, SelectedDanhMucLoaiChi.Id, iLoaiChungTu, donViTinh).ToList();
                            string SLNS = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                            _reportData = _reportData.Where(x => x.SLNS == SLNS).ToList();
                        }
                        else
                        {
                            _reportData = _quyKCBChiTietService.GetDataReportQtcQuyKPKThongTri2(yearOfWork, CbxQuaterSelected.ValueItem, donvi.Id, lstLns, _sessionInfo.Principal, SelectedDanhMucLoaiChi.Id, iLoaiChungTu, donViTinh).ToList();
                        }

                        _reportData = new List<ReportBHQTCQKPKThongTriQuery>()
                        {
                            new ReportBHQTCQKPKThongTriQuery()
                            {
                                FTienDeNghiQuyetToanQuyNay = _reportData.Sum(x => x.FTienDeNghiQuyetToanQuyNay),
                                SMoTa = NoiDungChi
                            }
                        };

                        tongTien = _reportData.Sum(x => x.FTienDeNghiQuyetToanQuyNay);

                        _reportData.ForEach(x =>
                        {
                            x.SLNS = string.Empty;
                            x.SL = string.Empty;
                            x.SK = string.Empty;
                            x.SM = string.Empty;
                            x.STM = string.Empty;
                            x.STTM = string.Empty;
                            x.SNG = string.Empty;

                        });

                        _reportData = _reportData.Where(_x => _x.FTienDeNghiQuyetToanQuyNay != 0).ToList();

                        AddEmptyItems(_reportData);

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("Nam", DateTime.Now.Year.ToString());
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("DonVi", donViChild != null ? donViChild.TenDonVi : string.Empty);
                        data.Add("Ve", string.Format("quý {0} năm {1}", CbxQuaterSelected.ValueItem, yearOfWork));
                        data.Add("TongChiTieu", tongTien);
                        data.Add("Items", _reportData);
                        data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                        data.Add("TienBangChu", StringUtils.NumberToText(tongTien.Value * donViTinh, true));
                        data.Add("GhiChu", string.Empty);
                        data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
                        ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPKThongTriQuery>(templateFileName, data);
                        //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                        string fileNameWithoutExtension = string.Format(sFileName + "_{0}_{1}", donvi.Id, DateTime.Now.ToStringTimeStamp());
                        results.Add(new ExportResult(donvi.MaTenDonVi, fileNameWithoutExtension, null, xlsFile));
                    }
                }
                else
                {
                    var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                    if (IsInTheoTongHop)
                    {
                        var lstChungTu = _quyKCBService.FindIndex(yearOfWork).Where(x => x.IID_MaDonVi == lstDonViChecked).FirstOrDefault();
                        if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                        {
                            var sSoChungTu = lstChungTu.STongHop.Split(",");
                            var lstChungTuChild = _quyKCBService.FindIndex(yearOfWork).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                            var lsdMaDonVi = lstChungTuChild.Select(x => x.IID_MaDonVi).Distinct().ToList();
                            lstDonViChecked = string.Join(",", lsdMaDonVi);
                        }
                    }

                    string sLNSSelected = SelectedDanhMucLoaiChi.HiddenValue;

                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        iLoaiChungTu = SettlementTypeLoaiChungTu.ChungTu;
                    }
                    DonVi donViChild = _donViService.FindByIdDonVi(donViCurrent.IIDMaDonVi, yearOfWork);


                    _reportData = _quyKCBChiTietService.GetDataReportQtcQuyKPKThongTri1(yearOfWork, CbxQuaterSelected.ValueItem, lstDonViChecked,
                                                                                        _sessionInfo.Principal, iLoaiChungTu,
                                                                                        SelectedDanhMucLoaiChi.Id, donViTinh).ToList();
                    if (sLNSSelected == LNSValue.LNS_9050001_9050002)
                    {
                        string sLNS = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                        _reportData = _reportData.Where(x => x.SLNS == sLNS).ToList();
                    }

                    List<ReportBHQTCQKPKThongTriQuery> result = _reportData
                                                                     .GroupBy(l => l.STenDonVi)
                                                                     .Select(cl => new ReportBHQTCQKPKThongTriQuery
                                                                     {
                                                                         SMaDonVi = cl.First().SMaDonVi,
                                                                         IID_DonVi = cl.First().IID_DonVi,
                                                                         STenDonVi = cl.First().STenDonVi,
                                                                         FTongTienDeNghiQuyetToanQuyNay = cl.Sum(c => c.FTongTienDeNghiQuyetToanQuyNay),
                                                                     }).ToList();

                    result.ForEach(x =>
                    {
                        x.Stt = Stt.ToString();
                        Stt++;
                    });

                    result = result.Where(x => x.FTongTienDeNghiQuyetToanQuyNay != 0).ToList();
                    double? tongTien = result.Sum(x => x.FTongTienDeNghiQuyetToanQuyNay);

                    AddEmptyItems(result);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);

                    data.Add("Nam", DateTime.Now.Year.ToString());
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("DonVi", donViCurrent.TenDonVi);
                    data.Add("Ve", string.Format("quý {0} năm {1}", CbxQuaterSelected.ValueItem, yearOfWork));
                    data.Add("TongChiTieu", tongTien);
                    data.Add("Items", result);
                    data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                    data.Add("TienBangChu", StringUtils.NumberToText(tongTien.Value * donViTinh, true));
                    data.Add("GhiChu", string.Empty);
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
                    ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPKThongTriQuery>(templateFileName, data);
                    //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                    string fileNameWithoutExtension = string.Format(sFileName + "_{0}_{1}", donViCurrent.TenDonVi, DateTime.Now.ToStringTimeStamp());
                    results.Add(new ExportResult(donViCurrent.TenDonVi, fileNameWithoutExtension, null, xlsFile));
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
                    _logger.Error(e.Error.Message);
                IsLoading = false;
            });
        }

        private ExportResult OnExportCoverSheet()
        {
            //_dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            RptBhQtcChiQuyKPKQuyetToanToBia rptToBia = new RptBhQtcChiQuyKPKQuyetToanToBia
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
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_QUYKPK, ExportFileName.RPT_BH_QTC_QKPK_TOBIA);
            string fileNamePrefix = ExportFileName.RPT_BH_QTC_QKPK_TOBIA.Split(".").First() + "_" + CbxQuaterSelected.DisplayItem;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private void FormatDisplay()
        {
            int iTypePrint = SettlementTypeValue;
            if (iTypePrint == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS)
            {
                foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.STM)))
                {
                    var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                    if (parent != null && parent.SM != string.Empty)
                    {
                        item.SM = string.Empty;
                        item.SL = string.Empty;
                        item.SK = string.Empty;
                        item.SLNS = string.Empty;
                    }
                }
                foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.STTM)))
                {
                    var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                    if (parent != null && parent.STM != string.Empty)
                    {
                        item.STM = string.Empty;
                        item.SM = string.Empty;
                        item.SL = string.Empty;
                        item.SK = string.Empty;
                        item.SLNS = string.Empty;
                    }
                }
            }

            if (iTypePrint == (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT)
            {
                foreach (var item in _reportDataKeHoach.Where(x => !string.IsNullOrEmpty(x.STM)))
                {
                    var parent = _reportDataKeHoach.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                    if (parent != null && parent.SM != string.Empty)
                    {
                        item.SM = string.Empty;

                    }
                }
                foreach (var item in _reportDataKeHoach.Where(x => !string.IsNullOrEmpty(x.STTM)))
                {
                    var parent = _reportDataKeHoach.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                    if (parent != null && parent.STM != string.Empty)
                    {
                        item.STM = string.Empty;
                        item.SM = string.Empty;
                    }
                }
            }
        }

        private void CalculateData()
        {
            int iTypePrint = SettlementTypeValue;

            if (iTypePrint == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS)
            {
                foreach (var item in _reportData.Where(x => !x.IsHangCha && (x.FTienDeNghiQuyetToanQuyNay != 0)))
                {
                    CalculateParent(item, item);
                }
            }

            if (iTypePrint == (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT)
            {

                _reportDataKeHoach.Where(x => x.IsHangCha)
                 .ForAll(x =>
                 {
                     //x.FTien_DuToanNamTruocChuyenSang = 0;
                     //x.FTien_DuToanGiaoNamNay = 0;
                     x.FTien_TongDuToanDuocGiao = 0;
                     x.FTienThucChi = 0;
                     x.FTienXacNhanQuyetToanQuyNay = 0;
                     x.FTienDeNghiQuyetToanQuyNay = 0;
                     x.FTienQuyetToanDaDuyet = 0;
                 });
                var temp = _reportDataKeHoach.Where(x => !x.IsHangCha).ToList();
                var dictByMlns = _reportDataKeHoach.GroupBy(x => x.IdMlns).ToDictionary(x => x.Key, x => x.First());
                foreach (var item in temp)
                {
                    CalculateParentKeHoach(item.IdMlnsCha, item, dictByMlns);
                }
            }
        }

        private void CalculateParentKeHoach(Guid idParent, ReportBHQTCQKPKThongTriQuery item, Dictionary<Guid, ReportBHQTCQKPKThongTriQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienXacNhanQuyetToanQuyNay += item.FTienXacNhanQuyetToanQuyNay;
            model.FTienDeNghiQuyetToanQuyNay += item.FTienDeNghiQuyetToanQuyNay;
            //model.FTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
            model.FTienThucChi += item.FTienThucChi;
            model.FTienQuyetToanDaDuyet += item.FTienQuyetToanDaDuyet;
            //model.FTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
            model.FTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;

            CalculateParentKeHoach(model.IdMlnsCha, item, dictByMlns);
        }

        private void CalculateParent(ReportBHQTCQKPKThongTriQuery item1, ReportBHQTCQKPKThongTriQuery item2)
        {

            var parentItem = _reportData.Where(x => x.IdMlns == item1.IdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.FTienDeNghiQuyetToanQuyNay += item2.FTienDeNghiQuyetToanQuyNay;
            CalculateParent(parentItem, item2);
        }

        public string GetFileNameIsDataReport()
        {

            if (SelectedDanhMucLoaiChi.ValueItem == MaLoaiChiBHXH.SMAKCBTS)
            {
                return ExportFileName.RPT_BH_QTC_QKPK_MACDINH_KEHOACH;
            }

            if (SelectedDanhMucLoaiChi.ValueItem == MaLoaiChiBHXH.SMAHSSVNLD)
            {
                if (LoaiChi == LoaiChi.Loai_HSSV)
                {
                    return ExportFileName.RPT_BH_QTC_QKPK_HSSV_KEHOACH;
                }
                else
                {
                    return ExportFileName.RPT_BH_QTC_QKPK_NLD_KEHOACH;
                }
            }

            if (SelectedDanhMucLoaiChi.ValueItem == MaLoaiChiBHXH.SMAKCBBHYT)
            {
                return ExportFileName.RPT_BH_QTC_QKPK_KCBBHYT_KEHOACH;
            }

            if (SelectedDanhMucLoaiChi.ValueItem == MaLoaiChiBHXH.SMAMSTTBYT)
            {
                return ExportFileName.RPT_BH_QTC_QKPK_MSTTBYT_KEHOACH;
            }

            if (SelectedDanhMucLoaiChi.ValueItem == MaLoaiChiBHXH.SMABHTN)
            {
                return ExportFileName.RPT_BH_QTC_QKPK_BHTN_KEHOACH;
            }

            return string.Empty;
        }

        private string GetFileNameIsVerbalExplanationReport()
        {
            return ExportFileName.RPT_BH_QTC_QKPK_MACDINH_KEHOACH_ISSOLIEU;
        }

        private string GetFileNameReport()
        {
            string sFileName = string.Empty;
            int iTypePrint = SettlementTypeValue;
            string sLNS = SelectedDanhMucLoaiChi.HiddenValue;
            if (SelectedDanhMucLoaiChi != null)
            {
                if (iTypePrint == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS)
                {
                    if (sLNS == LNSValue.LNS_9050001_9050002)
                    {
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() || SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString())
                        {
                            if (LoaiChi == LoaiChi.Loai_HSSV)
                            {
                                sFileName = ExportFileName.RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI2;
                            }
                            else
                            {
                                sFileName = ExportFileName.RPT_BH_QTC_QKPK_NLD_THONGTRILOAI2;
                            }

                        }
                        else
                        {
                            if (LoaiChi == LoaiChi.Loai_HSSV)
                            {
                                sFileName = ExportFileName.RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI1;
                            }
                            else
                            {
                                sFileName = ExportFileName.RPT_BH_QTC_QKPK_NLD_THONGTRILOAI1;
                            }
                        }
                    }
                    else if (sLNS == LNSValue.LNS_9010006_9010007)
                    {
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() || SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString())
                        {
                            sFileName = ExportFileName.RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI2;

                        }
                        else
                        {
                            sFileName = ExportFileName.RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI1;
                        }
                    }
                    else
                    {
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() || SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString())
                        {
                            sFileName = ExportFileName.RPT_BH_QTC_QKPK_KCBBHYTQN_BHTN_MSTTBYT_THONGTRILOAI2;

                        }
                        else
                        {
                            sFileName = ExportFileName.RPT_BH_QTC_QKPK_KCBBHYTQN_BHTN_MSTTBYT_THONGTRILOAI1;
                        }

                    }
                }

                if (iTypePrint == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY)
                {
                    if (sLNS == LNSValue.LNS_9050001_9050002)
                    {
                        if (LoaiChi == LoaiChi.Loai_HSSV)
                        {
                            sFileName = ExportFileName.RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI1;
                        }
                        else
                        {
                            sFileName = ExportFileName.RPT_BH_QTC_QKPK_NLD_THONGTRILOAI1;
                        }
                    }
                    else if (sLNS == LNSValue.LNS_9010006_9010007)
                    {
                        sFileName = ExportFileName.RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI1;
                    }

                    else
                    {
                        sFileName = ExportFileName.RPT_BH_QTC_QKPK_KCBBHYTQN_BHTN_MSTTBYT_THONGTRILOAI1;
                    }
                }
            }

            return sFileName;
        }

        private string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input += "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_QTC_QUYKPK, input + FileExtensionFormats.Xlsx);
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
    }
}
