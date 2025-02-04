using System.Windows;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Import;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Import
{
    /// <summary>
    /// Interaction logic for ImportQuyetToanChiQuyKinhPhiKhac.xaml
    /// </summary>
    public partial class ImportQuyetToanChiQuyKinhPhiKhac : Window
    {
        public ImportQuyetToanChiQuyKinhPhiKhac()
        {
            InitializeComponent();
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImportQuyetToanChiQuyKinhPhiKhacViewModel)this.DataContext;
        }
    }
}
