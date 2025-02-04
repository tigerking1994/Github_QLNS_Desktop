using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan;

namespace VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan
{
    /// <summary>
    /// Interaction logic for SalaryYearPlanDetail.xaml
    /// </summary>
    public partial class NewSalaryYearPlanDetail : Window
    {
        public NewSalaryYearPlanDetail()
        {
            InitializeComponent();
            DgRegularSettlementDetailPlan.BeginningEdit += dgData_BeginningEdit;
        }

        private void dgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgRegularSettlementDetailPlan.SelectedItem = e.Row.Item;
            var vm = (NewSalaryYearPlanDetailViewModel)this.DataContext;
            if (vm != null && vm.SelectedItemChungTu.BHangCha == true)
            {
                e.Cancel = true;
            }
        }
    }
}
