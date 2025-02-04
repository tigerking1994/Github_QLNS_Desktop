using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution
{
    /// <summary>
    ///     Interaction logic for DemandNumber.xaml
    /// </summary>
    public partial class DistributionIndex : UserControl
    {
        public DistributionIndex()
        {
            InitializeComponent();
        }

        private void DgDistributionIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (DistributionIndexViewModel)this.DataContext;
            vm.OnChangeVisibilityColumn();
            DgDistributionIndex.ReloadElementFrozenColumnData();
        }
    }
}
