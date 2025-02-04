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
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ContractInfo;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.ContractInfo
{
    /// <summary>
    /// Interaction logic for ContractInfoDialog.xaml
    /// </summary>
    public partial class ContractInfoDialog : Window
    {
        public ContractInfoDialog()
        {
            InitializeComponent();
            dgdGoiThauNhaThauDiaLog.BeginningEdit += dgdGoiThauNhaThau_BeginEdit;
        }

        private void dgdGoiThauNhaThau_BeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (ContractInfoDialogViewModel)this.DataContext;
            dgdGoiThauNhaThauDiaLog.SelectedItem = e.Row.Item;
            if (vm != null && vm.IsViewOnly)
            {
                e.Cancel = true;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
