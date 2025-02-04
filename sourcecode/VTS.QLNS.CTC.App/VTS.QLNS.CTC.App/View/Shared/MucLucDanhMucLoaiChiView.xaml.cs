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
using VTS.QLNS.CTC.App.ViewModel.Shared;

namespace VTS.QLNS.CTC.App.View.Shared
{
    /// <summary>
    /// Interaction logic for MucLucDanhMucLoaiChiView.xaml
    /// </summary>
    public partial class MucLucDanhMucLoaiChiView : UserControl
    {
        public MucLucDanhMucLoaiChiView()
        {
            InitializeComponent();
            this.dgdDataMLNS.Loaded += GenericControlCustom_Loaded;
        }
        private void GenericControlCustom_Loaded(object sender, RoutedEventArgs e)
        {
            // Clear ItemBindingGroup so it isn't applied to new rows
            this.dgdDataMLNS.ItemBindingGroup = null;
            // Clear BindingGroup on already created rows
            foreach (var item in this.dgdDataMLNS.Items)
            {
                var row = dgdDataMLNS.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (row != null)
                {
                    row.BindingGroup = null;
                }
            }
            if (dgdDataMLNS.Items.Count > 0)
            {
                var cell = new DataGridCellInfo(dgdDataMLNS.Items[0], dgdDataMLNS.Columns[0]);
                dgdDataMLNS.CurrentCell = cell;
                dgdDataMLNS.BeginEdit();
                dgdDataMLNS.CancelEdit();
                dgdDataMLNS.CancelEdit();
            }
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataMLNS.SelectedItem = e.Row.Item;
            var vm = this.DataContext;
            var property = vm.GetType().GetProperty("SelectedItem");
            var propertyVal = property.GetValue(vm, null);
            if (vm != null && propertyVal != null)
            {
                var selectedItem = propertyVal as BhDmMucLucNganSachModel;
                if (!selectedItem.IsEditable)
                    e.Cancel = true;
            }
        }

        private void Lnstxtbox_Loaded(object sender, RoutedEventArgs e)
        {
            var x = sender as System.Windows.Controls.TextBox;
            Keyboard.Focus(x);
        }

        private void Lnstxtbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            MucLucNganSachViewModel mucLucNganSachViewModel = DataContext as MucLucNganSachViewModel;
            if (e.Key == Key.Return)
            {
                mucLucNganSachViewModel.OnFilterDataByColumn(nameof(BhDmMucLucNganSachModel.SLNS));
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                mucLucNganSachViewModel.ToggleFilterPopup(false, nameof(BhDmMucLucNganSachModel.SLNS) + mucLucNganSachViewModel.POPUP_SUFFIX);
                e.Handled = true;
            }
        }
    }
}
