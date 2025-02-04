using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtQtQuyetToanQuery
    {
        public Guid Id { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public Guid? IdDuAn { get; set; }
        public Guid? IdDeNghiQuyetToan { get; set; }
        public string TenDuAn { get; set; }
        public double? TongMucDauTuPheDuyet { get; set; }
        public double? TienQuyetToanPheDuyet { get; set; }
        public string MaDuAn { get; set; }
        public string UserCreate { get; set; }
        public double ChiPhiThietHai { get; set; }
        public double ChiPhiKhongTaoNenTaiSan { get; set; }
        public double TaiSanDaiHanThuocCDTQuanLy { get; set; }
        public double TaiSanDaiHanDonViKhacQuanLy { get; set; }
        public double TaiSanNganHanThuocCDTQuanLy { get; set; }
        public double TaiSanNganHanDonViKhacQuanLy { get; set; }
        public bool BKhoa { get; set; }
        public string MaDonVi { get; set; }
        public string sKhoiCong { get; set; }
        public string sKetThuc { get; set; }
    }
}
