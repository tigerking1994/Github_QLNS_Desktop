using System.Windows;
using System.Windows.Controls;

namespace VTS.QLNS.CTC.App.View.Budget.Estimate.Import
{
    /// <summary>
    /// Interaction logic for DivisionImport.xaml
    /// </summary>
    public partial class DivisionImport : Window
    {
        public DivisionImport()
        {
            InitializeComponent();
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ViewModel.Budget.Estimate.Import.DivisionImportViewModel)this.DataContext;
            var button = sender as Button;
            var index = vm.OnAddMLNS();
            if (index == -1) return;
            DgMLNS.UpdateLayout();
            DgMLNS.ScrollIntoView(DgMLNS.Items[index]);
        }
    }
}
