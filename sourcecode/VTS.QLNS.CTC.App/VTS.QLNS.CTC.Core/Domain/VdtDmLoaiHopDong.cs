using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmLoaiHopDong : EntityBase
    {
        [Column("iID_LoaiHopDongID")]
        public override Guid Id { get; set; }
        public string SMaLoaiHopDong { get; set; }
        public string STenVietTat { get; set; }
        public string STenLoaiHopDong { get; set; }
        public string SMoTa { get; set; }
        public int? IThuTu { get; set; }
    }
}
