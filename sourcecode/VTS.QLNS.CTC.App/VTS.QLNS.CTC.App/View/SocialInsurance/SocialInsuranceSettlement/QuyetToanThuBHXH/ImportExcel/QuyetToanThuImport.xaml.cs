using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.ImportExcel
{
    /// <summary>
    /// Interaction logic for QuyetToanThuImport.xaml
    /// </summary>
    public partial class QuyetToanThuImport : Window
    {
        public QuyetToanThuImport()
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
