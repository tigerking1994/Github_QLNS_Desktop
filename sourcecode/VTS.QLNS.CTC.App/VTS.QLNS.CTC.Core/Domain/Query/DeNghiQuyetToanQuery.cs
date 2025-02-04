using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DeNghiQuyetToanQuery
    {
        public Guid Id { get; set; }
        public string MaDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string TenChuDauTu { get; set; }
        public string SoBaoCao { get; set; }
        public DateTime? ThoiGianKhoiCong { get; set; }
        public DateTime? ThoiGianHoanThanh { get; set; }
        public double GiaTriDeNghiQuyetToan { get; set; }
        public double ChiPhiThietHai { get; set; }
        public double ChiPhiKhongTaoNenTaiSan { get; set; }
        public double TaiSanDaiHanThuocCDTQuanLy { get; set; }
        public double TaiSanDaiHanDonViKhacQuanLy { get; set; }
        public double TaiSanNganHanThuocCDTQuanLy { get; set; }
        public double TaiSanNganHanDonViKhacQuanLy { get; set; }
        public DateTime? ThoiGianBaoCao { get; set; }
        public string NguoiTao { get; set; }
        public bool IsLock { get; set; }
        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }
        public string IdLoaiQuyetToan { get; set; }
    }
}
