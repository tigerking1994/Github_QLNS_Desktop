using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTTM_BHYT_ThanNhan")]
    public class BhDtTmBHYTTN : EntityBase
    {
        [Column("iID_DTTM_BHYT_ThanNhan")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? ILoaiDuToan { get; set; }
        public string SMoTa { get; set; }
        public string SDSLNS { get; set; }
        public double? FDuToan { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public bool? BIsKhoa { get; set; }
        public int? INamLamViec { get; set; }
        public string IIDMaDonVi { get; set; }
        public Guid? IID_DonVi { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
    }
}

