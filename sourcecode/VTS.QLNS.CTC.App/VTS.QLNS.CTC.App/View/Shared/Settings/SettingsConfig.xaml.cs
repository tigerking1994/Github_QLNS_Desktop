using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.Shared.Settings
{
    /// <summary>
    /// Interaction logic for SettingsConfig.xaml
    /// </summary>
    public partial class SettingsConfig : Window
    {
        public SettingsConfig()
        {
            InitializeComponent();
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Visible;
            btnOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Collapsed;
            btnOpenMenu.Visibility = Visibility.Visible;
        }
    }
}
