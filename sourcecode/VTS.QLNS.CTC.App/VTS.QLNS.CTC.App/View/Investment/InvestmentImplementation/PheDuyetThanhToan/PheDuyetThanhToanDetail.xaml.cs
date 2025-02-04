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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PheDuyetThanhToan
{
    /// <summary>
    /// Interaction logic for PheDuyetThanhToanDetail.xaml
    /// </summary>
    public partial class PheDuyetThanhToanDetail : Window
    {
        public PheDuyetThanhToanDetail()
        {
            InitializeComponent();
            dgdPheDuyetThanhToanDetail.BeginningEdit += DgdData_BeginningEdit;
        }

        private void DgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (PheDuyetThanhToanDetailViewModel)this.DataContext;
            dgdPheDuyetThanhToanDetail.SelectedItem = e.Row.Item;
            if (vm != null)
            {
                var selected = (PheDuyetThanhToanChiTietModel)e.Row.Item;
                var curentColumn = e.Column;
                if (selected.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_NAY || selected.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC)
                {
                    if (curentColumn.SortMemberPath.Equals("cbxLns") || curentColumn.SortMemberPath.Equals("cbxL") || curentColumn.SortMemberPath.Equals("cbxK")
                        || curentColumn.SortMemberPath.Equals("cbxM") || curentColumn.SortMemberPath.Equals("cbxTM") || curentColumn.SortMemberPath.Equals("cbxTTM")
                        || curentColumn.SortMemberPath.Equals("cbxNG"))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void fGiaTri_GotFocus(object sender, RoutedEventArgs e)
        {
            var vm = (PheDuyetThanhToanDetailViewModel)this.DataContext;
            vm.CheckRequireMlns();
        }
    }
}
