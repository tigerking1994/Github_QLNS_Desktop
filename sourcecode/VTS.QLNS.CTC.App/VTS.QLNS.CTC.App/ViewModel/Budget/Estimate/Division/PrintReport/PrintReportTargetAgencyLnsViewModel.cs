using AutoMapper;
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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportTargetAgencyLnsViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DuToanChiTieuLNS>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _lnsView;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _cap1;
        private string _diaDiem;

        public override string Name
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY];
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportTargetMajors);

        private bool _isExportData;
        public bool IsExportData
        {
            get => ListLNS.Any() && ListLNS.Any(x => x.IsSelected);
            set => SetProperty(ref _isExportData, value);
        }

        // start handle chon dot
        private ObservableCollection<ComboboxManyItem> _dataDot = new ObservableCollection<ComboboxManyItem>();
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
                    LoadLNS();
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
                LoadLNS();
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

        private bool _checkPrintTNG;
        public bool CheckPrintTNG
        {
            get => _checkPrintTNG;
            set => SetProperty(ref _checkPrintTNG, value);
        }

        // start handle cover sheet
        private ObservableCollection<NsMuclucNgansachModel> _listLNS = new ObservableCollection<NsMuclucNgansachModel>();
        public ObservableCollection<NsMuclucNgansachModel> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        private string _labelSelectedCountLNS;
        public string LabelSelectedCountLNS
        {
            get => $"CHỌN LNS ({ListLNS.Count(item => item.IsSelected)}/{ListLNS.Count})";
            set => SetProperty(ref _labelSelectedCountLNS, value);
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => ListLNS.Any() && ListLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                foreach (var item in ListLNS) item.IsSelected = _selectAllLNS;
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _lnsView.Refresh();
                }
            }
        }

        public RelayCommand ConfigSignCommand { get; }

        public PrintReportTargetAgencyLnsViewModel(
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel) : base(exportService, danhMucService, sessionService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            if (Model == null)
            {
                Model = new DtChungTuModel();
            }

            LoadDataDot();
            if (!Models.Any())
            {
                return;
            }
            InitReportDefaultDate();
            LoadTieuDe();
            LoadPaperPrintTypes();
            LoadCatUnitTypes();
            LoadLNS();
            LoadDanhMuc();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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

        private void LoadDanhMuc()
        {
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadDataDot()
        {
            if (Models == null || !Models.Any())
            {
                var predicate = CreatePredicateChungTuIndex();
                var listDtChungTu = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
                Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDtChungTu);
            }

            DataDot = _mapper.Map<ObservableCollection<ComboboxManyItem>>(Models);
            var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem2)).ToList();
            DataDot = new ObservableCollection<ComboboxManyItem>(ordered);
            if (DataDot != null && DataDot.Count() > 0)
                _dataDotSelected = DataDot.FirstOrDefault();
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            if (Model != null && Model.ILoaiChungTu.HasValue)
            {
                predicate = predicate.And(x => x.ILoaiChungTu.HasValue && x.ILoaiChungTu.Equals(Model.ILoaiChungTu));
            }
            return predicate;
        }

        public override void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A3 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A3 ngang", ValueItem = "2"}
            };

            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            PaperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }

        private void LoadLNS()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var dtChungTuSelected = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            List<string> listLNS = new List<string>();
            var lns = _dtChungTuChiTietService.GetLnsHasData(new List<Guid> { dtChungTuSelected.Id });
            if (_checkPrintAccumulation)
            {
                List<Guid> listId = Models.Where(x => x.DNgayChungTu.HasValue && x.DNgayChungTu <= DateUtils.EndOfDay(dtChungTuSelected.DNgayChungTu.Value)).Select(x => x.Id).ToList();
                listLNS = _dtChungTuChiTietService.GetLnsHasData(listId).ToList();
                var listLNSNhan = GetTotalBudgetAvailable().Where(x => x.HasData).Select(x => x.SLns).Distinct().ToList();
                listLNS = listLNS.Union(listLNSNhan).ToList();
            }
            else
                listLNS = _dtChungTuChiTietService.GetLnsHasData(new List<Guid> { dtChungTuSelected.Id }).ToList();

            var listNsMucLucNganSach = _nsMucLucNganSachService.FindByListLnsDonVi(string.Join(",", listLNS), yearOfWork).ToList();

            ListLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);
            _lnsView = CollectionViewSource.GetDefaultView(ListLNS);
            _lnsView.Filter = obj => string.IsNullOrWhiteSpace(_searchLNS)
                                     || (obj is NsMuclucNgansachModel item && item.LNSDisplay.Contains(_searchLNS.Trim(), StringComparison.OrdinalIgnoreCase));

            OnPropertyChanged(nameof(SelectAllLNS));
            OnPropertyChanged(nameof(LabelSelectedCountLNS));
            OnPropertyChanged(nameof(IsExportData));
            if (_listLNS != null && _listLNS.Count > 0)
            {
                foreach (var model in _listLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                        {
                            foreach (var item in _listLNS)
                            {
                                if (item.MlnsIdParent == model.MlnsId)
                                {
                                    item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(LabelSelectedCountLNS));
                            OnPropertyChanged(nameof(IsExportData));
                        }
                    };
                }
            }
        }

        public override string GetFileTemplate(string strPageNumber = "")
        {
            var paper = "Normal";
            if (PaperPrintTypeSelected.ValueItem.Equals("2"))
            {
                paper = "A3Ngang";
            }
            if (CheckPrintAccumulation)
            {
                return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_LNS}_{paper}{strPageNumber}_LuyKe{StringUtils.EXCEL_EXTENSION}");
            }
            else
            {
                return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_LNS}_{paper}{strPageNumber}{StringUtils.EXCEL_EXTENSION}");
            }
        }

        public override void Export(object obj, ExportType type)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
                switch (type)
                {
                    case ExportType.EXCEL:
                        dataExport = ConvertDataExport(GetData(), GetTotalBudgetAvailable(), StringUtils.EXCEL_EXTENSION);
                        break;
                    case ExportType.PDF:
                        dataExport = ConvertDataExport(GetData(), GetTotalBudgetAvailable(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.WORD:
                        break;
                    case ExportType.BROWSER:
                        dataExport = ConvertDataExport(GetData(), GetTotalBudgetAvailable(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.SIGNATURE:
                        break;
                }
                foreach (var item in dataExport.Select((value, index) => new { index, value }))
                {
                    var itemValue = item.value;
                    var itemIndex = item.index;
                    List<int> hideColumns = new List<int>();
                    if (SelectedPrintTypeMLNS != null)
                    {
                        hideColumns = ExportExcelHelper<DuToanChiTieuLNS>.HideColumn(SelectedPrintTypeMLNS.ValueItem);
                    }

                    if (itemIndex != 0)
                    {
                        hideColumns.Add(3);
                    }

                    var xlsFile = _exportService.Export<DuToanChiTieuLNS, DuToanChiTieuLNSDynamicColumn>(itemValue.Item1, itemValue.Item3, hideColumns);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(itemValue.Item2);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            },
            (s, e) =>
            {
                if (e.Error == null && (type.Equals(ExportType.EXCEL) || type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER)))
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result.Count() == 0)
                        MessageBoxHelper.Info(Resources.AlertEmptyReport);
                    else _exportService.Open(result, type.Equals(ExportType.EXCEL) ? ExportType.EXCEL : ExportType.PDF);
                }
                IsLoading = false;
            });
            //HandleAfterExport();
        }

        private List<DtChungTuChiTietModel> GetTotalBudgetAvailable()
        {
            var ctFirst = Models.FirstOrDefault();
            DateTime voucherDate = DateTime.Now;
            int voucherIndex = 0;

            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var chungTuIdAccumulation = Models.Where(x => x.DNgayChungTu.HasValue && x.DNgayChungTu <= DateUtils.EndOfDay(dataDotSelectedModel.DNgayChungTu.Value));

            if (!CheckPrintAccumulation)
            {
                if (chungTuIdAccumulation.Any(x => x.DNgayQuyetDinh.HasValue))
                    voucherDate = Models.Where(x => x.DNgayQuyetDinh.HasValue).Min(x => x.DNgayQuyetDinh.Value);
                else if (chungTuIdAccumulation.Any(x => x.DNgayChungTu.HasValue))
                    voucherDate = chungTuIdAccumulation.Where(x => x.DNgayChungTu.HasValue).Min(x => x.DNgayChungTu.Value);
                voucherIndex = chungTuIdAccumulation.Min(x => x.ISoChungTuIndex.Value);
            }
            else
            {
                if (chungTuIdAccumulation.Any(x => x.DNgayQuyetDinh.HasValue))
                    voucherDate = chungTuIdAccumulation.Where(x => x.DNgayQuyetDinh.HasValue).Max(x => x.DNgayQuyetDinh.Value);
                else if (chungTuIdAccumulation.Any(x => x.DNgayChungTu.HasValue))
                    voucherDate = chungTuIdAccumulation.Where(x => x.DNgayChungTu.HasValue).Max(x => x.DNgayChungTu.Value);
            }
            var budgetEstimateDivisionCondition = new EstimationVoucherDetailCriteria
            {
                ChungTuId = string.Join(",", chungTuIdAccumulation.Select(x => x.Id)),
                LNS = string.Join(",", chungTuIdAccumulation.Select(x => x.SDslns)),
                YearOfWork = ctFirst.INamLamViec,
                YearOfBudget = ctFirst.INamNganSach,
                BudgetSource = ctFirst.IIdMaNguonNganSach,
                VoucherDate = voucherDate,
                VoucherIndex = voucherIndex,
                IsLuyKe = CheckPrintAccumulation,
                DonViTinh = int.Parse(CatUnitTypeSelected.ValueItem)
            };

            var listDetail = _dtChungTuChiTietService.FindBudgetEstimateDivisionBySoQuyetDinh(budgetEstimateDivisionCondition).ToList();
            var listDetailModel = _mapper.Map<List<DtChungTuChiTietModel>>(listDetail);
            return listDetailModel;
        }


        public override IEnumerable<DtChungTuChiTietModel> GetData()
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var chungTuId = dataDotSelectedModel.Id.ToString();
            if (_checkPrintAccumulation)
            {
                var chungTuIdAccumulation = string.Join(StringUtils.COMMA, Models.Where(x =>
                {
                    if (x.DNgayQuyetDinh.HasValue)
                    {
                        return x.DNgayQuyetDinh.Value <= DateUtils.EndOfDay(dataDotSelectedModel.DNgayChungTu.Value);
                    }
                    else
                    {
                        return x.DNgayChungTu.HasValue && x.DNgayChungTu <= DateUtils.EndOfDay(dataDotSelectedModel.DNgayChungTu.Value);
                    }
                }
                )
                    .Select(x => x.Id.ToString()));
                chungTuId = string.Join(StringUtils.COMMA, chungTuId, chungTuIdAccumulation);
            }

            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Guid.Empty,
                ChungTuId = chungTuId,
                LNS = string.Join(StringUtils.COMMA, ListLNS.Select(x => x.Lns)),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                VoucherDate = dataDotSelectedModel.DNgayChungTu.HasValue ? dataDotSelectedModel.DNgayChungTu : DateTime.Now,
                SoChungTu = string.IsNullOrEmpty(dataDotSelectedModel.SSoChungTu) ? string.Empty : dataDotSelectedModel.SSoChungTu,
                DonViTinh = int.Parse(CatUnitTypeSelected.ValueItem)
            };

            Func<NsDtChungTuChiTietQuery, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

            var listChungTuChiTiet = _dtChungTuChiTietService.FindByCond(searchCondition, procedure: "rpt_du_toan_chi_tieu_LNS").Where(x => x.BHangCha || hasData.Invoke(x)).ToList();
            var listChungTuChiTietModel = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet);

            return listChungTuChiTietModel;
        }

        public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<DtChungTuChiTietModel> listData, IEnumerable<DtChungTuChiTietModel> listDataNhan, string extension)
        {
            if (extension == StringUtils.PDF_EXTENSION)
            {
                return ConvertDataExportPDF(listData, listDataNhan, extension);
            }
            else
            {
                return ConvertDataExportExcel(listData, listDataNhan, extension);
            }
        }
        public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExportPDF(IEnumerable<DtChungTuChiTietModel> listData, IEnumerable<DtChungTuChiTietModel> listDataNhan, string extension)
        {
            var idDonViLogin = _sessionService.Current.IdDonVi;
            var dataGroupByDonVi = listData.GroupBy(x => x.IIdMaDonVi).ToDictionary(x => x.Key, x => x.ToList());
            dataGroupByDonVi = dataGroupByDonVi.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value.OrderBy(i => i.STenDonVi).ToList());

            var dataGroupByDonViNhan = listDataNhan.GroupBy(x => x.SLns.ToString()).ToDictionary(x => x.Key, x => x.ToList());

            var lnsSelected = ListLNS.Where(x => x.IsSelected);
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));

            Func<DtChungTuChiTietModel, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

            var pageSize = 6;
            if (PaperPrintTypeSelected.ValueItem.Equals("2"))
            {
                pageSize = 10;
            }

            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            var dictLnsByPageSize = lnsSelected.Select((x, i) => new { Group = i / pageSize, Value = x })
                .GroupBy(item => item.Group, g => g.Value)
                .ToDictionary(x => x.Key, x => x.ToList());

            var catUnitTypeStr = "Tờ số: {0} Đơn vị tính: " + CatUnitTypeSelected.DisplayItem;
            var catUnitType = Convert.ToInt32(CatUnitTypeSelected.ValueItem);

            foreach (var dictByPage in dictLnsByPageSize)
            {
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                /* 
                data.Add("Cap1", dictByPage.Key > 0 ? string.Empty : _cap1);
                data.Add("Cap2", dictByPage.Key > 0 ? string.Empty : _sessionService.Current.TenDonVi);
                data.Add("TitleFirst", dictByPage.Key > 0 ? string.Empty : TxtTitleFirst);
                data.Add("TitleSecond", dictByPage.Key > 0 ? string.Empty : TxtTitleSecond);
                data.Add("TitleThird", dictByPage.Key > 0 ? string.Empty : TxtTitleThird);
                 */

                data.Add("Cap1", _cap1);
                data.Add("Cap2", _sessionService.Current.TenDonVi);
                data.Add("TitleFirst", TxtTitleFirst);
                data.Add("TitleSecond", TxtTitleSecond);
                data.Add("TitleThird", TxtTitleThird);
                data.Add("CatUnitType", string.Format(catUnitTypeStr, dictByPage.Key + 1));

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);
                for (var i = 0; i < pageSize; i++)
                {
                    data.Add($"MoTa{i + 1}", dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel()).MoTa);
                    data.Add($"Lns{i + 1}", dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel()).Lns);
                }

                var dataExport = new List<DuToanChiTieuLNS>();
                foreach (var item in dataGroupByDonVi)
                {
                    var dictDonViByMlns = item.Value.GroupBy(x => x.SLns.ToString()).ToDictionary(x => x.Key, x => x.ToList());
                    dataGroupByDonVi.TryGetValue(item.Key, out var temp);
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel());
                        dictValue.Add(i, dictDonViByMlns.GetValueOrDefault(string.IsNullOrEmpty(mlnsByCol.Lns) ? string.Empty : mlnsByCol.Lns, new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuLns = new DuToanChiTieuLNS();
                    dataExport.Add(duToanChiTieuLns);

                    duToanChiTieuLns.TenDonVi = item.Value.First().STenDonVi;
                    duToanChiTieuLns.Val = item.Value.Sum(GetDataByType)  ;
                    CalculateDataExportDynamicCol(duToanChiTieuLns, dictValue, catUnitType);
                }

                double[] totalVal = new double[11];
                double[] totalValConLai = new double[11];
                totalVal[0] = dataExport.Sum(x => x.Val);
                totalVal[1] = dataExport.Sum(x => x.Val1);
                totalVal[2] = dataExport.Sum(x => x.Val2);
                totalVal[3] = dataExport.Sum(x => x.Val3);
                totalVal[4] = dataExport.Sum(x => x.Val4);
                totalVal[5] = dataExport.Sum(x => x.Val5);
                totalVal[6] = dataExport.Sum(x => x.Val6);
                totalVal[7] = dataExport.Sum(x => x.Val7);
                totalVal[8] = dataExport.Sum(x => x.Val8);
                totalVal[9] = dataExport.Sum(x => x.Val9);
                totalVal[10] = dataExport.Sum(x => x.Val10);

                if (CheckPrintAccumulation)
                {
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel());
                        totalValConLai[i + 1] = dataGroupByDonViNhan.GetValueOrDefault(string.IsNullOrEmpty(mlnsByCol.Lns) ? string.Empty : mlnsByCol.Lns, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
                    }

                    totalValConLai[0] = dataGroupByDonViNhan.Sum(x => x.Value.Sum(GetDataByType))  ;

                    data.Add("TotalValConLai", totalValConLai[0] - totalVal[0]);
                    data.Add("TotalVal1ConLai", totalValConLai[1] - totalVal[1]);
                    data.Add("TotalVal2ConLai", totalValConLai[2] - totalVal[2]);
                    data.Add("TotalVal3ConLai", totalValConLai[3] - totalVal[3]);
                    data.Add("TotalVal4ConLai", totalValConLai[4] - totalVal[4]);
                    data.Add("TotalVal5ConLai", totalValConLai[5] - totalVal[5]);
                    data.Add("TotalVal6ConLai", totalValConLai[6] - totalVal[6]);
                    data.Add("TotalVal7ConLai", totalValConLai[7] - totalVal[7]);
                    data.Add("TotalVal8ConLai", totalValConLai[8] - totalVal[8]);
                    data.Add("TotalVal9ConLai", totalValConLai[9] - totalVal[9]);
                    data.Add("TotalVal10ConLai", totalValConLai[10] - totalVal[10]);
                }

                data.Add("TotalVal", totalVal[0]);
                data.Add("TotalVal1", totalVal[1]);
                data.Add("TotalVal2", totalVal[2]);
                data.Add("TotalVal3", totalVal[3]);
                data.Add("TotalVal4", totalVal[4]);
                data.Add("TotalVal5", totalVal[5]);
                data.Add("TotalVal6", totalVal[6]);
                data.Add("TotalVal7", totalVal[7]);
                data.Add("TotalVal8", totalVal[8]);
                data.Add("TotalVal9", totalVal[9]);
                data.Add("TotalVal10", totalVal[10]);
                data.Add("Items", dataExport);
                //data.Add("DiaDiem", dictByPage.Key > 0 ? string.Empty : _diaDiem);
                //data.Add("Ngay", dictByPage.Key > 0 ? string.Empty : DateUtils.FormatDateReport(ReportDate));
                data.Add("DiaDiem", _diaDiem);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);

                data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);

                data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                var strPage = dictByPage.Key > 0 ? "_To1" : string.Empty;
                var strPageNumber = dictByPage.Key > 0 ? $"_To{dictByPage.Key + 1}" : string.Empty;

                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_LNS}_{idDonViLogin}_{DateUtils.GetFormatDateReport()}{strPageNumber}{extension}";
                var templateFileName = GetFileTemplate(strPage);
                listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
            }

            return listResult;
        }

        public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExportExcel(IEnumerable<DtChungTuChiTietModel> listData, IEnumerable<DtChungTuChiTietModel> listDataNhan, string extension)
        {
            var idDonViLogin = _sessionService.Current.IdDonVi;
            var dataGroupByDonVi = listData.GroupBy(x => x.IIdMaDonVi).ToDictionary(x => x.Key, x => x.ToList());
            dataGroupByDonVi = dataGroupByDonVi.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value.OrderBy(i => i.STenDonVi).ToList());

            var lnsSelected = ListLNS.Where(x => x.IsSelected);
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));

            Func<DtChungTuChiTietModel, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

            int pageSize = lnsSelected.Count();

            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            var dictLnsByPageSize = lnsSelected.Select((x, i) => new { Group = i / pageSize, Value = x })
                .GroupBy(item => item.Group, g => g.Value)
                .ToDictionary(x => x.Key, x => x.ToList());

            var catUnitTypeStr = "Tờ số: {0} Đơn vị tính: " + CatUnitTypeSelected.DisplayItem;
            var catUnitType = Convert.ToInt32(CatUnitTypeSelected.ValueItem);

            List<DuToanChiTieuLNSDynamicColumn> lstheader = new List<DuToanChiTieuLNSDynamicColumn>();
            List<DuToanChiTieuLNSDynamicColumn> lsttotal = new List<DuToanChiTieuLNSDynamicColumn>();

            foreach (var dictByPage in dictLnsByPageSize)
            {
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", _cap1);
                data.Add("Cap2", _sessionService.Current.TenDonVi);
                data.Add("TitleFirst", TxtTitleFirst);
                data.Add("TitleSecond", TxtTitleSecond);
                data.Add("TitleThird", TxtTitleThird);
                data.Add("CatUnitType", string.Format(catUnitTypeStr, dictByPage.Key + 1));

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                //Add Header
                for (var i = 0; i < pageSize; i++)
                {
                    DuToanChiTieuLNSDynamicColumn headerCol = new DuToanChiTieuLNSDynamicColumn();
                    headerCol.sMucLucNganSach = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel()).MoTa;
                    headerCol.sLNS = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel()).Lns;
                    lstheader.Add(headerCol);
                }

                var dataExport = new List<DuToanChiTieuLNS>();
                foreach (var item in dataGroupByDonVi)
                {
                    var dictDonViByMlns = item.Value.GroupBy(x => x.SLns.ToString()).ToDictionary(x => x.Key, x => x.ToList());
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel());
                        dictValue.Add(i, dictDonViByMlns.GetValueOrDefault(string.IsNullOrEmpty(mlnsByCol.Lns) ? string.Empty : mlnsByCol.Lns, new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuLns = new DuToanChiTieuLNS();
                    dataExport.Add(duToanChiTieuLns);

                    duToanChiTieuLns.TenDonVi = item.Value.First().STenDonVi;
                    duToanChiTieuLns.Val = item.Value.Sum(GetDataByType)  ;


                    //Add list column dynamic
                    duToanChiTieuLns.LstGiaTri = new List<DuToanChiTieuLNSDynamicColumn>();
                    for (var i = 0; i < pageSize; i++)
                    {
                        duToanChiTieuLns.LstGiaTri.Add(new DuToanChiTieuLNSDynamicColumn
                        {
                            sMucLucNganSach = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel()).MoTa,
                            sLNS = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel()).Lns,
                            fTuChi = dictValue.GetValueOrDefault(i, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  
                        });
                    }

                }

                //Add Total
                for (var i = 0; i < pageSize; i++)
                {
                    DuToanChiTieuLNSDynamicColumn totalCol = new DuToanChiTieuLNSDynamicColumn();
                    string sLNS = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel()).Lns;
                    totalCol.sLNS = sLNS;
                    totalCol.sMucLucNganSach = dictByIndexCol.GetValueOrDefault(i, new NsMuclucNgansachModel()).MoTa;
                    totalCol.fTuChi = listData.Where(x => x.SLns == sLNS).Sum(x => x.FTuChi);
                    lsttotal.Add(totalCol);

                }

                if (pageSize < 6)
                {
                    for (int i = 0; i < 6 - pageSize; i++)
                    {
                        DuToanChiTieuLNSDynamicColumn col = new DuToanChiTieuLNSDynamicColumn();
                        lstheader.Add(col);
                        lsttotal.Add(col);
                        foreach (var item in dataExport)
                        {
                            item.LstGiaTri.Add(col);
                        }
                    }
                }

                data.Add("Count", 10000);
                data.Add("Items", dataExport);
                data.Add("LstMoTa", lstheader);
                data.Add("LstLNS", lstheader);
                data.Add("LstTotal", lsttotal);
                data.Add("TotalVal", lsttotal.Sum(x => x.fTuChi));


                data.Add("DiaDiem", _diaDiem);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);

                data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);

                data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                var strPage = dictByPage.Key > 0 ? "_To1" : string.Empty;
                var strPageNumber = dictByPage.Key > 0 ? $"_To{dictByPage.Key + 1}" : string.Empty;

                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_LNS_EXCEl}_{idDonViLogin}_{DateUtils.GetFormatDateReport()}{strPageNumber}{extension}";
                var templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_LNS_EXCEl}{StringUtils.EXCEL_EXTENSION}");
                listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
            }

            return listResult;
        }


        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private double GetDataByType(DtChungTuChiTietModel chungTuChiTiet)
        {
            if (_dataDotSelected == null)
            {
                return 0.0;
            }

            var dataDotModel = Models.FirstOrDefault(x => x.Id.Equals(_dataDotSelected.Id));
            if (dataDotModel == null || !dataDotModel.ILoaiChungTu.HasValue)
            {
                return 0.0;
            }

            if (VoucherType.NSSD_Key.Equals(dataDotModel.ILoaiChungTu.ToString()))
            {
                return chungTuChiTiet.FTuChi + chungTuChiTiet.FHienVat;
            }
            else if (VoucherType.NSBD_Key.Equals(dataDotModel.ILoaiChungTu.ToString()))
            {
                return chungTuChiTiet.FHangNhap + chungTuChiTiet.FHangMua + chungTuChiTiet.FPhanCap;
            }

            return 0.0;
        }

        private void CalculateDataExportDynamicCol(DuToanChiTieuLNS duToanChiTieuLns, Dictionary<int, List<DtChungTuChiTietModel>> dictValue, int catUnitType)
        {
            duToanChiTieuLns.Val1 = dictValue.GetValueOrDefault(0, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val2 = dictValue.GetValueOrDefault(1, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val3 = dictValue.GetValueOrDefault(2, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val4 = dictValue.GetValueOrDefault(3, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val5 = dictValue.GetValueOrDefault(4, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val6 = dictValue.GetValueOrDefault(5, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val7 = dictValue.GetValueOrDefault(6, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val8 = dictValue.GetValueOrDefault(7, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val9 = dictValue.GetValueOrDefault(8, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
            duToanChiTieuLns.Val10 = dictValue.GetValueOrDefault(9, new List<DtChungTuChiTietModel>()).Sum(GetDataByType)  ;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_TONGHOP_LNS;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
