using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDuToanQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdQdDauTuId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public int? ILoai { get; set; }
        public int? IdLoaiDuToan { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string STenChuongTrinh { get; set; }
        public Guid? IIdTiGiaUsdVndId { get; set; }
        public Guid? IIdTiGiaUsdEurId { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
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
        public bool BIsXoa { get; set; }
        public Guid? IIdDuToanGocId { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public Guid? IIdKHTongTheID { get; set; }

        // Another properties
        public string STenDonVi { get; set; }
        public string STenDuAn { get; set; }
        public int TotalFiles { get; set; }
        public string SDieuChinhTu { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public double? FTiGiaNhap { get; set; }
    }
}
