using AutoMapper;
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
using VTS.QLNS.CTC.App.Model.Report;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportDetailTargetAgencyLnsViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DuToanChiTieuLNS>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _coverSheetView;
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
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_AGENCY_LNS];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_AGENCY_LNS];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_AGENCY_LNS];
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportDetailTargetAgencyLns);

        private bool _isExportData;
        public bool IsExportData
        {
            get => ListCoverSheet.Any() && ListCoverSheet.Any(x => x.IsChecked);
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
                    TxtTitleSecond = $"Kèm theo Quyết định {_dataDotSelected.DisplayItem1}";
                }
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

        private string _txtTieuDe3;
        public string TxtTieuDe3
        {
            get => _txtTieuDe3;
            set => SetProperty(ref _txtTieuDe3, value);
        }

        private bool _checkPrintTNG;
        public bool CheckPrintTNG
        {
            get => _checkPrintTNG;
            set => SetProperty(ref _checkPrintTNG, value);
        }

        // start handle cover sheet
        private ObservableCollection<CheckBoxItem> _listCoverSheet = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListCoverSheet
        {
            get => _listCoverSheet;
            set => SetProperty(ref _listCoverSheet, value);
        }

        private string _labelSelectedCountCoverSheet;
        public string LabelSelectedCountCoverSheet
        {
            get => $"CHỌN TỜ ({ListCoverSheet.Count(item => item.IsChecked)}/{ListCoverSheet.Count})";
            set => SetProperty(ref _labelSelectedCountCoverSheet, value);
        }

        private bool _selectAllCoverSheet;
        public bool SelectAllCoverSheet
        {
            get => ListCoverSheet.Any() && ListCoverSheet.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllCoverSheet, value);
                foreach (var item in ListCoverSheet) item.IsChecked = _selectAllCoverSheet;
            }
        }

        private string _searchCoverSheet;
        public string SearchCoverSheet
        {
            get => _searchCoverSheet;
            set
            {
                if (SetProperty(ref _searchCoverSheet, value))
                {
                    _coverSheetView.Refresh();
                }
            }
        }

        public RelayCommand ConfigSignCommand { get; }

        public PrintReportDetailTargetAgencyLnsViewModel(
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
            TxtTitleFirst = $"Dự toán chi ngân sách năm {_sessionService.Current.YearOfWork}";
            LoadDataDot();
            if (!Models.Any())
            {
                return;
            }
            LoadCoverSheet();
            LoadCatUnitTypes();
            LoadDanhMuc();
            LoadDanhMucChuKy();
        }

        private void LoadDataDot()
        {
            if (Models == null || !Models.Any())
            {
                var predicate = CreatePredicateChungTuIndex();
                var listDtChungTu = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
                Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDtChungTu);
            }

            Models = new ObservableCollection<DtChungTuModel>(Models.Where(x => x.ILoaiChungTu.ToString().Equals(VoucherType.NSSD_Key)));
            DataDot = _mapper.Map<ObservableCollection<ComboboxManyItem>>(Models);
            _dataDotSelected = DataDot.FirstOrDefault(x => Model != null && x.Id.Equals(Model.Id));
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
        private void LoadCoverSheet()
        {
            ListCoverSheet = new ObservableCollection<CheckBoxItem>
            {
                new CheckBoxItem
                {
                    DisplayItem = "Tờ 1",
                    ValueItem = "1"
                }
            };

            OnPropertyChanged(nameof(LabelSelectedCountCoverSheet));
            OnPropertyChanged(nameof(SelectAllCoverSheet));
            OnPropertyChanged(nameof(IsExportData));
            // Filter
            _coverSheetView = CollectionViewSource.GetDefaultView(ListCoverSheet);
            _coverSheetView.Filter = obj => string.IsNullOrWhiteSpace(_searchCoverSheet)
                                            || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchCoverSheet, StringComparison.OrdinalIgnoreCase));

            foreach (var model in ListCoverSheet)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountCoverSheet));
                    OnPropertyChanged(nameof(SelectAllCoverSheet));
                    OnPropertyChanged(nameof(IsExportData));
                };
            }
        }

        public override string GetFileTemplate(string strPageNumber = "")
        {
            return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_LNS_CHONTO}{strPageNumber}{StringUtils.EXCEL_EXTENSION}");
        }

        public override IEnumerable<DtChungTuChiTietModel> GetData()
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var chungTuId = dataDotSelectedModel.Id.ToString();

            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Guid.Empty,
                ChungTuId = chungTuId,
                LNS = dataDotSelectedModel.SDslns,
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                VoucherDate = dataDotSelectedModel.DNgayChungTu.HasValue ? dataDotSelectedModel.DNgayChungTu : DateTime.Now,
                SoChungTu = string.IsNullOrEmpty(dataDotSelectedModel.SSoChungTu) ? string.Empty : dataDotSelectedModel.SSoChungTu
            };

            Func<NsDtChungTuChiTietQuery, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

            var listChungTuChiTiet = _dtChungTuChiTietService.FindByCond(searchCondition, procedure: "rpt_du_toan_chi_tieu_LNS").Where(x => x.BHangCha || hasData.Invoke(x)).ToList();
            var listChungTuChiTietModel = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet);
            return listChungTuChiTietModel;
        }

        public override List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<DtChungTuChiTietModel> listData, string extension)
        {
            var idDonViLogin = _sessionService.Current.IdDonVi;
            var dataGroupByDonVi = listData.GroupBy(x => x.IIdMaDonVi).ToDictionary(x => x.Key, x => x.ToList());
            var listLns = listData.GroupBy(x => x.IIdMlns.ToString()).Select(x => x.First());

            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));

            Func<DtChungTuChiTietModel, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

            var pageSize = 6;

            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            var dictLnsByPageSize = listLns.Select((x, i) => new { Group = i / pageSize, Value = x })
                .GroupBy(item => item.Group, g => g.Value)
                .ToDictionary(x => x.Key, x => x.ToList());

            var catUnitTypeStr = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem;
            var catUnitType = Convert.ToInt32(CatUnitTypeSelected.ValueItem);

            foreach (var dictByPage in dictLnsByPageSize)
            {
                if (ListCoverSheet.Count == 1 && dictByPage.Key > 0)
                {
                    break;
                }

                var data = new Dictionary<string, object>
                {
                    { "Cap1", dictByPage.Key > 0 ? string.Empty : _cap1 },
                    { "Cap2", dictByPage.Key > 0 ? string.Empty : _sessionService.Current.TenDonVi },
                    { "TitleFirst", dictByPage.Key > 0 ? string.Empty : TxtTitleFirst },
                    { "TitleSecond", dictByPage.Key > 0 ? string.Empty : TxtTitleSecond },
                    { "TieuDe3", dictByPage.Key > 0 ? string.Empty : TxtTieuDe3 },
                    { "CatUnitType", catUnitTypeStr },
                    { "DiaDiem", dictByPage.Key > 0 ? string.Empty : _diaDiem },
                    { "Ngay", dictByPage.Key > 0 ? string.Empty : DateUtils.FormatDateReport(ReportDate) }
                };
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                var dataExport = new List<DuToanChiTieuLNS>();
                var listDictValue = new List<Dictionary<int, List<DtChungTuChiTietModel>>>();
                foreach (var item in dataGroupByDonVi)
                {
                    var dictDonViByMlns = item.Value.GroupBy(x => x.IIdMlns.ToString()).ToDictionary(x => x.Key, x => x.ToList());
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    listDictValue.Add(dictValue);
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, new DtChungTuChiTietModel());
                        dictValue.Add(i, dictDonViByMlns.GetValueOrDefault(mlnsByCol.IIdMlns.ToString(), new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuLns = new DuToanChiTieuLNS();
                    dataExport.Add(duToanChiTieuLns);

                    duToanChiTieuLns.TenDonVi = item.Value.First().STenDonVi;
                    duToanChiTieuLns.ValTuChi = item.Value.Sum(x => x.FTuChi) / catUnitType;
                    duToanChiTieuLns.ValHienVat = item.Value.Sum(x => x.FHienVat) / catUnitType;
                    CalculateDataExportDynamicCol(duToanChiTieuLns, dictValue, catUnitType);
                }

                for (var i = 0; i < pageSize; i++)
                {
                    var model = dictByIndexCol.GetValueOrDefault(i, new DtChungTuChiTietModel());
                    data.Add($"MoTa{i + 1}", model.SMoTa);
                    data.Add($"Lns{i + 1}", model.SLns);
                    data.Add($"LoaiTien{i + 1}", GetLoaiTien(listDictValue, i));
                    data.Add($"Index{i + 1}", !string.IsNullOrEmpty(model.SLns) ? $"({i + 1})" : string.Empty);
                }

                data.Add("TotalTuChi", dataExport.Sum(x => x.ValTuChi));
                data.Add("TotalHienVat", dataExport.Sum(x => x.ValHienVat));
                data.Add("TotalVal1", dataExport.Sum(x => x.Val1));
                data.Add("TotalVal2", dataExport.Sum(x => x.Val2));
                data.Add("TotalVal3", dataExport.Sum(x => x.Val3));
                data.Add("TotalVal4", dataExport.Sum(x => x.Val4));
                data.Add("TotalVal5", dataExport.Sum(x => x.Val5));
                data.Add("TotalVal6", dataExport.Sum(x => x.Val6));
                data.Add("TotalVal7", dataExport.Sum(x => x.Val7));
                data.Add("TotalVal8", dataExport.Sum(x => x.Val8));
                data.Add("TotalVal9", dataExport.Sum(x => x.Val9));
                data.Add("TotalVal10", dataExport.Sum(x => x.Val10));

                data.Add("ThuaLenh1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty));
                data.Add("ThuaLenh2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty));
                data.Add("ThuaLenh3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty));

                data.Add("ChucDanh1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty));
                data.Add("ChucDanh2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty));
                data.Add("ChucDanh3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty));

                data.Add("Ten1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty));
                data.Add("Ten2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty));
                data.Add("Ten3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty));

                data.Add("Items", dataExport);

                var strPageNumber = dictByPage.Key > 0 ? "_To2" : "_To1";
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_LNS_CHONTO}_{idDonViLogin}_{DateUtils.GetFormatDateReport()}{strPageNumber}{extension}";
                var templateFileName = GetFileTemplate(strPageNumber);
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
            duToanChiTieuLns.Val1 = dictValue.GetValueOrDefault(0, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val2 = dictValue.GetValueOrDefault(1, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val3 = dictValue.GetValueOrDefault(2, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val4 = dictValue.GetValueOrDefault(3, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val5 = dictValue.GetValueOrDefault(4, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val6 = dictValue.GetValueOrDefault(5, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val7 = dictValue.GetValueOrDefault(6, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val8 = dictValue.GetValueOrDefault(7, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val9 = dictValue.GetValueOrDefault(8, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val10 = dictValue.GetValueOrDefault(9, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
        }

        private string GetLoaiTien(List<Dictionary<int, List<DtChungTuChiTietModel>>> listDictVal, int indexCol)
        {
            var isContainsValTuChi = listDictVal.Any(x => x.GetValueOrDefault(indexCol, new List<DtChungTuChiTietModel>()).Any(x => x.FTuChi != 0));
            var isContainsValHienVat = listDictVal.Any(x => x.GetValueOrDefault(indexCol, new List<DtChungTuChiTietModel>()).Any(x => x.FHienVat != 0));

            var listResult = new List<string>();
            if (isContainsValTuChi)
            {
                listResult.Add("Bằng tiền");
            }
            if (isContainsValHienVat)
            {
                listResult.Add("Bằng hiện vật");
            }

            return string.Join(StringUtils.COMMA, listResult);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIEU_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_CHITIEU_LNS;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadDanhMuc()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucQuanLy = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;

                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            }
        }

        private void LoadDanhMucChuKy()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIEU_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                TxtTieuDe3 = _dmChuKy.TieuDe3MoTa;
        }
    }
}
