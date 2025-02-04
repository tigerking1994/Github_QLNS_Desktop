using Microsoft.Win32;
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

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.ChuTruongDauTu
{
    /// <summary>
    /// Interaction logic for ChuTruongDauTuDialog.xaml
    /// </summary>
    public partial class ChuTruongDauTuDialog : Window
    {
        public ChuTruongDauTuDialog()
        {
            InitializeComponent();
            dgdDataNguonVonDetail.BeginningEdit += dgdDataNguonVon_BeginningEdit;
            dgdDataHangMucDetail.BeginningEdit += dgdDataHangMucDetail_BeginningEdit;
        }

        private void dgdDataNguonVon_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataNguonVonDetail.SelectedItem = e.Row.Item;
        }

        private void dgdDataHangMucDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataHangMucDetail.SelectedItem = e.Row.Item;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
