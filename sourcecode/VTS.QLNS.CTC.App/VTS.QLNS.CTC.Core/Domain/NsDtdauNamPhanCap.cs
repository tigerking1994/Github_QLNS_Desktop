using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsDtdauNamPhanCap : EntityBase
    {
        [Column("iID_DTDauNam_PhanCap")]
        public override Guid Id { get; set; }
        //public Guid IIdDtdauNamPhanCap { get; set; }
        public Guid IIdCtdtdauNamChiTiet { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIdMlns { get; set; }
        public double? FTuChi { get; set; }
        public string SXauNoiMa { get; set; }
        public string SGhiChu { get; set; }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public Guid? IIdCtdtDauNam { get; set; }
        public string sXauNoiMaGoc { get; set; }

        [NotMapped]
        public bool ImportStatus { get; set; }

    }
}
