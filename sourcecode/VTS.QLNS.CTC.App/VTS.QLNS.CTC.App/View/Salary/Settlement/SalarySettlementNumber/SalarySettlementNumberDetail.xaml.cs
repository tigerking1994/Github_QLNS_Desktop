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
using VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SettlementNumber;

namespace VTS.QLNS.CTC.App.View.Salary.Settlement.SalarySettlementNumber
{
    /// <summary>
    /// Interaction logic for SalarySettlementNumberDetail.xaml
    /// </summary>
    public partial class SalarySettlementNumberDetail : Window
    {
        public SalarySettlementNumberDetail()
        {
            InitializeComponent();
        }

        private void DgArmyDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgdDataSettlementNumberDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (SettlementNumberDetailViewModel)DataContext;
            if (viewModel != null)
            {
                viewModel.SettlementNumberDetail_BeginningEditHanlder(e);
            }
        }
    }
}
