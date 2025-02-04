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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.View.Shared
{
    /// <summary>
    /// Interaction logic for MuclucSKTView.xaml
    /// </summary>
    public partial class MuclucSKTView : UserControl
    {
        public MuclucSKTView()
        {
            InitializeComponent();
        }

        private void GenericControlCustom_Loaded(object sender, RoutedEventArgs e)
        {
            // Clear ItemBindingGroup so it isn't applied to new rows
            this.dgdDataMLSKT.ItemBindingGroup = null;
            // Clear BindingGroup on already created rows
            foreach (var item in this.dgdDataMLSKT.Items)
            {
                var row = dgdDataMLSKT.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (row != null)
                {
                    row.BindingGroup = null;
                }
            }
            if (dgdDataMLSKT.Items.Count > 0)
            {
                var cell = new DataGridCellInfo(dgdDataMLSKT.Items[0], dgdDataMLSKT.Columns[0]);
                dgdDataMLSKT.CurrentCell = cell;
                dgdDataMLSKT.BeginEdit();
                dgdDataMLSKT.CancelEdit();
                dgdDataMLSKT.CancelEdit();
            }
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataMLSKT.SelectedItem = e.Row.Item;
            var vm = this.DataContext;
            var property = vm.GetType().GetProperty("SelectedItem");
            var propertyVal = property.GetValue(vm, null);
            if (vm != null && propertyVal != null)
            {
                var selectedItem = propertyVal as SktMucLucModel;
                if (!selectedItem.IsEditable)
                    e.Cancel = true;
            }
        }

        private void Lnstxtbox_Loaded(object sender, RoutedEventArgs e)
        {
            var x = sender as System.Windows.Controls.TextBox;
            Keyboard.Focus(x);
        }
    }
}
