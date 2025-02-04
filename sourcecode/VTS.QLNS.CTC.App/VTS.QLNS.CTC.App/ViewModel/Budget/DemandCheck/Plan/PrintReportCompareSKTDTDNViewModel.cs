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
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PrintReportCompareSKTDTDNViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private ICollectionView _listDonViView;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ILog _logger;
        private readonly DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _typeChuKy;
        private string _diaDiem;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly ISktMucLucService _iSktMucLucService;
        private SessionInfo _sessionInfo;
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private List<DataReportDynamic> _lstDataDynamic = new List<DataReportDynamic>();
        private List<DataReportDynamic> _lstDataDynamicSummary = new List<DataReportDynamic>();
        private List<DataReportDynamic2> _lstDataDynamic2 = new List<DataReportDynamic2>();
        private List<DataReportDynamic2> _lstDataDynamicSummary2 = new List<DataReportDynamic2>();

        public bool IsInMotTo { get; set; }
        private bool _inTheoTongHop;
        public bool InTheoTongHop
        {
            get => _inTheoTongHop;
            set
            {
                SetProperty(ref _inTheoTongHop, value);
                LoadDonVi();
            }
        }
        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.PrintReportCompareSKTDTDN);
        public bool IsShowInTheoTongHop => _sessionService.Current.IsQuanLyDonViCha && SelectedLoaiBaoCao != null && SelectedLoaiBaoCao.ValueItem == Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI;
        public bool IsShowDonViIn => SelectedLoaiBaoCao != null && SelectedLoaiBaoCao.ValueItem == Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI;
        public NsBaoCaoGhiChuDialogViewModel NsBaoCaoGhiChuDialogViewModel { get; }
        private ObservableCollection<CheckBoxItem> _listDonVi;
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }
        public List<ReportDuToanDauNamTongHopQuery> DataReportTongHop;

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

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
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

        private ComboboxItem _budgetSourceTypeSelected;

        public ComboboxItem BudgetSourceTypeSelected
        {
            get => _budgetSourceTypeSelected;
            set
            {
                SetProperty(ref _budgetSourceTypeSelected, value);
                LoadDonVi();
                //OnPropertyChanged(nameof(SelectedCountDonVi));
            }
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        private string _txtTitleFirst;

        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

        private string _txtTitleThird;
        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set => SetProperty(ref _txtTitleThird, value);
        }

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Count : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _isEnabledOnePage;
        public bool IsEnabledOnePage
        {
            get
            {
                if (SelectedLoaiBaoCao != null && SelectedLoaiBaoCao.ValueItem != Utility.LoaiBaoCao.DU_TOAN_NS_CHI_MUCLUC_DONVI)
                    return true;
                else
                    return false;
            }
            set
            {
                SetProperty(ref _isEnabledOnePage, value);
                OnPropertyChanged(nameof(IsInMotTo));
            }
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
                    SelectAll(value.Value, ListDonVi);
                    OnPropertyChanged();
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

        private ComboboxItem _voucherTypes = new ComboboxItem();

        public ComboboxItem VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
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

        private ObservableCollection<ComboboxItem> _loaiBaoCao;
        public ObservableCollection<ComboboxItem> LoaiBaoCao
        {
            get => _loaiBaoCao;
            set => SetProperty(ref _loaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                SetProperty(ref _selectedLoaiBaoCao, value);
                OnPropertyChanged(nameof(IsShowInTheoTongHop));
                OnPropertyChanged(nameof(IsShowDonViIn));
                if (SelectedLoaiBaoCao != null && SelectedLoaiBaoCao.ValueItem != Utility.LoaiBaoCao.DU_TOAN_NS_CHI_MUCLUC_DONVI)
                    IsInMotTo = false;
                OnPropertyChanged(nameof(IsEnabledOnePage));
                OnPropertyChanged(nameof(IsInMotTo));
                LoadTypeChuKy();
                InTheoTongHop = false;
            }
        }

        private ObservableCollection<ComboboxItem> _dataLoaiNganSach;
        public ObservableCollection<ComboboxItem> DataLoaiNganSach
        {
            get => _dataLoaiNganSach;
            set => SetProperty(ref _dataLoaiNganSach, value);
        }

        private ComboboxItem _selectedLoaiNganSach;
        public ComboboxItem SelectedLoaiNganSach
        {
            get => _selectedLoaiNganSach;
            set
            {
                SetProperty(ref _selectedLoaiNganSach, value);
                LoadDonVi();
                LoadTypeChuKy();
            }
        }

        public RelayCommand NoteCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public PrintReportCompareSKTDTDNViewModel(
         IMapper mapper,
         ISessionService sessionService,
         INsDonViService nsDonViService,
         ILog logger,
         ISktSoLieuService sktSoLieuService,
         IDanhMucService danhMucService,
         IExportService exportService,
         INsNguoiDungDonViService nsNguoiDungDonViService,
         IDmChuKyService dmChuKyService,
         DmChuKyDialogViewModel dmChuKyDialogViewModel,
         INsMucLucNganSachService iNsMucLucNganSachService,
         ISktMucLucService iSktMucLucService,
         NsBaoCaoGhiChuDialogViewModel nsBaoCaoGhiChuDialogViewModel,
         INsBaoCaoGhiChuService ghiChuService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _sktSoLieuService = sktSoLieuService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _nsDonViService = nsDonViService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _iSktMucLucService = iSktMucLucService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            NsBaoCaoGhiChuDialogViewModel = nsBaoCaoGhiChuDialogViewModel;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;

            NoteCommand = new RelayCommand(obj => OnNoteCommand());
            PrintExcelCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            PrintCommand = new RelayCommand(o => OnExport(ExportType.PDF));
            PrintBrowserCommand = new RelayCommand(o => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            _ghiChuService = ghiChuService;
        }

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                InTheoTongHop = false;
                ReportDate = DateTime.Now;
                LoadBudgetSourceTypes();
                LoadCombobox();
                LoadTypeChuKy();
                LoadChiTietToi();
                LoadDonVi();
                DanhMuc danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                if (!ListDonVi.Where(item => item.IsChecked).Any())
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                switch (SelectedLoaiBaoCao.ValueItem)
                {
                    case Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI:
                        if (IsInMotTo)
                        {
                            if (exportType == ExportType.EXCEL)
                                PrintUnitDetailReport(ExportType.EXCEL_ONE_PAPER);
                            else
                                PrintUnitDetailReport(ExportType.PDF_ONE_PAPER);
                        }
                        else
                            PrintUnitDetailReport(exportType);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI:
                        if (IsInMotTo)
                        {
                            if (exportType == ExportType.EXCEL)
                                PrintUnitSummarylReport(ExportType.EXCEL_ONE_PAPER);
                            else
                                PrintUnitSummarylReport(ExportType.PDF_ONE_PAPER);
                        }
                        else
                            PrintUnitSummarylReport(exportType);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void PrintUnitDetailReport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    List<CheckBoxItem> listIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();
                    int donViTinh = int.Parse(_selectedDonViTinh.ValueItem);
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    int maNguonNS = _sessionService.Current.Budget;
                    string h1 = $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}";
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, yearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    foreach (CheckBoxItem dv in listIdDonVi)
                    {
                        List<ReportBudgetEstimateQuery> listData = _sktSoLieuService.ExportSoSanhSKTDTDN(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, dv.ValueItem, int.Parse(SelectedLoaiNganSach.ValueItem), false, donViTinh).ToList();
                        listData.ForEach(x =>
                        {
                            if (x.IIDMLSKT != null)
                            {
                                x.MergeRange = "A9:M9";
                            }
                        });
                        List<NsMucLucNganSach> nsMucLucs = FindListParentMucLucByChild(listData.Where(x => x.IIDMLNS != null).ToList());
                        foreach (NsMucLucNganSach mlc in nsMucLucs)
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IsHangCha = mlc.BHangCha;
                            mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIDMLNS = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            mlCha.STNG = mlc.Tng;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            listData.Add(mlCha);
                        }
                        List<string> lstIdMucLucSKT = listData.Select(x => x.SKyHieu).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs = FindListParentMucLucSKTByChild(lstIdMucLucSKT);
                        foreach (NsSktMucLuc mlc in sktMucLucs)
                        {
                            if (!listData.Any(x => x.SKyHieu == mlc.SKyHieu && string.IsNullOrEmpty(x.SXauNoiMa)))
                            {
                                ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                                mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                                mlCha.IIDMLSKT = mlc.IIDMLSKT;
                                mlCha.SKyHieu = mlc.SKyHieu;
                                mlCha.SMoTa = mlc.SMoTa;

                                // set L bằng MoTa :))))
                                mlCha.SLNS = $"{mlc.SSTT} {mlc.SMoTa}";
                                mlCha.SK = mlc.SK;
                                mlCha.MergeRange = "A9:M9";
                                listData.Add(mlCha);
                            }
                        }

                        UpdateKyHieuCha(listData);
                        listData.Where(x => !string.IsNullOrEmpty(x.SXauNoiMa)).OrderByDescending(x => x.SXauNoiMa).ToList().ForEach(x =>
                        {
                            ReportBudgetEstimateQuery parent = listData.FirstOrDefault(y => y.IIDMLNS == x.IIDMLNSCha);
                            if (parent is object && !string.IsNullOrEmpty(x.SM))
                            {
                                if (!string.IsNullOrEmpty(parent.STNG))
                                    x.STNG = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STTM))
                                    x.STTM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STM))
                                    x.STM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SM))
                                    x.SM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SK))
                                    x.SK = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SL))
                                    x.SL = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SLNS))
                                    x.SLNS = string.Empty;
                            }
                        });


                        listData = listData.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();
                        listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);


                        if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                        {
                            listData = listData.Where(x => string.IsNullOrEmpty(x.STNG)
                                && string.IsNullOrEmpty(x.STNG1)
                                && string.IsNullOrEmpty(x.STNG2)
                                && string.IsNullOrEmpty(x.STNG3)).ToList();
                            listData.Where(x => !string.IsNullOrEmpty(x.SNG)).Select(x => x.IsHangCha = false).ToList();
                        }

                        //CalculateData(listData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Cap1", GetDonViBanHanh(1, _dmChuKy.LoaiDVBanHanh1, itemDanhMuc, dv.NameItem));
                        data.Add("Cap2", GetDonViBanHanh(2, _dmChuKy.LoaiDVBanHanh2, itemDanhMuc, dv.NameItem));
                        data.Add("DonViIn", dv.NameItem);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("h2", "");
                        data.Add("Year", yearOfWork);
                        data.Add("DonViTinh", h1);
                        data.Add("IsShowDonViIn", IsShowDonViIn);
                        AddChuKy(data, _typeChuKy);
                        List<GhiChu> ghiChu = GetGhiChu();
                        data.Add("HasGhiChu", ghiChu.Any());
                        data.Add("ListGhiChu", ghiChu);
                        if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                            data.Add("sttNoiDung", "G");
                        else
                            data.Add("sttNoiDung", "H");
                        string fileNamePrefix;
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DOICHIEU_SKT_DTDN));
                        fileNamePrefix = dv.ValueItem + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        string chiTietToi = _selectedInToiMuc != null ? ConvertChiTietToi(_selectedInToiMuc.ValueItem) : "Ngành";
                        List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                        if (hideColumns.Any())
                        {
                            for (int i = 0; i < hideColumns.Count; i++)
                            {
                                hideColumns[i] += 1;
                            }
                        }
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu>(templateFileName, data, hideColumns);
                        results.Add(new ExportResult(dv.ValueItem, fileNameWithoutExtension, null, xlsFile));

                    }
                    e.Result = results;
                }, (s, e) =>
                        {
                            if (e.Error == null)
                            {
                                List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void PrintUnitSummarylReport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<string> listIdDonVi = ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
                    int donViTinh = int.Parse(_selectedDonViTinh.ValueItem);
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    int maNguonNS = _sessionService.Current.Budget;
                    string h1 = $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}";

                    string sDonVi = string.Join(",", listIdDonVi);
                    List<ReportBudgetEstimateQuery> listData = _sktSoLieuService.ExportSoSanhSKTDTDN(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, sDonVi, int.Parse(SelectedLoaiNganSach.ValueItem), InTheoTongHop, donViTinh).ToList();
                    listData.ForEach(x =>
                    {
                        if (x.IIDMLSKT != null)
                        {
                            x.MergeRange = "A9:M9";
                            x.SLNS = x.SMoTa;
                        }
                    });
                    List<Guid> lstIdMucLuc = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                    List<NsMucLucNganSach> nsMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                    nsMucLucs.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);

                    foreach (NsMucLucNganSach mlc in nsMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.MlnsId))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IsHangCha = mlc.BHangCha;
                            mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIDMLNS = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            mlCha.STNG = mlc.Tng;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            listData.Add(mlCha);
                        }
                    }
                    List<string> lstIdMucLucSKT = listData.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucSKTByChild(lstIdMucLucSKT);
                    foreach (NsSktMucLuc mlc in sktMucLucs)
                    {
                        if (!listData.Any(x => x.SKyHieu == mlc.SKyHieu && string.IsNullOrEmpty(x.SXauNoiMa)))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                            mlCha.IIDMLSKT = mlc.IIDMLSKT;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            mlCha.SL = mlc.SL;
                            mlCha.SLNS = $"{mlc.SSTT} {mlc.SMoTa}";
                            mlCha.SK = mlc.SK;
                            mlCha.MergeRange = "A9:M9";
                            listData.Add(mlCha);
                        }
                    }

                    UpdateKyHieuCha(listData);

                    listData.Where(x => !string.IsNullOrEmpty(x.SXauNoiMa)).OrderByDescending(x => x.SXauNoiMa).ToList().ForEach(x =>
                    {
                        ReportBudgetEstimateQuery parent = listData.FirstOrDefault(y => y.IIDMLNS == x.IIDMLNSCha);
                        if (parent is object && !string.IsNullOrEmpty(x.SM))
                        {
                            if (!string.IsNullOrEmpty(parent.STNG))
                                x.STNG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STTM))
                                x.STTM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STM))
                                x.STM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SM))
                                x.SM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SK))
                                x.SK = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SL))
                                x.SL = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SLNS))
                                x.SLNS = string.Empty;
                        }
                    });

                    listData = listData.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();
                    listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);

                    if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                    {
                        listData = listData.Where(x => string.IsNullOrEmpty(x.STNG)
                            && string.IsNullOrEmpty(x.STNG1)
                            && string.IsNullOrEmpty(x.STNG2)
                            && string.IsNullOrEmpty(x.STNG3)).ToList();
                        listData.Where(x => !string.IsNullOrEmpty(x.SNG)).Select(x => x.IsHangCha = false).ToList();
                    }

                    //CalculateData(listData);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonViIn", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("Year", yearOfWork);
                    data.Add("DonViTinh", h1);
                    data.Add("IsShowDonViIn", IsShowDonViIn ? "1" : "");
                    AddChuKy(data, _typeChuKy);
                    List<GhiChu> ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);
                    if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                        data.Add("sttNoiDung", "G");
                    else
                        data.Add("sttNoiDung", "H");
                    string fileNamePrefix;
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DOICHIEU_SKT_DTDN));
                    fileNamePrefix = _sessionInfo.IdDonVi + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    string chiTietToi = _selectedInToiMuc != null ? ConvertChiTietToi(_selectedInToiMuc.ValueItem) : "Ngành";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                    if (hideColumns.Any())
                    {
                        for (int i = 0; i < hideColumns.Count; i++)
                        {
                            hideColumns[i] += 1;
                        }
                    }
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu>(templateFileName, data, hideColumns);
                    results.Add(new ExportResult(_sessionInfo.IdDonVi, fileNameWithoutExtension, null, xlsFile));


                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void PrintHorizontalUnitlReport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    string fileNamePrefix;
                    int donViTinh = int.Parse(_selectedDonViTinh.ValueItem);
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    int maNguonNS = _sessionService.Current.Budget;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    IEnumerable<string> donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                    List<string> listIdDonVi = ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).Select(x => x.ValueItem).Distinct().ToList();
                    string h1 = $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}";
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    string sDonVi = string.Join(",", listIdDonVi);
                    List<ReportBudgetEstimateQuery> listData = _sktSoLieuService.ExportDuToanNganSachDonViNgang(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, sDonVi, donViTinh).ToList();

                    //page1
                    string tenDonVi1 = string.Empty;
                    string tenDonVi2 = string.Empty;
                    string tenDonVi3 = string.Empty;
                    string tenDonVi4 = string.Empty;
                    string tenDonVi5 = string.Empty;
                    string tenDonVi6 = string.Empty;
                    for (int i = 0; i < listIdDonVi.Count; i++)
                    {
                        if (i > 4) break;
                        switch (i)
                        {
                            case 0:
                                tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 1:
                                tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 2:
                                tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 3:
                                tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 4:
                                tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            default:
                                break;
                        }

                        List<ReportBudgetEstimateQuery> dataDonvi1 = new List<ReportBudgetEstimateQuery>();
                        dataDonvi1 = _sktSoLieuService.ExportDuToanNganSachDonViNgang(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, listIdDonVi[i], donViTinh).ToList();
                        foreach (ReportBudgetEstimateQuery item in dataDonvi1)
                        {
                            switch (i)
                            {
                                case 0:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.TuChiDV1 = item.FTuChi;
                                        return n;
                                    }).ToList();
                                    break;
                                case 1:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.TuChiDV2 = item.FTuChi;
                                        return n;
                                    }).ToList();
                                    break;
                                case 2:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.TuChiDV3 = item.FTuChi;
                                        return n;
                                    }).ToList();
                                    break;
                                case 3:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.TuChiDV4 = item.FTuChi;
                                        return n;
                                    }).ToList();
                                    break;
                                case 4:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.TuChiDV5 = item.FTuChi;
                                        return n;
                                    }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    List<Guid> lstIdMucLuc = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                    List<NsMucLucNganSach> nsMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                    nsMucLucs.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);

                    foreach (NsMucLucNganSach mlc in nsMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.MlnsId))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IsHangCha = mlc.BHangCha;
                            mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIDMLNS = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            mlCha.STNG = mlc.Tng;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            listData.Add(mlCha);
                        }
                    }
                    List<string> lstIdMucLucSKT = listData.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucSKTByChild(lstIdMucLucSKT);
                    foreach (NsSktMucLuc mlc in sktMucLucs)
                    {
                        if (!lstIdMucLucSKT.Contains(mlc.SKyHieu))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                            mlCha.IIDMLSKT = mlc.IIDMLSKT;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            listData.Add(mlCha);
                        }
                    }

                    UpdateKyHieuCha(listData);
                    listData = listData.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();
                    listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);

                    if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                    {
                        listData = listData.Where(x => string.IsNullOrEmpty(x.STNG)
                            && string.IsNullOrEmpty(x.STNG1)
                            && string.IsNullOrEmpty(x.STNG2)
                            && string.IsNullOrEmpty(x.STNG3)).ToList();
                        listData.Where(x => !string.IsNullOrEmpty(x.SNG)).Select(x => x.IsHangCha = false).ToList();
                    }

                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonViIn", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("TenDV1", tenDonVi1);
                    data.Add("TenDV2", tenDonVi2);
                    data.Add("TenDV3", tenDonVi3);
                    data.Add("TenDV4", tenDonVi4);
                    data.Add("TenDV5", tenDonVi5);
                    data.Add("TenDV6", tenDonVi6);
                    data.Add("Year", yearOfWork);
                    data.Add("DonViTinh", h1);
                    data.Add("IsShowDonViIn", IsShowDonViIn == true ? "1" : "");
                    AddChuKy(data, _typeChuKy);
                    List<GhiChu> ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);
                    if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                        data.Add("sttNoiDung", "G");
                    else
                        data.Add("sttNoiDung", "H");
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_TO1));
                    fileNamePrefix = _sessionInfo.IdDonVi + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    string chiTietToi = _selectedInToiMuc != null ? ConvertChiTietToi(_selectedInToiMuc.ValueItem) : "Ngành";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                    if (hideColumns.Any())
                    {
                        for (int i = 0; i < hideColumns.Count; i++)
                        {
                            hideColumns[i] += 1;
                        }
                    }
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu>(templateFileName, data, hideColumns);
                    results.Add(new ExportResult(_sessionInfo.IdDonVi, fileNameWithoutExtension, null, xlsFile));

                    //page 2
                    int numberPage = 0;

                    numberPage = (listIdDonVi.Count() - 3) / 5;
                    if ((listIdDonVi.Count() - 3) % 5 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        tenDonVi1 = string.Empty;
                        tenDonVi2 = string.Empty;
                        tenDonVi3 = string.Empty;
                        tenDonVi4 = string.Empty;
                        tenDonVi5 = string.Empty;
                        listData = _sktSoLieuService.ExportDuToanNganSachDonViNgang(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, sDonVi, donViTinh).ToList();

                        int sttPage = p - 2;
                        int countDonVi = 0;
                        for (int i = sttPage * 5 + 3; i <= sttPage * 5 + 5 + 5; i++)
                        {
                            if (i >= listIdDonVi.Count()) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 1:
                                    tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 2:
                                    tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 3:
                                    tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 4:
                                    tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportBudgetEstimateQuery> dataDonvi1 = new List<ReportBudgetEstimateQuery>();
                            dataDonvi1 = _sktSoLieuService.ExportDuToanNganSachDonViNgang(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, listIdDonVi[i], donViTinh).ToList();
                            foreach (ReportBudgetEstimateQuery item in dataDonvi1)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.TuChiDV1 = item.FTuChi;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 1:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.TuChiDV2 = item.FTuChi;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 2:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.TuChiDV3 = item.FTuChi;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 3:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.TuChiDV4 = item.FTuChi;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 4:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.TuChiDV5 = item.FTuChi;
                                            return n;
                                        }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            countDonVi++;
                        }

                        List<Guid> lstIdMucLuc2 = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                        List<NsMucLucNganSach> nsMucLucs2 = FindListParentMucLucByChild(lstIdMucLuc2);
                        nsMucLucs2.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);

                        foreach (NsMucLucNganSach mlc in nsMucLucs2)
                        {
                            if (!lstIdMucLuc.Contains(mlc.MlnsId))
                            {
                                ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                                mlCha.IsHangCha = mlc.BHangCha;
                                mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                                mlCha.IIDMLNS = mlc.MlnsId;
                                mlCha.SLNS = mlc.Lns;
                                mlCha.SL = mlc.L;
                                mlCha.SK = mlc.K;
                                mlCha.SM = mlc.M;
                                mlCha.STM = mlc.Tm;
                                mlCha.STTM = mlc.Ttm;
                                mlCha.SNG = mlc.Ng;
                                mlCha.STNG = mlc.Tng;
                                mlCha.SXauNoiMa = mlc.XauNoiMa;
                                mlCha.SMoTa = mlc.MoTa;
                                listData.Add(mlCha);
                            }
                        }

                        List<string> lstIdMucLucSKT2 = listData.Select(x => x.SKyHieu).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs2 = FindListParentMucLucSKTByChild(lstIdMucLucSKT2);
                        foreach (NsSktMucLuc mlc in sktMucLucs)
                        {
                            if (!lstIdMucLucSKT2.Contains(mlc.SKyHieu))
                            {
                                ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                                mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                                mlCha.IIDMLSKT = mlc.IIDMLSKT;
                                mlCha.SKyHieu = mlc.SKyHieu;
                                mlCha.SMoTa = mlc.SMoTa;
                                mlCha.SL = mlc.SL;
                                mlCha.SK = mlc.SK;
                                listData.Add(mlCha);
                            }
                        }

                        UpdateKyHieuCha(listData);
                        listData = listData.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();
                        listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);

                        data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                        data.Add("Cap2", _sessionInfo.TenDonVi);
                        data.Add("DonViIn", _sessionInfo.TenDonVi);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("h2", "");
                        data.Add("TenDV1", tenDonVi1);
                        data.Add("TenDV2", tenDonVi2);
                        data.Add("TenDV3", tenDonVi3);
                        data.Add("TenDV4", tenDonVi4);
                        data.Add("TenDV5", tenDonVi5);
                        data.Add("TenDV6", tenDonVi6);
                        data.Add("Year", yearOfWork);
                        data.Add("DonViTinh", h1);
                        data.Add("IsShowDonViIn", IsShowDonViIn == true ? "1" : "");
                        AddChuKy(data, _typeChuKy);
                        data.Add("HasGhiChu", ghiChu.Any());
                        data.Add("ListGhiChu", ghiChu);
                        if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                            data.Add("sttNoiDung", "G");
                        else
                            data.Add("sttNoiDung", "H");

                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_TO));
                        fileNamePrefix = _sessionInfo.IdDonVi + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu>(templateFileName, data, hideColumns);
                        results.Add(new ExportResult(_sessionInfo.IdDonVi, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void PrintHorizontalUnitlReportOnePaper(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    string fileNamePrefix;
                    int donViTinh = int.Parse(_selectedDonViTinh.ValueItem);
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    int maNguonNS = _sessionService.Current.Budget;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    IEnumerable<string> donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                    List<CheckBoxItem> listIdDonVi = ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).ToList();
                    string h1 = $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}";
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    // create Header agencies
                    List<HeaderReportDynamic> ListHeader1 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeaderPage1 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeader2 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeader3 = new List<HeaderReportDynamic>();
                    List<DataReportDynamic> ListDataDefault = new List<DataReportDynamic>();
                    int columnStart = 10;
                    int columnStartPage1 = 9;
                    string ColNameStart = GetExcelColumnName(columnStart);
                    string ColNameStartPage1 = GetExcelColumnName(columnStartPage1);
                    int columnEnd = columnStart + (listIdDonVi.Count > 1 ? listIdDonVi.Count - 1 : 0);
                    int columnEndPage1 = columnStartPage1 + (listIdDonVi.Count > 1 ? listIdDonVi.Count - 1 : 0);
                    string ColNameEnd = GetExcelColumnName(columnEnd);
                    string ColNameEndPage1 = GetExcelColumnName(columnEndPage1);
                    string mergeRange = string.Format("{0}9:{1}9", ColNameStart, ColNameEnd);
                    string mergeRangePage1 = string.Format("{0}9:{1}9", ColNameStartPage1, ColNameEndPage1);
                    string sDonVi = string.Join(",", listIdDonVi.Select(x => x.ValueItem));

                    int cl = 3;
                    foreach (var item in listIdDonVi.Select((value, index) => new { index, value }))
                    {
                        cl++;
                        if (item.index == NSConstants.ZERO)
                        {
                            ListHeader1.Add(new HeaderReportDynamic() { Header = "Dự toán ngân sách năm " + yearOfWork, Stt = 1, MergeRange = mergeRange });
                            ListHeaderPage1.Add(new HeaderReportDynamic() { Header = "Dự toán ngân sách năm " + yearOfWork, Stt = 1, MergeRange = mergeRangePage1 });
                            ListHeader2.Add(new HeaderReportDynamic() { Header = item.value.DisplayItem, Stt = 1, MergeRange = mergeRange });
                            ListHeader3.Add(new HeaderReportDynamic() { Header = cl.ToString(), Stt = 1, MergeRange = mergeRange });
                            ListDataDefault.Add(new DataReportDynamic());
                        }
                        else
                        {
                            ListHeader1.Add(new HeaderReportDynamic());
                            ListHeaderPage1.Add(new HeaderReportDynamic());
                            ListHeader2.Add(new HeaderReportDynamic() { Header = item.value.DisplayItem });
                            ListHeader3.Add(new HeaderReportDynamic() { Header = cl.ToString() });
                            ListDataDefault.Add(new DataReportDynamic());
                        }
                    }
                    _lstDataDynamic = ListDataDefault;
                    List<ReportBudgetEstimateQuery> listData = _sktSoLieuService.ExportDuToanNganSachDonViNgangExcel(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, sDonVi, donViTinh).ToList();
                    List<Guid> lstIdMucLuc = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                    List<NsMucLucNganSach> nsMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                    nsMucLucs.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);

                    foreach (NsMucLucNganSach mlc in nsMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.MlnsId))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IsHangCha = mlc.BHangCha;
                            mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIDMLNS = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            mlCha.STNG = mlc.Tng;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            listData.Add(mlCha);
                        }
                    }

                    List<string> lstIdMucLucSKT = listData.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucSKTByChild(lstIdMucLucSKT);
                    foreach (NsSktMucLuc mlc in sktMucLucs)
                    {
                        if (!lstIdMucLucSKT.Contains(mlc.SKyHieu))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                            mlCha.IIDMLSKT = mlc.IIDMLSKT;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            listData.Add(mlCha);
                        }
                    }

                    UpdateKyHieuCha(listData);
                    listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);

                    List<ReportBudgetEstimateQuery> listDataGroups = listData.GroupBy(g => new { g.IIDMLNS, g.IIDMLNSCha, g.IIDMLSKT, g.IIDMLSKTCha, g.SMoTa, g.SXauNoiMa, g.SKyHieu }).Select(x => new ReportBudgetEstimateQuery
                    {
                        IIDMLNS = x.FirstOrDefault().IIDMLNS,
                        IIDMLNSCha = x.FirstOrDefault().IIDMLNSCha,
                        IIDMLSKT = x.FirstOrDefault().IIDMLSKT,
                        IIDMLSKTCha = x.FirstOrDefault().IIDMLSKTCha,
                        SXauNoiMa = x.FirstOrDefault().SXauNoiMa,
                        SKyHieu = x.FirstOrDefault().SKyHieu,
                        SLNS = x.FirstOrDefault().SLNS,
                        SL = x.FirstOrDefault().SL,
                        SK = x.FirstOrDefault().SK,
                        SM = x.FirstOrDefault().SM,
                        STM = x.FirstOrDefault().STM,
                        STTM = x.FirstOrDefault().STTM,
                        SNG = x.FirstOrDefault().SNG,
                        STNG = x.FirstOrDefault().STNG,
                        STNG1 = x.FirstOrDefault().STNG1,
                        STNG2 = x.FirstOrDefault().STNG2,
                        STNG3 = x.FirstOrDefault().STNG3,
                        SMoTa = x.FirstOrDefault().SMoTa,
                        IsHangCha = x.FirstOrDefault().IsHangCha,
                        FTuChiBanThan = x.FirstOrDefault().FTuChiBanThan,
                        TongTuChi = x.Sum(x => x.TongTuChi),
                        FMucTienPhanBo = x.Sum(x => x.FMucTienPhanBo),
                        ListDataValue = GetDataDefault()
                    }).ToList();

                    CalculateDataExcel(listData, listDataGroups, listIdDonVi);
                    _lstDataDynamicSummary = GetDataDefault();
                    for (int i = 0; i < _lstDataDynamicSummary.Count; i++)
                    {
                        _lstDataDynamicSummary[i].FVal = listDataGroups.Where(x => !x.IsHangCha).Sum(y => y.ListDataValue[i].FVal);
                    }

                    listDataGroups = listDataGroups.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();

                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listDataGroups);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonViIn", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("Year", yearOfWork);
                    data.Add("DonViTinh", h1);
                    data.Add("IsShowDonViIn", IsShowDonViIn == true ? "1" : "");
                    data.Add("ListHeader1", ListHeaderPage1);
                    data.Add("ListHeader2", ListHeader2);
                    data.Add("ListHeader3", ListHeader3);
                    data.Add("ListDataSummary", _lstDataDynamicSummary);
                    AddChuKy(data, _typeChuKy);
                    List<GhiChu> ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);
                    if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                        data.Add("sttNoiDung", "G");
                    else
                        data.Add("sttNoiDung", "H");

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_EXCEL));
                    fileNamePrefix = _sessionInfo.IdDonVi + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    string chiTietToi = _selectedInToiMuc != null ? ConvertChiTietToi(_selectedInToiMuc.ValueItem) : "Ngành";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                    if (hideColumns.Any())
                    {
                        for (int i = 0; i < hideColumns.Count; i++)
                        {
                            hideColumns[i] += 1;
                        }
                    }
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu, HeaderReportDynamic, DataReportDynamic>(templateFileName, data, hideColumns);
                    results.Add(new ExportResult(_sessionInfo.IdDonVi, fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void PrintUnitDetailReportNSDTN(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<CheckBoxItem> listIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();
                    int donViTinh = int.Parse(_selectedDonViTinh.ValueItem);
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    int maNguonNS = _sessionService.Current.Budget;
                    string h1 = $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}";

                    foreach (CheckBoxItem dv in listIdDonVi)
                    {
                        List<ReportBudgetEstimateQuery> listData = _sktSoLieuService.ExportDuToanNganSachNSDTN(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, dv.ValueItem, donViTinh).ToList();
                        List<Guid> lstIdMucLuc = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                        List<NsMucLucNganSach> nsMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                        nsMucLucs.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);
                        foreach (NsMucLucNganSach mlc in nsMucLucs)
                        {
                            if (!lstIdMucLuc.Contains(mlc.MlnsId))
                            {
                                ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                                mlCha.IsHangCha = mlc.BHangCha;
                                mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                                mlCha.IIDMLNS = mlc.MlnsId;
                                mlCha.SLNS = mlc.Lns;
                                mlCha.SL = mlc.L;
                                mlCha.SK = mlc.K;
                                mlCha.SM = mlc.M;
                                mlCha.STM = mlc.Tm;
                                mlCha.STTM = mlc.Ttm;
                                mlCha.SNG = mlc.Ng;
                                mlCha.STNG = mlc.Tng;
                                mlCha.SXauNoiMa = mlc.XauNoiMa;
                                mlCha.SMoTa = mlc.MoTa;
                                listData.Add(mlCha);
                            }
                        }

                        List<string> lstIdMucLucSKT = listData.Select(x => x.SKyHieu).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs = FindListParentMucLucSKTByChild(lstIdMucLucSKT);
                        foreach (NsSktMucLuc mlc in sktMucLucs)
                        {
                            if (!lstIdMucLucSKT.Contains(mlc.SKyHieu))
                            {
                                ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                                mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                                mlCha.IIDMLSKT = mlc.IIDMLSKT;
                                mlCha.SKyHieu = mlc.SKyHieu;
                                mlCha.SMoTa = mlc.SMoTa;
                                mlCha.SL = mlc.SL;
                                mlCha.SK = mlc.SK;
                                listData.Add(mlCha);
                            }
                        }

                        UpdateKyHieuCha(listData);
                        listData = listData.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();
                        listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);
                        //CalculateData(listData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Cap1", GetDonViBanHanh(1, _dmChuKy.LoaiDVBanHanh1, itemDanhMuc, dv.NameItem));
                        data.Add("Cap2", GetDonViBanHanh(2, _dmChuKy.LoaiDVBanHanh2, itemDanhMuc, dv.NameItem));
                        data.Add("DonViIn", dv.NameItem);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("h2", "");
                        data.Add("Year", yearOfWork);
                        data.Add("DonViTinh", h1);
                        data.Add("IsShowDonViIn", IsShowDonViIn);
                        AddChuKy(data, _typeChuKy);
                        List<GhiChu> ghiChu = GetGhiChu();
                        data.Add("HasGhiChu", ghiChu.Any());
                        data.Add("ListGhiChu", ghiChu);
                        if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                            data.Add("sttNoiDung", "G");
                        else
                            data.Add("sttNoiDung", "H");
                        string fileNamePrefix;
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVI_NSDTN));
                        fileNamePrefix = dv.ValueItem + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        string chiTietToi = _selectedInToiMuc != null ? ConvertChiTietToi(_selectedInToiMuc.ValueItem) : "Ngành";
                        List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                        if (hideColumns.Any())
                        {
                            for (int i = 0; i < hideColumns.Count; i++)
                            {
                                hideColumns[i] += 1;
                            }
                        }
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu>(templateFileName, data, hideColumns);
                        results.Add(new ExportResult(dv.ValueItem, fileNameWithoutExtension, null, xlsFile));

                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void PrintUnitSummarylReportNSDTN(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<string> listIdDonVi = ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem).Distinct().ToList();
                    int donViTinh = int.Parse(_selectedDonViTinh.ValueItem);
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    int maNguonNS = _sessionService.Current.Budget;
                    string h1 = $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}";

                    string sDonVi = string.Join(",", listIdDonVi);
                    List<ReportBudgetEstimateQuery> listData = _sktSoLieuService.ExportDuToanNganSachNSDTN(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, sDonVi, donViTinh).ToList();
                    List<Guid> lstIdMucLuc = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                    List<NsMucLucNganSach> nsMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                    nsMucLucs.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);

                    foreach (NsMucLucNganSach mlc in nsMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.MlnsId))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IsHangCha = mlc.BHangCha;
                            mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIDMLNS = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            mlCha.STNG = mlc.Tng;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            listData.Add(mlCha);
                        }
                    }
                    List<string> lstIdMucLucSKT = listData.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucSKTByChild(lstIdMucLucSKT);
                    foreach (NsSktMucLuc mlc in sktMucLucs)
                    {
                        if (!lstIdMucLucSKT.Contains(mlc.SKyHieu))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                            mlCha.IIDMLSKT = mlc.IIDMLSKT;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            listData.Add(mlCha);
                        }
                    }

                    UpdateKyHieuCha(listData);
                    listData = listData.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();
                    listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);
                    //CalculateData(listData);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonViIn", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("Year", yearOfWork);
                    data.Add("DonViTinh", h1);
                    data.Add("IsShowDonViIn", IsShowDonViIn == true ? "1" : "");
                    AddChuKy(data, _typeChuKy);
                    List<GhiChu> ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);
                    if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                        data.Add("sttNoiDung", "G");
                    else
                        data.Add("sttNoiDung", "H");
                    string fileNamePrefix;
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVI_NSDTN));
                    fileNamePrefix = _sessionInfo.IdDonVi + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    string chiTietToi = _selectedInToiMuc != null ? ConvertChiTietToi(_selectedInToiMuc.ValueItem) : "Ngành";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                    if (hideColumns.Any())
                    {
                        for (int i = 0; i < hideColumns.Count; i++)
                        {
                            hideColumns[i] += 1;
                        }
                    }
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu>(templateFileName, data, hideColumns);
                    results.Add(new ExportResult(_sessionInfo.IdDonVi, fileNameWithoutExtension, null, xlsFile));


                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void PrintHorizontalUnitlReportNSDTN(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    string fileNamePrefix;
                    int donViTinh = int.Parse(_selectedDonViTinh.ValueItem);
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    int maNguonNS = _sessionService.Current.Budget;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    IEnumerable<string> donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                    List<string> listIdDonVi = ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).Select(x => x.ValueItem).Distinct().ToList();
                    string h1 = $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}";
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    string sDonVi = string.Join(",", listIdDonVi);
                    List<ReportBudgetEstimateQuery> listData = _sktSoLieuService.ExportDuToanNganSachDonViNgangNSDTN(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, sDonVi, donViTinh).ToList();

                    //page1
                    string tenDonVi1 = string.Empty;
                    string tenDonVi2 = string.Empty;
                    string tenDonVi3 = string.Empty;
                    string tenDonVi4 = string.Empty;
                    string tenDonVi5 = string.Empty;
                    string tenDonVi6 = string.Empty;
                    for (int i = 0; i < listIdDonVi.Count; i++)
                    {
                        if (i > 4) break;
                        switch (i)
                        {
                            case 0:
                                tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 1:
                                tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 2:
                                tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 3:
                                tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 4:
                                tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            default:
                                break;
                        }

                        List<ReportBudgetEstimateQuery> dataDonvi1 = new List<ReportBudgetEstimateQuery>();
                        dataDonvi1 = _sktSoLieuService.ExportDuToanNganSachDonViNgangNSDTN(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, listIdDonVi[i], donViTinh).ToList();
                        foreach (ReportBudgetEstimateQuery item in dataDonvi1)
                        {
                            switch (i)
                            {
                                case 0:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.HangNhapDV1 = item.FHangNhap;
                                        n.HangMuaDV1 = item.FHangMua;
                                        return n;
                                    }).ToList();
                                    break;
                                case 1:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.HangNhapDV2 = item.FHangNhap;
                                        n.HangMuaDV2 = item.FHangMua;
                                        return n;
                                    }).ToList();
                                    break;
                                case 2:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.HangNhapDV3 = item.FHangNhap;
                                        n.HangMuaDV3 = item.FHangMua;
                                        return n;
                                    }).ToList();
                                    break;
                                case 3:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.HangNhapDV4 = item.FHangNhap;
                                        n.HangMuaDV4 = item.FHangMua;
                                        return n;
                                    }).ToList();
                                    break;
                                case 4:
                                    listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.HangNhapDV5 = item.FHangNhap;
                                        n.HangMuaDV5 = item.FHangMua;
                                        return n;
                                    }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    List<Guid> lstIdMucLuc = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                    List<NsMucLucNganSach> nsMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                    nsMucLucs.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);

                    foreach (NsMucLucNganSach mlc in nsMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.MlnsId))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IsHangCha = mlc.BHangCha;
                            mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIDMLNS = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            mlCha.STNG = mlc.Tng;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            listData.Add(mlCha);
                        }
                    }
                    List<string> lstIdMucLucSKT = listData.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucSKTByChild(lstIdMucLucSKT);
                    foreach (NsSktMucLuc mlc in sktMucLucs)
                    {
                        if (!lstIdMucLucSKT.Contains(mlc.SKyHieu))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                            mlCha.IIDMLSKT = mlc.IIDMLSKT;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            listData.Add(mlCha);
                        }
                    }

                    UpdateKyHieuCha(listData);
                    listData = listData.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();
                    listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);

                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonViIn", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("TenDV1", tenDonVi1);
                    data.Add("TenDV2", tenDonVi2);
                    data.Add("TenDV3", tenDonVi3);
                    data.Add("TenDV4", tenDonVi4);
                    data.Add("TenDV5", tenDonVi5);
                    data.Add("TenDV6", tenDonVi6);
                    data.Add("Year", yearOfWork);
                    data.Add("DonViTinh", h1);
                    data.Add("IsShowDonViIn", IsShowDonViIn == true ? "1" : "");
                    AddChuKy(data, _typeChuKy);
                    List<GhiChu> ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);
                    if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                        data.Add("sttNoiDung", "G");
                    else
                        data.Add("sttNoiDung", "H");
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_NSDTN_TO1));
                    fileNamePrefix = _sessionInfo.IdDonVi + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    string chiTietToi = _selectedInToiMuc != null ? ConvertChiTietToi(_selectedInToiMuc.ValueItem) : "Ngành";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                    if (hideColumns.Any())
                    {
                        for (int i = 0; i < hideColumns.Count; i++)
                        {
                            hideColumns[i] += 1;
                        }
                    }
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu>(templateFileName, data, hideColumns);
                    results.Add(new ExportResult(_sessionInfo.IdDonVi, fileNameWithoutExtension, null, xlsFile));

                    //page 2
                    int numberPage = 0;

                    numberPage = (listIdDonVi.Count() - 3) / 5;
                    if ((listIdDonVi.Count() - 3) % 5 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        tenDonVi1 = string.Empty;
                        tenDonVi2 = string.Empty;
                        tenDonVi3 = string.Empty;
                        tenDonVi4 = string.Empty;
                        tenDonVi5 = string.Empty;
                        listData = _sktSoLieuService.ExportDuToanNganSachDonViNgangNSDTN(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, sDonVi, donViTinh).ToList();

                        int sttPage = p - 2;
                        int countDonVi = 0;
                        for (int i = sttPage * 5 + 3; i <= sttPage * 5 + 5 + 5; i++)
                        {
                            if (i >= listIdDonVi.Count()) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 1:
                                    tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 2:
                                    tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 3:
                                    tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 4:
                                    tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportBudgetEstimateQuery> dataDonvi1 = new List<ReportBudgetEstimateQuery>();
                            dataDonvi1 = _sktSoLieuService.ExportDuToanNganSachDonViNgangNSDTN(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, listIdDonVi[i], donViTinh).ToList();
                            foreach (ReportBudgetEstimateQuery item in dataDonvi1)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.HangNhapDV1 = item.FHangNhap;
                                            n.HangMuaDV1 = item.FHangMua;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 1:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.HangNhapDV2 = item.FHangNhap;
                                            n.HangMuaDV2 = item.FHangMua;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 2:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.HangNhapDV3 = item.FHangNhap;
                                            n.HangMuaDV3 = item.FHangMua;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 3:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.HangNhapDV4 = item.FHangNhap;
                                            n.HangMuaDV4 = item.FHangMua;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 4:
                                        listData.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.HangNhapDV5 = item.FHangNhap;
                                            n.HangMuaDV5 = item.FHangMua;
                                            return n;
                                        }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            countDonVi++;
                        }

                        List<Guid> lstIdMucLuc2 = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                        List<NsMucLucNganSach> nsMucLucs2 = FindListParentMucLucByChild(lstIdMucLuc2);
                        nsMucLucs2.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);

                        foreach (NsMucLucNganSach mlc in nsMucLucs2)
                        {
                            if (!lstIdMucLuc.Contains(mlc.MlnsId))
                            {
                                ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                                mlCha.IsHangCha = mlc.BHangCha;
                                mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                                mlCha.IIDMLNS = mlc.MlnsId;
                                mlCha.SLNS = mlc.Lns;
                                mlCha.SL = mlc.L;
                                mlCha.SK = mlc.K;
                                mlCha.SM = mlc.M;
                                mlCha.STM = mlc.Tm;
                                mlCha.STTM = mlc.Ttm;
                                mlCha.SNG = mlc.Ng;
                                mlCha.STNG = mlc.Tng;
                                mlCha.SXauNoiMa = mlc.XauNoiMa;
                                mlCha.SMoTa = mlc.MoTa;
                                listData.Add(mlCha);
                            }
                        }

                        List<string> lstIdMucLucSKT2 = listData.Select(x => x.SKyHieu).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs2 = FindListParentMucLucSKTByChild(lstIdMucLucSKT2);
                        foreach (NsSktMucLuc mlc in sktMucLucs)
                        {
                            if (!lstIdMucLucSKT2.Contains(mlc.SKyHieu))
                            {
                                ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                                mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                                mlCha.IIDMLSKT = mlc.IIDMLSKT;
                                mlCha.SKyHieu = mlc.SKyHieu;
                                mlCha.SMoTa = mlc.SMoTa;
                                mlCha.SL = mlc.SL;
                                mlCha.SK = mlc.SK;
                                listData.Add(mlCha);
                            }
                        }

                        UpdateKyHieuCha(listData);
                        listData = listData.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();
                        listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);

                        data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                        data.Add("Cap2", _sessionInfo.TenDonVi);
                        data.Add("DonViIn", _sessionInfo.TenDonVi);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("h2", "");
                        data.Add("TenDV1", tenDonVi1);
                        data.Add("TenDV2", tenDonVi2);
                        data.Add("TenDV3", tenDonVi3);
                        data.Add("TenDV4", tenDonVi4);
                        data.Add("TenDV5", tenDonVi5);
                        data.Add("TenDV6", tenDonVi6);
                        data.Add("Year", yearOfWork);
                        data.Add("DonViTinh", h1);
                        data.Add("IsShowDonViIn", IsShowDonViIn == true ? "1" : "");
                        AddChuKy(data, _typeChuKy);
                        data.Add("HasGhiChu", ghiChu.Any());
                        data.Add("ListGhiChu", ghiChu);
                        if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                            data.Add("sttNoiDung", "G");
                        else
                            data.Add("sttNoiDung", "H");

                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_NSDTN_TO));
                        fileNamePrefix = _sessionInfo.IdDonVi + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu>(templateFileName, data, hideColumns);
                        results.Add(new ExportResult(_sessionInfo.IdDonVi, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void PrintHorizontalUnitlReportOnePaperNSDTN(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    string fileNamePrefix;
                    int donViTinh = int.Parse(_selectedDonViTinh.ValueItem);
                    int yearOfWork = _sessionInfo.YearOfWork;
                    int yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    int maNguonNS = _sessionService.Current.Budget;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    IEnumerable<string> donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                    List<CheckBoxItem> listIdDonVi = ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).ToList();
                    string h1 = $"Đơn vị tính: {_selectedDonViTinh.DisplayItem}";
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    // create Header agencies
                    List<HeaderReportDynamic> ListHeader1 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeaderPage1 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeader2 = new List<HeaderReportDynamic>();
                    List<DataReportDynamic2> ListDataDefault = new List<DataReportDynamic2>();
                    int columnStart = 19;
                    int columnStartPage1 = 20;
                    string ColNameStart = GetExcelColumnName(columnStart);
                    string ColNameStartPage1 = GetExcelColumnName(columnStartPage1);
                    int columnEnd = columnStart;
                    int columnEndPage1 = listIdDonVi.Count * 2 + 23;
                    string ColNameEnd = GetExcelColumnName(columnEnd);
                    string ColNameEndPage1 = GetExcelColumnName(columnEndPage1);
                    string mergeRange = string.Format("{0}7:{1}7", ColNameStart, ColNameEnd);
                    string mergeRangePage1 = string.Format("{0}7:{1}7", ColNameStartPage1, ColNameEndPage1);
                    string sDonVi = string.Join(",", listIdDonVi.Select(x => x.ValueItem));

                    int startNum = 6;
                    List<SttHeader> lstStt = new List<SttHeader>();

                    for (int i = 0; i < listIdDonVi.Count; i++)
                    {
                        lstStt.Add(new SttHeader() { Stt1 = startNum });
                        startNum += 2;
                    }

                    foreach (var item in listIdDonVi.Select((value, index) => new { index, value }))
                    {

                        if (item.index == NSConstants.ZERO)
                        {
                            ListHeader1.Add(new HeaderReportDynamic() { Header = "Dự toán ngân sách năm " + yearOfWork, Stt = 1 });
                            ListHeaderPage1.Add(new HeaderReportDynamic() { Header = "Dự toán ngân sách năm " + yearOfWork, Stt = 1, MergeRange = mergeRangePage1 });
                            ListHeader2.Add(new HeaderReportDynamic() { Header = item.value.DisplayItem, Stt = 1, MergeRange = mergeRange });
                            ListDataDefault.Add(new DataReportDynamic2());
                        }
                        else
                        {
                            ListHeader1.Add(new HeaderReportDynamic());
                            ListHeaderPage1.Add(new HeaderReportDynamic());
                            ListHeader2.Add(new HeaderReportDynamic() { Header = item.value.DisplayItem });
                            ListDataDefault.Add(new DataReportDynamic2());
                        }
                    }
                    _lstDataDynamic2 = ListDataDefault;
                    List<ReportBudgetEstimateQuery> listData = _sktSoLieuService.ExportDuToanNganSachDonViNgangNSDTNExcel(yearOfWork, yearOfBudget, loaiNNS, maNguonNS, sDonVi, donViTinh).ToList();
                    List<Guid> lstIdMucLuc = listData.Select(x => x.IIDMLNS.GetValueOrDefault()).Distinct().ToList();
                    List<NsMucLucNganSach> nsMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                    nsMucLucs.RemoveAll(x => string.IsNullOrEmpty(x.MlnsIdParent.ToString()) || x.MlnsIdParent == Guid.Empty);

                    foreach (NsMucLucNganSach mlc in nsMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.MlnsId))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IsHangCha = mlc.BHangCha;
                            mlCha.IIDMLNSCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIDMLNS = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            mlCha.STNG = mlc.Tng;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            listData.Add(mlCha);
                        }
                    }

                    List<string> lstIdMucLucSKT = listData.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucSKTByChild(lstIdMucLucSKT);
                    foreach (NsSktMucLuc mlc in sktMucLucs)
                    {
                        if (!lstIdMucLucSKT.Contains(mlc.SKyHieu))
                        {
                            ReportBudgetEstimateQuery mlCha = new ReportBudgetEstimateQuery();
                            mlCha.IIDMLSKTCha = mlc.IIDMLSKTCha.GetValueOrDefault();
                            mlCha.IIDMLSKT = mlc.IIDMLSKT;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            listData.Add(mlCha);
                        }
                    }

                    UpdateKyHieuCha(listData);
                    listData.Where(x => string.IsNullOrEmpty(x.SXauNoiMa)).ForAll(n => n.IsHangCha = true);

                    List<ReportBudgetEstimateQuery> listDataGroups = listData.GroupBy(g => new { g.IIDMLNS, g.IIDMLNSCha, g.IIDMLSKT, g.IIDMLSKTCha, g.SMoTa, g.SXauNoiMa, g.SKyHieu }).Select(x => new ReportBudgetEstimateQuery
                    {
                        IIDMLNS = x.FirstOrDefault().IIDMLNS,
                        IIDMLNSCha = x.FirstOrDefault().IIDMLNSCha,
                        IIDMLSKT = x.FirstOrDefault().IIDMLSKT,
                        IIDMLSKTCha = x.FirstOrDefault().IIDMLSKTCha,
                        SXauNoiMa = x.FirstOrDefault().SXauNoiMa,
                        SKyHieu = x.FirstOrDefault().SKyHieu,
                        SLNS = x.FirstOrDefault().SLNS,
                        SL = x.FirstOrDefault().SL,
                        SK = x.FirstOrDefault().SK,
                        SM = x.FirstOrDefault().SM,
                        STM = x.FirstOrDefault().STM,
                        STTM = x.FirstOrDefault().STTM,
                        SNG = x.FirstOrDefault().SNG,
                        STNG = x.FirstOrDefault().STNG,
                        STNG1 = x.FirstOrDefault().STNG1,
                        STNG2 = x.FirstOrDefault().STNG2,
                        STNG3 = x.FirstOrDefault().STNG3,
                        SMoTa = x.FirstOrDefault().SMoTa,
                        IsHangCha = x.FirstOrDefault().IsHangCha,
                        FHangNhapBanThan = x.FirstOrDefault().FHangNhapBanThan,
                        FHangMuaBanThan = x.FirstOrDefault().FHangMuaBanThan,
                        TongHangNhap = x.Sum(x => x.TongHangNhap),
                        TongHangMua = x.Sum(x => x.TongHangMua),
                        FMucTienPhanBo = x.Sum(x => x.FMucTienPhanBo),
                        ListDataValue2 = GetDataDefault2()
                    }).ToList();

                    CalculateDataNSDTHExcel(listData, listDataGroups, listIdDonVi);
                    _lstDataDynamicSummary2 = GetDataDefault2();
                    for (int i = 0; i < _lstDataDynamicSummary2.Count; i++)
                    {
                        _lstDataDynamicSummary2[i].FHangNhap = listDataGroups.Where(x => !x.IsHangCha).Sum(y => y.ListDataValue2[i].FHangNhap);
                        _lstDataDynamicSummary2[i].FHangMua = listDataGroups.Where(x => !x.IsHangCha).Sum(y => y.ListDataValue2[i].FHangMua);
                    }

                    listDataGroups = listDataGroups.OrderBy(x => x.SKyHieu).ThenBy(x => x.SXauNoiMa).ToList();

                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listDataGroups);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonViIn", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("Year", yearOfWork);
                    data.Add("DonViTinh", h1);
                    data.Add("IsShowDonViIn", IsShowDonViIn == true ? "1" : "");
                    data.Add("ListHeader1", ListHeaderPage1);
                    data.Add("ListHeader2", ListHeader2);
                    data.Add("ListStt", lstStt);
                    data.Add("ListDataSummary", _lstDataDynamicSummary2);
                    AddChuKy(data, _typeChuKy);
                    List<GhiChu> ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);
                    if (_selectedInToiMuc.ValueItem.ToUpper() == "NG")
                        data.Add("sttNoiDung", "G");
                    else
                        data.Add("sttNoiDung", "H");

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_NSDTN_EXCEL));
                    fileNamePrefix = _sessionInfo.IdDonVi + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    string chiTietToi = _selectedInToiMuc != null ? ConvertChiTietToi(_selectedInToiMuc.ValueItem) : "Ngành";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                    if (hideColumns.Any())
                    {
                        for (int i = 0; i < hideColumns.Count; i++)
                        {
                            hideColumns[i] += 1;
                        }
                    }
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<ReportBudgetEstimateQuery, GhiChu, HeaderReportDynamic, DataReportDynamic2, SttHeader>(templateFileName, data, hideColumns);
                    results.Add(new ExportResult(_sessionInfo.IdDonVi, fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Tất cả", ValueItem = TypeLoaiNNS.TAT_CA.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() }
            };

            BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
        }

        public void LoadCombobox()
        {
            LoaiBaoCao = new ObservableCollection<ComboboxItem>();
            LoaiBaoCao.Add(new ComboboxItem { DisplayItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI, ValueItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI });
            LoaiBaoCao.Add(new ComboboxItem { DisplayItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI, ValueItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI });
            SelectedLoaiBaoCao = LoaiBaoCao.FirstOrDefault();

            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (DanhMuc dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();

            DataLoaiNganSach = new ObservableCollection<ComboboxItem>();
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = VoucherType.NSSD_Value });
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = VoucherType.NSBD_Value });
            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();

            LoadDonVi();
        }

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
                LoadTieuDe();
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadTypeChuKy()
        {
            if (SelectedLoaiNganSach?.ValueItem == VoucherType.NSSD_Key)
            {
                if (SelectedLoaiBaoCao != null && SelectedLoaiBaoCao.ValueItem == Utility.LoaiBaoCao.DU_TOAN_NS_CHI_MUCLUC_DONVI)
                    _typeChuKy = TypeChuKy.RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_SD_Ngang;
                else
                    _typeChuKy = TypeChuKy.RPT_NS_PLAN_SOSANH_SKTDTDN_SD;
            }
            else
            {
                if (SelectedLoaiBaoCao != null && SelectedLoaiBaoCao.ValueItem == Utility.LoaiBaoCao.DU_TOAN_NS_CHI_MUCLUC_DONVI)
                    _typeChuKy = TypeChuKy.RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_DT_Ngang;
                else
                    _typeChuKy = TypeChuKy.RPT_NS_PLAN_SOSANH_SKTDTDN_DT;
            }
            LoadTieuDe();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                TxtTitleFirst = string.Format(_dmChuKy.TieuDe1MoTa);
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                TxtTitleSecond = string.Format(_dmChuKy.TieuDe2MoTa);
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                TxtTitleThird = string.Format(_dmChuKy.TieuDe3MoTa);
        }

        private void LoadDonVi()
        {
            if (InTheoTongHop)
            {
                IEnumerable<DonVi> listDonvi = _nsDonViService.FindByLoai(_sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonvi);
            }
            else
            {
                List<NguoiDungDonVi> listNguoiDungDonVi = GetListDonViPhanQuyen();
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                List<DonVi> listDonvi = _nsDonViService.FindDonViCoDataSktSoLieuChiTietAllLoai(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                _sessionService.Current.Budget, loaiNNS, int.Parse(SelectedLoaiNganSach?.ValueItem ?? "1")).ToList();
                if (listNguoiDungDonVi != null && !listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(_sessionInfo.IdDonVi))
                {
                    listDonvi = listDonvi.Where(x => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(x.IIDMaDonVi)).ToList();
                }
                ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonvi);
            }

            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
            foreach (CheckBoxItem model in ListDonVi)
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
            OnPropertyChanged(nameof(IsEnableButtonPrint));
            OnPropertyChanged(nameof(ListDonVi));
            OnPropertyChanged(nameof(SelectedCountDonVi));
        }

        private List<NguoiDungDonVi> GetListDonViPhanQuyen()
        {
            System.Linq.Expressions.Expression<Func<NguoiDungDonVi, bool>> predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.IIDMaNguoiDung == _sessionService.Current.Principal);
            List<NguoiDungDonVi> listNguoiDungDonVi = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchDonVi!.ToLower());
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

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            DmChuKy dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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
            return Path.Combine(ExportPrefix.PATH_TL_SKT, input + FileExtensionFormats.Xlsx);
        }

        private List<NsMucLucNganSach> FindListParentMucLucByChild(List<ReportBudgetEstimateQuery> listMucLuc)
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            IEnumerable<NsMucLucNganSach> muclucs = _iNsMucLucNganSachService.FindByCondition(x => x.NamLamViec == yearOfWork && x.ITrangThai == 1 && !string.IsNullOrEmpty(x.M));
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsMucLucNganSach> nsMucLucs = new List<NsMucLucNganSach>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLNS.GetValueOrDefault()).ToList();
                while (true)
                {
                    List<Guid?> listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                    List<NsMucLucNganSach> listParent1 = muclucs.Where(x => listIdParent.Contains(x.MlnsId)).ToList();
                    if (listParent1.Count > 0)
                    {
                        List<Guid> lstId = listParent1.Select(item => item.MlnsId).ToList();
                        listIdMlskt.AddRange(lstId);
                        nsMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            nsMucLucs = nsMucLucs.GroupBy(x => x.MlnsId).Select(x => x.First()).OrderBy(x => x.XauNoiMa).ToList();
            return nsMucLucs;
        }

        private List<NsMucLucNganSach> FindListParentMucLucByChild(List<Guid> listIdMucLuc)
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            IEnumerable<NsMucLucNganSach> muclucs = _iNsMucLucNganSachService.FindByCondition(x => x.NamLamViec == yearOfWork && x.ITrangThai == 1 && !string.IsNullOrEmpty(x.M));
            List<NsMucLucNganSach> listMucLuc = muclucs.Where(x => listIdMucLuc.Contains(x.MlnsId)).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsMucLucNganSach> nsMucLucs = new List<NsMucLucNganSach>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.MlnsId).ToList();
                nsMucLucs.AddRange(listMucLuc);
                while (true)
                {
                    List<Guid?> listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.MlnsIdParent.GetValueOrDefault())).Select(x => x.MlnsIdParent).ToList();
                    List<NsMucLucNganSach> listParent1 = muclucs.Where(x => listIdParent.Contains(x.MlnsId)).ToList();
                    if (listParent1.Count > 0)
                    {
                        List<Guid> lstId = listParent1.Select(item => item.MlnsId).ToList();
                        listIdMlskt.AddRange(lstId);
                        nsMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            nsMucLucs = nsMucLucs.GroupBy(x => x.MlnsId).Select(x => x.First()).OrderBy(x => x.XauNoiMa).ToList();
            return nsMucLucs;
        }


        private List<NsSktMucLuc> FindListParentMucLucSKTByChild(List<string> listIdMucLuc)
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            List<NsSktMucLuc> muclucs = _iSktMucLucService.FindByCondition(x => x.INamLamViec == yearOfWork && x.ITrangThai == 1).ToList();
            List<NsSktMucLuc> listMucLuc = muclucs.Where(x => listIdMucLuc.Contains(x.SKyHieu)).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLSKT).ToList();
                sktMucLucs.AddRange(listMucLuc);
                while (true)
                {
                    List<Guid?> listIdParent = sktMucLucs.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                    List<NsSktMucLuc> listParent1 = muclucs.Where(x => listIdParent.Contains(x.IIDMLSKT)).ToList();
                    if (listParent1.Count > 0)
                    {
                        List<Guid> lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieu).ToList();
            return sktMucLucs;
        }



        private void UpdateKyHieuCha(List<ReportBudgetEstimateQuery> items)
        {
            Dictionary<Guid?, ReportBudgetEstimateQuery> dict = items.Where(x => x.IIDMLNS != null).GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.FirstOrDefault());
            void UpdateCha(ReportBudgetEstimateQuery item)
            {
                if (dict.TryGetValue(item.IIDMLNSCha, out ReportBudgetEstimateQuery parent))
                {
                    parent.SKyHieu = item.SKyHieu;
                    UpdateCha(parent);
                }
            }
            IEnumerable<ReportBudgetEstimateQuery> children = items.Where(x => x.IIDMLNSCha != null).GroupBy(x => x.IIDMLNSCha).Select(x => x.FirstOrDefault());
            foreach (ReportBudgetEstimateQuery item in children)
            {
                UpdateCha(item);
            }
        }

        private void CalculateData(List<ReportBudgetEstimateQuery> data)
        {
            data.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FMucTienPhanBo = 0;
                    x.FTuChi = 0;
                    return x;
                }).ToList();
            IEnumerable<ReportBudgetEstimateQuery> temp = data.Where(x => !x.IsHangCha);
            foreach (ReportBudgetEstimateQuery item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, data);
            }
        }

        private void CalculateParent(Guid? idParent, ReportBudgetEstimateQuery item, List<ReportBudgetEstimateQuery> lstItem)
        {
            ReportBudgetEstimateQuery model = lstItem.FirstOrDefault(x => x.IIDMLNS == idParent);
            if (model == null) return;
            model.FMucTienPhanBo += item.FMucTienPhanBo;
            model.FTuChi += item.FTuChi;
            CalculateParent(model.IIDMLNSCha, item, lstItem);
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
            }
            return chiTietToi;
        }

        private void OnNoteCommand()
        {
            NsBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuKy;
            NsBaoCaoGhiChuDialogViewModel.IsBaoCaoSoKiemTra = true;
            NsBaoCaoGhiChuDialogViewModel.IsPlanReport = true;
            NsBaoCaoGhiChuDialogViewModel.Init();
            NsBaoCaoGhiChuDialogViewModel.BudgetSourceTypeSelected = NsBaoCaoGhiChuDialogViewModel.BudgetSourceTypes.FirstOrDefault(x => x.ValueItem == BudgetSourceTypeSelected.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.PaperPrintTypeSelected = NsBaoCaoGhiChuDialogViewModel.PaperPrintTypes.FirstOrDefault(x => x.ValueItem == SelectedLoaiBaoCao.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.VoucherTypeSelected = NsBaoCaoGhiChuDialogViewModel.VoucherTypes.FirstOrDefault(x => x.ValueItem == SelectedLoaiNganSach.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.ShowDialogHost("PrintPlanBudgetEstimate");
        }

        private class GhiChu
        {
            public string Content { get; set; }
            public string SGhiChu => Content;
        }

        private List<GhiChu> GetGhiChu()
        {
            int iNamLamViec = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<NsCauHinhBaoCao, bool>> predicate = PredicateBuilder.True<NsCauHinhBaoCao>();
            predicate = predicate.And(x => x.INamLamViec.Equals(iNamLamViec));
            predicate = predicate.And(x => x.SMaBaoCao == _typeChuKy);
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

        private string GetMaGhiChu()
        {
            string data = JsonConvert.SerializeObject(new
            {
                LoaiBaoCao = SelectedLoaiBaoCao.ValueItem,
                LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                LoaiChungTu = SelectedLoaiNganSach.DisplayItem
            });
            return CompressExtension.CompressToBase64(data);
        }

        private string GetDonViBanHanh(int iDonVi, string loaiDVBH, DanhMuc itemDanhMuc, string selectedUnit)
        {
            string dvBanHanh = "";
            if (itemDanhMuc != null)
            {
                switch (loaiDVBH)
                {
                    case "1":
                        dvBanHanh = itemDanhMuc.SGiaTri;
                        break;
                    case "2":
                        dvBanHanh = _sessionInfo.TenDonVi;
                        break;
                    case "3":
                        dvBanHanh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).SGiaTri;
                        break;
                    case "4":
                        dvBanHanh = selectedUnit;
                        break;
                    case "5":
                        dvBanHanh = iDonVi == 1 ? _dmChuKy.TenDVBanHanh1 : _dmChuKy.TenDVBanHanh2;
                        break;
                    default:
                        dvBanHanh = iDonVi == 1 ? itemDanhMuc.SGiaTri : _sessionInfo.TenDonVi;
                        break;
                }
            }
            return dvBanHanh;
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

        private List<DataReportDynamic> GetDataDefault()
        {
            List<DataReportDynamic> result = new List<DataReportDynamic>();
            foreach (DataReportDynamic item in _lstDataDynamic)
            {
                result.Add(new DataReportDynamic());
            }

            return result;
        }

        private List<DataReportDynamic2> GetDataDefault2()
        {
            List<DataReportDynamic2> result = new List<DataReportDynamic2>();
            foreach (DataReportDynamic2 item in _lstDataDynamic2)
            {
                result.Add(new DataReportDynamic2());
            }

            return result;
        }

        private void CalculateDataExcel(List<ReportBudgetEstimateQuery> listData, List<ReportBudgetEstimateQuery> listResult, List<CheckBoxItem> listIdDonVi)
        {
            foreach (var item in listIdDonVi.Select((value, index) => new { index, value }))
            {
                List<ReportBudgetEstimateQuery> listDataAgencies = listData.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && x.IIdMaDonVi.Equals(item.value.ValueItem)).ToList();
                if (listDataAgencies.Any())
                {
                    listDataAgencies.ForEach(x =>
                    {
                        if (!string.IsNullOrEmpty(x.SXauNoiMa) && listResult.Any(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(x.SXauNoiMa)))
                        {
                            x.TongTuChi = listResult.FirstOrDefault(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(x.SXauNoiMa)).TongTuChi;
                        }
                    });
                }

                listResult.Where(x => x.IsHangCha)
                    .Select(x =>
                    {
                        x.TongTuChi = 0;
                        x.FTuChi = 0;
                        x.TuChiDV1 = 0;
                        x.TuChiDV2 = 0;
                        x.TuChiDV3 = 0;
                        x.TuChiDV4 = 0;
                        x.TuChiDV5 = 0;
                        x.TuChiDV6 = 0;
                        x.ListDataValue = GetDataDefault();
                        return x;
                    }).ToList();

                foreach (ReportBudgetEstimateQuery itemData in listResult.Where(x => !x.IsHangCha))
                {
                    itemData.ListDataValue[item.index].FVal = listDataAgencies.Where(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(itemData.SXauNoiMa)).Sum(s => s.FTuChi);
                }
            }
        }

        private void CalculateDataNSDTHExcel(List<ReportBudgetEstimateQuery> listData, List<ReportBudgetEstimateQuery> listResult, List<CheckBoxItem> listIdDonVi)
        {
            foreach (var item in listIdDonVi.Select((value, index) => new { index, value }))
            {
                List<ReportBudgetEstimateQuery> listDataAgencies = listData.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && x.IIdMaDonVi.Equals(item.value.ValueItem)).ToList();
                if (listDataAgencies.Any())
                {
                    listDataAgencies.ForEach(x =>
                    {
                        if (!string.IsNullOrEmpty(x.SXauNoiMa) && listResult.Any(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(x.SXauNoiMa)))
                        {
                            x.TongTuChi = listResult.FirstOrDefault(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(x.SXauNoiMa)).TongTuChi;
                        }
                    });
                }

                listResult.Where(x => x.IsHangCha)
                    .Select(x =>
                    {
                        x.TongHangNhap = 0;
                        x.TongHangMua = 0;
                        x.FHangNhapBanThan = 0;
                        x.FHangMuaBanThan = 0;
                        x.HangNhapDV1 = 0;
                        x.HangMuaDV1 = 0;
                        x.HangNhapDV2 = 0;
                        x.HangMuaDV2 = 0;
                        x.HangNhapDV3 = 0;
                        x.HangMuaDV3 = 0;
                        x.HangNhapDV4 = 0;
                        x.HangMuaDV4 = 0;
                        x.HangNhapDV5 = 0;
                        x.HangMuaDV5 = 0;
                        x.ListDataValue2 = GetDataDefault2();
                        return x;
                    }).ToList();

                foreach (ReportBudgetEstimateQuery itemData in listResult.Where(x => !x.IsHangCha))
                {
                    itemData.ListDataValue2[item.index].FHangNhap = listDataAgencies.Where(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(itemData.SXauNoiMa)).Sum(s => s.FHangNhap);
                    itemData.ListDataValue2[item.index].FHangMua = listDataAgencies.Where(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(itemData.SXauNoiMa)).Sum(s => s.FHangMua);
                }
            }
        }
    }
}
