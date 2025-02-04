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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT
{
    /// <summary>
    /// Interaction logic for KeHoachThuMuaBHYTIndex.xaml
    /// </summary>
    public partial class KeHoachThuMuaBHYTIndex : UserControl
    {
        public KeHoachThuMuaBHYTIndex()
        {
            InitializeComponent();
        }
        private void DgKhtBHXHIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (KeHoachThuMuaBHYTIndexViewModel)this.DataContext;
            DgKhtBHXHIndex.ReloadElementFrozenColumnData();
        }
    }
}
