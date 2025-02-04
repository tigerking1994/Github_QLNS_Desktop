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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision;

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision
{
    /// <summary>
    /// Interaction logic for DecisionApprovingNegotiationResultsDialog.xaml
    /// </summary>
    public partial class DAHDNKDecisionDialog : Window
    {
        public DAHDNKDecisionDialog()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dgdDanhSachGoiThau_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (DAHDNKDecisionDialogViewModel)this.DataContext;
            if (vm != null)
            {
                vm.DanhSachGoiThau_BeginningEdit(e);
            }
        }
    }
}