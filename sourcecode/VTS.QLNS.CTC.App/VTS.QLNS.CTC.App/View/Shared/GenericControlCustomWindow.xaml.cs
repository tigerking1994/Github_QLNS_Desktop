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

namespace VTS.QLNS.CTC.App.View.Shared
{
    /// <summary>
    /// Interaction logic for GenericControlCustomWindow.xaml
    /// </summary>
    public partial class GenericControlCustomWindow : Window
    {
        public Action<object> SavedAction;
        public GenericControlCustomWindow()
        {
            InitializeComponent();
        }
    }
}
