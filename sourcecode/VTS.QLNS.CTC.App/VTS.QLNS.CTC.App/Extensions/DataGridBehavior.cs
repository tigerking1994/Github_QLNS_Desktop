using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace VTS.QLNS.CTC.App.Extensions
{
    public class DataGridBehavior
    {
        #region DisplayRowNumber

        public static DependencyProperty DisplayRowNumberProperty =
            DependencyProperty.RegisterAttached("DisplayRowNumber",
                                                typeof(bool),
                                                typeof(DataGridBehavior),
                                                new FrameworkPropertyMetadata(false, OnDisplayRowNumberChanged));
        public static bool GetDisplayRowNumber(DependencyObject target)
        {
            return (bool)target.GetValue(DisplayRowNumberProperty);
        }

        public static void SetDisplayRowNumber(DependencyObject target, bool value)
        {
            target.SetValue(DisplayRowNumberProperty, value);
        }

        private static void OnDisplayRowNumberChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = target as DataGrid;
            if ((bool)e.NewValue)
            {
                EventHandler<DataGridRowEventArgs> loadedRowHandler = null;
                loadedRowHandler = (object sender, DataGridRowEventArgs ea) =>
                {
                    if (GetDisplayRowNumber(dataGrid) == false)
                    {
                        dataGrid.LoadingRow -= loadedRowHandler;
                        return;
                    }
                    ea.Row.Header = ea.Row.GetIndex() + 1;
                };
                dataGrid.LoadingRow += loadedRowHandler;

                ItemsChangedEventHandler itemsChangedHandler = null;
                itemsChangedHandler = (object sender, ItemsChangedEventArgs ea) =>
                {
                    if (GetDisplayRowNumber(dataGrid) == false)
                    {
                        dataGrid.ItemContainerGenerator.ItemsChanged -= itemsChangedHandler;
                        return;
                    }
                    GetVisualChildCollection<DataGridRow>(dataGrid).
                        ForEach(d => d.Header = d.GetIndex() + 1);
                };
                dataGrid.ItemContainerGenerator.ItemsChanged += itemsChangedHandler;
            }
        }

        #endregion // DisplayRowNumber

        #region Get Visuals

        private static List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

        #endregion // Get Visuals

        public static readonly DependencyProperty LastColumnFillProperty = DependencyProperty.RegisterAttached("LastColumnFill", typeof(bool), typeof(DataGridBehavior), new PropertyMetadata(default(bool), OnLastColumnFillChanged));

        public static void SetLastColumnFill(DataGrid element, bool value)
        {
            element.SetValue(LastColumnFillProperty, value);
        }

        public static bool GetLastColumnFill(DataGrid element)
        {
            return (bool)element.GetValue(LastColumnFillProperty);
        }

        private static void OnLastColumnFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid == null) return;

            dataGrid.Loaded -= OnDataGridLoaded;
            dataGrid.Loaded += OnDataGridLoaded;
        }

        private static void afterInvoke(DataGrid dataGrid)
        {
            bool nonMin = false;
            foreach (var col in dataGrid.Columns)
            {
                if (col.ActualWidth != col.MinWidth)
                {
                    nonMin = true;
                }
            }
            if (nonMin)
            {
                OnDataGridLoaded(dataGrid, null);
            }
        }

        public static void OnDataGridLoaded(object sender, RoutedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null) return;

            double totalWidth = 0;
            double dataGridActualWidth = dataGrid.ActualWidth - 2;
            var columns = dataGrid.Columns.Where(i => Visibility.Visible.Equals(i.Visibility)).ToList();
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                if (column.Visibility != Visibility.Visible)
                {
                    continue;
                }

                //if last column
                if (i == columns.Count - 1)
                {
                    if ((column.ActualWidth + totalWidth) < dataGridActualWidth)
                    {
                        column.Width = new DataGridLength(dataGridActualWidth - totalWidth);
                    }
                }
                else
                {
                    totalWidth += column.ActualWidth;
                }
            }
        }
    }
}
