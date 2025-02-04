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
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.KHLuaChonNhaThau;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.KHLuaChonNhaThau
{
    /// <summary>
    /// Interaction logic for KHLuaChonNhaThauDiaLog.xaml
    /// </summary>
    public partial class KHLuaChonNhaThauDiaLog : Window
    {
        public KHLuaChonNhaThauDiaLog()
        {
            InitializeComponent();
        }

        private void dgdKHLuaChonNhaThauDiaLog_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdKHLuaChonNhaThauDiaLog.SelectedItem = e.Row.Item;
        }

        private void dgdKHLCNTDuToanDialog_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdKHLCNTDuToanDialog.SelectedItem = e.Row.Item;
        }
    }
}
