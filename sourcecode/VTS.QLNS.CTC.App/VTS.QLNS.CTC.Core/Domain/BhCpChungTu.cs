using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhCpChungTu : EntityBase
    {
        [Column("iID_CP_ChungTu")]
        [Key]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public string SID_MaDonVi { get; set; }
        public string SLNS { get; set; }
        public Guid? IID_LoaiCap { get; set; }
        public double? FTienDaCap { get; set; }
        public double? FTienKeHoachCap { get; set; }
        public double? FTienDuToan { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public bool BIsKhoa { get; set; }
        public Guid IID_TongHop { get; set; }
        public int ILoaiTongHop { get; set; }
        public string STongHop { get; set; }
        public int IQuy { get; set; }
        public double? FTyLeThu { get; set; }
    }
}
