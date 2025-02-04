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
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres;
using VTS.QLNS.CTC.App.ViewModel.Salary.NewCadres;

namespace VTS.QLNS.CTC.App.View.NewSalary.NewCadres
{
    /// <summary>
    /// Interaction logic for UpdateMultiAllowenceCadres.xaml
    /// </summary>
    public partial class NewCadresUpdateMultiAllowence : Window
    {
        public NewCadresUpdateMultiAllowence()
        {
            InitializeComponent();
            dgAllowanceDetail.BeginningEdit += dgData_BeginningEdit;
        }

        private void dgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgAllowanceDetail.SelectedItem = e.Row.Item;
            var vm = (NewCadresUpdateMultiAllowenceViewModel)this.DataContext;
            if (vm != null && vm.SelectedAllowence.IsHangCha)
            {
                e.Cancel = true;
            }

            if (e.Column.SortMemberPath == GiaTri.SortMemberPath)
            {
                if (vm != null && vm.SelectedAllowence.BGiaTri == false)
                {
                    e.Cancel = true;
                }
            }

            if (e.Column.SortMemberPath == SoNgayHuong.SortMemberPath)
            {
                if (vm != null && vm.SelectedAllowence.BHuongPcSn == false)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
