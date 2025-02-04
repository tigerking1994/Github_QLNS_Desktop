using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH
{
    /// <summary>
    /// Interaction logic for AllocationDetail.xaml
    /// </summary>
    public partial class QuyetToanChiQuyBHXHDetail : Window
    {
        public QuyetToanChiQuyBHXHDetail()
        {
            InitializeComponent();
            dgdDataAllocationDetail.BeginningEdit += dgdData_BeginningEdit;
            dgdExplainSubtractsDetail1.BeginningEdit += dgdExplainData_BeginningEdit;

        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataAllocationDetail.SelectedItem = e.Row.Item;
            var vm = (QuyetToanChiQuyBHXHDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BIsKhoa || !vm.SelectedItem.IsEditable || !vm.IsEditByRole || !string.IsNullOrEmpty(vm.Model.SDSSoChungTuTongHop)))
            {
                e.Cancel = true;
            }
        }

        private void dgdExplainData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdExplainSubtractsDetail1.SelectedItem= e.Row.Item;
            var vm = (QuyetToanChiQuyBHXHDetailViewModel)this.DataContext;
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

        private void dgdExplainSubtractsDetail1_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                //scrollFooter1.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }


        private void dgdDataAllocationDetail_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

    }
}
