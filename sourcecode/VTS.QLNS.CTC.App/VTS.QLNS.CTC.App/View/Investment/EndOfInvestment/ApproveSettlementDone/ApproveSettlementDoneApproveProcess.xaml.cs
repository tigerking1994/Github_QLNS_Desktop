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
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ApproveSettlementDone;

namespace VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.ApproveSettlementDone
{
    /// <summary>
    /// Interaction logic for ApproveSettlementDoneApproveProcess.xaml
    /// </summary>
    public partial class ApproveSettlementDoneApproveProcess : Window
    {
        public ApproveSettlementDoneApproveProcess()
        {
            InitializeComponent();
            dgdPheDuyetQuyetToanProcess.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdPheDuyetQuyetToanProcess.SelectedItem = e.Row.Item;
            var vm = (ApproveSettlementDoneApproveProcessViewModel)this.DataContext;
            if (vm != null && (!vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
        }
    }
}
