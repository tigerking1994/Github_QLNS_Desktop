using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsDtChungTuChiTietCongKhaiQuery
    {
        [Column("sSTT")]
        public string STT { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        [Column("fDuToanDuocGiao")]
        public double DuToanDuocGiao { get; set; }
        [Column("fSoChuaPhanBo")]
        public double SoChuaPhanBo { get; set; }
        [Column("fSoPhanBo")]
        public double SoPhanBo { get; set; }
        [Column("iID_DMCongKhai")]
        public Guid ID_DMCongKhai { get; set; }
        [Column("iID_MaDonVi")]
        public string MaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string TenDonVi { get; set; }
        [Column("iID_DMCongKhai_Cha")]
        public Guid iID_DMCongKhai_Cha { get; set; }
    }
}
