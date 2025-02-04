using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsMucLucNganSachQuery : NsMucLucNganSach
    {
        [Column("iID")]
        public new Guid Id { get; set; }
        [Column("iID_MLNS")]
        public new Guid MlnsId { get; set; }
        [Column("iID_MLNS_Cha")]
        public new Guid? MlnsIdParent { get; set; }
        [Column("sXauNoiMa")]
        public new string XauNoiMa { get; set; }
        [Column("sLNS")]
        public new string Lns { get; set; }
        [Column("sL")]
        public new string L { get; set; }
        [Column("sK")]
        public new string K { get; set; }
        [Column("sM")]
        public new string M { get; set; }
        [Column("sTM")]
        public new string Tm { get; set; }
        [Column("sTTM")]
        public new string Ttm { get; set; }
        [Column("sNG")]
        public new string Ng { get; set; }
        [Column("sTNG")]
        public new string Tng { get; set; }
        [Column("sTNG1")]
        public new string Tng1 { get; set; }
        [Column("sTNG2")]
        public new string Tng2 { get; set; }
        [Column("sTNG3")]
        public new string Tng3 { get; set; }
        [Column("sMoTa")]
        public new string MoTa { get; set; }
        [Column("iNamLamViec")]
        public new int? NamLamViec { get; set; }
        [Column("bHangCha")]
        public new bool BHangCha { get; set; }
        [Column("iTrangThai")]
        public new int? ITrangThai { get; set; }
        [Column("iID_MaBQuanLy")]
        public new string IdPhongBan { get; set; }
        [Column("dNgayTao")]
        public new DateTime? DNgayTao { get; set; }
        [Column("sNguoiTao")]
        public new string SNguoiTao { get; set; }
        [Column("dNgaySua")]
        public new DateTime? DNgaySua { get; set; }
        [Column("sNguoiSua")]
        public new string SNguoiSua { get; set; }
        [Column("Tag")]
        public new string Tag { get; set; }
        [Column("Log")]
        public new string Log { get; set; }
        [Column("iLock")]
        public new bool? ILock { get; set; }
        [Column("iLoai")]
        public new string ILoai { get; set; }
        [Column("sChiTietToi")]
        public new string ChiTietToi { get; set; }
        [Column("bNgay")]
        public new bool BNgay { get; set; }
        [Column("bSoNguoi")]
        public new bool BSoNguoi { get; set; }
        [Column("bTonKho")]
        public new bool BTonKho { get; set; }
        [Column("bTuChi")]
        public new bool BTuChi { get; set; }
        [Column("bHangNhap")]
        public new bool BHangNhap { get; set; }
        [Column("bHangMua")]
        public new bool BHangMua { get; set; }
        [Column("bHienVat")]
        public new bool BHienVat { get; set; }
        [Column("bDuPhong")]
        public new bool BDuPhong { get; set; }
        [Column("bPhanCap")]
        public new bool BPhanCap { get; set; }
        [Column("sNhapTheoTruong")]
        public new string SNhapTheoTruong { get; set; }
        [Column("iID_MaDonVi")]
        public new string IdMaDonVi { get; set; }
        [Column("sCPChiTietToi")]
        public new string SCPChiTietToi { get; set; }
        [Column("bHangChaDuToan")]
        public new bool? BHangChaDuToan { get; set; }
        [Column("bHangChaQuyetToan")]
        public new bool? BHangChaQuyetToan { get; set; }
        [Column("sQuyetToanChiTietToi")]
        public new string SQuyetToanChiTietToi { get; set; }
        [Column("sDuToanChiTietToi")]
        public new string SDuToanChiTietToi { get; set; }
        [Column("MlnsParentName")]
        public string MlnsParentName { get; set; }
        [Column("UsedCPChiTietToi")]
        public string UsedCPChiTietToi { get; set; }
        [Column("UsedDuToanChiTietToi")]
        public string UsedDuToanChiTietToi { get; set; }
        [Column("UsedQuyetToanChiTietToi")]
        public string UsedQuyetToanChiTietToi { get; set; }
        [Column("UsedMLNS")]
        public Guid? UsedMLNS { get; set; }
        [Column("LNSID")]

        public Guid? LNSID { get; set; }
        public new bool IsEditableCPChiTietToi { get; set; }
        public new bool IsUsedDuToanChiTietToi { get; set; }
        public new bool IsUsedQuyetToanChiTietToi { get; set; }
        public new bool IsEditableStatus { get; set; }
        public string SktKyHieu { get; set; }
    }
}
