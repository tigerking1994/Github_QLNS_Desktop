using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DM_MucLucNganSach")]
    public partial class BhDmMucLucNganSach : EntityBase
    {
        [Column("iID")]
        [Key]
        public override Guid Id { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        [StringLength(50)]
        public string SLNS { get; set; }
        [Column("sL")]
        public string SL { get; set; }
        [Column("sK")]
        public string SK { get; set; }

        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STM { get; set; }
        [Column("sTTM")]
        public string STTM { get; set; }
        [Column("sNG")]
        public string SNG { get; set; }
        [Column("sTNG")]
        public string STNG { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("iTrangThai")]
        public int? ITrangThai { get; set; }
        [Column("bDuPhong")]
        public bool BDuPhong { get; set; }
        [StringLength(50)]
        [Column("bHangChaDuToan")]
        public bool? BHangChaDuToan { get; set; }
        [Column("bHangChaQuyetToan")]
        public bool? BHangChaQuyetToan { get; set; }
        [StringLength(50)]
        [Column("bHangMua")]
        public bool BHangMua { get; set; }
        [StringLength(250)]
        [Column("bHangNhap")]
        public bool BHangNhap { get; set; }
        [Column("bHienVat")]
        public bool BHienVat { get; set; }

        [Column("bNgay")]
        public bool BNgay { get; set; }
        [Column("bPhanCap")]
        public bool BPhanCap { get; set; }
        [Column("bSoNguoi")]
        public bool BSoNguoi { get; set; }
        [Column("bTonKho")]
        public bool BTonKho { get; set; }
        [Column("bTuChi")]
        public bool BTuChi { get; set; }
        [Column("sChiTietToi")]
        public string SChiTietToi { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("iLoai")]
        public string ILoai { get; set; }
        [Column("iLock")]
        public bool? ILock { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        [Column("iID_MaBQuanLy")]
        public string IIDMaBQuanLy { get; set; }
        public string Log { get; set; }
        public Guid IIDMLNS { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIDMLNSCha { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("sCPChiTietToi")]
        public string SCPChiTietToi { get; set; }
        [Column("sDuToanChiTietToi")]
        public string SDuToanChiTietToi { get; set; }
        [Column("sDuToanDieuChinhChiTietToi")]
        public string SDuToanDieuChinhChiTietToi { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNhapTheoTruong")]
        public string SNhapTheoTruong { get; set; }
        [Column("sQuyetToanChiTietToi")]
        public string SQuyetToanChiTietToi { get; set; }
        [Column("Tag")]
        public string Tag { get; set; }
        [Column("sTNG1")]
        public string STNG1 { get; set; }
        [Column("sTNG2")]
        public string STNG2 { get; set; }
        [Column("sTNG3")]
        public string STNG3 { get; set; }
        [Column("iLoaiNganSach")]
        public int? ILoaiNganSach { get; set; }
        [Column("sMaCB")]
        public string SMaCB { get; set; }
        [Column("sMaPhuCap")]
        public string SMaPhuCap { get; set; }
        [Column("bHangChaDuToanDieuChinh")]
        public bool? BHangChaDuToanDieuChinh { get; set; }
        [Column("iDonViTinh")]
        public int? IDonViTinh { get; set; }
        [Column("sNS_LuongChinh")]
        public string SNS_LuongChinh { get; set; }
        [Column("sNS_PCCV")]
        public string SNS_PCCV { get; set; }
        [Column("sNS_PCTN")]
        public string SNS_PCTN { get; set; }
        [Column("sNS_PCTNVK")]
        public string SNS_PCTNVK { get; set; }
        [Column("sNS_HSBL")]
        public string SNS_HSBL { get; set; }
        [Column("sLuongChinh")]
        public string SLuongChinh { get; set; }
        [Column("sPCCV")]
        public string SPCCV { get; set; }
        [Column("sPCTN")]
        public string SPCTN { get; set; }
        [Column("sPCTNVK")]
        public string SPCTNVK { get; set; }
        [NotMapped]
        public ICollection<BhDmMucLucNganSach> Children { get; set; }

        [NotMapped]
        public string MergeRangeChild { get; set; }
        [NotMapped]
        public int Rank { get; set; }

        [NotMapped]
        public string SKhoiDonVi { get; set; }
        public BhDmMucLucNganSach Clone()
        {
            return (BhDmMucLucNganSach)this.MemberwiseClone();
        }
        [NotMapped]
        public string STenCSYT { get; set; }
        [NotMapped]
        public Guid IIDCoSoYTe { get; set; }
        [NotMapped]
        public string IIDMaCoSoYTe { get; set; }

        [Column("fTyLe_BHXH_NSD")]
        public double? FTyLeBHXHNSD { get; set; }
        [Column("fTyLe_BHXH_NLD")]
        public double? FTyLeBHXHNLD { get; set; }
        [Column("fTyLe_BHYT_NSD")]
        public double? FTyLeBHYTNSD { get; set; }
        [Column("fTyLe_BHYT_NLD")]
        public double? FTyLeBHYTNLD { get; set; }
        [Column("fTyLe_BHTN_NSD")]
        public double? FTyLeBHTNNSD { get; set; }
        [Column("fTyLe_BHTN_NLD")]
        public double? FTyLeBHTNNLD { get; set; }
        [Column("fHeSoLayQuyLuong")]
        public double? FHeSoLayQuyLuong { get; set; }
    }
}
