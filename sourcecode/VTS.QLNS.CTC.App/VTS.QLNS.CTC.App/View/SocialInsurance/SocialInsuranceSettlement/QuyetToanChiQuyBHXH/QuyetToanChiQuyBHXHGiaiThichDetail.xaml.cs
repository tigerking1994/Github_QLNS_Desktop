using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH
{
    /// <summary>
    /// Interaction logic for QuyetToanChiQuyBHXHGiaiThichDetail.xaml
    /// </summary>
    public partial class QuyetToanChiQuyBHXHGiaiThichDetail : Window
    {
        public QuyetToanChiQuyBHXHGiaiThichDetail()
        {
            InitializeComponent();
            dgdDataAllocationDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataAllocationDetail.SelectedItem = e.Row.Item;
            var vm = (QuyetToanChiQuyBHXHDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BIsKhoa || !vm.SelectedItem.IsEditable || !vm.IsEditByRole || vm.Model.BDaTongHop || (!string.IsNullOrEmpty(vm.Model.SDSSoChungTuTongHop) && !vm.Model.SSoChungTu.Equals(vm.Model.SDSSoChungTuTongHop))))
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
