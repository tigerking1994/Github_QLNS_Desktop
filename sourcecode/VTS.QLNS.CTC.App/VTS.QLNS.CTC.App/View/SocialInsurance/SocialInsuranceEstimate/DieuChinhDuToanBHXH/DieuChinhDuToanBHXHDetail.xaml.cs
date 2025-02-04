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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH
{
    /// <summary>
    /// Interaction logic for DieuChinhDuToanBHXHDetail.xaml
    /// </summary>
    public partial class DieuChinhDuToanBHXHDetail : Window
    {
        public DieuChinhDuToanBHXHDetail()
        {
            InitializeComponent();
            dgDieuChinhDuToan.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgDieuChinhDuToan.SelectedItem = e.Row.Item;
            var vm = (DieuChinhDuToanBHXHDetailViewModel)this.DataContext;
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
