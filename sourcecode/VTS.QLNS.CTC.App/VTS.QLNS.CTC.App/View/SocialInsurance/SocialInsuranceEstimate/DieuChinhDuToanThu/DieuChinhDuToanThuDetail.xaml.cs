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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu
{
    /// <summary>
    /// Interaction logic for DieuChinhDuToanThuDetail.xaml
    /// </summary>
    public partial class DieuChinhDuToanThuDetail : Window
    {
        public DieuChinhDuToanThuDetail()
        {
            InitializeComponent();
            dgDieuChinhDuToanThuBHXH.BeginningEdit += dgdDIeuChinhDTT_BeginningEdit;
        }
        private void dgdDIeuChinhDTT_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgDieuChinhDuToanThuBHXH.SelectedItem = e.Row.Item;
            var vm = (DieuChinhDuToanThuDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BIsKhoa || !vm.SelectedItem.IsEditable || (!string.IsNullOrEmpty(vm.Model.STongHop))))
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
