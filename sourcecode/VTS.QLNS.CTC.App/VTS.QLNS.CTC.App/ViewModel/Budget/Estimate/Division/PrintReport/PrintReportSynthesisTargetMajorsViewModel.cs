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
using VTS.QLNS.CTC.App.Helper;
using log4net;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportSynthesisTargetMajorsViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DuToanChiTieuTongHop>
    {
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private ICollectionView _coverSheetView;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDonViService _nSDonViService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private string _cap1;

        public override string Name
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_MAJORS];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_MAJORS];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_MAJORS];
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportSynthesisTargetMajors);

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

        public PrintReportSynthesisTargetMajorsViewModel(
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDonViService nSDonViService,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel) : base(exportService, danhMucService, sessionService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _nSDonViService = nSDonViService;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            _logger = logger;
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
            LoadCoverSheet();
            LoadCatUnitTypes();
            LoadDanhMuc();
            LoadTieuDe();
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
            var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem2));
            DataDot = new ObservableCollection<ComboboxManyItem>(ordered);
            _dataDotSelected = DataDot.FirstOrDefault(x => Model != null && x.Id.Equals(Model.Id));
        }

        private void LoadDanhMuc()
        {
            string chiTietToi = "NG";
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucQuanLy = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;

                var danhMucDiaDiem = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;

                var danhMucMLNS = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                chiTietToi = danhMucMLNS == null ? chiTietToi : danhMucMLNS.SGiaTri;
            }
            PrintTypeMLNS = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
            SelectedPrintTypeMLNS = PrintTypeMLNS.First();
        }

        private void LoadTieuDe()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_NGANH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    _dmChuKy = new DmChuKy();
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                    TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                    TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
            return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_NGANH}{strPageNumber}{StringUtils.EXCEL_EXTENSION}");
        }

        public override IEnumerable<DtChungTuChiTietModel> GetData()
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = dataDotSelectedModel.Id,
                LNS = dataDotSelectedModel.SDslns,
                YearOfWork = dataDotSelectedModel.INamLamViec,
                YearOfBudget = dataDotSelectedModel.INamNganSach,
                BudgetSource = dataDotSelectedModel.IIdMaNguonNganSach,
                VoucherDate = dataDotSelectedModel.DNgayChungTu,
                IdDotNhan = dataDotSelectedModel.IIdDotNhan,
                SoChungTu = dataDotSelectedModel.SSoChungTu,
                UserName = _sessionService.Current.Principal
            };

            Func<NsDtChungTuChiTietQuery, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

            var listChungTuChiTiet = _dtChungTuChiTietService.FindByCond(searchCondition).Where(x => x.BHangCha || hasData.Invoke(x)).ToList();
            var listChungTuChiTietModel = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet);
            return listChungTuChiTietModel.OrderBy(x => x.SXauNoiMa);
        }

        public override List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<DtChungTuChiTietModel> listData, string extension)
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_NGANH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            var idDonViLogin = _sessionService.Current.IdDonVi;
            var dataGroupByMlnsId = listData.GroupBy(x => x.IIdMlns.ToString()).ToDictionary(x => x.Key, x => x.ToList());
            // get total budget
            var listDetailAvailable = GetTotalBudgetAvailable();
            var totalBudgetEstimateDivisionMapByCategoryId = listDetailAvailable
                .GroupBy(budget => budget.IIdMlns.ToString())
                .ToDictionary(g => g.Key, g => g.ToList());

            var listDetailUsed = GetTotalBudgetUsed("rpt_du_toan_chi_tieu_tong_hop_used");
            var usedBudgetEstimateDivisionMapByCategoryId = listDetailUsed
                .GroupBy(budget => budget.IIdMlns.ToString())
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.STenDonVi).ToDictionary(e => e.Key, e => e.ToList()));
            var listAgencies = listDetailUsed.Select(x => x.STenDonVi).ToHashSet();

            var pageSize = 2;
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            var dictAgencyByPageSize = listAgencies.Select((x, i) => new { Group = i / pageSize, Value = x })
                .GroupBy(item => item.Group, g => g.Value)
                .ToDictionary(x => x.Key, x => x.ToList());
            if (dictAgencyByPageSize.Count == 1)
            {
                dictAgencyByPageSize.Add(1, new List<string>());
            }

            if (!dictAgencyByPageSize.Any())
            {
                dictAgencyByPageSize.Add(0, new List<string>());
                dictAgencyByPageSize.Add(1, new List<string>());
            }

            var catUnitTypeStr = string.Format("Đơn vị tính: {0}", CatUnitTypeSelected.DisplayItem);
            var catUnitType = Convert.ToInt32(CatUnitTypeSelected.ValueItem);

            foreach (var dictByPage in dictAgencyByPageSize)
            {
                if (ListCoverSheet.Count == 1 && dictByPage.Key > 0)
                {
                    break;
                }

                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", dictByPage.Key > 0 ? string.Empty : _cap1);
                data.Add("Cap2", dictByPage.Key > 0 ? string.Empty : _sessionService.Current.TenDonVi);
                data.Add("TitleFirst", dictByPage.Key > 0 ? string.Empty : _txtTitleFirst);
                data.Add("TitleSecond", dictByPage.Key > 0 ? string.Empty : TxtTitleSecond);
                data.Add("CatUnitType", catUnitTypeStr);

                data.Add("DiaDiem", dictByPage.Key > 0 ? string.Empty : _diaDiem);
                data.Add("Ngay", dictByPage.Key > 0 ? string.Empty : DateUtils.GetCurrentDateReport());

                data.Add("ThuaLenh1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty));
                data.Add("ThuaLenh2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty));
                data.Add("ThuaLenh3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty));

                data.Add("ChucDanh1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty));
                data.Add("ChucDanh2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty));
                data.Add("ChucDanh3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty));

                data.Add("Ten1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty));
                data.Add("Ten2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty));
                data.Add("Ten3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty));

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                var dataExport = new List<DuToanChiTieuTongHop>();
                foreach (var item in dataGroupByMlnsId)
                {
                    var dictDonViByDonVi = usedBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(item.Key, new Dictionary<string, List<DtChungTuChiTietModel>>());
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, string.Empty);
                        dictValue.Add(i, dictDonViByDonVi.GetValueOrDefault(mlnsByCol, new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuTongHop = new DuToanChiTieuTongHop();
                    dataExport.Add(duToanChiTieuTongHop);

                    duToanChiTieuTongHop.Model = item.Value.ElementAt(0);

                    CalculateDataExportDynamicCol(item.Key, catUnitType, duToanChiTieuTongHop, dictValue, dictDonViByDonVi, totalBudgetEstimateDivisionMapByCategoryId);
                }

                var listFilterExport = CalculateData(dataExport);
                var dictTotalByIndexCol = CalculateTotal(listFilterExport);
                for (var i = 0; i < pageSize; i++)
                {
                    data.Add($"MoTa{i + 1}", dictByIndexCol.GetValueOrDefault(i, string.Empty));
                    data.Add($"Total{i + 1}", dictTotalByIndexCol.GetValueOrDefault(i, 0.0));
                }

                data.Add("TotalDuToan", dictTotalByIndexCol.GetValueOrDefault(-1, 0.0));
                data.Add("TotalPhanBo", dictTotalByIndexCol.GetValueOrDefault(-2, 0.0));
                data.Add("TotalConLai", dictTotalByIndexCol.GetValueOrDefault(-3, 0.0));

                switch (SelectedPrintTypeMLNS.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng1)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng2)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng3)).ToList();
                        break;
                }
                data.Add("Items", listFilterExport);

                var strPageNumber = dictByPage.Key > 0 ? "_To2" : "_To1";
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_NGANH}_{idDonViLogin}_{DateUtils.GetFormatDateReport()}{strPageNumber}{extension}";
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

        private void CalculateDataExportDynamicCol(
            string mlnsId,
            int catUnitType,
            DuToanChiTieuTongHop duToanChiTieuTongHop,
            Dictionary<int, List<DtChungTuChiTietModel>> dictValue,
            Dictionary<string, List<DtChungTuChiTietModel>> dictDonViByDonVi,
            Dictionary<string?, List<DtChungTuChiTietModel>> totalBudgetEstimateDivisionMapByCategoryId)
        {
            var listValDuToan = totalBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(mlnsId, new List<DtChungTuChiTietModel>());
            var listValPhanBo = dictDonViByDonVi.Values.SelectMany(x => x);

            duToanChiTieuTongHop.ValDuToan = listValDuToan.Sum(GetDataByType) / catUnitType;
            duToanChiTieuTongHop.ValPhanBo = listValPhanBo.Sum(GetDataByType) / catUnitType;

            duToanChiTieuTongHop.Val1 = dictValue.GetValueOrDefault(0, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuTongHop.Val2 = dictValue.GetValueOrDefault(1, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuTongHop.Val3 = dictValue.GetValueOrDefault(2, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuTongHop.Val4 = dictValue.GetValueOrDefault(3, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuTongHop.Val5 = dictValue.GetValueOrDefault(4, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuTongHop.Val6 = dictValue.GetValueOrDefault(5, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuTongHop.Val7 = dictValue.GetValueOrDefault(6, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuTongHop.Val8 = dictValue.GetValueOrDefault(7, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;

            duToanChiTieuTongHop.ValConLai = duToanChiTieuTongHop.ValDuToan - duToanChiTieuTongHop.ValPhanBo;
        }

        private List<DtChungTuChiTietModel> GetTotalBudgetAvailable()
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var budgetEstimateDivisionCondition = new EstimationVoucherDetailCriteria
            {
                ChungTuId = dataDotSelectedModel.Id.ToString(),
                LNS = dataDotSelectedModel.SDslns,
                YearOfWork = dataDotSelectedModel.INamLamViec,
                YearOfBudget = dataDotSelectedModel.INamNganSach,
                BudgetSource = dataDotSelectedModel.IIdMaNguonNganSach,
                Level = 0,
                Status = NSEntityStatus.ACTIVED
            };

            var listDetail = _dtChungTuChiTietService.FindBudgetEstimateDivision(budgetEstimateDivisionCondition).ToList();
            var listDetailModel = _mapper.Map<List<DtChungTuChiTietModel>>(listDetail);
            return listDetailModel;
        }

        private List<DtChungTuChiTietModel> GetTotalBudgetUsed(string procedure)
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = dataDotSelectedModel.Id,
                LNS = dataDotSelectedModel.SDslns,
                YearOfWork = dataDotSelectedModel.INamLamViec,
                YearOfBudget = dataDotSelectedModel.INamNganSach,
                BudgetSource = dataDotSelectedModel.IIdMaNguonNganSach,
                VoucherDate = dataDotSelectedModel.DNgayChungTu,
                IdDotNhan = dataDotSelectedModel.IIdDotNhan,
                SoChungTu = dataDotSelectedModel.SSoChungTu
            };

            var listDetail = _dtChungTuChiTietService.FindByCond(searchCondition, procedure).ToList();
            var listDetailModel = _mapper.Map<List<DtChungTuChiTietModel>>(listDetail);
            return listDetailModel;
        }

        private List<T> CalculateData<T>(IList<T> listData) where T : DuToanChiTieuTongHop
        {
            listData.Where(x => x.Model.IsHangCha)
                .ForAll(x =>
                {
                    x.ValDuToan = 0;
                    x.ValPhanBo = 0;
                    x.ValConLai = 0;
                });

            foreach (var item in listData.Where(x => x.Model.IsEditable && (x.ValDuToan != 0 || x.ValPhanBo != 0)))
            {
                CalculateParent(listData, item, item);
            }

            var listFilterResult = listData.Where(x => x.ValDuToan != 0 || x.ValPhanBo != 0).ToList();
            return listFilterResult;
        }

        private void CalculateParent<T>(IList<T> listData, T currentItem, T selfItem) where T : DuToanChiTieuTongHop
        {
            var parentItem = listData.FirstOrDefault(x => x.Model.IIdMlns == currentItem.Model.IIdMlnsCha);
            if (parentItem == null) return;
            parentItem.ValDuToan += selfItem.ValDuToan;
            parentItem.ValPhanBo += selfItem.ValPhanBo;
            parentItem.ValConLai += selfItem.ValConLai;
            CalculateParent(listData, parentItem, selfItem);
        }

        private Dictionary<int, double> CalculateTotal<T>(IList<T> listData) where T : DuToanChiTieuTongHop
        {
            var listChildren = listData.Where(x => x.Model.IsEditable).ToList();
            var dictVal = new Dictionary<int, double>
            {
                { -1, listChildren.Sum(x => x.ValDuToan) },
                { -2, listChildren.Sum(x => x.ValPhanBo) },
                { -3, listChildren.Sum(x => x.ValConLai) },

                { 0, listChildren.Sum(x => x.Val1) },
                { 1, listChildren.Sum(x => x.Val2) },
                { 2, listChildren.Sum(x => x.Val3) },
                { 3, listChildren.Sum(x => x.Val4) },
                { 4, listChildren.Sum(x => x.Val5) },
                { 5, listChildren.Sum(x => x.Val6) },
                { 6, listChildren.Sum(x => x.Val7) },
                { 7, listChildren.Sum(x => x.Val8) }
            };
            return dictVal;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_NGANH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_TONGHOP_NGANH;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
