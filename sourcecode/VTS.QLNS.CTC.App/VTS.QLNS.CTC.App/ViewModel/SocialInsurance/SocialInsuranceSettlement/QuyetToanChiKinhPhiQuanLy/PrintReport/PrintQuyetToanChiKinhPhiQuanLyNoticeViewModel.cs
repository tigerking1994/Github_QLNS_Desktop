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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.Explanation;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.Explanation;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport
{
    public class PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel : ViewModelBase
    {
        #region Interface
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IBhQtcQuyKinhPhiQuanLyService _kinhPhiQuanLyService;
        private readonly IBhQtcQuyKinhPhiQuanLyChiTietService _kinhPhiQuanLyChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;
        private INsPhongBanService _phongBanService;
        private ICollectionView _listAgency;
        private ICollectionView _listLNSView;
        #endregion

        #region Property
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private List<BhQtcQuyKinhPhiQuanLyQuery> _listChungTu;
        private List<BhQtcQuyKinhPhiQuanLyQuery> _listChungTuDotCap;
        private BhQtcQuyKinhPhiQuanLyQuery _chungTuSelected;
        private SessionInfo _sessionInfo;
        private List<BhQtcQuyKinhPhiQuanLyChiTietQuery> _listChungTuChiTiet;
        private List<ReportBHQTCQKPQuanLyThongTriQuery> _reportData;
        private List<ReportBHQTCQKPQuanLyThongTriQuery> _reportDataKeHoach;
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
                        NamePrint = "In thông tri chi kinh phí quản lý BHXH, BHYT";
                        _isShowLoaiBaoCao = false;
                        break;
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        NamePrint = "In báo cáo quyết toán chi kinh phí quản lý BHXH, BHYT";
                        _isShowLoaiBaoCao = true;
                        break;
                    default:
                        break;
                }

                OnPropertyChanged(nameof(IsShowLoaiBaoCao));
                return NamePrint;
            }
        }

        private bool _isShowLoaiBaoCao;
        public bool IsShowLoaiBaoCao
        {
            get => _isShowLoaiBaoCao;
            set => SetProperty(ref _isShowLoaiBaoCao, value);
        }

        public bool InMotToChecked { get; set; }
        public bool IsShowTheoTongHop => !IsShowLoaiBaoCao;
        public bool IsEnableCheckBoxInMotTo => !IsEnableCheckBoxSummary;
        public override Type ContentType => typeof(PrintQuyetToanChiKinhPhiQuanLyNotice);
        public override string Name => SettlementPrintName;
        public override string Description => SettlementPrintName;
        public override string Title => SettlementPrintName;
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();
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

                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        _isShowLoiGiaiThich = true;
                        break;
                }
                return _isShowLoiGiaiThich;
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
                LoadAgencies();
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
        #endregion

        #region RelayCommand
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        #endregion

        #region View Nodel
        public QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel QuyetToanThuGiaiThichBangLoiQuyKPQLViewModel { get; set; }
        #endregion

        #region Constructor
        public PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel(
                    INsDonViService donViService,
                    IMapper mapper,
                    ISessionService sessionService,
                    ILog logger,
                    IDanhMucService danhMucService,
                    IExportService exportService,
                    IDmChuKyService dmChuKyService,
                    DmChuKyDialogViewModel dmChuKyDialogViewModel,
                    IBhQtcQuyKinhPhiQuanLyService qtcQuyKinhPhiQuanLyService,
                    IBhQtcQuyKinhPhiQuanLyChiTietService bhQtcQuyKinhPhiQuanLyChiTiet,
                    INsNguoiDungDonViService nguoiDungDonViService,
                    INsDonViService nsDonViService,
                    IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
                    IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                    INsPhongBanService phongBanService,
                    IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService,
                    QuyetToanChiGiaiThichBangLoiQuyKPQLViewModel quyetToanChiGiaiThichBangLoiQuyKPQLViewModel)
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
            _kinhPhiQuanLyService = qtcQuyKinhPhiQuanLyService;
            _kinhPhiQuanLyChiTietService = bhQtcQuyKinhPhiQuanLyChiTiet;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;

            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            _nsDonViService = nsDonViService;
            _phongBanService = phongBanService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());

            QuyetToanThuGiaiThichBangLoiQuyKPQLViewModel = quyetToanChiGiaiThichBangLoiQuyKPQLViewModel;
        }

        private void OnOpenVerbalExplanationDialog()
        {
            var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (!lstDonViChecked.Any())
            {
                MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                return;
            }

            var lstChungTu = _kinhPhiQuanLyService.FindByCondition(x => x.INamChungTu == _sessionInfo.YearOfWork && x.IQuyChungTu == int.Parse(CbxQuaterSelected.ValueItem) && x.IID_MaDonVi == lstDonViChecked);
            var lstQtcQKPQLModel = _mapper.Map<ObservableCollection<BhQtcQuyKinhPhiQuanLyModel>>(lstChungTu);
            QuyetToanThuGiaiThichBangLoiQuyKPQLViewModel.BhQtcQuyKinhPhiQuanLyModel= lstQtcQKPQLModel.FirstOrDefault();
            QuyetToanThuGiaiThichBangLoiQuyKPQLViewModel.Init();
            var view = new VerbalExplanation { DataContext = QuyetToanThuGiaiThichBangLoiQuyKPQLViewModel };
            view.ShowDialog();
        }
        #endregion

        #region  Init
        public override void Init()
        {
            try
            {
                base.Init();
                _sessionInfo = _sessionService.Current;
                _isInitReport = true;
                LoadReportType();
                InitReportDefaultDate();
                ShowSoLieuChi();
                LoadQuater();
                _agencies = new ObservableCollection<AgencyModel>();
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

        private void ShowSoLieuChi()
        {
            switch (SettlementTypeValue)
            {
                case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:

                    _isShowLoaiBaoCao = false;
                    break;
                case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:

                    _isShowLoaiBaoCao = false;
                    break;
                case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:

                    _isShowLoaiBaoCao = true;
                    break;
                default:
                    break;
            }

            OnPropertyChanged(nameof(IsShowLoaiBaoCao));
        }
        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                int yearOfWork = _sessionInfo.YearOfWork;
                int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
                List<DonVi> lstAgencies = new List<DonVi>();

                if (SettlementTypeValue != (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT)
                {
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        lstAgencies = _kinhPhiQuanLyService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu).ToList();
                        lstAgencies = lstAgencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                    else
                    {
                        lstAgencies = _kinhPhiQuanLyService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu).ToList();
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
                        lstAgencies = _kinhPhiQuanLyService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu).ToList();
                        lstAgencies = lstAgencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                    else
                    {
                        lstAgencies = _kinhPhiQuanLyService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTuTongHop).ToList();
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
                            LoadLNS();
                        }
                    };
                }

                _listLNS = new ObservableCollection<CheckBoxTreeItem>();
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

        private void LoadReportType()
        {
            if (SettlementTypeValue == (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS)
            {
                _reportTypes = new List<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                    new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
                    new ComboboxItem { DisplayItem = "Theo loại chi", ValueItem = SummaryLNSReportType.Type.ToString() }
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
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
            DateTime dt = DateTime.Now;
            List<BhDmMucLucNganSach> listMLNS = new List<BhDmMucLucNganSach>();
            string agencyIds = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (agencyIds.Length > 0)
            {
                listMLNS = _bhDmMucLucNganSachService.FindSLNSForQTCQKPQL(yearOfWork, agencyIds, iQuy, dt, _sessionInfo.Principal).ToList();
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
        #endregion

        #region Load data
        
        private void LoadTieuDe()
        {
            int iTypePrint = SettlementTypeValue;
            switch (iTypePrint)
            {
                case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPQL_THONGTRILOAI1;
                    break;
                case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                    _typeChuKy = TypeChuKy.RPT_BH_QTC_QKPQL_CHITIET;
                    break;
                default:
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
                        Title1 = SettlementTitle.Title1QTCQKPQLTT;
                        break;
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        Title1 = SettlementTitle.TitleQTCQKPQLKeHoach;
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
                switch (iTypePrint)
                {
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_AGENCY:
                        Title2 = SettlementTitle.Title2QTCQKPQLTT;
                        break;
                    case (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT:
                        Title2 = SettlementTitle.Title2QTCQKPQLKeHoach;
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
                NoiDungChi = "Xác nhận chi kinh phí quản lý BHXH, BHYT";
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
        #endregion

        #region Add chữ ky
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
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();

            DmChuKyDialogViewModel.HasAddedSign4 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign5 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign6 = IsVerbalExplanation;

            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
        #endregion

        #region Export File
        private void ExportFile(bool isPdf)
        {
            int iPrintType = (int)SettlementTypeValue;

            switch (iPrintType)
            {
                case (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS:
                    ExportThongTriQuyetToanQuyLoai2(isPdf);
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
                    string sFileName = string.Empty;
                    var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(yearOfWork).ToList();
                    var iDDmLoaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(SettlementTypeSLNS.SLNS)).Select(x => x.Id).FirstOrDefault();
                    int iLoaiChungTu = 0;
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        iLoaiChungTu = 1;

                        foreach (var agency in Agencies.Where(n => n.Selected))
                        {
                            if (IsData)
                            {
                                sFileName = ExportFileName.RPT_BH_QTC_QKPQL_CHITIET;
                                results.AddRange(ProcessFile(yearOfWork, donViTinh, isPdf, agency, sFileName, iDDmLoaiChi, iLoaiChungTu));
                            }

                            if (IsVerbalExplanation)
                            {
                                sFileName = ExportFileName.RPT_BH_QTC_QKPQL_CHITIET_ISSOLIEU;
                                results.AddRange(ProcessFile(yearOfWork, donViTinh, isPdf, agency, sFileName, iDDmLoaiChi, iLoaiChungTu));
                            }
                        }
                    }
                    else
                    {

                        iLoaiChungTu = IsSummary ? 2 : 1;
                        var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));

                        if (IsData)
                        {
                            sFileName = ExportFileName.RPT_BH_QTC_QKPQL_CHITIET;
                            results.AddRange(ProcessFileForDonVi(yearOfWork, donViTinh, isPdf, lstDonViChecked, sFileName, iDDmLoaiChi, iLoaiChungTu));
                        }

                        if (IsVerbalExplanation)
                        {
                            sFileName = ExportFileName.RPT_BH_QTC_QKPQL_CHITIET_ISSOLIEU;
                            results.AddRange(ProcessFileForDonVi(yearOfWork, donViTinh, isPdf, lstDonViChecked, sFileName, iDDmLoaiChi, iLoaiChungTu));
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

        private List<ExportResult> ProcessFile(int yearOfWork, int donViTinh, bool isPdf, AgencyModel agency, string sFileName, Guid iDDmLoaiChi, int iLoaiChungTu)
        {
            List<ExportResult> results = new List<ExportResult>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
            string templateFileName = string.Empty;
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);

            QtcQuyKinhPhiQuanLyCriteria searchCondition = new QtcQuyKinhPhiQuanLyCriteria();
            searchCondition.NamLamViec = yearOfWork;
            searchCondition.IDMaDonVi = agency.Id;
            searchCondition.SNguoiTao = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = iLoaiChungTu;
            searchCondition.SLNS = LNSValue.LNS_9010003;
            searchCondition.DNgayChungTu = DateTime.Now;
            searchCondition.IQuyChungTu = int.Parse(CbxQuaterSelected.ValueItem);
            searchCondition.IDLoaiChi = iDDmLoaiChi;
            searchCondition.DonViTinh = donViTinh;
            _reportDataKeHoach = _kinhPhiQuanLyChiTietService.GetDataReportKeHoach(searchCondition).ToList();


            DonVi donViChild = _donViService.FindByIdDonVi(agency.Id, yearOfWork);
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
            CalculateData();
            FormatDisplay();
            double? tongTien = _reportDataKeHoach?.Select(x => x.FTienDeNghiQuyetToanQuyNay).FirstOrDefault();
            var SumFTienDuToanDuocGiao = _reportDataKeHoach?.Where(x => x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToiKPQL).Select(x => x.FTienDuToanDuocGiao).FirstOrDefault();
            var SumFTienThucChi = _reportDataKeHoach?.Select(x => x.FTienThucChi).FirstOrDefault();
            var SumFTienQuyetToanDaDuyet = _reportDataKeHoach?.Select(x => x.FTienQuyetToanDaDuyet).FirstOrDefault();
            var SumFTienDeNghiQuyetToanQuyNay = _reportDataKeHoach?.Select(x => x.FTienDeNghiQuyetToanQuyNay).FirstOrDefault();
            var SumFTienXacNhanQuyetToanQuyNay = _reportDataKeHoach?.Select(x => x.FTienXacNhanQuyetToanQuyNay).FirstOrDefault();

            _reportDataKeHoach = _reportDataKeHoach.Where(x => x.IsNotData).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("FormatNumber", formatNumber);
            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
            data.Add("SumFTienDuToanDuocGiao", SumFTienDuToanDuocGiao);
            data.Add("SumFTienThucChi", SumFTienThucChi);
            data.Add("SumFTienQuyetToanDaDuyet", SumFTienQuyetToanDaDuyet);
            data.Add("SumFTienDeNghiQuyetToanQuyNay", SumFTienDeNghiQuyetToanQuyNay);
            data.Add("SumFTienXacNhanQuyetToanQuyNay", SumFTienXacNhanQuyetToanQuyNay);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("Quy", CbxQuaterSelected.DisplayItem);
            data.Add("YearWork", _sessionInfo.YearOfWork);
            data.Add("DonVi", agency.TenDonVi);
            data.Add("IsAggregate", false);
            //data.Add("Ve", string.Format("quý {0} năm {1}", QuartersSelected.ValueItem, yearOfWork));
            data.Add("ListData", _reportDataKeHoach);
            data.Add("Header1", SelectedUnit.DisplayItem);
            data.Add("HEADER2", string.Empty);
            data.Add("TienBangChu", StringUtils.NumberToText(tongTien.Value * donViTinh, true));
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

            if (IsVerbalExplanation)
            {

                BhQtcQBHXHChiTietGiaiThichQuery lstGiaiThichBangLoi = new BhQtcQBHXHChiTietGiaiThichQuery();
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    lstGiaiThichBangLoi = _qtcQBHXHChiTietGiaiThichService.GetGiaiThichBangLoiTheoDonVi(yearOfWork, searchCondition.IDMaDonVi, int.Parse(CbxQuaterSelected.ValueItem), MaLoaiChiBHXH.SMAKPQL).FirstOrDefault();
                }
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
            ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPQuanLyThongTriQuery>(templateFileName, data);
            //if (!IsVerbalExplanation)
            //{
            //    xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
            //}
            string fileNameWithoutExtension = string.Format(sFileName + "_{0}_{1}", agency.MaTenDonVi, DateTime.Now.ToStringTimeStamp());
            results.Add(new ExportResult(agency.AgencyName, fileNameWithoutExtension, null, xlsFile));
            return results;
        }



        private List<ExportResult> ProcessFileForDonVi(int yearOfWork, int donViTinh, bool isPdf, string lstDonVi, string sFileName, Guid iDDmLoaiChi, int iLoaiChungTu)
        {
            List<ExportResult> results = new List<ExportResult>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
            string templateFileName = string.Empty;
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            QtcQuyKinhPhiQuanLyCriteria searchCondition = new QtcQuyKinhPhiQuanLyCriteria();
            searchCondition.NamLamViec = yearOfWork;
            searchCondition.IDMaDonVi = lstDonVi;
            searchCondition.SNguoiTao = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = iLoaiChungTu;
            searchCondition.SLNS = LNSValue.LNS_9010003;
            searchCondition.DNgayChungTu = DateTime.Now;
            searchCondition.IQuyChungTu = int.Parse(CbxQuaterSelected.ValueItem);
            searchCondition.IDLoaiChi = iDDmLoaiChi;
            searchCondition.DonViTinh = donViTinh;
            _reportDataKeHoach = _kinhPhiQuanLyChiTietService.GetDataReportKeHoach(searchCondition).ToList();

            DonVi donViChild = _donViService.FindByIdDonVi(LoaiDonVi.NOI_BO, yearOfWork);
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
            CalculateData();
            FormatDisplay();
            double? tongTien = _reportDataKeHoach?.Select(x => x.FTienDeNghiQuyetToanQuyNay).FirstOrDefault();
            var SumFTienDuToanDuocGiao = _reportDataKeHoach?.Where(x => x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToiKPQL).Select(x => x.FTienDuToanDuocGiao).FirstOrDefault();
            var SumFTienThucChi = _reportDataKeHoach?.Select(x => x.FTienThucChi).FirstOrDefault();
            var SumFTienQuyetToanDaDuyet = _reportDataKeHoach?.Select(x => x.FTienQuyetToanDaDuyet).FirstOrDefault();
            var SumFTienDeNghiQuyetToanQuyNay = _reportDataKeHoach?.Select(x => x.FTienDeNghiQuyetToanQuyNay).FirstOrDefault();
            var SumFTienXacNhanQuyetToanQuyNay = _reportDataKeHoach?.Select(x => x.FTienXacNhanQuyetToanQuyNay).FirstOrDefault();

            _reportDataKeHoach = _reportDataKeHoach.Where(x => x.IsNotData).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("FormatNumber", formatNumber);
            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
            data.Add("SumFTienDuToanDuocGiao", SumFTienDuToanDuocGiao);
            data.Add("SumFTienThucChi", SumFTienThucChi);
            data.Add("SumFTienQuyetToanDaDuyet", SumFTienQuyetToanDaDuyet);
            data.Add("SumFTienDeNghiQuyetToanQuyNay", SumFTienDeNghiQuyetToanQuyNay);
            data.Add("SumFTienXacNhanQuyetToanQuyNay", SumFTienXacNhanQuyetToanQuyNay);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("Quy", CbxQuaterSelected.DisplayItem);
            data.Add("YearWork", _sessionInfo.YearOfWork);
            data.Add("DonVi", _sessionInfo.TenDonVi);
            data.Add("IsAggregate", true);
            //data.Add("Ve", string.Format("quý {0} năm {1}", QuartersSelected.ValueItem, yearOfWork));
            data.Add("ListData", _reportDataKeHoach);
            data.Add("Header1", SelectedUnit.DisplayItem);
            data.Add("HEADER2", string.Empty);
            data.Add("TienBangChu", StringUtils.NumberToText(tongTien.Value * donViTinh, true));
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

            if (IsVerbalExplanation)
            {

                BhQtcQBHXHChiTietGiaiThichQuery lstGiaiThichBangLoi = new BhQtcQBHXHChiTietGiaiThichQuery();
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummary)
                {
                    lstGiaiThichBangLoi = _qtcQBHXHChiTietGiaiThichService.GetGiaiThichBangLoiTheoDonVi(yearOfWork, lstDonVi, int.Parse(CbxQuaterSelected.ValueItem), MaLoaiChiBHXH.SMAKPQL).FirstOrDefault();
                }

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
            ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPQuanLyThongTriQuery>(templateFileName, data);
            //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
            string fileNameWithoutExtension = string.Format(sFileName + "_{0}_{1}", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi), DateTime.Now.ToStringTimeStamp());
            results.Add(new ExportResult(!string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi), fileNameWithoutExtension, null, xlsFile));
            return results;
        }

        private void ExportThongTriQuyetToanQuyLoai2(bool isPdf)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();

                int donViTinh = int.Parse(SelectedUnit.ValueItem);
                FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                string lstLns = string.Join(",", ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList());
                string templateFileName = string.Empty;
                var yearOfWork = _sessionInfo.YearOfWork;
                int iLoaiChungTu = SettlementTypeLoaiChungTu.ChungTuTongHop;
                var donViCurrent = GetDonViOfCurrentUser();
                int Stt = 1;
                string sCap1 = GetLevelTitle(_dmChuKy, 1);
                string sCap2 = GetLevelTitle(_dmChuKy, 2);
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    iLoaiChungTu = SettlementTypeLoaiChungTu.ChungTu;
                    foreach (var donvi in Agencies.Where(n => n.Selected))
                    {
                        DonVi donViChild = _donViService.FindByIdDonVi(donvi.Id, yearOfWork);
                        DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                        _reportData = _kinhPhiQuanLyChiTietService.GetDataReportQtcQuyKPQlThongTri2(yearOfWork, CbxQuaterSelected.ValueItem,
                                                                                                    donvi.Id, LNSValue.LNS_9010003, _sessionInfo.Principal
                                                                                                    , iLoaiChungTu, donViTinh).ToList();
                        double? tongTien = _reportData.Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                        CalculateData();
                        FormatDisplay();
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

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);

                        _reportData = _reportData.Where(x => x.IsNotData).ToList();
                        data.Add("Nam", DateTime.Now.Year.ToString());
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("DonVi", donvi.MaTenDonVi);
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
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QTC_QKPQL_THONGTRILOAI2));
                        ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPQuanLyThongTriQuery>(templateFileName, data);
                        //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                        string fileNameWithoutExtension = string.Format("rptBH_QTC_QKPQL_Thongtri_Loai2_{0}_{1}", donvi.Id, DateTime.Now.ToStringTimeStamp());
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
                        var temp = _kinhPhiQuanLyChiTietService.GetDataReportQtcQuyKPQlThongTri2(yearOfWork, CbxQuaterSelected.ValueItem,
                                                                                                    donvi.Id, LNSValue.LNS_9010003, _sessionInfo.Principal
                                                                                                    , iLoaiChungTu, donViTinh).ToList();
                        _reportData = new List<ReportBHQTCQKPQuanLyThongTriQuery>()
                        {
                            new ReportBHQTCQKPQuanLyThongTriQuery()
                            {
                                FTienDeNghiQuyetToanQuyNay = temp.Sum(x => x.FTienDeNghiQuyetToanQuyNay),
                                SMoTa = NoiDungChi
                            }
                        };

                        double? tongTien = _reportData.Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                        //CalculateData();
                        //FormatDisplay();

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

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);

                        _reportData = _reportData.Where(x => x.IsNotData).ToList();

                        AddEmptyItems(_reportData);

                        data.Add("Nam", DateTime.Now.Year.ToString());
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("TieuDe3", Title3);
                        data.Add("DonVi", donvi.MaTenDonVi);
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
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QTC_QKPQL_THONGTRILOAI2));
                        ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPQuanLyThongTriQuery>(templateFileName, data);
                        //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                        string fileNameWithoutExtension = string.Format("rptBH_QTC_QKPQL_Thongtri_Loai2_{0}_{1}", donvi.Id, DateTime.Now.ToStringTimeStamp());
                        results.Add(new ExportResult(donvi.MaTenDonVi, fileNameWithoutExtension, null, xlsFile));

                    }
                }
                else
                {
                    var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));

                    if (IsInTheoTongHop)
                    {
                        var lstChungTu = _kinhPhiQuanLyService.FindIndex(yearOfWork).Where(x => x.IID_MaDonVi == lstDonViChecked).FirstOrDefault();
                        if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                        {
                            var sSoChungTu = lstChungTu.STongHop.Split(",");
                            var lstChungTuChild = _kinhPhiQuanLyService.FindIndex(yearOfWork).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                            var lsdMaDonVi = lstChungTuChild.Select(x => x.IID_MaDonVi).Distinct().ToList();
                            lstDonViChecked = string.Join(",", lsdMaDonVi);
                        }
                    }

                    _reportData = _kinhPhiQuanLyChiTietService.GetDataReportQtcQuyKPQlThongTri1(yearOfWork, CbxQuaterSelected.ValueItem, lstDonViChecked, _sessionInfo.Principal, iLoaiChungTu, donViTinh).ToList();
                    DonVi donViChild = _donViService.FindByIdDonVi(donViCurrent.IIDMaDonVi, yearOfWork);
                    _reportData.ForAll(x =>
                    {
                        x.Stt = Stt.ToString();
                        Stt++;
                    });

                    AddEmptyItems(_reportData);
                    double? tongTien = _reportData.Sum(x => x.FTongTienDeNghi);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                    //_reportData = _reportData.Where(x => x.IsNotData).ToList();
                    data.Add("Nam", DateTime.Now.Year.ToString());
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("DonVi", !string.IsNullOrEmpty(sCap2) ? sCap2 : donViCurrent.TenDonVi);
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
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QTC_QKPQL_THONGTRILOAI1));
                    ExcelFile xlsFile = _exportService.Export<ReportBHQTCQKPQuanLyThongTriQuery>(templateFileName, data);
                    //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                    string fileNameWithoutExtension = string.Format("rptBH_QTC_QKPQL_Thongtri_DonVi_{0}_{1}", donViCurrent.TenDonVi, DateTime.Now.ToStringTimeStamp());
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

        private ExportResult OnExportCoverSheet()
        {
            RptBhQtcChiQuyKPQLQuyetToanToBia rptToBia = new RptBhQtcChiQuyKPQLQuyetToanToBia
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
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTC_QUYKPQL, ExportFileName.RPT_BH_QTC_QKPQL_TOBIA);
            string fileNamePrefix = ExportFileName.RPT_BH_QTC_QKPQL_TOBIA.Split(".").First() + "_" + CbxQuaterSelected.DisplayItem;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input += "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_QTC_QUYKPQL, input + FileExtensionFormats.Xlsx);
        }

        private void FormatDisplay()
        {
            int iTypePrint = (int)SettlementTypeValue;
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
            else
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
            int iTypePrint = (int)SettlementTypeValue;

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
                     x.FTienDeNghiQuyetToanQuyNay = 0;
                     x.FTienQuyetToanDaDuyet = 0;
                     x.FTienThucChi = 0;
                     x.FTienXacNhanQuyetToanQuyNay = 0;
                 });
                var temp = _reportDataKeHoach.Where(x => !x.IsHangCha).ToList();
                var dictByMlns = _reportDataKeHoach.GroupBy(x => x.IdMlns).ToDictionary(x => x.Key, x => x.First());
                foreach (var item in temp)
                {
                    CalculateParentKeHoach(item.IdMlnsCha, item, dictByMlns);
                }
            }
        }

        private void CalculateParentKeHoach(Guid idParent, ReportBHQTCQKPQuanLyThongTriQuery item, Dictionary<Guid, ReportBHQTCQKPQuanLyThongTriQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienXacNhanQuyetToanQuyNay += item.FTienXacNhanQuyetToanQuyNay;
            model.FTienDeNghiQuyetToanQuyNay += item.FTienDeNghiQuyetToanQuyNay;
            model.FTienThucChi += item.FTienThucChi;
            model.FTienQuyetToanDaDuyet += item.FTienQuyetToanDaDuyet;

            CalculateParentKeHoach(model.IdMlnsCha, item, dictByMlns);
        }

        private void CalculateParent(ReportBHQTCQKPQuanLyThongTriQuery item1, ReportBHQTCQKPQuanLyThongTriQuery item2)
        {
            var parentItem = _reportData.Where(x => x.IdMlns == item1.IdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.FTienDeNghiQuyetToanQuyNay += item2.FTienDeNghiQuyetToanQuyNay;
            CalculateParent(parentItem, item2);
        }

        private DonVi GetDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        #endregion
    }
}