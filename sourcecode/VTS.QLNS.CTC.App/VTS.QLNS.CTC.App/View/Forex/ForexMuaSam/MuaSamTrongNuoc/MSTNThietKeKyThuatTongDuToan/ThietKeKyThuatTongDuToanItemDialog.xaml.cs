using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNThietKeKyThuatTongDuToan
{ 
    /// <summary>
    /// Interaction logic for ThietKeKyThuatVaTongDuToanHangMucDialog.xaml
    /// </summary>
    public partial class ThietKeKyThuatTongDuToanItemDialog : Window
    {
        public ThietKeKyThuatTongDuToanItemDialog()
        {
            InitializeComponent();
            dgdDataDuToanDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataDuToanDetail.SelectedItem = e.Row.Item;
            //var vm = (ThietKeKyThuatTongDuToanItemDialogViewModel)this.DataContext;
            //var selected = (NhDaDuToanHangMucModel)e.Row.Item;
            //var curentColumn = e.Column;

            //if (!vm.IsNotViewDetail)
            //{
            //    e.Cancel = true;
            //} 
        }
    }
}
