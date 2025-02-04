using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.PrintReport;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.Service.UserFunction;
using System.IO;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.PrintReport
{
    public class CapPhatBoSungReportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listCsYTeView;
        private ICollectionView _listLNSView;
        private readonly IMapper _mapper;
        private IExportService _exportService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private readonly IBhCpBsChungTuService _cpbsBHYTService;
        private readonly IBhCpBsChungTuChiTietService _cpbsBHYTChiTietService;
        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;
        public BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; }

        private ILog _logger;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private string _quarterMonth;
        private int _quarterMonthType;
        private string _quarterMonthBefore;
        private string _typeChuky;
        public override string Name => "IN BÁO CÁO CẤP PHÁT BỔ SUNG KP KCB BHYT";
        public override string Title { get; set; }
        public override string Description { get; set; }
        public override Type ContentType => typeof(CapPhatBoSungReport);
        public static ReportCPBSKCBBHYT ReportTypeInput { get; set; }

        private bool _isEnableLNS;
        public bool IsEnableLNS
        {
            get => _isEnableLNS;
            set => SetProperty(ref _isEnableLNS, value);
        }

        private bool _isEnableKehoach;
        public bool IsEnableKehoach
        {
            get => _isEnableKehoach;
            set => SetProperty(ref _isEnableKehoach, value);
        }

        private bool _isEnableTongTop;
        public bool IsEnableTongTop
        {
            get => _isEnableTongTop;
            set => SetProperty(ref _isEnableTongTop, value);
        }

        private bool _isEnableThongTri;
        public bool IsEnableThongTri
        {
            get => _isEnableThongTri;
            set => SetProperty(ref _isEnableThongTri, value);
        }

        private bool _printOnePage;
        public bool PrintOnePage
        {
            get => _printOnePage;
            set => SetProperty(ref _printOnePage, value);
        }

        private List<ComboboxItem> _monthAndQuarters;
        public List<ComboboxItem> MonthAndQuarters
        {
            get => _monthAndQuarters;
            set => SetProperty(ref _monthAndQuarters, value);
        }

        private ComboboxItem _monthAndQuartersSelected;
        public ComboboxItem MonthAndQuartersSelected
        {
            get => _monthAndQuartersSelected;
            set
            {
                SetProperty(ref _monthAndQuartersSelected, value);
                LoadCsYTe();
                OnPropertyChanged(nameof(ItemsReportType));
            }
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = ListLNS != null ? ListLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListLNS != null ? ListLNS.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        public string SelectedCountCsYTe
        {
            get
            {
                int totalCount = ListCsYTe != null ? ListCsYTe.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListCsYTe != null ? ListCsYTe.Count(item => item.IsChecked) : 0;
                return string.Format("CƠ SỞ Y TẾ ({0}/{1})", totalSelected, totalCount);
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

        private string _searchCsYTe;
        public string SearchCsYTe
        {
            get => _searchCsYTe;
            set
            {
                if (SetProperty(ref _searchCsYTe, value))
                {
                    _listCsYTeView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountCsYTe));
                }
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => ListLNS.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                foreach (CheckBoxItem item in ListLNS.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllLNS;
                }
            }
        }

        private bool _selectAllCsYTe;
        public bool SelectAllCsYTe
        {
            get => ListCsYTe.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllCsYTe, value);
                foreach (CheckBoxItem item in ListCsYTe.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllCsYTe;
                }
            }
        }

        private ObservableCollection<CheckBoxItem> _listCsYTe;
        public ObservableCollection<CheckBoxItem> ListCsYTe
        {
            get => _listCsYTe;
            set => SetProperty(ref _listCsYTe, value);
        }

        private ObservableCollection<CheckBoxTreeItem> _listLNS;
        public ObservableCollection<CheckBoxTreeItem> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        public bool IsExportEnable;


        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
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

        private ReportCPBSKCBBHYT _reportType;
        public ReportCPBSKCBBHYT ReportType
        {
            get => _reportType;
            set
            {
                SetProperty(ref _reportType, value);
                LoadTypeChuKy();
                LoadTieuDe();
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

        private List<ComboboxItem> _itemsReportType;
        public List<ComboboxItem> ItemsReportType
        {
            get => _itemsReportType;
            set => SetProperty(ref _itemsReportType, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                if (SetProperty(ref _selectedReportType, value) && value != null)
                {
                    ReportType = AllocationReportType.AllocationType[value.ValueItem];
                    LoadTitle2();
                    LoadCsYTe();
                    OnPropertyChanged(nameof(ReportType));
                    OnPropertyChanged(nameof(Title2));
                    OnPropertyChanged(nameof(ItemsReportType));
                }
            }
        }


        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }
        public CapPhatBoSungReportViewModel(INsDonViService donViService,
            ISessionService sessionService,
            IMapper mapper,
            IExportService exportService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            IBhCpBsChungTuService bhCpBsChungTuService,
            IBhCpBsChungTuChiTietService bhCpBsChungTuChiTietService,
            ILog logger,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _cpbsBHYTService = bhCpBsChungTuService;
            _cpbsBHYTChiTietService = bhCpBsChungTuChiTietService;
            _logger = logger;
            _bhGhiChuService = bhBaoCaoGhiChuService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;

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
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            if (!IsEnableKehoach)
            {
                _reportType = ReportCPBSKCBBHYT.KEHOACH_TNQN;
                Title = "In kế hoạch cấp bổ sung KP KCB BHYT";
                Description = "In kế hoạch cấp bổ sung KP KCB BHYT";
            }
            else if (!IsEnableTongTop)
            {
                _reportType = ReportCPBSKCBBHYT.TONGHOP_TNQN;
                Title = "In thông tri tổng hợp cấp bổ sung KP KCB BHYT";
                Description = "In thông tri tổng hợp cấp bổ sung KP KCB BHYT";
            }
            else
            {
                _reportType = ReportCPBSKCBBHYT.THONGTRI_TNQN;
                Title = "In thông tri chi tiết cấp bổ sung KP KCB BHYT";
                Description = "In thông tri chi tiết cấp bổ sung KP KCB BHYT";
            }

            InitReportDefaultDate();
            LoadReportType();
            LoadTypeChuKy();
            LoadTieuDe();
            LoadMonthsAndQuarters();
            LoadDanhMuc();
            LoadLNS();
            LoadCsYTe();
        }

        #region Note
        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>()
            { TypeChuKy.RPT_BHXH_CPBS_QN, TypeChuKy.RPT_BHXH_CPBS_TNQN_NLD, TypeChuKy.RPT_BHXH_CPBS_TONG_HOP_THONG_TRI_TNQN, TypeChuKy.RPT_BHXH_CPBS_TONG_HOP_THONG_TRI_TNQN_NLD, TypeChuKy.RPT_BHXH_CPBS_THONG_TRI_TNQN_NLD, TypeChuKy.RPT_BHXH_CPBS_THONG_TRI_TNQN };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(ListCsYTe);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.IsShowCanCu = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }
        #endregion

        private void LoadReportType()
        {
            List<ComboboxItem> lstType;
            switch (_reportType)
            {
                case ReportCPBSKCBBHYT.KEHOACH_TNQN:
                    lstType = new List<ComboboxItem>()
                    {
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPBSKCBBHYT.KEHOACH_TNQN).ToString(),
                                DisplayItem = AllocationReportType.AllocationReportTypeName[ReportCPBSKCBBHYT.KEHOACH_TNQN],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN)

                            },
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPBSKCBBHYT.KEHOACH_TNQN_NLD).ToString(),
                                DisplayItem = AllocationReportType.AllocationReportTypeName[ReportCPBSKCBBHYT.KEHOACH_TNQN_NLD],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD)
                            }
                    };
                    break;
                case ReportCPBSKCBBHYT.TONGHOP_TNQN:
                    lstType = new List<ComboboxItem>()
                    {
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPBSKCBBHYT.TONGHOP_TNQN).ToString(),
                                DisplayItem = AllocationReportType.AllocationReportTypeName[ReportCPBSKCBBHYT.TONGHOP_TNQN],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN)
                            },
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPBSKCBBHYT.TONGHOP_TNQN_NLD).ToString(),
                                DisplayItem = AllocationReportType.AllocationReportTypeName[ReportCPBSKCBBHYT.TONGHOP_TNQN_NLD],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD)
                            }
                    };
                    break;
                case ReportCPBSKCBBHYT.THONGTRI_TNQN:
                    lstType = new List<ComboboxItem>()
                    {
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPBSKCBBHYT.THONGTRI_TNQN).ToString(),
                                DisplayItem = AllocationReportType.AllocationReportTypeName[ReportCPBSKCBBHYT.THONGTRI_TNQN],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN)
                            },
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPBSKCBBHYT.THONGTRI_TNQN_NLD).ToString(),
                                DisplayItem = AllocationReportType.AllocationReportTypeName[ReportCPBSKCBBHYT.THONGTRI_TNQN_NLD],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD)

                            }
                    };
                    break;
                default:
                    lstType = new List<ComboboxItem>();
                    break;
            }
            ItemsReportType = new List<ComboboxItem>(lstType);
            SelectedReportType = ItemsReportType.FirstOrDefault(x => x.ValueItem.Equals(((int)ReportType).ToString()));
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            string idDonVi = _sessionInfo.IdDonVi;

            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate = predicate.And(n => n.SXauNoiMa.StartsWith(BhxhMLNS.KCB_BHYT) && n.INamLamViec == yearOfWork);
            var listLNSPrev = _bhDmMucLucNganSachService.FindByCondition(predicate).ToList().OrderBy(n => n.SXauNoiMa);

            ListLNS = new ObservableCollection<CheckBoxTreeItem>();
            ListLNS = _mapper.Map<ObservableCollection<CheckBoxTreeItem>>(listLNSPrev);
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
                        OnPropertyChanged(nameof(SelectedCountLNS));
                        OnPropertyChanged(nameof(SelectAllLNS));
                    }
                };
            }
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

        public void LoadCsYTe()
        {
            ListCsYTe = new ObservableCollection<CheckBoxItem>();

            var predicate = PredicateBuilder.True<BhDmCoSoYTe>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);

            IEnumerable<BhDmCoSoYTe> listDmYte = _bhDmCoSoYTeService.FindByCondition(predicate);
            if (SelectedReportType != null && MonthAndQuartersSelected != null)
            {
                List<string> lstMa = new List<string>();
                IEnumerable<string> lstMaCoSoKT;
                if (_reportType == ReportCPBSKCBBHYT.KEHOACH_TNQN)
                {
                    lstMaCoSoKT = _cpbsBHYTChiTietService.GetMaCoSoYTeDetailByCondition(int.Parse(MonthAndQuartersSelected.ValueItem), _sessionInfo.YearOfWork, SelectedReportType.HiddenValue, !IsEnableTongTop, AllocationFunction.CAP_BO_SUNG, true);
                }
                else
                {
                    lstMaCoSoKT = _cpbsBHYTChiTietService.GetMaCoSoYTeDetailByCondition(int.Parse(MonthAndQuartersSelected.ValueItem), _sessionInfo.YearOfWork, SelectedReportType.HiddenValue, !IsEnableTongTop, AllocationFunction.CAP_BO_SUNG);
                }

                if (lstMaCoSoKT != null && lstMaCoSoKT.Count() > 0)
                {
                    foreach (var item in lstMaCoSoKT)
                    {
                        string[] numbers = item.Split(",");
                        lstMa.AddRange(numbers);
                    }
                }

                if (!listDmYte.IsEmpty())
                {
                    listDmYte = listDmYte.Where(x => lstMa.Contains(x.IIDMaCoSoYTe)).OrderBy(o => o.IIDMaCoSoYTe);
                }
            }
            ListCsYTe = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDmYte);
            // Filter
            _listCsYTeView = CollectionViewSource.GetDefaultView(ListCsYTe);
            _listCsYTeView.Filter = ListDonViFilter;
            foreach (var model in ListCsYTe)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountCsYTe));
                        OnPropertyChanged(nameof(SelectAllCsYTe));
                    }
                };
            }
        }

        private bool ListDonViFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchCsYTe))
                result = item.ValueItem.ToLower().Contains(_searchCsYTe!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            if (ReportType == ReportCPBSKCBBHYT.THONGTRI_TNQN && SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.THONGTRI_TNQN).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPBSReportTitle.THONG_TRI_CHI_TIET_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPBSReportTitle.THONG_TRI_CHI_TIET_QN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPBSReportTitle.THONG_TRI_CHI_TIET_QN_TITLE_3;
            }
            else if (ReportType == ReportCPBSKCBBHYT.THONGTRI_TNQN_NLD && SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.THONGTRI_TNQN_NLD).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPBSReportTitle.THONG_TRI_CHI_TIET_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPBSReportTitle.THONG_TRI_CHI_TIET_TNQN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPBSReportTitle.THONG_TRI_CHI_TIET_TNQN_TITLE_3;
            }
            else if (ReportType == ReportCPBSKCBBHYT.TONGHOP_TNQN && SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.TONGHOP_TNQN).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPBSReportTitle.THONG_TRI_TONG_HOP_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPBSReportTitle.THONG_TRI_TONG_HOP_QN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPBSReportTitle.THONG_TRI_TONG_HOP_QN_TITLE_3;
            }
            else if (ReportType == ReportCPBSKCBBHYT.TONGHOP_TNQN_NLD && SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.TONGHOP_TNQN_NLD).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPBSReportTitle.THONG_TRI_TONG_HOP_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPBSReportTitle.THONG_TRI_TONG_HOP_TNQN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPBSReportTitle.THONG_TRI_TONG_HOP_TNQN_TITLE_3;
            }
            else if (ReportType == ReportCPBSKCBBHYT.KEHOACH_TNQN && SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.KEHOACH_TNQN).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPBSReportTitle.KE_HOACH_CAP_BO_SUNG_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPBSReportTitle.KE_HOACH_CAP_BO_SUNG_QN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPBSReportTitle.KE_HOACH_CAP_BO_SUNG_QN_TITLE_3;
            }
            else if (ReportType == ReportCPBSKCBBHYT.KEHOACH_TNQN_NLD && SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.KEHOACH_TNQN_NLD).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPBSReportTitle.KE_HOACH_CAP_BO_SUNG_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPBSReportTitle.THONG_TRI_CHI_TIET_TNQN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPBSReportTitle.THONG_TRI_CHI_TIET_TNQN_TITLE_3;
            }
            else
            {
                if (_dmChuKy == null)
                    _dmChuKy = new DmChuKy();
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                    Title1 = _dmChuKy.TieuDe1MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                    Title2 = _dmChuKy.TieuDe2MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                    Title3 = _dmChuKy.TieuDe3MoTa;
            }
        }

        private void LoadTitle2()
        {
            Title2 = string.IsNullOrEmpty(Title2) ? string.Format(AllocationReportType.AllocationReportTitle2[SelectedReportType.ValueItem], MonthAndQuartersSelected?.ValueItem, _sessionInfo.YearOfWork) : Title2;
        }

        private void LoadMonthsAndQuarters()
        {
            _monthAndQuarters = new List<ComboboxItem>();
            _monthAndQuarters.Add(new ComboboxItem("Quý I", "1"));
            _monthAndQuarters.Add(new ComboboxItem("Quý II", "2"));
            _monthAndQuarters.Add(new ComboboxItem("Quý III", "3"));
            _monthAndQuarters.Add(new ComboboxItem("Quý IV", "4"));
            MonthAndQuartersSelected = _monthAndQuarters.First();
        }
        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
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
            if (!IsEnableKehoach)
            {
                ExportKeHoachCapPhatBoSungKCBBHYT(exportType);
            }
            else if (!IsEnableTongTop)
            {
                ExportTongHopCapPhatBoSungKCBBHYT(exportType);
            }
            else if (!IsEnableThongTri)
            {
                ExportThongTriCapPhatBoSungKCBBHYT(exportType);
            }
        }

        private void ExportKeHoachCapPhatBoSungKCBBHYT(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdCsYte = ListCsYTe.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                    string sIdCsYTe = string.Join(",", lstIdCsYte);
                    int iLoai = (int)ReportType;
                    int iQuy = int.Parse(MonthAndQuartersSelected.ValueItem);
                    string sXauNoiMa = "";
                    List<BhCpBsChungTuChiTietQuery> lstData = new List<BhCpBsChungTuChiTietQuery>();
                    if (SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.KEHOACH_TNQN).ToString())
                    {
                        sXauNoiMa = BhxhMLNS.KinhPhi_KCB_BHYT_QN;
                        lstData = _cpbsBHYTChiTietService.ExportKeHoachCapPhatBoSungKCBBHYT(sIdCsYTe, iQuy, yearOfWork, _sessionInfo.Principal, donViTinh, sXauNoiMa).ToList();
                    }
                    else
                    {
                        sXauNoiMa = BhxhMLNS.KinhPhi_KCB_BHYT_TNQN_NLD;
                        lstData = _cpbsBHYTChiTietService.ExportKeHoachCapPhatBoSungKCBBHYT(sIdCsYTe, iQuy, yearOfWork, _sessionInfo.Principal, donViTinh, sXauNoiMa).ToList();
                    }

                    var lstMaCSYTExisted = lstData.Select(x => x.IIDMaCoSoYTe).ToList();
                    foreach (var cs in ListCsYTe.Where(x => x.IsChecked))
                    {
                        if (!lstMaCSYTExisted.Contains(cs.ValueItem))
                        {
                            var newData = new BhCpBsChungTuChiTietQuery();
                            newData.IIDMaCoSoYTe = cs.ValueItem;
                            newData.STenCoSoYTe = cs.NameItem;
                            newData.FDaQuyetToan = double.Parse("0");
                            newData.FDaCapUng = double.Parse("0");
                            newData.FSoCapBoSung = double.Parse("0");
                            lstData.Add(newData);
                        }
                    }

                    lstData.OrderBy(x => x.IIDMaCoSoYTe);

                    int i = 1;
                    lstData.Select(x =>
                    {
                        x.STT = i;
                        i++;
                        return x;
                    }).ToList();

                    var fTongDaQuyetToan = lstData.Sum(x => x.FDaQuyetToan);
                    var fTongDaCapUng = lstData.Sum(x => x.FDaCapUng);
                    var fTongThuaThieu = lstData.Sum(x => x.FThuaThieu);
                    var fTongSoCapBoSung = lstData.Sum(x => x.FSoCapBoSung);
                    ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    data.Add("YearWork", yearOfWork);
                    data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                    data.Add("FTongDaQuyetToan", fTongDaQuyetToan);
                    data.Add("FTongDaCapUng", fTongDaCapUng);
                    data.Add("FTongThuaThieu", fTongThuaThieu);
                    data.Add("FTongSoCapBoSung", fTongSoCapBoSung);

                    data.Add("ListData", lstData);
                    data.Add("FormatNumber", formatNumber);

                    AddChuKy(data, _typeChuky);
                    data.Add("Title1", Title1);
                    data.Add("Title2", Title2);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Quy", MonthAndQuartersSelected.ValueItem);
                    data.Add("QuyTruoc", Convert.ToInt32(MonthAndQuartersSelected.ValueItem) == 1 ? 4 : Convert.ToInt32(MonthAndQuartersSelected.ValueItem) - 1);
                    data.Add("Year", _sessionInfo.YearOfWork);
                    data.Add("YearTruoc", Convert.ToInt32(MonthAndQuartersSelected.ValueItem) == 1 ? _sessionInfo.YearOfWork - 1 : _sessionInfo.YearOfWork);

                    var fTongTongTien = (fTongSoCapBoSung ?? 0); //(fTongDaQuyetToan ?? 0) + (fTongDaCapUng ?? 0) + (fTongThuaThieu ?? 0) + 
                    data.Add("TongSoTien", fTongTongTien != 0 ? StringUtils.NumberToText((double)fTongTongTien, true) : string.Empty);

                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));

                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhCpBsChungTuChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH CẤP BỔ SUNG KINH PHÍ TNQN " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void ExportTongHopCapPhatBoSungKCBBHYT(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdCsYte = ListCsYTe.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                    string sIdCsYTe = string.Join(",", lstIdCsYte);
                    int iLoai = (int)ReportType;
                    int iQuy = int.Parse(MonthAndQuartersSelected.ValueItem);
                    List<BhCpBsChungTuChiTietQuery> lstData = new List<BhCpBsChungTuChiTietQuery>();
                    string sXauNoiMa = "";
                    if (SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.TONGHOP_TNQN).ToString())
                    {
                        sXauNoiMa = BhxhMLNS.KinhPhi_KCB_BHYT_QN;
                        lstData = _cpbsBHYTChiTietService.ExportTongHopCapPhatBoSungKCBBHYT(sIdCsYTe, iQuy, yearOfWork, _sessionInfo.Principal, donViTinh, sXauNoiMa).ToList();

                    }
                    else
                    {
                        sXauNoiMa = BhxhMLNS.KinhPhi_KCB_BHYT_TNQN_NLD;
                        lstData = _cpbsBHYTChiTietService.ExportTongHopCapPhatBoSungKCBBHYT(sIdCsYTe, iQuy, yearOfWork, _sessionInfo.Principal, donViTinh, sXauNoiMa).ToList();
                    }

                    var fTongDaQuyetToan = lstData.Sum(x => x.FDaQuyetToan);
                    var fTongDaCapUng = lstData.Sum(x => x.FDaCapUng);
                    var fTongThuaThieu = lstData.Sum(x => x.FThuaThieu);
                    var fTongSoCapBoSung = lstData.Sum(x => x.FSoCapBoSung);
                    ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    data.Add("YearWork", yearOfWork);
                    data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                    data.Add("FTongDaQuyetToan", fTongDaQuyetToan);
                    data.Add("FTongDaCapUng", fTongDaCapUng);
                    data.Add("FTongThuaThieu", fTongThuaThieu);
                    data.Add("FTongSoCapBoSung", fTongSoCapBoSung);
                    data.Add("ListData", lstData);
                    data.Add("FormatNumber", formatNumber);
                    AddChuKy(data, _typeChuky);
                    data.Add("Title1", Title1);
                    data.Add("Title2", Title2);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Quy", MonthAndQuartersSelected.ValueItem);
                    data.Add("Year", _sessionInfo.YearOfWork);
                    var fTongTongTien = (fTongDaQuyetToan ?? 0) + (fTongDaCapUng ?? 0) + (fTongThuaThieu ?? 0) + (fTongSoCapBoSung ?? 0);
                    data.Add("TongSoTien", fTongSoCapBoSung != 0 ? StringUtils.NumberToText((double)fTongSoCapBoSung, true) : string.Empty);
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhCpBsChungTuChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("TỔNG HỢP CẤP BỔ SUNG KINH PHÍ TNQN " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void ExportThongTriCapPhatBoSungKCBBHYT(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdCsYte = ListCsYTe.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                    string sXauNoiMa = "";
                    if (SelectedReportType.ValueItem == ((int)ReportCPBSKCBBHYT.THONGTRI_TNQN).ToString())
                    {
                        sXauNoiMa = BhxhMLNS.KinhPhi_KCB_BHYT_QN;
                    }
                    else
                    {
                        sXauNoiMa = BhxhMLNS.KinhPhi_KCB_BHYT_TNQN_NLD;
                    }

                    foreach (var itemCSYT in lstIdCsYte)
                    {
                        var selectedCSYT = _bhDmCoSoYTeService.GetCSYTByMa(itemCSYT, yearOfWork);
                        int iQuy = int.Parse(MonthAndQuartersSelected.ValueItem);
                        List<BhCpBsChungTuChiTietQuery> lstData = new List<BhCpBsChungTuChiTietQuery>();
                        lstData = _cpbsBHYTChiTietService.ExportThongTriCapPhatBoSungKCBBHYT(itemCSYT, iQuy, yearOfWork, donViTinh, sXauNoiMa).ToList();
                        var fTongSoCapBoSung = lstData.Sum(x => x.FSoCapBoSung);
                        ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                        data.Add("YearWork", yearOfWork);
                        data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                        data.Add("FTongSoCapBoSung", fTongSoCapBoSung);

                        data.Add("ListData", lstData);
                        data.Add("FormatNumber", formatNumber);

                        string name = "";
                        if (ReportType == ReportCPBSKCBBHYT.THONGTRI_TNQN)
                        {
                            name = "CẤP BỔ SUNG KINH PHÍ KCB BHYT QUÂN NHÂN";
                        }
                        else
                        {
                            name = "CẤP BỔ SUNG KINH PHÍ KCB BHYT TNQN & NLĐ";
                        }
                        data.Add("Name", name);

                        AddChuKy(data, _typeChuky);
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Quy", MonthAndQuartersSelected.ValueItem);
                        data.Add("Year", _sessionInfo.YearOfWork);
                        data.Add("TongSoTien", fTongSoCapBoSung != 0 ? StringUtils.NumberToText((double)fTongSoCapBoSung, true) : string.Empty);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        data.Add("TenCSYT", selectedCSYT.STenCoSoYTe);
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhCpBsChungTuChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult("THÔNG TRI CẤP BỔ SUNG KINH PHÍ KCB BHYT TNQN " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                        e.Result = results;
                    }

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (PrintOnePage)
                        {
                            if (exportType == ExportType.PDF)
                            {
                                _exportService.Open(result, ExportType.PDF_ONE_PAPER);

                            }
                            else
                            {
                                _exportService.Open(result, ExportType.EXCEL_ONE_PAPER);
                            }
                        }
                        else
                        {
                            _exportService.Open(result, exportType);
                        }
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
            if (ReportType == ReportCPBSKCBBHYT.KEHOACH_TNQN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_KE_HOACH_CAP_TAM_UNG_TNQN_DOC);
            }
            else if (ReportType == ReportCPBSKCBBHYT.KEHOACH_TNQN_NLD)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_TONG_HOP_CAP_TAM_UNG_TNQN_NLD_DOC);
            }
            else if (ReportType == ReportCPBSKCBBHYT.TONGHOP_TNQN || ReportType == ReportCPBSKCBBHYT.TONGHOP_TNQN_NLD)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_TONG_HOP_THONG_TRI_CBS_DOC);
            }
            else if (ReportType == ReportCPBSKCBBHYT.THONGTRI_TNQN || ReportType == ReportCPBSKCBBHYT.THONGTRI_TNQN_NLD)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THONG_TRI_CBS_DOC);
            }
            return Path.Combine(ExportPrefix.PATH_BH_CPBS, input + FileExtensionFormats.Xlsx);
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
        }

        private void LoadTypeChuKy()
        {
            switch (ReportType)
            {
                case ReportCPBSKCBBHYT.KEHOACH_TNQN:
                    _typeChuky = TypeChuKy.RPT_BHXH_CPBS_QN;
                    break;
                case ReportCPBSKCBBHYT.KEHOACH_TNQN_NLD:
                    _typeChuky = TypeChuKy.RPT_BHXH_CPBS_TNQN_NLD;
                    break;
                case ReportCPBSKCBBHYT.TONGHOP_TNQN:
                    _typeChuky = TypeChuKy.RPT_BHXH_CPBS_TONG_HOP_THONG_TRI_TNQN;
                    break;
                case ReportCPBSKCBBHYT.TONGHOP_TNQN_NLD:
                    _typeChuky = TypeChuKy.RPT_BHXH_CPBS_TONG_HOP_THONG_TRI_TNQN_NLD;
                    break;
                case ReportCPBSKCBBHYT.THONGTRI_TNQN:
                    _typeChuky = TypeChuKy.RPT_BHXH_CPBS_THONG_TRI_TNQN;
                    break;
                case ReportCPBSKCBBHYT.THONGTRI_TNQN_NLD:
                    _typeChuky = TypeChuKy.RPT_BHXH_CPBS_THONG_TRI_TNQN_NLD;
                    break;
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
