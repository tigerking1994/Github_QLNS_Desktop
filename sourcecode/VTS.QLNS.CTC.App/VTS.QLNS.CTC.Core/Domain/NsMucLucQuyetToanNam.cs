using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{

    public partial class NsMucLucQuyetToanNam : EntityBase
    {
        [Column("iID")]
        [Key]
        public override Guid Id { get; set; }
        public string Ma { get; set; }
        public string MaCha { get; set; }
        public string STT { get; set; }
        public string MoTa { get; set; }
        public int NamLamViec { get; set; }
        public bool BHangCha { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        [NotMapped]
        public virtual ICollection<NsMucLucQuyetToanNamMLNS> NsMucLucQuyetToanNamMLNS { get; set; }

    }
}
