using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Salary.Report.ReportSalaryPursuit;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CalculateSalary;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CategoryHoliday;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.InsuranceSalaryMonthTable;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.PursuitSalaryMonthTable;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.SalaryComparison;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.SalaryDevelopments;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.SalaryTableMonth;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement
{
    public class SalaryManagementViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT;
        public override string Name => "QUẢN LÝ LƯƠNG";
        public override string Description => "Quản lý lương";
        public override Type ContentType => typeof(View.Salary.SalaryManagement.SalaryManagement);
        public override PackIconKind IconKind => PackIconKind.CashPlus;
        public CalculateSalaryIndexViewModel CalculateSalaryIndexViewModel { get; }
        public PursuitSalaryIndexViewModel PursuitSalaryIndexViewModel { get; }
        public SalaryTableMonthIndexViewModel SalaryTableMonthViewModel { get; }
        public InsuranceSalaryIndexViewModel InsuranceSalaryIndexViewModel { get; }
        public BackPaySalaryIndexViewModel BackPaySalaryIndexViewModel { get; }
        public SalaryComparisonIndexViewModel SalaryComparisonIndexViewModel { get; }
        public SalaryDevelopmentsIndexViewModel SalaryDevelopmentsIndexViewModel { get; }
        public ReportIndexViewModel ListReportIndexViewModel { get; }

        public SalaryManagementViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider,
            CalculateSalaryIndexViewModel calculateSalaryIndexViewModel,
            PursuitSalaryIndexViewModel pursuitSalaryIndexViewModel,
            SalaryTableMonthIndexViewModel salaryTableMonthViewModel,
            InsuranceSalaryIndexViewModel insuranceSalaryIndexViewModel,
            BackPaySalaryIndexViewModel backPaySalaryIndexViewModel,
            SalaryComparisonIndexViewModel salaryComparisonIndexViewModel,
            ReportIndexViewModel listReportIndexViewModel,
            SalaryDevelopmentsIndexViewModel salaryDevelopmentsIndexViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _provider = provider;

            CalculateSalaryIndexViewModel = calculateSalaryIndexViewModel;
            PursuitSalaryIndexViewModel = pursuitSalaryIndexViewModel;
            SalaryTableMonthViewModel = salaryTableMonthViewModel;
            InsuranceSalaryIndexViewModel = insuranceSalaryIndexViewModel;
            SalaryComparisonIndexViewModel = salaryComparisonIndexViewModel;
            SalaryDevelopmentsIndexViewModel = salaryDevelopmentsIndexViewModel;
            ListReportIndexViewModel = listReportIndexViewModel;
            BackPaySalaryIndexViewModel = backPaySalaryIndexViewModel;

            calculateSalaryIndexViewModel.ParentPage = this;
            pursuitSalaryIndexViewModel.ParentPage = this;
            salaryTableMonthViewModel.ParentPage = this;
            backPaySalaryIndexViewModel.ParentPage = this;
            salaryComparisonIndexViewModel.ParentPage = this;
            listReportIndexViewModel.ParentPage = this;
            salaryDevelopmentsIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            InitDocument();
        }

        public void InitDocument()
        {
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                //CalculateSalaryIndexViewModel,
                SalaryTableMonthViewModel,
                PursuitSalaryIndexViewModel,
                InsuranceSalaryIndexViewModel,
                BackPaySalaryIndexViewModel,
                SalaryComparisonIndexViewModel,
                //SalaryComparisonIndexViewModel,
                SalaryDevelopmentsIndexViewModel,
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
