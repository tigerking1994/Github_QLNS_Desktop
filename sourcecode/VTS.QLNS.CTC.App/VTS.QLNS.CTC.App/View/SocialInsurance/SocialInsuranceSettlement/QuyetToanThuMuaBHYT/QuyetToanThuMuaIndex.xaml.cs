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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT
{
    /// <summary>
    /// Interaction logic for QuyetToanThuMuaIndex.xaml
    /// </summary>
    public partial class QuyetToanThuMuaIndex : UserControl
    {
        public QuyetToanThuMuaIndex()
        {
            InitializeComponent();
        }
        private void DgQttmBHYTIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (QuyetToanThuMuaIndexViewModel)this.DataContext;
            DgQttmBHYTIndex.ReloadElementFrozenColumnData();
        }
    }
}
