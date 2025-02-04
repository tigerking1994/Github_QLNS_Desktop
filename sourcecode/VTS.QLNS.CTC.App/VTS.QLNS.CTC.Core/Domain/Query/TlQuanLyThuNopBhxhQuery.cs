using System;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlQuanLyThuNopBhxhQuery
    {
        public Guid Id { get; set; }
        public string STen { get; set; }
        public int? ILoai { get; set; }
        public DateTime? DTuNgay { get; set; }
        public DateTime? DDenNgay { get; set; }
        public string SMaPban { get; set; }
        public string IIdMaDonVi { get; set; }
        public decimal? IThang { get; set; }
        public decimal? INam { get; set; }
        public int? ISoTt { get; set; }
        public string SMaCachTl { get; set; }
        public bool? Status { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public string SMoTa { get; set; }
        public bool BIsKhoa { get; set; }
        public bool IsTongHop { get; set; }
        public string STongHop { get; set; }
        public string STenDonVi { get; set; }
    }
}
