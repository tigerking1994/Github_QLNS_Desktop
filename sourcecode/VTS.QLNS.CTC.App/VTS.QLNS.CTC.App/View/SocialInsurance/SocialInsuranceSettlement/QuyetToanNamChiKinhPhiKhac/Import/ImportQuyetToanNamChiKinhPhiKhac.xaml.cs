using System.Windows;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.Import;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.Import
{
    /// <summary>
    /// Interaction logic for ImportQuyetToanNamChiKinhPhiKhac.xaml
    /// </summary>
    public partial class ImportQuyetToanNamChiKinhPhiKhac : Window
    {
        public ImportQuyetToanNamChiKinhPhiKhac()
        {
            InitializeComponent();
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImportQuyetToanNamChiKinhPhiKhacViewModel)this.DataContext;
        }
    }
}
