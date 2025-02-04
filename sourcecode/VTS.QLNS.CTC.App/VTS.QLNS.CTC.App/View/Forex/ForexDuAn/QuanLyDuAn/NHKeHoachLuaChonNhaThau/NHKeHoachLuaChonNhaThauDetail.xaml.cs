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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHKeHoachLuaChonNhaThau;

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyDuAn.NHKeHoachLuaChonNhaThau
{
    /// <summary>
    /// Interaction logic for NHKeHoachLuaChonNhaThauDetail.xaml
    /// </summary>
    public partial class NHKeHoachLuaChonNhaThauDetail : Window
    {
        public NHKeHoachLuaChonNhaThauDetail()
        {
            InitializeComponent();
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
            var vm = (NHKeHoachLuaChonNhaThauDetailViewModel)this.DataContext;
            var ILoaiDuToan = vm != null ? vm.ILoaiDuToan : null;
            var selected = (NhDaGoiThauDetailHangMucModel)e.Row.Item;
            var curentColumn = e.Column;

            if (curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetUSD") || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetVND")
                || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetEUR") || curentColumn.SortMemberPath.Equals("FGiaTriPheDuyetNgoaiTeKhac"))
            {
                var check = vm.ItemHangMuc.Where(x => x.IIdParentID == selected.IIdHangMucID).ToList();
                if ((selected.IsChecked && !selected.IsHangCha) || (selected.IsChecked && selected.IsHangCha && check.Count() == 0)) e.Cancel = false;
                else e.Cancel = true;
                if (!selected.IsChecked) e.Cancel = true;
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
