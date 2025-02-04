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
using VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProject;

namespace VTS.QLNS.CTC.App.View.Investment.Initialization.InitializationProject
{
    /// <summary>
    /// Interaction logic for InitializationProjectDetail.xaml
    /// </summary>
    public partial class InitializationProjectDetail : Window
    {
        public InitializationProjectDetail()
        {
            InitializeComponent();
        }

        private void Cell_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (InitializationProjectDetailViewModel)this.DataContext;
            vm.GetInfoDuAn();
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
