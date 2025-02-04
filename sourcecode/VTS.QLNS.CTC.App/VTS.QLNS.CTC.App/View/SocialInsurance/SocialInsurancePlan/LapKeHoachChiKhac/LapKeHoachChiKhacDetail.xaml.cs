using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac
{
    /// <summary>
    /// Interaction logic for LapKeHoachChiKhacDetail.xaml
    /// </summary>
    public partial class LapKeHoachChiKhacDetail : Window
    {
        public LapKeHoachChiKhacDetail()
        {
            InitializeComponent();
            DgKeHoachChiKhacDetailUpdate.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgKeHoachChiKhacDetailUpdate.SelectedItem = e.Row.Item;
            var vm = (LapKeHoachChiKhacDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable || vm.IsAnotherUserCreate|| vm.IsAggregate))
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

    }
}
