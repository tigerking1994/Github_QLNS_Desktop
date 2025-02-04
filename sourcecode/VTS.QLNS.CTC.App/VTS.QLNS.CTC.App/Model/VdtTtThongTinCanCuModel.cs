using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtThongTinCanCuModel : ModelBase
    {        
        public int? iSTT { get; set; }
        public Guid? iID_DeNghiThanhToanID { get; set; }
        public string sNoiDung { get; set; }
        public double? fGiaTri { get; set; }
        public DateTime? dDateCreate { get; set; }
        public DateTime? dDateUpdate { get; set; }
    }
}
