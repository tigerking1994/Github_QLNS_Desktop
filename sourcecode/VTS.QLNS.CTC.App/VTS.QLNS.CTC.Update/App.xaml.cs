using System;
using System.Windows;

namespace VTS.QLNS.CTC.Update
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string[] args = Environment.GetCommandLineArgs();
            MainWindow window = new MainWindow(args);
            window.Show();
        }
    }
}
