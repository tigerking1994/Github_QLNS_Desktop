using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Hospital;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Report;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate
{
    public class EstimateViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.BUDGET_ESTIMATE;
        public override string Name => "DỰ TOÁN";
        public override string Description => "Phân bổ dự toán cho đơn vị";
        public override Type ContentType => typeof(View.Budget.Estimate.Estimate);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public EstimateDiagramViewModel EstimateDiagramViewModel { get; }
        public DivisionIndexViewModel DivisionIndexViewModel { get; }
        public HospitalIndexViewModel HospitalIndexViewModel { get; }
        public DivisionEstimateIndexViewModel DivisionEstimateIndexViewModel { get; }
        public AdjustedEstimateIndexViewModel AdjustedEstimateIndexViewModel { get; }
        public DuToanDonViIndexViewModel DuToanDonViIndexViewModel { get; }
        public ReportDivisionCurrentViewModel ReportList { get; }
        public LevelBuggetIndexViewModel LevelBuggetIndexViewModel { get; }
        public BudgetReportIndexViewModel BudgetReportIndexViewModel { get; }

        public EstimateViewModel(DivisionIndexViewModel divisionIndexViewModel,
            DivisionEstimateIndexViewModel divisionEstimateIndexViewModel,
            HospitalIndexViewModel hospitalIndexViewModel,
            AdjustedEstimateIndexViewModel adjustedEstimateIndexViewModel,
            ReportDivisionCurrentViewModel reportList,
            DuToanDonViIndexViewModel duToanDonViIndexViewModel,
            EstimateDiagramViewModel estimateDiagramViewModel,
            LevelBuggetIndexViewModel levelBuggetIndexViewModel,
            BudgetReportIndexViewModel budgetReportIndexViewModel)
        {
            EstimateDiagramViewModel = estimateDiagramViewModel;
            DivisionIndexViewModel = divisionIndexViewModel;
            HospitalIndexViewModel = hospitalIndexViewModel;
            DivisionEstimateIndexViewModel = divisionEstimateIndexViewModel;
            AdjustedEstimateIndexViewModel = adjustedEstimateIndexViewModel;
            ReportList = reportList;
            DuToanDonViIndexViewModel = duToanDonViIndexViewModel;
            LevelBuggetIndexViewModel = levelBuggetIndexViewModel;
            BudgetReportIndexViewModel = budgetReportIndexViewModel;

            EstimateDiagramViewModel.ParentPage = this;
            DivisionIndexViewModel.ParentPage = this;
            HospitalIndexViewModel.ParentPage = this;
            DivisionEstimateIndexViewModel.ParentPage = this;
            ReportList.ParentPage = this;
            DuToanDonViIndexViewModel.ParentPage = this;
            LevelBuggetIndexViewModel.ParentPage = this;
            BudgetReportIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            BudgetReportIndexViewModel.ListLoaiBaoCao = new List<string> 
            { 
                NSConstants.DU_TOAN_NHAN_PHAN_BO, 
                NSConstants.DU_TOAN_PHAN_BO,
                NSConstants.DU_TOAN_DIEU_CHINH
            };
            BudgetReportIndexViewModel.Name = "Báo cáo Dự toán";
            BudgetReportIndexViewModel.Description = "Danh mục báo cáo Dự toán";
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                DivisionIndexViewModel,
                DivisionEstimateIndexViewModel,
                HospitalIndexViewModel,
                AdjustedEstimateIndexViewModel,
                ReportList,
                DuToanDonViIndexViewModel,
                LevelBuggetIndexViewModel,
                BudgetReportIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
