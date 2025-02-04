using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation;
using VTS.QLNS.CTC.App.ViewModel.Budget.CollectionsBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand;
using VTS.QLNS.CTC.App.ViewModel.Budget.Diagram;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget
{
    public class BudgetViewModel : ViewModelBase
    {
        private ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.BUDGET;
        public override string Name => "QUẢN LÝ NGÂN SÁCH";
        public override Type ContentType => typeof(View.Budget.Budget);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public DiagramViewModel DiagramViewModel { get; }
        public DemandCheckViewModel DemandCheckViewModel { get; }
        public EstimateViewModel EstimateViewModel { get; }
        public SettlementViewModel SettlementViewModel { get; }
        public AllocationViewModel AllocationViewModel { get; }
        public RevenueExpenditureViewModel RevenueExpenditureViewModel { get; }

        public BudgetViewModel(DiagramViewModel diagramViewModel,
            DemandCheckViewModel demandCheckViewModel,
            EstimateViewModel estimateViewModel,
            SettlementViewModel settlementViewModel,
            AllocationViewModel allocationViewModel,
            RevenueExpenditureViewModel revenueExpenditureViewModel,
            ISessionService sessionService)
        {
            DiagramViewModel = diagramViewModel;
            DemandCheckViewModel = demandCheckViewModel;
            EstimateViewModel = estimateViewModel;
            SettlementViewModel = settlementViewModel;
            AllocationViewModel = allocationViewModel;
            RevenueExpenditureViewModel = revenueExpenditureViewModel;
            _sessionService = sessionService;

            DiagramViewModel.ParentPage = this;
            DemandCheckViewModel.ParentPage = this;
            EstimateViewModel.ParentPage = this;
            SettlementViewModel.ParentPage = this;
            AllocationViewModel.ParentPage = this;
            RevenueExpenditureViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                //DiagramViewModel,
                DemandCheckViewModel,
                EstimateViewModel,
                AllocationViewModel,
                SettlementViewModel,
                RevenueExpenditureViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
