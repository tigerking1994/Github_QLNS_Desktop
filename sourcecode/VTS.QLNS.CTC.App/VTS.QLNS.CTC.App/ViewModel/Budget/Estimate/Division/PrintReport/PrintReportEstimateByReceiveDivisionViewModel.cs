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
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportEstimateByReceiveDivisionViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly INsDtChungTuService _chungTuChiTiet;
        private readonly INsDtChungTuChiTietService _chungTuChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private ICollectionView _listBudgetIndex;
        private List<NsMucLucNganSach> _listMucLucNganSach;
        private string _cap1;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private List<DuToanThongKeQuyetDinh> _exportData;
        private DuToanThongKeQuyetDinhTotal _total;
        private DmChuKy _dmChuKy;

        public override string Name => "Thống kê phân bổ dự toán theo đợt";
        public override Type ContentType => typeof(PrintReportEstimateByReceiveDivision);
        public override string Description => "Thống kê số liệu dự toán đã phân bổ theo đợt nhận";

        private List<ComboboxItem> _cbxSoQuyetDinh;
        public List<ComboboxItem> CbxSoQuyetDinh
        {
            get => _cbxSoQuyetDinh;
            set => SetProperty(ref _cbxSoQuyetDinh, value);
        }

        private ComboboxItem _cbxSoQuyetDinhSelected;
        public ComboboxItem CbxSoQuyetDinhSelected
        {
            get => _cbxSoQuyetDinhSelected;
            set
            {
                SetProperty(ref _cbxSoQuyetDinhSelected, value);
                if (_cbxSoQuyetDinhSelected != null)
                {
                    if (_cbxSoQuyetDinhSelected.ValueItem != null)
                    {
                        LoadTieuDe();
                        LoadBudgetIndexes();
                    }
                }
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

        public bool? IsAllItemsSelected
        {
            get
            {
                if (BudgetIndexes != null)
                {
                    var selected = BudgetIndexes.Select(item => item.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, BudgetIndexes);
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public bool IsExportEnable
        {
            get
            {
                if (_budgetIndexes != null)
                    return _budgetIndexes.Where(x => x.IsSelected).Count() > 0;
                return false;
            }
        }

        private ObservableCollection<ComboboxItem> _printTypeMLNS = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> PrintTypeMLNS
        {
            get => _printTypeMLNS;
            set => SetProperty(ref _printTypeMLNS, value);
        }

        private ComboboxItem _selectedPrintTypeMLNS;
        public ComboboxItem SelectedPrintTypeMLNS
        {
            get => _selectedPrintTypeMLNS;
            set => SetProperty(ref _selectedPrintTypeMLNS, value);
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes;
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

        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportEstimateByReceiveDivisionViewModel(
            IMapper mapper,
            INsDtChungTuService chungTuService,
            INsDtChungTuChiTietService chungTuChiTietService,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            INsMucLucNganSachService mucLucNganSachService,
            IExportService exportService,
            ISktSoLieuService sktSoLieuService,
            IDmChuKyService dmChuKyService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _mapper = mapper;
            _chungTuChiTiet = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _exportService = exportService;
            _mucLucNganSachService = mucLucNganSachService;
            _logger = logger;
            _sktSoLieuService = sktSoLieuService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelCommand = new RelayCommand(obj => OnExportFile(ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            PrintCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _listMucLucNganSach = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            ReloadData();
            LoadPaperPrintTypes();
            LoadSoQuyetDinh();
            LoadDanhMuc();
            LoadTieuDe();
        }

        public void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"},
            };

            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            PaperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }

        private void ReloadData()
        {
            CbxSoQuyetDinh = new List<ComboboxItem>();
            CbxSoQuyetDinhSelected = new ComboboxItem();
        }

        private void LoadTieuDe()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_THONGKE_THEO_SOQUYETDINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    _dmChuKy = new DmChuKy();
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                    Title1 = _dmChuKy.TieuDe1MoTa;
                else Title1 = "THỐNG KÊ PHÂN BỔ DỰ TOÁN THEO ĐỢT";
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                    Title2 = string.Format(_dmChuKy.TieuDe2MoTa, CbxSoQuyetDinhSelected.ValueItem);
                else Title2 = $"Đợt dự toán {CbxSoQuyetDinhSelected.ValueItem}";

                Title3 = _dmChuKy.TieuDe3MoTa;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadSoQuyetDinh()
        {
            List<string> listSoQuyetDinh = _chungTuChiTiet.FindSoQuyetDinh(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
            foreach (var sqd in listSoQuyetDinh)
            {
                CbxSoQuyetDinh.Add(new ComboboxItem(sqd, sqd));
            }
            CbxSoQuyetDinhSelected = CbxSoQuyetDinh.FirstOrDefault();
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionService.Current.YearOfWork)
                .OrderBy(n => n.SGiaTri).ToList();
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

            string chiTietToi = "NG";
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucQuanLy = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;

                var danhMucMLNS = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                chiTietToi = danhMucMLNS == null ? chiTietToi : danhMucMLNS.SGiaTri;
            }
            PrintTypeMLNS = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
            SelectedPrintTypeMLNS = PrintTypeMLNS.First();
        }

        private void LoadBudgetIndexes()
        {
            List<LNSQuery> lnsQueries = _mucLucNganSachService.FindBySoQuyetDinhDuToan(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                                                                                        _sessionInfo.Budget, CbxSoQuyetDinhSelected.ValueItem);
            List<NsMucLucNganSach> listMucLuc = new List<NsMucLucNganSach>();
            List<string> lns = new List<string>();
            foreach (var item in lnsQueries)
            {
                lns.Add(item.LNS);
            }

            listMucLuc = lns.OrderBy<string, string>((Func<string, string>)(x => x)).Select<string, NsMucLucNganSach>((Func<string, NsMucLucNganSach>)(x => new NsMucLucNganSach()
            {
                Lns = x,
                XauNoiMa = x,
                MoTa = _listMucLucNganSach.FirstOrDefault<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(m => m.XauNoiMa == x))?.MoTa,
                ChiTietToi = _listMucLucNganSach.FirstOrDefault<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(m => m.XauNoiMa == x))?.ChiTietToi
            })).Where<NsMucLucNganSach>((Func<NsMucLucNganSach, bool>)(x => !string.IsNullOrEmpty(x.MoTa))).ToList();

            _budgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLuc);
            OnPropertyChanged(nameof(BudgetIndexes));

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsExportEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                };
            }
            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        /// <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, IEnumerable<NsMuclucNgansachModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private void OnExportFile(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                int dvt = Convert.ToInt32(SelectedUnit.ValueItem);

                List<ReportDuToanThongKeSoQuyetDinhQuery> reportData = _chungTuChiTietService.GetDataReportDuToanThongKeSoQuyetDinh(
                                                                 _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                                                                 _sessionInfo.Budget, CbxSoQuyetDinhSelected.ValueItem,
                                                                 string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToList()),
                                                                 dvt).ToList();


                List<string> listSqd = reportData.Where(x => !string.IsNullOrEmpty(x.MaDonVi)).OrderBy(x => x.SoQuyetDinh).Select(x => x.SoQuyetDinh).Distinct().ToList();

                Dictionary<int, List<string>> dicSqd = new Dictionary<int, List<string>>();
                List<string> temp = new List<string>();
                List<int> pageCount = new List<int>();
                int pageIndex = 1;
                int i = 0;
                for (i = 0; i < listSqd.Count(); i++)
                {
                    if (i == 3)
                        break;
                    temp.Add(listSqd[i]);
                }
                dicSqd.Add(pageIndex, temp);
                pageCount.Add(pageIndex);

                for (i = 3; i < listSqd.Count; i = i + 5)
                {
                    temp = new List<string>();
                    for (int k = i; k < i + 5; k++)
                    {
                        if (listSqd.Count() == k)
                            break;
                        temp.Add(listSqd[k]);
                    }
                    pageIndex += 1;
                    pageCount.Add(pageIndex);
                    dicSqd.Add(pageIndex, temp);
                }

                foreach (var page in pageCount)
                {
                    string exportFilePrefixName = string.Empty;
                    string fileName = string.Empty;
                    string templateName = string.Empty;
                    if (page == 1)
                    {
                        templateName = Path.Combine(GetPath(ExportFileName.RPT_NS_DUTOAN_THONGKE_SOQUYETDINH));
                        exportFilePrefixName = ExportFileName.RPT_NS_DUTOAN_THONGKE_SOQUYETDINH.Split(".").First() + "_" + CbxSoQuyetDinhSelected.ValueItem.Replace("/", "");
                        fileName = StringUtils.CreateExportFileName(exportFilePrefixName);
                    }
                    else
                    {
                        templateName = Path.Combine(GetPath(ExportFileName.RPT_NS_DUTOAN_THONGKE_SOQUYETDINH_TRANG));
                        exportFilePrefixName = ExportFileName.RPT_NS_DUTOAN_THONGKE_SOQUYETDINH_TRANG.Split(".").First() + "_" + page + "_" + CbxSoQuyetDinhSelected.ValueItem.Replace("/", "");
                        fileName = StringUtils.CreateExportFileName(exportFilePrefixName);
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data = ExportPage(page, dicSqd, reportData, dvt, exportType);
                    List<int> hideColumns = ExportExcelHelper<DuToanThongKeQuyetDinh>.HideColumn(SelectedPrintTypeMLNS.ValueItem);
                    var xlsFile = _exportService.Export<DuToanThongKeQuyetDinh, DuToanThongKeQuyetDinhHeader, DuToanThongKeQuyetDinhTotal>(templateName, data, hideColumns);
                    results.Add(new ExportResult(fileName, fileName, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null && result.Count > 0)
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

        public string GetPath(string input)
        {
            if (PaperPrintTypeSelected.ValueItem == "2")
                input = input + "_Ngang";
            return Path.Combine(ExportPrefix.PATH_TL_DT, input + FileExtensionFormats.Xlsx);
        }

        private void CalculateTotal(List<DuToanThongKeQuyetDinh> exportData)
        {
            _total = new DuToanThongKeQuyetDinhTotal();
            foreach (var item in exportData.Where(x => !string.IsNullOrEmpty(x.MaDonVi)))
            {
                _total.TongSoPhanBo1 += item.SoPhanBo1;
                _total.TongSoPhanBo2 += item.SoPhanBo2;
                _total.TongSoPhanBo3 += item.SoPhanBo3;
                _total.TongSoPhanBo4 += item.SoPhanBo4;
                _total.TongSoPhanBo5 += item.SoPhanBo5;
            }
            _total.TongSoDuToan = exportData.Where(x => x.MlnsIdParent == null || x.MlnsIdParent.Equals(Guid.Empty)).Sum(x => x.SoDuToan);
            _total.TongConLai = exportData.Where(x => x.MlnsIdParent == null || x.MlnsIdParent.Equals(Guid.Empty)).Sum(x => x.ConLai);
        }

        private void CalculateDataLNS()
        {
            foreach (var item in _exportData.Where(x => !x.IsHangCha && string.IsNullOrEmpty(x.MaDonVi)))
            {
                var data = _exportData.Where(x => x.XauNoiMa == item.XauNoiMa && !string.IsNullOrEmpty(x.MaDonVi)).ToList();
                item.SoDuToan = data.First().SoDuToanTotal;
                item.ConLai = data.First().ConLaiTotal;
                item.SoPhanBo1 = data.Sum(x => x.SoPhanBo1);
                item.SoPhanBo2 = data.Sum(x => x.SoPhanBo2);
                item.SoPhanBo3 = data.Sum(x => x.SoPhanBo3);
                item.SoPhanBo4 = data.Sum(x => x.SoPhanBo4);
                item.SoPhanBo5 = data.Sum(x => x.SoPhanBo5);
            }
            _exportData.Where(x => x.IsHangCha).Select(x =>
            {
                x.SoDuToan = 0;
                x.ConLai = 0;
                x.SoPhanBo1 = 0;
                x.SoPhanBo2 = 0;
                x.SoPhanBo3 = 0;
                x.SoPhanBo4 = 0;
                x.SoPhanBo5 = 0;
                return x;
            }).ToList();
            foreach (var item in _exportData.Where(x => !x.IsHangCha && string.IsNullOrEmpty(x.MaDonVi)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(DuToanThongKeQuyetDinh currentItem, DuToanThongKeQuyetDinh selfItem)
        {
            var parentItem = _exportData.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.SoDuToan += selfItem.SoDuToan;
            parentItem.ConLai += selfItem.ConLai;
            parentItem.SoPhanBo1 += selfItem.SoPhanBo1;
            parentItem.SoPhanBo2 += selfItem.SoPhanBo2;
            parentItem.SoPhanBo3 += selfItem.SoPhanBo3;
            parentItem.SoPhanBo4 += selfItem.SoPhanBo4;
            parentItem.SoPhanBo5 += selfItem.SoPhanBo5;
            CalculateParent(parentItem, selfItem);
        }

        private Dictionary<string, object> ExportPage(int page, Dictionary<int, List<string>> dicSqd, List<ReportDuToanThongKeSoQuyetDinhQuery> reportData, int dvt, ExportType exportType = ExportType.PDF)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<string> listSqdByPage = dicSqd[page];
            List<string> listXauNoiMaDonVi = new List<string>();


            listXauNoiMaDonVi = reportData.Where(x => !x.BHangCha && !string.IsNullOrEmpty(x.MaDonVi)).Select(x => { string xauNoiMaDonVi = string.Join(StringUtils.DELIMITER, x.XauNoiMa, x.MaDonVi); return xauNoiMaDonVi; }).Distinct().ToList();
            _exportData = new List<DuToanThongKeQuyetDinh>();

            foreach (var item in reportData.Where(x => string.IsNullOrEmpty(x.MaDonVi)))
            {
                var dataDuToan = new DuToanThongKeQuyetDinh
                {
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
                    MlnsId = item.MlnsId,
                    MlnsIdParent = item.MlnsIdCha,
                    MoTa = item.MoTa,
                    IsHangCha = item.BHangCha,
                    SoDuToan = 0,
                    ConLai = 0
                };
                _exportData.Add(dataDuToan);
            }


            foreach (var xauNoiMa in reportData.Where(x => !x.BHangCha).Select(x => x.XauNoiMa).Distinct())
            {
                var xnm = reportData.Where(x => x.XauNoiMa == xauNoiMa && !string.IsNullOrEmpty(x.MaDonVi)).First();

                foreach (var xauNoiMaDonVi in listXauNoiMaDonVi.Where(x => x.Contains(xauNoiMa)))
                {
                    var reportDataByXauNoiMa = reportData.Where(x => string.Join(StringUtils.DELIMITER, x.XauNoiMa, x.MaDonVi) == xauNoiMaDonVi).ToList();
                    var dataDuToan = new DuToanThongKeQuyetDinh
                    {
                        XauNoiMa = xnm.XauNoiMa,
                        MlnsId = xnm.MlnsId,
                        MlnsIdParent = xnm.MlnsIdCha,
                        MoTa = reportDataByXauNoiMa.First().MaDonVi + " - " + reportDataByXauNoiMa.First().TenDonVi,
                        MaDonVi = reportDataByXauNoiMa.First().MaDonVi,
                        SoDuToan = 0,
                        ConLai = 0,
                        SoDuToanTotal = xnm.SoDuToan.HasValue ? xnm.SoDuToan.Value : 0,
                        ConLaiTotal = xnm.ConLai.HasValue ? xnm.ConLai.Value : 0
                    };
                    foreach (var item in reportDataByXauNoiMa)
                    {

                        var index = listSqdByPage.IndexOf(item.SoQuyetDinh);
                        double soPhanBo = item.SoPhanBo.HasValue ? item.SoPhanBo.Value : 0;
                        switch (index)
                        {
                            case 0:
                                dataDuToan.SoPhanBo1 = soPhanBo;
                                break;
                            case 1:
                                dataDuToan.SoPhanBo2 = soPhanBo;
                                break;
                            case 2:
                                dataDuToan.SoPhanBo3 = soPhanBo;
                                break;
                            case 3:
                                dataDuToan.SoPhanBo4 = soPhanBo;
                                break;
                            case 4:
                                dataDuToan.SoPhanBo5 = soPhanBo;
                                break;
                        }
                    }
                    _exportData.Add(dataDuToan);
                }
            }

            _exportData = _exportData.OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaDonVi).ToList();
            CalculateDataLNS();
            CalculateTotal(_exportData);
            switch (SelectedPrintTypeMLNS.ValueItem)
            {
                case nameof(MLNSFiled.NG):
                    _exportData = _exportData.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                    _exportData.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG):
                    _exportData = _exportData.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                    _exportData.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG1):
                    _exportData = _exportData.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                    _exportData.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                    break;
                case nameof(MLNSFiled.TNG2):
                    _exportData = _exportData.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                    _exportData.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                    break;
            }
            foreach (var item in _exportData.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _exportData.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                if (parent != null && item.M != string.Empty)
                {
                    if (!parent.L.IsEmpty() && !parent.K.IsEmpty())
                    {
                        item.L = string.Empty;
                        item.K = string.Empty;
                        item.LNS = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(parent.M))
                        item.M = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TM))
                        item.TM = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TTM))
                        item.TTM = string.Empty;
                    if (!string.IsNullOrEmpty(parent.NG))
                        item.NG = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG))
                        item.TNG = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG1))
                        item.TNG1 = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG2))
                        item.TNG2 = string.Empty;
                    if (!string.IsNullOrEmpty(parent.TNG3))
                        item.TNG3 = string.Empty;
                }
            }

            int iNumber = 0;
            for (int i = 0; i < _exportData.Count; i++)
            {
                if (!string.IsNullOrEmpty(_exportData[i].MaDonVi))
                {
                    iNumber++;
                    _exportData[i].MlnsId = Guid.NewGuid();
                    _exportData[i].MlnsIdParent = _exportData[i - iNumber].MlnsId;
                    _exportData[i].IsMaDonVi = true;

                }
                else
                {
                    iNumber = 0;
                }
            }

            DuToanThongKeQuyetDinhHeader header = new DuToanThongKeQuyetDinhHeader
            {
                Header1 = listSqdByPage.Count > 0 ? listSqdByPage[0] : string.Empty,
                Header2 = listSqdByPage.Count > 1 ? listSqdByPage[1] : string.Empty,
                Header3 = listSqdByPage.Count > 2 ? listSqdByPage[2] : string.Empty,
                Header4 = listSqdByPage.Count > 3 ? listSqdByPage[3] : string.Empty,
                Header5 = listSqdByPage.Count > 4 ? listSqdByPage[4] : string.Empty
            };
            List<DuToanThongKeQuyetDinhHeader> headers = new List<DuToanThongKeQuyetDinhHeader> { header };
            List<DuToanThongKeQuyetDinhTotal> totals = new List<DuToanThongKeQuyetDinhTotal> { _total };
            FormatNumber formatNumber = new FormatNumber(dvt, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("Cap1", _cap1);
            data.Add("Cap2", _sessionInfo.TenDonVi);
            data.Add("Headers", headers);
            data.Add("TieuDe1", Title1);
            data.Add("TieuDe2", Title2);
            data.Add("TieuDe3", Title3);
            data.Add("dvt", SelectedUnit.DisplayItem);
            data.Add("Items", _exportData);
            data.Add("Total", totals);
            data.Add("Count", 100000);

            return data;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_THONGKE_THEO_SOQUYETDINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_THONGKE_THEO_SOQUYETDINH;
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
