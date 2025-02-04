using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNThietKeKyThuatTongDuToan;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNThietKeKyThuatTongDuToan
{
    /// <summary>
    /// Interaction logic for ThietKeKyThuatVaTongDuToanDialog.xaml
    /// </summary>
    public partial class ThietKeKyThuatTongDuToanDialog : Window
    {
        public ThietKeKyThuatTongDuToanDialog()
        {
            InitializeComponent();
            dgdDataNguonVonDetail.BeginningEdit += dgdDataNguonVonDetail_BeginningEdit;
            dgdDataChiPhiDetail.BeginningEdit += dgdDataChiPhiDetail_BeginningEdit;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void dgdDataChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (MSCTNThietKeKyThuatTongDuToanDialogViewModel)DataContext;
            dgdDataChiPhiDetail.SelectedItem = e.Row.Item;
            var selected = (NhDaDuToanChiPhiModel)e.Row.Item;
            var curentColumn = e.Column;

            //if(!vm.IsNotViewDetail || !selected.IsEditHangMuc || (selected.IsHangCha && curentColumn.SortMemberPath.Equals("STenChiPhi")))
            //{
            //    e.Cancel = true;
            //}
            //if ((!selected.IsEditHangMuc || !selected.IsEditGiaTriChiPhi) && (curentColumn.SortMemberPath.Equals("GiaTriPheDuyet")))
            //{
            //    e.Cancel = true;
            //}
        }

        private void dgdDataNguonVonDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataNguonVonDetail.SelectedItem = e.Row.Item;
            var vm = (MSCTNThietKeKyThuatTongDuToanDialogViewModel)DataContext;

            //if (!vm.IsNotViewDetail)
            //{
            //    e.Cancel = true;
            //}
        }

        private void dgdDataNguonVonDetail_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                dgdDataNguonVonDetail_ScrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
        private void dgdDataChiPhiDetail_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                dgdDataChiPhiDetail_ScrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
