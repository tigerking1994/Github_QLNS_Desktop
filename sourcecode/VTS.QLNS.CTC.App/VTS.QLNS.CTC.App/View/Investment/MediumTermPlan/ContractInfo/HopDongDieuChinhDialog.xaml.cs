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
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ContractInfo;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.ContractInfo
{
    /// <summary>
    /// Interaction logic for HopDongDieuChinhDialog.xaml
    /// </summary>
    public partial class HopDongDieuChinhDialog : Window
    {
        public HopDongDieuChinhDialog()
        {
            InitializeComponent();
            dgdGoiThauHangMucDieuChinh.BeginningEdit += dgdPheDuyetDuAnDetail_BeginningEdit;
            dgdGoiThauNhaThauDiaLog.BeginningEdit += dgdGoiThauNhaThau_BeginEdit;
        }

        private void dgdPheDuyetDuAnDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdGoiThauHangMucDieuChinh.SelectedItem = e.Row.Item;
        }

        private void dgdGoiThauNhaThau_BeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (HopDongDieuChinhDialogViewModel)this.DataContext;
            dgdGoiThauNhaThauDiaLog.SelectedItem = e.Row.Item;
            if (vm != null && vm.IsViewOnly)
            {
                e.Cancel = true;
            }
        }
    }
}
