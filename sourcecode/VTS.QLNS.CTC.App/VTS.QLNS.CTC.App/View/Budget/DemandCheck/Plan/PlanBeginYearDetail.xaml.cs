using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan
{
    /// <summary>
    /// Interaction logic for PlanBeginYearDetail.xaml
    /// </summary>
    public partial class PlanBeginYearDetail : Window
    {
        public PlanBeginYearDetail()
        {
            InitializeComponent();
            dgdDataPlanBeginYearDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataPlanBeginYearDetail.SelectedItem = e.Row.Item;
            Model.SktSoLieuChiTietMLNSModel item = (Model.SktSoLieuChiTietMLNSModel)e.Row.Item;
            if (item == null || item.IsHangCha)
            {
                e.Cancel = true;
            }

            var vm = (PlanBeginYearDetailViewModel)this.DataContext;
            if (vm != null && (!vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void CategoryExpandedDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
