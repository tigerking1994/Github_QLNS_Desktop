using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT
{
    /// <summary>
    /// Interaction logic for AllocationDetail.xaml
    /// </summary>
    public partial class CapPhatTamUngKCBBHYTDetail : Window
    {
        public CapPhatTamUngKCBBHYTDetail()
        {
            InitializeComponent();
            dgdDataAllocationDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataAllocationDetail.SelectedItem = e.Row.Item;
            var vm = (CapPhatTamUngKCBBHYTDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BIsKhoa || !vm.SelectedItem.IsEditable || !vm.IsEditByRole || vm.Model.BIsTongHop || !string.IsNullOrEmpty(vm.Model.SDSSoChungTuTongHop)))
            {
                e.Cancel = true;
            }
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //if (e.HorizontalChange != 0.0f)
            //{
            //    scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            //}
        }
    }
}
