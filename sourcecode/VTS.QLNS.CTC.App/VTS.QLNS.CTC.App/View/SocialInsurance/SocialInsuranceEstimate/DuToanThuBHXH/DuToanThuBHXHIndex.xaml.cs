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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH
{
    /// <summary>
    /// Interaction logic for DuToanThuBHXHThuIndex.xaml
    /// </summary>
    public partial class DuToanThuBHXHIndex : UserControl
    {
        public DuToanThuBHXHIndex()
        {
            InitializeComponent();
        }
        private void DgDttBHXHIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (DuToanThuBHXHIndexViewModel)this.DataContext;

        }
    }
}
