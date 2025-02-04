using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT
{
    /// <summary>
    /// Interaction logic for AllocationList.xaml
    /// </summary>
    public partial class CapPhatTamUngKCBBHYTIndex : UserControl
    {
        public CapPhatTamUngKCBBHYTIndex()
        {
            InitializeComponent();
        }
        private void DgCapPhatTamUngIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (CapPhatTamUngKCBBHYTIndexViewModel)this.DataContext;
            dgdDataAllocationIndex.ReloadElementFrozenColumnData();
        }
    }
}
