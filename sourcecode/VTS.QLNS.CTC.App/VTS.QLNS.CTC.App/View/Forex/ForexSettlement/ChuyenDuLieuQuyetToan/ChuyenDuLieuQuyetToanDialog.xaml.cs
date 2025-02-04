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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ChuyenDuLieuQuyetToan;

namespace VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ChuyenDuLieuQuyetToan
{
    /// <summary>
    /// Interaction logic for ChuyenDuLieuQuyetToanDialog.xaml
    /// </summary>
    public partial class ChuyenDuLieuQuyetToanDialog : Window
    {
        public ChuyenDuLieuQuyetToanDialog()
        {
            InitializeComponent();
        }

        private void dgdDataMLNS_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (ChuyenDulieuQuyetToanDialogViewModel)DataContext;
            if (viewModel != null)
            {
                viewModel.ChuyenDuLieu_BeginningEditHanlder(e);
            }
        }
    }
}
