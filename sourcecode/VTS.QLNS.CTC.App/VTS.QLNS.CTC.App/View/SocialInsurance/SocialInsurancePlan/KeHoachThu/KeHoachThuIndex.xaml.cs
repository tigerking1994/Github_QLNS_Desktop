using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu
{
    /// <summary>
    /// Interaction logic for SocialInsurancePlan.xaml
    /// </summary>
    public partial class KeHoachThuIndex : UserControl
    {
        public KeHoachThuIndex()
        {
            InitializeComponent();
        }
        private void DgKhtBHXHIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (KeHoachThuIndexViewModel)this.DataContext;
            DgKhtBHXHIndex.ReloadElementFrozenColumnData();
        }
    }
}