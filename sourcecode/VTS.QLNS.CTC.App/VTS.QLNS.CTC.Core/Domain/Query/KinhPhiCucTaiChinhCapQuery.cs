using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KinhPhiCucTaiChinhCapQuery
    {
        public double fQuyTruocChuaGiaiNgan { get; set; }
        public double fQuyNayDuocCap { get; set; }
        public double fGiaiNganQuyNay { get; set; }
        public double fChuaGiaiNganChuyenQuySau 
        { 
            get
            {
                return fQuyTruocChuaGiaiNgan + fQuyNayDuocCap - fGiaiNganQuyNay;
            }
        }
    }
}
