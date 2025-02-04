using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH
{
    /// <summary>
    /// Interaction logic for QuyetToanThuDetail.xaml
    /// </summary>
    public partial class QuyetToanThuDetail : Window
    {
        public QuyetToanThuDetail()
        {
            InitializeComponent();
            DgQttVoucherDetail.BeginningEdit += dgdData_BeginningEdit;
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgQttVoucherDetail.SelectedItem = e.Row.Item;
            var vm = (QuyetToanThuDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable || vm.IsAnotherUserCreate))
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
