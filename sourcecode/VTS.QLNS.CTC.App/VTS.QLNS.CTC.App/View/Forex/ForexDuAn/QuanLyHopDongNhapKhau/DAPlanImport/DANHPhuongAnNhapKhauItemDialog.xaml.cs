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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport;

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport
{
    /// <summary>
    /// Interaction logic for NHPhuongAnNhapKhauItemDialog.xaml
    /// </summary>
    public partial class DANHPhuongAnNhapKhauItemDialog : UserControl
    {
        public DANHPhuongAnNhapKhauItemDialog()
        {
            InitializeComponent();
        }

        private void ExpandedDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (DANHPhuongAnNhapKhauItemDialogViewModel)this.DataContext;
            if (vm != null)
            {
                vm.HangMuc_BeginningEditHanlder(e);
            }
        }
    }
}
