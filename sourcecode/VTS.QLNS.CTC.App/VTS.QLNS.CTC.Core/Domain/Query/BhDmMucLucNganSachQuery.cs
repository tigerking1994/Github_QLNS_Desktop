using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDmMucLucNganSachQuery : BhDmMucLucNganSach
    {
        [Column("iID")]
        [Key]
        public new Guid Id { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        [StringLength(50)]
        public new string SLNS { get; set; }
        [Column("sL")]
        public new string SL { get; set; }
        [Column("sK")]
        public new string SK { get; set; }

        [Column("sM")]
        public new string SM { get; set; }
        [Column("sTM")]
        public new string STM { get; set; }
        [Column("sTTM")]
        public new string STTM { get; set; }
        [Column("sNG")]
        public new string SNG { get; set; }
        [Column("sTNG")]
        public new string STNG { get; set; }
        [Column("sMoTa")]
        public new string SMoTa { get; set; }
        [Column("bHangCha")]
        public new bool BHangCha { get; set; }
        [Column("iTrangThai")]
        public new int? ITrangThai { get; set; }
        [Column("MlnsParentName")]
        public string MlnsParentName { get; set; }
        [Column("bDuPhong")]
        public new bool BDuPhong { get; set; }
        [StringLength(50)]
        [Column("bHangChaDuToan")]
        public new bool? BHangChaDuToan { get; set; }
        [Column("bHangChaQuyetToan")]
        public new bool? BHangChaQuyetToan { get; set; }
        [StringLength(50)]
        [Column("bHangMua")]
        public new bool BHangMua { get; set; }
        [StringLength(250)]
        [Column("bHangNhap")]
        public new bool BHangNhap { get; set; }
        [Column("bHienVat")]
        public new bool BHienVat { get; set; }

        [Column("bNgay")]
        public new bool BNgay { get; set; }
        [Column("bPhanCap")]
        public new bool BPhanCap { get; set; }
        [Column("bSoNguoi")]
        public new bool BSoNguoi { get; set; }
        [Column("bTonKho")]
        public new bool BTonKho { get; set; }
        [Column("bTuChi")]
        public new bool BTuChi { get; set; }
        [Column("sChiTietToi")]
        public new string SChiTietToi { get; set; }
        [Column("dNgaySua")]
        public new DateTime? DNgaySua { get; set; }
        [Column("dNgayTao")]
        public new DateTime? DNgayTao { get; set; }
        [Column("iLoai")]
        public new string ILoai { get; set; }
        [Column("iLock")]
        public new bool? ILock { get; set; }
        [Column("iID_MaDonVi")]
        public new string IIDMaDonVi { get; set; }
        [Column("iID_MaBQuanLy")]
        public new string IIDMaBQuanLy { get; set; }
        [Column("[Log]")]
        public new string Log { get; set; }
        [Column("iID_MLNS")]
        public new Guid IIDMLNS { get; set; }
        [Column("iID_MLNS_Cha")]
        public new Guid? IIDMLNSCha { get; set; }
        [Column("iNamLamViec")]
        public new int? INamLamViec { get; set; }
        [Column("sCPChiTietToi")]
        public new string SCPChiTietToi { get; set; }
        [Column("sDuToanChiTietToi")]
        public new string SDuToanChiTietToi { get; set; }
        [Column("sNguoiSua")]
        public new string SNguoiSua { get; set; }
        [Column("sNguoiTao")]
        public new string SNguoiTao { get; set; }
        [Column("sNhapTheoTruong")]
        public new string SNhapTheoTruong { get; set; }
        [Column("sQuyetToanChiTietToi")]
        public new string SQuyetToanChiTietToi { get; set; }
        [Column("Tag")]
        public new string Tag { get; set; }
        [Column("sTNG1")]
        public new string STNG1 { get; set; }
        [Column("sTNG2")]
        public new string STNG2 { get; set; }
        [Column("sTNG3")]
        public new string STNG3 { get; set; }
        [Column("iLoaiNganSach")]
        public new int? ILoaiNganSach { get; set; }
        [Column("sMaCB")]
        public new string SMaCB { get; set; }
        [Column("UsedMLNS")]
        public Guid? UsedMLNS { get; set; }
        [Column("UsedCPChiTietToi")]
        public string UsedCPChiTietToi { get; set; }
        [Column("UsedDuToanChiTietToi")]
        public string UsedDuToanChiTietToi { get; set; }
        [Column("UsedDuToanDieuChinhChiTietToi")]
        public string UsedDuToanDieuChinhChiTietToi { get; set; }
        [Column("UsedQuyetToanChiTietToi")]
        public string UsedQuyetToanChiTietToi { get; set; }
        [Column("LNSID")]
        public Guid? LNSID { get; set; }
        [Column("bHangChaDuToanDieuChinh")]
        public new bool? BHangChaDuToanDieuChinh { get; set; }
        public new bool IsEditableCPChiTietToi { get; set; }
        public new bool IsUsedDuToanChiTietToi { get; set; }
        public new bool IsUsedQuyetToanChiTietToi { get; set; }
        public new bool IsUsedDuToanDieuChinhChiTietToi { get; set; }
        [NotMapped]
        public new string MergeRangeChild { get; set; }
        [NotMapped]
        public new int Rank { get; set; }

        [NotMapped]
        public new string SKhoiDonVi { get; set; }
        [NotMapped]
        public bool IsEditableStatus { get; set; }
        [NotMapped]
        public string SMaCheDo { get; set; }
        [NotMapped]
        public string STenCheDo { get; set; }

        [Column("fTyLe_BHXH_NSD")]
        public new double? FTyLeBHXHNSD { get; set; }
        [Column("fTyLe_BHXH_NLD")]
        public new double? FTyLeBHXHNLD { get; set; }
        [Column("fTyLe_BHYT_NSD")]
        public new double? FTyLeBHYTNSD { get; set; }
        [Column("fTyLe_BHYT_NLD")]
        public new double? FTyLeBHYTNLD { get; set; }
        [Column("fTyLe_BHTN_NSD")]
        public new double? FTyLeBHTNNSD { get; set; }
        [Column("fTyLe_BHTN_NLD")]
        public new double? FTyLeBHTNNLD { get; set; }
        [Column("fHeSoLayQuyLuong")]
        public new double? FHeSoLayQuyLuong { get; set; }
    }
}
