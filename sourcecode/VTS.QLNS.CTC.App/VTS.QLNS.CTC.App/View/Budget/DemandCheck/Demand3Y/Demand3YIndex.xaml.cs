using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand3Y;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand3Y
{
    /// <summary>
    ///     Interaction logic for DemandNumber.xaml
    /// </summary>
    public partial class Demand3YIndex : UserControl
    {
        public Demand3YIndex()
        {
            InitializeComponent();
        }

        private void DgDemand3YIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (Demand3YIndexViewModel)this.DataContext;
            vm.OnChangeVisibilityColumn();
            DgDemand3YIndex.ReloadElementFrozenColumnData();
        }
    }
}
