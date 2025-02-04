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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan
{
    /// <summary>
    /// Interaction logic for ImportPlanBeginYear.xaml
    /// </summary>
    public partial class ImportPlanBeginYear : Window
    {
        public ImportPlanBeginYear()
        {
            InitializeComponent();
        }

        private void CbChungTu_SelectionChange(object sender, EventArgs e)
        {
            var cbChungTu = (ComboBox)sender;
            if(cbChungTu.SelectedValue == VoucherType.NSSD_Key)
            {
                RbSettlementVoucherDetail.IsChecked = true;
            }
            else
            {
                RbCanCu.IsChecked = true;
            }
        }

        private void BtnHuyBo_OnClick(object sender, EventArgs e)
        {
            RbSettlementVoucherDetail.IsChecked = true;
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm= (ImportPlanBeginYearViewModel)this.DataContext;
            var button = sender as Button;
            RbError.IsChecked = true;
            var index = vm.OnAddMLNS();
            if (index == -1) return;
            DgMLNS.UpdateLayout();
            DgMLNS.ScrollIntoView(DgMLNS.Items[index]);
        }

        private void ButtonOpenErrorDacThu_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImportPlanBeginYearViewModel)this.DataContext;
            var button = sender as Button;
            RbError.IsChecked = true;
            var index = vm.OnAddMLNSCanCu();
            if (index == -1) return;
            DgMLNS.UpdateLayout();
            DgMLNS.ScrollIntoView(DgMLNS.Items[index]);
        }

        private void ButtonOpenErrorPhanCap_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImportPlanBeginYearViewModel)this.DataContext;
            var button = sender as Button;
            RbError.IsChecked = true;
            var index = vm.OnAddMLNSPhanCap();
            if (index == -1) return;
            DgMLNS.UpdateLayout();
            DgMLNS.ScrollIntoView(DgMLNS.Items[index]);
        }
    }
}
