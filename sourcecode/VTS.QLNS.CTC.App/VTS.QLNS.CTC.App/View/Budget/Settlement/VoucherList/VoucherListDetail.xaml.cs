using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.VoucherList;

namespace VTS.QLNS.CTC.App.View.Budget.Settlement.VoucherList
{
    /// <summary>
    /// Interaction logic for VoucherListDetail.xaml
    /// </summary>
    public partial class VoucherListDetail : Window
    {
        public VoucherListDetail()
        {
            InitializeComponent();
            DgVoucherListDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgVoucherListDetail.SelectedItem = e.Row.Item;
            var vm = (VoucherListDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
        }

        private void DgVoucherListDetail_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrollBottom.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
