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
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PheDuyetDuAn
{
    /// <summary>
    /// Interaction logic for PheDuyetDuAnDetail.xaml
    /// </summary>
    public partial class PheDuyetDuAnDetail : Window
    {
        public PheDuyetDuAnDetail()
        {
            InitializeComponent();
            dgdPheDuyetDuAnDetail.BeginningEdit += dgdPheDuyetDuAnDetail_BeginningEdit;
        }

        private void dgdPheDuyetDuAnDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdPheDuyetDuAnDetail.SelectedItem = e.Row.Item;
            var selected = (ApproveProjectDetailModel)e.Row.Item;
            var curentColumn = e.Column;

            //if (!selected.IsEditHangMuc && (curentColumn.SortMemberPath.Equals("GiaTriPheDuyet")))
            //{
            //    e.Cancel = true;
            //}

            //var vm = (PheDuyetDuAnDetailViewModel)this.DataContext;
            if (selected != null && !selected.IsEditHangMuc && curentColumn.SortMemberPath.Equals("GiaTriPheDuyet"))
            {
                e.Cancel = true;
            }
        }
    }
}
