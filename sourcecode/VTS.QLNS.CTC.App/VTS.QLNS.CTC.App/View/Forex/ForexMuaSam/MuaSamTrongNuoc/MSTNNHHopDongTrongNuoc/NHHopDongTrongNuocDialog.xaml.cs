using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNNHHopDongTrongNuoc;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNNHHopDongTrongNuoc
{
    /// <summary>
    /// Interaction logic for NHHopDongTrongNuocDialog.xaml
    /// </summary>
    public partial class NHHopDongTrongNuocDialog : Window
    {
        public NHHopDongTrongNuocDialog()
        {
            InitializeComponent();
            //dgdThongTinGoiThau.BeginningEdit += dgData_BeginningEdit_GoiThau;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dgData_BeginningEdit_GoiThau(object sender, DataGridBeginningEditEventArgs e)
        {
            //dgdThongTinGoiThau.SelectedItem = e.Row.Item;
            //var vm = (NHHopDongTrongNuocDialogViewModel)this.DataContext;
            //if (e.Column.SortMemberPath == TrungThauUsd.SortMemberPath)
            //{
            //    if (vm != null && vm.SelectedGoiThau.EditChiPhi)
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //if (e.Column.SortMemberPath == TrungThauKhac.SortMemberPath)
            //{
            //    if (vm != null && vm.SelectedGoiThau.EditChiPhi)
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //if (e.Column.SortMemberPath == TrungThauVnd.SortMemberPath)
            //{
            //    if (vm != null && vm.SelectedGoiThau.EditChiPhi)
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //if (e.Column.SortMemberPath == TrungThauEur.SortMemberPath)
            //{
            //    if (vm != null && vm.SelectedGoiThau.EditChiPhi)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }
    }
}
