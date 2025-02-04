using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand3Y;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand3Y
{
    /// <summary>
    /// Interaction logic for RegularBudgetDetailWindow.xaml
    /// </summary>
    public partial class Demand3YDetail : Window
    {
        public Demand3YDetail()
        {
            InitializeComponent();
            DgDemand3YDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgDemand3YDetail.SelectedItem = e.Row.Item;
            var vm = (Demand3YDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable || vm.IsAnotherUserCreate))
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
