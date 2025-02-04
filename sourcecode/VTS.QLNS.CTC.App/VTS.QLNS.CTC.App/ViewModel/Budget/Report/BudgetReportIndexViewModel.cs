using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.View.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Allocation.Report;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Expertise;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.App.View.Budget.Estimate.AdjustedEstimate;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Report;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Expertise;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Report
{
    public class BudgetReportIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private PrintCommunicateSettlementAgencyViewModel PrintCommunicateSettlementAgencyViewModel;
        private PrintSummaryRegularSettlementViewModel PrintSummaryRegularSettlementViewModel;
        private PrintSummaryDefenseSettlementViewModel PrintSummaryDefenseSettlementViewModel;
        private PrintSummaryStateSettlementViewModel PrintSummaryStateSettlementViewModel;
        private PrintEstimateSettlementViewModel PrintEstimateSettlementViewModel;
        private PrintSummaryYearSettlementViewModel PrintSummaryYearSettlementViewModel;
        private PrintSummaryVoucherListViewModel PrintSummaryVoucherListViewModel;
        private PrintReportPublicFinanceViewModel PrintReportPublicFinanceViewModel;
        private PrintVoucherListViewModel PrintVoucherListViewModel;
        private PrintArmyViewModel PrintArmyViewModel;
        private PrintArmyUpDownViewModel PrintArmyUpDownViewModel;
        private PrintArmyAverageViewModel PrintArmyAverageViewModel;
        private PrintArmyRegularViewModel PrintArmyRegularViewModel;
        private PrintArmyLeaveViewModel PrintArmyLeaveViewModel;
        private AdjustedEstimateTheoLanReportViewModel AdjustedEstimateTheoLanReportViewModel;
        private ReportDieuChinhDuToanTongHop ReportDieuChinhDuToanTongHop;
        private PrintArmyJurisprudenceViewModel PrintArmyJurisprudenceViewModel;
        private PrintAllocationNoticeViewModel PrintAllocationNoticeViewModel;
        private PrintAllocationDonViViewModel PrintAllocationDonViViewModel;
        private PrintAllocationRequestViewModel PrintAllocationRequestViewModel;
        private PrintAllocationTypeViewModel PrintAllocationTypeViewModel;
        private AllocationReportCompareViewModel AllocationReportCompareViewModel;
        private PrintReportReceiveDivisionViewModel PrintReportReceiveDivisionViewModel;
        private PrintReportEstimateByReceiveDivisionViewModel PrintReportEstimateByReceiveDivisionViewModel;
        private PrintReportCoverSheetViewModel PrintReportCoverSheetViewModel;
        private PrintReportTargetAgencyViewModel PrintReportTargetAgencyViewModel;
        private PrintReportTargetMajorsDayViewModel PrintReportTargetMajorsDayViewModel;
        private PrintReportSynthesisAgencyViewModel PrintReportSynthesisAgencyViewModel;
        private PrintReportSynthesisDivisionViewModel PrintReportSynthesisDivisionViewModel;
        private PrintReportTargetAgencyLnsViewModel PrintReportTargetAgencyLnsViewModel;
        private AdjustedEstimateReportViewModel AdjustedEstimateReportViewModel;
        private PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel;
        private ExpertisePrintReportCTCViewModel ExpertisePrintReportCTCViewModel;
        private ExpertisePrintReportDataBySKTViewModel ExpertisePrintReportDataBySKTViewModel;
        private PrintReportReceiveTheCheckNumberViewModel PrintReportReceiveTheCheckNumberViewModel;
        private PrintReportChiTietDuToanDonViViewModel PrintReportChiTietDuToanDonViViewModel;
        private PrintReportCompareDemandCheckViewModel PrintReportCompareDemandCheckViewModel;
        private PrintReportChiThuongXuyenQuocPhongViewModel PrintReportChiThuongXuyenQuocPhongViewModel;
        private PrintYearSettlementViewModel PrintYearSettlementViewModel;
        private PrintYearSummarySettlementViewModel PrintYearSummarySettlementViewModel;
        private PrintYearApprovalSettlementViewModel PrintYearApprovalSettlementViewModel;

        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override Type ContentType => typeof(BudgetReportIndex);
        public override PackIconKind IconKind => PackIconKind.FileDocument;

        private bool _isVisibleLoaiChungTu;
        public bool IsVisibleLoaiChungTu
        {
            get => _isVisibleLoaiChungTu;
            set => SetProperty(ref _isVisibleLoaiChungTu, value);
        }

        private bool _isVisibleLoaiNganhThamDinh;
        public bool IsVisibleLoaiNganhThamDinh
        {
            get => _isVisibleLoaiNganhThamDinh;
            set => SetProperty(ref _isVisibleLoaiNganhThamDinh, value);
        }

        private ObservableCollection<DmChuKyModel> _listBaoCao;
        public ObservableCollection<DmChuKyModel> ListBaoCao
        {
            get => _listBaoCao;
            set => SetProperty(ref _listBaoCao, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (!string.IsNullOrEmpty(_searchText.Trim()))
                    _listBaoCaoView.Refresh();
            }
        }

        private List<ComboboxItem> _listLoaiChungTu;
        public List<ComboboxItem> ListLoaiChungTu
        {
            get => _listLoaiChungTu;
            set => SetProperty(ref _listLoaiChungTu, value);
        }

        private ComboboxItem _loaiChungTuSelected;
        public ComboboxItem LoaiChungTuSelected
        {
            get => _loaiChungTuSelected;
            set => SetProperty(ref _loaiChungTuSelected, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetSearchCommand { get; }

        public List<string> ListLoaiBaoCao { get; set; }

        public BudgetReportIndexViewModel(
            IDmChuKyService chuKyService,
            IMapper mapper,
            PrintCommunicateSettlementAgencyViewModel printCommunicateSettlementAgencyViewModel,
            PrintSummaryRegularSettlementViewModel printSummaryRegularSettlementViewModel,
            PrintSummaryDefenseSettlementViewModel printSummaryDefenseSettlementViewModel,
            PrintSummaryStateSettlementViewModel printSummaryStateSettlementViewModel,
            PrintEstimateSettlementViewModel printEstimateSettlementViewModel,
            PrintSummaryYearSettlementViewModel printSummaryYearSettlementViewModel,
            PrintSummaryVoucherListViewModel printSummaryVoucherListViewModel,
            PrintVoucherListViewModel printVoucherListViewModel,
            PrintArmyViewModel printArmyViewModel,
            PrintArmyUpDownViewModel printArmyUpDownViewModel,
            PrintArmyAverageViewModel printArmyAverageViewModel,
            PrintArmyRegularViewModel printArmyRegularViewModel,
            PrintArmyLeaveViewModel printArmyLeaveViewModel,
            PrintArmyJurisprudenceViewModel printArmyJurisprudenceViewModel,
            PrintAllocationNoticeViewModel printAllocationNoticeViewModel,
            PrintAllocationDonViViewModel printAllocationDonViViewModel,
            PrintAllocationRequestViewModel printAllocationRequestViewModel,
            PrintAllocationTypeViewModel printAllocationTypeViewModel,
            AllocationReportCompareViewModel allocationReportCompareViewModel,
            PrintReportReceiveDivisionViewModel printReportReceiveDivisionViewModel,
            PrintReportEstimateByReceiveDivisionViewModel printReportEstimateByReceiveDivisionViewModel,
            PrintReportCoverSheetViewModel printReportCoverSheetViewModel,
            PrintReportTargetAgencyViewModel printReportTargetAgencyViewModel,
            PrintReportTargetMajorsDayViewModel printReportTargetMajorsDayViewModel,
            PrintReportSynthesisAgencyViewModel printReportSynthesisAgencyViewModel,
            PrintReportSynthesisDivisionViewModel printReportSynthesisDivisionViewModel,
            PrintReportTargetAgencyLnsViewModel printReportTargetAgencyLnsViewModel,
            AdjustedEstimateReportViewModel adjustedEstimateReportViewModel,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel,
            ExpertisePrintReportCTCViewModel expertisePrintReportCTCViewModel,
            ExpertisePrintReportDataBySKTViewModel expertisePrintReportDataBySKTViewModel,
            PrintReportReceiveTheCheckNumberViewModel printReportReceiveTheCheckNumberViewModel,
            PrintReportChiTietDuToanDonViViewModel printReportChiTietDuToanDonViViewModel,
            PrintReportCompareDemandCheckViewModel printReportCompareDemandCheckViewModel,
            PrintReportChiThuongXuyenQuocPhongViewModel printReportChiThuongXuyenQuocPhongViewModel,
            PrintYearSettlementViewModel printYearSettlementViewModel,
            PrintReportPublicFinanceViewModel printReportPublicFinanceViewModel,
            AdjustedEstimateTheoLanReportViewModel adjustedEstimateTheoLanReportViewModel,
            ReportDieuChinhDuToanTongHop reportDieuChinhDuToanTongHop,
            PrintYearApprovalSettlementViewModel printYearApprovalSettlementViewModel,
            PrintYearSummarySettlementViewModel printYearSummarySettlementViewModel)
        {
            _chuKyService = chuKyService;
            _mapper = mapper;
            //quyết toán
            PrintCommunicateSettlementAgencyViewModel = printCommunicateSettlementAgencyViewModel;
            PrintSummaryRegularSettlementViewModel = printSummaryRegularSettlementViewModel;
            PrintSummaryDefenseSettlementViewModel = printSummaryDefenseSettlementViewModel;
            PrintSummaryStateSettlementViewModel = printSummaryStateSettlementViewModel;
            PrintEstimateSettlementViewModel = printEstimateSettlementViewModel;
            PrintSummaryYearSettlementViewModel = printSummaryYearSettlementViewModel;
            //bảng kê
            PrintSummaryVoucherListViewModel = printSummaryVoucherListViewModel;
            PrintVoucherListViewModel = printVoucherListViewModel;
            //quân số
            PrintArmyViewModel = printArmyViewModel;
            PrintArmyUpDownViewModel = printArmyUpDownViewModel;
            PrintArmyAverageViewModel = printArmyAverageViewModel;
            PrintArmyRegularViewModel = printArmyRegularViewModel;
            PrintArmyLeaveViewModel = printArmyLeaveViewModel;
            PrintArmyJurisprudenceViewModel = printArmyJurisprudenceViewModel;
            //cấp phát
            PrintAllocationNoticeViewModel = printAllocationNoticeViewModel;
            PrintAllocationDonViViewModel = printAllocationDonViViewModel;
            PrintAllocationRequestViewModel = printAllocationRequestViewModel;
            PrintAllocationTypeViewModel = printAllocationTypeViewModel;
            AllocationReportCompareViewModel = allocationReportCompareViewModel;
            //nhận dự toán
            PrintReportReceiveDivisionViewModel = printReportReceiveDivisionViewModel;
            PrintReportEstimateByReceiveDivisionViewModel = printReportEstimateByReceiveDivisionViewModel;
            //phân bổ dự toán
            PrintReportCoverSheetViewModel = printReportCoverSheetViewModel;
            PrintReportTargetAgencyViewModel = printReportTargetAgencyViewModel;
            PrintReportTargetMajorsDayViewModel = printReportTargetMajorsDayViewModel;
            PrintReportSynthesisAgencyViewModel = printReportSynthesisAgencyViewModel;
            PrintReportSynthesisDivisionViewModel = printReportSynthesisDivisionViewModel;
            PrintReportTargetAgencyLnsViewModel = printReportTargetAgencyLnsViewModel;
            AdjustedEstimateReportViewModel = adjustedEstimateReportViewModel;
            //số nhu cầu
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;
            //ngành thẩm định
            ExpertisePrintReportCTCViewModel = expertisePrintReportCTCViewModel;
            ExpertisePrintReportDataBySKTViewModel = expertisePrintReportDataBySKTViewModel;
            //số kiểm tra
            PrintReportReceiveTheCheckNumberViewModel = printReportReceiveTheCheckNumberViewModel;
            //dự toán đầu năm
            PrintReportChiTietDuToanDonViViewModel = printReportChiTietDuToanDonViViewModel;
            PrintReportCompareDemandCheckViewModel = printReportCompareDemandCheckViewModel;
            PrintReportChiThuongXuyenQuocPhongViewModel = printReportChiThuongXuyenQuocPhongViewModel;
            // quyết toán năm
            PrintYearSettlementViewModel = printYearSettlementViewModel;
            PrintYearSummarySettlementViewModel = printYearSummarySettlementViewModel;
            PrintYearApprovalSettlementViewModel = printYearApprovalSettlementViewModel;


            AdjustedEstimateTheoLanReportViewModel = adjustedEstimateTheoLanReportViewModel;
            ReportDieuChinhDuToanTongHop = reportDieuChinhDuToanTongHop;
            PrintReportPublicFinanceViewModel = printReportPublicFinanceViewModel;

            SearchCommand = new RelayCommand(obj => _listBaoCaoView.Refresh());
            ResetSearchCommand = new RelayCommand(obj => { SearchText = string.Empty; _listBaoCaoView.Refresh(); });
        }

        public override void Init()
        {
            base.Init();
            LoadLoaiChungTu();
            LoadData();
        }

        private void LoadLoaiChungTu()
        {
            _isVisibleLoaiChungTu = false;
            if (ListLoaiBaoCao.Any(x => x == NSConstants.DU_TOAN_NHAN_PHAN_BO || x == NSConstants.DU_TOAN_PHAN_BO || x == NSConstants.NGANH_THAM_DINH))
                _isVisibleLoaiChungTu = true;
            OnPropertyChanged(nameof(IsVisibleLoaiChungTu));
            _listLoaiChungTu = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };
            _loaiChungTuSelected = _listLoaiChungTu.First();
            OnPropertyChanged(nameof(ListLoaiChungTu));
            OnPropertyChanged(nameof(LoaiChungTuSelected));
        }

        private void LoadData()
        {
            var predicate = PredicateBuilder.True<DmChuKy>();
            predicate = predicate.And(x => ListLoaiBaoCao.Contains(x.SLoai) && x.BDanhSach == true);
            List<DmChuKy> listBaoCao = _chuKyService.FindByCondition(predicate).OrderBy(x => x.Ten).ToList();
            List<DmChuKyModel> listBaoCaoModel = _mapper.Map<List<DmChuKyModel>>(listBaoCao);
            List<DmChuKyModel> data = new List<DmChuKyModel>();
            int parent = 1;
            int child = 0;
            foreach (var loaiBaoCao in ListLoaiBaoCao)
            {
                data.Add(new DmChuKyModel
                {
                    Ten = GetTenLoaiBaoCao(loaiBaoCao),
                    Stt = parent.ToString()
                });
                child = 1;
                foreach (var baoCao in listBaoCaoModel.Where(x => x.SLoai == loaiBaoCao))
                {
                    baoCao.Stt = string.Format("{0}.{1}", parent, child);
                    data.Add(baoCao);
                    child++;
                }
                parent++;
            }
            _listBaoCao = new ObservableCollection<DmChuKyModel>(data);
            OnPropertyChanged(nameof(ListBaoCao));
            _listBaoCaoView = CollectionViewSource.GetDefaultView(ListBaoCao);
            _listBaoCaoView.Filter = ListBaoCaoFilter;
        }

        private string GetTenLoaiBaoCao(string loaiBaoCao)
        {
            switch (loaiBaoCao)
            {
                case NSConstants.SO_NHU_CAU:
                    return "SỐ NHU CẦU ĐƠN VỊ";
                case NSConstants.NGANH_THAM_DINH:
                    return "NGÀNH THẨM ĐỊNH";
                case NSConstants.SO_KIEM_TRA_NHAN:
                    return "NHẬN SỐ KIỂM TRA";
                case NSConstants.SO_KIEM_TRA_PHAN_BO:
                    return "PHÂN BỔ SỐ KIỂM TRA";
                case NSConstants.DU_TOAN_DAU_NAM:
                    return "LẬP DỰ TOÁN ĐẦU NĂM";
                case NSConstants.DU_TOAN_NHAN_PHAN_BO:
                    return "NHẬN PHÂN BỔ DỰ TOÁN";
                case NSConstants.DU_TOAN_PHAN_BO:
                    return "PHÂN BỔ DỰ TOÁN";
                case NSConstants.DU_TOAN_DIEU_CHINH:
                    return "ĐIỀU CHỈNH DỰ TOÁN";
                case NSConstants.CAP_PHAT:
                    return "CẤP PHÁT";
                case NSConstants.QUYET_TOAN:
                    return "QUYẾT TOÁN";
                case NSConstants.QUAN_SO:
                    return "QUÂN SỐ";
                case NSConstants.BANG_KE:
                    return "BẢNG KÊ";
                case NSConstants.QUYET_TOAN_NAM:
                    return "QUYẾT TOÁN NĂM";
            }
            return string.Empty;
        }

        private bool ListBaoCaoFilter(object obj)
        {
            bool result = true;
            var item = (DmChuKyModel)obj;
            if (!string.IsNullOrEmpty(SearchText))
                result = result && item.Ten.ToLower().Contains(SearchText.Trim().ToLower());
            item.IsFilter = result;
            return result;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            _searchText = string.Empty;
            LoadData();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            object view = null;
            switch (SelectedItem.IdType)
            {
                case TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_DONVI:
                    PrintCommunicateSettlementAgencyViewModel.Init();
                    view = new PrintCommunicateSettlementAgency { DataContext = PrintCommunicateSettlementAgencyViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP:
                    PrintSummaryRegularSettlementViewModel.Init();
                    view = new PrintSummaryRegularSettlement { DataContext = PrintSummaryRegularSettlementViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP:
                    PrintSummaryDefenseSettlementViewModel.Init();
                    view = new PrintSummaryDefenseSettlement { DataContext = PrintSummaryDefenseSettlementViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOAN_NHANUOC_TONGHOP:
                    PrintSummaryStateSettlementViewModel.Init();
                    view = new PrintSummaryStateSettlement { DataContext = PrintSummaryStateSettlementViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOAN_DUTOAN:
                    PrintEstimateSettlementViewModel.Init();
                    view = new PrintEstimateSettlement { DataContext = PrintEstimateSettlementViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOAN_NAM_LNS:
                    PrintSummaryYearSettlementViewModel.Init();
                    view = new PrintSummaryYearSettlement { DataContext = PrintSummaryYearSettlementViewModel };
                    break;
                case TypeChuKy.RPT_NS_BANGKE_CHUNGTU:
                    PrintVoucherListViewModel.Init();
                    view = new PrintVoucherList { DataContext = PrintVoucherListViewModel };
                    break;
                case TypeChuKy.RPT_NS_BANGKE_TONGHOP:
                    PrintSummaryVoucherListViewModel.Init();
                    view = new PrintSummaryVoucherList { DataContext = PrintSummaryVoucherListViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUANSO_DONVI:
                    PrintArmyViewModel.Init();
                    view = new PrintArmy { DataContext = PrintArmyViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUANSO_TANGGIAM:
                    PrintArmyUpDownViewModel.Init();
                    view = new PrintArmyUpDown { DataContext = PrintArmyUpDownViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUANSO_BINHQUAN:
                    PrintArmyAverageViewModel.Init();
                    view = new PrintArmyAverage { DataContext = PrintArmyAverageViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUANSO_THUONGXUYEN:
                    PrintArmyRegularViewModel.Init();
                    view = new PrintArmyRegular { DataContext = PrintArmyRegularViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUANSO_RAQUAN:
                    PrintArmyLeaveViewModel.Init();
                    view = new PrintArmyLeave { DataContext = PrintArmyLeaveViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUANSO_LIENTHAM:
                    PrintArmyJurisprudenceViewModel.Init();
                    view = new PrintArmyJurisprudence { DataContext = PrintArmyJurisprudenceViewModel };
                    break;
                case TypeChuKy.RPT_NS_CAPPHAT_LNS:
                    PrintAllocationNoticeViewModel.Init();
                    view = new PrintAllocationNotice { DataContext = PrintAllocationNoticeViewModel };
                    break;
                case TypeChuKy.RPT_NS_CAPPHAT_DONVI:
                    PrintAllocationDonViViewModel.Init();
                    view = new PrintAllocationDonVi { DataContext = PrintAllocationDonViViewModel };
                    break;
                case TypeChuKy.RPT_NS_CAPPHAT_DENGHI:
                    PrintAllocationRequestViewModel.Init();
                    view = new PrintAllocationRequest { DataContext = PrintAllocationRequestViewModel };
                    break;
                case TypeChuKy.RPT_NS_CAPPHAT_LOAICAP:
                    PrintAllocationTypeViewModel.Init();
                    view = new PrintAllocationType { DataContext = PrintAllocationTypeViewModel };
                    break;
                case TypeChuKy.RPT_NS_CAPPHAT_SOSANH:
                    AllocationReportCompareViewModel.Init();
                    view = new AllocationReportCompare { DataContext = AllocationReportCompareViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_THEODOT:
                    view = ShowBaoCaoDuToan(DivisionPrintType.BUDGET_PERIOD);
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_LUYKEDENDOT:
                    view = ShowBaoCaoDuToan(DivisionPrintType.BUDGET_ACCUMULATION);
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_THEONGANH:
                    view = ShowBaoCaoDuToan(DivisionPrintType.BUDGET_SPECIALIZED);
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_TONGHOP_TUCHI:
                    view = ShowBaoCaoDuToan(DivisionPrintType.SYNTHESIS_BUDGET_SELF);
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_TONGHOP_HIENVAT:
                    view = ShowBaoCaoDuToan(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS);
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_TONGHOP_CHUNG:
                    view = ShowBaoCaoDuToan(DivisionPrintType.SYNTHESIS_BUDGET_COMMON);
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_THONGKE_THEO_SOQUYETDINH:
                    PrintReportEstimateByReceiveDivisionViewModel.Init();
                    view = new PrintReportEstimateByReceiveDivision { DataContext = PrintReportEstimateByReceiveDivisionViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_TOBIA:
                    PrintReportCoverSheetViewModel.Init();
                    view = new PrintReportCoverSheet { DataContext = PrintReportCoverSheetViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI:
                    PrintReportTargetAgencyViewModel.Init();
                    view = new PrintReportTargetAgency { DataContext = PrintReportTargetAgencyViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_CHITIET_NGANH:
                    PrintReportTargetMajorsDayViewModel.Init();
                    view = new PrintReportTargetMajorsDay { DataContext = PrintReportTargetMajorsDayViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_TONGHOP_PHANBO:
                    PrintReportSynthesisAgencyViewModel.Init();
                    view = new PrintReportSynthesisAgency { DataContext = PrintReportSynthesisAgencyViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_TONGHOP_THEODOT:
                    PrintReportSynthesisDivisionViewModel.Init();
                    view = new PrintReportSynthesisDivision { DataContext = PrintReportSynthesisDivisionViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_TONGHOP_LNS:
                    PrintReportTargetAgencyLnsViewModel.Init();
                    view = new PrintReportTargetAgencyLns { DataContext = PrintReportTargetAgencyLnsViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH:
                    AdjustedEstimateReportViewModel.Init();
                    view = new AdjustedEstimateReport { DataContext = AdjustedEstimateReportViewModel };
                    break;
                case TypeChuKy.RPT_NS_SNC_CHITIET:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER;
                    PrintReportDemandOrgViewModel.Init();
                    view = new PrintReportDemandOrg { DataContext = PrintReportDemandOrgViewModel };
                    break;
                case TypeChuKy.RPT_NS_SNC_TONGHOP:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY;
                    PrintReportDemandOrgViewModel.Init();
                    view = new PrintReportDemandOrg { DataContext = PrintReportDemandOrgViewModel };
                    break;
                case TypeChuKy.RPT_NS_SO_NHU_CAU_THEONGANH:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH;
                    PrintReportDemandOrgViewModel.Init();
                    view = new PrintReportDemandOrg { DataContext = PrintReportDemandOrgViewModel };
                    break;
                case TypeChuKy.RPT_NS_NGANHTHAMDINH_CTC:
                    ExpertisePrintReportCTCViewModel.LoaiNganSach = int.Parse(LoaiChungTuSelected.ValueItem);
                    ExpertisePrintReportCTCViewModel.Init();
                    view = new ExpertisePrintReportCTC { DataContext = ExpertisePrintReportCTCViewModel };
                    break;
                case TypeChuKy.RPT_NS_NGANHTHAMDINH_SKT:
                    ExpertisePrintReportDataBySKTViewModel.LoaiNganSach = int.Parse(LoaiChungTuSelected.ValueItem);
                    ExpertisePrintReportDataBySKTViewModel.Init();
                    view = new ExpertisePrintReportDataBySKT { DataContext = ExpertisePrintReportDataBySKTViewModel };
                    break;
                case TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA:
                    PrintReportReceiveTheCheckNumberViewModel._demandCheckPrintType = DemandCheckPrintType.THE_REPORT_RECEIVES_THE_CHECK_NUMBER;
                    PrintReportReceiveTheCheckNumberViewModel.Init();
                    view = new PrintReportReceiveTheCheckNumber { DataContext = PrintReportReceiveTheCheckNumberViewModel };
                    break;
                case TypeChuKy.RPT_NS_NHAN_SO_KIEM_TRA_THEONGANH:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH;
                    PrintReportDemandOrgViewModel.Init();
                    view = new PrintReportDemandOrg { DataContext = PrintReportDemandOrgViewModel };
                    break;
                case TypeChuKy.RPT_NS_PHANBO_SOKIEMTRA_DONVI:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI;
                    PrintReportDemandOrgViewModel.Init();
                    view = new PrintReportDemandOrg { DataContext = PrintReportDemandOrgViewModel };
                    break;
                case TypeChuKy.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA;
                    PrintReportDemandOrgViewModel.Init();
                    view = new PrintReportDemandOrg { DataContext = PrintReportDemandOrgViewModel };
                    break;
                case TypeChuKy.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH;
                    PrintReportDemandOrgViewModel.Init();
                    view = new PrintReportDemandOrg { DataContext = PrintReportDemandOrgViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_DAUNAM:
                    PrintReportChiTietDuToanDonViViewModel.TypeReport = LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN;
                    PrintReportChiTietDuToanDonViViewModel.Init();
                    view = new PrintReportChiTietDuToanDonVi() { DataContext = PrintReportChiTietDuToanDonViViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_SOSANH_SOKIEMTRA:
                    PrintReportCompareDemandCheckViewModel.LoaiChungTu = LoaiChungTuSelected.ValueItem;
                    PrintReportCompareDemandCheckViewModel.Init();
                    view = new PrintReportCompareDemandCheck() { DataContext = PrintReportCompareDemandCheckViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN:
                    PrintReportChiThuongXuyenQuocPhongViewModel.Init();
                    view = new PrintReportChiThuongXuyenQuocPhong() { DataContext = PrintReportChiThuongXuyenQuocPhongViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOANNAM:
                    PrintYearSettlementViewModel.Init();
                    view = new PrintYearSettlement() { DataContext = PrintYearSettlementViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOANNAM_TONGHOP:
                    PrintYearSummarySettlementViewModel.Init();
                    view = new PrintYearSummarySettlement() { DataContext = PrintYearSummarySettlementViewModel };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOANNAM_XETDUYET:
                    PrintYearApprovalSettlementViewModel.Init();
                    view = new PrintYearApprovalSettlement() { DataContext = PrintYearApprovalSettlementViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_CHITIEU_LNS:
                    PrintReportTargetAgencyLnsViewModel.Init();
                    view = new PrintReportTargetAgencyLns() { DataContext = PrintReportTargetAgencyLnsViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI:
                    AdjustedEstimateTheoLanReportViewModel.Init();
                    view = new AdjustedEstimateTheoLanReport() { DataContext = AdjustedEstimateTheoLanReportViewModel };
                    break;
                case TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2:
                    ReportDieuChinhDuToanTongHop.Init();
                    view = new AdjustedEstimateTheoLanReport() { DataContext = ReportDieuChinhDuToanTongHop };
                    break;
                case TypeChuKy.RPT_NS_PHANBODUTOAN_CONGKHAITAICHINH:
                    PrintReportPublicFinanceViewModel.Init();
                    view = new PrintReportPublicFinance() { DataContext = PrintReportPublicFinanceViewModel };
                    break;

            }
            if (view != null)
                DialogHost.Show(view, SettlementScreen.ROOT_DIALOG, null, null);
        }

        private object ShowBaoCaoDuToan(DivisionPrintType divisionPrintType)
        {
            PrintReportReceiveDivisionViewModel.DivisionPrintType = divisionPrintType;
            PrintReportReceiveDivisionViewModel.LoaiChungTu = int.Parse(LoaiChungTuSelected.ValueItem);
            PrintReportReceiveDivisionViewModel.Init();
            return new PrintReportReceiveDivision { DataContext = PrintReportReceiveDivisionViewModel };
        }
    }
}
