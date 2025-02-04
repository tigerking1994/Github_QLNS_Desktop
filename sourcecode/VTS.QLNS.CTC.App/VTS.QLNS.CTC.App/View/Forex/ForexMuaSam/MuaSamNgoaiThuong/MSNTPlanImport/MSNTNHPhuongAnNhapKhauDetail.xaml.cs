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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport
{
    /// <summary>
    /// Interaction logic for NHPhuongAnNhapKhauDetail.xaml
    /// </summary>
    public partial class MSNTNHPhuongAnNhapKhauDetail : Window
    {
        public MSNTNHPhuongAnNhapKhauDetail()
        {
            InitializeComponent();
        }

        private void dgdNHKeHoachLuaChonNhaThauChiPhiDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (MSNTNHPhuongAnNhapKhauDetailViewModel)this.DataContext;
            if(viewModel != null)
            {
                viewModel.GoiThauChiPhi_BeginningEditHanlder(e);
            }
        }
    }
}
