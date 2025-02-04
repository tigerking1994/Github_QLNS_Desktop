using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtThongTinCanCu : EntityBase
    {        
        public int? iSTT { get; set; }
        public Guid? iID_DeNghiThanhToanID { get; set; }
        public string sNoiDung { get; set; }
        public double? fGiaTri { get; set; }
        public DateTime? dDateCreate { get; set; }
        public DateTime? dDateUpdate { get; set; }
    }
}
