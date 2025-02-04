using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao
{
    /// <summary>
    /// Interaction logic for NhanDuToanChiTietChiKPQL.xaml
    /// </summary>
    public partial class NhanDuToanChiTietChiKPQL : UserControl
    {
        public NhanDuToanChiTietChiKPQL()
        {
            InitializeComponent();
            DgNhanKPQL.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgNhanKPQL.SelectedItem = e.Row.Item;
            var vm = (NhanDuToanChiTietChiKPQLViewModel)this.DataContext;
            if (vm != null && vm.SelectedItem.BHangCha)
            {
                e.Cancel = true;
            }
        }
    }
}
