using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBHChungTuCapPhatThongTriQuery
    {
        public Guid IdMlns { get; set; }
        public Guid? IdMlnsCha { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        [NotMapped]
        public string SLK => string.IsNullOrEmpty(SL) && string.IsNullOrEmpty(SK) ? string.Empty : SL + StringUtils.DIVISION + SK;
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string SXauNoiMa { get; set; }
        public double FTienKeHoach { get; set; }
        public string SMoTa { get; set; }
        public bool IsHangCha { get; set; }
        public string SDuToanChiTietToi { get;set; }
    }
}
