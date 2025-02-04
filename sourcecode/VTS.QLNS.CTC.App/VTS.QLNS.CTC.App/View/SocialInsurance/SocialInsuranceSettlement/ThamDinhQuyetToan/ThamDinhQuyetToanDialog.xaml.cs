using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan
{
    /// <summary>
    /// Interaction logic for CapPhatBoSungDialog.xaml
    /// </summary>
    public partial class ThamDinhQuyetToanDialog : UserControl
    {
        public ThamDinhQuyetToanDialog()
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
