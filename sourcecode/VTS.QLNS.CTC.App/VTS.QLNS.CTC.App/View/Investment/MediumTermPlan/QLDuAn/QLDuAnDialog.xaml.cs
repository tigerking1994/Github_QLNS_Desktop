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
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.QLDuAn
{
    /// <summary>
    /// Interaction logic for QLDuAnDialog.xaml
    /// </summary>
    public partial class QLDuAnDialog : Window
    {
        public QLDuAnDialog()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //if (openFileDialog.ShowDialog())
            //    txtEditor.Text = openFileDialog.FileName;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dgdDataHangMucProjectDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgdDataHangMucProjectDetail.SelectedItem = e.Row.Item;
        }
    }
}
