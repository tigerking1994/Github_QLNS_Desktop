using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu
{
    /// <summary>
    /// Interaction logic for PhanBoDuToanThuDetail.xaml
    /// </summary>
    public partial class PhanBoDuToanThuDetail : Window
    {
        public PhanBoDuToanThuDetail()
        {
            InitializeComponent();
            dgPhanBoDuToan.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgPhanBoDuToan.SelectedItem = e.Row.Item;
            var vm = (PhanBoDuToanThuDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dgPhanBoDuToan.ScrollIntoView(dgPhanBoDuToan.Items[0], dgPhanBoDuToan.Columns[0]);
        }
    }
}
