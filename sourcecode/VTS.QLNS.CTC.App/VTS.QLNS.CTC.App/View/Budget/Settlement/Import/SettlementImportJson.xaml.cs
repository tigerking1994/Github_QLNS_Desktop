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

namespace VTS.QLNS.CTC.App.View.Budget.Settlement.Import
{
    /// <summary>
    /// Interaction logic for SettlementImportJson.xaml
    /// </summary>
    public partial class SettlementImportJson : Window
    {
        public SettlementImportJson()
        {
            InitializeComponent();
        }

        private void dgSettlementImportJson_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgSettlementImportJson.SelectedItem = e.Row.Item;
        }
    }
}
