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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo
{
    /// <summary>
    /// Interaction logic for ForexContractHangMucDialog.xaml
    /// </summary>
    public partial class ForexContractHangMucDialog : Window
    {
        public ForexContractHangMucDialog()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void dgdDataNhDaHopDongHangMucDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataNhDaHopDongHangMucDetail.SelectedItem = e.Row.Item;
        }

        private void FTienHopDongHangMuc_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (ForexContractHangMucDialogViewModel)this.DataContext;
            var control = (TextBox)sender;
            if (vm.CheckHangMucCanEditGiatri())
            {
                control.IsEnabled = true;
            }
            else
            {
                control.IsEnabled = false;
            }
        }
    }
}
