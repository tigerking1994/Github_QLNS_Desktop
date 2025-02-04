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
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonUngDeXuat;

namespace VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachVonUngDeXuat
{
    /// <summary>
    /// Interaction logic for KeHoachVonUngDeXuatDetail.xaml
    /// </summary>
    public partial class KeHoachVonUngDeXuatDetail : Window
    {
        public KeHoachVonUngDeXuatDetail()
        {
            InitializeComponent();
            dgdKeHoachVonUngDeXuatDetail.BeginningEdit += DgdData_BeginningEdit;
        }

        private void DgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (KeHoachVonUngDeXuatDetailViewModel)this.DataContext;
            dgdKeHoachVonUngDeXuatDetail.SelectedItem = e.Row.Item;
        }

        private void fChiTieuNganSach_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (KeHoachVonUngDeXuatDetailViewModel)this.DataContext;
            vm.CheckMucLucNganSach();
        }
                        
    }
}
