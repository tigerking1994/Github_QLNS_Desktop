using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class SktChungTuChiTietQuery
    {
        public string MoTa { get; set; }
        public Guid? IdParent { get; set; }
        public Guid IdMucLuc { get; set; }
        public string Stt { get; set; }
        public string KyHieu { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        public double TotalTuChi { get; set; }
        public double TotalHuyDong { get; set; }
        public double TotalPhanCap { get; set; }
        public double TotalMuaHangHienVat { get; set; }
        [NotMapped]
        public int Level { get; set; }
        [NotMapped]
        public double TotalNSSD { get; set; }
        [NotMapped]
        public double TotalNSBD { get; set; }
        [Column("soLieuCot1")]
        public double SoLieuCot1 { get; set; }
        [Column("soLieuCot2")]
        public double SoLieuCot2 { get; set; }
        public double ChenhLech => SoLieuCot2 - SoLieuCot1;
        [Column("iID_MLSKT")]
        public Guid IIdMlskt { get; set; }
        [Column("iID_MLSKTCha")]
        public Guid IIdParentMlskt { get; set; }
        [Column("sL")]
        public string SL { get; set; }
        [Column("sK")]
        public string SK { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sNG")]
        public string SNG { get; set; }
        public bool IsMaDonVi { get; set; }
    }
}
