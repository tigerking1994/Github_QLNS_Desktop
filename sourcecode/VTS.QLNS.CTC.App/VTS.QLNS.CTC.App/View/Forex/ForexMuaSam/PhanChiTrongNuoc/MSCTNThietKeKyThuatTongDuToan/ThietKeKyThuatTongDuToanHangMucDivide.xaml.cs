using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNThietKeKyThuatTongDuToan
{
    /// <summary>
    /// Interaction logic for ThietKeKyThuatVaTongDuToanHangMucDivide.xaml
    /// </summary>
    public partial class ThietKeKyThuatTongDuToanHangMucDivide : Window
    {
        public ThietKeKyThuatTongDuToanHangMucDivide()
        {
            InitializeComponent();
            dgdTKTCHangMucPhanChia.BeginningEdit += dgdTKTCHangMucPhanChia_BeginningEdit;
        }

        private void dgdTKTCHangMucPhanChia_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdTKTCHangMucPhanChia.SelectedItem = e.Row.Item;

            var selected = (DuToanDetailModel)e.Row.Item;
            var curentColumn = e.Column;

            if (!selected.IsChecked && curentColumn.SortMemberPath.Equals("FGiaTriPhanChia"))
            {
                e.Cancel = true;
            }
        }
    }
}
