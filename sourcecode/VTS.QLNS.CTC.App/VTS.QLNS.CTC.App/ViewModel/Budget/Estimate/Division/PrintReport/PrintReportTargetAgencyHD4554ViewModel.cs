using AutoMapper;
using ControlzEx.Standard;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportTargetAgencyHD4554ViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DtChungTuChiTietModel>
    {
        private INsDtNhanPhanBoMapService _dtChungTuMapService;
        private IMapper _mapper;
        private ICollectionView _listDonViView;
        private INsDonViService _nsDonViService;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private INsDtChungTuService _dtChungTuService;
        private INsDtChungTuChiTietService _chungTuChiTietService;
        private ITnDtChungTuService _tnDtChungTuService;
        private ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private ISktSoLieuService _sktSoLieuService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsMucLucNganSachService _iNsMucLucNganSachService;
        private INsDtNhanPhanBoMapService _iNsDtNhanPhanBoMapService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private bool _hasParentAgency;
        private List<DonVi> _listAgencyOfUser;
        private DuToanTongChiTieu _tongChiTieu;
        private string _diaDiem;
        private string _cap1;
        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportTargetAgencyHD4554);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public List<ReportChiTieuDuToanQuery> ListDataReportChiTieuDonVi { get; set; }
        public List<TnDtDuToanReportModel> ListDataReportDuToan { get; set; }
        public List<TnDtDuToanReport2Model> ListDataReportThuNop { get; set; }

        public IEnumerable<DtChungTuModel> DtChungTuModelNhanPhanBos { get; set; }
        public List<TnDtChungTu> TnDtChungTuPhanBos { get; set; }
        public ObservableCollection<ComboboxItem> Agencies;

        private ObservableCollection<ComboboxItem> _dataDotIn;
        public ObservableCollection<ComboboxItem> DataDotIn
        {
            get => _dataDotIn;
            set => SetProperty(ref _dataDotIn, value);
        }

        private ComboboxItem _selectedDotIn;
        public ComboboxItem SelectedDotIn
        {
            get => _selectedDotIn;
            set
            {
                if (SetProperty(ref _selectedDotIn, value))
                {
                    LoadTieuDe();
                    LoadDonVi();
                }
            }
        }

        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        private string _tieuDe3;
        public string TieuDe3
        {
            get => _tieuDe3;
            set => SetProperty(ref _tieuDe3, value);
        }

        private bool _isLuyKeToiDot;
        public bool IsLuyKeToiDot
        {
            get => _isLuyKeToiDot;
            set
            {
                if (SetProperty(ref _isLuyKeToiDot, value))
                {
                    LoadTieuDe();
                    LoadDonVi();
                }
            }
        }

        private bool _isInTheoChungTu;
        public bool IsInTheoChungTu
        {
            get => _isInTheoChungTu;
            set
            {
                if (SetProperty(ref _isInTheoChungTu, value))
                {
                    LoadTieuDe();
                    LoadDonVi();
                    LoadDotPhanBo();
                }
            }
        }

        private bool _isInTieuNganh;
        public bool IsInTieuNganh
        {
            get => _isInTieuNganh;
            set => SetProperty(ref _isInTieuNganh, value);
        }

        private bool _isInMaKBNN;
        public bool IsInMaKBNN
        {
            get => _isInMaKBNN;
            set => SetProperty(ref _isInMaKBNN, value);
        }

        public bool InMotToChecked { get; set; }

        public bool InMaKBNN { get; set; }

        //private bool _isInTongHop;
        //public bool IsInTongHop
        //{
        //    get => _isInTongHop;
        //    set => SetProperty(ref _isInTongHop, value);
        //}

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

        private ObservableCollection<ComboboxItem> _dataLoaiChungTu;
        public ObservableCollection<ComboboxItem> DataLoaiChungTu
        {
            get => _dataLoaiChungTu;
            set => SetProperty(ref _dataLoaiChungTu, value);
        }

        private ComboboxItem _selectedLoaiChungTu;
        public ComboboxItem SelectedLoaiChungTu
        {
            get => _selectedLoaiChungTu;
            set
            {
                if (SetProperty(ref _selectedLoaiChungTu, value))
                {
                    LoadDonVi();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiBaoCao;
        public ObservableCollection<ComboboxItem> DataLoaiBaoCao
        {
            get => _dataLoaiBaoCao;
            set => SetProperty(ref _dataLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                SetProperty(ref _selectedLoaiBaoCao, value);
                if (SelectedLoaiBaoCao != null)
                {
                    LoadTieuDe();
                    LoadDonVi();
                    IsInMaKBNN = SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_CHITIET_DONVI;
                }
            }
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

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Count : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        public bool? SelectAllDonVi
        {
            get
            {
                if (ListDonVi != null)
                {
                    var selected = ListDonVi.Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ListDonVi);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        public bool IsEnableButtonPrint
        {
            get
            {
                if (ListDonVi != null && ListDonVi.Where(n => n.IsChecked).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_cbxVoucherTypeSelected != null)
                {
                    LoadDotPhanBo();
                    LoadDonVi();
                }
            }
        }

        private bool IsInit { get; set; }
        private string _chiTietToi;
        private DonVi DonViRoot;
        private List<DonVi> ListDonViCurrent;
        private List<string> ListMaDonViCurrent;

        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportTargetAgencyHD4554ViewModel(
           INsDtNhanPhanBoMapService dtChungTuMapService,
           IMapper mapper,
           INsDonViService nSDonViService,
           ISessionService sessionService,
           ISktSoLieuService sktSoLieuService,
           ILog logger,
           INsDtChungTuChiTietService chungTuChiTietService,
           IExportService exportService,
           INsDtChungTuService dtChungTuService,
           IDanhMucService danhMucService,
           IDmChuKyService dmChuKyService,
           INsDtNhanPhanBoMapService iNsDtNhanPhanBoMapService,
           INsMucLucNganSachService iNsMucLucNganSachService,
           ITnDtChungTuService tnDtChungTuService,
           ITnDtChungTuChiTietService tnDtChungTuChiTietService,
           DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _dtChungTuMapService = dtChungTuMapService;
            _mapper = mapper;
            _nsDonViService = nSDonViService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _exportService = exportService;
            _sktSoLieuService = sktSoLieuService;
            _dtChungTuService = dtChungTuService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _iNsDtNhanPhanBoMapService = iNsDtNhanPhanBoMapService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _logger = logger;
            _tnDtChungTuService = tnDtChungTuService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;

            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => OnExportReport(ExportType.PDF));
            PrintExcelCommand = new RelayCommand(o => OnExportReport(ExportType.EXCEL));
            PrintBrowserCommand = new RelayCommand(o => OnExportReport(ExportType.PDF, true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                if (Model == null)
                {
                    Model = new DtChungTuModel();
                }
                IsInit = true;
                _sessionInfo = _sessionService.Current;
                LoadLoaiBaoCao();
                LoadUserAgency();
                LoadVoucherType();
                LoadCombobox();
                LoadTieuDe();
                IsInit = false;
                LoadDonVi();
                LoadDanhMuc();
                LoadChiTietToi();
                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? "Hà Nội" : danhMucDiaDiem.SGiaTri;
                DonViRoot = _nsDonViService.FindByCondition(NSConstants.ZERO, StatusType.ACTIVE, _sessionInfo.YearOfWork).FirstOrDefault();
                ListDonViCurrent = _nsDonViService.FindAllDataDonViCurrent(_sessionInfo.YearOfWork).ToList();
                ListMaDonViCurrent = ListDonViCurrent.IsEmpty() ? new List<string>() : ListDonViCurrent.Select(x => x.IIDMaDonVi).ToList();
                ReportDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnExportReport(ExportType exportType, bool isBrowser = false)
        {
            try
            {
                _ = BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int i = 0;
                    if (SelectedDotIn != null)
                    {

                        var res = PrintPhuongAnPhanBo4554(exportType);
                        if (res != null) results.AddRange(res);
                        else
                        {
                            MessageBoxHelper.Warning("Không có dữ liệu");
                            return;
                        }


                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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


        public string GetHeader2Report()
        {
            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
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
        private void AddRangeCheckbox(List<CheckBoxItem> items, int size)
        {
            items ??= new List<CheckBoxItem>();
            int count = items.Count < 4 ? (items.Count) : items.Count % size;
            List<CheckBoxItem> itemsAdd = new List<CheckBoxItem>();
            for (int i = 0; i < (count == 0 ? count : size - count); i++)
            {
                itemsAdd.Add(new CheckBoxItem());
            }
            if (!itemsAdd.IsEmpty()) items.AddRange(itemsAdd);
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public List<ExportResult> PrintPhuongAnPhanBo4554(ExportType exportType)
        {
            List<ExportResult> exportResults = new List<ExportResult>();

            int donViTinh = SelectedDonViTinh != null ? int.Parse(SelectedDonViTinh.ValueItem) : 1;
            var tuple = GetTupleIdChungTu();
            var agencies = ListDonVi.Where(x => x.IsChecked).OrderBy(o => o.ValueItem).ToList();
            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            List<int> hideColumns = new List<int>();
            int startCol = 17;
            int endCol = InMotToChecked ? startCol + agencies.Count() : startCol + 3;
            string mergeRange = $"{GetExcelColumnName(startCol)}10:{GetExcelColumnName(endCol)}10";
            var listData = _chungTuChiTietService.ExportPhuongAnPhanBo4554(
                string.Join(",", agencies.Select(x => x.ValueItem)), _sessionInfo.YearOfWork,  _sessionInfo.YearOfBudget,_sessionInfo.Budget, string.Join(",", tuple.Item2.IsEmpty() ? new List<Guid>() { Guid.Empty } : tuple.Item2), string.Join(",", tuple.Item3.IsEmpty() ? new List<Guid>() { Guid.Empty } : tuple.Item3), donViTinh, SelectedLoaiChungTu.ValueItem).ToList();
            ListDataReportDuToan = _mapper.Map<List<TnDtDuToanReportModel>>(listData.Where(x => !x.IsThuNop).ToList());
            ListDataReportThuNop = _mapper.Map<List<TnDtDuToanReport2Model>>(listData.Where(x => x.IsThuNop).ToList());
            int size = InMotToChecked ? agencies.Count() + 1 : 4;
            agencies.Insert(0, new CheckBoxItem() { DisplayItem = "Bản thân" });
            AddRangeCheckbox(agencies, size);
            List<List<CheckBoxItem>> listDonViSplits = SplitList(agencies, size).ToList();
            foreach (var donviPage in listDonViSplits.Select((value, index) => (value, index)))
            {
                List<HeaderReport> listHeader = new List<HeaderReport>();
                List<TnDtDuToanReportModel> resultDuToans = new List<TnDtDuToanReportModel>();
                List<TnDtDuToanReport2Model> resultThuNops = new List<TnDtDuToanReport2Model>();
                List<TnDtDuToanReportModel> itemsDuToanPage = new List<TnDtDuToanReportModel>();
                List<TnDtDuToanReport2Model> itemsThuNopPage = new List<TnDtDuToanReport2Model>();

                var itemsDuToanClone = ListDataReportDuToan.Clone();
                var itemsThuNopClone = ListDataReportThuNop.Clone();
                if (donviPage.index == NSConstants.ZERO)
                {
                    itemsDuToanPage = itemsDuToanClone.Where(x => x.BHangCha || (!x.BHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi) || x.IPhanCap == NSConstants.ZERO)).ToList();
                    itemsThuNopPage = itemsThuNopClone.Where(x => x.BHangCha || (!x.BHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi) || x.IPhanCap == NSConstants.ZERO)).ToList();
                }
                else
                {
                    itemsDuToanPage = itemsDuToanClone.Where(x => x.BHangCha || (!x.BHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi) && (donviPage.value.Where(f => !string.IsNullOrEmpty(f.ValueItem)).Select(s => s.ValueItem).Contains(x.IIdMaDonVi)) || x.IPhanCap == NSConstants.ZERO)).ToList();
                    itemsThuNopPage = itemsThuNopClone.Where(x => x.BHangCha || (!x.BHangCha && !string.IsNullOrEmpty(x.IIdMaDonVi) && (donviPage.value.Where(f => !string.IsNullOrEmpty(f.ValueItem)).Select(s => s.ValueItem).Contains(x.IIdMaDonVi)) || x.IPhanCap == NSConstants.ZERO)).ToList();
                }

                foreach (var tenDv in donviPage.value.Select((value, index) => (value, index)))
                {
                    listHeader.Add(new HeaderReport
                    {
                        Name = tenDv.value.DisplayItem,
                        Index = donviPage.index > 0 ? tenDv.index + 1 : tenDv.index + 2,
                        ColMerge = tenDv.index + 1
                    });
                }
                var itemsDuToanResult = itemsDuToanPage.GroupBy(x => x.MlnsId).Select(s => new TnDtDuToanReportModel()
                {
                    MlnsId = s.Key,
                    MlnsIdParent = s.FirstOrDefault()?.MlnsIdParent ?? Guid.Empty,
                    XauNoiMa = s.FirstOrDefault()?.XauNoiMa ?? string.Empty,
                    M = s.FirstOrDefault()?.M ?? string.Empty,
                    Lns = s.FirstOrDefault()?.Lns ?? string.Empty,
                    L = s.FirstOrDefault()?.L ?? string.Empty,
                    K = s.FirstOrDefault()?.K ?? string.Empty,
                    Tm = s.FirstOrDefault()?.Tm ?? string.Empty,
                    Ttm = s.FirstOrDefault()?.Ttm ?? string.Empty,
                    Ng = s.FirstOrDefault()?.Ng ?? string.Empty,
                    Tng = s.FirstOrDefault()?.Tng ?? string.Empty,
                    Tng1 = s.FirstOrDefault()?.Tng1 ?? string.Empty,
                    Tng2 = s.FirstOrDefault()?.Tng2 ?? string.Empty,
                    Tng3 = s.FirstOrDefault()?.Tng3 ?? string.Empty,
                    MoTa = s.FirstOrDefault()?.MoTa ?? string.Empty,
                    BHangCha = s.FirstOrDefault()?.BHangCha ?? false,
                    //FDuToan = ParseDataDuToan(s.ToList(), null),
                    LstGiaTri = ParseDataGroup(s.ToList(), null, donviPage.value),
                    ChiTietToi = s.FirstOrDefault()?.ChiTietToi
                }).ToList();

                var itemsThuNopResult = itemsThuNopPage.GroupBy(x => x.MlnsId).Select(s => new TnDtDuToanReport2Model()
                {
                    MlnsId = s.Key,
                    MlnsIdParent = s.FirstOrDefault()?.MlnsIdParent ?? Guid.Empty,
                    XauNoiMa = s.FirstOrDefault()?.XauNoiMa ?? string.Empty,
                    M = s.FirstOrDefault()?.M ?? string.Empty,
                    Lns = s.FirstOrDefault()?.Lns ?? string.Empty,
                    L = s.FirstOrDefault()?.L ?? string.Empty,
                    K = s.FirstOrDefault()?.K ?? string.Empty,
                    Tm = s.FirstOrDefault()?.Tm ?? string.Empty,
                    Ttm = s.FirstOrDefault()?.Ttm ?? string.Empty,
                    Ng = s.FirstOrDefault()?.Ng ?? string.Empty,
                    Tng = s.FirstOrDefault()?.Tng ?? string.Empty,
                    Tng1 = s.FirstOrDefault()?.Tng1 ?? string.Empty,
                    Tng2 = s.FirstOrDefault()?.Tng2 ?? string.Empty,
                    Tng3 = s.FirstOrDefault()?.Tng3 ?? string.Empty,
                    MoTa = s.FirstOrDefault()?.MoTa ?? string.Empty,
                    BHangCha = s.FirstOrDefault()?.BHangCha ?? false,
                    //FDuToan = ParseDataDuToan(null, s.ToList()),
                    ListGiaTri = ParseDataGroup(null, s.ToList(), donviPage.value),
                    ChiTietToi = s.FirstOrDefault()?.ChiTietToi
                }).ToList();

                var itemChildDuToan = new List<TnDtDuToanReportModel>();
                if (!itemsDuToanResult.IsEmpty())
                {
                    itemsDuToanResult.Where(x => !x.BHangCha).ForAll(x => ParseDataDuToan(x, null));
                    itemChildDuToan = itemsDuToanResult.Where(x => !x.BHangCha && x.IsDynamic).ToList();
                    foreach (var item in itemChildDuToan)
                    {
                        FilterData(item, resultDuToans, itemsDuToanResult);
                    }
                }
                resultDuToans = resultDuToans.Select(x =>
                {
                    if (x.Lns != x.XauNoiMa)
                    {
                        x.Lns = string.Empty;
                    }
                    return x;
                })
                .OrderBy(o => o.XauNoiMa).ToList();

                var itemChildThuNop = new List<TnDtDuToanReport2Model>();
                if (!itemsThuNopResult.IsEmpty())
                {
                    itemsThuNopResult.Where(x => !x.BHangCha).ForAll(x => ParseDataDuToan(null, x));
                    itemChildThuNop = itemsThuNopResult.Where(x => !x.BHangCha && x.IsDynamic).ToList();
                    foreach (var item in itemChildThuNop)
                    {
                        FilterData2(item, resultThuNops, itemsThuNopResult);
                    }
                }
                resultThuNops = resultThuNops.Select(x =>
                {
                    if (x.Lns != x.XauNoiMa)
                    {
                        x.Lns = string.Empty;
                    }
                    return x;
                })
                .OrderBy(o => o.XauNoiMa).ToList();
                if (SelectedLoaiBaoCao.ValueItem == "2")
                {
                    Guid newId = Guid.NewGuid();

                    var thunop = resultThuNops.Where(x => x.MlnsIdParent.IsNullOrEmpty());
                    if (thunop != null) thunop.ForAll(x => x.MlnsIdParent = newId);
                    resultThuNops.Insert(0, new TnDtDuToanReport2Model()
                    {
                        MlnsId = newId,
                        MlnsIdParent = Guid.Empty,
                        MoTa = "A. TỔNG SỐ THU NỘP NGÂN SÁCH NHÀ NƯỚC",
                        ListGiaTri = GetDefaultListData(listHeader),
                        BHangCha = true
                    });
                }
                else if (SelectedLoaiBaoCao.ValueItem == "3")
                {
                    Guid newId = Guid.NewGuid();
                    var dutoan = resultDuToans.Where(x => x.MlnsIdParent.IsNullOrEmpty());
                    if (dutoan != null) dutoan.ForAll(x => x.MlnsIdParent = newId);
                    resultThuNops.Insert(0, new TnDtDuToanReport2Model()
                    {
                        MlnsId = newId,
                        MlnsIdParent = Guid.Empty,
                        MoTa = "B. DỰ TOÁN CHI NGÂN SÁCH NHÀ NƯỚC",
                        ListGiaTri = GetDefaultListData(listHeader),
                        BHangCha = true
                    });
                }
                else
                {
                    Guid newId = Guid.NewGuid();
                    var dutoan = resultDuToans.Where(x => x.MlnsIdParent.IsNullOrEmpty());
                    if (dutoan != null) dutoan.ForAll(x => x.MlnsIdParent = newId);
                    Guid newId2 = Guid.NewGuid();
                    var thunop = resultThuNops.Where(x => x.MlnsIdParent.IsNullOrEmpty());
                    if (thunop != null) thunop.ForAll(x => x.MlnsIdParent = newId2);
                    resultDuToans.Insert(0, new TnDtDuToanReportModel()
                    {
                        MlnsId = newId,
                        MlnsIdParent = Guid.Empty,
                        MoTa = "B. DỰ TOÁN CHI NGÂN SÁCH NHÀ NƯỚC",
                        LstGiaTri = GetDefaultListData(listHeader),
                        BHangCha = true
                    });

                    resultThuNops.Insert(0, new TnDtDuToanReport2Model()
                    {
                        MlnsId = newId2,
                        MlnsIdParent = Guid.Empty,
                        MoTa = "A. TỔNG SỐ THU NỘP NGÂN SÁCH NHÀ NƯỚC",
                        ListGiaTri = GetDefaultListData(listHeader),
                        BHangCha = true
                    });
                }

                HiddenColumnItems(ref resultDuToans,ref resultThuNops);
                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", resultThuNops);
                data.Add("ListData2", resultDuToans);
                data.Add("TieuDe1", TieuDe1);
                data.Add("TieuDe2", TieuDe2);
                data.Add("TieuDe3", TieuDe3);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("DonVi", _sessionInfo.TenDonVi);
                data.Add("Cap1", GetDonViBanHanh(1, _dmChuKy.LoaiDVBanHanh1, itemDanhMuc, _sessionInfo.TenDonVi));
                data.Add("Cap2", GetDonViBanHanh(2, _dmChuKy.LoaiDVBanHanh2, itemDanhMuc, _sessionInfo.TenDonVi));
                data.Add("DiaDiem", _diaDiem);
                data.Add("h1", _selectedDonViTinh != null ? $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}" : "");
                data.Add("donViTinh", "Đơn vị tính: " + (_selectedDonViTinh != null ? _selectedDonViTinh.DisplayItem : ""));
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("IsPhuLuc", false);
                data.Add("IsTrinhKy", true);

                data.Add("Count", 1000000);
                data.Add("Count2", 2000000);
                AddChuKy(data, "");
                //var ghiChu = GetGhiChu();
                //data.Add("HasGhiChu", ghiChu.Any());
                //data.Add("ListGhiChu", ghiChu);
                data.Add("Headers", listHeader);
                data.Add("Headers2", listHeader);
                data.Add("ListHeader1", listHeader);
                data.Add("Total", listHeader);
                data.Add("Empty", listHeader);
                data.Add("MergeRange", mergeRange);
                hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(SelectedInToiMuc.ValueItem);
                string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOAN_PHUONG_AN_ONEPAGE));
                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + _sessionInfo.TenDonVi;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<TnDtDuToanReportModel, TnDtDuToanReport2Model, TnDtdnChungTuChiTietDynamicModel, HeaderReport>(templateFileName, data, hideColumns.Select(x => x + 2).ToList());
                if (donviPage.index > 0)
                {
                    xlsFile.SetColHidden(16, true);// hidden cot Du toan duoc giao  tu page 2
                }
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
            }

            return exportResults;
        }

        private void HiddenColumnItems(ref List<TnDtDuToanReportModel> items1, ref List<TnDtDuToanReport2Model> items2)
        {
            if (SelectedInToiMuc.ValueItem == NSChiTietToi.DB_VALUE_NGANH)
            {
                if (!items1.IsEmpty() && items1.Any(x => x.IsTng))
                {
                    var itemRemove = items1.Where(x => x.IsTng).ToList();
                    CalculateDataRemove(items1, itemRemove);
                    items1 = items1.Except(itemRemove).ToList();
                }
                if (!items2.IsEmpty() && items2.Any(x => x.IsTng))
                {
                    var itemRemove2 = items2.Where(x => x.IsTng).ToList();
                    CalculateDataRemoveThu(items2, itemRemove2);
                    items2 = items2.Except(itemRemove2).ToList();
                }
            }
        }

        private void CalculateDataRemove(List<TnDtDuToanReportModel> items, List<TnDtDuToanReportModel> itemsRemove)
        {
            var dicItem = items.ToDictionary(key => key.MlnsId, value => value);
            foreach (var item in itemsRemove)
            {
                CalculateParent(item, dicItem);
            }
        }

        private void CalculateParent(TnDtDuToanReportModel item, Dictionary<Guid?, TnDtDuToanReportModel> dicitems)
        {
            if (item is null || item.MlnsIdParent.IsNullOrEmpty()) return;
            TnDtDuToanReportModel parent = dicitems.ContainsKey(item.MlnsIdParent) ? dicitems[item.MlnsIdParent] : null;
            if (parent is null || parent.MlnsIdParent.IsNullOrEmpty()) return;
            parent.FDuToan += item.FDuToan;
            for (int i = 0; i < item.LstGiaTri.Count; i++)
            {
                parent.LstGiaTri[i].FVal += item.LstGiaTri[i].FVal;

            }
            if (!parent.IsTng)
            {
                parent.IsHangCha = false;
                parent.BHangCha = false;
                return;
            }
            CalculateParent(parent, dicitems);
        }

        private void CalculateDataRemoveThu(List<TnDtDuToanReport2Model> items, List<TnDtDuToanReport2Model> itemsRemove)
        {
            var dicItem = items.ToDictionary(key => key.MlnsId, value => value);
            foreach (var item in itemsRemove)
            {
                CalculateParentThu(item, dicItem);
            }
        }

        private void CalculateParentThu(TnDtDuToanReport2Model item, Dictionary<Guid?, TnDtDuToanReport2Model> dicitems)
        {
            if (item is null || item.MlnsIdParent.IsNullOrEmpty()) return;
            TnDtDuToanReport2Model parent = dicitems.ContainsKey(item.MlnsIdParent) ? dicitems[item.MlnsIdParent] : null;
            if (parent is null || parent.MlnsIdParent.IsNullOrEmpty()) return;
            parent.FDuToan += item.FDuToan;
            for (int i = 0; i < item.ListGiaTri.Count; i++)
            {
                parent.ListGiaTri[i].FVal += item.ListGiaTri[i].FVal;
            }
            if (!parent.IsTng)
            {
                parent.IsHangCha = false;
                parent.BHangCha = false;
                return;
            }
            CalculateParentThu(parent, dicitems);
        }

        private List<TnDtdnChungTuChiTietDynamicModel> GetDefaultListData(List<HeaderReport> headers)
        {
            List<TnDtdnChungTuChiTietDynamicModel> outPut = new List<TnDtdnChungTuChiTietDynamicModel>();
            foreach (var item in headers)
            {
                outPut.Add(new TnDtdnChungTuChiTietDynamicModel());
            }

            return outPut;
        }

        private string GetChiTietToi(List<TnDtDuToanReportModel> items, List<TnDtDuToanReport2Model> items2)
        {
            List<string> sChiTietTois = items.Where(x => !string.IsNullOrEmpty(x.ChiTietToi)).Select(x => x.ChiTietToi).Distinct().ToList();
            List<string> sChiTietTois2 = items2.Where(x => !string.IsNullOrEmpty(x.ChiTietToi)).Select(x => x.ChiTietToi).Distinct().ToList();
            if (sChiTietTois.IsEmpty() && sChiTietTois2.IsEmpty()) return "NG";
            List<string> chiTiet = new List<string>
            {
                DynamicMLNS.GetMaxNameColumnByChiTietToi(sChiTietTois),
                DynamicMLNS.GetMaxNameColumnByChiTietToi(sChiTietTois2)
            };
            return chiTiet.OrderByDescending(x => x).First();
        }

        private void FilterData(TnDtDuToanReportModel item, List<TnDtDuToanReportModel> results, List<TnDtDuToanReportModel> items)
        {
            if (item.IsDynamic)
            {
                results.Add(item);
                AddParent(item, results, items);
            }
        }
        private void AddParent(TnDtDuToanReportModel item, List<TnDtDuToanReportModel> results, List<TnDtDuToanReportModel> items)
        {
            if (item.MlnsIdParent.IsNullOrEmpty()) return;
            HandleMlns(item, results, items, null, null, null);
            if (results.Any(x => item.MlnsIdParent.Equals(x.MlnsId))) return;
            TnDtDuToanReportModel parent = items.FirstOrDefault(x => x.MlnsId.Equals(item.MlnsIdParent));
            if (parent is null) return;
            results.Add(parent);
            AddParent(parent, results, items);
        }

        private void HandleMlns(TnDtDuToanReportModel item, List<TnDtDuToanReportModel> parents1, List<TnDtDuToanReportModel> items1, TnDtDuToanReport2Model item2, List<TnDtDuToanReport2Model> parents2, List<TnDtDuToanReport2Model> items2)
        {
            if (item2 is null)
            {
                TnDtDuToanReportModel parent = items1.FirstOrDefault(x => x.MlnsId.Equals(item.MlnsIdParent));
                if (parents1.Any(x => item.MlnsIdParent.Equals(x.MlnsId)))
                {
                    TnDtDuToanReportModel ite = parents1.FirstOrDefault(x => x.MlnsIdParent.Equals(item.MlnsIdParent) && x.MlnsId != item.MlnsId);
                    if (ite is null) return;
                    //if (string.IsNullOrEmpty(ite.Lns)) item.Lns = string.Empty;
                    if (string.IsNullOrEmpty(ite.L)) item.L = string.Empty;
                    if (string.IsNullOrEmpty(ite.K)) item.K = string.Empty;
                    if (string.IsNullOrEmpty(ite.M)) item.M = string.Empty;
                    if (string.IsNullOrEmpty(ite.Tm)) item.Tm = string.Empty;
                    if (string.IsNullOrEmpty(ite.Ttm)) item.Ttm = string.Empty;
                    if (string.IsNullOrEmpty(ite.Ng)) item.Ng = string.Empty;
                }
                else
                {
                    if (parent is null) return;
                    //if (!string.IsNullOrEmpty(parent.Lns) && (parent.XauNoiMa != parent.Lns)) item.Lns = string.Empty;
                    if (!string.IsNullOrEmpty(parent.L)) item.L = string.Empty;
                    if (!string.IsNullOrEmpty(parent.K)) item.K = string.Empty;
                    if (!string.IsNullOrEmpty(parent.M)) item.M = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Tm)) item.Tm = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Ttm)) item.Ttm = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Ng)) item.Ng = string.Empty;
                }

            }
            else
            {
                TnDtDuToanReport2Model parent2 = items2.FirstOrDefault(x => x.MlnsId.Equals(item2.MlnsIdParent));
                if (parents2.Any(x => item2.MlnsIdParent.Equals(x.MlnsId)))
                {
                    TnDtDuToanReport2Model ite = parents2.FirstOrDefault(x => x.MlnsIdParent.Equals(item2.MlnsIdParent) && x.MlnsId != item2.MlnsId);
                    if (ite is null) return;
                    //if (string.IsNullOrEmpty(ite.Lns)) item2.Lns = string.Empty;
                    if (string.IsNullOrEmpty(ite.L)) item2.L = string.Empty;
                    if (string.IsNullOrEmpty(ite.K)) item2.K = string.Empty;
                    if (string.IsNullOrEmpty(ite.M)) item2.M = string.Empty;
                    if (string.IsNullOrEmpty(ite.Tm)) item2.Tm = string.Empty;
                    if (string.IsNullOrEmpty(ite.Ttm)) item2.Ttm = string.Empty;
                    if (string.IsNullOrEmpty(ite.Ng)) item2.Ng = string.Empty;
                }
                else
                {
                    if (parent2 is null) return;
                   // if (!string.IsNullOrEmpty(parent2.Lns) && (parent2.XauNoiMa != parent2.Lns)) item2.Lns = string.Empty;
                    if (!string.IsNullOrEmpty(parent2.L)) item2.L = string.Empty;
                    if (!string.IsNullOrEmpty(parent2.K)) item2.K = string.Empty;
                    if (!string.IsNullOrEmpty(parent2.M)) item2.M = string.Empty;
                    if (!string.IsNullOrEmpty(parent2.Tm)) item2.Tm = string.Empty;
                    if (!string.IsNullOrEmpty(parent2.Ttm)) item2.Ttm = string.Empty;
                    if (!string.IsNullOrEmpty(parent2.Ng)) item2.Ng = string.Empty;
                }
            }
        }

        private void FilterData2(TnDtDuToanReport2Model item, List<TnDtDuToanReport2Model> results, List<TnDtDuToanReport2Model> items)
        {
            if (item.IsDynamic)
            {
                results.Add(item);
                AddParent2(item, results, items);
            }
        }
        private void AddParent2(TnDtDuToanReport2Model item, List<TnDtDuToanReport2Model> results, List<TnDtDuToanReport2Model> items)
        {
            if (item.MlnsIdParent.IsNullOrEmpty()) return;
            HandleMlns(null, null, null, item, results, items);
            if (results.Any(x => item.MlnsIdParent.Equals(x.MlnsId))) return;
            TnDtDuToanReport2Model parent = items.FirstOrDefault(x => x.MlnsId.Equals(item.MlnsIdParent));
            if (parent is null) return;
            results.Add(parent);
            AddParent2(parent, results, items);
        }
        private List<TnDtdnChungTuChiTietDynamicModel> ParseDataGroup(List<TnDtDuToanReportModel> items, List<TnDtDuToanReport2Model> items2, List<CheckBoxItem> donvi)
        {

            List<TnDtdnChungTuChiTietDynamicModel> outPut = new List<TnDtdnChungTuChiTietDynamicModel>();
            if (items == null)
            {
                if (items2.IsEmpty() && donvi.IsEmpty()) return outPut;
                //AddDataCurrent(outPut, items, donvi);
                foreach (var dv in donvi.Select((value, index) => (index, value)))
                {
                    TnDtdnChungTuChiTietDynamicModel giaTri = new TnDtdnChungTuChiTietDynamicModel();

                    if (!string.IsNullOrEmpty(dv.value.ValueItem))
                    {

                        TnDtDuToanReport2Model gtDonVi = items2.FirstOrDefault(x => dv.value.ValueItem.Equals(x.IIdMaDonVi) && !ListMaDonViCurrent.Contains(x.IIdMaDonVi));
                        if (gtDonVi != null)
                        {
                            giaTri.FVal = gtDonVi.FTuChi;
                        }
                    }
                    else if (dv.index == NSConstants.ZERO)
                    {
                        var itemCurrents = items2.Where(x => ListMaDonViCurrent.Contains(x.IIdMaDonVi) && NSConstants.ZERO != x.IPhanCap);
                        giaTri.FVal = itemCurrents.Sum(x => x.FTuChi);
                    }
                    outPut.Add(giaTri);

                }
            }
            else
            {
                if (items.IsEmpty() && donvi.IsEmpty()) return outPut;
                //AddDataCurrent(outPut, items, donvi);
                foreach (var dv in donvi.Select((value, index) => (index, value)))
                {
                    TnDtdnChungTuChiTietDynamicModel giaTri = new TnDtdnChungTuChiTietDynamicModel();

                    if (!string.IsNullOrEmpty(dv.value.ValueItem))
                    {
                        TnDtDuToanReportModel gtDonVi = items.FirstOrDefault(x => dv.value.ValueItem.Equals(x.IIdMaDonVi) && !ListMaDonViCurrent.Contains(x.IIdMaDonVi));

                        if (gtDonVi != null)
                        {
                            giaTri.FVal = gtDonVi.FTuChi;
                        }
                    }
                    else if (dv.index == NSConstants.ZERO)
                    {
                        var itemCurrents = items.Where(x => ListMaDonViCurrent.Contains(x.IIdMaDonVi) && NSConstants.ZERO != x.IPhanCap);
                        giaTri.FVal = itemCurrents.Sum(x => x.FTuChi);
                    }
                    outPut.Add(giaTri);
                }
            }
            
            return outPut;
        }

        private void ParseDataDuToan(TnDtDuToanReportModel item, TnDtDuToanReport2Model item2)
        {
            double FDuToan = 0;
            if (item == null)
            {
                if (ListDataReportThuNop.IsEmpty()) return;
                List<TnDtDuToanReport2Model> itemThuNop = ListDataReportThuNop.Where(x => x.IPhanCap == NSConstants.ZERO && x.MlnsId.Equals(item2.MlnsId)).ToList();
                FDuToan = itemThuNop.Sum(x => x.FTuChi);
                item2.FDuToan = FDuToan;
            }
            else
            {
                if (ListDataReportDuToan.IsEmpty()) return;
                List<TnDtDuToanReportModel> itemDuToan = ListDataReportDuToan.Where(x => x.IPhanCap == NSConstants.ZERO && x.MlnsId.Equals(item.MlnsId)).ToList();
                FDuToan = itemDuToan.Sum(x => x.FTuChi);
                item.FDuToan = FDuToan;

            }
        }


        private string GetDonViBanHanh(int iDonVi, string loaiDVBH, DanhMuc itemDanhMuc, string selectedUnit)
        {
            string dvBanHanh = "";
            if (itemDanhMuc != null)
            {
                switch (iDonVi)
                {
                    case 1:
                        dvBanHanh = itemDanhMuc.SGiaTri;
                        break;
                    case 2:
                        dvBanHanh = _sessionInfo.TenDonVi;
                        break;
                }
            }
            return dvBanHanh;
        }
        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public string GetTemplateName(string input)
        {
            string loaiChungTuStr = "_NSSD";
            input += loaiChungTuStr;
            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG)
            {
                input += "_landscape";
            }
            input += ".xlsx";
            return input;
        }

        private void LoadDotPhanBo()
        {
            DataDotIn = new ObservableCollection<ComboboxItem>();
            var predicate = CreatePredicateChungTuIndex();
            var predicateThuNop = CreatePredicateChungTuThuNopIndex();
            var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            TnDtChungTuPhanBos = _tnDtChungTuService.FindByCondition(predicateThuNop).ToList();
            Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);
            Models.OrderByDescending(c => c.DNgayQuyetDinh).ThenByDescending(c => c.DNgayChungTu);
            List<string> lstSoQuyetDinh = new List<string>();
            Dictionary<string, string> dicSoQuyetDinhNgay = new Dictionary<string, string>();
            List<DateTime> lstNgayChungTu = new List<DateTime>();
            if (IsInTheoChungTu)
            {
                foreach (DtChungTuModel item in Models)
                {
                    string mota = item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
                    mota += StringUtils.SPACE;
                    mota += item.SMoTa;
                    if (!string.IsNullOrEmpty(item.SSoQuyetDinh))
                    {
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = item.Id.ToString(),
                            DisplayItem = string.Format("{0} - {1}\n{2}", item.SSoQuyetDinh, item.SSoChungTu, mota),
                            HiddenValue = "1",
                            HiddenValueOption2 = TargetAgencyHD4554.DU_TOAN_PHANBO

                        });
                    }
                    else
                    {
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = item.Id.ToString(),
                            DisplayItem = string.Format("{0} - {1}\n{2}", item.SSoQuyetDinh, item.SSoChungTu, mota),
                            HiddenValue = "2",
                            HiddenValueOption2 = TargetAgencyHD4554.DU_TOAN_PHANBO

                        });
                    }

                }

                //add chung tu thu nop
                foreach (var item in TnDtChungTuPhanBos)
                {
                    string mota = item.NgayQuyetDinh.HasValue ? item.NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
                    mota += StringUtils.SPACE;
                    mota += item.MoTaChiTiet;
                    if (!string.IsNullOrEmpty(item.SoChungTu))
                    {
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = item.Id.ToString(),
                            DisplayItem = string.Format("{0} - {1}\n{2}", item.SoQuyetDinh, item.SoChungTu, mota),
                            HiddenValue = "1",
                            HiddenValueOption2 = TargetAgencyHD4554.THU_NOP_PHANBO

                        });
                    }
                    else
                    {
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = item.Id.ToString(),
                            DisplayItem = string.Format("{0} - {1}\n{2}", item.SoQuyetDinh, item.SoChungTu, mota),
                            HiddenValue = "2",
                            HiddenValueOption2 = TargetAgencyHD4554.THU_NOP_PHANBO
                        });
                    }
                }
            }
            else
            {
                foreach (DtChungTuModel item in Models)
                {
                    if (!string.IsNullOrEmpty(item.SSoQuyetDinh) && item.DNgayQuyetDinh.HasValue)
                    {
                        if (!dicSoQuyetDinhNgay.ContainsKey(item.SSoQuyetDinh))
                        {
                            lstSoQuyetDinh.Add(item.SSoQuyetDinh);
                            dicSoQuyetDinhNgay.Add(item.SSoQuyetDinh, item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy"));
                            var lstDotBySoQuyetDinh = Models.Where(x => !string.IsNullOrEmpty(x.SSoQuyetDinh) && x.SSoQuyetDinh.Equals(item.SSoQuyetDinh));
                            var firstList = lstDotBySoQuyetDinh.FirstOrDefault();
                            string mota = firstList.DNgayQuyetDinh.HasValue ? firstList.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
                            mota += StringUtils.SPACE;
                            mota += firstList.SMoTa;

                            DataDotIn.Add(new ComboboxItem
                            {
                                ValueItem = item.SSoQuyetDinh,
                                DisplayItem = string.Format("{0}\n{1}", item.SSoQuyetDinh, mota),
                                HiddenValue = "1",
                                HiddenValueOption2 = TargetAgencyHD4554.DU_TOAN_PHANBO
                            });
                        }
                    }
                    else
                    {
                        var ngayChungTu = item.DNgayChungTu.GetValueOrDefault().Date;
                        if (!lstNgayChungTu.Contains(ngayChungTu))
                        {
                            lstNgayChungTu.Add(item.DNgayChungTu.GetValueOrDefault().Date);
                            var lstDotByNgayChungTu = Models.Where(x => x.DNgayChungTu.GetValueOrDefault().Date.Equals(ngayChungTu) && string.IsNullOrEmpty(x.SSoQuyetDinh));
                            string mota = "";
                            foreach (var dt in lstDotByNgayChungTu)
                            {
                                if (!string.IsNullOrEmpty(mota))
                                {
                                    mota += "\n";
                                }
                                mota += (dt.DNgayChungTu.HasValue
                                    ? dt.DNgayChungTu.Value.ToString("dd/MM/yyyy")
                                    : string.Empty) + " " + dt.SMoTa;
                            }
                            var lstSoChungTuMota = string.Join(",", lstDotByNgayChungTu.Select(x => x.SSoChungTu));
                            DataDotIn.Add(new ComboboxItem
                            {
                                ValueItem = ngayChungTu.ToString("dd/MM/yyyy"),
                                DisplayItem = string.Format("{0}\n{1}", lstSoChungTuMota, mota),
                                HiddenValue = "2",
                                HiddenValueOption2 = TargetAgencyHD4554.DU_TOAN_PHANBO

                            });
                        }
                    }
                }

                GetDataDot(dicSoQuyetDinhNgay, lstNgayChungTu);
            }

            var ordered = DataDotIn.OrderByDescending(c => DateTime.Parse(c.DisplayItem.Split('\n')[1].Split(" ")[0])).ToList();
            DataDotIn = new ObservableCollection<ComboboxItem>(ordered);
            if (!DataDotIn.IsEmpty())
            {
                SelectedDotIn = DataDotIn.Where(x => x.ValueItem.Equals(Model.SSoQuyetDinh) || x.ValueItem.Equals(Model.SSoChungTu)).Select(x => x).DefaultIfEmpty(DataDotIn.ElementAt(0)).FirstOrDefault();
            }
        }

        private void GetDataDot(Dictionary<string, string> dicSoQuyetDinhNgay, List<DateTime> lstNgayChungTu)
        {
            foreach (var item in TnDtChungTuPhanBos)
            {
                if (!string.IsNullOrEmpty(item.SoQuyetDinh) && item.NgayQuyetDinh.HasValue)
                {
                    if (!dicSoQuyetDinhNgay.ContainsKey(item.SoQuyetDinh))
                    {
                        dicSoQuyetDinhNgay.Add(item.SoQuyetDinh, item.NgayQuyetDinh.Value.ToString("dd/MM/yyyy"));
                        var lstDotBySoQuyetDinh = TnDtChungTuPhanBos.Where(x => !string.IsNullOrEmpty(x.SoQuyetDinh) && x.SoQuyetDinh.Equals(item.SoQuyetDinh));
                        var firstList = lstDotBySoQuyetDinh.FirstOrDefault();
                        string mota = firstList.NgayQuyetDinh.HasValue ? firstList.NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
                        mota += StringUtils.SPACE;
                        mota += firstList.MoTaChiTiet;

                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = item.SoQuyetDinh,
                            DisplayItem = string.Format("{0}\n{1}", item.SoQuyetDinh, mota),
                            HiddenValue = "1",
                            HiddenValueOption2 = TargetAgencyHD4554.THU_NOP_PHANBO

                        });
                    }
                }
                else
                {
                    var ngayChungTu = item.NgayChungTu.GetValueOrDefault().Date;
                    if (!lstNgayChungTu.Contains(ngayChungTu))
                    {
                        lstNgayChungTu.Add(item.NgayChungTu.GetValueOrDefault().Date);
                        var lstDotByNgayChungTu = TnDtChungTuPhanBos.Where(x => x.NgayChungTu.GetValueOrDefault().Date.Equals(ngayChungTu) && string.IsNullOrEmpty(x.SoQuyetDinh));
                        string mota = "";
                        foreach (var dt in lstDotByNgayChungTu)
                        {
                            if (!string.IsNullOrEmpty(mota))
                            {
                                mota += "\n";
                            }
                            mota += (dt.NgayChungTu.HasValue
                                ? dt.NgayChungTu.Value.ToString("dd/MM/yyyy")
                                : string.Empty) + " " + dt.MoTaChiTiet;
                        }
                        var lstSoChungTuMota = string.Join(",", lstDotByNgayChungTu.Select(x => x.SoChungTu));
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = ngayChungTu.ToString("dd/MM/yyyy"),
                            DisplayItem = string.Format("{0}\n{1}", lstSoChungTuMota, mota),
                            HiddenValue = "2",
                            HiddenValueOption2 = TargetAgencyHD4554.THU_NOP_PHANBO

                        });
                    }
                }
            }

        }

        private void LoadCombobox()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = LoaiGiay.MACDINH, DisplayItem = LoaiGiay.DOC },
                new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG }
            };
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();

            DataLoaiChungTu = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = "0", DisplayItem = "In tổng hợp" },
                new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = "In theo ngân sách sử dụng" },
                new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = "In theo ngân sách đặc thù ngành" }
            };
            SelectedLoaiChungTu = DataLoaiChungTu.FirstOrDefault();

            List<ComboboxItem> units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh.Count == 0)
                units.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem
                {
                    DisplayItem = dvt.STen,
                    ValueItem = dvt.SGiaTri,
                    Type = dvt.SMoTa
                };
                units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            DataDonViTinh = new ObservableCollection<ComboboxItem>(units);
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        private void GetDonViReport(ref List<DonVi> listDonVi)
        {
            listDonVi ??= new List<DonVi>();
            var tuple = GetTupleIdChungTu();
            int iLoaiChungTu = SelectedLoaiChungTu != null ? int.Parse(SelectedLoaiChungTu.ValueItem) : 0;
            if (tuple != null)
            {
                switch (tuple.Item1)
                {
                    case TargetAgencyHD4554.DU_TOAN_PHANBO:
                        listDonVi = tuple.Item2.IsEmpty() ? new List<DonVi>() : _nsDonViService.FindForEstimateDivisionReport(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, iLoaiChungTu, string.Join(",", tuple.Item2), IsLuyKeToiDot).ToList();
                        break;
                    case TargetAgencyHD4554.THU_NOP_PHANBO:
                        listDonVi = tuple.Item3.IsEmpty() ? new List<DonVi>() : _nsDonViService.FindForRevenueExpenditureDivisionReport(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, string.Join(",", tuple.Item3), IsLuyKeToiDot).ToList();
                        break;
                    case TargetAgencyHD4554.ALL:
                        var listDonViDt = tuple.Item2.IsEmpty() ? new List<DonVi>() : _nsDonViService.FindForEstimateDivisionReport(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, iLoaiChungTu, string.Join(",", tuple.Item2), IsLuyKeToiDot).ToList();
                        var listDonViTn = tuple.Item3.IsEmpty() ? new List<DonVi>() : _nsDonViService.FindForRevenueExpenditureDivisionReport(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, string.Join(",", tuple.Item3), IsLuyKeToiDot).ToList();
                        if (!listDonViDt.IsEmpty()) listDonVi.AddRange(listDonViDt);
                        if (!listDonViTn.IsEmpty()) listDonVi.AddRange(listDonViTn);
                        break;
                    default:
                        listDonVi = new List<DonVi>();
                        break;
                }
            }

        }

        private void LoadDonVi()
        {
            if (IsInit) return;
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            List<DonVi> listDonVi = new List<DonVi>();
            GetDonViReport(ref listDonVi);

            if (!_hasParentAgency)
            {
                listDonVi = listDonVi.Where(x => _listAgencyOfUser.Select(y => y.IIDMaDonVi).Contains(x.IIDMaDonVi))
                    .ToList();
            }
            listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();

            foreach (DonVi item in listDonVi)
            {
                if ((ListDonVi.Count() == 0 || ListDonVi.Where(n => n.ValueItem == item.IIDMaDonVi).Count() == 0) &&
                    !string.IsNullOrEmpty(item.IIDMaDonVi)
                    && !string.IsNullOrEmpty(item.TenDonVi))
                    ListDonVi.Add(new CheckBoxItem { ValueItem = item.IIDMaDonVi, DisplayItem = item.TenDonVi });
            }

            //ListDonVi.Add(new CheckBoxItem { ValueItem = "@", DisplayItem = string.Format("{0} - {1}", "@", "Dự phòng") });
            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
            foreach (var model in ListDonVi)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                        OnPropertyChanged(nameof(IsEnableButtonPrint));
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

        private void LoadTieuDe()
        {
            try
            {
                string titleFirst = $"PHƯƠNG ÁN PHÂN BỔ DỰ TOÁN CHI NGÂN SÁCH NHÀ NƯỚC NĂM {_sessionInfo.YearOfWork}";

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_PHUONGAN_THU_CHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _dmChuKy ??= new DmChuKy();
                _dmChuKy.TieuDe1MoTa = !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa) ? _dmChuKy.TieuDe1MoTa : titleFirst;
                TieuDe1 = _dmChuKy.TieuDe1MoTa;
                TieuDe2 = _dmChuKy.TieuDe2MoTa;
                TieuDe3 = _dmChuKy.TieuDe3MoTa;
                if (IsLuyKeToiDot)
                {
                    List<DtChungTuModel> lstChungTu = SelectedDotIn != null ? Models.Where(n => n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList() : new List<DtChungTuModel>();
                    if (lstChungTu.Count > 0)
                    {
                        var ngayChungTu = lstChungTu.FirstOrDefault().DNgayQuyetDinh.HasValue
                            ? lstChungTu.FirstOrDefault().DNgayChungTu.Value.ToString("dd/MM/yyyy")
                            : string.Empty;
                        TieuDe2 = string.Format("(Tới đợt ngày: {0})", ngayChungTu);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDanhMuc()
        {
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                _chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(_chiTietToi));
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
            }
        }

        private void LoadLoaiBaoCao()
        {
            var printType = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tất cả", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Phần thu", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Phần chi", ValueItem = "3"}
            };
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>(printType);
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedLoaiBaoCao));
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            return predicate;
        }

        private Expression<Func<TnDtChungTu, bool>> CreatePredicateChungTuThuNopIndex()
        {
            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.NguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.NamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            return predicate;
        }

        private void LoadNhanPhanBo()
        {
            var dtChungTuMapByIdPhanBoDuToan = _dtChungTuMapService.FindByIdPhanBoDuToan(Model.Id.ToString()).ToList();
            var listIdNhanPhanBo = dtChungTuMapByIdPhanBoDuToan.Select(e => e.IIdCtduToanNhan.ToString()).ToHashSet();

            var listNhanPhanBo = new List<NsDtChungTu>();
            if (dtChungTuMapByIdPhanBoDuToan.Count() > 0)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => listIdNhanPhanBo.Contains(x.Id.ToString()));
                listNhanPhanBo = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            }
            DtChungTuModelNhanPhanBos = _mapper.Map<ObservableCollection<DtChungTuModel>>(listNhanPhanBo);
        }

        private void LoadAgencies()
        {
            var idDonVi = _sessionInfo.IdDonVi;
            var namLamViec = _sessionInfo.YearOfWork;
            var iLoai = SoChungTuType.EstimateDivision;

            if (Model.ILoaiChungTu.HasValue && VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.ToString()))
            {
                var listDonVi = _nsDonViService.FindByListIdDonVi(idDonVi, namLamViec);
                if (listDonVi.Any(item => true.Equals(item.BCoNSNganh) && item.ITrangThai == NSEntityStatus.ACTIVED && item.Loai == SoChungTuType.ReceiveEstimate.ToString()))
                {
                    iLoai = 2;
                }
                else
                {
                    iLoai = SoChungTuType.EstimateDivision;
                }
            }

            var listNsDonVi = _nsDonViService.FindByCondition(iLoai, NSEntityStatus.ACTIVED, namLamViec).ToList();
            if (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                var listIdDonVi = string.IsNullOrEmpty(Model.SDsidMaDonVi) ? new List<string>() : Model.SDsidMaDonVi.Split(",").ToList();
                listNsDonVi = listNsDonVi.Where(x => listIdDonVi.Contains(x.IIDMaDonVi)).ToList();
            }

            Agencies = _mapper.Map<ObservableCollection<ComboboxItem>>(listNsDonVi);
        }

        private DtChungTuChiTietModel CloneRow(DtChungTuChiTietModel sourceItem)
        {
            var targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IIdDtchungTu = null;
            targetItem.IIdMaDonVi = null;
            targetItem.FTuChi = 0;
            targetItem.FHienVat = 0;
            targetItem.FHangNhap = 0;
            targetItem.FHangMua = 0;
            targetItem.FPhanCap = 0;
            targetItem.SGhiChu = null;
            targetItem.IsModified = true;
            targetItem.IsEnabledCbxDotNhan = true;
            targetItem.IsEnabledCbxDonVi = true;
            targetItem.FTuChiTruocDieuChinh = 0;
            targetItem.FTuChiSauDieuChinh = 0;
            targetItem.FHienVatTruocDieuChinh = 0;
            targetItem.FHienVatSauDieuChinh = 0;

            if (sourceItem.CbxDonVi != null && sourceItem.CbxDonVi.Any())
            {
                var defaultValue = Agencies.ElementAt(0);
                var agency = Agencies.Where(x => x.ValueItem.Equals(sourceItem.IIdMaDonVi)).DefaultIfEmpty(defaultValue).First();
                targetItem.IIdMaDonVi = agency.ValueItem;
                targetItem.STenDonVi = agency.DisplayItem;
            }

            return targetItem;
        }

        private string CreateKeyMapRow(DtChungTuChiTietModel item)
        {
            if (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                return string.Join(StringUtils.DELIMITER, item.IIdDtchungTu.ToString(), item.IIdMlns.ToString(), item.IIdMaDonVi, item.IIdCtduToanNhan);
            }
            return string.Join(StringUtils.DELIMITER, item.IIdDtchungTu.ToString(), item.IIdMlns.ToString(), item.IIdCtduToanNhan, item.IdDotPhanBoTruoc);
        }

        public string GetPath(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_DT, input);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_DONVI)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            }
            else
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            }
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                LoadTieuDe();
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadVoucherType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);

            OnPropertyChanged(nameof(CbxVoucherType));
            OnPropertyChanged(nameof(CbxVoucherTypeSelected));
        }

        private void LoadUserAgency()
        {
            _hasParentAgency = false;
            _listAgencyOfUser = _nsDonViService.GetDanhSachDonViByNguoiDung(_sessionInfo.Principal, _sessionInfo.YearOfWork).ToList();
            if (_listAgencyOfUser.Any(x => x.Loai == LoaiDonVi.ROOT))
                _hasParentAgency = true;
        }


        /**
         * 
         *OutPut Tuple:
         *item1: 1 dự toán (item2), 2 thu nộp (item3), 3 lấy cả 2(item2, item3)
         *item2:  data dự toán (item2)
         *item3:  data thu nộp (item3)
         * 
         **/

        private Tuple<string, List<Guid>, List<Guid>> GetTupleIdChungTu()
        {
            if (IsInTheoChungTu && SelectedDotIn != null)
            {
                Guid.TryParse(SelectedDotIn.ValueItem, out Guid guidSelected);

                switch (SelectedLoaiBaoCao.ValueItem)
                {
                    case "1":
                        if (SelectedDotIn.HiddenValueOption2 == TargetAgencyHD4554.DU_TOAN_PHANBO)
                        {
                            return Tuple.Create(TargetAgencyHD4554.ALL, new List<Guid>() { guidSelected }, new List<Guid>());
                        }
                        else
                        {
                            return Tuple.Create(TargetAgencyHD4554.ALL, new List<Guid>(), new List<Guid>() { guidSelected });

                        }
                    case "2":
                        if (SelectedDotIn.HiddenValueOption2 == TargetAgencyHD4554.DU_TOAN_PHANBO)
                        {
                            return Tuple.Create(TargetAgencyHD4554.THU_NOP_PHANBO, new List<Guid>(), new List<Guid>());
                        }
                        else
                        {
                            return Tuple.Create(TargetAgencyHD4554.THU_NOP_PHANBO, new List<Guid>(), new List<Guid>() { guidSelected });

                        }
                    case "3":
                        if (SelectedDotIn.HiddenValueOption2 == TargetAgencyHD4554.DU_TOAN_PHANBO)
                        {
                            return Tuple.Create(TargetAgencyHD4554.DU_TOAN_PHANBO, new List<Guid>() { guidSelected }, new List<Guid>());
                        }
                        else
                        {
                            return Tuple.Create(TargetAgencyHD4554.DU_TOAN_PHANBO, new List<Guid>(), new List<Guid>());

                        }
                    default:
                        return Tuple.Create(TargetAgencyHD4554.ALL, new List<Guid>(), new List<Guid>());
                }
            }
            List<Guid> resultItem2 = new List<Guid>();
            List<Guid> resultItem3 = new List<Guid>();
            List<DtChungTuModel> lstChungTu = new List<DtChungTuModel>();
            List<TnDtChungTu> lstChungTuPhanBo = new List<TnDtChungTu>();

            DateTime ngayLuyKe = DateTime.MinValue;
            DateTime ngayLuyKePhanBo = DateTime.MinValue;

            if (SelectedDotIn == null) return Tuple.Create(TargetAgencyHD4554.ALL, new List<Guid>(), new List<Guid>());
            if (SelectedDotIn.HiddenValue.Equals("1"))
            {
                lstChungTu = Models.Where(n => !string.IsNullOrEmpty(n.SSoQuyetDinh) && n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
                if (!lstChungTu.IsEmpty())
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayQuyetDinh.GetValueOrDefault().Date;
                }

                lstChungTuPhanBo = TnDtChungTuPhanBos.Where(n => !string.IsNullOrEmpty(n.SoQuyetDinh) && n.SoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
                if (!lstChungTuPhanBo.IsEmpty())
                {
                    ngayLuyKePhanBo = lstChungTuPhanBo.FirstOrDefault().NgayQuyetDinh.GetValueOrDefault().Date;
                }
            }
            else
            {
                var ngayChungTu = SelectedDotIn.ValueItem;
                lstChungTu = Models.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.HasValue && n.DNgayChungTu.Value.ToString("dd/MM/yyyy").Equals(ngayChungTu)).ToList();
                if (!lstChungTu.IsEmpty())
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayChungTu.GetValueOrDefault().Date;
                }

                lstChungTuPhanBo = TnDtChungTuPhanBos.Where(n => string.IsNullOrEmpty(n.SoQuyetDinh) && n.NgayQuyetDinh.HasValue && n.NgayQuyetDinh.Value.ToString("dd/MM/yyyy").Equals(ngayChungTu)).ToList();
                if (!lstChungTuPhanBo.IsEmpty())
                {
                    ngayLuyKePhanBo = lstChungTuPhanBo.FirstOrDefault().NgayChungTu.GetValueOrDefault().Date;
                }
            }

            resultItem2.AddRange(lstChungTu.Select(x => x.Id));
            resultItem3.AddRange(lstChungTuPhanBo.Select(x => x.Id));

            // ds chung tu luy ke
            if (IsLuyKeToiDot)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(x => (x.DNgayQuyetDinh == null && x.DNgayChungTu != null && x.DNgayChungTu.Value.Date <= ngayLuyKe) || (x.DNgayQuyetDinh != null && x.DNgayQuyetDinh.Value.Date <= ngayLuyKe));
                var lstCtLuyKe = _dtChungTuService.FindByCondition(predicate).ToList();
                if (!lstCtLuyKe.IsEmpty())
                {
                    resultItem2.AddRange(lstCtLuyKe.Select(x => x.Id));
                }

                //Thu nop
                var predicateTN = PredicateBuilder.True<TnDtChungTu>();
                predicateTN = predicateTN.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
                predicateTN = predicateTN.And(x => x.NamNganSach == _sessionInfo.YearOfBudget);
                predicateTN = predicateTN.And(x => x.NguonNganSach == _sessionInfo.Budget);
                predicateTN = predicateTN.And(x => x.ILoai == SoChungTuType.EstimateDivision);
                predicateTN = predicateTN.And(x => (x.NgayQuyetDinh == null && x.NgayChungTu != null && x.NgayChungTu.Value.Date <= ngayLuyKePhanBo) || (x.NgayQuyetDinh != null && x.NgayQuyetDinh.Value.Date <= ngayLuyKePhanBo));
                var lstCtLuyKeThuNop = _tnDtChungTuService.FindByCondition(predicateTN).ToList();
                if (!lstCtLuyKeThuNop.IsEmpty())
                {
                    resultItem3.AddRange(lstCtLuyKeThuNop.Select(x => x.Id));
                }
            }
            if (SelectedLoaiBaoCao.ValueItem == "2")
            {
                resultItem2 = new List<Guid>();

            }
            else if (SelectedLoaiBaoCao.ValueItem == "3")
            {
                resultItem3 = new List<Guid>();
            }
            return Tuple.Create(TargetAgencyHD4554.ALL, resultItem2, resultItem3);
        }

        private List<Guid> GetListChungTuReport()
        {
            if (IsInTheoChungTu && SelectedDotIn != null)
            {
                Guid.TryParse(SelectedDotIn.ValueItem, out Guid guidSelected);
                return new List<Guid>() { guidSelected };
            }

            List<Guid> result = new List<Guid>();
            List<DtChungTuModel> lstChungTu = new List<DtChungTuModel>();
            DateTime ngayLuyKe = DateTime.MinValue;
            if (SelectedDotIn == null) return new List<Guid>();
            if (SelectedDotIn.HiddenValue.Equals("1"))
            {
                lstChungTu = Models.Where(n => !string.IsNullOrEmpty(n.SSoQuyetDinh) && n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayQuyetDinh.GetValueOrDefault().Date;
                }
            }
            else
            {
                var ngayChungTu = SelectedDotIn.ValueItem;
                lstChungTu = Models.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.HasValue && n.DNgayChungTu.Value.ToString("dd/MM/yyyy").Equals(ngayChungTu)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayChungTu.GetValueOrDefault().Date;
                }
            }
            if (SelectedLoaiChungTu != null)
            {
                if (SelectedLoaiChungTu.ValueItem == "0")
                {
                    result.AddRange(lstChungTu.Select(x => x.Id));
                }
                else
                {
                    result.AddRange(lstChungTu.Where(x => x.ILoaiChungTu == int.Parse(SelectedLoaiChungTu.ValueItem)).Select(x => x.Id));
                }
            }
            else
            {
                result.AddRange(lstChungTu.Select(x => x.Id));
            }
            // ds chung tu luy ke
            if (IsLuyKeToiDot)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(x => (x.DNgayQuyetDinh == null && x.DNgayChungTu != null && x.DNgayChungTu.Value.Date <= ngayLuyKe) || (x.DNgayQuyetDinh != null && x.DNgayQuyetDinh.Value.Date <= ngayLuyKe));
                if (SelectedLoaiChungTu != null)
                {
                    predicate = predicate.And(x => SelectedLoaiChungTu.ValueItem == "0" || x.ILoaiChungTu == int.Parse(SelectedLoaiChungTu.ValueItem));
                }
                var lstCtLuyKe = _dtChungTuService.FindByCondition(predicate).ToList();
                if (lstCtLuyKe.Count > 0)
                {
                    result.AddRange(lstCtLuyKe.Select(x => x.Id));
                }
            }

            return result;
        }

        public void CheckCotHienThiBangChu(List<DtChungTuChiTietModel> lstData, Dictionary<string, object> dic)
        {
            var lstLns = lstData.Select(x => x.SLns).ToList();
            string concatLns = string.Join(",", lstLns);
            var listSettingMLNS = _iNsMucLucNganSachService.FindByListLnsDonVi(concatLns, _sessionInfo.YearOfWork).ToList();

            if (listSettingMLNS.Any(x => x.BTonKho))
            {
                dic.Add("BTonKho", true);
            }
            if (listSettingMLNS.Any(x => x.BTuChi))
            {
                dic.Add("BTuChi", true);
            }

            if (listSettingMLNS.Any(x => x.BHienVat))
            {
                dic.Add("BHienVat", true);
            }

            if (listSettingMLNS.Any(x => x.BDuPhong))
            {
                dic.Add("BDuPhong", true);
            }

            if (listSettingMLNS.Any(x => x.BHangMua))
            {
                dic.Add("BHangMua", true);
            }

            if (listSettingMLNS.Any(x => x.BHangNhap))
            {
                dic.Add("BHangNhap", true);
            }

            if (listSettingMLNS.Any(x => x.BPhanCap))
            {
                dic.Add("BPhanCap", true);
            }
        }

        private string ConvertChiTietToi(string maCiTietToi)
        {
            string chiTietToi = maCiTietToi;
            switch (maCiTietToi.ToUpper())
            {
                case "NG":
                    chiTietToi = "Ngành";
                    break;
                case "TNG":
                    chiTietToi = "Tiểu ngành";
                    break;
                case "TNG1":
                    chiTietToi = "Tiểu ngành 1";
                    break;
                case "TNG2":
                    chiTietToi = "Tiểu ngành 2";
                    break;
                case "TNG3":
                    chiTietToi = "Tiểu ngành 3";
                    break;
            }
            return chiTietToi;
        }

        public void AddNgayDiaDiem(Dictionary<string, object> data)
        {
            //add ngày địa điểm
            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
            data.Add("DiaDiem", _diaDiem);
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

        public string GetTemplate(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_DT, input + FileExtensionFormats.Xlsx);
        }
    }

}
