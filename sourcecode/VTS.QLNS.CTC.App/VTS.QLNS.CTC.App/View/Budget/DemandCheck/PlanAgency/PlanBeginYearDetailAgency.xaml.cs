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
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PlanAgency;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.PlanAgency
{
    /// <summary>
    /// Interaction logic for PlanBeginYearDetailAgency.xaml
    /// </summary>
    public partial class PlanBeginYearDetailAgency : Window
    {
        public PlanBeginYearDetailAgency()
        {
            InitializeComponent();
            dgdDataPlanBeginYearDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataPlanBeginYearDetail.SelectedItem = e.Row.Item;
            Model.SktSoLieuChiTietMLNSModel item = (Model.SktSoLieuChiTietMLNSModel)e.Row.Item;
            if (item == null || item.IsHangCha || item.IsReadonlyByLNSUser)
            {
                e.Cancel = true;
            }

            var vm = (PlanBeginYearDetailAgencyViewModel)this.DataContext;
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
    }
}
