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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PritnReport
{
    public class PrintQuyetToanChiNamBHXHViewModel : ViewModelBase
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
        private IQtcnBHXHChiTietService _qtcnBHXHChiTietService;
        private IQtcnBHXHService _qtcnBHXHService;
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
        public bool IshowLoaiBaoCao { get; set; }
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
                    case (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                        name = "In báo cáo quyết toán chi các chế độ BHXH";
                        IshowLoaiBaoCao = true;
                        break;
                    case (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH:
                        name = "In quyết toán chi các chế độ BHXH";
                        IshowLoaiBaoCao = false;
                        break;
                }
                OnPropertyChanged(nameof(IsShowTheoTongHop));
                return name;
            }
        }
        public bool IsShowTheoTongHop => IshowLoaiBaoCao;
        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
        public override Type ContentType => typeof(PrintQuyetToanChiNam);

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

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintQuyetToanChiNamBHXHViewModel(INsMucLucNganSachService mucLucNganSachService,
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
            IQtcnBHXHChiTietService qtcnBHXHChiTietService,
            DataInterpretationViewModel dataInterpretationViewModel,
            VerbalExplanationViewModel verbalExplanationViewModel,
            IQtcnBHXHService qtcnBHXHService)
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
            _qtcnBHXHChiTietService = qtcnBHXHChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            DataInterpretationViewModel = dataInterpretationViewModel;
            VerbalExplanationViewModel = verbalExplanationViewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _qtcnBHXHService = qtcnBHXHService;

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
            ShowIsTongHop();
            _sessionInfo = _sessionService.Current;
            _isInitReport = true;
            YearOfWork = _sessionInfo.YearOfWork;
            _agencies = new ObservableCollection<AgencyModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            IsDataInterpretation = false;
            LoadTieuDe();
            LoadReportType();
            LoadDanhMuc();
            LoadBQuanLy();
            LoadAgencies();
            _isInitReport = false;
            IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
            LoadKieuGiay();
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
                listMLNS = _bhDmMucLucNganSachService.FindSLNSForQTCNBHXH(yearOfWork, agencyIds, dtime, _sessionInfo.Principal).ToList();
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

        private void LoadTieuDe()
        {
            var typeChuKy = SettlementTypeValue switch
            {
                (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH => TypeChuKy.RPT_BH_QUYETTOAN_BAOCAOQUYETTOANCHIBHXH,
                _ => TypeChuKy.RPT_BH_QUYETTOAN_QUYETTOANCHIBHXH
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
                    (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH => SettlementTitle.Title1QTCNBHXHKeHoach,
                    _ => SettlementTitle.Title1QTCNBHXHPhuLuc
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
                    (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH => SettlementTitle.Title2QTCNBHXHKeHoach,
                    _ => SettlementTitle.Title2QTCNBHXHPhuLuc
                };
            }
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
                List<DonVi> lstDonVis = new List<DonVi>();

                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    lstDonVis = _qtcnBHXHService.FindByDonViForNamLamViec(yearOfWork, SettlementTypeLoaiChungTu.ChungTu).ToList();
                    lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();

                }
                else
                {
                    lstDonVis = _qtcnBHXHService.FindByDonViForNamLamViec(yearOfWork, SettlementTypeLoaiChungTu.ChungTuTongHop).ToList();
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

        private void LoadKieuGiay()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG_A3, DisplayItem = LoaiGiay.NGANG_A3 });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();
        }

        private void OnExportFile(ExportType exportType)
        {
            if (SettlementTypeValue == (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH)
            {
                ExportBaoCaoQuyetToanChiCacCheDoBHXH(exportType);
            }
            else
            {
                ExportQuyetToanChiCacCheDoBHXH(exportType);
            }
        }

        private void ExportBaoCaoQuyetToanChiCacCheDoBHXH(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdDonVi = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id).ToList());
                    bool isTongHop = true;
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        foreach (var item in Agencies.Where(n => n.Selected))
                        {
                            isTongHop = false;
                            List<BhQtcnBHXHChiTietQuery> lstData = new List<BhQtcnBHXHChiTietQuery>();
                            DonVi donViChild = _donViService.FindByIdDonVi(item.Id, yearOfWork);
                            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                            lstData = _qtcnBHXHChiTietService.ExportBaoCaoQuyetToanChiNamCacCheDoBHXH(YearOfWork, item.Id,
                                 LNSValue.LNS_9010001_9010002,
                                donViTinh, isTongHop).ToList();

                            CalculateData(lstData);
                            lstData = lstData.Where(x => (x.ISoDuToanDuocDuyet ?? 0) != 0 || (x.FTienDuToanDuyet ?? 0) != 0
                                                         || (x.ITongSoThucChi ?? 0) != 0 || (x.FTongTienThucChi ?? 0) != 0
                                                         || (x.ISoSQThucChi ?? 0) != 0 || (x.FTienSQThucChi ?? 0) != 0
                                                         || (x.ISoQNCNThucChi ?? 0) != 0 || (x.FTienQNCNThucChi ?? 0) != 0
                                                         || (x.ISoCNVCQPThucChi ?? 0) != 0 || (x.ISoCNVCQPThucChi ?? 0) != 0
                                                         || (x.ISoLDHDThucChi ?? 0) != 0 || (x.ISoLDHDThucChi ?? 0) != 0
                                                         || (x.ISoHSQBSThucChi ?? 0) != 0 || (x.FTienHSQBSThucChi ?? 0) != 0
                                                         || (x.FTienThua ?? 0) != 0 || (x.FTienThieu ?? 0) != 0).ToList();

                            lstData.ForEach(x =>
                            {
                                x.FTienThua = x.FTienDuToanDuyet > x.FTongTienThucChi ? x.FTienDuToanDuyet - x.FTongTienThucChi : 0;
                                x.FTienThieu = x.FTongTienThucChi > x.FTienDuToanDuyet ? x.FTongTienThucChi - x.FTienDuToanDuyet : 0;
                                x.FTiLeThucHienTrenDuToan = x.FTienDuToanDuyet > 0 ? (x.FTongTienThucChi / x.FTienDuToanDuyet) * 100 : 0;
                            });
                            lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                            data.Add("YearWork", yearOfWork);
                            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                            data.Add("DonVi", item.TenDonVi);
                            data.Add("ListData", lstData);

                            //Tính tổng
                            int? FTongISoDuToanDuocDuyet = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoDuToanDuocDuyet).Sum();
                            Double? FTongTienDuToanDuyet = lstData?.Where(x => x.BHangCha && string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanDuyet).Sum();
                            int? FTongITongSoThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ITongSoThucChi).Sum();
                            Double? FTongTongTienThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienThucChi).Sum();
                            int? FTongISoSQThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoSQThucChi).Sum();
                            Double? FTongTienSQThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienSQThucChi).Sum();
                            int? FTongISoQNCNThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoQNCNThucChi).Sum();
                            Double? FTongTienQNCNThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienQNCNThucChi).Sum();
                            int? FTongISoCNVCQPThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoCNVCQPThucChi).Sum();
                            Double? FTongTienCNVCQPThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienCNVCQPThucChi).Sum();
                            int? FTongISoLDHDThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoLDHDThucChi).Sum();
                            Double? FTongTienLDHDThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienLDHDThucChi).Sum();
                            int? FTongISoHSQBSThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoHSQBSThucChi).Sum();
                            Double? FTongTienHSQBSThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienHSQBSThucChi).Sum();
                            Double? FTongTienThua = FTongTienDuToanDuyet - FTongTongTienThucChi > 0 ? FTongTienDuToanDuyet - FTongTongTienThucChi : 0;
                            Double? FTongTienThieu = FTongTongTienThucChi - FTongTienDuToanDuyet > 0 ? FTongTongTienThucChi - FTongTienDuToanDuyet : 0;
                            double? FTyLe = (FTongTienDuToanDuyet > 0) ? ((FTongTongTienThucChi ?? 0) / (FTongTienDuToanDuyet ?? 0)) * 100 : 0;
                            data.Add("TieuDe1", Title1);
                            data.Add("FTongISoDuToanDuocDuyet", FTongISoDuToanDuocDuyet);
                            data.Add("FTongTienDuToanDuyet", FTongTienDuToanDuyet);
                            data.Add("FTongITongSoThucChi", FTongITongSoThucChi);
                            data.Add("FTongTongTienThucChi", FTongTongTienThucChi);
                            data.Add("FTongISoSQThucChi", FTongISoSQThucChi);
                            data.Add("FTongTienSQThucChi", FTongTienSQThucChi);
                            data.Add("FTongISoQNCNThucChi", FTongISoQNCNThucChi);
                            data.Add("FTongTienQNCNThucChi", FTongTienQNCNThucChi);
                            data.Add("FTongISoCNVCQPThucChi", FTongISoCNVCQPThucChi);
                            data.Add("FTongTienCNVCQPThucChi", FTongTienCNVCQPThucChi);
                            data.Add("FTongISoLDHDThucChi", FTongISoLDHDThucChi);
                            data.Add("FTongTienLDHDThucChi", FTongTienLDHDThucChi);
                            data.Add("FTongISoHSQBSThucChi", FTongISoHSQBSThucChi);
                            data.Add("FTongTienHSQBSThucChi", FTongTienHSQBSThucChi);
                            data.Add("FTongTienThua", FTongTienThua);
                            data.Add("FTongTienThieu", FTongTienThieu);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("FTyLe", FTyLe);
                            data.Add("IsAggregate", false);
                            data.Add("TienBangChu", StringUtils.NumberToText(FTongTongTienThucChi.Value * donViTinh, true));
                            AddChuKy(data);

                            data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", _sessionInfo.YearOfWork);
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQtcnBHXHChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        }
                    }
                    else
                    {
                        isTongHop = false;
                        if (IsInTheoTongHop)
                        {
                            var lstChungTu = _qtcnBHXHService.GetDanhSachQuyetToanNamBHXH(yearOfWork).Where(x => x.IIdMaDonVi == lstIdDonVi).FirstOrDefault();
                            lstIdDonVi = lstChungTu.IIdMaDonVi;
                            isTongHop = true;
                            //if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                            //{
                            //    var sSoChungTu = lstChungTu.STongHop.Split(",");
                            //    var lstChungTuChild = _qtcnBHXHService.GetDanhSachQuyetToanNamBHXH(yearOfWork).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                            //    var lsdMaDonVi = lstChungTuChild.Select(x => x.IIdMaDonVi).Distinct().ToList();
                            //    lstIdDonVi = string.Join(",", lsdMaDonVi);
                            //    isTongHop = true;
                            //}
                        }

                        List<BhQtcnBHXHChiTietQuery> lstData = new List<BhQtcnBHXHChiTietQuery>();

                        DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                        lstData = _qtcnBHXHChiTietService.ExportBaoCaoQuyetToanChiNamCacCheDoBHXH(YearOfWork, lstIdDonVi,
                                                                                                   LNSValue.LNS_9010001_9010002, donViTinh, isTongHop).ToList();

                        CalculateData(lstData);
                        lstData = lstData.Where(x => (x.ISoDuToanDuocDuyet ?? 0) != 0 || (x.FTienDuToanDuyet ?? 0) != 0
                                                     || (x.ITongSoThucChi ?? 0) != 0 || (x.FTongTienThucChi ?? 0) != 0
                                                     || (x.ISoSQThucChi ?? 0) != 0 || (x.FTienSQThucChi ?? 0) != 0
                                                     || (x.ISoQNCNThucChi ?? 0) != 0 || (x.FTienQNCNThucChi ?? 0) != 0
                                                     || (x.ISoCNVCQPThucChi ?? 0) != 0 || (x.ISoCNVCQPThucChi ?? 0) != 0
                                                     || (x.ISoLDHDThucChi ?? 0) != 0 || (x.ISoLDHDThucChi ?? 0) != 0
                                                     || (x.ISoHSQBSThucChi ?? 0) != 0 || (x.FTienHSQBSThucChi ?? 0) != 0
                                                     || (x.FTienThua ?? 0) != 0 || (x.FTienThieu ?? 0) != 0).ToList();

                        lstData.ForAll(x =>
                        {
                            x.FTienThua = x.FTienDuToanDuyet > x.FTongTienThucChi ? x.FTienDuToanDuyet - x.FTongTienThucChi : 0;
                            x.FTienThieu = x.FTongTienThucChi > x.FTienDuToanDuyet ? x.FTongTienThucChi - x.FTienDuToanDuyet : 0;
                            x.FTiLeThucHienTrenDuToan = x.FTienDuToanDuyet > 0 ? x.FTongTienThucChi / x.FTienDuToanDuyet : 0;
                        });
                        lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                        data.Add("YearWork", yearOfWork);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("DonVi", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("ListData", lstData);

                        //Tính tổng
                        int? FTongISoDuToanDuocDuyet = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoDuToanDuocDuyet).Sum();
                        Double? FTongTienDuToanDuyet = lstData?.Where(x => x.BHangCha && string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanDuyet).Sum();
                        int? FTongITongSoThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ITongSoThucChi).Sum();
                        Double? FTongTongTienThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTongTienThucChi).Sum();
                        int? FTongISoSQThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoSQThucChi).Sum();
                        Double? FTongTienSQThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienSQThucChi).Sum();
                        int? FTongISoQNCNThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoQNCNThucChi).Sum();
                        Double? FTongTienQNCNThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienQNCNThucChi).Sum();
                        int? FTongISoCNVCQPThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoCNVCQPThucChi).Sum();
                        Double? FTongTienCNVCQPThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienCNVCQPThucChi).Sum();
                        int? FTongISoLDHDThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoLDHDThucChi).Sum();
                        Double? FTongTienLDHDThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienLDHDThucChi).Sum();
                        int? FTongISoHSQBSThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.ISoHSQBSThucChi).Sum();
                        Double? FTongTienHSQBSThucChi = lstData?.Where(x => !x.BHangCha).Select(x => x.FTienHSQBSThucChi).Sum();
                        Double? FTongTienThua = FTongTienDuToanDuyet - FTongTongTienThucChi > 0 ? FTongTienDuToanDuyet - FTongTongTienThucChi : 0;
                        Double? FTongTienThieu = FTongTongTienThucChi - FTongTienDuToanDuyet > 0 ? FTongTongTienThucChi - FTongTienDuToanDuyet : 0;
                        double? FTyLe = (FTongTienDuToanDuyet > 0) ? ((FTongTongTienThucChi ?? 0) / (FTongTienDuToanDuyet ?? 0)) * 100 : 0;

                        data.Add("TieuDe1", Title1);
                        data.Add("FTongISoDuToanDuocDuyet", FTongISoDuToanDuocDuyet);
                        data.Add("FTongTienDuToanDuyet", FTongTienDuToanDuyet);
                        data.Add("FTongITongSoThucChi", FTongITongSoThucChi);
                        data.Add("FTongTongTienThucChi", FTongTongTienThucChi);
                        data.Add("FTongISoSQThucChi", FTongISoSQThucChi);
                        data.Add("FTongTienSQThucChi", FTongTienSQThucChi);
                        data.Add("FTongISoQNCNThucChi", FTongISoQNCNThucChi);
                        data.Add("FTongTienQNCNThucChi", FTongTienQNCNThucChi);
                        data.Add("FTongISoCNVCQPThucChi", FTongISoCNVCQPThucChi);
                        data.Add("FTongTienCNVCQPThucChi", FTongTienCNVCQPThucChi);
                        data.Add("FTongISoLDHDThucChi", FTongISoLDHDThucChi);
                        data.Add("FTongTienLDHDThucChi", FTongTienLDHDThucChi);
                        data.Add("FTongISoHSQBSThucChi", FTongISoHSQBSThucChi);
                        data.Add("FTongTienHSQBSThucChi", FTongTienHSQBSThucChi);
                        data.Add("FTongTienThua", FTongTienThua);
                        data.Add("FTongTienThieu", FTongTienThieu);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("FTyLe", FTyLe);
                        data.Add("IsAggregate", true);
                        data.Add("TienBangChu", StringUtils.NumberToText(FTongTongTienThucChi.Value * donViTinh, true));
                        AddChuKy(data);

                        data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                        data.Add("Year", _sessionInfo.YearOfWork);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhQtcnBHXHChiTietQuery>(templateFileName, data);
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

        private void ExportQuyetToanChiCacCheDoBHXH(ExportType exportType)
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

                    List<BhBaoCaoQuyetToanChiNamQuery> lstData = new List<BhBaoCaoQuyetToanChiNamQuery>();

                    lstData = _qtcnBHXHChiTietService.ExportQuyetToanChiNamCacCheDoBHXH(YearOfWork, string.Join(",", lstIdDonVi), LNSValue.LNS_9010001_9010002,
                                                                                        donViTinh, isTongHop).ToList();

                    var lstDataSum = lstData.Where(x => x.IKhoi == int.Parse(LoaiDonVi.ROOT)).ToList();
                    var tongTien = lstDataSum.FirstOrDefault();
                    var tienTong = tongTien.FTongCong;
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    data.Add("YearWork", yearOfWork);

                    data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                    data.Add("ListData", lstData);

                    //Tính tổng
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("FormatNumber", formatNumber);

                    //AddChuKy(data);

                    data.Add("DiaDiem", string.Empty);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", _sessionInfo.YearOfWork);

                    data.Add("TienBangChu", StringUtils.NumberToText(tienTong.Value * donViTinh, true));
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhBaoCaoQuyetToanChiNamQuery>(templateFileName, data);
                    results.Add(new ExportResult("QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void CalculateData(List<BhQtcnBHXHChiTietQuery> lstNdtChungTuChiTiet)
        {
            lstNdtChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    //x.FTienDuToanDuyet = 0;
                    x.ISoDuToanDuocDuyet = 0;
                    x.ITongSoThucChi = 0;
                    x.FTongTienThucChi = 0;
                    x.ISoSQThucChi = 0;
                    x.FTienSQThucChi = 0;
                    x.ISoQNCNThucChi = 0;
                    x.FTienQNCNThucChi = 0;
                    x.ISoCNVCQPThucChi = 0;
                    x.FTienCNVCQPThucChi = 0;
                    x.ISoHSQBSThucChi = 0;
                    x.FTienHSQBSThucChi = 0;
                    x.FTienThua = 0;
                    x.FTienThieu = 0;
                    x.ISoLDHDThucChi = 0;
                    x.FTienLDHDThucChi = 0;
                    return x;
                }).ToList();
            var temp = lstNdtChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
            }

            var lstDuToan = lstNdtChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.SL) || x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).ToList();

            lstDuToan.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.SL))
                {
                    x.BHangCha = false;

                }

                //x.FTienDuToanDuyet = 0;
            });

            foreach (var item in lstDuToan)
            {
                CalculateParentDuToan(item.IID_MLNS_Cha, item, lstDuToan);
            }

            foreach (var item in lstNdtChungTuChiTiet)
            {
                foreach (var itemTienDuToan in lstDuToan)
                {
                    if (!string.IsNullOrEmpty(itemTienDuToan.SL))
                    {
                        itemTienDuToan.BHangCha = true;

                    }

                    if (item.IIdMucLucNganSach == itemTienDuToan.IIdMucLucNganSach)
                    {
                        item.FTienDuToanDuyet = itemTienDuToan.FTienDuToanDuyet;
                    }

                }
            }
        }

        private void CalculateParentDuToan(Guid? idParent, BhQtcnBHXHChiTietQuery item, List<BhQtcnBHXHChiTietQuery> lstNdtChungTuChiTiet)
        {
            var dictByMlns = lstNdtChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);

            CalculateParentDuToan(model.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
        }

        private void CalculateParent(Guid? idParent, BhQtcnBHXHChiTietQuery item, List<BhQtcnBHXHChiTietQuery> lstNdtChungTuChiTiet)
        {
            var dictByMlns = lstNdtChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            //model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);
            model.ISoDuToanDuocDuyet = (model.ISoDuToanDuocDuyet ?? 0) + (item.ISoDuToanDuocDuyet ?? 0);
            model.ITongSoThucChi = (model.ITongSoThucChi ?? 0) + (item.ITongSoThucChi ?? 0);
            model.FTongTienThucChi = (model.FTongTienThucChi ?? 0) + (item.FTongTienThucChi ?? 0);
            model.ISoSQThucChi = (model.ISoSQThucChi ?? 0) + (item.ISoSQThucChi ?? 0);
            model.FTienSQThucChi = (model.FTienSQThucChi ?? 0) + (item.FTienSQThucChi ?? 0);
            model.ISoQNCNThucChi = (model.ISoQNCNThucChi ?? 0) + (item.ISoQNCNThucChi ?? 0);
            model.FTienQNCNThucChi = (model.FTienQNCNThucChi ?? 0) + (item.FTienQNCNThucChi ?? 0);
            model.ISoCNVCQPThucChi = (model.ISoCNVCQPThucChi ?? 0) + (item.ISoCNVCQPThucChi ?? 0);
            model.FTienCNVCQPThucChi = (model.FTienCNVCQPThucChi ?? 0) + (item.FTienCNVCQPThucChi ?? 0);
            model.ISoHSQBSThucChi = (model.ISoHSQBSThucChi ?? 0) + (item.ISoHSQBSThucChi ?? 0);
            model.FTienHSQBSThucChi = (model.FTienHSQBSThucChi ?? 0) + (item.FTienHSQBSThucChi ?? 0);
            model.FTienThua = (model.FTienThua ?? 0) + (item.FTienThua ?? 0);
            model.ISoLDHDThucChi = (model.ISoLDHDThucChi ?? 0) + (item.ISoLDHDThucChi ?? 0);
            model.FTienLDHDThucChi = (model.FTienLDHDThucChi ?? 0) + (item.FTienLDHDThucChi ?? 0);
            CalculateParent(model.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
        }

        private string GetTemplate()
        {
            string input = "";
            if (SettlementTypeValue == (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANNAM_BAOCAOQUYETTOANCHIBHXH);
            }
            else
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RP_BH_EXPORT_QUYETTOANNAM_QUYETTOANCHIBHXH);
            }

            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG_A3)
            {
                return Path.Combine(ExportPrefix.PATH_BH_QTCNBHXH, input + "_A3" + FileExtensionFormats.Xlsx);
            }
            else
            {
                return Path.Combine(ExportPrefix.PATH_BH_QTCNBHXH, input + FileExtensionFormats.Xlsx);
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
