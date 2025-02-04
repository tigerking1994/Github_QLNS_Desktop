using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.SalaryYearPlan;

namespace VTS.QLNS.CTC.App.View.Salary.SalaryManagementPlan.SalaryYearPlan
{
    /// <summary>
    /// Interaction logic for SalaryYearPlanDetail.xaml
    /// </summary>
    public partial class SalaryYearPlanDetail : Window
    {
        public SalaryYearPlanDetail()
        {
            InitializeComponent();
            DgRegularSettlementDetailPlan.BeginningEdit += dgData_BeginningEdit;
        }

        private void dgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgRegularSettlementDetailPlan.SelectedItem = e.Row.Item;
            var vm = (SalaryYearPlanDetailViewModel)this.DataContext;
            if (vm != null && vm.SelectedItemChungTu.BHangCha == true)
            {
                e.Cancel = true;
            }
        }
    }
}
