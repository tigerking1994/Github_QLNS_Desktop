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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManagerApproved;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanManagerApproved
{
    /// <summary>
    /// Interaction logic for ProjectInPlanManagerApprovedDiaLog.xaml
    /// </summary>
    public partial class ProjectInPlanManagerApprovedDiaLog : Window
    {
        public ProjectInPlanManagerApprovedDiaLog()
        {
            InitializeComponent();
        }

        private void dgdDataPlanManagerDetailDeXuat_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollHeader1.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
