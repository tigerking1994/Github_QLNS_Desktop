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
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.User;

namespace VTS.QLNS.CTC.App.View.SystemAdmin.UserManagement.User
{
    /// <summary>
    /// Interaction logic for UpdateUserDialog.xaml
    /// </summary>
    public partial class UserDialog : UserControl
    {
        public UserDialog()
        {
            InitializeComponent();
        }
        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as UserDialogViewModel).Model.Password = (sender as PasswordBox).SecurePassword;
        }
    }
}
