using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQtDuToanQuyetToanThangQuery
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
        public double ChiTieu { get; set; }
        public double TuChi { get; set; }
        public double Thang1 { get; set; }
        public double Thang2 { get; set; }
        public double Thang3 { get; set; }
        public double Thang4 { get; set; }
        public double Thang5 { get; set; }
        public double Thang6 { get; set; }
        public double Thang7 { get; set; }
        public double Thang8 { get; set; }
        public double Thang9 { get; set; }
        public double Thang10 { get; set; }
        public double Thang11 { get; set; }
        public double Thang12 { get; set; }
        public string TenDonVi { get; set; }

        [NotMapped]
        public double SoConLai
        {
            get => ChiTieu - TuChi;
        }

        [NotMapped]
        public double TiLe
        {
            get => ChiTieu == 0 ? 0 : TuChi * 100 / ChiTieu;
        }
        [NotMapped]
        public bool HasData => ChiTieu != 0 || TuChi != 0
                            || Thang1 != 0 || Thang2 != 0 || Thang3 != 0 || Thang4 != 0
                            || Thang5 != 0 || Thang6 != 0 || Thang7 != 0 || Thang8 != 0
                            || Thang9 != 0 || Thang10 != 0 || Thang11 != 0 || Thang12 != 0;
        [NotMapped]
        public double ChiTieuOrigin => ChiTieu;
    }
}
