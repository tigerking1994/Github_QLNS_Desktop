using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ApprovalOfAdvanceCaptital;
//using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan;
//using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ContractInfo;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PrintReport;
//using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GoiThau;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PrintReportProcessProject;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PrintReportAnnualBudgetAllocation;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.PrintReportAdjustmentPlan;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.AnnualSettlement;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ThongTriQuyetToan;
//using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.KHLuaChonNhaThau;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.CapPhatThanhToan;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThanhToanKhoBac;
//using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn;
//using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.BaoCaoKetQuaGiaiNganChiKPDT;
//using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.KeHoachChiQuy;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.DeNghiThanhToanChiPhi;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation
{
    public class InvestmentImplementationViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION;
        //public override string Name => "THỰC HIỆN ĐẦU TƯ";
        public override string Name => "CẤP PHÁT THANH TOÁN";
        public override string Description => "Cấp phát thanh toán";
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.InvestmentImplementation);
        public override PackIconKind IconKind => PackIconKind.Money;

        //public TKTCVaTongDuToanIndexViewModel TKTCVaTongDuToanIndexViewModel { get; }
        //public GoiThauIndexViewModel GoiThauIndexViewModel { get; }
        //public ContractInfoIndexViewModel ContractInfoViewModel { get; }
        public ProjectInformationPrintReportViewModel ProjectInformationPrintReportViewModel { get; }
        public PheDuyetThanhToanIndexViewModel PheDuyetThanhToanIndexViewModel { get; }
        public ApprovalOfAdvanceCaptitalIndexViewModel ApprovalOfAdvanceCaptitalIndexViewModel { get; }
        public ThongTriCapPhatIndexViewModel ThongTriCapPhatIndexViewModel { get; }
        public AnnualSettlementIndexViewModel AnnualSettlementIndexViewModel { get; }
        //public ThongTriQuyetToanIndexViewModel ThongTriQuyetToanIndexViewModel { get; set; }
        public PrintReportProcessProjectViewModel PrintReportProcessProjectViewModel { get; set; }
        public DuToanNSQPNamPrintReportViewModel DuToanNSQPNamPrintReportViewModel { get; }
        public PrintReportAdjustmentPlanViewModel PrintReportAdjustmentPlanViewModel { get; }
        public ProjectCapitalAdjustmentReportViewModel ProjectCapitalAdjustmentReportViewModel { get; }
        public ProjectAllocationPrintReportViewModel ProjectAllocationPrintReportViewModel { get; }
        public PrintReportAnnualBudgetAllocationViewModel PrintReportAnnualBudgetAllocationViewModel { get; }
        //public KHLuaChonNhaThauIndexViewModel KHLuaChonNhaThauIndexViewModel { get; }
        public CapPhatThanhToanIndexViewModel CapPhatThanhToanIndexViewModel { get; }
        public ThanhToanKhoBacIndexViewModel ThanhToanKhoBacIndexViewModel { get; }
       // public QLDuAnIndexViewModel QLDuAnIndexViewModel { get; }
       // public PheDuyetDuAnIndexViewModel PheDuyetDuAnIndexViewModel { get; }
        //public QuyetToanVDTIndexViewModel QuyetToanVDTIndexViewModel { get; }
        //public KeHoachChiQuyIndexViewModel KeHoachChiQuyIndexViewModel { get; }
        public BaoCaoKetQuaGiaiNganChiKPDTViewModel BaoCaoKetQuaGiaiNganChiKPDTViewModel { get; }
        public DeNghiThanhToanChiPhiIndexViewModel DeNghiThanhToanChiPhiIndexViewModel { get; }

        public InvestmentImplementationViewModel(
           // TKTCVaTongDuToanIndexViewModel tKTCVaTongDuToanIndexViewModel,
          //  GoiThauIndexViewModel goiThauIndexViewModel,
          //  ContractInfoIndexViewModel contractInfoViewModel,
            ProjectInformationPrintReportViewModel projectInformationPrintReportViewModel,
            PheDuyetThanhToanIndexViewModel pheDuyetThanhToanIndexViewModel,
            ApprovalOfAdvanceCaptitalIndexViewModel approvalOfAdvanceCaptitalIndexViewModel,
            ThongTriCapPhatIndexViewModel thongTriCapPhatIndexViewModel,
            AnnualSettlementIndexViewModel annualSettlementIndexViewModel,
            //QuyetToanVDTIndexViewModel quyetToanVDTIndexViewModel,
            //ThongTriQuyetToanIndexViewModel thongTriQuyetToanIndexViewModel,
            PrintReportProcessProjectViewModel printReportProcessProjectViewModel,
            DuToanNSQPNamPrintReportViewModel duToanNSQPNamPrintReportViewModel,
            PrintReportAdjustmentPlanViewModel printReportAdjustmentPlanViewModel,
            ProjectCapitalAdjustmentReportViewModel projectCapitalAdjustmentReportViewModel,
            ProjectAllocationPrintReportViewModel projectAllocationPrintReportViewModel,
            PrintReportAnnualBudgetAllocationViewModel printReportAnnualBudgetAllocationViewModel,
            //KHLuaChonNhaThauIndexViewModel kHLuaChonNhaThauIndexViewModel,
            CapPhatThanhToanIndexViewModel capPhatThanhToanIndexViewModel,
            ThanhToanKhoBacIndexViewModel thanhToanKhoBacIndexViewModel,
         //   QLDuAnIndexViewModel qLDuAnIndexViewModel,
         //   PheDuyetDuAnIndexViewModel pheDuyetDuAnIndexViewModel,
            //KeHoachChiQuyIndexViewModel keHoachChiQuyIndexViewModel,
            BaoCaoKetQuaGiaiNganChiKPDTViewModel baoCaoKetQuaGiaiNganChiKpdtViewModel,
            DeNghiThanhToanChiPhiIndexViewModel deNghiThanhToanChiPhiIndexViewModel)
        {
          //  QLDuAnIndexViewModel = qLDuAnIndexViewModel;
          //  TKTCVaTongDuToanIndexViewModel = tKTCVaTongDuToanIndexViewModel;
           // KHLuaChonNhaThauIndexViewModel = kHLuaChonNhaThauIndexViewModel;
        //    GoiThauIndexViewModel = goiThauIndexViewModel;
        //    ContractInfoViewModel = contractInfoViewModel;
            ProjectInformationPrintReportViewModel = projectInformationPrintReportViewModel;
            PheDuyetThanhToanIndexViewModel = pheDuyetThanhToanIndexViewModel;
            ApprovalOfAdvanceCaptitalIndexViewModel = approvalOfAdvanceCaptitalIndexViewModel;
            ThongTriCapPhatIndexViewModel = thongTriCapPhatIndexViewModel;
            AnnualSettlementIndexViewModel = annualSettlementIndexViewModel;
            //ThongTriQuyetToanIndexViewModel = thongTriQuyetToanIndexViewModel;
            PrintReportProcessProjectViewModel = printReportProcessProjectViewModel;
            DuToanNSQPNamPrintReportViewModel = duToanNSQPNamPrintReportViewModel;
            PrintReportAdjustmentPlanViewModel = printReportAdjustmentPlanViewModel;
            ProjectCapitalAdjustmentReportViewModel = projectCapitalAdjustmentReportViewModel;
            ProjectAllocationPrintReportViewModel = projectAllocationPrintReportViewModel;
            PrintReportAnnualBudgetAllocationViewModel = printReportAnnualBudgetAllocationViewModel;
            CapPhatThanhToanIndexViewModel = capPhatThanhToanIndexViewModel;
            ThanhToanKhoBacIndexViewModel = thanhToanKhoBacIndexViewModel;
         //   PheDuyetDuAnIndexViewModel = pheDuyetDuAnIndexViewModel;
            //QuyetToanVDTIndexViewModel = quyetToanVDTIndexViewModel;
            //KeHoachChiQuyIndexViewModel = keHoachChiQuyIndexViewModel;
            BaoCaoKetQuaGiaiNganChiKPDTViewModel = baoCaoKetQuaGiaiNganChiKpdtViewModel;
            DeNghiThanhToanChiPhiIndexViewModel = deNghiThanhToanChiPhiIndexViewModel;

        //    QLDuAnIndexViewModel.ParentPage = this;
        //    TKTCVaTongDuToanIndexViewModel.ParentPage = this;
         //   KHLuaChonNhaThauIndexViewModel.ParentPage = this;
        //    GoiThauIndexViewModel.ParentPage = this;
        //    ContractInfoViewModel.ParentPage = this;
            ProjectInformationPrintReportViewModel.ParentPage = this;
            PheDuyetThanhToanIndexViewModel.ParentPage = this;
            ApprovalOfAdvanceCaptitalIndexViewModel.ParentPage = this;
            ThongTriCapPhatIndexViewModel.ParentPage = this;
            AnnualSettlementIndexViewModel.ParentPage = this;
            //ThongTriQuyetToanIndexViewModel.ParentPage = this;
            PrintReportProcessProjectViewModel.ParentPage = this;
            DuToanNSQPNamPrintReportViewModel.ParentPage = this;
            PrintReportAdjustmentPlanViewModel.ParentPage = this;
            ProjectCapitalAdjustmentReportViewModel.ParentPage = this;
            ProjectAllocationPrintReportViewModel.ParentPage = this;
            PrintReportAnnualBudgetAllocationViewModel.ParentPage = this;
            CapPhatThanhToanIndexViewModel.ParentPage = this;
            ThanhToanKhoBacIndexViewModel.ParentPage = this;
         //   PheDuyetDuAnIndexViewModel.ParentPage = this;
            //QuyetToanVDTIndexViewModel.ParentPage = this;
            //KeHoachChiQuyIndexViewModel.ParentPage = this;
            BaoCaoKetQuaGiaiNganChiKPDTViewModel.ParentPage = this;
            DeNghiThanhToanChiPhiIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                //ProjectManagerIndexViewModel,
          //      QLDuAnIndexViewModel,
                //ApproveProjectIndexViewModel,
           //     PheDuyetDuAnIndexViewModel,
           //     TKTCVaTongDuToanIndexViewModel,
            //    KHLuaChonNhaThauIndexViewModel,
          //      GoiThauIndexViewModel,
          //      ContractInfoViewModel,
                

                //KeHoachChiQuyIndexViewModel,
                CapPhatThanhToanIndexViewModel,
                //DisbursementPaymentIndexViewModel,
                PheDuyetThanhToanIndexViewModel,
                ThongTriCapPhatIndexViewModel,
                DeNghiThanhToanChiPhiIndexViewModel,
                

                //ApprovalOfAdvanceCaptitalIndexViewModel,
                //AnnualSettlementIndexViewModel,
                //QuyetToanVDTIndexViewModel,
                //ThongTriQuyetToanIndexViewModel,
                PrintReportProcessProjectViewModel,
                //DuToanNSQPNamPrintReportViewModel,
                //PrintReportAdjustmentPlanViewModel,
                //ProjectCapitalAdjustmentReportViewModel,
                ProjectAllocationPrintReportViewModel,
                PrintReportAnnualBudgetAllocationViewModel,
                BaoCaoKetQuaGiaiNganChiKPDTViewModel,
                ProjectInformationPrintReportViewModel
                //ThanhToanKhoBacIndexViewModel
            };
            //DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);

            DocumentationSelectedItem = CapPhatThanhToanIndexViewModel;
        }
    }
}
