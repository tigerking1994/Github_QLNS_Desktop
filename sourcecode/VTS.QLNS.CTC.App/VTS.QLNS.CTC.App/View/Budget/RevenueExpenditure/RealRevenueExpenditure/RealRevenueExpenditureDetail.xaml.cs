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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RealRevenueExpenditure;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RealRevenueExpenditure
{
    /// <summary>
    /// Interaction logic for RealRevenueExpenditureDetail.xaml
    /// </summary>
    public partial class RealRevenueExpenditureDetail : Window
    {
        public RealRevenueExpenditureDetail()
        {
            InitializeComponent();
            dgdDataRealRevenueExpenditureDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataRealRevenueExpenditureDetail.SelectedItem = e.Row.Item;
            var vm = (RealRevenueExpenditureDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa.Value || vm.SelectedItem.BHangCha
              || ((!string.IsNullOrEmpty(vm.Model.STongHop) && !vm.Model.STongHop.Equals(vm.Model.SSoChungTu)))))
            {
                e.Cancel = true;
            }
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                //TODO
                //scrollHeader.ScrollToHorizontalOffset(e.HorizontalOffset);
                //TODO
            }
        }
    }
}
