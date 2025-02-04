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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH
{
    /// <summary>
    /// Interaction logic for CapPhatBoSungIndex.xaml
    /// </summary>
    public partial class QuyetToanChiQuyBHXHIndex : UserControl
    {
        public QuyetToanChiQuyBHXHIndex()
        {
            InitializeComponent();
        }
        private void DgCapPhatBoSungIndex_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (QuyetToanChiQuyBHXHIndexViewModel)this.DataContext;
            DgCapPhatBoSungIndex.ReloadElementFrozenColumnData();
        }
    }
}
