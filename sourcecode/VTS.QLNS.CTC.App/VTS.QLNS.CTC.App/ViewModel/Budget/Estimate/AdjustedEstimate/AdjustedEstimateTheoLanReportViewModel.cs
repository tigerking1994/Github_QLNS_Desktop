using AutoMapper;
using log4net;
using Newtonsoft.Json;
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
using VTS.QLNS.CTC.App.View.Budget.Estimate.AdjustedEstimate;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate
{
    public class AdjustedEstimateTheoLanReportViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _listDonViView;
        private readonly INsDcChungTuService _chungTuService;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly INsPhongBanService _iNsPhongBanService;
        private readonly INsDonViService _donViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ISessionService _sessionService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IExportService _exportService;
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly DanhMucNganhService _danhMucNganhService;
        private readonly DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private readonly List<NsDcChungTuQuery> _listChungTu;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private List<DanhMuc> _listDanhMucNganh;
        private ICollectionView _listBudgetIndex;
        private List<DcChungTuChiTietModel> _listChungTuChiTiet;
        private AdjustedEstimateDetailTotalModel _detailTotal;
        public NsBaoCaoGhiChuDialogViewModel NsBaoCaoGhiChuDialogViewModel { get; }
        public override string Name => "Báo cáo điều chỉnh dự toán - Đơn vị";
        public override string Title => "Báo cáo điều chỉnh dự toán - Đơn vị";
        public override string Description => "Báo cáo điều chỉnh dự toán - Đơn vị";
        public override Type ContentType => typeof(AdjustedEstimateTheoLanReport);
        public AjustmentEstimatePrintType PrintType { get; set; }
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }

        public bool IsCalculating { get; set; } = false;

        #region list LNS

        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
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
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes != null && BudgetIndexes.Any() && BudgetIndexes.Where(x => x.IsFilter).All(item => item.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                if (BudgetIndexes != null)
                {
                    IsCalculating = true;
                    BudgetIndexes.Select(c => { c.IsSelected = _isSelectAllBudgetIndex; return c; }).ToList();
                    IsCalculating = false;
                    OnPropertyChanged(nameof(IsExportEnable));
                }
            }
        }
        #endregion


        private List<ComboboxItem> _dataDot;
        public List<ComboboxItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }


        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Count : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _inTangGiam;

        public bool InTangGiam
        {
            get => _inTangGiam;
            set => SetProperty(ref _inTangGiam, value);
        }

        private bool _congTangGiam;
        public bool CongTangGiam
        {
            get => _congTangGiam;
            set => SetProperty(ref _congTangGiam, value);
        }
        private bool _inChuyenNamSau;
        public bool InChuyenNamSau
        {
            get => _inChuyenNamSau;
            set => SetProperty(ref _inChuyenNamSau, value);
        }

        private ObservableCollection<ComboboxItem> _cbxBQuanLy;
        public ObservableCollection<ComboboxItem> CbxBQuanLy
        {
            get => _cbxBQuanLy;
            set => SetProperty(ref _cbxBQuanLy, value);
        }

        private ComboboxItem _cbxBQuanLySelected;
        public ComboboxItem CbxBQuanLySelected
        {
            get => _cbxBQuanLySelected;
            set
            {
                SetProperty(ref _cbxBQuanLySelected, value);
                if (!IsCalculating) LoadBudgetIndexes();
            }
        }

        private void LoadBudgetIndexes()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var predicate = PredicateBuilder.True<NsDcChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.INamNganSach == _sessionInfo.YearOfBudget
                                                && x.ILoaiDuKien == int.Parse(DataDotSelected.ValueItem)
                                                && x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => ListDonVi.Where(n => n.IsChecked).Select(z => z.ValueItem).Contains(x.IIdMaDonVi));
                var allchungtu = _chungTuService.FindByCondition(predicate);
                var chungtus = allchungtu.Where(x => ListDonVi.Where(n => n.IsChecked).Select(x => x.ValueItem).Contains(x.IIdMaDonVi));
                if (chungtus.IsEmpty()) return;

                //List<string> listLNSChungTu = chungTu.SDslns.Split(",").ToList();

                List<string> listLNSChungTu = new List<string>();
                foreach (var ct in chungtus)
                {
                    listLNSChungTu.AddRange(ct.SDslns.Split(",").ToList());
                }

                IEnumerable<string> listLNS = _mucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork
                                            && listLNSChungTu.Contains(x.Lns)
                                            && (CbxBQuanLySelected.ValueItem == "0" || CbxBQuanLySelected.ValueItem == x.IdPhongBan)).Select(x => x.Lns).Distinct();

                EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
                {
                    VoucherIds = string.Join(",", chungtus.Select(x => x.Id)),
                    //LNS = chungTu.SDslns,
                    LNS = string.Join(",", listLNS),
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    IdDonVi = string.Join(",", ListDonVi.Where(n => n.IsChecked).Select(x => x.ValueItem)),
                    LoaiDuKien = int.Parse(DataDotSelected.ValueItem),
                    //LoaiDuKien = chungTu.ILoaiDuKien,
                    //LoaiChungTu = chungTu.ILoaiChungTu,
                    //VoucherDate = chungTu.DNgayChungTu,
                    UserName = _sessionInfo.Principal
                };

                var data = _chungTuChiTietService.FindAllNSDCChungTuByCondition(searchCondition).ToList();
                var lns = data.Where(x => !string.IsNullOrEmpty(x.SLns) && x.HasDataSummary).Select(x => x.SLns).Distinct();

                var listMucLuc = _mucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork
                                            && lns.Contains(x.Lns)
                                            && CbxBQuanLySelected != null
                                            && string.IsNullOrEmpty(x.L)
                                            && (CbxBQuanLySelected.ValueItem == "0" || CbxBQuanLySelected.ValueItem == x.IdPhongBan)).Distinct().ToList();

                e.Result = listMucLuc;
            }, (s, e) =>
            {
                List<NsMucLucNganSach> listMLNS = e.Result as List<NsMucLucNganSach> ?? new List<NsMucLucNganSach>();
                BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMLNS);

                _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
                _listBudgetIndex.Filter = ListBudgetIndexFilter;
                foreach (var model in BudgetIndexes)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected) && !IsCalculating)
                        {
                            IsCalculating = true;
                            SetCheckChildren(BudgetIndexes, model);
                            SetCheckParent(BudgetIndexes, model);
                            IsCalculating = false;
                            OnPropertyChanged(nameof(IsExportEnable));
                            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        }
                    };
                }
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                IsLoading = false;
            });

        }

        private void SetCheckChildren(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsIdParent == item.MlnsId)
                {
                    e.IsSelected = item.IsSelected;
                    SetCheckChildren(items, e);
                }
            }
        }

        private void SetCheckParent(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsId == item.MlnsIdParent)
                {
                    e.IsSelected = items.Where(x => x.MlnsIdParent == item.MlnsIdParent).All(x => x.IsSelected);
                    SetCheckParent(items, e);
                }
            }
        }
        private bool ListBudgetIndexFilter(object obj)
        {
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchBudgetIndexText))
                result = item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public bool? SelectAllDonVi
        {
            get
            {
                if (ListDonVi != null)
                {
                    List<bool> selected = ListDonVi.Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    IsCalculating = true;
                    SelectAll(value.Value, ListDonVi);
                    LoadBudgetIndexes();
                    IsCalculating = false;
                }
            }
        }

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (CheckBoxItem model in models)
            {
                model.IsChecked = select;
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _listDonViView.Refresh();
                }
            }
        }

        public bool IsExportEnable
        {
            get
            {
                return ListDonVi != null && ListDonVi.Where(n => n.IsChecked).Any()
                    && _budgetIndexes != null && _budgetIndexes.Where(x => x.IsSelected).Any();
            }
        }

        private ComboboxItem _dataDotSelected;
        public ComboboxItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                LoadDonVi();
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

        public bool IsShowBQuanLy => true;

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

        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }

        public AdjustedEstimateTheoLanReportViewModel(
            IMapper mapper,
            ILog logger,
            INsDcChungTuService chungTuService,
            INsDcChungTuChiTietService chungTuChiTietService,
            INsDonViService donViService,
            ISessionService sessionService,
            IDanhMucService danhMucSerivce,
            INsMucLucNganSachService mucLucNganSachService,
            INsPhongBanService iNsPhongBanService,
            IDmChuKyService dmChuKyService,
            IExportService exportService,
            DanhMucNganhService danhMucNganhService,
            NsBaoCaoGhiChuDialogViewModel nsBaoCaoGhiChuDialogViewModel,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            INsBaoCaoGhiChuService ghiChuService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _donViService = donViService;
            _mucLucNganSachService = mucLucNganSachService;
            _danhMucService = danhMucSerivce;
            _dmChuKyService = dmChuKyService;
            _iNsPhongBanService = iNsPhongBanService;
            _exportService = exportService;
            _danhMucNganhService = danhMucNganhService;
            _ghiChuService = ghiChuService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            NsBaoCaoGhiChuDialogViewModel = nsBaoCaoGhiChuDialogViewModel;

            ExportExcelCommand = new RelayCommand(obj => OnExportFile(ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            PrintCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            IsCalculating = true;
            InChuyenNamSau = false;
            InTangGiam = false;
            CongTangGiam = false;
            InitReportDefaultDate();
            LoadDataDot();
            LoadDanhMuc();
            LoadDonVi();
            LoadChiTietToi();
            LoadTieuDe();
            LoadDanhMuc();
            LoadBQuanLy();
            LoadBudgetIndexes();
            IsCalculating = false;
        }

        private void LoadBQuanLy()
        {
            _cbxBQuanLy = new ObservableCollection<ComboboxItem>();
            System.Linq.Expressions.Expression<Func<DmBQuanLy, bool>> predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DmBQuanLy> data = _iNsPhongBanService.FindByCondition(predicate).ToList();
            _cbxBQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            _cbxBQuanLy.Insert(0, new ComboboxItem { DisplayItem = "Tất cả", ValueItem = "0" });
            _cbxBQuanLySelected = _cbxBQuanLy.FirstOrDefault();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void LoadDataDot()
        {
            _dataDot = new List<ComboboxItem>();
            _dataDot.Add(new ComboboxItem("Lần 1", "1"));
            _dataDot.Add(new ComboboxItem("Lần 2", "2"));
            DataDotSelected = DataDot.First();
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh.Count == 0)
                _units.Add(new ComboboxItem("Đồng", "1"));
            foreach (DanhMuc dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            OnPropertyChanged(nameof(Units));
            _selectedUnit = Units.ElementAt(0);

            DanhMuc danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            DanhMuc danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadChiTietToi()
        {
            List<DanhMuc> danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                DanhMuc danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
            }
        }

        private void LoadNganhByDonVi()
        {
            List<DonVi> listNsDonVi = new List<DonVi>();
            listNsDonVi = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
            listNsDonVi = listNsDonVi.Where(x => x.BCoNSNganh && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT)).ToList();
            AuthenticationInfo authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionInfo);
            _listDanhMucNganh = _danhMucNganhService.FindAll(authenticationInfo).Where(x => listNsDonVi.Select(y => y.IIDMaDonVi).Contains(x.SGiaTri)).ToList();
        }

        private void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();

            List<DonVi> listDonVi = _donViService.FindForAdjustmentEstimateReport(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                _sessionInfo.Budget, int.Parse(DataDotSelected.ValueItem)).OrderBy(x => x.IIDMaDonVi).ToList();

            foreach (DonVi item in listDonVi)
            {
                if ((ListDonVi.Count() == 0 || ListDonVi.Where(n => n.ValueItem == item.IIDMaDonVi).Count() == 0) &&
                    !string.IsNullOrEmpty(item.IIDMaDonVi)
                    && !string.IsNullOrEmpty(item.TenDonVi))
                    ListDonVi.Add(new CheckBoxItem { ValueItem = item.IIDMaDonVi, DisplayItem = item.TenDonVi });
            }

            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
            foreach (CheckBoxItem model in ListDonVi)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        if (!IsCalculating) LoadBudgetIndexes();
                        OnPropertyChanged(nameof(SelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }

            OnPropertyChanged(nameof(ListDonVi));
            OnPropertyChanged(nameof(SelectedCountDonVi));
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchDonVi.Trim()!.ToLower());
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void OnExportFile(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                List<ExportResult> results = new List<ExportResult>();
                var predicate = PredicateBuilder.True<NsDcChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.INamNganSach == _sessionInfo.YearOfBudget
                                                && x.ILoaiDuKien == int.Parse(DataDotSelected.ValueItem)
                                                && x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => ListDonVi.Where(n => n.IsChecked).Select(z => z.ValueItem).Contains(x.IIdMaDonVi));
                var allchungtu = _chungTuService.FindByCondition(predicate);

                foreach (CheckBoxItem itemDonVi in ListDonVi.Where(n => n.IsChecked))
                {

                    //NsDcChungTu chungTu = _chungTuService.FindByCondition(predicate).FirstOrDefault()
                    var chungtus = allchungtu.Where(x => itemDonVi.ValueItem.Equals(x.IIdMaDonVi));
                    if (chungtus.IsEmpty()) return;

                    //List<string> listLNSChungTu = chungTu.SDslns.Split(",").ToList();

                    List<string> listLNSChungTu = new List<string>();
                    foreach (var ct in chungtus)
                    {
                        listLNSChungTu.AddRange(ct.SDslns.Split(",").ToList());
                    }

                    IEnumerable<string> listLNS = _mucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork
                                                && listLNSChungTu.Contains(x.Lns)
                                                && (CbxBQuanLySelected.ValueItem == "0" || CbxBQuanLySelected.ValueItem == x.IdPhongBan)).Select(x => x.Lns).Distinct();

                    EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
                    {
                        VoucherIds = string.Join(",", chungtus.Select(x => x.Id)),
                        //LNS = chungTu.SDslns,
                        LNS = string.Join(",", listLNS),
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget,
                        IdDonVi = itemDonVi.ValueItem,
                        LoaiDuKien = int.Parse(DataDotSelected.ValueItem),
                        //LoaiDuKien = chungTu.ILoaiDuKien,
                        //LoaiChungTu = chungTu.ILoaiChungTu,
                        //VoucherDate = chungTu.DNgayChungTu,
                        UserName = _sessionInfo.Principal
                    };

                    var lns = _budgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToList();
                    List<NsDcChungTuChiTietQuery> data = _chungTuChiTietService.FindAllNSDCChungTuByCondition(searchCondition).Where(x => string.IsNullOrEmpty(x.SL) || lns.Contains(x.SLns)).ToList();
                    data.ForAll(x =>
                    {
                        x.FDuToanNganSachNam = x.FDuToanNganSachNam / dvt;
                        x.FDuToanChuyenNamSau = x.FDuToanChuyenNamSau / dvt;
                        x.FDuKienQtDauNam = x.FDuKienQtDauNam / dvt;
                        x.FDuKienQtCuoiNam = x.FDuKienQtCuoiNam / dvt;
                    });
                    if (_listDanhMucNganh != null && _listDanhMucNganh.Count > 0)
                    {
                        List<string> listXauNoiMa = StringUtils.GetListXauNoiMaParent(data.Where(x => !x.BHangCha && _listDanhMucNganh.Select(x => x.IIDMaDanhMuc).Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                        data = data.Where(x => listXauNoiMa.Contains(x.SXauNoiMa)).ToList();
                    }
                    if (InTangGiam || InChuyenNamSau)
                    {
                        _listChungTuChiTiet = _mapper.Map<List<DcChungTuChiTietModel>>(data).Where(x => FillterDataCheckbox(x)).ToList();
                        //_listChungTuChiTiet = _mapper.Map<List<DcChungTuChiTietModel>>(data).Where(x => x.IsHangCha || !x.IsHangCha && (x.FTang > 0 || x.FGiam > 0)).ToList();

                    }
                    else
                    {
                        _listChungTuChiTiet = _mapper.Map<List<DcChungTuChiTietModel>>(data);
                    }
                    CalculateData();
                    _listChungTuChiTiet = _listChungTuChiTiet.Where(x => HasInputData(x)).ToList();
                    switch (SelectedInToiMuc.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                            _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                            _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                            _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                            _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }
                    for (int i = 01; i < _listChungTuChiTiet.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(_listChungTuChiTiet[i].STm))
                        {
                            _listChungTuChiTiet[i].SL = String.Empty;
                            continue;
                        }
                        if (!string.IsNullOrEmpty(_listChungTuChiTiet[i].SM))
                        {
                            if (string.IsNullOrEmpty(_listChungTuChiTiet[i - 1].SL) && string.IsNullOrEmpty(_listChungTuChiTiet[i - 1].SK))
                                _listChungTuChiTiet[i].SL = _listChungTuChiTiet[i].SL + "-" + _listChungTuChiTiet[i].SK;
                            else _listChungTuChiTiet[i].SL = String.Empty;
                        }
                    }
                    _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.SXauNoiMa) && x.SXauNoiMa.Length.Equals(NSConstants.MLNS_LENGTH_1)).ForAll(s => s.IIdMlnsCha = Guid.Empty);
                    string header1 = "Đơn vị tính: " + SelectedUnit.DisplayItem;
                    RptDuToanDieuChinh report = new RptDuToanDieuChinh();
                    report.Cap1 = GetLevelTitle(_dmChuKy, 1);
                    report.Cap2 = GetLevelTitle(_dmChuKy, 2);
                    report.TenDonVi = "Đơn vị: " + itemDonVi.DisplayItem;
                    report.TieuDe1 = Title1;
                    report.TieuDe2 = Title2;
                    report.TieuDe3 = Title3;
                    report.Header1 = header1;
                    report.TongCongBangChu = StringUtils.NumberToText(_detailTotal.FTongTongCong * dvt);
                    report.Items = _listChungTuChiTiet;
                    report.TongDuToanNganSachNam = _detailTotal.FTongDuToanNganSachNam;
                    report.TongDuToanChuyenNamSau = _detailTotal.FTongDuToanChuyenNamSau;
                    report.TongDuToanConLai = _detailTotal.FTongDuToanConLai;
                    report.TongDuKienQtDauNam = _detailTotal.FTongDuKienQuyetToanDauNam;
                    report.TongDuKienQtCuoiNam = _detailTotal.FTongDuKienQuyetToanCuoiNam;
                    report.TongTongCong = _detailTotal.FTongTongCong;
                    report.TongTang = _detailTotal.FTongTang;
                    report.TongGiam = _detailTotal.FTongGiam;
                    report.NamLamViec = _sessionInfo.YearOfWork;
                    report.DiaDiem = _diaDiem;
                    report.Ngay = DateUtils.FormatDateReport(ReportDate);
                    report.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                    report.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                    report.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                    report.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                    report.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                    report.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                    report.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                    report.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                    report.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;
                    if (DataDotSelected != null && int.Parse(DataDotSelected.ValueItem) == (int)EstimateSettlementType.SIX_MONTH)
                    {
                        report.QtDauNam = 6;
                        report.QtCuoiNam = 6;
                    }
                    else
                    {
                        report.QtDauNam = 9;
                        report.QtCuoiNam = 3;
                    }

                    Dictionary<string, object> reportData = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                    reportData.Add("FormatNumber", formatNumber);
                    foreach (System.Reflection.PropertyInfo prop in report.GetType().GetProperties())
                    {
                        reportData.Add(prop.Name, prop.GetValue(report));
                    }

                    List<GhiChu> ghiChu = GetGhiChu();
                    reportData.Add("HasGhiChu", ghiChu.Any());
                    reportData.Add("ListGhiChu", ghiChu);
                    reportData.Add("Count", 100000);
                    string chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DcDuToanColumn>.HideColumn(chiTietToi).Select(x => x + 1).ToList();

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_DIEUCHINH);
                    string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU.Split(".").First(); //+ "_" + chungTu.SSoChungTu;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<DcChungTuChiTietModel, GhiChu>(templateFileName, reportData, hideColumns);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private bool FillterDataCheckbox(DcChungTuChiTietModel item)
        {
            if (InChuyenNamSau && InTangGiam)
            {
                return item.IsHangCha || !item.IsHangCha && (item.FTang > 0 || item.FGiam > 0 || item.FDuToanChuyenNamSau > 0);

            }
            else if (InTangGiam)
            {
                return item.IsHangCha || !item.IsHangCha && (item.FTang > 0 || item.FGiam > 0);
            }
            else
            {
                return item.IsHangCha || !item.IsHangCha && item.FDuToanChuyenNamSau > 0;
            }
        }
        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            string loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            Dictionary<string, DanhMuc> danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

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

        private void CalculateData()
        {
            if (CongTangGiam)
            {
                _listChungTuChiTiet.Where(x => x.IsHangCha)
                    .Select(x =>
                    {
                        x.FDuToanNganSachNam = 0;
                        x.FDuToanChuyenNamSau = 0;
                        x.FDuKienQtDauNam = 0;
                        x.FDuKienQtCuoiNam = 0;
                        x.FTang = 0;
                        x.FGiam = 0;
                        x.CongTangGiam = true;
                        return x;
                    }).ToList();
            }
            else
            {
                _listChungTuChiTiet.Where(x => x.IsHangCha)
                    .Select(x =>
                    {
                        x.FDuToanNganSachNam = 0;
                        x.FDuToanChuyenNamSau = 0;
                        x.FDuKienQtDauNam = 0;
                        x.FDuKienQtCuoiNam = 0;
                        return x;
                    }).ToList();
            }

            //_listChungTuChiTiet.Where(x => !x.IsHangCha)
            //    .ForAll(x =>
            //    {
            //        x.FTang = x.FTongCong > x.FDuToanConLai ? x.FTongCong - x.FDuToanConLai : 0;
            //        x.FGiam = x.FDuToanConLai > x.FTongCong ? x.FDuToanConLai - x.FTongCong : 0;
            //    });

            foreach (DcChungTuChiTietModel item in _listChungTuChiTiet.Where(x => x.IsEditable && (x.FDuToanChuyenNamSau != 0 || x.FDuToanNganSachNam != 0 || x.FDuKienQtDauNam != 0 || x.FDuKienQtCuoiNam != 0)))
            {
                CalculateParent(item, item);
            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            _detailTotal = new AdjustedEstimateDetailTotalModel();
            if (!CongTangGiam)
            {
                _listChungTuChiTiet.Where(x => x.IsEditable && HasInputData(x))
                    .ForAll(item =>
                    {
                        _detailTotal.FTongDuToanNganSachNam += item.FDuToanNganSachNam;
                        _detailTotal.FTongDuToanChuyenNamSau += item.FDuToanChuyenNamSau;
                        _detailTotal.FTongDuToanConLai += item.FDuToanConLai;
                        _detailTotal.FTongDuKienQuyetToanDauNam += item.FDuKienQtDauNam;
                        _detailTotal.FTongDuKienQuyetToanCuoiNam += item.FDuKienQtCuoiNam;
                        _detailTotal.FTongTongCong += item.FTongCong;
                    });
                _detailTotal.FTongGiam = _detailTotal.FTongDuToanConLai > _detailTotal.FTongTongCong ? _detailTotal.FTongDuToanConLai - _detailTotal.FTongTongCong : 0;
                _detailTotal.FTongTang = _detailTotal.FTongDuToanConLai < _detailTotal.FTongTongCong ? _detailTotal.FTongTongCong - _detailTotal.FTongDuToanConLai : 0;
            }
            else
            {
                _listChungTuChiTiet.Where(x => x.IsEditable && HasInputData(x))
                    .ForAll(item =>
                    {
                        _detailTotal.FTongDuToanNganSachNam += item.FDuToanNganSachNam;
                        _detailTotal.FTongDuToanChuyenNamSau += item.FDuToanChuyenNamSau;
                        _detailTotal.FTongDuToanConLai += item.FDuToanConLai;
                        _detailTotal.FTongDuKienQuyetToanDauNam += item.FDuKienQtDauNam;
                        _detailTotal.FTongDuKienQuyetToanCuoiNam += item.FDuKienQtCuoiNam;
                        _detailTotal.FTongTongCong += item.FTongCong;
                        _detailTotal.FTongTang += item.FTang;
                        _detailTotal.FTongGiam += item.FGiam;
                    });
            }
        }

        private void CalculateParent(DcChungTuChiTietModel currentItem, DcChungTuChiTietModel seftItem)
        {
            DcChungTuChiTietModel parrentItem = _listChungTuChiTiet.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FDuToanNganSachNam += seftItem.FDuToanNganSachNam;
            parrentItem.FDuToanChuyenNamSau += seftItem.FDuToanChuyenNamSau;
            parrentItem.FDuKienQtDauNam += seftItem.FDuKienQtDauNam;
            parrentItem.FDuKienQtCuoiNam += seftItem.FDuKienQtCuoiNam;
            if (CongTangGiam)
            {
                parrentItem.FTang += seftItem.FTang;
                parrentItem.FGiam += seftItem.FGiam;
            }
            //parrentItem.FTang += seftItem.FTang;
            //parrentItem.FGiam += seftItem.FGiam;
            CalculateParent(parrentItem, seftItem);
        }

        private bool HasInputData(DcChungTuChiTietModel item)
        {
            bool hasDataDuKienDauNam = item.FDuKienQtDauNam != 0;
            bool hasDataDuKienCuoiNam = item.FDuKienQtCuoiNam != 0;
            bool hasDataDuToanNganSachNam = item.FDuToanNganSachNam != 0;
            bool hasDataFDuToanChuyenNamSau = item.FDuToanChuyenNamSau != 0;
            return hasDataDuKienDauNam || hasDataDuKienCuoiNam || hasDataDuToanNganSachNam || hasDataFDuToanChuyenNamSau;
        }

        private void OnNoteCommand()
        {
            NsBaoCaoGhiChuDialogViewModel.SMaBaoCao = TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI;
            NsBaoCaoGhiChuDialogViewModel.Init();
            NsBaoCaoGhiChuDialogViewModel.BQuanLySelected = NsBaoCaoGhiChuDialogViewModel.BQuanLyItems.FirstOrDefault(x => x.ValueItem == CbxBQuanLySelected.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.ShowDialogHost("UnitAdjustedEstimateReport");
        }

        private class GhiChu
        {
            public string Content { get; set; }
            public string SGhiChu => Content;
        }

        private string GetMaGhiChu()
        {
            string data = JsonConvert.SerializeObject(new
            {
                BQuanLy = CbxBQuanLySelected.DisplayItem
            });
            return CompressExtension.CompressToBase64(data);
        }

        private List<GhiChu> GetGhiChu()
        {
            string typeChuKy = TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI;
            int iNamLamViec = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<NsCauHinhBaoCao, bool>> predicate = PredicateBuilder.True<NsCauHinhBaoCao>();
            predicate = predicate.And(x => x.INamLamViec.Equals(iNamLamViec));
            predicate = predicate.And(x => x.SMaBaoCao == typeChuKy);
            predicate = predicate.And(x => x.SMaGhiChu == GetMaGhiChu());
            List<NsCauHinhBaoCao> data = _ghiChuService.FindByCondition(predicate).ToList();
            if (!string.IsNullOrEmpty(data.FirstOrDefault()?.SGhiChu))
            {
                return data.FirstOrDefault()?.SGhiChu.Split(Environment.NewLine).Select(x => new GhiChu()
                {
                    Content = x
                }).ToList();
            }
            else
            {
                return new List<GhiChu>();
            }
        }
    }
}
