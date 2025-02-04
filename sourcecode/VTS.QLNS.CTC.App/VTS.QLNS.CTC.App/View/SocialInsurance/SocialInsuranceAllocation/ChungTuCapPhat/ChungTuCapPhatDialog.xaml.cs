using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat
{
    /// <summary>
    /// Interaction logic for ChungTuCapPhatDialog.xaml
    /// </summary>
    public partial class ChungTuCapPhatDialog : UserControl
    {
        public ChungTuCapPhatDialog()
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