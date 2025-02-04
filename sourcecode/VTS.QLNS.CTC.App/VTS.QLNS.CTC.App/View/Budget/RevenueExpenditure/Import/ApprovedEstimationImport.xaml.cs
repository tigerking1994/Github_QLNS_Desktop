using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Import
{
    /// <summary>
    /// Interaction logic for ApprovedEstimationImport.xaml
    /// </summary>
    public partial class ApprovedEstimationImport : Window
    {
        public ApprovedEstimationImport()
        {
            InitializeComponent();
        }
        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            //var vm = (ViewModel.Budget.Estimate.Import.DivisionImportViewModel)this.DataContext;
            //var button = sender as Button;
            //var index = vm.OnAddMLNS();
            //if (index == -1) return;
            //DgMLNS.UpdateLayout();
            //DgMLNS.ScrollIntoView(DgMLNS.Items[index]);
        }
    }
}
