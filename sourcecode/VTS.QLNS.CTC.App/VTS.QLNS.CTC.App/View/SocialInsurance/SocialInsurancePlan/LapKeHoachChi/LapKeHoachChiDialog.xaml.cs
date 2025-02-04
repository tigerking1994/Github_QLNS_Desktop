using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi
{
    /// <summary>
    /// Interaction logic for LapKeHoachChiDialog.xaml
    /// </summary>
    public partial class LapKeHoachChiDialog : UserControl
    {
        public LapKeHoachChiDialog()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DgDemandIndex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
