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
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.DefenseBudget;

namespace VTS.QLNS.CTC.App.View.Budget.Settlement.DefenseBudget
{
    /// <summary>
    /// Interaction logic for DefenseBudgetDetail.xaml
    /// </summary>
    public partial class DefenseBudgetDetail : Window
    {
        public DefenseBudgetDetail()
        {
            InitializeComponent();
            DgDefenseBudgetSelfPay1.BeginningEdit += dgdDataSelfPay_BeginningEdit;
        }

        private void dgdDataSelfPay_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgDefenseBudgetSelfPay1.SelectedItem = e.Row.Item;
            var vm = (DefenseBudgetDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable || !vm.IsEditByRole
                || ((!string.IsNullOrEmpty(vm.Model.STongHop) && !vm.Model.STongHop.Equals(vm.Model.SSoChungTu)) && !vm.IsShowColumnDonVi && e.Column.SortMemberPath != nameof(SettlementVoucherDetailModel.SGhiChu))
                || (vm.IsReadonlyDeNghi && e.Column.SortMemberPath == nameof(SettlementVoucherDetailModel.FTuChiDeNghi))))
            {
                e.Cancel = true;
            }
        }

        private void DgDefenseBudgetSelfPay_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrollBottom.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
