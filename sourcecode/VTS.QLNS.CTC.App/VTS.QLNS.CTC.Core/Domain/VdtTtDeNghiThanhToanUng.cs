using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtDeNghiThanhToanUng : EntityBase
    {
        public VdtTtDeNghiThanhToanUng()
        {
            VdtTtDeNghiThanhToanUngChiTiets = new HashSet<VdtTtDeNghiThanhToanUngChiTiet>();
        }

        public Guid Id { get; set; }
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIDMaDonViQuanLy { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public string SNguoiLap { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdLoaiNganSach { get; set; }
        public Guid? IIdKhoanNganSach { get; set; }
        public double? FGiaTriThanhToan { get; set; }
        public double? FGiaTriTamUng { get; set; }
        public double? FGiaTriThuHoiUngNgoaiChiTieu { get; set; }
        public double? FGiaTriThuHoi { get; set; }
        public string SGhiChu { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        [NotMapped]
        public List<Guid> lstDuAnId { get; set; }

        public virtual ICollection<VdtTtDeNghiThanhToanUngChiTiet> VdtTtDeNghiThanhToanUngChiTiets { get; set; }
    }
}
