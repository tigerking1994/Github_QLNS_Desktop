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
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonNamDuocDuyet;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlan;

namespace VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachVonNamDuocDuyet
{
    /// <summary>
    /// Interaction logic for KHVNamDuocDuyetDetail.xaml
    /// </summary>
    public partial class KHVNamDuocDuyetDetail : Window
    {
        public KHVNamDuocDuyetDetail()
        {
            InitializeComponent();
            dgdKHVNamDuocDuyetDetail.BeginningEdit += DgdData_BeginningEdit;
        }

        private void DgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (KHVNamDuocDuyetDetailViewModel)this.DataContext;
            dgdKHVNamDuocDuyetDetail.SelectedItem = e.Row.Item;

            if (vm != null)
            {
                var selected = (VdtKhvPhanBoVonDonViChiTietPheDuyetModel)e.Row.Item;

                if (selected != null && !selected.BActive)
                {
                    e.Cancel = true;
                }
            }
        }

        private void fChiTieuNganSach_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (KHVNamDuocDuyetDetailViewModel)this.DataContext;
            vm.GetInfoDuAn();
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
