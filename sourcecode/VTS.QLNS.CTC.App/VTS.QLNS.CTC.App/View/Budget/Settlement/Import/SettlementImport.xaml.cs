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
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Import;

namespace VTS.QLNS.CTC.App.View.Budget.Settlement.Import
{
    /// <summary>
    /// Interaction logic for SetlementImport.xaml
    /// </summary>
    public partial class SettlementImport : Window
    {
        public SettlementImport()
        {
            InitializeComponent();
            DgSettlementImport1.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgSettlementImport1.SelectedItem = e.Row.Item;
            var vm = (SettlementImportViewModel)this.DataContext;
            if (vm != null && ((SettlementVoucherDetailImportModel)DgSettlementImport1.SelectedItem).BHangCha)
            {
                e.Cancel = true;
            }
        }
    }
}
