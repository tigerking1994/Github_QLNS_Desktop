using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.Converters
{
    public class TextLinesToFlowDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FlowDocument document = new FlowDocument();

            if (value is List<AppRelease> lines)
            {
                lines.OrderByDescending(x => x.Version).ToList().ForEach(x =>
                {
                    Paragraph paragraph1 = new Paragraph();
                    paragraph1.Inlines.Add(new Run("Phiên bản "));
                    paragraph1.Inlines.Add(new Bold(new Run(x.Version)));
                    document.Blocks.Add(paragraph1);
                    x.Notes.SelectMany(x => x.NoteDetails).GroupBy(y => y.Module).ToList().ForEach(y =>
                    {
                        Paragraph paragraph2 = new Paragraph();
                        paragraph2.Inlines.Add(new Run("\tPhân hệ "));
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
                                Kind = PackIconKind.FeatureHighlight
                            };
                            TextBlock textBlock = new TextBlock
                            {
                                Text = " Tính năng"
                            };
                            stackPannel.Children.Add(icon);
                            stackPannel.Children.Add(textBlock);
                            paragraph3.Inlines.Add(stackPannel);
                        }
                    });
                });
            }

            return document;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
