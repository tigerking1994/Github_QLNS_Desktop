using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhQuyetDinhDamPhamQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdKhTongTheNhiemVuChiId { get; set; }
        public Guid? IIdDonViThucHien { get; set; }
        public Guid? IIdDonViQuanLy { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdPhuongAnNhapKhauId { get; set; }
        public Guid? IIdGocId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTaChiTietQuyetDinh { get; set; }
        public int? ILoaiQuyetDinh { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public DateTime DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool? BIsXoa { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public int? ILoai { get; set; }

        // Another properties
        public Guid? IIdKhTongTheId { get; set; }
        public string SPhuongAnNhapKhau { get; set; }
        public string STenChuongTrinh { get; set; }
        public string STenDonVi { get; set; }
        public string DieuChinhTu { get; set; }
        public double? FTiGiaNhap { get; set; }
        public string STenDuAn { get; set; }
    }
}
