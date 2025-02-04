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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexDeNghiThanhToan;

namespace VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexDeNghiThanhToan
{
    /// <summary>
    /// Interaction logic for ForexDeNghiThanhToanDialog.xaml
    /// </summary>
    public partial class ForexDeNghiThanhToanDialog : Window
    {
        public ForexDeNghiThanhToanDialog()
        {
            InitializeComponent();
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void USDKyNay_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (ForexDeNghiThanhToanDialogViewModel)this.DataContext;
            var txtBox = (TextBox)sender;
            var itemSelected = (NhTtThanhToanChiTietModel)txtBox.DataContext;
            if (itemSelected != null)
            {
                vm.SelectedNhTtThanhToanChiTiet = itemSelected;
            }
            vm.GetInfoMucLucNs();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
