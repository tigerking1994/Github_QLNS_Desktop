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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo;

namespace VTS.QLNS.CTC.App.View.Forex.ForexSettlement.QuyetToanNienDo
{
    /// <summary>
    /// Interaction logic for QuyetToanNienDoDetail.xaml
    /// </summary>
    public partial class QuyetToanNienDoDetail : Window
    {
        public QuyetToanNienDoDetail()
        {
            InitializeComponent();
            DgdNhQuyetToanNienDoDetail.BeginningEdit += DgdNhQuyetToanNienDoDetail_BeginningEdit;
        }

        private void DgdNhQuyetToanNienDoDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var target = (DataGrid)sender;
            target.SelectedItem = e.Row.Item;
            var vm = (QuyetToanNienDoDetailViewModel)this.DataContext;
            if (vm != null)
            {
                var selected = (NhQtQuyetToanNienDoChiTietModel)e.Row.Item;
                if (selected.SLevel != "4")
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
