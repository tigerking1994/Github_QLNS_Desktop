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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VTS.QLNS.CTC.App.View.Salary.Settlement.SalaryConfiguration;

namespace VTS.QLNS.CTC.App.View.Salary.Settlement.SocialInsuranceConfiguration
{
    /// <summary>
    /// Interaction logic for BHXHConfiguration.xaml
    /// </summary>
    public partial class BHXHConfiguration : UserControl
    {
        public BHXHConfiguration()
        {
            InitializeComponent();
            DgBHXHConfiguration.BeginningEdit += DgBHXHConfiguration_BeginningEdit;
        }

        private void DgBHXHConfiguration_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgBHXHConfiguration.SelectedItem = e.Row.Item;
        }
    }
}
