using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Shared;

namespace VTS.QLNS.CTC.App.View.Shared
{
    /// <summary>
    /// Interaction logic for MucLucPopupBoxCapBacLuongView.xaml
    /// </summary>
    public partial class MucLucPopupBoxCapBacLuongView : UserControl
    {
        public MucLucPopupBoxCapBacLuongView()
        {
            InitializeComponent();
            //DataGridCapBac.Loaded += GenericControlCustom_Loaded;
            DataGridCapBacLuong.Loaded += GenericControlCustom_Loaded;
        }

        private void GenericControlCustom_Loaded(object sender, RoutedEventArgs e)
        {
            //// Clear ItemBindingGroup so it isn't applied to new rows
            //DataGridCapBac.ItemBindingGroup = null;
            //DataGridCapBacLuong.ItemBindingGroup = null;
            //// Clear BindingGroup on already created rows
            //foreach (var item in DataGridCapBac.Items)
            //{
            //    var row = DataGridCapBac.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
            //    if (row != null)
            //        row.BindingGroup = null;
            //}

            foreach (var item in DataGridCapBacLuong.Items)
            {
                var row = DataGridCapBacLuong.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (row != null)
                    row.BindingGroup = null;
            }

            //if (DataGridCapBac.Items.Count > 0)
            //{
            //    var cell = new DataGridCellInfo(DataGridCapBac.Items[0], DataGridCapBac.Columns[0]);
            //    DataGridCapBac.CurrentCell = cell;
            //    DataGridCapBac.BeginEdit();
            //    DataGridCapBac.CancelEdit();
            //    DataGridCapBac.CancelEdit();
            //}

            if (DataGridCapBacLuong.Items.Count > 0)
            {
                var cell = new DataGridCellInfo(DataGridCapBacLuong.Items[0], DataGridCapBacLuong.Columns[0]);
                DataGridCapBacLuong.CurrentCell = cell;
                DataGridCapBacLuong.BeginEdit();
                DataGridCapBacLuong.CancelEdit();
                DataGridCapBacLuong.CancelEdit();
            }
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //DataGridCapBac.SelectedItem = e.Row.Item;
            var vm = DataContext;
            var property = vm.GetType().GetProperty("SelectedCapBacItem");
            var propertyVal = property.GetValue(vm, null);
            if (vm != null && propertyVal != null)
            {
                var selectedItem = propertyVal as TlDmCapBacNq104Model;
                if (!selectedItem.IsEditable)
                    e.Cancel = true;
            }
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            var x = sender as System.Windows.Controls.TextBox;
            Keyboard.Focus(x);
        }

        private void MaCapBacTextBox_Loaded(object sender, KeyEventArgs e)
        {
            MucLucCapBacLuongViewModel mucLucCapBacLuongViewModel = DataContext as MucLucCapBacLuongViewModel;
            if (e.Key == Key.Return)
            {
                mucLucCapBacLuongViewModel.OnFilterDataByColumn(nameof(NsMuclucNgansachModel.Lns));
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                mucLucCapBacLuongViewModel.ToggleFilterPopup(false, nameof(NsMuclucNgansachModel.Lns)
                    + mucLucCapBacLuongViewModel.PopupBoxSuffix);
                e.Handled = true;
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
