using Aspose.Cells;
using AutoMapper;
using FlexCel.Core;
using FlexCel.Pdf;
using FlexCel.Render;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Libs.PDFViewer.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service.Impl
{
    public class ExportService : IExportService
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ReportPreviewViewModel _reportPreviewViewModel;
        private readonly INsMucLucNganSachService _iMucLucNganSachService;
        private readonly ICryptographyService _iCryptographyService;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        private readonly string _templatePath;
        private readonly string _tmpPath;
        public bool IsCancel { get; set; }

        public ExportService(
            ILog logger,
            IMapper mapper,
            IConfiguration configuration,
            INsMucLucNganSachService iMucLucNganSachService,
            ICryptographyService iCryptographyService,
            ReportPreviewViewModel reportPreviewViewModel,
            IHTTPUploadFileService hTTPUploadFileService)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _reportPreviewViewModel = reportPreviewViewModel;
            _iCryptographyService = iCryptographyService;
            _iMucLucNganSachService = iMucLucNganSachService;
            _hTTPUploadFileService = hTTPUploadFileService;

            _templatePath = _configuration.GetSection(ConfigHelper.TEMPLATE_XLXSPATH).Value;
            _tmpPath = Path.Combine(IOExtensions.ApplicationPath, _configuration.GetSection(ConfigHelper.TEMP_PATH).Value);

            IOExtensions.CreateDirectoryIfNotExists(_templatePath);
            IOExtensions.CreateDirectoryIfNotExists(_tmpPath);

            try
            {
                // Clear temp folder
                IOExtensions.ClearForlder(_tmpPath);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportJsonFile(string fileName, object jsonObject)
        {
            try
            {
                //var options = new JsonSerializerOptions
                //{
                //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                //    WriteIndented = true
                //};
                //string json = JsonSerializer.Serialize(jsonObject, options);
                //fileName = Path.GetFileNameWithoutExtension(fileName) + DateTime.Now.ToString("ddMMyyyyhhmmss") + Path.GetExtension(fileName);
                //using (StreamWriter stream = File.AppendText(Path.Combine(IOExtensions.TempPath, fileName)))
                //{
                //    stream.Write(json);
                //}

                //IOExtensions.OpenFolder(IOExtensions.TempPath);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra, không thể xuất file.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public ExcelFile Export(string templateFileName, DataTable data)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);

                fr.AddTable("ChiTiet", data);

                // Run and save file
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export(string templateFileName, Dictionary<string, object> data)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string path = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(path, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());
                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                fr.SetUserFunction("CurrencyToText", new CurrencyToText());
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Run and save file
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export<T>(string templateFileName, Dictionary<string, object> data)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());
                fr.SetUserFunction("CurrencyToText", new CurrencyToText());
                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Run and save file
                 
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export<T>(XlsFile xlsFile, Dictionary<string, object> data)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                fr.SetUserFunction("CurrencyToText", new CurrencyToText());
                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Run and save file
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export<T>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null, bool protectSheet = false)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());

                if (protectSheet)
                {
                    xlsFile.Protection.SetSheetProtection("111111", new TSheetProtectionOptions(true)
                    {
                        SelectLockedCells = false,
                        SelectUnlockedCells = true,
                    });
                }

                fr.SetUserFunction("CurrencyToText", new CurrencyToText());
                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Hide  column
                if (hideColumns != null)
                {
                    foreach (var column in hideColumns)
                    {
                        xlsFile.SetColHidden(column, true);
                    }
                }

                // Run and save file
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export<T1, T2>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null, bool protectSheet = false)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());

                if (protectSheet)
                {
                    xlsFile.Protection.SetSheetProtection("111111", new TSheetProtectionOptions(true)
                    {
                        SelectLockedCells = false,
                        SelectUnlockedCells = true,
                        ColumnFormatting = true
                    });
                }

                //xlsFile.Protection.SetSheetProtectionOptions(new TSheetProtectionOptions(true));
                fr.SetUserFunction("CurrencyToText", new CurrencyToText());
                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T1>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T1>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T2>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T2>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Hide  column
                if (hideColumns != null)
                {
                    foreach (var column in hideColumns)
                    {
                        xlsFile.SetColHidden(column, true);
                    }
                }

                // Run export
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile HiddenExport<T1, T2>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null, List<int> hideRows = null, bool protectSheet = false)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());

                if (protectSheet)
                {
                    xlsFile.Protection.SetSheetProtection("111111", new TSheetProtectionOptions(true)
                    {
                        SelectLockedCells = false,
                        SelectUnlockedCells = true,
                        ColumnFormatting = true
                    });
                }

                fr.SetUserFunction("CurrencyToText", new CurrencyToText());
                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T1>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T1>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T2>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T2>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Hide  column
                if (hideColumns != null)
                {
                    foreach (var column in hideColumns)
                    {
                        xlsFile.SetColHidden(column, true);
                    }
                }

                if (hideRows != null)
                {
                    foreach (var row in hideRows)
                    {
                        xlsFile.SetRowHidden(row, true);
                    }
                }

                // Run export
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export<T1, T2, T3>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null, bool protectSheet = false)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());

                if (protectSheet)
                {
                    xlsFile.Protection.SetSheetProtection("111111", new TSheetProtectionOptions(true)
                    {
                        SelectLockedCells = false,
                        SelectUnlockedCells = true,
                    });
                }

                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T1>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T1>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T2>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T2>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T3>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T3>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Hide  column
                if (hideColumns != null)
                {
                    foreach (var column in hideColumns)
                    {
                        xlsFile.SetColHidden(column, true);
                    }
                }
                // Run export
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export<T1, T2, T3, T4>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());

                // Binding data
                fr.SetUserFunction("CurrencyToText", new CurrencyToText());
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T1>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T1>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T2>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T2>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T3>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T3>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T4>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T4>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Hide  column
                if (hideColumns != null)
                {
                    foreach (var column in hideColumns)
                    {
                        xlsFile.SetColHidden(column, true);
                    }
                }

                // Run export
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export<T1, T2, T3, T4, T5>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());

                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T1>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T1>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T2>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T2>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T3>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T3>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T4>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T4>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T5>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T5>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Hide  column
                if (hideColumns != null)
                {
                    foreach (var column in hideColumns)
                    {
                        xlsFile.SetColHidden(column, true);
                    }
                }

                // Run export
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public ExcelFile Export<T1, T2, T3, T4, T5, T6>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null)
        {
            using (FlexCelReport fr = new FlexCelReport(true))
            {
                string templatePath = Path.Combine(_templatePath, templateFileName);
                XlsFile xlsFile = new XlsFile(templatePath, true);
                SetFont(xlsFile, !data.TryGetValue("Ngay", out var ngay) || ngay.ToString().IsEmpty());

                // Binding data
                fr.SetExpression("fRow", "<#Row height(Autofit;" + System.Windows.Application.Current.Properties[NSConstants.ROWHEIGHT] + ")>");
                foreach (KeyValuePair<string, object> entry in data)
                {
                    if (entry.Value is DataTable)
                    {
                        var dataTbl = entry.Value as DataTable;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T1>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T1>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T2>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T2>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T3>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T3>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T4>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T4>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T5>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T5>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is IEnumerable<T6>)
                    {
                        var dataTbl = entry.Value as IEnumerable<T6>;
                        fr.AddTable(entry.Key, dataTbl);
                    }
                    else if (entry.Value is TFlexCelUserFunction)
                    {
                        fr.SetUserFunction(entry.Key, entry.Value as TFlexCelUserFunction);
                    }
                    else
                    {
                        fr.SetValue(entry.Key, entry.Value);
                    }
                }

                // Hide  column
                if (hideColumns != null)
                {
                    foreach (var column in hideColumns)
                    {
                        xlsFile.SetColHidden(column, true);
                    }
                }

                // Run export
                fr.Run(xlsFile);
                return xlsFile;
            }
        }

        public string ExportExcel(ExcelFile xlsFile, string filename = null)
        {
            try
            {
                if (filename.IsEmpty())
                    filename = IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Xlsx, Path.GetFileNameWithoutExtension(xlsFile.ActiveFileName));

                xlsFile.Save(filename, TFileFormats.Xlsx);
                return xlsFile.ActiveFileName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public string ExportExcel(ExcelFile xlsFile, ref MemoryStream fileStream)
        {
            try
            {
                xlsFile.Protection.SetSheetProtectionOptions(null);
                xlsFile.Save(fileStream, TFileFormats.Xlsx);
                fileStream.Seek(0, SeekOrigin.Begin);
                return xlsFile.ActiveFileName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public string ExportPdf2(ExcelFile xlsFile, string filename)
        {
            using (FlexCelPdfExport flexCelPdfExport = new FlexCelPdfExport(xlsFile, true))
            {
                flexCelPdfExport.FontEmbed = TFontEmbed.Embed;
                flexCelPdfExport.FontMapping = TFontMapping.ReplaceStandardFonts;
                flexCelPdfExport.Export(filename);
                return filename;
            }
        }

        public string ExportPdf(ExcelFile xlsFile, string filename = null)
        {
            if (filename.IsEmpty())
                filename = IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf, Path.GetFileNameWithoutExtension(xlsFile.ActiveFileName));

            using (FlexCelPdfExport flexCelPdfExport = new FlexCelPdfExport(xlsFile, true))
            {
                flexCelPdfExport.FontEmbed = TFontEmbed.Embed;
                flexCelPdfExport.FontMapping = TFontMapping.ReplaceStandardFonts;
                flexCelPdfExport.Export(filename);
                return filename;
            }
        }

        public string ExportPdf(ExcelFile[] xlsFiles, string filename)
        {
            if (filename.IsEmpty())
                filename = IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf);

            using (FlexCelPdfExport flexCelPdfExport = new FlexCelPdfExport())
            {
                flexCelPdfExport.FontEmbed = TFontEmbed.Embed;
                flexCelPdfExport.FontMapping = TFontMapping.ReplaceStandardFonts;
                flexCelPdfExport.AllowOverwritingFiles = true;

                using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    flexCelPdfExport.BeginExport(fileStream);
                    int totalPagesAllSheets = 0;
                    for (int index = 0; index < xlsFiles.Length; index++)
                    {
                        ExcelFile xlsFile = xlsFiles[index];
                        flexCelPdfExport.Workbook = xlsFile;
                        totalPagesAllSheets += flexCelPdfExport.TotalPagesInSheet();
                        //flexCelPdfExport.ExportAllVisibleSheets(false, string.Format("File pdf {0}", index + 1));
                    }
                    int totalPagesPerSheet = 1;
                    for (int index = 0; index < xlsFiles.Length; index++)
                    {
                        ExcelFile xlsFile = xlsFiles[index];
                        flexCelPdfExport.Workbook = xlsFile;
                        //flexCelPdfExport.ExportAllVisibleSheets(false, string.Format("File pdf {0}", index + 1));
                        flexCelPdfExport.ExportSheet(totalPagesPerSheet, totalPagesAllSheets);
                        totalPagesPerSheet += flexCelPdfExport.TotalPagesInSheet();

                    }
                    flexCelPdfExport.EndExport();
                }
                return filename;
            }
        }

        public string ExportExcel(ExcelFile[] xlsFiles, string filename = null)
        {
            try
            {
                if (filename.IsEmpty())
                {
                    filename = IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Xlsx);
                }
                var firstExcel = xlsFiles[0];
                int lenghtExcel = xlsFiles.Length;
                for (int i = 1; i < xlsFiles.Length; i++)
                {
                    firstExcel.InsertAndCopySheets(1, i + 1, 1, xlsFiles[i]);
                }
                firstExcel.Save(filename);
                return filename;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public string ExportExcelAspose(ExcelFile[] xlsFiles, string filename = null)
        {
            try
            {
                if (filename.IsEmpty())
                {
                    filename = IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Xlsx);
                }

                Workbook workbook = new Workbook();
                Worksheet summarySheet = workbook.Worksheets["Sheet1"];

                int totalRowCount = 1;

                foreach (var xlsFile in xlsFiles)
                {
                    Workbook temp = new Workbook(xlsFile.ActiveFileName);
                    Worksheet sourceSheet = temp.Worksheets["Sheet1"];
                    Range sourceRange;
                    Range destRange;

                    sourceRange = sourceSheet.Cells.MaxDisplayRange;

                    destRange = summarySheet.Cells.CreateRange(
                            sourceRange.FirstRow + totalRowCount,
                            sourceRange.FirstColumn,
                            sourceRange.RowCount,
                            sourceRange.ColumnCount);

                    destRange.Copy(sourceRange);
                    totalRowCount += sourceRange.RowCount;
                }

                workbook.Save(filename);
                return filename;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void ShowPdf(PdfFileModel pdfFile, int exportType = 0)
        {
            //_reportPreviewViewModel.PageRowDisplay = exportType == (int)ExportType.PDF_ONE_PAPER ? PageRowDisplayType.SinglePageRow : PageRowDisplayType.ContinuousPageRows
            _reportPreviewViewModel.Items = new ObservableCollection<PdfFileModel>() { pdfFile };
            _reportPreviewViewModel.Init();
            _reportPreviewViewModel.ShowDialog();
        }

        public void ShowPdf(List<PdfFileModel> pdfFiles, int exportType = 0)
        {
            //_reportPreviewViewModel.PageRowDisplay = exportType == (int)ExportType.PDF_ONE_PAPER ? PageRowDisplayType.SinglePageRow : PageRowDisplayType.ContinuousPageRows
            _reportPreviewViewModel.Items = new ObservableCollection<PdfFileModel>(pdfFiles);
            _reportPreviewViewModel.Init();
            _reportPreviewViewModel.ShowDialog();
        }

        public void Open(List<ExportResult> results, ExportType exportType)
        {
            if (results != null && results.Count > 0)
            {
                switch (exportType)
                {
                    case ExportType.EXCEL:
                        using (var dialog = new FolderBrowserDialog())
                        {
                            dialog.Description = Resources.MsgSaveFolder;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedPath = dialog.SelectedPath;
                                foreach (var rs in results)
                                {
                                    rs.FileName = this.ExportExcel(rs.ExcelFile, Path.Combine(selectedPath, rs.FileNameWithoutExtension + FileExtensionFormats.Xlsx));
                                }
                                IOExtensions.OpenFiles(results.Last().FileName);
                            }
                        }
                        break;
                    case ExportType.PDF:
                        if (!Directory.Exists(_tmpPath))
                        {
                            Directory.CreateDirectory(_tmpPath);
                        }
                        foreach (var rs in results)
                        {
                            rs.FileName = ExportPdf(rs.ExcelFile, IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf, rs.FileNameWithoutExtension));
                        }
                        ShowPdf(_mapper.Map<List<PdfFileModel>>(results));
                        break;

                    case ExportType.PDF_ONE_PAPER:
                        var firstExcel = results.FirstOrDefault();
                        var pdfResult = results.Select(n => n.ExcelFile).ToArray();
                        firstExcel.FileName = ExportPdf(pdfResult, IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf, "Tổng hợp đơn vị"));
                        firstExcel.Title = "Tổng hợp đơn vị";
                        ShowPdf(_mapper.Map<PdfFileModel>(firstExcel), (int)exportType);
                        break;
                    case ExportType.EXCEL_ONE_PAPER:
                        using (var dialog = new FolderBrowserDialog())
                        {
                            dialog.Description = Resources.MsgSaveFolder;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedPath = dialog.SelectedPath;
                                var firstExcels = results.FirstOrDefault();
                                var excelResults = results.Select(n => n.ExcelFile).ToArray();
                                var fileNameWithoutExtension = StringUtils.CreateExportFileName("TongHopDonVi");

                                firstExcels.FileName = ExportExcel(excelResults, Path.Combine(selectedPath, fileNameWithoutExtension + FileExtensionFormats.Xlsx));
                                IOExtensions.OpenFiles(firstExcels.FileName);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void Open2(List<ExportResult> results, ExportType exportType, Action<int> applyChanges, Action complete)
        {
            if (results != null && results.Count > 0)
            {
                switch (exportType)
                {
                    case ExportType.EXCEL:
                        using (var dialog = new FolderBrowserDialog())
                        {
                            dialog.Description = Resources.MsgSaveFolder;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedPath = dialog.SelectedPath;
                                foreach (var rs in results)
                                {
                                    rs.FileName = this.ExportExcel(rs.ExcelFile, Path.Combine(selectedPath, rs.FileNameWithoutExtension + FileExtensionFormats.Xlsx));
                                }
                                IOExtensions.OpenFiles(results.Last().FileName);
                            }
                            complete();
                        }
                        break;
                    case ExportType.PDF:
                        if (!Directory.Exists(_tmpPath))
                        {
                            Directory.CreateDirectory(_tmpPath);
                        }
                        BackgroundWorkerHelper.Run((e, s) =>
                        {
                            foreach (var rss in results.Select((value, index) => new { index, value }))
                            {
                                applyChanges(rss.index);
                                var rs = rss.value;
                                rs.FileName = ExportPdf2(rs.ExcelFile, IOExtensions.CreateTempFile2(_tmpPath, FileExtensionFormats.Pdf, rs.FileNameWithoutExtension));
                                (e as System.ComponentModel.BackgroundWorker).ReportProgress((rss.index + 1) * 100 / results.Count, null);
                                if (IsCancel)
                                {
                                    s.Cancel = true;
                                    return;
                                }

                                // gọi hàm CancalAsync sẽ đổi trạng thái CancellationPending của worker thành true
                            }
                        }, (e, s) =>
                        {
                            if (!IsCancel)
                            {
                                ShowPdf(_mapper.Map<List<PdfFileModel>>(results));
                            }
                            complete();
                        }, (e, s) =>
                        {
                            applyChanges(s.ProgressPercentage);
                        });

                        break;

                    case ExportType.PDF_ONE_PAPER:
                        var firstExcel = results.FirstOrDefault();
                        var pdfResult = results.Select(n => n.ExcelFile).ToArray();
                        firstExcel.FileName = ExportPdf(pdfResult, IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf, "Tổng hợp đơn vị"));
                        firstExcel.Title = "Tổng hợp đơn vị";
                        ShowPdf(_mapper.Map<PdfFileModel>(firstExcel), (int)exportType);
                        break;
                    case ExportType.EXCEL_ONE_PAPER:
                        using (var dialog = new FolderBrowserDialog())
                        {
                            dialog.Description = Resources.MsgSaveFolder;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedPath = dialog.SelectedPath;
                                var firstExcels = results.FirstOrDefault();
                                var excelResults = results.Select(n => n.ExcelFile).ToArray();
                                var fileNameWithoutExtension = StringUtils.CreateExportFileName("TongHopDonVi");

                                firstExcels.FileName = ExportExcel(excelResults, Path.Combine(selectedPath, fileNameWithoutExtension + FileExtensionFormats.Xlsx));
                                IOExtensions.OpenFiles(firstExcels.FileName);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void OpenEncrypt(List<ExportResult> results, ExportType exportType)
        {
            if (results != null && results.Count > 0)
            {
                switch (exportType)
                {
                    case ExportType.EXCEL:
                        using (var dialog = new FolderBrowserDialog())
                        {
                            dialog.Description = Resources.MsgSaveFolder;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedPath = dialog.SelectedPath;
                                foreach (var rs in results)
                                {
                                    rs.FileName = this.ExportExcel(rs.ExcelFile, Path.Combine(selectedPath, rs.FileNameWithoutExtension + FileExtensionFormats.Xlsx));
                                    var securityFileName = rs.FileName.Replace(FileExtensionFormats.Xlsx, FileExtensionFormats.Security);
                                    _iCryptographyService.EncryptFile(rs.FileName, securityFileName);
                                }
                                IOExtensions.OpenFiles(results.Last().FileName);
                            }
                        }
                        break;
                    case ExportType.PDF:
                        foreach (var rs in results)
                        {
                            rs.FileName = this.ExportPdf(rs.ExcelFile, IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf, rs.FileNameWithoutExtension));
                        }
                        ShowPdf(_mapper.Map<List<PdfFileModel>>(results));
                        break;

                    case ExportType.PDF_ONE_PAPER:
                        var firstExcel = results.FirstOrDefault();
                        var pdfResult = results.Select(n => n.ExcelFile).ToArray();
                        firstExcel.FileName = ExportPdf(pdfResult, IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf, firstExcel.FileNameWithoutExtension));
                        ShowPdf(_mapper.Map<PdfFileModel>(firstExcel), (int)exportType);
                        break;
                    case ExportType.EXCEL_ONE_PAPER:
                        using (var dialog = new FolderBrowserDialog())
                        {
                            dialog.Description = Resources.MsgSaveFolder;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedPath = dialog.SelectedPath;
                                var firstExcels = results.FirstOrDefault();
                                var excelResults = results.Select(n => n.ExcelFile).ToArray();
                                firstExcels.FileName = ExportExcel(excelResults, Path.Combine(selectedPath, firstExcels.FileNameWithoutExtension + FileExtensionFormats.Xlsx));
                                IOExtensions.OpenFiles(results.First().FileName);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void Open(ExportResult result, ExportType exportType, ref string sUrlLocal)
        {
            string sFileName = result.FileNameWithoutExtension + FileExtensionFormats.Xlsx;
            string sFolderSaveFileLocal = Path.Combine(IOExtensions.ApplicationPath, ConstantUrlPathPhanHe.UrlFolderFile);
            if (!Directory.Exists(sFolderSaveFileLocal))
            {
                Directory.CreateDirectory(sFolderSaveFileLocal);
            }
            string excelFileName = Path.Combine(sFolderSaveFileLocal, sFileName);
            sUrlLocal = excelFileName;
            this.ExportExcel(result.ExcelFile, excelFileName);
        }

        public void OnCancelProgress()
        {
            IsCancel = true;
        }

        public void OnProgress()
        {
            IsCancel = false;
        }
        public void Open(ExportResult result, ref MemoryStream fileStream)
        {
            ExportExcel(result.ExcelFile, ref fileStream);
        }

        public void OpenEncrypt(ExportResult result, ExportType exportType)
        {
            if (result != null)
            {
                switch (exportType)
                {
                    case ExportType.EXCEL:
                        using (var dialog = new SaveFileDialog())
                        {
                            var ext = FileExtensionFormats.Xlsx;
                            dialog.Title = Resources.MsgSaveFile;
                            dialog.RestoreDirectory = true;
                            dialog.FileName = result.FileNameWithoutExtension + ext;
                            dialog.Filter = IOExtensions.FileDialogFilterByExtension(ext);
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string excelFileName = dialog.FileName;
                                string securityFileName = excelFileName.Replace(FileExtensionFormats.Xlsx, FileExtensionFormats.Security);
                                this.ExportExcel(result.ExcelFile, excelFileName);
                                _iCryptographyService.EncryptFile(excelFileName, securityFileName);
                                IOExtensions.OpenFiles(securityFileName);
                            }
                        }
                        break;
                    case ExportType.PDF:
                        string pdfFileName = IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf, result.FileNameWithoutExtension);
                        result.FileName = this.ExportPdf(result.ExcelFile, pdfFileName);
                        this.ShowPdf(_mapper.Map<PdfFileModel>(result));
                        break;
                    default:
                        break;
                }
            }
        }

        public void Open(ExportResult result, ExportType exportType)
        {
            if (result != null)
            {
                switch (exportType)
                {
                    case ExportType.EXCEL:
                        using (var dialog = new SaveFileDialog())
                        {
                            var ext = FileExtensionFormats.Xlsx;
                            dialog.Title = Resources.MsgSaveFile;
                            dialog.RestoreDirectory = true;
                            dialog.FileName = result.FileNameWithoutExtension + ext;
                            dialog.Filter = IOExtensions.FileDialogFilterByExtension(ext);
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string excelFileName = dialog.FileName;
                                this.ExportExcel(result.ExcelFile, excelFileName);
                                IOExtensions.OpenFiles(excelFileName);
                            }
                        }
                        break;
                    case ExportType.PDF:
                        string pdfFileName = IOExtensions.CreateTempFile(_tmpPath, FileExtensionFormats.Pdf, result.FileNameWithoutExtension);
                        result.FileName = this.ExportPdf(result.ExcelFile, pdfFileName);
                        this.ShowPdf(_mapper.Map<PdfFileModel>(result));
                        break;
                    default:
                        break;
                }
            }
        }

        public bool OpenJson<T>(List<T> results) where T : class
        {
            if (results == null || !results.Any())
                return false;
            StringBuilder sb = JsonExtensions.ConvertListToJson<T>(results);
            if (sb == null || sb.Length == 0)
                return false;
            string sTableName = typeof(T).Name;
            using (var dialog = new SaveFileDialog())
            {
                var ext = FileExtensionFormats.Json;
                dialog.Title = Resources.MsgSaveFile;
                dialog.RestoreDirectory = true;
                dialog.FileName = string.Format("{0}_{1}{2}", sTableName, DateTime.Now.ToString("ddMMyyyyHHmmssss"), FileExtensionFormats.Json);
                dialog.Filter = IOExtensions.FileDialogFilterByExtension(ext);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string jsonFileName = dialog.FileName;
                    WriteFileJson(sb, jsonFileName);
                    IOExtensions.OpenFiles(jsonFileName);
                }
            }
            return true;
        }

        private string WriteFileJson(StringBuilder sb, string fileName)
        {
            File.WriteAllText(fileName, sb.ToString());
            return fileName;
        }

        public void GenerateImportData<T>(List<string> lstLns, IEnumerable<T> Items, List<T> resultToTals, List<GenericReportHeader> headers,
            int yearOfWork, Func<T, bool> filterTotalResult = null, Dictionary<string, string> mapData = null) where T : ModelBase, new()
        {
            if (filterTotalResult == null)
            {
                filterTotalResult = t => true;
            }

            if (mapData == null)
            {
                mapData = new Dictionary<string, string>
                {
                    {"TonKho", "FTonKho"},
                    {"TuChi", "FTuChi"},
                    {"HienVat", "FHienVat"},
                    {"DuPhong", "FDuPhong"},
                    {"HangMua", "FHangMua"},
                    {"HangNhap", "FHangNhap"},
                    {"PhanCap", "FPhanCap"}
                };

            }
            string concatLns = string.Join(",", lstLns);
            var listSettingMLNS = _iMucLucNganSachService.FindByListLnsDonVi(concatLns, yearOfWork).ToList();
            GenericReportHeader hd = new GenericReportHeader();
            bool isFirst = true;

            if (listSettingMLNS.Any(x => x.BTonKho))
            {
                hd = new GenericReportHeader();
                hd.Header1 = "Trong đó";
                hd.Header2 = "Tồn kho";
                hd.Loai = mapData["TonKho"];
                hd.IsFirst = isFirst;
                isFirst = false;
                headers.Add(hd);
            }
            if (listSettingMLNS.Any(x => x.BTuChi))
            {
                hd = new GenericReportHeader();
                hd.Header1 = "Trong đó";
                hd.Header2 = "Tự chi";
                hd.Loai = mapData["TuChi"];
                ;
                hd.IsFirst = isFirst;
                isFirst = false;
                headers.Add(hd);
            }

            if (listSettingMLNS.Any(x => x.BHienVat))
            {
                hd = new GenericReportHeader();
                hd.Header1 = "Trong đó";
                hd.Header2 = "Hiện vật";
                hd.Loai = mapData["HienVat"];
                hd.IsFirst = isFirst;
                isFirst = false;
                headers.Add(hd);
            }

            if (listSettingMLNS.Any(x => x.BDuPhong))
            {
                hd = new GenericReportHeader();
                hd.Header1 = "Trong đó";
                hd.Header2 = "Dự phòng";
                hd.Loai = mapData["DuPhong"];
                hd.IsFirst = isFirst;
                isFirst = false;
                headers.Add(hd);
            }

            if (listSettingMLNS.Any(x => x.BHangMua))
            {
                hd = new GenericReportHeader();
                hd.Header1 = "Trong đó";
                hd.Header2 = "Hàng mua";
                hd.Loai = mapData["HangMua"];
                hd.IsFirst = isFirst;
                isFirst = false;
                headers.Add(hd);
            }

            if (listSettingMLNS.Any(x => x.BHangNhap))
            {
                hd = new GenericReportHeader();
                hd.Header1 = "Trong đó";
                hd.Header2 = "Hàng nhập";
                hd.Loai = mapData["HangNhap"];
                hd.IsFirst = isFirst;
                isFirst = false;
                headers.Add(hd);
            }

            if (listSettingMLNS.Any(x => x.BPhanCap))
            {
                hd = new GenericReportHeader();
                hd.Header1 = "Trong đó";
                hd.Header2 = "Phân cấp";
                hd.Loai = mapData["PhanCap"];
                hd.IsFirst = isFirst;
                headers.Add(hd);
            }

            if (headers.Count() == 0)
            {
                hd = new GenericReportHeader();
                hd.Header1 = "Trong đó";
                hd.Header2 = "";
                hd.Loai = "Empty";
                hd.IsFirst = isFirst;
                headers.Add(hd);
            }

            int totalWidth = 100;
            foreach (var it in headers)
            {
                it.WidthHeader2 = (totalWidth / 7).ToString();
            }

            T rstt = new T();
            rstt.LstDataTotalModels = new List<DataModel>();
            // get displayed Column here
            List<string> displayedColumns = headers.Where(h => h.Loai != null).Select(h => h.Loai).ToList();
            Dictionary<string, double> totalValueDictionary = new Dictionary<string, double>();
            foreach (var prop in displayedColumns)
            {
                totalValueDictionary.Add(prop, 0);
            }

            foreach (var item in Items)
            {
                item.LstDataModels = new List<DataModel>();
                foreach (var prop in displayedColumns)
                {
                    PropertyInfo propertyInfo = item.GetType().GetProperty(prop);
                    if (propertyInfo != null)
                    {
                        double val = Double.Parse(propertyInfo.GetValue(item).ToString());
                        item.LstDataModels.Add(new DataModel(val));
                        if (filterTotalResult.Invoke(item))
                            totalValueDictionary[prop] = totalValueDictionary[prop] + val;
                    }
                    else
                    {
                        item.LstDataModels.Add(new DataModel(0));
                    }

                    item.Total = item.LstDataModels.Sum(t => t.Value);
                }
            }

            foreach (var prop in displayedColumns)
            {
                rstt.LstDataTotalModels.Add(new DataModel(totalValueDictionary[prop]));
            }

            resultToTals.Add(rstt);
        }

        public void FormatAllRowHeight<T>(ObservableCollection<T> items, string propertyName, int startRowItems, int maxCharacterPerLine, ExcelFile xlsFile)
        {
            int counter = 0;
            foreach (var item in items)
            {
                if (item.GetType().GetProperty(propertyName).GetValue(item, null).ToString().Length < maxCharacterPerLine)
                {
                    xlsFile.SetRowHeight(startRowItems + counter, 500);
                }
                counter++;
            }
        }


        private void SetFont(XlsFile xlsFile, bool rmDateLocation = false)
        {
            int fontSize = 100;
            var fontType = GlobalVariables.GetItemsByTag("FontType");
            var fontSizeString = GlobalVariables.GetItemsByTag("FontSize");
            if (fontType == "0")
                fontType = "Times New Roman";
            if (fontSizeString == "0")
                fontSizeString = "100";
            else
                _ = int.TryParse(fontSizeString, out fontSize);

            //const string ReplaceWith = "Times New Roman";
            if (fontSize != 100 || rmDateLocation)
            {
                TExcelObjectList val = new TExcelObjectList();
                xlsFile.GetObjectsInRange(TXlsCellRange.FullRange(), val);

                for (var i = 0; i < val.Count; i++)
                {
                    var shape = xlsFile.GetObjectPropertiesByShapeId(val.ShapeId(i), true);

                    for (var ii = 0; ii < shape.ChildrenCount; ii++)
                    {
                        var child = shape.Children(ii + 1);
                        ShapeProcess(xlsFile, child, i + 1, fontSize, rmDateLocation);
                    }

                    //shape.ShapeOptions.SetValue(TShapeOption.fFitShapeToText, true);
                    var text = shape?.Text;
                    if (text is TDrawingRichString value)
                    {
                        var arr0 = new List<TDrawingTextParagraph>();
                        if (!(rmDateLocation && (text.Value.Contains("#DiaDiem") || text.Value.Contains("#Ngay"))))
                        {
                            for (var x = 0; x < value.ParagraphCount; x++)
                            {
                                var arr1 = new List<TDrawingTextRun>();
                                foreach (var y in value.Paragraph(x).Runs)
                                {
                                    var textProperty = y.TextProperties;
                                    var obj = y.TextProperties.Attributes;
                                    var size = y.TextProperties.Attributes.Size * fontSize / 100;
                                    var textAttribute = new TDrawingTextAttributes(obj.Kumimoji, obj.Lang, obj.AltLang, size, obj.Bold, obj.Italic, obj.Underline, obj.Strike, obj.Kern, obj.Capitalization, obj.Spacing, obj.NormalizeH, obj.Baseline, obj.NoProof, obj.Dirty, obj.Err, obj.SmartTagClean, obj.SmartTagId, obj.BookmarkLinkTarget);
                                    var textProperties = new TDrawingTextProperties(textProperty.Fill, textProperty.Line, textProperty.Effects, textProperty.Highlight, textProperty.Underline, textProperty.Latin, textProperty.EastAsian, textProperty.ComplexScript, textProperty.Symbol, textProperty.HyperlinkClick, textProperty.HyperlinkMouseOver, textProperty.RightToLeft, textAttribute);
                                    var textRun = new TDrawingTextRun(y.Text, textProperties);
                                    arr1.Add(textRun);
                                }
                                arr0.Add(new TDrawingTextParagraph(arr1.ToArray(), value.Paragraph(x).Properties, value.Paragraph(x).EndParagraphProperties));
                            }
                        }

                        var richString = new TDrawingRichString(arr0.ToArray());
                        xlsFile.SetObjectText(i + 1, shape.ObjectPathAbsolute, richString);
                    }
                }
            }

            for (int i = 0; i <= xlsFile.FontCount; i++)
            {
                TFlxFont fnt = xlsFile.GetFont(i);
                fnt.Name = fontType;
                fnt.Size20 = fnt.Size20 * fontSize / 100;
                xlsFile.SetFont(i, fnt);
            }

        }

        private void ShapeProcess(XlsFile xlsFile, TShapeProperties shape, int index, int fontSize, bool rmDateLocation = false)
        {
            for (var ii = 0; ii < shape.ChildrenCount; ii++)
            {
                var child = shape.Children(ii + 1);
                ShapeProcess(xlsFile, child, index, fontSize);
            }
            var text = shape?.Text;
            if (text is TDrawingRichString value)
            {
                var arr0 = new List<TDrawingTextParagraph>();
                if (!(rmDateLocation && (text.Value.Contains("#DiaDiem") || text.Value.Contains("#Ngay"))))
                {
                    for (var x = 0; x < value.ParagraphCount; x++)
                    {
                        var arr1 = new List<TDrawingTextRun>();
                        foreach (var y in value.Paragraph(x).Runs)
                        {
                            var textProperty = y.TextProperties;
                            var obj = y.TextProperties.Attributes;
                            var size = y.TextProperties.Attributes.Size * fontSize / 100;
                            var textAttribute = new TDrawingTextAttributes(obj.Kumimoji, obj.Lang, obj.AltLang, size, obj.Bold, obj.Italic, obj.Underline, obj.Strike, obj.Kern, obj.Capitalization, obj.Spacing, obj.NormalizeH, obj.Baseline, obj.NoProof, obj.Dirty, obj.Err, obj.SmartTagClean, obj.SmartTagId, obj.BookmarkLinkTarget);
                            var textProperties = new TDrawingTextProperties(textProperty.Fill, textProperty.Line, textProperty.Effects, textProperty.Highlight, textProperty.Underline, textProperty.Latin, textProperty.EastAsian, textProperty.ComplexScript, textProperty.Symbol, textProperty.HyperlinkClick, textProperty.HyperlinkMouseOver, textProperty.RightToLeft, textAttribute);
                            var textRun = new TDrawingTextRun(y.Text, textProperties);
                            arr1.Add(textRun);
                        }
                        arr0.Add(new TDrawingTextParagraph(arr1.ToArray(), value.Paragraph(x).Properties, value.Paragraph(x).EndParagraphProperties));
                    }
                }

                var richString = new TDrawingRichString(arr0.ToArray());
                xlsFile.SetObjectText(index, shape.ObjectPathAbsolute, richString);
            }
        }
    }


    public class ExportResult
    {
        public string Title { get; set; }
        public string FileNameWithoutExtension { get; set; }
        public string FileName { get; set; }
        public ExcelFile ExcelFile { get; set; }

        public ExportResult(string fileName, ExcelFile excelFile)
        {
            FileName = fileName;
            ExcelFile = excelFile;
        }

        public ExportResult(string title, string fileName, ExcelFile excelFile)
        {
            Title = title;
            FileName = fileName;
            ExcelFile = excelFile;
        }

        public ExportResult(string title, string fileNameWithoutExtension, string fileName, ExcelFile excelFile)
        {
            Title = title;
            FileNameWithoutExtension = MakeValidFileName(fileNameWithoutExtension);
            FileName = MakeValidFileName(fileName);
            ExcelFile = excelFile;
        }

        private static string MakeValidFileName(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
                string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

                return System.Text.RegularExpressions.Regex.Replace(fileName, invalidRegStr, "_");
            }
            return string.Empty;
        }
    }

    public class ImportDataGenericModel
    {
        public List<DataModel> LstDataModels { get; set; }
    }

    public class DataModel
    {
        public double Value { get; set; }

        public DataModel(double value)
        {
            this.Value = value;
        }
    }
}

