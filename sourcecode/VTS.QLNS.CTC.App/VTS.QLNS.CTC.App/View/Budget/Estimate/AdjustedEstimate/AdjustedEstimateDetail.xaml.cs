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
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate;

namespace VTS.QLNS.CTC.App.View.Budget.Estimate.AdjustedEstimate
{
    /// <summary>
    /// Interaction logic for AdjustedEstimateDetail.xaml
    /// </summary>
    public partial class AdjustedEstimateDetail : Window
    {
        public AdjustedEstimateDetail()
        {
            InitializeComponent();
            dgDieuChinhDuToan1.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgDieuChinhDuToan1.SelectedItem = e.Row.Item;
            var vm = (AdjustedEstimateDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable || (!string.IsNullOrEmpty(vm.Model.STongHop) && !vm.IsShowColumnDonVi && e.Column.SortMemberPath != nameof(DcChungTuChiTietModel.SGhiChu))))
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
