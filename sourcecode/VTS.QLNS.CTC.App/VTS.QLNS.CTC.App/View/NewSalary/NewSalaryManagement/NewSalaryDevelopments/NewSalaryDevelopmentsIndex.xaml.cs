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

namespace VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagement.NewSalaryDevelopments
{
    /// <summary>
    /// Interaction logic for SalaryDevelopmentsIndex.xaml
    /// </summary>
    public partial class NewSalaryDevelopmentsIndex : UserControl
    {
        public NewSalaryDevelopmentsIndex()
        {
            InitializeComponent();
            this.DgSalaryDevelopments.AutoGeneratingColumn += Datagrid_AutoGeneratingColumn;
        }

        void Datagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(decimal))
            {
                DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null)
                {
                    dataGridTextColumn.Binding.StringFormat = "{0:n4}";
                }
            }
        }
    }
}
