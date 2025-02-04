using System.Windows;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT.Import;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT.Import
{
    /// <summary>
    /// Interaction logic for NhanPhanBoDuToanThuBHYTImport.xaml
    /// </summary>
    public partial class ImportNhanPhanBoDuToanThuBHYT : Window
    {
        public ImportNhanPhanBoDuToanThuBHYT()
        {
            InitializeComponent();
        }

        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = (NhanPhanBoDuToanThuBHYTImportlViewModel)this.DataContext;
        }
    }
}
