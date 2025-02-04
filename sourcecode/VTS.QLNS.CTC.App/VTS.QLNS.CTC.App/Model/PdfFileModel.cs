using FlexCel.Core;
using System.IO;

namespace VTS.QLNS.CTC.App.Model
{
    public class PdfFileModel : BindableBase
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
        public ExcelFile ExcelFile { get; set; }

        public PdfFileModel()
        {
        }

        public PdfFileModel(string title, string filePath, ExcelFile excelFile)
        {
            Title = title;
            FilePath = filePath;
            ExcelFile = excelFile;
        }
    }
}
