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
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB.PritnReport
{
    public class PrintQuyetToanChiNamKCViewModel : ViewModelBase
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
        private IQtcnKCBChiTietService _qtcnKCBChiTietService;
        private IQtcnKCBService _qtcnKCBService;
        private VerbalExplanationViewModel VerbalExplanationViewModel;
        private DataInterpretationViewModel DataInterpretationViewModel;
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

        private string SettlementName
        {
            get
            {
                switch (SettlementTypeValue)
                {
                    case (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                        name = "In báo cáo quyết toán năm khám chữa bệnh tại quân y đơn vị";
                        break;
                    case (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH:
                        name = "In phụ lục báo cáo quyết toán năm khám chữa bệnh tại quân y";
                        break;
                }
                return name;
            }
        }

        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
        public override Type ContentType => typeof(PrintQuyetToanChiNamKCB);

        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();
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
                    //LoadLNS();
                    OnPropertyChanged(nameof(IsExportEnable));
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

        public bool IsExportEnable => Agencies != null && Agencies.Where(x => x.Selected).Count() > 0;


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

        private int _yearOfWork;
        public int YearOfWork { get => _yearOfWork; set => SetProperty(ref _yearOfWork, value); }

        public bool IshowLoaiBaoCao { get; set; }
        public bool IsShowTheoTongHop => IshowLoaiBaoCao;
        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintQuyetToanChiNamKCViewModel(INsMucLucNganSachService mucLucNganSachService,
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
            IQtcnKCBChiTietService qtcnKCBChiTietService,
            DataInterpretationViewModel dataInterpretationViewModel,
            VerbalExplanationViewModel verbalExplanationViewModel,
            IQtcnKCBService qtcnKCBService)
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
            _qtcnKCBChiTietService = qtcnKCBChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            DataInterpretationViewModel = dataInterpretationViewModel;
            VerbalExplanationViewModel = verbalExplanationViewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _qtcnKCBService = qtcnKCBService;

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
            YearOfWork = _sessionInfo.YearOfWork;
            LoadReportType();
            ShowIsTongHop();
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
            LoadKieuGiay();
        }

        private void ShowIsTongHop()
        {
            switch (SettlementTypeValue)
            {
                case (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                    IshowLoaiBaoCao = true;
                    break;
                case (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH:
                    IshowLoaiBaoCao = false;
                    break;
            }

            OnPropertyChanged(nameof(IsShowTheoTongHop));
        }


        public void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            DateTime dtime = DateTime.Now;
            List<BhDmMucLucNganSach> listMLNS = new List<BhDmMucLucNganSach>();

            string agencyIds = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (agencyIds.Length > 0)
            {
                listMLNS = _bhDmMucLucNganSachService.FindSLNSForQTCNKCB(yearOfWork, agencyIds, dtime, _sessionInfo.Principal).ToList();
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

        private void LoadKieuGiay()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG_A3, DisplayItem = LoaiGiay.NGANG_A3 });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();
        }
        private void LoadTieuDe()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH => TypeChuKy.RPT_BH_QUYETTOAN_BAOCAOQUYETTOANCHIBHXH_KCB,
                _ => TypeChuKy.RPT_BH_QUYETTOAN_QUYETTOANCHIBHXH_KCB
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
                    (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH => SettlementTitle.Title1QTCNKCBKeHoach,
                    _ => SettlementTitle.Title1QTCNKCBPhuLuc
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
                    (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH => "",
                    _ => SettlementTitle.Title2QTCNKCBPhuLuc
                };
            }

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
            {
                Title3 = _dmChuKy.TieuDe3MoTa;
            }
            else
            {
                Title3 = SettlementTypeValue switch
                {
                    (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH => "",
                    _ => SettlementTitle.Title3QTCNKCBPhuLuc
                };
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
                List<DonVi> lstDonVis = new List<DonVi>();
                var yearOfWork = _sessionInfo.YearOfWork;

                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    lstDonVis = _qtcnKCBService.FindByDonViForNamLamViec(yearOfWork, SettlementTypeLoaiChungTu.ChungTu).ToList();
                    lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();

                }
                else
                {
                    lstDonVis = _qtcnKCBService.FindByDonViForNamLamViec(yearOfWork, SettlementTypeLoaiChungTu.ChungTuTongHop).ToList();
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

        private void OnExportFile(ExportType exportType)
        {
            if (SettlementTypeValue == (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH)
            {
                ExportBaoCaoQuyetToanKhamChuaBenhTaiQuanYDonVi(exportType);
            }
            else
            {
                ExportPhuLucBaoCaoQuyetToanNamKCB(exportType);
            }

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

        private void ExportBaoCaoQuyetToanKhamChuaBenhTaiQuanYDonVi(ExportType exportType)
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
                    int stt = 1;
                    bool isTongHop;
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        foreach (var item in Agencies.Where(n => n.Selected))
                        {
                            isTongHop = false;
                            List<BhQtcnKCBChiTietQuery> lstData = new List<BhQtcnKCBChiTietQuery>();
                            DonVi donViChild = _donViService.FindByIdDonVi(item.Id, yearOfWork);
                            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                            lstData = _qtcnKCBChiTietService.ExportBaoCaoQuyetToanKhamChuaBenhTaiQuanYDonVi(YearOfWork, item.Id,
                                                                                                            string.Join(",", LNSValue.LNS_9010004_9010005),
                                                                                                            donViTinh, isTongHop).ToList();

                            CalculateData(lstData);
                            lstData = lstData.Where(x => (x.FTienDuToanNamTruocChuyenSang ?? 0) != 0 || (x.FTienDuToanGiaoNamNay ?? 0) != 0
                                                         || (x.FTienTongDuToanDuocGiao ?? 0) != 0 || (x.FTienThucChi ?? 0) != 0
                                                         || (x.FTienThua ?? 0) != 0 || (x.FTienThieu ?? 0) != 0).ToList();
                            lstData.ForEach(x =>
                            {
                                x.STT = stt;
                                stt++;
                                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                                {
                                    x.FTienTongDuToanDuocGiao = x.FTienDuToanGiaoNamNay + x.FTienDuToanNamTruocChuyenSang;
                                    x.FTienThua = (x.FTienTongDuToanDuocGiao > x.FTienThucChi) ? x.FTienTongDuToanDuocGiao - x.FTienThucChi : 0;
                                    x.FTienThieu = (x.FTienTongDuToanDuocGiao < x.FTienThucChi) ? x.FTienThucChi - x.FTienTongDuToanDuocGiao : 0;
                                    x.FTiLeThucHienTrenDuToan = x.FTienTongDuToanDuocGiao > 0 ? (x.FTienThucChi / x.FTienTongDuToanDuocGiao) * 100 : 0;
                                }
                            });
                            lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                            data.Add("TieuDe1", Title1);
                            data.Add("YearWork", yearOfWork);
                            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                            data.Add("DonVi", donViChild != null ? donViChild.TenDonVi : string.Empty);
                            data.Add("ListData", lstData);

                            //Tính tổng
                            Double? FTongTienDuToanNamTruocChuyenSang = lstData?.Where(x => x.BHangCha).Select(x => x.FTienDuToanNamTruocChuyenSang).Sum();
                            Double? FTongTienDuToanGiaoNamNay = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanGiaoNamNay).FirstOrDefault();
                            Double? FTongTienTongDuToanDuocGiao = FTongTienDuToanNamTruocChuyenSang + FTongTienDuToanGiaoNamNay;
                            Double? FTongTienThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienThucChi).Sum();
                            Double? FTongTienThua = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienThua).Sum();
                            Double? FTongTienThieu = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienThieu).Sum();

                            data.Add("FTongTienDuToanNamTruocChuyenSang", FTongTienDuToanNamTruocChuyenSang);
                            data.Add("FTongTienDuToanGiaoNamNay", FTongTienDuToanGiaoNamNay);
                            data.Add("FTongTienTongDuToanDuocGiao", FTongTienTongDuToanDuocGiao);
                            data.Add("FTongTienThucChi", FTongTienThucChi);
                            data.Add("FTongTienThua", FTongTienThua);
                            data.Add("FTongTienThieu", FTongTienThieu);

                            data.Add("FormatNumber", formatNumber);

                            AddChuKy(data);
                            Double? TongTien = FTongTienThucChi ?? 0;
                            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", _sessionInfo.YearOfWork);
                            data.Add("TienBangChu", StringUtils.NumberToText(TongTien.Value * donViTinh, true));

                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQtcnKCBChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI KHÁM CHỮA BỆNH QUÂN Y BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                    }

                    else
                    {
                        isTongHop = false;
                        var lstIdDonViCheckd = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id).ToList());
                        if (IsInTheoTongHop)
                        {
                            isTongHop = true;
                            //var lstChungTu = _qtcnKCBService.GetDanhSachQuyetToanNamKCB(yearOfWork, null).Where(x => x.IIdMaDonVi == lstIdDonViCheckd).FirstOrDefault();
                            //if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                            //{
                            //    var sSoChungTu = lstChungTu.STongHop.Split(",");
                            //    var lstChungTuChild = _qtcnKCBService.GetDanhSachQuyetToanNamKCB(yearOfWork, null).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                            //    var lsdMaDonVi = lstChungTuChild.Select(x => x.IIdMaDonVi).Distinct().ToList();
                            //    lstIdDonViCheckd = string.Join(",", lsdMaDonVi);
                            //}
                        }

                        List<BhQtcnKCBChiTietQuery> lstData = new List<BhQtcnKCBChiTietQuery>();
                        DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                        lstData = _qtcnKCBChiTietService.ExportBaoCaoQuyetToanKhamChuaBenhTaiQuanYDonVi(YearOfWork, lstIdDonViCheckd,
                                                                                                        LNSValue.LNS_9010004_9010005,
                                                                                                        donViTinh, isTongHop).ToList();

                        CalculateData(lstData);
                        lstData = lstData.Where(x => (x.FTienDuToanNamTruocChuyenSang ?? 0) != 0 || (x.FTienDuToanGiaoNamNay ?? 0) != 0
                                                     || (x.FTienTongDuToanDuocGiao ?? 0) != 0 || (x.FTienThucChi ?? 0) != 0
                                                     || (x.FTienThua ?? 0) != 0 || (x.FTienThieu ?? 0) != 0).ToList();

                        lstData.ForEach(x =>
                        {
                            x.STT = stt;
                            stt++;
                            if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                            {
                                x.FTienTongDuToanDuocGiao = x.FTienDuToanGiaoNamNay + x.FTienDuToanNamTruocChuyenSang;
                                x.FTienThua = (x.FTienTongDuToanDuocGiao > x.FTienThucChi) ? x.FTienTongDuToanDuocGiao - x.FTienThucChi : 0;
                                x.FTienThieu = (x.FTienTongDuToanDuocGiao < x.FTienThucChi) ? x.FTienThucChi - x.FTienTongDuToanDuocGiao : 0;
                                x.FTiLeThucHienTrenDuToan = x.FTienTongDuToanDuocGiao > 0 ? (x.FTienThucChi / x.FTienTongDuToanDuocGiao) * 100 : 0;
                            }
                        });
                        lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                        data.Add("TieuDe1", Title1);
                        data.Add("YearWork", yearOfWork);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("DonVi", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("ListData", lstData);

                        //Tính tổng
                        Double? FTongTienDuToanNamTruocChuyenSang = lstData?.Where(x => x.BHangCha).Select(x => x.FTienDuToanNamTruocChuyenSang).Sum();
                        Double? FTongTienDuToanGiaoNamNay = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanGiaoNamNay).FirstOrDefault();
                        Double? FTongTienTongDuToanDuocGiao = FTongTienDuToanNamTruocChuyenSang + FTongTienDuToanGiaoNamNay;
                        Double? FTongTienThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienThucChi).Sum();
                        Double? FTongTienThua = lstData?.Where(x => x.BHangCha).Select(x => x.FTienThua).Sum();
                        Double? FTongTienThieu = lstData?.Where(x => x.BHangCha).Select(x => x.FTienThieu).Sum();

                        data.Add("FTongTienDuToanNamTruocChuyenSang", FTongTienDuToanNamTruocChuyenSang);
                        data.Add("FTongTienDuToanGiaoNamNay", FTongTienDuToanGiaoNamNay);
                        data.Add("FTongTienTongDuToanDuocGiao", FTongTienTongDuToanDuocGiao);
                        data.Add("FTongTienThucChi", FTongTienThucChi);
                        data.Add("FTongTienThua", FTongTienThua);
                        data.Add("FTongTienThieu", FTongTienThieu);

                        data.Add("FormatNumber", formatNumber);

                        AddChuKy(data);
                        Double? TongTien = (FTongTienTongDuToanDuocGiao ?? 0) + (FTongTienThucChi ?? 0);
                        data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                        data.Add("Year", _sessionInfo.YearOfWork);
                        data.Add("TienBangChu", StringUtils.NumberToText(TongTien.Value * donViTinh, true));

                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhQtcnKCBChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
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
        }

        private void ExportPhuLucBaoCaoQuyetToanNamKCB(ExportType exportType)
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
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        isTongHop = false;
                    }

                    List<BhQtcnKCBChiTietQuery> lstData = new List<BhQtcnKCBChiTietQuery>();
                    lstData = _qtcnKCBChiTietService.ExportPhuLucQuyetToanKhamChuaBenhTaiQuanYDonVi(YearOfWork, string.Join(",", lstIdDonVi), LNSValue.LNS_9010004_9010005,
                    donViTinh, isTongHop).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("Cap1", donViParent != null ? donViParent.TenDonVi : string.Empty);
                    data.Add("YearWork", yearOfWork);
                    data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                    data.Add("ListData", lstData);

                    //Tính tổng
                    Double? FTongChiTieuNamTruoc = lstData.Select(x => x.FChiTieuNamTruoc).Sum();
                    Double? FTongChiTieuNamNay = lstData.Select(x => x.FChiTieuNamNay).Sum();
                    Double? FTongTienTongCong = lstData.Select(x => x.FTongCong).Sum();
                    Double? FTongTienQuyetToan = lstData.Select(x => x.FTienQuyetToan).Sum();
                    Double? FTongTienThua = lstData.Select(x => x.FTienThua).Sum();
                    Double? FTongTienThieu = lstData.Select(x => x.FTienThieu).Sum();

                    data.Add("FTongChiTieuNamTruoc", FTongChiTieuNamTruoc);
                    data.Add("FTongChiTieuNamNay", FTongChiTieuNamNay);
                    data.Add("FTongTienTongCong", FTongTienTongCong);
                    data.Add("FTongTienQuyetToan", FTongTienQuyetToan);
                    data.Add("FTongTienThua", FTongTienThua);
                    data.Add("FTongTienThieu", FTongTienThieu);

                    Double? TongTien = FTongTienQuyetToan ?? 0;
                    data.Add("TienBangChu", StringUtils.NumberToText(TongTien.Value * donViTinh, true));
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
                    var xlsFile = _exportService.Export<BhQtcnKCBChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("PHỤ LỤC QUYẾT TOÁN NĂM KCB QUÂN Y ĐƠN VỊ " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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
                //}
            }
        }

        private void CalculateData(List<BhQtcnKCBChiTietQuery> lstNdtChungTuChiTiet)
        {
            lstNdtChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    //x.FTienDuToanNamTruocChuyenSang = 0;
                    //x.FTienDuToanGiaoNamNay = 0;
                    x.FTienTongDuToanDuocGiao = 0;
                    x.FTienThucChi = 0;
                    x.FTienThua = 0;
                    x.FTienThieu = 0;
                    return x;
                }).ToList();
            var temp = lstNdtChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, BhQtcnKCBChiTietQuery item, List<BhQtcnKCBChiTietQuery> lstNdtChungTuChiTiet)
        {
            var dictByMlns = lstNdtChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.FTienDuToanNamTruocChuyenSang = (model.FTienDuToanNamTruocChuyenSang ?? 0) + (item.FTienDuToanNamTruocChuyenSang ?? 0);
            model.FTienDuToanGiaoNamNay = (model.FTienDuToanGiaoNamNay ?? 0) + (item.FTienDuToanGiaoNamNay ?? 0);
            model.FTienTongDuToanDuocGiao = (model.FTienTongDuToanDuocGiao ?? 0) + (item.FTienTongDuToanDuocGiao ?? 0);
            model.FTienThucChi = (model.FTienThucChi ?? 0) + (item.FTienThucChi ?? 0);
            model.FTienThua = (model.FTienThua ?? 0) + (item.FTienThua ?? 0);
            model.FTienThieu = (model.FTienThieu ?? 0) + (item.FTienThieu ?? 0);
            CalculateParent(model.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
        }

        private string GetTemplate()
        {
            string input = "";
            if (SettlementTypeValue == (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANNAM_BAOCAOQUYETTOANCHIKCBQUANYDONVI);
            }
            else
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANNAM_PHHULUCBAOCAOQUYETTOANNAMKCBQUANYDONVI);
            }

            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG_A3)
            {
                return Path.Combine(ExportPrefix.PATH_BH_QTCNKCB, input + "_A3" + FileExtensionFormats.Xlsx);
            }
            else
            {
                return Path.Combine(ExportPrefix.PATH_BH_QTCNKCB, input + FileExtensionFormats.Xlsx);
            }
        }
        private void OnConfigSign()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH => TypeChuKy.RPT_BH_QUYETTOAN_BAOCAOQUYETTOANCHIBHXH,
                _ => TypeChuKy.RPT_BH_QUYETTOAN_QUYETTOANCHIBHXH

            };

            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = typeChuKy;
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

        
    }
}
