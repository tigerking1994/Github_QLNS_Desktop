using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail;

namespace VTS.QLNS.CTC.App.View.Forex.ForexPlan.PlanDetail
{
    /// <summary>
    /// Interaction logic for DmTiGiaDialog.xaml
    /// </summary>
    public partial class DmTiGiaDialog : Window
    {
        public DmTiGiaDialog()
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