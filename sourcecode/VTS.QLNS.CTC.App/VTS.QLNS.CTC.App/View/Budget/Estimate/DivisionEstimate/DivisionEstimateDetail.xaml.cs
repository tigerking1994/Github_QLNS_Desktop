using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;

namespace VTS.QLNS.CTC.App.View.Budget.Estimate.DivisionEstimate
{
    /// <summary>
    /// Interaction logic for DivisionEstimateDetail.xaml
    /// </summary>
    public partial class DivisionEstimateDetail : Window
    {
        public DivisionEstimateDetail()
        {
            InitializeComponent();
            dgPhanBoDuToan.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgPhanBoDuToan.SelectedItem = e.Row.Item;
            DtChungTuChiTietModel model = (DtChungTuChiTietModel)dgPhanBoDuToan.SelectedItem;
            var vm = (DivisionEstimateDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
            if (((e.Column.SortMemberPath == TongCong.SortMemberPath || e.Column.SortMemberPath == TuChiDieuChinh.SortMemberPath) && !model.IsEditTuChi)
                || ((e.Column.SortMemberPath == HienVat.SortMemberPath || e.Column.SortMemberPath == HienVatDieuChinh.SortMemberPath) && !model.IsEditHienVat)
                || ((e.Column.SortMemberPath == HangNhap.SortMemberPath || e.Column.SortMemberPath == HangNhapDieuChinh.SortMemberPath) && !model.IsEditHangNhap)
                || ((e.Column.SortMemberPath == HangMua.SortMemberPath || e.Column.SortMemberPath == HangMuaDieuChinh.SortMemberPath) && !model.IsEditHangMua)
                || (e.Column.SortMemberPath == DuPhong.SortMemberPath && !model.IsEditDuPhong)
                || ((e.Column.SortMemberPath == PhanCap.SortMemberPath || e.Column.SortMemberPath == PhanCapDieuChinh.SortMemberPath) && !model.IsEditPhanCap))
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dgPhanBoDuToan.ScrollIntoView(dgPhanBoDuToan.Items[0], dgPhanBoDuToan.Columns[0]);
        }
    }
}
