using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.View.Budget.Settlement.Army
{
    /// <summary>
    /// Interaction logic for ArmyDetail.xaml
    /// </summary>
    public partial class ArmyDetail : Window
    {
        public ArmyDetail()
        {
            InitializeComponent();
            DgArmyDetailChange.BeginningEdit += dgdData_BeginningEdit;
        }

        private void dgdData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DgArmyDetailChange.SelectedItem = e.Row.Item;
            if (((ArmyVoucherDetailModel)e.Row.Item).BHangCha || TblLock.Text == "Mở" || TblIsEdit.Text.ToLower().Equals("false"))
            {
                e.Cancel = true;
            }
        }
    }
}
