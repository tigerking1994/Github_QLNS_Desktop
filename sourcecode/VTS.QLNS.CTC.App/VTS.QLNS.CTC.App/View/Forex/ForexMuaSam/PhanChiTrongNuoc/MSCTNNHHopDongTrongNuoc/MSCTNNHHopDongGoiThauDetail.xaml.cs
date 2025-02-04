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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNNHHopDongTrongNuoc;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNNHHopDongTrongNuoc
{
    /// <summary>
    /// Interaction logic for NHHopDongGoiThauDetail.xaml
    /// </summary>
    public partial class MSCTNNHHopDongGoiThauDetail : Window
    {
        public MSCTNNHHopDongGoiThauDetail()
        {
            InitializeComponent();
            //dgdHopDongChiPhi.BeginningEdit += dgData_BeginningEdit_ChiPhi;
        }

        private void dgData_BeginningEdit_ChiPhi(object sender, DataGridBeginningEditEventArgs e)
        {
            //dgdHopDongChiPhi.SelectedItem = e.Row.Item;
            //var vm = (MSCTNNHHopDongTrongNuocChiTietHangMucViewModel)this.DataContext;
            //if (e.Column.SortMemberPath == ChiPhiUsd.SortMemberPath)
            //{
            //    if (vm != null && vm.SelectedChiPhi.EditChiPhi)
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //if (e.Column.SortMemberPath == ChiPhiVnd.SortMemberPath)
            //{
            //    if (vm != null && vm.SelectedChiPhi.EditChiPhi)
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //if (e.Column.SortMemberPath == ChiPhiEur.SortMemberPath)
            //{
            //    if (vm != null && vm.SelectedChiPhi.EditChiPhi)
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //if (e.Column.SortMemberPath == ChiPhiKhac.SortMemberPath)
            //{
            //    if (vm != null && vm.SelectedChiPhi.EditChiPhi)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }
    }
}
