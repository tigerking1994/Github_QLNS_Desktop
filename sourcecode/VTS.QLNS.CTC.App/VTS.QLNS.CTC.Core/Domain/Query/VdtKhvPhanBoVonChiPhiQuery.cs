using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvPhanBoVonChiPhiQuery
    {
        public Guid Id { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string STenNguonVon { get; set; }
        public string STenDonVi { get; set; }
        public int? ILanDieuChinh { get; set; }
        public string DieuChinhTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IIdLoaiNguonVonId { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SLoaiDieuChinh { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public double? FGiaTriDuocDuyet { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdPhanBoGocChiPhiId { get; set; }
        public bool? BKhoa { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
    }
}
