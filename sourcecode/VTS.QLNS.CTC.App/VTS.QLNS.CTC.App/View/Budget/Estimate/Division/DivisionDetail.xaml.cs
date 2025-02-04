using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division;

namespace VTS.QLNS.CTC.App.View.Budget.Estimate.Division
{
    /// <summary>
    /// Interaction logic for DivisionDetail.xaml
    /// </summary>
    public partial class DivisionDetail : Window
    {
        public DivisionDetail()
        {
            InitializeComponent();
            DgdDivisionDetail1.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgdDivisionDetail1.SelectedItem = e.Row.Item;
            DtChungTuChiTietModel model = (DtChungTuChiTietModel)DgdDivisionDetail1.SelectedItem;
            var vm = (DivisionDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
            if ((e.Column.SortMemberPath == TongCong.SortMemberPath && !model.IsEditTuChi)) 
                //|| (e.Column.SortMemberPath == HienVat.SortMemberPath && !model.IsEditHienVat)
                //|| (e.Column.SortMemberPath == HangNhap.SortMemberPath && !model.IsEditHangNhap)
                //|| (e.Column.SortMemberPath == HangMua.SortMemberPath && !model.IsEditHangMua)
                //|| (e.Column.SortMemberPath == DuPhong.SortMemberPath && !model.IsEditDuPhong)
                //|| (e.Column.SortMemberPath == PhanCap.SortMemberPath && !model.IsEditPhanCap))
            {
                e.Cancel = true;
            }
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrollBottom.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DgdDivisionDetail1.ScrollIntoView(DgdDivisionDetail1.Items[0], DgdDivisionDetail1.Columns[0]);

        }
        private void Button_Click_Mirror(object sender, RoutedEventArgs e)
        {
            if (DgdDivisionDetailImport1 != null && DgdDivisionDetailImport1.Items.Count > 0)
            {
                DgdDivisionDetailImport1.ScrollIntoView(DgdDivisionDetailImport1.Items[0], DgdDivisionDetailImport1.Columns[0]);
            }
            else if (DgdDivisionDetailInput1 != null && DgdDivisionDetailInput1.Items.Count > 0)
            {
                DgdDivisionDetailInput1.ScrollIntoView(DgdDivisionDetailInput1.Items[0], DgdDivisionDetailInput1.Columns[0]);
            }
        }
    }
}
