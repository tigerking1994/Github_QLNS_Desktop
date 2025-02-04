using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsDcChungTuChiTietQuery
    {
        [Column("iID_DCCTChiTiet")]
        public Guid Id { get; set; }
        [Column("iID_DCChungTu")]
        public Guid? IIdDcchungTu { get; set; }
        [Column("iID_MLNS")]
        public Guid IIdMlns { get; set; }
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
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("DuToanNganSachNam")]
        public double? FDuToanNganSachNam { get; set; }
        [Column("DuToanChuyenNamSau")]
        public double? FDuToanChuyenNamSau { get; set; }
        [Column("DuKienQtDauNam")]
        public double? FDuKienQtDauNam { get; set; }
        [Column("DuKienQtCuoiNam")]
        public double? FDuKienQtCuoiNam { get; set; }
        [Column("sChiTietToi")]
        public string SChiTietToi { get; set; }
        [Column("bHangChaDuToan")]
        public bool BHangChaDuToan { get; set; }

        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("iLoaiDuKien")]
        public int ILoaiDuKien { get; set; }
        [NotMapped]
        public bool HasDataSummary => FDuToanNganSachNam.GetValueOrDefault() != 0 
            || FDuToanChuyenNamSau.GetValueOrDefault() != 0
            || FDuKienQtDauNam.GetValueOrDefault() != 0 
            || FDuKienQtCuoiNam.GetValueOrDefault() != 0;
    }
}
