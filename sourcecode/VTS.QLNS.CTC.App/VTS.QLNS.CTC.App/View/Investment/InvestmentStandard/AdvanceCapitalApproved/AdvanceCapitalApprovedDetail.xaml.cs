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
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.AdvanceCapitalApproved;

namespace VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.AdvanceCapitalApproved
{
    /// <summary>
    /// Interaction logic for AdvanceCapitalApprovedDetail.xaml
    /// </summary>
    public partial class AdvanceCapitalApprovedDetail : Window
    {
        public AdvanceCapitalApprovedDetail()
        {
            InitializeComponent();
            dgdAdvanceCapitalApprovedDetail.BeginningEdit += DgdData_BeginningEdit;
        }

        private void DgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (AdvanceCapitalApprovedDetailViewModel)this.DataContext;
            dgdAdvanceCapitalApprovedDetail.SelectedItem = e.Row.Item;
        }

        private void fChiTieuNganSach_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (AdvanceCapitalApprovedDetailViewModel)this.DataContext;
            vm.CheckMucLucNganSach();
        }
    }
}
