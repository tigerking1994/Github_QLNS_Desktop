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
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.PrintReport
{
    public class PrintCapPhatTamUngKCBBHYTViewModel : ViewModelBase
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
        private readonly IBhCpBsChungTuChiTietService _cpbsBHYTChiTietService;


        private readonly ICptuBHYTService _cptuBHYTService;
        private readonly ICptuBHYTChiTietService _cptuBHYTChiTietService;

        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }

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

        public override string Name => "IN THÔNG TRI CHI TIẾT CẤP TẠM ỨNG KP KCB BHYT";
        public override string Title { get; set; }
        public override string Description { get; set; }
        public override Type ContentType => typeof(PrintCapPhatTamUngKCBBHYT);

        private bool _inMotTo;
        public bool InMotTo
        {
            get => _inMotTo;
            set => SetProperty(ref _inMotTo, value);
        }

        private bool _isMillionRound;
        public bool IsMillionRound
        {
            get => _isMillionRound;
            set => SetProperty(ref _isMillionRound, value);
        }

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
                OnPropertyChanged(nameof(ListCsYTe));
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

        private ReportCPTUKCBBHYT _reportType;
        public ReportCPTUKCBBHYT ReportType
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
                    ReportType = InsuranceAllocationReportType.AllocationType[value.ValueItem];
                    LoadTitle2();
                    LoadCsYTe();
                    OnPropertyChanged(nameof(ReportType));
                    OnPropertyChanged(nameof(Title2));
                    OnPropertyChanged(nameof(ItemsReportType));
                    OnPropertyChanged(nameof(ListCsYTe));
                }
            }
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }

        public PrintCapPhatTamUngKCBBHYTViewModel(INsDonViService donViService,
            ISessionService sessionService,
            IMapper mapper,
            IExportService exportService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            ICptuBHYTService cptuBHYTService,
            ICptuBHYTChiTietService cptuBHYTChiTietService,
            ILog logger,
            IBhCpBsChungTuChiTietService bhCpBsChungTuChiTietService,
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
            _cptuBHYTService = cptuBHYTService;
            _cptuBHYTChiTietService = cptuBHYTChiTietService;
            _cpbsBHYTChiTietService = bhCpBsChungTuChiTietService;
            _logger = logger;
            _bhGhiChuService = bhBaoCaoGhiChuService;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

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
                _reportType = ReportCPTUKCBBHYT.KEHOACH_TNQN;
                Title = "In kế hoạch cấp tạm ứng kinh phí KCB BHYT";
                Description = "In kế hoạch cấp tạm ứng kinh phí KCB BHYT";
            }
            else if (!IsEnableTongTop)
            {
                _reportType = ReportCPTUKCBBHYT.TONGHOP_TNQN;
                Title = "In thông tri tổng hợp cấp tạm ứng KP KCB BHYT";
                Description = "In thông tri tổng hợp cấp tạm ứng KP KCB BHYT";
            }
            else
            {
                _reportType = ReportCPTUKCBBHYT.THONGTRI_TNQN;
                Title = "In thông tri chi tiết cấp tạm ứng KP KCB BHYT";
                Description = "In thông tri chi tiết cấp tạm ứng KP KCB BHYT";
            }
            LoadReportType();
            InitReportDefaultDate();
            LoadTypeChuKy();
            LoadTieuDe();
            LoadMonthsAndQuarters();
            LoadDanhMuc();
            LoadLNS();
            LoadCsYTe();
        }

        private void LoadReportType()
        {
            List<ComboboxItem> lstType;
            switch (_reportType)
            {
                case ReportCPTUKCBBHYT.KEHOACH_TNQN:
                    lstType = new List<ComboboxItem>()
                    {
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPTUKCBBHYT.KEHOACH_TNQN).ToString(),
                                DisplayItem = InsuranceAllocationReportType.AllocationReportTypeName[ReportCPTUKCBBHYT.KEHOACH_TNQN],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN)

                            },
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD).ToString(),
                                DisplayItem = InsuranceAllocationReportType.AllocationReportTypeName[ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD)
                            }
                    };
                    break;
                case ReportCPTUKCBBHYT.TONGHOP_TNQN:
                    lstType = new List<ComboboxItem>()
                    {
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPTUKCBBHYT.TONGHOP_TNQN).ToString(),
                                DisplayItem = InsuranceAllocationReportType.AllocationReportTypeName[ReportCPTUKCBBHYT.TONGHOP_TNQN],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN)
                            },
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPTUKCBBHYT.TONGHOP_TNQN_NLD).ToString(),
                                DisplayItem = InsuranceAllocationReportType.AllocationReportTypeName[ReportCPTUKCBBHYT.TONGHOP_TNQN_NLD],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD)
                            }
                    };
                    break;
                case ReportCPTUKCBBHYT.THONGTRI_TNQN:
                    lstType = new List<ComboboxItem>()
                    {
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPTUKCBBHYT.THONGTRI_TNQN).ToString(),
                                DisplayItem = InsuranceAllocationReportType.AllocationReportTypeName[ReportCPTUKCBBHYT.THONGTRI_TNQN],
                                HiddenValue = CapKinhPhi.GetLns((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN)
                            },
                            new ComboboxItem
                            {
                                ValueItem = ((int)ReportCPTUKCBBHYT.THONGTRI_TNQN_NLD).ToString(),
                                DisplayItem = InsuranceAllocationReportType.AllocationReportTypeName[ReportCPTUKCBBHYT.THONGTRI_TNQN_NLD],
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

        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>()
            { TypeChuKy.RPT_BH_CAPPHAT_KEHOACH_TNQN, TypeChuKy.RPT_BH_CAPPHAT_KEHOACH_TNQN_NLD, TypeChuKy.RPT_BH_CAPPHAT_TONGHOP_TNQN, TypeChuKy.RPT_BH_CAPPHAT_TONGHOP_TNQN_NLD, TypeChuKy.RPT_BH_CAPPHAT_THONGTRI_TNQN, TypeChuKy.RPT_BH_CAPPHAT_THONGTRI_TNQN_NLD };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(ListCsYTe);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.IsShowCanCu = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            string idDonVi = _sessionInfo.IdDonVi;

            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
            //predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(n => n.SXauNoiMa.StartsWith("904"));
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
                
                if (_reportType == ReportCPTUKCBBHYT.KEHOACH_TNQN || _reportType == ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD)
                {
                    lstMaCoSoKT = _cpbsBHYTChiTietService.GetMaCoSoYTeDetailByCondition(int.Parse(MonthAndQuartersSelected.ValueItem), _sessionInfo.YearOfWork, SelectedReportType.HiddenValue, !IsEnableTongTop, AllocationFunction.CAP_TAM_UNG, true);
                } 
                else
                {
                    lstMaCoSoKT = _cpbsBHYTChiTietService.GetMaCoSoYTeDetailByCondition(int.Parse(MonthAndQuartersSelected.ValueItem), _sessionInfo.YearOfWork, SelectedReportType.HiddenValue, !IsEnableTongTop, AllocationFunction.CAP_TAM_UNG);
                }

                if (lstMaCoSoKT != null && lstMaCoSoKT.Count() > 0)
                {
                    foreach(var item in lstMaCoSoKT)
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

            if (ReportType == ReportCPTUKCBBHYT.THONGTRI_TNQN && SelectedReportType.ValueItem == ((int)ReportCPTUKCBBHYT.THONGTRI_TNQN).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPTUBHYTReportTitle.THONG_TRI_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPTUBHYTReportTitle.THONG_TRI_QN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPTUBHYTReportTitle.THONG_TRI_QN_TITLE_3;
            }
            else if (ReportType == ReportCPTUKCBBHYT.THONGTRI_TNQN_NLD && SelectedReportType.ValueItem == ((int)ReportCPTUKCBBHYT.THONGTRI_TNQN_NLD).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPTUBHYTReportTitle.THONG_TRI_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPTUBHYTReportTitle.THONG_TRI_TNQN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPTUBHYTReportTitle.THONG_TRI_TNQN_TITLE_3;
            }
            else if (ReportType == ReportCPTUKCBBHYT.TONGHOP_TNQN && SelectedReportType.ValueItem == ((int)ReportCPTUKCBBHYT.TONGHOP_TNQN).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPTUBHYTReportTitle.TONG_HOP_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPTUBHYTReportTitle.TONG_HOP_QN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPTUBHYTReportTitle.TONG_HOP_QN_TITLE_3;
            }
            else if (ReportType == ReportCPTUKCBBHYT.TONGHOP_TNQN_NLD && SelectedReportType.ValueItem == ((int)ReportCPTUKCBBHYT.TONGHOP_TNQN_NLD).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPTUBHYTReportTitle.TONG_HOP_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPTUBHYTReportTitle.TONG_HOP_TNQN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPTUBHYTReportTitle.TONG_HOP_TNQN_TITLE_3;
            }
            else if (ReportType == ReportCPTUKCBBHYT.KEHOACH_TNQN && SelectedReportType.ValueItem == ((int)ReportCPTUKCBBHYT.KEHOACH_TNQN).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPTUBHYTReportTitle.KE_HOACH_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPTUBHYTReportTitle.KE_HOACH_QN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPTUBHYTReportTitle.KE_HOACH_QN_TITLE_3;
            }
            else if (ReportType == ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD && SelectedReportType.ValueItem == ((int)ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD).ToString())
            {
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultCPTUBHYTReportTitle.KE_HOACH_TITLE_1;
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultCPTUBHYTReportTitle.KE_HOACH_TNQN_TITLE_2;
                Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultCPTUBHYTReportTitle.KE_HOACH_TNQN_TITLE_3;
            }
            else
            {
                if (_dmChuKy == null || _dmChuKy.Id.IsNullOrEmpty())
                    _dmChuKy = new DmChuKy();
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                    Title1 = _dmChuKy.TieuDe1MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                    Title2 = _dmChuKy.TieuDe2MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                    Title3 = _dmChuKy.TieuDe3MoTa;
                //switch (_reportType)
                //{
                //    case ReportCPTUKCBBHYT.KEHOACH_TNQN:
                //        Title1 = "KẾ HOẠCH";
                //        break;
                //    case ReportCPTUKCBBHYT.TONGHOP_TNQN:
                //        Title1 = "THÔNG TRI TỔNG HỢP";
                //        break;
                //    case ReportCPTUKCBBHYT.THONGTRI_TNQN:
                //        Title1 = "THÔNG TRI";
                //        break;
                //}
            }
            
        }

        private void LoadTitle2()
        {
            Title2 = string.IsNullOrEmpty(Title2) ? string.Format(InsuranceAllocationReportType.AllocationReportTitle2[SelectedReportType.ValueItem], MonthAndQuartersSelected?.ValueItem, _sessionInfo.YearOfWork) : Title2;
        }

        private void AddTitle(Dictionary<string, object> data)
        {
            data.Add("Title3", Title3);
            data.Add("Title2", Title2);
            data.Add("Title1", Title1);
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
                ExportKeHoachCapPhatTamUngKCBBHYT(exportType);
            }
            else if (!IsEnableTongTop)
            {
                ExportTongHopCapPhatTamUngKCBBHYT(exportType);
            }
            else
            {
                ExportThongTriCapPhatTamUngKCBBHYT(exportType);
            }

        }

        private void ExportKeHoachCapPhatTamUngKCBBHYT(ExportType exportType)
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

                    List<BhCptuBHYTChiTietQuery> lstData = new List<BhCptuBHYTChiTietQuery>();
                    lstData = _cptuBHYTChiTietService.ExportKeHoachCapPhatTamUngKCBBHYT(sIdCsYTe, iLoai, iQuy, yearOfWork, _sessionInfo.Principal, donViTinh, SelectedReportType?.HiddenValue ?? string.Empty, IsMillionRound).ToList();

                    var lstMaCSYTExisted = lstData.Select(x => x.IID_MaCoSoYTe).ToList();
                    foreach (var cs in ListCsYTe.Where(x => x.IsChecked))
                    {
                        if (!lstMaCSYTExisted.Contains(cs.ValueItem))
                        {
                            var newData = new BhCptuBHYTChiTietQuery();
                            newData.IID_MaCoSoYTe = cs.ValueItem;
                            newData.STenCoSoYTe = cs.NameItem;
                            newData.FQTQuyTruoc = double.Parse("0");
                            newData.FTamUngQuyNay = double.Parse("0");
                            newData.FLuyKeCapDenCuoiQuy = double.Parse("0");
                            lstData.Add(newData);
                        }
                    }

                    lstData.OrderBy(x => x.IID_MaCoSoYTe);

                    int i = 1;
                    lstData.Select(x =>
                    {
                        x.STT = i;
                        i++;
                        return x;
                    }).ToList();

                    var fTongQTQuyTruoc = lstData.Sum(x => x.FQTQuyTruoc);
                    var fTongTamUngQuyNay = lstData.Sum(x => x.FTamUngQuyNay);
                    var fTongLuyKeCapDenCuoiQuy = lstData.Sum(x => x.FLuyKeCapDenCuoiQuy);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    data.Add("YearWork", yearOfWork);
                    data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());


                    data.Add("FTongQTQuyTruoc", fTongQTQuyTruoc);
                    data.Add("FTongTamUngQuyNay", fTongTamUngQuyNay);
                    data.Add("FTongLuyKeCapDenCuoiQuy", fTongLuyKeCapDenCuoiQuy);
                    data.Add("ListData", lstData);
                    data.Add("FormatNumber", formatNumber);

                    AddChuKy(data);
                    AddTitle(data);

                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Quy", MonthAndQuartersSelected.ValueItem);
                    data.Add("Year", _sessionInfo.YearOfWork);
                    data.Add("PreQuy", MonthAndQuartersSelected.ValueItem == "1" ? "4" : (Int32.Parse(MonthAndQuartersSelected.ValueItem) - 1).ToString());
                    data.Add("PreYear", MonthAndQuartersSelected.ValueItem == "1" ? _sessionInfo.YearOfWork - 1 : _sessionInfo.YearOfWork);

                    var fTongCongTongTien = (fTongQTQuyTruoc ?? 0) + (fTongTamUngQuyNay ?? 0) + (fTongLuyKeCapDenCuoiQuy ?? 0);
                    data.Add("TongSoTien", fTongTamUngQuyNay != null ? StringUtils.NumberToText((double)fTongTamUngQuyNay, true) : string.Empty);

                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    _bhGhiChuService.AddReportConfig(data, _typeChuky, _sessionInfo.YearOfWork);

                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhCptuBHYTChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH CẤP TẠM ỨNG KINH PHÍ TNQN " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (InMotTo)
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

        private void ExportTongHopCapPhatTamUngKCBBHYT(ExportType exportType)
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

                    List<BhCptuBHYTChiTietQuery> lstData = new List<BhCptuBHYTChiTietQuery>();
                    lstData = _cptuBHYTChiTietService.ExportTongHopCapPhatTamUngKCBBHYT(sIdCsYTe, iLoai, iQuy, yearOfWork, _sessionInfo.Principal, donViTinh, SelectedReportType?.HiddenValue ?? string.Empty, IsMillionRound).ToList();

                    var fTongTamUngQuyNay = lstData.Where(x => !x.BHangCha).Sum(x => x.FTamUngQuyNay);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    data.Add("YearWork", yearOfWork);
                    data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());


                    data.Add("FTongTamUngQuyNay", fTongTamUngQuyNay);
                    data.Add("ListData", lstData);
                    data.Add("FormatNumber", formatNumber);

                    AddChuKy(data);
                    AddTitle(data);

                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Quy", MonthAndQuartersSelected.ValueItem);
                    data.Add("Year", _sessionInfo.YearOfWork);

                    string name = "";
                    if (ReportType == ReportCPTUKCBBHYT.TONGHOP_TNQN)
                    {
                        name = "CẤP TẠM ỨNG KINH PHÍ KCB TNQN";
                    }
                    else
                    {
                        name = "CẤP TẠM ỨNG KINH PHÍ KCB TNQN & NLĐ";
                    }
                    data.Add("Name", name);

                    data.Add("TongSoTien", fTongTamUngQuyNay != null ? StringUtils.NumberToText((double)fTongTamUngQuyNay, true) : string.Empty);

                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhCptuBHYTChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("TỔNG HỢP CẤP TẠM ỨNG KINH PHÍ TNQN " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void ExportThongTriCapPhatTamUngKCBBHYT(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstIdCsYte = ListCsYTe.Where(x => x.IsChecked).ToList();

                    foreach (var itemCSYT in lstIdCsYte)
                    {
                        int iLoai = (int)ReportType;
                        int iQuy = int.Parse(MonthAndQuartersSelected.ValueItem);
                        var lstLns = SelectedReportType.HiddenValue;
                        string sLns = string.Join(",", lstLns);

                        List<BhCptuBHYTChiTietQuery> lstData = new List<BhCptuBHYTChiTietQuery>();
                        lstData = _cptuBHYTChiTietService.ExportThongTriCapPhatTamUngKCBBHYT(itemCSYT.ValueItem, iLoai, sLns, iQuy, yearOfWork, _sessionInfo.Principal, donViTinh, IsMillionRound).ToList();
                        CalculateData(lstData);
                        lstData = lstData.OrderBy(x => x.SXauNoiMa).ToList();
                        var fTongTamUngQuyNay = lstData.Where(x => !x.BHangCha).Sum(x => x.FTamUngQuyNay);
                        ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                        data.Add("YearWork", yearOfWork);
                        data.Add("DonVi", itemCSYT.NameItem.ToUpper());
                        data.Add("FTongTamUngQuyNay", fTongTamUngQuyNay);
                        data.Add("ListData", lstData);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Quy", MonthAndQuartersSelected.ValueItem);
                        data.Add("Year", _sessionInfo.YearOfWork);
                        AddChuKy(data);
                        AddTitle(data);
                        string name = "";
                        if (ReportType == ReportCPTUKCBBHYT.TONGHOP_TNQN)
                        {
                            name = "THÔNG TRI CẤP TẠM ỨNG KINH PHÍ KCB TNQN";
                        }
                        else
                        {
                            name = "THÔNG TRI CẤP TẠM ỨNG KINH PHÍ KCB TNQN & NLĐ";
                        }
                        data.Add("TongSoTien", fTongTamUngQuyNay != null ? StringUtils.NumberToText((double)fTongTamUngQuyNay, true) : string.Empty);
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhCptuBHYTChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult("TỔNG HỢP CẤP TẠM ỨNG KINH PHÍ TNQN " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        e.Result = results;
                    }
                    
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (InMotTo)
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


        private void CalculateData(List<BhCptuBHYTChiTietQuery> lstNdtChungTuChiTiet)
        {
            lstNdtChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FLuyKeCapDenCuoiQuy = 0;
                    x.FQTQuyTruoc = 0;
                    x.FTamUngQuyNay = 0;
                    return x;
                }).ToList();
            var temp = lstNdtChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, BhCptuBHYTChiTietQuery item, List<BhCptuBHYTChiTietQuery> lstNdtChungTuChiTiet)
        {
            var dictByMlns = lstNdtChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }
            var model = dictByMlns[idParent];
            model.FLuyKeCapDenCuoiQuy = (model.FLuyKeCapDenCuoiQuy ?? 0) + (item.FLuyKeCapDenCuoiQuy ?? 0);
            model.FQTQuyTruoc = (model.FQTQuyTruoc ?? 0) + (item.FQTQuyTruoc ?? 0);
            model.FTamUngQuyNay = (model.FTamUngQuyNay ?? 0) + (item.FTamUngQuyNay ?? 0);
            CalculateParent(model.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
        }

        private string GetTemplate()
        {
            string input = "";
            if(ReportType == ReportCPTUKCBBHYT.KEHOACH_TNQN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_KEHOACHCAPTAMUNGQNKCHBHYT_DOC);
            }
            else if(ReportType == ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_KEHOACHCAPTAMUNGQNNLDKCHBHYT_DOC);
            }
            else if(ReportType == ReportCPTUKCBBHYT.TONGHOP_TNQN || ReportType == ReportCPTUKCBBHYT.TONGHOP_TNQN_NLD)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_TONGHOPTHONGTRICAPTAMUNGKINHPHIKCBBHYT_DOC);
            }
            else if (ReportType == ReportCPTUKCBBHYT.THONGTRI_TNQN || ReportType == ReportCPTUKCBBHYT.THONGTRI_TNQN_NLD)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THONGTRICAPTAMUNGKINHPHIKCBBHYT_DOC);
            }
            return Path.Combine(ExportPrefix.PATH_BH_CPTU, input + FileExtensionFormats.Xlsx);
        }
      

        private void LoadTypeChuKy()
        {
            switch (ReportType)
            {
                case ReportCPTUKCBBHYT.KEHOACH_TNQN:
                    _typeChuky = TypeChuKy.RPT_BH_CAPPHAT_KEHOACH_TNQN;
                    break;
                case ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD:
                    _typeChuky = TypeChuKy.RPT_BH_CAPPHAT_KEHOACH_TNQN_NLD;
                    break;
                case ReportCPTUKCBBHYT.TONGHOP_TNQN:
                    _typeChuky = TypeChuKy.RPT_BH_CAPPHAT_TONGHOP_TNQN;
                    break;
                case ReportCPTUKCBBHYT.TONGHOP_TNQN_NLD:
                    _typeChuky = TypeChuKy.RPT_BH_CAPPHAT_TONGHOP_TNQN_NLD;
                    break;
                case ReportCPTUKCBBHYT.THONGTRI_TNQN:
                    _typeChuky = TypeChuKy.RPT_BH_CAPPHAT_THONGTRI_TNQN;
                    break;
                case ReportCPTUKCBBHYT.THONGTRI_TNQN_NLD:
                    _typeChuky = TypeChuKy.RPT_BH_CAPPHAT_THONGTRI_TNQN_NLD;
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

        private void AddChuKy(Dictionary<string, object> data)
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;

            data.Add("DiaDiem", _diaDiem);
        }
    }
}