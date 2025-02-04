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
using VTS.QLNS.CTC.App.Model;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.DeNghiThanhToanChiPhi
{
    /// <summary>
    /// Interaction logic for DeNghiThanhToanChiPhiDetail.xaml
    /// </summary>
    public partial class DeNghiThanhToanChiPhiDetail : Window
    {
        public DeNghiThanhToanChiPhiDetail()
        {
            InitializeComponent();
            dgdCapPhatThanhToanDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdCapPhatThanhToanDetail.SelectedItem = e.Row.Item;
            if (!((VdtTtDeNghiThanhToanChiPhiChiTietModel)e.Row.Item).IsEditHangMuc)
            {
                e.Cancel = true;
            }
        }
    }
}
