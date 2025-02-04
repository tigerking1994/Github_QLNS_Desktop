using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.ImportQtcqBHXH
{
    /// <summary>
    /// Interaction logic for ImportKhtBHXH.xaml
    /// </summary>
    public partial class ImportQuyetToanChiQuyBHXH : Window
    {
        public ImportQuyetToanChiQuyBHXH()
        {
            InitializeComponent();
        }
        private void ButtonOpenError_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Import.ImportQuyetToanChiQuyBHXHViewModel)this.DataContext;
            //var button = sender as Button;
            //var index = vm.OnAddMLNS();
            //if (index == -1) return;
            //DgMLNS.UpdateLayout();
            //DgMLNS.ScrollIntoView(DgMLNS.Items[index]);
        }
        private void dgdDataImportDetail_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //if (e.HorizontalChange != 0.0f)
            //{
            //    scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            //}
        }
    }
}
