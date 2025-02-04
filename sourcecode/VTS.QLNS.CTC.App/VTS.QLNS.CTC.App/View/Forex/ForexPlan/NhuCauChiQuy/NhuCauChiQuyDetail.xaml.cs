using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy;

namespace VTS.QLNS.CTC.App.View.Forex.ForexPlan.NhuCauChiQuy
{
    /// <summary>
    /// Interaction logic for NhuCauChiQuyDetail.xaml
    /// </summary>
    public partial class NhuCauChiQuyDetail : Window
    {
        public NhuCauChiQuyDetail()
        {
            InitializeComponent();
            DgNhNhuCauChiQuyDetail.BeginningEdit += DgNhNhuCauChiQuyDetail_BeginningEdit;

        }

        private void DgNhNhuCauChiQuyDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var viewModel = (NhuCauChiQuyDetailViewModel)this.DataContext;
            if (viewModel != null)
            {
                viewModel.NhuCauChiQuyChiTiet_BeginningEditHanlder(e);
            }
        }
    }
    
}
