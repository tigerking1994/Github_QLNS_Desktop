using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.KHLuaChonNhaThau
{
    /// <summary>
    /// Interaction logic for KHLuaChonNhaThauDetail.xaml
    /// </summary>
    public partial class KHLuaChonNhaThauDetail : Window
    {
        public KHLuaChonNhaThauDetail()
        {
            InitializeComponent();
        }
        private void dgdKHLCNTNguonVonDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var selected = (VdtKhlcNhaThauGoiThauDetailModel)e.Row.Item;
            var curentColumn = e.Column;
            dgdKHLCNTNguonVonDetail.SelectedItem = e.Row.Item;
            if (!selected.IsChecked && (curentColumn.SortMemberPath.Equals("FGiaTriGoiThau")))
            {
                e.Cancel = true;
            }
        }

        private void dgdKHLCNTChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdKHLCNTChiPhiDetail.SelectedItem = e.Row.Item;

            var selected = (VdtKhlcNhaThauGoiThauDetailModel)e.Row.Item;
            var curentColumn = e.Column;

            if ((!selected.IsChecked || !selected.IsEdit) && (curentColumn.SortMemberPath.Equals("FGiaTriGoiThau") || curentColumn.SortMemberPath.Equals("SNoiDung")))
            {
                e.Cancel = true;
            }
        }

        //private void dgdKHLCNTNguonVonDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        //{
        //    var selected = (VdtKhlcNhaThauGoiThauDetailModel)e.Row.Item;
        //    var curentColumn = e.Column;
        //    dgdKHLCNTNguonVonDetail.SelectedItem = e.Row.Item;
        //    if (!(selected.IsChecked ?? false) && (curentColumn.SortMemberPath.Equals("FGiaTriGoiThau")))
        //    {
        //        e.Cancel = true;
        //    }
        //}

        //private void dgdKHLCNTChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        //{
        //    dgdKHLCNTChiPhiDetail.SelectedItem = e.Row.Item;

        //    var selected = (VdtKhlcNhaThauGoiThauDetailModel)e.Row.Item;
        //    var curentColumn = e.Column;

        //    if ((!(selected.IsChecked ?? true) || !selected.IsEdit) && (curentColumn.SortMemberPath.Equals("FGiaTriGoiThau")))
        //    {
        //        e.Cancel = true;
        //    }
        //}

        private void dgdKHLCNTHangMucDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdKHLCNTHangMucDetail.SelectedItem = e.Row.Item;
        }
    }
}
