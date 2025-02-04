using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQtThongTriLNSQuery
    {
        [Column("iID_MLNS")]
        public Guid MLNS_Id { get; set; }
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
        [Column("sTNG1")]
        public string TNG1 { get; set; }
        [Column("sTNG2")]
        public string TNG2 { get; set; }
        [Column("sTNG3")]
        public string TNG3 { get; set; }
        [Column("sXauNoima")]
        public string XauNoiMa { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        public bool IsHangCha { get; set; }
        public double? TuChi { get; set; }
        public double? HienVat { get; set; }
        public double? SoNguoi { get; set; }
        public double? SoNgay { get; set; }
        public double? SoLuot { get; set; }
        [Column("MaDonVi")]
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
    }
}
