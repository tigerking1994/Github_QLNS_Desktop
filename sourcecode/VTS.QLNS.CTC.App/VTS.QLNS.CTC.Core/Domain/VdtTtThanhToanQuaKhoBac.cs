using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtThanhToanQuaKhoBac : EntityBase
    {
        public VdtTtThanhToanQuaKhoBac()
        {
            VdtTtThanhToanQuaKhoBacChiTiets = new HashSet<VdtTtThanhToanQuaKhoBacChiTiet>();
        }
        public Guid Id { get; set; }
        public string SNguoiLap { get; set; }
        public string SSoThanhToan { get; set; }
        public DateTime? DNgayThanhToan { get; set; }
        public Guid? IIdDonViNhanThanhToanId { get; set; }
        public string IIdMaDonViNhanThanhToanID { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLyID { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdLoaiNganSach { get; set; }
        public Guid? IIdKhoanNganSach { get; set; }
        public double? FGiaTriThanhToan { get; set; }
        public double? FGiaTriTamUng { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGia { get; set; }
        public string SGhiChu { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        [NotMapped]
        public List<Guid> lstDuAnId { get; set; }

        public virtual ICollection<VdtTtThanhToanQuaKhoBacChiTiet> VdtTtThanhToanQuaKhoBacChiTiets { get; set; }
    }
}
