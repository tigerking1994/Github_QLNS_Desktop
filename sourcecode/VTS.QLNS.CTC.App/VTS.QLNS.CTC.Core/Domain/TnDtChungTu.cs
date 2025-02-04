using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TnDtChungTu : EntityBase
    {
        public Guid Id { get; set; }
        public string SoChungTu { get; set; }
        public int? SoChungTuIndex { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string Lns { get; set; }
        public int ILoai { get; set; }
        public int? NamNganSach { get; set; }
        public int? NguonNganSach { get; set; }
        public int? LoaiNganSach { get; set; }
        public int? NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public double TuChiSum { get; set; }
        public bool IsLocked { get; set; }
        public string MoTaChiTiet { get; set; }
        public int IDot { get; set; }
        public string IdDonViTao { get; set; }
        public int? IGuiNhan { get; set; }
        public string IdDotNhan { get; set; }
        public int? LoaiChungTu { get; set; }
        public string SoLieuNhap { get; set; }
        public int? IKiemDuyet { get; set; }
        public string ITongHop { get; set; }
    }
}
