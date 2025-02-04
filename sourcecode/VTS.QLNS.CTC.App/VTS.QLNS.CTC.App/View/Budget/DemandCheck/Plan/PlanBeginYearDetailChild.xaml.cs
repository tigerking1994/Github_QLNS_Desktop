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
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan
{
    /// <summary>
    /// Interaction logic for PlanBeginYearDetailChild.xaml
    /// </summary>
    public partial class PlanBeginYearDetailChild : Window
    {
        public PlanBeginYearDetailChild()
        {
            InitializeComponent();
            dgdDataPlanBeginYearDetailChildDonVi.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataPlanBeginYearDetailChildDonVi.SelectedItem = e.Row.Item;
            Model.SktSoLieuPhanCapModel item = (Model.SktSoLieuPhanCapModel)e.Row.Item;
            if (item == null || item.IsHangCha)
            {
                e.Cancel = true;
            }

            var vm = (PlanBeginYearDetailChildViewModel)this.DataContext;
            if (vm != null && (!vm.SelectedItem.IsEditable))
            {
                e.Cancel = true;
            }
        }
    }
}
