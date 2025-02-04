using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class MlnsByKeHoachVonQuery
    {
        public Guid IidKeHoachVonId { get; set; }
        public int ILoaiKeHoachVon { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string SMoTa { get; set; }
        public string SXauNoiMa
        {
            get
            {
                if (!string.IsNullOrEmpty(NG))
                    return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", LNS, L, K, M, TM, TTM, NG);
                else if(!string.IsNullOrEmpty(TTM))
                    return string.Format("{0}-{1}-{2}-{3}-{4}-{5}", LNS, L, K, M, TM, TTM);
                else if (!string.IsNullOrEmpty(TM))
                    return string.Format("{0}-{1}-{2}-{3}-{4}", LNS, L, K, M, TM);
                else if (!string.IsNullOrEmpty(M))
                    return string.Format("{0}-{1}-{2}-{3}", LNS, L, K, M);
                return string.Empty;
            }
        }
    }
}
