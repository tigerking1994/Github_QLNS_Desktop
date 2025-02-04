using System.Windows;
using System.Windows.Controls;

namespace VTS.QLNS.CTC.App.View.Budget.Settlement.Explanation
{
    /// <summary>
    /// Interaction logic for DataInterpretation.xaml
    /// </summary>
    public partial class DataInterpretation : Window
    {
        public DataInterpretation()
        {
            InitializeComponent();
        }

        private void dgdData_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                scrollFooter.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        /*
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabController.SelectedIndex == 0) // 0 là index của tab đầu tiên
            {
                btnSaveAndUpdate.Visibility = Visibility.Collapsed; // ẩn button
            }
            else
            {
                btnSaveAndUpdate.Visibility = Visibility.Visible; // hiển thị button
            }
        }
        */
    }
}
