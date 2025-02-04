using FlexCel.Core;
using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service
{
    public interface IExportService
    {
        ExcelFile Export(string templateFileName, DataTable data);
        ExcelFile Export(string templateFileName, Dictionary<string, object> data);
        ExcelFile Export<T>(string templateFileName, Dictionary<string, object> data);
        ExcelFile Export<T>(XlsFile xlsFile, Dictionary<string, object> data);
        ExcelFile Export<T>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null, bool protectSheet = false);
        ExcelFile Export<T1, T2>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null, bool protectSheet = false);
        ExcelFile HiddenExport<T1, T2>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null, List<int> hideRows = null, bool protectSheet = false);
        ExcelFile Export<T1, T2, T3>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null, bool protectSheet = false);
        ExcelFile Export<T1, T2, T3, T4>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null);
        ExcelFile Export<T1, T2, T3, T4, T5>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null);
        ExcelFile Export<T1, T2, T3, T4, T5, T6>(string templateFileName, Dictionary<string, object> data, List<int> hideColumns = null);
        void ExportJsonFile(string fileName, object jsonObj);
        bool OpenJson<T>(List<T> results) where T : class;
        string ExportExcel(ExcelFile xls, string filename = null);
        /// <summary>
        /// Convert exel file to pdf
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        string ExportPdf(ExcelFile xls, string filename = null);
        /// <summary>
        /// Merge multil file exel to one page pdf
        /// </summary>
        /// <param name="xlsFiles"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        string ExportPdf(ExcelFile[] xlsFiles, string filename = null);
        /// <summary>
        /// Show mutil pdf file after convert exel file to pdf
        /// </summary>
        /// <param name="pdfFiles"></param>
        /// <param name="exportType"></param>
        void ShowPdf(PdfFileModel pdfFile, int exportType = 0);
        /// <summary>
        /// Show mutil pdf file after convert exel file to pdf
        /// </summary>
        /// <param name="pdfFiles"></param>
        /// <param name="exportType"></param>
        void ShowPdf(List<PdfFileModel> pdfFiles, int exportType = 0);
        /// <summary>
        /// Open result when export complete
        /// </summary>
        /// <param name="results"></param>
        /// <param name="exportType"></param>
        void Open(List<ExportResult> results, ExportType exportType);
        void Open2(List<ExportResult> results, ExportType exportType, Action<int> applyChanges, Action complete);
        void OpenEncrypt(List<ExportResult> results, ExportType exportType);
        void Open(ExportResult result, ExportType exportType, ref string sUrlLocal);
        void Open(ExportResult result, ref MemoryStream fileStream);
        void OnCancelProgress();
        void OnProgress();

        /// <summary>
        /// Open mutil result when export complete
        /// </summary>
        /// <param name="result"></param>
        /// <param name="exportType"></param>
        void Open(ExportResult result, ExportType exportType);

        void GenerateImportData<T>(List<string> lstLns, IEnumerable<T> Items, List<T> resultToTals, List<GenericReportHeader> headers,
            int yearOfWork, Func<T, bool> filterResultTotal = null, Dictionary<string, string> mapData = null) where T : ModelBase, new();

        /// <summary>
        /// Nếu giá trị của thuộc tính propertyName quá ngắn thì set min height cho dòng đó
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="propertyName">tên thuộc tính xét độ dài</param>
        /// <param name="startRowItems">dòng bắt đầu loop trong file excel</param>
        /// <param name="maxCharacterPerLine">chiều dài max trong 1 dòng</param>
        /// <param name="xlsFile"></param>
        void FormatAllRowHeight<T>(ObservableCollection<T> items, string propertyName, int startRowItems, int maxCharacterPerLine, ExcelFile xlsFile);
    }
}
