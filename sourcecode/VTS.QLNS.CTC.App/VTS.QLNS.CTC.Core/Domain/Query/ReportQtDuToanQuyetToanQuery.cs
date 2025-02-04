using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQtDuToanQuyetToanQuery
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
        [Column("sXauNoiMa")]
        public string XauNoiMa { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        public bool IsHangCha { get; set; }
        public bool BHangChaDuToan { get; set; }
        public bool BHangChaQuyetToan { get; set; }
        public double ChiTieu { get; set; }
        public int SoNguoi { get; set; }
        public int SoNgay { get; set; }
        public double TuChi2 { get; set; }
        public double TuChi { get; set; }
        public string TenDonVi { get; set; }
        [NotMapped]
        public double? DenKyNay
        {
            get => TuChi2 + TuChi;
        }

        [NotMapped]
        public double? SoConLai
        {
            get => ChiTieu - TuChi2 - TuChi;
        }

        [NotMapped]
        public double TiLe
        {
            get => ChiTieu != 0 ? (TuChi2 + TuChi) * 100 / ChiTieu : 0;
        }
        [NotMapped]
        public bool HasData => ChiTieu != 0 || TuChi2 != 0 || TuChi != 0;
        [NotMapped]
        public double ChiTieuOrigin => ChiTieu;
    }
}
