using AutoMapper;
using AutoMapper.Internal;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.IO;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Model.Report;
using System.Reflection;
using VTS.QLNS.CTC.App.Helper;
using FlexCel.XlsAdapter;
using Microsoft.Extensions.Configuration;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportTargetMajorAgencyViewModel : ReportViewModelBase<DtChungTuModel, ReportChiTieuDuToanDynamicQuery, ReportChiTieuDuToanDynamicQuery>
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
        private readonly INsDonViService _nSDonViService;
        private readonly IConfiguration _configuration;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private DuToanTongChiTieu _tongChiTieu;
        private List<ReportChiTieuDuToanDynamicQuery> _listDataReportChiTieuNganh;
        private List<DonVi> _dataDonVi;
        private Dictionary<string, int> lstColCount;
        private readonly string _templatePath;

        public override string Name
        {
            get => "In chỉ tiêu - Ngành theo số quyết định cho đơn vị";
        }

        public override string Title
        {
            get => "In chỉ tiêu - Ngành theo số quyết định cho đơn vị";
        }

        public override string Description
        {
            get => "Chọn thông số in chỉ tiêu dự toán cho Ngành bảo đảm theo số quyết định cho đơn vị";
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportTargetMajorAgency);

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

        private ObservableCollection<ComboboxItem> _cbxTitleFirst = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> CbxTitleFirst
        {
            get => _cbxTitleFirst;
            set => SetProperty(ref _cbxTitleFirst, value);
        }

        private ComboboxItem _cbxTitleFirstSelected;

        public ComboboxItem CbxTitleFirstSelected
        {
            get => _cbxTitleFirstSelected;
            set => SetProperty(ref _cbxTitleFirstSelected, value);
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

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

        public RelayCommand ConfigSignCommand { get; }

        public PrintReportTargetMajorAgencyViewModel(
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDtNhanPhanBoMapService dtChungTuMapService,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            ISktSoLieuService sktSoLieuService,
            INsMucLucNganSachService nSMucLucNganSachService,
            INsDonViService nSDonViService,
            IConfiguration configuration,
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
            LoadCbxTitleFirst();
            LoadPaperPrintTypes();
            LoadCatUnitTypes();
            LoadSpecialized();
            LoadAgency();
        }

        private void LoadAgency()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            _dataDonVi = _nSDonViService.FindByCondition(predicate).ToList();
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
                    ComboboxManyItem itemCombobox = new ComboboxManyItem();
                    itemCombobox.DisplayItem = soQD;
                    itemCombobox.ValueItem = string.Join(",", chungTus.Select(n => n.Id.ToString()).ToList());
                    itemCombobox.DisplayItem2 = string.Join(",", chungTus.Select(n => n.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")).ToList());
                    sourceCombobox.Add(itemCombobox);
                }

                List<string> listNgayChungTu = listDtChungTu.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh)).Select(n => n.DNgayChungTu.Value.ToString("dd/MM/yyyy")).Distinct().ToList();
                foreach (string ngayChungTu in listNgayChungTu)
                {
                    List<NsDtChungTu> chungTus = listDtChungTu.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.Value.ToString("dd/MM/yyyy") == ngayChungTu).ToList();
                    ComboboxManyItem itemCombobox = new ComboboxManyItem();
                    itemCombobox.DisplayItem = ngayChungTu;
                    itemCombobox.ValueItem = string.Join(",", chungTus.Select(n => n.Id.ToString()).ToList());

                    sourceCombobox.Add(itemCombobox);
                }
                Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDtChungTu);
                DataDot = new ObservableCollection<ComboboxManyItem>(sourceCombobox);
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
            //predicate = predicate.And(x => x.ILoaiChungTu.HasValue && x.ILoaiChungTu.Value == int.Parse(CbxVoucherTypeSelected.ValueItem));
            return predicate;
        }

        private void LoadCbxTitleFirst()
        {
            var cbxTitleFirst = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Dự toán chi ngân sách năm " + _sessionInfo.YearOfWork, ValueItem = "1"},
            };
            CbxTitleFirst = new ObservableCollection<ComboboxItem>(cbxTitleFirst);
            CbxTitleFirstSelected = cbxTitleFirst.ElementAt(0);
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

        public string GetFileNameTemplate(int numberAgency)
        {
            var paper = "Normal";
            if (PaperPrintTypeSelected.ValueItem.Equals("2"))
            {
                paper = "A4Ngang";
            }
            return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY}_{numberAgency.ToString()}_{paper}{string.Empty}{StringUtils.EXCEL_EXTENSION}");
        }

        private string GetHeaderTitle(List<string> list, string maDonVi)
        {
            if (_dataDonVi != null && _dataDonVi.Count > 0)
            {
                DonVi item = _dataDonVi.Where(n => n.IIDMaDonVi == maDonVi).FirstOrDefault();
                return item != null ? item.TenDonVi.Trim() : string.Empty;
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
                                                                        true).ToList();
            return listDataReportChiTieuNganh.Where(n => !string.IsNullOrEmpty(n.MaDonVi));
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
            Export(obj, ExportType.BROWSER);
        }

        private void ConfigTemplateFile(ref XlsFile template, List<ReportChiTieuDuToanDynamicQuery> dataTableExport, string templateName)
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
            int hiddenColWidth = 0;

            if (_countCol == 1)
            {
                if (PaperPrintTypeSelected.ValueItem.Equals("2"))
                {
                    width = 9000;
                    hiddenColWidth = 1350;
                }
                else
                {
                    width = 6000;
                    hiddenColWidth = 1050;
                }
            }
            else if (_countCol == 2)
            {
                if (PaperPrintTypeSelected.ValueItem.Equals("2"))
                {
                    width = 9000;
                    hiddenColWidth = 1350;
                }
                else
                {
                    width = 5000;
                    hiddenColWidth = 850;
                }
            }
            else if (_countCol == 3)
            {
                if (PaperPrintTypeSelected.ValueItem.Equals("2"))
                {
                    width = 9000;
                    hiddenColWidth = 1350;
                }
                else
                {
                    width = 5000;
                    hiddenColWidth = 850;
                }
            }

            if (dataTableExport.Where(n => !string.IsNullOrEmpty(n.TNG3)).Count() == 0)
            {
                width += hiddenColWidth;
                template.SetColHidden(9, true);
            }
            if (dataTableExport.Where(n => !string.IsNullOrEmpty(n.TNG2)).Count() == 0)
            {
                width += hiddenColWidth;
                template.SetColHidden(8, true);
            }
            if (dataTableExport.Where(n => !string.IsNullOrEmpty(n.TNG1)).Count() == 0)
            {
                width += hiddenColWidth;
                template.SetColHidden(7, true);
            }
            if (dataTableExport.Where(n => !string.IsNullOrEmpty(n.TNG)).Count() == 0)
            {
                width += hiddenColWidth;
                template.SetColHidden(6, true);
            }
            template.SetColWidth(10, width);
        }

        public override void Export(object obj, ExportType type)
        {
            lstColCount = new Dictionary<string, int>();
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
                foreach (var item in dataExport)
                {
                    string templatePath = Path.Combine(_templatePath, item.Item1);
                    XlsFile xlsFileExport = new XlsFile(templatePath, true);
                    ConfigTemplateFile(ref xlsFileExport, dataTableExport, item.Item1);


                    //xlsFileExport.SetColWidth(2, 3000);
                    var xlsFile = _exportService.Export<ReportChiTieuDuToanDynamicQuery>(xlsFileExport, item.Item3);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Item2);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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

        private List<ReportChiTieuDuToanDynamicQuery> ProcessDynamicAgency(List<ReportChiTieuDuToanDynamicQuery> data, List<string> lstCol)
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
                    item.Col1 = data.Where(n => n.XauNoiMa == item.XauNoiMa && n.MaDonVi == lstCol.ElementAt(0)).Sum(n => n.TongSoOld);
                }
                if (lstCol.Count >= 2)
                {
                    item.Col2 = data.Where(n => n.XauNoiMa == item.XauNoiMa && n.MaDonVi == lstCol.ElementAt(1)).Sum(n => n.TongSoOld);
                }
                if (lstCol.Count >= 3)
                {
                    item.Col3 = data.Where(n => n.XauNoiMa == item.XauNoiMa && n.MaDonVi == lstCol.ElementAt(2)).Sum(n => n.TongSoOld);
                }
            }
            return listResult;
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

                List<ReportChiTieuDuToanDynamicQuery> listExportDetailAgency = ProcessDynamicAgency(listDataExport, lstDonVi);
                foreach (var item in listSpecializedChecked)
                {
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
                                    MoTa = mlnsItem.MoTa,
                                    XauNoiMa = mlnsItem.XauNoiMa,
                                    HienVat = 0,
                                    TuChi = 0,
                                    IsHangCha = true,
                                    MlnsId = mlnsItem.MlnsId,
                                    MlnsIdParent = mlnsItem.MlnsIdParent
                                });
                            }
                            foreach (ReportChiTieuDuToanDynamicQuery exportItem in dataExportOfSpecialized)
                            {
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == exportItem.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    exportItem.MlnsIdParent = listHeader[index].MlnsId;
                                    if (!string.IsNullOrEmpty(exportItem.MaDonVi))
                                    {
                                        //exportItem.TTM = string.Empty;

                                        //exportItem.MoTa = exportItem.TenDonVi;
                                    }
                                    listHeader.Insert(index + 1, exportItem);
                                }
                            }
                        }
                        _listDataReportChiTieuNganh = new List<ReportChiTieuDuToanDynamicQuery>(listHeader);
                        _listDataReportChiTieuNganh.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                        _listDataReportChiTieuNganh.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                        _listDataReportChiTieuNganh.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();
                    }
                    CalculateDataLNS();
                    var danhMucItem = dictDanhMucById.GetValueOrDefault(item.Id, new DanhMuc());
                    var data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TitleFirst", CbxTitleFirstSelected.DisplayItem);
                    data.Add("TitleSecond", TxtTitleSecond);
                    data.Add("TenNganh", danhMucItem.STen);
                    data.Add("TongSo", tongSoAll);
                    data.Add("CatUnitType", string.Format("Đơn vị tính: {0}", CatUnitTypeSelected.DisplayItem));
                    data.Add("TitleCol1", GetHeaderTitle(lstDonVi, lstDonVi.ElementAt(0)));
                    data.Add("TotalCol1", _tongChiTieu.TongCol1);
                    data.Add("TotalCol1Text", StringUtils.NumberToText(_tongChiTieu.TongCol1 * donViTinh));

                    if (lstDonVi.Count >= 2)
                    {
                        data.Add("TitleCol2", GetHeaderTitle(lstDonVi, lstDonVi.ElementAt(1)));
                        data.Add("TotalCol2", _tongChiTieu.TongCol2);
                        data.Add("TotalCol2Text", StringUtils.NumberToText(_tongChiTieu.TongCol2 * donViTinh));
                    }

                    if (lstDonVi.Count >= 3)
                    {
                        data.Add("TitleCol3", GetHeaderTitle(lstDonVi, lstDonVi.ElementAt(2)));
                        data.Add("TotalCol3", _tongChiTieu.TongCol3);
                        data.Add("TotalCol3Text", StringUtils.NumberToText(_tongChiTieu.TongCol3 * donViTinh));
                    }

                    data.Add("Items", _listDataReportChiTieuNganh);
                    string templateFileName = GetFileNameTemplate(lstDonVi.Count());
                    if (!lstColCount.ContainsKey(templateFileName))
                        lstColCount.Add(templateFileName, lstDonVi.Count());
                    var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY}_{countPage.ToString()}_{StringUtils.RemoveAccents(danhMucItem.STen)}_{DateUtils.GetFormatDateReport()}{extension}";
                    listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
                }
                countPage++;
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
                x.Col1 = 0; x.Col2 = 0; x.Col3 = 0; x.Col4 = 0; x.Col5 = 0; x.Col6 = 0; x.Col7 = 0; x.TongAll = 0;
                return x;
            }).ToList();
            foreach (var item in _listDataReportChiTieuNganh.Where(x => !x.IsHangCha && string.IsNullOrEmpty(x.MaDonVi) &&
            ((x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0 || x.Col1 != 0 || x.Col2 != 0 || x.Col3 != 0 || x.Col4 != 0 || x.Col5 != 0 || x.Col6 != 0 || x.Col7 != 0 || x.TongAll != 0))))
            {
                CalculateParent(item, item);
            }
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
            parentItem.TongAll += selfItem.TongAll;
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
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}


