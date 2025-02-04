using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.View.SystemAdmin.Utilities
{
    /// <summary>
    /// Interaction logic for QueryData.xaml
    /// </summary>
    public partial class QueryData : UserControl
    {
        public QueryData()
        {
            InitializeComponent();
            SearchPanel.Install(QueryEditor);
        }

        private void Comment(object sender, RoutedEventArgs e)
        {
            TextDocument document = this.QueryEditor.Document;
            DocumentLine lineByOffset1 = document.GetLineByOffset(this.QueryEditor.SelectionStart);
            DocumentLine lineByOffset2 = document.GetLineByOffset(this.QueryEditor.SelectionStart + this.QueryEditor.SelectionLength);
            for (DocumentLine documentLine = lineByOffset1; documentLine != null && documentLine.LineNumber < lineByOffset2.LineNumber + 1; documentLine = documentLine.NextLine)
                this.QueryEditor.Document.Insert(documentLine.Offset, "--");
        }

        private void Uncomment(object sender, RoutedEventArgs e)
        {
            TextDocument document = this.QueryEditor.Document;
            DocumentLine lineByOffset1 = document.GetLineByOffset(this.QueryEditor.SelectionStart);
            DocumentLine lineByOffset2 = document.GetLineByOffset(this.QueryEditor.SelectionStart + this.QueryEditor.SelectionLength);
            for (DocumentLine documentLine = lineByOffset1; documentLine != null && documentLine.LineNumber < lineByOffset2.LineNumber + 1; documentLine = documentLine.NextLine)
            {
                if (this.QueryEditor.Document.GetText(documentLine.Offset, documentLine.TotalLength).StartsWith("--"))
                    document.Remove(documentLine.Offset, 2);
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();

            // Replace all underscores with two underscores, to prevent AccessKey handling
            e.Column.Header = header.Replace("_", "__");
        }
    }
}
