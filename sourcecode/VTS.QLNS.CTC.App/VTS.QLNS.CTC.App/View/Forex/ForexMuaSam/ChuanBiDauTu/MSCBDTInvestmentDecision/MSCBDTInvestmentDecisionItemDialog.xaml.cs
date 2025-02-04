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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentDecision;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentDecision
{
    /// <summary>
    /// Interaction logic for ForexInvestmentDecisionItemDialog.xaml
    /// </summary>
    public partial class MSCBDTInvestmentDecisionItemDialog : UserControl
    {
        public MSCBDTInvestmentDecisionItemDialog()
        {
            InitializeComponent();
        }

        private void dgNhQdDauTuHangMuc_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (MSCBDTInvestmentDecisionItemDialogViewModel)this.DataContext;
            if (vm != null)
            {
                vm.QdDauTuHangMuc_BeginningEditHanlder(e);
            }
        }
    }
}
