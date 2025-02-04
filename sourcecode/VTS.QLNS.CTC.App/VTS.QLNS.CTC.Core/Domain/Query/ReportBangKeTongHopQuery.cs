using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBangKeTongHopQuery
    {
        [Column("iID_MLNS")]
        public Guid? MLNS_Id { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? MLNS_Id_Parent { get; set; }
        [Column("sLNS")]
        public string LNS { get; set; }
        [Column("sL")]
        public string L { get; set; }
        [Column("sK")]
        public string K { get; set; }
        [Column("sM")]
        public string M { get; set; }
        [Column("sTM")]
        public string TM { get; set; }
        [Column("sTTM")]
        public string TTM { get; set; }
        [Column("sNG")]
        public string NG { get; set; }
        [Column("sTNG")]
        public string TNG { get; set; }
        [Column("sXauNoiMa")]
        public string XauNoiMa { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public bool IsHangCha { get; set; }
        public string SoChungTu { get; set; }
        public string NgayChungTu { get; set; }
        public string SoQuyetDinh { get; set; }
        public string TenDonVi { get; set; }
        public double? TuChi { get; set; }
        public double? HienVat { get; set; }
        public string SLoai { get; set; }
        public int? ILoaiChi { get; set; }
    }
}
