using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportTargetMajorsDayViewModel : ReportViewModelBase<DtChungTuModel, ReportChiTieuDuToanDynamicQuery, ReportChiTieuDuToanDynamicQuery>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _specializedView;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsMucLucNganSachService _nSMucLucNganSachService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly IConfiguration _configuration;
        private readonly INsDonViService _nSDonViService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private DuToanTongChiTieu _tongChiTieu;
        private List<ReportChiTieuDuToanDynamicQuery> _listDataReportChiTieuNganh;
        private readonly string _templatePath;
        private int _countCol;
        private Dictionary<string, int> lstColCount;
        private List<DonVi> _dataDonVi;
        private List<NsMucLucNganSach> _dataMucLucNganSach;
        private string _chiTietToi;

        public override string Name
        {
            get => "In chỉ tiêu - Ngành theo số quyết định";
        }

        public override string Title
        {
            get => "In chỉ tiêu - Ngành theo số quyết định";
        }

        public override string Description
        {
            get => "Chọn thông số in chỉ tiêu dự toán cho Ngành bảo đảm theo số quyết định";
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportTargetMajors);

        private bool _isExportData;
        public bool IsExportData
        {
            get => ListSpecialized.Any() && ListSpecialized.Any(x => x.IsChecked);
            set => SetProperty(ref _isExportData, value);
        }

        // start handle chon dot
        private ObservableCollection<ComboboxManyItem> _dataDot;
        public ObservableCollection<ComboboxManyItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxManyItem _dataDotSelected;
        public ComboboxManyItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                if (_dataDotSelected != null)
                {
                    LoadSpecialized();
                }
            }
        }

        private bool _checkPrintAccumulation;
        public bool CheckPrintAccumulation
        {
            get => _checkPrintAccumulation;
            set
            {
                SetProperty(ref _checkPrintAccumulation, value);
                LoadSpecialized();
            }
        }

        private bool _checkPrintSpecialized;
        public bool CheckPrintSpecialized
        {
            get => _checkPrintSpecialized;
            set
            {
                SetProperty(ref _checkPrintSpecialized, value);
                LoadSpecialized();
            }
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

        //private bool _checkPrintTNG;
        //public bool CheckPrintTNG
        //{
        //    get => _checkPrintTNG;
        //    set => SetProperty(ref _checkPrintTNG, value);
        //}

        private bool _checkPrintAgency;
        public bool CheckPrintAgency
        {
            get => _checkPrintAgency;
            set => SetProperty(ref _checkPrintAgency, value);
        }

        // start handle Specialized
        public List<DanhMuc> ListDanhMuc = new List<DanhMuc>();
        private ObservableCollection<CheckBoxItem> _listSpecialized = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListSpecialized
        {
            get => _listSpecialized;
            set => SetProperty(ref _listSpecialized, value);
        }

        private string _labelSelectedCountSpecialized;
        public string LabelSelectedCountSpecialized
        {
            get
            {
                if (_checkPrintSpecialized)
                    _labelSelectedCountSpecialized = $"CHỌN CHUYÊN NGÀNH ({ListSpecialized.Count(item => item.IsChecked)}/{ListSpecialized.Count})";
                else
                    _labelSelectedCountSpecialized = $"CHỌN NGÀNH ({ListSpecialized.Count(item => item.IsChecked)}/{ListSpecialized.Count})";

                return _labelSelectedCountSpecialized;
            }
            set => SetProperty(ref _labelSelectedCountSpecialized, value);
        }

        private bool _selectAllSpecialized;

        public bool SelectAllSpecialized
        {
            get => ListSpecialized.Any() && ListSpecialized.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllSpecialized, value);
                foreach (var item in ListSpecialized) item.IsChecked = _selectAllSpecialized;
            }
        }

        private string _searchSpecialized;
        public string SearchSpecialized
        {
            get => _searchSpecialized;
            set
            {
                if (SetProperty(ref _searchSpecialized, value))
                {
                    _specializedView.Refresh();
                }
            }
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
                OnPropertyChanged(nameof(IsEnabledCheckPrintAgency));
            }
        }

        public bool IsEnabledCheckPrintAgency => SelectedLoaiBaoCao?.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_DONVI;

        private ObservableCollection<ComboboxItem> _levelPrint = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> LevelPrint
        {
            get => _levelPrint;
            set => SetProperty(ref _levelPrint, value);
        }

        private ComboboxItem _selectedLevelPrint;
        public ComboboxItem SelectedLevelPrint
        {
            get => _selectedLevelPrint;
            set => SetProperty(ref _selectedLevelPrint, value);
        }

        public RelayCommand ConfigSignCommand { get; }

        public PrintReportTargetMajorsDayViewModel(
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDtNhanPhanBoMapService dtChungTuMapService,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            ISktSoLieuService sktSoLieuService,
            INsDonViService nSDonViService,
            IConfiguration configuration,
            INsMucLucNganSachService nSMucLucNganSachService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel) : base(exportService, danhMucService, sessionService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _dtChungTuMapService = dtChungTuMapService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _sktSoLieuService = sktSoLieuService;
            _nSDonViService = nSDonViService;
            _nSMucLucNganSachService = nSMucLucNganSachService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _configuration = configuration;
            _templatePath = _configuration.GetSection(ConfigHelper.TEMPLATE_XLXSPATH).Value;
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            if (Model == null)
            {
                Model = new DtChungTuModel();
            }
            _sessionInfo = _sessionService.Current;
            //LoadVoucherType();
            LoadDotPhanBo();
            LoadTieuDe();
            LoadPaperPrintTypes();
            LoadCatUnitTypes();
            LoadSpecialized();
            LoadLoaiBaoCao();
            LoadAgency();
            LoadToiMucIn();
        }

        private void LoadToiMucIn()
        {
            DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
            if (danhMucChiTietToi != null)
            {
                _chiTietToi = danhMucChiTietToi.SGiaTri;
            }
            else
            {
                _chiTietToi = "NG";
            }
            LevelPrint = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(_chiTietToi));
            SelectedLevelPrint = LevelPrint.FirstOrDefault();
        }

        private void LoadTieuDe()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_NGANH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    _dmChuKy = new DmChuKy();
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                    TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                    TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                {
                    TxtTitleThird = _dmChuKy.TieuDe3MoTa;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadAgency()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            _dataDonVi = _nSDonViService.FindByCondition(predicate).ToList();
        }

        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.DUTOAN_TONGHOP_SOPHANBO, DisplayItem = LoaiBaoCao.DUTOAN_TONGHOP_SOPHANBO });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.DUTOAN_TONGHOP_DONVI, DisplayItem = LoaiBaoCao.DUTOAN_TONGHOP_DONVI });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.DUTOAN_TONGHOP_DONVI_MLNS_DOC, DisplayItem = LoaiBaoCao.DUTOAN_TONGHOP_DONVI_MLNS_DOC });
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedLoaiBaoCao));
        }

        public override void OnExportExcel(object obj)
        {
            Export(obj, ExportType.EXCEL);
        }

        public override void OnExportPdf(object obj)
        {
            Export(obj, ExportType.PDF);
        }
        public override void OnExportSignature(object obj)
        {
            Export(obj, ExportType.SIGNATURE);
        }

        public override void OnPrint(object obj)
        {
            Export(obj, ExportType.PDF);
        }

        private void ConfigTemplateFile(ref XlsFile template, List<ReportChiTieuDuToanDynamicQuery> dataTableExport)
        {
            if (dataTableExport == null || dataTableExport.Count() == 0)
            {
                return;
            }
            int width = 0;

            width = template.GetColWidth(11);

            switch (SelectedLevelPrint.ValueItem)
            {
                case "NG":
                    template.SetColHidden(7, true);
                    template.SetColHidden(8, true);
                    template.SetColHidden(9, true);
                    template.SetColHidden(10, true);
                    width += template.GetColWidth(7) + template.GetColWidth(8) + template.GetColWidth(9) + template.GetColWidth(10);
                    break;
                case "TNG":
                    template.SetColHidden(8, true);
                    template.SetColHidden(9, true);
                    template.SetColHidden(10, true);
                    width += template.GetColWidth(8) + template.GetColWidth(9) + template.GetColWidth(10);
                    break;
                case "TNG1":
                    template.SetColHidden(9, true);
                    template.SetColHidden(10, true);
                    width += template.GetColWidth(9) + template.GetColWidth(10);
                    break;
                case "TNG2":
                    template.SetColHidden(10, true);
                    width += template.GetColWidth(10);
                    break;
                case "TNG3":
                    break;
                default:
                    break;
            }

            template.SetColWidth(11, width);
        }

        public override void Export(object obj, ExportType type)
        {
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_SOPHANBO)
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
                    List<ReportChiTieuDuToanDynamicQuery> dataTableExport = GetData().ToList();
                    switch (type)
                    {
                        case ExportType.EXCEL:
                            dataExport = ConvertDataExport(dataTableExport, StringUtils.EXCEL_EXTENSION);
                            break;
                        case ExportType.PDF:
                            dataExport = ConvertDataExport(dataTableExport, StringUtils.PDF_EXTENSION);
                            break;
                        case ExportType.WORD:
                            break;
                        case ExportType.BROWSER:
                            dataExport = ConvertDataExport(dataTableExport, StringUtils.PDF_EXTENSION);
                            break;
                        case ExportType.SIGNATURE:
                            break;
                    }
                    var prefixTenNganh = "Ngành";
                    if (CheckPrintSpecialized)
                    {
                        prefixTenNganh = "Chuyên ngành";
                    }
                    foreach (var item in dataExport)
                    {
                        string templatePath = Path.Combine(_templatePath, item.Item1);
                        XlsFile xlsFileExport = new XlsFile(templatePath, true);
                        ConfigTemplateFile(ref xlsFileExport, dataTableExport);
                        //xlsFileExport.SetColWidth(2, 3000);
                        var xlsFile = _exportService.Export<ReportChiTieuDuToanDynamicQuery>(xlsFileExport, item.Item3);
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Item2);
                        results.Add(new ExportResult(prefixTenNganh + " " + item.Item3["TenNganh"], fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                },
                (s, e) =>
                {
                    if (e.Error == null && type.Equals(ExportType.EXCEL) || type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER))
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, type.Equals(ExportType.EXCEL) ? ExportType.EXCEL : ExportType.PDF);
                    }
                    IsLoading = false;
                });
            }
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_DONVI)
            {
                ExportDataAgency(type);
            }
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_DONVI_MLNS_DOC)
            {
                ExportDataAgencyMLNS(type);
            }
        }

        public IEnumerable<ReportChiTieuDuToanDynamicQuery> GetDataAgency()
        {
            int donViTinh = GetDonViTinh();
            if (_dataDotSelected == null)
            {
                return new List<ReportChiTieuDuToanDynamicQuery>();
            }
            var listDataReportChiTieuNganh = _dtChungTuChiTietService.GetDataReportChiTieuNganhAll(_sessionInfo.YearOfWork,
                                                                        _sessionInfo.Budget,
                                                                        _sessionInfo.YearOfBudget,
                                                                        string.Join(",", ListSpecialized.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList()),
                                                                        _dataDotSelected.ValueItem,
                                                                        donViTinh,
                                                                        CheckPrintAccumulation,
                                                                        true).ToList();
            return listDataReportChiTieuNganh.Where(n => !string.IsNullOrEmpty(n.MaDonVi));
        }

        public IEnumerable<ReportChiTieuDuToanDynamicMLNSQuery> GetDataAgencyMLNS()
        {
            int donViTinh = GetDonViTinh();
            if (_dataDotSelected == null)
            {
                return new List<ReportChiTieuDuToanDynamicMLNSQuery>();
            }
            var listDataReportChiTieuNganh = _dtChungTuChiTietService.GetDataReportChiTieuNganhAllMLNS(_sessionInfo.YearOfWork,
                                                                        _sessionInfo.Budget,
                                                                        _sessionInfo.YearOfBudget,
                                                                        string.Join(",", ListSpecialized.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList()),
                                                                        _dataDotSelected.ValueItem,
                                                                        donViTinh,
                                                                        CheckPrintAccumulation).ToList();
            return listDataReportChiTieuNganh.Where(n => !string.IsNullOrEmpty(n.MaDonVi));
        }

        private void ExportDataAgency(ExportType type)
        {
            lstColCount = new Dictionary<string, int>();
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
                List<ReportChiTieuDuToanDynamicQuery> dataTableExport = GetDataAgency().ToList();
                switch (type)
                {
                    case ExportType.EXCEL:
                        dataExport = ConvertDataExportAgency(dataTableExport, StringUtils.EXCEL_EXTENSION);
                        break;
                    case ExportType.PDF:
                        dataExport = ConvertDataExportAgency(dataTableExport, StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.WORD:
                        break;
                    case ExportType.BROWSER:
                        dataExport = ConvertDataExportAgency(dataTableExport, StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.SIGNATURE:
                        break;
                }
                var prefixTenNganh = "Ngành";
                if (CheckPrintSpecialized)
                {
                    prefixTenNganh = "Chuyên ngành";
                }
                foreach (var item in dataExport)
                {
                    string templatePath = Path.Combine(_templatePath, item.Item1);
                    XlsFile xlsFileExport = new XlsFile(templatePath, true);
                    ConfigTemplateFileAgency(ref xlsFileExport, dataTableExport, item.Item1);
                    var xlsFile = _exportService.Export<ReportChiTieuDuToanDynamicQuery>(xlsFileExport, item.Item3);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Item2);
                    results.Add(new ExportResult(prefixTenNganh + " " + item.Item3["TenNganh"], fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            },
            (s, e) =>
            {
                if (e.Error == null && type.Equals(ExportType.EXCEL) || type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER))
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, type.Equals(ExportType.EXCEL) ? ExportType.EXCEL : ExportType.PDF);
                }
                IsLoading = false;
            });
        }

        private List<ReportChiTieuDuToanDynamicMLNSReport> ConvertObjectDataToMLNS(List<ReportChiTieuDuToanDynamicMLNSQuery> dataExport, string nganh)
        {
            _dataMucLucNganSach = _nSMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
            List<ReportChiTieuDuToanDynamicMLNSReport> result = new List<ReportChiTieuDuToanDynamicMLNSReport>();
            List<string> listAgency = dataExport.Where(n => !string.IsNullOrEmpty(nganh) && nganh.Split(",").Contains(n.NG)).Select(n => n.MaDonVi).Distinct().ToList();
            if (listAgency == null || listAgency.Count() == 0)
            {
                return result;
            }
            foreach (string agency in listAgency)
            {
                ReportChiTieuDuToanDynamicMLNSReport itemResult = new ReportChiTieuDuToanDynamicMLNSReport();
                itemResult.MaDonVi = agency;
                itemResult.TenDonVi = dataExport.FirstOrDefault(n => n.MaDonVi == agency).TenDonVi;
                itemResult.Nganh = nganh;
                itemResult.LstGiaTri = GetDetailMLNS(dataExport, agency, nganh);
                result.Add(itemResult);
            }
            return result;
        }

        private void GetParentMLNS(DetailDataDynamicMLNSReport sourceItem)
        {
            NsMucLucNganSach itemMLNS = _dataMucLucNganSach.FirstOrDefault(n => n.XauNoiMa == sourceItem.XauNoiMa);
            NsMucLucNganSach itemMLNSParent = itemMLNS != null ? (_dataMucLucNganSach.FirstOrDefault(n => n.MlnsId == itemMLNS.MlnsIdParent)) : null;
            if (itemMLNS != null && itemMLNSParent != null)
            {
                sourceItem.XauNoiMaCha = itemMLNSParent.XauNoiMa;
                sourceItem.MoTaCha = itemMLNSParent.MoTa;
            }
            else
            {
                sourceItem.XauNoiMaCha = string.Empty;
                sourceItem.MoTaCha = string.Empty;
            }
        }

        private List<DetailDataDynamicMLNSReport> GetDetailMLNS(List<ReportChiTieuDuToanDynamicMLNSQuery> source, string maDonVi, string nganh)
        {
            List<DetailDataDynamicMLNSReport> result = new List<DetailDataDynamicMLNSReport>();
            List<ReportChiTieuDuToanDynamicMLNSQuery> data = source.Where(n => n.MaDonVi == maDonVi && !string.IsNullOrEmpty(nganh) && nganh.Split(",").Contains(n.NG)).ToList();
            List<string> lstXauNoiMa = source.Where(n => !string.IsNullOrEmpty(nganh) && nganh.Split(",").Contains(n.NG)).Select(n => n.XauNoiMa).Distinct().ToList();
            foreach (string xauNoiMa in lstXauNoiMa)
            {
                DetailDataDynamicMLNSReport itemResult = new DetailDataDynamicMLNSReport();
                itemResult.XauNoiMa = xauNoiMa;
                itemResult.MoTa = source.FirstOrDefault(n => n.XauNoiMa == xauNoiMa).MoTa;
                itemResult.TonKho = (data.Where(n => n.XauNoiMa == xauNoiMa).ToList() != null && data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Count() > 0) ?
                     data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Sum(n => n.TonKho) : 0;
                itemResult.TuChi = (data.Where(n => n.XauNoiMa == xauNoiMa).ToList() != null && data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Count() > 0) ?
                     data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Sum(n => n.TuChi) : 0;
                itemResult.HienVat = (data.Where(n => n.XauNoiMa == xauNoiMa).ToList() != null && data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Count() > 0) ?
                     data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Sum(n => n.HienVat) : 0;
                itemResult.DuPhong = (data.Where(n => n.XauNoiMa == xauNoiMa).ToList() != null && data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Count() > 0) ?
                     data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Sum(n => n.DuPhong) : 0;
                itemResult.HangNhap = (data.Where(n => n.XauNoiMa == xauNoiMa).ToList() != null && data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Count() > 0) ?
                     data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Sum(n => n.HangNhap) : 0;
                itemResult.HangMua = (data.Where(n => n.XauNoiMa == xauNoiMa).ToList() != null && data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Count() > 0) ?
                     data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Sum(n => n.HangMua) : 0;
                itemResult.PhanCap = (data.Where(n => n.XauNoiMa == xauNoiMa).ToList() != null && data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Count() > 0) ?
                     data.Where(n => n.XauNoiMa == xauNoiMa).ToList().Sum(n => n.PhanCap) : 0;
                GetParentMLNS(itemResult);
                result.Add(itemResult);
            }
            return result;
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

        //public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExportAgencyMLNS(IEnumerable<ReportChiTieuDuToanDynamicMLNSQuery> dataExport, string extension)
        //{
        //    int donViTinh = GetDonViTinh();
        //    var dictDanhMucById = ListDanhMuc.ToDictionary(x => x.Id);
        //    var listSpecializedChecked = ListSpecialized.Where(x => x.IsChecked).ToList();
        //    var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
        //    if (dataExport == null || dataExport.Count() == 0)
        //    {
        //        return listResult;
        //    }
        //    foreach (var item in listSpecializedChecked)
        //    {
        //        List<ReportChiTieuDuToanDynamicMLNSReport> listExport = ConvertObjectDataToMLNS(dataExport.ToList(), item.ValueItem);
        //        List<HeaderReportDuToanDynamicMLNS> header = new List<HeaderReportDuToanDynamicMLNS>();
        //        List<HeaderDetail> listXauNoiMaCon = new List<HeaderDetail>();
        //        List<HeaderDetail> listXauNoiMaCha = new List<HeaderDetail>();
        //        foreach (ReportChiTieuDuToanDynamicMLNSReport itemExport in listExport)
        //        {
        //            foreach (string itemXauNoiMa in itemExport.LstGiaTri.Select(n => string.Format("{0}", n.XauNoiMa)).Distinct())
        //            {
        //                string tempItem = itemExport.LstGiaTri.FirstOrDefault(n => n.XauNoiMa == itemXauNoiMa).MoTa;
        //                //if (listXauNoiMaCon.Count == 0 || !listXauNoiMaCon.Select(n => n.MoTa).ToList().Contains(tempItem))
        //                //{
        //                listXauNoiMaCon.Add(new HeaderDetail { MoTa = tempItem });
        //                string parentTempItem = itemExport.LstGiaTri.FirstOrDefault(n => n.MoTa == tempItem) != null ? (
        //                    itemExport.LstGiaTri.FirstOrDefault(n => n.MoTa == tempItem).MoTaCha) : string.Empty;
        //                listXauNoiMaCha.Add(new HeaderDetail { MoTa = parentTempItem });
        //                //}
        //            }
        //        }
        //        header.Add(new HeaderReportDuToanDynamicMLNS { LstNganhHeader = listXauNoiMaCha, LstMucLuc = listXauNoiMaCon });
        //        List<ReportChiTieuDuToanDynamicMLNSReport> rowTongso = new List<ReportChiTieuDuToanDynamicMLNSReport>();
        //        List<DetailDataDynamicMLNSReport> tongso = new List<DetailDataDynamicMLNSReport>();
        //        foreach (var itemGiaTri in dataExport.Where(n => item.ValueItem.Split(",").Contains(n.NG)).Select(n => n.XauNoiMa).Distinct().ToList())
        //        {
        //            tongso.Add(new DetailDataDynamicMLNSReport
        //            {
        //                GiaTriHangTong =
        //                (dataExport != null && dataExport.Where(n => n.XauNoiMa == itemGiaTri).ToList().Count() > 0) ?
        //                dataExport.Where(n => n.XauNoiMa == itemGiaTri).Sum(n => n.TongSoOld) : 0
        //            });
        //        }
        //        rowTongso.Add(new ReportChiTieuDuToanDynamicMLNSReport { LstGiaTriTotal = tongso });
        //        var danhMucItem = dictDanhMucById.GetValueOrDefault(item.Id, new DanhMuc());
        //        var data = new Dictionary<string, object>();
        //        FormatNumber formatNumber = new FormatNumber(donViTinh);
        //        data.Add("FormatNumber", formatNumber);
        //        data.Add("TieuDe1", TxtTitleFirst);
        //        data.Add("TieuDe2", TxtTitleSecond);
        //        data.Add("TenNganh", danhMucItem.STen);
        //        data.Add("Headers", header);
        //        data.Add("ListDataTotal", rowTongso);
        //        data.Add("TongSoAll", tongso.Sum(n => n.GiaTriHangTong));
        //        listExport.Select(n => { n.Stt = (listExport.IndexOf(n) + 1).ToString(); return n; }).ToList();
        //        data.Add("ListData", listExport);
        //        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS}{StringUtils.EXCEL_EXTENSION}");

        //        var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS}_{StringUtils.RemoveAccents(danhMucItem.STen)}_{DateUtils.GetFormatDateReport()}{extension}";
        //        listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
        //    }
        //    return listResult;
        //}

        //private void ExportDataAgencyMLNS(ExportType type)
        //{
        //    lstColCount = new Dictionary<string, int>();
        //    int countCol = 0;
        //    BackgroundWorkerHelper.Run((s, e) =>
        //    {
        //        IsLoading = true;
        //        List<ExportResult> results = new List<ExportResult>();
        //        List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
        //        List<ReportChiTieuDuToanDynamicMLNSQuery> dataTableExport = GetDataAgencyMLNS().ToList();

        //        countCol = (dataTableExport != null && dataTableExport.Count() > 0) ? (dataTableExport.Select(n => n.XauNoiMa).Distinct().ToList().Count() + 10) : 0;
        //        dataExport = ConvertDataExportAgencyMLNS(dataTableExport, StringUtils.EXCEL);
        //        foreach (var item in dataExport)
        //        {
        //            string templatePath = Path.Combine(_templatePath, item.Item1);
        //            // XlsFile xlsFileExport = new XlsFile(templatePath, true);
        //            var xlsFile = ExportService.Export<HeaderReportDuToanDynamicMLNS, ReportChiTieuDuToanDynamicMLNSReport>(item.Item1, item.Item3);

        //            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Item2);
        //            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
        //        }
        //        e.Result = results;
        //    },
        //    (s, e) =>
        //    {
        //        if (e.Error == null && type.Equals(ExportType.EXCEL) || type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER))
        //        {
        //            var result = (List<ExportResult>)e.Result;
        //            ExportService.MergeCellAndOpen(result, ExportType.EXCEL, 7, 5, countCol);
        //        }
        //        IsLoading = false;
        //    });
        //}

        private List<NsMucLucNganSach> GetMLNS()
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            List<NsMucLucNganSach> data = _nSMucLucNganSachService.FindByCondition(predicate).OrderBy(n => n.XauNoiMa).ToList();
            return data;
        }

        private void GetParentMLNS(List<NsMucLucNganSachCha> lstMlResult)
        {
            foreach (var item in lstMlResult)
            {
                NsMucLucNganSach itemMLNSParent = _dataMucLucNganSach.FirstOrDefault(n => n.MlnsId == item.MlnsIdParent);
                if (itemMLNSParent != null)
                {
                    item.XauNoiMaCha = itemMLNSParent.XauNoiMa;
                    item.MoTaCha = itemMLNSParent.MoTa;
                }
                else
                {
                    item.XauNoiMaCha = string.Empty;
                    item.MoTaCha = string.Empty;
                }
            }

        }

        private List<DanhMuc> GetListDanhMucChuyenNganhById(List<string> lstIdChuyenNganh)
        {
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => VoucherType.DM_Nganh.Equals(x.SType));
            predicate = predicate.And(x => lstIdChuyenNganh.Contains(x.IIDMaDanhMuc));
            return _danhMucService.FindByCondition(predicate).ToList();
        }

        public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExportAgencyMLNS(IEnumerable<ReportChiTieuDuToanDynamicMLNSQuery> dataExport, string extension)
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var yearOfBudget = _sessionService.Current.YearOfBudget;
            var budgetSource = _sessionService.Current.Budget;
            int donViTinh = GetDonViTinh();
            var dictDanhMucById = ListDanhMuc.ToDictionary(x => x.Id);
            var listSpecializedChecked = ListSpecialized.Where(x => x.IsChecked).ToList();
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            List<NsMucLucNganSach> listMLNSDb = GetMLNS();
            if (dataExport == null || dataExport.Count() == 0)
            {
                return listResult;
            }
            foreach (var item in listSpecializedChecked)
            {
                List<ReportChiTieuDuToanDynamicMLNSQuery> dataPrint = dataExport.Where(n => !string.IsNullOrEmpty(item.ValueItem) && item.ValueItem.Split(",").Contains(n.NG)).ToList();
                string listIdDonVi = string.Join(",", dataPrint.Select(n => n.MaDonVi).ToList());
                var listDonVi = _nSDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
                listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();
                List<string> listXauNoiMaData = dataPrint.Where(n => !string.IsNullOrEmpty(item.ValueItem) && item.ValueItem.Split(",").Contains(n.NG)).Select(n => n.XauNoiMa).Distinct().ToList();
                //List<NsMucLucNganSach> sktMucLucsSource = GetMLNS().Where(n => listXauNoiMaData.Contains(n.XauNoiMa)).OrderBy(n => n.XauNoiMa).ToList();
                List<NsMucLucNganSach> sktMucLucsSource = listMLNSDb.Where(n => listXauNoiMaData.Contains(n.XauNoiMa)).OrderBy(x => x.Ng).ToList();
                List<NsMucLucNganSachCha> lstMlResult = new List<NsMucLucNganSachCha>();
                //lstMlResult = _mapper.Map<List<NsMucLucNganSachCha>>(mucluc);
                List<string> lstNg = new List<string>();
                foreach (var mucluc in sktMucLucsSource)
                {
                    var mlResultChildren = new NsMucLucNganSachCha();
                    if (lstNg.Count <= 0 || !lstNg.Contains(mucluc.Ng))
                    {
                        NsMucLucNganSachCha mlTong = new NsMucLucNganSachCha();
                        mlTong.Ng = mucluc.Ng;
                        mlTong.MoTa = "(+)";
                        lstMlResult.Add(mlTong);
                        lstNg.Add(mucluc.Ng);
                    }
                    mlResultChildren = _mapper.Map<NsMucLucNganSachCha>(mucluc);
                    lstMlResult.Add(mlResultChildren);
                }
                //GetParentMLNS(lstMlResult);
                List<List<NsMucLucNganSachCha>> listMucLucSplits = SplitList(lstMlResult, 6).ToList();
                var lstIdChuyenNganh = !string.IsNullOrEmpty(item.ValueItem) ? item.ValueItem.Split(",").ToList() : new List<string>();
                var lstChuyenNganh = GetListDanhMucChuyenNganhById(lstIdChuyenNganh);
                for (int i = 0; i < listMucLucSplits.Count; i++)
                {
                    List<HeaderReportDuToanDynamicMLNS> headers = new List<HeaderReportDuToanDynamicMLNS>();
                    List<string> lstNganhHeader = listMucLucSplits[i].Select(x => x.Ng).Distinct().ToList();
                    int columnStart = 4;
                    foreach (var nganhHeader in lstNganhHeader)
                    {
                        var nganh = lstChuyenNganh.FirstOrDefault(x => x.IIDMaDanhMuc.Equals(nganhHeader));
                        var lstMlHeader = listMucLucSplits[i].Where(x => nganhHeader.Contains(x.Ng)).Select(x => new { MoTa = x.MoTa, Lns = x.M + StringUtils.DIVISION + x.Tm + StringUtils.DIVISION + x.Ttm + StringUtils.DIVISION + x.Ng }).ToList();
                        var mergeRange = "";
                        var columnStartName = GetExcelColumnName(columnStart);
                        var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                        mergeRange = columnStartName + "7" + ":" + columnEndName + "7";
                        //columnStart += 1;
                        HeaderReportDuToanDynamicMLNS hd = new HeaderReportDuToanDynamicMLNS();
                        hd.TenNganh = nganh != null ? nganh.STen : "";

                        hd.MergeRange = mergeRange;
                        int j = 0;
                        foreach (var mlHeader in lstMlHeader)
                        {
                            HeaderDetail mlhd = new HeaderDetail();
                            if (j == 0)
                            {
                                mlhd = new HeaderDetail();
                                mlhd.MoTa = nganh != null ? nganh.STen : "";
                                mlhd.SSttBC = "1";
                                hd.LstNganhHeader.Add(mlhd);
                            }
                            else
                            {
                                mlhd = new HeaderDetail();
                                mlhd.MoTa = "";
                                mlhd.SSttBC = "2";
                                hd.LstNganhHeader.Add(mlhd);
                            }
                            mlhd = new HeaderDetail();
                            mlhd.MoTa = mlHeader.MoTa;
                            mlhd.Lns = mlHeader.Lns;
                            if (j == 0 && mlhd.MoTa == "(+)")
                            {
                                mlhd.Lns = String.Empty;

                            }
                            hd.LstMucLuc.Add(mlhd);
                            j++;
                        }
                        headers.Add(hd);
                    }
                    int stt = 1;
                    List<ReportChiTieuDuToanDynamicMLNSReport> results = new List<ReportChiTieuDuToanDynamicMLNSReport>();
                    foreach (var dv in listDonVi)
                    {
                        ReportChiTieuDuToanDynamicMLNSReport kq = new ReportChiTieuDuToanDynamicMLNSReport();
                        kq.Stt = (stt++).ToString();
                        kq.MaDonVi = dv.IIDMaDonVi;
                        kq.TenDonVi = dv.TenDonVi;
                        //kq.TongCong = listDataDonViPrint.Where(x => x.IIdMaDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TotalValue);
                        kq.SubToal = (double)(dataPrint.Where(x => x.MaDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi));
                        kq.LstGiaTri = new List<DetailDataDynamicMLNSReport>();
                        foreach (var ml in listMucLucSplits[i])
                        {
                            //DetailDataDynamicMLNSReport giaTri = new DetailDataDynamicMLNSReport();
                            //giaTri = new DetailDataDynamicMLNSReport();
                            //var gtDonVi = dataPrint.Where(x => x.MaDonVi == dv.IIDMaDonVi && x.XauNoiMa == ml.XauNoiMa).ToList();
                            //giaTri.FTuChi = gtDonVi != null ? gtDonVi.Sum(n => n.TongSoOld) : 0;
                            //kq.LstGiaTri.Add(giaTri);


                            DetailDataDynamicMLNSReport giaTri = new DetailDataDynamicMLNSReport();
                            if (ml.NamLamViec.Equals(0) || ml.NamLamViec == null)
                            {
                                giaTri = new DetailDataDynamicMLNSReport();
                                var gtDonVi = dataPrint.Where(x => ml.Ng.Contains(x.NG) && x.MaDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                giaTri.FTuChi = (double)gtDonVi;
                                kq.LstGiaTri.Add(giaTri);
                                //kq.SubToal = kq.SubToal + giaTri.FTuChi;                                
                            }
                            else
                            {
                                giaTri = new DetailDataDynamicMLNSReport();
                                var gtDonVi = dataPrint.FirstOrDefault(x => x.MaDonVi == dv.IIDMaDonVi && x.MlnsId == ml.MlnsId);
                                giaTri.FTuChi = (double)(gtDonVi != null ? gtDonVi.TuChi : 0);
                                kq.LstGiaTri.Add(giaTri);
                            }
                        }
                        results.Add(kq);
                    }
                    results = results.OrderBy(n => n.MaDonVi).ToList();

                    //List<ReportChiTieuDuToanDynamicMLNSReport> resultsTotal = new List<ReportChiTieuDuToanDynamicMLNSReport>();
                    //ReportChiTieuDuToanDynamicMLNSReport total = new ReportChiTieuDuToanDynamicMLNSReport();
                    //total.LstGiaTriTotal = new List<DetailDataDynamicMLNSReport>();
                    //foreach (var dv in listMucLucSplits[i])
                    //{
                    //    DetailDataDynamicMLNSReport giaTri = new DetailDataDynamicMLNSReport();
                    //    giaTri = new DetailDataDynamicMLNSReport();
                    //    var gtDonVi = dataPrint.Where(x => x.XauNoiMa.Equals(dv.XauNoiMa)).Sum(x => x.TongSoOld);
                    //    giaTri.FTuChi = gtDonVi;
                    //    total.LstGiaTriTotal.Add(giaTri);
                    //}
                    //resultsTotal.Add(total);

                    List<ReportChiTieuDuToanDynamicMLNSReport> resultsTotal = new List<ReportChiTieuDuToanDynamicMLNSReport>();
                    ReportChiTieuDuToanDynamicMLNSReport total = new ReportChiTieuDuToanDynamicMLNSReport();
                    total.LstGiaTriTotal = new List<DetailDataDynamicMLNSReport>();
                    foreach (var dv in listMucLucSplits[i])
                    {
                        DetailDataDynamicMLNSReport giaTri = new DetailDataDynamicMLNSReport();
                        if (dv.NamLamViec.Equals(0) || dv.NamLamViec == null)
                        {
                            giaTri = new DetailDataDynamicMLNSReport();
                            var gtDonVi = dataPrint.Where(x => dv.Ng.Contains(x.NG)).Sum(x => x.TuChi);
                            giaTri.FTuChi = (double)gtDonVi;
                            total.LstGiaTriTotal.Add(giaTri);
                        }
                        else
                        {
                            giaTri = new DetailDataDynamicMLNSReport();
                            var gtDonVi = dataPrint.Where(x => x.MlnsId.Equals(dv.MlnsId)).Sum(x => x.TuChi);
                            giaTri.FTuChi = (double)gtDonVi;
                            total.LstGiaTriTotal.Add(giaTri);
                        }
                    }
                    resultsTotal.Add(total);

                    var prefixTenNganh = "Ngành";
                    if (CheckPrintSpecialized)
                    {
                        prefixTenNganh = "Chuyên ngành";
                    }
                    var sumTotal = dataPrint.Sum(x => x.TongSoOld);
                    var numColumnMerge = listMucLucSplits[i].Count() < 3 ? 4 : listMucLucSplits[i].Count();
                    var nameColunmMerge = GetExcelColumnName(numColumnMerge + 4);
                    var data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    var danhMucItem = dictDanhMucById.GetValueOrDefault(item.Id, new DanhMuc());
                    data.Add("PrefixTenNganh", prefixTenNganh);
                    data.Add("TenNganh", danhMucItem != null ? danhMucItem.STen : "");
                    data.Add("Headers", headers);
                    data.Add("ListDataTotal", resultsTotal);
                    double tongSoAll = dataPrint.Sum(n => n.TongSoOld);
                    data.Add("TongSoAll", tongSoAll);
                    data.Add("DonViTinh", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : string.Empty) + " - Tờ: " + (i + 1)); ;
                    // listExport.Select(n => { n.Stt = (listExport.IndexOf(n) + 1).ToString(); return n; }).ToList();
                    data.Add("ListData", results);
                    if (i == 0)
                    {
                        data.Add("TongTienBangChu", StringUtils.NumberToText(tongSoAll * donViTinh, true));
                    }
                    string templateFileName = string.Empty;
                    if (i == 0)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS + "_Trang1"}{StringUtils.EXCEL_EXTENSION}");
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS + "_Trang"}{StringUtils.EXCEL_EXTENSION}");

                    }
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS);

                    listResult.Add(Tuple.Create(templateFileName, fileNameWithoutExtension + "_" + i.ToString() + "_"
                        + (danhMucItem != null ? danhMucItem.IIDMaDanhMuc : string.Empty) + "_", data));
                }
            }
            return listResult;
        }


        public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExportAgencyMLNSChuyenNganh(IEnumerable<ReportChiTieuDuToanDynamicMLNSQuery> dataExport, string extension)
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var yearOfBudget = _sessionService.Current.YearOfBudget;
            var budgetSource = _sessionService.Current.Budget;
            int donViTinh = GetDonViTinh();
            var dictDanhMucById = ListDanhMuc.ToDictionary(x => x.Id);
            var listSpecializedChecked = ListSpecialized.Where(x => x.IsChecked).ToList();
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            List<NsMucLucNganSach> listMLNSDb = GetMLNS();
            if (dataExport == null || dataExport.Count() == 0)
            {
                return listResult;
            }
            foreach (var item in listSpecializedChecked)
            {
                List<ReportChiTieuDuToanDynamicMLNSQuery> dataPrint = dataExport.Where(n => !string.IsNullOrEmpty(item.ValueItem) && item.ValueItem.Split(",").Contains(n.NG)).ToList();
                string listIdDonVi = string.Join(",", dataPrint.Select(n => n.MaDonVi).ToList());
                var listDonVi = _nSDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
                listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();
                List<string> listXauNoiMaData = dataPrint.Where(n => !string.IsNullOrEmpty(item.ValueItem) && item.ValueItem.Split(",").Contains(n.NG)).Select(n => n.XauNoiMa).Distinct().ToList();

                List<NsMucLucNganSach> sktMucLucsSource = listMLNSDb.Where(n => listXauNoiMaData.Contains(n.XauNoiMa)).OrderBy(x => x.Ng).ToList();

                List<NsMucLucNganSachCha> lstMlResult = new List<NsMucLucNganSachCha>();
                List<string> lstLns = new List<string>();
                foreach (var mucluc in sktMucLucsSource)
                {
                    var mlResultChildren = new NsMucLucNganSachCha();
                    if (lstLns.Count <= 0 || !lstLns.Contains(mucluc.Lns))
                    {
                        NsMucLucNganSachCha mlTong = new NsMucLucNganSachCha();
                        mlTong.Ng = mucluc.Ng;
                        mlTong.MoTa = "(+)";
                        mlTong.Lns = mucluc.Lns;
                        lstMlResult.Add(mlTong);
                        lstLns.Add(mucluc.Lns);
                    }
                    mlResultChildren = _mapper.Map<NsMucLucNganSachCha>(mucluc);
                    lstMlResult.Add(mlResultChildren);
                }

                List<List<NsMucLucNganSachCha>> listMucLucSplits = SplitList(lstMlResult, 6).ToList();
                var lstIdChuyenNganh = !string.IsNullOrEmpty(item.ValueItem) ? item.ValueItem.Split(",").ToList() : new List<string>();
                var lstChuyenNganh = GetListDanhMucChuyenNganhById(lstIdChuyenNganh);
                for (int i = 0; i < listMucLucSplits.Count; i++)
                {
                    List<HeaderReportDuToanDynamicMLNS> headers = new List<HeaderReportDuToanDynamicMLNS>();
                    List<string> lstLnsHeader = listMucLucSplits[i].Select(x => x.Lns).Distinct().ToList();
                    int columnStart = 4;
                    foreach (var lns in lstLnsHeader)
                    {

                        var lstMlHeader = listMucLucSplits[i].Where(x => x.Lns == lns).Select(x => new { MoTa = x.MoTa, Lns = x.M + StringUtils.DIVISION + x.Tm + StringUtils.DIVISION + x.Ttm + StringUtils.DIVISION + x.Ng }).ToList();
                        var mergeRange = "";
                        var columnStartName = GetExcelColumnName(columnStart);
                        var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                        mergeRange = columnStartName + "7" + ":" + columnEndName + "7";

                        //columnStart += 1;
                        HeaderReportDuToanDynamicMLNS hd = new HeaderReportDuToanDynamicMLNS();

                        hd.MergeRange = mergeRange;
                        int j = 0;
                        foreach (var mlHeader in lstMlHeader)
                        {
                            HeaderDetail mlhd = new HeaderDetail();
                            if (j == 0)
                            {
                                mlhd = new HeaderDetail();
                                mlhd.MoTa = lns;
                                mlhd.SSttBC = "1";
                                hd.LstNganhHeader.Add(mlhd);
                            }
                            else
                            {
                                mlhd = new HeaderDetail();
                                mlhd.MoTa = "";
                                mlhd.SSttBC = "2";
                                hd.LstNganhHeader.Add(mlhd);
                            }
                            mlhd = new HeaderDetail();
                            mlhd.MoTa = mlHeader.MoTa;
                            mlhd.Lns = mlHeader.Lns;
                            if (j == 0)
                            {
                                mlhd.Lns = String.Empty;
                            }
                            hd.LstMucLuc.Add(mlhd);
                            j++;
                        }
                        headers.Add(hd);
                    }
                    int stt = 1;
                    List<ReportChiTieuDuToanDynamicMLNSReport> results = new List<ReportChiTieuDuToanDynamicMLNSReport>();
                    foreach (var dv in listDonVi)
                    {
                        ReportChiTieuDuToanDynamicMLNSReport kq = new ReportChiTieuDuToanDynamicMLNSReport();
                        kq.Stt = (stt++).ToString();
                        kq.MaDonVi = dv.IIDMaDonVi;
                        kq.TenDonVi = dv.TenDonVi;
                        //kq.TongCong = listDataDonViPrint.Where(x => x.IIdMaDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TotalValue);
                        kq.SubToal = (double)(dataPrint.Where(x => x.MaDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi));
                        kq.LstGiaTri = new List<DetailDataDynamicMLNSReport>();
                        foreach (var ml in listMucLucSplits[i])
                        {
                            DetailDataDynamicMLNSReport giaTri = new DetailDataDynamicMLNSReport();
                            if (ml.NamLamViec.Equals(0) || ml.NamLamViec == null)
                            {
                                giaTri = new DetailDataDynamicMLNSReport();
                                List<string> sXauNoiMaLoaiNS = new List<string>();
                                sXauNoiMaLoaiNS = dataPrint.Where(x => x.LNS == ml.Lns).Select(x => x.XauNoiMa).ToList();
                                var gtDonVi = dataPrint.Where(x => sXauNoiMaLoaiNS.Contains(x.XauNoiMa) && x.MaDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                giaTri.FTuChi = (double)gtDonVi;
                                kq.LstGiaTri.Add(giaTri);
                                //kq.SubToal = kq.SubToal + giaTri.FTuChi;                                
                            }
                            else
                            {
                                giaTri = new DetailDataDynamicMLNSReport();
                                var gtDonVi = dataPrint.FirstOrDefault(x => x.MaDonVi == dv.IIDMaDonVi && x.MlnsId == ml.MlnsId);
                                giaTri.FTuChi = (double)(gtDonVi != null ? gtDonVi.TuChi : 0);
                                kq.LstGiaTri.Add(giaTri);
                            }
                        }
                        results.Add(kq);
                    }
                    results = results.OrderBy(n => n.MaDonVi).ToList();

                    List<ReportChiTieuDuToanDynamicMLNSReport> resultsTotal = new List<ReportChiTieuDuToanDynamicMLNSReport>();
                    ReportChiTieuDuToanDynamicMLNSReport total = new ReportChiTieuDuToanDynamicMLNSReport();
                    total.LstGiaTriTotal = new List<DetailDataDynamicMLNSReport>();
                    foreach (var dv in listMucLucSplits[i])
                    {
                        DetailDataDynamicMLNSReport giaTri = new DetailDataDynamicMLNSReport();
                        if (dv.NamLamViec.Equals(0) || dv.NamLamViec == null)
                        {
                            giaTri = new DetailDataDynamicMLNSReport();
                            List<string> sXauNoiMaLoaiNS = new List<string>();
                            sXauNoiMaLoaiNS = dataPrint.Where(x => x.LNS == dv.Lns).Select(x => x.XauNoiMa).ToList();
                            var gtDonVi = dataPrint.Where(x => sXauNoiMaLoaiNS.Contains(x.XauNoiMa)).Sum(x => x.TuChi);
                            giaTri.FTuChi = (double)gtDonVi;
                            total.LstGiaTriTotal.Add(giaTri);
                        }
                        else
                        {
                            giaTri = new DetailDataDynamicMLNSReport();
                            var gtDonVi = dataPrint.Where(x => x.MlnsId.Equals(dv.MlnsId)).Sum(x => x.TuChi);
                            giaTri.FTuChi = (double)gtDonVi;
                            total.LstGiaTriTotal.Add(giaTri);
                        }
                    }
                    resultsTotal.Add(total);

                    var prefixTenNganh = "Ngành";
                    if (CheckPrintSpecialized)
                    {
                        prefixTenNganh = "Chuyên ngành";
                    }
                    var sumTotal = dataPrint.Sum(x => x.TongSoOld);
                    var numColumnMerge = listMucLucSplits[i].Count() < 3 ? 4 : listMucLucSplits[i].Count();
                    var nameColunmMerge = GetExcelColumnName(numColumnMerge + 4);
                    var data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    var danhMucItem = dictDanhMucById.GetValueOrDefault(item.Id, new DanhMuc());
                    data.Add("PrefixTenNganh", prefixTenNganh);
                    data.Add("TenNganh", danhMucItem != null ? danhMucItem.STen : "");
                    data.Add("Headers", headers);
                    data.Add("ListDataTotal", resultsTotal);
                    double tongSoAll = dataPrint.Sum(n => n.TongSoOld);
                    data.Add("TongSoAll", tongSoAll);
                    data.Add("DonViTinh", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : string.Empty) + " - Tờ: " + (i + 1)); ;
                    // listExport.Select(n => { n.Stt = (listExport.IndexOf(n) + 1).ToString(); return n; }).ToList();
                    data.Add("ListData", results);
                    if (i == 0)
                    {
                        data.Add("TongTienBangChu", StringUtils.NumberToText(tongSoAll * donViTinh, true));
                    }
                    string templateFileName = string.Empty;
                    if (i == 0)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS + "_Trang1"}{StringUtils.EXCEL_EXTENSION}");
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS + "_Trang"}{StringUtils.EXCEL_EXTENSION}");

                    }
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS);

                    listResult.Add(Tuple.Create(templateFileName, fileNameWithoutExtension + "_" + i.ToString() + "_"
                        + (danhMucItem != null ? danhMucItem.IIDMaDanhMuc : string.Empty) + "_", data));
                }
            }
            return listResult;
        }

        private void ExportDataAgencyMLNS(ExportType type)
        {
            lstColCount = new Dictionary<string, int>();
            int countCol = 0;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
                List<ReportChiTieuDuToanDynamicMLNSQuery> dataTableExport = GetDataAgencyMLNS().ToList();
                _dataMucLucNganSach = _nSMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                countCol = (dataTableExport != null && dataTableExport.Count() > 0) ? (dataTableExport.Select(n => n.XauNoiMa).Distinct().ToList().Count() + 10) : 0;
                if (CheckPrintSpecialized)
                {
                    dataExport = ConvertDataExportAgencyMLNSChuyenNganh(dataTableExport, StringUtils.EXCEL);
                }
                else
                {
                    dataExport = ConvertDataExportAgencyMLNS(dataTableExport, StringUtils.EXCEL);
                }

                //dataExport = ConvertDataExportAgencyMLNS(dataTableExport, StringUtils.EXCEL);
                foreach (var item in dataExport)
                {
                    string templatePath = Path.Combine(_templatePath, item.Item1);
                    // XlsFile xlsFileExport = new XlsFile(templatePath, true);
                    var xlsFile = _exportService.Export<HeaderReportDuToanDynamicMLNS, ReportChiTieuDuToanDynamicMLNSReport>(item.Item1, item.Item3);

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Item2);
                    if (CheckPrintSpecialized)
                    {
                        // xlsFile.SetColHidden(4, true);
                        results.Add(new ExportResult("Chuyên ngành " + item.Item3["TenNganh"], fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        results.Add(new ExportResult("Ngành " + item.Item3["TenNganh"], fileNameWithoutExtension, null, xlsFile));
                    }
                }
                e.Result = results;
            },
            (s, e) =>
            {
                if (e.Error == null && type.Equals(ExportType.EXCEL) || type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER))
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, type);
                    //_exportService.MergeCellAndOpen(result, ExportType.EXCEL, 7, 5, countCol);
                }
                IsLoading = false;
            });
        }

        private void ConfigTemplateFileAgency(ref XlsFile template, List<ReportChiTieuDuToanDynamicQuery> dataTableExport, string templateName)
        {
            if (dataTableExport == null || dataTableExport.Count() == 0)
            {
                return;
            }
            if (!lstColCount.ContainsKey(templateName))
            {
                return;
            }
            int _countCol = lstColCount[templateName];

            int width = 0;

            width = template.GetColWidth(11);

            switch (SelectedLevelPrint.ValueItem)
            {
                case "NG":
                    template.SetColHidden(7, true);
                    template.SetColHidden(8, true);
                    template.SetColHidden(9, true);
                    template.SetColHidden(10, true);
                    width += template.GetColWidth(7) + template.GetColWidth(8) + template.GetColWidth(9) + template.GetColWidth(10);
                    break;
                case "TNG":
                    template.SetColHidden(8, true);
                    template.SetColHidden(9, true);
                    template.SetColHidden(10, true);
                    width += template.GetColWidth(8) + template.GetColWidth(9) + template.GetColWidth(10);
                    break;
                case "TNG1":
                    template.SetColHidden(9, true);
                    template.SetColHidden(10, true);
                    width += template.GetColWidth(9) + template.GetColWidth(10);
                    break;
                case "TNG2":
                    template.SetColHidden(10, true);
                    width += template.GetColWidth(10);
                    break;
                case "TNG3":
                    break;
                default:
                    break;
            }

            template.SetColWidth(11, width);
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void SetTongSoAll(List<ReportChiTieuDuToanDynamicQuery> target, List<ReportChiTieuDuToanDynamicQuery> source)
        {
            foreach (var item in target)
            {
                item.TongAll = source.Where(n => n.XauNoiMa == item.XauNoiMa).Sum(n => n.TongSoOld);
            }
        }

        public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExportAgency(IEnumerable<ReportChiTieuDuToanDynamicQuery> dataExport, string extension)
        {
            int donViTinh = GetDonViTinh();
            var dictDanhMucById = ListDanhMuc.ToDictionary(x => x.Id);
            var listSpecializedChecked = ListSpecialized.Where(x => x.IsChecked).ToList();
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            if (dataExport == null || dataExport.Count() == 0)
            {
                return listResult;
            }
            dataExport = dataExport.OrderBy(x => x.MaDonVi).ToList();
            List<string> listAgency = dataExport.Select(n => n.MaDonVi).Distinct().ToList();

            if (listAgency == null || listAgency.Count() == 0)
            {
                return listResult;
            }
            List<List<string>> listAgencyPage = new List<List<string>>();
            listAgencyPage = SplitList(listAgency, 3).ToList();

            List<ReportChiTieuDuToanDynamicQuery> listDataExport = dataExport.ToList();

            int countPage = 1;
            foreach (List<string> lstDonVi in listAgencyPage)
            {
                if (lstDonVi == null || lstDonVi.Count() == 0)
                {
                    continue;
                }
                foreach (var item in listSpecializedChecked)
                {
                    List<ReportChiTieuDuToanDynamicQuery> listExportDetailAgency = ProcessDynamicAgency(listDataExport, lstDonVi, item.ValueItem);
                    List<ReportChiTieuDuToanDynamicQuery> listHeader = new List<ReportChiTieuDuToanDynamicQuery>();
                    _listDataReportChiTieuNganh = new List<ReportChiTieuDuToanDynamicQuery>();
                    List<ReportChiTieuDuToanDynamicQuery> dataExportOfSpecialized = listExportDetailAgency.Where(x => item.ValueItem.Contains(x.NG)).ToList();
                    List<ReportChiTieuDuToanDynamicQuery> listDataGlobal = listDataExport.Where(x => item.ValueItem.Contains(x.NG)).ToList();
                    double tongSoAll = listDataGlobal.Where(n => !n.IsHangCha).Sum(n => n.TongSoOld);
                    SetTongSoAll(dataExportOfSpecialized, listDataGlobal);
                    CalculateTotal(dataExportOfSpecialized.ToList());
                    if (dataExportOfSpecialized != null && dataExportOfSpecialized.ToList().Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                                                                    string.Join(",", dataExportOfSpecialized.Select(n => n.XauNoiMa).ToList())).ToList();
                        listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach mlnsItem in listParent)
                            {
                                listHeader.Add(new ReportChiTieuDuToanDynamicQuery
                                {
                                    LNS = mlnsItem.Lns,
                                    L = mlnsItem.L,
                                    K = mlnsItem.K,
                                    M = mlnsItem.M,
                                    TM = mlnsItem.Tm,
                                    TTM = mlnsItem.Ttm,
                                    NG = mlnsItem.Ng,
                                    TNG = mlnsItem.Tng,
                                    TNG1 = mlnsItem.Tng1,
                                    TNG2 = mlnsItem.Tng2,
                                    TNG3 = mlnsItem.Tng3,
                                    MoTa = mlnsItem.MoTa,
                                    XauNoiMa = mlnsItem.XauNoiMa,
                                    HienVat = 0,
                                    TuChi = 0,
                                    IsHangCha = true,
                                    MlnsId = mlnsItem.MlnsId,
                                    MlnsIdParent = mlnsItem.MlnsIdParent.GetValueOrDefault(Guid.Empty)
                                });
                            }
                            foreach (ReportChiTieuDuToanDynamicQuery exportItem in dataExportOfSpecialized)
                            {
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == exportItem.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    exportItem.MlnsIdParent = listHeader[index].MlnsId;
                                    listHeader.Insert(index + 1, exportItem);
                                }
                            }
                        }
                        _listDataReportChiTieuNganh = new List<ReportChiTieuDuToanDynamicQuery>(listHeader.OrderBy(x => x.XauNoiMa));
                        _listDataReportChiTieuNganh.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                        _listDataReportChiTieuNganh.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                        _listDataReportChiTieuNganh.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();
                    }
                    CalculateDataLNSAgency();
                    switch (SelectedLevelPrint.ValueItem)
                    {
                        case "NG":
                            _listDataReportChiTieuNganh = _listDataReportChiTieuNganh.Where(n => string.IsNullOrEmpty(n.TNG)
                             && string.IsNullOrEmpty(n.TNG1)
                             && string.IsNullOrEmpty(n.TNG2)
                             && string.IsNullOrEmpty(n.TNG3)).ToList();
                            _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case "TNG":
                            _listDataReportChiTieuNganh = _listDataReportChiTieuNganh.Where(n => string.IsNullOrEmpty(n.TNG1)
                             && string.IsNullOrEmpty(n.TNG2)
                             && string.IsNullOrEmpty(n.TNG3)).ToList();
                            _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case "TNG1":
                            _listDataReportChiTieuNganh = _listDataReportChiTieuNganh.Where(n => string.IsNullOrEmpty(n.TNG2)
                             && string.IsNullOrEmpty(n.TNG3)).ToList();
                            _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case "TNG2":
                            _listDataReportChiTieuNganh = _listDataReportChiTieuNganh.Where(n => string.IsNullOrEmpty(n.TNG3)).ToList();
                            _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case "TNG3":
                            break;
                        default:
                            break;
                    }

                    foreach (var i in _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                    {
                        var parent = _listDataReportChiTieuNganh.Where(x => x.MlnsId == i.MlnsIdParent).LastOrDefault();
                        //if(i.M != string.Empty)
                        //{
                        //    i.L = string.Empty;
                        //    i.K = string.Empty;
                        //    i.LNS = string.Empty;
                        //}
                        //if (parent != null && (i.M != string.Empty || i.L != string.Empty || i.K != string.Empty))
                        //{
                        //    i.L = string.Empty;
                        //    i.K = string.Empty;
                        //    i.LNS = string.Empty;
                        //    if (!string.IsNullOrEmpty(parent.M))
                        //        i.M = string.Empty;
                        //    if (!string.IsNullOrEmpty(parent.TM))
                        //        i.TM = string.Empty;
                        //    if (!string.IsNullOrEmpty(parent.TTM))
                        //        i.TTM = string.Empty;
                        //    if (!string.IsNullOrEmpty(parent.NG))
                        //        i.NG = string.Empty;
                        //    if (!string.IsNullOrEmpty(parent.TNG))
                        //        i.TNG = string.Empty;
                        //    if (!string.IsNullOrEmpty(parent.TNG1))
                        //        i.TNG1 = string.Empty;
                        //    if (!string.IsNullOrEmpty(parent.TNG2))
                        //        i.TNG2 = string.Empty;
                        //    if (!string.IsNullOrEmpty(parent.TNG3))
                        //        i.TNG3 = string.Empty;
                        //}

                        if (i.M != string.Empty || i.TM != string.Empty || i.TTM != string.Empty)
                        {
                            if (parent != null && !string.IsNullOrEmpty(parent.L) && !string.IsNullOrEmpty(parent.K))
                            {
                                i.L = string.Empty;
                                i.K = string.Empty;
                                i.LNS = string.Empty;
                            }

                            if (parent != null && (i.L != string.Empty || i.K != string.Empty))
                            {
                                if (!string.IsNullOrEmpty(parent.M))
                                    i.M = string.Empty;
                                if (!string.IsNullOrEmpty(parent.TM))
                                    i.TM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.TTM))
                                    i.TTM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.NG))
                                    i.NG = string.Empty;
                                if (!string.IsNullOrEmpty(parent.TNG))
                                    i.TNG = string.Empty;
                                if (!string.IsNullOrEmpty(parent.TNG1))
                                    i.TNG1 = string.Empty;
                                if (!string.IsNullOrEmpty(parent.TNG2))
                                    i.TNG2 = string.Empty;
                                if (!string.IsNullOrEmpty(parent.TNG3))
                                    i.TNG3 = string.Empty;
                            }
                        }
                    }

                    CheckParent(_listDataReportChiTieuNganh);

                    var danhMucItem = dictDanhMucById.GetValueOrDefault(item.Id, new DanhMuc());
                    var data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TitleFirst", TxtTitleFirst);
                    data.Add("TitleSecond", TxtTitleSecond);
                    data.Add("TitleThird", TxtTitleThird);
                    data.Add("TenNganh", danhMucItem.STen);
                    data.Add("TongSo", tongSoAll);
                    data.Add("CatUnitType", string.Format("Đơn vị tính: {0}", CatUnitTypeSelected.DisplayItem));
                    data.Add("TitleCol1", GetHeaderTitleAgency(lstDonVi, lstDonVi.ElementAt(0)));
                    data.Add("TotalCol1", _tongChiTieu.TongCol1);
                    data.Add("TotalCol1Text", StringUtils.NumberToText(_tongChiTieu.TongCol1 * donViTinh));
                    data.Add("TotalTongSoText", StringUtils.NumberToText(tongSoAll * donViTinh));

                    if (lstDonVi.Count >= 2)
                    {
                        data.Add("TitleCol2", GetHeaderTitleAgency(lstDonVi, lstDonVi.ElementAt(1)));
                        data.Add("TotalCol2", _tongChiTieu.TongCol2);
                        data.Add("TotalCol2Text", StringUtils.NumberToText(_tongChiTieu.TongCol2 * donViTinh));
                    }

                    if (lstDonVi.Count >= 3)
                    {
                        data.Add("TitleCol3", GetHeaderTitleAgency(lstDonVi, lstDonVi.ElementAt(2)));
                        data.Add("TotalCol3", _tongChiTieu.TongCol3);
                        data.Add("TotalCol3Text", StringUtils.NumberToText(_tongChiTieu.TongCol3 * donViTinh));
                    }

                    data.Add("Items", _listDataReportChiTieuNganh);
                    data.Add("Count", 100000);
                    string templateFileName = GetFileNameTemplateAgency(lstDonVi.Count());
                    if (!lstColCount.ContainsKey(templateFileName))
                        lstColCount.Add(templateFileName, lstDonVi.Count());
                    var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY}_{countPage.ToString()}_{StringUtils.RemoveAccents(danhMucItem.STen)}_{DateUtils.GetFormatDateReport()}{extension}";
                    listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
                }
                countPage++;
            }
            return listResult;
        }

        public string GetFileNameTemplateAgency(int numberAgency)
        {
            var paper = "Normal";
            if (PaperPrintTypeSelected.ValueItem.Equals("2"))
            {
                paper = "A4Ngang";
            }
            return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY}_{numberAgency.ToString()}_{paper}{string.Empty}{StringUtils.EXCEL_EXTENSION}");
        }

        private string GetHeaderTitleAgency(List<string> list, string maDonVi)
        {
            if (_dataDonVi != null && _dataDonVi.Count > 0)
            {
                DonVi item = _dataDonVi.Where(n => n.IIDMaDonVi == maDonVi).FirstOrDefault();
                return item != null ? item.TenDonVi.Trim() : string.Empty;
            }
            return string.Empty;
        }

        private List<ReportChiTieuDuToanDynamicQuery> ProcessDynamicAgency(List<ReportChiTieuDuToanDynamicQuery> data, List<string> lstCol, string nganh)
        {
            List<ReportChiTieuDuToanDynamicQuery> listResult = new List<ReportChiTieuDuToanDynamicQuery>();
            foreach (ReportChiTieuDuToanDynamicQuery item in data)
            {
                if (listResult.Count() == 0 || !listResult.Select(n => n.XauNoiMa).ToList().Contains(item.XauNoiMa))
                {
                    listResult.Add(new ReportChiTieuDuToanDynamicQuery
                    {
                        LNS1 = item.LNS1,
                        LNS3 = item.LNS3,
                        LNS5 = item.LNS5,
                        LNS = item.LNS,
                        L = item.L,
                        K = item.K,
                        M = item.M,
                        TM = item.TM,
                        TTM = item.TTM,
                        NG = item.NG,
                        TNG = item.TNG,
                        TNG1 = item.TNG1,
                        TNG2 = item.TNG2,
                        TNG3 = item.TNG3,
                        XauNoiMa = item.XauNoiMa,
                        MoTa = item.MoTa,
                        MlnsId = item.MlnsId,
                        MlnsIdParent = item.MlnsIdParent,
                        IsHangCha = item.IsHangCha
                    });
                }
            }

            foreach (var item in listResult)
            {
                if (lstCol.Count >= 1)
                {
                    item.Col1 = data.Where(n => n.XauNoiMa == item.XauNoiMa && n.MaDonVi == lstCol.ElementAt(0) && nganh.Contains(n.NG)).Sum(n => n.TongSoOld);
                }
                if (lstCol.Count >= 2)
                {
                    item.Col2 = data.Where(n => n.XauNoiMa == item.XauNoiMa && n.MaDonVi == lstCol.ElementAt(1) && nganh.Contains(n.NG)).Sum(n => n.TongSoOld);
                }
                if (lstCol.Count >= 3)
                {
                    item.Col3 = data.Where(n => n.XauNoiMa == item.XauNoiMa && n.MaDonVi == lstCol.ElementAt(2) && nganh.Contains(n.NG)).Sum(n => n.TongSoOld);
                }
            }
            return listResult;
        }

        private void LoadDotPhanBo()
        {
            var predicate = CreatePredicateChungTuIndex();
            List<NsDtChungTu> listDtChungTu = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            listDtChungTu.Where(n => !n.DNgayQuyetDinh.HasValue).Select(n => { n.DNgayQuyetDinh = n.DNgayChungTu.Value; return n; }).ToList();
            if (listDtChungTu != null && listDtChungTu.Count > 0)
            {
                List<string> listSoQuyetDinh = listDtChungTu.Where(n => !string.IsNullOrEmpty(n.SSoQuyetDinh)).Select(n => n.SSoQuyetDinh).Distinct().ToList();
                List<ComboboxManyItem> sourceCombobox = new List<ComboboxManyItem>();
                foreach (string soQD in listSoQuyetDinh)
                {
                    List<NsDtChungTu> chungTus = listDtChungTu.Where(n => n.SSoQuyetDinh == soQD).ToList();
                    var firstCt = chungTus.FirstOrDefault();
                    string mota = firstCt.DNgayChungTu.HasValue ? firstCt.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty;
                    mota += StringUtils.SPACE + firstCt.SMoTa;
                    ComboboxManyItem itemCombobox = new ComboboxManyItem();
                    itemCombobox.DisplayItem = string.Format("{0}\n{1}", soQD, mota);
                    itemCombobox.ValueItem = string.Join(",", chungTus.Select(n => n.Id.ToString()).ToList());
                    sourceCombobox.Add(itemCombobox);
                }

                List<string> listNgayChungTu = listDtChungTu.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh)).Select(n => n.DNgayChungTu.Value.ToString("dd/MM/yyyy")).Distinct().ToList();
                foreach (string ngayChungTu in listNgayChungTu)
                {
                    List<NsDtChungTu> chungTus = listDtChungTu.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.Value.ToString("dd/MM/yyyy") == ngayChungTu).ToList();
                    string soCt = string.Join(",", chungTus.Select(x => x.SSoChungTu));
                    var firstCt = chungTus.FirstOrDefault();
                    string mota = firstCt.DNgayChungTu.HasValue ? firstCt.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty;
                    mota += StringUtils.SPACE + firstCt.SMoTa;
                    ComboboxManyItem itemCombobox = new ComboboxManyItem();
                    itemCombobox.DisplayItem = string.Format("{0}\n{1}", soCt, mota);
                    itemCombobox.ValueItem = string.Join(",", chungTus.Select(n => n.Id.ToString()).ToList());
                    sourceCombobox.Add(itemCombobox);
                }
                Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDtChungTu);
                DataDot = new ObservableCollection<ComboboxManyItem>(sourceCombobox);
                var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem.Split('\n')[1].Split(" ")[0])).ToList();
                DataDot = new ObservableCollection<ComboboxManyItem>(ordered);
                DataDotSelected = DataDot.FirstOrDefault();
            }
            else
            {
                DataDot = new ObservableCollection<ComboboxManyItem>();
                Models = new ObservableCollection<DtChungTuModel>();
            }
            OnPropertyChanged(nameof(DataDot));
            OnPropertyChanged(nameof(DataDotSelected));
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

        private bool CheckListContai(List<string> source, List<string> target)
        {
            foreach (string item in target)
            {
                if (source.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        private void LoadSpecialized()
        {
            ListDanhMuc = new List<DanhMuc>();
            var yearOfWork = _sessionInfo.YearOfWork;
            ListSpecialized = new ObservableCollection<CheckBoxItem>();

            if (_dataDotSelected != null && CheckListContai(_dataDotSelected.ValueItem.Split(",").ToList(), Models.Select(n => n.Id.ToString()).ToList()))
            {
                List<string> listIdDotSelected = _dataDotSelected.ValueItem.Split(",").ToList();
                List<DtChungTuModel> dtChungTuSelected = Models.Where(n => listIdDotSelected.Contains(n.Id.ToString())).ToList();
                string idChungTu = string.Join(",", listIdDotSelected);
                if (_checkPrintAccumulation)
                {
                    idChungTu = string.Join(StringUtils.COMMA, Models.Where(x => x.DNgayChungTu.HasValue && x.DNgayChungTu <= DateUtils.EndOfDay(dtChungTuSelected.Max(n => n.DNgayQuyetDinh).Value)).Select(x => x.Id.ToString()));
                }
                var type = EstimationReport.DU_TOAN_THEO_NGANH;
                if (_checkPrintSpecialized)
                {
                    type = EstimationReport.DU_TOAN_THEO_CHUYEN_NGANH;
                }
                ListDanhMuc = _danhMucService.FindDanhMucTheoNganh(idChungTu, yearOfWork, type).ToList();
            }

            ListSpecialized = _mapper.Map<ObservableCollection<CheckBoxItem>>(ListDanhMuc);
            OnPropertyChanged(nameof(LabelSelectedCountSpecialized));
            OnPropertyChanged(nameof(SelectAllSpecialized));
            OnPropertyChanged(nameof(IsExportData));
            // Filter
            _specializedView = CollectionViewSource.GetDefaultView(ListSpecialized);
            _specializedView.Filter = obj => string.IsNullOrWhiteSpace(_searchSpecialized)
                                             || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchSpecialized.Trim(), StringComparison.OrdinalIgnoreCase));
            foreach (var model in ListSpecialized)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountSpecialized));
                    OnPropertyChanged(nameof(SelectAllSpecialized));
                    OnPropertyChanged(nameof(IsExportData));
                };
            }
        }

        public string GetFileNameTemplate(string numberCol)
        {
            var loaiChungTuStr = "All";
            loaiChungTuStr = loaiChungTuStr + numberCol;
            var inTieuNganh = string.Empty;
            //if (CheckPrintTNG)
            inTieuNganh = "_TNG";
            var paper = "Normal";
            if (PaperPrintTypeSelected.ValueItem.Equals("2"))
            {
                paper = "A4Ngang";
            }
            return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH}_{loaiChungTuStr}{inTieuNganh}_{paper}{string.Empty}{StringUtils.EXCEL_EXTENSION}");
        }

        private List<string> GetListColumn(List<string> listLNS)
        {
            if (listLNS == null || listLNS.Count == 0)
            {
                return new List<string>();
            }
            List<string> result = new List<string>();
            Dictionary<int, string> listCol = new Dictionary<int, string>();
            var predicate = _nSMucLucNganSachService.createPredicateAllNull();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(n => listLNS.Contains(n.Lns));
            List<NsMucLucNganSach> listMLNS = _nSMucLucNganSachService.FindByCondition(predicate).OrderBy(n => n.XauNoiMa).ToList();
            foreach (NsMucLucNganSach item in listMLNS)
            {
                if (item.BTonKho && !listCol.ContainsKey(1))
                {
                    listCol.Add(1, "TonKho");
                }
                if (item.BTuChi && !listCol.ContainsKey(2))
                {
                    listCol.Add(2, "TuChi");
                }
                if (item.BHangNhap && !listCol.ContainsKey(3))
                {
                    listCol.Add(3, "HangNhap");
                }
                if (item.BHangMua && !listCol.ContainsKey(4))
                {
                    listCol.Add(4, "HangMua");
                }
                if (item.BHienVat && !listCol.ContainsKey(5))
                {
                    listCol.Add(5, "HienVat");
                }
                if (item.BDuPhong && !listCol.ContainsKey(6))
                {
                    listCol.Add(6, "DuPhong");
                }
                if (item.BPhanCap && !listCol.ContainsKey(7))
                {
                    listCol.Add(7, "PhanCap");
                }
            }
            result = listCol.Select(n => n.Value).ToList();
            return result.Distinct().ToList();
        }

        private string GetHeaderTitle(List<string> list, int index)
        {
            if (list.Count() >= (index + 1))
            {
                string item = list.ElementAt(index);
                switch (item)
                {
                    case "TonKho":
                        return "Tồn kho";
                    case "TuChi":
                        return "Tự chi";
                    case "HangNhap":
                        return "Hàng nhập";
                    case "HangMua":
                        return "Hàng mua";
                    case "HienVat":
                        return "Hiện vật";
                    case "DuPhong":
                        return "Dự phòng";
                    case "PhanCap":
                        return "Phân cấp";
                    default:
                        return string.Empty;
                }
            }
            return string.Empty;
        }

        public override IEnumerable<ReportChiTieuDuToanDynamicQuery> GetData()
        {
            int donViTinh = GetDonViTinh();
            if (_dataDotSelected == null)
            {
                return new List<ReportChiTieuDuToanDynamicQuery>();
            }
            var listDataReportChiTieuNganh = _dtChungTuChiTietService.GetDataReportChiTieuNganhAll(_sessionInfo.YearOfWork,
                                                                        _sessionInfo.Budget,
                                                                        _sessionInfo.YearOfBudget,
                                                                        string.Join(",", ListSpecialized.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList()),
                                                                        _dataDotSelected.ValueItem,
                                                                        donViTinh,
                                                                        CheckPrintAccumulation,
                                                                        CheckPrintAgency).ToList();
            return listDataReportChiTieuNganh;
        }

        private void ProcessDynamicCol(ref List<ReportChiTieuDuToanDynamicQuery> data, List<string> lstCol)
        {
            foreach (ReportChiTieuDuToanDynamicQuery item in data)
            {
                int count = 1;
                foreach (string colName in lstCol)
                {
                    Type myType = item.GetType();
                    PropertyInfo pinfo = myType.GetProperty("Caption");
                    SetValueByFieldName(item, "Col" + count, colName);
                    count++;
                }
            }
        }

        private void SetValueByFieldName(ReportChiTieuDuToanDynamicQuery item, string fieldNameTarget, string fieldNameSource)
        {
            Type myType = item.GetType();
            PropertyInfo pinfoSource = myType.GetProperty(fieldNameSource);
            PropertyInfo pinfoTarget = myType.GetProperty(fieldNameTarget);
            pinfoTarget.SetValue(item, pinfoSource.GetValue(item, null), null);
        }

        private void CheckParent(List<ReportChiTieuDuToanDynamicQuery> input)
        {
            List<string> listXauNoiMa = new List<string>();
            if (input == null || input.Count() == 0)
            {
                return;
            }
            foreach (ReportChiTieuDuToanDynamicQuery item in input)
            {
                ReportChiTieuDuToanDynamicQuery itemChild = input.FirstOrDefault(n => n.MlnsIdParent == item.MlnsId);
                if (itemChild != null)
                {
                    item.IsHangCha = true;
                }
                else
                {
                    item.IsHangCha = false;
                }
            }
        }

        public override List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<ReportChiTieuDuToanDynamicQuery> dataExport, string extension)
        {
            int donViTinh = GetDonViTinh();
            var dictDanhMucById = ListDanhMuc.ToDictionary(x => x.Id);
            var listSpecializedChecked = ListSpecialized.Where(x => x.IsChecked).ToList();
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            if (dataExport == null || dataExport.Count() == 0)
            {
                return listResult;
            }
            List<string> listCol = GetListColumn(dataExport.Select(n => n.LNS).ToList());
            if (listCol == null || listCol.Count() == 0)
            {
                _countCol = 0;
                return listResult;
            }
            _countCol = listCol.Count();

            List<ReportChiTieuDuToanDynamicQuery> listDataExport = dataExport.ToList();
            ProcessDynamicCol(ref listDataExport, listCol);
            foreach (var item in listSpecializedChecked)
            {
                List<ReportChiTieuDuToanDynamicQuery> listHeader = new List<ReportChiTieuDuToanDynamicQuery>();
                _listDataReportChiTieuNganh = new List<ReportChiTieuDuToanDynamicQuery>();
                List<ReportChiTieuDuToanDynamicQuery> dataExportOfSpecialized = listDataExport.Where(x => item.ValueItem.Contains(x.NG) && !string.IsNullOrEmpty(x.NG)).ToList();
                CalculateTotal(dataExportOfSpecialized.ToList());
                if (dataExportOfSpecialized != null && dataExportOfSpecialized.ToList().Count > 0)
                {
                    List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                                                                string.Join(",", dataExportOfSpecialized.Select(n => n.XauNoiMa).ToList())).ToList();
                    listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                    if (listParent != null && listParent.Count > 0)
                    {
                        foreach (NsMucLucNganSach mlnsItem in listParent)
                        {
                            listHeader.Add(new ReportChiTieuDuToanDynamicQuery
                            {
                                LNS = mlnsItem.Lns,
                                L = mlnsItem.L,
                                K = mlnsItem.K,
                                M = mlnsItem.M,
                                TM = mlnsItem.Tm,
                                TTM = mlnsItem.Ttm,
                                NG = mlnsItem.Ng,
                                TNG = mlnsItem.Tng,
                                TNG1 = mlnsItem.Tng1,
                                TNG2 = mlnsItem.Tng2,
                                TNG3 = mlnsItem.Tng3,
                                MoTa = mlnsItem.MoTa,
                                XauNoiMa = mlnsItem.XauNoiMa,
                                HienVat = 0,
                                TuChi = 0,
                                IsHangCha = mlnsItem.BHangChaDuToan.GetValueOrDefault(),
                                MlnsId = mlnsItem.MlnsId,
                                MlnsIdParent = mlnsItem.MlnsIdParent.GetValueOrDefault(Guid.Empty)
                            });
                        }
                        foreach (ReportChiTieuDuToanDynamicQuery exportItem in dataExportOfSpecialized)
                        {
                            int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == exportItem.MlnsIdParent).FirstOrDefault());
                            if (index >= 0)
                            {
                                exportItem.MlnsIdParent = listHeader[index].MlnsId;
                                if (CheckPrintAgency)
                                {
                                    if (!string.IsNullOrEmpty(exportItem.MaDonVi))
                                    {
                                        exportItem.LNS = string.Empty;
                                        exportItem.L = string.Empty;
                                        exportItem.K = string.Empty;
                                        exportItem.TTM = string.Empty;
                                        exportItem.NG = string.Empty;
                                        exportItem.TNG = string.Empty;
                                        exportItem.TNG1 = string.Empty;
                                        exportItem.TNG2 = string.Empty;
                                        exportItem.TNG3 = string.Empty;
                                        exportItem.MoTa = exportItem.MaDonVi + "-" + exportItem.TenDonVi;
                                    }
                                }
                                listHeader.Insert(index + 1, exportItem);
                            }
                        }
                    }
                    _listDataReportChiTieuNganh = new List<ReportChiTieuDuToanDynamicQuery>(listHeader.OrderBy(x => x.XauNoiMa));
                    //_listDataReportChiTieuNganh.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                    //_listDataReportChiTieuNganh.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                    //_listDataReportChiTieuNganh.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();
                }
                CalculateDataLNS();
                switch (SelectedLevelPrint.ValueItem)
                {
                    case "NG":
                        _listDataReportChiTieuNganh = _listDataReportChiTieuNganh.Where(n => string.IsNullOrEmpty(n.TNG)
                         && string.IsNullOrEmpty(n.TNG1)
                         && string.IsNullOrEmpty(n.TNG2)
                         && string.IsNullOrEmpty(n.TNG3)).ToList();
                        _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case "TNG":
                        _listDataReportChiTieuNganh = _listDataReportChiTieuNganh.Where(n => string.IsNullOrEmpty(n.TNG1)
                         && string.IsNullOrEmpty(n.TNG2)
                         && string.IsNullOrEmpty(n.TNG3)).ToList();
                        _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case "TNG1":
                        _listDataReportChiTieuNganh = _listDataReportChiTieuNganh.Where(n => string.IsNullOrEmpty(n.TNG2)
                         && string.IsNullOrEmpty(n.TNG3)).ToList();
                        _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case "TNG2":
                        _listDataReportChiTieuNganh = _listDataReportChiTieuNganh.Where(n => string.IsNullOrEmpty(n.TNG3)).ToList();
                        _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case "TNG3":
                        _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.TNG3)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    default:
                        break;
                }

                foreach (var i in _listDataReportChiTieuNganh.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                {
                    var parent = _listDataReportChiTieuNganh.Where(x => x.MlnsId == i.MlnsIdParent).LastOrDefault();

                    if (i.M != string.Empty || i.TM != string.Empty || i.TTM != string.Empty)
                    {

                        if (parent != null)
                        {
                            if (!string.IsNullOrEmpty(parent.L) && !string.IsNullOrEmpty(parent.K))
                            {
                                i.L = string.Empty;
                                i.K = string.Empty;
                                i.LNS = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(parent.M))
                                i.M = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TM))
                                i.TM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TTM))
                                i.TTM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.NG))
                                i.NG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG))
                                i.TNG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG1))
                                i.TNG1 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG2))
                                i.TNG2 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG3))
                                i.TNG3 = string.Empty;
                        }
                    }
                }

                CheckParent(_listDataReportChiTieuNganh);


                var danhMucItem = dictDanhMucById.GetValueOrDefault(item.Id, new DanhMuc());
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                data.Add("TitleFirst", TxtTitleFirst);
                data.Add("TitleSecond", TxtTitleSecond);
                data.Add("TitleThird", TxtTitleThird);
                data.Add("TenNganh", danhMucItem.STen);
                data.Add("TongSo", _tongChiTieu.TongSo);
                data.Add("CatUnitType", string.Format("Đơn vị tính: {0}", CatUnitTypeSelected.DisplayItem));

                data.Add("TotalTongSoText", StringUtils.NumberToText(_tongChiTieu.TongSo * donViTinh));
                data.Add("TitleCol1", GetHeaderTitle(listCol, 0));
                data.Add("TotalCol1", _tongChiTieu.TongCol1);
                data.Add("TotalCol1Text", StringUtils.NumberToText(_tongChiTieu.TongCol1 * donViTinh));

                if (listCol.Count >= 2)
                {
                    data.Add("TitleCol2", GetHeaderTitle(listCol, 1));
                    data.Add("TotalCol2", _tongChiTieu.TongCol2);
                    data.Add("TotalCol2Text", StringUtils.NumberToText(_tongChiTieu.TongCol2 * donViTinh));
                }
                if (listCol.Count >= 3)
                {
                    data.Add("TitleCol3", GetHeaderTitle(listCol, 2));
                    data.Add("TotalCol3", _tongChiTieu.TongCol3);
                    data.Add("TotalCol3Text", StringUtils.NumberToText(_tongChiTieu.TongCol3 * donViTinh));
                }
                if (listCol.Count >= 4)
                {
                    data.Add("TitleCol4", GetHeaderTitle(listCol, 3));
                    data.Add("TotalCol4", _tongChiTieu.TongCol4);
                    data.Add("TotalCol4Text", StringUtils.NumberToText(_tongChiTieu.TongCol4 * donViTinh));
                }
                if (listCol.Count >= 5)
                {
                    data.Add("TitleCol5", GetHeaderTitle(listCol, 4));
                    data.Add("TotalCol5", _tongChiTieu.TongCol5);
                    data.Add("TotalCol5Text", StringUtils.NumberToText(_tongChiTieu.TongCol5 * donViTinh));
                }
                if (listCol.Count >= 6)
                {
                    data.Add("TitleCol6", GetHeaderTitle(listCol, 5));
                    data.Add("TotalCol6", _tongChiTieu.TongCol6);
                    data.Add("TotalCol6Text", StringUtils.NumberToText(_tongChiTieu.TongCol6 * donViTinh));
                }
                if (listCol.Count >= 7)
                {
                    data.Add("TitleCol7", GetHeaderTitle(listCol, 6));
                    data.Add("TotalCol7", _tongChiTieu.TongCol7);
                    data.Add("TotalCol7Text", StringUtils.NumberToText(_tongChiTieu.TongCol7 * donViTinh));
                }
                data.Add("Items", _listDataReportChiTieuNganh);
                data.Add("Count", 100000);
                string templateFileName = GetFileNameTemplate(listCol.Count().ToString());
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH}_{StringUtils.RemoveAccents(danhMucItem.STen)}_{DateUtils.GetFormatDateReport()}{extension}";
                listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
            }
            return listResult;
        }

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CalculateDataLNS()
        {
            _listDataReportChiTieuNganh.Where(x => x.IsHangCha).Select(x =>
            {
                x.TuChi = 0; x.HienVat = 0; x.DuPhong = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0;
                x.Col1 = 0; x.Col2 = 0; x.Col3 = 0; x.Col4 = 0; x.Col5 = 0; x.Col6 = 0; x.Col7 = 0;
                return x;
            }).ToList();
            foreach (var item in _listDataReportChiTieuNganh.Where(x => !x.IsHangCha && string.IsNullOrEmpty(x.MaDonVi) &&
            ((x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0 || x.Col1 != 0 || x.Col2 != 0 || x.Col3 != 0 || x.Col4 != 0 || x.Col5 != 0 || x.Col6 != 0 || x.Col7 != 0))))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateDataLNSAgency()
        {
            _listDataReportChiTieuNganh.Where(x => x.IsHangCha).Select(x =>
            {
                x.TuChi = 0; x.HienVat = 0; x.DuPhong = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0;
                x.Col1 = 0; x.Col2 = 0; x.Col3 = 0; x.Col4 = 0; x.Col5 = 0; x.Col6 = 0; x.Col7 = 0; x.TongAll = 0;
                return x;
            }).ToList();
            foreach (var item in _listDataReportChiTieuNganh.Where(x => !x.IsHangCha && string.IsNullOrEmpty(x.MaDonVi) &&
            ((x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0 || x.Col1 != 0 || x.Col2 != 0 || x.Col3 != 0 || x.Col4 != 0 || x.Col5 != 0 || x.Col6 != 0 || x.Col7 != 0 || x.TongAll != 0))))
            {
                CalculateParentAgency(item, item);
            }
        }

        private void CalculateParentAgency(ReportChiTieuDuToanDynamicQuery currentItem, ReportChiTieuDuToanDynamicQuery selfItem)
        {
            var parentItem = _listDataReportChiTieuNganh.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuPhong += selfItem.DuPhong;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.TonKho += selfItem.TonKho;
            parentItem.TongAll += selfItem.TongAll;
            parentItem.Col1 += selfItem.Col1;
            parentItem.Col2 += selfItem.Col2;
            parentItem.Col3 += selfItem.Col3;
            parentItem.Col4 += selfItem.Col4;
            parentItem.Col5 += selfItem.Col5;
            parentItem.Col6 += selfItem.Col6;
            parentItem.Col7 += selfItem.Col7;
            CalculateParentAgency(parentItem, selfItem);
        }

        private void CalculateParent(ReportChiTieuDuToanDynamicQuery currentItem, ReportChiTieuDuToanDynamicQuery selfItem)
        {
            var parentItem = _listDataReportChiTieuNganh.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuPhong += selfItem.DuPhong;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.TonKho += selfItem.TonKho;
            parentItem.Col1 += selfItem.Col1;
            parentItem.Col2 += selfItem.Col2;
            parentItem.Col3 += selfItem.Col3;
            parentItem.Col4 += selfItem.Col4;
            parentItem.Col5 += selfItem.Col5;
            parentItem.Col6 += selfItem.Col6;
            parentItem.Col7 += selfItem.Col7;
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateTotal(List<ReportChiTieuDuToanDynamicQuery> listData)
        {
            _tongChiTieu = new DuToanTongChiTieu();
            foreach (ReportChiTieuDuToanDynamicQuery item in listData.Where(x => !x.IsHangCha))
            {
                _tongChiTieu.TongTuChi += item.TuChi.HasValue ? item.TuChi.Value : 0;
                _tongChiTieu.TongHienVat += item.HienVat.HasValue ? item.HienVat.Value : 0;
                _tongChiTieu.TongDuPhong += item.DuPhong.HasValue ? item.DuPhong.Value : 0;
                _tongChiTieu.TongHangNhap += item.HangNhap.HasValue ? item.HangNhap.Value : 0;
                _tongChiTieu.TongHangMua += item.HangMua.HasValue ? item.HangMua.Value : 0;
                _tongChiTieu.TongPhanCap += item.PhanCap.HasValue ? item.PhanCap.Value : 0;
                _tongChiTieu.TongSo += item.TongSo;

                _tongChiTieu.TongCol1 += item.Col1.HasValue ? item.Col1.Value : 0;
                _tongChiTieu.TongCol2 += item.Col2.HasValue ? item.Col2.Value : 0;
                _tongChiTieu.TongCol3 += item.Col3.HasValue ? item.Col3.Value : 0;
                _tongChiTieu.TongCol4 += item.Col4.HasValue ? item.Col4.Value : 0;
                _tongChiTieu.TongCol5 += item.Col5.HasValue ? item.Col5.Value : 0;
                _tongChiTieu.TongCol6 += item.Col6.HasValue ? item.Col6.Value : 0;
                _tongChiTieu.TongCol7 += item.Col7.HasValue ? item.Col7.Value : 0;
            };
        }

        public int GetDonViTinh()
        {
            if (CatUnitTypeSelected == null || string.IsNullOrEmpty(CatUnitTypeSelected.ValueItem))
                return 1;
            return int.Parse(CatUnitTypeSelected.ValueItem);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_NGANH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_CHITIET_NGANH;
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
    }
}

