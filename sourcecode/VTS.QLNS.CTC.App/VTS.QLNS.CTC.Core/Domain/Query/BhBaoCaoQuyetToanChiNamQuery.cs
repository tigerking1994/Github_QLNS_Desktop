using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhBaoCaoQuyetToanChiNamQuery
    {
        [Column("sTT")]
        public string STT { get; set; }

        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }

        [Column("IID_MaDonVi")]
        public string iID_MaDonVi { get; set; }

        [Column("iKhoi")]
        public int IKhoi { get; set; }

        [Column("sLNS")]
        public string SLNS { get; set; }

        [Column("fTroCapOmDau")]
        public Double? FTroCapOmDau { get; set; }

        [Column("fTroCapThaiSan")]
        public Double? FTroCapThaiSan { get; set; }

        [Column("fTroCapTaiNanNN")]
        public Double? FTroCapTaiNanNN { get; set; }

        [Column("fTroCapHuuTri")]
        public Double? FTroCapHuuTri { get; set; }

        [Column("fTroCapPhucVien")]
        public Double? FTroCapPhucVien { get; set; }

        [Column("fTroCapXuatNgu")]
        public Double? FTroCapXuatNgu { get; set; }

        [Column("fTroCapThoiViec")]
        public Double? FTroCapThoiViec { get; set; }

        [Column("fTroCapTuTuat")]
        public Double? FTroCapTuTuat { get; set; }

        [Column("level")]
        public int Level { get; set; }

        [Column("bHangCha")]
        public bool BHangCha { get; set; }

        [NotMapped]
        public Double? FTongCong => (FTroCapOmDau ?? 0) + (FTroCapThaiSan ?? 0) + (FTroCapTaiNanNN ?? 0) + (FTroCapHuuTri ?? 0) + (FTroCapPhucVien ?? 0) + (FTroCapXuatNgu ?? 0)
           + (FTroCapThoiViec ?? 0) + (FTroCapTuTuat ?? 0);
    }

    public class BaoCaoNhanVaQuyetToanKinhPhi
    {
        public int IMa { get; set; }
        public int IMaCha { get; set; }
        public int IKieuChu { get; set; }
        public string STT { get; set; }
        public string NoiDung { get; set; }
        public double FDuToanNamTruoc { get; set; }
        public double FDuToanNamNay { get; set; }
        public double FKinhPhiNamTruoc { get; set; }
        public double FKinhPhiNamNay { get; set; }
        public double FSoDeNghi { get; set; }
        public string SMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public bool IsFillData { get; set; }
    }
}
