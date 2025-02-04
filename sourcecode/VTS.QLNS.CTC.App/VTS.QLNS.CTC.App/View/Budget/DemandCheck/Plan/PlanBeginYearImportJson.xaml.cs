using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan
{
    /// <summary>
    /// Interaction logic for PlanBeginYearImportJson.xaml
    /// </summary>
    public partial class PlanBeginYearImportJson : Window
    {
        public PlanBeginYearImportJson()
        {
            InitializeComponent();
        }

        private void dgNsDtDauNamChungTuImportJson_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dgNsDtDauNamChungTuImportJson.SelectedItem = e.Row.Item;
        }
    }
}
