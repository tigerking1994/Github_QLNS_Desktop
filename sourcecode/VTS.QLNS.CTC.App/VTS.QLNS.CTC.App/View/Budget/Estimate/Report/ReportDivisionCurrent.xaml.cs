using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace VTS.QLNS.CTC.App.View.Budget.Estimate
{
    /// <summary>
    /// Interaction logic for UserControl3.xaml
    /// </summary>
    public partial class ReportDivisionCurrent : UserControl
    {
        public ReportDivisionCurrent()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }

        private void DgdReportDivisionCurrent_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrollBottom.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.popupbox.Focus();
        }
    }
}
