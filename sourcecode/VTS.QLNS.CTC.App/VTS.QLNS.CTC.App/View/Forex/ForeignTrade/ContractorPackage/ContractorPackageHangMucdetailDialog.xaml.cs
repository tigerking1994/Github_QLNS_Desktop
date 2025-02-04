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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForeignTrade.ContractorPackage;

namespace VTS.QLNS.CTC.App.View.Forex.ForeignTrade.ContractorPackage
{
    /// <summary>
    /// Interaction logic for ContractorPackageHangMucdetailDialog.xaml
    /// </summary>
    public partial class ContractorPackageHangMucdetailDialog : Window
    {
        public ContractorPackageHangMucdetailDialog()
        {
            InitializeComponent();
            dgdThongTinPhuLucHangMuc.BeginningEdit += dgData_BeginningEdit_HangMuc;
        }

        private void dgData_BeginningEdit_HangMuc(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdThongTinPhuLucHangMuc.SelectedItem = e.Row.Item;
            var vm = (ContractorPackageHangMucDetailDialogViewModel)this.DataContext;
            if (e.Column.SortMemberPath == GoiThauUSD.SortMemberPath)
            {
                if (vm != null && !vm.HangMucChiPhiSelected.IsSelected)
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauVND.SortMemberPath)
            {
                if (vm != null && !vm.HangMucChiPhiSelected.IsSelected)
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauEUR.SortMemberPath)
            {
                if (vm != null && !vm.HangMucChiPhiSelected.IsSelected)
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauNgoaiTeKhac.SortMemberPath)
            {
                if (vm != null && !vm.HangMucChiPhiSelected.IsSelected)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
