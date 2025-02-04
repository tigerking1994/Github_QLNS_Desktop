using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi
{
    /// <summary>
    /// Interaction logic for DivisionEstimateDetail.xaml
    /// </summary>
    public partial class PhanBoDuToanChiTietChiKPQL : Window
    {
        public PhanBoDuToanChiTietChiKPQL()
        {
            InitializeComponent();
            dgPhanBoDuToanKQPL.BeginningEdit += dgdData_BeginningEdit;

        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgPhanBoDuToanKQPL.SelectedItem = e.Row.Item;
            var vm = (PhanBoDuToanChiTietChiKPQLViewModel)this.DataContext;
            if (vm != null && vm.SelectedItem.BHangCha)
            {
                e.Cancel = true;
            }
        }
        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }
    }
}
