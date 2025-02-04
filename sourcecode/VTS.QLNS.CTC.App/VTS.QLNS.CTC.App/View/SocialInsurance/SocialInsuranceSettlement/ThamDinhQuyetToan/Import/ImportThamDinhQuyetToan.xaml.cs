using System.Windows;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.Import
{
    /// <summary>
    /// Interaction logic for ImportQuyetToanNamChiKinhPhiKhac.xaml
    /// </summary>
    public partial class ImportThamDinhQuyetToan : Window
    {
        public ImportThamDinhQuyetToan()
        {
            InitializeComponent();
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as ImportThamDinhQuyetToan;
        }
    }
}
