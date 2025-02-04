using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.DefenseBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.RegularBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.StateBudget;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Diagram
{
    public class DiagramViewModel : ViewModelBase
    {
        private PrintSummaryYearSettlementViewModel PrintSummaryYearSettlementViewModel;

        public override string Name => "SƠ ĐỒ CHỨC NĂNG";
        public override Type ContentType => typeof(View.Budget.Diagram.Diagram);
        public override PackIconKind IconKind => PackIconKind.Sitemap;

        public RelayCommand EstimateCommand { get; }
        public RelayCommand AllocationCommand { get; }
        public RelayCommand DemandCheckCommand { get; }
        public RelayCommand RegularBudgetCommand { get; }
        public RelayCommand DefenseBudgetCommand { get; }
        public RelayCommand StateBudgetCommand { get; }
        public RelayCommand ArmyVoucherCommand { get; }
        public RelayCommand SummaryYearSettlementCommand { get; }

        public DiagramViewModel(PrintSummaryYearSettlementViewModel printSummaryYearSettlementViewModel)
        {
            PrintSummaryYearSettlementViewModel = printSummaryYearSettlementViewModel;

            EstimateCommand = new RelayCommand(obj => GoEstimate());
            AllocationCommand = new RelayCommand(obj => GoAllocation());
            DemandCheckCommand = new RelayCommand(obj => GoDemandCheck());
            RegularBudgetCommand = new RelayCommand(obj => GoSettlement((int)SettlementOrdinal.REGULAR_BUDGET));
            DefenseBudgetCommand = new RelayCommand(obj => GoSettlement((int)SettlementOrdinal.DEFENSE_BUDGET));
            StateBudgetCommand = new RelayCommand(obj => GoSettlement((int)SettlementOrdinal.STATE_BUDGET));
            ArmyVoucherCommand = new RelayCommand(obj => GoSettlement((int)SettlementOrdinal.ARMY_VOUCHER));
            SummaryYearSettlementCommand = new RelayCommand(obj => OnSummaryYearSettlement());
        }

        private void GoDemandCheck()
        {
            BudgetViewModel budgetViewModel = (BudgetViewModel)this.ParentPage;
            if (budgetViewModel != null)
            {
                DemandCheckViewModel demandCheckViewModel = (DemandCheckViewModel)budgetViewModel.Documentation.FirstOrDefault(obj => obj is DemandCheckViewModel);
                if (demandCheckViewModel != null)
                {
                    budgetViewModel.DocumentationSelectedItem = demandCheckViewModel;
                }
            }
        }

        private void GoAllocation()
        {
            BudgetViewModel budgetViewModel = (BudgetViewModel)this.ParentPage;
            if (budgetViewModel != null)
            {
                AllocationViewModel allocationViewModel = (AllocationViewModel)budgetViewModel.Documentation.FirstOrDefault(obj => obj is AllocationViewModel);
                if (allocationViewModel != null)
                {
                    budgetViewModel.DocumentationSelectedItem = allocationViewModel;
                }
            }
        }

        private void GoEstimate()
        {
            BudgetViewModel budgetViewModel = (BudgetViewModel)this.ParentPage;
            if (budgetViewModel != null)
            {
                EstimateViewModel estimateViewModel = (EstimateViewModel)budgetViewModel.Documentation.FirstOrDefault(obj => obj is EstimateViewModel);
                if (estimateViewModel != null)
                {
                    budgetViewModel.DocumentationSelectedItem = estimateViewModel;
                }
            }
        }

        private void GoSettlement(int settlementOrdinal)
        {
            BudgetViewModel budgetViewModel = (BudgetViewModel)this.ParentPage;
            if (budgetViewModel != null)
            {
                SettlementViewModel settlementViewModel = (SettlementViewModel)budgetViewModel.Documentation.FirstOrDefault(obj => obj is SettlementViewModel);
                //settlementViewModel.SettlementOrdinal = settlementOrdinal;
                if (settlementViewModel != null)
                {
                    budgetViewModel.DocumentationSelectedItem = settlementViewModel;
                }
            }
        }

        private void OnSummaryYearSettlement()
        {
            PrintSummaryYearSettlementViewModel.Init();
            var view = new PrintSummaryYearSettlement { DataContext = PrintSummaryYearSettlementViewModel };
            DialogHost.Show(view, "RootDialog");
        }
    }
}
