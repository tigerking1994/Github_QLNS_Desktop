using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution
{
    /// <summary>
    /// Interaction logic for DistributionImportJson.xaml
    /// </summary>
    public partial class DistributionImportJson : Window
    {
        public DistributionImportJson()
        {
            InitializeComponent();
        }

        private void dgDistributionImportJson_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgDistributionImportJson.SelectedItem = e.Row.Item;
        }
    }
}
