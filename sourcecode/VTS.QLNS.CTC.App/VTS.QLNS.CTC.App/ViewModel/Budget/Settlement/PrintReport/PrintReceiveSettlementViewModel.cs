using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static Microsoft.SqlServer.Management.Sdk.Sfc.RequestObjectInfo;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintReceiveSettlementViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IExportService _exportService;
        private INsDonViService _donViService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private readonly IDmMucLucQuyetToanService _dmMucLucQuyetToanService;
        private IMapper _mapper;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private IDanhMucService _danhMucService;
        private INsPhongBanService _phongBanService;
        private IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungLnsService _nsNguoiDungLNSService;
        private ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _listMucLucNganSach;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn MLQT ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _isHasData;

        public override string Name => "Báo cáo tình hình nhận và quyết toán kinh phí";
        public override string Title => "Báo cáo tình hình nhận và quyết toán kinh phí";
        public override string Description => "Chọn in tổng hợp đơn vị hoặc chi tiết từng đơn vị";

        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintReceiveSettlement);

        private ObservableCollection<ComboboxItem> _loaiBaoCao;
        public ObservableCollection<ComboboxItem> LoaiBaoCao
        {
            get => _loaiBaoCao;
            set => SetProperty(ref _loaiBaoCao, value);
        }

        private bool _inTheoTongHop;
        public bool InTheoTongHop
        {
            get => _inTheoTongHop;
            set
            {
                SetProperty(ref _inTheoTongHop, value);
                LoadAgencies();
            }
        }

        public bool IsShowInTheoTongHop => _sessionService.Current.IsQuanLyDonViCha && SelectedLoaiBaoCao != null && SelectedLoaiBaoCao.ValueItem == Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI;

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                SetProperty(ref _selectedLoaiBaoCao, value);
                OnPropertyChanged(nameof(IsShowInTheoTongHop));
                InTheoTongHop = false;
            }
        }




        public bool Flag { get; set; } = false;

        #region list LNS
        private ObservableCollection<DmMucLucQuyetToanModel> _budgetIndexes = new ObservableCollection<DmMucLucQuyetToanModel>();
        public ObservableCollection<DmMucLucQuyetToanModel> BudgetIndexes
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
            get => BudgetIndexes.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                Flag = true;
                BudgetIndexes.ForAll(x => x.IsSelected = value);
                Flag = false;
                LoadAgencies();
            }
        }
        #endregion

        #region list agency
        private ObservableCollection<AgencyModel> _agencies = new ObservableCollection<AgencyModel>();
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
                    LoadAgencies();
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
        private bool _isSelectAllAgency;
        public bool IsSelectAllAgency
        {
            get => Agencies.Count() > 0 && Agencies.All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectAllAgency, value);
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectAllAgency;
                }
            }
        }
        #endregion

        public bool IsExportEnable
        {
            get
            {
                if (_budgetIndexes != null && _agencies != null)
                    return _budgetIndexes.Where(x => x.IsSelected).Any()
                        && _agencies.Where(x => x.Selected).Any();
                return false;
            }
        }

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

        private ObservableCollection<ComboboxItem> _dataInToiMuc;
        public ObservableCollection<ComboboxItem> DataInToiMuc
        {
            get => _dataInToiMuc;
            set => SetProperty(ref _dataInToiMuc, value);
        }

        private ComboboxItem _selectedInToiMuc;
        public ComboboxItem SelectedInToiMuc
        {
            get => _selectedInToiMuc;
            set => SetProperty(ref _selectedInToiMuc, value);
        }

        private ObservableCollection<ComboboxItem> _cbbTypeYearBudgets;
        public ObservableCollection<ComboboxItem> CbbTypeYearBudgets
        {
            get => _cbbTypeYearBudgets;
            set => SetProperty(ref _cbbTypeYearBudgets, value);
        }

        private ComboboxItem _cbbTypeYearBudgetSelected;
        public ComboboxItem CbbTypeYearBudgetSelected
        {
            get => _cbbTypeYearBudgetSelected;
            set
            {
                SetProperty(ref _cbbTypeYearBudgetSelected, value);
                LoadBudgetIndexes();
                LoadAgencies();
            }
        }



        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReceiveSettlementViewModel(ISessionService sessionService,
            IExportService exportService,
            INsDonViService donViService,
            INsMucLucNganSachService mucLucNganSachService,
            IMapper mapper,
            INsQtChungTuChiTietService chungTuChiTietService,
            IDanhMucService danhMucService,
            INsNguoiDungLnsService nsNguoiDungLNSService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDmMucLucQuyetToanService dmMucLucQuyetToanService,
            INsPhongBanService phongBanService,
            ILog logger)
        {
            _sessionService = sessionService;
            _exportService = exportService;
            _donViService = donViService;
            _mucLucNganSachService = mucLucNganSachService;
            _mapper = mapper;
            _chungTuChiTietService = chungTuChiTietService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _phongBanService = phongBanService;
            _nsNguoiDungLNSService = nsNguoiDungLNSService;
            _dmMucLucQuyetToanService = dmMucLucQuyetToanService;
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
            _sessionInfo = _sessionService.Current;
            InTheoTongHop = false;
            InitReportDefaultDate();
            LoadBQuanLy();
            _sessionInfo = _sessionService.Current;
            LoadTieuDe();
            LoadDanhMuc();
            LoadChiTietToi();
            LoadLoaiBaoCao();
            LoadQuater();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NHAN_KINHPHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            else
                Title2 = $"Năm {_sessionInfo.YearOfWork}";
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;

            var years = _danhMucService.FindByType(TypeDanhMuc.NS_NamNganSach);
        }

        public void LoadLoaiBaoCao()
        {
            LoaiBaoCao = new ObservableCollection<ComboboxItem>();
            LoaiBaoCao.Add(new ComboboxItem { DisplayItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI, ValueItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI });
            LoaiBaoCao.Add(new ComboboxItem { DisplayItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI, ValueItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI });
            SelectedLoaiBaoCao = LoaiBaoCao.FirstOrDefault();
        }

        private List<string> GetListLNSByUser()
        {
            var predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionService.Current.Principal);
            List<NsNguoiDungLns> listNguoiDungDonVi = _nsNguoiDungLNSService.FindAll(predicate).ToList();
            return listNguoiDungDonVi.Select(x => x.SLns).ToList();
        }

        private void LoadBQuanLy()
        {
            List<DmBQuanLy> listPhongBan = _phongBanService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork);
            _bQuanLy = _mapper.Map<List<ComboboxItem>>(listPhongBan);
            if (_bQuanLy.Count() > 0)
            {
                _bQuanLy.Insert(0, new ComboboxItem("Tất cả", string.Empty));
                SelectedBQuanLy = _bQuanLy.First();
            }
        }

        private void LoadBudgetIndexes()
        {
            List<NsMucLucQuyetToanNam> listMucLuc = new List<NsMucLucQuyetToanNam>();
            if (CbbTypeYearBudgetSelected is null) { return; }
            if (CbbTypeYearBudgetSelected.ValueItem == "2") // Nam truoc
            {
                var reportData = _chungTuChiTietService.FindForReceiveSettlementReport(new ReportSettlementCriteria
                {
                    YearOfWork = _sessionInfo.YearOfWork,
                    Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                    YearOfBudgets = "3",
                    BudgetSource = _sessionInfo.Budget,
                    AgencyId = string.Empty,
                    MaString = string.Empty,
                    MaBQuanLy = SelectedBQuanLy?.ValueItem ?? string.Empty,
                });
                CalculateData(reportData);
                var listMa = reportData.Where(x => x.HasData).Select(x => x.Ma);

                var dmQuyetToanNam = _dmMucLucQuyetToanService.FindByCondition(x => listMa.Contains(x.Ma) && x.NamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.Ma).ToList();
                BudgetIndexes = _mapper.Map<ObservableCollection<DmMucLucQuyetToanModel>>(dmQuyetToanNam);

            }
            else if (CbbTypeYearBudgetSelected.ValueItem == "3") // nam nay
            {
                var reportData = _chungTuChiTietService.FindForReceiveSettlementReport(new ReportSettlementCriteria
                {
                    YearOfWork = _sessionInfo.YearOfWork,
                    Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                    YearOfBudgets = "2",
                    BudgetSource = _sessionInfo.Budget,
                    AgencyId = string.Empty,
                    MaString = string.Empty,
                    MaBQuanLy = SelectedBQuanLy?.ValueItem ?? string.Empty,
                });
                CalculateData(reportData);
                var listMa = reportData.Where(x => x.HasData).Select(x => x.Ma);

                var dmQuyetToanNam = _dmMucLucQuyetToanService.FindByCondition(x => listMa.Contains(x.Ma) && x.NamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.Ma).ToList();
                BudgetIndexes = _mapper.Map<ObservableCollection<DmMucLucQuyetToanModel>>(dmQuyetToanNam);
            }
            else
            {
                var reportData = _chungTuChiTietService.FindForReceiveSettlementReport(new ReportSettlementCriteria
                {
                    YearOfWork = _sessionInfo.YearOfWork,
                    Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                    YearOfBudgets = "2,3",
                    BudgetSource = _sessionInfo.Budget,
                    AgencyId = string.Empty,
                    MaString = string.Empty,
                    MaBQuanLy = SelectedBQuanLy?.ValueItem ?? string.Empty,
                });
                CalculateData(reportData);
                var listMa = reportData.Where(x => x.HasData).Select(x => x.Ma);

                var dmQuyetToanNam = _dmMucLucQuyetToanService.FindByCondition(x => listMa.Contains(x.Ma) && x.NamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.Ma).ToList();
                BudgetIndexes = _mapper.Map<ObservableCollection<DmMucLucQuyetToanModel>>(dmQuyetToanNam);

            }

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DmMucLucQuyetToanModel.IsSelected) && !Flag)
                    {
                        Flag = true;
                        SetCheckChildren(BudgetIndexes, model);
                        SetCheckParent(BudgetIndexes, model);
                        Flag = false;
                        LoadAgencies();
                        OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                        OnPropertyChanged(nameof(IsExportEnable));
                        OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
            OnPropertyChanged(nameof(IsExportEnable));
        }

        private void SetCheckChildren(ObservableCollection<DmMucLucQuyetToanModel> items, DmMucLucQuyetToanModel item)
        {
            foreach (var e in items)
            {
                if (e.SMaCha == item.SMa)
                {
                    e.IsSelected = item.IsSelected;
                    SetCheckChildren(items, e);
                }
            }
        }

        private void SetCheckParent(ObservableCollection<DmMucLucQuyetToanModel> items, DmMucLucQuyetToanModel item)
        {
            foreach (var e in items)
            {
                if (e.SMa == item.SMaCha)
                {
                    e.IsSelected = items.Where(x => x.SMaCha == item.SMaCha).All(x => x.IsSelected);
                    SetCheckParent(items, e);
                }
            }
        }

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                if (CbbTypeYearBudgetSelected is null) return;
                if (InTheoTongHop)
                {
                    IEnumerable<DonVi> listDonvi = _donViService.FindByLoai(_sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                    Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonvi);
                }
                else
                {
                    List<DonVi> _listDonVi = _donViService.FindForReceiveSettlementReport(_sessionInfo.YearOfWork,
                    CbbTypeYearBudgetSelected.ValueItem switch
                    {
                        "1" => "2,3",
                        "2" => "3",
                        "3" => "2",
                        _ => throw new Exception()
                    },
                    _sessionInfo.Budget, string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.SMa))).ToList();
                    var idsDonViQuanLy = _sessionService.Current.DonViQuanLy.Select(x => x.IIDMaDonVi);
                    if (!_listDonVi.Any(x => idsDonViQuanLy.Contains(x.IIDMaDonVi) && x.Loai == LoaiDonVi.ROOT))
                    {
                        _listDonVi = _listDonVi.Where(x => idsDonViQuanLy.Contains(x.IIDMaDonVi)).ToList();
                    }
                    _listDonVi = _listDonVi.Where(x => x.Loai == LoaiDonVi.NOI_BO).ToList();

                    Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(_listDonVi);
                }
            }, (s, e) =>
            {
                IsLoading = false;
                _listAgency = CollectionViewSource.GetDefaultView(Agencies);
                _listAgency.Filter = ListAgencyFilter;
                foreach (var model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected))
                        {
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                            OnPropertyChanged(nameof(IsExportEnable));
                            OnPropertyChanged(nameof(IsSelectAllAgency));
                        }
                    };
                }
                OnPropertyChanged(nameof(SelectedAgencyCount));
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(IsSelectAllAgency));
            });
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
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

        private void LoadQuater()
        {
            CbbTypeYearBudgets = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = "1", DisplayItem = "Tổng hợp" },
                new ComboboxItem { ValueItem = "2", DisplayItem = "Năm trước chuyển sang" },
                new ComboboxItem { ValueItem = "3", DisplayItem = "Năm nay" }
            };

            CbbTypeYearBudgetSelected = CbbTypeYearBudgets.FirstOrDefault(n => n.ValueItem == "1");

        }

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
                _selectedInToiMuc = DataInToiMuc.FirstOrDefault(n => n.DisplayItem == "TNG");
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

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    ExportResult exportResult = default;
                    switch (SelectedLoaiBaoCao.ValueItem)
                    {
                        case Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI:
                            foreach (var agency in Agencies.Where(x => x.Selected))
                            {
                                exportResult = ProcessExport(agency.Id, agency.TenDonVi, exportType);
                                if (exportResult != null)
                                    results.Add(exportResult);
                            }
                            break;
                        case Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI:
                            exportResult = ProcessExport(string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id)), "Tổng hợp", exportType);
                            if (exportResult != null)
                                results.Add(exportResult);
                            break;
                        default:
                            break;
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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
                MessageBox.Show(Resources.ErrorExportReport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CalculateData(List<ReportQtNhanQuyetToanKinhPhiQuery> list)
        {
            list.Where(x => x.IsHangCha).Select(x =>
            {
                x.DuToanNganSach = 0;
                x.KinhPhiDuocCap = 0;
                x.KinhPhiDeNghi = 0;
                x.ChuyenNamSauDaCap = 0;
                x.ChuyenNamSauTongSo = 0;
                return x;
            }).ToList();
            var children = list.Where(x => !x.IsHangCha && x.HasData && !string.IsNullOrEmpty(x.MaCha)).ToList();
            var dict = list.Where(x => !string.IsNullOrEmpty(x.Ma)).GroupBy(x => x.Ma).ToDictionary(x => x.Key, x => x.FirstOrDefault());
            foreach (var child in children)
            {
                CalculateParent(dict, child.MaCha, child);
            }
        }

        private void CalculateParent(Dictionary<string, ReportQtNhanQuyetToanKinhPhiQuery> dict, string maCha, ReportQtNhanQuyetToanKinhPhiQuery item)
        {
            if (!string.IsNullOrEmpty(maCha) && dict.TryGetValue(maCha, out var parentItem))
            {
                parentItem.DuToanNganSach += item.DuToanNganSach;
                parentItem.KinhPhiDuocCap += item.KinhPhiDuocCap;
                parentItem.KinhPhiDeNghi += item.KinhPhiDeNghi;
                parentItem.ChuyenNamSauDaCap += item.ChuyenNamSauDaCap;
                parentItem.ChuyenNamSauTongSo += item.ChuyenNamSauTongSo;
                CalculateParent(dict, parentItem.MaCha, item);
            }
            else
            {
                return;
            }
        }

        private ExportResult ProcessExport(string agencyIds, string agencyNam, ExportType exportType)
        {
            try
            {
                List<ReportQtNhanQuyetToanKinhPhiQuery> reportData = new List<ReportQtNhanQuyetToanKinhPhiQuery>();
                if (CbbTypeYearBudgetSelected.ValueItem == "2")
                {
                    reportData = _chungTuChiTietService.FindForReceiveSettlementReport(new ReportSettlementCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                        YearOfBudgets = "3",
                        BudgetSource = _sessionInfo.Budget,
                        AgencyId = agencyIds,
                        MaString = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.SMa))
                    });
                    reportData.Where(x => string.IsNullOrEmpty(x.MaCha)).Select(x => x.MaCha = "0A").ToList();
                    var parent = new ReportQtNhanQuyetToanKinhPhiQuery
                    {
                        STT = "A",
                        MoTa = "NĂM TRƯỚC CHUYỂN SANG",
                        Ma = "0A",
                        MaCha = "0",
                        IsHangCha = true
                    };
                    reportData.Insert(0, parent);
                }
                else if (CbbTypeYearBudgetSelected.ValueItem == "3")
                {
                    reportData = _chungTuChiTietService.FindForReceiveSettlementReport(new ReportSettlementCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                        YearOfBudgets = "2",
                        BudgetSource = _sessionInfo.Budget,
                        AgencyId = agencyIds,
                        MaString = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.SMa))
                    });
                    reportData.Where(x => string.IsNullOrEmpty(x.MaCha)).Select(x => x.MaCha = "0B").ToList();
                    var parent = new ReportQtNhanQuyetToanKinhPhiQuery
                    {
                        STT = "B",
                        MoTa = "NĂM NAY",
                        Ma = "0B",
                        MaCha = "0",
                        IsHangCha = true
                    };
                    reportData.Insert(0, parent);
                }
                else
                {
                    var reportData1 = _chungTuChiTietService.FindForReceiveSettlementReport(new ReportSettlementCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                        YearOfBudgets = "3",
                        BudgetSource = _sessionInfo.Budget,
                        AgencyId = agencyIds,
                        MaString = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.SMa))
                    });
                    reportData1.Select(x =>
                    {
                        x.Ma = x.Ma + "A";
                        if (string.IsNullOrEmpty(x.MaCha))
                        {
                            x.MaCha = "0A";
                        }
                        else
                        {
                            x.MaCha = x.MaCha + "A";
                        }
                        return x;
                    }).ToList();
                    var parent1 = new ReportQtNhanQuyetToanKinhPhiQuery
                    {
                        STT = "A",
                        MoTa = "NĂM TRƯỚC CHUYỂN SANG",
                        Ma = "0A",
                        MaCha = "0",
                        IsHangCha = true
                    };
                    reportData1.Insert(0, parent1);
                    var reportData2 = _chungTuChiTietService.FindForReceiveSettlementReport(new ReportSettlementCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                        YearOfBudgets = "2",
                        BudgetSource = _sessionInfo.Budget,
                        AgencyId = agencyIds,
                        MaString = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.SMa))
                    });
                    reportData2.Select(x =>
                    {
                        x.Ma = x.Ma + "B";
                        if (string.IsNullOrEmpty(x.MaCha))
                        {
                            x.MaCha = "0B";
                        }
                        else
                        {
                            x.MaCha = x.MaCha + "B";
                        }
                        return x;
                    }).ToList();
                    var parent2 = new ReportQtNhanQuyetToanKinhPhiQuery
                    {
                        STT = "B",
                        MoTa = "NĂM NAY",
                        Ma = "0B",
                        MaCha = "0",
                        IsHangCha = true
                    };
                    reportData2.Insert(0, parent2);
                    reportData.AddRange(reportData1);
                    reportData.AddRange(reportData2);
                }

                if (!reportData.Exists(x => x.HasData)) return null;

                CalculateData(reportData);
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NHAN_KINHPHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                string tenDonViDuocChon = SelectedLoaiBaoCao.ValueItem switch
                {
                    Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI => _sessionInfo.TenDonVi,
                    _ => agencyNam
                };


                (string DonViBanHanh1, string DonViBanHanh2) GetLevelTitle()
                {
                    var danhMucChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NHAN_KINHPHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);
                    var loaiDVBanHanh1 = danhMucChuKy?.LoaiDVBanHanh1;
                    var loaiDVBanHanh2 = danhMucChuKy?.LoaiDVBanHanh2;

                    return (loaiDVBanHanh1 switch
                    {
                        LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                        LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                        LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                        LoaiDonViBanHanh.DON_VI_DUOC_CHON => tenDonViDuocChon,
                        LoaiDonViBanHanh.TUY_CHINH => danhMucChuKy.TenDVBanHanh1,
                        _ => string.Empty
                    }, loaiDVBanHanh2 switch
                    {
                        LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                        LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                        LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                        LoaiDonViBanHanh.DON_VI_DUOC_CHON => tenDonViDuocChon,
                        LoaiDonViBanHanh.TUY_CHINH => danhMucChuKy.TenDVBanHanh2,
                        _ => string.Empty
                    });
                }

                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    ["LoaiBaoCao"] = _cbbTypeYearBudgetSelected != null ? _cbbTypeYearBudgetSelected.DisplayItem : string.Empty,
                    ["DonVi"] = tenDonViDuocChon,
                    ["YearOfWork"] = _sessionInfo.YearOfWork,
                    ["Items"] = reportData.Where(x => x.HasData),
                    ["Cap1"] = GetLevelTitle().DonViBanHanh1,
                    ["Cap2"] = GetLevelTitle().DonViBanHanh2,
                    ["TieuDe1"] = Title1,
                    ["TieuDe2"] = Title2,
                    ["TieuDe3"] = Title3,
                    ["Ngay"] = DateUtils.FormatDateReport(ReportDate),
                    ["DiaDiem"] = _diaDiem,
                    ["h2"] = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem),
                    ["ChucDanh1"] = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty,
                    ["ChucDanh2"] = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty,
                    ["ChucDanh3"] = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty,
                    ["ThuaLenh1"] = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty,
                    ["ThuaLenh2"] = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty,
                    ["ThuaLenh3"] = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty,
                    ["Ten1"] = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty,
                    ["Ten2"] = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty,
                    ["Ten3"] = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty,
                    //["TongCongBangChu"] = StringUtils.NumberToText(tongSoTien * dvt),
                };
                FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);

                string fileName = string.Empty;
                fileName = ExportFileName.RPT_NS_QUYETTOAN_NHAN_KINHPHI;

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, fileName);
                string fileNamePrefix = string.Format("{0}_{1}", fileName.Split(".").First(), agencyNam);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportQtNhanQuyetToanKinhPhiQuery>(templateFileName, data);
                return new ExportResult(agencyNam, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }


        private List<PrintYearSummarySettlementQuery> ProcessSummaryData2(bool isSetHangCha, List<PrintYearSummarySettlementQuery> items)
        {
            List<PrintYearSummarySettlementQuery> result = new List<PrintYearSummarySettlementQuery>();
            result = items.GroupBy(g => new { g.MLNS_Id, g.MLNS_Id_Parent, g.LNS, g.L, g.K, g.M, g.TM, g.TTM, g.NG, g.TNG, g.TNG1, g.TNG2, g.TNG3, g.XauNoiMa, g.MoTa, g.IsHangCha }).Select(x => new PrintYearSummarySettlementQuery
            {
                MLNS_Id = x.Key.MLNS_Id,
                MLNS_Id_Parent = x.Key.MLNS_Id_Parent.HasValue ? x.Key.MLNS_Id_Parent.Value : Guid.Empty,
                LNS = x.Key.LNS,
                L = x.Key.L,
                K = x.Key.K,
                M = x.Key.M,
                TM = x.Key.TM,
                TTM = x.Key.TTM,
                NG = x.Key.NG,
                TNG = x.Key.TNG,
                TNG1 = x.Key.TNG1,
                TNG2 = x.Key.TNG2,
                TNG3 = x.Key.TNG3,
                XauNoiMa = x.Key.XauNoiMa,
                MoTa = x.Key.MoTa,
                IsHangCha = isSetHangCha ? true : x.Key.IsHangCha,
                DuToanNganSach = x.Sum(rpt => rpt.DuToanNganSach),
                SoDeNghiQuyetToan = x.Sum(rpt => rpt.SoDeNghiQuyetToan),
                SoChuyenNamSau = x.Sum(c => c.SoChuyenNamSau)
            }).ToList();
            return result;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_NHAN_KINHPHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOAN_NHAN_KINHPHI;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }


    }
}
