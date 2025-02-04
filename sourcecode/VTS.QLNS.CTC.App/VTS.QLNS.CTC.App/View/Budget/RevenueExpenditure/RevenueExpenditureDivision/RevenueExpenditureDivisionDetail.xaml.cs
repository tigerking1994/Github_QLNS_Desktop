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
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RevenueExpenditureDivision;

namespace VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RevenueExpenditureDivision
{
    /// <summary>
    /// Interaction logic for RevenueExpenditureDivisionDetail.xaml
    /// </summary>
    public partial class RevenueExpenditureDivisionDetail : Window
    {
        public RevenueExpenditureDivisionDetail()
        {
            InitializeComponent();
            dgdDataRevenueExpenditureDivsionDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataRevenueExpenditureDivsionDetail.SelectedItem = e.Row.Item;
            var vm = (RevenueExpenditureDivisionDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.IsLocked || !vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
        }
    }
}
