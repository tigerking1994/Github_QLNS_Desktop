using Aspose.Cells;
using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.ViewModel.ImportViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service.Impl
{
    public class ImportExcelService : IImportExcelService
    {
        private readonly INhDmNhaThauRepository _nhaThauRepository;
        private readonly INhDaHopDongRepository _nhDaHopDongRepository;
        private readonly IMucLucNganSachRepository _mucLucNganSachRepository;
        private readonly ITnDanhMucLoaiHinhRepository _tnDanhMucLoaiHinhRepository;
        private readonly ISktMucLucRepository _iSktMucLucRepository;
        private readonly IBhDmMucLucNganSachRepository _bhDmMucLucNganSachRepository;
        private readonly ISessionService _sessionService;
        private readonly ICryptographyService _cryptographyService;
        private int _lastRowToRead = 0;
        private int _countAttributes = 0;

        public ImportExcelService(
            INhDmNhaThauRepository nhaThauRepository,
            INhDaHopDongRepository nhDaHopDongRepository,
            IMucLucNganSachRepository mucLucNganSachRepository,
            IBhDmMucLucNganSachRepository bhDmMucLucNganSachRepository,
            ISessionService sessionService,
            ICryptographyService cryptographyService,
            ITnDanhMucLoaiHinhRepository tnDanhMucLoaiHinhRepository,
            ISktMucLucRepository iSktMucLucRepository)
        {
            _nhDaHopDongRepository = nhDaHopDongRepository;
            _nhaThauRepository = nhaThauRepository;
            _mucLucNganSachRepository = mucLucNganSachRepository;
            _cryptographyService = cryptographyService;
            _sessionService = sessionService;
            _tnDanhMucLoaiHinhRepository = tnDanhMucLoaiHinhRepository;
            _iSktMucLucRepository = iSktMucLucRepository;
            _bhDmMucLucNganSachRepository = bhDmMucLucNganSachRepository;
        }

        public ColumnAttribute GetColumnAttribute<T>(int index)
        {
            ColumnAttribute columnAttribute = typeof(T).GetProperties()
                .Where(prop => prop.GetCustomAttributes(true).Length > 0)
                .Select(prop => prop.GetCustomAttributes(true).First() as ColumnAttribute)
                .FirstOrDefault(cusProp => cusProp.ColumnIndex.Equals(index));
            return columnAttribute;
        }

        public ImportResult<T> ProcessData<T>(string fileName, bool isCheckFirstColEmpty = true)
        {
            _countAttributes = 0;
            ImportResult<T> result = new ImportResult<T>();
            result.ImportErrors = new List<ImportErrorItem>();
            result.Data = new List<T>();

            XlsFile _xlsFile = new XlsFile();
            _xlsFile.Open(fileName);

            SheetAttribute sheetAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SheetAttribute));
            int currentRow = sheetAttribute.RowStart + 1;

            foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                    if (attribute != null && attribute.ColumnIndex > _countAttributes)
                    {
                        _countAttributes = attribute.ColumnIndex;
                    }
                }
            }

            Func<DataRow, T> mapperFunction = row =>
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo importStatusProp = typeof(T).GetProperty("ImportStatus");
                System.Reflection.PropertyInfo importErrorMLNS = typeof(T).GetProperty("IsErrorMLNS");
                foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (prop.GetCustomAttributes(true).Length > 0)
                    {
                        ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                        if (attribute != null && prop.GetSetMethod() != null)
                            prop.SetValue(item, GetValueOfCell(row.Field<object>(attribute.ColumnIndex)));
                    }
                }

                int rowIndex = currentRow - sheetAttribute.RowStart - 1;
                List<ImportErrorItem> errors = ValidateItem<T>(item, rowIndex);
                if (errors.Count > 0)
                {
                    result.ImportErrors.AddRange(errors);

                    importStatusProp.SetValue(item, false);
                    if (importErrorMLNS != null && errors.Any(x => x.IsErrorMLNS))
                        importErrorMLNS.SetValue(item, true);
                }
                else importStatusProp.SetValue(item, true);

                currentRow++;
                return item;
            };
            List<T> settlementVouchers = LoadExcelDataTable(fileName, mapperFunction, sheetAttribute.ColumnStart, sheetAttribute.RowStart, sheetAttribute.SheetIndex, isCheckFirstColEmpty, false).Where(d => d != null).ToList();
            result.Data = settlementVouchers;
            return result;
        }

        public ImportResult<T> ProcessDataUnique<T>(string fileName, bool isCheckFirstColEmpty = true)
        {
            ImportResult<T> result = new ImportResult<T>();

            result.ImportErrors = new List<ImportErrorItem>();
            result.Data = new List<T>();

            XlsFile _xlsFile = new XlsFile();
            _xlsFile.Open(fileName);

            SheetAttribute sheetAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SheetAttribute));
            int currentRow = sheetAttribute.RowStart + 1;
            Func<DataRow, T> mapperFunction = row =>
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo importStatusProp = typeof(T).GetProperty("ImportStatus");
                System.Reflection.PropertyInfo importErrorMLNS = typeof(T).GetProperty("IsErrorMLNS");
                foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (prop.GetCustomAttributes(true).Length > 0)
                    {
                        ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                        if (attribute != null && prop.GetSetMethod() != null)
                            prop.SetValue(item, GetValueOfCell(row.Field<object>(attribute.ColumnIndex)));
                    }
                }

                int rowIndex = currentRow - sheetAttribute.RowStart - 1;
                List<ImportErrorItem> errors = ValidateItem<T>(item, rowIndex);
                if (errors.Count > 0)
                {
                    result.ImportErrors.AddRange(errors);

                    importStatusProp.SetValue(item, false);
                    if (importErrorMLNS != null && errors.Any(x => x.IsErrorMLNS))
                        importErrorMLNS.SetValue(item, true);
                }
                else importStatusProp.SetValue(item, true);

                currentRow++;
                return item;
            };
            List<T> settlementVouchers = LoadExcelDataTable(fileName, mapperFunction, sheetAttribute.ColumnStart, sheetAttribute.RowStart, sheetAttribute.SheetIndex, isCheckFirstColEmpty, true).Where(d => d != null).ToList();
            result.Data = settlementVouchers;
            return result;
        }

        public ImportResult<T> ProcessDataUnique<T>(MemoryStream fileName, string tokenKey, bool isCheckFirstColEmpty = true)
        {
            ImportResult<T> result = new ImportResult<T>();

            result.ImportErrors = new List<ImportErrorItem>();
            result.Data = new List<T>();

            //XlsFile _xlsFile = new XlsFile();
            //_xlsFile.Open(fileName);

            SheetAttribute sheetAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SheetAttribute));
            int currentRow = sheetAttribute.RowStart + 1;
            Func<DataRow, T> mapperFunction = row =>
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo importStatusProp = typeof(T).GetProperty("ImportStatus");
                System.Reflection.PropertyInfo importErrorMLNS = typeof(T).GetProperty("IsErrorMLNS");
                foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (prop.GetCustomAttributes(true).Length > 0)
                    {
                        ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                        if (attribute != null && prop.GetSetMethod() != null)
                            prop.SetValue(item, GetValueOfCell(row.Field<object>(attribute.ColumnIndex)));
                    }
                }

                int rowIndex = currentRow - sheetAttribute.RowStart - 1;
                List<ImportErrorItem> errors = ValidateItem<T>(item, rowIndex);
                if (errors.Count > 0)
                {
                    result.ImportErrors.AddRange(errors);

                    importStatusProp.SetValue(item, false);
                    if (importErrorMLNS != null && errors.Any(x => x.IsErrorMLNS))
                        importErrorMLNS.SetValue(item, true);
                }
                else importStatusProp.SetValue(item, true);

                currentRow++;
                return item;
            };
            List<T> settlementVouchers = LoadExcelDataTable(fileName, mapperFunction, sheetAttribute.ColumnStart, sheetAttribute.RowStart, sheetAttribute.SheetIndex, isCheckFirstColEmpty, true).Where(d => d != null).ToList();
            result.Data = settlementVouchers;
            return result;
        }

        public ImportResult<T> ProcessDataByRow<T>(string fileName, int rowStart)
        {
            ImportResult<T> result = new ImportResult<T>();
            result.ImportErrors = new List<ImportErrorItem>();
            result.Data = new List<T>();

            XlsFile _xlsFile = new XlsFile();
            _xlsFile.Open(fileName);

            SheetAttribute sheetAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SheetAttribute));
            int currentRow = rowStart + 1;
            Func<DataRow, T> mapperFunction = row =>
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo importStatusProp = typeof(T).GetProperty("ImportStatus");
                System.Reflection.PropertyInfo importErrorMLNS = typeof(T).GetProperty("IsErrorMLNS");
                foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (prop.GetCustomAttributes(true).Length > 0)
                    {
                        ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                        if (prop.GetSetMethod() != null)
                            prop.SetValue(item, GetValueOfCell(row.Field<object>(attribute.ColumnIndex)));
                    }
                }

                int rowIndex = currentRow - rowStart - 1;
                List<ImportErrorItem> errors = ValidateItem<T>(item, rowIndex);
                if (errors.Count > 0)
                {
                    result.ImportErrors.AddRange(errors);

                    importStatusProp.SetValue(item, false);
                    if (importErrorMLNS != null && errors.Any(x => x.IsErrorMLNS))
                        importErrorMLNS.SetValue(item, true);
                }
                else importStatusProp.SetValue(item, true);

                currentRow++;
                return item;
            };
            List<T> settlementVouchers = LoadExcelDataTable(fileName, mapperFunction, sheetAttribute.ColumnStart, rowStart, sheetAttribute.SheetIndex).Where(d => d != null).ToList();
            result.Data = settlementVouchers;
            return result;
        }

        public string GetValueOfCell(object cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }
            return cell.ToString();
        }

        public virtual DataTable LoadExcelDataTable(string filename, int column = 0, int row = 0, int sheet = 0, bool isCheckFirstColEmpty = true, bool isUnique = false)
        {
            Worksheet worksheet = new Workbook(filename).Worksheets[sheet];
            //Get the cells collection.
            Cells cells = worksheet.Cells;
            int totalColumns = cells.MaxDataColumn + 1;
            if (cells.MaxDataColumn < _countAttributes)
            {
                throw new Exception("Sai template");
            }
            // Get the last row index
            //int lastRow = cells.GetLastDataRow(column) + 1;
            int lastRow = (isCheckFirstColEmpty ? cells.GetLastDataRow(column) : cells.MaxDataRow) + 1;
            if (_lastRowToRead != 0 && lastRow > _lastRowToRead)
            {
                lastRow = _lastRowToRead;
            }

            int totalRows = lastRow - row;
            if (totalRows <= 0)
            {
                return new DataTable();
            }

            /*
            if (isUnique)
            {
                var xlsFile = new XlsFile(filename);
                var firstCheck = xlsFile.GetNamedRange("__Area_Items__", -1)?.Name;
                var secondCheck = xlsFile.GetNamedRange(1)?.Comment;
                var thirdCheck = xlsFile.Find("CheckSum", null, null, false, true, true, false);

                if (firstCheck == null || thirdCheck == null)
                {
                    throw new WrongReportException("Error file excel!");
                }
                if (firstCheck != "__Area_Items__" || secondCheck != "Workbook")
                {
                    throw new WrongReportException("Error file excel!");
                }
            }
            */
            return worksheet.Cells.ExportDataTable(row, column, totalRows, totalColumns, new ExportTableOptions { CheckMixedValueType = false, ExportAsString = true });
        }

        public virtual DataTable LoadExcelDataTable(MemoryStream filename, int column = 0, int row = 0, int sheet = 0, bool isCheckFirstColEmpty = true, bool isUnique = false)
        {
            Worksheet worksheet = new Workbook(filename).Worksheets[sheet];
            filename.Position = 0;
            //Get the cells collection.
            Cells cells = worksheet.Cells;
            int totalColumns = cells.MaxDataColumn + 1;
            // Get the last row index
            //int lastRow = cells.GetLastDataRow(column) + 1;
            int lastRow = (isCheckFirstColEmpty ? cells.GetLastDataRow(column) : cells.MaxDataRow) + 1;
            if (_lastRowToRead != 0 && lastRow > _lastRowToRead)
            {
                lastRow = _lastRowToRead;
            }

            int totalRows = lastRow - row;
            if (totalRows <= 0)
            {
                return new DataTable();
            }

            if (isUnique)
            {
                XlsFile xlsFile = new XlsFile();
                xlsFile.Open(filename);
            }
            return worksheet.Cells.ExportDataTable(row, column, totalRows, totalColumns, new ExportTableOptions { CheckMixedValueType = false, ExportAsString = true });
        }

        public string GetCellValue(string fileName, string sheetName, string addressName)
        {

            //Instantiating a Workbook object

            Workbook workbook = new Workbook(fileName);

            //Obtaining the reference of the Active worksheet

            Worksheet worksheet = workbook.Worksheets.GetSheetByCodeName(sheetName);

            //retrieve value from cell

            string returnValue = worksheet.Cells[addressName].Value.ToString();

            return returnValue;

        }

        public IEnumerable<T> LoadExcelDataTable<T>(MemoryStream fileName, Func<DataRow, T> convertFunction, int column = 0, int row = 0, int sheet = 0, bool isCheckFirstColEmpty = true, bool isUnique = false)
        {
            if (!isCheckFirstColEmpty)
            {
                return LoadExcelDataTable(fileName, column, row, sheet, isCheckFirstColEmpty, isUnique).AsEnumerable().Select(convertFunction).ToList();
            }
            return LoadExcelDataTable(fileName, column, row, sheet, isCheckFirstColEmpty, isUnique).AsEnumerable().Where(row => !string.IsNullOrEmpty(GetValueOfCell(row.Field<object>(column)))).Select(convertFunction).ToList();
        }

        public IEnumerable<T> LoadExcelDataTable<T>(string fileName, Func<DataRow, T> convertFunction, int column = 0, int row = 0, int sheet = 0, bool isCheckFirstColEmpty = true, bool isUnique = false)
        {
            if (!isCheckFirstColEmpty)
            {
                return LoadExcelDataTable(fileName, column, row, sheet, isCheckFirstColEmpty, isUnique).AsEnumerable().ToList().Select(convertFunction).ToList();
            }
            return LoadExcelDataTable(fileName, column, row, sheet, isCheckFirstColEmpty, isUnique).AsEnumerable().ToList().Where(row => !string.IsNullOrEmpty(GetValueOfCell(row.Field<object>(column)))).Select(convertFunction).ToList();
        }

        public List<ImportErrorItem> ValidateItem<T>(T item, int rowIndex)
        {
            List<ImportErrorItem> errors = new List<ImportErrorItem>();
            foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                    string val = prop.GetValue(item)?.ToString();
                    if (attribute == null) continue;
                    if (attribute.IsRequired)
                    {
                        if (string.IsNullOrEmpty(val))
                            errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Dữ liệu không được để trống", ColumnName = attribute.ColumnName });
                    }
                    if (attribute.DataType == ValidateType.IsIntNumber)
                    {
                        if (string.IsNullOrEmpty(val))
                        {
                            prop.SetValue(item, "0");
                            continue;
                        }
                        if (!int.TryParse(val, out int n))
                            errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Dữ liệu không đúng định dạng số", ColumnName = attribute.ColumnName });
                    }
                    if (attribute.DataType == ValidateType.IsNumber)
                    {
                        if (string.IsNullOrEmpty(val))
                        {
                            prop.SetValue(item, "0");
                            continue;
                        }
                        if (!double.TryParse(val, out double n))
                            errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Dữ liệu không đúng định dạng số", ColumnName = attribute.ColumnName });
                    }
                    if (attribute.DataType == ValidateType.IsDateTime)
                    {
                        if (string.IsNullOrEmpty(val))
                        {
                            continue;
                        }
                        if (!DateTime.TryParse(val, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out DateTime d))
                            errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Dữ liệu không đúng định dạng ngày tháng", ColumnName = attribute.ColumnName });
                    }
                    if (attribute.DataType == ValidateType.IsXauNoiMa)
                    {
                        IEnumerable<NsMucLucNganSach> mlns = _mucLucNganSachRepository.FindAll(d => d.XauNoiMa.Equals(val) && d.NamLamViec == _sessionService.Current.YearOfWork).ToList();
                        if (mlns.Count() == 0)
                            errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Mục lục ngân sách không tồn tại", ColumnName = "Mục lục ngân sách", IsErrorMLNS = true });
                    }
                    if (attribute.DataType == ValidateType.IsLoaiHinh)
                    {
                        IEnumerable<TnDanhMucLoaiHinh> mlns = _tnDanhMucLoaiHinhRepository.FindAll(d => d.Lns.Equals(val) && d.INamLamViec == _sessionService.Current.YearOfWork).ToList();
                        if (mlns.Count() == 0)
                            errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Mục lục ngân sách không tồn tại", ColumnName = "Mục lục ngân sách", IsErrorMLNS = true });
                    }
                    if (attribute.DataType == ValidateType.KyHieu)
                    {
                        IEnumerable<NsSktMucLuc> mlns = _iSktMucLucRepository.FindAll(d => d.SKyHieu.Equals(val) && d.INamLamViec == _sessionService.Current.YearOfWork).ToList();
                        if (mlns.Count() == 0)
                            errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Mục lục số kiểm tra không tồn tại", ColumnName = "Ký hiệu", IsErrorMLNS = true });
                    }
                    if (attribute.DataType == ValidateType.IsXauNoiMaBH)
                    {
                        IEnumerable<BhDmMucLucNganSach> mlns = _bhDmMucLucNganSachRepository.FindAll(d => d.SXauNoiMa.Equals(val) && d.INamLamViec == _sessionService.Current.YearOfWork).ToList();
                        if (mlns.Count() == 0)
                            errors.Add(new ImportErrorItem { Row = rowIndex, Error = "Xâu nối mã không tồn tại trong mục lục BHXH", ColumnName = "Xâu nối mã", IsErrorMLNS = true });
                    }
                }
            }
            return errors;
        }

        public virtual DataTable LoadExcelDataTable(string filename, int column = 0, int row = 0, string sheet = "")
        {
            Worksheet worksheet = new Workbook(filename).Worksheets[sheet];
            int totalColumns = worksheet.Cells.MaxDataColumn + 1;
            int lastRow = worksheet.Cells.MaxDataRow + 1;
            int totalRows = lastRow - row;
            if (totalRows <= 0)
            {
                return new DataTable();
            }
            return worksheet.Cells.ExportDataTable(row, column, totalRows, totalColumns, new ExportTableOptions { CheckMixedValueType = false, ExportAsString = true });
        }

        public IEnumerable<T> LoadExcelDataTable<T>(string fileName, Func<DataRow, T> convertFunction, int column = 0, int row = 0, string sheet = "")
        {
            return LoadExcelDataTable(fileName, column, row, sheet).AsEnumerable().Where(row => true).Select(convertFunction).ToList();
        }

        public ImportResult<T> ProcessData<T>(string fileName, string sheetName, int rowStart, List<ImportField> importFields)
        {
            ImportResult<T> result = new ImportResult<T>();
            result.ImportErrors = new List<ImportErrorItem>();
            result.Data = new List<T>();

            XlsFile _xlsFile = new XlsFile();
            _xlsFile.Open(fileName);

            int currentRow = rowStart + 1;
            Func<DataRow, T> mapperFunction = row =>
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo importStatusProp = typeof(T).GetProperty("ImportStatus");
                System.Reflection.PropertyInfo errorDetail = typeof(T).GetProperty("ErrorDetail");
                System.Reflection.PropertyInfo IsImported = typeof(T).GetProperty("IsImported");
                List<ImportErrorItem> errors = Validate<T>(row, sheetName, currentRow, importFields);
                if (errors.Count > 0)
                {
                    result.ImportErrors.AddRange(errors);

                    importStatusProp.SetValue(item, false);
                    if (IsImported != null)
                        IsImported.SetValue(item, false);
                    if (errorDetail != null)
                        errorDetail.SetValue(item, string.Join(", ", errors.Select(e => e.Error)));
                }
                else
                {
                    if (IsImported != null)
                        IsImported.SetValue(item, true);
                    importStatusProp.SetValue(item, true);
                }
                foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (Attribute.IsDefined(prop, typeof(ColumnAttribute)))
                    {
                        ImportField importField = importFields.FirstOrDefault(p => p.PropertyName.Equals(prop.Name));
                        string val;
                        try
                        {
                            val = importField.ExcelColVal.HasValue ? GetValueOfCell(row.Field<object>(importField.ExcelColVal.Value)) : string.Empty;
                        }
                        catch (Exception)
                        {
                            val = string.Empty;
                        }
                        prop.SetValue(item, val);
                    }
                }
                currentRow++;
                return item;
            };
            List<T> settlementVouchers = LoadExcelDataTable(fileName, mapperFunction, 0, rowStart, sheetName).Where(d => d != null).ToList();
            result.Data = settlementVouchers;
            return result;
        }

        public IEnumerable<T> Validate<T>(IEnumerable<T> data, List<ImportField> importFields) where T : BaseImportModel
        {
            foreach (T item in data)
            {
                item.ErrorDetail = "";
                item.ImportStatus = true;
                foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                {
                    ImportField importField = importFields.FirstOrDefault(p => p.PropertyName.Equals(prop.Name));
                    if (Attribute.IsDefined(prop, typeof(ColumnAttribute)))
                    {
                        ColumnAttribute attribute = (ColumnAttribute)Attribute.GetCustomAttribute(prop, typeof(ColumnAttribute));
                        string colName = attribute.ColumnName;
                        string val = prop.GetValue(item, null).ToString();
                        if (importField != null && importField.IsRequired)
                        {
                            attribute.IsRequired = true;
                        }
                        if (attribute.IsRequired)
                        {
                            if (string.IsNullOrEmpty(val))
                            {
                                item.ErrorDetail = colName + " không được để trống";
                                item.ImportStatus = false;
                            }
                        }
                        if (attribute.DataType == ValidateType.IsIntNumber)
                        {
                            if (string.IsNullOrEmpty(val))
                            {
                                continue;
                            }
                            if (!int.TryParse(val, out int n))
                            {
                                item.ErrorDetail = colName + " không đúng định dạng số";
                                item.ImportStatus = false;
                            }
                        }
                        if (attribute.DataType == ValidateType.IsNumber)
                        {
                            if (string.IsNullOrEmpty(val))
                            {
                                continue;
                            }
                            if (!double.TryParse(val, out double n))
                            {
                                item.ErrorDetail = colName + " không đúng định dạng số";
                                item.ImportStatus = false;
                            }
                        }
                        if (attribute.DataType == ValidateType.IsDateTime)
                        {
                            if (string.IsNullOrEmpty(val))
                            {
                                continue;
                            }
                            if (!DateTime.TryParse(val, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out DateTime d))
                            {
                                item.ErrorDetail = colName + " không đúng định dạng ngày tháng";
                                item.ImportStatus = false;
                            }
                        }
                    }
                }
            }
            return data;
        }

        public List<ImportErrorItem> Validate<T>(DataRow row, string sheet, int currentRow, List<ImportField> importFields)
        {
            List<ImportErrorItem> errors = new List<ImportErrorItem>();
            foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
            {
                ImportField importField = importFields.FirstOrDefault(p => p.PropertyName.Equals(prop.Name));
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    ColumnAttribute attribute = (ColumnAttribute)Attribute.GetCustomAttribute(prop, typeof(ColumnAttribute));
                    if (attribute == null)
                    {
                        continue;
                    }
                    string colName = attribute.ColumnName;
                    // lấy dữ liệu theo cột mà user đã chọn
                    string val;
                    try
                    {
                        val = importField.ExcelColVal.HasValue ? GetValueOfCell(row.Field<object>(importField.ExcelColVal.Value)) : string.Empty;
                    }
                    catch (Exception)
                    {
                        val = string.Empty;
                    }
                    // Nếu user chọn bắt buộc
                    if (importField != null && importField.IsRequired)
                    {
                        attribute.IsRequired = true;
                    }
                    if (attribute.IsRequired)
                    {
                        if (string.IsNullOrEmpty(val))
                            errors.Add(new ImportErrorItem { Row = currentRow, Error = colName + " không được để trống", ColumnName = attribute.ColumnName, SheetName = sheet });
                    }
                    if (attribute.DataType == ValidateType.IsIntNumber)
                    {
                        if (string.IsNullOrEmpty(val))
                        {
                            continue;
                        }
                        if (!int.TryParse(val, out int n))
                            errors.Add(new ImportErrorItem { Row = currentRow, Error = colName + " không đúng định dạng số", ColumnName = attribute.ColumnName, SheetName = sheet });
                    }
                    if (attribute.DataType == ValidateType.IsNumber)
                    {
                        if (string.IsNullOrEmpty(val))
                        {
                            continue;
                        }
                        if (!double.TryParse(val, out double n))
                            errors.Add(new ImportErrorItem { Row = currentRow, Error = colName + " không đúng định dạng số", ColumnName = attribute.ColumnName, SheetName = sheet });
                    }
                    if (attribute.DataType == ValidateType.IsDateTime)
                    {
                        if (string.IsNullOrEmpty(val))
                        {
                            continue;
                        }
                        if (!DateTime.TryParse(val, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out DateTime d))
                            errors.Add(new ImportErrorItem { Row = currentRow, Error = colName + " không đúng định dạng ngày tháng", ColumnName = attribute.ColumnName, SheetName = sheet });
                    }
                }
            }
            return errors;
        }

        public string FindCellByValue(string fileName, string value, int sheetIndex)
        {
            Workbook workbook = new Workbook(fileName);
            Worksheet worksheet = workbook.Worksheets[sheetIndex];
            Cells cells = worksheet.Cells;
            FindOptions findOptions = new FindOptions();
            findOptions.LookAtType = LookAtType.StartWith;
            Cell cell = cells.Find(value, null, findOptions);
            return cell != null ? cell.Name : string.Empty;
        }

        public void SetLastRowToRead(int value)
        {
            _lastRowToRead = value;
        }

        public List<T> GetDataJson<T>(string fileName) where T : class
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName)) return new List<T>();
            string str = File.ReadAllText(fileName);
            return JsonExtensions.ConvertJsonToList<T>(str);
        }

        public ImportResult<T1, T2, T3, T4, T5> ProcessData<T1, T2, T3, T4, T5>(string fileName = "", bool isCheckFirstColEmpty = true)
        {
            ImportResult<T1, T2, T3, T4, T5> result = new ImportResult<T1, T2, T3, T4, T5>();
            result.ImportErrors1 = new List<ImportErrorItem>();
            result.ImportErrors2 = new List<ImportErrorItem>();
            result.ImportErrors3 = new List<ImportErrorItem>();
            result.ImportErrors4 = new List<ImportErrorItem>();
            result.ImportErrors5 = new List<ImportErrorItem>();
            result.Data1 = new List<T1>();
            result.Data2 = new List<T2>();
            result.Data3 = new List<T3>();
            result.Data4 = new List<T4>();
            result.Data5 = new List<T5>();
            //var idex = result.GetType().().;
            XlsFile _xlsFile = new XlsFile();
            _xlsFile.Open(fileName);
            ImportResult<T1> result1 = MapDataSheetModel<T1>(fileName, isCheckFirstColEmpty);
            ImportResult<T2> result2 = MapDataSheetModel<T2>(fileName, isCheckFirstColEmpty);
            ImportResult<T3> result3 = MapDataSheetModel<T3>(fileName, isCheckFirstColEmpty);
            ImportResult<T4> result4 = MapDataSheetModel<T4>(fileName, isCheckFirstColEmpty);
            ImportResult<T5> result5 = MapDataSheetModel<T5>(fileName, isCheckFirstColEmpty);
            result.ImportErrors1 = result1.ImportErrors;
            result.Data1 = result1.Data;
            result.ImportErrors2 = result2.ImportErrors;
            result.Data2 = result2.Data;
            result.ImportErrors3 = result3.ImportErrors;
            result.Data3 = result3.Data;
            result.ImportErrors4 = result4.ImportErrors;
            result.Data4 = result4.Data;
            result.ImportErrors5 = result5.ImportErrors;
            result.Data5 = result5.Data;
            return result;

        }

        private ImportResult<T> MapDataSheetModel<T>(string fileName = "", bool isCheckFirstColEmpty = true)
        {
            ImportResult<T> result = new ImportResult<T>();
            SheetAttribute sheetAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SheetAttribute));
            int currentRow = sheetAttribute.RowStart + 1;
            Func<DataRow, T> mapperFunction = row =>
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo importStatusProp = typeof(T).GetProperty("ImportStatus");
                System.Reflection.PropertyInfo importErrorMLNS = typeof(T).GetProperty("IsErrorMLNS");
                foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (prop.GetCustomAttributes(true).Length > 0)
                    {
                        ColumnAttribute attribute = prop.GetCustomAttributes(true).First() as ColumnAttribute;
                        if (attribute != null && prop.GetSetMethod() != null)
                            prop.SetValue(item, GetValueOfCell(row.Field<object>(attribute.ColumnIndex)));
                    }
                }

                int rowIndex = currentRow - sheetAttribute.RowStart - 1;
                List<ImportErrorItem> errors = ValidateItem<T>(item, rowIndex);
                if (errors.Count > 0)
                {
                    result.ImportErrors.AddRange(errors);

                    importStatusProp.SetValue(item, false);
                    if (importErrorMLNS != null && errors.Any(x => x.IsErrorMLNS))
                        importErrorMLNS.SetValue(item, true);
                }
                else importStatusProp.SetValue(item, true);

                currentRow++;
                return item;
            };
            List<T> settlementVouchers = LoadExcelDataTable(fileName, mapperFunction, sheetAttribute.ColumnStart, sheetAttribute.RowStart, sheetAttribute.SheetIndex, isCheckFirstColEmpty, false).Where(d => d != null).ToList();
            result.Data = settlementVouchers;
            return result;
        }

    }
}
