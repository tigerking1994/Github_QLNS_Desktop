using System;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportCapPhatThongTriQuery
    {
        public Guid IdMlns { get; set; }
        public Guid? IdMlnsCha { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        [NotMapped]
        public string LK => string.IsNullOrEmpty(L) && string.IsNullOrEmpty(K) ? string.Empty : L + StringUtils.DIVISION + K;
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string XauNoiMa { get; set; }
        public double TuChi { get; set; }
        public string MoTa { get; set; }
        public bool IsHangCha { get; set; }
    }
}
