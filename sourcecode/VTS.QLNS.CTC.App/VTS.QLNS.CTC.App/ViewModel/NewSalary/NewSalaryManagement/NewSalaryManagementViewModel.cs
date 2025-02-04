using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewCalculateSalary;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewCategoryHoliday;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewPursuitSalaryMonthTable;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewSalaryComparison;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewSalaryDevelopments;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewSalaryTableMonth;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement
{
    public class NewSalaryManagementViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT;
        public override string Name => "QUẢN LÝ LƯƠNG";
        public override string Description => "Quản lý lương";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagement.NewSalaryManagement);
        public override PackIconKind IconKind => PackIconKind.CashPlus;
        public NewCalculateSalaryIndexViewModel CalculateSalaryIndexViewModel { get; }
        public NewPursuitSalaryIndexViewModel PursuitSalaryIndexViewModel { get; }
        public NewSalaryTableMonthIndexViewModel SalaryTableMonthViewModel { get; }
        public NewInsuranceSalaryIndexViewModel InsuranceSalaryIndexViewModel { get; }
        public NewSalaryComparisonIndexViewModel SalaryComparisonIndexViewModel { get; }
        public NewSalaryDevelopmentsIndexViewModel SalaryDevelopmentsIndexViewModel { get; }
        public NewReportIndexViewModel ListReportIndexViewModel { get; }

        public NewSalaryManagementViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider,
            NewCalculateSalaryIndexViewModel calculateSalaryIndexViewModel,
            NewPursuitSalaryIndexViewModel pursuitSalaryIndexViewModel,
            NewSalaryTableMonthIndexViewModel salaryTableMonthViewModel,
            NewInsuranceSalaryIndexViewModel insuranceSalaryIndexViewModel,
            NewSalaryComparisonIndexViewModel salaryComparisonIndexViewModel,
            NewReportIndexViewModel listReportIndexViewModel,
            NewSalaryDevelopmentsIndexViewModel salaryDevelopmentsIndexViewModel)
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

            calculateSalaryIndexViewModel.ParentPage = this;
            pursuitSalaryIndexViewModel.ParentPage = this;
            salaryTableMonthViewModel.ParentPage = this;
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
