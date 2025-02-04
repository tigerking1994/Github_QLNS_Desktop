using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.RegularSettlement;

namespace VTS.QLNS.CTC.App.View.Salary.Settlement.RegularSettlement
{
    /// <summary>
    /// Interaction logic for RegularSettlementDetail.xaml
    /// </summary>
    public partial class RegularSettlementDetail : Window
    {
        public RegularSettlementDetail()
        {
            InitializeComponent();
            DgRegularSettlementDetail.BeginningEdit += DgData_BeginningEdit;
        }

        private void DgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (RegularSettlementDetailViewModel)this.DataContext;
            DgRegularSettlementDetail.SelectedItem = e.Row.Item;
            if (vm != null)
            {
                var selected = (TlQtChungTuChiTietModel)e.Row.Item;
                if (selected.BHangCha == true)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
