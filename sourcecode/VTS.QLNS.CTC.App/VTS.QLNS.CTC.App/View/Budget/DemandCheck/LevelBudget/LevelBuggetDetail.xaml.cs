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
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.LevelBudget;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.LevelBudget
{
    /// <summary>
    /// Interaction logic for LevelBuggetDetail.xaml
    /// </summary>
    public partial class LevelBuggetDetail : Window
    {
        public LevelBuggetDetail()
        {
            InitializeComponent();
            dgdDataLevelBuggetDetail.BeginningEdit += dgdData_BeginningEdit;
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataLevelBuggetDetail.SelectedItem = e.Row.Item;
            var vm = (LevelBuggetDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.IsLocked || !vm.SelectedItem.IsEditable))
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
