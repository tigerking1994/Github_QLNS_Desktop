using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPCNTDecision;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPCNTDecision
{
    /// <summary>
    /// Interaction logic for DecisionHangMucDetailDialog.xaml
    /// </summary>
    public partial class MSPCNTDecisionHangMucDetailDialog : Window
    {
        public MSPCNTDecisionHangMucDetailDialog()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void dgdDataHangMucDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataQuyetDinhChiPhiHangMucDetail.SelectedItem = e.Row.Item;
        }

        private void FGiaTriHangMuc_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (MSPCNTDecisionHangMucDetailDialogViewModel)this.DataContext;
            var control = (TextBox)sender;
            if (vm.CheckHangMucCanEditGiatri())
            {
                control.IsEnabled = true;
            }
            else
            {
                control.IsEnabled = false;
            }
        }
    }
}