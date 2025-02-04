using System.Windows;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.ImportChiKinhPhiQuanLy;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.ImportChiKinhPhiQuanLy
{
    /// <summary>
    /// Interaction logic for ImportQuyetToanChiKinhPhiQuanLy.xaml
    /// </summary>
    public partial class ImportQuyetToanChiKinhPhiQuanLy : Window
    {
        public ImportQuyetToanChiKinhPhiQuanLy()
        {
            InitializeComponent();
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImportQuyetToanChiKinhPhiQuanLyViewModel)this.DataContext;
        }
    }
}
