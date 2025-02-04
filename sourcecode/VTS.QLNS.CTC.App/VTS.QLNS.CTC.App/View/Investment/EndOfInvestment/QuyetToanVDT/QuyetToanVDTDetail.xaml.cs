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
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT;

namespace VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.QuyetToanVDT
{
    /// <summary>
    /// Interaction logic for QuyetToanVDTDetail.xaml
    /// </summary>
    public partial class QuyetToanVDTDetail : Window
    {
        public bool isCalled { get; set; }
        public QuyetToanVDTDetail()
        {
            InitializeComponent();
            dgdQuyetToanVDTDetailTongHop.BeginningEdit += DgdData_BeginningEdit;
            isCalled = false;
        }

        private void DgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (QuyetToanVDTDetailViewModel)this.DataContext;
            if (vm != null)
            {
                if(e.Row.Item != null)
                {
                    if(e.Row.Item is VdtQtBcquyetToanNienDoChiTiet1Model)
                    {
                        var selected = (VdtQtBcquyetToanNienDoChiTiet1Model)e.Row.Item;
                        if (selected.IsHangCha)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        var selected = (VdtQtBcQuyetToanNienDoPhanTichModel)e.Row.Item;
                        if (selected.IsHangCha)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void rbChungTuPhanTichChecked(object sender, RoutedEventArgs e)
        {
            if (!isCalled)
            {
                dgdQuyetToanVDTDetailPhanTich.ReloadData();
                isCalled = true;
            }
            dgdQuyetToanVDTDetailTongHop.Visibility = Visibility.Hidden;
            dgdQuyetToanVDTDetailPhanTich.Visibility = Visibility.Visible;
        }

        private void rbChungTuTongHopChecked(object sender, RoutedEventArgs e)
        {
            dgdQuyetToanVDTDetailTongHop.Visibility = Visibility.Visible;
            dgdQuyetToanVDTDetailPhanTich.Visibility = Visibility.Hidden;
        }


    }
}

