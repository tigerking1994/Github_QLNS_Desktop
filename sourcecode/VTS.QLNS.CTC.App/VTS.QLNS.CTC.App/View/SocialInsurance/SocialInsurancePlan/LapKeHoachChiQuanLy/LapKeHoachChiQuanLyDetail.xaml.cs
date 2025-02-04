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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy
{
    /// <summary>
    /// Interaction logic for LapKeHoachChiQuanLyDetail.xaml
    /// </summary>
    public partial class LapKeHoachChiQuanLyDetail : Window
    {
        public LapKeHoachChiQuanLyDetail()
        {
            InitializeComponent();
            DgKeHoachChiQuanLyDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgKeHoachChiQuanLyDetail.SelectedItem = e.Row.Item;
            var vm = (LapKeHoachChiQuanLyDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable || vm.IsAnotherUserCreate || vm.IsAggregate))
            {
                e.Cancel = true;
            }
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
