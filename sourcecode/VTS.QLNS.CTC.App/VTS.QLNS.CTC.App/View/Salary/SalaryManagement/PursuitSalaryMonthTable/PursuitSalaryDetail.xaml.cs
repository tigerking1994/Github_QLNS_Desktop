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
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.PursuitSalaryMonthTable;

namespace VTS.QLNS.CTC.App.View.Salary.SalaryManagement.PursuitSalaryMonthTable
{
    /// <summary>
    /// Interaction logic for PursuitSalaryDetail.xaml
    /// </summary>
    public partial class PursuitSalaryDetail : Window
    {
        public PursuitSalaryDetail()
        {
            InitializeComponent();
            this.DgPursuitSalary.AutoGeneratingColumn += Datagrid_AutoGeneratingColumn;
        }

        void Datagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(decimal))
            {
                DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null)
                {
                    if (dataGridTextColumn.Header.ToString().Contains("HS"))
                    {
                        dataGridTextColumn.Binding.StringFormat = "{0:n4}";
                    }
                    else
                    {
                        dataGridTextColumn.Binding.StringFormat = "{0:n0}";
                    }
                }
            }

            e.Column.Header = e.Column.Header.ToString().Replace("_", "__");
        }
    }
}
