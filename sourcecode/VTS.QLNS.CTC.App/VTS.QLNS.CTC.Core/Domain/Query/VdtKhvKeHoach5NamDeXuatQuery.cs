using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamDeXuatQuery
    {
        public Guid Id { get; set; }
        public Guid IIdDonViId { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public string STrangThai { get; set; }
        public double? FGiaTriKeHoach { get; set; }
        public DateTime? DDateCreate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SUserDelete { get; set; }
        public string SUserCreate { get; set; }
        public string SoKeHoach { get; set; }
        public DateTime? NgayLap { get; set; }
        public int GiaiDoanTu { get; set; }
        public int GiaiDoanDen { get; set; }
        public string STenDonVi { get; set; }
        public string SoLanDC { get; set; }
        public int? ILoai { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? NamLamViec { get; set; }
        public string MoTaChiTiet { get; set; }
        public string DieuChinhTu { get; set; }
        public int TotalFiles { get; set; }
        public bool BKhoa { get; set; }
        public string STongHop { get; set; }
        public Guid? IIdTongHop { get; set; }
    }
}
