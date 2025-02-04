using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand3Y;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Expertise;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PlanAgency;
using VTS.QLNS.CTC.App.ViewModel.Budget.Report;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck
{
    public class DemandCheckViewModel : ViewModelBase
    {
        private ISessionService _sessionService;

        public override string Name => "SỐ NHU CẦU - KIỂM TRA";
        public override string Title => "SỐ NHU CẦU - KIỂM TRA";
        public override string Description => "Số nhu cầu, số kiểm tra";
        public override Type ContentType => typeof(View.Budget.DemandCheck.DemandCheck);
        public override PackIconKind IconKind => PackIconKind.Input;
        public override string FuncCode => NSFunctionCode.BUDGET_DEMANDCHECK;

        public DemandCheckFunctionMapViewModel DemandCheckFunctionMapViewModel { get; }
        public DemandIndexViewModel DemandIndexViewModel { get; }
        public Demand3YIndexViewModel Demand3YIndexViewModel { get; }
        public CheckIndexViewModel CheckIndexViewModel { get; }
        public PlanBeginYearIndexViewModel PlanBeginYearViewModel { get; }
        public DistributionIndexViewModel DistributionIndexViewModel { get; }
        public ExpertiseIndexViewModel ExpertiseIndexViewModel { get; }
        public PlanAgencyBeginYearIndexViewModel PlanAgencyBeginYearIndexViewModel { get; }
        public BudgetReportIndexViewModel BudgetReportIndexViewModel { get; }

        public DemandCheckViewModel(
            DemandCheckFunctionMapViewModel demandCheckFunctionMapViewModel,
            DemandIndexViewModel demandIndexViewModel,
            Demand3YIndexViewModel demand3YIndexViewModel,
            DistributionIndexViewModel distributionIndexViewModel,
            CheckIndexViewModel checkIndexViewModel,
            ExpertiseIndexViewModel expertiseIndexViewModel,
            PlanBeginYearIndexViewModel planBeginYearViewModel,
            PlanAgencyBeginYearIndexViewModel planAgencyBeginYearIndexViewModel,
            BudgetReportIndexViewModel budgetReportIndexViewModel,
            ISessionService sessionService)
        {
            DemandCheckFunctionMapViewModel = demandCheckFunctionMapViewModel;
            DemandIndexViewModel = demandIndexViewModel;
            Demand3YIndexViewModel = demand3YIndexViewModel;
            CheckIndexViewModel = checkIndexViewModel;
            DistributionIndexViewModel = distributionIndexViewModel;
            PlanBeginYearViewModel = planBeginYearViewModel;
            ExpertiseIndexViewModel = expertiseIndexViewModel;
            PlanAgencyBeginYearIndexViewModel = planAgencyBeginYearIndexViewModel;
            BudgetReportIndexViewModel = budgetReportIndexViewModel;

            DemandCheckFunctionMapViewModel.ParentPage = this;
            DemandIndexViewModel.ParentPage = this;
            Demand3YIndexViewModel.ParentPage = this;
            CheckIndexViewModel.ParentPage = this;
            DistributionIndexViewModel.ParentPage = this;
            PlanBeginYearViewModel.ParentPage = this;
            ExpertiseIndexViewModel.ParentPage = this;
            BudgetReportIndexViewModel.ParentPage = this;
            _sessionService = sessionService;
        }

        public override void Init()
        {
            MarginRequirement = new Thickness(0);
            BudgetReportIndexViewModel.ListLoaiBaoCao = new List<string>
            {
                NSConstants.SO_NHU_CAU,
                NSConstants.SO_NHU_CAU_3_NAM,
                NSConstants.NGANH_THAM_DINH,
                NSConstants.SO_KIEM_TRA_NHAN,
                NSConstants.SO_KIEM_TRA_PHAN_BO,
                NSConstants.DU_TOAN_DAU_NAM
            };
            BudgetReportIndexViewModel.Name = "Báo cáo SNC - SKT";
            BudgetReportIndexViewModel.Description = "Danh mục báo cáo Số nhu cầu - Số kiểm tra";
            Documentation = new ObservableCollection<ViewModelBase>
            {
                DemandIndexViewModel,
                Demand3YIndexViewModel,
                ExpertiseIndexViewModel,
                CheckIndexViewModel,
                DistributionIndexViewModel,
                PlanBeginYearViewModel,
                BudgetReportIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }

        public override bool CheckPermission()
        {
            int yearOfBudget = _sessionService.Current.YearOfBudget;
            if (yearOfBudget != NAM_NGAN_SACH.NAM_NAY)
            {
                return false;
            }
            return base.CheckPermission();
        }
    }
}