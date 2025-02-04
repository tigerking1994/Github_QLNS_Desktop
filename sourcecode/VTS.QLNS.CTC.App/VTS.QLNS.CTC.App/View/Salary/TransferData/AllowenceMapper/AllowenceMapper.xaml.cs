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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.ViewModel.Salary.TransferData.AllowenceMapper;

namespace VTS.QLNS.CTC.App.View.Salary.TransferData.AllowenceMapper
{
    /// <summary>
    /// Interaction logic for AllowenceMapper.xaml
    /// </summary>
    public partial class AllowenceMapper : UserControl
    {
        public AllowenceMapper()
        {
            InitializeComponent();
            dgAllowenceMapper.BeginningEdit += dgData_BeginningEdit;
        }

        private void dgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgAllowenceMapper.SelectedItem = e.Row.Item;
        }
    }
}
