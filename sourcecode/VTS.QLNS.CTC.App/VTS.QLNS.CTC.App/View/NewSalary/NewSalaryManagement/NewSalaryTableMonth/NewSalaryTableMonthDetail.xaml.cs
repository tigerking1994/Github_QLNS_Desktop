using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DataGridTextColumn = System.Windows.Controls.DataGridTextColumn;

namespace VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagement.NewSalaryTableMonth
{
    /// <summary>
    /// Interaction logic for SalaryDetailTableMonth.xaml
    /// </summary>
    public partial class NewSalaryTableMonthDetail : Window
    {
        public NewSalaryTableMonthDetail()
        {
            InitializeComponent();
            this.dgSalaryTableMonth.AutoGeneratingColumn += Datagrid_AutoGeneratingColumn;
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
            if (new List<string> { "name", "parent_ID", "object_id" }.Contains(e.Column.Header.ToString()))
            {
                DataGridTextColumn t = e.Column as DataGridTextColumn;
                t.Visibility = Visibility.Hidden;
            }
            e.Column.Header = e.Column.Header.ToString().Replace("_", "__");
        }
    }
}
