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

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH
{
    /// <summary>
    /// Interaction logic for QuyetToanThuIndex.xaml
    /// </summary>
    public partial class QuyetToanThuIndex : UserControl
    {
        public QuyetToanThuIndex()
        {
            InitializeComponent();
        }
        private void DgQttBHXHIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (QuyetToanThuIndexViewModel)this.DataContext;
            DgQttBHXHIndex.ReloadElementFrozenColumnData();
        }
    }
}
