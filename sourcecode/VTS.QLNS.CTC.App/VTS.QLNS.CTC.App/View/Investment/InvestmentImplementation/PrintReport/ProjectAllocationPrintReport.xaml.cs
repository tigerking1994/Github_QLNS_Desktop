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

namespace VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PrintReport
{
    /// <summary>
    /// Interaction logic for ProjectAllocationPrintReport.xaml
    /// </summary>
    public partial class ProjectAllocationPrintReport : UserControl
    {
        public ProjectAllocationPrintReport()
        {
            InitializeComponent();
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                //TODO
                //scrollHeader.ScrollToHorizontalOffset(e.HorizontalOffset);
                //TODO
            }
        }
    }
}
