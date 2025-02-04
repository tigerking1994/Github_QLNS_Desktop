using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace VTS.QLNS.CTC.App.Extensions
{
    public static class XamlDisplayExtensions
    {
        public static readonly DependencyProperty ButtonDockProperty = DependencyProperty.RegisterAttached(
            "ButtonDock", typeof(Dock), typeof(XamlDisplayExtensions), new PropertyMetadata(default(Dock)));

        public static void SetButtonDock(DependencyObject element, Dock value)
        {
            element.SetValue(ButtonDockProperty, value);
        }

        public static Dock GetButtonDock(DependencyObject element)
        {
            return (Dock)element.GetValue(ButtonDockProperty);
        }
    }
}
