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

namespace VTS.QLNS.CTC.App.View.Salary.Settlement.SalaryConfiguration
{
    /// <summary>
    /// Interaction logic for SalaryConfiguration.xaml
    /// </summary>
    public partial class SalaryConfiguration : UserControl
    {
        public SalaryConfiguration()
        {
            InitializeComponent();
            DgSalaryConfiguration.BeginningEdit += DgSalaryConfiguration_BeginningEdit;
        }

        private void DgSalaryConfiguration_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgSalaryConfiguration.SelectedItem = e.Row.Item;
        }
    }
}
