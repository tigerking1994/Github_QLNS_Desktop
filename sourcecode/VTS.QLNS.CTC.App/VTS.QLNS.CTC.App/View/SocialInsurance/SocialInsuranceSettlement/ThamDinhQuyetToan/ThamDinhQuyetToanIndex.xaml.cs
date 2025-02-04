using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan
{
    /// <summary>
    /// Interaction logic for CapPhatBoSungIndex.xaml
    /// </summary>
    public partial class ThamDinhQuyetToanIndex : UserControl
    {
        public ThamDinhQuyetToanIndex()
        {
            InitializeComponent();
        }
        private void DgCapPhatBoSungIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (ThamDinhQuyetToanIndexViewModel)this.DataContext;
            DgCapPhatBoSungIndex.ReloadElementFrozenColumnData();
        }
    }
}
