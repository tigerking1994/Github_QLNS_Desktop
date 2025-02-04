using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement;

namespace VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement
{
    /// <summary>
    /// Interaction logic for RequestSettlementDetail.xaml
    /// </summary>
    public partial class RequestSettlementDetail : Window
    {
        public RequestSettlementDetail()
        {
            InitializeComponent();
            dgdDeNghiQuyetToanChiTiet.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDeNghiQuyetToanChiTiet.SelectedItem = e.Row.Item;
            var vm = (RequestSettlementDetailViewModel)this.DataContext;
            if (vm != null && (!vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
        }
    }
}
