using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_KHT_BHXH")]
    public partial class BhKhtBHXH : EntityBase
    {
        [Column("iID_KHT_BHXH")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string IID_MaDonVi { get; set; }
        public string SMoTa { get; set; }
        public bool BIsKhoa { get; set; }
        public double? FTongKeHoach { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public Guid? IIDTongHopID { get; set; }
        public int? ILoaiTongHop { get; set; }
        public int? IQSBQNam { get; set; }
        public double? FLuongChinh { get; set; }
        public double? FPhuCapChucVu { get; set; }
        public double? FPCTNNghe { get; set; }
        public double? FPCTNVuotKhung { get; set; }
        public double? FNghiOm { get; set; }
        public double? FHSBL { get; set; }
        public double? FTongQTLN { get; set; }
        public double? FThuBHXHNLDDong { get; set; }
        public double? FThuBHXHNSDDong { get; set; }
        public double? FThuBHXH { get; set; }
        public double? FThuBHYTNLDDong { get; set; }
        public double? FThuBHYTNSDDong { get; set; }
        public double? FTongBHYT { get; set; }
        public double? FThuBHTNNLDDong { get; set; }
        public double? FThuBHTNNSDDong { get; set; }
        public double? FThuBHTN { get; set; }
        public double? FTong { get; set; }
        public bool? BDaTongHop { get; set; }
        public string SBangLuongKeHoach { get; set; }

    }
}
