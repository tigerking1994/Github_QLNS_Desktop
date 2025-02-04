using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Report
{
    public class DuToanDonViIndexViewModel : DetailViewModelBase<DtChungTuModel, DtChungTuChiTietModel>
    {
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly ISessionService _sessionService;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView _chungTuChiTietItemsView;
        private ICollectionView _dTCTItemsView;
        private INsMucLucNganSachService _mucLucNganSachService;
        private IMapper _mapper;
        private DuToanDonViCriteria _searchCondition;
        private INsDonViService _nsDonViService;
        private PrintReportTargetAgencyViewModel PrintReportTargetAgencyViewModel;
        private PrintReportSynthesisAgencyViewModel PrintReportSynthesisAgencyViewModel;
        private PrintReportSynthesisDivisionViewModel PrintReportSynthesisDivisionViewModel;
        private PrintReportDetailTargetAgencyLnsViewModel PrintReportDetailTargetAgencyLnsViewModel;
        private PrintReportSynthesisTargetMajorsViewModel PrintReportSynthesisTargetMajorsViewModel;
        private List<NsMucLucNganSach> _listMLNS;

        public override string GroupName => "BÁO CÁO - THỐNG KÊ";
        public override string Name => "Dự toán của đơn vị";

        public override string Description => "Thống kê dự toán ngân sách " + (_selectedNsDonViModel != null ? _selectedNsDonViModel.TenDonVi : string.Empty);

        public override Type ContentType => typeof(DuToanDonViIndex);
        public bool IsSaveData => _chungTuChiTietItems.Any(item => item.IsModified || item.IsDeleted);

        private DtChungTuModel _divisionModel;
        public DtChungTuModel DivisionModel
        {
            get => _divisionModel;
            set => SetProperty(ref _divisionModel, value);
        }

        private ObservableCollection<DtChungTuChiTietModel> _chungTuChiTietItems;
        public ObservableCollection<DtChungTuChiTietModel> ChungTuChiTietItems
        {
            get => _chungTuChiTietItems;
            set => SetProperty(ref _chungTuChiTietItems, value);
        }

        private DtChungTuChiTietModel _selectedChungTuChiTiet;
        public DtChungTuChiTietModel SelectedChungTuChiTiet
        {
            get => _selectedChungTuChiTiet;
            set => SetProperty(ref _selectedChungTuChiTiet, value);
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogItemsView.Refresh();
                }
            }
        }

        private string _searchCT;
        public string SearchCT
        {
            get => _searchCT;
            set
            {
                if (SetProperty(ref _searchCT, value))
                {
                    _dTCTItemsView.Refresh();
                }
            }
        }

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private string _selectedDonVi;
        public string SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        private string _selectedChungTu;
        public string SelectedChungTu
        {
            get => SelectedChungTuModel != null ? string.Format("{0} | {1} | {2} ", SelectedChungTuModel.SSoChungTu, SelectedChungTuModel.DNgayChungTu.HasValue ? SelectedChungTuModel.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty, SelectedChungTuModel.SMoTa) : string.Empty;
            set => SetProperty(ref _selectedChungTu, value);
        }

        private ObservableCollection<NsMucLucNganSach> _budgetCatalogItems;
        public ObservableCollection<NsMucLucNganSach> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private NsMucLucNganSach _selectedBudgetCatalog;
        public NsMucLucNganSach SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                SetProperty(ref _selectedBudgetCatalog, value);
                if (_selectedBudgetCatalog != null)
                {
                    SelectedLNS = _selectedBudgetCatalog.Lns;
                    _chungTuChiTietItemsView.Refresh();
                }
                IsOpenLnsPopup = false;
            }
        }


        private List<DonViModel> _nsDonViModels;
        public List<DonViModel> NSDonViModels
        {
            get => _nsDonViModels;
            set => SetProperty(ref _nsDonViModels, value);
        }

        private DonViModel _selectedNsDonViModel;
        public DonViModel SelectedNsDonViModel
        {
            get => _selectedNsDonViModel;
            set
            {
                SetProperty(ref _selectedNsDonViModel, value);
                if (_selectedNsDonViModel != null)
                {
                    OnPropertyChanged(nameof(Description));
                    LoadData();
                }
            }
        }

        private ObservableCollection<DtChungTuModel> _dtChungTuModels;
        public ObservableCollection<DtChungTuModel> DtChungTuModels
        {
            get => _dtChungTuModels;
            set => SetProperty(ref _dtChungTuModels, value);
        }

        private DtChungTuModel _selectedChungTuModel;
        public DtChungTuModel SelectedChungTuModel
        {
            get => _selectedChungTuModel;
            set
            {
                SetProperty(ref _selectedChungTuModel, value);
                if (_selectedChungTuModel != null)
                {
                    OnPropertyChanged(nameof(SelectedChungTu));
                    LoadData();
                }
                IsOpenDTCTPopup = false;
            }
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set
            {
                SetProperty(ref _isOpenLnsPopup, value);
            }
        }

        private bool _isOpenDTCTPopup;
        public bool IsOpenDTCTPopup
        {
            get => _isOpenDTCTPopup;
            set
            {
                SetProperty(ref _isOpenDTCTPopup, value);
            }
        }


        private EstimationDetailCriteria _detailFilter;
        public EstimationDetailCriteria DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private double _totalTuChi;
        public double TotalTuChi
        {
            get => _totalTuChi;
            set => SetProperty(ref _totalTuChi, value);
        }

        private double _totalHienVat;
        public double TotalHienVat
        {
            get => _totalHienVat;
            set => SetProperty(ref _totalHienVat, value);
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private Visibility _hiddenBottom;
        public Visibility HiddenBottom
        {
            get => _hiddenBottom;
            set => SetProperty(ref _hiddenBottom, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetSearchCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand RefreshCommand { get; set; }

        public DuToanDonViIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDtChungTuService dtChungTuService,
            INsDonViService nsDonViService,
            INsMucLucNganSachService mucLucNganSachService,
            IDanhMucService danhMucService,
            PrintReportTargetAgencyViewModel printReportTargetAgencyViewModel,
            PrintReportSynthesisAgencyViewModel printReportSynthesisAgencyViewModel,
            PrintReportSynthesisDivisionViewModel printReportSynthesisDivisionViewModel,
            PrintReportDetailTargetAgencyLnsViewModel printReportDetailTargetAgencyLnsViewModel,
            PrintReportSynthesisTargetMajorsViewModel printReportSynthesisTargetMajorsViewModel) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _dtChungTuChiTietService = (NsDtChungTuChiTietService)dtChungTuChiTietService;
            _dtChungTuService = dtChungTuService;
            _nsDonViService = nsDonViService;
            _mucLucNganSachService = mucLucNganSachService;
            PrintReportTargetAgencyViewModel = printReportTargetAgencyViewModel;
            PrintReportSynthesisAgencyViewModel = printReportSynthesisAgencyViewModel;
            PrintReportSynthesisDivisionViewModel = printReportSynthesisDivisionViewModel;
            PrintReportDetailTargetAgencyLnsViewModel = printReportDetailTargetAgencyLnsViewModel;
            PrintReportSynthesisTargetMajorsViewModel = printReportSynthesisTargetMajorsViewModel;

            ResetSearchCommand = new RelayCommand(obj => OnResetSearch());
            SearchCommand = new RelayCommand(obj => _chungTuChiTietItemsView.Refresh());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            RefreshCommand = new RelayCommand(obj => Init());
        }

        public override void Init()
        {
            DetailFilter = new EstimationDetailCriteria();
            _searchCondition = new DuToanDonViCriteria();
            _listMLNS = _mucLucNganSachService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            LoadNSDV();
            LoadDTCT();
            LoadData();
        }

        private void LoadData()
        {
            _searchCondition.dvt = 1;
            _searchCondition.NamLamViec = _sessionService.Current.YearOfWork;
            _searchCondition.NamNganSach = _sessionService.Current.YearOfBudget;
            _searchCondition.NguonNganSach = _sessionService.Current.Budget;
            _searchCondition.IdDonVi = SelectedNsDonViModel.IIDMaDonVi;
            _searchCondition.NgayChungTu = SelectedChungTuModel != null ? SelectedChungTuModel.DNgayChungTu : null;
            if (_searchCondition.IdDonVi == null || _searchCondition.NgayChungTu == null)
            {
                _chungTuChiTietItems = new ObservableCollection<DtChungTuChiTietModel>();
                HiddenBottom = Visibility.Hidden;
            }
            else
            {
                var dtchuntu = DtChungTuModels.Where(x => x.DNgayChungTu <= SelectedChungTuModel.DNgayChungTu);
                List<string> sSoLNS = new List<string>();
                foreach (var dt in dtchuntu)
                {
                    sSoLNS.Add(dt.SDslns);
                }
                string sLNSs = string.Empty;
                if (sSoLNS.Count() > 0)
                {
                    sLNSs = string.Join(",", sSoLNS);
                }
                _searchCondition.LNS = sLNSs;
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                _searchCondition.bKhoa = userAgency.Count() > 0 ? 0 : 1;
                IEnumerable<DuToanDonViQuery> _chungTuChiTiet = _dtChungTuChiTietService.FindDuToanDonvi(_searchCondition);
                CalculateData(_chungTuChiTiet);
                _chungTuChiTiet = _chungTuChiTiet.Where(x => x.TuChi != 0 || x.HienVat != 0);
                if (_chungTuChiTiet.Count() > 0)
                {
                    HiddenBottom = Visibility.Visible;
                }
                else
                {
                    HiddenBottom = Visibility.Hidden;
                }
                _chungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(_chungTuChiTiet);
            }


            _chungTuChiTietItemsView = CollectionViewSource.GetDefaultView(ChungTuChiTietItems);
            _chungTuChiTietItemsView.Filter = ChungTuChiTietItemsViewFilter;
            CaculateTotal();
            OnPropertyChanged(nameof(ChungTuChiTietItems));
            OnPropertyChanged(nameof(HiddenBottom));
            LoadLNSIndexCondition();
        }

        private void CalculateData(IEnumerable<DuToanDonViQuery> listdata)
        {
            listdata.Where(x => x.IsHangCha).Select(x =>
            {
                x.TuChi = 0;
                x.HienVat = 0;
                return x;
            }).ToList();

            foreach (var item in listdata.Where(x => x.TuChi != 0 || x.HienVat != 0))
            {
                CalculateParent(listdata, item, item);
            }
        }

        private void CalculateParent(IEnumerable<DuToanDonViQuery> listdata, DuToanDonViQuery currentItem, DuToanDonViQuery seftItem)
        {
            var parrentItem = listdata.FirstOrDefault(x => x.iID_MLNS == currentItem.iID_MLNS_Cha);
            if (parrentItem == null) return;
            parrentItem.TuChi += seftItem.TuChi;
            parrentItem.HienVat += seftItem.HienVat;
            CalculateParent(listdata, parrentItem, seftItem);
        }

        private void CaculateTotal()
        {
            TotalTuChi = 0;
            TotalHienVat = 0;
            var listChildren = _chungTuChiTietItems.Where(x => !x.IsHangCha && !x.IsDeleted && (x.FTuChi != 0 || x.FHienVat != 0) && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                TotalTuChi += item.FTuChi;
                TotalHienVat += item.FHienVat;
            }
        }
        private void OnRefresh()
        {
            SelectedLNS = string.Empty;
            DetailFilter = new EstimationDetailCriteria();
            LoadData();
        }

        private void OnResetSearch()
        {
            SelectedLNS = string.Empty;
            DetailFilter = new EstimationDetailCriteria();
            _chungTuChiTietItemsView.Refresh();
        }

        private void LoadLNSIndexCondition()
        {
            List<NsMucLucNganSach> listMLNS = _listMLNS.Where(x => _chungTuChiTietItems.Select(x => x.SLns).ToList().Contains(x.Lns)).ToList();
            listMLNS.Insert(0, new NsMucLucNganSach
            {
                Lns = string.Empty,
                MoTa = "-- TẤT CẢ --"
            });
            _budgetCatalogItems = new ObservableCollection<NsMucLucNganSach>(listMLNS);
            OnPropertyChanged(nameof(BudgetCatalogItems));
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
        }

        private bool BudgetCatalogItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().Contains(_searchLNS!.ToLower());
        }

        private void LoadNSDV()
        {
            DonVi nsDonVi = _nsDonViService.FindByIdDonVi(_sessionService.Current.IdDonVi, _sessionService.Current.YearOfWork);
            IEnumerable<DonVi> nsDonVis = _nsDonViService.FindByCondition(d => d.NamLamViec == _sessionService.Current.YearOfWork && "1".Equals(d.Loai));
            _nsDonViModels = _mapper.Map<List<DonViModel>>(nsDonVis);
            OnPropertyChanged(nameof(NSDonViModels));
            if (NSDonViModels.Count > 0)
            {
                _selectedNsDonViModel = NSDonViModels.FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedNsDonViModel));
            OnPropertyChanged(nameof(Description));
        }

        private void LoadDTCT()
        {
            Expression<Func<NsDtChungTu, bool>> predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(d => d.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(d => d.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(d => d.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(d => d.ILoai == SoChungTuType.EstimateDivision);

            List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (userAgency.Count() == 0)
            {
                predicate = predicate.And(d => d.BKhoa);
            }

            IEnumerable<NsDtChungTu> dtChungTus = _dtChungTuService.FindByCondition(predicate);
            _dtChungTuModels = _mapper.Map<ObservableCollection<DtChungTuModel>>(dtChungTus);
            OnPropertyChanged(nameof(DtChungTuModels));
            if (DtChungTuModels.Count > 0)
                _selectedChungTuModel = DtChungTuModels.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedChungTuModel));
            _dTCTItemsView = CollectionViewSource.GetDefaultView(DtChungTuModels);
            _dTCTItemsView.Filter = CTItemsFilter;
            OnPropertyChanged(nameof(SelectedChungTu));
        }

        private bool CTItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchCT))
            {
                return true;
            }
            return obj is DtChungTuModel item && item.SSoChungTu.ToLower().Contains(_searchCT!.ToLower());
        }

        private bool ChungTuChiTietItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (DtChungTuChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLns.ToLower().Contains(SelectedLNS.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().Contains(DetailFilter.L.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().Contains(DetailFilter.K.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().Contains(DetailFilter.M.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STm.ToLower().Contains(DetailFilter.TM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STtm.ToLower().Contains(DetailFilter.TTM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNg.ToLower().Contains(DetailFilter.NG.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.STng.ToLower().Contains(DetailFilter.TNG.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG1))
                result = result && item.STng1.ToLower().Contains(DetailFilter.TNG1.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG2))
                result = result && item.STng2.ToLower().Contains(DetailFilter.TNG2.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG3))
                result = result && item.STng3.ToLower().Contains(DetailFilter.TNG3.Trim().ToLower());
            return result;
        }

        private void OpenPrintDialog(object param)
        {
            var divisionEstimatePrintType = (DivisionEstimatePrintType)((int)param);
            object content = null;
            switch (divisionEstimatePrintType)
            {
                case DivisionEstimatePrintType.TARGET_AGENCY:
                    PrintReportTargetAgencyViewModel.Model = SelectedChungTuModel;
                    PrintReportTargetAgencyViewModel.Models = DtChungTuModels;
                    PrintReportTargetAgencyViewModel.Init();
                    content = new PrintReportTargetAgency
                    {
                        DataContext = PrintReportTargetAgencyViewModel
                    };
                    break;
                case DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY:
                    PrintReportSynthesisAgencyViewModel.Model = SelectedChungTuModel;
                    PrintReportSynthesisAgencyViewModel.Models = DtChungTuModels;
                    PrintReportSynthesisAgencyViewModel.Init();
                    content = new PrintReportSynthesisAgency
                    {
                        DataContext = PrintReportSynthesisAgencyViewModel
                    };
                    break;
                case DivisionEstimatePrintType.SYNTHESIS_BUDGET_DIVISION:
                    PrintReportSynthesisDivisionViewModel.Model = SelectedChungTuModel;
                    PrintReportSynthesisDivisionViewModel.Models = DtChungTuModels;
                    PrintReportSynthesisDivisionViewModel.Init();
                    content = new PrintReportSynthesisDivision
                    {
                        DataContext = PrintReportSynthesisDivisionViewModel
                    };
                    break;
                case DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_AGENCY_LNS:
                    PrintReportDetailTargetAgencyLnsViewModel.Model = SelectedChungTuModel;
                    PrintReportDetailTargetAgencyLnsViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    PrintReportDetailTargetAgencyLnsViewModel.Init();
                    content = new PrintReportDetailTargetAgencyLns
                    {
                        DataContext = PrintReportDetailTargetAgencyLnsViewModel
                    };
                    break;
                case DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_MAJORS:
                    PrintReportSynthesisTargetMajorsViewModel.Model = SelectedChungTuModel;
                    PrintReportSynthesisTargetMajorsViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    PrintReportSynthesisTargetMajorsViewModel.Init();
                    content = new PrintReportSynthesisTargetMajors
                    {
                        DataContext = PrintReportSynthesisTargetMajorsViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DivisionEstimateScreen.ROOT_DIALOG, null, null);
            }
        }
    }
}
