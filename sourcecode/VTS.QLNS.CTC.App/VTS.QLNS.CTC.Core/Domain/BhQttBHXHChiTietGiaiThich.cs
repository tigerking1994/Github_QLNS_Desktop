using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_QTT_BHXH_CTCT_GiaiThich")]
    public partial class BhQttBHXHChiTietGiaiThich : EntityBase
    {
        [Column("iID_QT_CTCT_GiaiThich")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? QttBHXHId { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DTuNgay { get; set; }
        public DateTime? DDenNgay { get; set; }
        public string IIDMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public int IQuyNam { get; set; }
        public int IQuyNamLoai { get; set; }
        public string SQuyNamMoTa { get; set; }
        public string SLNS { get; set; }
        public Guid? IIDMLNS { get; set; }
        public string SXauNoiMa { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SNoiDung { get; set; }
        public string SKienNghi { get; set; }
        public string SLoaiThu { get; set; }
        public int? IQuanSo { get; set; }
        public double? FPhaiNopBHXH { get; set; }
        public double? FPhaiNopTrongQuyNam { get; set; }
        public double? FTruyThuQuyNamTruoc { get; set; }
        public double? FPhaiNopQuyNamTruoc { get; set; }
        public double? FDaNopTrongQuyNam { get; set; }
        public double? FConPhaiNopTiep { get; set; }
        public double? FTruyThuBHXHNLD { get; set; }
        public double? FTruyThuBHXHNSD { get; set; }
        public double? FTruyThuBHXHTongCong { get; set; }
        public double? FTruyThuBHYTNLD { get; set; }
        public double? FTruyThuBHYTNSD { get; set; }
        public double? FTruyThuBHYTTongCong { get; set; }
        public double? FTruyThuBHTNNLD { get; set; }
        public double? FTruyThuBHTNNSD { get; set; }
        public double? FTruyThuBHTNTongCong { get; set; }
        public double? FTongTruyThuBHXH { get; set; }
        public double? FSoPhaiThuNop { get; set; }
        public double? FSoDaNopTrongNam { get; set; }
        public double? FSoDaNopSau3112 { get; set; }
        public double? FTongSoDaNop { get; set; }
        public double? FSoConPhaiNop { get; set; }
        public double? FQuyTienLuongCanCu { get; set; }
        public double? FSoTienGiamDong { get; set; }
        public double? FLuongChinh { get; set; }
        public double? FPCChucVu { get; set; }
        public double? FPCTNNghe { get; set; }
        public double? FPCTNVuotKhung { get; set; }
        public double? FNghiOm { get; set; }
        public double? FHSBL { get; set; }
        public int? ILoaiGiaiThich { get; set; }
        [NotMapped]
        public bool BHangCha { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public int ISTT { get; set; }
        [NotMapped]
        public Guid IIDMLNSCha { get; set; }
    }
}
