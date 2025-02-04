using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.Import
{
    /// <summary>
    /// Interaction logic for CapPhatBoSungImport.xaml
    /// </summary>
    public partial class CapPhatBoSungImport : Window
    {
        public CapPhatBoSungImport()
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
