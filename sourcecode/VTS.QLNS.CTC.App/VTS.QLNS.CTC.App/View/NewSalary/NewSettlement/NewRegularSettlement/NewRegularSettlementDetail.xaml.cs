using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewRegularSettlement;

namespace VTS.QLNS.CTC.App.View.NewSalary.NewSettlement.NewRegularSettlement
{
    /// <summary>
    /// Interaction logic for RegularSettlementDetail.xaml
    /// </summary>
    public partial class NewRegularSettlementDetail : Window
    {
        public NewRegularSettlementDetail()
        {
            InitializeComponent();
            DgRegularSettlementDetail.BeginningEdit += DgData_BeginningEdit;
        }

        private void DgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (NewRegularSettlementDetailViewModel)this.DataContext;
            DgRegularSettlementDetail.SelectedItem = e.Row.Item;
            if (vm != null)
            {
                var selected = (TlQtChungTuChiTietNq104Model)e.Row.Item;
                if (selected.BHangCha == true)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
