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

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.ChuanBiDauTu.DACBDTInvestmentPolicy
{
    /// <summary>
    /// Interaction logic for ForexInvestmentPolicyDialog.xaml
    /// </summary>
    public partial class DACBDTInvestmentPolicyDialog : Window
    {
        public DACBDTInvestmentPolicyDialog()
        {
            InitializeComponent();
        }

        private void dgNhChuTruongDauTuNguonVon_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataGrid obj = (DataGrid)sender;
            obj.SelectedItem = e.Row.Item;
        }

        private void dgNhChuTruongDauTuHangMuc_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataGrid obj = (DataGrid)sender;
            obj.SelectedItem = e.Row.Item;
        }
    }
}
