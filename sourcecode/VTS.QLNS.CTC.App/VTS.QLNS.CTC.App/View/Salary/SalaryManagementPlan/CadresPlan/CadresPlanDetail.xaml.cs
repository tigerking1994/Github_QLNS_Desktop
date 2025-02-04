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
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CadresPlan;

namespace VTS.QLNS.CTC.App.View.Salary.SalaryManagementPlan.CadresPlan
{
    /// <summary>
    /// Interaction logic for CadresPlanDetail.xaml
    /// </summary>
    public partial class CadresPlanDetail : Window
    {
        public CadresPlanDetail()
        {
            InitializeComponent();
            dgAllowanceDetail.BeginningEdit += dgData_BeginningEdit;
        }

        private void dgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgAllowanceDetail.SelectedItem = e.Row.Item;
            var vm = (CadresPlanDetailViewModel)this.DataContext;
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
