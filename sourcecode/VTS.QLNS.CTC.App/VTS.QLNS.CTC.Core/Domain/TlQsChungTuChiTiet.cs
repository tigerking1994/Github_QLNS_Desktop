using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlQsChungTuChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public string IdChungTu { get; set; }
        public string MlnsId { get; set; }
        public string MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public int? Thang { get; set; }
        public int NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? ThieuUy { get; set; }
        public double? TrungUy { get; set; }
        public double? ThuongUy { get; set; }
        public double? DaiUy { get; set; }
        public double? ThieuTa { get; set; }
        public double? TrungTa { get; set; }
        public double? ThuongTa { get; set; }
        public double? DaiTa { get; set; }
        public double? Tuong { get; set; }
        public double? BinhNhi { get; set; }
        public double? BinhNhat { get; set; }
        public double? HaSi { get; set; }
        public double? TrungSi { get; set; }
        public double? ThuongSi { get; set; }
        public double? Qncn { get; set; }
        public double? Vcqp { get; set; }
        public double? Cnqp { get; set; }
        public double? Ldhd { get; set; }
        public double? Ccqp { get; set; }
        public double? TongSo { get; set; }
        public string GhiChu { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public double? ThieuUyCn { get; set; }
        public double? TrungUyCn { get; set; }
        public double? ThuongUyCn { get; set; }
        public double? DaiUyCn { get; set; }
        public double? ThieuTaCn { get; set; }
        public double? TrungTaCn { get; set; }
        public double? ThuongTaCn { get; set; }
    }
}
