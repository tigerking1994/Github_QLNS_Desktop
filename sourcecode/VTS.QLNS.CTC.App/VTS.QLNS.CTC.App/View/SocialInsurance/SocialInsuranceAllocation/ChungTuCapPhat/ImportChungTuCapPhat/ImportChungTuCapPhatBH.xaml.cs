using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.ImportChungTuCapPhat;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.ImportChungTuCapPhat
{
    /// <summary>
    /// Interaction logic for ImportChungTuCapPhat.xaml
    /// </summary>
    public partial class ImportChungTuCapPhatBH : Window
    {
        public ImportChungTuCapPhatBH()
        {
            InitializeComponent();
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImportChungTuCapPhatViewModel)this.DataContext;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
