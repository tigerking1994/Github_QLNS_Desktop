using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChiPhiKhac.QuyetDinhChiPhiKhac;

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.ChiPhiKhac.QuyetDinhChiPhiKhac
{
    /// <summary>
    /// Interaction logic for DuAnQuyetDinhChiPhiKhacDialog.xaml
    /// </summary>
    public partial class DuAnQuyetDinhChiPhiKhacDialog : Window
    {
        public DuAnQuyetDinhChiPhiKhacDialog()
        {
            InitializeComponent();
            dgdDataChiPhiDetail.BeginningEdit += dgdDataChiPhiDetail_BeginningEdit;

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void dgdDataChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (DuAnQuyetDinhChiPhiKhacDialogViewModel)DataContext;
            dgdDataChiPhiDetail.SelectedItem = e.Row.Item;
            var selected = (NhDaQuyetDinhKhacChiPhiModel)e.Row.Item;
            var curentColumn = e.Column;
            if (vm.IsDetail)
                e.Cancel = true;

            //if(!vm.IsNotViewDetail || !selected.IsEditHangMuc || (selected.IsHangCha && curentColumn.SortMemberPath.Equals("STenChiPhi")))
            //{
            //    e.Cancel = true;
            //}
            //if ((!selected.IsEditHangMuc || !selected.IsEditGiaTriChiPhi) && (curentColumn.SortMemberPath.Equals("GiaTriPheDuyet")))
            //{
            //    e.Cancel = true;
            //}
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
