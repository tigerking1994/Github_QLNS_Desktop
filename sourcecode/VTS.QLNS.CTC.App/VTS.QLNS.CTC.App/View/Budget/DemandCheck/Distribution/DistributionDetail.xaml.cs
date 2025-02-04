using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution
{
    /// <summary>
    /// Interaction logic for RegularBudgetDetailWindow.xaml
    /// </summary>
    public partial class DistributionDetail : Window
    {
        public DistributionDetail()
        {
            InitializeComponent();
            DgDistributionDetail1.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgDistributionDetail1.SelectedItem = e.Row.Item;
            var vm = (DistributionDetailViewModel)this.DataContext;
            if (vm != null && (!vm.SelectedItem.IsEditable || vm.SelectedItem.IIdMaDonVi == "999"))
            {
                e.Cancel = true;
            }
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
