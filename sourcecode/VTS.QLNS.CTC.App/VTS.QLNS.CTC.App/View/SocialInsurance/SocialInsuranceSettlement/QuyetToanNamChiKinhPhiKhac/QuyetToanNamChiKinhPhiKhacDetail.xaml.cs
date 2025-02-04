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
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac;

namespace VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac
{
    /// <summary>
    /// Interaction logic for QuyetToanNamChiKinhPhiKhacDetail.xaml
    /// </summary>
    public partial class QuyetToanNamChiKinhPhiKhacDetail : Window
    {
        public QuyetToanNamChiKinhPhiKhacDetail()
        {
            InitializeComponent();
            dgdDataQuyetToanChiKPKDetail.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataQuyetToanChiKPKDetail.SelectedItem = e.Row.Item;
            var vm = (QuyetToanNamChiKinhPhiKhacDetailViewModel)this.DataContext;
            if (vm != null && (vm.Model.BIsKhoa || !vm.SelectedItem.IsEditable))
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
