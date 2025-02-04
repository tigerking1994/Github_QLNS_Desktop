using System.Windows;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy.Import;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy.Import
{
    /// <summary>
    /// Interaction logic for ImportQuyetToanChiNamKinhPhiQuanLy.xaml
    /// </summary>
    public partial class ImportQuyetToanChiNamKinhPhiQuanLy : Window
    {
        public ImportQuyetToanChiNamKinhPhiQuanLy()
        {
            InitializeComponent();
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImportQuyetToanChiNamKinhPhiQuanLyViewModel)this.DataContext;
        }
    }
}
