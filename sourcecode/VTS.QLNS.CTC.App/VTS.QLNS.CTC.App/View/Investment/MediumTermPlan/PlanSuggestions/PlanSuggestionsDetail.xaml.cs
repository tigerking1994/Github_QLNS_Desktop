using System;
using System.Collections.Generic;
using System.Linq;
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
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanSuggestions
{
    /// <summary>
    /// Interaction logic for PlanSuggestionsDetail.xaml
    /// </summary>
    public partial class PlanSuggestionsDetail : Window
    {
        public PlanSuggestionsDetail()
        {
            InitializeComponent();
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (PlanSuggestionsDetailViewModel)this.DataContext;
            if (vm != null)
            {
                var selected = (VdtKhvKeHoach5NamDeXuatChiTietModel)e.Row.Item;
                var curentColumn = e.Column;

                if (selected.IsParent && (curentColumn.SortMemberPath.Equals("FHanMucDauTu")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuNhat")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuHai")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuBa")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuTu")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuNam")
                    || curentColumn.SortMemberPath.Equals("FGiaTriBoTri")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuNhatDc")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuHaiDc")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuBaDc")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuTuDc")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuNamDc")
                    || curentColumn.SortMemberPath.Equals("FGiaTriNamThuNamDc")
                    || curentColumn.SortMemberPath.Equals("FGiaTriBoTriDc")))
                {
                    e.Cancel = true;
                }

                if (selected.IsStatus.Equals(MediumTermPlanType.GROUP) && !curentColumn.SortMemberPath.Equals("STen"))
                {
                    e.Cancel = true;
                }

                if (vm.IsDuAnChuyenTiep)
                {
                    if (curentColumn.SortMemberPath.Equals("FGiaTriNamThuNhat")
                        || curentColumn.SortMemberPath.Equals("FGiaTriNamThuHai")
                        || curentColumn.SortMemberPath.Equals("FGiaTriNamThuBa")
                        || curentColumn.SortMemberPath.Equals("FGiaTriNamThuTu")
                        || curentColumn.SortMemberPath.Equals("FGiaTriNamThuNam")
                        || curentColumn.SortMemberPath.Equals("SGhiChu"))
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }

                if ((selected.BActive != null && !selected.BActive.Value) || selected.IsDeleted || selected.IsParent)
                {
                    e.Cancel = true;
                }

                if(selected.IsDieuChinhProject && curentColumn.SortMemberPath.Equals("FHanMucDauTu"))
                {
                    e.Cancel = true;
                }
            }
        }


        private void dgdDataPlanManagerDetailDeXuat_ScrollChanged(object sender, ScrollChangedEventArgs e)
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
                                            case 8:
                                                var t = FindChild<TextBlock>(dataGrid, "canvas8");
                                                if (t is object) t.Visibility = Visibility.Collapsed;
                                                break;
                                            case 13:
                                                var t1 = FindChild<TextBlock>(dataGrid, "canvas13");
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
                            case 8:
                                var t = FindChild<TextBlock>(dataGrid, "canvas8");
                                if (t is object) t.Visibility = Visibility.Visible;
                                break;
                            case 13:
                                var t1 = FindChild<TextBlock>(dataGrid, "canvas13");
                                if (t1 is object) t1.Visibility = Visibility.Visible;
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
        }

        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T c)
                {
                    return c;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild is T d) return d;
                }
            }
            return null;
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
