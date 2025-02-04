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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTInvestmentDecision;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.ChuanBiDauTu.DACBDTInvestmentDecision
{
    /// <summary>
    /// Interaction logic for ForexInvestmentDecisionDialog.xaml
    /// </summary>
    public partial class DACBDTInvestmentDecisionDialog : Window
    {
        public DACBDTInvestmentDecisionDialog()
        {
            InitializeComponent();
        }

        private void dgNhQdDauTuNguonVon_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            dataGrid.SelectedItem = e.Row.Item;
        }

        private void dgNhQdDauTuChiPhi_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (DACBDTInvestmentDecisionDialogViewModel)this.DataContext;
            if (vm != null)
            {
                vm.QdDauTuChiPhi_BeginningEditHanlder(e);
            }
        }
    }
}
