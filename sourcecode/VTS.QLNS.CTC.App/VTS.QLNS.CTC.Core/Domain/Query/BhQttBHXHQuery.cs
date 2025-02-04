using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQttBHXHQuery
    {
        [Column("iID_QTT_BHXH_ChungTu")]
        [Key]
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SMoTa { get; set; }
        public bool BIsKhoa { get; set; }
        public int IQuyNam { get; set; }
        public int IQuyNamLoai { get; set; }
        public string SQuyNamMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public string SDsMlns { get; set; }
        public int? ILoaiTongHop { get; set; }
        public int? ILoai { get; set; }
        public bool? BDaTongHop { get; set; }
        public int? IQSBQNam { get; set; }
        public double? FLuongChinh { get; set; }
        public double? FPhuCapChucVu { get; set; }
        public double? FPCTNNghe { get; set; }
        public double? FPCTNVuotKhung { get; set; }
        public double? FNghiOm { get; set; }
        public double? FHSBL { get; set; }
        public double? FTongQuyTienLuongNam { get; set; }
        public double? FDuToan { get; set; }
        public double? FDaQuyetToan { get; set; }
        public double? FConLai { get; set; }
        public double? FThuBHXHNLD { get; set; }
        public double? FThuBHXHNSD { get; set; }
        public double? FTongSoPhaiThuBHXH { get; set; }
        public double? FThuBHYTNLD { get; set; }
        public double? FThuBHYTNSD { get; set; }
        public double? FTongSoPhaiThuBHYT { get; set; }
        public double? FThuBHTNNLD { get; set; }
        public double? FThuBHTNNSD { get; set; }
        public double? FTongSoPhaiThuBHTN { get; set; }
        public double? FTongCong { get; set; }
        public double? FSoPhaiThuBHXHNLD { get; set; }
        public double? FSoPhaiThuBHXHNSD { get; set; }
        public double? FTongPhaiThuBHXH { get; set; }

    }
}
