using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi
{
    /// <summary>
    /// Interaction logic for DivisionEstimateDetail.xaml
    /// </summary>
    public partial class PhanBoDuToanChiDetail : Window
    {
        public PhanBoDuToanChiDetail()
        {
            InitializeComponent();
            dgPhanBoDuToan.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgPhanBoDuToan.SelectedItem = e.Row.Item;
            var vm = (PhanBoDuToanChiDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable || vm.IsAnotherUserCreate))
            {
                e.Cancel = true;
            }
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
