using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV
{
    /// <summary>
    /// Interaction logic for LapKeHoachChiKCBQYDVDetail.xaml
    /// </summary>
    public partial class LapKeHoachChiKCBQYDVDetail : Window
    {
        public LapKeHoachChiKCBQYDVDetail()
        {
            InitializeComponent();
            DgKeHoachChiKCBQYDVDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgKeHoachChiKCBQYDVDetail.SelectedItem = e.Row.Item;
            var vm = (LapKeHoachChiKCBQYDVDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable || vm.IsAnotherUserCreate|| vm.IsAggregate || vm.SelectedItem.IsRemainRow))
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
