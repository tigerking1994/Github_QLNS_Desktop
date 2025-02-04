using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvKeHoach5Nam : EntityBase
    {
        [Column("iID_KeHoach5NamID")]
        public override Guid Id { get; set; }
        public Guid IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        //public Guid? IIdNhomQuanLyId { get; set; }
        public int IGiaiDoanTu { get; set; }
        public int IGiaiDoanDen { get; set; }
        //public Guid? IIdLoaiNguonVonId { get; set; }
        //public Guid? IIdLoaiNganSachId { get; set; }
        //public Guid? IIdKhoanNganSachId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool BKhoa { get; set; }
        //public bool? BLaThayThe { get; set; }
        //public string SLoaiDieuChinh { get; set; }
        public string STrangThai { get; set; }
        public double? FGiaTriDuocDuyet { get; set; }
        //public Guid IIdDonViTienTeId { get; set; }
        //public double? FTiGiaDonVi { get; set; }
        //public Guid IIdTienTeId { get; set; }
        //public double FTiGia { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SUserDelete { get; set; }
        //public int? IIdNguonVonId { get; set; }
        public int? ILoai { get; set; }
        public int? NamLamViec { get; set; }
        public string MoTaChiTiet { get; set; }
        public Guid? IIDKhthDeXuat { get; set; }
    }
}
