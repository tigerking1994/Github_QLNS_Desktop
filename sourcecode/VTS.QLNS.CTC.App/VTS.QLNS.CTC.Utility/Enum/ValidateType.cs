using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public enum ValidateType
    {
        IsRequired,
        IsIntNumber,
        IsNumber,
        IsDateTime,
        IsString,
        IsLns,
        IsXauNoiMa,
        IsLoaiHinh,
        IsMaDuAn,
        KyHieu,
        IsXauNoiMaBH
    }

    public class ValidationRule
    {
        public ValidateType validateType { get; set; }
        public string errMsg { get; set; }
    }
}
