using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;

namespace VTS.QLNS.CTC.App.View
{
    /// <summary>
    /// Interaction logic for ReleaseNote.xaml
    /// </summary>
    public partial class ReleaseNote : UserControl
    {
        public ReleaseNote()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ReleaseNoteViewModel viewModel)
            {
                UpdateRichTextBox(viewModel.TextLines);
                viewModel.TextLines.CollectionChanged += (s, e) => UpdateRichTextBox(viewModel.TextLines);
            }
        }



        private void UpdateRichTextBox(ObservableCollection<AppRelease> lines)
        {
            FlowDocument document = new FlowDocument();


            lines.ToList().OrderByDescending(x => x.Version).ToList().ForEach(x =>
            {
                Paragraph paragraph1 = new Paragraph();
                paragraph1.Inlines.Add(new Bold(new Run("PHIÊN BẢN ")));
                paragraph1.Inlines.Add(new Bold(new Run(x.Version)));
                document.Blocks.Add(paragraph1);
                x.Notes.SelectMany(x => x.NoteDetails).GroupBy(y => y.Module).ToList().ForEach(y =>
                {
                    Paragraph paragraph2 = new Paragraph();
                    paragraph2.Inlines.Add(new Italic(new Run("   Phân hệ ")));
                    paragraph2.Inlines.Add(new Bold(new Run(y.Key)));
                    document.Blocks.Add(paragraph2);
                    if (y.Any(x => x.Features.Count > 0))
                    {
                        Paragraph paragraph3 = new Paragraph();
                        StackPanel stackPannel = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                        };
                        PackIcon icon = new PackIcon
                        {
                            Kind = PackIconKind.FeatureHighlight,
                            Margin = new Thickness(10, 0, 0, 0)
                        };
                        TextBlock textBlock = new TextBlock
                        {
                            Text = " Tính năng"
                        };
                        stackPannel.Children.Add(icon);
                        stackPannel.Children.Add(textBlock);
                        paragraph3.Inlines.Add(stackPannel);
                        document.Blocks.Add(paragraph3);

                        y.SelectMany(x => x.Features).ToList().ForEach(x =>
                        {
                            document.Blocks.Add(new Paragraph(new Run($"     - {x}")));
                        });
                    }
                    if (y.Any(x => x.BugFixes.Count > 0))
                    {
                        Paragraph paragraph3 = new Paragraph();
                        StackPanel stackPannel = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                        };
                        PackIcon icon = new PackIcon
                        {
                            Kind = PackIconKind.BugCheck,
                            Margin = new Thickness(10, 0, 0, 0)
                        };
                        TextBlock textBlock = new TextBlock
                        {
                            Text = " Sửa lỗi"
                        };
                        stackPannel.Children.Add(icon);
                        stackPannel.Children.Add(textBlock);
                        paragraph3.Inlines.Add(stackPannel);
                        document.Blocks.Add(paragraph3);

                        y.SelectMany(x => x.BugFixes).ToList().ForEach(x =>
                        {
                            document.Blocks.Add(new Paragraph(new Run($"     - {x}")));
                        });
                    }
                    if (y.Any(x => x.Improvements.Count > 0))
                    {
                        Paragraph paragraph3 = new Paragraph();
                        StackPanel stackPannel = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                        };
                        PackIcon icon = new PackIcon
                        {
                            Kind = PackIconKind.TransferUp,
                            Margin = new Thickness(10, 0, 0, 0)
                        };
                        TextBlock textBlock = new TextBlock
                        {
                            Text = " Tối ưu hiệu năng"
                        };
                        stackPannel.Children.Add(icon);
                        stackPannel.Children.Add(textBlock);
                        paragraph3.Inlines.Add(stackPannel);
                        document.Blocks.Add(paragraph3);

                        y.SelectMany(x => x.Improvements).ToList().ForEach(x =>
                        {
                            document.Blocks.Add(new Paragraph(new Run($"     - {x}")));
                        });
                    }
                });
            });

            myRichTextBox.Document = document;
        }
    }
}
