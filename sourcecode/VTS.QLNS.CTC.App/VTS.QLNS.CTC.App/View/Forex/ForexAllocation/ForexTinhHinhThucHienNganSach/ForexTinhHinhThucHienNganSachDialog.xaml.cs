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
using VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexTinhHinhThucHienNganSach;

namespace VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexTinhHinhThucHienNganSach
{
    /// <summary>
    /// Interaction logic for ForexTinhHinhThucHienNganSachDialog.xaml
    /// </summary>
    public partial class ForexTinhHinhThucHienNganSachDialog : Window
    {
        public ForexTinhHinhThucHienNganSachDialog()
        {
            InitializeComponent();
            dgDataPheDuyetThanhToanDetail.BeginningEdit += dgData_DataPheDuyet_ThanhToan;
        }

        private void dgData_DataPheDuyet_ThanhToan(object sender, DataGridBeginningEditEventArgs e)
        {
            dgDataPheDuyetThanhToanDetail.SelectedItem = e.Row.Item;
            var vm = (ForexTinhHinhThucHienNganSachDialogViewModel)this.DataContext;
            if (e.Column.SortMemberPath == chbox1.SortMemberPath)
            {
                if (vm != null && vm.SelectedDsPheDuyetThanhToan.IsEnabled)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
