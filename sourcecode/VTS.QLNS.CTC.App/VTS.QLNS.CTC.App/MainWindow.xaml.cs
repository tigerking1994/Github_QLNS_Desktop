using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.ViewModel;

namespace VTS.QLNS.CTC.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ControlzEx.Theming.ThemeManager.Current.ChangeTheme(this, "Light.Teal");
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar)
                    return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }
            MenuToggleButton.IsChecked = false;
        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MainScrollViewer.ScrollToHome();
        }

        private void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyTheme(DarkModeToggleButton.IsChecked == true);
        }

        private static void ModifyTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var vm = this.DataContext as MainWindowViewModel;
            if (!vm.IsLogout)
                e.Cancel = true;
        }
    }
}
