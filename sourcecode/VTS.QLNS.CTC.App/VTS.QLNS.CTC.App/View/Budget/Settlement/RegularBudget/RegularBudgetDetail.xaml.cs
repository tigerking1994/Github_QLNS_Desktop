using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.RegularBudget;

namespace VTS.QLNS.CTC.App.View.Budget.Settlement.RegularBudget
{
    /// <summary>
    /// Interaction logic for RegularBudgetDetailWindow.xaml
    /// </summary>
    public partial class RegularBudgetDetail : Window
    {
        public RegularBudgetDetail()
        {
            InitializeComponent();
            DgRegularBudgetDetail1.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgRegularBudgetDetail1.SelectedItem = e.Row.Item;
            var vm = (RegularBudgetDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BKhoa || !vm.SelectedItem.IsEditable || !vm.IsEditByRole
                || ((!string.IsNullOrEmpty(vm.Model.STongHop) && !vm.Model.STongHop.Equals(vm.Model.SSoChungTu)) && !vm.IsShowColumnDonVi && e.Column.SortMemberPath != nameof(SettlementVoucherDetailModel.SGhiChu))
                || (vm.IsReadonlyDeNghi && e.Column.SortMemberPath == nameof(SettlementVoucherDetailModel.FTuChiDeNghi))))
            {
                e.Cancel = true;
            }
        }
        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var scrollViewer = e.OriginalSource as ScrollViewer;

            if (scrollViewer is object)
            {
                var columnFronzen = GetColumnStartOffset(dataGrid, dataGrid.FrozenColumnCount);
                foreach (var col in dataGrid.Columns.Select((value, index) => new { value, index }))
                {
                    var columnToCheck = GetColumnStartOffset(dataGrid, col.index);

                    if (columnToCheck - columnFronzen > 0 && columnToCheck - columnFronzen < scrollViewer.HorizontalOffset)
                    {
                        if (dataGrid.Columns[col.index - 1].HeaderTemplate?.LoadContent() is Grid grid)
                        {
                            var children = grid.Children;
                            foreach (var child in children)
                            {
                                if (child is Border control && control.Child is Canvas canvas)
                                {
                                    var g = canvas.Children[0] as Grid;
                                    if (g != null)
                                    {
                                        var textBlock = g.Children[0] as TextBlock;
                                        if (textBlock != null) textBlock.Visibility = Visibility.Collapsed;
                                        switch (col.index)
                                        {
                                            case 18:
                                                var t = FindChild<TextBlock>(dataGrid, "canvas18");
                                                if (t is object) t.Visibility = Visibility.Collapsed;
                                                break;
                                            case 24:
                                                var t1 = FindChild<TextBlock>(dataGrid, "canvas24");
                                                if (t1 is object) t1.Visibility = Visibility.Collapsed;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        switch (col.index)
                        {
                            case 18:
                                var t = FindChild<TextBlock>(dataGrid, "canvas18");
                                if (t is object) t.Visibility = Visibility.Visible;
                                break;
                            case 24:
                                var t1 = FindChild<TextBlock>(dataGrid, "canvas24");
                                if (t1 is object) t1.Visibility = Visibility.Visible;
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent is valid.
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // Recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // If the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        private double GetColumnStartOffset(DataGrid dataGrid, int columnIndexToCheck)
        {
            double offset = 0;
            for (int i = 0; i < columnIndexToCheck; i++)
            {
                if (dataGrid.Columns[i].Visibility == Visibility.Visible)
                    offset += dataGrid.Columns[i].ActualWidth;
            }
            return offset;
        }
    }
}
