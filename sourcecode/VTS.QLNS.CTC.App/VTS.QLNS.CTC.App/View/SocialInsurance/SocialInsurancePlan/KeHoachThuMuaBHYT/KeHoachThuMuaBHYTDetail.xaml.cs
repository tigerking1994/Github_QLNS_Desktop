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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT
{
    /// <summary>
    /// Interaction logic for KeHoachThuMuaBHYTDetail.xaml
    /// </summary>
    public partial class KeHoachThuMuaBHYTDetail : Window
    {
        public KeHoachThuMuaBHYTDetail()
        {
            InitializeComponent();
            DgKhtmBhytDetail.BeginningEdit += dgdData_BeginningEdit;
        }
        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgKhtmBhytDetail.SelectedItem = e.Row.Item;
            var vm = (KeHoachThuMuaBHYTDetailViewModel)this.DataContext;
            if (vm != null && (vm.IsLock || !vm.SelectedItem.IsEditable || vm.IsAnotherUserCreate))
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
