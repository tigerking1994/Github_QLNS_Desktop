using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhKhtBHXHChiTietQuery
    {
        [Column("iID_KHT_BHXHChiTiet")]
        [Key]
        public Guid Id { get; set; }
        [Column("iID_KHT_BHXH")]
        public Guid KhtBHXHId { get; set; }
        [Column("iID_LoaiDoiTuong")]
        public Guid? IIDLoaiDoiTuong { get; set; }
        [Column("sTenLoaiDoiTuong")]
        public string STenLoaiDoiTuong { get; set; }
        [Column("iQSBQNam")]
        public int? IQSBQNam { get; set; }
        [Column("fLuongChinh")]
        public double? FLuongChinh { get; set; }
        [Column("fPhuCapChucVu")]
        public double? FPhuCapChucVu { get; set; }
        [Column("fPCTNNghe")]
        public double? FPCTNNghe { get; set; }
        [Column("fPCTNVuotKhung")]
        public double? FPCTNVuotKhung { get; set; }
        [Column("fNghiOm")]
        public double? FNghiOm { get; set; }
        [Column("fHSBL")]
        public double? FHSBL { get; set; }
        [Column("fTongQTLN")]
        public double? FTongQuyTienLuongNam { get; set; }
        [Column("fThu_BHXH_NLD")]
        public double? FThuBHXHNguoiLaoDong { get; set; }
        [Column("fThu_BHXH_NSD")]
        public double? FThuBHXHNguoiSuDungLaoDong { get; set; }
        [Column("fTongThuBHXH")]
        public double? FTongThuBHXH { get; set; }
        [Column("fThu_BHYT_NLD")]
        public double? FThuBHYTNguoiLaoDong { get; set; }
        [Column("fThu_BHYT_NSD")]
        public double? FThuBHYTNguoiSuDungLaoDong { get; set; }
        [Column("fTongThuBHYT")]
        public double? FTongThuBHYT { get; set; }
        [Column("fThu_BHTN_NLD")]
        public double? FThuBHTNNguoiLaoDong { get; set; }
        [Column("fThu_BHTN_NSD")]
        public double? FThuBHTNNguoiSuDungLaoDong { get; set; }
        [Column("fTongThuBHTN")]
        public double? FTongThuBHTN { get; set; }
        [Column("fTongCong")]
        public double? FTongCong { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        public string Stt { get; set; }
        public string SMoTa { get; set; }
        public string SKyHieu { get; set; }
        [Column("iID_MucLucNganSach")]
        public Guid IIDMucLucNganSach { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid IdParent { get; set; }
        public string SM { get; set; }
        public string Nganh { get; set; }
        public bool IsAdd { get; set; }
        public bool IsAuToFillTuChi { get; set; }
        public string STenBhMLNS { get; set; }
        public string MaPhuCap { get; set; }
        public int Nam { get; set; }
        public double GiaTri { get; set; }
        public string XauNoiMa { get; set; }
        public int QSBQ { get; set; }
        public int? INamLamViec { get; set; }
        public bool IsHangCha { get; set; }
        public bool BHangCha => IsHangCha;
        public string SXauNoiMa { get; set; }
        public double? FTyLeBHXHNSD { get; set; }
        public double? FTyLeBHXHNLD { get; set; }
        public double? FTyLeBHYTNSD { get; set; }
        public double? FTyLeBHYTNLD { get; set; }
        public double? FTyLeBHTNNSD { get; set; }
        public double? FTyLeBHTNNLD { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string SLNS { get; set; }
        public bool HasDataToPrint => IQSBQNam.GetValueOrDefault() != 0 || FLuongChinh.GetValueOrDefault() != 0 || FPhuCapChucVu.GetValueOrDefault() != 0 ||
            FPCTNNghe.GetValueOrDefault() != 0 || FPCTNVuotKhung.GetValueOrDefault() != 0 || FNghiOm.GetValueOrDefault() != 0 ||
            FHSBL.GetValueOrDefault() != 0 || FThuBHXHNguoiLaoDong.GetValueOrDefault() != 0 || FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault() != 0 ||
            FThuBHYTNguoiLaoDong.GetValueOrDefault() != 0 || FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault() != 0 || FThuBHTNNguoiLaoDong.GetValueOrDefault() != 0 ||
            FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault() != 0;
    }
}
