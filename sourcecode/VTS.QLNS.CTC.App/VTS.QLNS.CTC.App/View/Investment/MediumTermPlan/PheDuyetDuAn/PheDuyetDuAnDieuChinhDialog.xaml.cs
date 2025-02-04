using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PheDuyetDuAn
{
    /// <summary>
    /// Interaction logic for PheDuyetDuAnDieuChinhDialog.xaml
    /// </summary>
    public partial class PheDuyetDuAnDieuChinhDialog : Window
    {
        public PheDuyetDuAnDieuChinhDialog()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dgdDataChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataChiPhiDetail.SelectedItem = e.Row.Item;
            var selected = (VdtDaQddtChiPhiModel)e.Row.Item;
            var curentColumn = e.Column;

            if ((!selected.IsEditHangMuc || !selected.IsEditGiaTriChiPhi) && (curentColumn.SortMemberPath.Equals("GiaTriPheDuyet")  || curentColumn.SortMemberPath.Equals("GiaTriDieuChinh")
                || curentColumn.SortMemberPath.Equals("GiaTriSauDieuChinh")))
            {
                e.Cancel = true;
            }
        }

        private void dgdDataNguonVonDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataNguonVonDetail.SelectedItem = e.Row.Item;
        }

        private void FGiaTriPheDuyet_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (PheDuyetDuAnDieuChinhDialogViewModel)this.DataContext;
            var control = (TextBox)sender;
            if (vm.CheckChiPhiHaveHangMuc())
            {
                control.IsEnabled = false;
            }
            else
            {
                control.IsEnabled = true;
            }
        }
    }
}
