using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo
{
    /// <summary>
    /// Interaction logic for ForexContractInfoDialog.xaml
    /// </summary>
    public partial class ForexContractInfoDialog : Window
    {
        public ForexContractInfoDialog()
        {
            InitializeComponent();
            dgdDataNhDaHopDongHangMucDetail.BeginningEdit += dgdDataNhDaHopDongHangMucDetail_BeginningEdit;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void dgdDataNhDaHopDongHangMucDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (ForexContractInfoDialogViewModel)this.DataContext;
            if (viewModel != null)
            {
                viewModel.GoiThauChiPhi_BeginningEditHanlder(e);
            }
        }
    }
}
