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
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.TKTCVaTongDuToan
{
    /// <summary>
    /// Interaction logic for TKTCVaTongDuToanDetail.xaml
    /// </summary>
    public partial class TKTCVaTongDuToanDetail : Window
    {
        public TKTCVaTongDuToanDetail()
        {
            InitializeComponent();
            dgdDataDuToanDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataDuToanDetail.SelectedItem = e.Row.Item;
            var vm = (TKTCVaTongDuToanDetailViewModel)this.DataContext;
            var selected = (DuToanDetailModel)e.Row.Item;
            var curentColumn = e.Column;

            if (!selected.IsEditHangMuc && (curentColumn.SortMemberPath.Equals("GiaTriPheDuyet")))
            {
                e.Cancel = true;
            }

        }
    }
}
