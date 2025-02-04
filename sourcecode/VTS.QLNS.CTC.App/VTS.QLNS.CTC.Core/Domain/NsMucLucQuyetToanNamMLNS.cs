using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{

    public partial class NsMucLucQuyetToanNamMLNS : EntityBase
    {
        [Column("iID")]
        [Key]
        public override Guid Id { get; set; }
        public string MaMLQT { get; set; }
        public int NamLamViec { get; set; }
        public string XauNoiMa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
    }
}
