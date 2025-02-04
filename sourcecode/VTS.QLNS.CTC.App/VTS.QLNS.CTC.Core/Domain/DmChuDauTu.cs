using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class DmChuDauTu : EntityBase
    {
        public Guid? IIDDonViCha { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SKyHieu { get; set; }
        public string SMoTa { get; set; }
        public int? INamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public string Loai { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public bool BHangCha { get; set; }
        public string MaSoDVSDNS { get; set; }
        public string STKTrongNuoc { get; set; }
        public string ChiNhanhTrongNuoc { get; set; }
        public string  STKNuocNgoai { get; set; }
        public string ChiNhanhNuocNgoai { get; set; }
        [NotMapped]
        public string TenCdtParent { get; set; }
    }
}
