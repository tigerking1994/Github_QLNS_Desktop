using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class ImportResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public List<ImportErrorItem> ImportErrors { get; set; } = new List<ImportErrorItem>();
    }
    public class ImportResult<T1, T2, T3, T4, T5>
    {
        public List<T1> Data1 { get; set; }
        public List<T2> Data2 { get; set; }
        public List<T3> Data3 { get; set; }
        public List<T4> Data4 { get; set; }
        public List<T5> Data5 { get; set; }
        public List<ImportErrorItem> ImportErrors1 { get; set; }
        public List<ImportErrorItem> ImportErrors2 { get; set; }
        public List<ImportErrorItem> ImportErrors3 { get; set; }
        public List<ImportErrorItem> ImportErrors4 { get; set; }
        public List<ImportErrorItem> ImportErrors5 { get; set; }
    }
}
