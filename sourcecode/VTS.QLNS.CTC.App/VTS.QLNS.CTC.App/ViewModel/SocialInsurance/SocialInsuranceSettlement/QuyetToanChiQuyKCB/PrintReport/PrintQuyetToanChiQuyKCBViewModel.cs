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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.Explanation;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Explanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.Explanation;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PritnReport
{
    public class PrintQuyetToanChiQuyKCBViewModel : ViewModelBase
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
        private IQtcqKCBChiTietService _qtcqKCBChiTietService;
        private readonly IQtcqKCBService _qtcqKCBService;
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
        public SettlementVoucherModel SettlementVoucher;
        public List<SettlementVoucherDetailModel> SettlementVoucherDetails;
        public int SettlementTypeValue { get; set; }
        public bool IsShowAll { get; set; }
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public string name { get; set; }

        private string SettlementName
        {
            get
            {
                switch (SettlementTypeValue)
                {
                    case (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOKCBQUANYDONVI:
                        name = "Báo cáo quyết toán chi kinh phí KCB tại quân y đơn vị";
                        _isShowLoaiBaoCao = true;
                        break;
                    case (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB:
                        name = "In thông tri quyết toán chi kinh phí KCB tại quân y đơn vị";
                        _isShowLoaiBaoCao = false;
                        break;

                }

                OnPropertyChanged(nameof(IsShowLoaiBaoCao));
                return name;
            }
        }

        public bool IsExportEnable
        {
            get
            {
                if (SettlementTypeValue == (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB)
                {
                    return Agencies != null && Agencies.Where(x => x.Selected).Count() > 0;
                }
                else
                {
                    return Agencies != null && Agencies.Where(x => x.Selected).Count() > 0 && (_isCoverSheet || _isData || _isVerbalExplanation || _isDataInterpretation || _isCheckAll);
                }
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
        private bool _isShowLoaiBaoCao;
        public bool IsShowLoaiBaoCao
        {
            get => _isShowLoaiBaoCao;
            set => SetProperty(ref _isShowLoaiBaoCao, value);
        }
        public bool InMotToChecked { get; set; }
        public bool IsEnableCheckBoxInMotTo => !IsEnableCheckBoxSummary;
        public bool IsShowTheoTongHop => !IsShowLoaiBaoCao;
        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
        public override System.Type ContentType => typeof(PrintQuyetToanChiQuyKCB);

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
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
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

        public QuyetToanChiGiaiThichBangLoiQuyKCBViewModel QuyetToanChiGiaiThichBangLoiQuyKCBViewModel { get; set; }

        public PrintQuyetToanChiQuyKCBViewModel(INsMucLucNganSachService mucLucNganSachService,
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
            IQtcqKCBChiTietService qtcqKCBChiTietService,
            IQtcqKCBService qtcqKCBService,
            IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService,
            QuyetToanChiGiaiThichBangLoiQuyKCBViewModel quyetToanChiGiaiThichBangLoiQuyKCBViewModel)
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
            _qtcqKCBChiTietService = qtcqKCBChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;

            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _qtcqKCBService = qtcqKCBService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;
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
            VerbalExplanationCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());

            QuyetToanChiGiaiThichBangLoiQuyKCBViewModel = quyetToanChiGiaiThichBangLoiQuyKCBViewModel;
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _isInitReport = true;
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
            var lstChungTu = _qtcqKCBService.FindByCondition(x => x.INamChungTu == _sessionInfo.YearOfWork && x.IQuyChungTu == int.Parse(_cbxQuaterSelected.ValueItem) && x.IIdMaDonVi == lstDonViChecked);
            var BhQtcqKCBModel = _mapper.Map<ObservableCollection<BhQtcqKCBModel>>(lstChungTu);
            QuyetToanChiGiaiThichBangLoiQuyKCBViewModel.BhQtcqKCBModel = BhQtcqKCBModel.FirstOrDefault();
            QuyetToanChiGiaiThichBangLoiQuyKCBViewModel.Init();
            var view = new VerbalExplanation { DataContext = QuyetToanChiGiaiThichBangLoiQuyKCBViewModel };
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
                listMLNS = _bhDmMucLucNganSachService.FindSLNSForQTCQKCB(yearOfWork, agencyIds, iQuy, dt, _sessionInfo.Principal).ToList();
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
        private void LoadTieuDe()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB => TypeChuKy.RPT_BH_QTC_KCB_THONGTRIQUYETTOANCHIKCB,
                _ => TypeChuKy.RPT_BH_QTC_KCB_TONGHOPCACDONVICHIPHIKCB
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
                Title1 = SettlementTypeValue switch
                {
                    (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOKCBQUANYDONVI => SettlementTitle.Title1QTCQKCBKeHoach,
                    _ => SettlementTitle.Title1QTCQKCBTT
                };
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
            {
                Title2 = _dmChuKy.TieuDe2MoTa;
            }
            else
            {
                Title2 = SettlementTypeValue switch
                {
                    (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOKCBQUANYDONVI => SettlementTitle.Title2QTCQKCBKeHoach,
                    _ => SettlementTitle.Title2QTCQKCBTT
                };
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.Ten6MoTa))
                NoiDungChi = _dmChuKy.Ten6MoTa;
            else
                NoiDungChi = "Xác nhận chi kinh phí khám chữa bệnh";
        }

        private void LoadReportType()
        {
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB)
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
                int yearOfWork = _sessionInfo.YearOfWork;
                int iQuy = int.Parse(CbxQuaterSelected.ValueItem);

                List<DonVi> lstDonVis = new List<DonVi>();
                if (SettlementTypeValue == (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB)
                {
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        lstDonVis = _qtcqKCBService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu);
                        lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                    else
                    {
                        lstDonVis = _qtcqKCBService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTuTongHop);
                        if (!IsInTheoTongHop)
                            lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                        else
                            lstDonVis = lstDonVis.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();
                    }
                }
                else
                {
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        lstDonVis = _qtcqKCBService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTu).ToList();
                        lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                    }
                    else
                    {
                        lstDonVis = _qtcqKCBService.FindByDonViForNamLamViec(yearOfWork, iQuy, SettlementTypeLoaiChungTu.ChungTuTongHop).ToList();
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
                            LoadLNS();
                        }
                    };
                }

                ListLNS = new ObservableCollection<CheckBoxTreeItem>();
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

        private void OnExportFile(ExportType exportType)
        {
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOKCBQUANYDONVI)
            {
                ExportBaoCaoKCBQuanYDonVi(exportType);
            }

            if (SettlementTypeValue == (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB)
            {
                ExportThongTriQuyetToanKCBQuanYDonVi(exportType);
            }

        }

        private void ExportBaoCaoKCBQuanYDonVi(ExportType exportType)
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
                                    string fileName = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOQUYETTOANCHIKCBQUANY);
                                    results.AddRange(ProcessFile(yearOfWork, donViTinh, donVi, exportType, fileName));
                                }

                                if (IsVerbalExplanation)
                                {
                                    string fileName = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOQUYETTOANCHIKCBQUANYSOLIEU);
                                    results.AddRange(ProcessFile(yearOfWork, donViTinh, donVi, exportType, fileName));
                                }
                            }
                        }
                    }
                    else
                    {
                        var lstIdDonVi = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id).ToList());

                        //if (IsSummary)
                        //{
                        //    var lstChungTu = _qtcqKCBService.GetDanhSachQuyetToanKCB(yearOfWork, null).Where(x => x.IIdMaDonVi == lstIdDonVi).FirstOrDefault();
                        //    if (!string.IsNullOrEmpty(lstChungTu.SDSSoChungTuTongHop))
                        //    {
                        //        var sSoChungTu = lstChungTu.SDSSoChungTuTongHop.Split(",");
                        //        var lstChungTuChild = _qtcqKCBService.GetDanhSachQuyetToanKCB(yearOfWork, null).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                        //        var lsdMaDonVi = lstChungTuChild.Select(x => x.IIdMaDonVi).Distinct().ToList();
                        //        lstIdDonVi = string.Join(",", lsdMaDonVi);
                        //    }
                        //}

                        if (IsData)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOQUYETTOANCHIKCBQUANY);
                            results.AddRange(ProcessFileForDonVi(yearOfWork, donViTinh, lstIdDonVi, exportType, fileName, !IsSummary));
                        }

                        if (IsVerbalExplanation)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOQUYETTOANCHIKCBQUANYSOLIEU);
                            results.AddRange(ProcessFileForDonVi(yearOfWork, donViTinh, lstIdDonVi, exportType, fileName, IsSummary));
                        }

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

        private List<ExportResult> ProcessFileForDonVi(int yearOfWork, int donViTinh, string donVi, ExportType exportType, string fileName, bool isTongHop)
        {
            List<ExportResult> results = new List<ExportResult>();
            var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
            //var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();

            int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
            DonVi donViChild = _donViService.FindByIdDonVi(donVi, yearOfWork);
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);
            BhQtcQBHXHChiTietGiaiThichQuery lstGiaiThichBangLoi = new BhQtcQBHXHChiTietGiaiThichQuery();
            if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummary)
            {
                lstGiaiThichBangLoi = _qtcQBHXHChiTietGiaiThichService.GetGiaiThichBangLoiTheoDonVi(yearOfWork, donVi, int.Parse(CbxQuaterSelected.ValueItem), MaLoaiChiBHXH.SMAKPQL).FirstOrDefault();
            }
            List<BhQtcqKCBChiTietQuery> lstData = new List<BhQtcqKCBChiTietQuery>();
            lstData = _qtcqKCBChiTietService.BaoCaoKCBQuanYDonVi(_sessionService.Current.YearOfWork, donVi, LNSValue.LNS_9010004_9010005, isTongHop, iQuy, donViTinh).ToList();
            List<BhQtcqKCBChiTietModel> lstDataQueryMap = _mapper.Map<ObservableCollection<BhQtcqKCBChiTietModel>>(lstData).ToList();
            CalculateData(lstDataQueryMap);
            lstDataQueryMap = lstDataQueryMap.Where(x => (x.FTienDuToanNamTruocChuyenSang ?? 0) != 0 || (x.FTienTongDuToanDuocGiao ?? 0) != 0
                                         || (x.FTienThucChi ?? 0) != 0 || (x.FTienQuyetToanDaDuyet ?? 0) != 0
                                         || (x.FTienDeNghiQuyetToanQuyNay ?? 0) != 0 || (x.FTienXacNhanQuyetToanQuyNay ?? 0) != 0).ToList();

            lstDataQueryMap = lstDataQueryMap.OrderBy(x => x.SXauNoiMa).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("YearWork", yearOfWork);
            data.Add("IsAggregate", true);
            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
            data.Add("DonVi", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
            data.Add("ListData", lstDataQueryMap);
            data.Add("Quy", CbxQuaterSelected.DisplayItem);

            //Tính tổng
            Double? FTongTienDuToanNamTruocChuyenSang = lstDataQueryMap?.Where(x => x.BHangCha).Select(x => x.FTienDuToanNamTruocChuyenSang).Sum();
            Double? FTongTienDuToanGiaoNamNay = lstDataQueryMap?.Select(x => x.FTienDuToanGiaoNamNay).FirstOrDefault();
            Double? FTongTienTongDuToanDuocGiao = lstDataQueryMap?.Select(x => x.FTienTongDuToanDuocGiao).FirstOrDefault();
            Double? FTongTienThucChi = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienThucChi).Sum();
            Double? FTongTienQuyetToanDaDuyet = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienQuyetToanDaDuyet).Sum();
            Double? FTongTienDeNghiQuyetToanQuyNay = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienDeNghiQuyetToanQuyNay).Sum();
            Double? FTongTienXacNhanQuyetToanQuyNay = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienXacNhanQuyetToanQuyNay).Sum();

            data.Add("FTongTienDuToanNamTruocChuyenSang", FTongTienDuToanNamTruocChuyenSang);
            data.Add("FTongTienDuToanGiaoNamNay", FTongTienDuToanGiaoNamNay);
            data.Add("FTongTienTongDuToanDuocGiao", FTongTienTongDuToanDuocGiao);
            data.Add("FTongTienThucChi", FTongTienThucChi);
            data.Add("FTongTienQuyetToanDaDuyet", FTongTienQuyetToanDaDuyet);
            data.Add("FTongTienDeNghiQuyetToanQuyNay", FTongTienDeNghiQuyetToanQuyNay);
            data.Add("FTongTienXacNhanQuyetToanQuyNay", FTongTienXacNhanQuyetToanQuyNay);
            data.Add("HEADER2", string.Empty);
            data.Add("FormatNumber", formatNumber);
            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
            data.Add("MoTaTinhHinh", lstGiaiThichBangLoi?.SMoTa_TinhHinh);
            data.Add("MoTaKienNghi", lstGiaiThichBangLoi?.SMoTa_KienNghi);
            AddChuKy(data);
            data.Add("TienBangChu", StringUtils.NumberToText(FTongTienDeNghiQuyetToanQuyNay.Value * donViTinh, true));
            data.Add("Year", _sessionInfo.YearOfWork);

            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQKCB, fileName + FileExtensionFormats.Xlsx);
            string fileNamePrefix;
            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
            var xlsFile = _exportService.Export<BhQtcqKCBChiTietModel>(templateFileName, data);
            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

            return results;
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
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private List<ExportResult> ProcessFile(int yearOfWork, int donViTinh, string donVi, ExportType exportType, string fileName)
        {
            List<ExportResult> results = new List<ExportResult>();
            var lstIdDonVi = Agencies.Where(x => x.Selected).Select(x => x.Id).ToList();
            //var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();

            int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
            string sCap1 = GetLevelTitle(_dmChuKy, 1);
            string sCap2 = GetLevelTitle(_dmChuKy, 2);

            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
            DonVi donViChild = _donViService.FindByIdDonVi(donVi, yearOfWork);
            List<BhQtcqKCBChiTietQuery> lstData = new List<BhQtcqKCBChiTietQuery>();

            BhQtcQBHXHChiTietGiaiThichQuery lstGiaiThichBangLoi = new BhQtcQBHXHChiTietGiaiThichQuery();
            if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
            {
                lstGiaiThichBangLoi = _qtcQBHXHChiTietGiaiThichService.GetGiaiThichBangLoiTheoDonVi(yearOfWork, donVi, int.Parse(CbxQuaterSelected.ValueItem), MaLoaiChiBHXH.SMAKCBQYDV).FirstOrDefault();
            }
            lstData = _qtcqKCBChiTietService.BaoCaoKCBQuanYDonVi(_sessionService.Current.YearOfWork, donVi, LNSValue.LNS_9010004_9010005, true, iQuy, donViTinh).ToList();
            List<BhQtcqKCBChiTietModel> lstDataQueryMap = _mapper.Map<ObservableCollection<BhQtcqKCBChiTietModel>>(lstData).ToList();
            CalculateData(lstDataQueryMap);
            lstDataQueryMap = lstDataQueryMap.Where(x => (x.FTienDuToanNamTruocChuyenSang ?? 0) != 0 || (x.FTienTongDuToanDuocGiao ?? 0) != 0
                                         || (x.FTienThucChi ?? 0) != 0 || (x.FTienQuyetToanDaDuyet ?? 0) != 0
                                         || (x.FTienDeNghiQuyetToanQuyNay ?? 0) != 0 || (x.FTienXacNhanQuyetToanQuyNay ?? 0) != 0).ToList();

            lstDataQueryMap = lstDataQueryMap.OrderBy(x => x.SXauNoiMa).ToList();
            //Tính tổng
            //Double? FTongTienDuToanNamTruocChuyenSang = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienDuToanNamTruocChuyenSang).Sum();
            Double? FTongTienDuToanNamTruocChuyenSang = lstDataQueryMap?.Where(x => x.BHangCha).Select(x => x.FTienDuToanNamTruocChuyenSang).Sum();
            Double? FTongTienDuToanGiaoNamNay = lstDataQueryMap?.Select(x => x.FTienDuToanGiaoNamNay).FirstOrDefault();
            Double? FTongTienTongDuToanDuocGiao = lstDataQueryMap?.Select(x => x.FTienTongDuToanDuocGiao).FirstOrDefault();
            Double? FTongTienThucChi = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienThucChi).Sum();
            Double? FTongTienQuyetToanDaDuyet = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienQuyetToanDaDuyet).Sum();
            Double? FTongTienDeNghiQuyetToanQuyNay = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienDeNghiQuyetToanQuyNay).Sum();
            Double? FTongTienXacNhanQuyetToanQuyNay = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienXacNhanQuyetToanQuyNay).Sum();
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("YearWork", yearOfWork);
            data.Add("IsAggregate", false);
            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
            data.Add("DonVi", donViChild.TenDonVi);
            data.Add("ListData", lstDataQueryMap);
            data.Add("Quy", CbxQuaterSelected.DisplayItem);
            data.Add("FTongTienDuToanNamTruocChuyenSang", FTongTienDuToanNamTruocChuyenSang);
            data.Add("FTongTienDuToanGiaoNamNay", FTongTienDuToanGiaoNamNay);
            data.Add("FTongTienTongDuToanDuocGiao", FTongTienTongDuToanDuocGiao);
            data.Add("FTongTienThucChi", FTongTienThucChi);
            data.Add("FTongTienQuyetToanDaDuyet", FTongTienQuyetToanDaDuyet);
            data.Add("FTongTienDeNghiQuyetToanQuyNay", FTongTienDeNghiQuyetToanQuyNay);
            data.Add("FTongTienXacNhanQuyetToanQuyNay", FTongTienXacNhanQuyetToanQuyNay);
            data.Add("HEADER2", string.Empty);
            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
            data.Add("MoTaTinhHinh", lstGiaiThichBangLoi?.SMoTa_TinhHinh);
            data.Add("MoTaKienNghi", lstGiaiThichBangLoi?.SMoTa_KienNghi);
            data.Add("TienBangChu", StringUtils.NumberToText(FTongTienDeNghiQuyetToanQuyNay.Value * donViTinh, true));
            data.Add("Year", _sessionInfo.YearOfWork);
            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
            AddChuKy(data);
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQKCB, fileName + FileExtensionFormats.Xlsx);
            string fileNamePrefix;
            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
            var xlsFile = _exportService.Export<BhQtcqKCBChiTietModel>(templateFileName, data);
            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

            return results;
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
        }

        private ExportResult OnExportCoverSheet()
        {
            RptBhQtcChiQuyKCBQuyetToanToBia rptToBia = new RptBhQtcChiQuyKCBQuyetToanToBia
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
            string templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQKCB, ExportFileName.RPT_BH_QTC_QKCB_TOBIA);
            string fileNamePrefix = ExportFileName.RPT_BH_QTC_QKCB_TOBIA.Split(".").First() + "_" + CbxQuaterSelected.DisplayItem;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
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

        private void ExportThongTriQuyetToanKCBQuanYDonVi(ExportType exportType)
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
                    //var lstLns = ListLNS.Where(x => x.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
                    bool isTongHop = true;
                    int iloaiChungTu = 2;
                    int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    var donViCurrent = GetDonViOfCurrentUser();

                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        foreach (var donVi in lstIdDonVi)
                        {
                            DonVi donViChild = _donViService.FindByIdDonVi(donVi, yearOfWork);
                            List<BhQtcqKCBChiTietQuery> lstData = new List<BhQtcqKCBChiTietQuery>();
                            lstData = _qtcqKCBChiTietService.BaoCaoKCBQuanYDonVi(_sessionService.Current.YearOfWork, donVi, LNSValue.LNS_9010004_9010005, isTongHop, iQuy, donViTinh).ToList();
                            List<BhQtcqKCBChiTietModel> lstDataQueryMap = _mapper.Map<ObservableCollection<BhQtcqKCBChiTietModel>>(lstData).ToList();
                            CalculateData(lstDataQueryMap);
                            lstDataQueryMap = lstDataQueryMap.Where(x => (x.FTienDeNghiQuyetToanQuyNay ?? 0) != 0).ToList();
                            lstDataQueryMap.ForEach(x =>
                            {
                                x.SLNS = string.Empty;
                                x.SL = string.Empty;
                                x.SK = string.Empty;
                                x.SM = string.Empty;
                                x.STM = string.Empty;
                                x.STTM = string.Empty;
                                x.SNG = string.Empty;

                            });
                            lstDataQueryMap = lstDataQueryMap.OrderBy(x => x.SXauNoiMa).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                            data.Add("TieuDe1", Title1);
                            data.Add("TieuDe2", Title2);
                            data.Add("YearWork", yearOfWork);
                            data.Add("DonVi", donViChild != null ? donViChild.TenDonVi : string.Empty);
                            data.Add("ListData", lstDataQueryMap);
                            data.Add("Quy", CbxQuaterSelected.DisplayItem);


                            //Tính tổng
                            Double? FTienDeNghiQuyetToanQuyNay = lstDataQueryMap?.Where(x => !x.BHangCha).Select(x => x.FTienDeNghiQuyetToanQuyNay).Sum();
                            data.Add("FTongTienDeNghiQuyetToanQuyNay", FTienDeNghiQuyetToanQuyNay);
                            data.Add("TongSoTien", FTienDeNghiQuyetToanQuyNay != null ? StringUtils.NumberToText((double)FTienDeNghiQuyetToanQuyNay, true) : string.Empty);
                            data.Add("FormatNumber", formatNumber);

                            AddChuKy(data);
                            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", _sessionInfo.YearOfWork);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQtcqKCBChiTietModel>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                    }
                    else if (SelectedReportType.ValueItem == SummaryLNSReportType.Type.ToString())
                    {
                        foreach (var donVi in lstIdDonVi)
                        {
                            DonVi donViChild = _donViService.FindByIdDonVi(donVi, yearOfWork);
                            List<BhQtcqKCBChiTietQuery> lstData = new List<BhQtcqKCBChiTietQuery>();
                            var temp = _qtcqKCBChiTietService.BaoCaoKCBQuanYDonVi(_sessionService.Current.YearOfWork, donVi, LNSValue.LNS_9010004_9010005, isTongHop, iQuy, donViTinh).ToList();
                            lstData = new List<BhQtcqKCBChiTietQuery>()
                            {
                                new BhQtcqKCBChiTietQuery()
                                {
                                    SNoiDung = NoiDungChi,
                                    FTienDeNghiQuyetToanQuyNay = temp.Sum(x => x.FTienDeNghiQuyetToanQuyNay)
                                }
                            };
                            //CalculateData(lstData);

                            lstData = lstData.Where(x => (x.FTienDeNghiQuyetToanQuyNay ?? 0) != 0).ToList();
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

                            //lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                            data.Add("TieuDe1", Title1);
                            data.Add("TieuDe2", Title2);
                            data.Add("YearWork", yearOfWork);
                            data.Add("DonVi", donViChild != null ? donViChild.TenDonVi : string.Empty);
                            data.Add("ListData", lstData);
                            data.Add("Quy", CbxQuaterSelected.DisplayItem);


                            //Tính tổng
                            Double? FTienDeNghiQuyetToanQuyNay = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienDeNghiQuyetToanQuyNay).Sum();
                            data.Add("FTongTienDeNghiQuyetToanQuyNay", FTienDeNghiQuyetToanQuyNay);
                            data.Add("TongSoTien", FTienDeNghiQuyetToanQuyNay != null ? StringUtils.NumberToText((double)FTienDeNghiQuyetToanQuyNay, true) : string.Empty);
                            data.Add("FormatNumber", formatNumber);

                            AddChuKy(data);
                            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", _sessionInfo.YearOfWork);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQtcqKCBChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                    }
                    else
                    {
                        var lstDonViChecked = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));

                        if (IsInTheoTongHop)
                        {
                            var lstChungTu = _qtcqKCBService.GetDanhSachQuyetToanKCB(yearOfWork).Where(x => x.IIdMaDonVi == lstDonViChecked).FirstOrDefault();
                            if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                            {
                                var sSoChungTu = lstChungTu.STongHop.Split(",");
                                var lstChungTuChild = _qtcqKCBService.GetDanhSachQuyetToanKCB(yearOfWork).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                                var lsdMaDonVi = lstChungTuChild.Select(x => x.IIdMaDonVi).Distinct().ToList();
                                lstDonViChecked = string.Join(",", lsdMaDonVi);
                            }
                        }


                        List<ReportBHQTCQKCBThongTriQuery> lstData = new List<ReportBHQTCQKCBThongTriQuery>();
                        lstData = _qtcqKCBChiTietService.GetDataThongTriDonVi(_sessionService.Current.YearOfWork, CbxQuaterSelected.ValueItem, lstDonViChecked, _sessionInfo.Principal, iloaiChungTu, donViTinh).ToList();

                        AddEmptyItems(lstData);

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("TieuDe1", Title1);
                        data.Add("TieuDe2", Title2);
                        data.Add("DonVi", donViCurrent.TenDonVi);
                        data.Add("Items", lstData);
                        data.Add("Ve", string.Format("quý {0} năm {1}", CbxQuaterSelected.ValueItem, yearOfWork));
                        //Tính tổng
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
                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQKCB, ExportFileName.RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOTHONGTRITHEODONVIKCBQUANY);
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<ReportBHQTCQKCBThongTriQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI QÚY KCB QUÂN Y ĐƠN VỊ " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        bool isPdf = exportType == ExportType.PDF;
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

        private void CalculateData(List<BhQtcqKCBChiTietModel> lstNdtChungTuChiTiet)
        {
            lstNdtChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    //x.FTienDuToanNamTruocChuyenSang = 0;
                    //x.FTienDuToanGiaoNamNay = 0;
                    x.FTienTongDuToanDuocGiao = 0;
                    x.FTienThucChi = 0;
                    x.FTienQuyetToanDaDuyet = 0;
                    x.FTienDeNghiQuyetToanQuyNay = 0;
                    x.FTienXacNhanQuyetToanQuyNay = 0;
                    return x;
                }).ToList();
            var temp = lstNdtChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, BhQtcqKCBChiTietModel item, List<BhQtcqKCBChiTietModel> lstNdtChungTuChiTiet)
        {
            var dictByMlns = lstNdtChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];

            //model.FTienDuToanNamTruocChuyenSang = (model.FTienDuToanNamTruocChuyenSang ?? 0) + (item.FTienDuToanNamTruocChuyenSang ?? 0);
            //model.FTienDuToanGiaoNamNay = (model.FTienDuToanGiaoNamNay ?? 0) + (item.FTienDuToanGiaoNamNay ?? 0);
            model.FTienTongDuToanDuocGiao = (model.FTienTongDuToanDuocGiao ?? 0) + (item.FTienTongDuToanDuocGiao ?? 0);
            model.FTienThucChi = (model.FTienThucChi ?? 0) + (item.FTienThucChi ?? 0);
            model.FTienQuyetToanDaDuyet = (model.FTienQuyetToanDaDuyet ?? 0) + (item.FTienQuyetToanDaDuyet ?? 0);
            model.FTienDeNghiQuyetToanQuyNay = (model.FTienDeNghiQuyetToanQuyNay ?? 0) + (item.FTienDeNghiQuyetToanQuyNay ?? 0);
            model.FTienXacNhanQuyetToanQuyNay = (model.FTienXacNhanQuyetToanQuyNay ?? 0) + (item.FTienXacNhanQuyetToanQuyNay ?? 0);

            CalculateParent(model.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
        }

        private string GetTemplate()
        {
            string input = "";
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOKCBQUANYDONVI)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOQUYETTOANCHIKCBQUANY);
            }
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOTONGHOPCCACDONVI)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPOMDAU04A);
            }
            if (SettlementTypeValue == (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANQUYKCBQUANY);
            }

            return Path.Combine(ExportPrefix.PATH_BH_QTCQKCB, input + FileExtensionFormats.Xlsx);
        }
        private void OnConfigSign()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB => TypeChuKy.RPT_BH_QTC_KCB_THONGTRIQUYETTOANCHIKCB,
                _ => TypeChuKy.RPT_BH_QTC_KCB_TONGHOPCACDONVICHIPHIKCB
            };

            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = typeChuKy;
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
    }
}
