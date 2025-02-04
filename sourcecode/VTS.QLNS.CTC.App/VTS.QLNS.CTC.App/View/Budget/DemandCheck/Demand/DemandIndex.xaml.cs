using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand
{
    /// <summary>
    ///     Interaction logic for DemandNumber.xaml
    /// </summary>
    public partial class DemandIndex : UserControl
    {
        public DemandIndex()
        {
            InitializeComponent();
        }

        private void DgDemandIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (DemandIndexViewModel)this.DataContext;
            vm.OnChangeVisibilityColumn();
            DgDemandIndex.ReloadElementFrozenColumnData();
        }
    }
}
