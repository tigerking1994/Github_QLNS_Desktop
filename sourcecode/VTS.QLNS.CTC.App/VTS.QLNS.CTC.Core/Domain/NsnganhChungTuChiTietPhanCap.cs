using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsNganhChungTuChiTietPhanCap : EntityBase
    {
        public override Guid Id { get; set; }
        public Guid IIdCtnganhChiTiet { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIdMlns { get; set; }
        public double? FPhanCap { get; set; }
        public double? FHienVat { get; set; }
        public string SXauNoiMa { get; set; }
        public string SGhiChu { get; set; }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
    }
}

