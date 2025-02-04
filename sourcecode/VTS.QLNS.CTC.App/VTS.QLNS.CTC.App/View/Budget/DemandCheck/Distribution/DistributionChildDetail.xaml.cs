using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution
{
    /// <summary>
    /// Interaction logic for RegularBudgetDetailWindow.xaml
    /// </summary>
    public partial class DistributionChildDetail : Window
    {
        public DistributionChildDetail()
        {
            InitializeComponent();
            DgDistributionChildDetail1.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgDistributionChildDetail1.SelectedItem = e.Row.Item;
            if (((NsSktChungTuChiTietModel)e.Row.Item).IsHangCha)
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
