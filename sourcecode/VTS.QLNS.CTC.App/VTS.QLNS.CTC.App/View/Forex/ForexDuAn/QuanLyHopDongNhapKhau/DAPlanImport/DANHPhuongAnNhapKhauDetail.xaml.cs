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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPlanImport;

namespace VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport
{
    /// <summary>
    /// Interaction logic for NHPhuongAnNhapKhauDetail.xaml
    /// </summary>
    public partial class DANHPhuongAnNhapKhauDetail : Window
    {
        public DANHPhuongAnNhapKhauDetail()
        {
            InitializeComponent();
        }

        private void dgdNHKeHoachLuaChonNhaThauChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (DANHPhuongAnNhapKhauDetailViewModel)this.DataContext;
            if(viewModel != null)
            {
                viewModel.GoiThauChiPhi_BeginningEditHanlder(e);
            }
        }

        private void ExpandedDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (DANHPhuongAnNhapKhauDetailViewModel)this.DataContext;
            if (vm != null)
            {
                vm.HangMuc_BeginningEditHanlder(e);
            }
        }

        private void NguonVonDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (DANHPhuongAnNhapKhauDetailViewModel)this.DataContext;
            //viewModel.NguonVonDataGrid_SelectionChanged();
        }
    }
}
