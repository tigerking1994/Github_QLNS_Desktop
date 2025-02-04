using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewConfigurationUpRank;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewAllowenceAdjustment;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewCadresPlan;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewMilitaryAdjustment;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan
{
    public class NewSalaryManagementPlanViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string Name => "QUẢN LÝ LƯƠNG KẾ HOẠCH";
        public override string Description => "Tính lương năm kế hoạch";
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewSettlement);
        public override PackIconKind IconKind => PackIconKind.Abacus;
        public override string FuncCode => NSFunctionCode.NEW_SALARY_QUAN_LY_LUONG_KE_HOACH;

        public NewConfigurationUpRankIndexViewModel ConfigurationUpRankIndexViewModel { get; }
        public NewAllowenceAdjustmentIndexViewModel AllowenceAdjustmentIndexViewModel { get; }
        public NewCadresPlanIndexViewModel CadresPlanIndexViewModel { get; }
        public NewMilitaryAdjustmentViewModel MilitaryAdjustmentViewModel { get; }
        public NewSalaryYearPlanIndexViewModel SalaryYearPlanIndexViewModel { get; }
        public NewReportIndexViewModel ListReportIndexViewModel { get; }

        public NewSalaryManagementPlanViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider,
            NewAllowenceAdjustmentIndexViewModel allowenceAdjustmentIndexViewModel,
            NewCadresPlanIndexViewModel cadresPlanIndexViewModel,
            NewConfigurationUpRankIndexViewModel configurationUpRankIndexViewModel,
            NewMilitaryAdjustmentViewModel militaryAdjustmentViewModel,
            NewSalaryYearPlanIndexViewModel salaryYearPlanIndexViewModel,
            NewReportIndexViewModel listReportIndexViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _provider = provider;

            ConfigurationUpRankIndexViewModel = configurationUpRankIndexViewModel;
            AllowenceAdjustmentIndexViewModel = allowenceAdjustmentIndexViewModel;
            CadresPlanIndexViewModel = cadresPlanIndexViewModel;
            MilitaryAdjustmentViewModel = militaryAdjustmentViewModel;
            SalaryYearPlanIndexViewModel = salaryYearPlanIndexViewModel;
            ListReportIndexViewModel = listReportIndexViewModel;

            configurationUpRankIndexViewModel.ParentPage = this;
            allowenceAdjustmentIndexViewModel.ParentPage = this;
            cadresPlanIndexViewModel.ParentPage = this;
            militaryAdjustmentViewModel.ParentPage = this;
            salaryYearPlanIndexViewModel.ParentPage = this;
            listReportIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            InitDocument();
        }

        private void InitDocument()
        {
            Documentation = new System.Collections.ObjectModel.ObservableCollection<ViewModelBase>()
            {
                AllowenceAdjustmentIndexViewModel,
                ConfigurationUpRankIndexViewModel,
                CadresPlanIndexViewModel,
                MilitaryAdjustmentViewModel,
                SalaryYearPlanIndexViewModel,
                ListReportIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }

        public void RedirectPage(object sender, EventArgs e)
        {
            InitDocument();
        }
    }
}
