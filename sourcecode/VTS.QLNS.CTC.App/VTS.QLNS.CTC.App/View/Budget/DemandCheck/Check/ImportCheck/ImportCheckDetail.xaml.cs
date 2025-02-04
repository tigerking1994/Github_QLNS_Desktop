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
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.ImportCheck;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.ImportCheck
{
    /// <summary>
    /// Interaction logic for ImportCheckDetail.xaml
    /// </summary>
    public partial class ImportCheckDetail : Window
    {
        public ImportCheckDetail()
        {
               InitializeComponent();
                DgCheckImport.BeginningEdit += dgdData_BeginningEdit;
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgCheckImport.SelectedItem = e.Row.Item;
            var vm = (ImportCheckDetailViewModel)this.DataContext;
            if (vm != null && ((CheckVoucherDetailImportModel)DgCheckImport.SelectedItem).BHangCha)
            {
                e.Cancel = true;
            }
        }
    }
}
