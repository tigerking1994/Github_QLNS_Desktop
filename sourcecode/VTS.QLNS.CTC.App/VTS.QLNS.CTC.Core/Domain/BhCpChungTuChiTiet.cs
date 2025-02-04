using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhCpChungTuChiTiet : EntityBase
    {
        [Column("iID_CP_ChungTu_ChiTiet")]
        [Key]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public Guid? IID_CP_ChungTu { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SNoiDung { get; set; }
        public double? FTienDuToan { get; set; }
        public double? FTienKeHoachCap { get; set; }
        public double? FTienDaCap { get; set; }
        public Guid? IID_DonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        public string SGhiChu { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SXauNoiMa { get; set; }
        public int? INamLamViec { get;set; }
    }
}
