using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check
{
    /// <summary>
    /// Interaction logic for CheckExpertiseDetail.xaml
    /// </summary>
    public partial class CheckExpertiseDetail : Window
    {
        public Action<object> SavedAction;
        public CheckExpertiseDetail()
        {
            InitializeComponent();
            dgdDataExpertiseDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataExpertiseDetail.SelectedItem = e.Row.Item;
            var vm = (CheckExpertiseDetailViewModel)this.DataContext;
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
