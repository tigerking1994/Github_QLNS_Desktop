using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check
{
    /// <summary>
    /// Interaction logic for RegularBudgetDetailWindow.xaml
    /// </summary>
    public partial class CheckDetail : Window
    {
        public CheckDetail()
        {
            InitializeComponent();
            DgCheckDetail2.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgCheckDetail2.SelectedItem = e.Row.Item;
            var vm = (CheckDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable))
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
