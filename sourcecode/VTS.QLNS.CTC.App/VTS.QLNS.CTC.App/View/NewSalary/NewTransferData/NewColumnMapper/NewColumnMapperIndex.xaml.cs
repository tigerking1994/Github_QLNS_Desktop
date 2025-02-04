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

namespace VTS.QLNS.CTC.App.View.NewSalary.NewTransferData.NewColumnMapper
{
    /// <summary>
    /// Interaction logic for ColumnMapperIndex.xaml
    /// </summary>
    public partial class NewColumnMapperIndex : UserControl
    {
        public NewColumnMapperIndex()
        {
            InitializeComponent();
            dgColumnMapper.BeginningEdit += dgData_BeginningEdit;
        }

        private void dgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgColumnMapper.SelectedItem = e.Row.Item;
        }
    }
}
