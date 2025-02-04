using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau
{
    /// <summary>
    /// Interaction logic for HopDongTrongNuocKhongGoiThauDialog.xaml
    /// </summary>
    public partial class HopDongTrongNuocKhongGoiThauDialog : Window
    {
        public HopDongTrongNuocKhongGoiThauDialog()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dgdDanhMucKeHoachDatHangDanhMuc_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (MSTNHopDongTrongNuocKhongGoiThauDialogViewModel)this.DataContext;
            if (vm != null)
            {
                vm.HangMuc_BeginningEditHanlder(e);
            }
        }
    }
}
