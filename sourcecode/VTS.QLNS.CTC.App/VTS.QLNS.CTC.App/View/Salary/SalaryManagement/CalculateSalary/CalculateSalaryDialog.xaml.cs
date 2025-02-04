using ICSharpCode.AvalonEdit.Search;
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

namespace VTS.QLNS.CTC.App.View.Salary.SalaryManagement.CalculateSalary
{
    /// <summary>
    /// Interaction logic for CalculateSalaryDialog.xaml
    /// </summary>
    public partial class CalculateSalaryDialog : Window
    {
        public CalculateSalaryDialog()
        {
            InitializeComponent();
            SearchPanel.Install(CalculationEditor);
        }
    }
}
