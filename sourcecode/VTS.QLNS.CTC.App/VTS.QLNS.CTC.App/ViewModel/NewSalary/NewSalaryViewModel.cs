using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewTransferData;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary
{
    public class NewSalaryViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.NEW_SALARY;
        public override string Name => "QUẢN LÝ LƯƠNG NQ27";
        public override Type ContentType => typeof(View.NewSalary.NewSalary);
        public override PackIconKind IconKind => PackIconKind.CreditCard;

        public CategoryNewSalaryViewModel CategorySalaryViewModel { get; }
        public NewCadresViewModel CadresViewModel { get; }
        public NewSalaryManagementViewModel SalaryManagementViewModel { get; }
        public NewSalarySettlementViewModel SalarySettlementViewModel { get; }
        public NewSalaryManagementPlanViewModel SalaryManagementPlanViewModel { get; }
        public NewTransferDataViewModel TransferDataViewModel { get; }

        public NewSalaryViewModel(
            NewCadresViewModel cadresViewModel,
            NewSalaryManagementViewModel salaryManagementViewModel,
            NewSalarySettlementViewModel salarySettlementViewModel,
            NewSalaryManagementPlanViewModel salaryManagementPlanViewModel,
            CategoryNewSalaryViewModel categorySalaryViewModel,
            NewTransferDataViewModel transferDataViewModel)
        {
            CadresViewModel = cadresViewModel;
            SalaryManagementViewModel = salaryManagementViewModel;
            SalarySettlementViewModel = salarySettlementViewModel;
            SalaryManagementPlanViewModel = salaryManagementPlanViewModel;
            CategorySalaryViewModel = categorySalaryViewModel;
            TransferDataViewModel = transferDataViewModel;

            CategorySalaryViewModel.ParentPage = this;
            CadresViewModel.ParentPage = this;
            SalaryManagementViewModel.ParentPage = this;
            SalarySettlementViewModel.ParentPage = this;
            SalaryManagementPlanViewModel.ParentPage = this;
            TransferDataViewModel.ParentPage = this;
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
                TransferDataViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
