using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH
{
    /// <summary>
    /// Interaction logic for CapPhatBoSungIndex.xaml
    /// </summary>
    public partial class QuyetToanChiNamBHXHIndex : UserControl
    {
        public QuyetToanChiNamBHXHIndex()
        {
            InitializeComponent();
        }
        private void DgCapPhatBoSungIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (QuyetToanChiNamBHXHIndexViewModel)this.DataContext;
            DgCapPhatBoSungIndex.ReloadElementFrozenColumnData();
        }
    }
}
