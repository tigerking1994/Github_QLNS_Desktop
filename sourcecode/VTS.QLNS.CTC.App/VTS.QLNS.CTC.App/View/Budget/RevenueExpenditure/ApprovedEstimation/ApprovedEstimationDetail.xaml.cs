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
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.ApprovedEstimation;

namespace VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.ApprovedEstimation
{
    /// <summary>
    /// Interaction logic for ApprovedEstimationDetail.xaml
    /// </summary>
    public partial class ApprovedEstimationDetail : Window
    {
        public ApprovedEstimationDetail()
        {
            InitializeComponent();
            dgdDataApprovedEstimationDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataApprovedEstimationDetail.SelectedItem = e.Row.Item;
            var vm = (ApprovedEstimationDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.IsLocked || !vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
        }
    }
}
