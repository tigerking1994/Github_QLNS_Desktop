using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBhDtCtctKPQLQuery
    {
        public string STT { get; set; }
        public string SNoiDung { get; set; }
        public Guid IID_MLNS { get;set; }
        public Guid IID_MLNS_Cha { get; set; }
        public double? FSoTien { get;set; }
        public bool BHangCha { get;set; }
        public string SXauNoiMa { get;set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SLNS { get; set; }
    }
}
