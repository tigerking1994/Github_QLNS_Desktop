using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace VTS.QLNS.CTC.App.View.Forex.ForexPlan.PlanOverview
{
    /// <summary>
    /// Interaction logic for Forex.xaml
    /// </summary>
    public partial class PlanOverviewIndex : UserControl
    {
        public PlanOverviewIndex()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}