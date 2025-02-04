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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.TKTCVaTongDuToan
{
    /// <summary>
    /// Interaction logic for VdtDaDuToanDialog.xaml
    /// </summary>
    public partial class VdtDaDuToanDialog : Window
    {
        public VdtDaDuToanDialog()
        {
            InitializeComponent();
            dgdDataNguonVonDetail.BeginningEdit += dgdDataNguonVonDetail_BeginningEdit;
            dgdDataChiPhiDetail.BeginningEdit += dgdDataChiPhiDetail_BeginningEdit;
        }

        private void dgdDataChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataChiPhiDetail.SelectedItem = e.Row.Item;
            var selected = (VdtDaDuToanChiPhiModel)e.Row.Item;
            var curentColumn = e.Column;

            if ((!selected.IsEditHangMuc || !selected.IsEditGiaTriChiPhi) && (curentColumn.SortMemberPath.Equals("GiaTriPheDuyet")))
            {
                e.Cancel = true;
            }
        }

        private void dgdDataNguonVonDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataNguonVonDetail.SelectedItem = e.Row.Item;
        }
    }
}
