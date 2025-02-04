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
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.ForexBudget;

namespace VTS.QLNS.CTC.App.View.Budget.Settlement.ForexBudget
{
    /// <summary>
    /// Interaction logic for ForexBudgetDetail.xaml
    /// </summary>
    public partial class ForexBudgetDetail : Window
    {
        public ForexBudgetDetail()
        {
            InitializeComponent();
            DgForexBudgetSelfPay1.BeginningEdit += dgdDataSelfPay_BeginningEdit;
        }

        private void dgdDataSelfPay_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgForexBudgetSelfPay1.SelectedItem = e.Row.Item;
            var vm = (ForexBudgetDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable || !vm.IsEditByRole
                || ((!string.IsNullOrEmpty(vm.Model.STongHop) && !vm.Model.STongHop.Equals(vm.Model.SSoChungTu)) && !vm.IsShowColumnDonVi && e.Column.SortMemberPath != nameof(SettlementVoucherDetailModel.SGhiChu))
                || (vm.IsReadonlyDeNghi && e.Column.SortMemberPath == nameof(SettlementVoucherDetailModel.FTuChiDeNghi))))
            {
                e.Cancel = true;
            }
        }

        private void DgForexBudgetSelfPay_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrollBottom.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
