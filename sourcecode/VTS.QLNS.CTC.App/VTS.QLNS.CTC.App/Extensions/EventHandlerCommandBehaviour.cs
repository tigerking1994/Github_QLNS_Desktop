using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.Extensions
{
    public class EventHandlerCommandBehaviour
    {
    }

    public class SelectionChangedBehaviour
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(SelectionChangedBehaviour), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            Selector selector = (Selector)depObj;
            if (selector != null)
            {
                selector.SelectionChanged += new SelectionChangedEventHandler(SelectionChanged);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selector selector = (Selector)sender;
            if (selector != null)
            {
                ICommand command = selector.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(selector.SelectedItem);
                }
            }
        }
    }

    public class SelectionMouseDoubleClickBehaviour
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(SelectionMouseDoubleClickBehaviour), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            Selector selector = (Selector)depObj;
            if (selector != null)
            {
                selector.MouseDoubleClick += new MouseButtonEventHandler(MouseDoubleClick);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selector selector = (Selector)sender;
            if (selector != null)
            {
                ICommand command = selector.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(selector.SelectedItem);
                }
            }
        }
    }

    public class RowEditEndingBehaviour
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(RowEditEndingBehaviour), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            DataGrid dataGrid = (DataGrid)depObj;
            if (dataGrid != null)
            {
                dataGrid.RowEditEnding += new EventHandler<DataGridRowEditEndingEventArgs>(RowEditEnding);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            if (dataGrid != null)
            {
                ICommand command = dataGrid.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(dataGrid.SelectedItem);
                }
            }
        }
    }

    public class TextBoxKeyUpBehaviour
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(TextBoxKeyUpBehaviour), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            TextBox textboxControl = (TextBox)depObj;
            if (textboxControl != null)
            {
                textboxControl.KeyUp += new KeyEventHandler(KeyUp);
            }
        }

        private static void KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textboxControl = (TextBox)sender;
            if (textboxControl != null)
            {
                ICommand command = textboxControl.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(textboxControl.Text);
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }
    public class Behaviours
    {
        public static readonly DependencyProperty DoubleClickCommandProperty =
           DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(Behaviours),
                                               new PropertyMetadata(DoubleClick_PropertyChanged));

        public static void SetDoubleClickCommand(UIElement element, ICommand value)
        {
            element.SetValue(DoubleClickCommandProperty, value);
        }

        public static ICommand GetDoubleClickCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DoubleClickCommandProperty);
        }

        private static void DoubleClick_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DataGridRow row)) return;

            if (e.NewValue != null)
            {
                row.AddHandler(DataGridRow.MouseDoubleClickEvent, new RoutedEventHandler(DataGrid_MouseDoubleClick));
            }
            else
            {
                row.RemoveHandler(DataGridRow.MouseDoubleClickEvent, new RoutedEventHandler(DataGrid_MouseDoubleClick));
            }
        }

        private static void DataGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is DataGridRow row)) return;
            var cmd = GetDoubleClickCommand(row);
            if (cmd.CanExecute(row.Item))
                cmd.Execute(row.Item);
        }
    }

    public class CellDoubleClickBehaviours
    {
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(CellDoubleClickBehaviours),
                new PropertyMetadata(DoubleClick_PropertyChanged));

        public static void SetDoubleClickCommand(UIElement element, ICommand value)
        {
            element.SetValue(DoubleClickCommandProperty, value);
        }

        public static ICommand GetDoubleClickCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DoubleClickCommandProperty);
        }

        private static void DoubleClick_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DataGridCell cell)) return;

            if (e.NewValue != null)
            {
                cell.AddHandler(DataGridCell.MouseDoubleClickEvent, new RoutedEventHandler(DataGrid_MouseDoubleClick));
            }
            else
            {
                cell.RemoveHandler(DataGridCell.MouseDoubleClickEvent, new RoutedEventHandler(DataGrid_MouseDoubleClick));
            }
        }

        private static void DataGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is DataGridCell cell)) return;
            var cmd = GetDoubleClickCommand(cell);
            if (cmd.CanExecute(cell))
                cmd.Execute(cell);
        }
    }

    public class GridAutoGeneratingColumnBehaviour
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(GridAutoGeneratingColumnBehaviour), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            DataGrid selector = (DataGrid)depObj;
            if (selector != null)
            {
                selector.AutoGeneratingColumn += new EventHandler<DataGridAutoGeneratingColumnEventArgs>(OnAutoGeneratingColumn);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGrid selector = (DataGrid)sender;
            if (selector != null)
            {
                ICommand command = selector.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(e);
                }
            }
        }
    }

    public class SelectingItemAttachedProperty
    {
        public static readonly DependencyProperty SelectingItemProperty = DependencyProperty.RegisterAttached(
            "SelectingItem",
            typeof(object),
            typeof(SelectingItemAttachedProperty),
            new PropertyMetadata(default(object), OnSelectingItemChanged));

        public static object GetSelectingItem(DependencyObject target)
        {
            return (object)target.GetValue(SelectingItemProperty);
        }

        public static void SetSelectingItem(DependencyObject target, object value)
        {
            target.SetValue(SelectingItemProperty, value);
        }

        static void OnSelectingItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var grid = sender as DataGrid;
            if (grid == null || grid.SelectedItem == null)
                return;
            var selectedData = grid.SelectedItem as ModelBase;
            if (selectedData.Id == null || selectedData.Id.Equals(Guid.Empty))
            {
                grid.ScrollIntoView(grid.SelectedItem, grid.Columns[0]);
                /*grid.CurrentCell = new DataGridCellInfo(grid.SelectedItem, grid.Columns[0]);
                grid.BeginEdit();*/
            }
            /*else
            {
                grid.CurrentCell = new DataGridCellInfo(grid.SelectedItem, grid.Columns[0]);
                grid.CancelEdit();
            }*/
        }
    }

    public class SelectedCellChangedProperty
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(SelectedCellChangedProperty), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            DataGrid selector = (DataGrid)depObj;
            if (selector != null)
            {
                selector.CurrentCellChanged += new EventHandler<EventArgs>(OnSelectedCellChanged);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void OnSelectedCellChanged(object sender, EventArgs e)
        {
            DataGrid selector = (DataGrid)sender;
            if (selector == null || selector.CurrentCell == null || selector.CurrentCell.Column == null)
            {
                return;
            }
            var col = selector.CurrentCell.Column.SortMemberPath;
            if (selector != null)
            {
                ICommand command = selector.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(col);
                }
            }
        }
    }

    public class PopupBoxLoadedBehavior
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(PopupBoxLoadedBehavior), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            PopupBox selector = (PopupBox)depObj;
            if (selector != null)
            {
                selector.Loaded += new RoutedEventHandler(OnLoadedPopupBox);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void OnLoadedPopupBox(object sender, RoutedEventArgs e)
        {
            PopupBox selector = (PopupBox)sender;
            if (selector != null)
            {
                ICommand command = selector.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(sender);
                }
            }
        }
    }

    public class CloseWindowBehaviour
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(CloseWindowBehaviour), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            Window window = (Window)depObj;
            if (window != null)
            {
                window.Closing += new CancelEventHandler(CloseWindow);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void CloseWindow(object sender, CancelEventArgs e)
        {
            Window window = (Window)sender;
            if (window != null)
            {
                ICommand command = window.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(sender);
                }
            }
        }
    }

    public class ComboBoxLoadedBehavior
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(ComboBoxLoadedBehavior), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ComboBox selector = (ComboBox)depObj;
            if (selector != null)
            {
                selector.Loaded += new RoutedEventHandler(OnLoadedComboBox);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void OnLoadedComboBox(object sender, RoutedEventArgs e)
        {
            ComboBox selector = (ComboBox)sender;
            if (selector != null)
            {
                ICommand command = selector.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(sender);
                }
            }
        }
    }

    public class PasswordChangeBehavior
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(PasswordChangeBehavior), new PropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            PasswordBox selector = (PasswordBox)depObj;
            if (selector != null)
            {
                selector.PasswordChanged += new RoutedEventHandler(OnChangePassword);
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        private static void OnChangePassword(object sender, RoutedEventArgs e)
        {
            PasswordBox selector = (PasswordBox)sender;
            if (selector != null)
            {
                ICommand command = selector.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(sender);
                }
            }
        }
    }
}
