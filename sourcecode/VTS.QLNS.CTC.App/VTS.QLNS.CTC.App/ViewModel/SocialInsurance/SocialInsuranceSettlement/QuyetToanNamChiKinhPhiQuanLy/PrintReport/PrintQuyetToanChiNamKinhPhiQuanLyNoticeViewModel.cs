using AutoMapper;
using ControlzEx.Standard;
using FlexCel.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy.PrintReport
{
    public class PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel : ViewModelBase
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
        private INsPhongBanService _phongBanService;
        private readonly IBhQtcNamKinhPhiQuanLyService _kinhPhiQuanLyService;
        private readonly IBhQtcNamKinhPhiQuanLyChiTietService _kinhPhiQuanLyChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private ICollectionView _listAgency;
        private ICollectionView _listLNSView;
        #endregion

        #region Property
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private List<BhQtcNamKinhPhiQuanLyQuery> _listChungTu;
        private List<BhQtcNamKinhPhiQuanLyQuery> _listChungTuDotCap;
        private BhQtcNamKinhPhiQuanLyQuery _chungTuSelected;
        private SessionInfo _sessionInfo;
        private List<ReportBHQTCNKPQuanLyPhuLucQuery> _listPhuLuc;
        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> _reportData;
        private bool _isCapPhatToanDonVi;
        private string _diaDiem;
        private string _typeChuKy;
        private bool _checkAllAgencies;
        private bool _isInitReport;
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
        public override Type ContentType => typeof(PrintQuyetToanNamChiKinhPhiQuanLyNotice);
        public override string Name
        {
            get
            {
                if (SettlementTypeValue != (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN)
                {
                    return "IN PHỤ LỤC QUYẾT TOÁN CHI NĂM KINH PHÍ QUẢN LÝ BHXH, BHYT";
                }
                else
                {
                    return "IN BÁO CÁO QUYẾT TOÁN CHI NĂM KINH PHÍ QUẢN LÝ BHXH, BHYT";
                }
            }
        }
        public override string Description
        {
            get
            {
                if (SettlementTypeValue == (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN)
                {
                    return "IN BÁO CÁO QUYẾT TOÁN CHI NĂM KINH PHÍ QUẢN LÝ BHXH, BHYT";
                }
                else
                {
                    return "IN PHỤ LỤC QUYẾT TOÁN CHI NĂM KINH PHÍ QUẢN LÝ BHXH, BHYT";
                }
            }
        }

        public bool IshowLoaiBaoCao { get; set; }
        public bool IsShowTheoTongHop => IshowLoaiBaoCao;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();
        public int SettlementTypeValue { get; set; }
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
        #endregion

        #region RelayCommand
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region Constructor
        public PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel(
                    INsDonViService donViService,
                    IMapper mapper,
                    ISessionService sessionService,
                    ILog logger,
                    IDanhMucService danhMucService,
                    IExportService exportService,
                    IDmChuKyService dmChuKyService,
                    DmChuKyDialogViewModel dmChuKyDialogViewModel,
                    IBhQtcNamKinhPhiQuanLyService kinhPhiQuanLyService,
                    IBhQtcNamKinhPhiQuanLyChiTietService kinhPhiQuanLyChiTiet,
                    INsNguoiDungDonViService nguoiDungDonViService,
                    INsDonViService nsDonViService,
                    IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
                    INsPhongBanService nsPhongBanService,
                    IBhDmMucLucNganSachService bhDmMucLucNganSachService
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
            _phongBanService = nsPhongBanService;
            _kinhPhiQuanLyService = kinhPhiQuanLyService;
            _kinhPhiQuanLyChiTietService = kinhPhiQuanLyChiTiet;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;

            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            _nsDonViService = nsDonViService;
        }
        #endregion

        #region  Init
        public override void Init()
        {
            try
            {
                base.Init();
                InitReportDefaultDate();
                _sessionInfo = _sessionService.Current;
                _isInitReport = true;
                YearOfWork = _sessionInfo.YearOfWork;
                _agencies = new ObservableCollection<AgencyModel>();
                IsSummary = false;
                IsSummaryAgency = false;
                IsDataInterpretation = false;
                ShowIsTongHop();
                LoadTieuDe();
                LoadReportType();
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
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() }
            };
            _selectedReportType = _reportTypes.First();
        }

        private void LoadTieuDe()
        {
            _typeChuKy = SettlementTypeValue switch
            {
                (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM => TypeChuKy.RPT_BH_QTC_NKPQL_CHITIET,
                _ => TypeChuKy.RPT_BH_QTC_NKPQL_CHITIET_PHULUC
            };
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

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
                    (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM => SettlementTitle.Title1QTCNKPQLPhuLuc,
                    _ => SettlementTitle.Title1QTCNKPQLKeHoach
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
                    (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM => SettlementTitle.Title2QTCNKPQLPhuLuc,
                    _ => ""
                };
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        #region Add chu ky
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

        #region Load data

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

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var yearOfWork = _sessionInfo.YearOfWork;
                List<DonVi> lstDonVis = new List<DonVi>();

                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    lstDonVis = _kinhPhiQuanLyService.FindByDonViForNamLamViec(yearOfWork, SettlementTypeLoaiChungTu.ChungTu).ToList();
                    lstDonVis = lstDonVis.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();

                }
                else
                {
                    lstDonVis = _kinhPhiQuanLyService.FindByDonViForNamLamViec(yearOfWork, SettlementTypeLoaiChungTu.ChungTuTongHop).ToList();
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

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            DateTime dtime = DateTime.Now;
            List<BhDmMucLucNganSach> listMLNS = new List<BhDmMucLucNganSach>();

            string agencyIds = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
            if (agencyIds.Length > 0)
            {
                listMLNS = _bhDmMucLucNganSachService.FindSLNSForQTCNKPQL(yearOfWork, agencyIds, dtime, _sessionInfo.Principal).ToList();
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
        #endregion

        #region Export Data
        private void ExportFile(bool exportType)
        {
            int typePrint = (int)SettlementTypeValue;
            switch (typePrint)
            {
                case (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM:
                    OnPrintReportPhucLuc(exportType);
                    break;
                case (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN:
                    OnPrintReportKeHoach(exportType);
                    break;
                default:
                    break;
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

        private void LoadKieuGiay()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG_A3, DisplayItem = LoaiGiay.NGANG_A3 });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();
        }

        private void OnPrintReportKeHoach(bool exportType)
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

                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType ? ExportType.PDF : ExportType.EXCEL);
                    QtcNamKinhPhiQuanLyCriteria searchCondition = new QtcNamKinhPhiQuanLyCriteria();
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        foreach (var item in Agencies.Where(n => n.Selected))
                        {
                            List<BhQtcNamKinhPhiQuanLyChiTietQuery> lstData = new List<BhQtcNamKinhPhiQuanLyChiTietQuery>();
                            DonVi donViChild = _donViService.FindByIdDonVi(item.Id, yearOfWork);

                            searchCondition.IDMaDonVi = item.Id;
                            searchCondition.NamLamViec = yearOfWork;
                            searchCondition.SLNS = LNSValue.LNS_9010003;
                            searchCondition.DonViTinh = donViTinh;
                            searchCondition.LoaiChungTu = BhxhLoaiChungTu.BhxhChungTu;
                            lstData = _kinhPhiQuanLyChiTietService.FindGetReportKeHoach(searchCondition).ToList();
                            var lstDataKeHoach = _mapper.Map<List<BhQtcNamKinhPhiQuanLyChiTietModel>>(lstData);
                            CalculateData(lstDataKeHoach);
                            FormatDisplay(lstDataKeHoach);

                            lstDataKeHoach = lstDataKeHoach.Where(x => x.IsHasData).ToList();

                            var SumFTienDuToanNamTruocChuyenSang = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTien_DuToanNamTruocChuyenSang).FirstOrDefault();
                            var SumFTienDuToanGiaoNamNay = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTien_DuToanGiaoNamNay).FirstOrDefault();
                            var SumFTienThucChi = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTien_ThucChi).FirstOrDefault();
                            var SumFTongDuToanDuocGiao = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTien_TongDuToanDuocGiao).FirstOrDefault();
                            var SumTienThua = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTienThua).FirstOrDefault();
                            var SumFTienThieu = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTienThieu).FirstOrDefault();
                            var SumTile = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTiLeThucHienTrenDuToan).FirstOrDefault();

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                            data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                            data.Add("TieuDe1", Title1 ?? SettlementTitle.Title1QTCNKPQLKeHoach);
                            data.Add("SumFTienDuToanNamTruocChuyenSang", SumFTienDuToanNamTruocChuyenSang);
                            data.Add("SumFTienDuToanGiaoNamNay", SumFTienDuToanGiaoNamNay);
                            data.Add("SumFTienThucChi", SumFTienThucChi);
                            data.Add("SumFTongDuToanDuocGiao", SumFTongDuToanDuocGiao);
                            data.Add("SumFTienThieu", SumFTienThieu);
                            data.Add("SumTienThua", SumTienThua);
                            data.Add("SumTile", SumTile);
                            data.Add("YearOfWork", "Năm " + _sessionInfo.YearOfWork);
                            data.Add("DonVi", item.AgencyName);

                            data.Add("ListData", lstDataKeHoach);
                            data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                            data.Add("IsAggregate", false);
                            data.Add("TienBangChu", StringUtils.NumberToText((SumFTienThucChi != null ? SumFTienThucChi.Value : 0) * donViTinh, true));
                            AddChuKy(data);
                            data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate))); 
                            string templateFileName;
                            templateFileName = GetTemplate();
                            ExcelFile xlsFile = _exportService.Export<BhQtcNamKinhPhiQuanLyChiTietModel>(templateFileName, data);
                            //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                            string fileNameWithoutExtension = string.Format("rptBH_QTC_NKPQL_ChungTu_ChiTiet_{0}_{1}", item.Id, DateTime.Now.ToStringTimeStamp());
                            results.Add(new ExportResult(item.AgencyName, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    else
                    {
                        searchCondition.LoaiChungTu = BhxhLoaiChungTu.BhxhChungTu;
                        var lstIdDonViCheckd = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id).ToList());
                        if (IsInTheoTongHop)
                        {
                            searchCondition.LoaiChungTu = BhxhLoaiChungTu.BhxhChungTuTongHop;
                            //var lstChungTu = _kinhPhiQuanLyService.FindIndex(yearOfWork).Where(x => x.IID_MaDonVi == lstIdDonViCheckd).FirstOrDefault();
                            //if (!string.IsNullOrEmpty(lstChungTu.STongHop))
                            //{
                            //    var sSoChungTu = lstChungTu.STongHop.Split(",");
                            //    var lstChungTuChild = _kinhPhiQuanLyService.FindIndex(yearOfWork).Where(x => sSoChungTu.Contains(x.SSoChungTu)).ToList();
                            //    var lsdMaDonVi = lstChungTuChild.Select(x => x.IID_MaDonVi).Distinct().ToList();
                            //    lstIdDonViCheckd = string.Join(",", lsdMaDonVi);
                            //}
                        }

                        List<BhQtcNamKinhPhiQuanLyChiTietQuery> lstData = new List<BhQtcNamKinhPhiQuanLyChiTietQuery>();

                        DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, yearOfWork);
                        searchCondition.IDMaDonVi = lstIdDonViCheckd;
                        searchCondition.NamLamViec = yearOfWork;
                        searchCondition.SLNS = string.Join(",", LNSValue.LNS_9010003);
                        searchCondition.DonViTinh = donViTinh;

                        lstData = _kinhPhiQuanLyChiTietService.FindGetReportKeHoach(searchCondition).ToList();
                        var lstDataKeHoach = _mapper.Map<List<BhQtcNamKinhPhiQuanLyChiTietModel>>(lstData);
                        CalculateData(lstDataKeHoach);
                        FormatDisplay(lstDataKeHoach);
                        lstDataKeHoach = lstDataKeHoach.Where(x => x.IsHasData).ToList();
                        var SumFTienDuToanNamTruocChuyenSang = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTien_DuToanNamTruocChuyenSang).FirstOrDefault();
                        var SumFTienDuToanGiaoNamNay = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTien_DuToanGiaoNamNay).FirstOrDefault();
                        var SumFTienThucChi = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTien_ThucChi).FirstOrDefault();
                        var SumFTongDuToanDuocGiao = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTien_TongDuToanDuocGiao).FirstOrDefault();
                        var SumTienThua = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTienThua).FirstOrDefault();
                        var SumFTienThieu = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTienThieu).FirstOrDefault();
                        var SumTile = lstDataKeHoach.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).Select(x => x.FTiLeThucHienTrenDuToan).FirstOrDefault();

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("TieuDe1", Title1 ?? SettlementTitle.Title1QTCNKPQLKeHoach);
                        data.Add("SumFTienDuToanNamTruocChuyenSang", SumFTienDuToanNamTruocChuyenSang);
                        data.Add("SumFTienDuToanGiaoNamNay", SumFTienDuToanGiaoNamNay);
                        data.Add("SumFTienThucChi", SumFTienThucChi);
                        data.Add("SumFTongDuToanDuocGiao", SumFTongDuToanDuocGiao);
                        data.Add("SumFTienThieu", SumFTienThieu);
                        data.Add("SumTienThua", SumTienThua);
                        data.Add("SumTile", SumTile);
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("YearOfWork", "Năm " + _sessionInfo.YearOfWork);
                        data.Add("ListData", lstDataKeHoach);
                        data.Add("IsAggregate", true);
                        data.Add("Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "");
                        data.Add("TienBangChu", StringUtils.NumberToText(SumFTienThucChi.Value * donViTinh, true));
                        AddChuKy(data);
                        data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                        string templateFileName;
                        templateFileName = GetTemplate();
                        ExcelFile xlsFile = _exportService.Export<BhQtcNamKinhPhiQuanLyChiTietModel>(templateFileName, data);
                        //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                        string fileNameWithoutExtension = string.Format("rptBH_QTC_NKPQL_ChungTu_ChiTiet_{0}_{1}", _sessionInfo.IdDonVi, DateTime.Now.ToStringTimeStamp());
                        results.Add(new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile));
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType ? ExportType.PDF : ExportType.EXCEL);
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

        private string GetTemplate()
        {
            string input = "";
            if (SettlementTypeValue == (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QTC_NKPQL_CHITIET);
            }
            else
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QTC_NKPQL_CHUNGTU_DONVI_PHULUC_BHXH);
            }

            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG_A3)
            {
                return Path.Combine(ExportPrefix.PATH_BH_QTC_NAMKPQL, input + "_A3" + FileExtensionFormats.Xlsx);
            }
            else
            {
                return Path.Combine(ExportPrefix.PATH_BH_QTC_NAMKPQL, input + FileExtensionFormats.Xlsx);
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

        private void OnPrintReportPhucLuc(bool exportType)
        {
            try
            {
                string listDonVi = "";
                listDonVi = string.Join(",", Agencies.Where(item => item.Selected).Select(x => x.Id).ToList());
                if (listDonVi == "")
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var donViCurrent = GetDonViOfCurrentUser();
                    List<ExportResult> results = new List<ExportResult>();

                    listDonVi = string.Join(",", Agencies.Where(item => item.Selected).Select(x => x.Id).ToList());
                    string templateFileName;
                    int stt = 1;

                    _listPhuLuc = _kinhPhiQuanLyChiTietService.FindGetReportPhuLuc(yearOfWork, listDonVi, donViTinh).ToList();

                    //_listPhuLuc.ForAll(x =>
                    //{
                    //    x.STT = stt;
                    //    stt++;

                    //});

                    var sumFTienDaThucHienNamTruoc = _listPhuLuc.Sum(x => x.FTienDaThucHienNamTruoc);
                    var sumFTienNamNay = _listPhuLuc.Sum(x => x.FTienNamNay);
                    var sumFTienQuyetToan = _listPhuLuc.Sum(x => x.FTienQuyetToan);
                    var sumFTienThieu = _listPhuLuc.Sum(x => x.FTienThieu);
                    var sumFTienThua = _listPhuLuc.Sum(x => x.FTienThua);
                    var sumFTienCong = _listPhuLuc.Sum(x => x.FTienCong);
                    var TongSoTien = _listPhuLuc.Sum(x => x.FTienQuyetToan);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("TieuDe1", Title1 ?? SettlementTitle.Title1QTCNKPQLPhuLuc);
                    data.Add("TieuDe2", Title2 ?? SettlementTitle.Title2QTCNKPQLPhuLuc);
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


                    templateFileName = GetTemplate();
                    ExcelFile xlsFile = _exportService.Export<ReportBHQTCNKPQuanLyPhuLucQuery, DonViModel>(templateFileName, data);
                    //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                    string fileNameWithoutExtension = string.Format("rptBH_QTC_NKPQL_DonVi_{0}_{1}", donViCurrent.TenDonVi, DateTime.Now.ToStringTimeStamp());
                    results.Add(new ExportResult(donViCurrent.TenDonVi, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType ? ExportType.PDF : ExportType.EXCEL);
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
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private void FormatDisplay(List<BhQtcNamKinhPhiQuanLyChiTietModel> lstDataKeHoach)
        {
            foreach (var item in lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.STM)))
            {
                var parent = lstDataKeHoach.FirstOrDefault(x => x.IID_MucLucNganSach == item.IdParent);
                if (parent != null && parent.SM != string.Empty)
                {
                    item.SM = string.Empty;

                }
            }
            foreach (var item in lstDataKeHoach.Where(x => !string.IsNullOrEmpty(x.STTM)))
            {
                var parent = lstDataKeHoach.FirstOrDefault(x => x.IID_MucLucNganSach == item.IdParent);
                if (parent != null && parent.STM != string.Empty)
                {
                    item.STM = string.Empty;
                    item.SM = string.Empty;
                }
            }
        }

        private void CalculateData(List<BhQtcNamKinhPhiQuanLyChiTietModel> lstDataKeHoach)
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

                var temp = lstDataKeHoach.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
                var dictByMlns = lstDataKeHoach.GroupBy(x => x.IID_MucLucNganSach).ToDictionary(x => x.Key, x => x.First());
                foreach (var item in temp)
                {
                    CalculateParent(item.IdParent, item, dictByMlns);
                }

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParent(Guid idParent, BhQtcNamKinhPhiQuanLyChiTietModel item, Dictionary<Guid?, BhQtcNamKinhPhiQuanLyChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            //model.FTien_DuToanNamTruocChuyenSang += item.FTien_DuToanNamTruocChuyenSang;
            //model.FTien_DuToanGiaoNamNay += item.FTien_DuToanGiaoNamNay;
            //model.FTien_TongDuToanDuocGiao += item.FTien_TongDuToanDuocGiao;
            model.FTien_ThucChi += item.FTien_ThucChi;

            CalculateParent(model.IdParent, item, dictByMlns);
        }
        #endregion
    }
}
