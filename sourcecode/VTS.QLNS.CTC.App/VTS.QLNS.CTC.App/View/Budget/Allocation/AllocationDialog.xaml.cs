using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
namespace VTS.QLNS.CTC.App.View.Budget.Allocation
{
    /// <summary>
    /// Interaction logic for AddAllocation.xaml
    /// </summary>
    public partial class AllocationDialog : UserControl
    {
        public AllocationDialog()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            bool bFlag = false;
            Regex regex = new Regex(@"[+-]?[1-9]+[.]?[0-9]*([e][+-]?[0-9]+)?");
            if (!string.IsNullOrEmpty(e.Text) && ( regex.IsMatch(SoCapPhat.Text + e.Text) == false || (regex.IsMatch(SoCapPhat.Text + e.Text) && float.Parse(SoCapPhat.Text + e.Text) > 100)))
            {
                bFlag = true;
            }
           
            e.Handled = bFlag;
        }
    }
}