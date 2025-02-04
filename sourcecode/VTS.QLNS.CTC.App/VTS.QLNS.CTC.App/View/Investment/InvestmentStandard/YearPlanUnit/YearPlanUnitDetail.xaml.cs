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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.VonNamDonVi;

namespace VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.VonNamDonVi
{
    /// <summary>
    /// Interaction logic for YearPlanDetail.xaml
    /// </summary>
    public partial class VonNamDonViDetail : Window
    {
        public VonNamDonViDetail()
        {
            InitializeComponent();
        }
        
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (YearPlanUnitDetailViewModel)this.DataContext;

            if(vm != null)
            {
                var selected = (PhanBoVonDonViChiTietModel)e.Row.Item;

                if(selected != null && selected.BActive != null && !selected.BActive.Value)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
