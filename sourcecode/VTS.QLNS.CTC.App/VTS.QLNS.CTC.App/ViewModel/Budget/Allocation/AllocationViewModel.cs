using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.Budget.Report;
using System.Collections.Generic;


namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation
{
    public class AllocationViewModel : ViewModelBase 
    {
        private IMapper _mapper;
        private IServiceProvider _provider;
        private ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.BUDGET_ALLOCATION;
        public override string Name => "CẤP PHÁT";
        public override string Description => "Cấp phát đơn vị";
        public override Type ContentType => typeof(View.Budget.Allocation.Allocation);
        public override PackIconKind IconKind => PackIconKind.LayersTriple;
         
        public AllocationReceiveIndexViewModel AllocationReceiveIndexViewModel { get; }
        public AllocationIndexViewModel AllocationIndexViewModel { get; }
        public BudgetReportIndexViewModel BudgetReportIndexViewModel { get; }

        public AllocationViewModel(
            AllocationReceiveIndexViewModel allocationReceiveIndexViewModel,
            AllocationIndexViewModel allocationIndexViewModel,
            BudgetReportIndexViewModel budgetReportIndexViewModel,
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _provider = provider;

            AllocationReceiveIndexViewModel = allocationReceiveIndexViewModel;
            AllocationIndexViewModel = allocationIndexViewModel;
            BudgetReportIndexViewModel = budgetReportIndexViewModel;

            AllocationReceiveIndexViewModel.ParentPage = this;
            AllocationIndexViewModel.ParentPage = this;
            BudgetReportIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            BudgetReportIndexViewModel.ListLoaiBaoCao = new List<string> { NSConstants.CAP_PHAT };
            BudgetReportIndexViewModel.Name = "Báo cáo Cấp phát";
            BudgetReportIndexViewModel.Description = "Danh mục báo cáo Cấp phát";
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                AllocationReceiveIndexViewModel,
                AllocationIndexViewModel,
                BudgetReportIndexViewModel,
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }

    }
}
