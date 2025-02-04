using System;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaQuyetDinhKhacQuery : EntityBase
    {
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string STenDonVi { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string STenQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
        public string SMoTa { get; set; }
        public string STenChuongTrinh { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool? BIsActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public string SDieuChinhTu { get; set; }
        public bool BIsXoa { get; set; }
        public string Title { get; set; }
        public Guid? IIdParentId { get; set; }
    }
}
