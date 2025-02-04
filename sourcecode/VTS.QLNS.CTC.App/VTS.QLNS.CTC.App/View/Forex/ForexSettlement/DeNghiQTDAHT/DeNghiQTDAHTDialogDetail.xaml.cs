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
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT;

namespace VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT
{
    /// <summary>
    /// Interaction logic for DeNghiQTDAHTDialogDetail.xaml
    /// </summary>
    public partial class DeNghiQTDAHTDialogDetail : Window
    {
        public DeNghiQTDAHTDialogDetail()
        {
            InitializeComponent();
        }

        private void dgdDataCPHM_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (DeNghiQTDAHTDialogDetailViewModel)DataContext;
            if (viewModel != null)
            {
                viewModel.ChiPhiHangMuc_BeginningEditHanlder(e);
            }
        }

        private void dgdDataCPHM_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
