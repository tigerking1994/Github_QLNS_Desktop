using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public static class ViewModelExtensions
    {
        public static Window Show(this ViewModelBase vm)
        {
            Window window = (Window)vm.Content;
            window.Show();
            return window;
        }

        public static Window ShowDialog(this ViewModelBase vm)
        {
            Window window = (Window)vm.Content;
            window.ShowDialog();
            return window;
        }

        public static void ShowDialogHost(this ViewModelBase vm, string dialogIdentifier = "RootDialog")
        {
            DialogHost.Show(vm.Content, dialogIdentifier);
        }
    }
}
