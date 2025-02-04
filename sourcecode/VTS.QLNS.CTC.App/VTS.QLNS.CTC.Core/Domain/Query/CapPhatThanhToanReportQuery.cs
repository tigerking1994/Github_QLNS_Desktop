using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class CapPhatThanhToanReportQuery
    {
        public string TenDuAn { get; set; }
        public string MaDuAn { get; set; }
        public string TenDonVi { get; set; }
        public string TenChuDauTu { get; set; }
        public string TenHopDong { get; set; }
        public DateTime? NgayHopDong { get; set; }
        public string SoDeNghi { get; set; }
        public int LoaiThanhToan { get; set; }
        public string TenNguonVon { get; set; }
        public int NamKeHoach { get; set; }
        public double ThanhToanTN { get; set; }
        public double ThanhToanNN { get; set; }
        public double ThuHoiTN { get; set; }
        public double ThuHoiNN { get; set; }
        public string NoiDung { get; set; }
        public double GiaTriHopDong { get; set; }
        public double ThueGiaTriGiaTang { get; set; }
        public double ChuyenTienBaoHanh { get; set; }
        public string TenNhaThau { get; set; }
        public string SoTaiKhoanNhaThau { get; set; }
        public string duToanPheDuyet { get; set; }
        public DateTime khvNgayQuyetDinh { get; set; }
        public DateTime thanhtoanNgayDeNghi { get; set; }
        public string SBanQuanLyDuAn { get; set; }
    }
}
