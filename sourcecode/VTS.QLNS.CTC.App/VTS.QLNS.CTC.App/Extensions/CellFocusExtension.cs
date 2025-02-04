using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace VTS.QLNS.CTC.App.Extensions
{
    public static class CellFocusExtension
    {
        public static readonly DependencyProperty ElementToFocusProperty = DependencyProperty.RegisterAttached("ElementToFocus", typeof(Control),
            typeof(CellFocusExtension), new UIPropertyMetadata(null, ElementToFocusPropertyChanged));

        public static Control GetElementToFocus(Button button)
        {
            return (Control)button.GetValue(ElementToFocusProperty);
        }

        public static void SetElementToFocus(Button button, Control value)
        {
            button.SetValue(ElementToFocusProperty, value);
        }

        public static void ElementToFocusPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.Click += (s, args) =>
                {
                    Control control = GetElementToFocus(button);
                    if (control != null)
                    {
                        control.Focus();
                    }
                };
            }
        }
    }
}
