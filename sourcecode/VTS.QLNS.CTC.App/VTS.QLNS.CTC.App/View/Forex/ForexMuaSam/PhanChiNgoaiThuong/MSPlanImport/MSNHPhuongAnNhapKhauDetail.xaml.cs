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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPlanImport;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPlanImport
{
    /// <summary>
    /// Interaction logic for NHPhuongAnNhapKhauDetail.xaml
    /// </summary>
    public partial class MSNHPhuongAnNhapKhauDetail : Window
    {
        public MSNHPhuongAnNhapKhauDetail()
        {
            InitializeComponent();
        }

        private void dgdNHKeHoachLuaChonNhaThauChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (MSNHPhuongAnNhapKhauDetailViewModel)this.DataContext;
            if(viewModel != null)
            {
                viewModel.GoiThauChiPhi_BeginningEditHanlder(e);
            }
        }

        private void ExpandedDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (MSNHPhuongAnNhapKhauDetailViewModel)this.DataContext;
            if (vm != null)
            {
                vm.HangMuc_BeginningEditHanlder(e);
            }
        }
    }
}
