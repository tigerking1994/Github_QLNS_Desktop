using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan
{
    /// <summary>
    /// Interaction logic for PlanBudgetBeginYearDetail.xaml
    /// </summary>
    public partial class PlanBudgetBeginYearDetail : Window
    {
        public PlanBudgetBeginYearDetail()
        {
            InitializeComponent();
            dgdDataRevenuePlanBudgetBeginYearDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataRevenuePlanBudgetBeginYearDetail.SelectedItem = e.Row.Item;
            var vm = (PlanBudgetBeginYearDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable || !string.IsNullOrEmpty(vm.Model.SDSSoChungTuTongHop)))
            {
                e.Cancel = true;
            }
        }
    }
}
