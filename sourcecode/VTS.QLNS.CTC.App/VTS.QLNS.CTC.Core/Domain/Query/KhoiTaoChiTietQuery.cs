using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KhoiTaoChiTietQuery
    {
        public Guid Id { get; set; }
        public Guid? IdDb { get; set; }
        public int MaNguonNganSach { get; set; }
        public string TenNganSach { get; set; }
        public string MoTaNganSach { get; set; }
        public Guid? IdKhoiTaoID { get; set; }
        public int? IdNguonVonID { get; set; }
        public Guid? IdLoaiNguonVonID { get; set; }
        public double KHVonHetNamTruoc { get; set; }
        public double LuyKeThanhToanKLHT { get; set; }
        public double LuyKeThanhToanTamUng { get; set; }
        public double ThanhToanQuaKB { get; set; }
        public double TamUngQuaKB { get; set; }
        public Guid? IdDonViTienTeID { get; set; }
        public double TiGiaDonVi { get; set; }
        public Guid? IdTienTeID { get; set; }
        public double TiGia { get; set; }
        public string IdMucID { get; set; }
        public string IdTieuMucID { get; set; }
        public string IdTietMucID { get; set; }
        public string IdNganhID { get; set; }
        public double SoChuyenChiTieuDaCap { get; set; }
        public double SoChuyenChiTieuChuaCap { get; set; }
    }
}
