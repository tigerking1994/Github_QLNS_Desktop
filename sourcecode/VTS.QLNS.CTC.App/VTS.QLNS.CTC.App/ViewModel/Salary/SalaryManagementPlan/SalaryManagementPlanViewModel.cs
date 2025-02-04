using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.ConfigurationUpRank;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.AllowenceAdjustment;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CadresPlan;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.MilitaryAdjustment;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.SalaryYearPlan;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.Salary.Report.ReportSalaryPursuit;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CanBoNghiHuuKeHoach;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CBNHKeHoach;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan
{
    public class SalaryManagementPlanViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string Name => "QUẢN LÝ LƯƠNG KẾ HOẠCH";
        public override string Description => "Tính lương năm kế hoạch";
        public override Type ContentType => typeof(View.Salary.Settlement.Settlement);
        public override PackIconKind IconKind => PackIconKind.Abacus;
        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH;

        public ConfigurationUpRankIndexViewModel ConfigurationUpRankIndexViewModel { get; }
        public AllowenceAdjustmentIndexViewModel AllowenceAdjustmentIndexViewModel { get; }
        public CadresPlanIndexViewModel CadresPlanIndexViewModel { get; }
        public MilitaryAdjustmentViewModel MilitaryAdjustmentViewModel { get; }
        public SalaryYearPlanIndexViewModel SalaryYearPlanIndexViewModel { get; }
        public ReportIndexViewModel ListReportIndexViewModel { get; }
        //public CBNHKeHoachViewModel CBNHKeHoachViewModel { get; }

        //public CanBoNghiHuuKeHoachViewModel CanBoNghiHuuKeHoachViewModel { get; }

        public SalaryManagementPlanViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider,
            AllowenceAdjustmentIndexViewModel allowenceAdjustmentIndexViewModel,
            CadresPlanIndexViewModel cadresPlanIndexViewModel,
            ConfigurationUpRankIndexViewModel configurationUpRankIndexViewModel,
            MilitaryAdjustmentViewModel militaryAdjustmentViewModel,
            SalaryYearPlanIndexViewModel salaryYearPlanIndexViewModel,
            ReportIndexViewModel listReportIndexViewModel
            //CBNHKeHoachViewModel cBNHKeHoachViewModel,
            //CanBoNghiHuuKeHoachViewModel canBoNghiHuuKeHoachViewModel
            )
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
            //CBNHKeHoachViewModel = cBNHKeHoachViewModel;
            //CanBoNghiHuuKeHoachViewModel = canBoNghiHuuKeHoachViewModel;

            configurationUpRankIndexViewModel.ParentPage = this;
            allowenceAdjustmentIndexViewModel.ParentPage = this;
            cadresPlanIndexViewModel.ParentPage = this;
            militaryAdjustmentViewModel.ParentPage = this;
            salaryYearPlanIndexViewModel.ParentPage = this;
            listReportIndexViewModel.ParentPage = this;
            //cBNHKeHoachViewModel.ParentPage = this;
            //canBoNghiHuuKeHoachViewModel.ParentPage = this;
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
                //CBNHKeHoachViewModel,
                //CanBoNghiHuuKeHoachViewModel,
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
