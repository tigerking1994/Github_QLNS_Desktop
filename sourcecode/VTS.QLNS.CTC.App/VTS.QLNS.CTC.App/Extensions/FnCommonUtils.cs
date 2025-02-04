using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Extensions
{
    public class FnCommonUtils
    {
        public static List<ComboboxItem> LoadMonths()
        {
            return Enumerable.Range(1, 12)
                .Select(item => new ComboboxItem
                {
                    DisplayItem = "Tháng " + item,
                    ValueItem = item.ToString(),
                    Type = "M"
                }).ToList();
        }

        public static List<ComboboxItem> LoadQuarters()
        {
            return Enumerable.Range(1, 4)
                .Select(item => new ComboboxItem
                {
                    DisplayItem = "Quý " + item,
                    ValueItem = item.ToString(),
                    Type = "Q"
                }).ToList();
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                if (!(child is T childType))
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    // If the child's name is set for search
                    if (child is FrameworkElement frameworkElement && childName.Equals(frameworkElement.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        // if the child's name is of the request name
                        foundChild = child as T;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = child as T;
                    break;
                }
            }

            return foundChild;
        }
    }
}
