using AutoMapper;
using ControlzEx.Standard;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
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
    public class ReportDieuChinhDuToanTongHop : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _listDonViView;
        private readonly INsDcChungTuService _chungTuService;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly INsDonViService _donViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IExportService _exportService;
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly DanhMucNganhService _danhMucNganhService;
        private readonly DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private ICollectionView _listBudgetIndex;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private readonly List<NsDcChungTuQuery> _listChungTu;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private List<DanhMuc> _listDanhMucNganh;
        private List<DcChungTuChiTietModel> _listChungTuChiTiet;
        private AdjustedEstimateDetailTotalModel _detailTotal;
        private Dictionary<string, DcChungTuChiTietModel> _itemMap;
        public NsBaoCaoGhiChuDialogViewModel NsBaoCaoGhiChuDialogViewModel { get; }
        public override string Name => "Báo cáo điều chỉnh dự toán - Tổng hợp";
        public override string Title => "Báo cáo điều chỉnh dự toán - Tổng hợp";
        public override string Description => "Báo cáo điều chỉnh dự toán - Tổng hợp";
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


        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set => SetProperty(ref _paperPrintTypeSelected, value);
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

        public virtual void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"},
            };

            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            PaperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }

        public string GetPath(string input)
        {
            if (PaperPrintTypeSelected.ValueItem == "2")
                input = input + "_Ngang";
            return Path.Combine(ExportPrefix.PATH_TL_DT, input + FileExtensionFormats.Xlsx);
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

        private string _title1;
        public string Title1
        {
            get => _title1;
            set => SetProperty(ref _title1, value);
        }

        public bool IsShowBQuanLy => false;

        private bool _inMotToChecked;
        public bool InMotToChecked
        {
            get => _inMotToChecked;
            set => SetProperty(ref _inMotToChecked, value);
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

        public ReportDieuChinhDuToanTongHop(
            IMapper mapper,
            ILog logger,
            INsDcChungTuService chungTuService,
            INsDcChungTuChiTietService chungTuChiTietService,
            INsDonViService donViService,
            ISessionService sessionService,
            IDanhMucService danhMucSerivce,
            IDmChuKyService dmChuKyService,
            INsMucLucNganSachService mucLucNganSachService,
            IExportService exportService,
            DanhMucNganhService danhMucNganhService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            NsBaoCaoGhiChuDialogViewModel nsBaoCaoGhiChuDialogViewModel,
            INsBaoCaoGhiChuService ghiChuService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _mucLucNganSachService = mucLucNganSachService;
            _donViService = donViService;
            _danhMucService = danhMucSerivce;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _danhMucNganhService = danhMucNganhService;
            _ghiChuService = ghiChuService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            NsBaoCaoGhiChuDialogViewModel = nsBaoCaoGhiChuDialogViewModel;

            ExportExcelCommand = new RelayCommand(obj => OnExportFileExcel(ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            PrintCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }

        public override void Init()
        {
            base.Init();
            IsCalculating = true;
            InMotToChecked = false;
            _sessionInfo = _sessionService.Current;
            LoadPaperPrintTypes();
            InitReportDefaultDate();
            LoadDataDot();
            LoadDanhMuc();
            LoadDonVi();
            LoadChiTietToi();
            LoadTieuDe();
            LoadDanhMuc();
            LoadBudgetIndexes();
            IsCalculating = false;
        }

        private void OnExportFileExcel(ExportType exportType)
        {
            if (InMotToChecked)
            {
                OnExportFileExcelOnePage(exportType);
            }
            else
            {
                OnExportFile(exportType);
            }
        }
        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public static IEnumerable<List<T>> SplitList2<T>(List<T> bigList, int nSize = 4)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                if (i == 0)
                {
                    yield return bigList.GetRange(0, Math.Min(nSize - 1, bigList.Count - i));
                    i--;
                } else
                {
                    yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
                }
            }
        }

        private void GetDataExport2(List<NsDcChungTu> allchungtu, List<CheckBoxItem> ListDonVi, int dvt)
        {
            List<NsDcChungTuChiTietQuery> listData = new List<NsDcChungTuChiTietQuery>();
            var idChungtus = string.Join(",", allchungtu.Select(x => x.Id));
            var lstDonvis = string.Join(",", ListDonVi.Select(x => x.ValueItem));
            List<string> slns = new List<string>();
            allchungtu.ForEach(x =>
            {
                slns.AddRange(x.SDslns.Split(","));
            });
            var sLnss = new HashSet<string>(slns);
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherIds = idChungtus,
                LNS = string.Join(",", sLnss.Select(x => x)),
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                IdDonVi = lstDonvis,
                LoaiDuKien = int.Parse(DataDotSelected.ValueItem),
                //LoaiChungTu = chungTu.ILoaiChungTu,
                //VoucherDate = chungTu.DNgayChungTu,
                UserName = _sessionInfo.Principal
            };

            var lns = _budgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToList();
            List<NsDcChungTuChiTietQuery> data = _chungTuChiTietService.FindAllNSDCChungTuByCondition(searchCondition).Where(x => string.IsNullOrEmpty(x.SL) || lns.Contains(x.SLns)).ToList();
            data.ForAll(x =>
            {
                //x.IIdMaDonVi = itemDonVi.ValueItem;
                x.FDuToanNganSachNam /= dvt;
                x.FDuToanChuyenNamSau /= dvt;
                x.FDuKienQtDauNam /= dvt;
                x.FDuKienQtCuoiNam /= dvt;
            });
            listData.AddRange(data);
            //foreach (CheckBoxItem itemDonVi in ListDonVi)
            //{
            //    var chungtus = allchungtu.Where(x => x.IIdMaDonVi.Equals(itemDonVi.ValueItem));
            //    if (chungtus.IsEmpty()) return;

            //    List<string> listLNSChungTu = new List<string>();
            //    foreach (var ct in chungtus)
            //    {
            //        listLNSChungTu.AddRange(ct.SDslns.Split(",").ToList());
            //    }

            //    EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            //    {
            //        VoucherIds = string.Join(",", chungtus.Select(x => x.Id)),
            //        LNS = string.Join(",", listLNSChungTu),
            //        YearOfWork = _sessionInfo.YearOfWork,
            //        YearOfBudget = _sessionInfo.YearOfBudget,
            //        BudgetSource = _sessionInfo.Budget,
            //        IdDonVi = itemDonVi.ValueItem,
            //        LoaiDuKien = int.Parse(DataDotSelected.ValueItem),
            //        //LoaiChungTu = chungTu.ILoaiChungTu,
            //        //VoucherDate = chungTu.DNgayChungTu,
            //        UserName = _sessionInfo.Principal
            //    };

            //    List<NsDcChungTuChiTietQuery> data = _chungTuChiTietService.FindAllNSDCChungTuByCondition(searchCondition).ToList();
            //    data.ForAll(x =>
            //    {
            //        x.IIdMaDonVi = itemDonVi.ValueItem;
            //        x.FDuToanNganSachNam /= dvt;
            //        x.FDuToanChuyenNamSau /= dvt;
            //        x.FDuKienQtDauNam /= dvt;
            //        x.FDuKienQtCuoiNam /= dvt;
            //    });
            //    listData.AddRange(data);
            //}

            if (_listDanhMucNganh != null && _listDanhMucNganh.Count > 0)
            {
                List<string> listXauNoiMa = StringUtils.GetListXauNoiMaParent(listData.Where(x => !x.BHangCha && _listDanhMucNganh.Select(x => x.IIDMaDanhMuc).Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                listData = listData.Where(x => listXauNoiMa.Contains(x.SXauNoiMa)).ToList();
            }
            _listChungTuChiTiet = _mapper.Map<List<DcChungTuChiTietModel>>(listData);
            _itemMap = _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && !x.IIdMlns.IsNullOrEmpty()).ToDictionary(key => $"{key.IIdMaDonVi}-{key.IIdMlns}", value => value);
        }
        private void GetDataExport(List<NsDcChungTu> allchungtu, List<CheckBoxItem> ListDonVi, int dvt)
        {
            List<NsDcChungTuChiTietQuery> listData = new List<NsDcChungTuChiTietQuery>();

            foreach (CheckBoxItem itemDonVi in ListDonVi)
            {
                var chungtus = allchungtu.Where(x => x.IIdMaDonVi.Equals(itemDonVi.ValueItem));
                if (chungtus.IsEmpty()) return;

                List<string> listLNSChungTu = new List<string>();
                foreach (var ct in chungtus)
                {
                    listLNSChungTu.AddRange(ct.SDslns.Split(",").ToList());
                }

                EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
                {
                    VoucherIds = string.Join(",", chungtus.Select(x => x.Id)),
                    LNS = string.Join(",", listLNSChungTu),
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    IdDonVi = itemDonVi.ValueItem,
                    LoaiDuKien = int.Parse(DataDotSelected.ValueItem),
                    //LoaiChungTu = chungTu.ILoaiChungTu,
                    //VoucherDate = chungTu.DNgayChungTu,
                    UserName = _sessionInfo.Principal
                };

                List<NsDcChungTuChiTietQuery> data = _chungTuChiTietService.FindAllNSDCChungTuByCondition(searchCondition).ToList();
                data.ForAll(x =>
                {
                    x.IIdMaDonVi = itemDonVi.ValueItem;
                    x.FDuToanNganSachNam /= dvt;
                    x.FDuToanChuyenNamSau /= dvt;
                    x.FDuKienQtDauNam /= dvt;
                    x.FDuKienQtCuoiNam /= dvt;
                });
                listData.AddRange(data);
            }

            if (_listDanhMucNganh != null && _listDanhMucNganh.Count > 0)
            {
                List<string> listXauNoiMa = StringUtils.GetListXauNoiMaParent(listData.Where(x => !x.BHangCha && _listDanhMucNganh.Select(x => x.IIDMaDanhMuc).Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                listData = listData.Where(x => listXauNoiMa.Contains(x.SXauNoiMa)).ToList();
            }
            _listChungTuChiTiet = _mapper.Map<List<DcChungTuChiTietModel>>(listData);
            _itemMap = _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && !x.IIdMlns.IsNullOrEmpty()).ToDictionary(key => $"{key.IIdMaDonVi}-{key.IIdMlns}", value => value);
        }

        private void OnExportFileExcelOnePage(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> exportResults = new List<ExportResult>();
                int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                List<CheckBoxItem> lstIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();
                int size = lstIdDonVi.Count();
                List<List<CheckBoxItem>> listDonViSplits = SplitList(lstIdDonVi, size).ToList();
                System.Linq.Expressions.Expression<Func<NsDcChungTu, bool>> predicate = PredicateBuilder.True<NsDcChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.INamNganSach == _sessionInfo.YearOfBudget
                                                && x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => lstIdDonVi.Select(x => x.ValueItem).Contains(x.IIdMaDonVi));
                predicate = predicate.And(x => x.ILoaiDuKien.ToString().Equals(DataDotSelected.ValueItem));
                var allchungtu = _chungTuService.FindByCondition(predicate).ToList();

                GetDataExport2(allchungtu, lstIdDonVi, dvt);
                //CalculateData();

                switch (SelectedInToiMuc.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                        break;
                }

                List<Guid> listIdMucLuc = _listChungTuChiTiet.Select(x => x.IIdMlns).Distinct().ToList();
                HandlerDataExportOnePege(listDonViSplits, listIdMucLuc, size, dvt, exportType, exportResults);
                e.Result = exportResults;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    List<ExportResult> result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        //if (InMotToChecked)
                        //    exportType = ExportType.PDF_ONE_PAPER;
                        if (InMotToChecked)
                        {
                            if (exportType == ExportType.EXCEL)
                            {
                                exportType = ExportType.EXCEL_ONE_PAPER;
                            }
                            else
                            {
                                exportType = ExportType.PDF_ONE_PAPER;
                            }
                        }
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
        private void OnExportFile(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> exportResults = new List<ExportResult>();
                int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                int size = PaperPrintTypeSelected.ValueItem == "2" ? 8 : 4;
                List<CheckBoxItem> lstIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();

                List<List<CheckBoxItem>> listDonViSplits = SplitList2(lstIdDonVi, size).ToList();
                System.Linq.Expressions.Expression<Func<NsDcChungTu, bool>> predicate = PredicateBuilder.True<NsDcChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.INamNganSach == _sessionInfo.YearOfBudget
                                                && x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => lstIdDonVi.Select(x => x.ValueItem).Contains(x.IIdMaDonVi));
                predicate = predicate.And(x => x.ILoaiDuKien.ToString().Equals(DataDotSelected.ValueItem));
                var allchungtu = _chungTuService.FindByCondition(predicate).ToList();

                GetDataExport2(allchungtu, lstIdDonVi, dvt);
                //CalculateData();

                switch (SelectedInToiMuc.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        _listChungTuChiTiet = _listChungTuChiTiet.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                        break;
                }

                List<Guid> listIdMucLuc = _listChungTuChiTiet.Select(x => x.IIdMlns).Distinct().ToList();
                HandlerDataExport2(listDonViSplits, listIdMucLuc, size, dvt, exportType, exportResults);
                e.Result = exportResults;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    List<ExportResult> result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        if (InMotToChecked)
                            exportType = ExportType.PDF_ONE_PAPER;
                        //if (InMotToChecked)
                        //    {
                        //    if (exportType == ExportType.EXCEL)
                        //    {
                        //        exportType = ExportType.EXCEL_ONE_PAPER;
                        //    }
                        //    else
                        //    {
                        //        exportType = ExportType.PDF_ONE_PAPER;
                        //    }
                        //}
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

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

        private void HandlerDataExportOnePege(List<List<CheckBoxItem>> listDonViSplits, List<Guid> listIdMucLuc, int size, int dvt, ExportType exportType, List<ExportResult> exportResults)
        {
            List<CheckBoxItem> lstIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();
            List<HeaderReportDynamic> ListHeaderPage1 = new List<HeaderReportDynamic>();
            int columnStartPage1 = 16;
            int columnEndPage1 = columnStartPage1 + (lstIdDonVi.Count > 1 ? lstIdDonVi.Count - 1 : 0);
            var ColNameStartPage1 = GetExcelColumnName(columnStartPage1);
            var ColNameEndPage1 = GetExcelColumnName(columnEndPage1);
            var mergeRangePage1 = string.Format("{0}6:{1}6", ColNameStartPage1, ColNameEndPage1);
            foreach (var item in lstIdDonVi.Select((value, index) => new { index, value }))
            {
                if (item.index == NSConstants.ZERO)
                {
                    ListHeaderPage1.Add(new HeaderReportDynamic() { Header = "Trong đó", Stt = 1, MergeRange = mergeRangePage1 });
                }
                else
                {
                    ListHeaderPage1.Add(new HeaderReportDynamic());
                }
            }

            int donViTinh = 1;
            foreach (var donviPage in listDonViSplits)
            {
                List<ReportDieuChinhDuToanQuery> results = new List<ReportDieuChinhDuToanQuery>();
                List<HeaderReportDieuChinhDuToanTongHop> headers = new List<HeaderReportDieuChinhDuToanTongHop>();
                var itemsClone = _listChungTuChiTiet.Clone();
                var itemsPage = itemsClone.Where(x => x.IsHangCha || (!x.IsHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi) && donviPage.Select(s => s.ValueItem).Contains(x.IIdMaDonVi))).ToList();
                if (donviPage.Count < size)
                {
                    int countEmpty = size - donviPage.Count;
                    for (int j = 0; j < countEmpty; j++)
                    {
                        CheckBoxItem emptyCb = new CheckBoxItem();
                        donviPage.Add(emptyCb);
                    }
                }
                foreach (CheckBoxItem tenDv in donviPage)
                {
                    HeaderReportDieuChinhDuToanTongHop hd = new HeaderReportDieuChinhDuToanTongHop();
                    hd.TenDonVi = tenDv.DisplayItem;
                    headers.Add(hd);
                }
                var itemsResult = itemsPage.GroupBy(x => x.IIdMlns).Select(s => new ReportDieuChinhDuToanQuery()
                {
                    IIdMlns = s.Key,
                    IIdMlnsCha = s.FirstOrDefault()?.IIdMlnsCha ?? Guid.Empty,
                    SXauNoiMa = s.FirstOrDefault()?.SXauNoiMa ?? string.Empty,
                    M = s.FirstOrDefault()?.SM ?? string.Empty,
                    Lns = s.FirstOrDefault()?.SLns ?? string.Empty,
                    L = s.FirstOrDefault()?.SL ?? string.Empty,
                    K = s.FirstOrDefault()?.SK ?? string.Empty,
                    Tm = s.FirstOrDefault()?.STm ?? string.Empty,
                    Ttm = s.FirstOrDefault()?.STtm ?? string.Empty,
                    Ng = s.FirstOrDefault()?.SNg ?? string.Empty,
                    Tng = s.FirstOrDefault()?.STng ?? string.Empty,
                    Tng1 = s.FirstOrDefault()?.STng1 ?? string.Empty,
                    Tng2 = s.FirstOrDefault()?.STng2 ?? string.Empty,
                    Tng3 = s.FirstOrDefault()?.STng3 ?? string.Empty,
                    MoTa = s.FirstOrDefault()?.SMoTa ?? string.Empty,
                    bHangCha = SelectedInToiMuc.ValueItem == nameof(MLNSFiled.NG) && !string.IsNullOrEmpty(s.FirstOrDefault()?.SNg) ? false : (s.FirstOrDefault()?.IsHangCha ?? false),
                    LstGiaTri = ParseDataGroup(s.ToList(), donviPage)
                }).ToList();
                var itemChild = new List<ReportDieuChinhDuToanQuery>();
                if (!itemsResult.IsEmpty())
                {
                    itemChild = itemsResult.Where(x => !x.bHangCha).ToList();
                    foreach (var item in itemChild)
                    {
                        FilterData(item, results, itemsResult);
                    }
                }
                results = results.OrderBy(o => o.SXauNoiMa).ToList();
                List<ReportDieuChinhDuToanQuery> resultsTotal = new List<ReportDieuChinhDuToanQuery>();
                ReportDieuChinhDuToanQuery total = new ReportDieuChinhDuToanQuery();
                total.LstTong = new List<NsSktChungTuChiTiet>();
                foreach (CheckBoxItem dv in donviPage)
                {
                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                    if (!string.IsNullOrEmpty(dv.ValueItem))
                    {
                        giaTri.FCong = _listChungTuChiTiet.Where(x => x.IIdMlnsCha.IsNullOrEmpty() && x.IIdMaDonVi == dv.ValueItem).Sum(x => x.FTongCong - x.FDuToanConLai);
                        total.LstTong.Add(giaTri);
                    }
                    else
                    {
                        total.LstTong.Add(giaTri);
                    }
                }
                total.TongCong = total.LstTong.Sum(x => x.FCong);
                resultsTotal.Add(total);
                ReportDieuChinhDuToanQuery tongSoTien = resultsTotal.FirstOrDefault();
                string header1 = "Đơn vị tính: " + SelectedUnit.DisplayItem;
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "TieuDe1", Title1 },
                        { "ListData", results },
                        { "ListTotal", resultsTotal },
                        { "Headers", headers },
                        { "FormatNumber", formatNumber },
                        { "TieuDe2", Title2 },
                        { "TieuDe3", Title3 },
                        { "Ngay", DateUtils.FormatDateReport(ReportDate) },
                        { "DiaDiem", _diaDiem },
                        { "Count", 1000000 },
                        { "Header1", string.Format(header1, listDonViSplits.IndexOf(donviPage), listDonViSplits.Count) },
                        { "TongCongBangChu", tongSoTien != null ? StringUtils.NumberToText(tongSoTien.TongCong * dvt) : string.Empty }
                    };
                AddChuKy(data);

                List<GhiChu> ghiChu = GetGhiChu();
                data.Add("HasGhiChu", ghiChu.Any());
                data.Add("ListGhiChu", ghiChu);
                data.Add("ListHeader1", ListHeaderPage1);

                string chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<DcDuToanColumn>.HideColumn(chiTietToi).Select(x => x + 1).ToList();

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2_ONEPAGE);
                string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2_ONEPAGE.Split(".").First();
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportDieuChinhDuToanQuery, HeaderReportDieuChinhDuToanTongHop, GhiChu, HeaderReportDynamic>(templateFileName, data, hideColumns);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
            }
        }

        private void HandlerDataExport2(List<List<CheckBoxItem>> listDonViSplits, List<Guid> listIdMucLuc, int size, int dvt, ExportType exportType, List<ExportResult> exportResults)
        {
            int donViTinh = 1;
            foreach (var donviPage in listDonViSplits)
            {
                List<ReportDieuChinhDuToanQuery> results = new List<ReportDieuChinhDuToanQuery>();
                List<HeaderReportDieuChinhDuToanTongHop> headers = new List<HeaderReportDieuChinhDuToanTongHop>();
                var itemsClone = _listChungTuChiTiet.Clone();
                List<DcChungTuChiTietModel> itemsPage = new List<DcChungTuChiTietModel>();

                // Tờ 1 lấy hết dữ liệu (chỉ cần cột tổng cộng có dữ liệu)
                if (listDonViSplits.IndexOf(donviPage) == 0)
                {
                    itemsPage = itemsClone.Where(x => x.IsHangCha || (!x.IsHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi))).ToList();
                }
                else
                {
                    itemsPage = itemsClone.Where(x => x.IsHangCha || (!x.IsHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi) && donviPage.Select(s => s.ValueItem).Contains(x.IIdMaDonVi))).ToList();
                }

                if (donviPage.Count < size)
                {
                    int countEmpty = size - donviPage.Count;
                    for (int j = 0; j < countEmpty; j++)
                    {
                        CheckBoxItem emptyCb = new CheckBoxItem();
                        donviPage.Add(emptyCb);
                    }
                }
                foreach (CheckBoxItem tenDv in donviPage)
                {
                    HeaderReportDieuChinhDuToanTongHop hd = new HeaderReportDieuChinhDuToanTongHop();
                    hd.TenDonVi = tenDv.DisplayItem;
                    headers.Add(hd);
                }
                var itemsResult = itemsPage.GroupBy(x => x.IIdMlns).Select(s => new ReportDieuChinhDuToanQuery()
                {
                    IIdMlns = s.Key,
                    IIdMlnsCha = s.FirstOrDefault()?.IIdMlnsCha ?? Guid.Empty,
                    SXauNoiMa = s.FirstOrDefault()?.SXauNoiMa ?? string.Empty,
                    M = s.FirstOrDefault()?.SM ?? string.Empty,
                    Lns = s.FirstOrDefault()?.SLns ?? string.Empty,
                    L = s.FirstOrDefault()?.SL ?? string.Empty,
                    K = s.FirstOrDefault()?.SK ?? string.Empty,
                    Tm = s.FirstOrDefault()?.STm ?? string.Empty,
                    Ttm = s.FirstOrDefault()?.STtm ?? string.Empty,
                    Ng = s.FirstOrDefault()?.SNg ?? string.Empty,
                    Tng = s.FirstOrDefault()?.STng ?? string.Empty,
                    Tng1 = s.FirstOrDefault()?.STng1 ?? string.Empty,
                    Tng2 = s.FirstOrDefault()?.STng2 ?? string.Empty,
                    Tng3 = s.FirstOrDefault()?.STng3 ?? string.Empty,
                    MoTa = s.FirstOrDefault()?.SMoTa ?? string.Empty,
                    bHangCha = SelectedInToiMuc.ValueItem == nameof(MLNSFiled.NG) && !string.IsNullOrEmpty(s.FirstOrDefault()?.SNg) ? false : (s.FirstOrDefault()?.IsHangCha ?? false),
                    LstGiaTri = ParseDataGroup(s.ToList(), donviPage)
                }).ToList();
                var itemChild = new List<ReportDieuChinhDuToanQuery>();
                if (!itemsResult.IsEmpty())
                {
                    itemChild = itemsResult.Where(x => !x.bHangCha).ToList();
                    foreach (var item in itemChild)
                    {
                        FilterData(item, results, itemsResult, listDonViSplits.IndexOf(donviPage) == 0);
                    }
                }
                results = results.OrderBy(o => o.SXauNoiMa).ToList();
                List<ReportDieuChinhDuToanQuery> resultsTotal = new List<ReportDieuChinhDuToanQuery>();
                ReportDieuChinhDuToanQuery total = new ReportDieuChinhDuToanQuery();
                total.LstTong = new List<NsSktChungTuChiTiet>();
                foreach (CheckBoxItem dv in donviPage)
                {
                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                    if (!string.IsNullOrEmpty(dv.ValueItem))
                    {
                        giaTri.FCong = _listChungTuChiTiet.Where(x => x.IIdMlnsCha.IsNullOrEmpty() && x.IIdMaDonVi == dv.ValueItem).Sum(x => x.FTongCong - x.FDuToanConLai);
                        total.LstTong.Add(giaTri);
                    }
                    else
                    {
                        total.LstTong.Add(giaTri);
                    }
                }
                total.TongCong = total.LstTong.Sum(x => x.FCong);
                resultsTotal.Add(total);
                var tongSoTien = _listChungTuChiTiet.Where(x => !x.IsHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi)).Sum(x => x.FTongCong - x.FDuToanConLai);
                string header1 = "Đơn vị tính: " + SelectedUnit.DisplayItem;
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "TieuDe1", Title1 },
                        { "ListData", results },
                        { "ListTotal", resultsTotal },
                        { "Headers", headers },
                        { "FormatNumber", formatNumber },
                        { "TieuDe2", Title2 },
                        { "TieuDe3", Title3 },
                        { "Ngay", DateUtils.FormatDateReport(ReportDate) },
                        { "DiaDiem", _diaDiem },
                        { "Count", 1000000 },
                        { "Header1", string.Format(header1, listDonViSplits.IndexOf(donviPage), listDonViSplits.Count) },
                        { "TongCongBangChu", StringUtils.NumberToText(tongSoTien * dvt) }
                    };
                AddChuKy(data);

                List<GhiChu> ghiChu = GetGhiChu();
                data.Add("HasGhiChu", ghiChu.Any());
                data.Add("ListGhiChu", ghiChu);

                string chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<DcDuToanColumn>.HideColumn(chiTietToi).Select(x => x + 1).ToList();
                if (listDonViSplits.IndexOf(donviPage) == 0)
                {
                    if (PaperPrintTypeSelected.ValueItem == "2")
                    {
                        data.Add("MergeRange", "P6:V6");
                        hideColumns.Add(23);
                    } else
                    {
                        data.Add("MergeRange", "P6:R6");
                        hideColumns.Add(19);
                    }
                } else
                {
                    if (PaperPrintTypeSelected.ValueItem == "2")
                    {
                        data.Add("MergeRange", "P6:W6");
                        hideColumns.Add(15);
                    }
                    else
                    {
                        data.Add("MergeRange", "P6:S6");
                        hideColumns.Add(15);
                    }
                }
                string templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2.Replace(".xlsx", ""));
                string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2.Split(".").First();
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportDieuChinhDuToanQuery, HeaderReportDieuChinhDuToanTongHop, GhiChu>(templateFileName, data, hideColumns);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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
                                            && listLNSChungTu.Contains(x.Lns)).Select(x => x.Lns).Distinct();

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
                                            && string.IsNullOrEmpty(x.L)).Distinct().ToList();

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

        private void FilterData(ReportDieuChinhDuToanQuery item, List<ReportDieuChinhDuToanQuery> results, List<ReportDieuChinhDuToanQuery> items, bool isFirstPage = false)
        {
            bool hasKL = false;
            if (item.M == string.Empty)
            {
                if (item.K == string.Empty && item.L == string.Empty) hasKL = false;
                else hasKL = true;
            }
            else
            {
                if (hasKL) item.Lns = string.Empty;
                else item.Lns = item.L + "-" + item.K;
            }
            if (item.Tm != string.Empty) item.Lns = string.Empty;
            if (isFirstPage) item.TongCong = _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && x.IIdMlns == item.IIdMlns).Sum(x => x.FTongCong - x.FDuToanConLai);
            //item.TongCong = item.LstGiaTri.Sum(x => x.FCong);
            if (isFirstPage && item.TongCong != 0)
            {
                results.Add(item);
                AddParent(item, results, items);
            } else if (!item.LstGiaTri.IsEmpty() && item.LstGiaTri.Any(x => x.FCong != 0))
            {
                results.Add(item);
                AddParent(item, results, items);
            }
        }

        private void AddParent(ReportDieuChinhDuToanQuery item, List<ReportDieuChinhDuToanQuery> results, List<ReportDieuChinhDuToanQuery> items)
        {
            if (item.IIdMlnsCha.IsNullOrEmpty()) return;
            if (results.Any(x => item.IIdMlnsCha.Equals(x.IIdMlns))) return;
            ReportDieuChinhDuToanQuery parent = items.FirstOrDefault(x => x.IIdMlns.Equals(item.IIdMlnsCha));
            if (parent is null) return;
            results.Add(parent);
            AddParent(parent, results, items);
        }

        private List<NsSktChungTuChiTiet> ParseDataGroup(List<DcChungTuChiTietModel> items, List<CheckBoxItem> donvi)
        {
            List<NsSktChungTuChiTiet> outPut = new List<NsSktChungTuChiTiet>();
            if (items.IsEmpty() && donvi.IsEmpty()) return outPut;
            foreach (CheckBoxItem dv in donvi)
            {
                NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                if (!string.IsNullOrEmpty(dv.ValueItem))
                {
                    DcChungTuChiTietModel gtDonVi = items.FirstOrDefault(x => dv.ValueItem.Equals(x.IIdMaDonVi));
                    if (gtDonVi is null) giaTri.FCong = 0;
                    else giaTri.FCong = gtDonVi.FTongCong - gtDonVi.FDuToanConLai;
                    outPut.Add(giaTri);
                }
                else
                {
                    outPut.Add(giaTri);
                }
            }
            return outPut;
        }

        private void HandlerDataExport(List<List<CheckBoxItem>> listDonViSplits, List<Guid> listIdMucLuc, int size, int dvt, ExportType exportType, List<ExportResult> exportResults)
        {

            int donViTinh = 1;
            Dictionary<string, DcChungTuChiTietModel> dicItems = _listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && !x.IIdMlns.IsNullOrEmpty()).ToDictionary(key => $"{key.IIdMaDonVi}-{key.IIdMlns}", value => value);
            foreach (var donviPage in listDonViSplits)
            //for (int i = 0; i < listDonViSplits.Count; i++)
            {
                List<ReportDieuChinhDuToanQuery> results = new List<ReportDieuChinhDuToanQuery>();
                List<HeaderReportDieuChinhDuToanTongHop> headers = new List<HeaderReportDieuChinhDuToanTongHop>();

                if (donviPage.Count < size)
                {
                    int countEmpty = size - donviPage.Count;
                    for (int j = 0; j < countEmpty; j++)
                    {
                        CheckBoxItem emptyCb = new CheckBoxItem();
                        donviPage.Add(emptyCb);
                    }
                }
                foreach (CheckBoxItem tenDv in donviPage)
                {
                    HeaderReportDieuChinhDuToanTongHop hd = new HeaderReportDieuChinhDuToanTongHop();
                    hd.TenDonVi = tenDv.DisplayItem;
                    headers.Add(hd);
                }
                bool hasKL = false;
                foreach (Guid ml in listIdMucLuc)
                {
                    DcChungTuChiTietModel mucluc = _listChungTuChiTiet.FirstOrDefault(x => x.IIdMlns == ml);
                    ReportDieuChinhDuToanQuery kq = new ReportDieuChinhDuToanQuery
                    {
                        IIdMlns = mucluc?.IIdMlns ?? Guid.Empty,
                        IIdMlnsCha = mucluc?.IIdMlnsCha ?? Guid.Empty,
                        M = mucluc?.SM ?? string.Empty,
                        Lns = mucluc?.SLns ?? string.Empty,
                        L = mucluc?.SL ?? string.Empty,
                        K = mucluc?.SK ?? string.Empty,
                        Tm = mucluc?.STm ?? string.Empty,
                        Ttm = mucluc?.STtm ?? string.Empty,
                        Ng = mucluc?.SNg ?? string.Empty,
                        Tng = mucluc?.STng ?? string.Empty,
                        Tng1 = mucluc?.STng1 ?? string.Empty,
                        Tng2 = mucluc?.STng2 ?? string.Empty,
                        Tng3 = mucluc?.STng3 ?? string.Empty,
                        MoTa = mucluc?.SMoTa ?? string.Empty,
                        bHangCha = SelectedInToiMuc.ValueItem == nameof(MLNSFiled.NG) && !string.IsNullOrEmpty(mucluc?.SNg) ? false : (mucluc?.IsHangCha ?? false),
                        LstGiaTri = new List<NsSktChungTuChiTiet>()
                    };
                    foreach (CheckBoxItem dv in donviPage)
                    {
                        NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                        if (!string.IsNullOrEmpty(dv.ValueItem))
                        {
                            DcChungTuChiTietModel gtDonVi = dicItems.ContainsKey($"{dv.ValueItem}-{ml}") ? dicItems[$"{dv.ValueItem}-{ml}"] : null;
                            if (gtDonVi is null) giaTri.FCong = 0;
                            else giaTri.FCong = gtDonVi.FTongCong - gtDonVi.FDuToanConLai;
                            kq.LstGiaTri.Add(giaTri);
                        }
                        else
                        {
                            kq.LstGiaTri.Add(giaTri);
                        }
                    }
                    if (kq.M == string.Empty)
                    {
                        if (kq.K == string.Empty && kq.L == string.Empty) hasKL = false;
                        else hasKL = true;
                    }
                    else
                    {
                        if (hasKL) kq.Lns = string.Empty;
                        else kq.Lns = kq.L + "-" + kq.K;
                    }
                    if (kq.Tm != string.Empty) kq.Lns = string.Empty;
                    kq.TongCong = kq.LstGiaTri.Sum(x => x.FCong);
                    if (!kq.LstGiaTri.IsEmpty() && kq.LstGiaTri.Any(x => x.FCong != 0))
                        results.Add(kq);
                }

                List<ReportDieuChinhDuToanQuery> resultsTotal = new List<ReportDieuChinhDuToanQuery>();
                ReportDieuChinhDuToanQuery total = new ReportDieuChinhDuToanQuery();
                total.LstTong = new List<NsSktChungTuChiTiet>();
                foreach (CheckBoxItem dv in donviPage)
                {
                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                    if (!string.IsNullOrEmpty(dv.ValueItem))
                    {
                        giaTri.FCong = _listChungTuChiTiet.Where(x => x.IIdMlnsCha.IsNullOrEmpty() && x.IIdMaDonVi == dv.ValueItem).Sum(x => x.FTongCong - x.FDuToanConLai);
                        total.LstTong.Add(giaTri);
                    }
                    else
                    {
                        total.LstTong.Add(giaTri);
                    }
                }
                total.TongCong = total.LstTong.Sum(x => x.FCong);
                resultsTotal.Add(total);
                ReportDieuChinhDuToanQuery tongSoTien = resultsTotal.FirstOrDefault();
                string header1 = "Đơn vị tính: " + SelectedUnit.DisplayItem;
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "TieuDe1", Title1 },
                        { "ListData", results },
                        { "ListTotal", resultsTotal },
                        { "Headers", headers },
                        { "FormatNumber", formatNumber },
                        { "TieuDe2", Title2 },
                        { "TieuDe3", Title3 },
                        { "Ngay", DateUtils.FormatDateReport(ReportDate) },
                        { "DiaDiem", _diaDiem },
                        { "Count", 1000000 },
                        { "Header1", string.Format(header1, listDonViSplits.IndexOf(donviPage), listDonViSplits.Count) },
                        { "TongCongBangChu", tongSoTien != null ? StringUtils.NumberToText(tongSoTien.TongCong * dvt) : string.Empty }
                    };
                AddChuKy(data);

                List<GhiChu> ghiChu = GetGhiChu();
                data.Add("HasGhiChu", ghiChu.Any());
                data.Add("ListGhiChu", ghiChu);

                string chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<DcDuToanColumn>.HideColumn(chiTietToi).Select(x => x + 1).ToList();

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2);
                string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2.Split(".").First();
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportDieuChinhDuToanQuery, HeaderReportDieuChinhDuToanTongHop, GhiChu>(templateFileName, data, hideColumns);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
            }
        }

        private void CalculateData()
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

            var items = _listChungTuChiTiet.Where(x => x.IsEditable && (x.FDuToanNganSachNam != 0 || x.FDuToanChuyenNamSau != 0 || x.FDuKienQtDauNam != 0 || x.FDuKienQtCuoiNam != 0));

            foreach (DcChungTuChiTietModel item in items)
            {
                CalculateParent(item, item);
            }

            CalculateTotal();
        }
        private void CalculateTotal()
        {
            _detailTotal = new AdjustedEstimateDetailTotalModel();

            _listChungTuChiTiet.Where(x => x.IIdMlnsCha.IsNullOrEmpty())
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


        private void CalculateParent(DcChungTuChiTietModel currentItem, DcChungTuChiTietModel seftItem)
        {
            //DcChungTuChiTietModel parrentItem = _listChungTuChiTiet.FirstOrDefault(x => x.IIdMlns == currentItem.IIdMlnsCha && x.IIdMaDonVi == currentItem.IIdMaDonVi);
            DcChungTuChiTietModel parrentItem = _itemMap.ContainsKey($"{currentItem.IIdMaDonVi}-{currentItem.IIdMlnsCha}") ? _itemMap[$"{currentItem.IIdMaDonVi}-{currentItem.IIdMlnsCha}"] : null;
            if (parrentItem is null) return;
            parrentItem.FDuToanNganSachNam += seftItem.FDuToanNganSachNam;
            parrentItem.FDuToanChuyenNamSau += seftItem.FDuToanChuyenNamSau;
            parrentItem.FDuKienQtDauNam += seftItem.FDuKienQtDauNam;
            parrentItem.FDuKienQtCuoiNam += seftItem.FDuKienQtCuoiNam;
            //parrentItem.FTang += seftItem.FTang;
            //parrentItem.FGiam += seftItem.FGiam;
            CalculateParent(parrentItem, seftItem);
        }

        private bool HasInputData(DcChungTuChiTietModel item)
        {
            bool hasDataDuKienDauNam = item.FDuKienQtDauNam != 0;
            bool hasDataDuToanChuyenNamSau = item.FDuToanChuyenNamSau != 0;
            bool hasDataDuKienCuoiNam = item.FDuKienQtCuoiNam != 0;
            bool hasDataDuToanNganSachNam = item.FDuToanNganSachNam != 0;
            return hasDataDuKienDauNam || hasDataDuKienCuoiNam || hasDataDuToanNganSachNam;
        }

        private void AddChuKy(Dictionary<string, object> data)
        {
            data.Add("Diadiem", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
        }

        private void OnNoteCommand()
        {
            NsBaoCaoGhiChuDialogViewModel.SMaBaoCao = TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2;
            NsBaoCaoGhiChuDialogViewModel.Init();
            NsBaoCaoGhiChuDialogViewModel.ShowDialogHost("UnitAdjustedEstimateReport");
        }

        private List<GhiChu> GetGhiChu()
        {
            string typeChuKy = TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2;
            int iNamLamViec = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<NsCauHinhBaoCao, bool>> predicate = PredicateBuilder.True<NsCauHinhBaoCao>();
            predicate = predicate.And(x => x.INamLamViec.Equals(iNamLamViec));
            predicate = predicate.And(x => x.SMaBaoCao == typeChuKy);
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

        private class GhiChu
        {
            public string Content { get; set; }
            public string SGhiChu => Content;
        }
    }
}
