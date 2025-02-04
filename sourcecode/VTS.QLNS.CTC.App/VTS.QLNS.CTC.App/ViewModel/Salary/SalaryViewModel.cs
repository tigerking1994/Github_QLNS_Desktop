using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.Salary.Cadres;
using VTS.QLNS.CTC.App.ViewModel.Salary.Report;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryUtilities;
using VTS.QLNS.CTC.App.ViewModel.Salary.Settlement;
using VTS.QLNS.CTC.App.ViewModel.Salary.TransferData;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary
{
    public class SalaryViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.SALARY;
        public override string Name => "QUẢN LÝ LƯƠNG";
        public override Type ContentType => typeof(View.Salary.Salary);
        public override PackIconKind IconKind => PackIconKind.CashMultiple;

        public CategorySalaryViewModel CategorySalaryViewModel { get; }
        public CadresViewModel CadresViewModel { get; }
        public SalaryManagementViewModel SalaryManagementViewModel { get; }
        public SalarySettlementViewModel SalarySettlementViewModel { get; }
        public SalaryManagementPlanViewModel SalaryManagementPlanViewModel { get; }
        public TransferDataViewModel TransferDataViewModel { get; }
        public SalaryUtilitiesViewModel SalaryUtilitiesViewModel { get; }

        public SalaryViewModel(
            CadresViewModel cadresViewModel,
            SalaryManagementViewModel salaryManagementViewModel,
            SalarySettlementViewModel salarySettlementViewModel,
            SalaryManagementPlanViewModel salaryManagementPlanViewModel,
            CategorySalaryViewModel categorySalaryViewModel,
            TransferDataViewModel transferDataViewModel,
            SalaryUtilitiesViewModel salaryUtilitiesViewModel)
        {
            CadresViewModel = cadresViewModel;
            SalaryManagementViewModel = salaryManagementViewModel;
            SalarySettlementViewModel = salarySettlementViewModel;
            SalaryManagementPlanViewModel = salaryManagementPlanViewModel;
            CategorySalaryViewModel = categorySalaryViewModel;
            TransferDataViewModel = transferDataViewModel;
            SalaryUtilitiesViewModel = salaryUtilitiesViewModel;

            CategorySalaryViewModel.ParentPage = this;
            CadresViewModel.ParentPage = this;
            SalaryManagementViewModel.ParentPage = this;
            SalarySettlementViewModel.ParentPage = this;
            SalaryManagementPlanViewModel.ParentPage = this;
            TransferDataViewModel.ParentPage = this;
            SalaryUtilitiesViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                CadresViewModel,
                SalaryManagementViewModel,
                SalarySettlementViewModel,
                SalaryManagementPlanViewModel,
                CategorySalaryViewModel,
                TransferDataViewModel,
                SalaryUtilitiesViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
