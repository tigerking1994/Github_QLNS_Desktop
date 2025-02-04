using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhQtTaiSan : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SMaTaiSan { get; set; }
        public string STenTaiSan { get; set; }
        public Guid? IIdLoaiTaiSan { get; set; }
        public Guid? IIdChungTuTaiSanId { get; set; }
        public string SMoTaTaiSan { get; set; }
        public DateTime? DNgayBatDauSuDung { get; set; }
        public int? ITrangThai { get; set; }
        public int? ILoaiTaiSan { get; set; }
        public int? ITinhTrangSuDung { get; set; }
        public double? FSoLuong { get; set; }
        public double? FNguyenGia { get; set; }
        public Guid? IIdMaDonViId { get; set; }
        public Guid IIdHopDongId { get; set; }
        public string SDonViTinh { get; set; }
        //Another properties 

    }
}
