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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport
{
    public class PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel : ViewModelBase
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsDonViService _nsDonViService;
        private readonly ILog _logger;
        private readonly INsPhongBanService _phongBanService;
        private readonly IBhThamDinhQuyetToanService _chungTuService;
        private readonly IBhQtcNamKinhPhiQuanLyChiTietService _kinhPhiQuanLyChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;
        private ICollectionView _listAgency;
        private ICollectionView _listLNSView;
        #endregion

        #region Property
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private List<ReportBHQTCNKPQuanLyPhuLucQuery> _listPhuLuc;
        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> _reportData;
        private string _typeChuKy;
        private bool _checkAllAgencies;
        public int SettlementTypeValue { get; set; }
        public bool IsShowAll { get; set; }
        public override Type ContentType => typeof(PrintBaoCaoQuyetToanChiKinhPhiQuanLy);

        private string SettlementName => SettlementTypeValue switch
        {
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_QUAN_LY_BHXH_BHYT => "Báo cáo chỉ tiêu quyết toán chi kinh phí quản lý BHXH, BHYT",
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_TRUONG_SA_DK => "Báo cáo chỉ tiêu quyết toán chi kinh phí KCB tại Trường Sa - DK",
            _ => ""
        };

        public override string Name => SettlementName;
        public override string Title => SettlementName;

        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
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

        private int _yearOfWork;
        public int YearOfWork { get => _yearOfWork; set => SetProperty(ref _yearOfWork, value); }
        #endregion

        #region RelayCommand
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }
        #endregion

        #region Constructor
        public PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel(
                    IMapper mapper,
                    ISessionService sessionService,
                    ILog logger,
                    IDanhMucService danhMucService,
                    IExportService exportService,
                    IDmChuKyService dmChuKyService,
                    DmChuKyDialogViewModel dmChuKyDialogViewModel,
                    IBhQtcNamKinhPhiQuanLyChiTietService kinhPhiQuanLyChiTiet,
                    INsDonViService nsDonViService,
                    INsPhongBanService nsPhongBanService,
                    IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                    IBhThamDinhQuyetToanService iBhThamDinhQuyetToanService,
                    IBhBaoCaoGhiChuService iBhBaoCaoGhiChuService,
                    BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel
            )
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            _chungTuService = iBhThamDinhQuyetToanService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _phongBanService = nsPhongBanService;
            _kinhPhiQuanLyChiTietService = kinhPhiQuanLyChiTiet;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhGhiChuService = iBhBaoCaoGhiChuService;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
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
                YearOfWork = _sessionInfo.YearOfWork;
                _agencies = new ObservableCollection<AgencyModel>();
                IsDataInterpretation = false;
                LoadTieuDe();
                LoadReportType();
                LoadDanhMuc();
                LoadBQuanLy();
                LoadAgencies();
                IsShowAll = _sessionInfo.YearOfBudget == 1 || _sessionInfo.YearOfBudget == 4;
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

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
            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_QUAN_LY_BHXH_BHYT)
            {
                _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPQL_CHITIET;
                Title1 = "Phụ lục IX";
                Title2 = SettlementTitle.Title1QTCNKPQLPhuLuc + " Năm" + _sessionInfo.YearOfWork;
            }
            else if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_TRUONG_SA_DK)
            {
                _typeChuKy = TypeChuKy.RPT_BH_QTC_NKPK_TSDK_DONVI_PHULUC;
                Title1 = "Phụ lục XI";
                Title2 = SettlementTitle.Title2QTCNKPKTSDKPhuLuc + " Năm" + _sessionInfo.YearOfWork;
            }

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
            {
                Title1 = _dmChuKy.TieuDe1MoTa;
            }

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
            {
                Title2 = _dmChuKy.TieuDe2MoTa;
            }

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
            {
                Title3 = _dmChuKy.TieuDe3MoTa;
            }
            else
            {
                Title3 = SettlementTitle.Title2QTCNKPQLPhuLuc;
            }
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

        private void OnNoteCommand()
        {
            var maBaoCao = "";
            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_QUAN_LY_BHXH_BHYT)
            {
                maBaoCao = TypeChuKy.RPT_BH_QTC_NKPQL_CHITIET;
            }
            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_TRUONG_SA_DK)
            {
                maBaoCao = TypeChuKy.RPT_BH_QTC_NKPK_TSDK_DONVI_PHULUC;
            }

            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { maBaoCao };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuKy;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("QTChiKPQLDialog");
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
        }

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var lstIdDonVi = GetListIdDonVi();
                List<DonVi> agencies = _nsDonViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                {
                    agencies = agencies.Where(x => x.Loai != LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
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

        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            List<BhThamDinhQuyetToan> listChungTuDuocXem = new List<BhThamDinhQuyetToan>();
            listChungTuDuocXem = _chungTuService.FindUnitVoucher(yearOfWork).ToList();
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
            OnPrintReportPhucLuc(exportType);
        }

        private string GetTemplate()
        {
            string input = "";
            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_QUAN_LY_BHXH_BHYT)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_CHI_KPQL);
            }

            if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_TRUONG_SA_DK)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_CHI_KPK_TRUONGSADK);
            }
            return Path.Combine(ExportPrefix.PATH_BH_THAMDINHQUYETTOAN, input + FileExtensionFormats.Xlsx);
        }

        public void AddNote(Dictionary<string, object> data, string idType, string idMaDonVi = null)
        {
            BhCauHinhBaoCao bhGhiChu;
            var predicate = PredicateBuilder.True<BhCauHinhBaoCao>();
            predicate = predicate.And(x => x.SMaBaoCao == idType && x.INamLamViec == _sessionInfo.YearOfWork);

            if (string.IsNullOrEmpty(idMaDonVi))
            {
                predicate = predicate.And(x => x.ILoaiBaoCao == (int)NoteTypeBhxh.AgencySummary);
            }
            else
            {
                var listDV = idMaDonVi.Split(",");
                predicate = predicate.And(x => x.ILoaiBaoCao == (int)NoteTypeBhxh.AgencyDetail && listDV.Contains(x.IIdMaDonVi));
            }
            bhGhiChu = _bhGhiChuService.FindByCondition(predicate).FirstOrDefault();
            data.Add("GhiChu", bhGhiChu == null ? string.Empty : bhGhiChu.SGhiChu);
            data.Add("TieuDeGhiChu", bhGhiChu == null ? "" : "Ghi chú: ");
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
                    _listPhuLuc = _kinhPhiQuanLyChiTietService.FindGetReportQTKPQL_KPKTSDK(yearOfWork, listDonVi, SettlementTypeValue).ToList();
                    _listPhuLuc.ForAll(x =>
                    {
                        x.FTienDaThucHienNamTruoc /= donViTinh;
                        x.FTienNamNay /= donViTinh;
                        x.FTienQuyetToan /= donViTinh;
                        x.FTienCong = x.FTienDaThucHienNamTruoc + x.FTienNamNay;
                        x.FTienThieu = x.FTienQuyetToan > x.FTienCong ? x.FTienQuyetToan - x.FTienCong : 0;
                        x.FTienThua = x.FTienQuyetToan < x.FTienCong ? x.FTienCong - x.FTienQuyetToan : 0;
                    });

                    var sumFTienDaThucHienNamTruoc = _listPhuLuc.Sum(x => x.FTienDaThucHienNamTruoc);
                    var sumFTienNamNay = _listPhuLuc.Sum(x => x.FTienNamNay);
                    var sumFTienQuyetToan = _listPhuLuc.Sum(x => x.FTienQuyetToan);
                    var sumFTienThieu = _listPhuLuc.Sum(x => x.FTienThieu);
                    var sumFTienThua = _listPhuLuc.Sum(x => x.FTienThua);
                    var sumFTienCong = _listPhuLuc.Sum(x => x.FTienCong);
                    var TongSoTien = _listPhuLuc.Sum(x => x.FTienQuyetToan);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType ? ExportType.PDF : ExportType.EXCEL);
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

                    ExtensionMethods.CheckPassElementOrGetDefault10Element(_listPhuLuc);
                    int stt = 1;
                    foreach (var i in _listPhuLuc.Where(x => x.HasData.GetValueOrDefault()))
                    {
                        i.STT = stt++;
                    }

                    AddNote(data, _typeChuKy, listDonVi);
                    templateFileName = GetTemplate();
                    ExcelFile xlsFile = _exportService.Export<ReportBHQTCNKPQuanLyPhuLucQuery, DonViModel>(templateFileName, data);
                    xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
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
        #endregion
    }
}

