using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmDonVi : EntityBase
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string ParentId { get; set; }
        public string XauNoiMa { get; set; }
        public bool? ITrangThai { get; set; }

        [NotMapped]
        public string TenDonViCha { get; set; }
        [NotMapped]
        public bool BHangCha { get; set; }
        public ICollection<TlDmCanBo> TlDmCanBos { get; set; }
        public ICollection<TlDsCapNhapBangLuong> TlDsCapNhapBangLuongs { get; set; }
        public ICollection<TlQtChungTu> TlQtChungTus { get; set; }
        public ICollection<TlQtChungTuNq104> TlQtChungTusNq104 { get; set; }
        public ICollection<TlQsChungTu> TlQsChungTus { get; set; }
    }
}
