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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport;

namespace VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport
{
    /// <summary>
    /// Interaction logic for NHPhuongAnNhapKhauItemDialog.xaml
    /// </summary>
    public partial class MSNTNHPhuongAnNhapKhauItemDialog : UserControl
    {
        public MSNTNHPhuongAnNhapKhauItemDialog()
        {
            InitializeComponent();
        }

        private void ExpandedDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (MSNTNHPhuongAnNhapKhauItemDialogViewModel)this.DataContext;
            if (vm != null)
            {
                vm.HangMuc_BeginningEditHanlder(e);
            }
        }
    }
}
