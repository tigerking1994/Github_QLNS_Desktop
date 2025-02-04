using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan
{
    /// <summary>
    /// Interaction logic for AllocationDetail.xaml
    /// </summary>
    public partial class ThamDinhQuyetToanDetail : Window
    {
        public ThamDinhQuyetToanDetail()
        {
            InitializeComponent();
            dgdPheDuyetQuyetToanNamDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdPheDuyetQuyetToanNamDetail.SelectedItem = e.Row.Item;
            var vm = (ThamDinhQuyetToanDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.IsLocked
                || !vm.SelectedItem.IsEditable
                || !vm.IsEditByRole
                || vm.Model.BDaTongHop
                || (!string.IsNullOrEmpty(vm.Model.STongHop) && !vm.Model.SSoChungTu.Equals(vm.Model.STongHop))))
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
