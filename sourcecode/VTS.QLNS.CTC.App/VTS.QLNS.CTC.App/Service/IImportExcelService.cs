using System.Collections.Generic;
using System.IO;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.ViewModel.ImportViewModel;

namespace VTS.QLNS.CTC.App.Service
{
    public interface IImportExcelService
    {
        ImportResult<T> ProcessData<T>(string fileName, bool isCheckFirstColEmpty = true);
        ImportResult<T> ProcessDataUnique<T>(string fileName, bool isCheckFirstColEmpty = true);
        ImportResult<T> ProcessDataUnique<T>(MemoryStream fileName, string tokenKey, bool isCheckFirstColEmpty = true);
        ImportResult<T> ProcessDataByRow<T>(string fileName, int rowStart);
        ColumnAttribute GetColumnAttribute<T>(int index);
        ImportResult<T> ProcessData<T>(string fileName, string sheetName, int rowStart, List<ImportField> importFields);
        IEnumerable<T> Validate<T>(IEnumerable<T> data, List<ImportField> importFields) where T : BaseImportModel;
        List<ImportErrorItem> ValidateItem<T>(T item, int rowIndex);
        string GetCellValue(string fileName, string sheetName, string addressName);
        string FindCellByValue(string fileName, string value, int sheetIndex);
        void SetLastRowToRead(int value);
        List<T> GetDataJson<T>(string fileName) where T : class;
        ImportResult<T1, T2, T3, T4, T5> ProcessData<T1, T2, T3, T4, T5>(string fileName, bool isCheckFirstColEmpty = true);

    }
}
