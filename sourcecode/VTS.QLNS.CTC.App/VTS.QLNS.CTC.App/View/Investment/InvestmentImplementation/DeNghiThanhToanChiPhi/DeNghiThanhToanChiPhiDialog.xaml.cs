using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.DeNghiThanhToanChiPhi
{
    /// <summary>
    /// Interaction logic for DeNghiThanhToanChiPhiDialog.xaml
    /// </summary>
    public partial class DeNghiThanhToanChiPhiDialog : UserControl
    {
        public DeNghiThanhToanChiPhiDialog()
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
