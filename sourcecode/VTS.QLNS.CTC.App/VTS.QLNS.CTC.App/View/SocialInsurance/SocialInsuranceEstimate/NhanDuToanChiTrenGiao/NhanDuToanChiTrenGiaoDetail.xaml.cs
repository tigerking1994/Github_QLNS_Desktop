using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao
{
    /// <summary>
    /// Interaction logic for NhanDuToanChiTrenGiaoDetail.xaml
    /// </summary>
    public partial class NhanDuToanChiTrenGiaoDetail : Window
    {
        public NhanDuToanChiTrenGiaoDetail()
        {
            InitializeComponent();
            DgNhanPhanBoChiDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgNhanPhanBoChiDetail.SelectedItem = e.Row.Item;
            var vm = (NhanDuToanChiTrenGiaoDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable || vm.IsAnotherUserCreate))
            {
                //e.Cancel = true;
            }
        }
    }
}
