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

namespace VTS.QLNS.CTC.App.View.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao
{
    /// <summary>
    /// Interaction logic for ForexDanhSachKhoiTaoDetail.xaml
    /// </summary>
    public partial class ForexDanhSachKhoiTaoDetail : Window
    {
        public ForexDanhSachKhoiTaoDetail()
        {
            InitializeComponent();
        }

        private void DgdNhKtKhoiTaoCapPhatDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var target = (DataGrid)sender;
            target.SelectedItem = e.Row.Item;
        }
    }
}
