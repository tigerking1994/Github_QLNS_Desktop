using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtPheDuyetThanhToan : EntityBase
    {
        public Guid Id { get; set; }
        public Guid IIdDeNghiThanhToan { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public string SNguoiLap { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IIdNguonVonId { get; set; }
        public int ILoaiThanhToan { get; set; }
        public Guid IIdDuAnId { get; set; }
        public Guid IIdHopDongId { get; set; }
        public string SGhiChu { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
    }
}
