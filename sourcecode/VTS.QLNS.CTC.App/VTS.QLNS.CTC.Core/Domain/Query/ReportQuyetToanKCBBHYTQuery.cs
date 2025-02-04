using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQuyetToanKCBBHYTQuery
    {
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string SXauNoiMa { get; set; }
        public string SMoTa { get; set; }
        public Guid IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }
        public string IID_MaCoSoYTe { get; set; }
        public string STenCoSoYTe { get; set; }
        public double? FQuyetToanQuyNay { get; set; }


    }
}
