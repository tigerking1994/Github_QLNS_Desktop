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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKForexContractInfo;

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKForexContractInfo
{
    /// <summary>
    /// Interaction logic for ForexContractInfoItems.xaml
    /// </summary>
    public partial class ForexContractInfoItems : UserControl
    {
        public ForexContractInfoItems()
        {
            InitializeComponent();
        }

        private void HopDongHangMuc_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (ForexContractInfoItemsViewModel)DataContext;
            if (vm != null)
            {
                vm.HangMuc_BeginningEditHanlder(e);
            }
        }
    }
}
