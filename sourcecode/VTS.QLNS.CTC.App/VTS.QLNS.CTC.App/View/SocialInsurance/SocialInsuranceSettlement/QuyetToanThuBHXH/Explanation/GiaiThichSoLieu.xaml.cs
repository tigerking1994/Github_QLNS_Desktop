using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.Explanation;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.Explanation
{
    /// <summary>
    /// Interaction logic for GiaiThichSoLieu.xaml
    /// </summary>
    public partial class GiaiThichSoLieu : Window
    {
        public GiaiThichSoLieu()
        {
            InitializeComponent();
            DgQttGiaiThichSoLieu2.BeginningEdit += dgdData_BeginningEdit;
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgQttGiaiThichSoLieu2.SelectedItem = e.Row.Item;
            var vm = (QuyetToanThuGiaiThichSoLieuViewModel)this.DataContext;
            if (vm != null && vm.SelectedItem.BHangCha)
            {
                e.Cancel = true;
            }
        }
        private void dgdDataGTTT_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooterGTTT.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
        private void dgdDataSoSanh_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooterTHSS.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
        private void dgdDataGiamDong_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooterGTGD.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
