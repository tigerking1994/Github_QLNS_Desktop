using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT;

namespace VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.QuyetToanVDT
{
    /// <summary>
    /// Interaction logic for QuyetToanVDTCQTCDetail.xaml
    /// </summary>
    public partial class QuyetToanVDTVonUngDetail : Window
    {
        public QuyetToanVDTVonUngDetail()
        {
            InitializeComponent();
            dgdQuyetToanVDTDetail.BeginningEdit += dgdQuyetToanVDTDetail_BeginEdit;
        }

        private void dgdQuyetToanVDTDetail_BeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var curentColumn = e.Column;
            var vm = (QuyetToanVDTVonUngDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.IIDNguonVonID ?? 0) != 1)
            {
                var selected = (BcquyetToanNienDoVonUngChiTietModel)e.Row.Item;
                if (selected.IsHangCha)
                {
                    e.Cancel = true;
                }
                if (curentColumn.SortMemberPath.Equals("FThuHoiVonNamNay"))
                    e.Cancel = true;
            }
        }
    }
}
