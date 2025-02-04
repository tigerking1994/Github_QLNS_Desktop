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
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewCadresPlan;

namespace VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagementPlan.NewCadresPlan
{
    /// <summary>
    /// Interaction logic for CadresPlanDetail.xaml
    /// </summary>
    public partial class NewCadresPlanDetail : Window
    {
        public NewCadresPlanDetail()
        {
            InitializeComponent();
            dgAllowanceDetail.BeginningEdit += dgData_BeginningEdit;
        }

        private void dgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgAllowanceDetail.SelectedItem = e.Row.Item;
            var vm = (NewCadresPlanDetailViewModel)this.DataContext;
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

        private void DetailWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                tbxSearchPhuCap.Focus();
            }
        }
    }
}
