using System;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class ColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public int ColumnIndex { get; set; }
        public bool IsRequired { get; set; }
        public ValidateType DataType { get; set; }
        public string DBCol { get; set; }
        public string Description { get; set; }
        public MLNSFiled MlnsFiled { get; set; }

        public ColumnAttribute(string columnName, int columnIndex, ValidateType dataType = ValidateType.IsString, bool isRequired = false)
        {
            ColumnName = columnName;
            ColumnIndex = columnIndex;
            IsRequired = isRequired;
            DataType = dataType;
        }

        public ColumnAttribute(int columnIndex, ValidateType dataType = ValidateType.IsString, bool isRequired = false)
        {
            ColumnIndex = columnIndex;
            IsRequired = isRequired;
            DataType = dataType;
        }

        public ColumnAttribute(ValidateType dataType = ValidateType.IsString)
        {
            DataType = dataType;
        }
        
        public ColumnAttribute(string columnName, int columnIndex, string dbCol, string description, ValidateType dataType = ValidateType.IsString, bool isRequired = false)
        {
            ColumnName = columnName;
            ColumnIndex = columnIndex;
            IsRequired = isRequired;
            DataType = dataType;
            DBCol = dbCol;
            Description = description;
        }

        public ColumnAttribute(string columnName, int columnIndex, MLNSFiled mlnsFiled)
        {
            ColumnName = columnName;
            ColumnIndex = columnIndex;
            MlnsFiled = mlnsFiled;
        }
    }
}
