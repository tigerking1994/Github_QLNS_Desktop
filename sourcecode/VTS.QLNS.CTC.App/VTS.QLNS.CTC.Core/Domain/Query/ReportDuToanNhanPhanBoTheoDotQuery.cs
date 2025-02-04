using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDuToanNhanPhanBoTheoDotQuery
    {
        [Column("iID_MLNS")]
        public Guid? IIdMlns { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIdMlnsCha { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        public string SLns { get; set; }
        [Column("sL")]
        public string SL { get; set; }
        [Column("sK")]
        public string SK { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STm { get; set; }
        [Column("sTTM")]
        public string STtm { get; set; }
        [Column("sNG")]
        public string SNg { get; set; }
        [Column("sTNG")]
        public string STng { get; set; }
        [Column("sTNG1")]
        public string STng1 { get; set; }
        [Column("sTNG2")]
        public string STng2 { get; set; }
        [Column("sTNG3")]
        public string STng3 { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("iNamNganSach")]
        public int? INamNganSach { get; set; }
        [Column("iID_MaNguonNganSach")]
        public int? IIdMaNguonNganSach { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("iPhanCap")]
        public int IPhanCap { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("fTuChi")]
        public double FTuChi { get; set; }
        [Column("fHienVat")]
        public double FHienVat { get; set; }
        [Column("fHangNhap")]
        public double FHangNhap { get; set; }
        [Column("fHangMua")]
        public double FHangMua { get; set; }
        [Column("fPhanCap")]
        public double FPhanCap { get; set; }
        [Column("fDuPhong")]
        public double FDuPhong { get; set; }
        [Column("iID_CTDuToan_Nhan")]
        public Guid? IIdCtduToanNhan { get; set; }
        [Column("iDuLieuNhan")]
        public int IDuLieuNhan { get; set; }
        [Column("sChiTietToi")]
        public string SChiTietToi { get; set; }
        [Column("bHangChaDuToan")]
        public bool BHangChaDuToan { get; set; }
        [NotMapped]
        public bool IsEditTuChi { get; set; }
        [NotMapped]
        public bool IsEditHienVat { get; set; }
        [NotMapped]
        public bool IsEditHangNhap { get; set; }
        [NotMapped]
        public bool IsEditHangMua { get; set; }
        [NotMapped]
        public bool IsEditDuPhong { get; set; }
        [NotMapped]
        public bool IsEditPhanCap { get; set; }
    }
}
