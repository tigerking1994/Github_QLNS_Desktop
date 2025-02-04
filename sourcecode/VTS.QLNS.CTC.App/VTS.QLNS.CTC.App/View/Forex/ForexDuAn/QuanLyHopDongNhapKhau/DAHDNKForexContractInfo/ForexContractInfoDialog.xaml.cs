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
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKForexContractInfo;

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKForexContractInfo
{
    /// <summary>
    /// Interaction logic for ForexContractInfoDialog.xaml
    /// </summary>
    public partial class ForexContractInfoDialog : Window
    {
        public ForexContractInfoDialog()
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
            var vm = (ForexContractInfoDialogViewModel)this.DataContext;
            if (vm != null)
            {
                vm.HangMuc_BeginningEditHanlder(e);
            }
        }
        private void FTienHopDongHangMuc_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (ForexContractInfoDialogViewModel)this.DataContext;
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
