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
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GoiThau;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GoiThau
{
    /// <summary>
    /// Interaction logic for GoiThauDetail.xaml
    /// </summary>
    public partial class GoiThauDetail : Window
    {
        public GoiThauDetail()
        {
            InitializeComponent();
            dgdGoiThauNguonVonDetail.BeginningEdit += dgdData_BeginningEdit;
            dgdGoiThauHangMucDetail.BeginningEdit += dgdGoiThauHangMucDetail_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdGoiThauNguonVonDetail.SelectedItem = e.Row.Item;
            var vm = (GoiThauDetailViewModel)this.DataContext;
            if (vm != null && vm.SelectedItem.IsHangCha)
            {
                e.Cancel = true;
            }
        }

        private void dgdGoiThauHangMucDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdGoiThauHangMucDetail.SelectedItem = e.Row.Item;
            //var vm = (GoiThauDetailViewModel)this.DataContext;
            //if (vm != null && vm.SelectedItem.IsHangCha)
            //{
            //    e.Cancel = true;
            //}
        }
    }
}
