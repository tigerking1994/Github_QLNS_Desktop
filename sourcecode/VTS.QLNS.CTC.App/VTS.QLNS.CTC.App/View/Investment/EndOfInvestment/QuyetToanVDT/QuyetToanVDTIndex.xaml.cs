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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT;

namespace VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.QuyetToanVDT
{
    /// <summary>
    /// Interaction logic for QuyetToanVDTIndex.xaml
    /// </summary>
    public partial class QuyetToanVDTIndex : UserControl
    {
        public QuyetToanVDTIndex()
        {
            InitializeComponent();
            dgdQuyetToanVDT.BeginningEdit += DgdData_BeginningEdit;
            dgdQuyetToanVDTTongHop.BeginningEdit += DgdDataTongHop_BeginningEdit;
        }

        private void DgdDataTongHop_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (QuyetToanVDTIndexViewModel)this.DataContext;
            dgdQuyetToanVDTTongHop.SelectedItem = e.Row.Item;
        }

        private void DgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var vm = (QuyetToanVDTIndexViewModel)this.DataContext;
            dgdQuyetToanVDT.SelectedItem = e.Row.Item;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
