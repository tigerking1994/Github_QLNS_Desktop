using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT
{
    /// <summary>
    /// Interaction logic for DivisionEstimateDetail.xaml
    /// </summary>
    public partial class PhanBoDuToanThuMuaBHYTDetail : Window
    {
        public PhanBoDuToanThuMuaBHYTDetail()
        {
            InitializeComponent();
            dgPhanBoDuToan.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgPhanBoDuToan.SelectedItem = e.Row.Item;
            var vm = (PhanBoDuToanThuMuaBHYTDetailViewModel)this.DataContext;
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
