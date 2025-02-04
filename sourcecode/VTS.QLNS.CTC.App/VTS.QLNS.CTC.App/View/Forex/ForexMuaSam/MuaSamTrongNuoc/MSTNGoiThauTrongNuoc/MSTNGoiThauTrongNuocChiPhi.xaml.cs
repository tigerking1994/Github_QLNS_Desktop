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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNGoiThauTrongNuoc;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNGoiThauTrongNuoc
{
    /// <summary>
    /// Interaction logic for MSTNGoiThauTrongNuocChiPhi.xaml
    /// </summary>
    public partial class MSTNGoiThauTrongNuocChiPhi : Window
    {
        public MSTNGoiThauTrongNuocChiPhi()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void dgdNhDaKHLCNTNguonVonDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdNHKeHoachLuaChonNhaThauNguonVon1Detail.SelectedItem = e.Row.Item;

            var selected = (NhDaGoiThauDetailNguonVonModel)e.Row.Item;
            var curentColumn = e.Column;

            if (curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetUSD") || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetVND")
                || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetEUR") || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetNgoaiTeKhac"))
            {
                if (!selected.IsChecked) e.Cancel = true;
            }
        }

        private void dgdNhDaKHLCNTChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdNHKeHoachLuaChonNhaThauChiPhiDetail.SelectedItem = e.Row.Item;

            var selected = (NhDaGoiThauDetailChiPhiModel)e.Row.Item;
            var curentColumn = e.Column;

            if (curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetUSD") || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetVND")
                || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetEUR") || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetNgoaiTeKhac"))
            {
                if (!selected.IsChecked || selected.BHaveHangMuc) e.Cancel = true;
            }
        }

        private void dgdNhDaKHLCNTHangMucDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdNHKeHoachLuaChonNhaThauHangMucDetail.SelectedItem = e.Row.Item;
            var vm = (MSTNGoiThauTrongNuocChiPhiViewModel)this.DataContext;
            var ILoaiDuToan = vm != null ? vm.ILoaiDuToan : null;
            var selected = (NhDaGoiThauDetailHangMucModel)e.Row.Item;
            var curentColumn = e.Column;

            if (curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetUSD") || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetVND")
                || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetEUR") || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetNgoaiTeKhac"))
            {
                var check = vm.ItemHangMuc.Where(x => x.IIdParentID == selected.IIdHangMucID).ToList();
                if ((ILoaiDuToan == 2 && !selected.IsHangCha) || (selected.IsHangCha && check.Count == 0)) e.Cancel = false;
                else e.Cancel = false;
                if (!selected.IsChecked) e.Cancel = false;
            }
        }

        private void MultiComboBox_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void dgdNHKeHoachLuaChonNhaThauHangMucDetail_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrollNHKeHoachLuaChonNhaThauHangMucDetailFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void dgdNHKeHoachLuaChonNhaThauChiPhiDetail_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrolldgdNHKeHoachLuaChonNhaThauChiPhiDetailFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
