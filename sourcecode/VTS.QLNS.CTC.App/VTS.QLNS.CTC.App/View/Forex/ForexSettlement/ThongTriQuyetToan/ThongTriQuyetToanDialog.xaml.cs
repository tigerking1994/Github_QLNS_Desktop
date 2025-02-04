using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
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
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ThongTriQuyetToan
{
    /// <summary>
    /// Interaction logic for ThongTriQuyetToanDialog.xaml
    /// </summary>
    public partial class ThongTriQuyetToanDialog : Window
    {
        public ThongTriQuyetToanDialog()
        {
            InitializeComponent();
        }

        private void SelectLoaiThongTri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = SelectLoaiThongTri.SelectedValue == null ? 1 : (int)SelectLoaiThongTri.SelectedValue;
            if (value == 2)
            {
                USDDeNghi.Visibility = Visibility.Hidden;
                VNDDeNghi.Visibility = Visibility.Hidden;
                USDThuaNop.Visibility = Visibility.Visible;
                VNDThuaNop.Visibility = Visibility.Visible;
            }
            else
            {
                USDDeNghi.Visibility = Visibility.Visible;
                VNDDeNghi.Visibility = Visibility.Visible;
                USDThuaNop.Visibility = Visibility.Hidden;
                VNDThuaNop.Visibility = Visibility.Hidden;
            }
        }
    }
}
