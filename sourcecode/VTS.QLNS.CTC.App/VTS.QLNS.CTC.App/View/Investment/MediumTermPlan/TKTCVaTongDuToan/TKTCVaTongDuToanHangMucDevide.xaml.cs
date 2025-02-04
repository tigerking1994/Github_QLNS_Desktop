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
    /// Interaction logic for TKTCVaTongDuToanHangMucDevide.xaml
    /// </summary>
    public partial class TKTCVaTongDuToanHangMucDevide : Window
    {
        public TKTCVaTongDuToanHangMucDevide()
        {
            InitializeComponent();
            dgdTKTCHangMucPhanChia.BeginningEdit += dgdTKTCHangMucPhanChia_BeginningEdit;
        }

        private void dgdTKTCHangMucPhanChia_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdTKTCHangMucPhanChia.SelectedItem = e.Row.Item;

            var selected = (DuToanDetailModel)e.Row.Item;
            var curentColumn = e.Column;

            if (!selected.IsChecked && (curentColumn.SortMemberPath.Equals("FGiaTriPhanChia")))
            {
                e.Cancel = true;
            }
        }
    }
}
