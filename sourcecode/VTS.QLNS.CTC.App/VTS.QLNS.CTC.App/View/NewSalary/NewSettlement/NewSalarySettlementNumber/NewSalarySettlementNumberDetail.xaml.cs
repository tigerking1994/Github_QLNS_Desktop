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
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewSettlementNumber;

namespace VTS.QLNS.CTC.App.View.NewSalary.NewSettlement.NewSalarySettlementNumber
{
    /// <summary>
    /// Interaction logic for SalarySettlementNumberDetail.xaml
    /// </summary>
    public partial class NewSalarySettlementNumberDetail : Window
    {
        public NewSalarySettlementNumberDetail()
        {
            InitializeComponent();
        }

        private void DgArmyDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgdDataSettlementNumberDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (NewSettlementNumberDetailViewModel)DataContext;
            if (viewModel != null)
            {
                viewModel.SettlementNumberDetail_BeginningEditHanlder(e);
            }
        }
    }
}
