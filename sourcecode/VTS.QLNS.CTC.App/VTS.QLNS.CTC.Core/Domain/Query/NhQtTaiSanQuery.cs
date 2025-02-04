using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhQtTaiSanQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SMaTaiSan { get; set; }
        public string STenTaiSan { get; set; }
        public Guid? IIdLoaiTaiSan { get; set; }
        public string SMoTaTaiSan { get; set; }
        public DateTime? DNgayBatDauSuDung { get; set; }
        public int? ITrangThai { get; set; }
        public int? ILoaiTaiSan { get; set; }
        public int? ITinhTrangSuDung { get; set; }
        public double? FSoLuong { get; set; }
        public double? FNguyenGia { get; set; }
        public Guid IIdMaDonViId { get; set; }
        public Guid IIdHopDongId { get; set; }
        public string SDonViTinh { get; set; }
        public string STenLoaiTaiSan { get; set; }
        public string STenDonVi { get; set; }
        public string STenDuAn { get; set; }
        public string STenHopDong { get; set; }
        public double? FTaiSan1 { get; set; }
        public double? FTaiSan2 { get; set; }
        public double? FTrangThai1 { get; set; }
        public double? FTrangThai2 { get; set; }
        public double? FTrangThai3 { get; set; }
        public double? FTinhTrangSuDung1 { get; set; }
        public double? FTinhTrangSuDung2 { get; set; }
        public double? FTinhTrangSuDung3 { get; set; }
        public string STT { get; set; }
    }
}
