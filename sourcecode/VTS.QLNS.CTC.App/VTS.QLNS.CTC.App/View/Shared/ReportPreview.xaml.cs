using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Libs.PDFViewer;
using VTS.QLNS.CTC.Libs.PDFViewer.Enum;

namespace VTS.QLNS.CTC.App.View.Shared
{
    /// <summary>
    /// Interaction logic for PdfViewer.xaml
    /// </summary>
    public partial class ReportPreview : Window
    {
        public ReportPreview()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            /*var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }*/
            MenuToggleButton.IsChecked = false;
        }
    }
}
