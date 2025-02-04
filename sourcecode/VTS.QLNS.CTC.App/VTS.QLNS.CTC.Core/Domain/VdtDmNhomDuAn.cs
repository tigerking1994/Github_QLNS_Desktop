using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmNhomDuAn: EntityBase
    {
        [Column("iID_NhomDuAnID")]
        public override Guid Id { get; set; }
        public string SMaNhomDuAn { get; set; }
        public string STenVietTat { get; set; }
        public string STenNhomDuAn { get; set; }
        public string SMoTa { get; set; }
        public int INamThucHien { get; set; }
        public int? IThuTu { get; set; }
        public decimal? MTien { get; set; }
    }
}
