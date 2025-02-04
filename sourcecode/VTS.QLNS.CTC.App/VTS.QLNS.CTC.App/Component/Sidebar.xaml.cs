using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Component
{
    /// <summary>
    /// Interaction logic for Sidebar.xaml
    /// </summary>
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Visible;
            btnOpenMenu.Visibility = Visibility.Collapsed;
            foreach (var item in ListBox.Items.Groups)
            {
                GroupItem container = ListBox.ItemContainerGenerator.ContainerFromItem(item) as GroupItem;
                IEnumerable<TextBlock> groupNameList = FindVisualChildren<TextBlock>(container);
                if (groupNameList != null)
                {
                    groupNameList.ForAll(x => x.TextWrapping = TextWrapping.Wrap);
                }
                var expanders = FindVisualChildren<Expander>(container);
                foreach (Expander expander in expanders)
                {
                    var toggleButtons = FindVisualChildren<ToggleButton>(expander);
                    toggleButtons.ForAll(y => y.Visibility = Visibility.Visible);
                }
            }
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Collapsed;
            btnOpenMenu.Visibility = Visibility.Visible;
            foreach (var item in ListBox.Items.Groups)
            {
                GroupItem container = ListBox.ItemContainerGenerator.ContainerFromItem(item) as GroupItem;
                IEnumerable<TextBlock> groupNameList = FindVisualChildren<TextBlock>(container);
                if (groupNameList != null)
                {
                    groupNameList.ForAll(x => x.TextWrapping = TextWrapping.NoWrap);
                }
                var expanders = FindVisualChildren<Expander>(container);
                foreach (Expander expander in expanders)
                {
                    var toggleButtons = FindVisualChildren<ToggleButton>(expander);
                    toggleButtons.ForAll(y => y.Visibility = Visibility.Collapsed);
                }
            }
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
