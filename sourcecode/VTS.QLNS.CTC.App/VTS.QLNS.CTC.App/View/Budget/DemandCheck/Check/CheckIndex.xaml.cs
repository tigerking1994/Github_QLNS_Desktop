using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check
{
    /// <summary>
    ///     Interaction logic for DemandNumber.xaml
    /// </summary>
    public partial class CheckIndex : UserControl
    {
        public CheckIndex()
        {
            InitializeComponent();
        }

        private void DgCheckIndex_OnTargetUpdatedUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (CheckIndexViewModel)this.DataContext;
            vm.OnChangeVisibilityColumn();
            DgCheckIndex.ReloadElementFrozenColumnData();
        }
    }
}
