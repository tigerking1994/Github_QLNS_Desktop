using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Salary.ImportSalary;

namespace VTS.QLNS.CTC.App.View.Salary.ImportSalary
{
    /// <summary>
    /// Interaction logic for ImportSalary.xaml
    /// </summary>
    public partial class ImportSalary : Window
    {
        public ImportSalary()
        {
            InitializeComponent();
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(DgSalaryImportDetail, SalaryTableMonthDetail_DataContextChanged);
            }
        }
        void SalaryTableMonthDetail_DataContextChanged(object sender, EventArgs e)
        {
            var vm = (ImportSalaryViewModel)this.DataContext;
            var list = vm.SalaryMonthDetailImportViewModels;
            if (list != null)
            {
                DgSalaryImportDetail.Columns.Clear();
                var col = new DataGridTextColumn();
                col.Header = "Mã cán bộ";
                col.Binding = new Binding("MaCanBo");
                DgSalaryImportDetail.Columns.Add(col);
                col = new DataGridTextColumn();
                col.Header = "Tên cán bộ";
                col.Binding = new Binding("TenCanBo");
                DgSalaryImportDetail.Columns.Add(col);
                col = new DataGridTextColumn();
                col.Header = "Tháng";
                col.Binding = new Binding("Thang");
                DgSalaryImportDetail.Columns.Add(col);
                col = new DataGridTextColumn();
                col.Header = "Năm";
                col.Binding = new Binding("Nam");
                DgSalaryImportDetail.Columns.Add(col);
                col = new DataGridTextColumn();
                col.Header = "Mã đơn vị";
                col.Binding = new Binding("MaDonVi");
                DgSalaryImportDetail.Columns.Add(col);

                foreach (var item in list)
                {
                    if (item.LstPhuCap != null)
                    {
                        var lenght = item.LstPhuCap.Count;
                        for (int i = 0; i < lenght; i++)
                        {
                            string text = "LstPhuCap[" + i + "].GiaTri";
                            var colPhuCap = new DataGridTextColumn();
                            colPhuCap.Header = item.LstPhuCap[i].MaPhuCap;
                            colPhuCap.Binding = new Binding(text);
                            DgSalaryImportDetail.Columns.Add(colPhuCap);
                        }
                    }
                    break;
                }
            }
        }
    }
}
