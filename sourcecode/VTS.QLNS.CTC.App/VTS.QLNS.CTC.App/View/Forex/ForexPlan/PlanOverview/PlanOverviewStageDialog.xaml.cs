using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTForexProjectInformation;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanOverview;
using System.Linq;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.App.View.Forex.ForexPlan.PlanOverview
{
    /// <summary>
    /// Interaction logic for PlanOverviewStageDialog.xaml
    /// </summary>
    public partial class PlanOverviewStageDialog : Window
    {
        public PlanOverviewStageDialog()
        {
            InitializeComponent();
            dgdDataChuongTrinhPlanOverview.SelectionChanged += dgdData_BeginSelectionEdit;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dgdDataChuongTrinhPlanOverview_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (PlanOverviewStageDialogViewModel)DataContext;
            if(viewModel != null)
            {
                viewModel.NhiemVuChi_BeginningEditHanlder(e);
            }
        }

        private void dgdDataChuongTrinhPlanOverview_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                ScrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
        private void dgdData_BeginSelectionEdit(object sender, SelectionChangedEventArgs e)
        {
            // ... Get SelectedItems from DataGrid.
            var grid = sender as DataGrid;
            var selected = grid.SelectedItems;
            var vm = (PlanOverviewStageDialogViewModel)this.DataContext;
            var lstIdSelected = new List<Guid>();
            foreach (var item in selected)
            {
                if (vm == null || vm.ItemsKhTongTheNhiemVuChi.IsEmpty()) return;
                var selectedItem = item as NhKhTongTheNhiemVuChiModel;
                if (selectedItem == null) return;
                if (!vm.ItemsKhTongTheNhiemVuChi.Any(x => x.Id.Equals(selectedItem.Id))) return;
                vm.ItemsKhTongTheNhiemVuChi.FirstOrDefault(x => x.Id.Equals(selectedItem.Id)).IsSelected = true;
                lstIdSelected.Add(selectedItem.Id);
            }
            if (!lstIdSelected.IsEmpty())
            {
                if (vm == null || vm.ItemsKhTongTheNhiemVuChi.IsEmpty()) return;
                vm.ItemsKhTongTheNhiemVuChi.Where(x => !lstIdSelected.Contains(x.Id)).Select(y => { y.IsSelected = false; return y; }).ToList();
            }
        }
    }
}