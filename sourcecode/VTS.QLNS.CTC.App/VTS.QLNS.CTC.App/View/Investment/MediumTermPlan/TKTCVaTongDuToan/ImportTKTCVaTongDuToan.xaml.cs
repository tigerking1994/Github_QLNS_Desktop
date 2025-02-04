using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.TKTCVaTongDuToan
{
    /// <summary>
    /// Interaction logic for ImportKhtBHXH.xaml
    /// </summary>
    public partial class ImportTKTCVaTongDuToan : Window
    {
        public ImportTKTCVaTongDuToan()
        {
            InitializeComponent();
        }
        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            //var vm = (ViewModel.Budget.Estimate.Import.ImportNdtctgBHXHViewModel)this.DataContext;
            //var button = sender as Button;
            //var index = vm.OnAddMLNS();
            //if (index == -1) return;
            //DgMLNS.UpdateLayout();
            //DgMLNS.ScrollIntoView(DgMLNS.Items[index]);
        }
    }
}
