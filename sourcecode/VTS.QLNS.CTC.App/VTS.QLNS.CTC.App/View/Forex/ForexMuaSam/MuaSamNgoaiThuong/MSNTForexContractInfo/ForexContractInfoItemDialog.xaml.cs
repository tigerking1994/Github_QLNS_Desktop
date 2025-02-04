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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo
{
    public partial class ForexContractInfoItemDialog : UserControl
    {
        public ForexContractInfoItemDialog()
        {
            InitializeComponent();
            dgdDataHopDongDetail.BeginningEdit += dgdData_BeginningEdit;
            dgdDataHopDongDetail.BeginningEdit += ExpandedDataGrid_BeginningEdit;
        }

        private void ExpandedDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataHopDongDetail.SelectedItem = e.Row.Item;
            var vm = (ForexContractInfoItemDialogViewModel)this.DataContext;
            if (vm != null)
            {
                vm.HangMuc_BeginningEditHanlder(e);
            }
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataHopDongDetail.SelectedItem = e.Row.Item;
        }
    }
}
