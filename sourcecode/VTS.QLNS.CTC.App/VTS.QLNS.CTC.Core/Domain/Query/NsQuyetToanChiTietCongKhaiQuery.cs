using System;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsQuyetToanChiTietCongKhaiQuery
    {
        [Column("sSTT")]
        public string STT { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        [Column("sMa")]
        public string SMa { get; set; }
        [Column("sMaCha")]
        public string SMaCha { get; set; }
        [Column("fDuToanDuocGiao")]
        public double DuToanDuocGiao { get; set; }
        [Column("fTuChiNamTruoc")]
        public double TuChiDuocDuyetNamTruoc { get; set; }
        [Column("fTuChiNamNay")]
        public double TuChiDuocDuyetNamNay { get; set; }
        [Column("fTiLeDuToan")]
        public double TiLeDuToan { get; set; }
        [Column("fTiLeSoVoiNamTruoc")]
        public double TiLeSoVoiNamTruoc { get; set; }
        [Column("iID_DMCongKhai")]
        public Guid ID_DMCongKhai { get; set; }
        [Column("iID_MaDonVi")]
        public string MaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string TenDonVi { get; set; }
        [Column("iID_DMCongKhai_Cha")]
        public Guid IID_DMCongKhai_Cha { get; set; }
        public bool bHangCha { get; set; }
    }
}
