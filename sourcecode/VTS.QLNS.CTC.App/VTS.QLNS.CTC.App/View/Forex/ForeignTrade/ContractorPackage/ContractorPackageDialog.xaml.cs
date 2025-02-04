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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForeignTrade.ContractorPackage;

namespace VTS.QLNS.CTC.App.View.Forex.ForeignTrade.ContractorPackage
{
    /// <summary>
    /// Interaction logic for ContractorPackageDialog.xaml
    /// </summary>
    public partial class ContractorPackageDialog : Window
    {
        public ContractorPackageDialog()
        {
            InitializeComponent();
            dgThongTinNGuonVon.BeginningEdit += dgData_BeginningEdit_NguonVon;
            dgdThongTinPhuLucChiPhi.BeginningEdit += dgData_BeginningEdit_ChiPhi;
        }

        private void dgData_BeginningEdit_NguonVon(object sender, DataGridBeginningEditEventArgs e)
        {
            dgThongTinNGuonVon.SelectedItem = e.Row.Item;
            var vm = (ContractorPackageDialogViewModel)this.DataContext;
            if (e.Column.SortMemberPath == GoiThauUSD.SortMemberPath)
            {
                if (vm != null && !vm.SelectedThongTinNguon.IsSelected)
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauVND.SortMemberPath)
            {
                if (vm != null && !vm.SelectedThongTinNguon.IsSelected)
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauEUR.SortMemberPath)
            {
                if (vm != null && !vm.SelectedThongTinNguon.IsSelected)
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauNgoaiTeKhac.SortMemberPath)
            {
                if (vm != null && !vm.SelectedThongTinNguon.IsSelected)
                {
                    e.Cancel = true;
                }
            }
        }

        private void dgData_BeginningEdit_ChiPhi(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdThongTinPhuLucChiPhi.SelectedItem = e.Row.Item;
            var vm = (ContractorPackageDialogViewModel)this.DataContext;
            if (e.Column.SortMemberPath == GoiThauChiPhiUSD.SortMemberPath)
            {
                if (vm != null && (vm.SelectedThongTinChiPhi.IsSelected == false || vm.SelectedThongTinChiPhi.IsEditHangMuc))
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauChiPhiVND.SortMemberPath)
            {
                if (vm != null && (vm.SelectedThongTinChiPhi.IsSelected == false || vm.SelectedThongTinChiPhi.IsEditHangMuc))
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauChiPhiEUR.SortMemberPath)
            {
                if (vm != null && (vm.SelectedThongTinChiPhi.IsSelected == false || vm.SelectedThongTinChiPhi.IsEditHangMuc))
                {
                    e.Cancel = true;
                }
            }
            if (e.Column.SortMemberPath == GoiThauChiPhiNgoaiTeKhac.SortMemberPath)
            {
                if (vm != null && (vm.SelectedThongTinChiPhi.IsSelected == false || vm.SelectedThongTinChiPhi.IsEditHangMuc))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
