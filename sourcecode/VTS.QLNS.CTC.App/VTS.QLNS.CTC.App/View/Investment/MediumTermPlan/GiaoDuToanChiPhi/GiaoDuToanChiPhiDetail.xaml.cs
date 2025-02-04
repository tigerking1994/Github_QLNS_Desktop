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

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GiaoDuToanChiPhi
{
    /// <summary>
    /// Interaction logic for GiaoDuToanChiPhiDetail.xaml
    /// </summary>
    public partial class GiaoDuToanChiPhiDetail : Window
    {
        public GiaoDuToanChiPhiDetail()
        {
            InitializeComponent();
        }

        private void dgGiaoDuToanChiPhi_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataGrid obj = (DataGrid)sender;
            obj.SelectedItem = e.Row.Item;
        }
    }
}
