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
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintCommunicateSettlementAgencyViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private ISessionService _sessionService;
        private INsQtChungTuService _chungTuService;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsDonViService _donViService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsPhongBanService _phongBanService;
        private IMapper _mapper;
        private ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _listMucLucNganSach;
        private List<NsMucLucNganSach> _listMucLucNganSachResult;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private DmChuKy _dmChuKy;
        private string _diaDiem;


        public override string Name => "In thông tri quyết toán - Tổng hợp đơn vị";
        public override string Title => "In thông tri quyết toán - Tổng hợp đơn vị";
        public override string Description => "Chọn in thông tri quyết toán tổng hợp theo Tháng hoặc Quý";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintCommunicateSettlementAgency);

        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
        {
            get => _quarterMonthSelected;
            set
            {
                if (SetProperty(ref _quarterMonthSelected, value))
                {
                    LoadAgencies();
                    OnPropertyChanged(nameof(IsExportEnable));
                }
            }
        }
        public string SettlementTypeValue { get; set; }

        private List<ComboboxItem> _loaiNganSach;
        public List<ComboboxItem> LoaiNganSach
        {
            get => _loaiNganSach;
            set => SetProperty(ref _loaiNganSach, value);
        }

        private ComboboxItem _loaiNganSachSelected;
        public ComboboxItem LoaiNganSachSelected
        {
            get => _loaiNganSachSelected;
            set
            {
                SetProperty(ref _loaiNganSachSelected, value);
                LoadBudgetIndexes();
            }
        }

        private List<ComboboxItem> _khoi;
        public List<ComboboxItem> Khoi
        {
            get => _khoi;
            set => SetProperty(ref _khoi, value);
        }

        private ComboboxItem _selectedKhoi;
        public ComboboxItem SelectedKhoi
        {
            get => _selectedKhoi;
            set
            {
                if (SetProperty(ref _selectedKhoi, value))
                {
                    LoadAgencies();
                }
            }
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
            set
            {
                if (SetProperty(ref _selectedBQuanLy, value))
                {
                    LoadBudgetIndexes();
                }
            }
        }

        private bool _isInTongHop;
        public bool IsInTongHop
        {
            get => _isInTongHop;
            set
            {
                if (SetProperty(ref _isInTongHop, value))
                {
                    LoadAgencies();
                }
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
                    totalCount = Agencies != null ? Agencies.Count : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies.Count > 0 && Agencies.All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                }
                OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
            }
        }
        #endregion

        #region list LNS
        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes;
        public ObservableCollection<NsMuclucNgansachModel> BudgetIndexes
        {
            get => _budgetIndexes;
            set => SetProperty(ref _budgetIndexes, value);
        }

        private string _searchBudgetIndexText;
        public string SearchBudgetIndexText
        {
            set
            {
                if (SetProperty(ref _searchBudgetIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Count : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Count > 0 && BudgetIndexes.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        public bool IsExportEnable
        {
            get
            {
                if (_budgetIndexes != null)
                    return _budgetIndexes.Where(x => x.IsSelected).Count() > 0;
                return false;
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

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintCommunicateSettlementAgencyViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            INsQtChungTuService chungTuService,
            INsMucLucNganSachService mucLucNganSachService,
            INsQtChungTuChiTietService chungTuChiTietService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            INsPhongBanService phongBanService,
            IExportService exportService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            ILog logger)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _chungTuService = chungTuService;
            _mucLucNganSachService = mucLucNganSachService;
            _chungTuChiTietService = chungTuChiTietService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _phongBanService = phongBanService;
            _logger = logger;
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
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _isInTongHop = false;
            LoaiNganSachSelected = null;
            _sessionInfo = _sessionService.Current;
            _listMucLucNganSach = _mucLucNganSachService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Empty, string.Empty);
            // _listMucLucNganSach = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            LoadTieuDe();
            ResetCondition();
            LoadQuarterMonths();
            LoadLoaiNganSach();
            LoadKhoi();
            LoadBQuanLy();
            LoadDanhMuc();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;

            LoadTieuDeTheoLoaiNganSach();
        }

        private void LoadTieuDeTheoLoaiNganSach()
        {
            //var typeChuKy = SettlementTypeValue switch
            //{
            //    SettlementType.REGULAR_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP,
            //    SettlementType.DEFENSE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP,
            //    SettlementType.STATE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_NHANUOC_TONGHOP,
            //    SettlementType.FOREX_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_NGOAIHOI_TONGHOP,
            //    SettlementType.EXPENSE_BUDGET => TypeChuKy.RPT_NS_QUYETTOAN_KINHPHIKHAC_TONGHOP,
            //    _ => TypeChuKy.RPT_NS_QUYETTOAN_TATCA_TONGHOP
            //};

            switch (SettlementTypeValue)
            {
                case SettlementType.REGULAR_BUDGET:
                    Title2 = "Xác nhận cho KP lương, phụ cấp, tiền ăn";
                    break;
                case SettlementType.DEFENSE_BUDGET:
                    //Title2 = "Xác nhận cho kinh phí nghiệp vụ HC";
                    Title2 = "Xác nhận kinh phí ngân sách nhà nước chi thường xuyên cho quốc phòng";
                    break;
                case SettlementType.STATE_BUDGET:
                    Title2 = "Xác nhận kinh phí ngân sách nhà nước chi sự nghiệp";
                    break;
                case SettlementType.FOREX_BUDGET:
                    Title2 = "Xác nhận kinh phí ngân sách chi quỹ dự trữ ngoại hối";
                    break;
                default:
                    Title2 = "Xác nhận kinh phí ngân sách kinh phí khác";
                    break;
            }
        }

        private void ResetCondition()
        {
            _agencies = new ObservableCollection<AgencyModel>();
            _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();

            OnPropertyChanged(nameof(Agencies));
            OnPropertyChanged(nameof(BudgetIndexes));
        }

        /// <summary>
        /// Tạo data cho combobox qúy
        /// </summary>
        private void LoadQuarterMonths()
        {
            _quarterMonths = new List<ComboboxItem>();
            _quarterMonths.Add(new ComboboxItem("Quý I", "3"));
            _quarterMonths.Add(new ComboboxItem("Quý II", "6"));
            _quarterMonths.Add(new ComboboxItem("Quý III", "9"));
            _quarterMonths.Add(new ComboboxItem("Quý IV", "12"));
            for (int i = 1; i <= 12; i++)
            {
                _quarterMonths.Add(new ComboboxItem("Tháng " + i, i.ToString()));
            }
        }

        private void LoadLoaiNganSach()
        {
            _loaiNganSach = new List<ComboboxItem>();
            _loaiNganSach.Add(new ComboboxItem("Tất cả", "-1"));
            _loaiNganSach.Add(new ComboboxItem("Thường xuyên", "0"));
            _loaiNganSach.Add(new ComboboxItem("Nghiệp vụ", "1"));
            _loaiNganSach.Add(new ComboboxItem("NSNN", "2"));
            _loaiNganSach.Add(new ComboboxItem("Kinh phí khác", "3"));
            _loaiNganSach.Add(new ComboboxItem("Quốc phòng khác", "4"));
            _loaiNganSach.Add(new ComboboxItem("Nhà nước khác", "4"));
            LoaiNganSachSelected = LoaiNganSach.First(n => n.ValueItem.Equals("-1"));
        }

        private void LoadKhoi()
        {
            _khoi = new List<ComboboxItem>
            {
                new ComboboxItem ("Tất cả", string.Empty),
                new ComboboxItem ("Doanh nghiệp", "1"),
                new ComboboxItem ("Dự toán", "2"),
                new ComboboxItem ("Bệnh viện tự chủ", "3"),
            };
            SelectedKhoi = _khoi.First();
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
            if (QuarterMonthSelected == null) return;
            int quarterMonthType = 0;
            if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                quarterMonthType = (int)QuarterMonth.QUARTER;
            else quarterMonthType = (int)QuarterMonth.MONTH;
            List<DonVi> listDonVi = _donViService.FindBySettlementMonth(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, QuarterMonthSelected.ValueItem, quarterMonthType, SettlementTypeValue).ToList();

            var idsDonViQuanLy = _sessionService.Current.IdsDonViQuanLy;

            if (!listDonVi.Any(x => idsDonViQuanLy.Contains(x.IIDMaDonVi) && x.Loai == LoaiDonVi.ROOT))
            {
                listDonVi = listDonVi.Where(x => idsDonViQuanLy.Contains(x.IIDMaDonVi)).ToList();
            }

            if (!IsInTongHop)
                if (!SelectedKhoi.ValueItem.IsEmpty())
                    listDonVi = listDonVi.Where(x => x.Loai != LoaiDonVi.ROOT && x.Khoi == SelectedKhoi.ValueItem).ToList();
                else
                    listDonVi = listDonVi.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
            else listDonVi = listDonVi.Where(x => x.Loai == LoaiDonVi.ROOT).ToList();

            var chungTuTongHop = new NsQtChungTu();
            if (!listDonVi.IsEmpty())
            {
                var predicate = PredicateBuilder.True<NsQtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.IIdMaDonVi == listDonVi.FirstOrDefault().IIDMaDonVi);
                chungTuTongHop = _chungTuService.FindByCondition(predicate).FirstOrDefault();
            }

            if (IsInTongHop && chungTuTongHop != null
                && (string.IsNullOrEmpty(chungTuTongHop.STongHop))
                && !SelectedKhoi.ValueItem.IsEmpty()
            )
                listDonVi = listDonVi.Where(x => x.Khoi == SelectedKhoi.ValueItem).ToList();

            _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
            _listAgency = CollectionViewSource.GetDefaultView(_agencies);
            _listAgency.Filter = ListAgencyFilter;
            foreach (var model in Agencies)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(AgencyModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedAgencyCount));
                        OnPropertyChanged(nameof(IsSelectedAllAgency));
                        LoadBudgetIndexes();
                    }
                };
            }
            _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();

            OnPropertyChanged(nameof(Agencies));
            OnPropertyChanged(nameof(IsSelectedAllAgency));
            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
            OnPropertyChanged(nameof(SelectedAgencyCount));
            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
            OnPropertyChanged(nameof(BudgetIndexes));
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
        }

        private void LoadBudgetIndexes()
        {
            if (QuarterMonthSelected == null) return;
            string quarterMonthId = String.Empty;
            int quarterMonthType = 0;
            quarterMonthId = QuarterMonthSelected.ValueItem;
            if (QuarterMonthSelected.DisplayItem.Contains("Quý"))
                quarterMonthType = (int)QuarterMonth.QUARTER;
            else quarterMonthType = (int)QuarterMonth.MONTH;
            List<LNSQuery> lnsQueries = _mucLucNganSachService.FindBySettlementMonth(_sessionInfo.YearOfWork, _sessionInfo.Budget, string.Join(",", _agencies.Where(x => x.Selected).Select(x => x.Id).ToArray()), quarterMonthId, quarterMonthType, SettlementTypeValue);

            List<NsMucLucNganSach> listMucLuc = new List<NsMucLucNganSach>();
            List<string> lns = new List<string>();
            foreach (var item in lnsQueries)
            {
                lns.Add(item.LNS1);
                lns.Add(item.LNS3);
                lns.Add(item.LNS);
            }

            listMucLuc = lns.Distinct().OrderBy<string, string>((Func<string, string>)(x => x)).Select<string, NsMucLucNganSach>((Func<string, NsMucLucNganSach>)(x =>
            {
                var mlns = _listMucLucNganSach.FirstOrDefault<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(m => m.XauNoiMa == x));
                return new NsMucLucNganSach()
                {
                    Lns = x,
                    XauNoiMa = x,
                    MoTa = mlns == null ? string.Empty : mlns.MoTa,
                    MlnsId = mlns == null ? Guid.Empty : (Guid)mlns.MlnsId,
                    MlnsIdParent = mlns == null ? Guid.Empty : (mlns.MlnsIdParent == null ? Guid.Empty : (Guid)mlns.MlnsIdParent),
                    ILoaiNganSach = mlns == null ? -1 : (mlns.ILoaiNganSach == null ? -1 : mlns.ILoaiNganSach),
                    IdPhongBan = mlns == null ? String.Empty : (mlns.IdPhongBan == null ? String.Empty : mlns.IdPhongBan),
                };
            })).Where<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(x => !string.IsNullOrEmpty(x.MoTa))).ToList();




            var a = listMucLuc;
            if (LoaiNganSachSelected != null && !LoaiNganSachSelected.ValueItem.Equals("-1"))
            {
                listMucLuc = listMucLuc.Where(x => x.ILoaiNganSach == int.Parse(LoaiNganSachSelected.ValueItem)).ToList();
                _listMucLucNganSachResult = ObjectCopier.Clone(listMucLuc);
                foreach (var item in listMucLuc)
                {
                    GetParentmlns(item, a);
                }
                listMucLuc = ObjectCopier.Clone(_listMucLucNganSachResult);
                listMucLuc = listMucLuc.OrderBy(x => x.XauNoiMa).ToList();

            }
            if (SelectedBQuanLy != null && !SelectedBQuanLy.ValueItem.Equals(String.Empty))
            {
                listMucLuc = listMucLuc.Where(x => x.IdPhongBan.Equals(SelectedBQuanLy.ValueItem)).ToList();
                _listMucLucNganSachResult = ObjectCopier.Clone(listMucLuc);
                foreach (var item in listMucLuc)
                {
                    GetParentmlns(item, a);
                }
                listMucLuc = ObjectCopier.Clone(_listMucLucNganSachResult);
                listMucLuc = listMucLuc.OrderBy(x => x.XauNoiMa).ToList();
            }



            BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLuc);

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                    {
                        foreach (NsMuclucNgansachModel item in BudgetIndexes)
                        {
                            if (item.MlnsIdParent == model.MlnsId)
                                item.IsSelected = model.IsSelected;
                        }
                        OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                        OnPropertyChanged(nameof(IsExportEnable));
                        OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
        }

        private void GetParentmlns(NsMucLucNganSach item, List<NsMucLucNganSach> lstmlnsitem)
        {
            var returnitem = lstmlnsitem.Where(x => x.MlnsId.Equals(item.MlnsIdParent)).FirstOrDefault();
            if (returnitem != null && !_listMucLucNganSachResult.Where(x => x.MlnsId == returnitem.MlnsId).Any())
            //if(returnitem != null ) 
            {
                _listMucLucNganSachResult.Add(returnitem);
                GetParentmlns(returnitem, lstmlnsitem);
            }
        }


        private void GetParentmlns(string item, List<NsMucLucNganSach> lstmlnsitem)
        {
            var returnitem = lstmlnsitem.Where(x => x.Lns.Equals(item)).FirstOrDefault();
            if (returnitem != null && !_listMucLucNganSachResult.Where(x => x.MlnsId == returnitem.MlnsId).Any())
            //if(returnitem != null ) 
            {
                _listMucLucNganSachResult.Add(returnitem);
                GetParentmlns(returnitem.Lns, lstmlnsitem);
            }
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri)
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
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
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
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => "CÁC ĐƠN VỊ",
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }


        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                    int iKhoi = -1;
                    if (SelectedKhoi != null && !SelectedKhoi.ValueItem.IsEmpty()) iKhoi = int.Parse(SelectedKhoi.ValueItem);
                    List<ReportQtThongTriDonViQuery> _reportThongTriDonVi = _chungTuChiTietService.FindForSettlementAgencyReport(
                        _sessionInfo.YearOfWork,
                        _sessionInfo.YearOfBudget,
                        _sessionInfo.Budget,
                        string.Join(",", _agencies.Where(x => x.Selected).Select(x => x.Id).ToArray()),
                        QuarterMonthSelected.ValueItem,
                        string.Join(",", _budgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToArray()),
                        dvt,
                        IsInTongHop,
                        iKhoi).OrderBy(x => x.Id_DonVi).ToList();
                    _reportThongTriDonVi = _reportThongTriDonVi.Where(x => x.TuChi != 0).ToList();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    RptQuyetToanThongTriDonVi rptDonVi = new RptQuyetToanThongTriDonVi();
                    rptDonVi.TieuDe1 = Title1;
                    rptDonVi.TieuDe2 = Title2;
                    rptDonVi.TieuDe3 = Title3;
                    rptDonVi.header2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                    rptDonVi.Cap2 = GetLevelTitle(_dmChuKy, 1);
                    rptDonVi.Cap3 = GetLevelTitle(_dmChuKy, 2);
                    //rptDonVi.Cap2 = _sessionInfo.TenDonVi;
                    //rptDonVi.Cap3 = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).FirstOrDefault()?.SGiaTri;
                    rptDonVi.Nam = DateTime.Now.Year.ToString();
                    rptDonVi.DonVi = "CÁC ĐƠN VỊ";
                    rptDonVi.ThoiGian = QuarterMonthSelected.DisplayItem + " năm " + _sessionInfo.YearOfWork;
                    rptDonVi.TongTuChi = _reportThongTriDonVi.Select(x => x.TuChi == null ? 0 : x.TuChi.Value).Sum();
                    rptDonVi.TienTuChi = StringUtils.NumberToText(rptDonVi.TongTuChi * dvt);
                    rptDonVi.Ngay = DateUtils.FormatDateReport(ReportDate);
                    rptDonVi.DiaDiem = _diaDiem;
                    rptDonVi.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                    rptDonVi.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                    rptDonVi.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                    rptDonVi.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                    rptDonVi.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                    rptDonVi.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                    rptDonVi.ThuaUyQuyen1 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen1MoTa : string.Empty;
                    rptDonVi.ThuaUyQuyen2 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen2MoTa : string.Empty;
                    rptDonVi.ThuaUyQuyen3 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen3MoTa : string.Empty;
                    rptDonVi.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                    rptDonVi.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                    rptDonVi.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                    rptDonVi.Items = _reportThongTriDonVi;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                    data.Add("FormatNumber", formatNumber);
                    foreach (var prop in rptDonVi.GetType().GetProperties())
                    {
                        data.Add(prop.Name, prop.GetValue(rptDonVi));
                    }

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_THONGTRI_DONVI);
                    string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_THONGTRI_DONVI.Split(".").First();
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportQtThongTriDonViQuery>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
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

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_DONVI;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
