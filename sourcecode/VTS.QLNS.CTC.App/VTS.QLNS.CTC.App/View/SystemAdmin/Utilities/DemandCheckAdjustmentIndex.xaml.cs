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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities;

namespace VTS.QLNS.CTC.App.View.SystemAdmin.Utilities
{
    /// <summary>
    /// Interaction logic for DemandCheckAdjustmentIndex.xaml
    /// </summary>
    public partial class DemandCheckAdjustmentIndex : UserControl
    {
        public DemandCheckAdjustmentIndex()
        {
            InitializeComponent();
        }
        private void DgCheckIndex_OnTargetUpdatedUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (DemandCheckAdjustmentViewModel)this.DataContext;
            vm.OnChangeVisibilityColumn();
            DgAdjustCheckIndex.ReloadElementFrozenColumnData();
        }
    }
}
