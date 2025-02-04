using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhdtcnpbMapBHXH : EntityBase
    {
        [Column("iID_DTCNhanPhanBoMap")]
        public override Guid Id { get; set; }
        public Guid iID_BHDTC_NhanPhanBo { get; set; }
        public Guid iID_BHDTC_PhanBo { get; set; }
        public DateTime? dNgayTao { get; set; }
        public string sNguoiTao { get; set; }
        public DateTime? dNgaySua { get; set; }
        public string sNguoiSua { get; set; }

    }
}
