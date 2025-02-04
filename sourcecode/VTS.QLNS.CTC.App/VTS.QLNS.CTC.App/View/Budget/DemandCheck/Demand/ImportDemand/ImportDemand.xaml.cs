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
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Import;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand.ImportDemand
{
    /// <summary>
    /// Interaction logic for ImportDemand.xaml
    /// </summary>
    public partial class ImportDemand : Window
    {
        public ImportDemand()
        {
            InitializeComponent();
            DgDemandImport.BeginningEdit += dgdData_BeginningEdit;
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgDemandImport.SelectedItem = e.Row.Item;
            var vm = (ImportDemandViewModel)this.DataContext;
            if (vm != null && ((DemandVoucherDetailImportModel)DgDemandImport.SelectedItem).BHangCha)
            {
                e.Cancel = true;
            }
        }

    }
}
