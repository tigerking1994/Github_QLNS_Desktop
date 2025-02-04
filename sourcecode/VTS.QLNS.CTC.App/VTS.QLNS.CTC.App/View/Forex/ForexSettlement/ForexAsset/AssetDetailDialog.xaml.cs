using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ForexAsset
{
    /// <summary>
    /// Interaction logic for AssetDetailDialog.xaml
    /// </summary>
    public partial class AssetDetailDialog : Window
    {
        public AssetDetailDialog()
        {
            InitializeComponent();
        }
        private void dgdDataTaiSan_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataTaiSan.SelectedItem = e.Row.Item;
        }
    }
}
