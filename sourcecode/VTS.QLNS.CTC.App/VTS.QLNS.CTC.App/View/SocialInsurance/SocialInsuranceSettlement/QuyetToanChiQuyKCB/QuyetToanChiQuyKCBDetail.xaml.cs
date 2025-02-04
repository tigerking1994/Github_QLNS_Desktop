using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB
{
    /// <summary>
    /// Interaction logic for AllocationDetail.xaml
    /// </summary>
    public partial class QuyetToanChiQuyKCBDetail : Window
    {
        public QuyetToanChiQuyKCBDetail()
        {
            InitializeComponent();
            dgdDataQuyKCBDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        { 
            dgdDataQuyKCBDetail.SelectedItem = e.Row.Item;
            var vm = (QuyetToanChiQuyKCBDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BIsKhoa || !vm.SelectedItem.IsEditable || !vm.IsEditByRole || !string.IsNullOrEmpty(vm.Model.SDSSoChungTuTongHop)))
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
