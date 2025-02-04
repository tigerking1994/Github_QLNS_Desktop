using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlQsKeHoachChiTietModel : ModelBase
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public double? FSoBinhNhi { get; set; }
        public decimal? FPcrqBinhNhi { get; set; }
        public double? FSoBinhNhat { get; set; }
        public decimal? FPcrqBinhNhat { get; set; }
        public double? FSoHaSi { get; set; }
        public decimal? FPcrqHaSi { get; set; }
        public double? FSoTrungSi { get; set; }
        public decimal? FPcrqTrungSi { get; set; }
        public double? FSoThuongSi { get; set; }
        public decimal? FPcrqThuongSi { get; set; }
        public double? FSoThieuUy { get; set; }
        public double? FSoTrungUy { get; set; }
        public double? FSoThuongUy { get; set; }
        public double? FSoDaiUy { get; set; }
        public double? FSoThieuTa { get; set; }
        public double? FSoTrungTa { get; set; }
        public double? FSoThuongTa { get; set; }
        public double? FSoDaiTa { get; set; }
        public double? FSoTuong { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double? FSoQncn { get; set; }
    }
}
