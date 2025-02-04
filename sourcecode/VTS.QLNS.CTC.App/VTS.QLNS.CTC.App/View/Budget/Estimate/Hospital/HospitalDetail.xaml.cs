using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Hospital;

namespace VTS.QLNS.CTC.App.View.Budget.Estimate.Hospital
{
    /// <summary>
    /// Interaction logic for HospitalDetail.xaml
    /// </summary>
    public partial class HospitalDetail : Window
    {
        public HospitalDetail()
        {
            InitializeComponent();
            DgdHospitalDetail.BeginningEdit += dgdData_BeginningEdit;
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgdHospitalDetail.SelectedItem = e.Row.Item;
            DtChungTuChiTietModel model = (DtChungTuChiTietModel)DgdHospitalDetail.SelectedItem;
            var vm = (HospitalDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable || !vm.IsEditByRole))
            {
                e.Cancel = true;
            }
            if ((e.Column.SortMemberPath == TuChi.SortMemberPath && !model.IsEditTuChi)
                || (e.Column.SortMemberPath == HienVat.SortMemberPath && !model.IsEditHienVat)
                || (e.Column.SortMemberPath == HangNhap.SortMemberPath && !model.IsEditHangNhap)
                || (e.Column.SortMemberPath == HangMua.SortMemberPath && !model.IsEditHangMua)
                || (e.Column.SortMemberPath == DuPhong.SortMemberPath && !model.IsEditDuPhong)
                || (e.Column.SortMemberPath == PhanCap.SortMemberPath && !model.IsEditPhanCap))
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
    }
}
