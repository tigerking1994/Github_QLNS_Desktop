using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Import;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.ImportCheck
{
    /// <summary>
    /// Interaction logic for ImportCheck.xaml
    /// </summary>
    public partial class ImportCheck : Window
    {
        public ImportCheck()
        {
            InitializeComponent();
            DgCheckImport.BeginningEdit += dgdData_BeginningEdit;
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgCheckImport.SelectedItem = e.Row.Item;
            var vm = (ImportCheckViewModel)this.DataContext;
            if (vm != null && ((CheckVoucherDetailImportModel)DgCheckImport.SelectedItem).BHangCha)
            {
                e.Cancel = true;
            }
        }
    }
}
