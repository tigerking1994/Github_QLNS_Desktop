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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT
{
    /// <summary>
    /// Interaction logic for QuyetToanCapKinhPhiKcbIndex.xaml
    /// </summary>
    public partial class QuyetToanCapKinhPhiKcbIndex : UserControl
    {
        public QuyetToanCapKinhPhiKcbIndex()
        {
            InitializeComponent();
        }
        private void DgQTCapKPKCBIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (QuyetToanCapKinhPhiKcbIndexViewModel)this.DataContext;
            DgQTCapKPKCBIndex.ReloadElementFrozenColumnData();
        }
    }
}
