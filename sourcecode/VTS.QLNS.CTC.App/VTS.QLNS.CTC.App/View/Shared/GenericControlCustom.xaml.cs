using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
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
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.View.Shared
{
    /// <summary>
    /// Interaction logic for PageControlCustom.xaml
    /// </summary>

    public partial class GenericControlCustom : UserControl
    {
        public GenericControlCustom()
        {
            InitializeComponent();
            this.dgdData.Loaded += GenericControlCustom_Loaded;
        }

        private void GenericControlCustom_Loaded(object sender, RoutedEventArgs e)
        {
            // Clear ItemBindingGroup so it isn't applied to new rows
            this.dgdData.ItemBindingGroup = null;
            // Clear BindingGroup on already created rows
            foreach (var item in this.dgdData.Items)
            {
                var row = dgdData.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (row != null)
                {
                    row.BindingGroup = null;
                }
            }
            if (dgdData.Items.Count > 0)
            {
                var cell = new DataGridCellInfo(dgdData.Items[0], dgdData.Columns[0]);
                dgdData.CurrentCell = cell;
                dgdData.BeginEdit();
                dgdData.CancelEdit();
                dgdData.CancelEdit();
            }
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdData.SelectedItem = e.Row.Item;
            var vm = this.DataContext;
            var property = vm.GetType().GetProperty("SelectedItem");
            var propertyVal = property.GetValue(vm, null);
            if (vm != null && propertyVal != null)
            {
                var selectedItem = propertyVal as ModelBase;
                if (!selectedItem.IsEditable && selectedItem.GetType().Name != nameof(DmCongKhaiTaiChinhModel))
                {
                    e.Cancel = true;

                }
                if (!selectedItem.IsEnabled && selectedItem.GetType().Name == nameof(BhDmThamDinhQuyetToanModel))
                {
                    e.Cancel = true;
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
